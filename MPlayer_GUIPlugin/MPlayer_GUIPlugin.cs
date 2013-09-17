#region Copyright (C) 2006-2013 MisterD

/* 
 *	Copyright (C) 2006-2013 MisterD
 *
 *  This Program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2, or (at your option)
 *  any later version.
 *   
 *  This Program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *   
 *  You should have received a copy of the GNU General Public License
 *  along with GNU Make; see the file COPYING.  If not, write to
 *  the Free Software Foundation, 675 Mass Ave, Cambridge, MA 02139, USA. 
 *  http://www.gnu.org/copyleft/gpl.html
 *
 */

#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using MediaPortal.GUI.Library;
using System.Collections;
using MediaPortal.Util;
using MediaPortal.Dialogs;
using MediaPortal.Playlists;
using MediaPortal.Player;
using MediaPortal.GUI.Video;
using MediaPortal.Configuration;
using Action = MediaPortal.GUI.Library.Action;

namespace MPlayer
{
  /// <summary>
  /// Window plugin for the MPlayer External player plugin
  /// </summary>
  public sealed class MPlayerGUIPlugin : GUIWindow, IShowPlugin, ISetupForm
  {
    #region enum
    /// <summary>
    /// Possible View
    /// </summary>
    public enum View
    {
      /// <summary>
      /// List view
      /// </summary>
      List = 0,
      /// <summary>
      /// Icon view
      /// </summary>
      Icons = 1,
      /// <summary>
      /// Large icon view
      /// </summary>
      LargeIcons = 2,
      /// <summary>
      /// Filmstrip view
      /// </summary>
      FilmStrip = 3
    }
    #endregion

    #region variables
    /// <summary>
    /// Virutal Directory
    /// </summary>
    private readonly VirtualDirectory _mDirectory = new VirtualDirectory();

    /// <summary>
    /// ViewAs Button
    /// </summary>
    [SkinControlAttribute(2)]
    private GUIButtonControl btnViewAs = null;

    /// <summary>
    /// SortBy Button
    /// </summary>
    [SkinControlAttribute(3)]
    private GUISortButtonControl btnSortBy = null;

    /// <summary>
    /// PlayStream Button
    /// </summary>
    [SkinControlAttribute(5)]
    private GUIButtonControl btnPlayStream = null;

    /// <summary>
    /// PlayDisc Button
    /// </summary>
    [SkinControlAttribute(6)]
    private GUIButtonControl btnPlayDisc = null;

    /// <summary>
    /// Delete Button
    /// </summary>
    [SkinControlAttribute(7)]
    private GUIButtonControl btnDelete = null;

    /// <summary>
    /// FacadeView 
    /// </summary>
    [SkinControlAttribute(50)]
    private GUIFacadeControl facadeView = null;

    /// <summary>
    /// Playlistplayer instance
    /// </summary>
    private static readonly PlayListPlayer PlaylistPlayer;

    /// <summary>
    /// Current virtual Path
    /// </summary>
    private string _virtualPath;

    /// <summary>
    /// Display _name of the plugin
    /// </summary>
    private readonly string _displayName;

    /// <summary>
    /// Indicates if the my video Shares are used
    /// </summary>
    private readonly bool _useMyVideoShares;

    /// <summary>
    /// Indicates if the my music Shares are used
    /// </summary>
    private readonly bool _useMyMusicShares;

    /// <summary>
    /// List of _path of all shares
    /// </summary>
    private List<String> _sharePaths;

    /// <summary>
    /// Indicates if playlists should be treat as folders
    /// </summary>
    private readonly bool _treatPlaylistsAsFolders;

    /// <summary>
    /// Indicates if playlists should be treat as folders
    /// </summary>
    private readonly bool _useDvdnav;

    /// <summary>
    /// Disable sorting of elements. Onlöy needed for playlists.
    /// </summary>
    private bool _disableSorting;
    #endregion

    #region ctor
    /// <summary>
    /// Static Constructor for the Playlistplayer instance
    /// </summary>
    static MPlayerGUIPlugin()
    {
      PlaylistPlayer = PlayListPlayer.SingletonPlayer;
    }

