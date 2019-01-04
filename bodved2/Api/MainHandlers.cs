using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bodved2.ViewModels;
using Starcounter;
using BDB2;
using System.Globalization;
using System.Web;
using System.Net.Mail;

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
                //MasterPage master = GetMasterPageFromSession();
                //return master;

                return Self.GET("/bodved/DDs");
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
                (master.CurrentPage as DDsPage).canMdfy = master.Token == "" ? false : true;
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
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CC2CETs/{cc}");
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

            Handle.GET("/bodved/CF2CEFs/{?}/{?}", (ulong cc, ulong pp) =>
            {
                MasterPage master = GetMasterPageFromSession();
                //if (!(master.CurrentPage is CT2CETsPage))
                {
                    master.CurrentPage = GetLauncherPage($"/bodved/partials/CF2CEFs/{cc}/{pp}");
                    //(master.CurrentPage as CCsPage).Init();
                }
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

            ///////////////////////////////////////////////////

            // Lookup calisma
            Handle.GET("/bodved/PPjson/{?}", (long ticks) =>
            {
                var json = new PPsJson();
                //ticks den sonra degisenleri gonder
                var dt = new DateTime(ticks);
                json.PPs.Data = Db.SQL<PP>("SELECT r FROM PP r where r.ObjectNo > ?", 0);
                json.Read.Ticks = DateTime.Now.Ticks;
                return json;
            });

            Handle.POST("/bodved/PUTjson/{?}", (string unp) =>
            {
                var es = ""; // EncodeQueryString("sener.demiral@gmail.com");
                var ds = ""; // DecodeQueryString(es);

                //SendMail();

                return $"{unp} : {es} {ds}";
            });
            /*
            Handle.PUT("/bodved/Sign", (UserInfo ui) =>
            {
                UU uu = null;
                var aaa = Session.Current;
                MasterPage master = GetMasterPageFromSession();

                if ( !string.IsNullOrEmpty(ui.token) && string.IsNullOrEmpty(ui.email) && string.IsNullOrEmpty(ui.pwd))  // AutoSignIn
                {
                    uu = Db.SQL<UU>("select r from UU r where r.Token = ?", ui.token).FirstOrDefault();
                    if(uu == null)
                    {
                        ui.token = "";
                        ui.mesaj = "Tekrar Giriş Yapın.";
                    }
                    else
                    {
                        //var session = Session.Ensure();
                        //session.Store["bodved"] = ui;
                        //MasterPage master = GetMasterPageFromSession();
                        //master.Token = ui.token;
                        ui.mesaj = "";
                    }

                }
                else if (!string.IsNullOrEmpty(ui.email) && !string.IsNullOrEmpty(ui.pwd))  // Sign Up/In
                {
                    uu = Db.SQL<UU>("select r from UU r where r.Email = ?", ui.email).FirstOrDefault();
                    if(uu == null)  // SignUp
                    {
                        string newToken = EncodeQueryString(ui.email); // CreateToken
                        Db.Transact(() =>
                        {
                            new UU
                            {
                                Email = ui.email,
                                Pwd = ui.pwd,
                                Token = newToken,
                                InsTS = DateTime.Now,
                                IsConfirmed = false,
                            };
                        });
                        var email = EncodeQueryString(ui.email);
                        SendMail(email);
                        ui.email = "";
                        ui.pwd = "";
                        ui.mesaj = "Mailinize gelen linki tıklayarak doğrulama işlemini tamamlayın.";
                    }
                    else  // SignIn
                    {
                        if(uu.Pwd == ui.pwd)
                        {
                            ui.pwd = "";
                            ui.token = uu.Token;
                            ui.mesaj = "LoggedIn";
                        }
                        else
                        {
                            ui.pwd = "";
                            ui.token = "";
                            ui.mesaj = "Hatali eMail/Password";
                        }
                    }
                }

                return ui;

                //return $"{es} {ds}";
                //return $"{signTxt} : {es} {ds}";
            });
            */
            Handle.GET("/bodved/confirmemail/{?}", (string deMail) =>
            {
                MasterPage master = GetMasterPageFromSession();
                var eMail = H.DecodeQueryString(deMail);

                master.Token = "";

                UU uu = Db.SQL<UU>("select r from UU r where r.Email = ?", eMail).FirstOrDefault();
                if(uu != null)
                {
                    Db.Transact(() =>
                    {
                        uu.IsConfirmed = true;
                    });
                    master.Token = uu.Token;
                    //return master; // Self.GET("/bodved/DDs");
                }
                return master;
            });

        }

        private static Json GetLauncherPage(string url, bool dbScope = false)
        {
            if (dbScope)
                return Db.Scope(() => Self.GET<Json>(url));
            else
            {
                return Self.GET<Json>(url);
            }
        }

        protected static MasterPage GetMasterPageFromSession()
        {
            var session = Session.Ensure();

            MasterPage master = null; // session.Store[nameof(MasterPage)] as MasterPage ?? new MasterPage();

            if (session.Store[nameof(MasterPage)] == null)
            {
                master = new MasterPage();

                // https://github.com/Starcounter/Home/issues/455#issuecomment-443884798
                /*// tr calismiyor
                CultureInfo culture = CultureInfo.CreateSpecificCulture("tr-TR");
                session.CurrentCulture = culture;
                session.CurrentUICulture = culture;
                */
                session.Store[nameof(MasterPage)] = master;
                // increment site entry counter

                master.EntCntFrmtd = $"{BDB2.STAT.UpdEntCnt():n0}";
            }
            else
            {
                if (session.Store["bodved"] != null)
                {
                    var aaa = session.Store["bodved"];
                    master.Token = (string)aaa["token"];
                }
                master = session.Store[nameof(MasterPage)] as MasterPage;
            }

            return master;
        }

    }
}
