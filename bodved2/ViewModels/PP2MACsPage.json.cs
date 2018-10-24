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
                    sng.RkpRnk    = mac.GRnk;
                    sng.RkpRnkPX  = mac.GRnkPX;
                    sng.RkpCToNo  = (long)mac.GCToNo;
                    sng.RkpCTAd   = mac.GCTAd;

                    sng.SncMac = mac.SncMacRvrs;
                    sng.SncSet = mac.SncSetRvrs;
                    sng.SncSet = $"<{mac.SncMacRvrs}> {mac.SncSetRvrs}";
                    sng.Rnk = mac.HRnk;
                    sng.RnkPX = mac.HRnkPX;
                    sng.CToNo = (long)mac.HCToNo;
                    sng.CTAd = mac.HCTAd;
                    sng.WL = mac.HWL;
                }
                else
                {
                    sng.RkpPPoNo  = (long)mac.HPP1oNo;
                    sng.RkpPPAd   = mac.HPP1Ad;
                    sng.RkpRnk    = mac.HRnk;
                    sng.RkpRnkPX  = mac.HRnkPX;
                    sng.RkpCToNo  = (long)mac.HCToNo;
                    sng.RkpCTAd   = mac.HCTAd;

                    sng.SncMac = mac.SncMac;
                    sng.SncSet = $"<{mac.SncMac}> {mac.SncSet}";
                    sng.Rnk = mac.GRnk;
                    sng.RnkPX = mac.GRnkPX;
                    sng.CToNo = (long)mac.GCToNo;
                    sng.CTAd = mac.GCTAd;
                    sng.WL = mac.GWL;
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
