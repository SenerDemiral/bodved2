using System;
using Starcounter;
using BDB2;
using bodved2.ViewModels;
using bodved2.Api;
using System.Globalization;
using System.Threading;
using System.Linq;
//using bodved2.Helpers;

namespace bodved2
{
    class Program
    {
        static void Main()
        {
            /*
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("tr-TR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            */
            IHandler[] handlers = new IHandler[]
            {
                new MainHandlers(),
                //new ContentHandlers(),
                new HookHandlers(),
                new PartialHandlers()
            };

            foreach (IHandler handler in handlers)
            {
                handler.Register();
            }

            if (Db.SQL<DD>("select r from DD r").FirstOrDefault() == null)
            {
                Db.Transact(() =>
                {
                    new DD
                    {
                        Dnm = 18,
                        Ad = "2018-19 Turnuvaları"
                    };
                    new DD
                    {
                        Dnm = 17,
                        Ad = "2017-18 Turnuvaları"
                    };
                });
            }

            Console.WriteLine("------------------");
            
            /*
            // Aysen OZDEMIR/58 soyadi KONAK olmus ama ben birdaha acmisim 15696 ve yenisine CTP, CETX, MAC, PPRD girmisim (baslangic ranki 1778)
            Db.Transact(() =>
            {
                PP PPsil = (PP)Db.FromId(15696);
                if (PPsil == null)
                    return; // Bulunamadi

                PP PPcur = (PP)Db.FromId(58);
                if (PPcur == null)
                    return; // Bulunamadi

                var macs = Db.SQL<MAC>("select r from MAC r where r.GPP1 = ? or r.GPP2 = ?", PPsil, PPsil);
                foreach(var mac in macs)
                {
                    if (mac.GPP1?.GetObjectNo() == PPsil.GetObjectNo())
                        mac.GPP1 = PPcur;
                    if (mac.GPP2?.GetObjectNo() == PPsil.GetObjectNo())
                        mac.GPP2 = PPcur;
                }

                var ctps = Db.SQL<CTP>("select r from CTP r where r.PP = ?", PPsil);
                foreach (var ctp in ctps)
                    ctp.PP = PPcur;

                var cetxs = Db.SQL<CETX>("select r from CETX r where r.PP = ?", PPsil);
                foreach (var cetx in cetxs)
                    cetx.PP = PPcur;

                var pprds = Db.SQL<PPRD>("select r from PPRD r where r.PP = ?", PPsil);
                foreach (var pprd in pprds)
                    pprd.Delete();

                PPsil.Delete();
            });
            */

            // Bir kerelik
            /*
            H.PPRD_DonemBasiIslemleri(17);
            H.PPRD_DonemBasiIslemleri(18);
            H.CTP_RefreshSonuc(17);
            H.PPRD_RefreshSonuc(17);
            */

            //H.PP_RefeshCurrentActivities(18);   // PP.IsRun = T/F Onceki donem de oynamis
            
            // Yeni donem islemleri, bir kerelik
            //H.PPRD_YeniDonemIslemleri(19);

            //H.PPRD_17RankPXgosterme();
            //H.PPRD_Ayarla();          // Bir kere
            //H.PPRD_RefreshSonuc(17);    // Bir kere
            //H.CF_Create();              // Bir kere

            H.MAC_RefreshSonuc(H.DnmRun);
            H.CET_RefreshSonuc(H.DnmRun);
            H.CT_RefreshSonuc(H.DnmRun);
            H.CTP_RefreshSonuc(H.DnmRun);
            H.PPRD_RefreshSonuc(H.DnmRun);
            H.PPRD_RefreshCurRuns(H.DnmRun);
            H.DD_RefreshSonuc(H.DnmRun);


            //H.CEF_RefreshSonuc();   // Bunlari da Donem/Sezon luk yap.
            //H.CF_RefreshSonuc();
            //H.CEF_RefreshDnm18();

            // Bunlara gerek kalmadi
            /*
            H.MAC_RefreshSonuc();
            H.CET_RefreshSonuc();
            H.CT_RefreshSonuc();
            H.CTP_RefreshSonucNew();
            H.PP_RefreshSonuc();
            H.CEF_RefreshSonuc();
            H.CF_RefreshSonuc();

            H.MAC_RefreshGlobalRank();
            */

            //H.PerfDeneme();

            //H.Deneme2();
        }
    }
}