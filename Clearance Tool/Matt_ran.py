import wx


class Mattran(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)

        font = wx.Font(wx.FONTSIZE_MEDIUM, wx.FONTFAMILY_DEFAULT, wx.FONTSTYLE_NORMAL, wx.FONTWEIGHT_NORMAL)

        lbl1 = wx.StaticText(self, -1, "Input File : ", pos=(30, 30))
        lbl1.SetFont(font)
        self.txt_input = wx.TextCtrl(self, id=wx.ID_ANY, pos=(200, 25), size=(300, 25))

        lbl2 = wx.StaticText(self, -1, "Input Build Clearances : ", pos=(30, 75))
        lbl2.SetFont(font)
        self.txt_ibc = wx.TextCtrl(self, id=wx.ID_ANY, pos=(200, 70), size=(300, 25))

        lbl3 = wx.StaticText(self, -1, "Legacy Data : ", pos=(30, 115))
        lbl3.SetFont(font)
        self.txt_ld = wx.TextCtrl(self, id=wx.ID_ANY, pos=(200, 110), size=(300, 25))

        vbox = wx.BoxSizer(wx.VERTICAL)
        reg_panel = wx.Panel(self, 1, name=' ', pos=(25, 150), size=(550, 240))
        reg_panel.SetBackgroundColour('#ededed')
        lbl4 = wx.StaticText(reg_panel, -1, "Stage wise regression :", pos=(10, 5))
        lbl4.SetFont(font)
        self.rb1 = wx.RadioButton(reg_panel, -1, label='Yes', pos=(185, 8))
        self.rb2 = wx.RadioButton(reg_panel, -1, label='No', pos=(230, 8))
        lbl5 = wx.StaticText(reg_panel, -1, "Select the stage", pos=(10, 40))
        lbl5.SetFont(font)
        stages = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10']
        wx.ComboBox(reg_panel, 1, " ", (145, 40), wx.DefaultSize, stages, wx.CB_DROPDOWN)
        lbl6 = wx.StaticText(reg_panel, -1, "Tau Scale Case : ", pos=(30, 75))
        self.txt_tsc = wx.TextCtrl(reg_panel, id=wx.ID_ANY, pos=(150, 70), size=(100, 20))
        lbl7 = wx.StaticText(reg_panel, -1, "Tau Scale disk : ", pos=(285, 75))
        self.txt_tsd = wx.TextCtrl(reg_panel, id=wx.ID_ANY, pos=(425, 70), size=(100, 20))
        lbl8 = wx.StaticText(reg_panel, -1, "Tau Scale blade : ", pos=(30, 105))
        self.txt_tsb = wx.TextCtrl(reg_panel, id=wx.ID_ANY, pos=(150, 100), size=(100, 20))
        lbl9 = wx.StaticText(reg_panel, -1, "Tau Scale parameters : ", pos=(285, 105))
        self.txt_tsp = wx.TextCtrl(reg_panel, id=wx.ID_ANY, pos=(425, 100), size=(100, 20))
        lbl10 = wx.StaticText(reg_panel, -1, "Tau Scale min : ", pos=(30, 135))
        self.txt_tmin = wx.TextCtrl(reg_panel, id=wx.ID_ANY, pos=(150, 130), size=(100, 20))
        lbl11 = wx.StaticText(reg_panel, -1, "Tau Scale max : ", pos=(285, 135))
        self.txt_tmax = wx.TextCtrl(reg_panel, id=wx.ID_ANY, pos=(425, 130), size=(100, 20))
        lbl12 = wx.StaticText(reg_panel, -1, "Time Steps for transients : ", pos=(30, 165))
        self.txt_tst = wx.TextCtrl(reg_panel, id=wx.ID_ANY, pos=(170, 160), size=(100, 20))
        btn_ppt = wx.Button(reg_panel, -1, label="Run", pos=(200, 200), size=(180, 30))
        vbox.Add(reg_panel, wx.ID_ANY, wx.EXPAND)
        reg_panel.SetSizer(vbox)

        vbox1 = wx.BoxSizer(wx.VERTICAL)
        grid_area = wx.Panel(self, 1, name='Grid Area*', pos=(620, 10), size=(550, 700))
        # grid_area.SetBackgroundColour('#ededed')
        mtr = wx.Notebook(grid_area, -1, pos=(5, 5), size=(540, 690))
        rc_page = Results_Compare(mtr)
        edo_page = EngineData_Ouput(mtr)
        mtr.AddPage(rc_page, 'Results Compare')
        mtr.AddPage(edo_page, 'Engine data vs Output')
        vbox1.Add(mtr, wx.ID_ANY, wx.EXPAND)
        grid_area.SetSizer(vbox1)

        btn_ebc = wx.Button(self, -1, label="Estimate Build Clearance", pos=(50, 500), size=(180, 30))
        grid = wx.grid.Grid(self, 1, name='Build Clearance', pos=(280, 480), size=(285, 110))
        grid.CreateGrid(4, 2)
        grid.SetColLabelValue(0, 'Stage')
        grid.SetColLabelValue(1, 'Build Clearance')
        grid.SetColSize(0, 100)
        grid.SetColSize(1, 100)


class Results_Compare(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)

        vbox1 = wx.BoxSizer(wx.VERTICAL)
        rc_panel = wx.Panel(self, -1, name='Results Compare', pos=(5, 280), size=(520, 300))
        rc_panel.SetBackgroundColour('#ededed')
        btn_ppt = wx.Button(self, -1, label="Presentation", pos=(220, 600), size=(100, 30))
        vbox1.Add(rc_panel, wx.ID_ANY, wx.EXPAND)
        rc_panel.SetSizer(vbox1)
        rc_panel.Fit()


class EngineData_Ouput(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)

        vbox1 = wx.BoxSizer(wx.VERTICAL)
        edo_panel = wx.Panel(self, -1, name='Results Compare', pos=(5, 280), size=(520, 300))
        edo_panel.SetBackgroundColour('#ededed')
        btn_ppt = wx.Button(self, -1, label="Presentation", pos=(220, 600), size=(100, 30))
        vbox1.Add(edo_panel, wx.ID_ANY, wx.EXPAND)
        edo_panel.SetSizer(vbox1)
        edo_panel.Fit()