using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;

namespace KPI_CFB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string ReplaceSpecialCharactersInFileName(string fname)
        {
            string[] schar = new string[]{"/", @"\", ":", "*", "?", "\"", "<", ">", "|"};
            for (int i = 0; i < schar.Length; i++)
            {
                if(fname.Contains(schar[i]))
                {
                    fname = fname.Replace(schar[i], "_");
                }
            }
            return fname;
        }
        private List<TrackerInfo> ReadExcelFile()
        {
            List<TrackerInfo> lstTrackerInfo = new List<TrackerInfo>();
            try
            {
                
                //Create COM Objects. Create a COM object for everything that is referenced
                Excel.Application xlApp = new Excel.Application();
                xlApp.Visible = false;
                xlApp.DisplayAlerts = false;
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(tbXlPath.Text.Trim(), Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Excel.Worksheet xlWorksheet = xlWorkbook.Worksheets[1];
                //read the excel file
                for (int nr = 2; nr <= xlWorksheet.UsedRange.Rows.Count + 5; nr++)
                {
                    TrackerInfo oTracker = new TrackerInfo();
                    oTracker.m_sRFQNumber = GetCellValue(xlWorksheet, nr, 1);
                    oTracker.m_sQuotationNumber = GetCellValue(xlWorksheet, nr, 2);
                    oTracker.m_sDiehlResponsible = GetCellValue(xlWorksheet, nr, 3);
                    oTracker.m_sProjectTitle = GetCellValue(xlWorksheet, nr, 4);
                    oTracker.m_sOnsite = GetCellValue(xlWorksheet, nr, 5);
                    oTracker.m_sOffshore = GetCellValue(xlWorksheet, nr, 6);
                    oTracker.m_sSentOn = GetCellValue(xlWorksheet, nr, 7);
                    string fname = GetCellValue(xlWorksheet, nr, 8);
                    oTracker.m_sFileName = ReplaceSpecialCharactersInFileName(fname);
                    oTracker.m_sFileLocation = GetCellValue(xlWorksheet, nr, 9);
                    //end of reading
                    if (oTracker.m_sRFQNumber.Length == 0) break;
                    lstTrackerInfo.Add(oTracker);
                }
                // Close the excel.
                xlWorkbook.Close(false, Type.Missing, Type.Missing);
                Marshal.ReleaseComObject(xlWorksheet);
                Marshal.ReleaseComObject(xlWorkbook);
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            return lstTrackerInfo;
        }
        private bool WriteExcelFile(TrackerInfo oTracker)
        {
            try
            {
                string sTemplatePath = Application.StartupPath + "\\bin\\" + "KPI-CFB Template.xlsx";
                string sNewFileName = oTracker.m_sFileLocation + "\\" + oTracker.m_sFileName + ".xlsx";
                //check for folder exists
                if (System.IO.Directory.Exists(oTracker.m_sFileLocation) && oTracker.m_sFileName.Length > 0)
                {
                    //Delete the file if exists
                    int nResp = 0;
                    if (System.IO.File.Exists(sNewFileName))
                    {
                        try
                        {
                            System.IO.File.Delete(sNewFileName);
                        }
                        catch
                        {
                            nResp = 1;
                        }
                    }
                    //every thing is OK
                    if (nResp == 0)
                    {
                        //copy the file
                        System.IO.File.Copy(sTemplatePath, sNewFileName, true);
                        //open excel instance
                        Excel.Application xlApp = new Excel.Application();
                        xlApp.DisplayAlerts = false;
                        Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(sNewFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        Excel.Worksheet xlWorksheet = xlWorkbook.Worksheets[1];
                        //push the data
                        xlWorksheet.get_Range("B3", "B3").Value2 = oTracker.m_sRFQNumber;
                        xlWorksheet.get_Range("B4", "B4").Value2 = oTracker.m_sQuotationNumber;
                        xlWorksheet.get_Range("B5", "B5").Value2 = oTracker.m_sDiehlResponsible;
                        xlWorksheet.get_Range("B6", "B6").Value2 = oTracker.m_sProjectTitle;
                        xlWorksheet.get_Range("C11", "C11").Value2 = oTracker.m_sOnsite;
                        xlWorksheet.get_Range("D11", "D11").Value2 = oTracker.m_sOffshore;
                        xlWorksheet.get_Range("B26", "B26").Value2 = oTracker.m_sSentOn;
                        //save & close
                        xlWorkbook.Save();
                        xlWorkbook.Close(true);
                        //Memory release
                        Marshal.ReleaseComObject(xlWorksheet);
                        Marshal.ReleaseComObject(xlWorkbook);
                        xlApp.Quit();
                        Marshal.ReleaseComObject(xlApp);
                        return true;
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            return false;
        }

        private string GetCellValue(Excel._Worksheet xlWorksheet, int nRw, int nCl)
        {
            string sCellValue = "";
            try
            {
                if (xlWorksheet.Cells[nRw, nCl] != null && xlWorksheet.Cells[nRw, nCl].Value2 != null)
                {
                    sCellValue = xlWorksheet.Cells[nRw, nCl].Value2.ToString();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            return sCellValue.Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string fpath = "";
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Excel File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "(*.xlsx*)|*.xlsx*";
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                tbXlPath.Text = fdlg.FileName;
                //fpath = fdlg.FileName;
            }
        }

        private void btn_cncl_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(tbXlPath.Text.Trim()))
                {
                    //please wait label
                    lblWait.Visible = true;
                    lblWait.Refresh();
                    this.Refresh();
                    //read excel file
                    List<TrackerInfo> lstTrackerInfo = ReadExcelFile();
                    //write the data
                    string sResp = "";
                    for(int i=0;i<lstTrackerInfo.Count;i++)
                    //foreach (TrackerInfo oTracker in lstTrackerInfo)
                    {
                        bool bResp = WriteExcelFile(lstTrackerInfo[i]);
                        if (bResp == false)
                        {
                            sResp = sResp + lstTrackerInfo[i].m_sRFQNumber + "\n";
                        }
                    }
                    //Final statement
                    if (sResp.Trim().Length > 0)
                    {
                        MessageBox.Show("Failed to update the following RFQ's" + "\n\n" + sResp);
                    }
                    else
                    {
                        MessageBox.Show("Successfully updated all the data");
                    }
                    this.Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                lblWait.Visible = false;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
    public class TrackerInfo
    {
        public string m_sRFQNumber = "";
        public string m_sQuotationNumber = "";
        public string m_sDiehlResponsible = "";
        public string m_sProjectTitle = "";
        public string m_sOnsite = "";
        public string m_sOffshore = "";
        public string m_sSentOn = "";
        public string m_sFileName = "";
        public string m_sFileLocation = "";
        public bool m_bIsUpdated = false;
    }
}
