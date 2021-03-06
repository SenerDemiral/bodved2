﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RestWinFormsClient
{
    public partial class ccXF : DevExpress.XtraEditors.XtraForm
    {
        public DataSetGnl.DDRow DDRow = null;
        private string qry = "";
        private ulong prm = 0;

        public ccXF()
        {
            InitializeComponent();
        }

        private void ccXF_Load(object sender, EventArgs e)
        {
            if(DDRow != null)
            {
                qry = "DD";
                prm = (ulong)DDRow.Dnm;
            }

            FillDB();
        }

        private void FillDB()
        {
            string res = "";
            cCGridControl.DataSource = null;
            dataSetGnl.CC.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.CCFill(qry, prm); }).Wait();
            toolStripStatusLabel1.Text = res;
            cCGridControl.DataSource = cCBindingSource;

            gridView1.BestFitColumns();
        }

        private void cCBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private DialogResult UpdateDB()
        {
            if (!Validate())
                return DialogResult.Cancel;
            cCBindingSource.EndEdit();

            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            DialogResult dr = DialogResult.OK;

            // Ok:    No change
            // Yes:   Update succesfull
            // Abort: Hata
            // No:    Update var kaydetmedi

            if (dataSetGnl.HasChanges())
            {
                dr = XtraMessageBox.Show("Değişiklik var. Kaydetmek istiyormusunuz?", "Update", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    string err = dataSetGnl.CCUpdate();
                    if (err != string.Empty)
                    {
                        MessageBox.Show(err);
                        dr = DialogResult.Abort;
                    }
                }
            }
            return dr;
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(colRowKey, 0);

        }

        private void teamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue(colSkl).ToString() == "T")
            {
                ctXF frm = new ctXF();
                frm.MdiParent = Program.MF;
                frm.CCRow = (DataSetGnl.CCRow)gridView1.GetFocusedDataRow();
                frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} Teams [ctXF]";

                frm.Show();
            }
            else if (gridView1.GetFocusedRowCellValue(colSkl).ToString() == "F")
            {
                cfXF frm = new cfXF();
                frm.MdiParent = Program.MF;
                frm.CCRow = (DataSetGnl.CCRow)gridView1.GetFocusedDataRow();
                frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} Players [cfXF]";
                frm.Show();
            }
        }

        private void eventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // CC.Skl ine gore T/F
            if (gridView1.GetFocusedRowCellValue(colSkl).ToString() == "T")
            {
                cetXF frm = new cetXF();
                frm.MdiParent = Program.MF;
                frm.CCRow = (DataSetGnl.CCRow)gridView1.GetFocusedDataRow();
                frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} / {gridView1.GetFocusedRowCellValue(colSkl)} Events [cetXF]";
                frm.Show();
            }
            else if (gridView1.GetFocusedRowCellValue(colSkl).ToString() == "F")
            {
                cefXF frm = new cefXF();
                frm.MdiParent = Program.MF;
                frm.CCRow = (DataSetGnl.CCRow)gridView1.GetFocusedDataRow();
                frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} / {gridView1.GetFocusedRowCellValue(colSkl)} Events [cefXF]";
                frm.Show();
            }
        }

        private void matchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            macXF frm = new macXF();
            frm.MdiParent = Program.MF;
            frm.CCRow = (DataSetGnl.CCRow)gridView1.GetFocusedDataRow();
            frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} / {gridView1.GetFocusedRowCellValue(colSkl)} Matches [macXF]";
            frm.Show();
        }

        private void createEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string err = dataSetGnl.PerfomAction("CreateEvents", $"{gridView1.GetFocusedRowCellValue(colRowKey)}");
            if (err != string.Empty)
                MessageBox.Show(err);
        }

        private void refreshSonAktiviteleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string err = dataSetGnl.PerfomAction("RefeshCurrentActivities", $"{gridView1.GetFocusedRowCellValue(colDnm)}");
            if (err != string.Empty)
                MessageBox.Show(err);

        }
    }
}