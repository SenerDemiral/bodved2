namespace RestWinFormsClient
{
    partial class ccXF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ccXF));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.cCBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.cCBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetGnl = new RestWinFormsClient.DataSetGnl();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cCBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.cCGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRowKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSkl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGrp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsRun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsRnkd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTNSM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTNDM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTNSS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTNDS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTSMK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTDMK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTEGP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTEMP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTEBP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTEXP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cCBindingNavigator)).BeginInit();
            this.cCBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cCBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cCGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cCBindingNavigator);
            this.layoutControl1.Controls.Add(this.cCGridControl);
            this.layoutControl1.Controls.Add(this.statusStrip1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(322, 199, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1198, 396);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // cCBindingNavigator
            // 
            this.cCBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.cCBindingNavigator.AutoSize = false;
            this.cCBindingNavigator.BindingSource = this.cCBindingSource;
            this.cCBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.cCBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.cCBindingNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.cCBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.cCBindingNavigatorSaveItem});
            this.cCBindingNavigator.Location = new System.Drawing.Point(12, 12);
            this.cCBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.cCBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.cCBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.cCBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.cCBindingNavigator.Name = "cCBindingNavigator";
            this.cCBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.cCBindingNavigator.Size = new System.Drawing.Size(1174, 30);
            this.cCBindingNavigator.TabIndex = 1;
            this.cCBindingNavigator.Text = "bindingNavigator1";
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
            // cCBindingSource
            // 
            this.cCBindingSource.DataMember = "CC";
            this.cCBindingSource.DataSource = this.dataSetGnl;
            // 
            // dataSetGnl
            // 
            this.dataSetGnl.DataSetName = "DataSetGnl";
            this.dataSetGnl.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 27);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
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
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 30);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 27);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // cCBindingNavigatorSaveItem
            // 
            this.cCBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cCBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("cCBindingNavigatorSaveItem.Image")));
            this.cCBindingNavigatorSaveItem.Name = "cCBindingNavigatorSaveItem";
            this.cCBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 27);
            this.cCBindingNavigatorSaveItem.Text = "Save Data";
            this.cCBindingNavigatorSaveItem.Click += new System.EventHandler(this.cCBindingNavigatorSaveItem_Click);
            // 
            // cCGridControl
            // 
            this.cCGridControl.DataSource = this.cCBindingSource;
            this.cCGridControl.Location = new System.Drawing.Point(12, 46);
            this.cCGridControl.MainView = this.gridView1;
            this.cCGridControl.Name = "cCGridControl";
            this.cCGridControl.Size = new System.Drawing.Size(1174, 314);
            this.cCGridControl.TabIndex = 5;
            this.cCGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRowKey,
            this.colSkl,
            this.colGrp,
            this.colAd,
            this.colInfo,
            this.colIsRun,
            this.colIsRnkd,
            this.colTNSM,
            this.colTNDM,
            this.colTNSS,
            this.colTNDS,
            this.colTSMK,
            this.colTDMK,
            this.colTEGP,
            this.colTEMP,
            this.colTEBP,
            this.colTEXP});
            this.gridView1.GridControl = this.cCGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            // 
            // colRowKey
            // 
            this.colRowKey.FieldName = "RowKey";
            this.colRowKey.Name = "colRowKey";
            this.colRowKey.Visible = true;
            this.colRowKey.VisibleIndex = 0;
            // 
            // colSkl
            // 
            this.colSkl.FieldName = "Skl";
            this.colSkl.Name = "colSkl";
            this.colSkl.Visible = true;
            this.colSkl.VisibleIndex = 1;
            // 
            // colGrp
            // 
            this.colGrp.FieldName = "Grp";
            this.colGrp.Name = "colGrp";
            this.colGrp.Visible = true;
            this.colGrp.VisibleIndex = 2;
            // 
            // colAd
            // 
            this.colAd.FieldName = "Ad";
            this.colAd.Name = "colAd";
            this.colAd.Visible = true;
            this.colAd.VisibleIndex = 3;
            // 
            // colInfo
            // 
            this.colInfo.FieldName = "Info";
            this.colInfo.Name = "colInfo";
            this.colInfo.Visible = true;
            this.colInfo.VisibleIndex = 4;
            // 
            // colIsRun
            // 
            this.colIsRun.FieldName = "IsRun";
            this.colIsRun.Name = "colIsRun";
            this.colIsRun.Visible = true;
            this.colIsRun.VisibleIndex = 5;
            // 
            // colIsRnkd
            // 
            this.colIsRnkd.FieldName = "IsRnkd";
            this.colIsRnkd.Name = "colIsRnkd";
            this.colIsRnkd.Visible = true;
            this.colIsRnkd.VisibleIndex = 6;
            // 
            // colTNSM
            // 
            this.colTNSM.FieldName = "TNSM";
            this.colTNSM.Name = "colTNSM";
            this.colTNSM.Visible = true;
            this.colTNSM.VisibleIndex = 7;
            // 
            // colTNDM
            // 
            this.colTNDM.FieldName = "TNDM";
            this.colTNDM.Name = "colTNDM";
            this.colTNDM.Visible = true;
            this.colTNDM.VisibleIndex = 8;
            // 
            // colTNSS
            // 
            this.colTNSS.FieldName = "TNSS";
            this.colTNSS.Name = "colTNSS";
            this.colTNSS.Visible = true;
            this.colTNSS.VisibleIndex = 9;
            // 
            // colTNDS
            // 
            this.colTNDS.FieldName = "TNDS";
            this.colTNDS.Name = "colTNDS";
            this.colTNDS.Visible = true;
            this.colTNDS.VisibleIndex = 10;
            // 
            // colTSMK
            // 
            this.colTSMK.FieldName = "TSMK";
            this.colTSMK.Name = "colTSMK";
            this.colTSMK.Visible = true;
            this.colTSMK.VisibleIndex = 11;
            // 
            // colTDMK
            // 
            this.colTDMK.FieldName = "TDMK";
            this.colTDMK.Name = "colTDMK";
            this.colTDMK.Visible = true;
            this.colTDMK.VisibleIndex = 12;
            // 
            // colTEGP
            // 
            this.colTEGP.FieldName = "TEGP";
            this.colTEGP.Name = "colTEGP";
            this.colTEGP.Visible = true;
            this.colTEGP.VisibleIndex = 13;
            // 
            // colTEMP
            // 
            this.colTEMP.FieldName = "TEMP";
            this.colTEMP.Name = "colTEMP";
            this.colTEMP.Visible = true;
            this.colTEMP.VisibleIndex = 14;
            // 
            // colTEBP
            // 
            this.colTEBP.FieldName = "TEBP";
            this.colTEBP.Name = "colTEBP";
            this.colTEBP.Visible = true;
            this.colTEBP.VisibleIndex = 15;
            // 
            // colTEXP
            // 
            this.colTEXP.FieldName = "TEXP";
            this.colTEXP.Name = "colTEXP";
            this.colTEXP.Visible = true;
            this.colTEXP.VisibleIndex = 16;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(12, 364);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1174, 20);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 15);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(1198, 396);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.statusStrip1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 352);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1178, 24);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.cCBindingNavigator;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1178, 34);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cCGridControl;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(1178, 318);
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // ccXF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 396);
            this.Controls.Add(this.layoutControl1);
            this.Name = "ccXF";
            this.Text = "ccXF";
            this.Load += new System.EventHandler(this.ccXF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cCBindingNavigator)).EndInit();
            this.cCBindingNavigator.ResumeLayout(false);
            this.cCBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cCBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cCGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.BindingNavigator cCBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.BindingSource cCBindingSource;
        private DataSetGnl dataSetGnl;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton cCBindingNavigatorSaveItem;
        private DevExpress.XtraGrid.GridControl cCGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colRowKey;
        private DevExpress.XtraGrid.Columns.GridColumn colSkl;
        private DevExpress.XtraGrid.Columns.GridColumn colGrp;
        private DevExpress.XtraGrid.Columns.GridColumn colAd;
        private DevExpress.XtraGrid.Columns.GridColumn colInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colIsRun;
        private DevExpress.XtraGrid.Columns.GridColumn colIsRnkd;
        private DevExpress.XtraGrid.Columns.GridColumn colTNSM;
        private DevExpress.XtraGrid.Columns.GridColumn colTNDM;
        private DevExpress.XtraGrid.Columns.GridColumn colTNSS;
        private DevExpress.XtraGrid.Columns.GridColumn colTNDS;
        private DevExpress.XtraGrid.Columns.GridColumn colTSMK;
        private DevExpress.XtraGrid.Columns.GridColumn colTDMK;
        private DevExpress.XtraGrid.Columns.GridColumn colTEGP;
        private DevExpress.XtraGrid.Columns.GridColumn colTEMP;
        private DevExpress.XtraGrid.Columns.GridColumn colTEBP;
        private DevExpress.XtraGrid.Columns.GridColumn colTEXP;
    }
}