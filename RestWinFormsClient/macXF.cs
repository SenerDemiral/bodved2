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
    public partial class macXF : DevExpress.XtraEditors.XtraForm
    {
        public ulong ccNO = 0;
        public ulong cebNO = 0;
        public ulong hctNO = 596, gctNO = 0, ppNO = 0;
        public string qry = "CEB";
        public ulong prm = 1472;

        public macXF()
        {
            InitializeComponent();

            mACGridControl.ExternalRepository = Program.MF.persistentRepository;
            colCC.ColumnEdit = Program.MF.CCrepositoryItemLookUpEdit;
            //colHPP1.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;
            //colHPP2.ColumnEdit = Program.MF.PPrepositoryItemLookUpEdit;
            //colGPP1.ColumnEdit = Program.MF.PPrepositoryItemLookUpEdit;
            //colGPP2.ColumnEdit = Program.MF.PPrepositoryItemLookUpEdit;
        }

        private void macXF_Load(object sender, EventArgs e)
        {
            if (qry == "CEB")
                cebNO = prm;
            if (qry == "PP")
                ppNO = prm;


            FillDB();
            DataSetGnl.hPPluRow hRow;
            DataSetGnl.gPPluRow gRow;

            DataSetGnl.PPluRow[] pps = (DataSetGnl.PPluRow[])Program.MF.dataSetGnl.PPlu.Select($"CTs LIKE '*<{hctNO}>*'");
            foreach(var pp in pps)
            {
                hRow = dataSetGnl.hPPlu.NewhPPluRow();
                hRow.RowKey = pp.RowKey;
                hRow.Ad = pp.Ad;
                hRow.Sex = pp.Sex;
                hRow.Tel = pp.Tel;
                hRow.CTs = pp.CTs;
                hRow.IsRun = pp.IsRun;

                dataSetGnl.hPPlu.Rows.Add(hRow);
            }
            pps = (DataSetGnl.PPluRow[])Program.MF.dataSetGnl.PPlu.Select($"CTs LIKE '*<{gctNO}>*'");
            foreach (var pp in pps)
            {
                gRow = dataSetGnl.gPPlu.NewgPPluRow();
                gRow.RowKey = pp.RowKey;
                gRow.Ad = pp.Ad;
                gRow.Sex = pp.Sex;
                gRow.Tel = pp.Tel;
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
                    hRow.Tel = sRow.Tel;
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
                    hRow.Tel = sRow.Tel;
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
                    gRow.Tel = sRow.Tel;
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
                    gRow.Tel = sRow.Tel;
                    gRow.CTs = sRow.CTs;
                    gRow.IsRun = false;

                    dataSetGnl.gPPlu.Rows.Add(gRow);
                }
            }
            //dataSetGnl.hPPlu.Rows. = Program.MF.dataSetGnl.PPlu.Select($"CTs LIKE '*<{hctNO}>*'").CopyToDataTable();
            //hPPluBindingSource.DataSource = abc;
        }

        private void FillDB()
        {
            string res = "";
            mACGridControl.DataSource = null;
            dataSetGnl.CC.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.MACFill(qry, prm); }).Wait();
            toolStripStatusLabel1.Text = res;
            mACGridControl.DataSource = mACBindingSource;
        }

        private void macBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            UpdateDB();
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
            gridView1.SetFocusedRowCellValue(colCC, ccNO);
            gridView1.SetFocusedRowCellValue(colCEB, cebNO);
            gridView1.SetFocusedRowCellValue(colHPP1, 0);
            gridView1.SetFocusedRowCellValue(colHPP2, 0);
            gridView1.SetFocusedRowCellValue(colGPP1, 0);
            gridView1.SetFocusedRowCellValue(colGPP2, 0);

        }

    }
}