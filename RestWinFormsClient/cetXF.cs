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
    public partial class cetXF : DevExpress.XtraEditors.XtraForm
    {
        public DataSetGnl.CCRow CCRow = null;
        private string qry;
        private ulong prm;

        public cetXF()
        {
            InitializeComponent();

            cETGridControl.ExternalRepository = Program.MF.persistentRepository;
            colCC.ColumnEdit = Program.MF.CCrepositoryItemLookUpEdit;
            colHCT.ColumnEdit = Program.MF.CTrepositoryItemLookUpEdit;
            colGCT.ColumnEdit = Program.MF.CTrepositoryItemLookUpEdit;
            colTrh.ColumnEdit = Program.MF.TRHrepositoryItemDateEdit;
            colDrm.ColumnEdit = Program.MF.DRMrepositoryItemImageComboBox;
        }

        private void cetXF_Load(object sender, EventArgs e)
        {
            qry = "";
            prm = 0;

            if (CCRow == null)
                gridView1.OptionsBehavior.Editable = true; //false;
            else
            {
                qry = "CC";
                prm = CCRow.RowKey;

                colCC.Visible = false;
            }

            FillDB();
        }

        private void FillDB()
        {
            string res = "";
            cETGridControl.DataSource = null;
            dataSetGnl.CET.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.CETFill(qry, prm); }).Wait();
            toolStripStatusLabel1.Text = res;
            cETGridControl.DataSource = cETBindingSource;

            gridView1.BestFitColumns();
        }

        private void cETBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private DialogResult UpdateDB()
        {
            if (!Validate())
                return DialogResult.Cancel;
            cETBindingSource.EndEdit();

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
                    string err = dataSetGnl.CETUpdate();
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
            gridView1.SetFocusedRowCellValue(colCC, CCRow.RowKey);
            gridView1.SetFocusedRowCellValue(colHCT, 0);
            gridView1.SetFocusedRowCellValue(colGCT, 0);

        }

        private void maclarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            macXF frm = new macXF();
            frm.CETRow = (DataSetGnl.CETRow)gridView1.GetFocusedDataRow();
            frm.Text = $"{gridView1.GetFocusedRowCellDisplayText(colHCT)} >< {gridView1.GetFocusedRowCellDisplayText(colGCT)} Matches [macXF]";
            frm.Show();
        }
    }
}