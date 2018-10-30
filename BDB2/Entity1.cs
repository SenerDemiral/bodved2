using System;
using System.Data;
using Starcounter;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Collections;

namespace BDB2
{
    [Database]
    public class STAT
    {
        public int ID { get; set; }
        public int IdVal { get; set; }

        public STAT()
        {
            ID = 1;
            IdVal = 4230;
        }

        public static int UpdEntCnt()
        {
            H.Write2Log($"Enter: {Session.Current.SessionId}");

            int EntCnt = 0;
            Db.Transact(() =>
            {
                var s = Db.SQL<STAT>("select s from STAT s where s.ID = ?", 1).FirstOrDefault();
                if (s == null)
                {
                    new STAT()
                    {
                        ID = 1,
                        IdVal = 32000
                    };
                    EntCnt = 32000; // Bodved2 baslangic
                }
                else
                {
                    s.IdVal += 1;
                    EntCnt = s.IdVal;
                }

            });
            return EntCnt;
        }

    }

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
        public ulong PPoNo => this.GetObjectNo();
        public string Sex { get; set; }
        public string Tel { get; set; }
        public int RnkIlk { get; set; }
        public int RnkBaz { get; set; }
        public int RnkSon { get; set; }
        public int RnkIdx { get; set; }     // RnkSon'a gore dizildiginde Sirasi
        public bool IsRun { get; set; }     // Aktif

        // Simdilik Eski Lig Rankleri
        public int Rnk1 { get; set; }
        public int Rnk2 { get; set; }
        public int Rnk3 { get; set; }

        public int SST => SSW + SSL;    // Single Set Total/Win/Lost
        public int SSW { get; set; }
        public int SSL { get; set; }

        public int SMT => SMW + SML;    // Single Mac Total/Win/Lost
        public int SMW { get; set; }
        public int SML { get; set; }
        public int SMX { get; set; }    //            HukmenMaglup

        public int DST => DSW + DSL;    // Double Set
        public int DSW { get; set; }
        public int DSL { get; set; }

        public int DMT => DMW + DML;    // Double Mac
        public int DMW { get; set; }
        public int DML { get; set; }


        public static void RefreshSonuc()   // Tum oyuncular icin
        {
            Dictionary<ulong, DictMacStat> dnm = new Dictionary<ulong, DictMacStat>();    // Players
            ulong hPPoNo, gPPoNo;
            DictMacStat hMS, gMS;

            int norPP = 0, norMAC = 0;
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var macs = Db.SQL<MAC>("select r from MAC r");
            foreach (var mac in macs)
            {
                norMAC++;
                hPPoNo = mac.HPP1oNo;
                gPPoNo = mac.GPP1oNo;

                if (!dnm.ContainsKey(hPPoNo))
                    dnm[hPPoNo] = new DictMacStat();
                if (!dnm.ContainsKey(gPPoNo))
                    dnm[gPPoNo] = new DictMacStat();
                
                if (mac.SoD == "S")
                {
                    /*
                    dnm[hPPoNo].SSW += mac.HSW;
                    dnm[hPPoNo].SSL += mac.GSW;
                    dnm[hPPoNo].SMW += mac.HMW;
                    dnm[hPPoNo].SML += mac.GMW;

                    dnm[gPPoNo].SSW += mac.GSW;
                    dnm[gPPoNo].SSL += mac.HSW;
                    dnm[gPPoNo].SMW += mac.GMW;
                    dnm[gPPoNo].SML += mac.HMW;
                    */
                    hMS = dnm[hPPoNo];
                    hMS.SSW += mac.HSW;
                    hMS.SSL += mac.GSW;
                    hMS.SMW += mac.HMW;
                    hMS.SML += mac.GMW;
                    dnm[hPPoNo] = hMS;

                    gMS = dnm[gPPoNo];
                    gMS.SSW += mac.GSW;
                    gMS.SSL += mac.HSW;
                    gMS.SMW += mac.GMW;
                    gMS.SML += mac.HMW;
                    dnm[gPPoNo] = gMS;

                }
                else
                {
                    /*
                    dnm[hPPoNo].DSW += mac.HSW;
                    dnm[hPPoNo].DSL += mac.GSW;
                    dnm[hPPoNo].DMW += mac.HMW;
                    dnm[hPPoNo].DML += mac.GMW;

                    dnm[gPPoNo].DSW += mac.GSW;
                    dnm[gPPoNo].DSL += mac.HSW;
                    dnm[gPPoNo].DMW += mac.GMW;
                    dnm[gPPoNo].DML += mac.HMW;

                    hPPoNo = mac.HPP2oNo;
                    gPPoNo = mac.GPP2oNo;
                    if (!dnm.ContainsKey(hPPoNo))
                        dnm[hPPoNo] = new DictMacStat();
                    if (!dnm.ContainsKey(gPPoNo))
                        dnm[gPPoNo] = new DictMacStat();

                    dnm[hPPoNo].DSW += mac.HSW;
                    dnm[hPPoNo].DSL += mac.GSW;
                    dnm[hPPoNo].DMW += mac.HMW;
                    dnm[hPPoNo].DML += mac.GMW;

                    dnm[gPPoNo].DSW += mac.GSW;
                    dnm[gPPoNo].DSL += mac.HSW;
                    dnm[gPPoNo].DMW += mac.GMW;
                    dnm[gPPoNo].DML += mac.HMW;
                    */
                    
                    hMS = dnm[hPPoNo];
                    hMS.DSW += mac.HSW;
                    hMS.DSL += mac.GSW;
                    hMS.DMW += mac.HMW;
                    hMS.DML += mac.GMW;
                    dnm[hPPoNo] = hMS;

                    gMS = dnm[gPPoNo];
                    gMS.DSW += mac.GSW;
                    gMS.DSL += mac.HSW;
                    gMS.DMW += mac.GMW;
                    gMS.DML += mac.HMW;
                    dnm[gPPoNo] = gMS;

                
                    hPPoNo = mac.HPP2oNo;
                    gPPoNo = mac.GPP2oNo;

                    if (!dnm.ContainsKey(hPPoNo))
                        dnm[hPPoNo] = new DictMacStat();
                    if (!dnm.ContainsKey(gPPoNo))
                        dnm[gPPoNo] = new DictMacStat();

                    hMS = dnm[hPPoNo];
                    hMS.DSW += mac.HSW;
                    hMS.DSL += mac.GSW;
                    hMS.DMW += mac.HMW;
                    hMS.DML += mac.GMW;
                    dnm[hPPoNo] = hMS;

                    gMS = dnm[gPPoNo];
                    gMS.DSW += mac.GSW;
                    gMS.DSL += mac.HSW;
                    gMS.DMW += mac.GMW;
                    gMS.DML += mac.HMW;
                    dnm[gPPoNo] = gMS;

                }

            }
            
            Db.TransactAsync(() =>
            {
                var pps = Db.SQL<PP>("select r from PP r");
                foreach (var pp in pps)
                {
                    if (dnm.ContainsKey(pp.PPoNo))
                    {
                        norPP++;
                        hMS = dnm[pp.PPoNo];
                        pp.SSW = hMS.SSW;
                        pp.SSL = hMS.SSL;
                        pp.SMW = hMS.SMW;
                        pp.SML = hMS.SML;
                        pp.DSW = hMS.DSW;
                        pp.DSL = hMS.DSL;
                        pp.DMW = hMS.DMW;
                        pp.DML = hMS.DML;
                    }
                    // Mac'i olmayan oyuncunun sonuclari sifirlanabilir
                }
            });


            watch.Stop();
            Console.WriteLine($"PP.RefreshSonuc(): MAC:{norMAC}, PP:{norPP} {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
        }

        public static void RefreshSonuc(MAC mac)    // Mactaki oyuncular icin yap
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Db.TransactAsync(() =>
            {
                var pps = Db.SQL<PP>("select r from PP r");

                RefreshSonuc(mac.HPP1);
                RefreshSonuc(mac.GPP1);
                RefreshSonuc(mac.HPP2);
                RefreshSonuc(mac.GPP2);
            });

            watch.Stop();
            Console.WriteLine($"PP.RefreshSonuc(): {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
        }

        public static void RefreshSonuc(PP pp)  // Bir oyuncu
        {
            int SSW = 0, DSW = 0, SMW = 0, DMW = 0;
            int SSL = 0, DSL = 0, SML = 0, DML = 0;

            var hmacs = Db.SQL<MAC>("select r from MAC r where r.HPP1 = ? or r.HPP2 = ?", pp, pp);
            foreach (var mac in hmacs)
            {
                if (mac.SoD == "S")
                {
                    SSW += mac.HSW;     // Single Set Win
                    SSL += mac.GSW;     //            Lost
                    SMW += mac.HMW;     //        Mac
                    SML += mac.GMW;
                }
                if (mac.SoD == "D")
                {
                    DSW += mac.HSW;
                    DSL += mac.GSW;
                    DMW += mac.HMW;
                    DML += mac.GMW;
                }
            }
            var gmacs = Db.SQL<MAC>("select r from MAC r where r.GPP1 = ? or r.GPP2 = ?", pp, pp);
            foreach (var mac in gmacs)
            {
                if (mac.SoD == "S")
                {
                    SSW += mac.GSW;
                    SSL += mac.HSW;
                    SMW += mac.GMW;
                    SML += mac.HMW;
                }
                if (mac.SoD == "D")
                {
                    DSW += mac.GSW;
                    DSL += mac.HSW;
                    DMW += mac.GMW;
                    DML += mac.HMW;
                }
            }
            //Db.TransactAsync(() =>
            //{
                pp.SSW = SSW;
                pp.SSL = SSL;
                pp.SMW = SMW;
                pp.SML = SML;
                pp.DSW = DSW;
                pp.DSL = DSL;
                pp.DMW = DMW;
                pp.DML = DML;
            //});
        }

    }

