using System;
using System.Windows.Forms;
using System.Xml;
using MediaPortal.Configuration;

namespace MPlayer.ConfigurationPanel
{
  /// <summary>
  /// This class represents the general section of the configuration
  /// </summary>
  public partial class ExtensionSection : UserControl
  {

    #region variables
    /// <summary>
    /// Last video extension setting
    /// </summary>
    private ExtensionSettings _lastVideoSettings;

    /// <summary>
    /// Last audio extension setting
    /// </summary>
    private ExtensionSettings _lastAudioSettings;
    #endregion

    #region ctor
    /// <summary>
    /// Constructor, which initilizes the control
    /// </summary>
    public ExtensionSection()
    {
      InitializeComponent();
    }
    #endregion

    #region configuration methods
    /// <summary>
    /// Loads the configuration for this section from the plugin own configuration file
    /// </summary>
    public void LoadConfiguration()
    {
      ListBox workingList = null;
      PlayMode mode = PlayMode.Unrecognized;
      XmlDocument doc = new XmlDocument();
      string path = Config.GetFile(Config.Dir.Config, "MPlayer_ExtPlayer.xml");
      doc.Load(path);
      if (doc.DocumentElement != null)
      {
        XmlNodeList listExtensionFamilies = doc.DocumentElement.SelectNodes("/mplayer/extensions");
        if (listExtensionFamilies != null)
          foreach (XmlNode nodeFamily in listExtensionFamilies)
          {
            if (nodeFamily.Attributes["family"].Value.Equals("Video"))
            {
              workingList = videoExtList;
              mode = PlayMode.Video;
            }
            else if (nodeFamily.Attributes["family"].Value.Equals("Audio"))
            {
              workingList = audioExtList;
              mode = PlayMode.Audio;
            }
            if (workingList != null) workingList.Items.Clear();
            XmlNodeList listExtensions = nodeFamily.SelectNodes("Extension");
            if (listExtensions != null)
              foreach (XmlNode nodeExtension in listExtensions)
              {
                ExtensionSettings settings = new ExtensionSettings
                                               {
                                                 Name = nodeExtension.Attributes["name"].Value,
                                                 Arguments = nodeExtension.Attributes["arguments"].Value,
                                                 ExtPlayerUse =
                                                   Boolean.Parse(nodeExtension.Attributes["extPlayerUse"].Value),
                                                 PlayMode = mode
                                               };
                if (workingList != null && !workingList.Items.Contains(settings.Name))
                {
                  workingList.Items.Add(settings);
                }
              }
          }
      }
    }

    /// <summary>
    /// Saves the configuration for this section in the plugin own configuration file
    /// </summary>
    public void SaveConfiguration()
    {
      videoExtList.SelectedIndex = -1;
      audioExtList.SelectedIndex = -1;
      XmlTextWriter writer = new XmlTextWriter(Config.GetFile(Config.Dir.Config, "MPlayer_ExtPlayer.xml"),
                                               System.Text.Encoding.UTF8) { Formatting = Formatting.Indented, Indentation = 1, IndentChar = (char)9 };
      writer.WriteStartDocument(true);
      writer.WriteStartElement("mplayer"); //<mplayer>
      writer.WriteAttributeString("version", "1");
      writer.WriteStartElement("extensions"); //<extensions>
      writer.WriteAttributeString("family", "Video");
      ExtensionSettings temp;
      for (int i = 0; i < videoExtList.Items.Count; i++)
      {
        temp = videoExtList.Items[i] as ExtensionSettings;
        writer.WriteStartElement("Extension"); //<Extension>
        if (temp != null)
        {
          writer.WriteAttributeString("name", temp.Name);
          writer.WriteAttributeString("arguments", temp.Arguments);
          writer.WriteAttributeString("extPlayerUse", temp.ExtPlayerUse.ToString());
        }
        writer.WriteEndElement(); //</Extension>
      }
      writer.WriteEndElement(); //</extensions>
      writer.WriteStartElement("extensions"); //<extensions>
      writer.WriteAttributeString("family", "Audio");
      for (int i = 0; i < audioExtList.Items.Count; i++)
      {
        temp = audioExtList.Items[i] as ExtensionSettings;
        writer.WriteStartElement("Extension"); //<Extension>
        if (temp != null)
        {
          writer.WriteAttributeString("name", temp.Name);
          writer.WriteAttributeString("arguments", temp.Arguments);
          writer.WriteAttributeString("extPlayerUse", temp.ExtPlayerUse.ToString());
        }
        writer.WriteEndElement(); //</Extension>
      }
      writer.WriteEndElement(); //</extensions>
      writer.WriteEndElement(); //</mplayer>
      writer.WriteEndDocument();
      writer.Close();
    }
    #endregion

