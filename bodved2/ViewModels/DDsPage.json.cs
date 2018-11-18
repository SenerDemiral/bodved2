using BDB2;
using Starcounter;
using System.Linq;

namespace bodved2.ViewModels
{
    partial class DDsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            Hdr = "BODVED Aktiviteleri";

            var dds = Db.SQL<DD>("SELECT r FROM DD r order by r.Dnm DESC");
            foreach (var dd in dds)
            {
                DDs.Add(new DDsElementJson
                {
                    DDoNo = (long)dd.GetObjectNo(),
                    Dnm = dd.Dnm,
                    Ad = dd.Ad,
                    Info = dd.Info,

                    KOC = $"{dd.KOC:n0}",
                    OOC = $"{dd.OOC:n0}",
                    Mac = $"{dd.SMC + dd.DMC:n0}",
                    Set = $"{dd.SSC + dd.DSC:n0}",
                    Sayi = $"{dd.SNC + dd.DNC:n0}"
                });
            }

        }
    }
}
