using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UBL_Tool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //get environment
            GetEnvironment();
            //Write log
            Utility.WriteErrorLog("", "", "");
            try
            {
                Application.Run(new Form1());
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }
        static void GetEnvironment()
        {
            try
            {
                //get temp dir
                Utility.GetTempDir();
                //get bin path
                string sDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                Utility.m_sBinPath = sDir + "\\bin\\";
            }
            catch (Exception ee)
            {
                Utility.WriteErrorLog(ee);
            }
        }
    }
}