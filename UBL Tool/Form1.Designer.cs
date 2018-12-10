namespace UBL_Tool
{
    partial class Form1
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btRun = new System.Windows.Forms.Button();
            this.gbSelectInputFile = new System.Windows.Forms.GroupBox();
            this.lbSelectInputFile = new System.Windows.Forms.Label();
            this.tbInputFile = new System.Windows.Forms.TextBox();
            this.lbStatus = new System.Windows.Forms.Label();
            this.gbSelectInputFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(389, 13);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancel.Location = new System.Drawing.Point(386, 61);
            this.btCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(85, 30);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btRun
            // 
            this.btRun.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btRun.Location = new System.Drawing.Point(276, 61);
            this.btRun.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btRun.Name = "btRun";
            this.btRun.Size = new System.Drawing.Size(85, 30);
            this.btRun.TabIndex = 4;
            this.btRun.Text = "Run";
            this.btRun.UseVisualStyleBackColor = true;
            this.btRun.Click += new System.EventHandler(this.btRun_Click);
            // 
            // gbSelectInputFile
            // 
            this.gbSelectInputFile.Controls.Add(this.lbSelectInputFile);
            this.gbSelectInputFile.Controls.Add(this.tbInputFile);
            this.gbSelectInputFile.Controls.Add(this.btnBrowse);
            this.gbSelectInputFile.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSelectInputFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbSelectInputFile.Location = new System.Drawing.Point(7, 3);
            this.gbSelectInputFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbSelectInputFile.Name = "gbSelectInputFile";
            this.gbSelectInputFile.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbSelectInputFile.Size = new System.Drawing.Size(472, 49);
            this.gbSelectInputFile.TabIndex = 5;
            this.gbSelectInputFile.TabStop = false;
            // 
            // lbSelectInputFile
            // 
            this.lbSelectInputFile.AutoSize = true;
            this.lbSelectInputFile.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSelectInputFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbSelectInputFile.Location = new System.Drawing.Point(7, 18);
            this.lbSelectInputFile.Name = "lbSelectInputFile";
            this.lbSelectInputFile.Size = new System.Drawing.Size(132, 15);
            this.lbSelectInputFile.TabIndex = 0;
            this.lbSelectInputFile.Text = "Select Input Excel File :";
            // 
            // tbInputFile
            // 
            this.tbInputFile.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInputFile.Location = new System.Drawing.Point(142, 15);
            this.tbInputFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbInputFile.Name = "tbInputFile";
            this.tbInputFile.Size = new System.Drawing.Size(241, 21);
            this.tbInputFile.TabIndex = 1;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.ForeColor = System.Drawing.Color.Blue;
            this.lbStatus.Location = new System.Drawing.Point(14, 69);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(88, 15);
            this.lbStatus.TabIndex = 6;
            this.lbStatus.Text = "Please Wait.....";
            this.lbStatus.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 99);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.gbSelectInputFile);
            this.Controls.Add(this.btRun);
            this.Controls.Add(this.btCancel);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "UBL Tool V1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbSelectInputFile.ResumeLayout(false);
            this.gbSelectInputFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btRun;
        private System.Windows.Forms.GroupBox gbSelectInputFile;
        private System.Windows.Forms.Label lbSelectInputFile;
        private System.Windows.Forms.Label lbStatus;
        public System.Windows.Forms.TextBox tbInputFile;
    }
}

