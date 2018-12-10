namespace _11_09_2018
{
    partial class BuildName
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
            this.lblbname = new System.Windows.Forms.Label();
            this.txtbname = new System.Windows.Forms.TextBox();
            this.btnBNOk = new System.Windows.Forms.Button();
            this.btnBNCncl = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblbname
            // 
            this.lblbname.AutoSize = true;
            this.lblbname.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbname.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblbname.Location = new System.Drawing.Point(16, 32);
            this.lblbname.Name = "lblbname";
            this.lblbname.Size = new System.Drawing.Size(91, 17);
            this.lblbname.TabIndex = 0;
            this.lblbname.Text = "Build Name :";
            // 
            // txtbname
            // 
            this.txtbname.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbname.Location = new System.Drawing.Point(113, 26);
            this.txtbname.Name = "txtbname";
            this.txtbname.Size = new System.Drawing.Size(175, 25);
            this.txtbname.TabIndex = 1;
            // 
            // btnBNOk
            // 
            this.btnBNOk.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBNOk.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBNOk.Location = new System.Drawing.Point(135, 67);
            this.btnBNOk.Name = "btnBNOk";
            this.btnBNOk.Size = new System.Drawing.Size(74, 30);
            this.btnBNOk.TabIndex = 2;
            this.btnBNOk.Text = "OK";
            this.btnBNOk.UseVisualStyleBackColor = true;
            this.btnBNOk.Click += new System.EventHandler(this.btnBNOk_Click);
            // 
            // btnBNCncl
            // 
            this.btnBNCncl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBNCncl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBNCncl.Location = new System.Drawing.Point(213, 68);
            this.btnBNCncl.Name = "btnBNCncl";
            this.btnBNCncl.Size = new System.Drawing.Size(75, 30);
            this.btnBNCncl.TabIndex = 3;
            this.btnBNCncl.Text = "Cancel";
            this.btnBNCncl.UseVisualStyleBackColor = true;
            this.btnBNCncl.Click += new System.EventHandler(this.btnBNCncl_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblbname);
            this.groupBox1.Controls.Add(this.btnBNCncl);
            this.groupBox1.Controls.Add(this.txtbname);
            this.groupBox1.Controls.Add(this.btnBNOk);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 118);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Build Name";
            // 
            // BuildName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 142);
            this.Controls.Add(this.groupBox1);
            this.Name = "BuildName";
            this.Text = "BuildName";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblbname;
        private System.Windows.Forms.TextBox txtbname;
        private System.Windows.Forms.Button btnBNOk;
        private System.Windows.Forms.Button btnBNCncl;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}