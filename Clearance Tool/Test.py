# import wx
# import wx.lib.filebrowsebutton
# class MyFrame(wx.Frame):
#     def __init__(self, parent, mytitle):
#         wx.Frame.__init__(self, parent, -1, mytitle, size=(500,100))
#         self.SetBackgroundColour("green")
#         panel = wx.Panel(self)
#         self.fbb = wx.lib.filebrowsebutton.FileBrowseButton(panel,labelText="Select a WAVE file:", fileMask="*.wav")
#         play_button = wx.Button(panel, -1, ">>  Play")
#         self.Bind(wx.EVT_BUTTON, self.onPlay, play_button)
#         # setup the layout with sizers
#         hsizer = wx.BoxSizer(wx.HORIZONTAL)
#         hsizer.Add(self.fbb, 1, wx.ALIGN_CENTER_VERTICAL)
#         hsizer.Add(play_button, 0, wx.ALIGN_CENTER_VERTICAL)
#         # create a border space
#         border = wx.BoxSizer(wx.VERTICAL)
#         border.Add(hsizer, 0, wx.EXPAND|wx.ALL, 15)
#         panel.SetSizer(border)
#     def onPlay(self, evt):
#         filename = self.fbb.GetValue()
#         self.sound = wx.Sound(filename)
#         # error handling ...
#         if self.sound.IsOk():
#             self.sound.Play(wx.SOUND_ASYNC)
#         else:
#             wx.MessageBox("Missing or invalid sound file", "Error")
# app = wx.App(0)
# # create a MyFrame instance and show the frame
# caption = "wx.lib.filebrowsebutton.FileBrowseButton()"
# MyFrame(None, caption).Show()
# app.MainLoop()

import wx, os, shutil, fnmatch #, sys, textwrap, re, Image, ImageEnhance,
import wx.lib.filebrowsebutton

ID_HELP = 1
ID_ABOUT = 2
ID_GO = 3


