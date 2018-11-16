using System;
using System.Linq;
using System.Threading.Tasks;
using BDB2;
using Grpc.Core;
using RestLib;
using Starcounter;


namespace RestStarcounterServer
{
    class Program
    {
        const int Port = 6000; //50055;

        static void Main()
        {
            // DROPs
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "PPRD_Idx").FirstOrDefault() != null)
                Db.SQL("DROP INDEX PPRD_Idx ON PPRD");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "PPR_PP").FirstOrDefault() != null)
                Db.SQL("DROP INDEX PPR_PP ON PPR");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "PPR_Dnm").FirstOrDefault() != null)
                Db.SQL("DROP INDEX PPR_Dnm ON PPR");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "PPR_Idx").FirstOrDefault() != null)
                Db.SQL("DROP INDEX PPR_Idx ON PPR");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "PPR_CC").FirstOrDefault() != null)
                Db.SQL("DROP INDEX PPR_CC ON PPR");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CTP_CTPP").FirstOrDefault() != null)
                Db.SQL("DROP INDEX CTP_CTPP ON CTP");


            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "PP_Ad").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX PP_Ad ON PP (Ad)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "PP_RnkIdx").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX PP_RnkIdx ON PP (RnkIdx)");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "PPRD_PPDnm").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX PPRD_PPDnm ON PPRD (PP, Dnm)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "PPRD_PP").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX PPRD_PP ON PPRD (PP)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "PPRd_Dnm").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX PPRD_Dnm ON PPRD (Dnm)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "PPRD_RnkIdx").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX PPRD_RnkIdx ON PPRD (RnkIdx)");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_CC").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_CC ON MAC (CC)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_CEB").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_CEB ON MAC (CEB)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_Trh").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_Trh ON MAC (Trh)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_hPP1").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_hPP1 ON MAC (HPP1)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_hPP2").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_hPP2 ON MAC (HPP2)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_gPP1").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_gPP1 ON MAC (GPP1)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "MAC_gPP2").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX MAC_gPP2 ON MAC (GPP2)");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CT_CC").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CT_CC ON CT (CC)");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CTP_CC").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CTP_CC ON CTP (CC)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CTP_CT").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CTP_CT ON CTP (CT)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CTP_PP").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CTP_PP ON CTP (PP)");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CET_CC").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CET_CC ON CET (CC)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CET_Trh").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CET_Trh ON CET (Trh)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CET_hCT").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CET_hCT ON CET (HCT)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CET_gCT").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CET_gCT ON CET (GCT)");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CF_CC").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CF_CC ON CF (CC)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CF_PP").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CF_PP ON CF (PP)");

            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CEF_CC").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CEF_CC ON CEF (CC)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CEF_Trh").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CEF_Trh ON CEF (Trh)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CEF_hPP").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CEF_hPP ON CEF (HPP)");
            if (Db.SQL("SELECT i FROM Starcounter.Metadata.\"Index\" i WHERE Name = ?", "CEF_gPP").FirstOrDefault() == null)
                Db.SQL("CREATE INDEX CEF_gPP ON CEF (GPP)");

            /*
            Hook<CC>.CommitUpdate += (p, obj) =>
            {
                Session.RunTaskForAll((s, id) =>
                {
                    s.CalculatePatchAndPushOnWebSocket();
                });
            };*/


            // Sadece Yeni DB ilk calistiginda yap
            //HBR.RestoreDB();    // Hic PP yoksa yapar

            //H.PopAll();
            //H.PPmove2baz(); // Eski Lig rank'i BazRnk'e koy

            /*
            H.PopPP();
            H.PopCC();
            H.PopCT();
            H.PopCTP();
            H.PopCET();
            H.PopMAC();
            */

            // Bunkara Gerek kalmadi
            /*
            H.MAC_RefreshSonuc();
            H.CEF_RefreshSonuc();
            H.CF_RefreshSonuc();
            H.CET_RefreshSonuc();
            H.CT_RefreshSonuc();
            H.CTP_RefreshSonucNew();
            H.PP_RefreshSonuc();
            H.MAC_RefreshGlobalRank();
            */
            //CTP.UpdateRnkBas(); //Yeni doneme basinda (bodved2 ye gecis)

            //CTP.UpdateRnkBit(); //Yeni doneme bitiminde

            //HBR.BackupDB();

            Server server = new Server
            {
                Services = { CRUDs.BindService(new CRUDsImpl()) },
                Ports = { new ServerPort("127.0.0.1", Port, ServerCredentials.Insecure) }
                //Ports = { new ServerPort("217.160.13.102", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Rest server listening on port " + Port);

            Handle.GET("/RestStarcounterServer", () =>
            {
                Task.Run(async () => { await server.ShutdownAsync(); }).Wait();
                //server.ShutdownAsync();
                return "ShutDown gRPC Server OK";
            });


        }
    }
}