using System;
using System.Data;
using Starcounter;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace BDB2
{
    [Database]
    public class BB   // Base
    {
        public ulong PK { get; set; }
        public string Ad { get; set; }
        public string Info { get; set; }
    }

    [Database]
    public class PP : BB   // Players
    {
        public string Sex { get; set; }
        public string Tel { get; set; }
        public int RnkBaz { get; set; }
        public int RnkSon { get; set; }
        public int RnkIdx { get; set; }     // RnkSon'a gore dizildiginde Sirasi
        public int ActLvl { get; set; }     // Aktif ise 0, degilse > 0 (Global listede en altta goster)

        public int SST => SSW + SSL;
        public int SSW { get; set; }
        public int SSL { get; set; }

        public int SMT => SMW + SML;
        public int SMW { get; set; }
        public int SML { get; set; }

        public int DST => DSW + DSL;
        public int DSW { get; set; }
        public int DSL { get; set; }

        public int DMT => DMW + DML;
        public int DMW { get; set; }
        public int DML { get; set; }

        public static void RefreshStat()
        {
            var pps = Db.SQL<PP>("select r from PP r");
            foreach(var pp in pps)
            {
                RefreshStat(pp);
            }
        }

        public static void RefreshStat(PP pp)
        {
            int SSW = 0, DSW = 0, SMW = 0, DMW = 0;
            int SSL = 0, DSL = 0, SML = 0, DML = 0;

            var hmacs = Db.SQL<MAC>("select r from MAC r where r.hPP1 = ? or r.hPP2 = ?", pp, pp);
            foreach (var mac in hmacs)
            {
                if (mac.SoD == "S")
                {
                    SSW += mac.hSW;     // Single Set Win
                    SSL += mac.gSW;     //            Lost
                    SMW += mac.hMW;     //        Mac
                    SML += mac.gMW;
                }
                if (mac.SoD == "D")
                {
                    DSW += mac.hSW;
                    DSL += mac.gSW;
                    DMW += mac.hMW;
                    DML += mac.gMW;
                }
            }
            var gmacs = Db.SQL<MAC>("select r from MAC r where r.gPP1 = ? or r.gPP2 = ?", pp, pp);
            foreach (var mac in gmacs)
            {
                if (mac.SoD == "S")
                {
                    SSW += mac.gSW;
                    SSL += mac.hSW;
                    SMW += mac.gMW;
                    SML += mac.hMW;
                }
                if (mac.SoD == "D")
                {
                    DSW += mac.gSW;
                    DSL += mac.hSW;
                    DMW += mac.gMW;
                    DML += mac.hMW;
                }
            }
            Db.TransactAsync(() =>
            {
                pp.SSW = SSW;
                pp.SSL = SSL;
                pp.SMW = SMW;
                pp.SML = SML;
                pp.DSW = DSW;
                pp.DSL = DSL;
                pp.DMW = DMW;
                pp.DML = DML;
            });
        }

    }

    [Database]
    public class CC : BB  // Competitions
    {
        public string Skl { get; set; }     // Takim/Ferdi
        public string Grp { get; set; }     // Ligdeki Grup oyuncularini bilmek icin
        public bool IsRun { get; set; }     // Cari, Devam eden (False:Bitti)
        public bool IsRnkd { get; set; }    // Rank hesaplnacak mi?
    }

    // Takim devre icinde diskalifiye edilebilir, bu durumdan sonraki Musabakalarinda Hukmen maglup olur
    [Database]
    public class CT : BB  // Takimlar
    {
        public CC CC { get; set; }
        public string Adres { get; set; }     // Takim/Ferdi
        public PP K1 { get; set; }
        public PP K2 { get; set; }

        public bool isDsk { get; set; }     // Diskalifiye/Ihrac

        public int NG { get; set; }         // Nof Galibiyet
        public int NM { get; set; }         //     Malubiyet
        public int NB { get; set; }         //     Beraberlik
        public int NT => NG + NM + NB;      //     Toplam
        public int NX { get; set; }         //     Oynamadi/Gelmedi

        public int KA { get; set; }         // sKor Aldigi
        public int KV { get; set; }         //      Verdigi
        public int KF => KA - KV;           //      Fark = Alidigi - Verdigi

        public int PW { get; set; }         // Puan

        public string K1Ad => K1 == null ? "-" : $"{K1.Ad} ({K1.Tel})";
        public string K2Ad => K2 == null ? "-" : $"{K2.Ad} ({K2.Tel})";

        // Diskalifiye edildikten sonraki Eventlerini update
        // Takimin Yaptigi Eventleri toplayarak Sonuclari update
        public static void RefreshSonuc(CT ct)
        {
            int NG = 0,
                NM = 0,
                NB = 0,
                NX = 0,
                KA = 0,
                KV = 0,
                PW = 0;

            Db.TransactAsync(() =>
            {
                // Home oldugu Events
                var hcets = Db.SQL<CET>("select r from CET r where r.hCT = ?", ct);
                foreach (var cet in hcets)
                {
                    KA += cet.hKW;
                    KV += cet.gKW;

                    if (cet.hPW > cet.gPW)
                        NG++;
                    else if (cet.hPW < cet.gPW)
                        NM++;
                    else
                        NB++;

                    PW += cet.hPW;

                    if (cet.Drm == "hX")
                        NX++;
                }

                // Guest oldugu Events
                var gcets = Db.SQL<CET>("select r from CET r where r.gCT = ?", ct);
                foreach (var cet in gcets)
                {
                    KA += cet.gKW;
                    KV += cet.hKW;

                    if (cet.hPW < cet.gPW)
                        NG++;
                    else if (cet.hPW > cet.gPW)
                        NM++;
                    else
                        NB++;

                    PW += cet.gPW;

                    if (cet.Drm == "gX")
                        NX++;
                }
                // Update CT
                ct.NG = NG; // NofGalibiyet
                ct.NM = NM; //    Malub
                ct.NB = NB; //    Beraberlik
                ct.NX = NX; //    MacaCikmadi
                ct.KA = KA; // sKor
                ct.KV = KV;
                ct.PW = PW; // Puan
            });
        }
    }

    [Database]
    public class CTP : BB   // TakimOyunculari
    {
        public CC CC { get; set; }
        public CT CT { get; set; }
        public PP PP { get; set; }
        public int Idx { get; set; }    // Takim icindeki sirasi

        public int RnkBas { get; set; } // Takima girdiginde hesaplanir
        public int RnkBit { get; set; } // Lig bittiginde hesaplanir

        public int SMT => SMW + SML;    // Single Mac Total
        public int SMW { get; set; }    //            Win
        public int SML { get; set; }    //            Lost
        public int SMX { get; set; }    //            HukmenMaglup

        public int DMT => DMW + DML;    // Double Mac Total
        public int DMW { get; set; }
        public int DML { get; set; }
        public int DMX { get; set; }    //            HukmenMaglup

        public string CTAd => CT == null ? "-" : $"{CT.Ad}";
        public string PPAd => PP == null ? "-" : $"{PP.Ad}";

        public static void RefreshSonucNew()
        {
            // RefreshSonuc'dan 5 kat hizli
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int nor = 0;
            ulong cc, cet, hct, gct, hpp, gpp;
            int hsw, gsw, hmw, gmw, hmx, gmx;
            string sod;

            List<Maclar> MacList = new List<Maclar>();
            foreach (var m in Db.SQL<MAC>("select m from MAC m where m.CEB IS BDB2.CET"))
            {
                nor++;

                cc = m.CC.GetObjectNo();
                cet = m.CEB.GetObjectNo();
                var ceto = m.CEB as CET;
                hct = ceto.hCT.GetObjectNo();
                gct = ceto.gCT.GetObjectNo();
                hpp = m.hPP1.GetObjectNo();
                gpp = m.gPP1.GetObjectNo();

                hsw = m.hSW;
                gsw = m.gSW;
                hmw = m.hMW;
                gmw = m.gMW;
                hmx = m.hMX;
                gmx = m.gMX;
                sod = m.SoD;

                MacList.Add(new Maclar
                {
                    CC = cc,
                    CET = cet,
                    CT = hct,
                    PP = hpp,
                    SoD = sod,
                    SW = hsw,
                    SL = gsw,
                    MW = hmw,
                    ML = gmw,
                    MX = hmx,
                });

                MacList.Add(new Maclar
                {
                    CC = cc,
                    CET = cet,
                    CT = gct,
                    PP = gpp,
                    SoD = sod,
                    SW = gsw,
                    SL = hsw,
                    MW = gmw,
                    ML = hmw,
                    MX = gmx,
                });

                if (sod == "D")
                {
                    hpp = m.hPP2.GetObjectNo();
                    gpp = m.gPP2.GetObjectNo();

                    MacList.Add(new Maclar
                    {
                        CC = cc,
                        CET = cet,
                        CT = hct,
                        PP = hpp,
                        SoD = sod,
                        SW = hsw,
                        SL = gsw,
                        MW = hmw,
                        ML = gmw,
                        MX = hmx,
                    });

                    MacList.Add(new Maclar
                    {
                        CC = cc,
                        CET = cet,
                        CT = gct,
                        PP = gpp,
                        SoD = sod,
                        SW = gsw,
                        SL = hsw,
                        MW = gmw,
                        ML = hmw,
                        MX = gmx,
                    });
                }
            }

            /*
            var invoiceSum =
            DSZoho.Tables["Invoices"].AsEnumerable()
            .Select (x => 
                new {  
                    InvNumber = x["invoice number"],
                    InvTotal = x["item price"],
                    Contact = x["customer name"],
                    InvDate = x["invoice date"],
                    DueDate = x["due date"],
                    Balance = x["balance"],
                    }
            )
             .GroupBy (s => new {s.InvNumber, s.Contact, s.InvDate, s.DueDate} )
             .Select (g => 
                    new {
                        InvNumber = g.Key.InvNumber,
                        InvDate = g.Key.InvDate,
                        DueDate = g.Key.DueDate,
                        Contact = g.Key.Contact,
                        InvTotal = g.Sum (x => Math.Round(Convert.ToDecimal(x.InvTotal), 2)),
                        Balance = g.Sum (x => Math.Round(Convert.ToDecimal(x.Balance), 2)),
                        } 
             );
            */
            /*
            var groupedResult4 = ml
                .Select(x =>
                   new
                   {
                       acc = x.CC,
                       acet = x.CET,
                       act = x.CT,
                       asod = x.SoD,
                       aSW = x.SW,
                       aMW = x.MW,
                   }
                )
                .OrderBy(x => x.act)//.ThenBy(x => x.asod)
                .GroupBy(s => new { s.act, s.asod })
             .Select(g =>
                   new
                   {
                       gact = g.Key.act,
                       gasod = g.Key.asod,
                       tSW = g.Sum(x => x.aSW),
                       tMW = g.Sum(x => x.aMW),
                   }
             );*/
            /*
            var groupedResult3 = from s in ml
                                group s by new { ct = s.CT, SoD = s.SoD } into grp
                                select new
                                {
                                    Key = grp.Key,
                                    ssw = grp.Sum(r => r.SW),
                                    ssl = grp.Sum(r => r.SL),
                                    smw = grp.Sum(r => r.MW),
                                    sml = grp.Sum(r => r.ML),
                                };
*/
            var groupedResult = MacList
                .OrderBy(x => x.CT).ThenBy(x => x.PP).ThenBy(x => x.SoD)
                .GroupBy(s => new { s.CT, s.PP, s.SoD })
                .Select(g => new
                {
                    gct = g.Key.CT,
                    gpp = g.Key.PP,
                    gsod = g.Key.SoD,
                    tSW = g.Sum(x => x.SW),
                    tSL = g.Sum(x => x.SL),
                    tMW = g.Sum(x => x.MW),
                    tML = g.Sum(x => x.ML),
                    tMX = g.Sum(x => x.MX),
                });

            //iterate each group 
            ulong pct = 0, ppp = 0;
            CTP ctp = null;
            Db.TransactAsync(() =>
            {
                foreach (var gr in groupedResult)
                {
                    if (pct != gr.gct || ppp != gr.gpp) // ctp yi bir kere okusun
                    {
                        ctp = Db.SQL<CTP>("select r from CTP r where r.CT.ObjectNo = ? and r.PP.ObjectNo = ?", gr.gct, gr.gpp).FirstOrDefault();
                        pct = gr.gct;
                        ppp = gr.gpp;
                    }
                    if (ctp != null)
                    {
                        if (gr.gsod == "S")
                        {
                            //ssw = gr.tSW;
                            //ssl = gr.tSL;
                            ctp.SMW = gr.tMW;
                            ctp.SML = gr.tML;
                            ctp.SMX = gr.tMX;
                        }
                        else if (gr.gsod == "D")
                        {
                            //dsw = gr.tSW;
                            //dsl = gr.tSL;
                            ctp.DMW = gr.tMW;
                            ctp.DML = gr.tML;
                            ctp.DMX = gr.tMX;
                        }
                    }
                }
            });
            watch.Stop();
            Console.WriteLine($"CTP.RefreshSonucNew() #MAC {nor}: {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
        }

        public static void RefreshSonuc()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var ccs = Db.SQL<CC>("select r from CC r where r.Skl = ?", "T");
            foreach (var cc in ccs)
            {
                RefreshSonuc(cc);
            }
            watch.Stop();
            Console.WriteLine($"CTP.RefreshSonuc(): {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
        }

        public static void RefreshSonuc(CC cc)
        {
            var cts = Db.SQL<CT>("select r from CT r where r.CC = ?", cc);
            foreach (var ct in cts)
            {
                RefreshSonuc(ct);
            }
        }

        // Takimdaki Oyuncularin Yaptigi Maclari toplayarak Sonuclari update
        public static void RefreshSonuc(CT ct)
        {
            int SMW, SML, SMX, DMW, DML, DMX;

            Db.TransactAsync(() =>
            {
                var ctps = Db.SQL<CTP>("select r from CTP r where r.CT = ?", ct);

                ulong ctONO = ct.GetObjectNo();
                //ulong ctpppONO = 0;
                foreach (var ctp in ctps)
                {
                    //ctpppONO = ctp.PP.GetObjectNo();
                    SMW = 0;
                    SML = 0;
                    SMX = 0;
                    DMW = 0;
                    DML = 0;
                    DMX = 0;
                    // Home olarak bu Takimda yaptiklari
                    var hmacs = Db.SQL<MAC>("select r from MAC r where (r.hPP1 = ? or r.hPP2 = ?) and CAST(r.CEB AS BDB2.CET).hCT = ?", ctp.PP, ctp.PP, ct);
                    //var hmacs = Db.SQL<MAC>("select r from MAC r where r.hPP1 = ? or r.hPP2 = ?", ctp.PP, ctp.PP);
                    //var hmacs = Db.SQL<MAC>("select r from MAC r where CAST(r.CEB AS BDB2.CET).hCT = ?", ct);
                    foreach (var mac in hmacs)
                    {
                        //if (mac.hPP1.GetObjectNo() == ctpppONO || mac.hPP2?.GetObjectNo() == ctpppONO)
                        //if ((mac.CEB as CEF)?.hPP.GetObjectNo() == ctONO)
                        {
                            if (mac.SoD == "S")
                            {
                                SMW += mac.hMW;
                                SML += mac.gMW;
                                if (mac.Drm == "hX")
                                    SMX++;
                            }
                            else
                            {
                                DMW += mac.hMW;
                                DML += mac.gMW;
                                if (mac.Drm == "hX")
                                    DMX++;
                            }
                        }
                    }
                    // Guest olarak yaptiklari
                    var gmacs = Db.SQL<MAC>("select r from MAC r where (r.gPP1 = ? or r.gPP2 = ?) and CAST(r.CEB AS BDB2.CET).gCT = ?", ctp.PP, ctp.PP, ct);
                    //var gmacs = Db.SQL<MAC>("select r from MAC r where r.gPP1 = ? or r.gPP2 = ?", ctp.PP, ctp.PP);
                    //var gmacs = Db.SQL<MAC>("select r from MAC r where r.gPP1 = ? ", ctp.PP);
                    foreach (var mac in gmacs)
                    {
                        if (mac.SoD == "S")
                        {
                            SMW += mac.gMW;
                            SML += mac.hMW;
                            if (mac.Drm == "gX")
                                SMX++;
                        }
                        else
                        {
                            DMW += mac.gMW;
                            DML += mac.hMW;
                            if (mac.Drm == "gX")
                                DMX++;
                        }
                    }
                    
                    ctp.SMW = SMW;
                    ctp.SML = SML;
                    ctp.SMX = SMX;
                    ctp.DMW = DMW;
                    ctp.DML = DML;
                    ctp.DMX = DMX;
                    
                }
            });
        }
     }

    [Database]
    public class CF : BB   // TurnuvaFertleri
    {
        public CC CC { get; set; }
        public PP PP { get; set; }
        public int Rnk { get; set; }
        public int RnkIdx { get; set; }
        //public int SonRnk => PP?.SonRnk == null ? 0 : PP.SonRnk == 0 ? PP.BazRnk : PP.SonRnk;

    }

    [Database]
    public class CEB : BB   // EventBase, Takim/Ferdi
    {
        public CC CC { get; set; }
        public DateTime Trh { get; set; }
        public string Drm { get; set; }      // Iptal, h/gX: Gelmedi, h/gD: Diskalifiye, OK: Oynandi
        public string Yer { get; set; }

        public int hSSW { get; set; }        // Home Kazandigi Single Set
        public int gSSW { get; set; }

        public int hDSW { get; set; }        // Home Kazandigi Double Set
        public int gDSW { get; set; }

        public int hSMW { get; set; }        // Home Kazandigi Single Mac
        public int gSMW { get; set; }

        public int hDMW { get; set; }        // Home Kazandigi Double Mac
        public int gDMW { get; set; }

        public int hKW { get; set; }         // Home Kazandigi sKor
        public int gKW { get; set; }

        public int hPW { get; set; }         // Home Kazandigi Puan
        public int gPW { get; set; }

    }

    [Database]
    public class CET : CEB    // EventTakim
    {
        public CT hCT { get; set; }     // Home Takim
        public CT gCT { get; set; }     // Guest Takim

        public static void RefreshSonuc(CET cet)
        {
            int hSMW = 0, 
                hSSW = 0,
                gSMW = 0, 
                gSSW = 0,
                hDMW = 0, 
                hDSW = 0,
                gDMW = 0, 
                gDSW = 0,
                hKW = 0, 
                gKW = 0, 
                hPW = 0,
                gPW = 0;

            Db.TransactAsync(() =>
            {
                if (cet.Drm == "OK")
                {
                    var macs = Db.SQL<MAC>("select m from MAC m where m.CEB = ?", cet);
                    foreach (var mac in macs)
                    {
                        if (mac.SoD == "S")
                        {
                            hSSW += mac.hSW;
                            gSSW += mac.gSW;
                            hSMW += mac.hMW;
                            gSMW += mac.gMW;
                        }
                        else if (mac.SoD == "D")
                        {
                            hDSW += mac.hSW;
                            gDSW += mac.gSW;
                            hDMW += mac.hMW;
                            gDMW += mac.gMW;
                        }
                    }
                    hKW = hSMW * 2 + hDMW * 3;
                    gKW = gSMW * 2 + gDMW * 3;

                    if (hKW > gKW)
                    {
                        hPW = 3;
                        gPW = 1;
                    }
                    else if (hKW < gKW)
                    {
                        hPW = 1;
                        gPW = 3;
                    }
                    else
                    {
                        hPW = 1;
                        gPW = 1;
                    }
                }
                else if (cet.Drm == "hX")  // Home Gelmedi/Cikmadi
                {

                }
                else if (cet.Drm == "gX")  // Guest Gelmedi/Cikmadi
                {

                }
                else if (cet.Drm == "hD")  // Home Diskalifiye
                {

                }
                else if (cet.Drm == "gD")  // Guest Diskalifiye
                {

                }

                cet.hSSW = hSSW;
                cet.gSSW = gSSW;
                cet.hDSW = hDSW;
                cet.gDSW = gDSW;
                cet.hKW = hKW;
                cet.gKW = gKW;
                cet.hPW = hPW;
                cet.gPW = gPW;
            });
        }
    }

    [Database]
    public class CETP   // EventTakimOyunculari
    {
        public CC CC { get; set; }
        public CET CET { get; set; }

        public string SoD { get; set; }     // SingleOrDouble
        public int Idx { get; set; }        // Mac Sirasi
        public PP hPP1 { get; set; }        // Home Oyuncu 1
        public PP hPP2 { get; set; }
        public PP gPP1 { get; set; }
        public PP gPP2 { get; set; }
    }

    [Database]
    public class CEF : CEB    // EventFerdi
    {
        public PP hPP { get; set; }
        public PP gPP { get; set; }
    }

    [Database]
    public class MAC   // Mac
    {
        public CC CC { get; set; }
        public CEB CEB { get; set; }        // CET/CEF
        public string CEBtyp => CEB?.GetType().Name;

        public int Idx { get; set; }        // CET Mac Sirasi, CEF de 0
        public DateTime Trh { get; set; }
        public string Drm { get; set; }     // Iptal I,h/g Gelmedi h/gHM,h/g SiralamaHatasi h/gSH, Oynandi OK  hX:Cikmadi, hZ:SiralamaHatasi/Diskalifiye
        public string Yer { get; set; }
        public string Hakem { get; set; }
        public string Info { get; set; }

        public string SoD { get; set; }     // SingleOrDouble
        public PP hPP1 { get; set; }
        public PP hPP2 { get; set; }
        public PP gPP1 { get; set; }
        public PP gPP2 { get; set; }

        public int h1W { get; set; }       // Home 1.Set aldigi Sayi    
        public int h2W { get; set; }
        public int h3W { get; set; }
        public int h4W { get; set; }
        public int h5W { get; set; }
        public int h6W { get; set; }
        public int h7W { get; set; }

        public int g1W { get; set; }       // Guest 1.Set aldigi Sayi   
        public int g2W { get; set; }
        public int g3W { get; set; }
        public int g4W { get; set; }
        public int g5W { get; set; }
        public int g6W { get; set; }
        public int g7W { get; set; }

        public int hSW { get; set; }        // Home Aldigi Set
        public int gSW { get; set; }        // Guest Aldigi Set

        public int hMW { get; set; }        // Home Aldigi Mac 0/1
        public int gMW { get; set; }        // Guest Aldigi Mac 0/1

        public int hMX { get; set; }        // Home Diskalifiye
        public int gMX { get; set; }        // Guest 

        public int hRnk { get; set; }       // Maca basladigindaki Rank
        public int hRnkPX { get; set; }     // Rank Point Exchange
        public int gRnk { get; set; }
        public int gRnkPX { get; set; }

        public static void RefreshSonuc(MAC mac)
        {
            // Compute Aldigi Setler
            int hSW = 0, gSW = 0;
            int hMW = 0, gMW = 0;
            int hMX = 0, gMX = 0;

            if (mac.Drm == "OK")
            {
                if (mac.h1W > mac.g1W)
                    hSW++;
                else if (mac.h1W < mac.g1W)
                    gSW++;
                if (mac.h2W > mac.g2W)
                    hSW++;
                else if (mac.h2W < mac.g2W)
                    gSW++;
                if (mac.h3W > mac.g3W)
                    hSW++;
                else if (mac.h3W < mac.g3W)
                    gSW++;
                if (mac.h4W > mac.g4W)
                    hSW++;
                else if (mac.h4W < mac.g4W)
                    gSW++;
                if (mac.h5W > mac.g5W)
                    hSW++;
                else if (mac.h5W < mac.g5W)
                    gSW++;
                if (mac.h6W > mac.g6W)
                    hSW++;
                else if (mac.h6W < mac.g6W)
                    gSW++;
                if (mac.h7W > mac.g7W)
                    hSW++;
                else if (mac.h7W < mac.g7W)
                    gSW++;

                // Compute Aldigi Mac 0/1
                if (hSW > gSW)
                    hMW++;
                else if (hSW < gSW)
                    gMW++;
            }
            else if (mac.Drm == "hX")
            {
                hMX = 1;
                hMW = 0;
                gMW = 1;
            }
            else if (mac.Drm == "gX")
            {
                gMX = 1;
                hMW = 1;
                gMW = 0;
            }

            Db.Transact(() =>
            {
                mac.hSW = hSW;
                mac.gSW = gSW;
                mac.hMW = hMW;
                mac.gMW = gMW;
                mac.hMX = hMX;
                mac.gMX = gMX;
            });
        }

        public static int compHomeRnkPX(bool isHomeWin, int hRnk, int gRnk)
        {
            // Home oyuncuya gore sonuc verir, Guest tersi olur (gPX = -hPX)
            int hPX = 0;

            int PS = 0; // Point Spread between players
            int ER = 0; // ExpectedResult
            int UR = 0; // UpsetResult

            // Compute
            PS = Math.Abs(hRnk - gRnk);

            if (PS < 13)
            {
                ER = 8;
                UR = 8;
            }
            else if (PS < 38)
            {
                ER = 7;
                UR = 10;
            }
            else if (PS < 63)
            {
                ER = 6;
                UR = 13;
            }
            else if (PS < 88)
            {
                ER = 5;
                UR = 16;
            }
            else if (PS < 113)
            {
                ER = 4;
                UR = 20;
            }
            else if (PS < 138)
            {
                ER = 3;
                UR = 25;
            }
            else if (PS < 163)
            {
                ER = 2;
                UR = 30;
            }
            else if (PS < 188)
            {
                ER = 2;
                UR = 35;
            }
            else if (PS < 213)
            {
                ER = 1;
                UR = 40;
            }
            else if (PS < 238)
            {
                ER = 1;
                UR = 45;
            }
            else
            {
                ER = 0;
                UR = 50;
            }

            if (hRnk >= gRnk)                   // Home: Iyi (Home'un kazanmasi bekleniyor)
                hPX += isHomeWin ? ER : -UR;    // Expected/HomeWin : Upset/HomeLost
            else                                // Home: Kotu (Home'un kaybetmesi bekleniyor)
                hPX += isHomeWin ? UR : -ER;    // Upset/HomeWin : Expected/HomeLost

            return hPX;
        }

        public static void deneme2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int nor = 0;

            // Bu sadece PP icin dogru olur, SoD secerek 
            DataTable table = new DataTable();
            table.Columns.Add("CC", typeof(ulong));
            table.Columns.Add("CET", typeof(ulong));
            table.Columns.Add("CT", typeof(ulong));
            table.Columns.Add("PP", typeof(ulong));
            table.Columns.Add("SoD", typeof(string));
            table.Columns.Add("SW", typeof(int));
            table.Columns.Add("SL", typeof(int));
            table.Columns.Add("MW", typeof(int));
            table.Columns.Add("ML", typeof(int));

            ulong cc, cet, hct, gct, hpp, gpp;
            int hsw, gsw, hmw, gmw;

            // Takim Maclari PP
            foreach (var m in Db.SQL<MAC>("select m from MAC m where m.CEB IS BDB2.CET"))
            {
                nor++;

                cc = m.CC.GetObjectNo();
                cet = m.CEB.GetObjectNo();
                var ceto = m.CEB as CET;
                hct = ceto.hCT.GetObjectNo();
                gct = ceto.gCT.GetObjectNo();

                hsw = m.hSW;
                gsw = m.gSW;
                hmw = m.hMW;
                gmw = m.gMW;

                if (m.SoD == "S")
                {
                    hpp = m.hPP1.GetObjectNo();
                    gpp = m.gPP1.GetObjectNo();
                    // Home
                    table.Rows.Add(cc, cet, hct, hpp, "S", hsw, gsw, hmw, gmw);
                    table.Rows.Add(cc, cet, gct, gpp, "S", gsw, hsw, gmw, hmw);
                }
                if (m.SoD == "D")
                {
                    hpp = m.hPP2.GetObjectNo();
                    gpp = m.gPP2.GetObjectNo();
                    table.Rows.Add(cc, cet, hct, hpp, "D", hsw, gsw, hmw, gmw);
                    table.Rows.Add(cc, cet, gct, gpp, "D", gsw, hsw, gmw, hmw);
                }
            }

            //table.Compute("")

            // CT her CC icin ayri tanimladigindan unique
            // CT Single Maclari
            /*
            var result = from row in table.AsEnumerable()
                         where row.Field<string>("SoD") == "S"
                         group row by row.Field<ulong>("CT") into grp
                         select new
                         {
                             ctID = grp.Key,
                             macCount = grp.Count(),
                             ssw = grp.Sum(r => r.Field<int>("SSW")),
                             ssl = grp.Sum(r => r.Field<int>("SSL")),
                             smw = grp.Sum(r => r.Field<int>("SMW")),
                             sml = grp.Sum(r => r.Field<int>("SML")),

                             dsw = grp.Sum(r => r.Field<int>("DSW")),
                             dsl = grp.Sum(r => r.Field<int>("DSL")),
                             dmw = grp.Sum(r => r.Field<int>("DMW")),
                             dml = grp.Sum(r => r.Field<int>("DML")),
                         };
            // PP Single Maclari
            var SinglesPP = from row in table.AsEnumerable()
                            where row.Field<string>("SoD") == "S"
                            group row by row.Field<ulong>("PP") into grp
                            select new
                            {
                                ctID = grp.Key,
                                macCount = grp.Count(),
                                sw = grp.Sum(r => r.Field<int>("SW")),
                                sl = grp.Sum(r => r.Field<int>("SL")),
                                mw = grp.Sum(r => r.Field<int>("MW")),
                                ml = grp.Sum(r => r.Field<int>("ML")),
                            };
            var DoublesPP = from row in table.AsEnumerable()
                            where row.Field<string>("SoD") == "D"
                            group row by row.Field<ulong>("PP") into grp
                            select new
                            {
                                ctID = grp.Key,
                                macCount = grp.Count(),
                                sw = grp.Sum(r => r.Field<int>("SW")),
                                sl = grp.Sum(r => r.Field<int>("SL")),
                                mw = grp.Sum(r => r.Field<int>("MW")),
                                ml = grp.Sum(r => r.Field<int>("ML")),
                            };
*/
            var qryLatestInterview = from rows in table.AsEnumerable()
                                     orderby rows["CT"], rows["PP"] 
                                     group rows by new { ct = rows["CT"], pp = rows["PP"], SoD = rows["SoD"] } into grp
                                     select new
                                     {
                                         key = grp.Key,
                                         ssw = grp.Sum(r => r.Field<int>("SW")),
                                         ssl = grp.Sum(r => r.Field<int>("SL")),
                                         smw = grp.Sum(r => r.Field<int>("MW")),
                                         sml = grp.Sum(r => r.Field<int>("ML")),

                                     };
            //select grp.First();

            foreach (var r in qryLatestInterview)
            {
                var aaa = r.key.ct;
                var bbb = r.key.pp;
                var ccc = r.key.SoD;
                var ctp = Db.SQL<CTP>("select r from CTP r where r.CT.ObjectNo = ? and r.PP.ObjectNo = ?", aaa, bbb).FirstOrDefault();

            }

            //var dtPositionInterviews = qryLatestInterview.CopyToDataTable();

            /*
            var myLinqQuery = table.AsEnumerable()
                         .GroupBy(r1 => r1.Field<int>("ID1"), r2 => r2.Field<int>("ID2"))
                         .Select(g =>
                         {
                             var row = table.NewRow();

                             row["ID1"] = g.Select(r => r.Field<int>("ID1"));
                             row["ID2"] = g.Select(r => r.Field<int>("ID2"));
                             row["Value1"] = g.Select(r => r.Field<string>("Value1"));
                             row["Value2"] = g.Max<string>(r => r.Field<string>("Value2"));

                             return row;
                         }).CopyToDataTable();
                         */
            /*
            var dt = table.AsEnumerable()
                .GroupBy(r => new { Col1 = r["CT"], Col2 = r["SoD"] })
                .Select(g => g.OrderBy(r => r["CT"]).First())
                .CopyToDataTable();
*/
            /*
            foreach (var t in result)
                Console.WriteLine(t.TeamID + " " + t.MemberCount);


            var query = (from DataRow data in table.Rows
                         select data).ToList();

            foreach(var r in query)
            {
                
            }
            */
            watch.Stop();
            Console.WriteLine($"deneme2 #MAC {nor}: {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
        }

        public static void deneme()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int nor = 0;
            Dictionary<string, MacStat> dnm = new Dictionary<string, MacStat>();    // Players
/*
            foreach (var p in Db.SQL<PP>("select p from PP p"))
            {
                dnm[p.GetObjectNo().ToString()] = new MacStat
                {
                    SSW = 0,
                    SSL = 0,
                    SMW = 0,
                    SML = 0,
                    DSW = 0,
                    DSL = 0,
                    DMW = 0,
                    DML = 0,
                };
            }*/
            string hPPoNo, gPPoNo;
            string hPP2oNo, gPP2oNo;
            MacStat hMS, gMS;
            foreach (var mac in Db.SQL<MAC>("select m from MAC m where m.CEB IS BDB2.CET"))
            {
                nor++;
                var hCT = (mac.CEB as CET).hCT.GetObjectNo().ToString();
                var gCT = (mac.CEB as CET).hCT.GetObjectNo().ToString();

                hPPoNo = $"{mac.hPP1.GetObjectNo()} {hCT}";
                gPPoNo = $"{mac.gPP1.GetObjectNo()} {gCT}";
                //gPPoNo = mac.gPP1.GetObjectNo().ToString() + gCT;

                if (!dnm.ContainsKey(hPPoNo))
                    dnm[hPPoNo] = new MacStat();
                if (!dnm.ContainsKey(gPPoNo))
                    dnm[gPPoNo] = new MacStat();

                if (mac.SoD == "S")
                {
                    hMS = dnm[hPPoNo];
                    hMS.SSW += mac.hSW;
                    hMS.SSL += mac.gSW;
                    hMS.SMW += mac.hMW;
                    hMS.SML += mac.gMW;
                    dnm[hPPoNo] = hMS;

                    gMS = dnm[gPPoNo];
                    gMS.SSW += mac.gSW;
                    gMS.SSL += mac.hSW;
                    gMS.SMW += mac.gMW;
                    gMS.SML += mac.hMW;
                    dnm[gPPoNo] = gMS;
                }
                
                else
                {
                    hMS = dnm[hPPoNo];
                    hMS.DSW += mac.hSW;
                    hMS.DSL += mac.gSW;
                    hMS.DMW += mac.hMW;
                    hMS.DML += mac.gMW;
                    dnm[hPPoNo] = hMS;

                    gMS = dnm[gPPoNo];
                    gMS.DSW += mac.gSW;
                    gMS.DSL += mac.hSW;
                    gMS.DMW += mac.gMW;
                    gMS.DML += mac.hMW;
                    dnm[gPPoNo] = gMS;

                    //hPP2oNo = mac.hPP2.GetObjectNo().ToString() + hCT;
                    //gPP2oNo = mac.gPP2.GetObjectNo().ToString() + gCT;
                    hPP2oNo = $"{mac.hPP2.GetObjectNo()} {hCT}";
                    gPP2oNo = $"{mac.gPP2.GetObjectNo()} {gCT}";
                    if (!dnm.ContainsKey(hPP2oNo))
                        dnm[hPP2oNo] = new MacStat();
                    if (!dnm.ContainsKey(gPP2oNo))
                        dnm[gPP2oNo] = new MacStat();

                    hMS = dnm[hPP2oNo];
                    hMS.DSW += mac.hSW;
                    hMS.DSL += mac.gSW;
                    hMS.DMW += mac.hMW;
                    hMS.DML += mac.gMW;
                    dnm[hPP2oNo] = hMS;

                    gMS = dnm[gPP2oNo];
                    gMS.DSW += mac.gSW;
                    gMS.DSL += mac.hSW;
                    gMS.DMW += mac.gMW;
                    gMS.DML += mac.hMW;
                    dnm[gPP2oNo] = gMS;

                }
                
            }
            string[] k;
            ulong pp, ct;
            MacStat ms;
            foreach(var pair in dnm)
            {
                k = pair.Key.Split(new Char[] { ' ' });
                pp = ulong.Parse(k[0]);
                ct = ulong.Parse(k[1]);
                ms = pair.Value;
            }
            watch.Stop();
            Console.WriteLine($"deneme #MAC {nor}: {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
        }

        public static void RefreshGlobalRank()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int nor = 0;

            ulong hPPoNo, gPPoNo;
            int hpRnk, gpRnk;
            int hPX = 0;
            Dictionary<ulong, int> ppDic = new Dictionary<ulong, int>();    // Players

            Db.TransactAsync(() =>
            {
                foreach (var p in Db.SQL<PP>("select p from PP p"))
                {
                    ppDic[p.GetObjectNo()] = p.RnkBaz;
                }

                // Sadece Single Maclar Rank uretir
                foreach (var mac in Db.SQL<MAC>("select m from MAC m order by m.Trh"))    
                //foreach (var mac in Db.SQL<BDB.MAC>("select m from MAC m where m.SoD = ? order by m.Trh", "S"))
                {
                    if (mac.SoD == "D") // Performans daha iyi Query de Single arama 
                        continue;

                    nor++;

                    hPPoNo = mac.hPP1.GetObjectNo();
                    gPPoNo = mac.gPP1.GetObjectNo();

                    hpRnk = ppDic[hPPoNo];
                    gpRnk = ppDic[gPPoNo];

                    hPX = 0;
                    if (mac.CC.IsRnkd)  // Rank hesaplanacak ise
                        if (mac.Drm == "OK" && hpRnk != 0 && gpRnk != 0)
                            hPX = compHomeRnkPX(mac.hMW == 0 ? false : true, hpRnk, gpRnk);

                    // Update MAC
                    mac.hRnkPX = hPX;
                    mac.hRnk   = hpRnk;

                    mac.gRnkPX = -hPX;
                    mac.gRnk   = gpRnk;

                    // Update dictionary
                    ppDic[hPPoNo] = hpRnk + hPX;
                    ppDic[gPPoNo] = gpRnk - hPX;
                }


                foreach (var p in Db.SQL<PP>("select p from PP p where p.SMT = ?", 0))
                {
                    ppDic[p.GetObjectNo()] = 0;
                }

                // Rank'e gore Sira verip PP update
                var items = from pair in ppDic
                            orderby pair.Value descending
                            select pair;

                int idx = 1;
                PP pp;
                foreach (var pair in items)
                {
                    pp = Db.FromId<PP>(pair.Key);
                    pp.RnkSon = pair.Value;
                    pp.RnkIdx = idx++;
                }
            });

            watch.Stop();
            Console.WriteLine($"RefreshGlobalRank {nor}: {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
        }
    }

    public class Maclar
    {
        public ulong CC;
        public ulong CET;
        public ulong CT;
        public ulong PP;
        public string SoD;

        public int SW;
        public int SL;
        public int MW;
        public int ML;
        public int MX;
    }

    public class MacStat
    {
        public int SSW;
        public int SSL;
        public int SMW;
        public int SML;

        public int DSW;
        public int DSL;
        public int DMW;
        public int DML;
    }
}