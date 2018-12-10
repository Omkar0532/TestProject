import sys, os
import wx
from DialogueForm import DialogueForm
# from DataTransfer import Data_Transfer


def MsgDlg(window, string, caption='wxProject', style=wx.YES_NO|wx.CANCEL):
    """Common MessageDialog."""
    dlg = wx.MessageDialog(window, string, caption, style)
    result = dlg.ShowModal()
    dlg.Destroy()
    return result


class TreeFrame_Dialogue(wx.Frame):

    def __init__(self, parent, id, title):
        wx.Frame.__init__(self, parent, id, title, wx.DefaultPosition, wx.Size(1200, 950),
                          style = wx.DEFAULT_FRAME_STYLE | wx.NO_FULL_REPAINT_ON_RESIZE)

        self.root = ''
        self.parent = ''
        self.child = ''
        '''################################################Menu Bar##################################################'''
        self.main_menu = wx.MenuBar()
        '''-------------------------------------------------File Menu------------------------------------------------'''
        self.file_menu = wx.Menu()
        new_item = self.file_menu.Append(wx.ID_NEW, '&New', 'New project')
        self.file_menu.Bind(wx.EVT_MENU, self.onSaveFile, new_item)
        open_item = self.file_menu.Append(wx.ID_OPEN, '&Open', 'Open project')
        self.file_menu.Bind(wx.EVT_MENU, self.OnOpen, open_item)
        save_item = self.file_menu.Append(wx.ID_ANY, '&Save', 'Save the file')
        self.file_menu.Bind(wx.EVT_MENU, self.onSaveFile, save_item)
        saveas_item = self.file_menu.Append(wx.ID_ANY, '&SaveAs', 'Save the file as user given name')
        self.file_menu.Bind(wx.EVT_MENU, self.onSaveFile, saveas_item)
        exit_item = self.file_menu.Append(wx.ID_EXIT, '&Exit', 'Exit program')
        self.file_menu.Bind(wx.EVT_MENU, self.OnQuit, exit_item)
        self.main_menu.Append(self.file_menu, '&File')

        '''-------------------------------------------------Edit Menu------------------------------------------------'''
        self.edit_menu = wx.Menu()
        add_item = self.edit_menu.Append(wx.ID_ANY, '&Add', 'Add file to project')
        self.edit_menu.Bind(wx.EVT_MENU, self.OnFileAdd, add_item)
        edit_item = self.edit_menu.Append(wx.ID_ANY, '&Edit', 'Edit the file')
        # self.edit_menu.Bind(wx.EVT_MENU, self.dlgform, edit_item)
        delete_item = self.edit_menu.Append(wx.ID_ANY, '&Delete', 'Delete file from project')
        self.edit_menu.Bind(wx.EVT_MENU, self.OnFileRemove, delete_item)
        self.main_menu.Append(self.edit_menu, '&Edit')

        '''----------------------------------------------Feedback Menu-----------------------------------------------'''
        feedback_menu = wx.Menu()
        ginfo_item = feedback_menu.Append(wx.ID_ANY, '&General Info', 'Give me feedback')
        self.Bind(wx.EVT_MENU, self.OnAbout, ginfo_item)
        help_item = feedback_menu.Append(wx.ID_ANY, '&Help', 'Do you need any help')
        self.Bind(wx.EVT_MENU, self.OnAbout, help_item)
        self.main_menu.Append(feedback_menu, '&Feedback')

        '''-------------------------------------------------About Menu-----------------------------------------------'''
        about_menu = wx.Menu()
        about_item = about_menu.Append(wx.ID_ANY, '&About', 'Information about this program')
        self.Bind(wx.EVT_MENU, self.OnAbout, about_item)
        self.main_menu.Append(about_menu, '&About')

        self.SetMenuBar(self.main_menu)

        '''------------------------------------------------Tool Bar--------------------------------------------------'''
        self.toolbar = self.CreateToolBar()

        new_bitmap = wx.Bitmap('C:\\Users\\ok49802\\Desktop\\Omkar\\11-09-2018\\abstract\\plus_blue.png')
        open_bitmap = wx.Bitmap('C:\\Users\\ok49802\\Desktop\\Omkar\\11-09-2018\\abstract\\open-file-icon.png')
        save_bitmap = wx.Bitmap('C:\\Users\\ok49802\\Desktop\\Omkar\\11-09-2018\\abstract\\save.png')

        new_image = self.toolbar.AddTool(wx.ID_ANY, new_bitmap)
        self.Bind(wx.EVT_TOOL, self.onSaveFile, new_image)
        open_image = self.toolbar.AddTool(wx.ID_ANY, open_bitmap)
        self.Bind(wx.EVT_TOOL, self.OnOpen, open_image)
        save_image = self.toolbar.AddTool(wx.ID_ANY, save_bitmap)
        self.Bind(wx.EVT_TOOL, self.onSaveFile, save_image)
        self.toolbar.Realize()

        '''----------------------------------Tree Operations---------------------------------------------------------'''
        splitter = wx.SplitterWindow(self)
        splitter.SetMinimumPaneSize(1)

        self.tree_ctrl = wx.TreeCtrl(splitter, style = wx.TR_DEFAULT_STYLE)

        self.editor = wx.TextCtrl(splitter, style=wx.TE_MULTILINE)
        self.editor.Enable(0)

        splitter.SplitVertically(self.tree_ctrl, self.editor)
        splitter.SetSashPosition(180, True)

        self.Bind(wx.EVT_TREE_SEL_CHANGED, self.OnSelChanged, self.tree_ctrl, id = 1)
        self.Bind(wx.EVT_TREE_ITEM_RIGHT_CLICK, self.OnShowPopup, self.tree_ctrl)
        self.display = wx.StaticText(self.editor, -1, '', (100, 100), style=wx.ALIGN_CENTRE)

        self.on_addRoot()

        self.Center()
        self.Show()

    def OnShowPopup(self, event):
        self.popupmenu = wx.Menu()
        add = self.popupmenu.Append(-1, 'Add')
        self.Bind(wx.EVT_MENU, self.onSelectContext, add)

        add_edit = self.popupmenu.Append(-1, 'Add/Edit')
        self.Bind(wx.EVT_MENU, self.onSelectContext, add_edit)

        self.PopupMenu(self.popupmenu, event.GetPoint())
        self.popupmenu.Destroy()

    def onSelectContext(self, event):
        item = self.popupmenu.FindItemById(event.GetId())
        text = item.GetText()
        print "Select context: %s" % text
        try:
            self.Bind(self, self.on_addParent(), item)
        except AssertionError:
            pass

    def on_addRoot(self):
        dlg = wx.TextEntryDialog(self, 'Name for new Node: ', 'Enter Root Name', 'Enter name', wx.OK | wx.CANCEL | wx.CENTRE)
        if dlg.ShowModal() == wx.ID_OK:
            name = dlg.GetValue()
            self.root = self.tree_ctrl.AddRoot(name)
        dlg.Destroy()

    def on_addParent(self):
        dlg = wx.TextEntryDialog(self, 'Name for parent Node: ', 'Enter Parent Name', 'Enter Parent Name', wx.OK | wx.CANCEL | wx.CENTRE)
        if dlg.ShowModal() == wx.ID_OK:
            pname = dlg.GetValue()
            self.parent = self.tree_ctrl.AppendItem(self.root, pname)
        dlg.Destroy()

    # def add_node(self, s1, s2, s3, s4, s5):
    #     li = [s1, s2, s3, s4, s5]
    #     for c in li:
    #         self.child = self.tree_ctrl.AppendItem(self.parent, c)
        # r = self.tree_ctrl.GetItemParent()
        # print r
        # for s in li:
        #     self.child = self.tree_ctrl.AppendItem(self.parent, s)

    def on_submit(self, e):
        print "Submitted"

    def CreateContextMenu(self):
        item = self._menu.Append(wx.ID_ADD)
        self.Bind(wx.EVT_MENU, self.onSelectContext, item)

        item = self._menu.Append(wx.ID_EDIT)
        self.Bind(wx.EVT_MENU, self.onSelectContext, item)

    def OnSelChanged(self, event):
        item = event.GetItem()
        self.display.SetLabel(self.tree_ctrl.GetItemText(item))

    '''--------------------------------------End of Tree Operations--------------------------------------------------'''

    def onSaveFile(self, e):
        self.currentDirectory = os.getcwd()
        self.wildcard = "Python source (*.py)|*.py|" \
                        "All files (*.*)|*.*"
        dlg = wx.FileDialog(self, "Save file as ...", "", self.currentDirectory, self.wildcard, wx.FD_SAVE)
        if dlg.ShowModal() == wx.ID_OK:
            path = dlg.GetPath()
            print "You chose the following filename: %s" % path
        dlg.Destroy()
        # self.Close()

    def OnOpen(self, e):
        self.frame1 = wx.Frame(None, -1, 'win.py')
        self.frame1.SetDimensions(0, 0, 200, 50)
        self.openFileDialog = wx.FileDialog(self.frame1, "Open", "", "","Python files (*.py)|*.py",wx.ID_OPEN |
                                            wx.FD_MULTIPLE | wx.FD_CHANGE_DIR | wx.FD_FILE_MUST_EXIST)
        self.openFileDialog.ShowModal()
        print(self.openFileDialog.GetPath())
        self.openFileDialog.Destroy()

    def OnQuit(self, e):
        self.Close()

    def OnFileAdd(self, event):
        """Adds a file to the current project."""
        if not self.CheckTreeRootItem():
            return
        dlg = wx.FileDialog(self, 'Choose a file to add.', '.', '', '*.*', wx.OPEN)
        if dlg.ShowModal() == wx.ID_OK:
            path = os.path.split(dlg.GetPath())
            self.tree_ctrl.AppendItem(self.root, path[1])
            self.tree_ctrl.Expand(self.root)
            # self.onSaveFile()

    def CheckTreeRootItem(self):
        """Is there any root item?"""
        if not self.root:
            MsgDlg(self, 'Please create or open a project before.', 'Error!', wx.OK)
            return False
        return True

    def OnFileRemove(self, event):
        """Removes a file to the current project."""
        if not self.CheckTreeRootItem():
            return
        item = self.tree_ctrl.GetSelection()
        if item != self.root:
            self.tree_ctrl.Delete(item)
            # self.project_save()

    # def setForm_data(self, s1, s2, s3, s4, s5):
    #     self.add_node(s1, s2, s3, s4, s5)

    # def dlgform(self, s1, s2, s3, s4, s5):
    #     obj2 = Data_Transfer()
    #     s1 = obj2.p1
    #     s2 = obj2.p2
    #     s3 = obj2.p3
    #     s4 = obj2.p4
    #     s5 = obj2.p5
    #     print s1, s2, s3, s4, s5+'2222222222222'
    #     self.add_node(s1, s2, s3, s4, s5)

    def OnAbout(self,e):
        dlg = wx.MessageDialog(self, "A small text editor", "About Sample Editor", wx.OK | wx.ICON_INFORMATION)
        dlg.ShowModal()  # Show it
        dlg.Destroy()  # finally destroy it when finished.

    # '''-------------------------------------------------Assembly Form------------------------------------------------'''
    # def OnRightClick(self):
    #
    #     self.panel = wx.Panel(self)
    #
    #     lbl_Asembly = wx.StaticText(self.panel, -1, "Asembly Name :", pos=(50, 50))
    #     self.txt_name = wx.TextCtrl(self.panel, id=wx.ID_ANY, pos=(150, 50), size=(150, 22))
    #
    #     lbl_image = wx.StaticText(self.panel, -1, "Add Image :", pos=(50, 85))
    #     txt_path = wx.TextCtrl(self.panel, id=wx.ID_ANY, pos=(150, 80), size=(150, 22))
    #     btn_browse = wx.Button(self.panel, -1, label="Browse", pos=(325, 80))
    #
    #     btn_submit = wx.Button(self.panel, -1, label="OK", pos=(75, 125))
    #     self.Bind(wx.EVT_BUTTON, self.asn_submit, btn_submit)
    #     btn_cancel = wx.Button(self.panel, -1, label="Cancel", pos=(200, 125))
    #     self.Bind(wx.EVT_BUTTON, self.asn_quit, btn_cancel)
    #
    #     self.SetSize((475, 225))
    #     self.SetTitle('Assembly Form')
    #     self.Centre()
    #     self.Show()
    #     self.Fit()
    #
    # def asn_submit(self):
    #     asname = self.txt_name.GetValue()
    #     print '********'
    #     print asname
    #     self.Close()
    #
    # def asn_quit(self):
    #     self.Close()

app = wx.App()
obj1 = TreeFrame_Dialogue(None, -1, 'Tree Frame Dialogue')
obj2 = DialogueForm(None)

v1 = obj2.t1
v2 = obj2.t2
v3 = obj2.t3
v4 = obj2.t4
v5 = obj2.t5

# obj1.add_node(v1, v2, v3, v4, v5)
obj1.Show(True)
app.SetTopWindow(obj1)
app.MainLoop()