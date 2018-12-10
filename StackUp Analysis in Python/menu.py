import os
import wx
# from Assembly_Name import Assembly_Name
# from DialogueForm import DialogueForm
from TreeScreen import TreeFrame_Dialogue


class Menu(wx.Frame):
    def __init__(self, *args, **kwargs):
        super(Menu, self).__init__(*args, **kwargs)

        self.menubar = wx.MenuBar()
        self.toolbar = self.CreateToolBar()
        self.fileMenu = wx.Menu()
        self.feedbackMenu = wx.Menu()
        self.aboutMenu = wx.Menu()
        self.CreateStatusBar()

        new_bitmap = wx.Bitmap('C:\\Users\\ok49802\\Desktop\\Omkar\\11-09-2018\\abstract\\plus_blue.png')
        open_bitmap = wx.Bitmap('C:\\Users\\ok49802\\Desktop\\Omkar\\11-09-2018\\abstract\\open-file-icon.png')

        new_image = self.toolbar.AddTool(wx.ID_ANY, new_bitmap)
        open_image = self.toolbar.AddTool(wx.ID_ANY, open_bitmap)
        self.toolbar.Realize()

        self.Bind(wx.EVT_TOOL, self.OnOpen, new_image)
        self.Bind(wx.EVT_TOOL, self.onSaveFile, open_image)

        new=self.fileMenu.Append(wx.ID_NEW, '&New\tCtrl+N', "To create a new file")
        self.fileMenu.Bind(wx.EVT_MENU , self.onSaveFile, new)

        open_file = self.fileMenu.Append(wx.ID_OPEN, '&Open\tCtrl+O', "To open a file")
        self.fileMenu.Bind(wx.EVT_MENU, self.OnOpen, open_file)

        self.fileMenu.AppendSeparator()

        exit_file = self.qmi = wx.MenuItem(self.fileMenu, wx.ID_EXIT, '&Quit\tCtrl+W', "Exiting")
        self.fileMenu.AppendItem(self.qmi)
        self.fileMenu.Bind(wx.EVT_MENU, self.OnQuit, exit_file)

        self.menubar.Append(self.fileMenu, '&File')
        self.SetMenuBar(self.menubar)

        self.feedbackMenu.Append(wx.ID_INFO, '&Feedback',"give me feedback")
        self.feedbackMenu.Append(wx.ID_HELP, '&Help',"how can i help you")
        self.feedbackMenu.AppendSeparator()

        self.menubar.Append(self.feedbackMenu, '&Feedback')
        self.SetMenuBar(self.menubar)

        about = self.aboutMenu.Append(wx.ID_ABOUT, "&About", " Information about this program")
        self.menubar.Append(self.aboutMenu, '&About')
        self.SetMenuBar(self.menubar)
        self.Bind(wx.EVT_MENU, self.OnAbout, about)

        self.SetSize((1200, 950))
        self.SetTitle('Menu')
        self.Centre()
        self.Show()

    def OnOpen(self,e):
        self.frame1 = wx.Frame(None, -1, 'win.py')
        self.frame1.SetDimensions(0, 0, 200, 50)
        self.openFileDialog = wx.FileDialog(self.frame1, "Open", "", "","Python files (*.py)|*.py",wx.ID_OPEN | wx.FD_MULTIPLE | wx.FD_CHANGE_DIR | wx.FD_FILE_MUST_EXIST)
        self.openFileDialog.ShowModal()
        print(self.openFileDialog.GetPath())
        self.openFileDialog.Destroy()

    def onSaveFile(self,e):
        self.currentDirectory = os.getcwd()
        self.wildcard = "Python source (*.py)|*.py|" \
            "All files (*.*)|*.*"
        dlg = wx.FileDialog(self, "Save file as ...", "", self.currentDirectory, self.wildcard, wx.FD_SAVE)
        if dlg.ShowModal() == wx.ID_OK:
            path = dlg.GetPath()
            print "You chose the following filename: %s" % path
        dlg.Destroy()
        # Assembly_Name(self)
        self.Close()
        # DialogueForm(self)

    def OnQuit(self, e):
        self.Close()

    def OnAbout(self,e):
        dlg = wx.MessageDialog(self, "A small text editor", "About Sample Editor", wx.OK | wx.ICON_INFORMATION)
        dlg.ShowModal()
        dlg.Destroy()

app = wx.App()
Menu(None)
app.MainLoop()