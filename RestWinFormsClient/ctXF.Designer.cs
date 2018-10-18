namespace RestWinFormsClient
{
    partial class ctXF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctXF));
            this.dataSetGnl = new RestWinFormsClient.DataSetGnl();
            this.cTBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cTBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
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
            this.cTBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.cTGridControl = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRowKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAdres = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsRun = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colK1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colK2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSMW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSML = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDMW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDML = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKF = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEB = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPW = new DevExpress.XtraGrid.Columns.GridColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cTBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cTBindingNavigator)).BeginInit();
            this.cTBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cTGridControl)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
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
            // cTBindingSource
            // 
            this.cTBindingSource.DataMember = "CT";
            this.cTBindingSource.DataSource = this.dataSetGnl;
            // 
            // cTBindingNavigator
            // 
            this.cTBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.cTBindingNavigator.AutoSize = false;
            this.cTBindingNavigator.BindingSource = this.cTBindingSource;
            this.cTBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.cTBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.cTBindingNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.cTBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.cTBindingNavigatorSaveItem});
            this.cTBindingNavigator.Location = new System.Drawing.Point(12, 12);
            this.cTBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.cTBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.cTBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.cTBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.cTBindingNavigator.Name = "cTBindingNavigator";
            this.cTBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.cTBindingNavigator.Size = new System.Drawing.Size(967, 30);
            this.cTBindingNavigator.TabIndex = 0;
            this.cTBindingNavigator.Text = "bindingNavigator1";
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
            // cTBindingNavigatorSaveItem
            // 
            this.cTBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cTBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("cTBindingNavigatorSaveItem.Image")));
            this.cTBindingNavigatorSaveItem.Name = "cTBindingNavigatorSaveItem";
            this.cTBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 27);
            this.cTBindingNavigatorSaveItem.Text = "Save Data";
            this.cTBindingNavigatorSaveItem.Click += new System.EventHandler(this.cTBindingNavigatorSaveItem_Click);
            // 
            // cTGridControl
            // 
            this.cTGridControl.ContextMenuStrip = this.contextMenuStrip;
            this.cTGridControl.DataSource = this.cTBindingSource;
            this.cTGridControl.Location = new System.Drawing.Point(12, 46);
            this.cTGridControl.MainView = this.gridView1;
            this.cTGridControl.Name = "cTGridControl";
            this.cTGridControl.Size = new System.Drawing.Size(967, 346);
            this.cTGridControl.TabIndex = 1;
            this.cTGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playersToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(112, 26);
            // 
            // playersToolStripMenuItem
            // 
            this.playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            this.playersToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.playersToolStripMenuItem.Text = "Players";
            this.playersToolStripMenuItem.Click += new System.EventHandler(this.playersToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRowKey,
            this.colAd,
            this.colAdres,
            this.colInfo,
            this.colIsRun,
            this.colCC,
            this.colK1,
            this.colK2,
            this.colSMW,
            this.colSML,
            this.colDMW,
            this.colDML,
            this.colKW,
            this.colKL,
            this.colKF,
            this.colEW,
            this.colEL,
            this.colEB,
            this.colEX,
            this.colPW});
            this.gridView1.GridControl = this.cTGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.gridView1.OptionsView.AutoCalcPreviewLineCount = true;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.PreviewFieldName = "Info";
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
            this.colRowKey.VisibleIndex = 1;
            this.colRowKey.Width = 60;
            // 
            // colAd
            // 
            this.colAd.FieldName = "Ad";
            this.colAd.Name = "colAd";
            this.colAd.Visible = true;
            this.colAd.VisibleIndex = 2;
            this.colAd.Width = 58;
            // 
            // colAdres
            // 
            this.colAdres.FieldName = "Adres";
            this.colAdres.Name = "colAdres";
            this.colAdres.Visible = true;
            this.colAdres.VisibleIndex = 3;
            this.colAdres.Width = 58;
            // 
            // colInfo
            // 
            this.colInfo.FieldName = "Info";
            this.colInfo.Name = "colInfo";
            this.colInfo.Visible = true;
            this.colInfo.VisibleIndex = 4;
            this.colInfo.Width = 58;
            // 
            // colIsRun
            // 
            this.colIsRun.FieldName = "IsRun";
            this.colIsRun.Name = "colIsRun";
            this.colIsRun.OptionsColumn.FixedWidth = true;
            this.colIsRun.Visible = true;
            this.colIsRun.VisibleIndex = 5;
            this.colIsRun.Width = 36;
            // 
            // colCC
            // 
            this.colCC.FieldName = "CC";
            this.colCC.Name = "colCC";
            this.colCC.OptionsColumn.AllowEdit = false;
            this.colCC.OptionsColumn.AllowFocus = false;
            this.colCC.Visible = true;
            this.colCC.VisibleIndex = 0;
            this.colCC.Width = 63;
            // 
            // colK1
            // 
            this.colK1.FieldName = "K1";
            this.colK1.Name = "colK1";
            this.colK1.Visible = true;
            this.colK1.VisibleIndex = 6;
            this.colK1.Width = 85;
            // 
            // colK2
            // 
            this.colK2.FieldName = "K2";
            this.colK2.Name = "colK2";
            this.colK2.Visible = true;
            this.colK2.VisibleIndex = 7;
            this.colK2.Width = 91;
            // 
            // colSMW
            // 
            this.colSMW.AppearanceCell.Options.UseTextOptions = true;
            this.colSMW.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSMW.FieldName = "SMW";
            this.colSMW.Name = "colSMW";
            this.colSMW.OptionsColumn.FixedWidth = true;
            this.colSMW.Visible = true;
            this.colSMW.VisibleIndex = 16;
            this.colSMW.Width = 40;
            // 
            // colSML
            // 
            this.colSML.AppearanceCell.Options.UseTextOptions = true;
            this.colSML.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSML.FieldName = "SML";
            this.colSML.Name = "colSML";
            this.colSML.OptionsColumn.FixedWidth = true;
            this.colSML.Visible = true;
            this.colSML.VisibleIndex = 17;
            this.colSML.Width = 40;
            // 
            // colDMW
            // 
            this.colDMW.AppearanceCell.Options.UseTextOptions = true;
            this.colDMW.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDMW.FieldName = "DMW";
            this.colDMW.Name = "colDMW";
            this.colDMW.OptionsColumn.FixedWidth = true;
            this.colDMW.Visible = true;
            this.colDMW.VisibleIndex = 18;
            this.colDMW.Width = 40;
            // 
            // colDML
            // 
            this.colDML.AppearanceCell.Options.UseTextOptions = true;
            this.colDML.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDML.FieldName = "DML";
            this.colDML.Name = "colDML";
            this.colDML.OptionsColumn.FixedWidth = true;
            this.colDML.Visible = true;
            this.colDML.VisibleIndex = 19;
            this.colDML.Width = 40;
            // 
            // colKW
            // 
            this.colKW.AppearanceCell.Options.UseTextOptions = true;
            this.colKW.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKW.FieldName = "KW";
            this.colKW.Name = "colKW";
            this.colKW.OptionsColumn.FixedWidth = true;
            this.colKW.Visible = true;
            this.colKW.VisibleIndex = 10;
            this.colKW.Width = 40;
            // 
            // colKL
            // 
            this.colKL.AppearanceCell.Options.UseTextOptions = true;
            this.colKL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKL.FieldName = "KL";
            this.colKL.Name = "colKL";
            this.colKL.OptionsColumn.FixedWidth = true;
            this.colKL.Visible = true;
            this.colKL.VisibleIndex = 11;
            this.colKL.Width = 40;
            // 
            // colKF
            // 
            this.colKF.AppearanceCell.ForeColor = System.Drawing.Color.Red;
            this.colKF.AppearanceCell.Options.UseForeColor = true;
            this.colKF.AppearanceCell.Options.UseTextOptions = true;
            this.colKF.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colKF.FieldName = "KF";
            this.colKF.Name = "colKF";
            this.colKF.OptionsColumn.FixedWidth = true;
            this.colKF.Visible = true;
            this.colKF.VisibleIndex = 9;
            this.colKF.Width = 40;
            // 
            // colEW
            // 
            this.colEW.AppearanceCell.Options.UseTextOptions = true;
            this.colEW.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEW.FieldName = "EW";
            this.colEW.Name = "colEW";
            this.colEW.OptionsColumn.FixedWidth = true;
            this.colEW.Visible = true;
            this.colEW.VisibleIndex = 12;
            this.colEW.Width = 30;
            // 
            // colEL
            // 
            this.colEL.AppearanceCell.Options.UseTextOptions = true;
            this.colEL.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEL.FieldName = "EL";
            this.colEL.Name = "colEL";
            this.colEL.OptionsColumn.FixedWidth = true;
            this.colEL.Visible = true;
            this.colEL.VisibleIndex = 13;
            this.colEL.Width = 30;
            // 
            // colEB
            // 
            this.colEB.AppearanceCell.Options.UseTextOptions = true;
            this.colEB.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEB.FieldName = "EB";
            this.colEB.Name = "colEB";
            this.colEB.OptionsColumn.FixedWidth = true;
            this.colEB.Visible = true;
            this.colEB.VisibleIndex = 14;
            this.colEB.Width = 30;
            // 
            // colEX
            // 
            this.colEX.AppearanceCell.Options.UseTextOptions = true;
            this.colEX.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colEX.FieldName = "EX";
            this.colEX.Name = "colEX";
            this.colEX.OptionsColumn.FixedWidth = true;
            this.colEX.Visible = true;
            this.colEX.VisibleIndex = 15;
            this.colEX.Width = 30;
            // 
            // colPW
            // 
            this.colPW.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colPW.AppearanceCell.ForeColor = System.Drawing.Color.White;
            this.colPW.AppearanceCell.Options.UseBackColor = true;
            this.colPW.AppearanceCell.Options.UseForeColor = true;
            this.colPW.AppearanceCell.Options.UseTextOptions = true;
            this.colPW.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPW.FieldName = "PW";
            this.colPW.Name = "colPW";
            this.colPW.OptionsColumn.FixedWidth = true;
            this.colPW.Visible = true;
            this.colPW.VisibleIndex = 8;
            this.colPW.Width = 40;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(12, 396);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(967, 20);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 15);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.statusStrip1);
            this.layoutControl1.Controls.Add(this.cTBindingNavigator);
            this.layoutControl1.Controls.Add(this.cTGridControl);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(991, 428);
            this.layoutControl1.TabIndex = 3;
            this.layoutControl1.Text = "layoutControl1";
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(991, 428);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cTGridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(971, 350);
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.cTBindingNavigator;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(971, 34);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.statusStrip1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 384);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(971, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // ctXF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 428);
            this.Controls.Add(this.layoutControl1);
            this.Name = "ctXF";
            this.Text = "ctXF";
            this.Load += new System.EventHandler(this.ctXF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cTBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cTBindingNavigator)).EndInit();
            this.cTBindingNavigator.ResumeLayout(false);
            this.cTBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cTGridControl)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataSetGnl dataSetGnl;
        private System.Windows.Forms.BindingSource cTBindingSource;
        private System.Windows.Forms.BindingNavigator cTBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
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
        private System.Windows.Forms.ToolStripButton cTBindingNavigatorSaveItem;
        private DevExpress.XtraGrid.GridControl cTGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colRowKey;
        private DevExpress.XtraGrid.Columns.GridColumn colAd;
        private DevExpress.XtraGrid.Columns.GridColumn colAdres;
        private DevExpress.XtraGrid.Columns.GridColumn colInfo;
        private DevExpress.XtraGrid.Columns.GridColumn colIsRun;
        private DevExpress.XtraGrid.Columns.GridColumn colCC;
        private DevExpress.XtraGrid.Columns.GridColumn colK1;
        private DevExpress.XtraGrid.Columns.GridColumn colK2;
        private DevExpress.XtraGrid.Columns.GridColumn colSMW;
        private DevExpress.XtraGrid.Columns.GridColumn colSML;
        private DevExpress.XtraGrid.Columns.GridColumn colDMW;
        private DevExpress.XtraGrid.Columns.GridColumn colDML;
        private DevExpress.XtraGrid.Columns.GridColumn colKW;
        private DevExpress.XtraGrid.Columns.GridColumn colKL;
        private DevExpress.XtraGrid.Columns.GridColumn colEW;
        private DevExpress.XtraGrid.Columns.GridColumn colEL;
        private DevExpress.XtraGrid.Columns.GridColumn colEB;
        private DevExpress.XtraGrid.Columns.GridColumn colEX;
        private DevExpress.XtraGrid.Columns.GridColumn colPW;
        private DevExpress.XtraGrid.Columns.GridColumn colKF;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem playersToolStripMenuItem;
    }
}