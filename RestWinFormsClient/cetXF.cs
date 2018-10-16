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
        public cetXF()
        {
            InitializeComponent();

            cETGridControl.ExternalRepository = Program.MF.persistentRepository;
            colCC.ColumnEdit = Program.MF.CCrepositoryItemLookUpEdit;
            colHCT.ColumnEdit = Program.MF.CTrepositoryItemLookUpEdit;
            colGCT.ColumnEdit = Program.MF.CTrepositoryItemLookUpEdit;
        }

        private void cetXF_Load(object sender, EventArgs e)
        {
            FillDB();
        }

        private void FillDB()
        {
            string res = "";
            cETGridControl.DataSource = null;
            dataSetGnl.CC.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.CETFill(); }).Wait();
            toolStripStatusLabel1.Text = res;
            cETGridControl.DataSource = cETBindingSource;
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
            gridView1.SetFocusedRowCellValue(colCC, 0);
            gridView1.SetFocusedRowCellValue(colHCT, 0);
            gridView1.SetFocusedRowCellValue(colGCT, 0);

        }

    }
}