using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;

namespace RestWinFormsClient
{
    public partial class MainXF : DevExpress.XtraEditors.XtraForm
    {
        Stopwatch sw = new Stopwatch();
        int nor = 0;
        ccXF frmCC;
        ctXF frmCT;
        ppXF frmPP;

        public MainXF()
        {
            InitializeComponent();
        }

        public void FillTanimlar()
        {
            nor = 0;

            Task.Run(async () =>
            {
                sw.Restart();
                //watcher.Start();

                await dataSetGnl.PPFill();
                await dataSetGnl.CCFill();
                await dataSetGnl.CTFill();

                sw.Stop();
                //InitLookups();
            }).ContinueWith((t) => {
                toolStripStatusLabel1.Text = $"recs read in {sw.ElapsedMilliseconds:n0} msec (1/1000sec)";
            });

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var doc = documentManager1.GetDocument(frmCC);
            if (doc != null)
                tabbedView1.Controller.Activate(doc);
            else
            {
                frmCC = new ccXF
                {
                    MdiParent = this
                };
                frmCC.Show();
            }

        }

        private void MainXF_Load(object sender, EventArgs e)
        {
        }

        private void CTnavBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var doc = documentManager1.GetDocument(frmCT);
            if (doc != null)
                tabbedView1.Controller.Activate(doc);
            else
            {
                frmCT = new ctXF
                {
                    MdiParent = this
                };
                frmCT.Show();
            }

        }

        private void PPnavBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var doc = documentManager1.GetDocument(frmPP);
            if (doc != null)
                tabbedView1.Controller.Activate(doc);
            else
            {
                frmPP = new ppXF
                {
                    MdiParent = this
                };
                frmPP.Show();
            }

        }
    }
}