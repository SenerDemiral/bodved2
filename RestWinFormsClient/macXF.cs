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
    public partial class macXF : DevExpress.XtraEditors.XtraForm
    {
        public DataSetGnl.PPRow PPRow = null;
        public DataSetGnl.CCRow CCRow = null;
        public DataSetGnl.CETRow CETRow = null;
        public DataSetGnl.CEFRow CEFRow = null;
        private ulong CC, CEB, HPP, GPP;
        private DateTime Trh;
        private string qry;
        private ulong prm;

        public macXF()
        {
            InitializeComponent();

            mACGridControl.ExternalRepository = Program.MF.persistentRepository;
            colCC.ColumnEdit = Program.MF.CCrepositoryItemLookUpEdit;
            colTrh.ColumnEdit = Program.MF.TRHrepositoryItemDateEdit;
            colSoD.ColumnEdit = Program.MF.SoDrepositoryItemImageComboBox;
            colDrm.ColumnEdit = Program.MF.DRMrepositoryItemImageComboBox;
        }

        private void macXF_Load(object sender, EventArgs e)
        {
            qry = "";
            prm = 0;

            if (CETRow != null)     // Takim
            {
                qry = "CET";
                prm = CETRow.RowKey;
                CC = CETRow.CC;
                CEB = CETRow.RowKey;
                Trh = CETRow.Trh;
                HPP = 0;
                GPP = 0;

                colCC.Visible = false;
                gridView1.SortInfo.ClearAndAddRange(new[] {
                new GridColumnSortInfo(colSoD, DevExpress.Data.ColumnSortOrder.Descending),
                new GridColumnSortInfo(colIdx, DevExpress.Data.ColumnSortOrder.Descending)
                });
            }
            else if (CEFRow != null)    // Ferdi
            {
                qry = "CEF";
                prm = CEFRow.RowKey;
                CC = CEFRow.CC;
                CEB = CEFRow.RowKey;
                Trh = CEFRow.Trh;
                HPP = CEFRow.HPP;
                GPP = CEFRow.GPP;

                colSoD.OptionsColumn.ReadOnly = true;
                colHPP1.OptionsColumn.ReadOnly = true;
                colHPP2.OptionsColumn.ReadOnly = true;
                colGPP1.OptionsColumn.ReadOnly = true;
                colGPP2.OptionsColumn.ReadOnly = true;

                colCC.Visible = false;
                gridView1.SortInfo.ClearAndAddRange(new[] {
                new GridColumnSortInfo(colSoD, DevExpress.Data.ColumnSortOrder.Descending),
                new GridColumnSortInfo(colIdx, DevExpress.Data.ColumnSortOrder.Descending)
                });
            }
            else
            {
                colHPP1.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;
                colHPP2.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;
                colGPP1.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;
                colGPP2.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;

                gridView1.OptionsBehavior.Editable = false;
                gridView1.ClearSorting();
                if (CCRow != null)
                {
                    qry = "CC";
                    prm = CCRow.RowKey;
                    colCC.Visible = false;
                    colTrh.GroupIndex = 0;
                }
                else if (PPRow != null)
                {
                    qry = "PP";
                    prm = PPRow.RowKey;
                    colTrh.GroupIndex = 0;
                }
            }

            FillDB();

            PrepareLookups();

            gridView1.FocusedRowHandle = 0;
        }

        private void PrepareLookups()
        {
            if (CETRow == null)
                return;

            DataSetGnl.hPPluRow hRow;
            DataSetGnl.gPPluRow gRow;
            DataSetGnl.PPluRow sRow;

            DataSetGnl.PPluRow[] pps = (DataSetGnl.PPluRow[])Program.MF.dataSetGnl.PPlu.Select($"CTs LIKE '*<{CETRow.HCT}>*'");
            foreach (var pp in pps)
            {
                hRow = dataSetGnl.hPPlu.NewhPPluRow();
                hRow.RowKey = pp.RowKey;
                hRow.Ad = pp.Ad;
                hRow.Sex = pp.Sex;
                hRow.CTs = pp.CTs;
                hRow.IsRun = pp.IsRun;

                dataSetGnl.hPPlu.Rows.Add(hRow);
            }
            pps = (DataSetGnl.PPluRow[])Program.MF.dataSetGnl.PPlu.Select($"CTs LIKE '*<{CETRow.GCT}>*'");
            foreach (var pp in pps)
            {
                gRow = dataSetGnl.gPPlu.NewgPPluRow();
                gRow.RowKey = pp.RowKey;
                gRow.Ad = pp.Ad;
                gRow.Sex = pp.Sex;
                gRow.CTs = pp.CTs;
                gRow.IsRun = pp.IsRun;

                dataSetGnl.gPPlu.Rows.Add(gRow);
            }

            // Diskalifiye 584 ekle
            sRow = (DataSetGnl.PPluRow)Program.MF.dataSetGnl.PPlu.Rows.Find(584);

            hRow = dataSetGnl.hPPlu.NewhPPluRow();
            hRow.RowKey = sRow.RowKey;
            hRow.Ad = sRow.Ad;
            hRow.Sex = sRow.Sex;
            hRow.CTs = sRow.CTs;
            hRow.IsRun = true;
            dataSetGnl.hPPlu.Rows.Add(hRow);

            gRow = dataSetGnl.gPPlu.NewgPPluRow();
            gRow.RowKey = sRow.RowKey;
            gRow.Ad = sRow.Ad;
            gRow.Sex = sRow.Sex;
            gRow.CTs = sRow.CTs;
            gRow.IsRun = true;
            dataSetGnl.gPPlu.Rows.Add(gRow);

            foreach (DataSetGnl.MACRow src in dataSetGnl.MAC.Rows)
            {

                if (dataSetGnl.hPPlu.Rows.Find(src.HPP1) == null)
                {
                    sRow = (DataSetGnl.PPluRow)Program.MF.dataSetGnl.PPlu.Rows.Find(src.HPP1);

                    hRow = dataSetGnl.hPPlu.NewhPPluRow();
                    hRow.RowKey = sRow.RowKey;
                    hRow.Ad = "▼ " + sRow.Ad;
                    hRow.Sex = sRow.Sex;
                    hRow.CTs = sRow.CTs;
                    hRow.IsRun = false;

                    dataSetGnl.hPPlu.Rows.Add(hRow);
                }
                if (!src.IsHPP2Null() && src.HPP2 != 0 && dataSetGnl.hPPlu.Rows.Find(src.HPP2) == null)
                {
                    sRow = (DataSetGnl.PPluRow)Program.MF.dataSetGnl.PPlu.Rows.Find(src.HPP2);

                    hRow = dataSetGnl.hPPlu.NewhPPluRow();
                    hRow.RowKey = sRow.RowKey;
                    hRow.Ad = "▼ " + sRow.Ad;
                    hRow.Sex = sRow.Sex;
                    hRow.CTs = sRow.CTs;
                    hRow.IsRun = false;

                    dataSetGnl.hPPlu.Rows.Add(hRow);
                }

                if (dataSetGnl.gPPlu.Rows.Find(src.GPP1) == null)
                {
                    sRow = (DataSetGnl.PPluRow)Program.MF.dataSetGnl.PPlu.Rows.Find(src.GPP1);

                    gRow = dataSetGnl.gPPlu.NewgPPluRow();
                    gRow.RowKey = sRow.RowKey;
                    gRow.Ad = "▼ " + sRow.Ad;
                    gRow.Sex = sRow.Sex;
                    gRow.CTs = sRow.CTs;
                    gRow.IsRun = false;

                    dataSetGnl.gPPlu.Rows.Add(gRow);
                }
                if (!src.IsGPP2Null() && src.GPP2 != 0 && dataSetGnl.gPPlu.Rows.Find(src.GPP2) == null)
                {
                    sRow = (DataSetGnl.PPluRow)Program.MF.dataSetGnl.PPlu.Rows.Find(src.GPP2);

                    gRow = dataSetGnl.gPPlu.NewgPPluRow();
                    gRow.RowKey = sRow.RowKey;
                    gRow.Ad = "▼ " + sRow.Ad;
                    gRow.Sex = sRow.Sex;
                    gRow.CTs = sRow.CTs;
                    gRow.IsRun = false;

                    dataSetGnl.gPPlu.Rows.Add(gRow);
                }
            }
        }

        private void FillDB()
        {
            dataLayoutControl1.DataSource = null;
            dataLayoutControl1.BeginInit();

            string res = "";
            mACGridControl.DataSource = null;
            dataSetGnl.MAC.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.MACFill(qry, prm); }).Wait();
            toolStripStatusLabel1.Text = res;

            mACGridControl.DataSource = mACBindingSource;
            dataLayoutControl1.EndInit();
            dataLayoutControl1.DataSource = mACBindingSource;

            gridView1.BestFitColumns();
        }

        private void macBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gridView1.FocusedColumn == colHPP2 || gridView1.FocusedColumn == colGPP2)
                if (gridView1.GetFocusedRowCellValue(colSoD).ToString() == "S")
                    e.Cancel = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            gridView1.AddNewRow();
            gridView1.AddNewRow();
            gridView1.AddNewRow();
            gridView1.AddNewRow();
            gridView1.AddNewRow();
            gridView1.AddNewRow();
            gridView1.AddNewRow();
            gridView1.AddNewRow();

            gridView1.AddNewRow();
            gridView1.AddNewRow();
            gridView1.AddNewRow();
            gridView1.AddNewRow();
        }

        private void HWTextEdit_Leave(object sender, EventArgs e)
        {
            if (!(sender as TextEdit).IsModified)
            {
                (sender as TextEdit).IsModified = false;
                return;

            }

            int v = 0, c = 0;
            string tag = (sender as TextEdit).Tag.ToString();

            
            switch (tag)
            {
                case "H1":
                    v = (int)H1WTextEdit.EditValue;
                    c = (int)G1WTextEdit.EditValue;
                    break;
                case "H2":
                    v = (int)H2WTextEdit.EditValue;
                    c = (int)G2WTextEdit.EditValue;
                    break;
                case "H3":
                    v = (int)H3WTextEdit.EditValue;
                    c = (int)G3WTextEdit.EditValue;
                    break;
                case "H4":
                    v = (int)H4WTextEdit.EditValue;
                    c = (int)G4WTextEdit.EditValue;
                    break;
                case "H5":
                    v = (int)H5WTextEdit.EditValue;
                    c = (int)G5WTextEdit.EditValue;
                    break;

                case "G1":
                    c = (int)H1WTextEdit.EditValue;
                    v = (int)G1WTextEdit.EditValue;
                    break;
                case "G2":
                    c = (int)H2WTextEdit.EditValue;
                    v = (int)G2WTextEdit.EditValue;
                    break;
                case "G3":
                    c = (int)H3WTextEdit.EditValue;
                    v = (int)G3WTextEdit.EditValue;
                    break;
                case "G4":
                    c = (int)H4WTextEdit.EditValue;
                    v = (int)G4WTextEdit.EditValue;
                    break;
                case "G5":
                    c = (int)H5WTextEdit.EditValue;
                    v = (int)G5WTextEdit.EditValue;
                    break;
                default:
                    break;
            }


            if (v == -1)
            {
                v = 0;
                c = 11;
            }
            else if (v > 0)
            {
                if (v <= 9)
                    c = 11;
                else
                    c = v + 2;
            }
            
            switch (tag)
            {
                case "H1":
                    H1WTextEdit.EditValue = v;
                    G1WTextEdit.EditValue = c;
                    H1WTextEdit.IsModified = false;
                    G1WTextEdit.IsModified = false;
                    break;
                case "H2":
                    H2WTextEdit.EditValue = v;
                    G2WTextEdit.EditValue = c;
                    H2WTextEdit.IsModified = false;
                    G2WTextEdit.IsModified = false;
                    break;
                case "H3":
                    H3WTextEdit.EditValue = v;
                    G3WTextEdit.EditValue = c;
                    H3WTextEdit.IsModified = false;
                    G3WTextEdit.IsModified = false;
                    break;
                case "H4":
                    H4WTextEdit.EditValue = v;
                    G4WTextEdit.EditValue = c;
                    H4WTextEdit.IsModified = false;
                    G4WTextEdit.IsModified = false;
                    break;
                case "H5":
                    H5WTextEdit.EditValue = v;
                    G5WTextEdit.EditValue = c;
                    H5WTextEdit.IsModified = false;
                    G5WTextEdit.IsModified = false;
                    break;

                case "G1":
                    H1WTextEdit.EditValue = c;
                    G1WTextEdit.EditValue = v;
                    H1WTextEdit.IsModified = false;
                    G1WTextEdit.IsModified = false;
                    break;
                case "G2":
                    H2WTextEdit.EditValue = c;
                    G2WTextEdit.EditValue = v;
                    H2WTextEdit.IsModified = false;
                    G2WTextEdit.IsModified = false;
                    break;
                case "G3":
                    H3WTextEdit.EditValue = c;
                    G3WTextEdit.EditValue = v;
                    H3WTextEdit.IsModified = false;
                    G3WTextEdit.IsModified = false;
                    break;
                case "G4":
                    H4WTextEdit.EditValue = c;
                    G4WTextEdit.EditValue = v;
                    H4WTextEdit.IsModified = false;
                    G4WTextEdit.IsModified = false;
                    break;
                case "G5":
                    H5WTextEdit.EditValue = c;
                    G5WTextEdit.EditValue = v;
                    H5WTextEdit.IsModified = false;
                    G5WTextEdit.IsModified = false;
                    break;
                default:
                    break;
            }
        }


        private DialogResult UpdateDB()
        {
            if (!Validate())
                return DialogResult.Cancel;
            mACBindingSource.EndEdit();

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

                    string err = dataSetGnl.MACUpdate();
                    if (err != string.Empty)
                    {
                        MessageBox.Show(err);
                        dr = DialogResult.Abort;
                    }
                }
            }
            return dr;
        }

        private void Diskalifiye()
        {
            //fordataSetGnl.MAC.Rows.Count
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            // mACBindingNavigator.AddNewItem = null; // AddNew kontrol edebilmek icin.
            if (CEFRow != null && gridView1.DataRowCount > 0)   // CEF icin sadece tek maci olabilir.
            {
                return;
            }

            gridView1.AddNewRow();
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridView1.SetFocusedRowCellValue(colRowKey, 0);
            gridView1.SetFocusedRowCellValue(colCC, CC);
            gridView1.SetFocusedRowCellValue(colCEB, CEB);
            gridView1.SetFocusedRowCellValue(colHPP1, HPP);
            gridView1.SetFocusedRowCellValue(colHPP2, 0);
            gridView1.SetFocusedRowCellValue(colGPP1, GPP);
            gridView1.SetFocusedRowCellValue(colGPP2, 0);
            gridView1.SetFocusedRowCellValue(colTrh, Trh);
            gridView1.SetFocusedRowCellValue(colSoD, "S");

            gridView1.SetFocusedRowCellValue(colDrm, "OK");
        }

    }
}