    /// <summary>
    /// Standard constructor which set the WindowID
    /// </summary>
    public MPlayerGUIPlugin()
    {
      GetID = 9533;
      _virtualPath = String.Empty;
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        CurrentView = (View)xmlreader.GetValueAsInt(string.Empty, "view", (int)View.List);

        CurrentSortMethod = (VideoSort.SortMethod)xmlreader.GetValueAsInt(string.Empty, "sortmethod", (int)VideoSort.SortMethod.Name);

        _displayName = xmlreader.GetValueAsString("mplayer", "displayNameOfGUI", "My MPlayer");
        _useMyVideoShares = xmlreader.GetValueAsBool("mplayer", "useMyVideoShares", true);
        _useMyMusicShares = xmlreader.GetValueAsBool("mplayer", "useMyMusicShares", true);
        _treatPlaylistsAsFolders = xmlreader.GetValueAsBool("mplayer", "treatPlaylistAsFolders", false);
        _useDvdnav = xmlreader.GetValueAsBool("mplayer", "useDVDNAV", false);
        String mStrLanguage = xmlreader.GetValueAsString("skin", "language", "English");
        LocalizeStrings.Load(mStrLanguage);
      }
    }
    #endregion

    #region GUI methods

    /// <summary>
    /// Gets/Sets the current view mode
    /// </summary>
    private View CurrentView { get; set; }

    /// <summary>
    /// Gets/Sets the current sort method
    /// </summary>
    private VideoSort.SortMethod CurrentSortMethod { get; set; }

    /// <summary>
    /// Gets/Sets the current sorting direction
    /// </summary>
    private bool CurrentSortAsc { get; set; }

    /// <summary>
    /// Inits the window plugin
    /// </summary>
    /// <returns>true, if init was successful</returns>
    public override bool Init()
    {
      bool bResult = Load(GUIGraphicsContext.Skin + @"\myMPlayer.xml");
      InitializeVirtualDirectory();
      return bResult;
    }

    /// <summary>
    /// Handler for the GUIMessage of the MP System
    /// </summary>
    /// <param name="message">Message of MP</param>
    /// <returns>Result</returns>
    public override bool OnMessage(GUIMessage message)
    {
      if (message.Message == GUIMessage.MessageType.GUI_MSG_WINDOW_INIT)
      {

        bool mplayerPlayerAvailable;
        using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
        {
          mplayerPlayerAvailable = xmlreader.GetValueAsBool("plugins", "MPlayer", false);
        }

        mplayerPlayerAvailable = (mplayerPlayerAvailable & File.Exists(Config.GetFile(Config.Dir.Plugins, "ExternalPlayers", "MPlayer_ExtPlayer.dll")));
        if (!mplayerPlayerAvailable)
        {
          GUIDialogOK dlgOk = (GUIDialogOK)GUIWindowManager.GetWindow((int)Window.WINDOW_DIALOG_OK);
          dlgOk.SetHeading("My MPlayer GUI");
          dlgOk.SetLine(1, "MPlayer External Player not available!");
          dlgOk.SetLine(2, "Please activate it in the Setup");
          dlgOk.DoModal(GetID);
          GUIWindowManager.ShowPreviousWindow();
        }
      }
      return base.OnMessage(message);
    }

    /// <summary>
    /// Tries to start the playback of an disc
    /// </summary>
    /// <returns>true, if successful</returns>
    private void OnPlayDisc()
    {
      String url = "";
      string[] drives = Environment.GetLogicalDrives();
      bool discFound = false;
      foreach (string driveElement in drives)
      {
        if (Utils.getDriveType(driveElement) == 5)
        {
          string driveLetter = driveElement.Substring(0, 1);
          if (File.Exists(String.Format(@"{0}:\VIDEO_TS\VIDEO_TS.IFO", driveLetter)))
          {
            if (_useDvdnav)
            {
              url = "dvdnav://" + driveLetter + ":.mplayer";
            }else
            {
              url = "dvd://" + driveLetter + ":.mplayer";
            }
            discFound = true;
            break;
          }
          if (File.Exists(String.Format(@"{0}:\MPEGAV\AVSEQ01.DAT", driveLetter)))
          {
            url = "vcd://" + String.Format(@"{0}:\MPEGAV\AVSEQ01.DAT", driveLetter) + ".mplayer";
            discFound = true;
            break;
          }
          if (File.Exists(String.Format(@"{0}:\MPEG2\AVSEQ01.MPG", driveLetter)))
          {
            url = "svcd://" + String.Format(@"{0}:\MPEG2\AVSEQ01.MPG", driveLetter) + ".mplayer";
            discFound = true;
            break;
          }
          if (File.Exists(String.Format(@"{0}:\MPEG2\AVSEQ01.MPEG", driveLetter)))
          {
            url = "svcd://" + String.Format(@"{0}:\MPEG2\AVSEQ01.MPEG", driveLetter) + ".mplayer";
            discFound = true;
            break;
          }
        }
      }
      if (discFound)
      {
        if (g_Player.Playing)
        {
          g_Player.Stop();
        }
        g_Player.Play(url);
        GUIGraphicsContext.IsFullScreenVideo = true;
        GUIWindowManager.ActivateWindow((int)Window.WINDOW_FULLSCREEN_VIDEO);
        return;
      }
      //no disc in drive...
      GUIDialogOK dlgOk = (GUIDialogOK)GUIWindowManager.GetWindow((int)Window.WINDOW_DIALOG_OK);
      dlgOk.SetHeading(3);//my videos
      dlgOk.SetLine(1, 219);//no disc
      dlgOk.DoModal(GetID);
      return;
    }

