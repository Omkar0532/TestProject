using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace UBL_Tool
{
    class Utility
    {
        static int myCnt = 0;
        public static string m_sTempPath = "";
        public static string m_sBinPath = "";
        public static string m_sOutputFolderPath = "";

        //*******************************************************
        //Function  : GetTempDir
        //Purpose   : Get the temporaory directory
        //*******************************************************
        public static void GetTempDir()
        {
            //Get temp Directory
            string path = null;
            try
            {
                //Get the TEMP Path
                path = System.Environment.GetEnvironmentVariable("TEMP");
                if (path == null || path.Length < 1)//Not found
                {
                    //Get the TMP path
                    path = System.Environment.GetEnvironmentVariable("TMP");
                    if (path == null || path.Length < 1)//Not found
                    {
                        path = System.Windows.Forms.Application.ExecutablePath.ToString();
                    }
                }
                m_sTempPath = path;
            }
            catch { }
        }
        //*******************************************************
        //Function  : WriteErrorLog
        //Purpose   : Write the error data to log
        //*******************************************************
        public static void WriteErrorLog(string sWarning, string sStackTrace, string sMsg)
        {
            try
            {
                if (sWarning.Trim().Length > 0)
                    MessageBox.Show(sWarning.Trim());
                string sLogFilepath = m_sTempPath + "\\" + "UBL_ErrorLog.dat";
                if (sLogFilepath != null && sLogFilepath.Length > 0)
                {
                    try
                    {
                        //Write the Error Log
                        StreamWriter sw;
                        if (myCnt == 0)//First Error
                        {
                            sw = System.IO.File.CreateText(sLogFilepath);
                            DateTime date = DateTime.Now;
                            sw.WriteLine("********** UBL Tool *********");
                            sw.WriteLine("Time                  : " + date.ToString());
                            sw.WriteLine("******************************");
                        }
                        else//Next Errors Append
                        {
                            sw = System.IO.File.AppendText(sLogFilepath);
                        }
                        //----- This is for error
                        if (sWarning.Length > 0 || sStackTrace.Length > 0)
                        {
                            sw.WriteLine("-----------------");
                            if (sWarning.Length > 0)
                                sw.WriteLine("Warning     : " + sWarning);//Write the Error Message
                            if (sStackTrace.Length > 0)
                                sw.WriteLine("StackTrace  : " + sStackTrace);//Write the StackTrace
                            sw.WriteLine("-----------------");
                        }
                        else if (sMsg.Length > 0)
                        {
                            sw.WriteLine(sMsg);
                        }
                        //close
                        sw.Close();
                        myCnt++;
                    }
                    catch
                    { }
                }
            }
            catch { }
        }
        //*******************************************************
        //Function  : WriteErrorLog
        //Purpose   : Write the error data to log
        //*******************************************************
        public static void WriteErrorLog(Exception ee)
        {
            try
            {
                if (ee != null)
                {
                    WriteErrorLog(ee.Message, ee.StackTrace, "");
                }
            }
            catch { }
        }
        //*******************************************************
        //Function  : WarnUser
        //Purpose   : Warn the user
        //*******************************************************
        public static void WarnUser(string smsg)
        {
            try
            {
                //show
                MessageBox.Show(smsg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }
        //*******************************************************
        //Function  : InformationUser
        //Purpose   : information message box to user
        //*******************************************************
        public static void InformationUser(string smsg)
        {
            try
            {
                //show
                MessageBox.Show(smsg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }
        public static string FileSaveDialog(string sTitle, string sFilter)
        {
            string sOPPath = "";
            try
            {
                //Ask for output file selection---
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.InitialDirectory = @"C:\";
                saveFileDialog1.Title = sTitle;
                saveFileDialog1.CheckFileExists = false;
                saveFileDialog1.CheckPathExists = true;
                saveFileDialog1.DefaultExt = "txt";
                saveFileDialog1.Filter = sFilter;
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    sOPPath = saveFileDialog1.FileName;
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
            return sOPPath;
        }
        public static string FileOpenDialog(string sHeader, string sType)
        {
            string sSelectedFile = "";
            try
            {
                //Ask for the HTML file
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.Title = sHeader;
                openFileDialog1.CheckPathExists = true;
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = sType;
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    sSelectedFile = openFileDialog1.FileName;
                }
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);//Write error log
            }
            return sSelectedFile;
        }

        public static void DeleteFile(string sPath)
        {
            try
            {
                if (sPath.Length > 0)
                {
                    if (System.IO.File.Exists(sPath))
                    {
                        System.IO.File.Delete(sPath);
                    }
                }
            }
            catch (Exception ee)
            {
                WriteErrorLog(ee);
            }
        }
        public static int GetInteger(string sVal)
        {
            try
            {
                if (sVal.Trim().Length > 0)
                {
                    return int.Parse(sVal.Trim());
                }
            }
            catch
            {
            }
            return 0;
        }
        public static bool GetInteger(string sVal, out int nValue)
        {
            nValue = -1;
            try
            {
                if (sVal.Trim().Length > 0)
                {
                    int nV = 0;
                    if (int.TryParse(sVal, out nV) == true)
                    {
                        nValue = nV;
                        return true;
                    }
                }
            }
            catch (Exception ee)
            {
                WriteErrorLog(ee);
            }
            return false;
        }
        public static bool GetReal(string sVal, ref double dValue)
        {
            try
            {
                sVal = sVal.Replace(",", ".").Trim();
                if (sVal.Length > 0)
                {
                    double dV = 0.0;
                    System.Globalization.NumberStyles style = System.Globalization.NumberStyles.Number;
                    System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                    if (double.TryParse(sVal, style, culture, out dV))
                    {
                        dValue = dV;
                        return true;
                    }
                }
            }
            catch (Exception ee)
            {
                WriteErrorLog(ee);
            }
            return false;
        }
        public static double GetReal(string sVal)
        {
            double dValue = 0.0;
            try
            {
                sVal = sVal.Replace(",", ".").Trim();
                if (sVal.Length > 0)
                {
                    double dV = 0.0;
                    System.Globalization.NumberStyles style = System.Globalization.NumberStyles.Number;
                    System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                    if (double.TryParse(sVal, style, culture, out dV))
                    {
                        dValue = dV;
                    }
                }
            }
            catch (Exception ee)
            {
                WriteErrorLog(ee);
            }
            return dValue;
        }
    }
}