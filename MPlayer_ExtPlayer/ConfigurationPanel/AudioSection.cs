#region Copyright (C) 2006-2008 MisterD

/* 
 *	Copyright (C) 2006-2008 MisterD
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
using System.Windows.Forms;
using DShowNET.Helper;
using MediaPortal.Configuration;

namespace MPlayer.ConfigurationPanel
{
  /// <summary>
  /// This class represents the audio section of the configuration
  /// </summary>
  public partial class AudioSection : UserControl
  {

    #region ctor
    /// <summary>
    /// Constructor, which initilizes the control
    /// </summary>
    public AudioSection()
    {
      InitializeComponent();
      soundOutputDevice.Items.Clear();
      soundOutputDevice.Items.Add("Default DirectSound Device");
      //
      // Fetch available Audio Renderers
      //
      foreach (Filter audioRenderer in Filters.AudioRenderers)
      {
        if (audioRenderer.Name.StartsWith("DirectSound: "))
        {
          soundOutputDevice.Items.Add(audioRenderer.Name.Remove(0, 13));
        }
      }
    }
    #endregion

    #region configuration methods
    /// <summary>
    /// Loads the configuration of this section
    /// </summary>
    public void LoadConfiguration()
    {
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        soundOutputDriver.SelectedIndex = xmlreader.GetValueAsInt("mplayer", "soundOutputDriver", (int)SoundOutputDriver.DirectSound);
        soundOutputDevice.SelectedIndex = xmlreader.GetValueAsInt("mplayer", "soundOutputDevice", 0);
        audioChannels.SelectedIndex = xmlreader.GetValueAsInt("mplayer", "audioChannels", (int)AudioChannels.Default);
        audioDelayStep.Value = xmlreader.GetValueAsInt("mplayer", "audioDelayStep", 100);
        passthroughAC3_DTS.Checked = xmlreader.GetValueAsBool("mplayer", "passthroughAC3DTS", false);
        audioNormalize.Checked = xmlreader.GetValueAsBool("mplayer", "audioNormalize", false);
      }
    }

    /// <summary>
    /// Stores the configuration of this section
    /// </summary>
    public void SaveConfiguration()
    {
      using (MediaPortal.Profile.Settings xmlWriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        xmlWriter.SetValue("mplayer", "soundOutputDriver", soundOutputDriver.SelectedIndex);
        xmlWriter.SetValue("mplayer", "soundOutputDevice", soundOutputDevice.SelectedIndex);
        xmlWriter.SetValue("mplayer", "audioChannels", audioChannels.SelectedIndex);
        xmlWriter.SetValue("mplayer", "audioDelayStep", audioDelayStep.Value);
        xmlWriter.SetValueAsBool("mplayer", "passthroughAC3DTS", passthroughAC3_DTS.Checked);
        xmlWriter.SetValueAsBool("mplayer", "audioNormalize", audioNormalize.Checked);
      }
    }
    #endregion

    #region event handling
    /// <summary>
    /// Handles the Selected index changed event of the sound output driver combo box
    /// </summary>
    /// <param _name="sender">Sender object</param>
    /// <param _name="e"></param>
    private void soundOutputDriver_SelectedIndexChanged(object sender, EventArgs e)
    {
      soundOutputDevice.Enabled = soundOutputDriver.SelectedIndex == (int)SoundOutputDriver.DirectSound;
    }

    #endregion
  }
}
