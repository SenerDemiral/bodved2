using System;
using Starcounter;
using BDB2;
using bodved2.ViewModels;
using bodved2.Api;
//using bodved2.Helpers;

namespace bodved2
{
    class Program
    {
        static void Main()
        {
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
            PP.RefreshStat();

            MAC.RefreshGlobalRank();

            //CTP.UpdateRnkBas(); //Yeni doneme basinda (bodved2 ye gecis)
            //CTP.UpdateRnkBit(); //Yeni doneme bitiminde
        }
    }
}