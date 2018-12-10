import wx
import wx.grid


class AxialDeflections(wx.Panel):
    def __init__(self, parent):
        wx.Panel.__init__(self, parent)

        font = wx.Font(wx.FONTSIZE_MEDIUM, wx.FONTFAMILY_DEFAULT, wx.FONTSTYLE_NORMAL, wx.FONTWEIGHT_NORMAL)

        lbl1 = wx.StaticText(self, -1, "Legacy* : ", pos=(50, 50))
        # lbl1.SetFont(font)
        self.txt_lgy = wx.TextCtrl(self, id=wx.ID_ANY, pos=(140, 45), size=(200, 25))
        btn_chk1 = wx.Button(self, -1, label="Check", pos=(360, 45))

        self.Bind(wx.EVT_BUTTON, self.on_Check, btn_chk1)

        lbl2 = wx.StaticText(self, -1, "Deflections : ", pos=(50, 100))
        # lbl2.SetFont(font)
        self.txt_deflection = wx.TextCtrl(self, id=wx.ID_ANY, pos=(140, 90), size=(200, 25))
        btn_chk2 = wx.Button(self, -1, label="Check", pos=(360, 90))

        lbl3 = wx.StaticText(self, -1, "PLOTING AREA*", pos=(500, 45))
        lbl3.SetFont(font)
        vbox = wx.BoxSizer(wx.VERTICAL)
        plot_graph = wx.Panel(self, 1, name='PLOTING AREA*', pos=(500, 70), size=(650, 450))
        plot_graph.SetBackgroundColour('#ededed')
        vbox.Add(plot_graph, wx.ID_ANY, wx.EXPAND)
        plot_graph.SetSizer(vbox)

        lbl4 = wx.StaticText(self, -1, "Deflections ", pos=(50, 180))
        lbl4.SetFont(font)
        list_panel = wx.Panel(self, 1, name='Deflections', pos=(50, 200), size=(200, 200))
        list_panel.SetBackgroundColour('#ededed')
        lst = wx.ListBox(list_panel, 1, style=wx.LB_DEFAULT, name='Deflections', pos=(50, 200), size=(200, 200))

        btn_ppmeter = wx.Button(self, -1, label="Plot Parameters", pos=(270, 250))

        lbl5 = wx.StaticText(self, -1, 'Scaling Deflections*', pos=(50, 450))
        lbl5.SetFont(font)
        grid = wx.grid.Grid(self, 1, name='Scaling Deflections*', pos=(50, 475), size=(325, 110))
        grid.CreateGrid(4, 2)
        # grid.HideRowLabels()
        grid.SetColLabelValue(0, 'Stage')
        grid.SetColLabelValue(1, 'Scaling')
        grid.SetColSize(0, 120)
        grid.SetColSize(1, 120)

    def on_Check(self, e):

        dialog = wx.DirDialog(self, 'Choose media directory', '', style=wx.DD_DEFAULT_STYLE)

        try:
            if dialog.ShowModal() == wx.ID_CANCEL:
                return
            elif dialog.ShowModal() == wx.ID_OK:
                path = dialog.GetPath()
                self.txt_lgy.Clear()
                self.txt_lgy.SetValue(path)
        except Exception:
            wx.LogError('Failed to open directory!')
            raise
        finally:
            dialog.Destroy()

