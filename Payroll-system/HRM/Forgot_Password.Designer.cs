namespace HRM
{
    partial class Forgot_Password
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.messagePanel = new System.Windows.Forms.Panel();
            this.yourPassTxt = new System.Windows.Forms.TextBox();
            this.exitBtn2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.answerLb = new System.Windows.Forms.Label();
            this.answerTxt = new System.Windows.Forms.TextBox();
            this.userTxt = new System.Windows.Forms.TextBox();
            this.questionLb = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.questionTxt = new System.Windows.Forms.TextBox();
            this.exitBtn = new System.Windows.Forms.Button();
            this.okBtn2 = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.messagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(381, 395);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.messagePanel);
            this.groupBox1.Controls.Add(this.answerLb);
            this.groupBox1.Controls.Add(this.answerTxt);
            this.groupBox1.Controls.Add(this.userTxt);
            this.groupBox1.Controls.Add(this.questionLb);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.questionTxt);
            this.groupBox1.Controls.Add(this.exitBtn);
            this.groupBox1.Controls.Add(this.okBtn2);
            this.groupBox1.Controls.Add(this.okBtn);
            this.groupBox1.Controls.Add(this.shapeContainer1);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 390);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Forgot your password?";
            // 
            // messagePanel
            // 
            this.messagePanel.BackColor = System.Drawing.Color.White;
            this.messagePanel.Controls.Add(this.yourPassTxt);
            this.messagePanel.Controls.Add(this.exitBtn2);
            this.messagePanel.Controls.Add(this.label3);
            this.messagePanel.Controls.Add(this.pictureBox1);
            this.messagePanel.Controls.Add(this.shapeContainer2);
            this.messagePanel.Location = new System.Drawing.Point(359, 36);
            this.messagePanel.Name = "messagePanel";
            this.messagePanel.Size = new System.Drawing.Size(374, 365);
            this.messagePanel.TabIndex = 68;
            this.messagePanel.Visible = false;
            // 
            // yourPassTxt
            // 
            this.yourPassTxt.BackColor = System.Drawing.Color.White;
            this.yourPassTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.yourPassTxt.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yourPassTxt.Location = new System.Drawing.Point(20, 214);
            this.yourPassTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.yourPassTxt.MaxLength = 50;
            this.yourPassTxt.Name = "yourPassTxt";
            this.yourPassTxt.Size = new System.Drawing.Size(324, 24);
            this.yourPassTxt.TabIndex = 44;
            this.yourPassTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // exitBtn2
            // 
            this.exitBtn2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.exitBtn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitBtn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitBtn2.ForeColor = System.Drawing.Color.White;
            this.exitBtn2.Location = new System.Drawing.Point(19, 260);
            this.exitBtn2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.exitBtn2.Name = "exitBtn2";
            this.exitBtn2.Size = new System.Drawing.Size(324, 29);
            this.exitBtn2.TabIndex = 43;
            this.exitBtn2.Text = "OK";
            this.exitBtn2.UseVisualStyleBackColor = false;
            this.exitBtn2.Click += new System.EventHandler(this.exitBtn2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(106, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 21);
            this.label3.TabIndex = 42;
            this.label3.Text = "Your password is:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HRM.Properties.Resources.checkLogo;
            this.pictureBox1.Location = new System.Drawing.Point(97, 70);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(169, 99);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2});
            this.shapeContainer2.Size = new System.Drawing.Size(374, 365);
            this.shapeContainer2.TabIndex = 45;
            this.shapeContainer2.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderColor = System.Drawing.SystemColors.Highlight;
            this.lineShape2.BorderWidth = 3;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = 18;
            this.lineShape2.X2 = 347;
            this.lineShape2.Y1 = 240;
            this.lineShape2.Y2 = 240;
            // 
            // answerLb
            // 
            this.answerLb.AutoSize = true;
            this.answerLb.Enabled = false;
            this.answerLb.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.answerLb.ForeColor = System.Drawing.Color.White;
            this.answerLb.Location = new System.Drawing.Point(120, 265);
            this.answerLb.Name = "answerLb";
            this.answerLb.Size = new System.Drawing.Size(145, 20);
            this.answerLb.TabIndex = 9;
            this.answerLb.Text = "Enter your Answer:";
            // 
            // answerTxt
            // 
            this.answerTxt.BackColor = System.Drawing.Color.White;
            this.answerTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.answerTxt.Enabled = false;
            this.answerTxt.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.answerTxt.Location = new System.Drawing.Point(26, 288);
            this.answerTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.answerTxt.MaxLength = 50;
            this.answerTxt.Name = "answerTxt";
            this.answerTxt.Size = new System.Drawing.Size(325, 24);
            this.answerTxt.TabIndex = 8;
            this.answerTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // userTxt
            // 
            this.userTxt.BackColor = System.Drawing.Color.White;
            this.userTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.userTxt.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userTxt.Location = new System.Drawing.Point(26, 60);
            this.userTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.userTxt.MaxLength = 50;
            this.userTxt.Name = "userTxt";
            this.userTxt.Size = new System.Drawing.Size(325, 24);
            this.userTxt.TabIndex = 4;
            this.userTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // questionLb
            // 
            this.questionLb.AutoSize = true;
            this.questionLb.Enabled = false;
            this.questionLb.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionLb.ForeColor = System.Drawing.Color.White;
            this.questionLb.Location = new System.Drawing.Point(148, 172);
            this.questionLb.Name = "questionLb";
            this.questionLb.Size = new System.Drawing.Size(78, 20);
            this.questionLb.TabIndex = 7;
            this.questionLb.Text = "Question:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(110, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Enter your username:";
            // 
            // questionTxt
            // 
            this.questionTxt.BackColor = System.Drawing.Color.White;
            this.questionTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.questionTxt.Enabled = false;
            this.questionTxt.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.questionTxt.Location = new System.Drawing.Point(26, 196);
            this.questionTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.questionTxt.MaxLength = 50;
            this.questionTxt.Multiline = true;
            this.questionTxt.Name = "questionTxt";
            this.questionTxt.ReadOnly = true;
            this.questionTxt.Size = new System.Drawing.Size(325, 65);
            this.questionTxt.TabIndex = 6;
            this.questionTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // exitBtn
            // 
            this.exitBtn.FlatAppearance.BorderSize = 0;
            this.exitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitBtn.ForeColor = System.Drawing.Color.Red;
            this.exitBtn.Location = new System.Drawing.Point(357, 0);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(25, 21);
            this.exitBtn.TabIndex = 66;
            this.exitBtn.Text = "X";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // okBtn2
            // 
            this.okBtn2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.okBtn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okBtn2.Enabled = false;
            this.okBtn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okBtn2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okBtn2.ForeColor = System.Drawing.Color.White;
            this.okBtn2.Location = new System.Drawing.Point(26, 331);
            this.okBtn2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.okBtn2.Name = "okBtn2";
            this.okBtn2.Size = new System.Drawing.Size(325, 29);
            this.okBtn2.TabIndex = 35;
            this.okBtn2.Text = "Ok";
            this.okBtn2.UseVisualStyleBackColor = false;
            this.okBtn2.Click += new System.EventHandler(this.okBtn2_Click);
            // 
            // okBtn
            // 
            this.okBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(79)))));
            this.okBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okBtn.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okBtn.ForeColor = System.Drawing.Color.White;
            this.okBtn.Location = new System.Drawing.Point(25, 100);
            this.okBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(325, 29);
            this.okBtn.TabIndex = 37;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = false;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 23);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(372, 364);
            this.shapeContainer1.TabIndex = 67;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderWidth = 3;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 2;
            this.lineShape1.X2 = 372;
            this.lineShape1.Y1 = 135;
            this.lineShape1.Y2 = 135;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(25, 177);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(331, 183);
            this.panel2.TabIndex = 69;
            // 
            // Forgot_Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(122)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(387, 401);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Forgot_Password";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Forgot_Password";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.messagePanel.ResumeLayout(false);
            this.messagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label answerLb;
        private System.Windows.Forms.TextBox answerTxt;
        private System.Windows.Forms.Label questionLb;
        private System.Windows.Forms.TextBox questionTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button okBtn2;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button exitBtn;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Panel messagePanel;
        private System.Windows.Forms.Button exitBtn2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox yourPassTxt;
        private System.Windows.Forms.Timer timer1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private System.Windows.Forms.Panel panel2;
    }
}