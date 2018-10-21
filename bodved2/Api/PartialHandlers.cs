using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;
using BDB2;
using bodved2.ViewModels;

namespace bodved2.Api
{
    class PartialHandlers : IHandler
    {
        public void Register()
        {
            Handle.GET("/bodved/partials/PPs", () =>
            {
                var page = new PPsPage();
                page.PPs.Data = Db.SQL<PP>("SELECT r FROM PP r order by r.RnkIdx");
                return page;
            });

            Handle.GET("/bodved/partials/CCs", () =>
            {
                return new CCsPage();
            });

            Handle.GET("/bodved/partials/CTs/{?}", (ulong cc) =>
            {
                var page = new CTsPage();
                CC CC = Db.FromId<CC>(cc);
                page.CCAd = $"{CC.Ad} Takım Puanları"; 
                page.CTs.Data = Db.SQL<CT>("SELECT r FROM CT r WHERE r.CC = ? order by r.Idx", CC);
                return page;

            });

            Handle.GET("/bodved/partials/CTPs/{?}", (ulong ct) =>
            {
                var page = new CTPsPage();
                CT CT = Db.FromId<CT>(ct);
                page.CTAd = $"{CT.Ad} Takım Oyuncuları";
                page.CTPs.Data = Db.SQL<CTP>("SELECT r FROM CTP r WHERE r.CT.ObjectNo = ? order by r.Idx", ct);
                return page;
            });

            Handle.GET("/bodved/partials/CETs/{?}", (ulong cc) =>
            {
                var page = new CETsPage();
                CC CC = Db.FromId<CC>(cc);
                page.CCAd = $"{CC.Ad} Fikstür";
                page.CETs.Data = Db.SQL<CET>("SELECT r FROM CET r WHERE r.CC = ? order by r.Trh", CC);
                return page;

            });

            Handle.GET("/bodved/partials/CET2MACs/{?}", (ulong cet) =>
            {
                var page = new CET2MACsPage();
                CET CET = Db.FromId<CET>(cet);
                page.HCTAd = $"{CET.HCT.Ad}";
                page.GCTAd = $"{CET.GCT.Ad}";
                page.Trh = $"{CET.Trh}";
                page.Sngls.Data = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.CEB = ? and r.SoD = ? order by r.Idx", CET, "S");
                return page;
            });
        }
    }
}