    [Database]
    public class CC : BB  // Competitions
    {
        public ulong CCoNo => this.GetObjectNo();
        public int Idx { get; set; } 
        public string Skl { get; set; }     // Takim/Ferdi
        public string Grp { get; set; }     // Ligdeki Grup oyuncularini bilmek icin
        public bool IsRun { get; set; }     // Cari, Devam eden (False:Bitti)
        public bool IsRnkd { get; set; }    // Rank hesaplnacak mi?

        public int TNSM { get; set; }        // Takim Nof Single Mac
        public int TNDM { get; set; }        //           Double
        public int TNSS { get; set; }        // Takim Nof Single Set
        public int TNDS { get; set; }        //           Double

        public int TSMK { get; set; }        // Takim SingleMac Skoru 2
        public int TDMK { get; set; }        //       Double 3

        public int TEGP { get; set; }         // Takim Event Galibiyet Puani
        public int TEMP { get; set; }         //            Malubiyet
        public int TEBP { get; set; }         //            Beraberlik
        public int TEXP { get; set; }         //            Cikmadi

    }

    // Takim devre icinde diskalifiye edilebilir, bu durumdan sonraki Musabakalarinda Hukmen maglup olur
    [Database]
    public class CT : BB  // Takimlar
    {
        public ulong CToNo => this.GetObjectNo();
        public CC CC { get; set; }
        public string Adres { get; set; }     // Takim/Ferdi
        public PP K1 { get; set; }            // 1.Kaptan
        public PP K2 { get; set; }

        public int Idx { get; set; }
        public bool IsRun { get; set; }     // Aktif oynuyor mu?

        //--------
        public int SMT => SMW + SML;        // Single Mac Total, Win, Lost
        public int SMW { get; set; }
        public int SML { get; set; }

        public int DMT => DMW + DML;        // Double Mac Total, Win, Lost
        public int DMW { get; set; }
        public int DML { get; set; }

        public int KF => KW - KL;           // sKor Fark, Win, Lost
        public int KW { get; set; }
        public int KL { get; set; }

        public int ET => EW + EL + EB + EX;        // Event  Total, Win, Lost, Beraber, Diskalifiye
        public int EW { get; set; }
        public int EL { get; set; }
        public int EB { get; set; }
        public int EX { get; set; }

        public int PW { get; set; }         // Puan Win
        public int PL { get; set; }
        //--------

        public string K1Ad => K1 == null ? "-" : $"{K1.Ad} ({K1.Tel})";
        public string K2Ad => K2 == null ? "-" : $"{K2.Ad} ({K2.Tel})";

        // Diskalifiye edildikten sonraki Eventlerini update
        // Takimin Yaptigi Eventleri toplayarak Sonuclari update
        public static void RefreshSonuc()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var ccs = Db.SQL<CC>("select r from CC r where r.Skl = ?", "T");
            foreach (var cc in ccs)
            {
                var cts = Db.SQL<CT>("select r from CT r where r.CC = ?", cc);
                foreach (var ct in cts)
                {
                    RefreshSonuc(ct);
                }

                // Sort for CC
                Db.TransactAsync(() =>
                {
                    cts = Db.SQL<CT>("SELECT r FROM CT r WHERE r.CC = ? order by r.PW DESC, r.KF DESC", cc);
                    int idx = 1;
                    foreach (var ct in cts)
                    {
                        ct.Idx = idx++;
                    }
                });

            }
            watch.Stop();
            Console.WriteLine($"CT.RefreshSonuc(): {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");

        }

