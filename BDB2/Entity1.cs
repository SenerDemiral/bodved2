using System;
using Starcounter;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace BDB2
{
    [Database]
    public class PP   // Players
    {
        public string Ad { get; set; }
        public int RnkBaz { get; set; }
        public int RnkSon { get; set; }
        public int RnkIdx { get; set; }     // RnkSon'a gore dizildiginde Sirasi
    }

    [Database]
    public class CC   // Competitions
    {
        public string Ad { get; set; }
        public string Skl { get; set; }     // Takim/Ferdi
        public bool isRun { get; set; }     // Cari, Devam eden (False:Bitti)
        public string Grp { get; set; }     // Ligdeki Grup oyuncularini bilmek icin
        public bool isRnkd { get; set; }    // Rank hesaplnacak mi?
    }

    [Database]
    public class CT   // Takimlar
    {
        public CC CC { get; set; }
        public string Ad { get; set; }

        public int EG { get; set; }         // Event sayisi Galibiyet
        public int EM { get; set; }         //              Malubiyet
        public int EB { get; set; }         //              Beraberlik
        public int ET => EG + EM + EB;      //              Toplam

        public int KA { get; set; }         // sKor Aldigi
        public int KV { get; set; }         //      Verdigi
        public int KF => KA - KV;           //      Fark = Alidigi - Verdigi

        public int PW { get; set; }         // Puan

        // Yaptigi Eventleri toplayarak Sonuclari update
        public static void RefreshSonuc(CT ct)
        {
            int EG = 0,
                EM = 0,
                EB = 0,
                KA = 0,
                KV = 0,
                KF = 0,
                PW = 0;

            Db.TransactAsync(() =>
            {
                // Home oldugu Events
                var cets = Db.SQL<CET>("select r from CET r where r.hCT = ?", ct);
                foreach (var cet in cets)
                {
                    KA += cet.hKW;
                    KV += cet.gKW;

                    if (cet.hPW > cet.gPW)
                        EG++;
                    else if (cet.hPW < cet.gPW)
                        EM++;
                    else
                        EB++;

                    PW += cet.hPW;
                }

                // Guest oldugu Events
                cets = Db.SQL<CET>("select r from CET r where r.gCT = ?", ct);
                foreach (var cet in cets)
                {
                    KA += cet.gKW;
                    KV += cet.hKW;

                    if (cet.hPW < cet.gPW)
                        EG++;
                    else if (cet.hPW > cet.gPW)
                        EM++;
                    else
                        EB++;

                    PW += cet.gPW;
                }
                // Update CT
                ct.EG = EG; // NofEvents
                ct.EM = EM;
                ct.EB = EB;
                ct.KA = KA; // sKor
                ct.KV = KV;
                ct.PW = PW; // Puan
            });
        }
    }

    [Database]
    public class CTP    // TakimOyunculari
    {
        public CC CC { get; set; }
        public CT CT { get; set; }
        public PP PP { get; set; }
    }

    [Database]
    public class CF    // TurnuvaFertleri
    {
        public CC CC { get; set; }
        public PP PP { get; set; }
        public int Rnk { get; set; }
        public int RnkIdx { get; set; }
        //public int SonRnk => PP?.SonRnk == null ? 0 : PP.SonRnk == 0 ? PP.BazRnk : PP.SonRnk;

    }

    [Database]
    public class CEB    // EventBase, Takim/Ferdi
    {
        public CC CC { get; set; }
        public DateTime Trh { get; set; }
        public string Drm { get; set; }      // Iptal,hGelmedi,gGelmedi,Oynandi
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
                else if (cet.Drm == "hHM")  // Home Hukmen Maglup
                {

                }
                else if (cet.Drm == "gHM")  // Guest Hukmen Maglup
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
    public class MAC    // Mac
    {
        public CC CC { get; set; }
        public CEB CEB { get; set; }        // CET/CEF

        public int Idx { get; set; }        // Mac Sirasi CET ise
        public DateTime Trh { get; set; }
        public string Drm { get; set; }     // Iptal I,h/g Gelmedi h/gHM,h/g SiralamaHatasi h/gSH, Oynandi OK
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

        public int hRnk { get; set; }       // Maca basladigindaki Rank
        public int hRnkPX { get; set; }     // Rank Point Exchange
        public int gRnk { get; set; }
        public int gRnkPX { get; set; }

        public static void RefreshSonuc(MAC mac)
        {
            // Compute Aldigi Setler
            int hSW = 0, gSW = 0;
            int hMW = 0, gMW = 0;

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

            Db.Transact(() =>
            {
                mac.hSW = hSW;
                mac.gSW = gSW;
                mac.hMW = hMW;
                mac.gMW = gMW;
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
                foreach (var mac in Db.SQL<MAC>("select m from MAC m where order by m.Trh"))    
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
                    if (mac.CC.isRnkd)  // Rank hesaplanacak ise
                        if (mac.Drm == "OK" )
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
}