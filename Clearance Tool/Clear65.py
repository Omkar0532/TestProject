import wx


class Clear_65(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)

        font = wx.Font(wx.FONTSIZE_MEDIUM, wx.FONTFAMILY_DEFAULT, wx.FONTSTYLE_NORMAL, wx.FONTWEIGHT_NORMAL)

        lbl1 = wx.StaticText(self, -1, "Input File : ", pos=(30, 30))
        lbl1.SetFont(font)
        self.txt_fp = wx.TextCtrl(self, id=wx.ID_ANY, pos=(130, 25), size=(300, 25))

        vbox1 = wx.BoxSizer(wx.VERTICAL)
        grid_area = wx.Panel(self, 1, name='Grid Area*', pos=(620, 10), size=(550, 700))
        # grid_area.SetBackgroundColour('#ededed')
        cnb = wx.Notebook(grid_area, -1, pos=(5, 5), size=(540, 690))
        res_page = Residue_Summary(cnb)
        reg_page = Regression_Check(cnb)
        cnb.AddPage(res_page, 'Residue Summary')
        cnb.AddPage(reg_page, 'Regression Check')
        vbox1.Add(cnb, wx.ID_ANY, wx.EXPAND)
        grid_area.SetSizer(vbox1)

        lbl2 = wx.StaticText(self, -1, "Cycle Definition", pos=(30, 75))
        lbl2.SetFont(font)

        vbox2 = wx.BoxSizer(wx.VERTICAL)
        cd_panel = wx.Panel(self, 1, name='Cycle Definition', pos=(30, 100), size=(500, 150))
        # cd_panel.SetBackgroundColour('#ededed')
        cd_sbox = wx.StaticBox(cd_panel, -1, 'TR accel def', pos=(5, 5), size=(490, 140))
        sbox_Szier = wx.StaticBoxSizer(cd_sbox, wx.HORIZONTAL)
        lbl3 = wx.StaticText(cd_sbox, -1, "SS IDLE :", pos=(13, 30))
        self.txt_ssidle = wx.TextCtrl(cd_sbox, id=wx.ID_ANY, pos=(60, 25), size=(80, 25))
        lbl4 = wx.StaticText(cd_sbox, -1, "Snap accel :", pos=(158, 30))
        self.txt_sxl = wx.TextCtrl(cd_sbox, id=wx.ID_ANY, pos=(225, 25), size=(80, 25))
        lbl5 = wx.StaticText(cd_sbox, -1, "SS Take Off :", pos=(318, 30))
        self.txt_sto = wx.TextCtrl(cd_sbox, id=wx.ID_ANY, pos=(388, 25), size=(80, 25))
        lbl6 = wx.StaticText(cd_sbox, -1, "Snap Decel :", pos=(30, 70))
        self.txt_sd = wx.TextCtrl(cd_sbox, id=wx.ID_ANY, pos=(98, 65), size=(80, 25))
        lbl7 = wx.StaticText(cd_sbox, -1, "End of Decel :", pos=(195, 70))
        self.txt_ed = wx.TextCtrl(cd_sbox, id=wx.ID_ANY, pos=(270, 65), size=(80, 25))
        vbox2.Add(sbox_Szier, wx.ID_ANY, wx.EXPAND)
        cd_panel.SetSizer(vbox2)
        cd_panel.Fit()

        lbl8 = wx.StaticText(self, -1, "No.of Stages :", pos=(30, 265))
        lbl8.SetFont(font)
        self.txt_nStages = wx.TextCtrl(self, id=wx.ID_ANY, pos=(135, 260), size=(65, 25))
        lbl9 = wx.StaticText(self, -1, "Stage wise Regression :", pos=(240, 265))
        lbl9.SetFont(font)
        self.txt_nStages = wx.TextCtrl(self, id=wx.ID_ANY, pos=(415, 260), size=(65, 25))

        lbl10 = wx.StaticText(self, -1, "Regression Parameters :", pos=(30, 310))
        lbl10.SetFont(font)

        vbox3 = wx.BoxSizer(wx.VERTICAL)
        rp_panel = wx.Panel(self, 1, name='Regression Parameters', pos=(30, 335), size=(570, 225))
        rp_panel.SetBackgroundColour('#ededed')
        lbl11 = wx.StaticText(rp_panel, -1, "Select the stage", pos=(10, 5))
        stages = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10']
        wx.ComboBox(rp_panel, 1, stages[0], (100, 5), wx.DefaultSize, stages, wx.CB_DROPDOWN)
        cl65_nb = wx.Notebook(rp_panel, -1, pos=(5, 30), size=(560, 190))
        tab1 = Blade(cl65_nb)
        tab2 = Disk(cl65_nb)
        tab3 = Case(cl65_nb)
        cl65_nb.AddPage(tab1, 'Blade')
        cl65_nb.AddPage(tab2, 'Disk')
        cl65_nb.AddPage(tab3, 'Case')
        vbox3.Add(cl65_nb, 1, wx.EXPAND)
        rp_panel.SetSizer(vbox3)

        lbl11 = wx.StaticText(self, -1, "Starting Stage :", pos=(30, 585))
        lbl11.SetFont(font)
        self.txt_nStages = wx.TextCtrl(self, id=wx.ID_ANY, pos=(150, 580), size=(65, 25))
        lbl12 = wx.StaticText(self, -1, "Ending Stage :", pos=(300, 585))
        lbl12.SetFont(font)
        self.txt_nStages = wx.TextCtrl(self, id=wx.ID_ANY, pos=(415, 580), size=(65, 25))

        btn_run = wx.Button(self, -1, label="Run", pos=(250, 650), size=(100, 30))