    /// <summary>
    /// Tries to start the playback of an internet stream
    /// </summary>
    /// <returns>true, if successful</returns>
    private static void OnPlayStream()
    {
      VirtualKeyboard keyboard = (VirtualKeyboard)GUIWindowManager.GetWindow((int)Window.WINDOW_VIRTUAL_KEYBOARD);
      if (null == keyboard)
        return;
      keyboard.Reset();
      keyboard.Text = String.Empty;
      keyboard.DoModal(GUIWindowManager.ActiveWindow);
      if (keyboard.IsConfirmed)
      {
        if (!keyboard.Text.Equals(String.Empty))
        {
          if (g_Player.Playing)
          {
            g_Player.Stop();
          }
          String url = keyboard.Text + ".mplayer";
          if (url.StartsWith("rtsp:"))
          {
            url = "ZZZZ:" + url.Remove(0, 5);
          }

          g_Player.Play(url);
          if (g_Player.Player != null && g_Player.IsVideo)
          {
            GUIGraphicsContext.IsFullScreenVideo = true;
            GUIWindowManager.ActivateWindow((int)Window.WINDOW_FULLSCREEN_VIDEO);
          }
        }
      }
      return;
    }

    /// <summary>
    /// Handles click events of GUI controls
    /// </summary>
    /// <param name="controlId">ID of the GUI control</param>
    /// <param name="control">Control Object</param>
    /// <param name="actionType">Performed ActionType</param>
    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      if (control == btnViewAs)
      {
        bool shouldContinue;
        do
        {
          shouldContinue = false;
          switch (CurrentView)
          {
            case View.List:
              CurrentView = View.Icons;
              if (facadeView.ThumbnailLayout == null)
                shouldContinue = true;
              else
                facadeView.CurrentLayout = GUIFacadeControl.Layout.SmallIcons;
              break;
            case View.Icons:
              CurrentView = View.LargeIcons;
              if (facadeView.ThumbnailLayout == null)
                shouldContinue = true;
              else
                facadeView.CurrentLayout = GUIFacadeControl.Layout.LargeIcons;
              break;
            case View.LargeIcons:
              CurrentView = View.FilmStrip;
              if (facadeView.FilmstripLayout == null)
                shouldContinue = true;
              else
                facadeView.CurrentLayout = GUIFacadeControl.Layout.Filmstrip;
              break;
            case View.FilmStrip:
              CurrentView = View.List;
              if (facadeView.ListLayout == null)
                shouldContinue = true;
              else
                facadeView.CurrentLayout = GUIFacadeControl.Layout.List;
              break;
          }
        } while (shouldContinue);
        SelectCurrentItem();
        GUIControl.FocusControl(GetID, controlId);
        return;
      }

