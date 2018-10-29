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
        public static void RestoreALL()
        {
            Dictionary<ulong, ulong> dON = new Dictionary<ulong, ulong>();    // dON[oldNO] = newNO

            // Sirayla yapilmali: PP, CC, CT, CTP, CET, CF, CEF, MAC
            // Her PP icin
            ulong oldNO = 0;

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
                                RnkBaz = Convert.ToInt32(ra[1]),
                                Sex = ra[2],
                                Ad = ra[3],
                                Tel = ra[4],
                                IsRun = ra[5] == "T" ? true : false,

                                Rnk1 = Convert.ToInt32(ra[7]),
                                Rnk2 = Convert.ToInt32(ra[8]),
                                Rnk3 = Convert.ToInt32(ra[9]),

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
                                Ad = ra[2],
                                IsRun = ra[3] == "T" ? true : false,
                                IsRnkd = ra[4] == "T" ? true : false,

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
                                IsRun = ra[6] == "T" ? true : false,
                            };
                            dON[oldNO] = ctp.GetObjectNo();
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

        public static void BackupPP()    // Oyuncular
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-PP2.txt", false))
            {
                sw.WriteLine("# oNo/RestoreAsPK|RnkBaz|Sex|Ad|Tel|IsRun");
                var recs = Db.SQL<PP>("select r from PP r order by r.Ad");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.RnkBaz}|{r.Sex}|{r.Ad}|{r.Tel}|{(r.IsRun ? "T" : "F")}");
                }
            }
        }

        public static void BackupCC()    // Competition
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-CC2.txt", false))
            {
                sw.WriteLine("# oNo/RestoreAsPK|Idx|Ad|IsRun|IsRnkd");
                var recs = Db.SQL<CC>("select r from CC r order by r.Idx");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.Idx}|{r.Ad}|{(r.IsRun ? "T" : "F")}|{(r.IsRnkd ? "T" : "F")}");
                }
            }
        }
        public static void BackupCT()    // Teams
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-CT2.txt", false))
            {
                sw.WriteLine("# oNo/RestoreAsPK|CC|K1PP|K2PP|Idx|Ad|IsRun");
                var recs = Db.SQL<CT>("select r from CT r order by r.Idx");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.CC.GetObjectNo()}|{r.K1?.GetObjectNo() ?? 0}|{r.K2?.GetObjectNo() ?? 0}|{r.Idx}|{r.Ad}|{(r.IsRun ? "T" : "F")}");
                }
            }
        }
        public static void BackupCTP()    // TeamPlayer
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-CTP2.txt", false))
            {
                sw.WriteLine("# oNo/RestoreAsPK|CC|CT|PP|Idx|IsRun");
                var recs = Db.SQL<CTP>("select r from CTP r");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.CC.GetObjectNo()}|{r.CT.GetObjectNo()}|{r.PP.GetObjectNo()}|{r.Idx}|{(r.IsRun ? "T" : "F")}");
                }
            }
        }

        public static void BackupCET()    // TeamEvent
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-CET2.txt", false))
            {
                sw.WriteLine("# oNo/RestoreAsPK|CC|HCT|GCT|Tarih|Drm");
                var recs = Db.SQL<CET>("select r from CET r");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.CC.GetObjectNo()}|{r.HCT.GetObjectNo()}|{r.GCT.GetObjectNo()}|{r.Trh:dd.MM.yyyy}|{r.Drm}");
                }
            }
        }

        public static void BackupMAC()    // Mac
        {
            using (StreamWriter sw = new StreamWriter($@"C:\Starcounter\BodVed2Data\BDB-MAC2.txt", false))
            {
                sw.WriteLine("# oNo/RestoreAsPK|CEB|Tarih|Drm|hPP1|hPP2|gPP1|gPP2|SoD|Idx|H1..7W|G1..7W");
                var recs = Db.SQL<MAC>("select r from MAC r order by r.Trh");
                foreach (var r in recs)
                {
                    sw.WriteLine($"{r.GetObjectNo()}|{r.CEB.GetObjectNo()}|{r.HPP1?.GetObjectNo() ?? 0}|{r.HPP2?.GetObjectNo() ?? 0}|{r.GPP1?.GetObjectNo() ?? 0}|{r.GPP2?.GetObjectNo() ?? 0}|{r.Trh:dd.MM.yyyy}|{r.Drm}|{r.SoD}|{ r.Idx}|{r.H1W}|{r.H2W}|{r.H3W}|{r.H4W}|{r.H5W}|{r.H6W}|{r.H7W}|{r.G1W}|{r.G2W}|{r.G3W}|{r.G4W}|{r.G5W}|{r.G6W}|{r.G7W}");
                }
            }
        }
    }
}
