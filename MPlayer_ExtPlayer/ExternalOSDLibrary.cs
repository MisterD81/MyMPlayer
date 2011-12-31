#region Copyright (C) 2006-2012 MisterD

/* 
 *	Copyright (C) 2006-2012 MisterD
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
using System.Globalization;
using System.Threading;
using MediaPortal.GUI.Library;
using ExternalOSDLibrary;
using Action = MediaPortal.GUI.Library.Action;

namespace MPlayer
{
  /// <summary>
  /// This class handles all osd relevant task for the external osd library
  /// </summary>
  internal class ExternalOSDLibrary : IOSDHandler
  {
    #region variables
    /// <summary>
    /// Instance of the osd controller of the ExternalOSDLibrary OSD
    /// </summary>
    private OSDController _osd;

    /// <summary>
    /// Reference to the internal osd, because it has to be deactivated
    /// </summary>
    private readonly InternalOSDHandler _internalOSDHandler;

    /// <summary>
    /// Indicates if the cache status is displayed
    /// </summary>
    private bool _showingCacheStatus;

    #endregion

    #region ctor
    /// <summary>
    /// Constructor which initialises the external osd library handler
    /// </summary>
    /// <param name="player">Instance of external player</param>
    public ExternalOSDLibrary(MPlayerExtPlayer player)
    {
      _showingCacheStatus = false;
      _internalOSDHandler = new InternalOSDHandler(player, false);
      using (new WaitCursor())
      {
        Thread thread = new Thread(OsdGetInstance);
        thread.Start();
        while (thread.IsAlive)
        {
          GUIWindowManager.Process();
          Thread.Sleep(100);
        }
      }
      _osd.ShowInit(LocalizeStrings.Get((int)LocalizedMessages.Initializing));
    }

    /// <summary>
    /// Creates the instance of the external OSD controller. Seperated for creation in a different thread.
    /// </summary>
    private void OsdGetInstance()
    {
      _osd = OSDController.Instance;
    }

    /// <summary>
    /// Disposes the object
    /// </summary>
    public void Dispose()
    {
      _osd.Dispose();
    }
    #endregion

    #region IOSDHandler Member
    /// <summary>
    /// Deactivates the internal osd
    /// </summary>
    /// <param name="deactivate">If false, OSD will only be deactivate when OSDVisbileForPause is true; If true always</param>
    public void DeactivateOSD(bool deactivate)
    {
      _internalOSDHandler.DeactivateOSD(true);
    }

    /// <summary>
    /// Gets the OSD Visible property
    /// </summary>
    public bool OsdVisible
    {
      get { return _osd.IsOSDVisible(); }
    }

    /// <summary>
    /// Shows the new subtitle language
    /// </summary>
    /// <param name="newSubtitleLanguage">Name of the new subtitle langauge</param>
    public void ShowSubtitleChanged(String newSubtitleLanguage)
    {
      _osd.ShowAlternativeOSD(LocalizeStrings.Get((int)LocalizedMessages.Subtitles) + ": " + newSubtitleLanguage, false);
    }

    /// <summary>
    /// Shows the new subtitle position
    /// </summary>
    /// <param name="newSubtitlePosition">Value of the new subtitle position</param>
    public void ShowSubtitlePositionChanged(String newSubtitlePosition)
    {
      _osd.ShowAlternativeOSD(LocalizeStrings.Get((int)LocalizedMessages.SubtitlePosition) + ": " + newSubtitlePosition, false);
    }

    /// <summary>
    /// Shows the new subtitle size
    /// </summary>
    /// <param name="newSubtitleSize">Value of the new subtitle size</param>
    public void ShowSubtitleSizeChanged(String newSubtitleSize)
    {
      _osd.ShowAlternativeOSD(LocalizeStrings.Get((int)LocalizedMessages.SubtitleSize) + ": " + newSubtitleSize, false);
    }

    /// <summary>
    /// Shows the new audio delay
    /// </summary>
    /// <param name="newAudioDelay">Value of the new audio delay</param>
    public void ShowAudioDelayChanged(String newAudioDelay)
    {
      _osd.ShowAlternativeOSD(LocalizeStrings.Get((int)LocalizedMessages.AudioDelay) + ": " + newAudioDelay, false);
    }

    /// <summary>
    /// Shows the new subtitle delay
    /// </summary>
    /// <param name="newSubtitleDelay">Value of the new subtitle delay</param>
    public void ShowSubtitleDelayChanged(String newSubtitleDelay)
    {
      _osd.ShowAlternativeOSD(LocalizeStrings.Get((int)LocalizedMessages.SubtitleDelay) + ": " + newSubtitleDelay, false);
    }

    /// <summary>
    /// Shows the new status of the subtitles
    /// </summary>
    /// <param name="enabled">New status of the subtitles</param>
    public void ShowSubtitleAcDeActivated(bool enabled)
    {
      _osd.ShowAlternativeOSD(LocalizeStrings.Get((int)LocalizedMessages.Subtitles), !enabled);
    }

    /// <summary>
    /// Updates the gui and osd
    /// </summary>
    public void UpdateGUI()
    {
      _osd.UpdateGUI();
    }

    #region Unused Methods
    /// <summary>
    /// Handles MP internal action related for the internal osd handler
    /// </summary>
    /// <param name="action">Action to handle</param>
    public void OnAction(Action action)
    {
    }

    /// <summary>
    /// Activate the osd of MPlayer
    /// </summary>
    /// <param name="activate">If false, OSD will only be activated when OSDVisbileForPause is true; If true always</param>
    public void ActivateOSD(bool activate)
    {
    }

    /// <summary>
    /// Sets that osd osd should be visible, because of pausing
    /// </summary>
    /// <param name="osdVisibleForPause">true/false</param>
    public void SetOSDVisibleForPause(bool osdVisibleForPause)
    {
    }

    /// <summary>
    /// Shows the new audio language
    /// </summary>
    /// <param name="newAudioLanguage">Name of the new audio langauge</param>
    public void ShowAudioChanged(String newAudioLanguage)
    {
    }

    /// <summary>
    /// Shows the new speed value
    /// </summary>
    /// <param name="newSpeed">Value of the new speed value</param>
    public void ShowSpeedChanged(String newSpeed)
    {
    }

    /// <summary>
    /// Shows the new display mode
    /// </summary>
    /// <param name="newDisplayMode">Name of the new display mode</param>
    public void ShowDisplayModeChanged(String newDisplayMode)
    {
    }
    #endregion
    #endregion

    #region IMessageHandler Member
    /// <summary>
    /// Handles a message that is retrieved from the MPlayer process
    /// </summary>
    /// <param name="message">Message to handle</param>
    public void HandleMessage(string message)
    {
      // Cache fill:  0.00% (0 bytes)  
      if (message.StartsWith("Cache fill: "))
      {
        string temp = message.Substring(12);
        int index = temp.IndexOf("%");
        temp = temp.Substring(0, index);
        float cacheFill;
        float.TryParse(temp.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out cacheFill);
        _osd.ShowCacheStatus(cacheFill);
        _showingCacheStatus = true;
      }
      else if (_showingCacheStatus)
      {
        _showingCacheStatus = false;
        _osd.HideCacheStatus();

      }
      else if (message.StartsWith("VO: [directx] ") ||
        message.StartsWith("VO: [direct3d] ") ||
        message.StartsWith("VO: [gl2] ") ||
        message.StartsWith("VO: [gl] "))
      {
        _osd.HideInit();
      }
    }
    #endregion

  }
}