    #region event handling
    /// <summary>
    /// Handles the Selected index change on the video Extension List
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void VideoExtListSelectedIndexChanged(object sender, EventArgs e)
    {
      if (_lastVideoSettings != null)
      {
        _lastVideoSettings.Arguments = videoArgument.Text;
        _lastVideoSettings.ExtPlayerUse = videoPlayerUse.Checked;
      }

      if (videoExtList.SelectedIndex > -1)
      {
        _lastVideoSettings = videoExtList.SelectedItem as ExtensionSettings;
        if (_lastVideoSettings != null)
        {
          videoExtension.Text = _lastVideoSettings.Name;
          videoArgument.Text = _lastVideoSettings.Arguments;
          videoPlayerUse.Checked = _lastVideoSettings.ExtPlayerUse;
        }
        videoExtension.Enabled = true;
        videoArgument.Enabled = true;
        videoPlayerUse.Enabled = true;
      }
      else
      {
        videoExtension.Text = String.Empty;
        videoArgument.Text = String.Empty;
        videoPlayerUse.Checked = false;
        _lastVideoSettings = null;
        videoExtension.Enabled = false;
        videoArgument.Enabled = false;
        videoPlayerUse.Enabled = false;
      }

    }

    /// <summary>
    /// Handles the Add-Button click event on the video_audio tab
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void VideoAddClick(object sender, EventArgs e)
    {
      ExtensionSettings temp = new ExtensionSettings(".newExt", PlayMode.Video, "", false);
      videoExtList.Items.Add(temp);
      videoExtList.SelectedItem = temp;
    }

    /// <summary>
    /// Handles the Delete-Button click event on the video_audio tab
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void VideoDeleteClick(object sender, EventArgs e)
    {
      if (videoExtList.SelectedIndex > -1)
      {
        videoExtList.Items.RemoveAt(videoExtList.SelectedIndex);
        if (videoExtList.Items.Count > 0)
        {
          videoExtList.SelectedIndex = 0;
        }
        else
        {
          videoExtList.SelectedIndex = -1;
        }
      }
    }

    /// <summary>
    /// Handles the Leave event on the extension Textfield on the video_audio tab
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void VideoExtensionLeave(object sender, EventArgs e)
    {
      if (_lastVideoSettings != null && (!videoExtension.Text.Equals(_lastVideoSettings.Name))
          && videoExtList.Items.Contains(videoExtension.Text))
      {
        MessageBox.Show(this, @"Video Extension: " + videoExtension.Text + @" already in the list", @"MPlayer configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
        videoExtension.Focus();
        return;
      }
      if (_lastVideoSettings != null)
      {
        _lastVideoSettings.Name = videoExtension.Text;
        videoExtList.Items[videoExtList.SelectedIndex] = _lastVideoSettings;
      }
    }

    /// <summary>
    /// Handles the Selected index change on the audio Extension List
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void AudioExtListSelectedIndexChanged(object sender, EventArgs e)
    {
      if (_lastAudioSettings != null)
      {
        _lastAudioSettings.Arguments = audioArgument.Text;
        _lastAudioSettings.ExtPlayerUse = audioPlayerUse.Checked;
      }

      if (audioExtList.SelectedIndex > -1)
      {
        _lastAudioSettings = audioExtList.SelectedItem as ExtensionSettings;
        if (_lastAudioSettings != null)
        {
          audioExtension.Text = _lastAudioSettings.Name;
          audioArgument.Text = _lastAudioSettings.Arguments;
          audioPlayerUse.Checked = _lastAudioSettings.ExtPlayerUse;
        }
        audioExtension.Enabled = true;
        audioArgument.Enabled = true;
        audioPlayerUse.Enabled = true;
      }
      else
      {
        audioExtension.Text = String.Empty;
        audioArgument.Text = String.Empty;
        audioPlayerUse.Checked = false;
        _lastAudioSettings = null;
        audioExtension.Enabled = false;
        audioArgument.Enabled = false;
        audioPlayerUse.Enabled = false;
      }

    }

    /// <summary>
    /// Handles the Add-Button click event on the video_audio tab
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void AudioAddClick(object sender, EventArgs e)
    {
      ExtensionSettings temp = new ExtensionSettings(".newExt", PlayMode.Audio, "", false);
      audioExtList.Items.Add(temp);
      audioExtList.SelectedItem = temp;
    }

    /// <summary>
    /// Handles the Delete-Button click event on the video_audio tab
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void AudioDeleteClick(object sender, EventArgs e)
    {
      if (audioExtList.SelectedIndex > -1)
      {
        ExtensionSettings settings = audioExtList.SelectedItem as ExtensionSettings;
        if (settings != null)
          if (!settings.Name.Equals(".cda"))
          {
            audioExtList.Items.RemoveAt(audioExtList.SelectedIndex);
            if (audioExtList.Items.Count > 0)
            {
              audioExtList.SelectedIndex = 0;
            }
            else
            {
              audioExtList.SelectedIndex = -1;
            }
          }
      }
    }

    /// <summary>
    /// Handles the Leave event on the extension textfield on the video_audio tab
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void AudioExtensionLeave(object sender, EventArgs e)
    {
      if (_lastAudioSettings != null && (!audioExtension.Text.Equals(_lastAudioSettings.Name))
          && audioExtList.Items.Contains(audioExtension.Text))
      {
        MessageBox.Show(this, @"Audio Extension: " + audioExtension.Text + @" already in the list", @"MPlayer configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
        audioExtension.Focus();
        return;
      }
      if (_lastAudioSettings != null)
      {
        _lastAudioSettings.Name = audioExtension.Text;
        audioExtList.Items[audioExtList.SelectedIndex] = _lastAudioSettings;
      }

    }
    #endregion
  }
}
