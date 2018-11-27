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
            Handle.GET("/bodved/partials/aboutpage", () =>
            {
                var page = new AboutPage();
                return page;
            });

            Handle.GET("/bodved/partials/PPs", () =>
            {
                var page = new PPsPage();
                //page.PPs.Data = Db.SQL<PP>("SELECT r FROM PP r order by r.RnkIdx");

                //var top = Db.SQL<long>("select COUNT(r) from PP r").FirstOrDefault();
                //var aktif = Db.SQL<long>("select count(r) from PP r where r.IsRun = ?", true).FirstOrDefault();
                //page.PPs.Data = Db.SQL<PP>("SELECT r FROM PP r order by r.Ad");

                page.Data = null;
                return page;
            });

            Handle.GET("/bodved/partials/DDs", () =>
            {
                var page = new DDsPage();
                //page.DDs.Data = Db.SQL<DD>("SELECT r FROM DD r order by r.Dnm DESC");
                page.Data = null;
                return page;
            });

            Handle.GET("/bodved/partials/PPRDs/{?}", (int dnm) =>
            {
                var page = new PPRDsPage();
                page.Dnm = dnm;
                //DD DD = Db.SQL<DD>("select r from DD r where r.Dnm = ?", dnm).FirstOrDefault();
                //page.Hdr = $"{DD.Ad} ► Oyuncular";
                //page.PPRDs.Data = Db.SQL<PPRD>("SELECT r FROM PPRD r where r.Dnm = ? order by r.RnkIdx", dnm);

                page.Data = null;
                return page;
            });


            Handle.GET("/bodved/partials/CCs/{?}", (int dnm) =>
            {
                var page = new CCsPage();
                page.CCs.Data = Db.SQL<CC>("SELECT r FROM CC r where r.Dnm = ? order by r.Idx", dnm);
                //page.Data = null;
                return page;
            });

            Handle.GET("/bodved/partials/CTs/{?}", (ulong cc) =>
            {
                var page = new CTsPage();
                CC CC = Db.FromId<CC>(cc);
                page.CCoNo = (long)CC.CCoNo;
                page.Hdr = $"{CC.Ad} ► Takım Puanları"; 
                page.CTs.Data = Db.SQL<CT>("SELECT r FROM CT r WHERE r.CC = ? order by r.Idx", CC);
                return page;
            });

            Handle.GET("/bodved/partials/CFs/{?}", (ulong cc) =>
            {
                var page = new CFsPage();
                page.CCoNo = (long)cc;
                page.Data = null;
                return page;
            });

            Handle.GET("/bodved/partials/CTPs/{?}", (ulong ct) =>
            {
                var page = new CTPsPage();

                //CT CT = Db.FromId<CT>(ct);
                //page.Hdr = $"{CT.CC.Ad} ► {CT.Ad} ► Takım Oyuncuları";
                //page.CTPs.Data = Db.SQL<CTP>("SELECT r FROM CTP r WHERE r.CT = ? order by r.RnkBas DESC, r.PP.Ad", CT);

                page.CToNo = (long)ct;
                page.Data = null;
                return page;
            });

            Handle.GET("/bodved/partials/CurEvents", () =>
            {
                var page = new CurEventsPage();
                page.Data = null;
                return page;
            });


            Handle.GET("/bodved/partials/CC2CETs/{?}", (ulong cc) =>
            {
                var page = new CC2CETsPage();
                page.CCoNo = (long)cc;
                page.Data = null;
                return page;
            });

            // CC2CETs kullaniliyor
            Handle.GET("/bodved/partials/CETs/{?}", (ulong cc) =>
            {
                var page = new CETsPage();
                CC CC = Db.FromId<CC>(cc);
                page.Hdr = $"{CC.Ad} ► Takım Fikstür";
                page.CETs.Data = Db.SQL<CET>("SELECT r FROM CET r WHERE r.CC = ? order by r.Trh", CC);
                return page;
            });

            Handle.GET("/bodved/partials/CEFs/{?}", (ulong cc) =>
            {
                var page = new CEFsPage();
                CC CC = Db.FromId<CC>(cc);
                page.Hdr = $"{CC.Ad} ► Ferdi Fikstür";
                page.CEFs.Data = Db.SQL<CEF>("SELECT r FROM CEF r WHERE r.CC = ? order by r.Trh, r.HPP.Ad, r.GPP.Ad", CC);
                return page;
            });

            Handle.GET("/bodved/partials/CET2MACs/{?}", (ulong cet) =>
            {
                var page = new CET2MACsPage();
                CET CET = Db.FromId<CET>(cet);

                page.Hdr = $"{CET.CC.Ad} ► Takım Maçları ► {CET.Trh:dd.MM.yy}";

                page.CET.Data = CET;
                page.Sngls.Data = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.CEB = ? and r.SoD = ? order by r.Idx", CET, "S");
                page.Dbls.Data = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.CEB = ? and r.SoD = ? order by r.Idx", CET, "D");
                return page;
            });

            Handle.GET("/bodved/partials/CEF2MACs/{?}", (ulong cef) =>
            {
                var page = new CEF2MACsPage();
                CEF CEF = Db.FromId<CEF>(cef);

                page.Hdr = $"{CEF.CC.Ad} ► Ferdi Maçları ► {CEF.Trh:dd.MM.yy}";

                page.CEF.Data = CEF;
                page.Sngls.Data = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.CEB = ? order by r.Idx", CEF);
                return page;
            });

            Handle.GET("/bodved/partials/PP2MACs/{?}", (ulong pp) =>
            {
                var page = new PP2MACsPage();
                page.PPoNo = (long)pp;
                page.Data = null;
                return page;

            });

            Handle.GET("/bodved/partials/PP2PPRDs/{?}", (ulong pp) =>
            {
                var page = new PP2PPRDsPage();
                PP PP = Db.FromId<PP>(pp);
                page.Hdr = $"{PP.Ad} ► Dönem Bilgileri";
                page.PPRDs.Data = Db.SQL<PPRD>("select r from PPRD r where r.PP = ? order by r.Dnm DESC", PP);
                return page;
            });

            Handle.GET("/bodved/partials/CT2CETs/{?}", (long ct) =>
            {
                var page = new CT2CETsPage();
                page.CToNo = ct;
                page.Data = null;
                return page;
            });

        }
    }
}
