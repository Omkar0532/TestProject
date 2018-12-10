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
    public partial class BuildName : Form
    {
        public string m_sBname = "";
        public TreeNode parent = null;
        public TreeNode child = null;
        main oMain = null;
        public BuildName(TreeNode p)
        {
            this.parent = p;
        }
        public BuildName(main obj)
        {
            InitializeComponent();
            oMain = obj;
            this.CenterToScreen();
        }

        private void btnBNOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtbname.Text.Length > 0)
                {
                    m_sBname = txtbname.Text;
                    this.Close();
                    parent = oMain.treeView1.SelectedNode;
                    if(parent != null)
                    {
                        child = parent.Nodes.Add(m_sBname);
                        child.ContextMenuStrip = oMain.PartInfo_contextMenuStrip;
                        oMain.treeView1.ExpandAll();
                        PartInformation oPInfo = new PartInformation(parent, child);
                        //child.ExpandAll();
                    }
                }
                else
                {
                    MessageBox.Show("Name must be given, please Enter name");
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee + "");
            }
        }

        private void btnBNCncl_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}