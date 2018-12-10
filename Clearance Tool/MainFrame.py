import wx
from MUDS import MUDS
from Axial_Deflections import AxialDeflections
from Radial_Deflections import RadialDeflections
from NormalDeflections import Normal_Deflections
from Clear65 import Clear_65
from Matt_ran import Mattran


class Main_Frame(wx.Frame):
    def __init__(self):
        wx.Frame.__init__(self, None, 1, 'Clearance Tool', size=(1200, 800))

        panel = wx.Panel(self)
        note_book = wx.Notebook(panel)
        page1 = MUDS(note_book)
        page2 = AxialDeflections(note_book)
        page3 = RadialDeflections(note_book)
        page4 = Normal_Deflections(note_book)
        page5 = Clear_65(note_book)
        page6 = Mattran(note_book)

        note_book.AddPage(page1, 'MUDS')
        note_book.AddPage(page2, 'Axial Deflections')
        note_book.AddPage(page3, 'Radial Deflections')
        note_book.AddPage(page4, 'Normal Deflections')
        note_book.AddPage(page5, 'Clear65')
        note_book.AddPage(page6, 'Mattran')

        sizer = wx.BoxSizer()
        sizer.Add(note_book, 1, wx.EXPAND)
        panel.SetSizer(sizer)

        self.Center()

app = wx.App()
Main_Frame().Show()
app.MainLoop()