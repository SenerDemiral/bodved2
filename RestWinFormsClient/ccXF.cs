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

namespace RestWinFormsClient
{
    public partial class ccXF : DevExpress.XtraEditors.XtraForm
    {
        public ccXF()
        {
            InitializeComponent();

            
        }

        private void ccXF_Load(object sender, EventArgs e)
        {
            FillDB();
        }

        private void FillDB()
        {
            string res = "";
            cCGridControl.DataSource = null;
            dataSetGnl.CC.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.CCFill(); }).Wait();
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
                frm.CCRow = (DataSetGnl.CCRow)gridView1.GetFocusedDataRow();
                frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} Teams [ctXF]";
                frm.ShowDialog();
            }
            else if (gridView1.GetFocusedRowCellValue(colSkl).ToString() == "F")
            {
                cfXF frm = new cfXF();
                frm.CCRow = (DataSetGnl.CCRow)gridView1.GetFocusedDataRow();
                frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} Players [cfXF]";
                frm.ShowDialog();
            }
        }

        private void eventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // CC.Skl ine gore T/F
            if (gridView1.GetFocusedRowCellValue(colSkl).ToString() == "T")
            {
                cetXF frm = new cetXF();
                frm.CCRow = (DataSetGnl.CCRow)gridView1.GetFocusedDataRow();
                frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} / {gridView1.GetFocusedRowCellValue(colSkl)} Events [cetXF]";
                frm.ShowDialog();
            }
            else if (gridView1.GetFocusedRowCellValue(colSkl).ToString() == "F")
            {
                cefXF frm = new cefXF();
                frm.CCRow = (DataSetGnl.CCRow)gridView1.GetFocusedDataRow();
                frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} / {gridView1.GetFocusedRowCellValue(colSkl)} Events [cefXF]";
                frm.ShowDialog();
            }
        }

        private void matchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            macXF frm = new macXF();
            frm.CCRow = (DataSetGnl.CCRow)gridView1.GetFocusedDataRow();
            frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} / {gridView1.GetFocusedRowCellValue(colSkl)} Matches [macXF]";
            frm.ShowDialog();
        }
    }
}