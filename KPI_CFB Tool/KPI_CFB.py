import wx
import re
import os
import sys
import xlrd
import xlwt
import shutil


class KPI_CFB(wx.Frame):
    def __init__(self):
        self.fpath = ''
        self.cellValue = ''
        # self.nResp = 0

        wx.Frame.__init__(self, None, 1, 'KPI_CFB Tool', size=(580, 150))

        font = wx.Font(wx.FONTSIZE_MEDIUM, wx.FONTFAMILY_DEFAULT, wx.FONTSTYLE_NORMAL, wx.FONTWEIGHT_NORMAL)

        panel = wx.Panel(self)

        self.lbl1 = wx.StaticText(panel, -1, "Select Input Excel File : ", pos=(10, 15))
        self.lbl1.SetFont(font)

        self.txt_fpath = wx.TextCtrl(panel, id=wx.ID_ANY, pos=(175, 13), size=(270, 22))

        btn_brwz = wx.Button(panel, -1, label="Browse", pos=(460, 10), size=(95, 25))
        self.Bind(wx.EVT_BUTTON, self.on_Browse, btn_brwz)

        btn_run = wx.Button(panel, -1, label="Run", pos=(360, 55), size=(95, 35))
        self.Bind(wx.EVT_BUTTON, self.on_Run, btn_run)

        btn_cncl = wx.Button(panel, -1, label="Cancel", pos=(460, 55), size=(95, 35))
        self.Bind(wx.EVT_BUTTON, self.on_Cancel, btn_cncl)

        self.lbl2 = wx.StaticText(panel, -1, "Please Wait.......", pos=(10, 70))
        self.lbl2.SetForegroundColour((0,0,139))
        self.lbl2.Disable()

        self.Center()

    def on_Browse(self, e):
        self.frame1 = wx.Frame(None, -1, 'win.py')
        self.frame1.SetDimensions(0, 0, 200, 50)
        self.openFileDialog = wx.FileDialog(self.frame1, "Open", "", "", "xlsx files (*.xlsx)|*.xlsx",
                                            wx.ID_OPEN | wx.FD_MULTIPLE | wx.FD_CHANGE_DIR | wx.FD_FILE_MUST_EXIST)
        self.openFileDialog.ShowModal()
        self.fpath = self.openFileDialog.GetPath()
        self.txt_fpath.SetValue(self.fpath)
        self.openFileDialog.Destroy()

    def on_Cancel(self, e):
        self.Close()

    def ReplaceSpecialCharactersInFilename(self, name):
        char_list = ["/", "<", ">", "!", "*", "|", ":"]
        re.sub(''.join(char_list), "_", name)
        return name

    def ReadExcelFile(self):
        self.fpath = self.txt_fpath.GetValue()
        # try:
        wb = xlrd.open_workbook('%s' % self.fpath, on_demand=True)
        ws = wb.sheet_by_index(0)
        # print 'Sheet Name = '+str(ws.name)
        # print 'No.of rows = ' + str(ws.nrows)
        # print 'No.of columns = ' + str(ws.ncols)
        for Rw in range(1, ws.nrows):
            oTracker = TrackerInfo
            oTracker.m_sRFQNumber = ws.cell_value(Rw, 0)
            oTracker.m_sQuotationNumber = ws.cell_value(Rw, 1)
            oTracker.m_sDiehlResponsible = ws.cell_value(Rw, 2)
            oTracker.m_sProjectTitle = ws.cell_value(Rw, 3)
            oTracker.m_sOnsite = ws.cell_value(Rw, 4)
            oTracker.m_sOffshore = ws.cell_value(Rw, 5)
            oTracker.m_sSentOn = ws.cell_value(Rw, 6)
            fname = ws.cell_value(Rw, 7)
            oTracker.m_sFileName = self.ReplaceSpecialCharactersInFilename(fname)
            oTracker.m_sFileLocation = ws.cell_value(Rw, 8)
            self.WriteExcelFile(oTracker)
        # except Exception as ee:
        #     exc_type, exc_obj, exc_tb = sys.exc_info()
        #     fname = os.path.split(exc_tb.tb_frame.f_code.co_filename)[1]
        #     print(exc_type, fname, exc_tb.tb_lineno)
        # return lstTrackerInfo

    def WriteExcelFile(self, oTrack):
        # try:
        sTempPath = 'C:\\Users\\ok49802\\Desktop\\KPI-CFB Template.xlsx'
        sNewFileName = oTrack.m_sFileLocation + '\\' + oTrack.m_sFileName + '.xlsx'
        os.path.join(oTrack.m_sFileLocation, oTrack.m_sFileName + '.xlsx')
        # if os.path.exists(self.fpath) and len(oTrack.m_sFileName) > 0:
        #     print '************'
        #     if os.path.exists(sNewFileName):
        #         print '#############'
        #         try:
        #             os.remove(sNewFileName)
        #             print '&&&&&&&&&&&&'
        #         finally:
        #             self.nResp = 1
        '''Now write into Excel file'''
        # if self.nResp == 0:
        shutil.copy(sTempPath, sNewFileName)
        wb = xlrd.open_workbook('%s' % sNewFileName, on_demand=True)
        # cwb = copy(wb)
        ws = wb.sheet_by_index(0)
        workbook = xlwt.Workbook(sNewFileName)
        worksheet = workbook.get_active_sheet

        # worksheet.write(2, 3, oTrack.m_sRFQNumber)
            # worksheet.write('B4', 'B4', oTracker.m_sQuotationNumber)
            # worksheet.write('B5', 'B5', oTracker.m_sDiehlResponsible)
            # worksheet.write('B6', 'B6', oTracker.m_sProjectTitle)
            # worksheet.write('C11', 'C11', oTracker.m_sOnsite)
            # worksheet.write('D11', 'D11', oTracker.m_sOffshore)
            # worksheet.write('B26', 'B26', oTracker.m_sSentOn)
        # workbook.save(sNewFileName)
        # except Exception as ee:
        #     exc_type, exc_obj, exc_tb = sys.exc_info()
        #     fname = os.path.split(exc_tb.tb_frame.f_code.co_filename)[1]
        #     print(exc_type, fname, exc_tb.tb_lineno)
        # return True

    def on_Run(self, e):
        # try:
        if os.path.exists(self.fpath):
            self.lbl2.Enable(True)
            self.lbl2.Refresh()
            self.Refresh()
            '''Read Excel FIle'''
            self.ReadExcelFile()
            # print lstTrackerInfo
            # for obj in lstTrackerInfo:
            #     self.WriteExcelFile(obj)
            # sResp = ''
            # for obj in lstTrackerInfo:
            #     bResp = self.WriteExcelFile(lstTrackerInfo[obj])
            #     if bResp is False:
            #         sResp = sResp + obj.m_sRFQNumber + '\n'
            # if len(sResp) > 0:
            #     print 'Failed to Update the following RFQs'+'\n\n'+sResp
        self.Close()
        # except Exception as ee:
        #     exc_type, exc_obj, exc_tb = sys.exc_info()
        #     fname = os.path.split(exc_tb.tb_frame.f_code.co_filename)[1]
        #     print(exc_type, fname, exc_tb.tb_lineno)


class TrackerInfo:
    m_sRFQNumber = ""
    m_sQuotationNumber = ""
    m_sDiehlResponsible = ""
    m_sProjectTitle = ""
    m_sOnsite = ""
    m_sOffshore = ""
    m_sSentOn = ""
    m_sFileName = ""
    m_sFileLocation = ""
    m_bIsUpdated = False

app = wx.App()
KPI_CFB().Show()
app.MainLoop()