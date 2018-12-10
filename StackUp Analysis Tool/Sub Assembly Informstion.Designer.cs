namespace _11_09_2018
{
    partial class Sub_Assembly_Informstion
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
            this.lblSAName = new System.Windows.Forms.Label();
            this.txtSAName = new System.Windows.Forms.TextBox();
            this.btnSANOk = new System.Windows.Forms.Button();
            this.btnSANCncl = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbImage = new System.Windows.Forms.GroupBox();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.csmImage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.gbImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.csmImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSAName
            // 
            this.lblSAName.AutoSize = true;
            this.lblSAName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSAName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblSAName.Location = new System.Drawing.Point(18, 36);
            this.lblSAName.Name = "lblSAName";
            this.lblSAName.Size = new System.Drawing.Size(152, 17);
            this.lblSAName.TabIndex = 0;
            this.lblSAName.Text = "Sub Assembly Name :";
            // 
            // txtSAName
            // 
            this.txtSAName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSAName.Location = new System.Drawing.Point(173, 31);
            this.txtSAName.Name = "txtSAName";
            this.txtSAName.Size = new System.Drawing.Size(209, 25);
            this.txtSAName.TabIndex = 1;
            // 
            // btnSANOk
            // 
            this.btnSANOk.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSANOk.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSANOk.Location = new System.Drawing.Point(232, 361);
            this.btnSANOk.Name = "btnSANOk";
            this.btnSANOk.Size = new System.Drawing.Size(74, 30);
            this.btnSANOk.TabIndex = 2;
            this.btnSANOk.Text = "OK";
            this.btnSANOk.UseVisualStyleBackColor = true;
            this.btnSANOk.Click += new System.EventHandler(this.btnSANOk_Click);
            // 
            // btnSANCncl
            // 
            this.btnSANCncl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSANCncl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSANCncl.Location = new System.Drawing.Point(320, 361);
            this.btnSANCncl.Name = "btnSANCncl";
            this.btnSANCncl.Size = new System.Drawing.Size(74, 30);
            this.btnSANCncl.TabIndex = 3;
            this.btnSANCncl.Text = "Cancel";
            this.btnSANCncl.UseVisualStyleBackColor = true;
            this.btnSANCncl.Click += new System.EventHandler(this.btnSANCncl_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSAName);
            this.groupBox1.Controls.Add(this.txtSAName);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 69);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sub Assembly Name";
            // 
            // gbImage
            // 
            this.gbImage.Controls.Add(this.pbImage);
            this.gbImage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbImage.ForeColor = System.Drawing.Color.Blue;
            this.gbImage.Location = new System.Drawing.Point(2, 72);
            this.gbImage.Name = "gbImage";
            this.gbImage.Size = new System.Drawing.Size(397, 284);
            this.gbImage.TabIndex = 2;
            this.gbImage.TabStop = false;
            this.gbImage.Text = "Image";
            // 
            // pbImage
            // 
            this.pbImage.ContextMenuStrip = this.csmImage;
            this.pbImage.Location = new System.Drawing.Point(6, 25);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(384, 253);
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // csmImage
            // 
            this.csmImage.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.csmImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.browseImageToolStripMenuItem,
            this.deleteImageToolStripMenuItem});
            this.csmImage.Name = "csmImage";
            this.csmImage.Size = new System.Drawing.Size(188, 92);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.copyToolStripMenuItem.Text = "Copy from Clipboard";
            // 
            // browseImageToolStripMenuItem
            // 
            this.browseImageToolStripMenuItem.Name = "browseImageToolStripMenuItem";
            this.browseImageToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.browseImageToolStripMenuItem.Text = "Browse Image";
            this.browseImageToolStripMenuItem.Click += new System.EventHandler(this.browseImageToolStripMenuItem_Click);
            // 
            // deleteImageToolStripMenuItem
            // 
            this.deleteImageToolStripMenuItem.Name = "deleteImageToolStripMenuItem";
            this.deleteImageToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.deleteImageToolStripMenuItem.Text = "Delete Image";
            // 
            // Sub_Assembly_Informstion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 404);
            this.Controls.Add(this.gbImage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSANCncl);
            this.Controls.Add(this.btnSANOk);
            this.Name = "Sub_Assembly_Informstion";
            this.Text = "Sub_Assembly_Informstion";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.csmImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSAName;
        private System.Windows.Forms.TextBox txtSAName;
        private System.Windows.Forms.Button btnSANOk;
        private System.Windows.Forms.Button btnSANCncl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbImage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.ContextMenuStrip csmImage;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteImageToolStripMenuItem;
    }
}