      if (control == btnSortBy)
      {
        switch (CurrentSortMethod)
        {
          case VideoSort.SortMethod.Name:
            CurrentSortMethod = VideoSort.SortMethod.Date;
            break;
          case VideoSort.SortMethod.Date:
            CurrentSortMethod = VideoSort.SortMethod.Size;
            break;
          case VideoSort.SortMethod.Size:
            CurrentSortMethod = VideoSort.SortMethod.Name;
            break;
        }
        OnSort();
        GUIControl.FocusControl(GetID, control.GetID);
      }
      if (control == btnPlayDisc)
      {
        OnPlayDisc();
        return;
      }
      if (control == btnPlayStream)
      {
        OnPlayStream();
        return;
      }
      if (control == facadeView)
      {
        GUIMessage msg = new GUIMessage(GUIMessage.MessageType.GUI_MSG_ITEM_SELECTED, GetID, 0, controlId, 0, 0, null);
        OnMessage(msg);
        if (actionType == Action.ActionType.ACTION_SHOW_INFO)
        {
          //OnInfo(iItem);
          facadeView.RefreshCoverArt();
        }
        if (actionType == Action.ActionType.ACTION_SELECT_ITEM)
        {
          OnClick();
        }
        if (actionType == Action.ActionType.ACTION_QUEUE_ITEM)
        {
          //OnQueueItem(iItem);
        }
      }
      if (control == btnDelete)
      {
        OnAction(new Action(Action.ActionType.ACTION_DELETE_ITEM, 0, 0));
      }
    }

    /// <summary>
    /// Handles the click in the facadeview
    /// </summary>
    private void OnClick()
    {
      GUIListItem item = facadeView.SelectedListItem;
      if (item == null)
        return;
      bool isFolderAMovie = false;
      string path = item.Path;
      if (item.IsFolder && !item.IsRemote)
      {
        // Check if folder is actually a DVD. If so don't browse this folder, but play the DVD!
        if ((File.Exists(path + @"\VIDEO_TS\VIDEO_TS.IFO")) && (item.Label != ".."))
        {
          isFolderAMovie = true;
          if(_useDvdnav)
          {
            path = "dvdnav://" + path;
          }else
          {
            path = "dvd://" + path;
          }
          //_path = item.Path + @"\VIDEO_TS\VIDEO_TS.IFO";
        }
        else if ((File.Exists(path + @"\MPEGAV\AVSEQ01.DAT")) && (item.Label != ".."))
        {
          isFolderAMovie = true;
          path = "vcd://" + String.Format(@"{0}\MPEGAV\AVSEQ01.DAT", item.Path);
        }
        else if ((File.Exists(path + @"\MPEG2\AVSEQ01.MPG")) && (item.Label != ".."))
        {
          isFolderAMovie = true;
          path = "svcd://" + String.Format(@"{0}\MPEG2\AVSEQ01.MPG", item.Path);
        }
        else if ((File.Exists(path + @"\MPEG2\AVSEQ01.MPEG")) && (item.Label != ".."))
        {
          isFolderAMovie = true;
          path = "svcd://" + String.Format(@"{0}\MPEG2\AVSEQ01.MPEG", item.Path);
        }
      }

      if ((item.IsFolder) && (!isFolderAMovie))
      {
        //currentSelectedItem = -1;
        _virtualPath = path;
        LoadDirectory(path);
      }
      else
      {
        if (PlayListFactory.IsPlayList(path))
        {
          LoadPlayList(path);
          return;
        }
        string movieFileName = _mDirectory.GetLocalFilename(path) + ".mplayer";
        if (movieFileName.StartsWith("rtsp:"))
        {
          movieFileName = "ZZZZ:" + movieFileName.Remove(0, 5);
        }
        g_Player.Play(movieFileName);
        if (g_Player.IsVideo)
        {
          GUIGraphicsContext.IsFullScreenVideo = true;
          GUIWindowManager.ActivateWindow((int)Window.WINDOW_FULLSCREEN_VIDEO);
        }
        return;
      }
    }

    /// <summary>
    /// Selects the current selected Item of the facadeView
    /// </summary>
    private void SelectCurrentItem()
    {
      int iItem = facadeView.SelectedListItemIndex;
      if (iItem > -1)
      {
        GUIControl.SelectItemControl(GetID, facadeView.GetID, iItem);
      }
      else if (facadeView.Count > 0)
      {
        GUIControl.SelectItemControl(GetID, facadeView.GetID, 0);
      }
      UpdateButtonStates();
    }

    /// <summary>
    /// Sorts the facadeView
    /// </summary>        
    private void OnSort()
    {
      if (!_disableSorting)
      {
        btnSortBy.Disabled = false;
        facadeView.Sort(new VideoSort(CurrentSortMethod, CurrentSortAsc));
        UpdateButtonStates();
      }
      else
      {
        btnSortBy.Disabled = true;
      }
    }

    /// <summary>
    /// Updates the labels of the button
    /// </summary>
    private void UpdateButtonStates()
    {
      GUIControl.HideControl(GetID, facadeView.GetID);

      int iControl = facadeView.GetID;
      GUIControl.ShowControl(GetID, iControl);
      GUIControl.FocusControl(GetID, iControl);
      string strLine = String.Empty;
      View view = CurrentView;
      switch (view)
      {
        case View.List:
          strLine = GUILocalizeStrings.Get(101);
          break;
        case View.Icons:
          strLine = GUILocalizeStrings.Get(100);
          break;
        case View.LargeIcons:
          strLine = GUILocalizeStrings.Get(417);
          break;
        case View.FilmStrip:
          strLine = GUILocalizeStrings.Get(733);
          break;
      }
      GUIControl.SetControlLabel(GetID, btnViewAs.GetID, strLine);

      switch (CurrentSortMethod)
      {
        case VideoSort.SortMethod.Name:
          strLine = GUILocalizeStrings.Get(365);
          break;
        case VideoSort.SortMethod.Date:
          strLine = GUILocalizeStrings.Get(104);
          break;
        case VideoSort.SortMethod.Size:
          strLine = GUILocalizeStrings.Get(105);
          break;
      }

      if (btnSortBy != null)
      {
        btnSortBy.Label = strLine;
        btnSortBy.IsAscending = CurrentSortAsc;
      }
    }

    /// <summary>
    /// Handles the page load event and initializes the plugin
    /// </summary>
    protected override void OnPageLoad()
    {
      base.OnPageLoad();
      GUIVideoOverlay videoOverlay = (GUIVideoOverlay)GUIWindowManager.GetWindow((int)Window.WINDOW_VIDEO_OVERLAY);
      if ((videoOverlay != null) && (videoOverlay.Focused))
        videoOverlay.Focused = false;

      LoadDirectory(_virtualPath);
      if (btnSortBy != null)
      {
        btnSortBy.SortChanged += SortChanged;
      }
      CurrentSortAsc = true;
      GUIControl.SetControlLabel(GetID, btnPlayStream.GetID, LocalizeStrings.Get((int)LocalizedMessages.PlayStream));
      GUIControl.SetControlLabel(GetID, btnPlayDisc.GetID, LocalizeStrings.Get((int)LocalizedMessages.PlayDisc));
      GUIControl.SetControlLabel(GetID, btnDelete.GetID, LocalizeStrings.Get((int)LocalizedMessages.Delete));

      OnSort();
      SwitchView();
      UpdateButtonStates();
    }

    /// <summary>
    /// Switches the view of the facadeview
    /// </summary>
    private void SwitchView()
    {
      if (facadeView == null)
        return;
      switch (CurrentView)
      {
        case View.List:
          facadeView.CurrentLayout = GUIFacadeControl.Layout.List;
          break;
        case View.Icons:
          facadeView.CurrentLayout = GUIFacadeControl.Layout.SmallIcons;
          break;
        case View.LargeIcons:
          facadeView.CurrentLayout = GUIFacadeControl.Layout.LargeIcons;
          break;
        case View.FilmStrip:
          facadeView.CurrentLayout = GUIFacadeControl.Layout.Filmstrip;
          break;
      }
    }


    /// <summary>
    /// Reads all extensions from external player plugin
    /// </summary>
    /// <returns>Lsit of all extensions</returns>
    private static ArrayList GetExtenstions()
    {
      ArrayList extensions = new ArrayList();
      XmlDocument doc = new XmlDocument();
      string path = Config.GetFile(Config.Dir.Config, "MPlayer_ExtPlayer.xml");
      doc.Load(path);
      if (doc.DocumentElement != null)
      {
        XmlNodeList listExtensions = doc.DocumentElement.SelectNodes("/mplayer/extensions/Extension");
        if (listExtensions != null)
          foreach (XmlNode nodeExtension in listExtensions)
          {
            extensions.Add(nodeExtension.Attributes["name"].Value);
          }
      }
      return extensions;
    }

    /// <summary>
    /// Loads a playlist and starts the playback
    /// </summary>
    /// <param name="playListFileName">Filename of the playlist</param>
    private void LoadPlayList(string playListFileName)
    {
      IPlayListIO loader = PlayListFactory.CreateIO(playListFileName);
      PlayList playlist = new PlayList();

      if (!loader.Load(playlist, playListFileName))
      {
        GUIDialogOK dlgOk = (GUIDialogOK)GUIWindowManager.GetWindow((int)Window.WINDOW_DIALOG_OK);
        if (dlgOk != null)
        {
          dlgOk.SetHeading(6);
          dlgOk.SetLine(1, 477);
          dlgOk.SetLine(2, String.Empty);
          dlgOk.DoModal(GetID);
        }
        return;
      }
      if (playlist.Count == 1)
      {
        string movieFileName = playlist[0].FileName + ".mplayer";
        if (movieFileName.StartsWith("rtsp:"))
        {
          movieFileName = "ZZZZ:" + movieFileName.Remove(0, 5);
        }
        if (g_Player.Play(movieFileName))
        {
          if (g_Player.Player != null && g_Player.IsVideo)
          {
            GUIGraphicsContext.IsFullScreenVideo = true;
            GUIWindowManager.ActivateWindow((int)Window.WINDOW_FULLSCREEN_VIDEO);
          }
        }
        return;
      }

      PlaylistPlayer.GetPlaylist(PlayListType.PLAYLIST_VIDEO).Clear();

      for (int i = 0; i < playlist.Count; ++i)
      {
        string movieFileName = playlist[i].FileName + ".mplayer";
        if (movieFileName.StartsWith("rtsp:"))
        {
          movieFileName = "ZZZZ:" + movieFileName.Remove(0, 5);
        }
        playlist[i].FileName = movieFileName;
        PlayListItem playListItem = playlist[i];
        playListItem.Type = PlayListItem.PlayListItemType.Unknown;
        PlaylistPlayer.GetPlaylist(PlayListType.PLAYLIST_VIDEO).Add(playListItem);
      }


      if (PlaylistPlayer.GetPlaylist(PlayListType.PLAYLIST_VIDEO).Count > 0)
      {
        PlaylistPlayer.GetPlaylist(PlayListType.PLAYLIST_VIDEO);
        PlaylistPlayer.CurrentPlaylistType = PlayListType.PLAYLIST_VIDEO;
        PlaylistPlayer.Reset();
        PlaylistPlayer.Play(0);

        if (g_Player.Player != null && g_Player.IsVideo)
        {
          GUIGraphicsContext.IsFullScreenVideo = true;
          GUIWindowManager.ActivateWindow((int)Window.WINDOW_FULLSCREEN_VIDEO);
        }
      }
    }

    /// <summary>
    /// Loads a new Directory
    /// </summary>
    /// <param name="newFolderName">Name of the folder</param>
    private void LoadDirectory(string newFolderName)
    {
      _disableSorting = false;
      String currentFolder = newFolderName;

      GUIControl.ClearControl(GetID, facadeView.GetID);
      List<GUIListItem> itemlist = _mDirectory.GetDirectoryExt(currentFolder);
      List<GUIListItem> itemfiltered = new List<GUIListItem>();
      for (int x = 0; x < itemlist.Count; ++x)
      {
        bool addItem = true;
        GUIListItem item1 = itemlist[x];
        for (int y = 0; y < itemlist.Count; ++y)
        {
          GUIListItem item2 = itemlist[y];
          if (x != y)
          {
            if (!item1.IsFolder || !item2.IsFolder)
            {
              if (!item1.IsRemote && !item2.IsRemote)
              {
                if (Utils.ShouldStack(item1.Path, item2.Path))
                {
                  if (String.Compare(item1.Path, item2.Path, true) > 0)
                  {
                    addItem = false;
                    // Update to reflect the stacked size
                    item1.FileInfo.Length += item2.FileInfo.Length;
                  }
                }
              }
            }
          }
        }

        if (addItem)
        {
          string label = item1.Label;

          Utils.RemoveStackEndings(ref label);
          item1.Label = label;
          if (_treatPlaylistsAsFolders && PlayListFactory.IsPlayList(item1.Path))
          {
            item1.IsFolder = true;
            Utils.SetDefaultIcons(item1);
          }
          itemfiltered.Add(item1);
        }
      }
      itemlist = itemfiltered;

      if (_treatPlaylistsAsFolders && PlayListFactory.IsPlayList(newFolderName))
      {
        IPlayListIO loader = PlayListFactory.CreateIO(newFolderName);
        PlayList playlist = new PlayList();
        _disableSorting = true;
        String basePath = Path.GetDirectoryName(Path.GetFullPath(newFolderName));
        if (loader.Load(playlist, newFolderName))
        {
          foreach (PlayListItem plItem in playlist)
          {
            GUIListItem item = new GUIListItem {Path = plItem.FileName};
            if (plItem.FileName.StartsWith(basePath))
            {
              String mainPath = plItem.FileName.Remove(0, basePath.Length + 1);
              if (mainPath.StartsWith("cue://") ||
              mainPath.StartsWith("cue://") ||
              mainPath.StartsWith("http://") ||
              mainPath.StartsWith("ftp://") ||
              mainPath.StartsWith("http_proxy://") ||
              mainPath.StartsWith("mms://") ||
              mainPath.StartsWith("mmst://") ||
              mainPath.StartsWith("mpst://") ||
              mainPath.StartsWith("rtsp://") ||
              mainPath.StartsWith("rtp://") ||
              mainPath.StartsWith("sdp://") ||
              mainPath.StartsWith("udp://") ||
              mainPath.StartsWith("unsv://"))
              {
                item.Path = mainPath;
              }
            }
            Log.Info("MPlayer GUI: " + item.Path);

            item.Label = plItem.Description;
            item.IsFolder = false;
            Utils.SetDefaultIcons(item);
            itemlist.Add(item);
          }
        }
      }

      foreach (GUIListItem item in itemlist)
      {
        facadeView.Add(item);
      }
      OnSort();
    }

    /// <summary>
    /// Overrides on action for deleting item
    /// </summary>
    /// <param name="action"></param>
    public override void OnAction(Action action)
    {
      if (action.wID == Action.ActionType.ACTION_DELETE_ITEM)
      {
        GUIListItem item = facadeView.SelectedListItem;
        if (item != null)
        {
          if (!item.IsFolder)
          {
            OnDeleteItem(item);
            return;
          }
        }
      }
      base.OnAction(action);
    }

    /// <summary>
    /// Handles the event, when the Sorting method changes
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private void SortChanged(object sender, SortEventArgs e)
    {
      CurrentSortAsc = e.Order != System.Windows.Forms.SortOrder.Descending;

      OnSort();
      GUIControl.FocusControl(GetID, ((GUIControl)sender).GetID);
    }

    /// <summary>
    /// Deletes an item from the gui list
    /// </summary>
    /// <param name="item">item to delete</param>
    private void OnDeleteItem(GUIListItem item)
    {
      if (item.IsRemote)
        return;
      if (!_mDirectory.RequestPin(item.Path))
      {
        return;
      }

      string movieFileName = Path.GetFileName(item.Path);
      string movieTitle = movieFileName;
      // delete single file
      GUIDialogYesNo dlgYesNo = (GUIDialogYesNo)GUIWindowManager.GetWindow((int)Window.WINDOW_DIALOG_YES_NO);
      if (null == dlgYesNo)
        return;
      dlgYesNo.SetHeading(GUILocalizeStrings.Get(925));
      dlgYesNo.SetLine(1, movieTitle);
      dlgYesNo.SetLine(2, String.Empty);
      dlgYesNo.SetLine(3, String.Empty);
      dlgYesNo.DoModal(GetID);

      if (!dlgYesNo.IsConfirmed)
        return;
      DoDeleteItem(item);

      int currentSelectedItem = facadeView.SelectedListItemIndex;
      if (currentSelectedItem > 0)
        currentSelectedItem--;
      LoadDirectory(_virtualPath);
      if (currentSelectedItem >= 0)
      {
        GUIControl.SelectItemControl(GetID, facadeView.GetID, currentSelectedItem);
      }
    }

    /// <summary>
    /// Deletes the item
    /// </summary>
    /// <param name="item">Item to delete</param>
    private void DoDeleteItem(GUIListItem item)
    {
      if (item.IsFolder)
      {
        if (item.IsRemote)
          return;
        if (item.Label != "..")
        {
          List<GUIListItem> items = _mDirectory.GetDirectoryUnProtectedExt(item.Path, false);
          foreach (GUIListItem subItem in items)
          {
            DoDeleteItem(subItem);
          }
          Utils.DirectoryDelete(item.Path);
        }
      }
      else
      {
        if (item.IsRemote)
          return;
        Utils.FileDelete(item.Path);
      }
    }


    /// <summary>
    /// Initialize the virtual directory
    /// </summary>
    private void InitializeVirtualDirectory()
    {
      _sharePaths = new List<string>();
      _mDirectory.SetExtensions(GetExtenstions());
      _mDirectory.AddExtension(".pls");
      _mDirectory.AddExtension(".m3u");

      _mDirectory.Clear();
      if (_useMyVideoShares)
      {
        AddSection("movies");
      }
      if (_useMyMusicShares)
      {
        AddSection("music");
      }
      XmlDocument doc = new XmlDocument();
      string path = Config.GetFile(Config.Dir.Config, "MPlayer_GUIPlugin.xml");
      doc.Load(path);
      if (doc.DocumentElement != null)
      {
        XmlNodeList listShare = doc.DocumentElement.SelectNodes("/mplayergui/Share");
        if (listShare != null)
          foreach (XmlNode nodeShare in listShare)
          {
            Share share = new Share
                            {Name = nodeShare.Attributes["name"].Value, Path = nodeShare.Attributes["path"].Value};
            if (!_sharePaths.Contains(share.Path.ToLower()))
            {
              _sharePaths.Add(share.Path.ToLower());
              _mDirectory.Add(share);
            }
          }
      }
    }

    /// <summary>
    /// Adds a section of MP shares
    /// </summary>
    /// <param name="section">Name of the section</param>
    private void AddSection(String section)
    {
      Share defaultshare = null;
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        string strDefault = xmlreader.GetValueAsString(section, "default", String.Empty);
        for (int i = 0; i < 20; i++)
        {
          string strShareName = String.Format("sharename{0}", i);
          string strSharePath = String.Format("sharepath{0}", i);
          string strPincode = String.Format("pincode{0}", i);

          string shareType = String.Format("sharetype{0}", i);
          string shareServer = String.Format("shareserver{0}", i);
          string shareLogin = String.Format("sharelogin{0}", i);
          string sharePwd = String.Format("sharepassword{0}", i);
          string sharePort = String.Format("shareport{0}", i);
          string remoteFolder = String.Format("shareremotepath{0}", i);
          string shareViewPath = String.Format("shareview{0}", i);

          Share share = new Share
                          {
                            Name = xmlreader.GetValueAsString(section, strShareName, String.Empty),
                            Path = xmlreader.GetValueAsString(section, strSharePath, String.Empty)
                          };

          string pinCode = Utils.DecryptPin(xmlreader.GetValueAsString(section, strPincode, string.Empty));
          if (pinCode != string.Empty)
            share.Pincode = Convert.ToInt32(pinCode);
          else
            share.Pincode = -1;

          share.IsFtpShare = xmlreader.GetValueAsBool(section, shareType, false);
          share.FtpServer = xmlreader.GetValueAsString(section, shareServer, String.Empty);
          share.FtpLoginName = xmlreader.GetValueAsString(section, shareLogin, String.Empty);
          share.FtpPassword = xmlreader.GetValueAsString(section, sharePwd, String.Empty);
          share.FtpPort = xmlreader.GetValueAsInt(section, sharePort, 21);
          share.FtpFolder = xmlreader.GetValueAsString(section, remoteFolder, "/");
          share.DefaultLayout = (GUIFacadeControl.Layout)xmlreader.GetValueAsInt(section, shareViewPath, (int)GUIFacadeControl.Layout.List);

          if (share.Name.Length > 0)
          {

            if (strDefault == share.Name)
            {
              share.Default = true;
              if (defaultshare == null)
              {
                defaultshare = share;
              }
            }

            if (!_sharePaths.Contains(share.Path.ToLower()))
            {
              _sharePaths.Add(share.Path.ToLower());
              _mDirectory.Add(share);
            }
          }
          else
            break;
        }
      }
    }
    #endregion

    #region IShowPlugin Member
    /// <summary>
    /// Returns true, if the Plugin should be listed in home as default
    /// </summary>
    /// <returns>true</returns>
    public bool ShowDefaultHome()
    {
      return true;
    }

    #endregion

    #region ISetupForm Member
    /// <summary>
    /// Description of the Plugin
    /// </summary>
    /// <returns>Plugin description</returns>
    public string Description()
    {
      return "MPlayer - The movie player - GUI Interface";
    }
    /// <summary>
    /// Returns Information of the Plugin for displaying in menu
    /// </summary>
    /// <param name="strButtonText">Text on the button</param>
    /// <param name="strButtonImage">Image of the button</param>
    /// <param name="strButtonImageFocus">Image of the button when focused</param>
    /// <param name="strPictureImage">Hover image of the plugin</param>
    /// <returns>true, if it should be display in menu</returns>
    public bool GetHome(out string strButtonText, out string strButtonImage, out string strButtonImageFocus, out string strPictureImage)
    {
      strButtonText = _displayName;
      strButtonImage = String.Empty;
      strButtonImageFocus = String.Empty;
      strPictureImage = "hover_my videos.png";
      return true;
    }
    /// <summary>
    /// Indicator if plugin can be enabled
    /// </summary>
    /// <returns>true, if it can be enabled</returns>
    public bool CanEnable()
    {
      return true;
    }
    /// <summary>
    /// Has the plugin a setup form?
    /// </summary>
    /// <returns>true, if there is a setup form</returns>
    public bool HasSetup()
    {
      return true;
    }
    /// <summary>
    /// Is by default enabled?
    /// </summary>
    /// <returns>true, if plugin is enabled by default</returns>
    public bool DefaultEnabled()
    {
      return true;
    }
    /// <summary>
    /// Method which shows the setup form of the plugin
    /// </summary>
    public void ShowPlugin()
    {
      ConfigurationForm confForm = new ConfigurationForm();
      confForm.ShowDialog();
    }
    /// <summary>
    /// Return the window id of the plugin
    /// </summary>
    /// <returns>Window ID of the plugin (9533)</returns>
    public int GetWindowId()
    {
      return 9533;
    }
    /// <summary>
    /// Author _name
    /// </summary>
    /// <returns>Name of the author</returns>
    public string Author()
    {
      return "MisterD";
    }
    /// <summary>
    /// Plugin _name
    /// </summary>
    /// <returns>Name of the plugin</returns>
    public string PluginName()
    {
      return "My MPlayer GUI";
    }

    #endregion
  }
}
