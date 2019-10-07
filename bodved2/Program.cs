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


            // Bir kerelik
            /*
            H.PPRD_DonemBasiIslemleri(17);
            H.PPRD_DonemBasiIslemleri(18);
            H.CTP_RefreshSonuc(17);
            H.PPRD_RefreshSonuc(17);
            */

            H.PP_RefeshCurrentActivities(18);   // PP.IsRun = T/F Onceki donem de oynamis
            
            // Yeni donem islemleri, bir kerelik
            H.PPRD_YeniDonemIslemleri(19);

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

            H.CEF_RefreshSonuc();   // Bunlari da Donem/Sezon luk yap.
            H.CF_RefreshSonuc();

            H.CEF_RefreshDnm18();

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