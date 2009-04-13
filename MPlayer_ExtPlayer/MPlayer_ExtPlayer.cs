#region Copyright (C) 2006-2009 MisterD

/* 
 *	Copyright (C) 2006-2009 MisterD
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
using System.Diagnostics;
using System.IO;
using System.Threading;
using MediaPortal.Player;
using MediaPortal.GUI.Library;

namespace MPlayer
{

  /// <summary>
  /// External Player for the Mplayer Video (and audio) player
  /// </summary>
  public class MPlayer_ExtPlayer : IExternalPlayer, ISetupForm
  {

    #region variables
    /// <summary>
    /// State of the player
    /// </summary>
    private PlayState _playState = PlayState.Init;

    /// <summary>
    /// MPlayer process
    /// </summary>
    private Process _mplayerProcess;

    /// <summary>
    /// Input stream of the process
    /// </summary>
    private StreamWriter _input;

    /// <summary>
    /// Current playing file
    /// </summary>
    private String _currentFile;

    /// <summary>
    /// ActionHandler on MP Actions
    /// </summary>
    private OnActionHandler _actionHandler;

    /// <summary>
    /// Exit Handler of the MPlayer process
    /// </summary>
    private EventHandler _exitHandler;

    /// <summary>
    /// DataReceivedHandler of the MPlayer process to parse the output message of MPlayer
    /// </summary>
    private DataReceivedEventHandler _dataReceivedHandler;

    /// <summary>
    /// Configuration Manager
    /// </summary>
    private readonly ConfigurationManager _configManager;

    /// <summary>
    /// Handler for all video rlevant tasks
    /// </summary>
    private VideoHandler _videoHandler;

    /// <summary>
    /// Handler for all audio and subtitle relevant tasks
    /// </summary>
    private AudioSubtitleHandler _audioSubtitleHandler;

    /// <summary>
    /// Handler for all seeking relevant tasks
    /// </summary>
    private SeekingHandler _seekingHandler;

    /// <summary>
    /// Handler for the internal osd of MPlayer
    /// </summary>
    private IOSDHandler _osdHandler;

    /// <summary>
    /// List of all message handlers
    /// </summary>
    private List<IMessageHandler> _messageHandlers;
    #endregion

    #region ctor
    /// <summary>
    /// Simple dummy constructor
    /// </summary>
    public MPlayer_ExtPlayer()
    {
      _configManager = ConfigurationManager.GetInstance();

    }
    #endregion

    #region IExternPlayer Member
    /// <summary>
    /// Author Name
    /// </summary>
    public override string AuthorName
    {
      get { return "MisterD"; }
    }

    /// <summary>
    /// Returns the supported Extensions
    /// </summary>
    /// <returns>String-Array of supported extensions</returns>
    public override string[] GetAllSupportedExtensions()
    {
      return _configManager.SupportedExtensions;
    }

    /// <summary>
    /// Name of the player
    /// </summary>
    public override string PlayerName
    {
      get { return "MPlayer"; }
    }

    /// <summary>
    /// Method which specifiy, if the player supports the file
    /// </summary>
    /// <param _name="filename">Filename</param>
    /// <returns>true, if the player can play this file</returns>
    public override bool SupportsFile(string filename)
    {
      return _configManager.SupportsFile(filename);
    }
    /// <summary>
    /// Version number of the player
    /// </summary>
    public override string VersionNumber
    {
      get { return "1.0"; }
    }
    #endregion

    #region ISetupForm Member
    /// <summary>
    /// Description of the Plugin
    /// </summary>
    /// <returns>Plugin description</returns>
    public override string Description()
    {
      return "MPlayer - The movie player";
    }

    /// <summary>
    /// Returns Information of the Plugin for displaying in menu
    /// </summary>
    /// <param _name="strButtonText">Text on the button</param>
    /// <param _name="strButtonImage">Image of the button</param>
    /// <param _name="strButtonImageFocus">Image of the button when focused</param>
    /// <param _name="strPictureImage">Hover image of the plugin</param>
    /// <returns>true, if it should be display in menu</returns>
    public override bool GetHome(out string strButtonText, out string strButtonImage, out string strButtonImageFocus, out string strPictureImage)
    {
      strButtonText = PluginName();
      strButtonImage = String.Empty;
      strButtonImageFocus = String.Empty;
      strPictureImage = String.Empty;
      return false;
    }

    /// <summary>
    /// Indicator if plugin can be enabled
    /// </summary>
    /// <returns>true, if it can be enabled</returns>
    new public bool CanEnable()
    {
      return true;
    }

    /// <summary>
    /// Has the plugin a setup form?
    /// </summary>
    /// <returns>true, if there is a setup form</returns>
    new public bool HasSetup()
    {
      return true;
    }

    /// <summary>
    /// Is by default enabled?
    /// </summary>
    /// <returns>true, if plugin is enabled by default</returns>
    new public bool DefaultEnabled()
    {
      return true;
    }

    /// <summary>
    /// Method which shows the setup form of the plugin
    /// </summary>
    public override void ShowPlugin()
    {
      ConfigurationForm confForm = new ConfigurationForm();
      confForm.ShowDialog();
    }
    #endregion

    #region IPlayer Member
    /// <summary>
    /// Plays the file
    /// </summary>
    /// <param _name="strFile">Filename</param>
    /// <returns>true, if playing has started</returns>
    public override bool Play(string strFile)
    {
      bool result;
      bool controlAdded = false;
      try
      {
        if (strFile.EndsWith(".mplayer"))
        {
          strFile = strFile.Remove(strFile.LastIndexOf(".mplayer"));
        }
        if (strFile.StartsWith("ZZZZ:"))
        {
          strFile = "rtsp:" + strFile.Remove(0, 5);
        }
        bool isVideo = _configManager.HasFileOrStreamVideo(strFile);
        InitSystem(isVideo);
        _currentFile = strFile;
        _mplayerProcess = _configManager.CreateProcessForFileName(strFile, _videoHandler.GetVideoHandle());
        _videoHandler.HasVideo = isVideo;
        if (isVideo)
        {
          _isVisible = true;
          _videoHandler.AddVideoWindowToForm();
          controlAdded = true;
        }
        else
        {
          _isVisible = false;
        }
        _exitHandler = MplayerProcess_Exited;
        _mplayerProcess.Exited += _exitHandler;
        _mplayerProcess.Start();
        _dataReceivedHandler = MplayerProcess_OutputDataReceived;
        _mplayerProcess.OutputDataReceived += _dataReceivedHandler;
        _mplayerProcess.BeginOutputReadLine();
        _input = _mplayerProcess.StandardInput;
        result = true;
      } catch (Exception e)
      {
        Log.Info("MPlayer Error: " + e.Message);
        Log.Error(e);
        if (controlAdded)
        {
          _videoHandler.RemoveVideoWindowToForm();
        }
        _currentFile = String.Empty;
        _playState = PlayState.Ended;
        result = false;
        _osdHandler.Dispose();
      }
      return result;
    }

    /// <summary>
    /// Indicator if the playing has ended
    /// </summary>
    public override bool Ended
    {
      get
      {
        return _playState == PlayState.Ended;
      }
    }

    /// <summary>
    /// Stops the playback
    /// </summary>
    public override void Stop()
    {
      SendCommand("quit");
      if (_videoHandler.HasVideo)
      {
        _videoHandler.RemoveVideoWindowToForm();
      }
      _mplayerProcess.OutputDataReceived -= _dataReceivedHandler;
      _videoHandler.Dispose();
      _audioSubtitleHandler.Dispose();
      _playState = PlayState.Stopped;
      _currentFile = String.Empty;
      GUIWindowManager.OnNewAction -= _actionHandler;
      _mplayerProcess.Exited -= _exitHandler;
      _osdHandler.Dispose();
      int loop = 0;
      while (!_mplayerProcess.HasExited && loop < 20)
      {
        Thread.Sleep(50);
        loop++;
      }
      if (!_mplayerProcess.HasExited)
      {
        _mplayerProcess.Kill();
      }
    }

    /// <summary>
    /// Pause the playback
    /// </summary>
    public override void Pause()
    {
      SendCommand("pause");
      if (_playState == PlayState.Playing)
      {
        _osdHandler.SetOSDVisibleForPause(true);
        _osdHandler.ActivateOSD(false);
        _playState = PlayState.Paused;
      }
      else if (_playState == PlayState.Paused)
      {
        _osdHandler.SetOSDVisibleForPause(false);
        _osdHandler.DeactivateOSD(false);
        _playState = PlayState.Playing;
      }
    }

    /// <summary>
    /// Returns if the playing is paused
    /// </summary>
    public override bool Paused
    {
      get
      {
        return _playState == PlayState.Paused;
      }
    }

    /// <summary>
    /// Returns if the playing state is playing
    /// </summary>
    public override bool Playing
    {
      get
      {
        return _playState == PlayState.Playing || _playState == PlayState.Paused;
      }
    }

    /// <summary>
    /// Returns if the playing state is stopped
    /// </summary>
    public override bool Stopped
    {
      get
      {
        return _playState == PlayState.Stopped || _playState == PlayState.Ended;
      }
    }

    /// <summary>
    /// Name of the _currentFile
    /// </summary>
    public override string CurrentFile
    {
      get
      {
        return _currentFile;
      }
    }

    /// <summary>
    /// Indicates, if playing an audio cd
    /// </summary>
    public override bool IsCDA
    {
      get
      {
        return _currentFile != null && _currentFile.EndsWith(".cda");
      }
    }

    /// <summary>
    /// Indicates, if playing a DVD menu
    /// </summary>
    public override bool IsDVDMenu
    {
      get
      {
        return IsDVD && _seekingHandler.IsDVDMenu;
      }
    }
    /// <summary>
    /// Indicates, if playing a DVD
    /// </summary>
    public override bool IsDVD
    {
      get
      {
        return _currentFile != null && (_currentFile.StartsWith("dvd://") || _currentFile.StartsWith("dvdnav://"));
      }
    }

    #region Internal Property
    /// <summary>
    /// Gets the internal player state
    /// </summary>
    internal PlayState PlayState
    {
      get { return _playState; }
    }
    #endregion

    #region Action Handling
    /// <summary>
    /// Handles the actions of Mediaportal.
    /// Here are the OSD-Actions are handled to use the mplayer _osd
    /// </summary>
    /// <param _name="action">Performed Action</param>
    private void OnNewAction(Action action)
    {
      _osdHandler.OnAction(action);
      _seekingHandler.OnAction(action);
      _audioSubtitleHandler.OnAction(action);

      switch (action.wID)
      {
        case Action.ActionType.ACTION_MOVE_LEFT:
        case Action.ActionType.ACTION_STEP_BACK:
          if (IsDVDMenu)
          {
            SendCommand("dvdnav left");
          }
          break;

        case Action.ActionType.ACTION_MOVE_RIGHT:
        case Action.ActionType.ACTION_STEP_FORWARD:
          if (IsDVDMenu)
          {
            SendCommand("dvdnav right");
          }
          break;
        case Action.ActionType.ACTION_MOVE_UP:
        case Action.ActionType.ACTION_BIG_STEP_FORWARD:
          if (IsDVDMenu)
          {
            SendCommand("dvdnav up");
          }
          break;

        case Action.ActionType.ACTION_MOVE_DOWN:
        case Action.ActionType.ACTION_BIG_STEP_BACK:
          if (IsDVDMenu)
          {
            SendCommand("dvdnav down");
          }
          break;

        case Action.ActionType.ACTION_SELECT_ITEM:
          if (IsDVDMenu)
          {
            SendCommand("dvdnav select");
            SendCommand("get_property stream_pos");
            SendCommand("get_property stream_pos");
          }
          break;

        case Action.ActionType.ACTION_DVD_MENU:
          SendCommand("dvdnav menu");
          SendCommand("get_property stream_pos");
          SendCommand("get_property stream_pos");
          break;
      }
    }

    /// <summary>
    /// This methods is called by Mediaportal periodically. This guarantees that osd is updated immediately
    /// </summary>
    public override void Process()
    {
      if (_osdHandler != null)
      {
        _osdHandler.UpdateGUI();
      }
      if (_seekingHandler != null)
      {
        _seekingHandler.CheckPosition();
      }
      base.Process();
    }
    #endregion

    #region IDisposable
    /// <summary>
    /// Release the player
    /// </summary>
    public override void Release()
    {
      if (_playState == PlayState.Playing || _playState == PlayState.Paused)
      {
        SendCommand("quit");
        _mplayerProcess.OutputDataReceived -= _dataReceivedHandler;
        _videoHandler.Dispose();
        _audioSubtitleHandler.Dispose();
        GUIWindowManager.OnNewAction -= _actionHandler;
        _mplayerProcess.Exited -= _exitHandler;
      }
    }
    #endregion

    #region InitSystem
    /// <summary>
    /// Initialize the system (Objects, Controls etc.)
    /// </summary>
    /// <param _name="isVideo">Indícate if the file has video</param>
    private void InitSystem(bool isVideo)
    {
      _playState = PlayState.Playing;
      if (_configManager.OsdMode == OSDMode.InternalMPlayer || !isVideo)
      {
        _osdHandler = new InternalOSDHandler(this, true);
      }
      else if (_configManager.OsdMode == OSDMode.ExternalOSDLibrary)
      {
        _osdHandler = new ExternalOSDLibrary(this);
      }
      _videoHandler = new VideoHandler(this, _osdHandler);
      _audioSubtitleHandler = new AudioSubtitleHandler(this, _osdHandler);
      _seekingHandler = new SeekingHandler(this, _osdHandler);
      _messageHandlers = new List<IMessageHandler>();
      _messageHandlers.Add(_videoHandler);
      _messageHandlers.Add(_audioSubtitleHandler);
      _messageHandlers.Add(_seekingHandler);
      _messageHandlers.Add(_osdHandler);
      _actionHandler = OnNewAction;
      GUIWindowManager.OnNewAction += _actionHandler;
    }
    #endregion

    #region MPlayer process communication
    /// <summary>
    /// Occures, when the mplayer process exists
    /// </summary>
    /// <param _name="sender">Sender</param>
    /// <param _name="e">EventArguments</param>
    private void MplayerProcess_Exited(object sender, EventArgs e)
    {
      Log.Info("MPlayer: Player exited");
      _playState = PlayState.Ended;
    }

    /// <summary>
    /// Sends a command with pausing_keep to the mplayer process
    /// </summary>
    /// <param _name="command">Mplayer command</param>
    internal void SendPausingKeepCommand(string command)
    {
      SendCommand("pausing_keep " + command);
    }

    /// <summary>
    /// Sends a command to the mplayer process
    /// </summary>
    /// <param _name="command">Mplayer command</param>
    internal void SendCommand(string command)
    {
      Log.Debug("MPlayer: Send command: " + command);
      const int linefeed = 10;
      _input.Write(command + (char)linefeed);
    }

    /// <summary>
    /// Method which handles asynchron the received data of the mplayer process
    /// Therefor the information will be parsed and the data extracted and stored in the object
    /// </summary>
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event _arguments</param>
    private void MplayerProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
      try
      {
        String result = e.Data;
        if (result != null)
        {
          Log.Info("MPlayer: Data received: " + e.Data);
          foreach (IMessageHandler messageHandler in _messageHandlers)
          {
            messageHandler.HandleMessage(result);
          }
          if (result.StartsWith("Exiting... (End of file)"))
          {
            _playState = PlayState.Ended;
          }
        }
      } catch (Exception ex)
      {
        Log.Error("MPlayer: exception occured while handling message\n{0}", ex);
      }
    }
    #endregion

    #region VideoHandler
    /// <summary>
    /// Has the file a video stream
    /// </summary>
    public override bool HasVideo
    {
      get
      {
        return _videoHandler.HasVideo;
      }
    }

    /// <summary>
    /// Places the video window
    /// </summary>
    public override void SetVideoWindow()
    {
      _videoHandler.SetVideoWindow();
    }

    /// <summary>
    /// Gets/Sets the x position of the video window
    /// </summary>
    public override int PositionX
    {
      get { return _videoHandler.PositionX; }
      set
      {
        _videoHandler.PositionX = value;
      }
    }

    /// <summary>
    /// Gets/Sets the y position of the video window
    /// </summary>
    public override int PositionY
    {
      get { return _videoHandler.PositionY; }
      set
      {
        _videoHandler.PositionY = value;
      }
    }

    /// <summary>
    /// Gets/Sets the width of the video window
    /// </summary>
    public override int RenderWidth
    {
      get { return _videoHandler.RenderWidth; }
      set
      {
        _videoHandler.RenderWidth = value;
      }
    }

    /// <summary>
    /// Gets/Sets the height of the video window
    /// </summary>
    public override int RenderHeight
    {
      get { return _videoHandler.RenderHeight; }
      set
      {
        _videoHandler.RenderHeight = value;
      }
    }

    /// <summary>
    /// Returns the width of the video
    /// </summary>
    public override int Width
    {
      get { return _videoHandler.Width; }
    }

    /// <summary>
    /// Returns the height of the video
    /// </summary>
    public override int Height
    {
      get { return _videoHandler.Height; }
    }

    /// <summary>
    /// Gets/Sets if the video should be as an fullscreen video
    /// </summary>
    public override bool FullScreen
    {
      get
      {
        if (_videoHandler != null)
        {
          return _videoHandler.FullScreen;
        }
        return false;
      }
      set
      {
        _videoHandler.FullScreen = value;
      }
    }

    /// <summary>
    /// Gets/Sets the _contrast of the video window
    /// </summary>
    public override int Contrast
    {
      get
      {
        return _videoHandler.Contrast;
      }
      set
      {
        _videoHandler.Contrast = value;
      }
    }

    /// <summary>
    /// Gets/Sets the _brightness of the video window
    /// </summary>
    public override int Brightness
    {
      get
      {
        return _videoHandler.Brightness;
      }
      set
      {
        _videoHandler.Brightness = value;
      }
    }

    /// <summary>
    /// Gets/Sets the _gamma of the video window
    /// </summary>
    public override int Gamma
    {
      get
      {
        return _videoHandler.Gamma;
      }
      set
      {
        _videoHandler.Gamma = value;
      }
    }

    /// <summary>
    /// Gets A/R Geometry of the video window
    /// </summary>
    public override Geometry.Type ARType
    {
      get { return _videoHandler.ARType; }
      set
      {
        _videoHandler.ARType = value;
      }
    }

    #endregion

    #region AudioSubtitleHandler
    /// <summary>
    /// Gets the number of audio streams
    /// </summary>
    public override int AudioStreams
    {
      get
      {
        return _audioSubtitleHandler.AudioStreams;
      }
    }

    /// <summary>
    /// Gets the number of subtitles
    /// </summary>
    public override int SubtitleStreams
    {
      get
      {
        return _audioSubtitleHandler.SubtitleStreams;
      }
    }

    /// <summary>
    /// Gets/Sets the current audio streams
    /// </summary>
    public override int CurrentAudioStream
    {
      get
      {
        return _audioSubtitleHandler.CurrentAudioStream;
      }
      set
      {
        _audioSubtitleHandler.CurrentAudioStream = value;
      }
    }

    /// <summary>
    /// Gets/Sets the current subtitles streams
    /// </summary>
    public override int CurrentSubtitleStream
    {
      get
      {
        return _audioSubtitleHandler.CurrentSubtitleStream;
      }
      set
      {
        _audioSubtitleHandler.CurrentSubtitleStream = value;
      }
    }

    /// <summary>
    /// Gives the _name of the audio language
    /// </summary>
    /// <param _name="iStream">Index of the audio language</param>
    /// <returns>Name of the audio language</returns>
    public override string AudioLanguage(int iStream)
    {
      return _audioSubtitleHandler.AudioLanguage(iStream);
    }

    /// <summary>
    /// Gives the _name of the subtitle language
    /// </summary>
    /// <param _name="iStream">Index of the subtitle language</param>
    /// <returns>Name of the subtitle language</returns>
    public override string SubtitleLanguage(int iStream)
    {
      return _audioSubtitleHandler.SubtitleLanguage(iStream);
    }

    /// <summary>
    /// Gets/Sets if subtitles are enabled
    /// </summary>
    public override bool EnableSubtitle
    {
      get
      {
        return _audioSubtitleHandler.EnableSubtitle;
      }
      set
      {
        _audioSubtitleHandler.EnableSubtitle = value;
      }
    }

    /// <summary>
    /// Gets/Sets the volume of the player
    /// </summary>
    public override int Volume
    {
      get
      {
        return _audioSubtitleHandler.Volume;
      }
      set
      {
        _audioSubtitleHandler.Volume = value;
      }
    }
    #endregion

    #region SeekingHandler
    /// <summary>
    /// Returns that the player supports seeking
    /// </summary>
    /// <returns>true</returns>
    public override bool CanSeek()
    {
      return _seekingHandler.CanSeek();
    }

    /// <summary>
    /// Seek to an absoulte position in seconds
    /// </summary>
    /// <param _name="dTime">New absoulte position in seconds</param>
    public override void SeekAbsolute(double dTime)
    {
      _seekingHandler.SeekAbsolute(dTime);
    }

    /// <summary>
    /// Seek to an relative position in seconds
    /// </summary>
    /// <param _name="dTime">Relative position in seconds</param>
    public override void SeekRelative(double dTime)
    {
      _seekingHandler.SeekRelative(dTime);
    }

    /// <summary>
    /// Seek to an absoulte position in percentage
    /// </summary>
    /// <param _name="iPercentage">New absoulte position in percentage</param>
    public override void SeekAsolutePercentage(int iPercentage)
    {
      _seekingHandler.SeekAsolutePercentage(iPercentage);
    }

    /// <summary>
    /// Seek to an relative position in percentage
    /// </summary>
    /// <param _name="iPercentage">Relative position in percentage</param>
    public override void SeekRelativePercentage(int iPercentage)
    {
      _seekingHandler.SeekRelativePercentage(iPercentage);
    }

    /// <summary>
    /// Gets/Sets the playing _speed of the file
    /// </summary>
    public override int Speed
    {
      get
      {
        return _seekingHandler.Speed;
      }
      set
      {
        _seekingHandler.Speed = value;
      }
    }

    /// <summary>
    /// Sets/Gets the current position in the file
    /// </summary>
    public override double CurrentPosition
    {
      get
      {
        return _seekingHandler.CurrentPosition;
      }
    }

    /// <summary>
    /// Total length of the file
    /// </summary>
    public override double Duration
    {
      get
      {
        return _seekingHandler.Duration;
      }
    }
    #endregion
    #endregion

  }
}
