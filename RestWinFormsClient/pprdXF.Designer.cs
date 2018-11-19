namespace RestWinFormsClient
{
    partial class pprdXF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pprdXF));
            this.dataSetGnl = new RestWinFormsClient.DataSetGnl();
            this.pPRDBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pPRDBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.pPRDBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.pPRDGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRowKey = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDnm = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRnkIdx = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRnkBas = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTopPX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRnkSon = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSonPX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsFerdi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPPTel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPRDBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPRDBindingNavigator)).BeginInit();
            this.pPRDBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pPRDGridControl)).BeginInit();
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
            // pPRDBindingSource
            // 
            this.pPRDBindingSource.DataMember = "PPRD";
            this.pPRDBindingSource.DataSource = this.dataSetGnl;
            // 
            // pPRDBindingNavigator
            // 
            this.pPRDBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.pPRDBindingNavigator.AutoSize = false;
            this.pPRDBindingNavigator.BindingSource = this.pPRDBindingSource;
            this.pPRDBindingNavigator.CountItem = null;
            this.pPRDBindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.pPRDBindingNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.pPRDBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.pPRDBindingNavigatorSaveItem});
            this.pPRDBindingNavigator.Location = new System.Drawing.Point(12, 12);
            this.pPRDBindingNavigator.MoveFirstItem = null;
            this.pPRDBindingNavigator.MoveLastItem = null;
            this.pPRDBindingNavigator.MoveNextItem = null;
            this.pPRDBindingNavigator.MovePreviousItem = null;
            this.pPRDBindingNavigator.Name = "pPRDBindingNavigator";
            this.pPRDBindingNavigator.PositionItem = null;
            this.pPRDBindingNavigator.Size = new System.Drawing.Size(681, 30);
            this.pPRDBindingNavigator.TabIndex = 0;
            this.pPRDBindingNavigator.Text = "bindingNavigator1";
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
            // pPRDBindingNavigatorSaveItem
            // 
            this.pPRDBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pPRDBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("pPRDBindingNavigatorSaveItem.Image")));
            this.pPRDBindingNavigatorSaveItem.Name = "pPRDBindingNavigatorSaveItem";
            this.pPRDBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 27);
            this.pPRDBindingNavigatorSaveItem.Text = "Save Data";
            this.pPRDBindingNavigatorSaveItem.Click += new System.EventHandler(this.pPRDBindingNavigatorSaveItem_Click);
            // 
            // pPRDGridControl
            // 
            this.pPRDGridControl.DataSource = this.pPRDBindingSource;
            this.pPRDGridControl.Location = new System.Drawing.Point(12, 46);
            this.pPRDGridControl.MainView = this.gridView1;
            this.pPRDGridControl.Name = "pPRDGridControl";
            this.pPRDGridControl.Size = new System.Drawing.Size(681, 252);
            this.pPRDGridControl.TabIndex = 0;
            this.pPRDGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colRowKey,
            this.colPP,
            this.colDnm,
            this.colRnkIdx,
            this.colRnkBas,
            this.colTopPX,
            this.colRnkSon,
            this.colSonPX,
            this.colIsFerdi,
            this.colPPTel});
            this.gridView1.GridControl = this.pPRDGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            this.gridView1.InitNewRow += new DevExpress.XtraGrid.Views.Grid.InitNewRowEventHandler(this.gridView1_InitNewRow);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "#";
            this.gridColumn1.FieldName = "gridColumn1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // colRowKey
            // 
            this.colRowKey.FieldName = "RowKey";
            this.colRowKey.Name = "colRowKey";
            this.colRowKey.OptionsColumn.AllowEdit = false;
            this.colRowKey.OptionsColumn.AllowFocus = false;
            this.colRowKey.OptionsColumn.FixedWidth = true;
            this.colRowKey.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "RowKey", "{0:#}")});
            this.colRowKey.Visible = true;
            this.colRowKey.VisibleIndex = 1;
            this.colRowKey.Width = 60;
            // 
            // colPP
            // 
            this.colPP.FieldName = "PP";
            this.colPP.Name = "colPP";
            this.colPP.Visible = true;
            this.colPP.VisibleIndex = 3;
            this.colPP.Width = 253;
            // 
            // colDnm
            // 
            this.colDnm.FieldName = "Dnm";
            this.colDnm.Name = "colDnm";
            this.colDnm.OptionsColumn.FixedWidth = true;
            this.colDnm.Visible = true;
            this.colDnm.VisibleIndex = 2;
            this.colDnm.Width = 50;
            // 
            // colRnkIdx
            // 
            this.colRnkIdx.FieldName = "RnkIdx";
            this.colRnkIdx.Name = "colRnkIdx";
            this.colRnkIdx.OptionsColumn.AllowEdit = false;
            this.colRnkIdx.OptionsColumn.AllowFocus = false;
            this.colRnkIdx.OptionsColumn.FixedWidth = true;
            this.colRnkIdx.Visible = true;
            this.colRnkIdx.VisibleIndex = 4;
            this.colRnkIdx.Width = 50;
            // 
            // colRnkBas
            // 
            this.colRnkBas.FieldName = "RnkBas";
            this.colRnkBas.Name = "colRnkBas";
            this.colRnkBas.OptionsColumn.FixedWidth = true;
            this.colRnkBas.Visible = true;
            this.colRnkBas.VisibleIndex = 5;
            this.colRnkBas.Width = 50;
            // 
            // colTopPX
            // 
            this.colTopPX.FieldName = "TopPX";
            this.colTopPX.Name = "colTopPX";
            this.colTopPX.OptionsColumn.AllowEdit = false;
            this.colTopPX.OptionsColumn.AllowFocus = false;
            this.colTopPX.OptionsColumn.FixedWidth = true;
            this.colTopPX.Visible = true;
            this.colTopPX.VisibleIndex = 7;
            this.colTopPX.Width = 50;
            // 
            // colRnkSon
            // 
            this.colRnkSon.FieldName = "RnkSon";
            this.colRnkSon.Name = "colRnkSon";
            this.colRnkSon.OptionsColumn.FixedWidth = true;
            this.colRnkSon.Visible = true;
            this.colRnkSon.VisibleIndex = 6;
            this.colRnkSon.Width = 50;
            // 
            // colSonPX
            // 
            this.colSonPX.FieldName = "SonPX";
            this.colSonPX.Name = "colSonPX";
            this.colSonPX.OptionsColumn.AllowEdit = false;
            this.colSonPX.OptionsColumn.AllowFocus = false;
            this.colSonPX.OptionsColumn.FixedWidth = true;
            this.colSonPX.Visible = true;
            this.colSonPX.VisibleIndex = 8;
            this.colSonPX.Width = 50;
            // 
            // colIsFerdi
            // 
            this.colIsFerdi.FieldName = "IsFerdi";
            this.colIsFerdi.Name = "colIsFerdi";
            this.colIsFerdi.OptionsColumn.FixedWidth = true;
            this.colIsFerdi.Visible = true;
            this.colIsFerdi.VisibleIndex = 9;
            this.colIsFerdi.Width = 50;
            // 
            // colPPTel
            // 
            this.colPPTel.FieldName = "PPTel";
            this.colPPTel.Name = "colPPTel";
            this.colPPTel.OptionsColumn.AllowEdit = false;
            this.colPPTel.OptionsColumn.AllowFocus = false;
            this.colPPTel.Visible = true;
            this.colPPTel.VisibleIndex = 10;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.statusStrip1);
            this.layoutControl1.Controls.Add(this.pPRDBindingNavigator);
            this.layoutControl1.Controls.Add(this.pPRDGridControl);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(705, 334);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(12, 302);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(681, 20);
            this.statusStrip1.TabIndex = 3;
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(705, 334);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.pPRDGridControl;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(685, 256);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.pPRDBindingNavigator;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(685, 34);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.statusStrip1;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 290);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(685, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // pprdXF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 334);
            this.Controls.Add(this.layoutControl1);
            this.Name = "pprdXF";
            this.Text = "pprdXF";
            this.Load += new System.EventHandler(this.pprdXF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSetGnl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPRDBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pPRDBindingNavigator)).EndInit();
            this.pPRDBindingNavigator.ResumeLayout(false);
            this.pPRDBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pPRDGridControl)).EndInit();
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
        private System.Windows.Forms.BindingSource pPRDBindingSource;
        private System.Windows.Forms.BindingNavigator pPRDBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton pPRDBindingNavigatorSaveItem;
        private DevExpress.XtraGrid.GridControl pPRDGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colRowKey;
        private DevExpress.XtraGrid.Columns.GridColumn colPP;
        private DevExpress.XtraGrid.Columns.GridColumn colDnm;
        private DevExpress.XtraGrid.Columns.GridColumn colRnkIdx;
        private DevExpress.XtraGrid.Columns.GridColumn colRnkBas;
        private DevExpress.XtraGrid.Columns.GridColumn colTopPX;
        private DevExpress.XtraGrid.Columns.GridColumn colRnkSon;
        private DevExpress.XtraGrid.Columns.GridColumn colSonPX;
        private DevExpress.XtraGrid.Columns.GridColumn colIsFerdi;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colPPTel;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}