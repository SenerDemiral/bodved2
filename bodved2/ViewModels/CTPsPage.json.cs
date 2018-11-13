using BDB2;
using Starcounter;

namespace bodved2.ViewModels
{
    partial class CTPsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            CT ct = Db.FromId<CT>((ulong)CToNo);
            Hdr = $"{ct.CC.Ad} ► {ct.Ad} ► Takım Oyuncuları";

            //int idx = 1;
            //var ctps = Db.SQL<CTP>("SELECT r FROM CTP r WHERE r.CT = ? order by r.RnkBas DESC, r.IsRun DESC, r.PP.Ad", ct);
            var ctps = Db.SQL<CTP>("SELECT r FROM CTP r WHERE r.CT = ? order by r.Idx", ct);
            foreach (var ctp in ctps)
            {
                CTPs.Add(new CTPsElementJson
                {
                    //Idx = idx++,
                    Idx = ctp.Idx,

                    PPoNo = (long)ctp.PP.GetObjectNo(),
                    PPAd = ctp.PP.Ad,
                    IsRun = ctp.IsRun,
                    RnkBas = ctp.RnkBas,
                    RnkBit = $"{ctp.RnkBit:#}",

                    SMT = $"{ctp.SMT:#}",
                    SMW = $"{ctp.SMW:#}",
                    SML = $"{ctp.SML:#}",
                    DMT = $"{ctp.DMT:#}",
                    DMW = $"{ctp.DMW:#}",
                    DML = $"{ctp.DML:#}",
                    SSW = $"{ctp.SSW:#}",
                    SSL = $"{ctp.SSL:#}",
                    DSW = $"{ctp.DSW:#}",
                    DSL = $"{ctp.DSL:#}",
                });
            }

        }
    }
}