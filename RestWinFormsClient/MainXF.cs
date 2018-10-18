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

        ppXF frmPP;
        ccXF frmCC;
        ctXF frmCT;
        cetXF frmCET;
        macXF frmMAC;

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

                await dataSetGnl.PPlookUp();
                await dataSetGnl.CCFill();
                await dataSetGnl.CTFill("",0);

                sw.Stop();
                //InitLookups();
            }).ContinueWith((t) => {
                
                toolStripStatusLabel1.Text = $"Lookup recs read in {sw.ElapsedMilliseconds:n0} milisec [{sw.Elapsed}]";
            });

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

        private void CCnavBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
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

        private void CETnavBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var doc = documentManager1.GetDocument(frmCET);
            if (doc != null)
                tabbedView1.Controller.Activate(doc);
            else
            {
                frmCET = new cetXF
                {
                    MdiParent = this
                };
                frmCET.Show();
            }
        }

        private void MACnavBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var doc = documentManager1.GetDocument(frmMAC);
            if (doc != null)
                tabbedView1.Controller.Activate(doc);
            else
            {
                frmMAC = new macXF
                {
                    MdiParent = this
                };
                frmMAC.Show();
            }
        }

        private void PPrepositoryItemGridLookUpEdit_QueryCloseUp(object sender, CancelEventArgs e)
        {
            var view = (sender as GridLookUpEdit).Properties.View;
            bool isRun = (bool)view.GetFocusedRowCellValue("IsRun");    // Availability
            e.Cancel = !isRun;

        }
    }
}