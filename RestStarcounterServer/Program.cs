using System;
using System.Linq;
using BDB2;
using Grpc.Core;
using RestLib;
using Starcounter;


namespace RestStarcounterServer
{
    class Program
    {
        const int Port = 50055;

        static void Main()
        {
            Server server = new Server
            {
                Services = { CRUDs.BindService(new CRUDsImpl()) },
                Ports = { new ServerPort("127.0.0.1", Port, ServerCredentials.Insecure) }
                //Ports = { new ServerPort("217.160.13.102", Port, ServerCredentials.Insecure) }
                //Ports = { new ServerPort("192.168.1.20", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Rest server listening on port " + Port);

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_CEB").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_CEB ON MAC (CEB)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_Trh").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_Trh ON MAC (Trh)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_hPP1").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_hPP1 ON MAC (hPP1)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_hPP2").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_hPP2 ON MAC (hPP2)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_gPP1").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_gPP1 ON MAC (gPP1)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_gPP2").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_gPP2 ON MAC (gPP2)");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CTP_CT").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CTP_CT ON CTP (CT)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CTP_PP").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CTP_PP ON CTP (PP)");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CET_Trh").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CET_Trh ON CET (Trh)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CET_hCT").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CET_hCT ON CET (hCT)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CET_gCT").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CET_gCT ON CET (gCT)");

            //MAC.deneme();
            H.PopPP();
            H.PopCC();
            H.PopCT();
            H.PopCTP();
            H.PopCET();
            H.PopMAC();

            CET.RefreshSonuc();
            CT.RefreshSonuc();
            CTP.RefreshSonucNew();
            PP.RefreshStat();
            MAC.RefreshGlobalRank();


        }
    }
}