using System;
using Starcounter;
using BDB2;

namespace bodved2
{
    class Program
    {
        static void Main()
        {
            H.PopPP();
            H.PopCC();
            H.PopCT();
            H.PopCTP();
            H.PopCET();
            H.PopMAC();

            PP.RefreshStat();
            MAC.RefreshGlobalRank();

            //CTP.RefreshSonuc();
            MAC.deneme();
        }
    }
}