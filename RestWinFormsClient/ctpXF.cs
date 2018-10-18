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
    public partial class ctpXF : DevExpress.XtraEditors.XtraForm
    {
        public DataSetGnl.CTRow CTRow = null;
        private string qry;
        private ulong prm;

        public ctpXF()
        {
            InitializeComponent();

            cTPGridControl.ExternalRepository = Program.MF.persistentRepository;
            colCC.ColumnEdit = Program.MF.CCrepositoryItemLookUpEdit;
            colPP.ColumnEdit = Program.MF.PPrepositoryItemLookUpEdit;
        }

        private void ctpXF_Load(object sender, EventArgs e)
        {
            qry = "";
            prm = 0;

            if (CTRow == null)
            {
                gridView1.OptionsBehavior.Editable = false;

                gridView1.SortInfo.ClearAndAddRange(new[] {
                    new GridColumnSortInfo(colIdx, DevExpress.Data.ColumnSortOrder.Ascending)
                });

                //colCC.GroupIndex = 0; Sasiriyor
            }
            else
            {
                qry = "CT";
                prm = CTRow.RowKey;
                colCC.Visible = false;
                colCT.Visible = false;
            }

            FillDB();
        }

        private void FillDB()
        {
            string res = "";
            cTPGridControl.DataSource = null;
            dataSetGnl.CTP.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.CTPFill(qry, prm); }).Wait();
            toolStripStatusLabel1.Text = res;
            cTPGridControl.DataSource = cTPBindingSource;
        }

        private void cTPBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private DialogResult UpdateDB()
        {
            if (!Validate())
                return DialogResult.Cancel;
            cTPBindingSource.EndEdit();

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
                    string err = dataSetGnl.CTPUpdate();
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
            gridView1.SetFocusedRowCellValue(colCC, CTRow.CC);
            gridView1.SetFocusedRowCellValue(colCT, CTRow.RowKey);
            gridView1.SetFocusedRowCellValue(colPP, 0);
        }

    }
}