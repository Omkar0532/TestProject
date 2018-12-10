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
    public partial class PartInformation : Form
    {
        public TreeNode parent = null;
        public TreeNode child = null;
        public TreeNode sub_child = null;
        public List<PartInfo> m_lsParts = new List<PartInfo>();
        main oMain = null;
        public int index = 0;
        public PartInformation(TreeNode p, TreeNode c)
        {
            this.parent = p;
            this.child = c;
        }
        public PartInformation(main obj)
        {
            InitializeComponent();
            this.CenterToScreen();
            oMain = obj;
        }

        private void btnAddPrt_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddPrt.Text == "Add Part")
                {
                    PartInfo oPInfo = new PartInfo();
                    
                    index = dataGridView1.Rows.Add();
                    
                    oPInfo.m_sPTNum = tbPTNum.Text.Trim();
                    oPInfo.m_sDashNum = tbDNum.Text.Trim();
                    oPInfo.m_sPTDesc = tbPTDesc.Text.Trim();
                    oPInfo.m_sPTRev = tbPTRev.Text.Trim();
                    oPInfo.m_sTolStatus = cbTStat.SelectedText;
                    oPInfo.m_sTolSource = cbTSrc.SelectedText;
                    oPInfo.m_sNominalDim = tbNLDim.Text.Trim();
                    oPInfo.m_sDiametricTol = tbDLTlc.Text.Trim();
                    oPInfo.m_sBonusTol = tbBTlc.Text.Trim();
                    oPInfo.m_sBilateralTol = tbBiTol.Text.Trim();
                    oPInfo.m_sUniTolPlus = tbUTPlus.Text.Trim();
                    oPInfo.m_sUniTolMinus = tbUTMinus.Text.Trim();
                    m_lsParts.Add(oPInfo);

                    dataGridView1[0, index].Value = (index + 1).ToString();
                    dataGridView1[1, index].Value = oPInfo.m_sPTNum;
                    dataGridView1[2, index].Value = oPInfo.m_sUniTolPlus;
                    dataGridView1[3, index].Value = oPInfo.m_sUniTolMinus;

                    tbPTNum.Text = "";
                    tbDNum.Text = "";
                    tbPTDesc.Text = "";
                    tbPTRev.Text = "";
                    tbNLDim.Text = "";
                    tbDLTlc.Text = "";
                    tbBTlc.Text = "";
                    tbBiTol.Text = "";
                    tbUTPlus.Text = "";
                    tbUTMinus.Text = "";
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee+"");
            }
        }

        private void btnPInfo_OK_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (PartInfo obj in m_lsParts)
                {
                    if (obj.m_sPTNum.Length > 0)
                    {
                        //MessageBox.Show(obj.m_sPTNum);
                        //m_sPartName = obj.m_sPTNum;
                        this.Close();
                        parent = oMain.treeView1.SelectedNode;
                        if (parent != null)
                        {
                            child = parent.TreeView.SelectedNode;
                            if (child != null)
                            {
                                sub_child = child.Nodes.Add(obj.m_sPTNum);
                                oMain.treeView1.ExpandAll();
                            }
                            else
                            {
                                MessageBox.Show("Selected Child = " + child.Text);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Selected Parent = " + parent.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("please enter Part/Tool number");
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee + "");
            }
        }

        private void btnPInfo_Cncl_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbBiTol_TextChanged(object sender, EventArgs e)
        {
            if (tbBiTol.Text.Length > 0)
            {
                tbUTPlus.Text = tbBiTol.Text;
                tbUTMinus.Text = tbBiTol.Text;
            }
            //else
            //{
            //    tbUTPlus.Enabled = false;
            //    tbUTMinus.Enabled = false;
            //}
        }
    }

    public class PartInfo
    {
        public string m_sPTNum = "";
        public string m_sDashNum = "";
        public string m_sPTDesc = "";
        public string m_sPTRev = "";
        public string m_sTolStatus = "";
        public string m_sTolSource = "";
        public string m_sNominalDim = "";
        public string m_sDiametricTol = "";
        public string m_sBonusTol = "";
        public string m_sBilateralTol = "";
        public string m_sUniTolPlus = "";
        public string m_sUniTolMinus = "";
        public bool m_bIsStandarPart = false;
    }
}