using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;

namespace BDB2
{
    public static class H
    {
        public static void PopPP()
        {
            if (Db.SQL<PP>("select r from PP r").FirstOrDefault() != null)
                return; // Kayit var yapma

            // 0:PK,1:RnkBaz,2:Sex,3:DgmTrh,4:Ad,5:eMail,6:Tel
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
                                RnkBaz = Convert.ToInt32(ra[1]),
                                Sex = ra[2],
                                Ad = ra[4],
                                Tel = ra[6]
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

                                isRnkd = true,
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
                                Idx = int.Parse(ra[3])
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
                                hCT = hct,
                                gCT = gct,

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
                                hPP1 = hPP1, 
                                hPP2 = hPP2, 
                                gPP1 = gPP1, 
                                gPP2 = gPP2,

                                Trh = DateTime.Parse(ra[6]),
                                SoD = ra[7],
                                Idx = int.Parse(ra[8]),

                                h1W = int.Parse(ra[9]),
                                h2W = int.Parse(ra[10]),
                                h3W = int.Parse(ra[11]),
                                h4W = int.Parse(ra[12]),
                                h5W = int.Parse(ra[13]),

                                g1W = int.Parse(ra[14]),
                                g2W = int.Parse(ra[15]),
                                g3W = int.Parse(ra[16]),
                                g4W = int.Parse(ra[17]),
                                g5W = int.Parse(ra[18]),

                                Drm = "OK"
                            };

                            MAC.RefreshSonuc(m);
                        }
                    }
                });
            }

        }
    }

}