        public static void RefreshSonuc(CT ct)
        {
            int SMW = 0,
                SML = 0,
                DMW = 0,
                DML = 0,
                KW = 0,
                KL = 0,
                EW = 0,
                EL = 0,
                EB = 0,
                EX = 0,
                PW = 0,
                PL = 0;


            Db.TransactAsync(() =>
            { 
                // Home oldugu Events
                var hcets = Db.SQL<CET>("select r from CET r where r.HCT = ?", ct);
                foreach (var cet in hcets)
                {
                    KW += cet.HKW;
                    KL += cet.GKW;

                    if (cet.HPW > cet.GPW)
                        EW++;
                    else if (cet.HPW < cet.GPW)
                        EL++;
                    else
                        EB++;

                    PW += cet.HPW;
                    PL += cet.GPW;

                    if (cet.Drm == "hX")
                        EX++;

                    SMW += cet.HSMW;
                    SML += cet.GSMW;    // Kaybettigi digerinin Kazandigi
                    DMW += cet.HDMW;
                    DML += cet.GDMW;
                }

                // Guest oldugu Events
                var gcets = Db.SQL<CET>("select r from CET r where r.GCT = ?", ct);
                foreach (var cet in gcets)
                {
                    KW += cet.GKW;
                    KL += cet.HKW;

                    if (cet.HPW < cet.GPW)
                        EW++;
                    else if (cet.HPW > cet.GPW)
                        EL++;
                    else
                        EB++;

                    PW += cet.GPW;
                    PL += cet.HPW;

                    if (cet.Drm == "gX")
                        EX++;

                    SMW += cet.GSMW;
                    SML += cet.HSMW;    // Kaybettigi digerinin Kazandigi
                    DMW += cet.GDMW;
                    DML += cet.HDMW;
                }

                // Update CT
                ct.SMW = SMW;   // Single Mac Win/Lost
                ct.SML = SML;
                ct.DMW = DMW;
                ct.DML = DML;

                ct.EW = EW;     // Evet Win/Lost/Berabere/Event'e cikmadi
                ct.EL = EL;
                ct.EB = EB;
                ct.EX = EX;
                ct.KW = KW;     // sKor Win
                ct.KL = KL;
                ct.PW = PW;     // Puan Win
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
        public bool IsRun { get; set; }     // Aktif oynuyor mu?

        public int RnkBas { get; set; } // Takima girdiginde hesaplanir
        public int RnkBit { get; set; } // Lig bittiginde hesaplanir

        public int SMT => SMW + SML;    // Single Mac Total, Win, Lost, Diskalifiye
        public int SMW { get; set; }
        public int SML { get; set; }
        public int SMX { get; set; }

        public int DMT => DMW + DML;    // Double Mac Total
        public int DMW { get; set; }
        public int DML { get; set; }
        public int DMX { get; set; }    //            HukmenMaglup

        public ulong CToNo => CT?.GetObjectNo() ?? 0;
        public ulong PPoNo => PP?.GetObjectNo() ?? 0;
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

            List<DictMaclar> MacList = new List<DictMaclar>();
            foreach (var m in Db.SQL<MAC>("select m from MAC m where m.CC IS NOT NULL and m.CEB IS BDB2.CET"))
            {
                nor++;

                cc = m.CC.GetObjectNo();
                cet = m.CEB.GetObjectNo();
                var ceto = m.CEB as CET;
                hct = ceto.HCT.GetObjectNo();
                gct = ceto.GCT.GetObjectNo();
                hpp = m.HPP1.GetObjectNo();
                gpp = m.GPP1.GetObjectNo();

                hsw = m.HSW;
                gsw = m.GSW;
                hmw = m.HMW;
                gmw = m.GMW;
                hmx = m.HMX;
                gmx = m.GMX;
                sod = m.SoD;

                MacList.Add(new DictMaclar
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

                MacList.Add(new DictMaclar
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
                    hpp = m.HPP2.GetObjectNo();
                    gpp = m.GPP2.GetObjectNo();

                    MacList.Add(new DictMaclar
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

                    MacList.Add(new DictMaclar
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
                .OrderBy(x => x.CT).ThenBy(x => x.PP) //.ThenBy(x => x.SoD)
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
                    var hmacs = Db.SQL<MAC>("select r from MAC r where (r.HPP1 = ? or r.HPP2 = ?) and CAST(r.CEB AS BDB2.CET).HCT = ?", ctp.PP, ctp.PP, ct);
                    //var hmacs = Db.SQL<MAC>("select r from MAC r where r.hPP1 = ? or r.hPP2 = ?", ctp.PP, ctp.PP);
                    //var hmacs = Db.SQL<MAC>("select r from MAC r where CAST(r.CEB AS BDB2.CET).HCT = ?", ct);
                    foreach (var mac in hmacs)
                    {
                        //if (mac.hPP1.GetObjectNo() == ctpppONO || mac.hPP2?.GetObjectNo() == ctpppONO)
                        //if ((mac.CEB as CEF)?.hPP.GetObjectNo() == ctONO)
                        {
                            if (mac.SoD == "S")
                            {
                                SMW += mac.HMW;
                                SML += mac.GMW;
                                if (mac.Drm == "hX")
                                    SMX++;
                            }
                            else
                            {
                                DMW += mac.HMW;
                                DML += mac.GMW;
                                if (mac.Drm == "hX")
                                    DMX++;
                            }
                        }
                    }
                    // Guest olarak yaptiklari
                    var gmacs = Db.SQL<MAC>("select r from MAC r where (r.GPP1 = ? or r.GPP2 = ?) and CAST(r.CEB AS BDB2.CET).GCT = ?", ctp.PP, ctp.PP, ct);
                    //var gmacs = Db.SQL<MAC>("select r from MAC r where r.GPP1 = ? or r.GPP2 = ?", ctp.PP, ctp.PP);
                    //var gmacs = Db.SQL<MAC>("select r from MAC r where r.GPP1 = ? ", ctp.PP);
                    foreach (var mac in gmacs)
                    {
                        if (mac.SoD == "S")
                        {
                            SMW += mac.GMW;
                            SML += mac.HMW;
                            if (mac.Drm == "gX")
                                SMX++;
                        }
                        else
                        {
                            DMW += mac.GMW;
                            DML += mac.HMW;
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

        public static void UpdateRnkBas()
        {
            // PP.RnkSon -> CTP.RnkBas 
            Db.TransactAsync(() =>
            {
                var ctps = Db.SQL<CTP>("select r from CTP r");
                foreach(var ctp in ctps)
                {
                    ctp.IsRun = ctp.PP.IsRun;
                    ctp.RnkBas = ctp.PP.RnkIlk;
                    ctp.RnkBit = ctp.PP.RnkSon;
                }

            });
        }
     }

    [Database]
    public class CF : BB   // TurnuvaFertleri
    {
        public ulong CFoNo => this.GetObjectNo();
        public CC CC { get; set; }
        public PP PP { get; set; }
        public int Idx { get; set; }
        public bool IsRun { get; set; }     // Aktif oynuyor mu?
        public int RnkBas { get; set; }     // Takima girdiginde hesaplanir
        public int RnkBit { get; set; }     // Lig bittiginde hesaplanir

        public int Rnk { get; set; }    // ??????
        public int RnkIdx { get; set; } // ??????
        public ulong PPoNo => PP?.GetObjectNo() ?? 0;
        public string PPAd => PP?.Ad;

        public int ST => SW + SL;        // Set Total, Win, Lost, Fark
        public int SW { get; set; }
        public int SL { get; set; }
        public int SF => SW - SL;

        public int MT => MW + ML;        // Mac Total, Win, Lost
        public int MW { get; set; }
        public int ML { get; set; }

        public int PW { get; set; }      // Puan Win
        public int PL { get; set; }

        public static void RefreshSonuc()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var ccs = Db.SQL<CC>("select r from CC r where r.Skl = ?", "F");
            foreach (var cc in ccs)
            {
                RefreshSonuc(cc);
            }

            watch.Stop();
            Console.WriteLine($"CF.RefreshSonuc(): {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
        }

        public static void RefreshSonuc(CC cc)
        {
            Dictionary<ulong, DictFerdiStat> dnm = new Dictionary<ulong, DictFerdiStat>();    // Players
            ulong hPPoNo, gPPoNo, PPoNo;

            // CCnin CF lerindeki Players
            var cfs = Db.SQL<CF>("select r from CF r where r.CC = ?", cc);
            foreach(var cf in cfs)
            {
                dnm[cf.PPoNo] = new DictFerdiStat();
            }

            Db.TransactAsync(() =>
            {
                var hcefs = Db.SQL<CEF>("select r from CEF r where r.CC = ?", cc);
                foreach (var cef in hcefs)
                {
                    hPPoNo = cef.HPP.GetObjectNo();
                    gPPoNo = cef.GPP.GetObjectNo();

                    // Home
                    dnm[hPPoNo].SW += cef.HSSW;
                    dnm[hPPoNo].SL += cef.GSSW;    // Kaybettigi digerinin Kazandigi
                    dnm[hPPoNo].MW += cef.HSMW;
                    dnm[hPPoNo].ML += cef.GSMW;    // Kaybettigi digerinin Kazandigi
                    dnm[hPPoNo].PW += cef.HPW;
                    dnm[hPPoNo].PL += cef.GPW;

                    // Guest
                    dnm[gPPoNo].SW += cef.GSSW;
                    dnm[gPPoNo].SL += cef.HSSW;    // Kaybettigi digerinin Kazandigi
                    dnm[gPPoNo].MW += cef.GSMW;
                    dnm[gPPoNo].ML += cef.HSMW;    // Kaybettigi digerinin Kazandigi
                    dnm[gPPoNo].PW += cef.GPW;
                    dnm[gPPoNo].PL += cef.HPW;

                    //if (cef.Drm == "hX")
                    //    EX++;

                }
                // Update
                cfs = Db.SQL<CF>("select r from CF r where r.CC = ?", cc);
                foreach (var cf in cfs)
                {
                    PPoNo = cf.PP.GetObjectNo();
                    cf.SW = dnm[PPoNo].SW;
                    cf.SL = dnm[PPoNo].SL;
                    cf.MW = dnm[PPoNo].MW;
                    cf.ML = dnm[PPoNo].ML;
                    cf.PW = dnm[PPoNo].PW;
                    cf.PL = dnm[PPoNo].PL;
                }

                int idx = 1;
                cfs = Db.SQL<CF>("select r from CF r where r.CC = ? order by r.PW DESC, r.SF ASC", cc);
                foreach (var cf in cfs)
                {
                    cf.Idx = idx++;
                }
            });
        }

    }

    [Database]
    public class CEB : BB   // EventBase, Takim/Ferdi
    {
        public CC CC { get; set; }
        public DateTime Trh { get; set; }
        public string Drm { get; set; }      // Iptal, h/gX: Gelmedi, h/gD: Diskalifiye, OK: Oynandi
        public string Yer { get; set; }

        public string Tarih => string.Format(H.cultureTR, "{0:dd.MM.yy ddd}", Trh);  //$"{Trh:dd.MM.yy ddd}";

        /*
        public static void RefreshSonucOLD()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var cebs = Db.SQL<CEB>("select r from CEB r");
            foreach (var ceb in cebs)
            {
                RefreshSonucOLD(ceb);
            }

            watch.Stop();
            Console.WriteLine($"CEB.RefreshSonuc(): {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
            //Console.WriteLine($"CET.RefreshSonuc(){DateTime.Now:dd.MM.yy ddd}");  // Turkce, Burda oluyor!!
        }

        public static void RefreshSonucOLD(CEB ceb)
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

            int TSMK = ceb.CC.TSMK;
            int TDMK = ceb.CC.TDMK;

            int TEGP = ceb.CC.TEGP;
            int TEMP = ceb.CC.TEMP;
            int TEBP = ceb.CC.TEBP;
            int TEXP = ceb.CC.TEXP;

            Db.TransactAsync(() =>
            {
                if (ceb.Drm == "OK")
                {
                    var macs = Db.SQL<MAC>("select m from MAC m where m.CEB.ObjectNo = ?", ceb.GetObjectNo());
                    foreach (var mac in macs)
                    {
                        if (mac.SoD == "S")
                        {
                            hSSW += mac.HSW;
                            gSSW += mac.GSW;
                            hSMW += mac.HMW;
                            gSMW += mac.GMW;
                        }
                        else if (mac.SoD == "D")
                        {
                            hDSW += mac.HSW;
                            gDSW += mac.GSW;
                            hDMW += mac.HMW;
                            gDMW += mac.GMW;
                        }
                    }
                    hKW = hSMW * TSMK + hDMW * TDMK;
                    gKW = gSMW * TSMK + gDMW * TDMK;

                    if (hKW > gKW)
                    {
                        hPW = TEGP;
                        gPW = TEMP;
                    }
                    else if (hKW < gKW)
                    {
                        hPW = TEMP;
                        gPW = TEGP;
                    }
                    else
                    {
                        hPW = TEBP;
                        gPW = TEBP;
                    }
                }
                else if (ceb.Drm == "hX")  // Home Gelmedi/Cikmadi
                {

                }
                else if (ceb.Drm == "gX")  // Guest Gelmedi/Cikmadi
                {

                }
                else if (ceb.Drm == "hD")  // Home Diskalifiye
                {

                }
                else if (ceb.Drm == "gD")  // Guest Diskalifiye
                {

                }

                // Update CET
                ceb.HSSW = hSSW;
                ceb.GSSW = gSSW;
                ceb.HDSW = hDSW;
                ceb.GDSW = gDSW;

                ceb.HSMW = hSMW;
                ceb.GSMW = gSMW;
                ceb.HDMW = hDMW;
                ceb.GDMW = gDMW;

                ceb.HKW = hKW;
                ceb.GKW = gKW;

                ceb.HPW = hPW;
                ceb.GPW = gPW;
            });
        }*/
    }

    [Database]
    public class CET : CEB    // EventTakim
    {
        public ulong CEToNo => this.GetObjectNo();
        public CT HCT { get; set; }     // Home Takim
        public CT GCT { get; set; }     // Guest Takim

        public ulong HCToNo => HCT?.GetObjectNo() ?? 0;
        public ulong GCToNo => GCT?.GetObjectNo() ?? 0;
        public string HCTAd => HCT?.Ad;
        public string GCTAd => GCT?.Ad;

        public int HSSW { get; set; }        // Home Single Set Win
        public int GSSW { get; set; }
        public int HDSW { get; set; }        // Home Double Set Win
        public int GDSW { get; set; }

        public int HSMW { get; set; }        // Home Single Mac Win
        public int GSMW { get; set; }
        public int HDMW { get; set; }        // Home Double Mac Win
        public int GDMW { get; set; }

        public int HKW { get; set; }         // Home sKor Win
        public int GKW { get; set; }

        public int HPW { get; set; }         // Home Kazandigi Puan
        public int GPW { get; set; }

        public string HR => $"{HSMW}S+{HDMW}D ►{HKW,2:D2} ►{HPW}";
        public string GR => $"{GSMW}S+{GDMW}D ►{GKW,2:D2} ►{GPW}";

        public string HWL => HPW == GPW ? "?" : HPW > GPW ? "W" : "L";
        public string GWL => HPW == GPW ? "?" : HPW < GPW ? "W" : "L";

        public static void RefreshSonuc()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var cets = Db.SQL<CET>("select r from CET r");
            foreach (var cet in cets)
            {
                RefreshSonuc(cet);
            }

            watch.Stop();
            Console.WriteLine($"CET.RefreshSonuc(): {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
            //Console.WriteLine($"CET.RefreshSonuc(){DateTime.Now:dd.MM.yy ddd}");  // Turkce, Burda oluyor!!
        }

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

            int TSMK = cet.CC.TSMK;
            int TDMK = cet.CC.TDMK;

            int TEGP = cet.CC.TEGP;
            int TEMP = cet.CC.TEMP;
            int TEBP = cet.CC.TEBP;
            int TEXP = cet.CC.TEXP;

            Db.TransactAsync(() =>
            {
                if (cet.Drm == "OK")
                {
                    var macs = Db.SQL<MAC>("select m from MAC m where m.CEB = ?", cet);
                    foreach (var mac in macs)
                    {
                        if (mac.SoD == "S")
                        {
                            hSSW += mac.HSW;
                            gSSW += mac.GSW;
                            hSMW += mac.HMW;
                            gSMW += mac.GMW;
                        }
                        else if (mac.SoD == "D")
                        {
                            hDSW += mac.HSW;
                            gDSW += mac.GSW;
                            hDMW += mac.HMW;
                            gDMW += mac.GMW;
                        }
                    }
                    hKW = hSMW * TSMK + hDMW * TDMK;
                    gKW = gSMW * TSMK + gDMW * TDMK;

                    if (hKW > gKW)
                    {
                        hPW = TEGP;
                        gPW = TEMP;
                    }
                    else if (hKW < gKW)
                    {
                        hPW = TEMP;
                        gPW = TEGP;
                    }
                    else
                    {
                        hPW = TEBP;
                        gPW = TEBP;
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

                // Update CET
                cet.HSSW = hSSW;
                cet.GSSW = gSSW;
                cet.HDSW = hDSW;
                cet.GDSW = gDSW;

                cet.HSMW = hSMW;
                cet.GSMW = gSMW;
                cet.HDMW = hDMW;
                cet.GDMW = gDMW;

                cet.HKW = hKW;
                cet.GKW = gKW;

                cet.HPW = hPW;
                cet.GPW = gPW;
            });
        }

        public static string CreateEvents(ulong CCoNo)
        {
            ArrayList al = new ArrayList();
            DateTime Trh = new DateTime(2099, 12, 31);
            CC cc = Db.FromId<CC>(CCoNo);

            var cet = Db.SQL<CET>("select r from CET r where r.CC = ?", cc).FirstOrDefault();
            if (cet != null)
                return $"{cc.Ad} Takim Fikstur zaten var.";

            var cts = Db.SQL<CT>("select r from CT r where r.CC = ? order by r.Ad", cc);
            foreach (var ct in cts)
            {
                al.Add(ct.GetObjectNo());
            }
            int cnt = al.Count;

            Db.TransactAsync(() =>
            {
                for (int i = 0; i < cnt; i++)
                {
                    for (int k = i + 1; k < cnt; k++)
                    {
                        new CET
                        {
                            CC = cc,
                            Trh = Trh,
                            HCT = Db.FromId<CT>((ulong)al[i]),
                            GCT = Db.FromId<CT>((ulong)al[k]),
                        };
                        new CET
                        {
                            CC = cc,
                            Trh = Trh,
                            GCT = Db.FromId<CT>((ulong)al[i]),
                            HCT = Db.FromId<CT>((ulong)al[k]),
                        };

                        //Trh = Trh.AddDays(-1);
                    }
                }
            });
            return "";
        }

    }

    [Database]
    public class CETP   // EventTakimOyunculari  KULLANILMIYOR
    {
        public CC CC { get; set; }
        public CET CET { get; set; }

        public string SoD { get; set; }     // SingleOrDouble
        public int Idx { get; set; }        // Mac Sirasi
        public PP HPP1 { get; set; }        // Home Oyuncu 1
        public PP HPP2 { get; set; }
        public PP GPP1 { get; set; }
        public PP GPP2 { get; set; }
    }

    [Database]
    public class CEF : CEB    // EventFerdi
    {
        public ulong CEFoNo => this.GetObjectNo();
        public PP HPP { get; set; }
        public PP GPP { get; set; }

        public ulong HPPoNo => HPP?.GetObjectNo() ?? 0;
        public ulong GPPoNo => GPP?.GetObjectNo() ?? 0;
        public string HPPAd => HPP?.Ad;
        public string GPPAd => GPP?.Ad;

        public int HSSW { get; set; }        // Home Single Set Win
        public int GSSW { get; set; }
        public int HSMW { get; set; }        // Home Single Mac Win
        public int GSMW { get; set; }
        public int HPW { get; set; }         // Home Puan Win
        public int GPW { get; set; }

        public string HR => $"{HSSW} ►{HSMW} ►{HPW}";
        public string GR => $"{GSSW} ►{GSMW} ►{GPW}";

        public string HWL => HPW == GPW ? "?" : HPW > GPW ? "W" : "L";
        public string GWL => HPW == GPW ? "?" : HPW < GPW ? "W" : "L";

        // Bir maci olur
        public string SncOzt
        {
            get
            {
                var mac = Db.SQL<MAC>("select r from BDB2.MAC r where r.CEB.ObjectNo = ?", this.GetObjectNo()).FirstOrDefault();
                if (mac == null)
                    return "";

                return $"({mac.SncMac}) {mac.SncSet}";
            }
        }

        public static void RefreshSonuc()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var cefs = Db.SQL<CEF>("select r from CEF r");
            foreach (var cef in cefs)
            {
                RefreshSonuc(cef);
            }

            watch.Stop();
            Console.WriteLine($"CEF.RefreshSonuc(): {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
        }

        public static void RefreshSonuc(CEF cef)
        {
            Db.TransactAsync(() =>
            {
                if (cef.Drm == "OK")
                {
                    // Ferdi Event'in tek bir single maci olur.
                    var mac = Db.SQL<MAC>("select r from MAC r where r.CEB.ObjectNo = ? and SoD = ?", cef.GetObjectNo(), "S").FirstOrDefault();
                    if (mac != null)
                    {
                        cef.HSSW = mac.HSW;
                        cef.GSSW = mac.GSW;
                        cef.HSMW = mac.HMW;
                        cef.GSMW = mac.GMW;
                        if (mac.HMW > mac.GMW)  // Home kazandi
                        {
                            cef.HPW = 3;
                            cef.GPW = 1;
                        }
                        else if (mac.HMW < mac.GMW)  // Guest kazandi
                        {
                            cef.HPW = 1;
                            cef.GPW = 3;
                        }
                        else // Berabere
                        {
                            cef.HPW = 2;
                            cef.GPW = 3;
                        }
                    }
                }
            });
        }

        public static string CreateEvents(ulong CCoNo)
        {
            ArrayList al = new ArrayList();
            DateTime Trh = new DateTime(2099, 12, 31);
            CC cc = Db.FromId<CC>(CCoNo);

            var cef = Db.SQL<CEF>("select r from CEF r where r.CC = ?", cc).FirstOrDefault();
            if (cef != null)
                return $"{cc.Ad} Ferdi Fikstur zaten var.";

            var cfs = Db.SQL<CF>("select r from CF r where r.CC = ? order by r.PP.Ad", cc);
            foreach (var cf in cfs)
            {
                al.Add(cf.PP.GetObjectNo());
            }
            int cnt = al.Count;

            Db.TransactAsync(() =>
            {
                for (int i = 0; i < cnt; i++)
                {
                    for (int k = i + 1; k < cnt; k++)
                    {
                        new CEF
                        {
                            CC = cc,
                            Trh = Trh,
                            HPP = Db.FromId<PP>((ulong)al[i]),
                            GPP = Db.FromId<PP>((ulong)al[k]),
                        };
                        //Trh = Trh.AddDays(-1);
                    }
                }
            });
            return "";
        }
    }

    [Database]
    public class MAC   // Mac
    {
        public ulong MACoNo => this.GetObjectNo();
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
        public PP HPP1 { get; set; }
        public PP HPP2 { get; set; }
        public PP GPP1 { get; set; }
        public PP GPP2 { get; set; }

        public int H1W { get; set; }       // Home 1.Set aldigi Sayi    
        public int H2W { get; set; }
        public int H3W { get; set; }
        public int H4W { get; set; }
        public int H5W { get; set; }
        public int H6W { get; set; }
        public int H7W { get; set; }

        public int G1W { get; set; }       // Guest 1.Set aldigi Sayi   
        public int G2W { get; set; }
        public int G3W { get; set; }
        public int G4W { get; set; }
        public int G5W { get; set; }
        public int G6W { get; set; }
        public int G7W { get; set; }

        public int HSW { get; set; }        // Home Set Win
        public int GSW { get; set; }        // Guest 

        public int HMW { get; set; }        // Home Mac Win
        public int GMW { get; set; }        // Guest 

        public int HMX { get; set; }        // Home Diskalifiye
        public int GMX { get; set; }        // Guest 

        public int HRnk { get; set; }       // Maca basladigindaki Rank
        public int HRnkPX { get; set; }     // Rank Point Exchange
        public int GRnk { get; set; }
        public int GRnkPX { get; set; }

        public string HWL => HMW == GMW ? "?" : HMW > GMW ? "W" : "L";
        public string GWL => HMW == GMW ? "?" : HMW < GMW ? "W" : "L";

        public ulong HPP1oNo => HPP1?.GetObjectNo() ?? 0;
        public ulong HPP2oNo => HPP2?.GetObjectNo() ?? 0;
        public ulong GPP1oNo => GPP1?.GetObjectNo() ?? 0;
        public ulong GPP2oNo => GPP2?.GetObjectNo() ?? 0;

        public string HPP1Ad => HPP1?.Ad;
        public string HPP2Ad => HPP2?.Ad;
        public string GPP1Ad => GPP1?.Ad;
        public string GPP2Ad => GPP2?.Ad;
        public string Tarih => $"{Trh:dd.MM.yy}";

        public ulong HCToNo => CEB is CET ? (CEB as CET).HCT.GetObjectNo() : 0;
        public ulong GCToNo => CEB is CET ? (CEB as CET).GCT.GetObjectNo() : 0;
        public string HCTAd => CEB is CET ? (CEB as CET).HCT.Ad : "";
        public string GCTAd => CEB is CET ? (CEB as CET).GCT.Ad : "";

        public string SncMac
        {
            get
            {
                if ((HSW + GSW) == 0)
                    return "";
                return $"{HSW}-{GSW}";
            }
        }

        public string SncSet
        {
            get
            {
                /*
                if (hR > gR)
                    rS = $"◀-{gR}";
                else if (hR < gR)
                    rS = $"{hR}-▶";
                */
                string rtr = "";
                if ((H1W + G1W) != 0)
                {
                    rtr = H1W > G1W ? $"●{G1W}" : $"{H1W}●";
                    if ((H2W + G2W) != 0)
                    {
                        rtr += H2W > G2W ? $"│●{G2W}" : $"│{H2W}●";
                        if ((H3W + G3W) != 0)
                        {
                            rtr += H3W > G3W ? $"│●{G3W}" : $"│{H3W}●";
                            if ((H4W + G4W) != 0)
                            {
                                rtr += H4W > G4W ? $"│●{G4W}" : $"│{H4W}●";
                                if ((H5W + G5W) != 0)
                                {
                                    rtr += H5W > G5W ? $"│●{G5W}" : $"│{H5W}●";
                                    if ((H6W + G6W) != 0)
                                    {
                                        rtr += H6W > G6W ? $"│●{G6W}" : $"│{H6W}●";
                                        if ((H7W + G7W) != 0)
                                        {
                                            rtr += H7W > G7W ? $"│●{G7W}" : $"│{H7W}●";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                /*
                if ((H1W + G1W) == 0)
                    return "";
                rtr = $"{H1W}-{G1W}│{H2W}-{G2W}";
                if ((H3W + G3W) != 0)
                {
                    rtr += $"●{H3W}-{G3W}";
                    if ((H4W + G4W) != 0)
                    {
                        rtr += $"●{H4W}-{G4W}";
                        if ((H5W + G5W) != 0)
                        {
                            rtr += $"●{H5W}-{G5W}";
                            if ((H6W + G6W) != 0)
                            {
                                rtr += $"●{H6W}-{G6W}";
                                if ((H7W + G7W) != 0)
                                    rtr += $"●{H7W}.{G7W}";
                            }
                        }
                    }
                }*/
                return rtr;
            }
        }

        public string SncMacRvrs
        {
            get
            {
                if ((HSW + GSW) == 0)
                    return "";
                return $"{GSW}-{HSW}";
            }
        }

        public string SncSetRvrs
        {
            get
            {
                string rtr = "";
                if ((H1W + G1W) != 0)
                {
                    rtr = H1W > G1W ? $"{G1W}●" : $"●{H1W}";
                    if ((H2W + G2W) != 0)
                    {
                        rtr += H2W > G2W ? $"│{G2W}●" : $"│●{H2W}";
                        if ((H3W + G3W) != 0)
                        {
                            rtr += H3W > G3W ? $"│{G3W}●" : $"│●{H3W}";
                            if ((H4W + G4W) != 0)
                            {
                                rtr += H4W > G4W ? $"│{G4W}●" : $"│●{H4W}";
                                if ((H5W + G5W) != 0)
                                {
                                    rtr += H5W > G5W ? $"│{G5W}●" : $"│●{H5W}";
                                    if ((H6W + G6W) != 0)
                                    {
                                        rtr += H6W > G6W ? $"│{G6W}●" : $"│●{H6W}";
                                        if ((H7W + G7W) != 0)
                                        {
                                            rtr += H7W > G7W ? $"│{G7W}●" : $"│●{H7W}";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                /*
                if ((H1W + G1W) == 0)
                    return "";

                string rtr = $"{G1W}-{H1W}●{G2W}-{H2W}";
                if ((H3W + G3W) != 0)
                {
                    rtr += $"●{G3W}-{H3W}";
                    if ((H4W + G4W) != 0)
                    {
                        rtr += $"●{G4W}-{H4W}";
                        if ((H5W + G5W) != 0)
                        {
                            rtr += $"●{G5W}-{H5W}";
                            if ((H6W + G6W) != 0)
                            {
                                rtr += $"●{G6W}-{H6W}";
                                if ((H7W + G7W) != 0)
                                    rtr += $"●{G7W}-{H7W}";
                            }
                        }
                    }
                }*/
                return rtr;
            }
        }



        public static void RefreshSonuc()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Db.TransactAsync(() => {
                var macs = Db.SQL<MAC>("select r from MAC r");
                foreach (var mac in macs)
                    RefreshSonuc(mac);
            });

            watch.Stop();
            Console.WriteLine($"MAC.RefreshSonuc(): {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
        }

        public static void RefreshSonuc(MAC mac)
        {
            // Compute Aldigi Setler
            int hSW = 0, gSW = 0;
            int hMW = 0, gMW = 0;
            int hMX = 0, gMX = 0;

            if (mac.Drm == "OK")
            {
                if (mac.H1W > mac.G1W)
                    hSW++;
                else if (mac.H1W < mac.G1W)
                    gSW++;
                if (mac.H2W > mac.G2W)
                    hSW++;
                else if (mac.H2W < mac.G2W)
                    gSW++;
                if (mac.H3W > mac.G3W)
                    hSW++;
                else if (mac.H3W < mac.G3W)
                    gSW++;
                if (mac.H4W > mac.G4W)
                    hSW++;
                else if (mac.H4W < mac.G4W)
                    gSW++;
                if (mac.H5W > mac.G5W)
                    hSW++;
                else if (mac.H5W < mac.G5W)
                    gSW++;
                if (mac.H6W > mac.G6W)
                    hSW++;
                else if (mac.H6W < mac.G6W)
                    gSW++;
                if (mac.H7W > mac.G7W)
                    hSW++;
                else if (mac.H7W < mac.G7W)
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

            //Db.TransactAsync(() =>
            //{
                mac.HSW = hSW;
                mac.GSW = gSW;
                mac.HMW = hMW;
                mac.GMW = gMW;
                mac.HMX = hMX;
                mac.GMX = gMX;
            //});
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
                hct = ceto.HCT.GetObjectNo();
                gct = ceto.GCT.GetObjectNo();

                hsw = m.HSW;
                gsw = m.GSW;
                hmw = m.HMW;
                gmw = m.GMW;

                if (m.SoD == "S")
                {
                    hpp = m.HPP1.GetObjectNo();
                    gpp = m.GPP1.GetObjectNo();
                    // Home
                    table.Rows.Add(cc, cet, hct, hpp, "S", hsw, gsw, hmw, gmw);
                    table.Rows.Add(cc, cet, gct, gpp, "S", gsw, hsw, gmw, hmw);
                }
                if (m.SoD == "D")
                {
                    hpp = m.HPP2.GetObjectNo();
                    gpp = m.GPP2.GetObjectNo();
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
            Dictionary<string, DictMacStat> dnm = new Dictionary<string, DictMacStat>();    // Players
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
            DictMacStat hMS, gMS;
            foreach (var mac in Db.SQL<MAC>("select m from MAC m where m.CEB IS BDB2.CET"))
            {
                nor++;
                var hCT = (mac.CEB as CET).HCT.GetObjectNo().ToString();
                var gCT = (mac.CEB as CET).GCT.GetObjectNo().ToString();

                hPPoNo = $"{mac.HPP1.GetObjectNo()} {hCT}";
                gPPoNo = $"{mac.GPP1.GetObjectNo()} {gCT}";
                //gPPoNo = mac.gPP1.GetObjectNo().ToString() + gCT;

                if (!dnm.ContainsKey(hPPoNo))
                    dnm[hPPoNo] = new DictMacStat();
                if (!dnm.ContainsKey(gPPoNo))
                    dnm[gPPoNo] = new DictMacStat();

                if (mac.SoD == "S")
                {
                    hMS = dnm[hPPoNo];
                    hMS.SSW += mac.HSW;
                    hMS.SSL += mac.GSW;
                    hMS.SMW += mac.HMW;
                    hMS.SML += mac.GMW;
                    dnm[hPPoNo] = hMS;

                    gMS = dnm[gPPoNo];
                    gMS.SSW += mac.GSW;
                    gMS.SSL += mac.HSW;
                    gMS.SMW += mac.GMW;
                    gMS.SML += mac.HMW;
                    dnm[gPPoNo] = gMS;
                }
                
                else
                {
                    hMS = dnm[hPPoNo];
                    hMS.DSW += mac.HSW;
                    hMS.DSL += mac.GSW;
                    hMS.DMW += mac.HMW;
                    hMS.DML += mac.GMW;
                    dnm[hPPoNo] = hMS;

                    gMS = dnm[gPPoNo];
                    gMS.DSW += mac.GSW;
                    gMS.DSL += mac.HSW;
                    gMS.DMW += mac.GMW;
                    gMS.DML += mac.HMW;
                    dnm[gPPoNo] = gMS;

                    //hPP2oNo = mac.hPP2.GetObjectNo().ToString() + hCT;
                    //gPP2oNo = mac.gPP2.GetObjectNo().ToString() + gCT;
                    hPP2oNo = $"{mac.HPP2.GetObjectNo()} {hCT}";
                    gPP2oNo = $"{mac.GPP2.GetObjectNo()} {gCT}";
                    if (!dnm.ContainsKey(hPP2oNo))
                        dnm[hPP2oNo] = new DictMacStat();
                    if (!dnm.ContainsKey(gPP2oNo))
                        dnm[gPP2oNo] = new DictMacStat();

                    hMS = dnm[hPP2oNo];
                    hMS.DSW += mac.HSW;
                    hMS.DSL += mac.GSW;
                    hMS.DMW += mac.HMW;
                    hMS.DML += mac.GMW;
                    dnm[hPP2oNo] = hMS;

                    gMS = dnm[gPP2oNo];
                    gMS.DSW += mac.GSW;
                    gMS.DSL += mac.HSW;
                    gMS.DMW += mac.GMW;
                    gMS.DML += mac.HMW;
                    dnm[gPP2oNo] = gMS;

                }
                
            }
            string[] k;
            ulong pp, ct;
            DictMacStat ms;
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
                //foreach (var mac in Db.SQL<MAC>("select m from MAC m where m.SoD = ? order by m.Trh", "S"))
                {
                    if (mac.SoD == "D") // Performans daha iyi Query de Single arama 
                        continue;

                    nor++;

                    hPPoNo = mac.HPP1.GetObjectNo();
                    gPPoNo = mac.GPP1.GetObjectNo();

                    hpRnk = ppDic[hPPoNo];
                    gpRnk = ppDic[gPPoNo];

                    hPX = 0;
                    if (mac.CC.IsRnkd)  // Rank hesaplanacak ise
                        if (mac.Drm == "OK" && hpRnk != 0 && gpRnk != 0)
                            hPX = compHomeRnkPX(mac.HMW == 0 ? false : true, hpRnk, gpRnk);

                    // Update MAC
                    mac.HRnkPX = hPX;
                    mac.HRnk   = hpRnk;

                    mac.GRnkPX = -hPX;
                    mac.GRnk   = gpRnk;

                    // Update dictionary
                    ppDic[hPPoNo] = hpRnk + hPX;
                    ppDic[gPPoNo] = gpRnk - hPX;
                }

                // Hic mac yapmamislari ve Ayrilmis olanlari Adina gore sirala
                int dc = 0;
                foreach (var p in Db.SQL<PP>("select p from PP p where p.SMT = ? or p.IsRun = ? order by p.Ad", 0, false))
                {
                    ppDic[p.GetObjectNo()] = dc--;
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
                    //pp.RnkSon = pair.Value;   // Siralamayi yukarda yaptin RnkSon sifirlama
                    pp.RnkIdx = idx++;
                }
            });

            watch.Stop();
            Console.WriteLine($"RefreshGlobalRank {nor}: {watch.ElapsedMilliseconds} msec  {watch.ElapsedTicks} ticks");
        }

        public static void RefreshGlobalRank(DateTime trh)
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
                    ppDic[p.GetObjectNo()] = p.RnkSon;  // p.RnkBaz
                }

                // Sadece Single Maclar Rank uretir
                foreach (var mac in Db.SQL<MAC>("select m from MAC m where m.Trh > ? order by m.Trh", trh))
                //foreach (var mac in Db.SQL<BDB.MAC>("select m from MAC m where m.SoD = ? order by m.Trh", "S"))
                {
                    if (mac.SoD == "D") // Performans daha iyi Query de Single arama 
                        continue;

                    nor++;

                    hPPoNo = mac.HPP1.GetObjectNo();
                    gPPoNo = mac.GPP1.GetObjectNo();

                    hpRnk = ppDic[hPPoNo];
                    gpRnk = ppDic[gPPoNo];

                    hPX = 0;
                    if (mac.CC.IsRnkd)  // Rank hesaplanacak ise
                        if (mac.Drm == "OK" && hpRnk != 0 && gpRnk != 0)
                            hPX = compHomeRnkPX(mac.HMW == 0 ? false : true, hpRnk, gpRnk);

                    // Update MAC
                    mac.HRnkPX = hPX;
                    mac.HRnk = hpRnk;

                    mac.GRnkPX = -hPX;
                    mac.GRnk = gpRnk;

                    // Update dictionary
                    ppDic[hPPoNo] = hpRnk + hPX;
                    ppDic[gPPoNo] = gpRnk - hPX;
                }


                foreach (var p in Db.SQL<PP>("select p from PP p where p.SMT = ? or p.IsRun = ?", 0, false))
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

    public class DictMaclar
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

    public class DictMacStat
    {
        public int SSW;
        public int SSL;
        public int SMW;
        public int SML;
        public int SMX;     // Diskalifiye

        public int DSW;
        public int DSL;
        public int DMW;
        public int DML;
    }

    public class DictFerdiStat
    {
        public int SW;
        public int SL;

        public int MW;
        public int ML;

        public int PW;
        public int PL;
    }
}