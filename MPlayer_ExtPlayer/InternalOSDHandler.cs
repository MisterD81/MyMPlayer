#region Copyright (C) 2006-2015 MisterD

/* 
 *	Copyright (C) 2006-2015 MisterD
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
using System.Threading;
using MediaPortal.Player;
using MediaPortal.GUI.Library;
using Action = MediaPortal.GUI.Library.Action;

namespace MPlayer
{
  /// <summary>
  /// This class handles all osd relevant task for the internal MPlayer osd
  /// </summary>
  internal class InternalOSDHandler : IOSDHandler
  {
    #region variables
    /// <summary>
    /// Time the _osd is displayed
    /// </summary>
    private readonly int _displayDuration;

    /// <summary>
    /// Is OSD visible
    /// </summary>
    private bool _osdVisible;

    /// <summary>
    /// OSD visible, because of state "Paused"
    /// </summary>
    private bool _osdVisibleForPause;

    /// <summary>
    /// OSD Timestamp
    /// </summary>
    private String _timeStamp;

    /// <summary>
    /// Position in the _osd timestamp
    /// </summary>
    private int _timeCodePosition;

    /// <summary>
    /// Message handler for MP messages
    /// </summary>
    private readonly SendMessageHandler _mpMessageHandler;

    /// <summary>
    /// Volumehandler for the volume changes in MP
    /// </summary>
    private readonly VolumeHandler _mpVolumeHandler;

    /// <summary>
    /// Reference to the main player component
    /// </summary>
    private readonly MPlayerExtPlayer _player;
    #endregion

    #region ctor
    /// <summary>
    /// Constructor which initialises the internal osd handler
    /// </summary>
    /// <param name="player">Instance of external player</param>
    /// <param name="playerUse">Indicates, if this instance will be used by the player class</param>
    public InternalOSDHandler(MPlayerExtPlayer player, bool playerUse)
    {
      _player = player;
      _displayDuration = 2000;
      _osdVisible = false;
      _osdVisibleForPause = false;
      _mpVolumeHandler = VolumeHandler.Instance;
      if (playerUse)
      {
        _mpMessageHandler = OnMessage;
        GUIWindowManager.Receivers += _mpMessageHandler;
      }
    }

    /// <summary>
    /// Disposes the object
    /// </summary>
    public void Dispose()
    {
      if (_mpMessageHandler != null)
      {
        GUIWindowManager.Receivers -= _mpMessageHandler;
      }
    }
    #endregion

    #region Private methods
    /// <summary>
    /// Handles the on message event. Needed for handling the volume change event
    /// </summary>
    /// <param name="message">Message to handle</param>
    private void OnMessage(GUIMessage message)
    {
      switch (message.Message)
      {
        case GUIMessage.MessageType.GUI_MSG_AUDIOVOLUME_CHANGED:
          if (!_osdVisible && _player.FullScreen)
          {
            if (_mpVolumeHandler.IsMuted)
            {
              SendOSDText(LocalizedMessages.Mute);
            }
            else
            {
              double currentVolume = _mpVolumeHandler.Volume;
              double maximumVolume = _mpVolumeHandler.Maximum;
              double percentage = currentVolume / maximumVolume * 100;
              SendOSDText(LocalizedMessages.Volume, percentage.ToString("00") + "%");
            }
          }
          break;
      }
    }

    /// <summary>
    /// Creates the new timestamp string with the additional key
    /// </summary>
    /// <param name="chKey">Additional key for the timestamp</param>
    private void ChangeTheTimeCode(char chKey)
    {
      if (_timeCodePosition <= 4)
      {
        //00:12
        _timeStamp += chKey;
        _timeCodePosition++;
        if (_timeCodePosition == 2)
        {
          _timeStamp += ":";
          _timeCodePosition++;
        }
      }
      if (_timeCodePosition > 4)
      {
        _timeStamp = "";
        _timeCodePosition = 0;
      }
    }

    /// <summary>
    /// Sends a localized OSD command with a specific duration to the mplayer proccess
    /// </summary>
    /// <param name="message">Message type</param>
    private void SendOSDText(LocalizedMessages message)
    {
      SendOSDText(LocalizeStrings.Get((int)message));
    }

    /// <summary>
    /// Sends a localized OSD command with a specific value to the mplayer proccess
    /// </summary>
    /// <param name="message">Message type</param>
    /// <param name="value">Value of the message</param>
    private void SendOSDText(LocalizedMessages message, String value)
    {
      SendOSDText(LocalizeStrings.Get((int)message) + ": " + value);
    }

    /// <summary>
    /// Sends a localized OSD command with a localized value to the mplayer proccess
    /// </summary>
    /// <param name="message">Message type</param>
    /// <param name="value">Value of the message</param>
    private void SendOSDText(LocalizedMessages message, LocalizedMessages value)
    {
      SendOSDText(LocalizeStrings.Get((int)message) + ": " + LocalizeStrings.Get((int)value));
    }

    /// <summary>
    /// Sends a OSD command to the mplayer process
    /// </summary>
    /// <param name="text">_osd command</param>
    private void SendOSDText(string text)
    {
      if (_player.FullScreen)
      {
        _player.SendPausingKeepCommand("osd_show_text \"" + text + "\" " + _displayDuration + " 0");
      }
    }
    #endregion

    #region IOSDHandler member
    /// <summary>
    /// Gets the OSD Visible property
    /// </summary>
    public bool OsdVisible
    {
      get { return _osdVisible; }
    }

    /// <summary>
    /// Handles MP internal action related for the internal osd handler
    /// </summary>
    /// <param name="action">Action to handle</param>
    public void OnAction(Action action)
    {
      switch (action.wID)
      {
        case Action.ActionType.ACTION_SHOW_OSD:
          if (_osdVisible)
          {
            DeactivateOSD(true);
          }
          else
          {
            ActivateOSD(true);
          }
          break;
        case Action.ActionType.ACTION_SHOW_GUI:
          if (_osdVisible)
          {
            DeactivateOSD(true);
          }
          break;
        case Action.ActionType.ACTION_MOVE_LEFT:
        case Action.ActionType.ACTION_STEP_BACK:
        case Action.ActionType.ACTION_MOVE_RIGHT:
        case Action.ActionType.ACTION_STEP_FORWARD:
          if (!_osdVisible)
          {
            String description = g_Player.GetStepDescription();
            if (!String.IsNullOrEmpty(description))
            {
              SendOSDText(LocalizedMessages.Seek, description);
            }
            else
            {
              SendOSDText("");
            }
          }
          break;
        case Action.ActionType.ACTION_KEY_PRESSED:
          if (!_osdVisible && action.m_key != null)
          {
            char chKey = (char)action.m_key.KeyChar;
            if (chKey >= '0' && chKey <= '9')
            {
              if (_player.CanSeek())
              {
                ChangeTheTimeCode(chKey);
                if (!String.IsNullOrEmpty(_timeStamp))
                {
                  SendOSDText(LocalizedMessages.JumpTo, _timeStamp);
                }
                else
                {
                  SendOSDText("");
                }
              }
            }
          }
          break;
      }
    }

    /// <summary>
    /// Activate the osd of MPlayer
    /// </summary>
    /// <param name="activate">If false, OSD will only be activated when OSDVisbileForPause is true; If true always</param>
    public void ActivateOSD(bool activate)
    {
      if (activate)
      {
        _osdVisible = true;
      }
      else if (_osdVisibleForPause)
      {
        activate = true;
      }
      if (_player.FullScreen && activate)
      {
        _player.SendPausingKeepCommand("osd 3");
      }
    }

    /// <summary>
    /// Deactivates the internal osd
    /// </summary>
    /// <param name="deactivate">If false, OSD will only be deactivate when OSDVisbileForPause is true; If true always</param>
    public void DeactivateOSD(bool deactivate)
    {
      if (deactivate)
      {
        _osdVisible = false;
      }
      deactivate = (!_osdVisible) && !_osdVisibleForPause;
      if (deactivate || !_player.FullScreen)
      {
        _player.SendPausingKeepCommand("osd 0");
        Thread.Sleep(200);
      }
    }

    /// <summary>
    /// Sets that osd osd should be visible, because of pausing
    /// </summary>
    /// <param name="osdVisibleForPause">true/false</param>
    public void SetOSDVisibleForPause(bool osdVisibleForPause)
    {
      _osdVisibleForPause = osdVisibleForPause;
    }

    /// <summary>
    /// Shows the new audio language
    /// </summary>
    /// <param name="newAudioLanguage">Name of the new audio langauge</param>
    public void ShowAudioChanged(String newAudioLanguage)
    {
      SendOSDText(LocalizedMessages.Audio, newAudioLanguage);
    }

    /// <summary>
    /// Shows the new subtitle language
    /// </summary>
    /// <param name="newSubtitleLanguage">Name of the new subtitle langauge</param>
    public void ShowSubtitleChanged(String newSubtitleLanguage)
    {
      SendOSDText(LocalizedMessages.Subtitles, newSubtitleLanguage);
    }

    /// <summary>
    /// Shows the new subtitle position
    /// </summary>
    /// <param name="newSubtitlePosition">Value of the new subtitle position</param>
    public void ShowSubtitlePositionChanged(String newSubtitlePosition)
    {
      SendOSDText(LocalizedMessages.SubtitlePosition, newSubtitlePosition);
    }

    /// <summary>
    /// Shows the new subtitle size
    /// </summary>
    /// <param name="newSubtitleSize">Value of the new subtitle size</param>
    public void ShowSubtitleSizeChanged(String newSubtitleSize)
    {
      SendOSDText(LocalizedMessages.SubtitleSize, newSubtitleSize);
    }

    /// <summary>
    /// Shows the new audio delay
    /// </summary>
    /// <param name="newAudioDelay">Value of the new audio delay</param>
    public void ShowAudioDelayChanged(String newAudioDelay)
    {
      SendOSDText(LocalizedMessages.AudioDelay, newAudioDelay);
    }

    /// <summary>
    /// Shows the new subtitle delay
    /// </summary>
    /// <param name="newSubtitleDelay">Value of the new subtitle delay</param>
    public void ShowSubtitleDelayChanged(String newSubtitleDelay)
    {
      SendOSDText(LocalizedMessages.SubtitleDelay, newSubtitleDelay);
    }

    /// <summary>
    /// Shows the new status of the subtitles
    /// </summary>
    /// <param name="enabled">New status of the subtitles</param>
    public void ShowSubtitleAcDeActivated(bool enabled)
    {
      SendOSDText(LocalizedMessages.Subtitles, enabled ? LocalizedMessages.Enabled : LocalizedMessages.Disabled);
    }

    /// <summary>
    /// Shows the new speed value
    /// </summary>
    /// <param name="newSpeed">Value of the new speed value</param>
    public void ShowSpeedChanged(String newSpeed)
    {
      SendOSDText(LocalizedMessages.Speed, newSpeed);
    }

    /// <summary>
    /// Shows the new display mode
    /// </summary>
    /// <param name="newDisplayMode">Name of the new display mode</param>
    public void ShowDisplayModeChanged(String newDisplayMode)
    {
      SendOSDText(LocalizedMessages.DisplayMode, newDisplayMode);
    }

    /// <summary>
    /// Updates the gui and osd
    /// </summary>
    public void UpdateGUI()
    {
    }
    #endregion

    #region IMessageHandler Member
    /// <summary>
    /// Handles a message that is retrieved from the MPlayer process
    /// </summary>
    /// <param name="message">Message to handle</param>
    public void HandleMessage(string message)
    {
    }
    #endregion

  }
}