class Wip(wx.Frame):
    def __init__(self, parent, id, title):
        wx.Frame.__init__(self, parent, id, title, size=(1000, 225))
        # Define menus
        menubar = wx.MenuBar()
        # Define file menu
        file = wx.Menu()
        quit = wx.MenuItem(file, 1, '&Quit\tCtrl+Q')
        file.AppendItem(quit)
        self.Bind(wx.EVT_MENU, self.OnQuit, id=1)
        # Define help menu
        helpMenu = wx.Menu()
        helpMenu.Append(ID_HELP, '&Help')
        helpMenu.Append(ID_ABOUT, '&About')
        self.Bind(wx.EVT_MENU, self.OnAboutBox, id=ID_ABOUT)

        # Append menus
        menubar.Append(file, '&File')
        menubar.Append(helpMenu, '&Help')
        self.SetMenuBar(menubar)
        # Define main panel
        panel = wx.Panel(self, -1)
        vbox = wx.BoxSizer(wx.VERTICAL)
        # Define buttons
        # Define button to select the pictures folder
        hbox1 = wx.BoxSizer(wx.HORIZONTAL)
        self.picsFolder = wx.lib.filebrowsebutton.DirBrowseButton(panel, labelText="Select your pictures directory:",
                                                                  size=(800, 30))
        hbox1.Add(self.picsFolder, 0, wx.ALL, 5)
        # Define the button to select the watermark file
        hbox2 = wx.BoxSizer(wx.HORIZONTAL)
        mask = '*.png'
        self.markFile = wx.lib.filebrowsebutton.FileBrowseButton(panel, labelText="Select an image file:",
                                                                 fileMask=mask, fileMode=wx.OPEN, size=(800, 30))
        hbox2.Add(self.markFile, 0, wx.ALL, 5)
        # Define a text box to select desired size
        hbox3 = wx.BoxSizer(wx.HORIZONTAL)
        self.pxText = wx.StaticText(panel, -1, 'Desired size of the longest picture side in pixels: ', pos=(300, 170),
                                    size=(321, 30))
        self.px = wx.TextCtrl(panel, -1, value='1024', pos=wx.Point(10, 90), size=wx.Size(45, 30))
        hbox3.Add(self.pxText, 0, wx.ALL, 5)
        hbox3.Add(self.px, 0, wx.ALL, 0)

        # Define the button to start processing
        hbox4 = wx.BoxSizer(wx.HORIZONTAL)
        go = wx.Button(panel, -1, 'Go', size=(100, 30))
        hbox4.Add(go, 0, wx.ALL, 5)
        self.Bind(wx.EVT_BUTTON, self.WaterMark, go)
        # Merge sizers
        vbox.Add(hbox1, 0, wx.ALIGN_LEFT | wx.ALL, 5)
        vbox.Add(hbox2, 0, wx.ALIGN_LEFT | wx.ALL, 5)
        vbox.Add(hbox3, 0, wx.ALIGN_LEFT | wx.ALL, 5)
        vbox.Add(hbox4, 0, wx.ALIGN_RIGHT | wx.ALL, 5)
        panel.SetSizer(vbox)
        self.Centre()
        self.Show(True)

    def OnQuit(self, event):
        self.Close()

    def OnAboutBox(self, event):
        info = wx.AboutDialogInfo()
        info.SetIcon(wx.Icon('icons/exit.png', wx.BITMAP_TYPE_PNG))
        info.SetName('Watermark Image Processing')
        info.SetVersion('1.0b')
        description = open('docs/info.txt').read()
        info.SetDescription(description)
        info.SetCopyright('(C) 2010 Acrocephalus Soft')
        info.SetWebSite('http://www.acrocephalus.net')
        license = open('docs/licence.txt').read()
        info.SetLicence(license)
        info.AddDeveloper('Daniel Valverde')
        info.AddDocWriter('Daniel Valverde')
        info.AddArtist('Daniel Valverde')
        info.AddTranslator('Daniel Valverde')
        wx.AboutBox(info)

    def WaterMark(self, event):
        px = self.px.GetValue()
        markFile = self.markFile.GetValue()
        picsFolder = self.picsFolder.GetValue()
        os.chdir(picsFolder)
        # Create a Backup directory
        if not os.path.exists('Backup'):
            os.mkdir('Backup')
        # Create a Resized directory
        if not os.path.exists('Resized'):
            os.mkdir('Resized')
        # Create a marked directory
        if not os.path.exists('Watermarked'):
            os.mkdir('Watermarked')
        # Image processing
        for file in fnmatch.filter(os.listdir(picsFolder), '*.jpg'):
            print 'Resizing image ' + file
            # Open the image
            img = Image.open(file)
            # Get actual image size and convert it to float
            width, height = img.size
            width = float(width)
            height = float(height)
            # Resize landscape images
            if width > height:
                resizeFactor = width / int(px * 2)
                img = img.resize((int(width / resizeFactor), int(height / resizeFactor)))
                width, height = img.size
                img = img.resize((int(width / 2), int(height / 2)), Image.ANTIALIAS)
            # Resize portrait images
            elif width < height:
                resizeFactor = height / int(px * 2)
                img = img.resize((int(width / resizeFactor), int(height / resizeFactor)))
                width, height = img.size
                img = img.resize((int(width / 2), int(height / 2)), Image.ANTIALIAS)
            # Resize square images
            else:
                img = img.resize((int(px * 2), int(px * 2)))
                width, height = img.size
                img = img.resize((int(px), int(px)), Image.ANTIALIAS)
            # Save resized image
            img.save(file)
        # Copy pictures to a Resized directory
        for filename in fnmatch.filter(os.listdir(picsFolder), '*.jpg'):
            shutil.copy2(filename, 'Resized/%s' % filename)
        # ----------------------------------------------------------------------------
        # Add mark
        # Getting the sizes of the base image and the mark
        imgmark = Image.open(markFile)
        markWidth, markHeight = imgmark.size
        for filename in fnmatch.filter(os.listdir(picsFolder), '*.jpg'):
            # Open the image
            img = Image.open(filename)
            # Get actual image size and convert it to float
            width, height = img.size
            baseim = Image.open(filename)
            logoim = Image.open(markFile)  # transparent image
            baseim.paste(logoim, (int(width) - int(markWidth) - 20, int(height) - int(markHeight) - 10), logoim)
            baseim.save(filename, 'JPEG')
        # ----------------------------------------------------------------------------
        # Recover EXIF data
        try:
            os.system('jhead -te Backup/*jpg *.jpg')
        except:
            print w.fill(
                'You don\'t have JHEAD installed, so WIP cannot recover the EXIF data. You can download and install it from http://www.sentex.net/ ~mwandel/jhead/')
        for filename in fnmatch.filter(os.listdir(picsFolder), '*.jpg'):
            shutil.move(filename, 'Watermarked/%s' % filename)


app = wx.App()
Wip(None, -1, 'Watermark Image Processing 1.0b')
app.MainLoop()