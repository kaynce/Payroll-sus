namespace HRM
{
    partial class Pending_User
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.exitBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.listOfUsersPage = new System.Windows.Forms.TabPage();
            this.listOfUserDataGridView = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.label6 = new System.Windows.Forms.Label();
            this.refreshBtn1 = new System.Windows.Forms.Button();
            this.archiveBtn = new System.Windows.Forms.Button();
            this.pendingUsersPage = new System.Windows.Forms.TabPage();
            this.pendingUsersDataGridView = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.blockBtn = new System.Windows.Forms.Button();
            this.approveBtn = new System.Windows.Forms.Button();
            this.refreshBtn2 = new System.Windows.Forms.Button();
            this.blockedListPage = new System.Windows.Forms.TabPage();
            this.blockListDataGridView = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.label7 = new System.Windows.Forms.Label();
            this.refreshBtn3 = new System.Windows.Forms.Button();
            this.archivePage = new System.Windows.Forms.TabPage();
            this.archiveDataGridView = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.label8 = new System.Windows.Forms.Label();
            this.refreshBtn4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.listOfUsersPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listOfUserDataGridView)).BeginInit();
            this.pendingUsersPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pendingUsersDataGridView)).BeginInit();
            this.blockedListPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockListDataGridView)).BeginInit();
            this.archivePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.archiveDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.groupBox1.Controls.Add(this.exitBtn);
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1012, 604);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List of User/s";
            // 
            // exitBtn
            // 
            this.exitBtn.BackColor = System.Drawing.Color.Transparent;
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitBtn.FlatAppearance.BorderSize = 0;
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.exitBtn.ForeColor = System.Drawing.Color.Red;
            this.exitBtn.Location = new System.Drawing.Point(586, -3);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(25, 25);
            this.exitBtn.TabIndex = 106;
            this.exitBtn.Text = "X";
            this.exitBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitBtn.UseVisualStyleBackColor = false;
            this.exitBtn.Visible = false;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.listOfUsersPage);
            this.tabControl1.Controls.Add(this.pendingUsersPage);
            this.tabControl1.Controls.Add(this.blockedListPage);
            this.tabControl1.Controls.Add(this.archivePage);
            this.tabControl1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(7, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(999, 570);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
            // 
            // listOfUsersPage
            // 
            this.listOfUsersPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.listOfUsersPage.Controls.Add(this.listOfUserDataGridView);
            this.listOfUsersPage.Controls.Add(this.label6);
            this.listOfUsersPage.Controls.Add(this.refreshBtn1);
            this.listOfUsersPage.Controls.Add(this.archiveBtn);
            this.listOfUsersPage.ForeColor = System.Drawing.Color.Black;
            this.listOfUsersPage.Location = new System.Drawing.Point(4, 26);
            this.listOfUsersPage.Name = "listOfUsersPage";
            this.listOfUsersPage.Padding = new System.Windows.Forms.Padding(3);
            this.listOfUsersPage.Size = new System.Drawing.Size(991, 540);
            this.listOfUsersPage.TabIndex = 0;
            this.listOfUsersPage.Text = "List of User(s)";
            // 
            // listOfUserDataGridView
            // 
            this.listOfUserDataGridView.AllowUserToAddRows = false;
            this.listOfUserDataGridView.AllowUserToDeleteRows = false;
            this.listOfUserDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.listOfUserDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.listOfUserDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listOfUserDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.listOfUserDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listOfUserDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.listOfUserDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.listOfUserDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.listOfUserDataGridView.ColumnHeadersHeight = 40;
            this.listOfUserDataGridView.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.listOfUserDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.listOfUserDataGridView.DoubleBuffered = true;
            this.listOfUserDataGridView.EnableHeadersVisualStyles = false;
            this.listOfUserDataGridView.HeaderBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.listOfUserDataGridView.HeaderForeColor = System.Drawing.Color.White;
            this.listOfUserDataGridView.Location = new System.Drawing.Point(6, 54);
            this.listOfUserDataGridView.Name = "listOfUserDataGridView";
            this.listOfUserDataGridView.ReadOnly = true;
            this.listOfUserDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.listOfUserDataGridView.RowHeadersVisible = false;
            this.listOfUserDataGridView.RowTemplate.DividerHeight = 1;
            this.listOfUserDataGridView.RowTemplate.Height = 30;
            this.listOfUserDataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.listOfUserDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listOfUserDataGridView.Size = new System.Drawing.Size(980, 480);
            this.listOfUserDataGridView.TabIndex = 116;
            this.listOfUserDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.listOfUserDataGridView_CellClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Gray;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkRed;
            this.label6.Location = new System.Drawing.Point(589, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 17);
            this.label6.TabIndex = 114;
            this.label6.Text = "Note: Double Click the cell to Archive";
            this.label6.Visible = false;
            // 
            // refreshBtn1
            // 
            this.refreshBtn1.BackColor = System.Drawing.Color.Gray;
            this.refreshBtn1.FlatAppearance.BorderSize = 0;
            this.refreshBtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshBtn1.Image = global::HRM.Properties.Resources.Refresh;
            this.refreshBtn1.Location = new System.Drawing.Point(554, 18);
            this.refreshBtn1.Name = "refreshBtn1";
            this.refreshBtn1.Size = new System.Drawing.Size(29, 30);
            this.refreshBtn1.TabIndex = 108;
            this.refreshBtn1.UseVisualStyleBackColor = false;
            this.refreshBtn1.Visible = false;
            this.refreshBtn1.Click += new System.EventHandler(this.refreshBtn1_Click);
            // 
            // archiveBtn
            // 
            this.archiveBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.archiveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.archiveBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.archiveBtn.ForeColor = System.Drawing.Color.White;
            this.archiveBtn.Location = new System.Drawing.Point(879, 18);
            this.archiveBtn.Name = "archiveBtn";
            this.archiveBtn.Size = new System.Drawing.Size(86, 33);
            this.archiveBtn.TabIndex = 52;
            this.archiveBtn.Text = "Archive";
            this.archiveBtn.UseVisualStyleBackColor = false;
            this.archiveBtn.Visible = false;
            this.archiveBtn.Click += new System.EventHandler(this.archiveBtn_Click);
            // 
            // pendingUsersPage
            // 
            this.pendingUsersPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.pendingUsersPage.Controls.Add(this.pendingUsersDataGridView);
            this.pendingUsersPage.Controls.Add(this.label1);
            this.pendingUsersPage.Controls.Add(this.blockBtn);
            this.pendingUsersPage.Controls.Add(this.approveBtn);
            this.pendingUsersPage.Controls.Add(this.refreshBtn2);
            this.pendingUsersPage.ForeColor = System.Drawing.Color.Black;
            this.pendingUsersPage.Location = new System.Drawing.Point(4, 26);
            this.pendingUsersPage.Name = "pendingUsersPage";
            this.pendingUsersPage.Padding = new System.Windows.Forms.Padding(3);
            this.pendingUsersPage.Size = new System.Drawing.Size(991, 540);
            this.pendingUsersPage.TabIndex = 1;
            this.pendingUsersPage.Text = "Pending User(s)";
            // 
            // pendingUsersDataGridView
            // 
            this.pendingUsersDataGridView.AllowUserToAddRows = false;
            this.pendingUsersDataGridView.AllowUserToDeleteRows = false;
            this.pendingUsersDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.pendingUsersDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.pendingUsersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pendingUsersDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.pendingUsersDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pendingUsersDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.pendingUsersDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.pendingUsersDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.pendingUsersDataGridView.ColumnHeadersHeight = 40;
            this.pendingUsersDataGridView.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.pendingUsersDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.pendingUsersDataGridView.DoubleBuffered = true;
            this.pendingUsersDataGridView.EnableHeadersVisualStyles = false;
            this.pendingUsersDataGridView.HeaderBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.pendingUsersDataGridView.HeaderForeColor = System.Drawing.Color.White;
            this.pendingUsersDataGridView.Location = new System.Drawing.Point(6, 54);
            this.pendingUsersDataGridView.Name = "pendingUsersDataGridView";
            this.pendingUsersDataGridView.ReadOnly = true;
            this.pendingUsersDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.pendingUsersDataGridView.RowHeadersVisible = false;
            this.pendingUsersDataGridView.RowTemplate.DividerHeight = 1;
            this.pendingUsersDataGridView.RowTemplate.Height = 30;
            this.pendingUsersDataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pendingUsersDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pendingUsersDataGridView.Size = new System.Drawing.Size(980, 480);
            this.pendingUsersDataGridView.TabIndex = 117;
            this.pendingUsersDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.pendingUsersDataGridView_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Gray;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(424, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 17);
            this.label1.TabIndex = 109;
            this.label1.Text = "Note: Double Click the cell to Approve/Block";
            this.label1.Visible = false;
            // 
            // blockBtn
            // 
            this.blockBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.blockBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.blockBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.blockBtn.ForeColor = System.Drawing.Color.White;
            this.blockBtn.Location = new System.Drawing.Point(787, 18);
            this.blockBtn.Name = "blockBtn";
            this.blockBtn.Size = new System.Drawing.Size(86, 33);
            this.blockBtn.TabIndex = 56;
            this.blockBtn.Text = "Block";
            this.blockBtn.UseVisualStyleBackColor = false;
            this.blockBtn.Visible = false;
            this.blockBtn.Click += new System.EventHandler(this.blockBtn_Click);
            // 
            // approveBtn
            // 
            this.approveBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.approveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.approveBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.approveBtn.ForeColor = System.Drawing.Color.White;
            this.approveBtn.Location = new System.Drawing.Point(879, 18);
            this.approveBtn.Name = "approveBtn";
            this.approveBtn.Size = new System.Drawing.Size(86, 33);
            this.approveBtn.TabIndex = 55;
            this.approveBtn.Text = "Approve";
            this.approveBtn.UseVisualStyleBackColor = false;
            this.approveBtn.Visible = false;
            // 
            // refreshBtn2
            // 
            this.refreshBtn2.BackColor = System.Drawing.Color.Gray;
            this.refreshBtn2.FlatAppearance.BorderSize = 0;
            this.refreshBtn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshBtn2.Image = global::HRM.Properties.Resources.Refresh;
            this.refreshBtn2.Location = new System.Drawing.Point(554, 18);
            this.refreshBtn2.Name = "refreshBtn2";
            this.refreshBtn2.Size = new System.Drawing.Size(29, 30);
            this.refreshBtn2.TabIndex = 108;
            this.refreshBtn2.UseVisualStyleBackColor = false;
            this.refreshBtn2.Visible = false;
            this.refreshBtn2.Click += new System.EventHandler(this.refreshBtn2_Click);
            // 
            // blockedListPage
            // 
            this.blockedListPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.blockedListPage.Controls.Add(this.blockListDataGridView);
            this.blockedListPage.Controls.Add(this.label7);
            this.blockedListPage.Controls.Add(this.refreshBtn3);
            this.blockedListPage.ForeColor = System.Drawing.Color.Black;
            this.blockedListPage.Location = new System.Drawing.Point(4, 26);
            this.blockedListPage.Name = "blockedListPage";
            this.blockedListPage.Padding = new System.Windows.Forms.Padding(3);
            this.blockedListPage.Size = new System.Drawing.Size(991, 540);
            this.blockedListPage.TabIndex = 2;
            this.blockedListPage.Text = "Blocked List";
            // 
            // blockListDataGridView
            // 
            this.blockListDataGridView.AllowUserToAddRows = false;
            this.blockListDataGridView.AllowUserToDeleteRows = false;
            this.blockListDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.blockListDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.blockListDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.blockListDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.blockListDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.blockListDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.blockListDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.blockListDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.blockListDataGridView.ColumnHeadersHeight = 40;
            this.blockListDataGridView.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.blockListDataGridView.DefaultCellStyle = dataGridViewCellStyle9;
            this.blockListDataGridView.DoubleBuffered = true;
            this.blockListDataGridView.EnableHeadersVisualStyles = false;
            this.blockListDataGridView.HeaderBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.blockListDataGridView.HeaderForeColor = System.Drawing.Color.White;
            this.blockListDataGridView.Location = new System.Drawing.Point(6, 54);
            this.blockListDataGridView.Name = "blockListDataGridView";
            this.blockListDataGridView.ReadOnly = true;
            this.blockListDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.blockListDataGridView.RowHeadersVisible = false;
            this.blockListDataGridView.RowTemplate.DividerHeight = 1;
            this.blockListDataGridView.RowTemplate.Height = 30;
            this.blockListDataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.blockListDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.blockListDataGridView.Size = new System.Drawing.Size(980, 480);
            this.blockListDataGridView.TabIndex = 118;
            this.blockListDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.blockListDataGridView_CellClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Gray;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkRed;
            this.label7.Location = new System.Drawing.Point(457, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(256, 17);
            this.label7.TabIndex = 114;
            this.label7.Text = "Note: Double Click the cell to Retrieve";
            this.label7.Visible = false;
            // 
            // refreshBtn3
            // 
            this.refreshBtn3.BackColor = System.Drawing.Color.Gray;
            this.refreshBtn3.FlatAppearance.BorderSize = 0;
            this.refreshBtn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshBtn3.Image = global::HRM.Properties.Resources.Refresh;
            this.refreshBtn3.Location = new System.Drawing.Point(554, 18);
            this.refreshBtn3.Name = "refreshBtn3";
            this.refreshBtn3.Size = new System.Drawing.Size(29, 30);
            this.refreshBtn3.TabIndex = 108;
            this.refreshBtn3.UseVisualStyleBackColor = false;
            this.refreshBtn3.Visible = false;
            this.refreshBtn3.Click += new System.EventHandler(this.refreshBtn3_Click);
            // 
            // archivePage
            // 
            this.archivePage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.archivePage.Controls.Add(this.archiveDataGridView);
            this.archivePage.Controls.Add(this.label8);
            this.archivePage.Controls.Add(this.refreshBtn4);
            this.archivePage.ForeColor = System.Drawing.Color.Black;
            this.archivePage.Location = new System.Drawing.Point(4, 26);
            this.archivePage.Name = "archivePage";
            this.archivePage.Padding = new System.Windows.Forms.Padding(3);
            this.archivePage.Size = new System.Drawing.Size(991, 540);
            this.archivePage.TabIndex = 3;
            this.archivePage.Text = "Archived";
            // 
            // archiveDataGridView
            // 
            this.archiveDataGridView.AllowUserToAddRows = false;
            this.archiveDataGridView.AllowUserToDeleteRows = false;
            this.archiveDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            this.archiveDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.archiveDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.archiveDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.archiveDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.archiveDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.archiveDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.archiveDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.archiveDataGridView.ColumnHeadersHeight = 40;
            this.archiveDataGridView.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(226)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.archiveDataGridView.DefaultCellStyle = dataGridViewCellStyle12;
            this.archiveDataGridView.DoubleBuffered = true;
            this.archiveDataGridView.EnableHeadersVisualStyles = false;
            this.archiveDataGridView.HeaderBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.archiveDataGridView.HeaderForeColor = System.Drawing.Color.White;
            this.archiveDataGridView.Location = new System.Drawing.Point(6, 54);
            this.archiveDataGridView.Name = "archiveDataGridView";
            this.archiveDataGridView.ReadOnly = true;
            this.archiveDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.archiveDataGridView.RowHeadersVisible = false;
            this.archiveDataGridView.RowTemplate.DividerHeight = 1;
            this.archiveDataGridView.RowTemplate.Height = 30;
            this.archiveDataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.archiveDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.archiveDataGridView.Size = new System.Drawing.Size(980, 480);
            this.archiveDataGridView.TabIndex = 119;
            this.archiveDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.archiveDataGridView_CellClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Gray;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkRed;
            this.label8.Location = new System.Drawing.Point(551, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(256, 17);
            this.label8.TabIndex = 114;
            this.label8.Text = "Note: Double Click the cell to Retrieve";
            this.label8.Visible = false;
            // 
            // refreshBtn4
            // 
            this.refreshBtn4.BackColor = System.Drawing.Color.Gray;
            this.refreshBtn4.FlatAppearance.BorderSize = 0;
            this.refreshBtn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshBtn4.Image = global::HRM.Properties.Resources.Refresh;
            this.refreshBtn4.Location = new System.Drawing.Point(554, 18);
            this.refreshBtn4.Name = "refreshBtn4";
            this.refreshBtn4.Size = new System.Drawing.Size(29, 30);
            this.refreshBtn4.TabIndex = 108;
            this.refreshBtn4.UseVisualStyleBackColor = false;
            this.refreshBtn4.Visible = false;
            this.refreshBtn4.Click += new System.EventHandler(this.refreshBtn4_Click);
            // 
            // Pending_User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1019, 608);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Pending_User";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pending_User";
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.listOfUsersPage.ResumeLayout(false);
            this.listOfUsersPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listOfUserDataGridView)).EndInit();
            this.pendingUsersPage.ResumeLayout(false);
            this.pendingUsersPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pendingUsersDataGridView)).EndInit();
            this.blockedListPage.ResumeLayout(false);
            this.blockedListPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockListDataGridView)).EndInit();
            this.archivePage.ResumeLayout(false);
            this.archivePage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.archiveDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage listOfUsersPage;
        private System.Windows.Forms.TabPage pendingUsersPage;
        private System.Windows.Forms.Button archiveBtn;
        private System.Windows.Forms.Button blockBtn;
        private System.Windows.Forms.Button approveBtn;
        private System.Windows.Forms.TabPage blockedListPage;
        private System.Windows.Forms.TabPage archivePage;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button refreshBtn1;
        private System.Windows.Forms.Button refreshBtn2;
        private System.Windows.Forms.Button refreshBtn3;
        private System.Windows.Forms.Button refreshBtn4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private Bunifu.Framework.UI.BunifuCustomDataGrid listOfUserDataGridView;
        private Bunifu.Framework.UI.BunifuCustomDataGrid pendingUsersDataGridView;
        private Bunifu.Framework.UI.BunifuCustomDataGrid blockListDataGridView;
        private Bunifu.Framework.UI.BunifuCustomDataGrid archiveDataGridView;
    }
}