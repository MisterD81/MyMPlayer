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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MediaPortal.UserInterface.Controls;

namespace MPlayer
{
  /// <summary>
  /// This class is the configuration wizard for the installer
  /// </summary>
  public partial class ConfigurationWizard : MPConfigForm
  {
    #region variables
    /// <summary>
    /// Current step in the wizard
    /// </summary>
    private int _currentStep;
    #endregion

    #region ctor
    /// <summary>
    /// Constructor, which initializes the wizard and load initial 
    /// configuration values
    /// </summary>
    public ConfigurationWizard()
    {
      InitializeComponent();
      streamSection1.LoadConfiguration();
      extensionSection1.LoadConfiguration();
      audioSection1.LoadConfiguration();
      subtitleSection1.LoadConfiguration();
      videoSection1.LoadConfiguration();
      generalSection1.LoadConfiguration();
      guiConfiguration1.LoadConfiguration();
      _currentStep = 0;
      switchToStep();
    }
    #endregion

    #region step handling
    /// <summary>
    /// Switches to the new selected step and updates the gui
    /// </summary>
    private void switchToStep()
    {
      switch (_currentStep)
      {
        case 0:
          backButton.Enabled = false;
          nextButton.Enabled = true;
          generalSection1.Visible = true;
          videoSection1.Visible = false;
          subtitleSection1.Visible = false;
          audioSection1.Visible = false;
          extensionSection1.Visible = false;
          streamSection1.Visible = false;
          guiConfiguration1.Visible = false;
          this.Text = "My MPlayer Configuration Wizard (1/7)";
          mainLabel.Text = "This configuration wizard will guide you through the configuration of My MPlayer.\n";
          mainLabel.Text += "You only have to specify the location of your MPlayer installation. All other values are set to recommend values.\n";
          mainLabel.Text += "In each step the wizard will give you some additional hints for the configuration of the settings.";

          infoBox.Text = "My MPlayer offers two different OSD, which can be used. The first one is the internal OSD of MPlayer. ";
          infoBox.Text += "This is also the recommend OSD. The second OSD is based on a special library. The main advantage is ";
          infoBox.Text += "that it looks like the standard OSD of MP and has more features (e.g. buffering status). ";
          infoBox.Text += "The main disadvantage is that it needs more cpu power.\r\n\r\n";
          infoBox.Text += "The cachesize is a very important setting, if you want to use MPlayer in combination with internet streams or DVDs. ";
          infoBox.Text += "The reason is that the standard cache size of MPlayer is very small. It is highly recommend to use at least a value ";
          infoBox.Text += "of 2048 (KB)\r\n\r\n";
          infoBox.Text += "If you any encouter stuttering than you should try to activate the option 'Priority Boost', which ";
          infoBox.Text += "increases the process priority of MPlayer\r\n\r\n";
          infoBox.Text += "Vista users should use the latest commandline version of MPlayer in combination with OpenGL or OpenGL2 video output driver. ";
          infoBox.Text += "This will give you the best results.";

          break;
        case 1:
          backButton.Enabled = true;
          nextButton.Enabled = true;
          generalSection1.Visible = false;
          videoSection1.Visible = true;
          subtitleSection1.Visible = false;
          audioSection1.Visible = false;
          extensionSection1.Visible = false;
          streamSection1.Visible = false;
          guiConfiguration1.Visible = false;
          this.Text = "My MPlayer Configuration Wizard (2/7)";
          mainLabel.Text = "In the second step you can set the video options.";

          infoBox.Text = "MPlayer offers different video output drivers. Unfortunately in combination with MP only the available video ";
          infoBox.Text += "output drivers are available, because the other drivers don't support to embed the video in MediaPortal. ";
          infoBox.Text += "If you are using XP than you should the DirectX driver. Vista users should use the OpenGL, OpenGL2 driver with Aero.";
          infoBox.Text += "If you encouter any error, than you sould try disabling the options 'Double buffering' and 'Direct Rendering'. \r\n\r\n";
          infoBox.Text += "You can turn on the option 'Framedrop' for a better A/V sync, which means that MPlayer will skip some frames.";

          break;
        case 2:
          backButton.Enabled = true;
          nextButton.Enabled = true;
          generalSection1.Visible = false;
          videoSection1.Visible = false;
          subtitleSection1.Visible = true;
          audioSection1.Visible = false;
          extensionSection1.Visible = false;
          streamSection1.Visible = false;
          guiConfiguration1.Visible = false;
          this.Text = "My MPlayer Configuration Wizard (3/7)";
          mainLabel.Text = "The next step is to configure the subtitle options. These are indepent from you selected options in My Movies of MP.";

          infoBox.Text = "The subtitle configuration of this plugin isn't only important for the displayed subtitles. ";
          infoBox.Text += "It also controls some parameters of the internal OSD of MPlayer. These paramters are the size ";
          infoBox.Text += "and the font of the subtitles, which will also be used for the internal OSD. This is a limitation ";
          infoBox.Text += "of MPlayer, which can't be changed. \r\n\r\n";
          infoBox.Text += "The option 'Enable subtitle' is independent from you selection of the plugin 'My Video' of MediaPortal. ";

          break;
        case 3:
          backButton.Enabled = true;
          nextButton.Enabled = true;
          generalSection1.Visible = false;
          videoSection1.Visible = false;
          subtitleSection1.Visible = false;
          audioSection1.Visible = true;
          extensionSection1.Visible = false;
          streamSection1.Visible = false;
          guiConfiguration1.Visible = false;
          this.Text = "My MPlayer Configuration Wizard (4/7)";
          mainLabel.Text = "In step 4 you can configure the audio options of MPlayer.";

          infoBox.Text = "MPlayer offers also different audio output drivers. If you select the DirectSound driver, than you can ";
          infoBox.Text += "select the output device. The other drivers don't have this feature.\r\n\r\n";
          infoBox.Text += "The option 'Audio delay step' is only important, if you want to adjust the audio delay during playback. ";
          infoBox.Text += "This option only defines the step, with that you can change the audio delay.";

          break;
        case 4:
          backButton.Enabled = true;
          nextButton.Enabled = true;
          generalSection1.Visible = false;
          videoSection1.Visible = false;
          subtitleSection1.Visible = false;
          audioSection1.Visible = false;
          extensionSection1.Visible = true;
          streamSection1.Visible = false;
          guiConfiguration1.Visible = false;
          this.Text = "My MPlayer Configuration Wizard (5/7)";
          mainLabel.Text = "After configuring the general options, you can now configure how My MPlayer is integrated in MP.\n";
          mainLabel.Text += "Only extensions where the option \"External player use\" is selected are played in My Movie or My Music.\n";
          mainLabel.Text += "Otherwise they are only played by the gui plugin of My MPlayer. ";
          mainLabel.Text += "You can also specify additional arguments for each extension.";

          infoBox.Text = "Both plugins (External player and GUI) only handle the extension that are listed here. ";
          infoBox.Text += "If you want additional extensions to be supported, you have add them here. ";

          break;
        case 5:
          backButton.Enabled = true;
          nextButton.Enabled = true;
          generalSection1.Visible = false;
          videoSection1.Visible = false;
          subtitleSection1.Visible = false;
          audioSection1.Visible = false;
          extensionSection1.Visible = false;
          streamSection1.Visible = true;
          guiConfiguration1.Visible = false;
          this.Text = "My MPlayer Configuration Wizard (6/7)";
          mainLabel.Text = "In the last step of the configuration of the external player you can specify additional arguments \n";
          mainLabel.Text += "for the playback of the specified streams.";

          infoBox.Text = "The plugin sets automatically langauge options for DVD, VCD and SVCDS depending and you current localisation.";

          break;
        case 6:
          backButton.Enabled = true;
          nextButton.Enabled = false;
          generalSection1.Visible = false;
          videoSection1.Visible = false;
          subtitleSection1.Visible = false;
          audioSection1.Visible = false;
          extensionSection1.Visible = false;
          streamSection1.Visible = false;
          guiConfiguration1.Visible = true;
          this.Text = "My MPlayer Configuration Wizard (7/7)";
          mainLabel.Text = "In the last step of this wizard, you can configure the gui plugin of My MPlayer.";

          infoBox.Text = "";

          break;
      }
    }
    #endregion

