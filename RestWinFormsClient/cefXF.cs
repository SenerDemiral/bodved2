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
    public partial class cefXF : DevExpress.XtraEditors.XtraForm
    {
        public DataSetGnl.CCRow CCRow = null;
        private string qry;
        private ulong prm;

        public cefXF()
        {
            InitializeComponent();

            cEFGridControl.ExternalRepository = Program.MF.persistentRepository;
            colCC.ColumnEdit = Program.MF.CCrepositoryItemLookUpEdit;
            colDrm.ColumnEdit = Program.MF.DRMrepositoryItemImageComboBox;
            //colHPP.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;
            //colGPP.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;

            //colPP.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;
        }

        private void cefXF_Load(object sender, EventArgs e)
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
            //Program.MF.PPrepositoryItemGridLookUpEdit.View.ActiveFilterString = $"[CC] = {prm}";
            PrepareLookups();
            FillDB();
        }

        private void PrepareLookups()
        {
            if (CCRow == null)
                return;

            Task.Run(async () => { await dataSetGnl.CFFill(qry, prm); }).Wait();

            DataSetGnl.cfPPluRow cfRow;
            DataSetGnl.PPluRow ppRow;

            foreach (DataSetGnl.CFRow cf in dataSetGnl.CF.Rows)
            {
                cfRow = dataSetGnl.cfPPlu.NewcfPPluRow();
                ppRow = Program.MF.dataSetGnl.PPlu.FindByRowKey(cf.PP);
                cfRow.RowKey = cf.PP;
                cfRow.Ad = ppRow.Ad;
                cfRow.Sex = ppRow.Sex;
                cfRow.IsRun = ppRow.IsRun;

                dataSetGnl.cfPPlu.Rows.Add(cfRow);
            }
            
        }

        private void FillDB()
        {
            string res = "";
            cEFGridControl.DataSource = null;
            dataSetGnl.CC.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.CEFFill(qry, prm); }).Wait();
            toolStripStatusLabel1.Text = res;
            cEFGridControl.DataSource = cEFBindingSource;

            gridView1.BestFitColumns();
        }

        private void cEFBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private DialogResult UpdateDB()
        {
            if (!Validate())
                return DialogResult.Cancel;
            cEFBindingSource.EndEdit();

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
                    string err = dataSetGnl.CEFUpdate();
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
            gridView1.SetFocusedRowCellValue(colHPP, 0);
            gridView1.SetFocusedRowCellValue(colGPP, 0);

        }

        private void cEFGridControl_Click(object sender, EventArgs e)
        {

        }

        private void maclarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            macXF frm = new macXF();
            frm.CEFRow = (DataSetGnl.CEFRow)gridView1.GetFocusedDataRow();
            frm.Text = $"{gridView1.GetFocusedRowCellDisplayText(colHPP)} >< {gridView1.GetFocusedRowCellDisplayText(colGPP)} Matches [macXF]";
            frm.ShowDialog();

        }
    }
}