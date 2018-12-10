using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace UBL_Tool
{
    public class Utilities
    {
        
    }
    public class Comments
    {
        //comments of rows A1-A6)
        public string m_sCellA1_AEnd = "";
        public List<Comments> lstCmnts = new List<Comments>();
    }

    public class Description
    {
        //Plane Description of column (H2-H10) and column (I2-I10))
        public string m_sCellH2_HEnd = "";
        public string m_sCellI2_IEnd = "";
        public List<Description> lstDesc = new List<Description>();
    }

    public class Titles
    {
        //Titles of columns(A13-End), (B13-End))
        public string m_sValueA13_AEnd = "";
        public string m_sValueB13_BEnd = "";
        public List<Titles> lstTitles = new List<Titles>();
    }

    public class VariableNames
    {
        //variable names of columns C13-Q13)
        public string m_sC12 = "";
        public string m_sD12 = "";
        public string m_sE12 = "";
        public string m_sF12 = "";
        public string m_sG12 = "";
        public string m_sH12 = "";
        public string m_sI12 = "";
        public string m_sJ12 = "";
        public string m_sK12 = "";
        public string m_sL12 = "";
        public string m_sM12 = "";
        public string m_sN12 = "";
        public string m_sO12 = "";
        public string m_sP12 = "";
        public string m_sQ12 = "";
        public List<VariableNames> lstVNames = new List<VariableNames>();
    }

    public class VariableInfo
    {
        //variable Information of columns C13-Q13)
        public string m_sALT = "";
        public string m_sXM = "";
        public string m_sSEGTIMES = "";
        public string m_sSEGTIMEM = "";
        public string m_sCMLTIMEM = "";
        public string m_sFLAG = "";
        public string m_sDTAMB = "";
        public string m_sHUMD = "";
        public string m_sGHPX = "";
        public string m_sSWB2X = "";
        public string m_sSWB3X = "";
        public string m_sSWB14X = "";
        public string m_sRC = "";
        public string m_sSPARM = "";
        public string m_sPARM = "";
        public List<VariableInfo> lstVInfo = new List<VariableInfo>();
    }

    class MyWinAPI
    {
        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;
        const int WM_CHAR = 0x0102;

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        IntPtr hWnd = FindWindow(null, "Untitled - Notepad");

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)] uint Msg, int wParam, int lParam);

        public static IntPtr cmdHwnd = IntPtr.Zero;

        /// <summary>
        /// Callback method to be used when enumerating windows.
        /// </summary>
        /// <param name="handle">Handle of the next window</param>
        /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
        /// <returns>True to continue the enumeration, false to bail</returns>
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        /// <summary>
        /// Returns a list of child windows
        /// </summary>
        /// <param name="parent">Parent of the windows to return</param>
        /// <returns>List of child windows</returns>
        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }


        /// <summary>
        /// Delegate for the EnumChildWindows method
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
        /// <returns>True to continue enumerating, false to bail.</returns>
        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            // You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        public static void EnterClick(string sMsg)
        {
            try
            {
                foreach (IntPtr child in GetChildWindows(FindWindow(null, "WinPlusConsole")))
                {
                    StringBuilder sb = new StringBuilder(100);
                    GetClassName(child, sb, sb.Capacity);
                    if (sb.ToString() == "PuTTY")
                    {
                        cmdHwnd = child;
                        uint oo;
                        GetWindowThreadProcessId(cmdHwnd, out oo);
                        Process instanceName = Process.GetProcessById((int)oo);
                        if (instanceName.MainWindowTitle.Contains("172.19.193.150 - PuTTY"))
                        {
                            uint wparam = 0 << 29 | 0;
                            string msg = sMsg;
                            int i = 0;
                            for (i = 0; i < msg.Length; i++)
                            {
                                PostMessage(cmdHwnd, WM_CHAR, (int)msg[i], 0);
                            }
                            PostMessage(cmdHwnd, WM_KEYDOWN, (IntPtr)Keys.Enter, (IntPtr)wparam);
                            break;
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}