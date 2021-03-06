﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Starcounter;

namespace BDB2
{
    public static class H
    {
        public static int DnmRun = 19;  // AktifDonem, Maclar basladiginda
        public static ulong dskPPoNo = 584;

        public static CultureInfo cultureTR = CultureInfo.CreateSpecificCulture("tr-TR");  // Tarihde gun gostermek icin

        public static int IntParse(string s)
        {
            if (int.TryParse(s, out int a))
                return a;
            return 0;
        }

        public static (string, string) GetSetSayi(string i1, string i2)
        {
            int x = 0;
            string o1 = "", o2 = i2;

            if (int.TryParse(i1, out int a))
            {
                if (a == 0)
                {
                    a = 0;
                    x = 11;
                }
                else
                {
                    if (a <= 9)
                        x = 11;
                    else
                    {
                        if (a > 97)
                            a = 97;
                        x = a + 2;
                    }
                }
                o1 = a.ToString("#");
                o2 = x.ToString("#");
            }
            else
            {
                o1 = "";
                o2 = "";
            }

            return (o1, o2);
        }

        public static ulong LoginOp(string pwd)
        {
            // AktifDonem CC lerden Pwd ara
            var cc = Db.SQL<CC>("select r from CC r where r.Dnm = ? and r.Pwd = ?", DnmRun, pwd).FirstOrDefault();

            if (cc == null)
                return 0;
            return cc.CCoNo;
        }

        public static void Write2Log(string Msg)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Starcounter\MyLog\BodVed-Log.txt", true))
            {
                //sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + Msg);
                sw.WriteLine($"{DateTime.Now:yy-MM-dd HH:mm:ss}");
            }
        }

        public static DateTime GetNextWeekday(DayOfWeek day)
        {
            DateTime result = DateTime.Today; //.AddDays(1);
            while (result.DayOfWeek != day)
                result = result.AddDays(1);
            return result;
        }

