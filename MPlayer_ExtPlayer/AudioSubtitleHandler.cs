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
using System.Globalization;
using MediaPortal.Player;
using MediaPortal.GUI.Library;

namespace MPlayer
{
  /// <summary>
  /// This class handles all audio and subtitle relevant tasks for the MPlayer external player plugin,
  /// including read and write operations
  /// </summary>
  internal class AudioSubtitleHandler : IDisposable, IMessageHandler
  {
    #region variables
    /// <summary>
    /// Playing volume
    /// </summary>
    private int _volume;

    /// <summary>
    /// Number of AudioStreams in the file
    /// </summary>
    private int _numberOfAudioStreams;

    /// <summary>
    /// Number of Subtitles in the file
    /// </summary>
    private int _numberOfSubtitles;

    /// <summary>
    /// Number of the current audio stream
    /// </summary>
    private int _currentAudioStream;

    /// <summary>
    /// Number of the current subtitle stream
    /// </summary>
    private int _currentSubtitleStream;

    /// <summary>
    /// Mapping from Audio ID to File Audio IDs
    /// </summary>
    private readonly Dictionary<int, int> _audioID;

    /// <summary>
    /// Names of the Audio Streams
    /// </summary>
    private readonly Dictionary<int, String> _audioNames;

    /// <summary>
    /// Mapping from Subtitle ID to File Subtitle IDs
    /// </summary>
    private readonly Dictionary<int, int> _subtitleID;

    /// <summary>
    /// Names of the Subtitle Stream
    /// </summary>
    private readonly Dictionary<int, String> _subtitleNames;

    /// <summary>
    /// Display subtitles
    /// </summary>
    private bool _subtitlesEnabled;

    /// <summary>
    /// Current audio delay in milliseconds
    /// </summary>
    private int _currentAudioDelay;

    /// <summary>
    /// Current subtitle delay in milliseconds
    /// </summary>
    private int _currentSubtitleDelay;

    /// <summary>
    /// Step to change the audio delay in milliseconds
    /// </summary>
    private readonly int _audioDelayStep;

    /// <summary>
    /// Step to change the subtitle delay in millisceonds
    /// </summary>
    private readonly int _subtitleDelayStep;

    /// <summary>
    /// Current subtitle position
    /// </summary>
    private int _currentSubtitlePosition;

    /// <summary>
    /// Size of the subtitles
    /// </summary>
    private int _currentSubtitleSize;

    /// <summary>
    /// Reference to the main player component
    /// </summary>
    private readonly MPlayer_ExtPlayer _player;

    /// <summary>
    /// Configuration Manager;
    /// </summary>
    private readonly ConfigurationManager _configManager;

    /// <summary>
    /// Instance of the current OSD Handler
    /// </summary>
    private readonly IOSDHandler _osdHandler;

    /// <summary>
    /// Message handler for MP messages
    /// </summary>
    private readonly SendMessageHandler _mpMessageHandler;
    #endregion

    #region ctor
    /// <summary>
    /// Constructor which initialises the audio and subtitle handler
    /// </summary>
    /// <param _name="player">Instance of external player</param>
    /// <param _name="osdHandler">Instance of the osdHandler</param>
    public AudioSubtitleHandler(MPlayer_ExtPlayer player, IOSDHandler osdHandler)
    {
      _player = player;
      _osdHandler = osdHandler;
      _audioID = new Dictionary<int, int>();
      _audioNames = new Dictionary<int, string>();
      _subtitleID = new Dictionary<int, int>();
      _subtitleNames = new Dictionary<int, string>();
      _numberOfAudioStreams = 0;
      _numberOfSubtitles = 0;
      _currentAudioStream = 0;
      _currentSubtitleStream = 0;
      _currentAudioDelay = 0;
      _currentSubtitleDelay = 0;
      _configManager = ConfigurationManager.GetInstance();
      _audioDelayStep = _configManager.AudioDelayStep;
      _subtitleDelayStep = _configManager.SubtitleDelayStep;
      _subtitlesEnabled = _configManager.EnableSubtitles;
      _currentSubtitlePosition = _configManager.SubtitlePosition;
      _currentSubtitleSize = _configManager.SubtitleSize;
      if (OSInfo.OSInfo.OSList.WindowsVista == OSInfo.OSInfo.GetOSName() || OSInfo.OSInfo.OSList.Windows2008 == OSInfo.OSInfo.GetOSName() || OSInfo.OSInfo.OSList.Windows7 == OSInfo.OSInfo.GetOSName())
      {
        _mpMessageHandler = OnMessage;
        GUIWindowManager.Receivers += _mpMessageHandler;
      }
      _volume = 100;
    }

