using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Diagnostics;

using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RestWinFormsClient2
{
    public partial class MainXF : DevExpress.XtraEditors.XtraForm
    {
        Stopwatch sw = new Stopwatch();
        int nor = 0;

        public MainXF()
        {
            InitializeComponent();

        }

        public void FillTanimlar()
        {
            nor = 0;

            //string err = dataSetGnl.PerfomAction("RefreshSonucDnmRun");

            Program.ccOno = dataSetGnl.Login("?", "password");

            Task.Run(async () =>
            {
                sw.Restart();
                //watcher.Start();
                
                dataSetGnl.PPlu.Rows.Clear();
                //dataSetGnl.CC.Rows.Clear();
                //dataSetGnl.CT.Rows.Clear();

                await dataSetGnl.PPlookUp();
                //await dataSetGnl.CCFill("", 0);
                //await dataSetGnl.CTFill("", 0);
                
                sw.Stop();
                //InitLookups();
            }).ContinueWith((t) => {

                toolStripStatusLabel1.Text = $"Lookup recs read in {sw.ElapsedMilliseconds:n0} milisec [{sw.Elapsed}]";
            });

        }

    }
}