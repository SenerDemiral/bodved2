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
    public partial class ctXF : DevExpress.XtraEditors.XtraForm
    {
        public ctXF()
        {
            InitializeComponent();
        }
        private void ctXF_Load(object sender, EventArgs e)
        {
            FillDB();
        }

        private void FillDB()
        {
            string res = "";
            cTGridControl.DataSource = null;
            dataSetGnl.CC.Rows.Clear();
            Task.Run(async () => { res = await dataSetGnl.CTFill(); }).Wait();
            toolStripStatusLabel1.Text = res;
            cTGridControl.DataSource = cTBindingSource;
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

    }
}