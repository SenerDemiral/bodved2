using Starcounter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDB2
{
    public class HBR
    {
        public static void RestoreDB()
        {
            if (Db.SQL<PP>("select r from PP r").FirstOrDefault() != null)
                return; // Kayit var yapma

            Dictionary<ulong, ulong> dON = new Dictionary<ulong, ulong>();    // dON[oldNO] = newNO

            ulong oldNO = 0;

            // Sirayla yapilmali: PP, CC, CT, CTP, CET, CF, CEF, MAC
            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-PP2.txt", System.Text.Encoding.UTF8))
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
                                RnkBaz = Convert.ToInt32(ra[2]),
                                RnkSon = Convert.ToInt32(ra[3]),
                                Sex = ra[4],
                                Ad = ra[5],
                                Tel = ra[6],
                                IsRun = ra[7] == "T" ? true : false,

                                Rnk1 = Convert.ToInt32(ra[8]),
                                Rnk2 = Convert.ToInt32(ra[9]),
                                Rnk3 = Convert.ToInt32(ra[10]),

                            };
                            dON[oldNO] = pp.GetObjectNo();
                        }
                    }
                });
            }

            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CC2.txt", System.Text.Encoding.UTF8))
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
                                Idx = int.Parse(ra[1]),
                                Skl = ra[2],
                                Ad = ra[3],
                                IsRun = ra[4] == "T" ? true : false,
                                IsRnkd = ra[5] == "T" ? true : false,

                            };
                            dON[oldNO] = cc.GetObjectNo();
                        }
                    }
                });
            }

            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CT2.txt", System.Text.Encoding.UTF8))
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
                                Idx = int.Parse(ra[4]),
                                Ad = ra[5],
                                IsRun = ra[6] == "T" ? true : false,
                                Adres = ra[7],
                            };
                            dON[oldNO] = ct.GetObjectNo();
                        }
                    }
                });
            }

            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CTP2.txt", System.Text.Encoding.UTF8))
            {
                string line;
                ulong oldCC, oldCT, oldPP;
                CTP ctp = null;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            oldNO = ulong.Parse(ra[0]);
                            oldCC = ulong.Parse(ra[1]);
                            oldCT = ulong.Parse(ra[2]);
                            oldPP = ulong.Parse(ra[3]);

                            ctp = new CTP
                            {
                                CC = Db.FromId<CC>(dON[oldCC]),
                                CT = Db.FromId<CT>(dON[oldCT]),
                                PP = oldPP == 0 ? null : Db.FromId<PP>(dON[oldPP]),
                                Idx = int.Parse(ra[4]),
                                IsRun = ra[5] == "T" ? true : false,
                            };
                            dON[oldNO] = ctp.GetObjectNo();
                        }
                    }
                });
            }

            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CF2.txt", System.Text.Encoding.UTF8))
            {
                string line;
                ulong oldCC, oldPP;
                CF cf = null;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            oldNO = ulong.Parse(ra[0]);
                            oldCC = ulong.Parse(ra[1]);
                            oldPP = ulong.Parse(ra[2]);

                            cf = new CF
                            {
                                CC = Db.FromId<CC>(dON[oldCC]),
                                PP = oldPP == 0 ? null : Db.FromId<PP>(dON[oldPP]),
                                Idx = int.Parse(ra[3]),
                                IsRun = ra[4] == "T" ? true : false,
                            };
                            dON[oldNO] = cf.GetObjectNo();
                        }
                    }
                });
            }

            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CET2.txt", System.Text.Encoding.UTF8))
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
                                Drm = ra[5]
                            };
                            dON[oldNO] = cet.GetObjectNo();
                        }
                    }
                });
            }

            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-CEF2.txt", System.Text.Encoding.UTF8))
            {
                string line;
                ulong oldCC, oldHPP, oldGPP;
                CEF cef = null;

                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            oldNO = ulong.Parse(ra[0]);
                            oldCC = ulong.Parse(ra[1]);
                            oldHPP = ulong.Parse(ra[2]);
                            oldGPP = ulong.Parse(ra[3]);

                            cef = new CEF
                            {
                                CC = Db.FromId<CC>(dON[oldCC]),
                                HPP = Db.FromId<PP>(dON[oldHPP]),
                                GPP = Db.FromId<PP>(dON[oldGPP]),
                                Trh = DateTime.Parse(ra[4]),
                                Drm = ra[5]
                            };
                            dON[oldNO] = cef.GetObjectNo();
                        }
                    }
                });
            }

            using (StreamReader sr = new StreamReader($@"C:\Starcounter\Bodved2Data\BDB-MAC2.txt", System.Text.Encoding.UTF8))
            {
                string line;
                ulong oldCC, oldCEB, oldHPP1, oldHPP2, oldGPP1, oldGPP2;
                MAC mac = null;
                CEB ceb = null;
                Db.Transact(() =>
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                        {
                            string[] ra = line.Split('|');

                            oldNO = ulong.Parse(ra[0]);
                            oldCC = ulong.Parse(ra[1]);
                            oldCEB = ulong.Parse(ra[2]);
                            oldHPP1 = ulong.Parse(ra[3]);
                            oldHPP2 = ulong.Parse(ra[4]);
                            oldGPP1 = ulong.Parse(ra[5]);
                            oldGPP2 = ulong.Parse(ra[6]);

                            if (Db.FromId(dON[oldCEB]) is CET)
                                ceb = Db.FromId<CET>(dON[oldCEB]);
                            else
                                ceb = Db.FromId<CEF>(dON[oldCEB]);

                            mac = new MAC
                            {
                                CC = Db.FromId<CC>(dON[oldCC]),
                                CEB = ceb,  // Db.FromId<CEB>(dON[oldCEB]) ????????????????????????????
                                HPP1 = oldHPP1 == 0 ? null : Db.FromId<PP>(dON[oldHPP1]),
                                HPP2 = oldHPP2 == 0 ? null : Db.FromId<PP>(dON[oldHPP2]),
                                GPP1 = oldGPP1 == 0 ? null : Db.FromId<PP>(dON[oldGPP1]),
                                GPP2 = oldGPP2 == 0 ? null : Db.FromId<PP>(dON[oldGPP2]),

                                Trh = DateTime.Parse(ra[7]),
                                Drm = ra[8],
                                SoD = ra[9],
                                Idx = int.Parse(ra[10]),

                                H1W = int.Parse(ra[11]),
                                H2W = int.Parse(ra[12]),
                                H3W = int.Parse(ra[13]),
                                H4W = int.Parse(ra[14]),
                                H5W = int.Parse(ra[15]),
                                H6W = int.Parse(ra[16]),
                                H7W = int.Parse(ra[17]),

                                G1W = int.Parse(ra[18]),
                                G2W = int.Parse(ra[19]),
                                G3W = int.Parse(ra[20]),
                                G4W = int.Parse(ra[21]),
                                G5W = int.Parse(ra[22]),
                                G6W = int.Parse(ra[23]),
                                G7W = int.Parse(ra[24]),
                            };
                            
                            //dON[oldNO] = mac.GetObjectNo();   // Gerek YOK
                        }
                    }
                });
            }
        }

        public static void BackupDB()
        {
            BackupPP();
            BackupCC();
            BackupCT();
            BackupCTP();
            BackupCF();
            BackupCET();
            BackupCEF();
            BackupMAC();
        }

        public static void BackupPP()    // Oyuncular
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-PP2.txt", false))
            {
                sw.WriteLine("# oNo|RnkIlk|RnkBaz|RnkSon|Sex|Ad|Tel|IsRun|Rnk1|Rnk2|Rnk3");
                var recs = Db.SQL<PP>("select r from PP r order by r.Ad");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.RnkIlk}|{r.RnkBaz}|{r.RnkSon}|{r.Sex}|{r.Ad}|{r.Tel}|{(r.IsRun ? "T" : "F")}|{r.Rnk1}|{r.Rnk2}|{r.Rnk3}");
                }
            }
        }
        public static void BackupCC()    // Competition
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-CC2.txt", false))
            {
                sw.WriteLine("# oNo|Idx|Skl|Ad|IsRun|IsRnkd");
                var recs = Db.SQL<CC>("select r from CC r order by r.Idx");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.Idx}|{r.Skl}|{r.Ad}|{(r.IsRun ? "T" : "F")}|{(r.IsRnkd ? "T" : "F")}");
                }
            }
        }
        public static void BackupCT()    // Teams
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-CT2.txt", false))
            {
                sw.WriteLine("# oNo|CC|K1PP|K2PP|Idx|Ad|IsRun|Adres");
                var recs = Db.SQL<CT>("select r from CT r order by r.Idx");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.CC.GetObjectNo()}|{r.K1?.GetObjectNo() ?? 0}|{r.K2?.GetObjectNo() ?? 0}|{r.Idx}|{r.Ad}|{(r.IsRun ? "T" : "F")}|{r.Adres}");
                }
            }
        }
        public static void BackupCTP()    // TeamPlayer
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-CTP2.txt", false))
            {
                sw.WriteLine("# oNo|CC|CT|PP|Idx|IsRun");
                var recs = Db.SQL<CTP>("select r from CTP r");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.CC.GetObjectNo()}|{r.CT.GetObjectNo()}|{r.PP.GetObjectNo()}|{r.Idx}|{(r.IsRun ? "T" : "F")}");
                }
            }
        }
        public static void BackupCF()    // FerdiPlayer
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-CF2.txt", false))
            {
                sw.WriteLine("# oNo|CC|PP|Idx|IsRun");
                var recs = Db.SQL<CF>("select r from CF r");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.CC.GetObjectNo()}|{r.PP.GetObjectNo()}|{r.Idx}|{(r.IsRun ? "T" : "F")}");
                }
            }
        }
        public static void BackupCET()    // TeamEvent
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-CET2.txt", false))
            {
                sw.WriteLine("# oNo|CC|HCT|GCT|Tarih|Drm");
                var recs = Db.SQL<CET>("select r from CET r order by r.Trh");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.CC.GetObjectNo()}|{r.HCT.GetObjectNo()}|{r.GCT.GetObjectNo()}|{r.Trh:dd.MM.yyyy}|{r.Drm}");
                }
            }
        }
        public static void BackupCEF()    // FerdiEvent
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-CEF2.txt", false))
            {
                sw.WriteLine("# oNo|CC|HPP|GPP|Tarih|Drm");
                var recs = Db.SQL<CEF>("select r from CEF r order by r.Trh");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.CC.GetObjectNo()}|{r.HPP.GetObjectNo()}|{r.GPP.GetObjectNo()}|{r.Trh:dd.MM.yyyy}|{r.Drm}");
                }
            }
        }
        public static void BackupMAC()    // Mac
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-MAC2.txt", false))
            {
                sw.WriteLine("# oNo|CC|CEB|hPP1|hPP2|gPP1|gPP2|Tarih|Drm|SoD|Idx|H1..7W|G1..7W");
                var recs = Db.SQL<MAC>("select r from MAC r order by r.Trh");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.CC.GetObjectNo()}|{r.CEB.GetObjectNo()}|{r.HPP1?.GetObjectNo() ?? 0}|{r.HPP2?.GetObjectNo() ?? 0}|{r.GPP1?.GetObjectNo() ?? 0}|{r.GPP2?.GetObjectNo() ?? 0}|{r.Trh:dd.MM.yyyy}|{r.Drm}|{r.SoD}|{ r.Idx}|{r.H1W}|{r.H2W}|{r.H3W}|{r.H4W}|{r.H5W}|{r.H6W}|{r.H7W}|{r.G1W}|{r.G2W}|{r.G3W}|{r.G4W}|{r.G5W}|{r.G6W}|{r.G7W}");
                }
            }
        }
    }
}
