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
            colHPP1.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;
            colHPP2.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;
            colGPP1.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;
            colGPP2.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;

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
                new GridColumnSortInfo(colIdx, DevExpress.Data.ColumnSortOrder.Ascending)
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

                colHPP1.OptionsColumn.ReadOnly = true;
                colHPP2.OptionsColumn.ReadOnly = true;
                colGPP1.OptionsColumn.ReadOnly = true;
                colGPP2.OptionsColumn.ReadOnly = true;

                colCC.Visible = false;
                gridView1.SortInfo.ClearAndAddRange(new[] {
                new GridColumnSortInfo(colSoD, DevExpress.Data.ColumnSortOrder.Descending),
                new GridColumnSortInfo(colIdx, DevExpress.Data.ColumnSortOrder.Ascending)
                });
            }
            else
            {
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

            DataSetGnl.PPluRow sRow;
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
            dataSetGnl.CC.Rows.Clear();
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

        }

    }
}