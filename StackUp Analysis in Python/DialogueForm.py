import wx
# from DataTransfer import Data_Transfer


class DialogueForm(wx.Frame):

    def __init__(self, *args, **kwargs):
        super(DialogueForm, self).__init__(*args, **kwargs)

        self.t1 = ''
        self.t2 = ''
        self.t3 = ''
        self.t4 = ''
        self.t5 = ''

        self.panel = wx.Panel(self)

        lbl_parameter1 = wx.StaticText(self.panel, -1, "Parameter1 :", pos=(50, 50))
        self.txt_parameter1 = wx.TextCtrl(self.panel, id=wx.ID_ANY, pos=(150, 50), size=(150,20))
        self.t1 = self.txt_parameter1.GetValue()

        lbl_parameter2 = wx.StaticText(self.panel, -1, "Parameter2 :", pos=(50, 100))
        self.txt_parameter2 = wx.TextCtrl(self.panel, id=wx.ID_ANY, pos=(150, 100), size=(150,20))
        self.t2 = self.txt_parameter2.GetValue()

        lbl_parameter3 = wx.StaticText(self.panel, -1, "Parameter3 :", pos=(50, 150))
        self.txt_parameter3 = wx.TextCtrl(self.panel, id=wx.ID_ANY, pos=(150, 150), size=(150,20))
        self.t3 = self.txt_parameter3.GetValue()

        lbl_parameter4 = wx.StaticText(self.panel, -1, "Parameter4 :", pos=(50, 200))
        self.txt_parameter4 = wx.TextCtrl(self.panel, id=wx.ID_ANY, pos=(150, 200), size=(150,20))
        self.t4 = self.txt_parameter4.GetValue()

        lbl_parameter5 = wx.StaticText(self.panel, -1, "Parameter5 :", pos=(50, 250))
        self.txt_parameter5 = wx.TextCtrl(self.panel, id=wx.ID_ANY, pos=(150, 250), size=(150,20))
        self.t5 = self.txt_parameter5.GetValue()

        btn_submit = wx.Button(self.panel, -1, label="OK", pos=(150, 300))
        self.Bind(wx.EVT_BUTTON, self.on_submit, btn_submit)

        self.SetTitle('Assembly Form')
        self.SetSize((350, 400))
        self.Centre()
        self.Show()
        self.Fit()

    def on_submit(self, e):
        # self.t1 = self.txt_parameter1.GetValue()
        # self.t2 = self.txt_parameter2.GetValue()
        # self.t3 = self.txt_parameter3.GetValue()
        # self.t4 = self.txt_parameter4.GetValue()
        # self.t5 = self.txt_parameter5.GetValue()
        print self.t1, self.t2, self.t3, self.t4, self.t5
        print "Submitted"
        # obj = Data_Transfer()
        # obj.getForm_data(self.t1, self.t2, self.t3, self.t4, self.t5)
        self.Close()


# app = wx.App()
# obj1 = DialogueForm(None)
# print obj1.t1, obj1.t2, obj1.t3, obj1.t4, obj1.t5
# obj2 = form_data(obj1)
# print obj2.s1, obj2.s2, obj2.s3, obj2.s4, obj2.s5+'@@@@@@@@@'
# print getattr(obj2, 's1', 's2')
# app.MainLoop()