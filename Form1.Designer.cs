namespace WinMoss
{
    partial class WinMoss
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbCommands = new System.Windows.Forms.GroupBox();
            this.btnClearOutput = new System.Windows.Forms.Button();
            this.btnClearQuery = new System.Windows.Forms.Button();
            this.btnSaveOutput = new System.Windows.Forms.Button();
            this.btnSaveQuery = new System.Windows.Forms.Button();
            this.btnRunMoss = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pgMoss = new System.Windows.Forms.PropertyGrid();
            this.gbCodeFiles = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.studentInstructions = new System.Windows.Forms.Label();
            this.lbStudentFiles = new System.Windows.Forms.ListBox();
            this.contextMenuStripCodeFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baseInstructions = new System.Windows.Forms.Label();
            this.lbBaseFiles = new System.Windows.Forms.ListBox();
            this.gbFindFiles = new System.Windows.Forms.GroupBox();
            this.cbtnAutoRun = new System.Windows.Forms.CheckBox();
            this.btnAddBaseFiles = new System.Windows.Forms.Button();
            this.btnClearBaseFiles = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ignFilter = new System.Windows.Forms.TextBox();
            this.cbtnDirectories = new System.Windows.Forms.CheckBox();
            this.btnClearStudentFiles = new System.Windows.Forms.Button();
            this.labFilter = new System.Windows.Forms.Label();
            this.btnRemoveFiles = new System.Windows.Forms.Button();
            this.btnAddStudentFiles = new System.Windows.Forms.Button();
            this.addFilter = new System.Windows.Forms.TextBox();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.rtxtOutput = new System.Windows.Forms.RichTextBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mossScriptToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMossScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mossScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateUserIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbCommands.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbCodeFiles.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStripCodeFiles.SuspendLayout();
            this.gbFindFiles.SuspendLayout();
            this.gbOutput.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.gbCommands, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbCodeFiles, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbFindFiles, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbOutput, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1016, 688);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gbCommands
            // 
            this.gbCommands.Controls.Add(this.btnClearOutput);
            this.gbCommands.Controls.Add(this.btnClearQuery);
            this.gbCommands.Controls.Add(this.btnSaveOutput);
            this.gbCommands.Controls.Add(this.btnSaveQuery);
            this.gbCommands.Controls.Add(this.btnRunMoss);
            this.gbCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCommands.Location = new System.Drawing.Point(714, 429);
            this.gbCommands.Name = "gbCommands";
            this.gbCommands.Size = new System.Drawing.Size(299, 114);
            this.gbCommands.TabIndex = 3;
            this.gbCommands.TabStop = false;
            this.gbCommands.Text = "Commands";
            // 
            // btnClearOutput
            // 
            this.btnClearOutput.Location = new System.Drawing.Point(90, 17);
            this.btnClearOutput.Name = "btnClearOutput";
            this.btnClearOutput.Size = new System.Drawing.Size(75, 39);
            this.btnClearOutput.TabIndex = 4;
            this.btnClearOutput.Text = "Clear Output";
            this.btnClearOutput.UseVisualStyleBackColor = true;
            this.btnClearOutput.Click += new System.EventHandler(this.btnClearOutput_Click);
            // 
            // btnClearQuery
            // 
            this.btnClearQuery.Location = new System.Drawing.Point(91, 67);
            this.btnClearQuery.Name = "btnClearQuery";
            this.btnClearQuery.Size = new System.Drawing.Size(75, 39);
            this.btnClearQuery.TabIndex = 3;
            this.btnClearQuery.Text = "Clear Query";
            this.btnClearQuery.UseVisualStyleBackColor = true;
            this.btnClearQuery.Click += new System.EventHandler(this.btnClearQuery_Click);
            // 
            // btnSaveOutput
            // 
            this.btnSaveOutput.Enabled = false;
            this.btnSaveOutput.Location = new System.Drawing.Point(171, 17);
            this.btnSaveOutput.Name = "btnSaveOutput";
            this.btnSaveOutput.Size = new System.Drawing.Size(75, 39);
            this.btnSaveOutput.TabIndex = 2;
            this.btnSaveOutput.Text = "Save Output";
            this.btnSaveOutput.UseVisualStyleBackColor = true;
            this.btnSaveOutput.Click += new System.EventHandler(this.btnSaveOutput_Click);
            // 
            // btnSaveQuery
            // 
            this.btnSaveQuery.Enabled = false;
            this.btnSaveQuery.Location = new System.Drawing.Point(171, 67);
            this.btnSaveQuery.Name = "btnSaveQuery";
            this.btnSaveQuery.Size = new System.Drawing.Size(75, 39);
            this.btnSaveQuery.TabIndex = 1;
            this.btnSaveQuery.Text = "Save Query";
            this.btnSaveQuery.UseVisualStyleBackColor = true;
            this.btnSaveQuery.Click += new System.EventHandler(this.btnSaveQuery_Click);
            // 
            // btnRunMoss
            // 
            this.btnRunMoss.Location = new System.Drawing.Point(9, 17);
            this.btnRunMoss.Name = "btnRunMoss";
            this.btnRunMoss.Size = new System.Drawing.Size(75, 39);
            this.btnRunMoss.TabIndex = 0;
            this.btnRunMoss.Text = "Run Moss";
            this.btnRunMoss.UseVisualStyleBackColor = true;
            this.btnRunMoss.Click += new System.EventHandler(this.btnRunMossEx_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pgMoss);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(714, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(299, 420);
            this.panel1.TabIndex = 0;
            // 
            // pgMoss
            // 
            this.pgMoss.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgMoss.Location = new System.Drawing.Point(0, 0);
            this.pgMoss.Name = "pgMoss";
            this.pgMoss.Size = new System.Drawing.Size(299, 420);
            this.pgMoss.TabIndex = 0;
            this.pgMoss.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgMoss_PropertyValueChanged);
            // 
            // gbCodeFiles
            // 
            this.gbCodeFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCodeFiles.Controls.Add(this.splitContainer1);
            this.gbCodeFiles.Location = new System.Drawing.Point(3, 3);
            this.gbCodeFiles.Name = "gbCodeFiles";
            this.gbCodeFiles.Size = new System.Drawing.Size(705, 420);
            this.gbCodeFiles.TabIndex = 1;
            this.gbCodeFiles.TabStop = false;
            this.gbCodeFiles.Text = "Code Files";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.studentInstructions);
            this.splitContainer1.Panel1.Controls.Add(this.lbStudentFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.baseInstructions);
            this.splitContainer1.Panel2.Controls.Add(this.lbBaseFiles);
            this.splitContainer1.Size = new System.Drawing.Size(699, 401);
            this.splitContainer1.SplitterDistance = 198;
            this.splitContainer1.TabIndex = 4;
            // 
            // studentInstructions
            // 
            this.studentInstructions.AllowDrop = true;
            this.studentInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.studentInstructions.AutoSize = true;
            this.studentInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studentInstructions.Location = new System.Drawing.Point(263, 87);
            this.studentInstructions.Name = "studentInstructions";
            this.studentInstructions.Size = new System.Drawing.Size(181, 20);
            this.studentInstructions.TabIndex = 1;
            this.studentInstructions.Text = "Drag Student Files Here";
            this.studentInstructions.DragEnter += new System.Windows.Forms.DragEventHandler(this.studentInstructionsDragEnter);
            // 
            // lbStudentFiles
            // 
            this.lbStudentFiles.AllowDrop = true;
            this.lbStudentFiles.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lbStudentFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbStudentFiles.ContextMenuStrip = this.contextMenuStripCodeFiles;
            this.lbStudentFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbStudentFiles.FormattingEnabled = true;
            this.lbStudentFiles.HorizontalScrollbar = true;
            this.lbStudentFiles.Location = new System.Drawing.Point(0, 0);
            this.lbStudentFiles.Name = "lbStudentFiles";
            this.lbStudentFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbStudentFiles.Size = new System.Drawing.Size(699, 198);
            this.lbStudentFiles.TabIndex = 0;
            this.lbStudentFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.Files_DragDrop);
            this.lbStudentFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.Files_DragEnter);
            // 
            // contextMenuStripCodeFiles
            // 
            this.contextMenuStripCodeFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeSelectedToolStripMenuItem});
            this.contextMenuStripCodeFiles.Name = "contextMenuStripCodeFiles";
            this.contextMenuStripCodeFiles.Size = new System.Drawing.Size(165, 26);
            // 
            // removeSelectedToolStripMenuItem
            // 
            this.removeSelectedToolStripMenuItem.Name = "removeSelectedToolStripMenuItem";
            this.removeSelectedToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.removeSelectedToolStripMenuItem.Text = "Remove Selected";
            this.removeSelectedToolStripMenuItem.Click += new System.EventHandler(this.btnRemoveFiles_Click);
            // 
            // baseInstructions
            // 
            this.baseInstructions.AllowDrop = true;
            this.baseInstructions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.baseInstructions.AutoSize = true;
            this.baseInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.baseInstructions.Location = new System.Drawing.Point(274, 91);
            this.baseInstructions.Name = "baseInstructions";
            this.baseInstructions.Size = new System.Drawing.Size(161, 20);
            this.baseInstructions.TabIndex = 2;
            this.baseInstructions.Text = "Drag Base Files Here";
            this.baseInstructions.DragEnter += new System.Windows.Forms.DragEventHandler(this.baseInstructionsDragEnter);
            // 
            // lbBaseFiles
            // 
            this.lbBaseFiles.AllowDrop = true;
            this.lbBaseFiles.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lbBaseFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbBaseFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbBaseFiles.FormattingEnabled = true;
            this.lbBaseFiles.Location = new System.Drawing.Point(0, 0);
            this.lbBaseFiles.Name = "lbBaseFiles";
            this.lbBaseFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbBaseFiles.Size = new System.Drawing.Size(699, 199);
            this.lbBaseFiles.TabIndex = 2;
            this.lbBaseFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.Files_DragDrop);
            this.lbBaseFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.Files_DragEnter);
            // 
            // gbFindFiles
            // 
            this.gbFindFiles.Controls.Add(this.cbtnAutoRun);
            this.gbFindFiles.Controls.Add(this.btnAddBaseFiles);
            this.gbFindFiles.Controls.Add(this.btnClearBaseFiles);
            this.gbFindFiles.Controls.Add(this.label1);
            this.gbFindFiles.Controls.Add(this.ignFilter);
            this.gbFindFiles.Controls.Add(this.cbtnDirectories);
            this.gbFindFiles.Controls.Add(this.btnClearStudentFiles);
            this.gbFindFiles.Controls.Add(this.labFilter);
            this.gbFindFiles.Controls.Add(this.btnRemoveFiles);
            this.gbFindFiles.Controls.Add(this.btnAddStudentFiles);
            this.gbFindFiles.Controls.Add(this.addFilter);
            this.gbFindFiles.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbFindFiles.Location = new System.Drawing.Point(3, 429);
            this.gbFindFiles.Name = "gbFindFiles";
            this.gbFindFiles.Size = new System.Drawing.Size(705, 114);
            this.gbFindFiles.TabIndex = 2;
            this.gbFindFiles.TabStop = false;
            this.gbFindFiles.Text = "Find Files";
            // 
            // cbtnAutoRun
            // 
            this.cbtnAutoRun.AutoSize = true;
            this.cbtnAutoRun.Location = new System.Drawing.Point(114, 79);
            this.cbtnAutoRun.Name = "cbtnAutoRun";
            this.cbtnAutoRun.Size = new System.Drawing.Size(151, 17);
            this.cbtnAutoRun.TabIndex = 10;
            this.cbtnAutoRun.Text = "AutoRun After Files Added";
            this.cbtnAutoRun.UseVisualStyleBackColor = true;
            // 
            // btnAddBaseFiles
            // 
            this.btnAddBaseFiles.Location = new System.Drawing.Point(434, 67);
            this.btnAddBaseFiles.Name = "btnAddBaseFiles";
            this.btnAddBaseFiles.Size = new System.Drawing.Size(75, 39);
            this.btnAddBaseFiles.TabIndex = 9;
            this.btnAddBaseFiles.Text = "Add Base Files";
            this.btnAddBaseFiles.UseVisualStyleBackColor = true;
            this.btnAddBaseFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // btnClearBaseFiles
            // 
            this.btnClearBaseFiles.Location = new System.Drawing.Point(621, 67);
            this.btnClearBaseFiles.Name = "btnClearBaseFiles";
            this.btnClearBaseFiles.Size = new System.Drawing.Size(78, 39);
            this.btnClearBaseFiles.TabIndex = 8;
            this.btnClearBaseFiles.Text = "Clear Base Files";
            this.btnClearBaseFiles.UseVisualStyleBackColor = true;
            this.btnClearBaseFiles.Click += new System.EventHandler(this.btnClearBaseFiles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ignore Filter:";
            // 
            // ignFilter
            // 
            this.ignFilter.Location = new System.Drawing.Point(77, 41);
            this.ignFilter.Name = "ignFilter";
            this.ignFilter.Size = new System.Drawing.Size(330, 20);
            this.ignFilter.TabIndex = 6;
            // 
            // cbtnDirectories
            // 
            this.cbtnDirectories.AutoSize = true;
            this.cbtnDirectories.Location = new System.Drawing.Point(9, 79);
            this.cbtnDirectories.Name = "cbtnDirectories";
            this.cbtnDirectories.Size = new System.Drawing.Size(98, 17);
            this.cbtnDirectories.TabIndex = 3;
            this.cbtnDirectories.Text = "Use Directories";
            this.cbtnDirectories.UseVisualStyleBackColor = true;
            this.cbtnDirectories.CheckedChanged += new System.EventHandler(this.cbtnDirectories_Changed);
            // 
            // btnClearStudentFiles
            // 
            this.btnClearStudentFiles.Location = new System.Drawing.Point(621, 17);
            this.btnClearStudentFiles.Name = "btnClearStudentFiles";
            this.btnClearStudentFiles.Size = new System.Drawing.Size(78, 39);
            this.btnClearStudentFiles.TabIndex = 5;
            this.btnClearStudentFiles.Text = "Clear Student Files";
            this.btnClearStudentFiles.UseVisualStyleBackColor = true;
            this.btnClearStudentFiles.Click += new System.EventHandler(this.btnClearStudentFiles_Click);
            // 
            // labFilter
            // 
            this.labFilter.AutoSize = true;
            this.labFilter.Location = new System.Drawing.Point(6, 21);
            this.labFilter.Name = "labFilter";
            this.labFilter.Size = new System.Drawing.Size(54, 13);
            this.labFilter.TabIndex = 4;
            this.labFilter.Text = "Add Filter:";
            // 
            // btnRemoveFiles
            // 
            this.btnRemoveFiles.Location = new System.Drawing.Point(515, 18);
            this.btnRemoveFiles.Name = "btnRemoveFiles";
            this.btnRemoveFiles.Size = new System.Drawing.Size(100, 39);
            this.btnRemoveFiles.TabIndex = 2;
            this.btnRemoveFiles.Text = "Remove Selected Student Files";
            this.btnRemoveFiles.UseVisualStyleBackColor = true;
            this.btnRemoveFiles.Click += new System.EventHandler(this.btnRemoveFiles_Click);
            // 
            // btnAddStudentFiles
            // 
            this.btnAddStudentFiles.Location = new System.Drawing.Point(434, 17);
            this.btnAddStudentFiles.Name = "btnAddStudentFiles";
            this.btnAddStudentFiles.Size = new System.Drawing.Size(75, 39);
            this.btnAddStudentFiles.TabIndex = 1;
            this.btnAddStudentFiles.Text = "Add Student Files";
            this.btnAddStudentFiles.UseVisualStyleBackColor = true;
            this.btnAddStudentFiles.Click += new System.EventHandler(this.btnAddFiles_Click);
            // 
            // addFilter
            // 
            this.addFilter.Location = new System.Drawing.Point(77, 18);
            this.addFilter.Name = "addFilter";
            this.addFilter.Size = new System.Drawing.Size(330, 20);
            this.addFilter.TabIndex = 0;
            // 
            // gbOutput
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.gbOutput, 3);
            this.gbOutput.Controls.Add(this.rtxtOutput);
            this.gbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOutput.Location = new System.Drawing.Point(3, 549);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Size = new System.Drawing.Size(1010, 136);
            this.gbOutput.TabIndex = 4;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "Output";
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.BackColor = System.Drawing.SystemColors.Control;
            this.rtxtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtOutput.Location = new System.Drawing.Point(3, 16);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Size = new System.Drawing.Size(1004, 117);
            this.rtxtOutput.TabIndex = 0;
            this.rtxtOutput.Text = "";
            this.rtxtOutput.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtxtOutput_LinkClicked);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.AutoScroll = true;
            this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1016, 688);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1016, 734);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip);
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 0);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1016, 22);
            this.statusStrip.TabIndex = 0;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mossScriptToolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1016, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mossScriptToolStripMenuItem1
            // 
            this.mossScriptToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateMossScriptToolStripMenuItem});
            this.mossScriptToolStripMenuItem1.Name = "mossScriptToolStripMenuItem1";
            this.mossScriptToolStripMenuItem1.Size = new System.Drawing.Size(80, 20);
            this.mossScriptToolStripMenuItem1.Text = "Moss Script";
            // 
            // updateMossScriptToolStripMenuItem
            // 
            this.updateMossScriptToolStripMenuItem.Name = "updateMossScriptToolStripMenuItem";
            this.updateMossScriptToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.updateMossScriptToolStripMenuItem.Text = "Update User ID";
            this.updateMossScriptToolStripMenuItem.Click += new System.EventHandler(this.updateMossScriptToolStripMenuItem_Click);
            // 
            // mossScriptToolStripMenuItem
            // 
            this.mossScriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateUserIDToolStripMenuItem});
            this.mossScriptToolStripMenuItem.Name = "mossScriptToolStripMenuItem";
            this.mossScriptToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.mossScriptToolStripMenuItem.Text = "Moss Script";
            // 
            // updateUserIDToolStripMenuItem
            // 
            this.updateUserIDToolStripMenuItem.Name = "updateUserIDToolStripMenuItem";
            this.updateUserIDToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.updateUserIDToolStripMenuItem.Text = "Update User ID";
            // 
            // WinMoss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "WinMoss";
            this.Text = "WinMoss";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinMoss_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbCommands.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gbCodeFiles.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStripCodeFiles.ResumeLayout(false);
            this.gbFindFiles.ResumeLayout(false);
            this.gbFindFiles.PerformLayout();
            this.gbOutput.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labFilter;
        private System.Windows.Forms.Label studentInstructions;
        private System.Windows.Forms.GroupBox gbCodeFiles;
        private System.Windows.Forms.GroupBox gbFindFiles;
        private System.Windows.Forms.GroupBox gbCommands;
        private System.Windows.Forms.GroupBox gbOutput;
        private System.Windows.Forms.PropertyGrid pgMoss;
        private System.Windows.Forms.RichTextBox rtxtOutput;
        private System.Windows.Forms.TextBox addFilter;
        private System.Windows.Forms.Button btnRemoveFiles;
        private System.Windows.Forms.Button btnAddStudentFiles;
        private System.Windows.Forms.Button btnRunMoss;
        private System.Windows.Forms.Button btnClearOutput;
        private System.Windows.Forms.Button btnClearQuery;
        private System.Windows.Forms.Button btnSaveOutput;
        private System.Windows.Forms.Button btnSaveQuery;
        private System.Windows.Forms.ListBox lbStudentFiles;
        private System.Windows.Forms.Button btnClearStudentFiles;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCodeFiles;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mossScriptToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem updateMossScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mossScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateUserIDToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ignFilter;
        private System.Windows.Forms.ListBox lbBaseFiles;
        private System.Windows.Forms.Button btnClearBaseFiles;
        private System.Windows.Forms.CheckBox cbtnDirectories;
        private System.Windows.Forms.Button btnAddBaseFiles;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label baseInstructions;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.CheckBox cbtnAutoRun;
    }
}
