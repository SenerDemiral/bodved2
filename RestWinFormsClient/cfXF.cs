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
    public partial class cfXF : DevExpress.XtraEditors.XtraForm
    {
        public DataSetGnl.CCRow CCRow = null;
        private string qry;
        private ulong prm;

        public cfXF()
        {
            InitializeComponent();

            cFGridControl.ExternalRepository = Program.MF.persistentRepository;
            colCC.ColumnEdit = Program.MF.CCrepositoryItemLookUpEdit;
            colPP.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;
        }

        private void cfXF_Load(object sender, EventArgs e)
        {
            qry = "";
            prm = 0;

            if (CCRow == null)
            {
                gridView1.OptionsBehavior.Editable = false;
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
            cFGridControl.DataSource = null;
            dataSetGnl.CC.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.CFFill(qry, prm); }).Wait();
            toolStripStatusLabel1.Text = res;
            cFGridControl.DataSource = cFBindingSource;

            gridView1.BestFitColumns();
        }

        private void cFBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private DialogResult UpdateDB()
        {
            if (!Validate())
                return DialogResult.Cancel;
            cFBindingSource.EndEdit();

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
                    string err = dataSetGnl.CFUpdate();
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
            gridView1.SetFocusedRowCellValue(colPP, 0);

        }

    }
}