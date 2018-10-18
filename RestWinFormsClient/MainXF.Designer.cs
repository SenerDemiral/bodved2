namespace RestWinFormsClient
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
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.CCnavBarItem = new DevExpress.XtraNavBar.NavBarItem();
            this.PPnavBarItem = new DevExpress.XtraNavBar.NavBarItem();
            this.CTnavBarItem = new DevExpress.XtraNavBar.NavBarItem();
            this.CETnavBarItem = new DevExpress.XtraNavBar.NavBarItem();
            this.MACnavBarItem = new DevExpress.XtraNavBar.NavBarItem();
            this.dataSetGnl = new RestWinFormsClient.DataSetGnl();
            this.persistentRepository = new DevExpress.XtraEditors.Repository.PersistentRepository(this.components);
            this.PPrepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.pPluBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CCrepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.cCBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.CTrepositoryItemLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.cTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PPrepositoryItemGridLookUpEdit = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.TRHrepositoryItemDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.pPBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SoDrepositoryItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PPrepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPluBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCrepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cCBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CTrepositoryItemLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PPrepositoryItemGridLookUpEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TRHrepositoryItemDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TRHrepositoryItemDateEdit.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoDrepositoryItemImageComboBox)).BeginInit();
            this.SuspendLayout();
            // 
            // documentManager1
            // 
            this.documentManager1.MdiParent = this;
            this.documentManager1.View = this.tabbedView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // tabbedView1
            // 
            this.tabbedView1.RootContainer.Element = null;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 482);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(788, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("62062155-118b-42c0-a9aa-13e2168cc652");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel1.Size = new System.Drawing.Size(200, 482);
            this.dockPanel1.Text = "Menü";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.navBarControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(191, 455);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.CTnavBarItem,
            this.PPnavBarItem,
            this.CCnavBarItem,
            this.CETnavBarItem,
            this.MACnavBarItem});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 191;
            this.navBarControl1.Size = new System.Drawing.Size(191, 455);
            this.navBarControl1.TabIndex = 1;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "Tanımlar";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.CCnavBarItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.PPnavBarItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.CTnavBarItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.CETnavBarItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.MACnavBarItem)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // CCnavBarItem
            // 
            this.CCnavBarItem.Caption = "Competitions";
            this.CCnavBarItem.Name = "CCnavBarItem";
            this.CCnavBarItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.CCnavBarItem_LinkClicked);
            // 
            // PPnavBarItem
            // 
            this.PPnavBarItem.Caption = "Players";
            this.PPnavBarItem.Name = "PPnavBarItem";
            this.PPnavBarItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.PPnavBarItem_LinkClicked);
            // 
            // CTnavBarItem
            // 
            this.CTnavBarItem.Caption = "Competition Teams";
            this.CTnavBarItem.Name = "CTnavBarItem";
            this.CTnavBarItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.CTnavBarItem_LinkClicked);
            // 
            // CETnavBarItem
            // 
            this.CETnavBarItem.Caption = "CET";
            this.CETnavBarItem.Name = "CETnavBarItem";
            this.CETnavBarItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.CETnavBarItem_LinkClicked);
            // 
            // MACnavBarItem
            // 
            this.MACnavBarItem.Caption = "MAC";
            this.MACnavBarItem.Name = "MACnavBarItem";
            this.MACnavBarItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.MACnavBarItem_LinkClicked);
            // 
            // dataSetGnl
            // 
            this.dataSetGnl.DataSetName = "DataSetGnl";
            this.dataSetGnl.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // persistentRepository
            // 
            this.persistentRepository.Items.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.PPrepositoryItemLookUpEdit,
            this.CCrepositoryItemLookUpEdit,
            this.CTrepositoryItemLookUpEdit,
            this.PPrepositoryItemGridLookUpEdit,
            this.TRHrepositoryItemDateEdit,
            this.SoDrepositoryItemImageComboBox});
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
            this.PPrepositoryItemLookUpEdit.DataSource = this.pPluBindingSource;
            this.PPrepositoryItemLookUpEdit.DisplayMember = "Ad";
            this.PPrepositoryItemLookUpEdit.Name = "PPrepositoryItemLookUpEdit";
            this.PPrepositoryItemLookUpEdit.ValueMember = "RowKey";
            // 
            // pPluBindingSource
            // 
            this.pPluBindingSource.DataMember = "PPlu";
            this.pPluBindingSource.DataSource = this.dataSetGnl;
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
            this.CCrepositoryItemLookUpEdit.DataSource = this.cCBindingSource;
            this.CCrepositoryItemLookUpEdit.DisplayMember = "Ad";
            this.CCrepositoryItemLookUpEdit.Name = "CCrepositoryItemLookUpEdit";
            this.CCrepositoryItemLookUpEdit.ValueMember = "RowKey";
            // 
            // cCBindingSource
            // 
            this.cCBindingSource.DataMember = "CC";
            this.cCBindingSource.DataSource = this.dataSetGnl;
            // 
            // CTrepositoryItemLookUpEdit
            // 
            this.CTrepositoryItemLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CTrepositoryItemLookUpEdit.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RowKey", "Row Key", 65, DevExpress.Utils.FormatType.Numeric, "", true, DevExpress.Utils.HorzAlignment.Far),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Ad", "Ad", 23, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.CTrepositoryItemLookUpEdit.DataSource = this.cTBindingSource;
            this.CTrepositoryItemLookUpEdit.DisplayMember = "Ad";
            this.CTrepositoryItemLookUpEdit.Name = "CTrepositoryItemLookUpEdit";
            this.CTrepositoryItemLookUpEdit.ValueMember = "RowKey";
            // 
            // cTBindingSource
            // 
            this.cTBindingSource.DataMember = "CT";
            this.cTBindingSource.DataSource = this.dataSetGnl;
            // 
            // PPrepositoryItemGridLookUpEdit
            // 
            this.PPrepositoryItemGridLookUpEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PPrepositoryItemGridLookUpEdit.DataSource = this.pPluBindingSource;
            this.PPrepositoryItemGridLookUpEdit.DisplayMember = "Ad";
            this.PPrepositoryItemGridLookUpEdit.Name = "PPrepositoryItemGridLookUpEdit";
            this.PPrepositoryItemGridLookUpEdit.ValueMember = "RowKey";
            this.PPrepositoryItemGridLookUpEdit.View = this.repositoryItemGridLookUpEdit1View;
            this.PPrepositoryItemGridLookUpEdit.QueryCloseUp += new System.ComponentModel.CancelEventHandler(this.PPrepositoryItemGridLookUpEdit_QueryCloseUp);
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
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
            // pPBindingSource
            // 
            this.pPBindingSource.DataMember = "PP";
            this.pPBindingSource.DataSource = this.dataSetGnl;
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
            // MainXF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 504);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.statusStrip1);
            this.IsMdiContainer = true;
            this.Name = "MainXF";
            this.Text = "BodVeD [MainXF]";
            this.Load += new System.EventHandler(this.MainXF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PPrepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPluBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CCrepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cCBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CTrepositoryItemLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PPrepositoryItemGridLookUpEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TRHrepositoryItemDateEdit.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TRHrepositoryItemDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoDrepositoryItemImageComboBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarItem CTnavBarItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevExpress.XtraNavBar.NavBarItem PPnavBarItem;
        public DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit PPrepositoryItemLookUpEdit;
        private System.Windows.Forms.BindingSource pPBindingSource;
        public DevExpress.XtraEditors.Repository.PersistentRepository persistentRepository;
        public DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit CCrepositoryItemLookUpEdit;
        private System.Windows.Forms.BindingSource cCBindingSource;
        public DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit CTrepositoryItemLookUpEdit;
        private System.Windows.Forms.BindingSource cTBindingSource;
        private DevExpress.XtraNavBar.NavBarItem CCnavBarItem;
        private DevExpress.XtraNavBar.NavBarItem CETnavBarItem;
        private DevExpress.XtraNavBar.NavBarItem MACnavBarItem;
        private System.Windows.Forms.BindingSource pPluBindingSource;
        public DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit PPrepositoryItemGridLookUpEdit;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        public DataSetGnl dataSetGnl;
        public DevExpress.XtraEditors.Repository.RepositoryItemDateEdit TRHrepositoryItemDateEdit;
        public DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox SoDrepositoryItemImageComboBox;
    }
}