        public static void PopAll() // ARTIK KULLANILMIYOR
        {
            if (Db.SQL<PP>("select r from PP r").FirstOrDefault() != null)
                return; // Kayit var yapma

            Dictionary<ulong, ulong> dON = new Dictionary<ulong, ulong>();    // dON[oldNO] = newNO
            ulong oldNO = 0;

            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-PP0.txt", System.Text.Encoding.UTF8))
            {
                string line;
                PP pp = null;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            oldNO = ulong.Parse(ra[0]);
                            pp = new PP
                            {
                                RnkIlk = Convert.ToInt32(ra[1]),
                                RnkBaz = Convert.ToInt32(ra[1]),
                                Sex = ra[2],
                                Ad = ra[4],
                                Tel = ra[6],
                                IsRun = true,

                                Rnk1 = Convert.ToInt32(ra[7]),
                                Rnk2 = Convert.ToInt32(ra[8]),
                                Rnk3 = Convert.ToInt32(ra[9]),

                            };
                            dON[oldNO] = pp.GetObjectNo();
                        }
                    }
                });
            }

            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CC0.txt", System.Text.Encoding.UTF8))
            {
                string line;
                CC cc = null;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            oldNO = ulong.Parse(ra[0]);
                            cc = new CC
                            {
                                Ad = ra[2],
                                Skl = ra[3],

                                IsRnkd = true,
                                TNSM = 8,   // Takim Single Mac Sayisi
                                TNDM = 3,   //       Double
                                TNSS = 5,   // Takim Single kac set uzerinden
                                TNDS = 5,   //       Double
                                TSMK = 2,   // Takim SingleMac Skoru
                                TDMK = 3,   //         DoubleMac 
                                TEGP = 2,   //       Event Galibiyet Puan
                                TEMP = 1,   //      
                                TEBP = 0,
                                TEXP = -1,
                            };
                            dON[oldNO] = cc.GetObjectNo();
                        }
                    }
                });
            }

            //0:PK|1:CC|2:Ad|3:Adres|4:Pw|5:K1|6:K2|7:K1.Ad|8:K2.Ad
            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CT0.txt", System.Text.Encoding.UTF8))
            {
                string line;
                ulong oldCC, oldK1, oldK2;
                CT ct = null;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            oldNO = ulong.Parse(ra[0]);
                            oldCC = ulong.Parse(ra[1]);
                            oldK1 = ulong.Parse(ra[2]);
                            oldK2 = ulong.Parse(ra[3]);

                            ct = new CT
                            {
                                CC = Db.FromId<CC>(dON[oldCC]),
                                K1 = oldK1 == 0 ? null : Db.FromId<PP>(dON[oldK1]),
                                K2 = oldK2 == 0 ? null : Db.FromId<PP>(dON[oldK2]),

                                Ad = ra[4],
                                Adres = ra[5],
                                IsRun = true
                            };
                            dON[oldNO] = ct.GetObjectNo();
                        }
                    }
                });
            }

            //0:CC|1:CT|2:PP|3:Idx|4:PPAd|5:CTAd
            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CTP0.txt", System.Text.Encoding.UTF8))
            {
                string line;
                ulong oldCC, oldCT, oldPP;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            oldCC = ulong.Parse(ra[0]);
                            oldCT = ulong.Parse(ra[1]);
                            oldPP = ulong.Parse(ra[2]);

                            new CTP
                            {
                                CC = Db.FromId<CC>(dON[oldCC]),
                                CT = Db.FromId<CT>(dON[oldCT]),
                                PP = oldPP == 0 ? null : Db.FromId<PP>(dON[oldPP]),
                                Idx = int.Parse(ra[3]),
                                IsRun = true,
                            };
                        }
                    }
                });
            }

            //0:PK|1:CC|2:hCT|3:gCT|4:Trh:dd.MM.yyyy HH:mm|5:hPok|6:gPok|7:Rok|8:hP|9:gP|10:hPW|11:hMSW|12:hMDW|13:gPW|14:gMSW|15:gMDW
            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CET0.txt", System.Text.Encoding.UTF8))
            {
                string line;
                ulong oldCC, oldHCT, oldGCT;
                CET cet = null;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            oldNO = ulong.Parse(ra[0]);
                            oldCC = ulong.Parse(ra[1]);
                            oldHCT = ulong.Parse(ra[2]);
                            oldGCT = ulong.Parse(ra[3]);

                            cet = new CET
                            {
                                CC = Db.FromId<CC>(dON[oldCC]),
                                HCT = Db.FromId<CT>(dON[oldHCT]),
                                GCT = Db.FromId<CT>(dON[oldGCT]),
                                Trh = DateTime.Parse(ra[4]),

                                Drm = "OK"
                            };
                            dON[oldNO] = cet.GetObjectNo();
                        }
                    }
                });
            }

            //0:CC|1:CET|2:hPP1|3:hPP2|4:gPP1|5:gPP2|6:Trh|7:SoD|8:Idx|9:hS1W|hS2W|hS3W|hS4W|hS5W|14:gS1W|gS2W|gS3W|gS4W|gS5W
            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-MAC0.txt", System.Text.Encoding.UTF8))
            {
                string line;
                ulong oldCC, oldCEB, oldHPP1, oldHPP2, oldGPP1, oldGPP2;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            oldCC = ulong.Parse(ra[0]);
                            oldCEB = ulong.Parse(ra[1]);
                            oldHPP1 = ulong.Parse(ra[2]);
                            oldHPP2 = ulong.Parse(ra[3]);
                            oldGPP1 = ulong.Parse(ra[4]);
                            oldGPP2 = ulong.Parse(ra[5]);

                            new MAC
                            {
                                CC = Db.FromId<CC>(dON[oldCC]),
                                CEB = Db.FromId<CEB>(dON[oldCEB]), //Calisiyor????????????????????????????
                                HPP1 = oldHPP1 == 0 ? null : Db.FromId<PP>(dON[oldHPP1]),
                                HPP2 = oldHPP2 == 0 ? null : Db.FromId<PP>(dON[oldHPP2]),
                                GPP1 = oldGPP1 == 0 ? null : Db.FromId<PP>(dON[oldGPP1]),
                                GPP2 = oldGPP2 == 0 ? null : Db.FromId<PP>(dON[oldGPP2]),

                                Trh = DateTime.Parse(ra[6]),
                                SoD = ra[7],
                                Idx = int.Parse(ra[8]),

                                H1W = int.Parse(ra[9]),
                                H2W = int.Parse(ra[10]),
                                H3W = int.Parse(ra[11]),
                                H4W = int.Parse(ra[12]),
                                H5W = int.Parse(ra[13]),

                                G1W = int.Parse(ra[14]),
                                G2W = int.Parse(ra[15]),
                                G3W = int.Parse(ra[16]),
                                G4W = int.Parse(ra[17]),
                                G5W = int.Parse(ra[18]),

                                Drm = "OK"
                            };
                        }
                    }
                });
            }
        }
        public static void PPbaz2ilk()  // KULLANILMIYOR
        {
            /*
            Db.TransactAsync(() =>
            {
                var pps = Db.SQL<PP>("select r from PP r");
                foreach (var pp in pps)
                {
                    pp.RnkIlk = pp.RnkBaz;
                }
            });*/
        }
        public static void PPmove2baz()  // ARTIK KULLANILMIYOR
        {
            Db.TransactAsync(() =>
            {
                var pps = Db.SQL<PP>("select r from PP r");
                foreach (var pp in pps)
                {
                    if (pp.Rnk1 > 0)
                        pp.RnkBaz = pp.Rnk1;
                    else if (pp.Rnk2 > 0)
                        pp.RnkBaz = pp.Rnk2;
                    else if (pp.Rnk3 > 0)
                        pp.RnkBaz = pp.Rnk3;

                    //pp.RnkSon = 0;
                    pp.RnkSon = pp.RnkBaz;
                }

                // eski CC ler Rank Uretmesin
                var ccs = Db.SQL<CC>("select r from CC r");
                foreach (var cc in ccs)
                {
                    cc.IsRnkd = false;
                }
            });
        }

        public static void PPRD_17RankPXgosterme()
        {
            // 17.Sezonun RnkPX lerini heaplama
            // 17'in baslangicini 18nin baslangicindan al, 17.Sezonun IsRnkd = false yap, 17.Sezonu tekrar hesaplat

            PPRD rd = null;
            Db.TransactAsync(() =>
            {
                var pprds = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ?", 17);
                foreach(var pprd in pprds)
                {
                    rd = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ? and r.PP = ?", 18, pprd.PP).FirstOrDefault();
                    if (rd != null)
                        pprd.RnkBas = rd.RnkBas;
                    else
                        pprd.RnkBas = pprd.PP.RnkIlk;
                }

            });
        }

        public static void MAC_Insert(long CEToNo)  // Create from CETX
        {
            CET cet = Db.FromId<CET>((ulong)CEToNo);
            if (cet.IsMLY)
                return;

            CETX gcetx = null;

            Db.TransactAsync(() =>
            {
                // Singles
                var cetxs = Db.SQL<CETX>("select r from CETX r where r.CET = ? and r.H_G = ? order by r.SngIdx", cet, "H");
                foreach (var cetx in cetxs)
                {
                    if (cetx.SngIdx != 0)
                    {
                        gcetx = Db.SQL<CETX>("select r from CETX r where r.CET = ? and r.H_G = ? and r.SngIdx = ?", cet, "G", cetx.SngIdx).FirstOrDefault();
                        if (gcetx != null)
                        {
                            var newMac = new MAC
                            {
                                CC = cet.CC,
                                CEB = cet,
                                SoD = "S",
                                Trh = cet.Trh,
                                Idx = cetx.SngIdx,
                                HPP1 = cetx.PP,
                                GPP1 = gcetx.PP,
                                Drm = "OK"
                            };

                            // RnkSon = 0 ise Idx = 88 yapiliyor ve oynayamaz/diskalifiye
                            if (cetx.Idx == 88)
                                newMac.Drm = "hX";
                            if (gcetx.Idx == 88)
                            {
                                if (newMac.Drm == "hX")
                                    newMac.Drm = "X";
                                else
                                   newMac.Drm = "gX";
                            }

                            if (cetx.PPoNo == dskPPoNo) // Diskalifye Player
                                newMac.Drm = "hX";
                            if (gcetx.PPoNo == dskPPoNo)
                            {
                                if (newMac.Drm == "hX")
                                    newMac.Drm = "X";
                                else
                                    newMac.Drm = "gX";
                            }
                        }
                    }
                }

                Dictionary<int, DictDblCETX> dic = new Dictionary<int, DictDblCETX>();

                // Doubles
                bool first = true;
                MAC mac = null;
                cetxs = Db.SQL<CETX>("select r from CETX r where r.CET = ? and r.H_G = ? order by r.DblIdx", cet, "H");
                foreach (var cetx in cetxs)
                {
                    if (cetx.DblIdx != 0)
                    {
                        if (first)
                        {
                            dic[cetx.DblIdx] = new DictDblCETX { HPP1oNo = cetx.PPoNo };
                            dic[cetx.DblIdx].HPP1Idx = cetx.Idx;
                            first = false;
                        }
                        else
                        {
                            dic[cetx.DblIdx].HPP2oNo = cetx.PPoNo;
                            dic[cetx.DblIdx].HPP2Idx = cetx.Idx;
                            first = true;
                        }
                    }
                }
                first = true;
                cetxs = Db.SQL<CETX>("select r from CETX r where r.CET = ? and r.H_G = ? order by r.DblIdx", cet, "G");
                foreach (var cetx in cetxs)
                {
                    if (cetx.DblIdx != 0)
                    {
                        if (first)
                        {
                            dic[cetx.DblIdx].GPP1oNo = cetx.PPoNo;
                            dic[cetx.DblIdx].GPP1Idx = cetx.Idx;
                            first = false;
                        }
                        else
                        {
                            dic[cetx.DblIdx].GPP2oNo = cetx.PPoNo;
                            dic[cetx.DblIdx].GPP2Idx = cetx.Idx;
                            first = true;
                        }
                    }
                }

                foreach (var pair in dic)
                {
                    mac = new MAC
                    {
                        CC = cet.CC,
                        CEB = cet,
                        SoD = "D",
                        Trh = cet.Trh,
                        Idx = pair.Key,
                        HPP1 = Db.FromId<PP>(pair.Value.HPP1oNo),
                        HPP2 = Db.FromId<PP>(pair.Value.HPP2oNo),
                        GPP1 = Db.FromId<PP>(pair.Value.GPP1oNo),
                        GPP2 = Db.FromId<PP>(pair.Value.GPP2oNo),
                        Drm = "OK"
                    };
                    if (pair.Value.HPP1Idx == 88 || pair.Value.HPP2Idx == 88)
                    {
                        mac.Drm = "hX";
                    }
                    if (pair.Value.GPP1Idx == 88 || pair.Value.GPP2Idx == 88)
                    {
                        if (mac.Drm == "hX")
                            mac.Drm = "X";
                        else
                            mac.Drm = "gX";
                    }

                    if (mac.HPP1oNo == dskPPoNo || mac.HPP2oNo == dskPPoNo)
                    {
                        mac.Drm = "hX";
                    }
                    if (mac.GPP1oNo == dskPPoNo || mac.GPP2oNo == dskPPoNo)
                    {
                        if (mac.Drm == "hX")
                            mac.Drm = "X";
                        else
                            mac.Drm = "gX";
                    }
                }

                cet.IsMLY = true;
            });
        }

        public static void CETX_Insert(long CEToNo, string H_G)
        {
            CET cet = Db.FromId<CET>((ulong)CEToNo);
            CT ct = null;
            if (H_G == "H")
                ct = cet.HCT;
            else
                ct = cet.GCT;

            int idx = 1;
            Db.TransactAsync(() =>
            {
                Db.SQL("delete from CETX where CET = ? and H_G = ?", cet, H_G);
                
                var cetx = Db.SQL<CETX>("select r from CETX r where r.CET = ? and r.H_G = ?", cet, H_G).FirstOrDefault();
                if (cetx == null)   // Kayit yok insert
                {
                    var ctps = Db.SQL<CTP>("select r from CTP r where r.CT = ? order by r.Idx", ct);
                    foreach(var ctp in ctps)
                    {
                        if (ctp.Idx < 99)   // 99 olanlar cikti/ayrildi, 88 olanlarin Rnkleri belli degil oynayamaz!
                        {
                            new CETX
                            {
                                CC = cet.CC,
                                CET = cet,
                                CT = ct,
                                H_G = H_G,
                                Idx = ctp.Idx,
                                Idx2 = 0,
                                PP = ctp.PP,
                                SngIdx = 0,
                                DblIdx = 0
                            };
                            idx++;
                        }
                    }
                    var PP = Db.FromId<PP>(H.dskPPoNo);    // Diskalifiye Oyuncu
                    for (int i = 0; i < 3; i++)
                    {
                        new CETX
                        {
                            CC = cet.CC,
                            CET = cet,
                            CT = ct,
                            H_G = H_G,
                            Idx = idx++,
                            Idx2 = 0,
                            PP = PP,
                            SngIdx = 0,
                            DblIdx = 0
                        };

                    }
                }
            });
        }

        public static void PPRD_Ayarla()
        {
            Db.TransactAsync(() =>
            {
                var pprds = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ?", 17);
                foreach (var pprd in pprds)
                {
                    pprd.RnkBas = pprd.PP.RnkIlk;
                }

                var pps = Db.SQL<PP>("select r from PP r");
                foreach (var pp in pps)
                {
                    if (pp.RnkIlk == 0)
                    {
                        var rd = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ? and r.PP = ?", 17, pp).FirstOrDefault();
                        if (rd != null)
                            pp.RnkBaz = rd.RnkBas;
                        else
                        {
                            rd = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ? and r.PP = ?", 18, pp).FirstOrDefault();
                            if (rd != null)
                                pp.RnkBaz = rd.RnkBas;
                        }
                    }
                    else
                        pp.RnkBaz = pp.RnkIlk;
                }
            });
        }

        public static PPRD PPRD_TryInsert(PP pp, int Dnm)
        {
            // Geldigi yerde transaction baslamis olmali.
            int prvDnm = Dnm - 1;
            int prvRnkSon = 0;

            var pprd = Db.SQL<PPRD>("select r from PPRD r where r.PP = ? and r.Dnm = ?", pp, Dnm).FirstOrDefault();
            if (pprd == null)
            {
                if (Dnm == 17)  // Baslangic
                {
                    pprd = new PPRD
                    {
                        PP = pp,
                        Dnm = Dnm,
                        //RnkBas = pp.RnkBaz,
                        RnkBas = pp.RnkIlk,
                        //RnkSon = pp.RnkBaz,
                    };
                }
                else if (Dnm == 18)  // Baslangic
                {
                    pprd = new PPRD
                    {
                        PP = pp,
                        Dnm = Dnm,
                        RnkBas = pp.RnkBaz,
                    };
                }
                else
                {
                    var prvPPRD = Db.SQL<PPRD>("select r from PPRD r where r.PP = ? and r.Dnm = ?", pp, prvDnm).FirstOrDefault();
                    if (prvPPRD == null)
                        prvRnkSon = pp.RnkBaz;
                    else
                        prvRnkSon = prvPPRD.RnkSon;

                    pprd = new PPRD
                    {
                        PP = pp,
                        Dnm = Dnm,
                        RnkBas = prvRnkSon,
                    };
                }
            }

            return pprd;
        }

        public static void PPRD_TryDelete(PP pp, int Dnm)
        {
            // Donemde CTP, CF ve MAC kayitlarinin olmamasi gerek
            var pprd = Db.SQL("select r from PPRD r where r.PP = ? and r.Dnm = ?", pp, Dnm).FirstOrDefault();
            if (pprd != null)
            {
                var cf = Db.SQL("select r from CF r where r.PP = ? and r.CC.Dnm = ?", pp, Dnm).FirstOrDefault();
                if (cf == null)
                {
                    var ctp = Db.SQL("select r from CTP r where r.PP = ? and r.CC.Dnm = ?", pp, Dnm).FirstOrDefault();
                    if (ctp == null)
                    {
                        var mac = Db.SQL<MAC>("select r from MAC r where r.CC.Dnm = ? and (r.HPP1 = ? or r.HPP2 = ? or r.GPP1 = ? or r.GPP2 = ?)", Dnm, pp, pp, pp, pp).FirstOrDefault();
                        if (mac == null)
                        {
                            pprd.Delete();
                        }
                    }
                }
            }
        }

        public static void PPRD_DonemSonuIslemleri(int Dnm)
        {
            // pprd.RnkSon'a Ferdi RnkPX eklemek gerek
            // RnkSon = RnkBas + RnkPX zaten hesaplanmis
            // RnkPXf alani olmali
        }

        public static string PPRD_YeniDonemIslemleri(int Dnm)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int nor = 0;
            int prvDnm = Dnm - 1;

            var rd = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ?", Dnm).FirstOrDefault();
            if (rd != null)
                return $"Dnm:{Dnm} kayıtları var! İşlem yapılmadı.";

            // Bir onceki donemden yeni Donem yarat
            Db.TransactAsync(() =>
            {
                var prvPPRDs = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ?", prvDnm);
                foreach (var prvPPRD in prvPPRDs)
                {
                    nor++;
                    new PPRD
                    {
                        PP = prvPPRD.PP,
                        Dnm = Dnm,
                        RnkBas = prvPPRD.RnkSon,
                        RnkSon = prvPPRD.RnkSon,
                    };
                }

                // Sort
                int idx = 1;
                var pprds = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ? order by r.RnkBas DESC", Dnm);
                foreach (var pprd in pprds)
                {
                    pprd.RnkIdx = idx++;
                }
            });

            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds,5} ms YeniDonemIslemleri({Dnm}) NOR: {nor:n0}");

            return "";
        }

        public static string PPRD_DonemBasiIslemleri(int Dnm)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int nor = 0;

            // Donemin Takim Oyunculari icin
            // Takimda oynamayan Ferdi de oynayabilir.
            // IsFerdi girisi burdan yapiliyor. (CF ye sonradan ekleniyor) Kayit varsa silME.

            var rd = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ?", Dnm).FirstOrDefault();
            if (rd != null)
                return $"Dnm:{Dnm} kayıtları var! İşlem yapılmadı.";

            Db.TransactAsync(() =>
            {
                // Mevcutlari sil
                Db.SQL("delete from PPRD where Dnm = ?", Dnm);

                var ctps = Db.SQL<CTP>("select r from CTP r where r.CC.Dnm = ?", Dnm);
                foreach (var ctp in ctps)
                {
                    nor++;
                    PPRD_TryInsert(ctp.PP, Dnm);
                }

                // Sort
                int idx = 1;
                var pprds = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ? order by r.RnkBas DESC", Dnm);
                foreach(var pprd in pprds)
                {
                    pprd.RnkIdx = idx++;
                }
            });

            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds,5} ms DonemBaslangicIslemleri({Dnm}) NOR: {nor:n0}");

            return "";
        }



        public static void PP_RefreshSonuc()   // Tum oyuncular icin KULLANILMIYOR
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
            Console.WriteLine($"{watch.ElapsedMilliseconds,5} ms PP.RefreshSonuc(): MAC:{norMAC:n0}, PP:{norPP:n0}");
        }

        public static void PP_RefreshSonuc(MAC mac)    // Mactaki oyuncular icin yap
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Db.TransactAsync(() =>
            {
                var pps = Db.SQL<PP>("select r from PP r");

                PP_RefreshSonuc(mac.HPP1);
                PP_RefreshSonuc(mac.GPP1);
                PP_RefreshSonuc(mac.HPP2);
                PP_RefreshSonuc(mac.GPP2);
            });

            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds,5} ms PP.RefreshSonuc(MAC)");
        }

        public static void PP_RefreshSonuc(PP pp)  // Bir oyuncu
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

        public static void PP_RefeshCurrentActivities(int Dnm)
        {
            // DnmRun a gore PP leri update eder.

            Dictionary<ulong, string> dct = new Dictionary<ulong, string>();

            Db.TransactAsync(() =>
            {
                var pps = Db.SQL<PP>("select r from PP r");
                foreach (var pp in pps)
                {
                    dct[pp.PPoNo] = "";
                }

                var ccs = Db.SQL<CC>("select r from CC r where r.Dnm = ? order by r.Idx", Dnm);
                foreach (var cc in ccs)
                {
                    var ctps = Db.SQL<CTP>("select r from CTP r where r.CC = ?", cc);
                    foreach (var ctp in ctps)
                    {
                        dct[ctp.PPoNo] += ctp.CTAd + "/";
                    }

                    var cfs = Db.SQL<CF>("select r from CF r where r.CC = ?", cc);
                    foreach (var cf in cfs)
                    {
                        dct[cf.PPoNo] += cf.CC.Ad + "/";
                    }
                }

                pps = Db.SQL<PP>("select r from PP r");
                foreach (var pp in pps)
                {
                    pp.CurRuns = dct[pp.PPoNo];
                    if (dct[pp.PPoNo] == "")
                        pp.IsRun = false;
                    else
                        pp.IsRun = true;
                }
            });

        }

        

        public static void CT_RefreshSonuc() // KULLANMA
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var ccs = Db.SQL<CC>("select r from CC r where r.Skl = ?", "T");
            foreach (var cc in ccs)
            {
                var cts = Db.SQL<CT>("select r from CT r where r.CC = ?", cc);
                foreach (var ct in cts)
                {
                    CT_RefreshSonuc(ct);
                }

                // Sort for CC
                Db.TransactAsync(() =>
                {
                    cts = Db.SQL<CT>("SELECT r FROM CT r WHERE r.CC = ? order by r.PW DESC, r.KF DESC, r.Ad", cc);
                    int idx = 1;
                    foreach (var ct in cts)
                    {
                        ct.Idx = idx++;
                    }
                });

            }
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds,5} ms CT.RefreshSonuc()");
        }

        public static void CT_RefreshSonuc(int Dnm)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Db.TransactAsync(() =>
            {
                var ccs = Db.SQL<CC>("select r from CC r where r.Dnm = ? and r.Skl = ?", Dnm, "T");
                foreach (var cc in ccs)
                {
                    var cts = Db.SQL<CT>("select r from CT r where r.CC = ?", cc);
                    foreach (var ct in cts)
                    {
                        CT_RefreshSonuc(ct);
                    }

                    cts = Db.SQL<CT>("SELECT r FROM CT r where r.CC = ? order by r.PW DESC, r.KF DESC, r.Ad", cc);
                    int idx = 1;
                    foreach (var ct in cts)
                    {
                        ct.Idx = idx++;
                    }
                }
            });

            watch.Stop();
            Console.WriteLine($"{DateTime.Now:dd.MM.yy HH:mm}  {watch.ElapsedMilliseconds,5} ms CT.RefreshSonuc({Dnm})");
        }

        public static void CT_RefreshSonuc(CC cc)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var cts = Db.SQL<CT>("select r from CT r where r.CC = ?", cc);
            foreach (var ct in cts)
            {
                CT_RefreshSonuc(ct);
            }

            // Sort for CC
            Db.TransactAsync(() =>
            {
                cts = Db.SQL<CT>("SELECT r FROM CT r where r.CC = ? order by r.PW DESC, r.KF DESC, r.Ad", cc);
                int idx = 1;
                foreach (var ct in cts)
                {
                    ct.Idx = idx++;
                }
            });

            watch.Stop();
            Console.WriteLine($"{DateTime.Now:dd.MM.yy HH:mm}  {watch.ElapsedMilliseconds,5} ms CT.RefreshSonuc({cc.Ad})");
        }

        public static void CT_RefreshSonuc(CT ct)
        {
            int SMW = 0,
                SML = 0,
                DMW = 0,
                DML = 0,
                KW = 0, // Skor Win
                KL = 0,
                EW = 0, // Event/Musabaka Win
                EL = 0,
                EB = 0,
                EX = 0, // Hukmen
                PW = 0, // Puan Win
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
                    else if (cet.HPW == cet.GPW && cet.Drm == "OK")
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
                    else if (cet.HPW == cet.GPW && cet.Drm == "OK")
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


        public static void CTP_RefreshSonuc(int Dnm)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int nor = 0;
            int hsw = 0, gsw = 0, hmw = 0, gmw = 0, hmx = 0, gmx = 0;
            string sod = "";

            CT HCT, GCT;

            List<ListMac> MacList = new List<ListMac>();

            //for (int i = 0; i < 100; i++)
            {
                var ccs = Db.SQL<CC>("select r from CC r where r.Dnm = ?", Dnm);
                foreach (var cc in ccs)
                {
                    var cets = Db.SQL<CET>("select r from CET r where r.CC = ?", cc);
                    foreach (var cet in cets)
                    {
                        HCT = cet.HCT;
                        GCT = cet.GCT;

                        var macs = Db.SQL<MAC>("select r from MAC r where r.CEB = ?", cet);
                        foreach (var mac in macs)
                        {
                            nor++;

                            sod = mac.SoD;
                            hsw = mac.HSW;
                            gsw = mac.GSW;
                            hmw = mac.HMW;
                            gmw = mac.GMW;
                            hmx = mac.HMX;
                            gmx = mac.GMX;

                            MacList.Add(new ListMac
                            {
                                CT = HCT,
                                PP = mac.HPP1,
                                SoD = sod,
                                SW = hsw,
                                SL = gsw,
                                MW = hmw,
                                ML = gmw,
                            });

                            MacList.Add(new ListMac
                            {
                                CT = GCT,
                                PP = mac.GPP1,
                                SoD = sod,
                                SW = gsw,
                                SL = hsw,
                                MW = gmw,
                                ML = hmw,
                            });

                            if (sod == "D")
                            {
                                MacList.Add(new ListMac
                                {
                                    CT = HCT,
                                    PP = mac.HPP2,
                                    SoD = sod,
                                    SW = hsw,
                                    SL = gsw,
                                    MW = hmw,
                                    ML = gmw,
                                });

                                MacList.Add(new ListMac
                                {
                                    CT = GCT,
                                    PP = mac.GPP2,
                                    SoD = sod,
                                    SW = gsw,
                                    SL = hsw,
                                    MW = gmw,
                                    ML = hmw,
                                });
                            }
                        }
                    }
                }
            }

            var groupedResult = MacList
            //.OrderBy(x => x.CT).ThenBy(x => x.PP) //.ThenBy(x => x.SoD)   // Gerek yok
            .GroupBy(s => new { s.CT, s.PP, s.SoD })
            .Select(g => new
            {
                ct = g.Key.CT,
                pp = g.Key.PP,
                sod = g.Key.SoD,
                tSW = g.Sum(x => x.SW),
                tSL = g.Sum(x => x.SL),
                tMW = g.Sum(x => x.MW),
                tML = g.Sum(x => x.ML),
            });

            //iterate each group 
            CTP ctp = null;
            Db.TransactAsync(() =>
            {
                foreach (var gr in groupedResult)
                {
                    ctp = Db.SQL<CTP>("select r from CTP r where r.CT = ? and r.PP = ?", gr.ct, gr.pp).FirstOrDefault();
                    if (ctp != null)
                    {
                        if (gr.sod == "S")
                        {
                            ctp.SSW = gr.tSW;
                            ctp.SSL = gr.tSL;
                            ctp.SMW = gr.tMW;
                            ctp.SML = gr.tML;
                        }
                        else if (gr.sod == "D")
                        {
                            ctp.DSW = gr.tSW;
                            ctp.DSL = gr.tSL;
                            ctp.DMW = gr.tMW;
                            ctp.DML = gr.tML;
                        }
                    }
                }
            });

            watch.Stop();
            Console.WriteLine($"{DateTime.Now:dd.MM.yy HH:mm}  {watch.ElapsedMilliseconds,5} ms CTP_RefreshSonuc({Dnm}) NOR:{nor:n0}");
        }


        public static void CTP_RefreshSonuc()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var ccs = Db.SQL<CC>("select r from CC r where r.Skl = ?", "T");
            foreach (var cc in ccs)
            {
                CTP_RefreshSonuc(cc);
            }
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds,5} ms CTP.RefreshSonuc()");
            //Console.WriteLine($"CTP.RefreshSonuc(): {watch.ElapsedMilliseconds} ms  {watch.ElapsedTicks} ticks");
        }

        public static void CTP_RefreshSonuc(CC cc)
        {
            var cts = Db.SQL<CT>("select r from CT r where r.CC = ?", cc);
            foreach (var ct in cts)
            {
                CTP_RefreshSonuc(ct);
            }
        }

        // Takimdaki Oyuncularin Yaptigi Maclari toplayarak Sonuclari update
        public static void CTP_RefreshSonuc(CT ct)
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

        public static void CTP_UpdateRnkBas()
        {
            // PP.RnkSon -> CTP.RnkBas 
            Db.TransactAsync(() =>
            {
                var ctps = Db.SQL<CTP>("select r from CTP r");
                foreach (var ctp in ctps)
                {
                    //ctp.IsRun = ctp.PP.IsRun;
                    //ctp.RnkBas = ctp.PP.RnkSon;
                    ctp.RnkBit = 0;
                }

            });
        }



        public static void CF_RefreshSonuc()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Db.TransactAsync(() =>
            {
                var ccs = Db.SQL<CC>("select r from CC r where r.Skl = ?", "F");
                foreach (var cc in ccs)
                {
                    CF_RefreshSonuc(cc);
                }
            });

            watch.Stop();
            Console.WriteLine($"{DateTime.Now:dd.MM.yy HH:mm}  {watch.ElapsedMilliseconds,5} ms CF.RefreshSonuc()");
            //Console.WriteLine($"CF.RefreshSonuc(): {watch.ElapsedMilliseconds} ms  {watch.ElapsedTicks} ticks");
        }

        public static void CF_RefreshSonuc(CC cc)
        {
            Dictionary<ulong, DictFerdiStat> dnm = new Dictionary<ulong, DictFerdiStat>();    // Players
            ulong hPPoNo, gPPoNo, PPoNo;

            // CCnin CF lerindeki Players
            var cfs = Db.SQL<CF>("select r from CF r where r.CC = ?", cc);
            foreach (var cf in cfs)
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

        public static void CF_Create()
        {
            if (Db.SQL<CF>("select r from CF r").FirstOrDefault() != null)
                return;

            Db.TransactAsync(() =>
            {
                int i = 1;
                CC cc = null;

                var pprds = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ? and r.IsFerdi = ? order by r.RnkBas DESC, r.PP.Ad", 18, true);
                foreach (var pprd in pprds)
                {
                    if (i == 1)         // A
                        cc = Db.FromId<CC>(8420);
                    else if (i == 11)    // B
                        cc = Db.FromId<CC>(8421);
                    else if (i == 31)    // C
                        cc = Db.FromId<CC>(8422);
                    else if (i == 51)    // D
                        cc = Db.FromId<CC>(8423);
                    else if (i == 71)    // E
                        cc = Db.FromId<CC>(8424);
                    else if (i == 92)   // F
                        cc = Db.FromId<CC>(8425);
                    else if (i == 113)   // G
                        cc = Db.FromId<CC>(8426);
                    else if (i == 134)   // H
                        cc = Db.FromId<CC>(8427);
                    else if (i == 155)   // I
                        cc = Db.FromId<CC>(8428);
                    else if (i == 176)   // J
                        cc = Db.FromId<CC>(8429);

                    new CF
                    {
                        CC = cc,
                        PP = pprd.PP,
                        IsRun = true,
                        RnkBas = pprd.RnkBas
                    };

                    i++;
                }
            });
        }



        public static void CEF_RefreshSonuc()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Db.TransactAsync(() =>
            {
                var cefs = Db.SQL<CEF>("select r from CEF r");
                foreach (var cef in cefs)
                {
                    CEF_RefreshSonuc(cef);
                }
            });

            watch.Stop();
            Console.WriteLine($"{DateTime.Now:dd.MM.yy HH:mm}  {watch.ElapsedMilliseconds,5} ms CEF.RefreshSonuc()");
            //Console.WriteLine($"CEF.RefreshSonuc(): {watch.ElapsedMilliseconds} ms  {watch.ElapsedTicks} ticks");
        }

        public static void CEF_RefreshSonuc(CEF cef)
        {
            Db.TransactAsync(() =>
            {
                if (cef.Drm == "OK")
                {
                    // Ferdi Event'in tek bir single maci olur.
                    //var mac = Db.SQL<MAC>("select r from MAC r where r.CEB.ObjectNo = ? and SoD = ?", cef.GetObjectNo(), "S").FirstOrDefault();
                    //var mac = Db.SQL<MAC>("select r from MAC r where r.CEB = ? and SoD = ?", cef, "S").FirstOrDefault();
                    var mac = Db.SQL<MAC>("select r from MAC r where r.CEB = ?", cef).FirstOrDefault();
                    if (mac != null)
                    {
                        cef.HSSW = mac.HSW;
                        cef.GSSW = mac.GSW;
                        cef.HSMW = mac.HMW;
                        cef.GSMW = mac.GMW;
                        if (mac.HMW > mac.GMW)  // Home kazandi
                        {
                            cef.HPW = cef.CC.TEGP;
                            cef.GPW = cef.CC.TEMP;
                        }
                        else if (mac.HMW < mac.GMW)  // Guest kazandi
                        {
                            cef.HPW = cef.CC.TEMP;
                            cef.GPW = cef.CC.TEGP;
                        }
                        else // Berabere
                        {
                            cef.HPW = cef.CC.TEBP;
                            cef.GPW = cef.CC.TEBP;
                        }
                    }
                }
                else if (string.IsNullOrEmpty(cef.Drm)) // Drm sonradan OK den NULL/Empty dondurulebilir
                {
                    cef.HSSW = 0;
                    cef.GSSW = 0;
                    cef.HSMW = 0;
                    cef.GSMW = 0;
                }
            });
        }

        public static string CEF_CreateEvents(ulong CCoNo)
        {
            ArrayList al = new ArrayList();
            DateTime Trh = new DateTime(2099, 01, 01);
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



        public static void CET_RefreshSonuc()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            var cets = Db.SQL<CET>("select r from CET r");
            foreach (var cet in cets)
            {
                CET_RefreshSonuc(cet);
            }

            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds,5} ms CET.RefreshSonuc()");
        }

        public static void CET_RefreshSonuc(int Dnm)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //var cets = Db.SQL<CET>("select r from CET r where r.CC.Dnm = ?", Dnm);
            var cets = Db.SQL<CET>("select r from CET r");
            foreach (var cet in cets)
            {
                if (cet.CC.Dnm == Dnm)
                    CET_RefreshSonuc(cet);
            }

            watch.Stop();
            Console.WriteLine($"{DateTime.Now:dd.MM.yy HH:mm}  {watch.ElapsedMilliseconds,5} ms CET.RefreshSonuc({Dnm})");
        }

        public static void CET_RefreshSonuc(CET cet)
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
                    hKW = 0;
                    gKW = cet.CC.TNSM * TSMK + cet.CC.TNDM * TDMK;
                    hPW = TEXP;
                    gPW = TEGP;
                }
                else if (cet.Drm == "gX")  // Guest Gelmedi/Cikmadi
                {
                    hKW = cet.CC.TNSM * TSMK + cet.CC.TNDM * TDMK;
                    gKW = 0;
                    hPW = TEGP;
                    gPW = TEXP;
                }
                else if (cet.Drm == "hD")  // Home Diskalifiye
                {
                    hKW = 0;
                    gKW = cet.CC.TNSM * TSMK + cet.CC.TNDM * TDMK;
                    hPW = TEXP;
                    gPW = TEGP;
                }
                else if (cet.Drm == "gD")  // Guest Diskalifiye
                {
                    hKW = cet.CC.TNSM * TSMK + cet.CC.TNDM * TDMK;
                    gKW = 0;
                    hPW = TEGP;
                    gPW = TEXP;
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

        public static string CET_CreateEvents(ulong CCoNo)
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



        public static void MAC_RefreshSonuc()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int nor = 0;

            Db.TransactAsync(() => {
                var macs = Db.SQL<MAC>("select r from MAC r");
                foreach (var mac in macs)
                {
                    nor++;
                    MAC_RefreshSonuc(mac);
                }
            });

            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds,5} ms MAC.RefreshSonuc() NOR:{nor:n0}");
        }

        public static void MAC_RefreshSonuc(int Dnm)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int nor = 0;

            Db.TransactAsync(() => {
                //var macs = Db.SQL<MAC>("select r from MAC r where r.CC.Dnm = ?", Dnm);
                var macs = Db.SQL<MAC>("select r from MAC r");
                foreach (var mac in macs)
                {
                    if (mac.CC.Dnm == Dnm)
                    { 
                        nor++;
                        MAC_RefreshSonuc(mac);
                    }
                }
            });

            watch.Stop();
            Console.WriteLine($"{DateTime.Now:dd.MM.yy HH:mm}  {watch.ElapsedMilliseconds,5} ms MAC.RefreshSonuc({Dnm}) NOR:{nor:n0}");
        }

        public static void MAC_RefreshSonuc(MAC mac)
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
            else if (mac.Drm == "hX" || mac.Drm == "hD")   // Maca Cıkmadı/Diskalifiye
            {
                hMX = 1;

                hSW = 0;
                hMW = 0;
                gMW = 1;
                gSW = 3;
            }
            else if (mac.Drm == "gX" || mac.Drm == "gD")
            {
                gMX = 1;

                hMW = 1;
                hSW = 3;
                gMW = 0;
                gSW = 0;
            }
            else if (mac.Drm == "hH")   // Sıralama Hatası (Set sonuclari aynen kaliyor)
            {
                hMX = 0;
                hMW = 0;
                gMW = 1;
                hSW = 0;
                gSW = 3;
            }
            else if (mac.Drm == "gH")
            {
                gMX = 0;
                hMW = 1;
                gMW = 0;
                hSW = 3;
                gSW = 0;
            }

            Db.TransactAsync(() =>
            {
                mac.HSW = hSW;
                mac.GSW = gSW;
                mac.HMW = hMW;
                mac.GMW = gMW;
                mac.HMX = hMX;
                mac.GMX = gMX;
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

        public static void MAC_RefreshGlobalRank()  // KULLANILMIYOR
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
                    if (mac.CC.IsRnkd)  // Rank hesaplanacak ise (Ferdiler en sonunda hesaplanacak, gec, isRnkd = false olacak)
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

                // Hic mac yapmamislari ve Ayrilmis olanlari Adina gore sirala
                int dc = 0;
                //foreach (var p in Db.SQL<PP>("select p from PP p where p.SMT = ? or p.IsRun = ? order by p.Ad", 0, false))
                foreach (var p in Db.SQL<PP>("select p from PP p where p.IsRun = ? order by p.Ad", false))
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
                    pp.RnkSon = pair.Value;   // Siralamayi yukarda yaptin RnkSon sifirlama
                    pp.RnkIdx = idx++;
                }
            });

            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds,5} ms MAC.RefreshGlobalRank() NOR: {nor:n0}");
            //Console.WriteLine($"RefreshGlobalRank {nor}: {watch.ElapsedMilliseconds} ms  {watch.ElapsedTicks} ticks");
        }

        public static void MAC_RefreshGlobalRank(DateTime trh)  // Kullanilmiyor
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
            Console.WriteLine($"RefreshGlobalRank {nor}: {watch.ElapsedMilliseconds} ms  {watch.ElapsedTicks} ticks");
        }

        public static void CEF_RefreshDnm18()   //18.Donem FerdiEvent lerin PX lerini hesapla sonrasinda CF topPX
        {
            //Donem/Sezon 18 icin
            int hPX = 0, gPX = 0;
            ulong ppNo = 0;

            Dictionary<ulong, int> rdDic = new Dictionary<ulong, int>();    // Players Donem Ranks
            Dictionary<ulong, int> tPXDic = new Dictionary<ulong, int>(); // Players PX toplam

            Db.TransactAsync(() =>
            {
                var pprds = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ?", 18);
                foreach (var pprd in pprds)
                {
                    ppNo = pprd.PP.GetObjectNo();
                    rdDic[ppNo] = pprd.RnkBas;
                    tPXDic[ppNo] = -9999;
                }

                var ccs = Db.SQL<CC>("select r from CC r where r.Dnm = ? and r.Skl = ?", 18, "F");
                foreach (var cc in ccs)
                {
                    var cefs = Db.SQL<CEF>("select r from CEF r where r.CC = ?", cc);
                    foreach (var cef in cefs)
                    {
                        if (cef.Drm == "OK")
                        {
                            hPX = compHomeRnkPX(cef.HSMW == 0 ? false : true, rdDic[cef.HPPoNo], rdDic[cef.GPPoNo]);
                            gPX = -hPX;

                            cef.HPX = hPX;
                            cef.GPX = gPX;

                            if (tPXDic[cef.HPPoNo] == -9999)
                                tPXDic[cef.HPPoNo] = 0;
                            tPXDic[cef.HPPoNo] += hPX;
                            if (tPXDic[cef.GPPoNo] == -9999)
                                tPXDic[cef.GPPoNo] = 0;
                            tPXDic[cef.GPPoNo] += gPX;
                        }
                        else
                        {
                            cef.HPX = 0;
                            cef.GPX = 0;
                        }
                    }

                    var cfs = Db.SQL<CF>("select r from CF r where r.CC = ?", cc);
                    int ppSay = 0;
                    int puan = 0, maxPuan = 0, minPuan = 0;
                    foreach (var cf in cfs)
                    {
                        cf.PX = tPXDic[cf.PPoNo];
                        ppSay++;
                    }
                    if (ppSay <= 10)
                    {
                        maxPuan = 5;
                        minPuan = -5;
                    }
                    else
                    {
                        maxPuan = 10;
                        minPuan = -10;
                    }
                    puan = maxPuan;
                    cfs = Db.SQL<CF>("select r from CF r where r.CC = ? order by r.PX DESC, r.RnkBas", cc);
                    foreach (var cf in cfs)
                    {
                        cf.PX = tPXDic[cf.PPoNo];
                        if (tPXDic[cf.PPoNo] == -9999)
                            cf.PW = minPuan;
                        else
                        {
                            cf.PW = puan;
                            puan--;
                            if (puan < minPuan)
                                puan = minPuan;
                        }
                    }
                }
            });
        }

        public static void PPRD_RefreshSonuc(int Dnm)
        {
            // MAC_RefreshDonemFerdiRank den once yapilmali
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int nor = 0;

            ulong hPPoNo, gPPoNo;
            int hpRnk, gpRnk;
            int hPX = 0;
            PPRD npprd = null;

            Dictionary<ulong, DictPPRD> rdDic = new Dictionary<ulong, DictPPRD>();    // Players Donem Ranks

            Db.TransactAsync(() =>
            {
                var pprds = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ?", Dnm);
                foreach (var pprd in pprds)
                {
                    rdDic[pprd.PP.GetObjectNo()] = new DictPPRD
                    {
                        RnkSon = pprd.RnkBas,
                    };
                }

                // Sadece Single Maclar Rank uretir.
                // O donemin Maclari taranacak
                //var macs = Db.SQL<MAC>("select r from MAC r where r.CC.Dnm = ? order by r.Trh", Dnm); // Bu 2 kat yavas calisiyor
                var macs = Db.SQL<MAC>("select r from MAC r order by r.Trh");
                foreach (var mac in macs)
                {
                    if (mac.CC.Dnm != Dnm || mac.SoD == "D") // Performans daha iyi Query de Single arama 
                        continue;

                    nor++;

                    hPPoNo = mac.HPP1.GetObjectNo();
                    gPPoNo = mac.GPP1.GetObjectNo();

                    // Oyuncu kaydi PPRD de yoksa yarat ve kullan
                    if (!rdDic.ContainsKey(hPPoNo))
                    {
                        npprd = PPRD_TryInsert(mac.HPP1, Dnm);
                        rdDic[hPPoNo] = new DictPPRD
                        {
                            RnkSon = npprd.RnkBas,
                        };
                    }
                    if (!rdDic.ContainsKey(gPPoNo))
                    {
                        npprd = PPRD_TryInsert(mac.GPP1, Dnm);
                        rdDic[gPPoNo] = new DictPPRD
                        {
                            RnkSon = npprd.RnkBas,
                        };
                    }

                    hpRnk = rdDic[hPPoNo].RnkSon;
                    gpRnk = rdDic[gPPoNo].RnkSon;

                    hPX = 0;
                    if (mac.CC.IsRnkd)  // Rank hesaplanacak ise (Ferdiler en sonunda hesaplanacak, isRnkd = false olacak)
                        if (mac.Drm == "OK" && hpRnk != 0 && gpRnk != 0)
                            hPX = compHomeRnkPX(mac.HMW == 0 ? false : true, hpRnk, gpRnk);

                    // Update MAC
                    mac.HRnkPX = hPX;
                    mac.HRnk = hpRnk;

                    mac.GRnkPX = -hPX;
                    mac.GRnk = gpRnk;
                    
                    // Update PP dictionary
                    rdDic[hPPoNo].RnkSon = hpRnk + hPX;
                    rdDic[gPPoNo].RnkSon = gpRnk - hPX;

                    // Update PX dictionary
                    rdDic[hPPoNo].TopPX += hPX;
                    rdDic[gPPoNo].TopPX += -hPX;

                    // Update SonPX dictionary
                    rdDic[hPPoNo].SonPX = hPX;
                    rdDic[gPPoNo].SonPX = -hPX;

                    // Update MW/ML dictionary
                    rdDic[hPPoNo].MW += mac.HMW;
                    rdDic[hPPoNo].ML += mac.GMW;
                    rdDic[gPPoNo].MW += mac.GMW;
                    rdDic[gPPoNo].ML += mac.HMW;
                    // Update SW/SL dictionary
                    rdDic[hPPoNo].SW += mac.HSW;
                    rdDic[hPPoNo].SL += mac.GSW;
                    rdDic[gPPoNo].SW += mac.GSW;
                    rdDic[gPPoNo].SL += mac.HSW;
                }

                // Update PPRD.RnkPX
                ulong ppNO = 0;
                pprds = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ?", Dnm);
                foreach (var pprd in pprds)
                {
                    ppNO = pprd.PP.GetObjectNo();
                    pprd.TopPX = rdDic[ppNO].TopPX;
                    pprd.RnkSon = pprd.RnkBas + rdDic[ppNO].TopPX;
                    pprd.SonPX = rdDic[ppNO].SonPX;

                    pprd.MW = rdDic[ppNO].MW;
                    pprd.ML = rdDic[ppNO].ML;
                    pprd.SW = rdDic[ppNO].SW;
                    pprd.SL = rdDic[ppNO].SL;
                }

                int idx = 1;
                pprds = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ? order by r.RnkSon DESC, r.RnkBas DESC", Dnm);
                foreach (var pprd in pprds)
                {
                    pprd.RnkIdx = idx++;
                }
            });

            watch.Stop();
            Console.WriteLine($"{DateTime.Now:dd.MM.yy HH:mm}  {watch.ElapsedMilliseconds,5} ms PPRD.RefreshSonuc({Dnm}) NOR: {nor:n0}");
        }

        public static void PPRD_RefreshCurRuns(int Dnm)
        {
            // DnmRun a gore PPRD leri update eder.
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int nor = 0;

            string info;
            char[] charsToTrim = { '♦', ' ' };

            Db.TransactAsync(() =>
            {
                var pprds = Db.SQL<PPRD>("SELECT r FROM PPRD r where r.Dnm = ?", Dnm);
                foreach (var pprd in pprds)
                {
                    nor++;
                    // Oynadigi CTP, CF leri bul
                    info = "";
                    var ctps = Db.SQL<CTP>("select r from CTP r where r.CC.Dnm = ? and r.PP = ?", H.DnmRun, pprd.PP);
                    foreach (var ctp in ctps)
                    {
                        info += ctp.CTAd + " ♦ ";
                    }

                    var cf = Db.SQL<CF>("select r from CF r where r.PP = ? and r.CC.Dnm = ?", pprd.PP, Dnm).FirstOrDefault();  // Ferdi olarak tek ligde oynayabilir
                    if(cf != null)
                        info += cf.CC.Ad + " ♦ ";

                    pprd.CurRuns = info.TrimEnd(charsToTrim);
                }
            });

            watch.Stop();
            Console.WriteLine($"{DateTime.Now:dd.MM.yy HH:mm}  {watch.ElapsedMilliseconds,5} ms PPRD_RefreshCurRuns({Dnm}) NOR: {nor:n0}");
        }



        public static void MAC_RefreshDonemFerdiRank(int DnmRun)
        {
            // TakimTurnuvalar bittikten sonra yapilacak
            // Oyuncunun RnkSon'u Her Ferdi Macta degismeyecek
            // Her Macini RnkBas/Son ile oynayacak aldigi PX ler toplanacak
            // Bu PX lerin yarisini(KURAL) PPRC.RnkPX'e koy 
            // Bir oyuncu ayni donemde birden cok Ferdi turnuvada oynayamaz.

            Dictionary<ulong, int> dctRnk = new Dictionary<ulong, int>();   // Oyuncunun Donem Basi/Sonu Rank
            Dictionary<ulong, int> dctPX = new Dictionary<ulong, int>();    // Oyuncunu ilgili CC de aldigi RnkPX toplami. Her CC icin hesaplanir

            // Donemin Bas/Son ranklerini al
            ulong ppNO = 0;
            var pprds = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ?", DnmRun);
            foreach (var pprd in pprds)
            {
                ppNO = pprd.PP.PPoNo;
                dctRnk[ppNO] = pprd.RnkBas;    // Istendiginde RnkBas da olabilir. RnkBas olmasi daha mantikli
                dctPX[ppNO] = 0;
            }

            int hPX = 0;
            ulong hPP = 0, gPP = 0;

            Db.TransactAsync(() =>
            {
                // Aktif/Son Ferdi Turnuvalar
                var macs = Db.SQL<MAC>("select r from MAC r where r.CC.Dnm = ? and r.CC.Skl = ?", DnmRun, "F");
                foreach (var mac in macs)
                {
                    hPP = mac.HPP1.GetObjectNo();
                    gPP = mac.GPP1.GetObjectNo();

                    // PX hsapla
                    hPX = 0;
                    if (mac.Drm == "OK")
                        hPX = compHomeRnkPX(mac.HMW == 0 ? false : true, dctRnk[hPP], dctRnk[gPP]);

                    dctPX[hPP] += hPX;
                    dctPX[gPP] += -hPX;
                }

                // Update PPRD
                pprds = Db.SQL<PPRD>("select r from PPRD r where r.Dnm = ?", DnmRun);
                foreach (var pprd in pprds)
                {
                    pprd.TopPX += dctPX[pprd.PP.PPoNo] / 2;
                }
            });
        
        }



        public static void DD_RefreshSonuc(int Dnm)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            int nor = 0;

            int SMC = 0; // SingleMacCount
            int DSC = 0;
            int SSC = 0;
            int SNC = 0; // SingleSayiCount
            int DNC = 0;
            int DMC = 0;

            HashSet<ulong> ppHS = new HashSet<ulong>(); // DnmRun da oynayanlari ayirmak icin.

            Db.TransactAsync(() =>
            {
                var macs = Db.SQL<MAC>("select r from MAC r");
                foreach (var mac in macs)
                {
                    if (mac.CC.Dnm != Dnm)
                        continue;

                    ppHS.Add(mac.HPP1oNo);     // Oynuyor
                    ppHS.Add(mac.HPP2oNo);     // Oynuyor
                    ppHS.Add(mac.GPP1oNo);     // Oynuyor
                    ppHS.Add(mac.GPP2oNo);     // Oynuyor

                    nor++;
                    if (mac.SoD == "S")
                    {
                        SMC += 1;
                        SSC += mac.HSW + mac.GSW;
                        SNC += mac.H1W + mac.H2W + mac.H3W + mac.H4W + mac.H5W + mac.H6W + mac.H7W + mac.G1W + mac.G2W + mac.G3W + mac.G4W + mac.G5W + mac.G6W + mac.G7W;

                    }
                    else
                    {
                        DMC += 1;
                        DSC += mac.HSW + mac.GSW;
                        DNC += mac.H1W + mac.H2W + mac.H3W + mac.H4W + mac.H5W + mac.H6W + mac.H7W + mac.G1W + mac.G2W + mac.G3W + mac.G4W + mac.G5W + mac.G6W + mac.G7W;
                    }
                }

                DD dd = Db.SQL<DD>("select r from DD r where r.Dnm = ?", Dnm).FirstOrDefault();
                dd.SMC = SMC;
                dd.DMC = DMC;
                dd.SSC = SSC;
                dd.DSC = DSC;
                dd.SNC = SNC;
                dd.DNC = DNC;

                dd.KOC = (int)Db.SQL<long>("select count(r) from PPRD r where r.Dnm = ?", Dnm).FirstOrDefault();
                dd.OOC = ppHS.Count;
            });


            watch.Stop();
            Console.WriteLine($"{DateTime.Now:dd.MM.yy HH:mm}  {watch.ElapsedMilliseconds,5} ms DD_RefreshSonuc({Dnm}) NOR: {nor:n0}");
        }



        public static void PerfDeneme()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int nor = 0;
            ulong aaa = 0;

            for (int i = 0; i < 1_000; i++)
            {
                //var macs = Db.SQL<MAC>("select r from MAC r");                        // 8300 ms
                //var macs = Db.SQL<MAC>("select r from MAC r order by Trh");           //  8600 ms
                var macs = Db.SQL<MAC>("select r from MAC r where r.CC.Dnm = ?", 17);   // 14500 ms
                foreach (var mac in macs)
                {
                    //if (mac.CC.Dnm == 17)
                    {
                        aaa += mac.HPP1.GetObjectNo();
                        //nor++;
                    }
                }
            }
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds,5} ms PerfDeneme() NOR: {nor:n0}");

        }

        public static void Deneme2()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            int nor = 0;
            int dnm = 0;
            List<ListMac> LM = new List<ListMac>();

            var macs = Db.SQL<MAC>("select r from MAC r");
            foreach (var mac in macs)
            {
                if (mac.CEB is CET)
                {
                    dnm = mac.CC.Dnm;

                    LM.Add(new ListMac
                    {
                        CT = (mac.CEB as CET).HCT,
                        PP = mac.HPP1,
                        Dnm = dnm,
                        MW = mac.HMW,
                        ML = mac.GMW,
                        SW = mac.HSW,
                        SL = mac.GSW,
                    });

                    LM.Add(new ListMac
                    {
                        CT = (mac.CEB as CET).GCT,
                        PP = mac.GPP1,
                        Dnm = dnm,
                        MW = mac.GMW,
                        ML = mac.HMW,
                        SW = mac.GSW,
                        SL = mac.HSW,
                    });

                    nor += 2;
                }
            }

            var groupedResult = LM
                           //.OrderBy(x => x.CT.CToNo).ThenBy(x => x.PP.PPoNo)
                           //.GroupBy(s => new { s.CT.CToNo, s.PP.PPoNo })
                           .GroupBy(s => new { s.CT, s.PP })
                           .Select(g => new
                           {
                               gct = g.Key.CT,
                               gpp = g.Key.PP,
                               tSW = g.Sum(x => x.SW),
                               tSL = g.Sum(x => x.SL),
                               tMW = g.Sum(x => x.MW),
                               tML = g.Sum(x => x.ML),
                           });


            CTP ctp = null;

            Db.TransactAsync(() =>
            {

                foreach (var gr in groupedResult)
                {
                    //Console.WriteLine($"{gr.gct}");
                    ctp = Db.SQL<CTP>("select r from CTP r where r.CT = ? and r.PP = ?", gr.gct, gr.gpp).FirstOrDefault();
                    /*
                    if (ctp != null)
                    {
                        ctp.SMW = gr.tMW;
                        ctp.SML = gr.tML;
                    }
                    */
                }
            });
            
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds,5} ms Deneme2() NOR: {nor:n0}");
        }

        public static string EncodeQueryString(string data)
        {
            string encodedData = String.Empty;
            try
            {
                byte[] data_byte = Encoding.UTF8.GetBytes(data);
                encodedData = System.Net.WebUtility.UrlEncode(Convert.ToBase64String(data_byte));
            }
            catch (Exception exception)
            {
                //Log exception
            }
            return encodedData;
        }

        public static string DecodeQueryString(string data)
        {
            string decodedData = String.Empty;
            try
            {
                //System.Net.WebUtility.UrlEncode
                //byte[] data_byte = Convert.FromBase64String(HttpUtility.UrlDecode(data));
                byte[] data_byte = Convert.FromBase64String(System.Net.WebUtility.UrlDecode(data));
                decodedData = Encoding.UTF8.GetString(data_byte);
            }
            catch (Exception exception)
            {
                //Log exception
            }
            return decodedData;
        }

        public static void SendMail(string eMail)
        {
            // gMail
            string body = $"<!DOCTYPE html><html><body><a href='http://masatenisi.online/bodved/confirmemail/{eMail}'>BODVED üyeliğiniz için tıklayınız</a></body></html>";

            MailMessage mail = new MailMessage();
            mail.To.Add("sener.demiral@gmail.com");
            mail.Subject = "Deneme";

            mail.From = new MailAddress("masatenisi.online@gmail.com", "BODVED");  // gMail
            mail.IsBodyHtml = true;
            mail.Body = body;


            SmtpClient smtp = new SmtpClient("smtp.gmail.com");   // gMail
            smtp.Credentials = new System.Net.NetworkCredential("masatenisi.online", "CanDilSen09");  // gMail
            smtp.EnableSsl = true;    // gMail
            smtp.Port = 587;


            //smtp.Send(mail);
            object userToken = null;
            smtp.SendAsync(mail, userToken);
        }

    }

}
