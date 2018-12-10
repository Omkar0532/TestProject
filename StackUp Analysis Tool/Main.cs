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
    public partial class main : Form
    {
        main oForm = null;
        public main()
        {
            InitializeComponent();
            oForm = this;
            this.CenterToScreen();

            dgvTolInfo.Visible = false;
            dgvPartInfo.Visible = false;
            gbOutput.Visible = false;
            gbDesignComments.Visible = false;
            gbGNComments.Visible = false;
            pbPanelImage.Visible = false;
        }

        private void addSubAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sub_Assembly_Informstion oSAInfo = new Sub_Assembly_Informstion(oForm);
            oSAInfo.ShowDialog();
        }

        private void addBuildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuildName obn = new BuildName(oForm);
            obn.ShowDialog();
        }

        private void addEditPartInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PartInformation opi = new PartInformation(oForm);
            opi.ShowDialog();
            
            int index = 0;
            TreeNode AssNode = oForm.treeView1.SelectedNode;
            TreeNode SubAssNode = opi.child.TreeView.SelectedNode;
            if (index > 0)
            {
                dgvTolInfo.Visible = false;
                dgvPartInfo.Visible = false;
                gbOutput.Visible = false;
                gbDesignComments.Visible = false;
                gbGNComments.Visible = false;
            }
            else
            {
                dgvTolInfo.Visible = true;
                dgvPartInfo.Visible = true;
                gbOutput.Visible = true;
                gbDesignComments.Visible = true;
                gbGNComments.Visible = true;
                if (AssNode != null && SubAssNode != null)
                {
                    dgvTolInfo.Rows.Clear();

                    foreach (PartInfo opinfo in opi.m_lsParts)
                    {
                        index = dgvTolInfo.Rows.Add();
                        dgvTolInfo[0, index].Value = (index + 1).ToString();
                        dgvTolInfo[1, index].Value = opinfo.m_sPTNum;
                        dgvTolInfo[2, index].Value = opinfo.m_sUniTolPlus;
                        dgvTolInfo[3, index].Value = opinfo.m_sUniTolMinus;
                    }
                }
               //MessageBox.Show("index = " + index);
            }
            
            if (index >= 0)
            {
                PartInfo objpi = opi.m_lsParts[index];
                
                dgvPartInfo.Rows.Clear();

                string[] sArr1 = { "Part Number Number", "Dash Number", "Part Description", "Part Rev", "Tolerance Status", "Tolerance Source", "Nominal Dimension", "Diametral Tolerance", "Bonus Tolerance" };
                string[] sArr2 = { objpi.m_sPTNum, objpi.m_sDashNum, objpi.m_sPTDesc, objpi.m_sPTRev, objpi.m_sTolStatus, objpi.m_sTolSource, objpi.m_sNominalDim, objpi.m_sDiametricTol, objpi.m_sBonusTol };

                for (int i = 0; i <= sArr1.Length-1; i++)
                {
                    int index1 = dgvPartInfo.Rows.Add();
                    //ssageBox.Show("index1 = "+index1);
                    dgvPartInfo[0, index1].Value = sArr1[index1];
                    dgvPartInfo[1, index1].Value = sArr2[index1];
                }
            }
        }

        private void dgvTolInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;
            string s = dgvTolInfo.Rows[a].Cells[1].Value.ToString();
            dgvPartInfo[1, 0].Value = s;
        }
    }
}