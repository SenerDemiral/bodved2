using System;
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
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                //Ports = { new ServerPort("217.160.13.102", Port, ServerCredentials.Insecure) }
                //Ports = { new ServerPort("192.168.1.20", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Rest server listening on port " + Port);

            MAC.deneme();

        }
    }
}