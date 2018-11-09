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

            H.MAC_RefreshSonuc();
            H.CET_RefreshSonuc();
            H.CT_RefreshSonuc();
            H.CTP_RefreshSonucNew();
            H.PP_RefreshSonuc();
            H.CEF_RefreshSonuc();
            H.CF_RefreshSonuc();

            H.MAC_RefreshGlobalRank();

            //CF.CreateCEFs(5131);

            //PPR.DonemBaslangicIslemleri(18);

            //MAC.RefreshDonemRank22222(18);

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


            //H.PPRD_DonemBasiIslemleri(17);
            //H.PPRD_DonemBasiIslemleri(18);

            H.MAC_RefreshDonemRank(17);
            H.MAC_RefreshDonemRank(18);

        }
    }
}