namespace RestWinFormsClient2
{
    partial class MainXF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.persistentRepository = new DevExpress.XtraEditors.Repository.PersistentRepository(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.PPrepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.CCrepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.CTrepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.PPrepositoryItemGridLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRowKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSex = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsRun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCTs = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TRHrepositoryItemDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.SoDrepositoryItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.DRMrepositoryItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.dataSetGnl = new RestWinFormsClient2.DataSetGnl();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PPrepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCrepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CTrepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PPrepositoryItemGridLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TRHrepositoryItemDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TRHrepositoryItemDateEdit.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoDrepositoryItemImageComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DRMrepositoryItemImageComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).BeginInit();
            this.SuspendLayout();
            // 
            // persistentRepository
            // 
            this.persistentRepository.Items.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.PPrepositoryItemLookUpEdit,
            this.CCrepositoryItemLookUpEdit,
            this.CTrepositoryItemLookUpEdit,
            this.PPrepositoryItemGridLookUpEdit,
            this.TRHrepositoryItemDateEdit,
            this.SoDrepositoryItemImageComboBox,
            this.DRMrepositoryItemImageComboBox});
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 239);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // PPrepositoryItemLookUpEdit
            // 
            this.PPrepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PPrepositoryItemLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ad", "Ad", 23, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Sex", "Sex", 28, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Tel", "Tel", 24, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("IsRun", "Is Run", 41, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CTs", "CTs", 28, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RowKey", "Row Key", 65, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far)});
            this.PPrepositoryItemLookUpEdit.DisplayMember = "Ad";
            this.PPrepositoryItemLookUpEdit.Name = "PPrepositoryItemLookUpEdit";
            this.PPrepositoryItemLookUpEdit.ValueMember = "RowKey";
            // 
            // CCrepositoryItemLookUpEdit
            // 
            this.CCrepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CCrepositoryItemLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RowKey", "Row Key", 40, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ad", "Ad", 50, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Skl", "Skl", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Grp", "Grp", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.CCrepositoryItemLookUpEdit.DisplayMember = "Ad";
            this.CCrepositoryItemLookUpEdit.Name = "CCrepositoryItemLookUpEdit";
            this.CCrepositoryItemLookUpEdit.ValueMember = "RowKey";
            // 
            // CTrepositoryItemLookUpEdit
            // 
            this.CTrepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CTrepositoryItemLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RowKey", "Row Key", 65, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ad", "Ad", 23, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.CTrepositoryItemLookUpEdit.DisplayMember = "Ad";
            this.CTrepositoryItemLookUpEdit.Name = "CTrepositoryItemLookUpEdit";
            this.CTrepositoryItemLookUpEdit.ValueMember = "RowKey";
            // 
            // PPrepositoryItemGridLookUpEdit
            // 
            this.PPrepositoryItemGridLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PPrepositoryItemGridLookUpEdit.DisplayMember = "Ad";
            this.PPrepositoryItemGridLookUpEdit.ImmediatePopup = true;
            this.PPrepositoryItemGridLookUpEdit.Name = "PPrepositoryItemGridLookUpEdit";
            this.PPrepositoryItemGridLookUpEdit.NullText = "";
            this.PPrepositoryItemGridLookUpEdit.ValueMember = "RowKey";
            this.PPrepositoryItemGridLookUpEdit.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAd,
            this.colRowKey,
            this.colSex,
            this.colIsRun,
            this.colCTs});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colIsRun, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colAd, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colAd
            // 
            this.colAd.FieldName = "Ad";
            this.colAd.Name = "colAd";
            this.colAd.Visible = true;
            this.colAd.VisibleIndex = 0;
            this.colAd.Width = 434;
            // 
            // colRowKey
            // 
            this.colRowKey.FieldName = "RowKey";
            this.colRowKey.Name = "colRowKey";
            this.colRowKey.Visible = true;
            this.colRowKey.VisibleIndex = 4;
            this.colRowKey.Width = 101;
            // 
            // colSex
            // 
            this.colSex.FieldName = "Sex";
            this.colSex.Name = "colSex";
            this.colSex.Visible = true;
            this.colSex.VisibleIndex = 1;
            this.colSex.Width = 35;
            // 
            // colIsRun
            // 
            this.colIsRun.Caption = "Run";
            this.colIsRun.FieldName = "IsRun";
            this.colIsRun.Name = "colIsRun";
            this.colIsRun.Visible = true;
            this.colIsRun.VisibleIndex = 2;
            this.colIsRun.Width = 48;
            // 
            // colCTs
            // 
            this.colCTs.FieldName = "CTs";
            this.colCTs.Name = "colCTs";
            this.colCTs.Visible = true;
            this.colCTs.VisibleIndex = 3;
            this.colCTs.Width = 314;
            // 
            // TRHrepositoryItemDateEdit
            // 
            this.TRHrepositoryItemDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TRHrepositoryItemDateEdit.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TRHrepositoryItemDateEdit.DisplayFormat.FormatString = "dd.MM.yy";
            this.TRHrepositoryItemDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.TRHrepositoryItemDateEdit.Mask.EditMask = "g";
            this.TRHrepositoryItemDateEdit.Name = "TRHrepositoryItemDateEdit";
            this.TRHrepositoryItemDateEdit.ShowWeekNumbers = true;
            // 
            // SoDrepositoryItemImageComboBox
            // 
            this.SoDrepositoryItemImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SoDrepositoryItemImageComboBox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("S", "S", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("D", "D", -1)});
            this.SoDrepositoryItemImageComboBox.Name = "SoDrepositoryItemImageComboBox";
            // 
            // DRMrepositoryItemImageComboBox
            // 
            this.DRMrepositoryItemImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DRMrepositoryItemImageComboBox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("OK", "OK", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("hX", "hX", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("gX", "gX", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("hH", "hH", -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("gH", "gH", -1)});
            this.DRMrepositoryItemImageComboBox.Name = "DRMrepositoryItemImageComboBox";
            // 
            // dataSetGnl
            // 
            this.dataSetGnl.DataSetName = "DataSetGnl";
            this.dataSetGnl.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // MainXF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainXF";
            this.Text = "MainXF";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PPrepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCrepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CTrepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PPrepositoryItemGridLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TRHrepositoryItemDateEdit.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TRHrepositoryItemDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoDrepositoryItemImageComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DRMrepositoryItemImageComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.Repository.PersistentRepository persistentRepository;
        public DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit PPrepositoryItemLookUpEdit;
        public DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit CCrepositoryItemLookUpEdit;
        public DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit CTrepositoryItemLookUpEdit;
        public DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit PPrepositoryItemGridLookUpEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colAd;
        private DevExpress.XtraGrid.Columns.GridColumn colRowKey;
        private DevExpress.XtraGrid.Columns.GridColumn colSex;
        private DevExpress.XtraGrid.Columns.GridColumn colIsRun;
        private DevExpress.XtraGrid.Columns.GridColumn colCTs;
        public DevExpress.XtraEditors.Repository.RepositoryItemDateEdit TRHrepositoryItemDateEdit;
        public DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox SoDrepositoryItemImageComboBox;
        public DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox DRMrepositoryItemImageComboBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public DataSetGnl dataSetGnl;
    }
}