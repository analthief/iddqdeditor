namespace Sharp
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.tmiFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiFileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tssSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmiFileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_FileSaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tmiFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tssSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tmiFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tssSep6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAll = new System.Windows.Forms.ToolStripButton();
            this.btnLoad = new System.Windows.Forms.ToolStripButton();
            this.tssSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.tssSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSelect = new System.Windows.Forms.ToolStripButton();
            this.tsCross = new System.Windows.Forms.ToolStripButton();
            this.tsLine = new System.Windows.Forms.ToolStripButton();
            this.btnAbout = new System.Windows.Forms.ToolStripButton();
            this.tsCircle = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.gbPreview = new System.Windows.Forms.GroupBox();
            this.pbPlus = new System.Windows.Forms.PictureBox();
            this.pbMinus = new System.Windows.Forms.PictureBox();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.tcSheets = new System.Windows.Forms.TabControl();
            this.gbShapesLst = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lvShapes = new System.Windows.Forms.ListView();
            this.clhType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tsMain.SuspendLayout();
            this.gbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.gbShapesLst.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFile,
            this.tssSep6,
            this.btnSave,
            this.btnSaveAll,
            this.btnLoad,
            this.tssSep3,
            this.btnRefresh,
            this.btnClear,
            this.tssSep4,
            this.tsSelect,
            this.tsCross,
            this.tsLine,
            this.btnAbout,
            this.tsCircle});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(846, 31);
            this.tsMain.TabIndex = 10;
            // 
            // tsFile
            // 
            this.tsFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmiFileNew,
            this.tmiFileClose,
            this.tssSep1,
            this.tmiFileLoad,
            this.tmiFileSave,
            this.tmi_FileSaveAll,
            this.tmiFileSaveAs,
            this.tssSep2,
            this.tmiFileExit});
            this.tsFile.Image = ((System.Drawing.Image)(resources.GetObject("tsFile.Image")));
            this.tsFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFile.Name = "tsFile";
            this.tsFile.ShowDropDownArrow = false;
            this.tsFile.Size = new System.Drawing.Size(29, 28);
            this.tsFile.Text = "&File";
            // 
            // tmiFileNew
            // 
            this.tmiFileNew.Name = "tmiFileNew";
            this.tmiFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tmiFileNew.Size = new System.Drawing.Size(187, 22);
            this.tmiFileNew.Text = "&New";
            this.tmiFileNew.Click += new System.EventHandler(this.tmiFileNew_Click);
            // 
            // tmiFileClose
            // 
            this.tmiFileClose.Name = "tmiFileClose";
            this.tmiFileClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.tmiFileClose.Size = new System.Drawing.Size(187, 22);
            this.tmiFileClose.Text = "&Close";
            this.tmiFileClose.Click += new System.EventHandler(this.tmiFileClose_Click);
            // 
            // tssSep1
            // 
            this.tssSep1.Name = "tssSep1";
            this.tssSep1.Size = new System.Drawing.Size(184, 6);
            // 
            // tmiFileLoad
            // 
            this.tmiFileLoad.Name = "tmiFileLoad";
            this.tmiFileLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tmiFileLoad.Size = new System.Drawing.Size(187, 22);
            this.tmiFileLoad.Text = "&Open";
            this.tmiFileLoad.Click += new System.EventHandler(this.tmiFileLoad_Click);
            // 
            // tmiFileSave
            // 
            this.tmiFileSave.Name = "tmiFileSave";
            this.tmiFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tmiFileSave.Size = new System.Drawing.Size(187, 22);
            this.tmiFileSave.Text = "&Save";
            this.tmiFileSave.Click += new System.EventHandler(this.tmiFileSave_Click);
            // 
            // tmi_FileSaveAll
            // 
            this.tmi_FileSaveAll.Name = "tmi_FileSaveAll";
            this.tmi_FileSaveAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.tmi_FileSaveAll.Size = new System.Drawing.Size(187, 22);
            this.tmi_FileSaveAll.Text = "Save A&ll";
            this.tmi_FileSaveAll.Click += new System.EventHandler(this.tmi_FileSaveAll_Click);
            // 
            // tmiFileSaveAs
            // 
            this.tmiFileSaveAs.Name = "tmiFileSaveAs";
            this.tmiFileSaveAs.Size = new System.Drawing.Size(187, 22);
            this.tmiFileSaveAs.Text = "Save &As...";
            this.tmiFileSaveAs.Click += new System.EventHandler(this.tmiFileSaveAs_Click);
            // 
            // tssSep2
            // 
            this.tssSep2.Name = "tssSep2";
            this.tssSep2.Size = new System.Drawing.Size(184, 6);
            // 
            // tmiFileExit
            // 
            this.tmiFileExit.Name = "tmiFileExit";
            this.tmiFileExit.Size = new System.Drawing.Size(187, 22);
            this.tmiFileExit.Text = "&Exit";
            this.tmiFileExit.Click += new System.EventHandler(this.tmiFileExit_Click);
            // 
            // tssSep6
            // 
            this.tssSep6.Name = "tssSep6";
            this.tssSep6.Size = new System.Drawing.Size(6, 31);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::Sharp.Properties.Resources.img_save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(28, 28);
            this.btnSave.Text = "Save figure to file";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAll.Image = global::Sharp.Properties.Resources.save_all_3770;
            this.btnSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(28, 28);
            this.btnSaveAll.Text = "Save All";
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoad.Image = global::Sharp.Properties.Resources.img_open;
            this.btnLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(28, 28);
            this.btnLoad.Text = "Load figure from file";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // tssSep3
            // 
            this.tssSep3.Name = "tssSep3";
            this.tssSep3.Size = new System.Drawing.Size(6, 31);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Sharp.Properties.Resources.img_refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(28, 28);
            this.btnRefresh.Text = "Refresh work area";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClear
            // 
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClear.Image = global::Sharp.Properties.Resources.img_clear;
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(28, 28);
            this.btnClear.Text = "Clear figure";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tssSep4
            // 
            this.tssSep4.Name = "tssSep4";
            this.tssSep4.Size = new System.Drawing.Size(6, 31);
            // 
            // tsSelect
            // 
            this.tsSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSelect.Image = global::Sharp.Properties.Resources.select_cur;
            this.tsSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSelect.Name = "tsSelect";
            this.tsSelect.Size = new System.Drawing.Size(28, 28);
            this.tsSelect.Text = "Select figure";
            this.tsSelect.Click += new System.EventHandler(this.tsSelect_Click);
            // 
            // tsCross
            // 
            this.tsCross.Checked = true;
            this.tsCross.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsCross.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsCross.Image = global::Sharp.Properties.Resources.gtk_close_7736;
            this.tsCross.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCross.Name = "tsCross";
            this.tsCross.Size = new System.Drawing.Size(28, 28);
            this.tsCross.Text = "Cross";
            this.tsCross.Click += new System.EventHandler(this.tsCross_Click);
            // 
            // tsLine
            // 
            this.tsLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsLine.Image = global::Sharp.Properties.Resources.stock_draw_line_3200;
            this.tsLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsLine.Name = "tsLine";
            this.tsLine.Size = new System.Drawing.Size(28, 28);
            this.tsLine.Text = "Line";
            this.tsLine.Click += new System.EventHandler(this.tsLine_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAbout.Image = global::Sharp.Properties.Resources.img_about;
            this.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(28, 28);
            this.btnAbout.Text = "toolStripButton1";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // tsCircle
            // 
            this.tsCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsCircle.Image = global::Sharp.Properties.Resources.stock_draw_circle_unfilled_5329;
            this.tsCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCircle.Name = "tsCircle";
            this.tsCircle.Size = new System.Drawing.Size(28, 28);
            this.tsCircle.Text = "Circle";
            this.tsCircle.Click += new System.EventHandler(this.tsCircle_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 665);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(846, 22);
            this.statusStrip.TabIndex = 11;
            this.statusStrip.Text = "statusStrip1";
            // 
            // gbPreview
            // 
            this.gbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPreview.Controls.Add(this.pbPlus);
            this.gbPreview.Controls.Add(this.pbMinus);
            this.gbPreview.Controls.Add(this.pbPreview);
            this.gbPreview.Location = new System.Drawing.Point(624, 34);
            this.gbPreview.Name = "gbPreview";
            this.gbPreview.Size = new System.Drawing.Size(216, 261);
            this.gbPreview.TabIndex = 13;
            this.gbPreview.TabStop = false;
            this.gbPreview.Text = "Preview";
            // 
            // pbPlus
            // 
            this.pbPlus.Image = global::Sharp.Properties.Resources.img_plus;
            this.pbPlus.Location = new System.Drawing.Point(184, 221);
            this.pbPlus.Name = "pbPlus";
            this.pbPlus.Size = new System.Drawing.Size(24, 24);
            this.pbPlus.TabIndex = 4;
            this.pbPlus.TabStop = false;
            this.pbPlus.Click += new System.EventHandler(this.pbPlus_Click);
            // 
            // pbMinus
            // 
            this.pbMinus.Image = global::Sharp.Properties.Resources.img_minus;
            this.pbMinus.Location = new System.Drawing.Point(154, 221);
            this.pbMinus.Name = "pbMinus";
            this.pbMinus.Size = new System.Drawing.Size(24, 24);
            this.pbMinus.TabIndex = 3;
            this.pbMinus.TabStop = false;
            this.pbMinus.Click += new System.EventHandler(this.pbMinus_Click);
            // 
            // pbPreview
            // 
            this.pbPreview.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPreview.Location = new System.Drawing.Point(8, 15);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(200, 200);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPreview.TabIndex = 0;
            this.pbPreview.TabStop = false;
            this.pbPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbPreview_MouseClick);
            // 
            // dlgOpen
            // 
            this.dlgOpen.DefaultExt = "*.shp";
            this.dlgOpen.Filter = "Shape files (*.shp)|*.shp|All files (*.*)|*.*";
            this.dlgOpen.Title = "Select file to open";
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "*.shp";
            this.dlgSave.Filter = "Shape files (*.shp)|*.shp|BMP (*.bmp)|*.bmp|All files (*.*)|*.*";
            this.dlgSave.Title = "Saving figure...";
            // 
            // tcSheets
            // 
            this.tcSheets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcSheets.Location = new System.Drawing.Point(0, 34);
            this.tcSheets.Name = "tcSheets";
            this.tcSheets.SelectedIndex = 0;
            this.tcSheets.Size = new System.Drawing.Size(612, 630);
            this.tcSheets.TabIndex = 0;
            this.tcSheets.SelectedIndexChanged += new System.EventHandler(this.tcSheets_SelectedIndexChanged);
            // 
            // gbShapesLst
            // 
            this.gbShapesLst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbShapesLst.Controls.Add(this.btnDelete);
            this.gbShapesLst.Controls.Add(this.lvShapes);
            this.gbShapesLst.Location = new System.Drawing.Point(624, 301);
            this.gbShapesLst.Name = "gbShapesLst";
            this.gbShapesLst.Size = new System.Drawing.Size(216, 361);
            this.gbShapesLst.TabIndex = 14;
            this.gbShapesLst.TabStop = false;
            this.gbShapesLst.Text = "Shapes";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(133, 322);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lvShapes
            // 
            this.lvShapes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clhType});
            this.lvShapes.Location = new System.Drawing.Point(8, 19);
            this.lvShapes.Name = "lvShapes";
            this.lvShapes.Size = new System.Drawing.Size(200, 297);
            this.lvShapes.TabIndex = 0;
            this.lvShapes.UseCompatibleStateImageBehavior = false;
            this.lvShapes.View = System.Windows.Forms.View.Details;
            this.lvShapes.SelectedIndexChanged += new System.EventHandler(this.lvShapes_SelectedIndexChanged);
            // 
            // clhType
            // 
            this.clhType.Text = "Figure";
            this.clhType.Width = 184;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 687);
            this.Controls.Add(this.gbShapesLst);
            this.Controls.Add(this.tcSheets);
            this.Controls.Add(this.gbPreview);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tsMain);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IDDQD Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.gbPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPlus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMinus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.gbShapesLst.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnLoad;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripSeparator tssSep3;
        private System.Windows.Forms.ToolStripSeparator tssSep4;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.GroupBox gbPreview;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.ToolStripDropDownButton tsFile;
        private System.Windows.Forms.ToolStripMenuItem tmiFileNew;
        private System.Windows.Forms.ToolStripSeparator tssSep1;
        private System.Windows.Forms.ToolStripMenuItem tmiFileLoad;
        private System.Windows.Forms.ToolStripMenuItem tmiFileSave;
        private System.Windows.Forms.ToolStripMenuItem tmiFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator tssSep2;
        private System.Windows.Forms.ToolStripMenuItem tmiFileExit;
        private System.Windows.Forms.ToolStripSeparator tssSep6;
        private System.Windows.Forms.PictureBox pbMinus;
        private System.Windows.Forms.PictureBox pbPlus;
        private System.Windows.Forms.ToolStripButton btnAbout;
        private System.Windows.Forms.ToolStripButton tsCross;
        private System.Windows.Forms.ToolStripButton tsLine;
        private System.Windows.Forms.ToolStripButton tsCircle;
        private System.Windows.Forms.TabControl tcSheets;
        private System.Windows.Forms.ToolStripMenuItem tmiFileClose;
        private System.Windows.Forms.ToolStripButton btnSaveAll;
        private System.Windows.Forms.ToolStripMenuItem tmi_FileSaveAll;
        private System.Windows.Forms.GroupBox gbShapesLst;
        private System.Windows.Forms.ListView lvShapes;
        private System.Windows.Forms.ColumnHeader clhType;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ToolStripButton tsSelect;
    }
}

