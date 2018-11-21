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
            //H.Write2Log($"Enter: {Session.Current.SessionId}");
            H.Write2Log("");

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
                    EntCnt = 32000; // Bodved2 baslangicgg
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
        //public ulong PK { get; set; } 
        public string Ad { get; set; }
        public string Info { get; set; }
    }

    [Database]
    public class DD : BB   // Donem
    {
        public int Dnm { get; set; }

        public int SMC { get; set; } // SingleMacCount
        public int DMC { get; set; }
        public int SSC { get; set; }
        public int DSC { get; set; }
        public int SNC { get; set; } // SingleSayiCount
        public int DNC { get; set; }

        public int ONC { get; set; }    // SIL

        public int KOC { get; set; }    // Kayıtlı Oyuncu Sayisi
        public int OOC { get; set; }    // Oynayan Oyuncu Sayisi (Unique)

        public string Ozet => $"Oyuncu {ONC:n0} ♦ Maç {SMC + DMC:n0} ♦ Set {SSC + DSC:n0} ♦ Sayı {SNC + DNC:n0}";
    }


    [Database]
    public class PPRD   // PP RankDonem
    {
        public PP PP { get; set; }
        public int Dnm { get; set; }        // Donem 2017-2018, Baslangic yili, son iki digit yeterli.
        public int RnkIdx { get; set; }
        public int RnkBas { get; set; }     // Bu Donemin baslangic Ranki, Manuel duzeltme yapilarak RnkOnc ile ayni olmayabilir.
        public int TopPX { get; set; }
        public int RnkSon { get; set; }
        public int SonPX { get; set; }      // Son Aldigi PX
        public bool IsFerdi { get; set; }   // Ferdi Turnuvaya katilacak?
        public string CurRuns { get; set; }

        public string DrmPX => SonPX == 0 ? "-" : SonPX > 0 ? "▲" : "▼";    // x: SonPX = 0, yani oynamamis henuz
        public string DrmRnk => RnkSon == RnkBas ? "=" : RnkSon > RnkBas ? "▲" : "▼";

        public string SonPXTxt => $"{SonPX:+#;-#;#}";

        public int MW { get; set; }
        public int ML { get; set; }
        public int SW { get; set; }
        public int SL { get; set; }

        public int RnkPX { get; set; }      // SIL
        public int RnkOnc { get; set; }     // SIL
        public int RnkBaz { get; set; }     // SIL

        public ulong PPoNo => PP.GetObjectNo();
        public string PPAd => PP.Ad;
        public string PPTel => PP.Tel;
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
        public int RnkBit { get; set; }
        public int RnkIdx { get; set; }     // RnkSon'a gore dizildiginde Sirasi
        public bool IsRun { get; set; }     // Aktif
        public bool IsFerdi { get; set; }   // Ferdi oynuyor mu?
        public string CurRuns { get; set; }  // Guncel Aktiviteleri


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

    }

    [Database]
    public class CC : BB  // Competitions
    {
        public ulong CCoNo => this.GetObjectNo();
        public int Dnm { get; set; }        // Donem Turnuvanin baslangic yilinin sok iki digiti. 17, 18, ...
        public int Idx { get; set; }        // (100 - Dnm) * 100 + 10: T1.Lig, + 21 T2A.Lig,... + 51 F1.Lig. 52 F2.Lig, ..
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

        public string K1Ad => K1 == null ? "-" : $"{K1.Ad}";
        public string K2Ad => K2 == null ? "-" : $"{K2.Ad}";
        public string K1Tel => K1 == null ? "-" : $"{K1.Tel}";
        public string K2Tel => K2 == null ? "-" : $"{K2.Tel}";

    }

    [Database]
    public class CTP : BB   // TakimOyunculari
    {
        public CC CC { get; set; }
        public CT CT { get; set; }
        public PP PP { get; set; }
        public int Idx { get; set; }    // Takim icindeki sirasi
        public bool IsRun { get; set; }     // Aktif oynuyor mu?

        //public int RnkBas { get; set; } // Takima girdiginde hesaplanir
        public int RnkBit { get; set; } // Lig bittiginde hesaplanir

        public int RnkBas
        {
            get
            {
                var pprd = Db.SQL<PPRD>("select r from BDB2.PPRD r where r.PP = ? and r.Dnm = ?", PP, CC.Dnm).FirstOrDefault();
                if (pprd != null)
                    return pprd.RnkBas;
                return 0;
            }
        }

        public int SMT => SMW + SML;    // Single Mac Total, Win, Lost, Diskalifiye
        public int SMW { get; set; }
        public int SML { get; set; }
        public int SMX { get; set; }

        public int DMT => DMW + DML;    // Double Mac Total
        public int DMW { get; set; }
        public int DML { get; set; }
        public int DMX { get; set; }    //            HukmenMaglup

        public int SSW { get; set; }
        public int SSL { get; set; }
        public int DSW { get; set; }
        public int DSL { get; set; }

        public ulong CToNo => CT?.GetObjectNo() ?? 0;
        public ulong PPoNo => PP?.GetObjectNo() ?? 0;
        public string CTAd => CT == null ? "-" : $"{CT.Ad}";
        public string PPAd => PP == null ? "-" : $"{PP.Ad}";

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

    }

    [Database]
    public class CEB : BB   // EventBase, Takim/Ferdi
    {
        public CC CC { get; set; }
        public DateTime Trh { get; set; }
        public string Drm { get; set; }      // Iptal, h/gX: Gelmedi, h/gD: Diskalifiye, OK: Oynandi
        public string Yer { get; set; }

        public string Tarih => string.Format(H.cultureTR, "{0:dd.MM.yy ddd}", Trh);  //$"{Trh:dd.MM.yy ddd}";

    }

    [Database]
    public class CET : CEB    // EventTakim
    {
        public ulong CEToNo => this.GetObjectNo();
        public CT HCT { get; set; }     // Home Takim
        public CT GCT { get; set; }     // Guest Takim

        public ulong CCoNo => CC.GetObjectNo();
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

        public string Puan => $"{HPW:#} - {GPW:#}";
        public string HR => $"{HSMW}S+{HDMW}D ►{HKW,2:D2} ►{HPW}";
        public string GR => $"{GSMW}S+{GDMW}D ►{GKW,2:D2} ►{GPW}";

        public string HWL => HPW == GPW ? "?" : HPW > GPW ? "W" : "L";
        public string GWL => HPW == GPW ? "?" : HPW < GPW ? "W" : "L";

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
                //var mac = Db.SQL<MAC>("select r from BDB2.MAC r where r.CEB.ObjectNo = ?", this.GetObjectNo()).FirstOrDefault();
                var mac = Db.SQL<MAC>("select r from BDB2.MAC r where r.CEB = ?", this).FirstOrDefault();
                if (mac == null)
                    return "";

                return $"({mac.SncMac}) {mac.SncSet}";
            }
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

        public string RnkTxt => $"{HRnk}{HRnkPX:+#;-#;⠀} ♦ {GRnk}{GRnkPX:+#;-#;⠀}";


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

    }

    public class ListMac
    {
        public CT CT;
        public PP PP;
        public ulong PPoNo;
        public int Dnm;
        public string SoD;

        public int SW;
        public int SL;
        public int MW;
        public int ML;
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

    public class DictPPRD
    {
        public int RnkSon;
        public int TopPX;
        public int SonPX;

        public int MW;
        public int ML;
        public int SW;
        public int SL;
    }

}