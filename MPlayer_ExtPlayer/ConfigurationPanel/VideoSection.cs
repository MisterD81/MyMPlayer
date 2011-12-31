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

using System.Windows.Forms;
using MediaPortal.Configuration;

namespace MPlayer.ConfigurationPanel
{
  /// <summary>
  /// This class represents the video section of the configuration
  /// </summary>
  public partial class VideoSection : UserControl
  {

    #region ctor
    /// <summary>
    /// Constructor, which initilizes the control
    /// </summary>
    public VideoSection()
    {
      InitializeComponent();
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
        VideoOutputDriver videoOutputDriverStandardValue = VideoOutputDriver.DirectX;
        if (OSInfo.OSInfo.OSList.WindowsVista == OSInfo.OSInfo.GetOSName() || OSInfo.OSInfo.OSList.Windows2008 == OSInfo.OSInfo.GetOSName() || OSInfo.OSInfo.OSList.Windows7 == OSInfo.OSInfo.GetOSName())
        {
          videoOutputDriverStandardValue = VideoOutputDriver.OpenGL2;
        }
        videoOutputDriver.SelectedIndex = xmlreader.GetValueAsInt("mplayer", "videoOutputDriver", (int)videoOutputDriverStandardValue);
        postProcessing.SelectedIndex = xmlreader.GetValueAsInt("mplayer", "postProcessing", (int)PostProcessing.Maximum);
        aspectRatio.SelectedIndex = xmlreader.GetValueAsInt("mplayer", "aspectRatio", (int)AspectRatio.Automatic);
        deinterlace.SelectedIndex = xmlreader.GetValueAsInt("mplayer", "deinterlace", (int)Deinterlace.Adaptive);
        noiseDenoise.SelectedIndex = xmlreader.GetValueAsInt("mplayer", "noise", (int)NoiseDenoise.Nothing);
        framedrop.Checked = xmlreader.GetValueAsBool("mplayer", "framedrop", false);
        directRendering.Checked = xmlreader.GetValueAsBool("mplayer", "directRendering", true);
        doubleBuffering.Checked = xmlreader.GetValueAsBool("mplayer", "doubleBuffering", true);
      }
    }

    /// <summary>
    /// Stores the configuration of this section
    /// </summary>
    public void SaveConfiguration()
    {
      using (MediaPortal.Profile.Settings xmlWriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        xmlWriter.SetValue("mplayer", "videoOutputDriver", videoOutputDriver.SelectedIndex);
        xmlWriter.SetValue("mplayer", "postProcessing", postProcessing.SelectedIndex);
        xmlWriter.SetValue("mplayer", "aspectRatio", aspectRatio.SelectedIndex);
        xmlWriter.SetValue("mplayer", "deinterlace", deinterlace.SelectedIndex);
        xmlWriter.SetValue("mplayer", "noise", noiseDenoise.SelectedIndex);
        xmlWriter.SetValueAsBool("mplayer", "framedrop", framedrop.Checked);
        xmlWriter.SetValueAsBool("mplayer", "directRendering", directRendering.Checked);
        xmlWriter.SetValueAsBool("mplayer", "doubleBuffering", doubleBuffering.Checked);
      }
    }
    #endregion

  }
}
