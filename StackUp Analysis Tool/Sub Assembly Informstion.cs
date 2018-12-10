using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _11_09_2018
{
    public partial class Sub_Assembly_Informstion : Form
    {
        public string m_sSAName = "";
        public string m_sImagePath = "";
        public TreeNode parent = null;
        public static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

        main oMain = null;
        public Sub_Assembly_Informstion(main obj)
        {
            InitializeComponent();
            this.CenterToScreen();
            oMain = obj;
        }

        private void btnSANOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSAName.Text.Length > 0)
                {
                    m_sSAName = txtSAName.Text;
                    this.Close();
                    parent = oMain.treeView1.Nodes[0].Nodes.Add(m_sSAName);
                    parent.ContextMenuStrip = oMain.build_contextMenuStrip;
                    oMain.treeView1.ExpandAll();
                    BuildName obn = new BuildName(parent);
                    //parent.ExpandAll();
                }
                else
                {
                    MessageBox.Show("Name must be given, please Enter name");
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee+"");
            }
        }

        private void btnSANCncl_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void browseImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdImage = new OpenFileDialog();
            ofdImage.InitialDirectory = @"C:\";
            ofdImage.Title = "Select Image";
            ofdImage.CheckPathExists = true;
            ofdImage.RestoreDirectory = true;
            ofdImage.Filter = "BMP|*.bmp|GIF|*.gif|JPG|*.jpg;*.jpeg|PNG|*.png|TIFF|*.tif;*.tiff|" + "All Graphics Types|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";
            if (ofdImage.ShowDialog() == DialogResult.OK)
            {
                m_sImagePath = ofdImage.FileName;
            }
            if (m_sImagePath.Length > 0 && System.IO.File.Exists(m_sImagePath))
            {
                //string image = System.IO.Path.GetFileName(m_sImagePath);
                Bitmap bimage = new Bitmap(m_sImagePath);
                //Image i = resizeImage(bimage, new Size(384, 253));
                pbImage.Image = resizeImage(bimage, new Size(384, 253));
            }
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}