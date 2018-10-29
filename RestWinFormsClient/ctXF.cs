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
using DevExpress.XtraGrid.Columns;

namespace RestWinFormsClient
{
    public partial class ctXF : DevExpress.XtraEditors.XtraForm
    {
        public DataSetGnl.CCRow CCRow = null;
        private string qry;
        private ulong prm;

        public ctXF()
        {
            InitializeComponent();

            cTGridControl.ExternalRepository = Program.MF.persistentRepository;
            colCC.ColumnEdit = Program.MF.CCrepositoryItemLookUpEdit;
            colK1.ColumnEdit = Program.MF.PPrepositoryItemLookUpEdit;
            colK2.ColumnEdit = Program.MF.PPrepositoryItemLookUpEdit;
        }

        private void ctXF_Load(object sender, EventArgs e)
        {
            qry = "";
            prm = 0;

            if (CCRow == null)
            {
                gridView1.OptionsBehavior.Editable = false;

                gridView1.SortInfo.ClearAndAddRange(new[] {
                    new GridColumnSortInfo(colPW, DevExpress.Data.ColumnSortOrder.Descending),
                    new GridColumnSortInfo(colKF, DevExpress.Data.ColumnSortOrder.Descending)
                });

                //colCC.GroupIndex = 0; Sasiriyor
            }
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
            cTGridControl.DataSource = null;
            dataSetGnl.CC.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.CTFill(qry, prm); }).Wait();
            toolStripStatusLabel1.Text = res;
            cTGridControl.DataSource = cTBindingSource;

            gridView1.BestFitColumns();
        }

        private void cTBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private DialogResult UpdateDB()
        {
            if (!Validate())
                return DialogResult.Cancel;
            cTBindingSource.EndEdit();

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
                    string err = dataSetGnl.CTUpdate();
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
            gridView1.SetFocusedRowCellValue(colK1, 0);
            gridView1.SetFocusedRowCellValue(colK2, 0);
        }

        private void playersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ctpXF frm = new ctpXF();
            frm.CTRow = (DataSetGnl.CTRow)gridView1.GetFocusedDataRow();
            frm.Text = $"{gridView1.GetFocusedRowCellValue(colAd)} Players [ctpXF]";
            frm.ShowDialog();
        }
    }
}