    #region event handling
    /// <summary>
    /// Handles the finish button click event
    /// </summary>
    /// <param _name="sender">Sender</param>
    /// <param _name="e">Event args</param>
    private void finishButton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    /// <summary>
    /// Handles the back button click event
    /// </summary>
    /// <param _name="sender">Sender</param>
    /// <param _name="e">Event args</param>
    private void backButton_Click(object sender, EventArgs e)
    {
      _currentStep--;
      switchToStep();
    }

    /// <summary>
    /// Handles the next button click event
    /// </summary>
    /// <param _name="sender">Sender</param>
    /// <param _name="e">Event args</param>
    private void nextButton_Click(object sender, EventArgs e)
    {
      _currentStep++;
      switchToStep();
    }

    /// <summary>
    /// Handles the form closing event
    /// </summary>
    /// <param _name="sender">Sender</param>
    /// <param _name="e">Event args</param>
    private void ConfigurationWizard_FormClosing(object sender, FormClosingEventArgs e)
    {
      generalSection1.SaveConfiguration();
      MediaPortal.Profile.Settings.SaveCache();
      videoSection1.SaveConfiguration();
      MediaPortal.Profile.Settings.SaveCache();
      audioSection1.SaveConfiguration();
      MediaPortal.Profile.Settings.SaveCache();
      subtitleSection1.SaveConfiguration();
      MediaPortal.Profile.Settings.SaveCache();
      streamSection1.SaveConfiguration();
      MediaPortal.Profile.Settings.SaveCache();
      extensionSection1.SaveConfiguration();
      MediaPortal.Profile.Settings.SaveCache();
      guiConfiguration1.SaveConfiguration();
      MediaPortal.Profile.Settings.SaveCache();
    }
    #endregion
  }
}