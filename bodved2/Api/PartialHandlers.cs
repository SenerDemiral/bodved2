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
                page.ccAd = $"{CC.Ad} Takım Puanları"; 
                page.CTs.Data = Db.SQL<CT>("SELECT r FROM CT r WHERE r.CC.ObjectNo = ? order by r.Idx", cc);
                return page;

            });

            Handle.GET("/bodved/partials/CTPs/{?}", (ulong ct) =>
            {
                var page = new CTPsPage();
                page.CTPs.Data = Db.SQL<CTP>("SELECT r FROM CTP r WHERE r.CT.ObjectNo = ?", ct);
                return page;
            });

        }
    }
}
