namespace RestWinFormsClient
{
    partial class ddXF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ddXF));
            this.dataSetGnl = new RestWinFormsClient.DataSetGnl();
            this.ddBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dDBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.dDBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.ddGridControl = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cETToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pPRDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRowKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDnm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDBindingNavigator)).BeginInit();
            this.dDBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddGridControl)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // dataSetGnl
            // 
            this.dataSetGnl.DataSetName = "DataSetGnl";
            this.dataSetGnl.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ddBindingSource
            // 
            this.ddBindingSource.DataMember = "DD";
            this.ddBindingSource.DataSource = this.dataSetGnl;
            // 
            // dDBindingNavigator
            // 
            this.dDBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.dDBindingNavigator.AutoSize = false;
            this.dDBindingNavigator.BindingSource = this.ddBindingSource;
            this.dDBindingNavigator.CountItem = null;
            this.dDBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.dDBindingNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.dDBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.dDBindingNavigatorSaveItem});
            this.dDBindingNavigator.Location = new System.Drawing.Point(12, 12);
            this.dDBindingNavigator.MoveFirstItem = null;
            this.dDBindingNavigator.MoveLastItem = null;
            this.dDBindingNavigator.MoveNextItem = null;
            this.dDBindingNavigator.MovePreviousItem = null;
            this.dDBindingNavigator.Name = "dDBindingNavigator";
            this.dDBindingNavigator.PositionItem = null;
            this.dDBindingNavigator.Size = new System.Drawing.Size(708, 30);
            this.dDBindingNavigator.TabIndex = 0;
            this.dDBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorAddNewItem.Text = "Add new";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorDeleteItem.Text = "Delete";
            // 
            // dDBindingNavigatorSaveItem
            // 
            this.dDBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.dDBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("dDBindingNavigatorSaveItem.Image")));
            this.dDBindingNavigatorSaveItem.Name = "dDBindingNavigatorSaveItem";
            this.dDBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 27);
            this.dDBindingNavigatorSaveItem.Text = "Save Data";
            this.dDBindingNavigatorSaveItem.Click += new System.EventHandler(this.ddBindingNavigatorSaveItem_Click);
            // 
            // ddGridControl
            // 
            this.ddGridControl.ContextMenuStrip = this.contextMenuStrip;
            this.ddGridControl.DataSource = this.ddBindingSource;
            this.ddGridControl.Location = new System.Drawing.Point(12, 46);
            this.ddGridControl.MainView = this.gridView1;
            this.ddGridControl.Name = "ddGridControl";
            this.ddGridControl.Size = new System.Drawing.Size(708, 233);
            this.ddGridControl.TabIndex = 1;
            this.ddGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cCToolStripMenuItem,
            this.cETToolStripMenuItem,
            this.pPRDToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(104, 70);
            // 
            // cCToolStripMenuItem
            // 
            this.cCToolStripMenuItem.Name = "cCToolStripMenuItem";
            this.cCToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.cCToolStripMenuItem.Text = "CC";
            this.cCToolStripMenuItem.Click += new System.EventHandler(this.cCToolStripMenuItem_Click);
            // 
            // cETToolStripMenuItem
            // 
            this.cETToolStripMenuItem.Name = "cETToolStripMenuItem";
            this.cETToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.cETToolStripMenuItem.Text = "CET";
            this.cETToolStripMenuItem.Click += new System.EventHandler(this.cETToolStripMenuItem_Click);
            // 
            // pPRDToolStripMenuItem
            // 
            this.pPRDToolStripMenuItem.Name = "pPRDToolStripMenuItem";
            this.pPRDToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.pPRDToolStripMenuItem.Text = "PPRD";
            this.pPRDToolStripMenuItem.Click += new System.EventHandler(this.pPRDToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRowKey,
            this.colDnm,
            this.colAd,
            this.colInfo});
            this.gridView1.GridControl = this.ddGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDnm, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            // 
            // colRowKey
            // 
            this.colRowKey.FieldName = "RowKey";
            this.colRowKey.Name = "colRowKey";
            this.colRowKey.OptionsColumn.AllowEdit = false;
            this.colRowKey.OptionsColumn.AllowFocus = false;
            this.colRowKey.OptionsColumn.FixedWidth = true;
            this.colRowKey.Visible = true;
            this.colRowKey.VisibleIndex = 0;
            // 
            // colDnm
            // 
            this.colDnm.FieldName = "Dnm";
            this.colDnm.Name = "colDnm";
            this.colDnm.OptionsColumn.FixedWidth = true;
            this.colDnm.Visible = true;
            this.colDnm.VisibleIndex = 1;
            // 
            // colAd
            // 
            this.colAd.FieldName = "Ad";
            this.colAd.Name = "colAd";
            this.colAd.Visible = true;
            this.colAd.VisibleIndex = 2;
            // 
            // colInfo
            // 
            this.colInfo.FieldName = "Info";
            this.colInfo.Name = "colInfo";
            this.colInfo.Visible = true;
            this.colInfo.VisibleIndex = 3;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.statusStrip1);
            this.layoutControl1.Controls.Add(this.dDBindingNavigator);
            this.layoutControl1.Controls.Add(this.ddGridControl);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(732, 315);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(12, 283);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(708, 20);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 15);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(732, 315);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ddGridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(712, 237);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dDBindingNavigator;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(712, 34);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.statusStrip1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 271);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(712, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // ddXF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 315);
            this.Controls.Add(this.layoutControl1);
            this.Name = "ddXF";
            this.Text = "ddXF";
            this.Load += new System.EventHandler(this.ddXF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDBindingNavigator)).EndInit();
            this.dDBindingNavigator.ResumeLayout(false);
            this.dDBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddGridControl)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataSetGnl dataSetGnl;
        private System.Windows.Forms.BindingSource ddBindingSource;
        private System.Windows.Forms.BindingNavigator dDBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton dDBindingNavigatorSaveItem;
        private DevExpress.XtraGrid.GridControl ddGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colRowKey;
        private DevExpress.XtraGrid.Columns.GridColumn colDnm;
        private DevExpress.XtraGrid.Columns.GridColumn colAd;
        private DevExpress.XtraGrid.Columns.GridColumn colInfo;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem cCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cETToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pPRDToolStripMenuItem;
    }
}