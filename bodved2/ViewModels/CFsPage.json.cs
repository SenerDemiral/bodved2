using BDB2;
using Starcounter;

namespace bodved2.ViewModels
{
    partial class CFsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            CC cc = Db.FromId<CC>((ulong)CCoNo);
            Hdr = $"{cc.Ad} ► Ferdi Sonuçları";

            int i = 1;
            var cfs = Db.SQL<CF>("SELECT r FROM CF r WHERE r.CC = ? order by r.PP.Ad", cc);
            foreach(var cf in cfs)
            {
                CFs.Add(new CFsElementJson
                {
                    CFoNo = (long)cf.CFoNo,
                    Idx = $"{i++}",   // Simdilik kullanma
                    PPoNo = (long)cf.PPoNo,
                    PPAd = cf.PPAd,

                    PW = $"{cf.PW:#}",
                    PL = $"{cf.PL:#}",
                    MT = $"{cf.MT:#}",
                    MW = $"{cf.MW:#}",
                    ML = $"{cf.ML:#}",
                    ST = $"{cf.ST:#}",
                    SW = $"{cf.SW:#}",
                    SL = $"{cf.SL:#}",
                    SF = $"{cf.SF:#}",
                });
            }

        }
    }
}
