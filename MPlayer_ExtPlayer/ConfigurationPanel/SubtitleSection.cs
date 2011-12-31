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
  /// This class represents the subtitle section of the configuration
  /// </summary>
  public partial class SubtitleSection : UserControl
  {

    #region ctor
    /// <summary>
    /// Constructor, which initilizes the control
    /// </summary>
    public SubtitleSection()
    {
      InitializeComponent();
      ConfigurationManager manager = ConfigurationManager.GetInstance();
      subtitleFont.Items.Clear();
      subtitleFont.Items.AddRange(manager.PossibleFonts);
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
        string subtitleFontName = xmlreader.GetValueAsString("mplayer", "subtitleFontName", "Arial");
        subtitleFont.SelectedItem = subtitleFontName;
        subtitles.Checked = xmlreader.GetValueAsBool("mplayer", "enableSubtitles", false);
        subtitleDelayStep.Value = xmlreader.GetValueAsInt("mplayer", "subtitleDelayStep", 100);
        subtitlePosition.Value = xmlreader.GetValueAsInt("mplayer", "subtitlePosition", 100);
        subtitleSize.Value = xmlreader.GetValueAsInt("mplayer", "subtitleSize", 5);
      }
    }

    /// <summary>
    /// Stores the configuration of this section
    /// </summary>
    public void SaveConfiguration()
    {
      using (MediaPortal.Profile.Settings xmlWriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        xmlWriter.SetValue("mplayer", "subtitleFontName", subtitleFont.SelectedItem.ToString());
        xmlWriter.SetValueAsBool("mplayer", "enableSubtitles", subtitles.Checked);
        xmlWriter.SetValue("mplayer", "subtitleDelayStep", subtitleDelayStep.Value);
        xmlWriter.SetValue("mplayer", "subtitleSize", subtitleSize.Value);
        xmlWriter.SetValue("mplayer", "subtitlePosition", subtitlePosition.Value);
      }
    }
    #endregion
  }
}
