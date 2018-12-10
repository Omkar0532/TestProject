import wx


class Normal_Deflections(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)

        font = wx.Font(wx.FONTSIZE_MEDIUM, wx.FONTFAMILY_DEFAULT, wx.FONTSTYLE_NORMAL, wx.FONTWEIGHT_NORMAL)

        lbl1 = wx.StaticText(self, -1, "Legacy* : ", pos=(50, 50))
        self.txt_lgy = wx.TextCtrl(self, id=wx.ID_ANY, pos=(140, 45), size=(200, 25))
        btn_chk1 = wx.Button(self, -1, label="Check", pos=(360, 45))

        lbl2 = wx.StaticText(self, -1, "Deflections : ", pos=(50, 100))
        self.txt_lgy = wx.TextCtrl(self, id=wx.ID_ANY, pos=(140, 90), size=(200, 25))
        btn_chk1 = wx.Button(self, -1, label="Check", pos=(360, 90))

        lbl3 = wx.StaticText(self, -1, "PLOTING AREA*", pos=(500, 45))
        lbl3.SetFont(font)
        vbox = wx.BoxSizer(wx.VERTICAL)
        plot_graph = wx.Panel(self, 1, name='PLOTING AREA*', pos=(500, 70), size=(650, 450))
        plot_graph.SetBackgroundColour('#ededed')
        vbox.Add(plot_graph, wx.ID_ANY, wx.EXPAND)
        plot_graph.SetSizer(vbox)

        lbl4 = wx.StaticText(self, -1, 'Angle', pos=(50, 170))
        lbl4.SetFont(font)
        grid = wx.grid.Grid(self, 1, name='Scaling Deflections*', pos=(140, 150), size=(282, 90))
        grid.CreateGrid(3, 2)
        grid.SetColLabelValue(0, 'Stage')
        grid.SetColLabelValue(1, 'Angle')
        grid.SetColSize(0, 100)
        grid.SetColSize(1, 100)

        lbl5 = wx.StaticText(self, -1, 'Deflections', pos=(50, 280))
        lbl5.SetFont(font)
        lbl6 = wx.StaticText(self, -1, 'Closedowns', pos=(50, 320))
        lbl6.SetFont(font)

        list_panel = wx.Panel(self, 1, name='Normal Deflections', pos=(50, 400), size=(200, 200))
        list_panel.SetBackgroundColour('#ededed')
        lst = wx.ListBox(list_panel, 1, style=wx.LB_DEFAULT, name='Normal Deflections', pos=(50, 200), size=(200, 200))

        btn_ppm = wx.Button(self, -1, label="Plot Parameters", pos=(260, 520), size=(150, 30))
        btn_cppt = wx.Button(self, -1, label="Consolidate PPT", pos=(260, 560), size=(150, 30))