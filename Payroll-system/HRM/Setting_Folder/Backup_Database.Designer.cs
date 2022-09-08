namespace HRM
{
    partial class Backup_Database
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backup_TakeBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.backupTxt = new System.Windows.Forms.TextBox();
            this.restore_BrowseLocationBtn = new System.Windows.Forms.Button();
            this.restore_DatabaseBtn = new System.Windows.Forms.Button();
            this.backup_BrowseLocationBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.restoreTxt = new System.Windows.Forms.TextBox();
            this.exitBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.backup_TakeBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.backupTxt);
            this.groupBox1.Controls.Add(this.restore_BrowseLocationBtn);
            this.groupBox1.Controls.Add(this.restore_DatabaseBtn);
            this.groupBox1.Controls.Add(this.backup_BrowseLocationBtn);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.restoreTxt);
            this.groupBox1.Controls.Add(this.exitBtn);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(176)))));
            this.groupBox1.Location = new System.Drawing.Point(3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1012, 604);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Backup Database";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.panel2.Location = new System.Drawing.Point(35, 286);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(920, 3);
            this.panel2.TabIndex = 150;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.panel1.Location = new System.Drawing.Point(31, 135);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(920, 3);
            this.panel1.TabIndex = 149;
            // 
            // backup_TakeBtn
            // 
            this.backup_TakeBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.backup_TakeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backup_TakeBtn.Enabled = false;
            this.backup_TakeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backup_TakeBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.backup_TakeBtn.ForeColor = System.Drawing.Color.White;
            this.backup_TakeBtn.Location = new System.Drawing.Point(516, 141);
            this.backup_TakeBtn.Name = "backup_TakeBtn";
            this.backup_TakeBtn.Size = new System.Drawing.Size(186, 33);
            this.backup_TakeBtn.TabIndex = 148;
            this.backup_TakeBtn.Text = "Take Backup";
            this.backup_TakeBtn.UseVisualStyleBackColor = false;
            this.backup_TakeBtn.Click += new System.EventHandler(this.backup_TakeBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(314, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 30);
            this.label1.TabIndex = 147;
            this.label1.Text = "Enter the location for backup:";
            // 
            // backupTxt
            // 
            this.backupTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.backupTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.backupTxt.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.backupTxt.ForeColor = System.Drawing.Color.White;
            this.backupTxt.Location = new System.Drawing.Point(30, 112);
            this.backupTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.backupTxt.MaxLength = 50;
            this.backupTxt.Name = "backupTxt";
            this.backupTxt.Size = new System.Drawing.Size(920, 22);
            this.backupTxt.TabIndex = 146;
            this.backupTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // restore_BrowseLocationBtn
            // 
            this.restore_BrowseLocationBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.restore_BrowseLocationBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.restore_BrowseLocationBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restore_BrowseLocationBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.restore_BrowseLocationBtn.ForeColor = System.Drawing.Color.White;
            this.restore_BrowseLocationBtn.Location = new System.Drawing.Point(319, 292);
            this.restore_BrowseLocationBtn.Name = "restore_BrowseLocationBtn";
            this.restore_BrowseLocationBtn.Size = new System.Drawing.Size(186, 33);
            this.restore_BrowseLocationBtn.TabIndex = 54;
            this.restore_BrowseLocationBtn.Text = "Browse Location";
            this.restore_BrowseLocationBtn.UseVisualStyleBackColor = false;
            this.restore_BrowseLocationBtn.Click += new System.EventHandler(this.restore_BrowseLocationBtn_Click);
            // 
            // restore_DatabaseBtn
            // 
            this.restore_DatabaseBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.restore_DatabaseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.restore_DatabaseBtn.Enabled = false;
            this.restore_DatabaseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restore_DatabaseBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.restore_DatabaseBtn.ForeColor = System.Drawing.Color.White;
            this.restore_DatabaseBtn.Location = new System.Drawing.Point(516, 292);
            this.restore_DatabaseBtn.Name = "restore_DatabaseBtn";
            this.restore_DatabaseBtn.Size = new System.Drawing.Size(186, 33);
            this.restore_DatabaseBtn.TabIndex = 145;
            this.restore_DatabaseBtn.Text = "Restore Database";
            this.restore_DatabaseBtn.UseVisualStyleBackColor = false;
            this.restore_DatabaseBtn.Click += new System.EventHandler(this.restore_DatabaseBtn_Click);
            // 
            // backup_BrowseLocationBtn
            // 
            this.backup_BrowseLocationBtn.BackColor = System.Drawing.SystemColors.Highlight;
            this.backup_BrowseLocationBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.backup_BrowseLocationBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backup_BrowseLocationBtn.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.backup_BrowseLocationBtn.ForeColor = System.Drawing.Color.White;
            this.backup_BrowseLocationBtn.Location = new System.Drawing.Point(319, 141);
            this.backup_BrowseLocationBtn.Name = "backup_BrowseLocationBtn";
            this.backup_BrowseLocationBtn.Size = new System.Drawing.Size(186, 33);
            this.backup_BrowseLocationBtn.TabIndex = 144;
            this.backup_BrowseLocationBtn.Text = "Browse Location";
            this.backup_BrowseLocationBtn.UseVisualStyleBackColor = false;
            this.backup_BrowseLocationBtn.Click += new System.EventHandler(this.backup_BrowseLocationBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(314, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(534, 30);
            this.label5.TabIndex = 142;
            this.label5.Text = "Enter the location from which the restore will take place:";
            // 
            // restoreTxt
            // 
            this.restoreTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(64)))));
            this.restoreTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.restoreTxt.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.restoreTxt.ForeColor = System.Drawing.Color.White;
            this.restoreTxt.Location = new System.Drawing.Point(36, 263);
            this.restoreTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.restoreTxt.MaxLength = 50;
            this.restoreTxt.Name = "restoreTxt";
            this.restoreTxt.Size = new System.Drawing.Size(920, 22);
            this.restoreTxt.TabIndex = 139;
            this.restoreTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // exitBtn
            // 
            this.exitBtn.BackColor = System.Drawing.Color.Transparent;
            this.exitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitBtn.FlatAppearance.BorderSize = 0;
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.exitBtn.ForeColor = System.Drawing.Color.Red;
            this.exitBtn.Location = new System.Drawing.Point(965, 0);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(25, 25);
            this.exitBtn.TabIndex = 133;
            this.exitBtn.Text = "X";
            this.exitBtn.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.exitBtn.UseVisualStyleBackColor = false;
            this.exitBtn.Visible = false;
            // 
            // Backup_Database
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1019, 608);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Backup_Database";
            this.Text = "Backup_Database";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button backup_TakeBtn;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox backupTxt;
        private System.Windows.Forms.Button restore_BrowseLocationBtn;
        private System.Windows.Forms.Button restore_DatabaseBtn;
        private System.Windows.Forms.Button backup_BrowseLocationBtn;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox restoreTxt;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}