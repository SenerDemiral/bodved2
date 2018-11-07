using Starcounter;
using BDB2;

namespace bodved2.ViewModels
{
    partial class PP2MACsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            PP PPt = Db.FromId<PP>((ulong)PPoNo);
            Hdr = PPt.Ad;

            PP.Data = PPt;
            //Sngls.Data = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.SoD = ? and (r.HPP1 = ? or r.GPP1 = ?) order by r.Trh DESC", "S", PPt, PPt);
            Dbls.Data = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.SoD = ? and (r.HPP1 = ? or r.GPP1 = ? or r.HPP2 = ? or r.GPP2 = ?) order by r.Trh DESC", "D", PPt, PPt, PPt, PPt);


            var macs = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.SoD = ? and (r.HPP1 = ? or r.GPP1 = ?) order by r.Trh DESC", "S", PPt, PPt);
            foreach(var mac in macs)
            {
                //var sng = Sngls.Add();

                SnglElement sng = new SnglElement()
                {
                    CEBoNo = (long)mac.CEB.GetObjectNo(),
                    MACoNo = (long)mac.GetObjectNo(),
                    Tarih = mac.Tarih,
                    SoD = mac.SoD,
                    Idx = mac.Idx,
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
