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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MediaPortal.Configuration;
using OsDetection;

namespace MPlayer.ConfigurationPanel
{
  /// <summary>
  /// This class represents the general section of the configuration
  /// </summary>
  public partial class GeneralSection : UserControl
  {

    #region ctor
    /// <summary>
    /// Constructor, which initilizes the control
    /// </summary>
    public GeneralSection()
    {
      InitializeComponent();
    }
    #endregion

    #region configuration methods
    /// <summary>
    /// Loads the configuration for this section
    /// </summary>
    public void LoadConfiguration()
    {
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        osdSelect.SelectedIndex = xmlreader.GetValueAsInt("mplayer", "osd", 0);
        optionalArguments.Text = xmlreader.GetValueAsString("mplayer", "generalArguments", String.Empty);
        rebuildIndex.Checked = xmlreader.GetValueAsBool("mplayer", "rebuildIndex", false);
        priorityBoost.Checked = xmlreader.GetValueAsBool("mplayer", "priorityBoost", true);
        int tempCacheSize = xmlreader.GetValueAsInt("mplayer", "cacheSize", 4096);
        if (tempCacheSize > 0)
        {
          cacheSize.Text = tempCacheSize.ToString();
        }
        else
        {
          cacheSize.Text = String.Empty;
        }
        mplayerPath.Text = xmlreader.GetValueAsString("mplayer", "mplayerPath", "C:\\Program Files\\MPlayer\\MPlayer.exe");
        bool blankScreenStandardValue = true;
        OSVersionInfo os = new OperatingSystemVersion();
        if (os.OSVersion == OSVersion.Vista || os.OSVersion == OSVersion.Win2008)
        {
          blankScreenStandardValue = false;
        }
        externalOSDLibraryBlank.Checked = xmlreader.GetValueAsBool("externalOSDLibrary", "blankScreen", blankScreenStandardValue);
      }
    }

    /// <summary>
    /// Stores the configuration for this section
    /// </summary>
    public void SaveConfiguration()
    {
      using (MediaPortal.Profile.Settings xmlWriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        xmlWriter.SetValue("mplayer", "generalArguments", optionalArguments.Text);
        xmlWriter.SetValue("mplayer", "osd", osdSelect.SelectedIndex);
        xmlWriter.SetValueAsBool("mplayer", "rebuildIndex", rebuildIndex.Checked);
        xmlWriter.SetValueAsBool("mplayer", "priorityBoost", priorityBoost.Checked);
        if (cacheSize.Text.Equals(String.Empty))
        {
          xmlWriter.SetValue("mplayer", "cacheSize", 0);
        }
        else
        {
          xmlWriter.SetValue("mplayer", "cacheSize", cacheSize.Text);
        }
        xmlWriter.SetValue("mplayer", "mplayerPath", mplayerPath.Text);
        xmlWriter.SetValueAsBool("externalOSDLibrary", "blankScreen", externalOSDLibraryBlank.Checked);
      }
    }
    #endregion

    #region event handling
    /// <summary>
    /// Handles the cache size textbox key press event
    /// </summary>
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event Arguments</param>
    private void cacheSize_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (!Char.IsNumber(e.KeyChar) && !Char.IsControl(e.KeyChar))
      {
        e.Handled = true;
      }
    }

    /// <summary>
    /// Handles the Browse-Button click event
    /// </summary>
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event Arguments</param>
    private void folderSearch_Click(object sender, EventArgs e)
    {
      openFileDialog1.Filter = "MPlayer commandline version (mplayer.exe)|mplayer.exe|MPlayer GUI version (gmplayer.exe)|gmplayer.exe";
      openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(mplayerPath.Text);
      openFileDialog1.FileName = System.IO.Path.GetFileName(mplayerPath.Text);
      if (openFileDialog1.FileName.Equals("mplayer.exe"))
      {
        openFileDialog1.FilterIndex = 1;
      }
      else
      {
        openFileDialog1.FilterIndex = 2;
      }
      openFileDialog1.ShowDialog();
      mplayerPath.Text = openFileDialog1.FileName;
    }
    #endregion
  }
}