class Residue_Summary(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)

        vbox1 = wx.BoxSizer(wx.VERTICAL)
        res_panel = wx.Panel(self, -1, name='Residue Summary', pos=(5, 120), size=(520, 450))
        res_panel.SetBackgroundColour('#ededed')
        vbox1.Add(res_panel, wx.ID_ANY, wx.EXPAND)
        res_panel.SetSizer(vbox1)
        res_panel.Fit()


class Regression_Check(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)

        vbox1 = wx.BoxSizer(wx.VERTICAL)
        reg_panel = wx.Panel(self, -1, name='Residue Summary', pos=(5, 280), size=(520, 300))
        reg_panel.SetBackgroundColour('#ededed')
        btn_ppt = wx.Button(self, -1, label="Presentation", pos=(220, 600), size=(100, 30))
        vbox1.Add(reg_panel, wx.ID_ANY, wx.EXPAND)
        reg_panel.SetSizer(vbox1)
        reg_panel.Fit()


class Blade(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)

        lbl1 = wx.StaticText(self, -1, "ANGLE :", pos=(5, 8))
        self.txt_angle = wx.TextCtrl(self, id=wx.ID_ANY, pos=(50, 3), size=(90, 25))

        lbl2 = wx.StaticText(self, -1, "TR Params", pos=(5, 35))
        self.txt_trp = wx.TextCtrl(self, id=wx.ID_ANY, pos=(65, 33), size=(90, 20))

        lbl3 = wx.StaticText(self, -1, "AccelWeight", pos=(165, 35))
        self.txt_trp = wx.TextCtrl(self, id=wx.ID_ANY, pos=(240, 33), size=(90, 20))

        lbl4 = wx.StaticText(self, -1, "SSParamsThermal", pos=(340, 35))
        self.txt_trp = wx.TextCtrl(self, id=wx.ID_ANY, pos=(440, 33), size=(90, 20))

        lbl4 = wx.StaticText(self, -1, "SSParamsSpeed", pos=(5, 65))
        self.txt_trp = wx.TextCtrl(self, id=wx.ID_ANY, pos=(95, 60), size=(90, 20))

        lbl5 = wx.StaticText(self, -1, "SSParamsPressure", pos=(195, 65))
        self.txt_trp = wx.TextCtrl(self, id=wx.ID_ANY, pos=(295, 60), size=(90, 20))

        lbl5 = wx.StaticText(self, -1, "Units", pos=(400, 65))
        self.txt_trp = wx.TextCtrl(self, id=wx.ID_ANY, pos=(435, 60), size=(90, 20))

        lbl6 = wx.StaticText(self, -1, "Locations to run :", pos=(5, 110))
        self.cb1 = wx.CheckBox(self, label='LE', pos=(100, 110))
        self.cb2 = wx.CheckBox(self, label='MID', pos=(135, 110))
        self.cb3 = wx.CheckBox(self, label='TE', pos=(180, 110))

        lbl7 = wx.StaticText(self, -1, "Directions to run :", pos=(275, 110))
        self.cb4 = wx.CheckBox(self, label='Axial', pos=(375, 110))
        self.cb5 = wx.CheckBox(self, label='Radial', pos=(425, 110))
        self.cb6 = wx.CheckBox(self, label='Normal', pos=(480, 110))

        lbl8 = wx.StaticText(self, -1, "Run Global Solve :", pos=(5, 135))
        self.rb1 = wx.RadioButton(self, -1, label='Yes', pos=(110, 135))
        self.rb2 = wx.RadioButton(self, -1, label='No', pos=(155, 135))

        lbl9 = wx.StaticText(self, -1, "SS Only :", pos=(405, 135))
        self.rb1 = wx.RadioButton(self, -1, label='Yes', pos=(455, 135))
        self.rb2 = wx.RadioButton(self, -1, label='No', pos=(495, 135))


class Disk(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)


class Case(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)