    /// <summary>
    /// Disposes the video handler
    /// </summary>
    public void Dispose()
    {
      if (_mpMessageHandler != null)
      {
        GUIWindowManager.Receivers -= _mpMessageHandler;
      }
      _audioNames.Clear();
      _subtitleNames.Clear();
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets/Sets the volume of the player
    /// </summary>
    public int Volume
    {
      get
      {
        return _volume;
      }
      set
      {
        _volume = value;
        _player.SendPausingKeepCommand("volume " + value + " 1");
      }
    }

    /// <summary>
    /// Gets the number of audio streams
    /// </summary>
    public int AudioStreams
    {
      get
      {
        if (_numberOfAudioStreams < 1)
          return 1;
        return _numberOfAudioStreams;
      }
    }

    /// <summary>
    /// Gets the number of subtitles
    /// </summary>
    public int SubtitleStreams
    {
      get
      {
        return _numberOfSubtitles;
      }
    }

    /// <summary>
    /// Gets/Sets the current audio streams
    /// </summary>
    public int CurrentAudioStream
    {
      get
      {
        return _currentAudioStream;
      }
      set
      {
        if (value < _numberOfAudioStreams)
        {
          _currentAudioStream = value;
          _player.SendPausingKeepCommand("set_property switch_audio " + _audioID[value]);
          _osdHandler.ShowAudioChanged(_audioNames[_audioID[value]]);
          _player.SendPausingKeepCommand("get_property switch_audio");
        }
      }
    }

    /// <summary>
    /// Gets/Sets the current subtitles streams
    /// </summary>
    public int CurrentSubtitleStream
    {
      get
      {
        return _currentSubtitleStream;
      }
      set
      {
        if (value < _numberOfSubtitles)
        {
          _currentSubtitleStream = value;
          _player.SendPausingKeepCommand("sub_select " + _subtitleID[value]);
          _osdHandler.ShowSubtitleChanged(_subtitleNames[_subtitleID[value]]);
        }

      }
    }

    /// <summary>
    /// Gets/Sets the subtitle position
    /// </summary>
    public int SubtitlePosition
    {
      get
      {
        return _currentSubtitlePosition;
      }
      set
      {
        if (value < 100 && value > 0)
        {
          _currentSubtitlePosition = value;
          _player.SendPausingKeepCommand("sub_pos " + _currentSubtitlePosition + " 1");
          _osdHandler.ShowSubtitlePositionChanged(_currentSubtitlePosition + "/100");
        }
      }
    }

    /// <summary>
    /// Sets the new subtitle size
    /// </summary>
    public int SubtitleSize
    {
      get
      {
        return _currentSubtitleSize;
      }
      set
      {
        if (value < 100 && value > 0)
        {
          _currentSubtitleSize = value;
          _player.SendPausingKeepCommand("sub_scale " + _currentSubtitleSize + " 1");
          _osdHandler.ShowSubtitleSizeChanged(_currentSubtitleSize + "/100");
        }
      }
    }

    /// <summary>
    /// Gets/Sets the delay of the audio stream
    /// </summary>
    public int AudioDelay
    {
      get
      {
        return _currentAudioDelay;
      }
      set
      {
        _currentAudioDelay = value;
        double seconds = _currentAudioDelay / 1000d;
        String temp = seconds.ToString();
        temp = temp.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");
        _player.SendPausingKeepCommand("audio_delay " + temp + " 1");
        _osdHandler.ShowAudioDelayChanged(_currentAudioDelay + " msec");
      }
    }

    /// <summary>
    /// Gets/Sets the subtitle delay
    /// </summary>
    public int SubtitleDelay
    {
      get
      {
        return _currentSubtitleDelay;
      }
      set
      {
        _currentSubtitleDelay = value;
        double seconds = _currentSubtitleDelay / 1000d;
        String temp = seconds.ToString();
        temp = temp.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");
        _player.SendPausingKeepCommand("sub_delay " + temp + " 1");
        _osdHandler.ShowSubtitleDelayChanged(_currentSubtitleDelay + " msec");
      }
    }

    /// <summary>
    /// Gets/Sets if subtitles are enabled
    /// </summary>
    public bool EnableSubtitle
    {
      get
      {
        return _subtitlesEnabled;
      }
      set
      {
        _subtitlesEnabled = value;
        if (_subtitlesEnabled)
        {
          _player.SendPausingKeepCommand("sub_visibility 1");
          _osdHandler.ShowSubtitleAcDeActivated(true);
        }
        else
        {
          _player.SendPausingKeepCommand("sub_visibility 0");
          _osdHandler.ShowSubtitleAcDeActivated(false);
          _player.SendPausingKeepCommand("forced_subs_only 1");
        }
      }
    }

    #endregion

    #region Public methods
    /// <summary>
    /// Gives the _name of the audio language
    /// </summary>
    /// <param _name="iStream">Index of the audio language</param>
    /// <returns>Name of the audio language</returns>
    public string AudioLanguage(int iStream)
    {
      try
      {
        if (_numberOfAudioStreams == 0)
        {
          return Strings.Unknown;
        }
        String audioName = _audioNames[_audioID[iStream]];
        String temp = audioName.Substring(0, 2);
        try
        {
          CultureInfo info = new CultureInfo(temp);
          audioName = info.DisplayName;
        } catch
        {
          Log.Info("MPlayer: Error while getting CulturInfo for: " + temp);
        }
        return audioName;
      } catch (Exception e)
      {
        Log.Info("MPlayer Error: Audiolanguage not found: " + e.Message);
        return Strings.Unknown;
      }
    }

    /// <summary>
    /// Gives the _name of the subtitle language
    /// </summary>
    /// <param _name="iStream">Index of the subtitle language</param>
    /// <returns>Name of the subtitle language</returns>
    public string SubtitleLanguage(int iStream)
    {
      try
      {
        if (_numberOfSubtitles == 0)
        {
          return Strings.Unknown;
        }
        return _subtitleNames[_subtitleID[iStream]];
      } catch (Exception e)
      {
        Log.Info("MPlayer Error: SubtitleLanguage not found: " + e.Message);
        return Strings.Unknown;
      }
    }

    /// <summary>
    /// Handles MP internal action related for the internal osd handler
    /// </summary>
    /// <param _name="action">Action to handle</param>
    public void OnAction(Action action)
    {
      if (_player.FullScreen && !_osdHandler.OsdVisible)
      {
        switch (action.wID)
        {
          case Action.ActionType.ACTION_AUDIO_DELAY_PLUS:
            AudioDelay += _audioDelayStep;
            break;
          case Action.ActionType.ACTION_AUDIO_DELAY_MIN:
            AudioDelay -= _audioDelayStep;
            break;
          case Action.ActionType.ACTION_SUBTITLE_DELAY_PLUS:
            SubtitleDelay += _subtitleDelayStep;
            break;
          case Action.ActionType.ACTION_SUBTITLE_DELAY_MIN:
            SubtitleDelay -= _subtitleDelayStep;
            break;
          case Action.ActionType.ACTION_MOVE_SELECTED_ITEM_UP:
            SubtitlePosition++;
            break;
          case Action.ActionType.ACTION_MOVE_SELECTED_ITEM_DOWN:
            SubtitlePosition--;
            break;
          case Action.ActionType.ACTION_INCREASE_TIMEBLOCK:
            SubtitleSize++;
            break;
          case Action.ActionType.ACTION_DECREASE_TIMEBLOCK:
            SubtitleSize--;
            break;
        }
      }
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Tries to get the language _name by creating a culture info
    /// </summary>
    /// <param _name="languageName">Identification of the lanugae (2 or 3 characters)</param>
    /// <returns></returns>
    private static String getLanguageName(String languageName)
    {
      String result = languageName;
      String temp = languageName.Substring(0, 2);
      try
      {
        CultureInfo info = new CultureInfo(temp);
        result = info.DisplayName;
      } catch
      {
        Log.Info("MPlayer: Error while getting CulturInfo for: " + temp);
      }
      return result;
    }

    /// <summary>
    /// Handles the on message event. Needed for handling the volume change event
    /// </summary>
    /// <param _name="message">Message to handle</param>
    private void OnMessage(GUIMessage message)
    {
      switch (message.Message)
      {
        case GUIMessage.MessageType.GUI_MSG_AUDIOVOLUME_CHANGED:
          double percentage;
          if (VolumeHandler.Instance.IsMuted)
          {
            percentage = 0;
          }
          else
          {
            double currentVolume = VolumeHandler.Instance.Volume;
            double maximumVolume = VolumeHandler.Instance.Maximum;
            percentage = currentVolume / maximumVolume * 100;
          }
          Volume = (int)percentage;
          break;
      }
    }
    #endregion

    #region IMessageHandler Member
    /// <summary>
    /// Handles a message that is retrieved from the MPlayer process
    /// </summary>
    /// <param _name="message">Message to handle</param>
    public void HandleMessage(string message)
    {
      if (message.StartsWith("DVDNAV, switched to title"))
      {
        _numberOfAudioStreams = 0;
        _audioID.Clear();
        _audioNames.Clear();
        _numberOfSubtitles = 0;
        _subtitleID.Clear();
        _subtitleNames.Clear();
      }else if (message.StartsWith("ID_AUDIO_ID"))
      {
        int temp;
        Int32.TryParse(message.Substring(12), out temp);
        if (!_audioNames.ContainsKey(temp))
        {
          _audioID.Add(_numberOfAudioStreams, temp);
          _audioNames.Add(temp, Strings.Unknown);
          _numberOfAudioStreams++;
        }
      }
      else if (message.StartsWith("ID_AID_"))
      {
        String help = message.Substring(7);
        int index = help.IndexOf('_');
        int temp;
        Int32.TryParse(help.Substring(0, index), out temp);
        index = message.IndexOf('=');
        _audioNames[temp] = getLanguageName(message.Substring(index + 1));
      }
      else if (message.StartsWith("ID_SUBTITLE_ID"))
      {
        int temp;
        Int32.TryParse(message.Substring(15), out temp);
        if (!_subtitleNames.ContainsKey(temp))
        {
          _subtitleID.Add(_numberOfSubtitles, temp);
          _subtitleNames.Add(temp, Strings.Unknown);
          _numberOfSubtitles++;
        }
      }
      else if (message.StartsWith("ID_SID_"))
      {
        String help = message.Substring(7);
        int index = help.IndexOf('_');
        int temp;
        Int32.TryParse(help.Substring(0, index), out temp);
        index = message.IndexOf('=');
        _subtitleNames[temp] = getLanguageName(message.Substring(index + 1));
      }
      else if (message.StartsWith("VO: [directx] ") ||
        message.StartsWith("VO: [direct3d] ") ||
        message.StartsWith("VO: [gl2] ") ||
        message.StartsWith("VO: [gl] "))
      {
        if (_mpMessageHandler != null)
        {
          _player.SendCommand("use_master");
          double percentage;
          if (VolumeHandler.Instance.IsMuted)
          {
            percentage = 0;
          }
          else
          {
            double currentVolume = VolumeHandler.Instance.Volume;
            double maximumVolume = VolumeHandler.Instance.Maximum;
            percentage = currentVolume / maximumVolume * 100;
          }
          Volume = (int)percentage;
        }
        if (_subtitlesEnabled)
        {
          _player.SendCommand("sub_visibility 1");
        }
        else
        {
          _player.SendCommand("sub_visibility 0");
          _player.SendCommand("forced_subs_only 1");
        }
      }
      else if (message.StartsWith("ANS_switch_audio"))
      {
        int id;
        Int32.TryParse(message.Substring(17), out id);
        if (id != _audioID[CurrentAudioStream])
        {
          _player.SendPausingKeepCommand("switch_audio");
          _player.SendPausingKeepCommand("get_property switch_audio");
        }
      }
    }
    #endregion
  }
}
