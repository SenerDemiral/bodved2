using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bodved2.ViewModels;
using Starcounter;

namespace bodved2.Api
{
    class MainHandlers : IHandler
    {
        public void Register()
        {
            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());

            // Workspace home page (landing page from launchpad) dashboard alias
            Handle.GET("/bodved", () =>
            {
                MasterPage master = GetMasterPageFromSession();
                return master;

                //return Self.GET("/bodved/organizations");
            });

            Handle.GET("/bodved/organizations", () =>
            {

                MasterPage master = GetMasterPageFromSession();
                /*
                if (!(master.CurrentPage is OrganizationsPage))
                {
                    master.CurrentPage = GetLauncherPage("/bodved/partials/organizations");
                    //(master.CurrentPage as OrganizationsPage).Init();
                }*/
                return master;
            });

            Handle.GET("/bodved/PPs", () =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is PPsPage))
                {
                    master.CurrentPage = GetLauncherPage("/bodved/partials/PPs");
                    //(master.CurrentPage as CCsPage).Init();
                }
                return master;
            });

            Handle.GET("/bodved/CCs", () =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is CCsPage))
                {
                    master.CurrentPage = GetLauncherPage("/bodved/partials/CCs");
                    //(master.CurrentPage as CCsPage).Init();
                }
                return master;
            });

            Handle.GET("/bodved/CTs/{?}", (ulong cc) =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is CTsPage))
                {
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CTs/{cc}");
                    //(master.CurrentPage as CCsPage).Init();
                }
                return master;
            });

            Handle.GET("/bodved/CTPs/{?}", (ulong ct) =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is CTPsPage))
                {
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CTPs/{ct}");
                    //(master.CurrentPage as CCsPage).Init();
                }
                return master;
            });

            Handle.GET("/bodved/CETs/{?}", (ulong cc) =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is CTsPage))
                {
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CETs/{cc}");
                    //(master.CurrentPage as CCsPage).Init();
                }
                return master;
            });

            Handle.GET("/bodved/CET2MACs/{?}", (ulong cet) =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is CET2MACsPage))
                {
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CET2MACs/{cet}");
                    //(master.CurrentPage as CCsPage).Init();
                }
                return master;
            });

        }

        private static Json GetLauncherPage(string url, bool dbScope = false)
        {
            if (dbScope)
                return Db.Scope(() => Self.GET<Json>(url));
            else
                return Self.GET<Json>(url);
        }

        protected static MasterPage GetMasterPageFromSession()
        {
            var session = Session.Ensure();

            var master = session.Store[nameof(MasterPage)] as MasterPage ?? new MasterPage();

            session.Store[nameof(MasterPage)] = master;

            master.ShowMenu = true;
            return master;
        }

    }
}
