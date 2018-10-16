﻿using Grpc.Core;
using RestLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestWinFormsClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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
        public static Channel channel = new Channel($"127.0.0.1:50055", ChannelCredentials.Insecure);
        public static CRUDs.CRUDsClient ClientCRUDs = new CRUDs.CRUDsClient(channel);
    }

}
