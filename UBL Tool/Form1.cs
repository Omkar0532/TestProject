using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Powerpoint = Microsoft.Office.Interop.PowerPoint;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32;

namespace UBL_Tool
{
    public partial class Form1 : Form
    {
        string m_sPuttyExe = "", m_sPSCPpath = "";
        public Form1()
        {
            InitializeComponent();
            lbStatus.Visible = false;
            this.CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                GetPuttyExePath();
                if (m_sPuttyExe.Length == 0 || m_sPSCPpath.Length == 0)
                {
                    Utility.WarnUser("PuTTy is not installed in the system..cant run UBL tool");
                    this.Close();
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }
        
        //On Clicking Browse button
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fdlg = new OpenFileDialog();
                fdlg.Title = "Excel File Dialog";
                fdlg.InitialDirectory = @"c:\";
                fdlg.Filter = "(*.xlsx*)|*.xlsx*";
                fdlg.RestoreDirectory = true;
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    tbInputFile.Text = fdlg.FileName;
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }

        //On Clicking Run button
        private void btRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(tbInputFile.Text.Trim()) && tbInputFile.Text.Trim().Length > 0)
                {
                    lbStatus.Visible = true;
                    lbStatus.Text = "Reading Excel file....";
                    lbStatus.Refresh();
                    this.Refresh();

                    //create working directory
                    string sFolder = System.IO.Path.GetDirectoryName(tbInputFile.Text.Trim());
                    Utility.m_sOutputFolderPath = sFolder + "\\PSA\\";
                    if (System.IO.Directory.Exists(Utility.m_sOutputFolderPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(Utility.m_sOutputFolderPath);
                    }
                    //copying the unigraph supporting files
                    string[] sArrFiles = System.IO.Directory.GetFiles(Utility.m_sBinPath + "\\unigraph", "*.*");
                    foreach (string sFile in sArrFiles)
                    {
                        string opFile = Utility.m_sOutputFolderPath + System.IO.Path.GetFileName(sFile);
                        System.IO.File.Copy(sFile, opFile, true);
                    }
                    
                    //Read the excel file
                    string sruninFileName = ReadExcelFile();

                    //Writing into Plots_V1.cmdfile
                    string[] sArrCMDFile = System.IO.Directory.GetFiles(Utility.m_sOutputFolderPath, "*.cmdfile");
                    foreach (string sCMDfile in sArrCMDFile)
                    {
                        if (File.Exists(sCMDfile))
                        {
                            string text = File.ReadAllText(sCMDfile);
                            text = text.Replace("zzzzz", Utility.m_sOutputFolderPath);
                            File.WriteAllText(sCMDfile, text);

                            string text1 = File.ReadAllText(sCMDfile);
                            text1 = text1.Replace("XXXXX", sruninFileName);
                            File.WriteAllText(sCMDfile, text1);
                        }
                        else
                        {
                            Utility.WarnUser("Unigraph supporting file doesn't exists");
                        }
                    }

                    if (sruninFileName.Length > 0)
                    {
                        //Copy file from windows to linux
                        System.Threading.Thread.Sleep(3000);
                        WindowsToLinux(sruninFileName);
                        //Execuate soap
                        System.Threading.Thread.Sleep(3000);
                        ExecuteSOAPP(sruninFileName);
                        //Copy file from linux to Windows
                        System.Threading.Thread.Sleep(3000);
                        LinuxToWindows(sruninFileName);
                        //Check for failed outputs in .sunout file
                        System.Threading.Thread.Sleep(5000);
                        CheckFailedOuputs(sruninFileName);
                        //Run Unigraf
                        System.Threading.Thread.Sleep(3000);
                        ExecuteUnigraph();
                        //Create ppt
                        System.Threading.Thread.Sleep(15000);
                        CreatePPT(sruninFileName);
                        //Closing the application
                        this.Close();
                    }
                }
                else
                {
                    Utility.WarnUser("Please select excel file");
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }

        //On Clicking Cancel button
        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //To get particular cell value
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
                Utility.WriteErrorLog(ee);
            }
            return sCellValue.Trim();
        }

        //Reading an Excel file
        private string ReadExcelFile()
        {
            string sruninFileName = "";
            try
            {
                Comments ocmnts = new Comments();
                Description odesc = new Description();
                Titles otitles = new Titles();
                VariableNames ovnames = new VariableNames();
                VariableInfo ovinfo = new VariableInfo();

                //Creating COM Objects
                Excel.Application xlApp = new Excel.Application();
                xlApp.Visible = false;
                xlApp.DisplayAlerts = false;
                Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(tbInputFile.Text.Trim(), Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Excel.Worksheet xlWorksheet = xlWorkbook.Worksheets[2];

                int range = xlWorksheet.UsedRange.Rows.Count;

                //Reading Comments of rows(A1-A6)
                for (int rw = 1; rw <= 6; rw++)
                {
                    Comments oCmnts = new Comments();
                    oCmnts.m_sCellA1_AEnd = GetCellValue(xlWorksheet, rw, 1);
                    if (oCmnts.m_sCellA1_AEnd.Length == 0)
                        break;
                    ocmnts.lstCmnts.Add(oCmnts);
                }

                //Reading Description of rows(H2-H10) and rows(I2-I10)
                for (int nRw = 2; nRw <= 10; nRw++)
                {
                    Description oDesc = new Description();
                    oDesc.m_sCellH2_HEnd = GetCellValue(xlWorksheet, nRw, 8);
                    oDesc.m_sCellI2_IEnd = GetCellValue(xlWorksheet, nRw, 9);
                    if (oDesc.m_sCellH2_HEnd.Length == 0 || oDesc.m_sCellI2_IEnd.Length == 0)
                        break;
                    odesc.lstDesc.Add(oDesc);
                }

                //Reading Titles of rows (A13 to AEnd) and (B13 to BEnd)
                for (int r = 13; r <= range; r++)
                {
                    Titles oTitles = new Titles();
                    oTitles.m_sValueA13_AEnd = GetCellValue(xlWorksheet, r, 1);
                    oTitles.m_sValueB13_BEnd = GetCellValue(xlWorksheet, r, 2);
                    if (oTitles.m_sValueA13_AEnd.Length == 0)
                        break;
                    otitles.lstTitles.Add(oTitles);
                }

                //Reading Variable names of columns (C12-Q12)
                VariableNames oVNames = new VariableNames();
                oVNames.m_sC12 = GetCellValue(xlWorksheet, 12, 3);
                oVNames.m_sD12 = GetCellValue(xlWorksheet, 12, 4);
                oVNames.m_sE12 = GetCellValue(xlWorksheet, 12, 5);
                oVNames.m_sF12 = GetCellValue(xlWorksheet, 12, 6);
                oVNames.m_sG12 = GetCellValue(xlWorksheet, 12, 7);
                oVNames.m_sH12 = GetCellValue(xlWorksheet, 12, 8);
                oVNames.m_sI12 = GetCellValue(xlWorksheet, 12, 9);
                oVNames.m_sJ12 = GetCellValue(xlWorksheet, 12, 10);
                oVNames.m_sK12 = GetCellValue(xlWorksheet, 12, 11);
                oVNames.m_sL12 = GetCellValue(xlWorksheet, 12, 12);
                oVNames.m_sM12 = GetCellValue(xlWorksheet, 12, 13);
                oVNames.m_sN12 = GetCellValue(xlWorksheet, 12, 14);
                oVNames.m_sO12 = GetCellValue(xlWorksheet, 12, 15);
                oVNames.m_sP12 = GetCellValue(xlWorksheet, 12, 16);
                oVNames.m_sQ12 = GetCellValue(xlWorksheet, 12, 17);
                ovnames.lstVNames.Add(oVNames);

                lbStatus.Text = "Writing into text file...";

                //Reading VariableInfo of Columns(C12-Q12) till end of rows
                for (int r = 13; r <= range; r++)
                {
                    VariableInfo oVInfo = new VariableInfo();
                    oVInfo.m_sALT = GetCellValue(xlWorksheet, r, 3);
                    oVInfo.m_sXM = GetCellValue(xlWorksheet, r, 4);
                    oVInfo.m_sSEGTIMES = GetCellValue(xlWorksheet, r, 5);
                    oVInfo.m_sSEGTIMEM = GetCellValue(xlWorksheet, r, 6);
                    oVInfo.m_sCMLTIMEM = GetCellValue(xlWorksheet, r, 7);
                    oVInfo.m_sFLAG = GetCellValue(xlWorksheet, r, 8);
                    oVInfo.m_sDTAMB = GetCellValue(xlWorksheet, r, 9);
                    oVInfo.m_sHUMD = GetCellValue(xlWorksheet, r, 10);
                    oVInfo.m_sGHPX = GetCellValue(xlWorksheet, r, 11);
                    oVInfo.m_sSWB2X = GetCellValue(xlWorksheet, r, 12);
                    oVInfo.m_sSWB3X = GetCellValue(xlWorksheet, r, 13);
                    oVInfo.m_sSWB14X = GetCellValue(xlWorksheet, r, 14);
                    oVInfo.m_sRC = GetCellValue(xlWorksheet, r, 15);
                    oVInfo.m_sSPARM = GetCellValue(xlWorksheet, r, 16);
                    oVInfo.m_sPARM = GetCellValue(xlWorksheet, r, 17);
                    if (oVInfo.m_sALT.Length == 0)
                        break;
                    ovinfo.lstVInfo.Add(oVInfo);
                }

                //Reading Text File Name
                if (xlWorksheet.get_Range("I1", "I1").Value2 != null)
                {
                    sruninFileName = xlWorksheet.get_Range("I1", "I1").Value2;
                }
                //close excel
                xlWorkbook.Close(false, Type.Missing, Type.Missing);
                Marshal.ReleaseComObject(xlWorksheet);
                Marshal.ReleaseComObject(xlWorkbook);
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);

                //Wrire the text file--
                if (sruninFileName.Length > 0)
                {
                    WriteTextFile(sruninFileName, ocmnts, odesc, otitles, ovnames, ovinfo);
                }
                else
                {
                    Utility.WarnUser("In Excell sheet, Cell I1 doesn't have any value");
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
            return sruninFileName;
        }

        //Reading Text file and Writing into Text file
        private void WriteTextFile(string sruninFileName, Comments ocmnts, Description odesc, Titles otitles, VariableNames ovnames, VariableInfo ovinfo)
        {
            try
            {
                string sSOAPPTextFilePath = Application.StartupPath + "\\bin\\" + "Standard_Header_SOAPP.runin";
                string sFilePath = Utility.m_sOutputFolderPath;
                string sNewTextFilePath = sFilePath + "" + sruninFileName + ".runin";
                                              
                if (System.IO.File.Exists(sSOAPPTextFilePath))
                {
                    System.IO.File.Copy(sSOAPPTextFilePath, sNewTextFilePath, true);

                    using (StreamWriter sw = File.AppendText(sNewTextFilePath))
                    {
                        //Writing Comments of Column (A1-A6)
                        for (int i = 0; i != ocmnts.lstCmnts.Count; i++ )
                        {
                            sw.WriteLine("$CMT " + ocmnts.lstCmnts[i].m_sCellA1_AEnd.Trim());
                        }
                        sw.WriteLine(" ");
                        
                        //Writing Plane Description of column (H2-H10) and (I2-I10)
                        sw.WriteLine("$CMT Ariplane, Rating, Model and Inlet Recovery");
                        sw.WriteLine("$SET");
                        for (int i = 0; i != odesc.lstDesc.Count; i++)
                        {
                            sw.WriteLine("   " + odesc.lstDesc[i].m_sCellH2_HEnd.Trim() + " = " + odesc.lstDesc[i].m_sCellI2_IEnd.Trim() + ".,");
                        }
                        sw.WriteLine("$END");
                        sw.WriteLine(" ");
                        
                        for (int i = 0; i != otitles.lstTitles.Count; i++)
                        {
                            sw.WriteLine("$SET CMNT=1., $END");
                            
                            //Writing Titles of columns(A13-End), (B13-End))
                            sw.WriteLine(otitles.lstTitles[i].m_sValueA13_AEnd.Trim() + " - " + otitles.lstTitles[i].m_sValueB13_BEnd.Trim());
                                                        
                            //Writing variable Information of columns C13-Q13) and with variable names of row (C12-Q12)
                            sw.WriteLine("$RUN");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sC12.Trim() + " = " + ovinfo.lstVInfo[i].m_sALT.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sD12.Trim() + " = " + ovinfo.lstVInfo[i].m_sXM.Trim() + ",");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sE12.Trim() + " = " + ovinfo.lstVInfo[i].m_sSEGTIMES.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sF12.Trim() + " = " + ovinfo.lstVInfo[i].m_sSEGTIMEM.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sG12.Trim() + " = " + ovinfo.lstVInfo[i].m_sCMLTIMEM.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sH12.Trim() + " = " + ovinfo.lstVInfo[i].m_sFLAG.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sI12.Trim() + " = " + ovinfo.lstVInfo[i].m_sDTAMB.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sJ12.Trim() + " = " + ovinfo.lstVInfo[i].m_sHUMD.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sK12.Trim() + " = " + ovinfo.lstVInfo[i].m_sGHPX.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sL12.Trim() + " = " + ovinfo.lstVInfo[i].m_sSWB2X.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sM12.Trim() + " = " + ovinfo.lstVInfo[i].m_sSWB3X.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sN12.Trim() + " = " + ovinfo.lstVInfo[i].m_sSWB14X.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sO12.Trim() + " = " + ovinfo.lstVInfo[i].m_sRC.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sP12.Trim() + " = " + ovinfo.lstVInfo[i].m_sSPARM.Trim() + ".,");
                            sw.WriteLine("   " + ovnames.lstVNames[0].m_sQ12.Trim() + " = " + ovinfo.lstVInfo[i].m_sPARM.Trim() + ".,");
                            sw.WriteLine("$END");
                            sw.WriteLine(" ");
                        }
                        lbStatus.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("There is no file in given path " + sSOAPPTextFilePath);
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }
        
        //Command to copy files to linux system
        public void WindowsToLinux(string sruninFileName)
        {
            try
            {
                string sNewTextFilePath = Utility.m_sOutputFolderPath + "" + sruninFileName + ".runin";
                string runin = @"/C" + "\"" + m_sPSCPpath + @"\pscp.exe" + "\"" + " -pw tech004 " + sNewTextFilePath + " info004@172.19.193.150:/home/info004/UBL_PSA/" + sruninFileName + ".runin";
                Process.Start(Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe", runin);
                
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }

        //Commands to copy .ubin and .sunout files from linux system
        public void LinuxToWindows(string sruninFileName)
        {
            try
            {
                string ubin = @"/C" + "\"" + m_sPSCPpath + @"\pscp.exe" + "\"" + @" -pw tech004 info004@172.19.193.150:/home/info004/UBL_PSA/" + sruninFileName + ".ubin" + " " + Utility.m_sOutputFolderPath + "" + sruninFileName + ".ubin";
                Process.Start(Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe", ubin);

                string sunout = @"/C" + "\"" + m_sPSCPpath + @"\pscp.exe" + "\"" + @" -pw tech004 info004@172.19.193.150:/home/info004/UBL_PSA/" + sruninFileName + ".sunout" + " " + Utility.m_sOutputFolderPath + "" + sruninFileName + ".sunout";
                Process.Start(Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe", sunout);
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }

        }

        //To execute .runin file with putty in linux system
        public void ExecuteSOAPP(string sruninFileName)
        {
            try
            {
                string sPuttyExe = @"/C" + "\"" + m_sPuttyExe + "\"" + " info004@172.19.193.150 -pw tech004";
                Process process = new Process();
                ProcessStartInfo procStartInfo = new ProcessStartInfo(Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe", sPuttyExe);
                process.StartInfo = procStartInfo;
                procStartInfo.UseShellExecute = false;
                process.Start();

                System.Threading.Thread.Sleep(2000);
                MyWinAPI.EnterClick("cd UBL_PSA");
                
                System.Threading.Thread.Sleep(2000);
                MyWinAPI.EnterClick("use soapp");

                System.Threading.Thread.Sleep(2000);
                string sSoappCommand = @"epslite -i /pw/devl/share/model/src/panels/prod/s302-select1_scn21_pt2564_concat.PANEL \ -R /home/info004/UBL_PSA/" + sruninFileName + ".runin" + @" \ -O " + sruninFileName;
                MyWinAPI.EnterClick(sSoappCommand);

                System.Threading.Thread.Sleep(5000);
                MyWinAPI.EnterClick("exit");
                //process.WaitForExit();
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }

        //Checking if any failed outputs in .sunout file
        private void CheckFailedOuputs(string sruninFileName)
        {
            try
            {
                int counter = 1;
                //int[] count;
                string line = "";
                string fpath = @"C:\Users\ok49802\Desktop\Omkar\putty.txt";//GetSunoutFile(sruninFileName);
                if (System.IO.File.Exists(fpath))
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(fpath);
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains("Failed") || line.Contains("failed") || line.Contains("FAILED"))
                        {
                            Utility.WarnUser("Error at line " + counter + " : " +line);
                        }
                        counter++;
                    }
                    file.Close();
                }
                else
                {
                    Utility.WarnUser(".sunout file doesn't exists while checking for failed outputs");
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }

        //Executing .ubin file in Unigraph to generate .gif files
        private void ExecuteUnigraph()
        {
            try
            {
                string command = Utility.m_sOutputFolderPath + "unigraph.bat";
                if (File.Exists(command))
                {
                    //Replacing the string with path to execute the unigraph in working directory
                    string text = File.ReadAllText(command);
                    text = text.Replace("zzzzz", Utility.m_sOutputFolderPath);
                    File.WriteAllText(command, text);

                    var proc1 = new ProcessStartInfo();
                    proc1.UseShellExecute = true;
                    proc1.WorkingDirectory = @"C:\Windows\System32";
                    proc1.FileName = @"C:\Windows\System32\cmd.exe";
                    proc1.Verb = "runas";
                    proc1.Arguments = "/C" + command;
                    //proc1.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(proc1);
                }
                else
                {
                    Utility.WarnUser("Unigraph supporting file doesn't exists");
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }

        //To create ppt with generated .gif files
        private void CreatePPT(string sruninFileName)
        {
            string sTemplatePPTPath = Utility.m_sBinPath + "Template.pptm";
            string sNewPPTPath = Utility.m_sOutputFolderPath + "" + sruninFileName + ".pptm";
            try
            {
                string[] sFiles = System.IO.Directory.GetFiles(Utility.m_sOutputFolderPath, "*.gif");
                if (System.IO.File.Exists(sTemplatePPTPath))
                {
                    System.IO.File.Copy(sTemplatePPTPath, sNewPPTPath, true);
                    Powerpoint.Application pptApp = new Powerpoint.Application();
                    Powerpoint.Presentation presentation = pptApp.Presentations.Open(sNewPPTPath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue);
                    Powerpoint.Slides oSlides = presentation.Slides;
                    for (int i = 0; i < sFiles.Length; i++)
                    {
                        int slide_index = i + 2;
                        Powerpoint.Slide oSlide = (Powerpoint.Slide)presentation.Slides._Index(slide_index);
                        Powerpoint.CustomLayout oLayout = (Powerpoint.CustomLayout)oSlide.CustomLayout;
                        pptApp.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
                        Powerpoint.Slide oNewSlide = presentation.Slides.AddSlide(slide_index + 1, oLayout);
                        oNewSlide.Shapes.AddPicture(sFiles[i], Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, 0, 86, 688, 401);
                    }
                    presentation.Save();
                }
                else
                {
                    MessageBox.Show(sTemplatePPTPath + " file doesn't exists in this path");
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }

        //To get putty.exe installation location
        public void GetPuttyExePath()
        {
            m_sPuttyExe = "";
            m_sPSCPpath = "";
            try
            {
                string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
                {
                    foreach (string skName in rk.GetSubKeyNames())
                    {
                        using (RegistryKey sk = rk.OpenSubKey(skName))
                        {
                            try
                            {
                                var displayName = sk.GetValue("DisplayName");
                                var oPutty = sk.GetValue("DisplayIcon");
                                var oPSCPpath = sk.GetValue("Inno Setup: App Path");
                                if (displayName != null)
                                {
                                    if (displayName.ToString().Contains("PuTTY"))
                                    {
                                        if (oPutty != null)
                                        {
                                            m_sPuttyExe = oPutty.ToString();
                                        }
                                        if (oPSCPpath != null)
                                        {
                                            m_sPSCPpath = oPSCPpath.ToString();
                                        }
                                        break;
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }

        //To get .sunout file
        public string GetSunoutFile(string sruninFileName)
        {
            string s_sunoutPath = "";
            try
            {
                string fname = "";
                string[] s_sunoutFiles = Directory.GetFiles(Utility.m_sOutputFolderPath, "*.sunout");
                for (int i = 0; i <= s_sunoutFiles.Length - 1; i++)
                {
                    fname = Path.GetFileName(s_sunoutFiles[i]);
                    if (fname.Equals(sruninFileName + ".sunout"))
                    {
                        s_sunoutPath = Utility.m_sOutputFolderPath + "" + fname;
                    }
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
            return s_sunoutPath;
        }
    }
}