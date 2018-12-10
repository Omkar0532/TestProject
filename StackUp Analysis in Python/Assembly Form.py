import wx


class Assembly_Form(wx.Frame):
    def __init__(self, *args, **kwargs):
        wx.Frame.__init__(self, *args, **kwargs)

        self.panel = wx.Panel(self)

        lbl_Asembly = wx.StaticText(self.panel, -1, "Asembly Name :", pos=(50, 50))
        self.txt_name = wx.TextCtrl(self.panel, id=wx.ID_ANY, pos=(150, 50), size=(150, 22))

        lbl_image = wx.StaticText(self.panel, -1, "Add Image :", pos=(50, 85))
        txt_path = wx.TextCtrl(self.panel, id=wx.ID_ANY, pos=(150, 80), size=(150, 22))
        btn_browse = wx.Button(self.panel, -1, label="Browse", pos=(325, 80))

        btn_submit = wx.Button(self.panel, -1, label="OK", pos=(75, 125))
        self.Bind(wx.EVT_BUTTON, self.asn_submit, btn_submit)
        btn_cancel = wx.Button(self.panel, -1, label="Cancel", pos=(200, 125))
        self.Bind(wx.EVT_BUTTON, self.asn_quit, btn_cancel)

        self.SetSize((475, 225))
        self.SetTitle('Assembly Form')
        self.Centre()
        self.Show()
        self.Fit()

    def asn_submit(self):
        asname = self.txt_name.GetValue()
        print '********'
        print asname
        self.Close()

    def asn_quit(self):
        self.Close()

'''------------------------------------------------End of Assebly Form-----------------------------------------------'''
app = wx.App()
Assembly_Form(None)
app.MainLoop()
