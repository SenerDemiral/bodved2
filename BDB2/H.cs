using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Starcounter;

namespace BDB2
{
    public static class H
    {
        public static CultureInfo cultureTR = CultureInfo.CreateSpecificCulture("tr-TR");  // Tarihde gun gostermek icin

        public static void Write2Log(string Msg)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\Starcounter\MyLog\BodVed-Log.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + Msg);
            }
        }

        public static void PopAll()
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

        /*
        public static void PopPP()
        {
            if (Db.SQL<PP>("select r from PP r").FirstOrDefault() != null)
                return; // Kayit var yapma

            // 0:PK,1:RnkBaz,2:Sex,3:DgmTrh,4:Ad,5:eMail,6:Tel,7:Rnk1,8:Rnk2,9:Rnk3
            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-PP.txt", System.Text.Encoding.UTF8))
            {
                string line;
                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            new PP
                            {
                                PK = Convert.ToUInt64(ra[0]),
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
                        }
                    }
                });
            }
        }

        public static void PopCC()
        {
            if (Db.SQL<CC>("select r from CC r").FirstOrDefault() != null)
                return; // Kayit var yapma

            //0:PK|1:ID|2:Ad|3:Skl|4:Grp|5:Idx|6:Lig|7:RnkID|8:RnkAd
            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CC.txt", System.Text.Encoding.UTF8))
            {
                string line;
                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            new CC
                            {
                                PK = Convert.ToUInt64(ra[0]),
                                Ad = ra[2],
                                Skl = ra[3],

                                IsRnkd = true,
                                TNSM = 8,   // Takim Single Mac Sayisi
                                TNDM = 3,   //       Double
                                TNSS = 5,   // Takim Single kac set uzerinden
                                TNDS = 5,   //       Double
                                TSMK = 2,   // Takim SingleMac Skoru
                                TDMK = 3,   //         DoubleMac 
                                TEGP = 2,   //       Event Galiibiyet Puan
                                TEMP = 1,   //      
                                TEBP = 0,
                                TEXP = -1,
                            };
                        }
                    }
                });
            }
        }

        public static void PopCT()
        {
            if (Db.SQL<CT>("select r from CT r").FirstOrDefault() != null)
                return; // Kayit var yapma

            //0:CC.PK|1:PK|2:Ad|3:Adres|4:Pw|5:K1.PK|6:K2.PK|7:K1.Ad|8:K2.Ad
            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CT.txt", System.Text.Encoding.UTF8))
            {
                string line;
                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            var cc = Db.SQL<CC>("select r from CC r where r.PK = ?", ulong.Parse(ra[0])).FirstOrDefault();
                            ulong K1PK = string.IsNullOrEmpty(ra[5]) ? 0 : ulong.Parse(ra[5]);
                            ulong K2PK = string.IsNullOrEmpty(ra[6]) ? 0 : ulong.Parse(ra[6]);
                            var ppK1 = Db.SQL<PP>("select r from PP r where r.PK = ?", K1PK).FirstOrDefault();
                            var ppK2 = Db.SQL<PP>("select r from PP r where r.PK = ?", K2PK).FirstOrDefault();

                            new CT
                            {
                                CC = cc,
                                K1 = ppK1,
                                K2 = ppK2,
                                PK = Convert.ToUInt64(ra[1]),
                                Ad = ra[2],
                                Adres = ra[3],
                                IsRun = true
                            };
                        }
                    }
                });
            }
        }

        public static void PopCTP()
        {
            if (Db.SQL<CTP>("select r from CTP r").FirstOrDefault() != null)
                return; // Kayit var yapma

            //0:CC.PK|1:CT.PK|2:PP.PK|3:Idx|4:PPAd|5:CTAd
            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CTP.txt", System.Text.Encoding.UTF8))
            {
                string line;
                ulong ccPK, ctPK, ppPK;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            ccPK = ulong.Parse(ra[0]);
                            ctPK = ulong.Parse(ra[1]);
                            ppPK = ulong.Parse(ra[2]);

                            var cc = Db.SQL<CC>("select r from CC r where r.PK = ?", ccPK).FirstOrDefault();
                            var ct = Db.SQL<CT>("select r from CT r where r.PK = ?", ctPK).FirstOrDefault();
                            var pp = Db.SQL<PP>("select r from PP r where r.PK = ?", ppPK).FirstOrDefault();


                            new CTP
                            {
                                CC = cc,
                                CT = ct,
                                PP = pp,
                                Idx = int.Parse(ra[3]),
                                IsRun = true,
                            };
                        }
                    }
                });
            }
        }

        public static void PopCET()
        {
            if (Db.SQL<CET>("select r from CET r").FirstOrDefault() != null)
                return; // Kayit var yapma

            //0:CC.PK|1:PK|2:hCT.PK|3:gCT.PK|4:Trh:dd.MM.yyyy HH:mm|5:hPok|6:gPok|7:Rok|8:hP|9:gP|10:hPW|11:hMSW|12:hMDW|13:gPW|14:gMSW|15:gMDW
            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CET.txt", System.Text.Encoding.UTF8))
            {
                string line;
                ulong ccPK, rPK, hctPK, gctPK;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            ccPK = ulong.Parse(ra[0]);
                            rPK = ulong.Parse(ra[1]);
                            hctPK = ulong.Parse(ra[2]);
                            gctPK = ulong.Parse(ra[3]);

                            var cc = Db.SQL<CC>("select r from CC r where r.PK = ?", ccPK).FirstOrDefault();
                            var hct = Db.SQL<CT>("select r from CT r where r.PK = ?", hctPK).FirstOrDefault();
                            var gct = Db.SQL<CT>("select r from CT r where r.PK = ?", gctPK).FirstOrDefault();

                            new CET
                            {
                                CC = cc,
                                PK = rPK,
                                Trh = DateTime.Parse(ra[4]),
                                HCT = hct,
                                GCT = gct,

                                Drm = "OK"
                            };
                        }
                    }
                });
            }
        }

        public static void PopMAC()
        {
            if (Db.SQL<MAC>("select r from MAC r").FirstOrDefault() != null)
                return; // Kayit var yapma

            //0:CC.PK|1:CET.PK|2:hPP1.PK|3:hPP2.PK|4:gPP1.PK|5:gPP2.PK|6:Trh|7:SoD|8:Idx|9:hS1W|hS2W|hS3W|hS4W|hS5W|14:gS1W|gS2W|gS3W|gS4W|gS5W
            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-MAC.txt", System.Text.Encoding.UTF8))
            {
                string line;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            var cc = Db.SQL<CC>("select r from CC r where r.PK = ?",    ulong.Parse(ra[0])).FirstOrDefault();
                            var cet = Db.SQL<CET>("select r from CET r where r.PK = ?", ulong.Parse(ra[1])).FirstOrDefault();
                            var hPP1 = Db.SQL<PP>("select r from PP r where r.PK = ?",  ulong.Parse(ra[2])).FirstOrDefault();
                            ulong hPP2PK = string.IsNullOrEmpty(ra[3]) ? 0 : ulong.Parse(ra[3]);
                            var hPP2 = Db.SQL<PP>("select r from PP r where r.PK = ?", hPP2PK).FirstOrDefault();
                            var gPP1 = Db.SQL<PP>("select r from PP r where r.PK = ?",  ulong.Parse(ra[4])).FirstOrDefault();
                            ulong gPP2PK = string.IsNullOrEmpty(ra[5]) ? 0 : ulong.Parse(ra[5]);
                            var gPP2 = Db.SQL<PP>("select r from PP r where r.PK = ?", gPP2PK).FirstOrDefault();

                            var m = new MAC
                            {
                                CC = cc,
                                CEB = cet,
                                HPP1 = hPP1, 
                                HPP2 = hPP2, 
                                GPP1 = gPP1, 
                                GPP2 = gPP2,

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
        */
        public static void PPbaz2ilk()
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
        public static void PPmove2baz()
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
    }

}
