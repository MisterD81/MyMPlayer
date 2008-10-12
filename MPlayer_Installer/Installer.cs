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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;
using MediaPortal.MPInstaller;
using MediaPortal.Util;

namespace MPlayer
{
  /// <summary>
  /// Installer plugin for MPI. It helps the user to configure the plugins
  /// </summary>
  public class Installer : IMPIInternalPlugin
  {

    #region Load Plugin
    /// <summary>
    /// Loads a plugin
    /// </summary>
    /// <param _name="pluginFile">Filename of the plugin</param>
    static public void LoadPlugins(string pluginFile)
    {
      if (!File.Exists(pluginFile))
      {
        MessageBox.Show("File not found " + pluginFile);
        return;
      }
      try
      {
        Assembly pluginAssembly = Assembly.LoadFrom(pluginFile);
        if (pluginAssembly != null)
        {
          Type[] exportedTypes = pluginAssembly.GetExportedTypes();

          foreach (Type type in exportedTypes)
          {
            if (type.IsAbstract)
            {
              continue;
            }
            if (type.GetInterface("MediaPortal.GUI.Library.ISetupForm") != null)
            {
              object pluginObject = Activator.CreateInstance(type);
              ISetupForm pluginForm = pluginObject as ISetupForm;
            }
          }
        }
      } catch (Exception unknownException)
      {
        MessageBox.Show("Exception in plugin loading :{0}", unknownException.Message);
      }
    }
    #endregion

    #region IMPIInternalPlugin Member
    /// <summary>
    /// On start install event
    /// </summary>
    /// <param _name="pk">Installer packacge struct</param>
    /// <returns>true, if successful</returns>
    public bool OnStartInstall(ref MPpackageStruct pk)
    {
      return true;
    }

    /// <summary>
    /// On end install event
    /// </summary>
    /// <param _name="pk">Installer packacge struct</param>
    /// <returns>true, if successful</returns>
    public bool OnEndInstall(ref MPpackageStruct pk)
    {
      String configPath = Config.GetFolder(Config.Dir.Plugins);
      LoadPlugins(configPath + @"\ExternalPlayers\MPlayer_ExtPlayer.dll");
      LoadPlugins(configPath + @"\Windows\MPlayer_GUIPlugin.dll");
      ConfigurationWizard wizard = new ConfigurationWizard();
      wizard.ShowDialog();
      using (MediaPortal.Profile.Settings xmlWriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        xmlWriter.SetValueAsBool("plugins", "My MPlayer GUI", true);
        xmlWriter.SetValueAsBool("plugins", "MPlayer", true);
        xmlWriter.SetValueAsBool("pluginsdlls", "MPlayer_GUIPlugin.dll", true);
        xmlWriter.SetValueAsBool("pluginsdlls", "MPlayer_ExtPlayer.dll", true);
        xmlWriter.SetValueAsBool("home", "My MPlayer GUI", true);
        xmlWriter.SetValueAsBool("myplugins", "My MPlayer GUI", false);
        xmlWriter.SetValueAsBool("pluginswindows", "MPlayer.MPlayer_GUIPlugin", true);
      }
      MediaPortal.Profile.Settings.SaveCache();
      return true;
    }

    /// <summary>
    /// On start uninstall event
    /// </summary>
    /// <param _name="pk">Installer packacge struct</param>
    /// <returns>true, if successful</returns>
    public bool OnStartUnInstall(ref MPpackageStruct pk)
    {
      return true;
    }

    /// <summary>
    /// On end uninstall event
    /// </summary>
    /// <param _name="pk">Installer packacge struct</param>
    /// <returns>true, if successful</returns>
    public bool OnEndUnInstall(ref MPpackageStruct pk)
    {
      DialogResult result = MessageBox.Show("Do you want to remove the settings from the Mediaportal configuration file?", "My MPlayer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if (result == DialogResult.Yes)
      {
        String path = Config.GetFile(Config.Dir.Config, "MPlayer_ExtPlayer.xml");
        if (File.Exists(path))
        {
          File.Delete(path);
        }
        path = Config.GetFile(Config.Dir.Config, "MPlayer_GUIPlugin.xml");
        if (File.Exists(path))
        {
          File.Delete(path);
        }
        using (MediaPortal.Profile.Settings xmlWriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml"), false))
        {
          xmlWriter.RemoveEntry("mplayer", "generalArguments");
          xmlWriter.RemoveEntry("mplayer", "osd");
          xmlWriter.RemoveEntry("mplayer", "rebuildIndex");
          xmlWriter.RemoveEntry("mplayer", "priorityBoost");
          xmlWriter.RemoveEntry("mplayer", "framedrop");
          xmlWriter.RemoveEntry("mplayer", "doubleBuffering");
          xmlWriter.RemoveEntry("mplayer", "directRendering");
          xmlWriter.RemoveEntry("mplayer", "audioNormalize");
          xmlWriter.RemoveEntry("mplayer", "passthroughAC3DTS");
          xmlWriter.RemoveEntry("mplayer", "subtitleFontName");
          xmlWriter.RemoveEntry("mplayer", "soundOutputDriver");
          xmlWriter.RemoveEntry("mplayer", "soundOutputDevice");
          xmlWriter.RemoveEntry("mplayer", "deinterlace");
          xmlWriter.RemoveEntry("mplayer", "aspectRatio");
          xmlWriter.RemoveEntry("mplayer", "postProcessing");
          xmlWriter.RemoveEntry("mplayer", "audioChannels");
          xmlWriter.RemoveEntry("mplayer", "enableSubtitles");
          xmlWriter.RemoveEntry("mplayer", "videoOutputDriver");
          xmlWriter.RemoveEntry("mplayer", "audioDelayStep");
          xmlWriter.RemoveEntry("mplayer", "subtitleDelayStep");
          xmlWriter.RemoveEntry("mplayer", "subtitleSize");
          xmlWriter.RemoveEntry("mplayer", "subtitlePosition");
          xmlWriter.RemoveEntry("mplayer", "cacheSize");
          xmlWriter.RemoveEntry("mplayer", "noise");
          xmlWriter.RemoveEntry("mplayer", "mplayerPath");
          xmlWriter.RemoveEntry("mplayer", "dvdArguments");
          xmlWriter.RemoveEntry("mplayer", "vcdArguments");
          xmlWriter.RemoveEntry("mplayer", "svcdArguments");
          xmlWriter.RemoveEntry("mplayer", "cueArguments");
          xmlWriter.RemoveEntry("mplayer", "ftpArguments");
          xmlWriter.RemoveEntry("mplayer", "httpArguments");
          xmlWriter.RemoveEntry("mplayer", "mmsArguments");
          xmlWriter.RemoveEntry("mplayer", "mpstArguments");
          xmlWriter.RemoveEntry("mplayer", "rtspArguments");
          xmlWriter.RemoveEntry("mplayer", "sdpArguments");
          xmlWriter.RemoveEntry("mplayer", "udpArguments");
          xmlWriter.RemoveEntry("mplayer", "unsvArguments");
          xmlWriter.RemoveEntry("mplayer", "displayNameOfGUI");
          xmlWriter.RemoveEntry("mplayer", "useMyMusicShares");
          xmlWriter.RemoveEntry("mplayer", "useMyVideoShares");
        }
      }
      return true;
    }
    #endregion
  }
}
