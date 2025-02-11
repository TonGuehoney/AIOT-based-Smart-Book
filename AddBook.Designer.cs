namespace BookMessageSysTem
{
    partial class AddBook
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
            this.btnStartIn = new System.Windows.Forms.Button();
            this.bookAuthor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.typeBook = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSeat = new System.Windows.Forms.TextBox();
            this.txtBookname = new System.Windows.Forms.TextBox();
            this.cmbCardID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStartIn
            // 
            this.btnStartIn.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnStartIn.FlatAppearance.BorderSize = 0;
            this.btnStartIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartIn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStartIn.ForeColor = System.Drawing.Color.White;
            this.btnStartIn.Location = new System.Drawing.Point(198, 320);
            this.btnStartIn.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartIn.Name = "btnStartIn";
            this.btnStartIn.Size = new System.Drawing.Size(170, 36);
            this.btnStartIn.TabIndex = 29;
            this.btnStartIn.Text = "加入书架";
            this.btnStartIn.UseVisualStyleBackColor = false;
            this.btnStartIn.Click += new System.EventHandler(this.btnStartIn_Click);
            // 
            // bookAuthor
            // 
            this.bookAuthor.Location = new System.Drawing.Point(180, 178);
            this.bookAuthor.Margin = new System.Windows.Forms.Padding(4);
            this.bookAuthor.Name = "bookAuthor";
            this.bookAuthor.Size = new System.Drawing.Size(213, 25);
            this.bookAuthor.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(93, 181);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 27;
            this.label6.Text = "图书作者：";
            // 
            // typeBook
            // 
            this.typeBook.Location = new System.Drawing.Point(180, 222);
            this.typeBook.Margin = new System.Windows.Forms.Padding(4);
            this.typeBook.Name = "typeBook";
            this.typeBook.Size = new System.Drawing.Size(213, 25);
            this.typeBook.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(93, 225);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 25;
            this.label4.Text = "图书类型：";
            // 
            // txtSeat
            // 
            this.txtSeat.Location = new System.Drawing.Point(180, 262);
            this.txtSeat.Margin = new System.Windows.Forms.Padding(4);
            this.txtSeat.Name = "txtSeat";
            this.txtSeat.Size = new System.Drawing.Size(213, 25);
            this.txtSeat.TabIndex = 24;
            // 
            // txtBookname
            // 
            this.txtBookname.Location = new System.Drawing.Point(180, 131);
            this.txtBookname.Margin = new System.Windows.Forms.Padding(4);
            this.txtBookname.Name = "txtBookname";
            this.txtBookname.Size = new System.Drawing.Size(213, 25);
            this.txtBookname.TabIndex = 23;
            // 
            // cmbCardID
            // 
            this.cmbCardID.Location = new System.Drawing.Point(180, 84);
            this.cmbCardID.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCardID.Name = "cmbCardID";
            this.cmbCardID.ReadOnly = true;
            this.cmbCardID.Size = new System.Drawing.Size(213, 25);
            this.cmbCardID.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(93, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "图书名称：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 265);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "所在书架：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "标签号：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(13, 21);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "添加图书";
            // 
            // AddBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(534, 416);
            this.Controls.Add(this.btnStartIn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bookAuthor);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.typeBook);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSeat);
            this.Controls.Add(this.cmbCardID);
            this.Controls.Add(this.txtBookname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddBook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加图书";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddBook_FormClosed);
            this.Load += new System.EventHandler(this.AddBook_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox typeBook;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSeat;
        private System.Windows.Forms.TextBox txtBookname;
        private System.Windows.Forms.TextBox cmbCardID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox bookAuthor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnStartIn;
    }
}