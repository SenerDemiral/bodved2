using BDB2;
using Starcounter;

namespace bodved2.ViewModels
{
    partial class CT2CETsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            CT ct = Db.FromId<CT>((ulong)CToNo);
            Hdr = ct.Ad;
            CTAd = ct.Ad;

            var cets = Db.SQL<CET>("select r from CET r where r.HCT = ? or r.GCT = ? order by r.Trh", ct);
            foreach(var cet in cets)
            {
                if (cet.HCT.GetObjectNo() == ct.GetObjectNo())  // Kendisi Home. Rakip Guest
                {
                    new CETsElementJson
                    {
                        CEToNo = (long)cet.GetObjectNo(),
                        Tarih = cet.Tarih,

                        RkpCToNo = (long)cet.GCToNo,
                        RkpCTAd = cet.GCTAd,
                        RkpPW = cet.GPW,
                        RkpKW = cet.GKW,
                        RkpSMW = cet.GSMW,
                        RkpDMW = cet.GDMW,

                        PW = cet.HPW,
                        KW = cet.HKW,
                        SMW = cet.HSMW,
                        DMW = cet.HDMW,
                    };
                }
                else
                {
                    new CETsElementJson
                    {
                        CEToNo = (long)cet.GetObjectNo(),
                        Tarih = cet.Tarih,

                        RkpCToNo = (long)cet.HCToNo,
                        RkpCTAd = cet.HCTAd,
                        RkpPW = cet.HPW,
                        RkpKW = cet.HKW,
                        RkpSMW = cet.HSMW,
                        RkpDMW = cet.HDMW,

                        PW = cet.GPW,
                        KW = cet.GKW,
                        SMW = cet.GSMW,
                        DMW = cet.GDMW,
                    };
                }
            }
        }
    }
}