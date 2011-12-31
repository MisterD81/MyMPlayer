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
using System.Windows.Forms;
using MediaPortal.Configuration;

namespace MPlayer.ConfigurationPanel
{
  /// <summary>
  /// This class represents the stream section of the configuration
  /// </summary>
  public partial class StreamSection : UserControl
  {

    #region ctor
    /// <summary>
    /// Constructor, which initilizes the control
    /// </summary>
    public StreamSection()
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
        dvdArguments.Text = xmlreader.GetValueAsString("mplayer", "dvdArguments", String.Empty);
        vcdArguments.Text = xmlreader.GetValueAsString("mplayer", "vcdArguments", String.Empty);
        svcdArguments.Text = xmlreader.GetValueAsString("mplayer", "svcdArguments", String.Empty);
        cueArguments.Text = xmlreader.GetValueAsString("mplayer", "cueArguments", String.Empty);
        ftpArguments.Text = xmlreader.GetValueAsString("mplayer", "ftpArguments", String.Empty);
        httpArguments.Text = xmlreader.GetValueAsString("mplayer", "httpArguments", String.Empty);
        mmsArguments.Text = xmlreader.GetValueAsString("mplayer", "mmsArguments", String.Empty);
        mpstArguments.Text = xmlreader.GetValueAsString("mplayer", "mpstArguments", String.Empty);
        rtspArguments.Text = xmlreader.GetValueAsString("mplayer", "rtspArguments", String.Empty);
        sdpArguments.Text = xmlreader.GetValueAsString("mplayer", "sdpArguments", String.Empty);
        udpArguments.Text = xmlreader.GetValueAsString("mplayer", "udpArguments", String.Empty);
        unsvArguments.Text = xmlreader.GetValueAsString("mplayer", "unsvArguments", String.Empty);
      }
    }

    /// <summary>
    /// Stores the configuration of this section
    /// </summary>
    public void SaveConfiguration()
    {
      using (MediaPortal.Profile.Settings xmlWriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        xmlWriter.SetValue("mplayer", "dvdArguments", dvdArguments.Text);
        xmlWriter.SetValue("mplayer", "vcdArguments", vcdArguments.Text);
        xmlWriter.SetValue("mplayer", "svcdArguments", svcdArguments.Text);
        xmlWriter.SetValue("mplayer", "cueArguments", cueArguments.Text);
        xmlWriter.SetValue("mplayer", "ftpArguments", ftpArguments.Text);
        xmlWriter.SetValue("mplayer", "httpArguments", httpArguments.Text);
        xmlWriter.SetValue("mplayer", "mmsArguments", mmsArguments.Text);
        xmlWriter.SetValue("mplayer", "mpstArguments", mpstArguments.Text);
        xmlWriter.SetValue("mplayer", "rtspArguments", rtspArguments.Text);
        xmlWriter.SetValue("mplayer", "sdpArguments", sdpArguments.Text);
        xmlWriter.SetValue("mplayer", "udpArguments", udpArguments.Text);
        xmlWriter.SetValue("mplayer", "unsvArguments", unsvArguments.Text);
      }
    }
    #endregion
  }
}
