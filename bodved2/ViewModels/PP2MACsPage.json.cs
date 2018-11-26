using Starcounter;
using BDB2;
using System.Linq;

namespace bodved2.ViewModels
{
    partial class PP2MACsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            PP pp = Db.FromId<PP>((ulong)PPoNo);

            //PP.RnkBaz = pp.RnkBaz;

            var pprds = Db.SQL<PPRD>("select r from PPRD r where r.PP = ? order by r.Dnm DESC", pp);
            foreach(var pprd in pprds)  // Son bulunan deger en son 
            {
                PPRDsElementJson rd = new PPRDsElementJson
                {
                    Dnm = pprd.Dnm,
                    RnkBas = pprd.RnkBas,
                    TopPX = pprd.TopPX,
                    RnkSon = pprd.RnkSon,
                    RnkIdx = pprd.RnkIdx,
                    SMW = pprd.MW,
                    SML = pprd.ML,
                    SSW = pprd.SW,
                    SSL = pprd.SL,
                };
                rd.SMT = rd.SMW + rd.SML;
                rd.SST = rd.SSW + rd.SSL; 

                PPRDs.Add(rd);
/*

                PP.Dnm = pprd.Dnm;
                PP.RnkSon = pprd.RnkSon;
                PP.RnkIdx = pprd.RnkIdx;

                PP.SMW += pprd.MW;
                PP.SML += pprd.ML;

                PP.SSW += pprd.SW;
                PP.SSL += pprd.SL;*/
            }
            /*
            PP.SMT = PP.SMW + PP.SML;
            PP.SST = PP.SSW + PP.SSL;*/

            Hdr = $"{pp.Ad}  ► Maçları";

            //PP.Data = pp;
            //Sngls.Data = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.SoD = ? and (r.HPP1 = ? or r.GPP1 = ?) order by r.Trh DESC", "S", PPt, PPt);
            Dbls.Data = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.SoD = ? and (r.HPP1 = ? or r.GPP1 = ? or r.HPP2 = ? or r.GPP2 = ?) order by r.Trh DESC", "D", pp, pp, pp, pp);


            var macs = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.SoD = ? and (r.HPP1 = ? or r.GPP1 = ?) order by r.Trh DESC", "S", pp, pp);
            foreach(var mac in macs)
            {
                //var sng2 = Sngls.Add();

                SnglElement sng = new SnglElement()
                {
                    CEBoNo = (long)mac.CEB.GetObjectNo(),
                    MACoNo = (long)mac.GetObjectNo(),
                    Tarih = mac.Tarih,
                    SoD = mac.SoD,
                    Idx = mac.Idx,
                    Info = $"{mac.Info} {mac.CC.Ad}",
                };

                if (PPoNo == (long)mac.HPP1oNo)  // Home Kendisi, Guest Rakip
                {
                    sng.RkpPPoNo  = (long)mac.GPP1oNo;
                    sng.RkpPPAd   = mac.GPP1Ad;
                    sng.RkpCTAd   = mac.GCTAd;
                    sng.RkpCToNo  = (long)mac.GCToNo;
                    sng.RkpCTAd   = mac.GCTAd;

                    sng.SncMac = mac.SncMacRvrs;
                    sng.SncSet = mac.SncSetRvrs;
                    sng.SncSet = $"<{mac.SncMacRvrs}> {mac.SncSetRvrs}";
                    sng.CToNo = (long)mac.HCToNo;
                    sng.CTAd = mac.HCTAd;
                    sng.WL = mac.HWL;

                    //[[item.RkpRnk]]/[[item.RkpRnkPX]] ♦ [[item.Rnk]]/[[item.RnkPX]]
                    //sng.RnkTxt = $"{mac.GRnk}{-5:+#;-#;*} ♦ {mac.HRnk}{5:+#;#;*}";
                    sng.RnkTxt = $"{mac.GRnk}{mac.GRnkPX:+#;-#;⠀} ♦ {mac.HRnk}{mac.HRnkPX:+#;-#;⠀}";
                }
                else
                {
                    sng.RkpPPoNo  = (long)mac.HPP1oNo;
                    sng.RkpPPAd   = mac.HPP1Ad;
                    sng.RkpCToNo  = (long)mac.HCToNo;
                    sng.RkpCTAd   = mac.HCTAd;

                    sng.SncMac = mac.SncMac;
                    sng.SncSet = $"<{mac.SncMac}> {mac.SncSet}";
                    sng.CToNo = (long)mac.GCToNo;
                    sng.CTAd = mac.GCTAd;
                    sng.WL = mac.GWL;

                    sng.RnkTxt = $"{mac.HRnk}{mac.HRnkPX:+#;-#;⠀} ♦ {mac.GRnk}{mac.GRnkPX:+#;-#;⠀}";
                }

                sng.Dnm = mac.CC.Dnm;

                Sngls.Add(sng);
            }

        }
        
        [PP2MACsPage_json.Sngls]
        public partial class SnglElement : Json
        {
            protected override void OnData()
            {
                base.OnData();
            }
        }
    }
}
