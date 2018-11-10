using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bodved2.ViewModels;
using Starcounter;
using BDB2;

namespace bodved2.Api
{
    class MainHandlers : IHandler
    {
        public void Register()
        {
            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());

            // Workspace home page (landing page from launchpad) dashboard alias

            Handle.GET("/bodved2", () =>
            {
                return Self.GET("/");
            });

            Handle.GET("/bodved", () =>
            {
                return Self.GET("/");
            });

            Handle.GET("/", () =>
            {
                //MasterPage master = GetMasterPageFromSession();
                //return master;

                return Self.GET("/bodved/DDs");
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

            Handle.GET("/bodved/AboutPage", () =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is PPsPage))
                {
                    master.CurrentPage = GetLauncherPage("/bodved/partials/aboutpage");
                    //(master.CurrentPage as CCsPage).Init();
                }
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

            Handle.GET("/bodved/DDs", () =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is DDsPage))
                {
                    master.CurrentPage = GetLauncherPage("/bodved/partials/DDs");
                }
                return master;
            });

            Handle.GET("/bodved/CurEvents", () =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is CurEventsPage))
                {
                    master.CurrentPage = GetLauncherPage("/bodved/partials/CurEvents");
                }
                return master;
            });

            Handle.GET("/bodved/PPRDs/{?}", (int dnm) =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is PPRDsPage))
                {
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/PPRDs/{dnm}");
                    //(master.CurrentPage as CCsPage).Init();
                }
                return master;
            });

            Handle.GET("/bodved/CCs/{?}", (int dnm) =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is CCsPage))
                {
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CCs/{dnm}");
                    //(master.CurrentPage as CCsPage).Init();
                }
                return master;
            });

            Handle.GET("/bodved/CE/{?}", (ulong cc) =>
            {
                // Event? CET/CEF neye gidecegini bul
                CC CC = Db.FromId<CC>(cc);

                MasterPage master = GetMasterPageFromSession();

                if (CC.Skl == "T")
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CETs/{cc}");
                else if (CC.Skl == "F")
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CEFs/{cc}");

                return master;
            });

            Handle.GET("/bodved/CK/{?}", (ulong cc) =>
            {
                // Katilanlar? CT/CF neye gidecegini bul
                CC CC = Db.FromId<CC>(cc);

                MasterPage master = GetMasterPageFromSession();

                if (CC.Skl == "T")
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CTs/{cc}");
                else if (CC.Skl == "F")
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CFs/{cc}");

                return master;
            });

            Handle.GET("/bodved/CT2CETs/{?}", (ulong ct) =>
            {
                MasterPage master = GetMasterPageFromSession();
                //if (!(master.CurrentPage is CT2CETsPage))
                {
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CT2CETs/{ct}");
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

            Handle.GET("/bodved/CEF2MACs/{?}", (ulong cet) =>
            {
                MasterPage master = GetMasterPageFromSession();
                if (!(master.CurrentPage is CEF2MACsPage))
                {
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CEF2MACs/{cet}");
                    //(master.CurrentPage as CCsPage).Init();
                }
                return master;
            });

            Handle.GET("/bodved/PP2MACs/{?}", (ulong pp) =>
            {
                MasterPage master = GetMasterPageFromSession();
                //if (!(master.CurrentPage is PP2MACsPage))
                {
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/PP2MACs/{pp}");
                    //(master.CurrentPage as CCsPage).Init();
                }
                return master;
            });

            Handle.GET("/bodved/PP2PPRDs/{?}", (ulong pp) =>
            {
                MasterPage master = GetMasterPageFromSession();
                //if (!(master.CurrentPage is PP2MACsPage))
                {
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/PP2PPRDs/{pp}");
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

            MasterPage master = null; // session.Store[nameof(MasterPage)] as MasterPage ?? new MasterPage();
            if (session.Store[nameof(MasterPage)] == null)
            {
                master = new MasterPage();
                session.Store[nameof(MasterPage)] = master;
                // increment site entry counter
                master.EntCntFrmtd = $"{BDB2.STAT.UpdEntCnt():n0}";
            }
            else
            {
                master = session.Store[nameof(MasterPage)] as MasterPage;
            }

            master.ShowMenu = true;
            return master;
        }

    }
}
