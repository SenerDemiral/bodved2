using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grpc.Core;
using RestLib;


namespace RestWinFormsClient2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static ulong ccOno = 0;
        public static MainXF MF; // = new MainXF();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MF = new MainXF();
            MF.FillTanimlar();
            Application.Run(MF);
        }
    }

    public static class grpcService
    {
        public static Channel channel = new Channel($"localhost:50055", ChannelCredentials.Insecure);
        //public static Channel channel = new Channel($"217.160.13.102:6000", ChannelCredentials.Insecure);
        public static CRUDs.CRUDsClient ClientCRUDs = new CRUDs.CRUDsClient(channel);
    }

}
