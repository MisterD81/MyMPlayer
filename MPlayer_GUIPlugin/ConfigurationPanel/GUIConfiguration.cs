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
using System.Windows.Forms;
using System.Xml;
using MediaPortal.GUI.Library;
using MediaPortal.Configuration;

namespace MPlayer.ConfigurationPanel
{
  /// <summary>
  /// Configuration panel for the gui configuration
  /// </summary>
  public partial class GUIConfiguration : UserControl
  {
    #region variables
    /// <summary>
    /// Last selected Share in the form
    /// </summary>
    private MPlayerShare _lastShare;
    #endregion

    #region
    /// <summary>
    /// Constructor, which initializes the configuration section
    /// </summary>
    public GUIConfiguration()
    {
      InitializeComponent();
    }
    #endregion

    #region event handling
    /// <summary>
    /// Handles the Add-Button click event
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void ShareAddClick(object sender, EventArgs e)
    {
      MPlayerShare temp = new MPlayerShare {Name = "NewLocation"};
      shareList.Items.Add(temp);
      shareList.SelectedItem = temp;

    }

    /// <summary>
    /// Handles the Delete-Button click event
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void ShareDeleteClick(object sender, EventArgs e)
    {
      if (shareList.SelectedIndex > -1)
      {
        shareList.Items.RemoveAt(shareList.SelectedIndex);
        if (shareList.Items.Count > 0)
        {
          shareList.SelectedIndex = 0;
        }
        else
        {
          shareList.SelectedIndex = -1;
        }
      }
    }
    /// <summary>
    /// Handles the Selected index change on the share list
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void ShareListSelectedIndexChanged(object sender, EventArgs e)
    {
      if (_lastShare != null)
      {
        _lastShare.Path = shareLocation.Text;
      }

      if (shareList.SelectedIndex > -1)
      {
        _lastShare = shareList.SelectedItem as MPlayerShare;
        if (_lastShare != null)
        {
          shareName.Text = _lastShare.Name;
          shareLocation.Text = _lastShare.Path;
        }
        shareName.Enabled = true;
        shareLocation.Enabled = true;
      }
      else
      {
        shareName.Text = String.Empty;
        shareLocation.Text = String.Empty;
        shareName.Enabled = false;
        shareLocation.Enabled = false;
        _lastShare = null;
      }

    }

    /// <summary>
    /// Handles the Leave event on the extension Textfield
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void ShareNameLeave(object sender, EventArgs e)
    {
      if (_lastShare != null)
      {
        int selectedIndex = shareList.SelectedIndex;
        if (selectedIndex != -1)
        {
          _lastShare.Name = shareName.Text;
          shareList.Items[selectedIndex] = _lastShare;
        }
      }

    }

    /// <summary>
    /// Handles the Leave event on the extension Textfield
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void ShareLocationLeave(object sender, EventArgs e)
    {
      if (_lastShare != null)
      {
        _lastShare.Path = shareLocation.Text;
        shareList.Items[shareList.SelectedIndex] = _lastShare;
      }


    }

    /// <summary>
    /// Handles the Browse-Button click event
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void BrowseButtonClick(object sender, EventArgs e)
    {
      folderBrowserDialog1.SelectedPath = shareLocation.Text;
      folderBrowserDialog1.ShowDialog();
      shareLocation.Text = folderBrowserDialog1.SelectedPath;
    }
    #endregion

    #region Configuration Methods
    /// <summary>
    /// Loads the configuration with the shares
    /// </summary>
    public void LoadConfiguration()
    {
      try
      {
        XmlDocument doc = new XmlDocument();
        string path = Config.GetFile(Config.Dir.Config, "MPlayer_GUIPlugin.xml");
        doc.Load(path);
        if (doc.DocumentElement != null)
        {
          XmlNodeList listShare = doc.DocumentElement.SelectNodes("/mplayergui/Share");
          if (listShare != null)
            foreach (XmlNode nodeShare in listShare)
            {
              MPlayerShare share = new MPlayerShare
                                      {
                                        Name = nodeShare.Attributes["name"].Value,
                                        Path = nodeShare.Attributes["path"].Value
                                      };
              shareList.Items.Add(share);
            }
        }
        using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
        {
          pluginName.Text = xmlreader.GetValueAsString("mplayer", "displayNameOfGUI", "My MPlayer GUI");
          myVideoShare.Checked = xmlreader.GetValueAsBool("mplayer", "useMyVideoShares", true);
          myMusicShare.Checked = xmlreader.GetValueAsBool("mplayer", "useMyMusicShares", true);
          playlistFolder.Checked = xmlreader.GetValueAsBool("mplayer", "treatPlaylistAsFolders", false);
          dvdNavCheckbox.Checked = xmlreader.GetValueAsBool("mplayer", "useDVDNAV", false);
        }
      }
      catch (Exception e)
      {
        Log.Info("MPlayer GUI Error: Configuration could not be loaded: " + e.Message);
      }

    }

    /// <summary>
    /// Stores the configuration with the shares
    /// </summary>
    public void SaveConfiguration()
    {
      shareList.SelectedIndex = -1;
      XmlTextWriter writer = new XmlTextWriter(Config.GetFile(Config.Dir.Config, "MPlayer_GUIPlugin.xml"),
                                               System.Text.Encoding.UTF8)
                               {Formatting = Formatting.Indented, Indentation = 1, IndentChar = (char)9};
      writer.WriteStartDocument(true);
      writer.WriteStartElement("mplayergui"); //<mplayer>
      writer.WriteAttributeString("version", "1");
      for (int i = 0; i < shareList.Items.Count; i++)
      {
        MPlayerShare temp = shareList.Items[i] as MPlayerShare;
        writer.WriteStartElement("Share"); //<Share>
        if (temp != null)
        {
          writer.WriteAttributeString("name", temp.Name);
          writer.WriteAttributeString("path", temp.Path);
        }
        writer.WriteEndElement(); //</Share>
      }
      writer.WriteEndElement(); //</mplayer>
      writer.WriteEndDocument();
      writer.Close();
      using (MediaPortal.Profile.Settings xmlWriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        xmlWriter.SetValue("mplayer", "displayNameOfGUI",
                           String.IsNullOrEmpty(pluginName.Text) ? "My MPlayer" : pluginName.Text);
        xmlWriter.SetValueAsBool("mplayer", "useMyMusicShares", myMusicShare.Checked);
        xmlWriter.SetValueAsBool("mplayer", "useMyVideoShares", myVideoShare.Checked);
        xmlWriter.SetValueAsBool("mplayer", "treatPlaylistAsFolders", playlistFolder.Checked);
        xmlWriter.SetValueAsBool("mplayer", "useDVDNAV", dvdNavCheckbox.Checked);
      }
    }
    #endregion


  }
}
