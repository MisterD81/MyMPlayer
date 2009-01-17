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
using MediaPortal.GUI.Library;

namespace MPlayer {
  internal interface IOSDHandler : IDisposable, IMessageHandler{
    #region Properties
    /// <summary>
    /// Gets the OSD Visible property
    /// </summary>
    bool OsdVisible { get; }
    #endregion

    #region Public methods
    /// <summary>
    /// Handles MP internal action related for the internal osd handler
    /// </summary>
    /// <param _name="action">Action to handle</param>
    void OnAction(Action action);

    /// <summary>
    /// Activate the osd of MPlayer
    /// </summary>
    /// <param _name="activate">If false, OSD will only be activated when OSDVisbileForPause is true; If true always</param>
    void ActivateOSD(bool activate);

    /// <summary>
    /// Deactivates the internal osd
    /// </summary>
    /// <param _name="deactivate">If false, OSD will only be deactivate when OSDVisbileForPause is true; If true always</param>
    void DeactivateOSD(bool deactivate);

    /// <summary>
    /// Sets that osd osd should be visible, because of pausing
    /// </summary>
    /// <param _name="osdVisibleForPause">true/false</param>
    void SetOSDVisibleForPause(bool osdVisibleForPause);

    /// <summary>
    /// Shows the new audio language
    /// </summary>
    /// <param _name="newAudioLanguage">Name of the new audio langauge</param>
    void ShowAudioChanged(String newAudioLanguage);

    /// <summary>
    /// Shows the new subtitle language
    /// </summary>
    /// <param _name="newSubtitleLanguage">Name of the new subtitle langauge</param>
    void ShowSubtitleChanged(String newSubtitleLanguage);

    /// <summary>
    /// Shows the new subtitle position
    /// </summary>
    /// <param _name="newSubtitlePosition">Value of the new subtitle position</param>
    void ShowSubtitlePositionChanged(String newSubtitlePosition);

    /// <summary>
    /// Shows the new subtitle size
    /// </summary>
    /// <param _name="newSubtitleSize">Value of the new subtitle size</param>
    void ShowSubtitleSizeChanged(String newSubtitleSize);

    /// <summary>
    /// Shows the new audio delay
    /// </summary>
    /// <param _name="newAudioDelay">Value of the new audio delay</param>
    void ShowAudioDelayChanged(String newAudioDelay);

    /// <summary>
    /// Shows the new subtitle delay
    /// </summary>
    /// <param _name="newSubtitleDelay">Value of the new subtitle delay</param>
    void ShowSubtitleDelayChanged(String newSubtitleDelay);

    /// <summary>
    /// Shows the new status of the subtitles
    /// </summary>
    /// <param _name="enabled">New status of the subtitles</param>
    void ShowSubtitleAcDeActivated(bool enabled);

    /// <summary>
    /// Shows the new speed value
    /// </summary>
    /// <param _name="newSpeed">Value of the new speed value</param>
    void ShowSpeedChanged(String newSpeed);

    /// <summary>
    /// Shows the new display mode
    /// </summary>
    /// <param _name="newDisplayMode">Name of the new display mode</param>
    void ShowDisplayModeChanged(String newDisplayMode);

    /// <summary>
    /// Updates the gui and osd
    /// </summary>
    void UpdateGUI();

    #endregion
  }
}