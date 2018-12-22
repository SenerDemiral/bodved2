using BDB2;
using Starcounter;
using System.Linq;

namespace bodved2.ViewModels
{
    partial class PPRDsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();
            string info = "";
            char[] charsToTrim = { '♦', ' ' };

            DD dd = Db.SQL<DD>("select r from DD r where r.Dnm = ?", Dnm).FirstOrDefault();
            Hdr = $"{dd.Ad} ► Oyuncular";

            var pprds = Db.SQL<PPRD>("SELECT r FROM PPRD r where r.Dnm = ? order by r.RnkIdx", Dnm);
            foreach(var pprd in pprds)
            {
                var rde = new PPRDsElementJson
                {
                    PPoNo = (long)pprd.PPoNo,
                    PPAd = pprd.PPAd,
                    RnkBas = pprd.RnkBas,
                    TopPX = pprd.TopPX,
                    TopPXs = $"{pprd.TopPX:+#;-#;#}",
                    TopPXi = pprd.TopPXi,
                    SonPX = pprd.SonPX,
                    SonPXs = $"{pprd.SonPX:+#;-#;#}",
                    SonPXi = pprd.SonPXi,

                    RnkSon = pprd.RnkSon,
                    RnkIdx = pprd.RnkIdx,

                    MWs = $"{pprd.MW:#}",
                    MLs = $"{pprd.ML:#}",
                    SWs = $"{pprd.SW:#}",
                    SLs = $"{pprd.SL:#}",

                    IsFerdi = pprd.IsFerdi,
                    CurRuns = pprd.CurRuns

                };
                /*
                // Oynadigi CTP, CF leri bul
                info = "";
                var ctps = Db.SQL<CTP>("select r from CTP r where r.CC.Dnm = ? and r.PP = ?", H.DnmRun, pprd.PP);
                foreach (var ctp in ctps)
                {
                    info += ctp.CTAd + " ♦ ";
                }
                rde.CurRuns = info.TrimEnd(charsToTrim);
                */
                PPRDs.Add(rde);
            }
        }
    }
}