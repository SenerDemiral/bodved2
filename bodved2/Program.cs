using System;
using Starcounter;
using BDB2;
using bodved2.ViewModels;
using bodved2.Api;
using System.Globalization;
using System.Threading;
//using bodved2.Helpers;

namespace bodved2
{
    class Program
    {
        static void Main()
        {
            
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("tr-TR");
            //Console.WriteLine($"{DateTime.Now:dd.MM.yy ddd}");  // Ingilizce
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            //Console.WriteLine($"{DateTime.Now:dd.MM.yy ddd}");  // Turkce, BDB2.Entity de olmuyor!!

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

            MAC.RefreshSonuc();
            CET.RefreshSonuc();
            CT.RefreshSonuc();
            CTP.RefreshSonucNew();
            PP.RefreshSonuc();
            CEF.RefreshSonuc();
            CF.RefreshSonuc();

            MAC.RefreshGlobalRank();

            //CF.CreateCEFs(5131);

            //PPR.DonemBaslangicIslemleri(18);

            //MAC.RefreshDonemRank22222(18);


        }
    }
}