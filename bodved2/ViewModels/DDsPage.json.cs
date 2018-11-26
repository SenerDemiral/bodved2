using BDB2;
using Starcounter;
using System.Collections.Generic;
using System.Linq;

namespace bodved2.ViewModels
{
    partial class DDsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            Hdr = "BODVED Aktiviteleri";
            int topMac = 0;
            int topSet = 0;
            int topSayi = 0;

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

                topMac += dd.SMC + dd.DMC;
                topSet += dd.SSC + dd.DSC;
                topSayi += dd.SNC + dd.DNC;
            }

            HashSet<ulong> ppHS = new HashSet<ulong>(); // Toplam Oynamıs Uniqe Oyuncu
            var macs = Db.SQL<MAC>("select r from MAC r");
            foreach (var mac in macs)
            {
                ppHS.Add(mac.HPP1oNo);     // Oynuyor
                ppHS.Add(mac.GPP1oNo);     // Oynuyor
                if (mac.SoD == "D")
                {
                    ppHS.Add(mac.HPP2oNo);     // Oynuyor
                    ppHS.Add(mac.GPP2oNo);     // Oynuyor
                }
            }

            long topPP = Db.SQL<long>("select count(r) from PP r").FirstOrDefault();

            TopPP = $"{topPP:n0}";
            RunPP = $"{ppHS.Count():n0}";
            TopMac = $"{topMac:n0}";
            TopSet = $"{topSet:n0}";
            TopSayi = $"{topSayi:n0}";


        }
    }
}
