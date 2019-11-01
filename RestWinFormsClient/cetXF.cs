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
using DevExpress.XtraGrid.Views.Base;

namespace RestWinFormsClient
{
    public partial class cetXF : DevExpress.XtraEditors.XtraForm
    {
        public DataSetGnl.CCRow CCRow = null;
        public DataSetGnl.DDRow DDRow = null;
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

            Program.MF.cTBindingSource.RemoveFilter();

        }

        private void cetXF_Load(object sender, EventArgs e)
        {
            qry = "";
            prm = 0;

            if(CCRow != null)
            {
                qry = "CC";
                prm = CCRow.RowKey;

                colCC.Visible = false;
                Program.MF.cTBindingSource.Filter = $"CC = {CCRow.RowKey}";
            }
            else if (DDRow != null)
            {
                qry = "DD";
                prm = (ulong)DDRow.Dnm;

                colCC.Visible = true;
            }
            else
                gridView1.OptionsBehavior.Editable = true; //false;

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
            frm.MdiParent = Program.MF;
            frm.CETRow = (DataSetGnl.CETRow)gridView1.GetFocusedDataRow();
            frm.Text = $"{gridView1.GetFocusedRowCellDisplayText(colHCT)} - {gridView1.GetFocusedRowCellDisplayText(colGCT)} Matches [macXF]";
            frm.HCTAd = gridView1.GetFocusedRowCellDisplayText(colHCT);
            frm.GCTAd = gridView1.GetFocusedRowCellDisplayText(colGCT);
            frm.Show();
        }

        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
            /*
            ColumnView view = (ColumnView)sender;
            if (view.FocusedColumn == colHCT)
            {
                LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
                string countryCode = Convert.ToString(view.GetFocusedRowCellValue("CountryCode"));
                editor.Properties.DataSource = DataContext.GetCitiesByCountryCode(countryCode);
            }*/
        }
    }
}