# import sys, os
import wx


class TreeFrame_Dialogue(wx.Frame):

    def __init__(self, parent, id, title):
        wx.Frame.__init__(self, parent, id, title, wx.DefaultPosition, wx.Size(1200, 950),
                          style = wx.DEFAULT_FRAME_STYLE | wx.NO_FULL_REPAINT_ON_RESIZE)

        self.root = ''
        self.parent = ''
        self.child = ''

        self.main_menu = wx.MenuBar()

        file_menu = wx.Menu()
        open_item = file_menu.Append(wx.ID_OPEN, '&Open', 'Open project')
        new_item = file_menu.Append(wx.ID_NEW, '&New', 'New project')
        save_item = file_menu.Append(wx.ID_ANY, '&Save', 'Save the file')
        SaveAs_item = file_menu.Append(wx.ID_ANY, '&SaveAs', 'Save the file as user given name')
        exit_item = file_menu.Append(wx.ID_EXIT, '&Exit', 'Exit program')
        self.main_menu.Append(file_menu, '&File')

        edit_menu = wx.Menu()
        add_item = edit_menu.Append(wx.ID_ANY, '&Add', 'Add file to project')
        edit_item = edit_menu.Append(wx.ID_ANY, '&Edit', 'Edit the file')
        delete_item = edit_menu.Append(wx.ID_ANY, '&Delete', 'Delete file from project')
        self.main_menu.Append(edit_menu, '&Edit')

        feedback_menu = wx.Menu()
        geninfo_item = feedback_menu.Append(wx.ID_ANY, '&General Info', 'Give me feedback')
        help_item = feedback_menu.Append(wx.ID_ANY, '&Help', 'Do you need any help')
        self.main_menu.Append(feedback_menu, '&Feedback')

        about_menu = wx.Menu()
        about_item = about_menu.Append(wx.ID_ANY, '&About', 'Information about this program')
        self.main_menu.Append(about_menu, '&About')

        self.SetMenuBar(self.main_menu)

        self.toolbar = self.CreateToolBar()

        new_bitmap = wx.Bitmap('C:\\Users\\ok49802\\Desktop\\Omkar\\11-09-2018\\abstract\\plus_blue.png')
        open_bitmap = wx.Bitmap('C:\\Users\\ok49802\\Desktop\\Omkar\\11-09-2018\\abstract\\open-file-icon.png')
        save_bitmap = wx.Bitmap('C:\\Users\\ok49802\\Desktop\\Omkar\\11-09-2018\\abstract\\save.png')

        new_image = self.toolbar.AddTool(wx.ID_ANY, new_bitmap)
        open_image = self.toolbar.AddTool(wx.ID_ANY, open_bitmap)
        save_image = self.toolbar.AddTool(wx.ID_ANY, save_bitmap)
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
        dlg = wx.TextEntryDialog(self, 'Name for new Node: ', 'Enter Root Name', 'Enter name', wx.OK | wx.CANCEL)
        if dlg.ShowModal() == wx.ID_OK:
            name = dlg.GetValue()
            self.root = self.tree_ctrl.AddRoot(name)
        dlg.Center()
        dlg.Destroy()

    def on_addParent(self):
        dlg = wx.TextEntryDialog(self, 'Name for parent Node: ', 'Enter Parent Name', 'Enter Parent Name', wx.OK | wx.CANCEL)
        if dlg.ShowModal() == wx.ID_OK:
            pname = dlg.GetValue()
            self.parent = self.tree_ctrl.AppendItem(self.root, pname)
        dlg.Destroy()

    def on_submit(self, e):
        print "Submitted"

    # def CreateContextMenu(self):
    #     item = self._menu.Append(wx.ID_ADD)
    #     self.Bind(wx.EVT_MENU, self.onSelectContext, item)
    #
    #     item = self._menu.Append(wx.ID_EDIT)
    #     self.Bind(wx.EVT_MENU, self.onSelectContext, item)

    def OnSelChanged(self, event):
        item = event.GetItem()
        self.display.SetLabel(self.tree_ctrl.GetItemText(item))

    '''--------------------------------------End of Tree Operations--------------------------------------------------'''

app = wx.App()
otframe = TreeFrame_Dialogue(None, -1, 'Tree Frame Dialogue')
otframe.Show(True)
app.SetTopWindow(otframe)
app.MainLoop()