import wx
import wx.grid


class MUDS(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)

        lbl1 = wx.StaticText(self, -1, "SSM2FC :", pos=(20, 50))
        self.txt_ssm = wx.TextCtrl(self, id=wx.ID_ANY, pos=(80, 45), size=(150, 22))

        lbl2 = wx.StaticText(self, -1, "TrM2FC :", pos=(20, 85))
        self.txt_trm = wx.TextCtrl(self, id=wx.ID_ANY, pos=(80, 80), size=(150, 22))

        lbl3 = wx.StaticText(self, -1, "SS Ubin* :", pos=(20, 130))
        self.txt_ssu = wx.TextCtrl(self, id=wx.ID_ANY, pos=(80, 125), size=(150, 22))
        btn_stime = wx.Button(self, -1, label="Start Time", pos=(250, 125))
        btn_etime = wx.Button(self, -1, label="End Time", pos=(350, 125))

        lbl4 = wx.StaticText(self, -1, "Tr Ubin* :", pos=(20, 165))
        self.txt_tru = wx.TextCtrl(self, id=wx.ID_ANY, pos=(80, 160), size=(150, 22))
        btn_stime1 = wx.Button(self, -1, label="Start Time", pos=(250, 158))
        btn_etime1 = wx.Button(self, -1, label="End Time", pos=(350, 158))

        font = wx.Font(wx.FONTSIZE_MEDIUM, wx.FONTFAMILY_DEFAULT, wx.FONTSTYLE_NORMAL, wx.FONTWEIGHT_NORMAL)

        lbl5 = wx.StaticText(self, -1, "User Defines Parameters*", pos=(20, 250))
        lbl5.SetFont(font)
        grid = wx.grid.Grid(self, 1, name='User Defines Parameters*',pos=(20, 280), size=(450, 130))
        grid.CreateGrid(5, 2)
        grid.SetColLabelValue(0, 'Parameter')
        grid.SetColLabelValue(1, 'Eqn.')
        grid.SetColSize(0, 135)
        grid.SetColSize(1, 230)

        lbl6 = wx.StaticText(self, -1, "PLOTING AREA*", pos=(500, 45))
        lbl6.SetFont(font)
        vbox = wx.BoxSizer(wx.VERTICAL)
        plot_graph = wx.Panel(self, 1, name='PLOTING AREA*', pos=(500, 75), size=(650, 350))
        plot_graph.SetBackgroundColour('#ededed')
        vbox.Add(plot_graph, wx.ID_ANY, wx.EXPAND | wx.ALIGN_RIGHT)
        plot_graph.SetSizer(vbox)

        lbl7 = wx.StaticText(self, -1, 'Time Point Selection :', pos = (20, 450))
        lbl7.SetFont(font)
        cbox1 = wx.Panel(self, 1, name='Time Point Selection', pos=(20, 480), size=(150, 100))
        cbox1.SetBackgroundColour('#ededed')
        self.cb1 = wx.CheckBox(cbox1, label='0', pos=(10, 10))
        self.cb2 = wx.CheckBox(cbox1, label='10', pos=(10, 30))
        self.cb3 = wx.CheckBox(cbox1, label='899', pos=(10, 50))
        self.cb4 = wx.CheckBox(cbox1, label='900', pos=(10, 70))

        lbl8 = wx.StaticText(self, -1, 'Flight Point Selection :', pos=(300, 450))
        lbl8.SetFont(font)
        cbox2 = wx.Panel(self, 1, name='Flight Point Selection', pos=(300, 480), size=(150, 100))
        cbox2.SetBackgroundColour('#ededed')
        self.cb1 = wx.CheckBox(cbox2, label='120', pos=(10, 10))
        self.cb2 = wx.CheckBox(cbox2, label='121', pos=(10, 30))
        self.cb3 = wx.CheckBox(cbox2, label='127', pos=(10, 50))

        lbl9 = wx.StaticText(self, -1, 'MUDS/USER Parameters :', pos=(580, 450))
        lbl9.SetFont(font)
        cbox3 = wx.Panel(self, 1, name='MUDS/USER Parameters :', pos=(580, 480), size=(200, 100))
        cbox3.SetBackgroundColour('#ededed')
        self.cb1 = wx.CheckBox(cbox3, label='W2', pos=(10, 10))
        self.cb2 = wx.CheckBox(cbox3, label='W25', pos=(10, 30))
        self.cb3 = wx.CheckBox(cbox3, label='W3', pos=(10, 50))
        self.cb4 = wx.CheckBox(cbox3, label='W4', pos=(10, 70))

        lbl10 = wx.StaticText(self, -1, 'Engine Data :', pos=(900, 450))
        lbl10.SetFont(font)
        cbox4 = wx.Panel(self, 1, name='Engine Data :', pos=(900, 480), size=(150, 100))
        cbox4.SetBackgroundColour('#ededed')
        self.cb1 = wx.CheckBox(cbox4, label='DK1C-A', pos=(10, 10))
        self.cb2 = wx.CheckBox(cbox4, label='DK1C-B', pos=(10, 30))
        self.cb3 = wx.CheckBox(cbox4, label='DK1C-C', pos=(10, 50))
        self.cb4 = wx.CheckBox(cbox4, label='DK1C-D', pos=(10, 70))

        btn_plotp = wx.Button(self, -1, label="Plot Parameters", pos=(500, 600), size=(150, 30))
        btn_ccfile = wx.Button(self, -1, label="Create Csv File", pos=(500, 645), size=(150, 30))