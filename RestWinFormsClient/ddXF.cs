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
    public partial class ddXF : DevExpress.XtraEditors.XtraForm
    {
        public ddXF()
        {
            InitializeComponent();
        }

        private void ddXF_Load(object sender, EventArgs e)
        {
            FillDB();
        }

        private void FillDB()
        {
            string res = "";
            ddGridControl.DataSource = null;
            dataSetGnl.CC.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.DDFill(); }).Wait();
            //dataSetGnl.DDFill().Wait();
            toolStripStatusLabel1.Text = res;
            ddGridControl.DataSource = ddBindingSource;

            gridView1.BestFitColumns();
        }

        private void ddBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private DialogResult UpdateDB()
        {
            if (!Validate())
                return DialogResult.Cancel;
            ddBindingSource.EndEdit();

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
                    string err = dataSetGnl.DDUpdate();
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

        private void cCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ccXF frm = new ccXF();
            frm.MdiParent = Program.MF;
            frm.DDRow = (DataSetGnl.DDRow)gridView1.GetFocusedDataRow();
            frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} Competitions [ccXF]";
            frm.Show();

        }

        private void cETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cetXF frm = new cetXF();
            frm.MdiParent = Program.MF;
            frm.DDRow = (DataSetGnl.DDRow)gridView1.GetFocusedDataRow();
            frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} Events [cetXF]";
            frm.Show();

        }

        private void pPRDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pprdXF frm = new pprdXF();
            frm.MdiParent = Program.MF;
            frm.DDRow = (DataSetGnl.DDRow)gridView1.GetFocusedDataRow();
            frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} Rank [pprdXF]";
            frm.Show();
        }
    }
}