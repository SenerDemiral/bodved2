using Starcounter;
using BDB2;
using System.Collections.Generic;

namespace bodved2.ViewModels
{
    partial class PPsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            ulong ppNO = 0;
            HashSet<ulong> ppHS = new HashSet<ulong>(); // DnmRun da oynayanlari ayirmak icin.

            // Aktif olan Oyunculari PPRD den al.
            var pprds = Db.SQL<PPRD>("select r from PPRD r where Dnm = ? order by r.RnkIdx", H.DnmRun);
            foreach(var pprd in pprds)
            {
                ppNO = pprd.PP.GetObjectNo();

                ppHS.Add(ppNO);     // Oynuyor

                new PPrunsElementJson
                {
                    PPoNo = (long)ppNO,
                    Ad = pprd.PP.Ad,
                    RnkBaz = pprd.PP.RnkBaz,
                    RnkSon = pprd.RnkSon,
                    RnkIdx = pprd.RnkIdx
                };
            }

            // Aktif Olmayanlari PP den
            var pps = Db.SQL<PP>("select r from PP r order by Ad");
            foreach(var pp in pps)
            {
                if (!ppHS.Contains(pp.GetObjectNo()))
                {
                    new PPoldsElementJson
                    {
                        PPoNo = (long)pp.GetObjectNo(),
                        Ad = pp.Ad,
                        RnkBaz = pp.RnkBaz,
                    };
                }
            }
        }
    }
}