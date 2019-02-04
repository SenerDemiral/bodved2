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
using DevExpress.XtraPrinting;

namespace RestWinFormsClient
{
    public partial class pprdXF : DevExpress.XtraEditors.XtraForm
    {
        public DataSetGnl.DDRow DDRow = null;
        private string qry = "";
        private ulong prm = 0;

        public pprdXF()
        {
            InitializeComponent();

            pPRDGridControl.ExternalRepository = Program.MF.persistentRepository;
            colPP.ColumnEdit = Program.MF.PPrepositoryItemGridLookUpEdit;
        }

        private void pprdXF_Load(object sender, EventArgs e)
        {
            if (DDRow != null)
            {
                qry = "DD";
                prm = (ulong)DDRow.Dnm;
            }

            FillDB();
        }

        private void FillDB()
        {
            string res = "";
            pPRDGridControl.DataSource = null;
            dataSetGnl.PPRD.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.PPRDFill(qry, prm); }).Wait();
            toolStripStatusLabel1.Text = res;
            pPRDGridControl.DataSource = pPRDBindingSource;

            gridView1.BestFitColumns();
        }

        private void pPRDBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            UpdateDB();
        }

        private DialogResult UpdateDB()
        {
            if (!Validate())
                return DialogResult.Cancel;
            pPRDBindingSource.EndEdit();

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
                    string err = dataSetGnl.PPRDUpdate();
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
            gridView1.SetFocusedRowCellValue(colDnm, prm);

        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if(e.Column == gridColumn1)
            e.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintingSystem ps = new PrintingSystem();
            PrintableComponentLink link = new PrintableComponentLink(ps);
            link.Component = pPRDGridControl;

            link.PaperKind = System.Drawing.Printing.PaperKind.A4;
            link.Landscape = false;
            link.Margins.Left = 50;
            link.Margins.Right = 50;
            link.Margins.Top = 50;
            link.Margins.Bottom = 50;

            var Font = new Font("Tahoma", 12, FontStyle.Bold);

            PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
            string mdlH = string.Format("BODVED OYUNCU LİSTESİ");
            phf.Header.Content.AddRange(new string[] { "", mdlH, "" });
            phf.Header.LineAlignment = BrickAlignment.Far;
            phf.Header.Font = Font;

            phf.Footer.Content.AddRange(new string[] { "[Date Printed] [Time Printed]", "masatenisi.online", "©Şener DEMİRAL" });
            phf.Footer.LineAlignment = BrickAlignment.Near;

            link.CreateDocument();
            link.ShowPreview();


        }
    }
}