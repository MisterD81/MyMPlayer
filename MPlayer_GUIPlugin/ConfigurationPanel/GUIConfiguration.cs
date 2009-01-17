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
    private MPlayer_Share lastShare;
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
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event Arguments</param>
    private void shareAdd_Click(object sender, EventArgs e)
    {
      MPlayer_Share temp = new MPlayer_Share();
      temp.Name = "NewLocation";
      shareList.Items.Add(temp);
      shareList.SelectedItem = temp;

    }

    /// <summary>
    /// Handles the Delete-Button click event
    /// </summary>
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event Arguments</param>
    private void shareDelete_Click(object sender, EventArgs e)
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
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event Arguments</param>
    private void shareList_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lastShare != null)
      {
        lastShare.Path = shareLocation.Text;
      }

      if (shareList.SelectedIndex > -1)
      {
        lastShare = shareList.SelectedItem as MPlayer_Share;
        if (lastShare != null)
        {
          shareName.Text = lastShare.Name;
          shareLocation.Text = lastShare.Path;
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
        lastShare = null;
      }

    }

    /// <summary>
    /// Handles the Leave event on the extension Textfield
    /// </summary>
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event Arguments</param>
    private void shareName_Leave(object sender, EventArgs e)
    {
      if (lastShare != null)
      {
        int selectedIndex = shareList.SelectedIndex;
        if (selectedIndex != -1)
        {
          lastShare.Name = shareName.Text;
          shareList.Items[selectedIndex] = lastShare;
        }
      }

    }

    /// <summary>
    /// Handles the Leave event on the extension Textfield
    /// </summary>
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event Arguments</param>
    private void shareLocation_Leave(object sender, EventArgs e)
    {
      if (lastShare != null)
      {
        lastShare.Path = shareLocation.Text;
        shareList.Items[shareList.SelectedIndex] = lastShare;
      }


    }

    /// <summary>
    /// Handles the Browse-Button click event
    /// </summary>
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event Arguments</param>
    private void browseButton_Click(object sender, EventArgs e)
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
        MPlayer_Share share;
        XmlDocument doc = new XmlDocument();
        string path = Config.GetFile(Config.Dir.Config, "MPlayer_GUIPlugin.xml");
        doc.Load(path);
        if (doc.DocumentElement != null)
        {
          XmlNodeList listShare = doc.DocumentElement.SelectNodes("/mplayergui/Share");
          if (listShare != null)
            foreach (XmlNode nodeShare in listShare)
            {
              share = new MPlayer_Share();
              share.Name = nodeShare.Attributes["name"].Value;
              share.Path = nodeShare.Attributes["path"].Value;
              shareList.Items.Add(share);
            }
        }
        using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
        {
          pluginName.Text = xmlreader.GetValueAsString("mplayer", "displayNameOfGUI", "My MPlayer GUI");
          myVideoShare.Checked = xmlreader.GetValueAsBool("mplayer", "useMyVideoShares", true);
          myMusicShare.Checked = xmlreader.GetValueAsBool("mplayer", "useMyMusicShares", true);
          playlistFolder.Checked = xmlreader.GetValueAsBool("mplayer", "treatPlaylistAsFolders", false);
        }
      } catch (Exception e)
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
      XmlTextWriter writer = new XmlTextWriter(Config.GetFile(Config.Dir.Config, "MPlayer_GUIPlugin.xml"), System.Text.Encoding.UTF8);
      writer.Formatting = Formatting.Indented;
      writer.Indentation = 1;
      writer.IndentChar = (char)9;
      writer.WriteStartDocument(true);
      writer.WriteStartElement("mplayergui"); //<mplayer>
      writer.WriteAttributeString("version", "1");
      MPlayer_Share temp;
      for (int i = 0; i < shareList.Items.Count; i++)
      {
        temp = shareList.Items[i] as MPlayer_Share;
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
        if (String.IsNullOrEmpty(pluginName.Text))
        {
          xmlWriter.SetValue("mplayer", "displayNameOfGUI", "My MPlayer");
        }
        else
        {
          xmlWriter.SetValue("mplayer", "displayNameOfGUI", pluginName.Text);
        }
        xmlWriter.SetValueAsBool("mplayer", "useMyMusicShares", myMusicShare.Checked);
        xmlWriter.SetValueAsBool("mplayer", "useMyVideoShares", myVideoShare.Checked);
        xmlWriter.SetValueAsBool("mplayer", "treatPlaylistAsFolders", playlistFolder.Checked);
      }
    }
    #endregion


  }
}
