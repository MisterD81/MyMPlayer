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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MediaPortal.GUI.Library;
using MediaPortal.Util;
using MediaPortal.Configuration;
using Microsoft.Win32;

namespace MPlayer
{

  #region enumeration
  /// <summary>
  /// State of the player
  /// </summary>
  public enum PlayState
  {
    /// <summary>
    /// In init phase
    /// </summary>
    Init,
    /// <summary>
    /// Playing
    /// </summary>
    Playing,
    /// <summary>
    /// Paused
    /// </summary>
    Paused,
    /// <summary>
    /// Stopped
    /// </summary>
    Stopped,
    /// <summary>
    /// Ended
    /// </summary>
    Ended
  }

  /// <summary>
  /// Possible Deinterlace Methods
  /// </summary>
  public enum Deinterlace
  {
    /// <summary>
    /// No Deinterlacing
    /// </summary>
    Off = 0,
    /// <summary>
    /// Simple Deinterlacing
    /// </summary>
    Simple = 1,
    /// <summary>
    /// Adaptive Deinterlacing
    /// </summary>
    Adaptive = 2
  }

  /// <summary>
  /// Possible AspectRatios
  /// </summary>
  public enum AspectRatio
  {
    /// <summary>
    /// Determine Automatic A/R
    /// </summary>
    Automatic = 0,
    /// <summary>
    /// Force 4:3 A/R
    /// </summary>
    m_4x3 = 1,
    /// <summary>
    /// Force 16:9 A/R
    /// </summary>
    m_16x9 = 2,
    /// <summary>
    /// Force 2,35 A/R
    /// </summary>
    m_2_35 = 3
  }

  /// <summary>
  /// Possible SoundOutputs
  /// </summary>
  public enum SoundOutputDriver
  {
    /// <summary>
    /// Don't Decode audio
    /// </summary>
    NoDecoding = 0,
    /// <summary>
    /// No audio output
    /// </summary>
    NoOutput = 1,
    /// <summary>
    /// Win32 output
    /// </summary>
    Win32 = 2,
    /// <summary>
    /// DirectSound output
    /// </summary>
    DirectSound = 3
  }

  /// <summary>
  /// Possible Postprocessing methods
  /// </summary>
  public enum PostProcessing
  {
    /// <summary>
    /// No Postprocessing
    /// </summary>
    NoPostProcessing = 0,
    /// <summary>
    /// Automatic Postprocessing
    /// </summary>
    Automatic = 1,
    /// <summary>
    /// Maximum Postprocessing
    /// </summary>
    Maximum = 2
  }

  /// <summary>
  /// Possible modes of audio channels
  /// </summary>
  public enum AudioChannels
  {
    /// <summary>
    /// Default audio channels of file
    /// </summary>
    Default,
    /// <summary>
    /// Stereo
    /// </summary>
    Stereo,
    /// <summary>
    /// Surround
    /// </summary>
    Surround,
    /// <summary>
    /// 5.1
    /// </summary>
    Full_5_1
  }

  /// <summary>
  /// Possible NoiseDenoise methods
  /// </summary>
  public enum NoiseDenoise
  {
    /// <summary>
    /// No _noiseDenoise
    /// </summary>
    Nothing,
    /// <summary>
    /// NoiseDenoise
    /// </summary>
    Noise,
    /// <summary>
    /// High quality denoise
    /// </summary>
    HighQualityDenoise,
    /// <summary>
    /// Denoise
    /// </summary>
    Denoise
  }

  /// <summary>
  /// Possible Video output driver
  /// </summary>
  public enum VideoOutputDriver
  {
    /// <summary>
    /// DirectX
    /// </summary>
    DirectX,
    /// <summary>
    /// DirectX without acceleration
    /// </summary>
    DirectXNoAccel,
    /// <summary>
    /// OpenGL
    /// </summary>
    OpenGL,
    /// <summary>
    /// OpenGL second generation
    /// </summary>
    OpenGL2
  }

  /// <summary>
  /// Possible osd variants
  /// </summary>
  public enum OSDMode
  {
    /// <summary>
    /// Internal MPlayer osd
    /// </summary>
    InternalMPlayer,
    /// <summary>
    /// OSD with the external osd library
    /// </summary>
    ExternalOSDLibrary
  }
  #endregion

  /// <summary>
  /// Handles configuration operations. Creates a list of all possible fonts
  /// and gernerates the parameters for the mplayer process
  /// </summary>
  public class ConfigurationManager
  {
    #region variables
    /// <summary>
    /// Singleton instance
    /// </summary>
    private static ConfigurationManager _singletonInstance;

    /// <summary>
    /// List of installed fonts
    /// </summary>
    private List<String> _fontsCollection;

    /// <summary>
    /// Supported Extension of the external Player
    /// </summary>
    private static string[] _supportedExtensions = new string[0];

    /// <summary>
    /// Dicitionary with all extension Settings
    /// </summary>
    private static Dictionary<String, ExtensionSettings> _extensionSettings = new Dictionary<String, ExtensionSettings>();

    /// <summary>
    /// Dictionary with all extension Setting for external Player
    /// </summary>
    private static Dictionary<String, ExtensionSettings> _extensionSettingsExtPlayer = new Dictionary<String, ExtensionSettings>();

    /// <summary>
    /// Rebuild Index of the file
    /// </summary>
    private bool _rebuildIndex;

    /// <summary>
    /// Prioritey Boost of the process
    /// </summary>
    private bool _priorityBoost;

    /// <summary>
    /// Framedrop
    /// </summary>
    private bool _framedrop;

    /// <summary>
    /// Double buffering
    /// </summary>
    private bool _doubleBuffering;

    /// <summary>
    /// Direct rendering
    /// </summary>
    private bool _directRendering;

    /// <summary>
    /// Normalize audio
    /// </summary>
    private bool _audioNormalize;

    /// <summary>
    /// Passthrough AC3 and DTS
    /// </summary>
    private bool _passthroughAC3_DTS;

    /// <summary>
    /// Deinterlace mode
    /// </summary>
    private Deinterlace _deinterlace;

    /// <summary>
    /// Selected SoundOutput Driver
    /// </summary>
    private SoundOutputDriver _soundOutputDriver;

    /// <summary>
    /// Sound output device
    /// </summary>
    private int _soundOutputDevice;

    /// <summary>
    /// Selected AsprectRatio
    /// </summary>
    private AspectRatio _aspectRatio;

    /// <summary>
    /// Selected Postprocessing mode
    /// </summary>
    private PostProcessing _postProcessing;

    /// <summary>
    /// Audio channel mode
    /// </summary>
    private AudioChannels _audioChannels;

    /// <summary>
    /// NoiseDenoise mode
    /// </summary>
    private NoiseDenoise _noiseDenoise;

    /// <summary>
    /// Cache size
    /// </summary>
    private int _cacheSize;

    /// <summary>
    /// Indicate if the font of the subtitle is set
    /// </summary>
    private bool _subtitleFontSet;

    /// <summary>
    /// Filename of the subtitle font
    /// </summary>
    private string _subtitleFontFileName;

    /// <summary>
    /// Path to mplayer.exe
    /// </summary>
    private String _mplayerPath;

    /// <summary>
    /// Timeout before a seek step is performed
    /// </summary>
    private int _seekStepTimeout;

    /// <summary>
    /// Enable subtitles by default
    /// </summary>
    private bool _enableSubtitles;

    /// <summary>
    /// Video output driver
    /// </summary>
    private VideoOutputDriver _videoOutputDriver;

    /// <summary>
    /// Step to change the audio delay in milliseconds
    /// </summary>
    private int _audioDelayStep;

    /// <summary>
    /// Step to change the subtitle delay in millisceonds
    /// </summary>
    private int _subtitleDelayStep;

    /// <summary>
    /// Start posisition of the subtitle
    /// </summary>
    private int _subtitlePosition;

    /// <summary>
    /// Size at startup of the subtitle (including the MPlayer OSD)
    /// </summary>
    private int _subtitleSize;

    /// <summary>
    /// Indicates the mode of the OSD, which the user wants to use
    /// </summary>
    private OSDMode _osdMode;
    #endregion

    #region ctor
    /// <summary>
    /// Initialize the system
    /// </summary>
    private ConfigurationManager()
    {
      InitializeFontList();
      ReadConfig();
    }

    /// <summary>
    /// Returns the singleton instance
    /// </summary>
    /// <returns>Singleton instance</returns>
    public static ConfigurationManager GetInstance()
    {
      if (_singletonInstance == null)
      {
        _singletonInstance = new ConfigurationManager();
      }
      return _singletonInstance;
    }
    #endregion

    #region properties
    /// <summary>
    /// Returns if the Subtitles are enabled by default
    /// </summary>
    public bool EnableSubtitles
    {
      get { return _enableSubtitles; }
    }

    /// <summary>
    /// Returns the step to change the audio delay in milliseconds
    /// </summary>
    public int AudioDelayStep
    {
      get { return _audioDelayStep; }
    }

    /// <summary>
    /// Returns the step to change the subtitle delay in milliseconds
    /// </summary>
    public int SubtitleDelayStep
    {
      get { return _subtitleDelayStep; }
    }

    /// <summary>
    /// Gets the start position of the subtitles
    /// </summary>
    public int SubtitlePosition
    {
      get { return _subtitlePosition; }
    }

    /// <summary>
    /// Gets the start size of the subtitles and the MPlayer OSD
    /// </summary>
    public int SubtitleSize
    {
      get { return _subtitleSize; }
    }

    /// <summary>
    /// Gets the mode of the osd, which the user wants to use
    /// </summary>
    public OSDMode OsdMode
    {
      get { return _osdMode; }
    }
    #endregion

    #region private methods
    /// <summary>
    /// Generates the list of all installed fonts
    /// </summary>
    private void InitializeFontList()
    {
      _fontsCollection = new List<string>();
      InstalledFontCollection fonts = new InstalledFontCollection();
      string fileName;
      foreach (FontFamily family in fonts.Families)
      {
        if (CheckSubtitleFont(family.Name, out fileName))
        {
          _fontsCollection.Add(family.Name);
        }
      }
    }

    /// <summary>
    /// Checks if we can retrieve the filename of the font family
    /// </summary>
    /// <param _name="subtitleFont">Name of the font family</param>
    /// <param _name="fileName">Filename of the font family</param>
    /// <returns>true, if Filename can be retrieved</returns>
    private bool CheckSubtitleFont(string subtitleFont, out string fileName)
    {
      fileName = String.Empty;
      using (RegistryKey subkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Fonts"))
      {
        if (subkey != null)
        {
          fileName = (string)subkey.GetValue(subtitleFont + " (TrueType)");
          if (fileName == null)
          {
            fileName = (string)subkey.GetValue(subtitleFont);
          }
          if (fileName == null)
          {
            fileName = (string)subkey.GetValue(subtitleFont + " (All Res)");
          }
        }
      }
      using (RegistryKey subkey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Fonts"))
      {
        if (subkey != null)
        {
          if (fileName == null)
          {
            fileName = (string)subkey.GetValue(subtitleFont + " (TrueType)");
          }
          if (fileName == null)
          {
            fileName = (string)subkey.GetValue(subtitleFont);
          }
          if (fileName == null)
          {
            fileName = (string)subkey.GetValue(subtitleFont + " (All Res)");
          }
        }
      }
      if (!System.IO.Path.IsPathRooted(fileName))
      {
        String systemroot = Environment.GetEnvironmentVariable("SystemRoot");
        fileName = systemroot + @"\fonts\" + fileName;
      }
      if (fileName != String.Empty)
      {
        return System.IO.File.Exists(fileName);
      }
      return false;
    }

    /// <summary>
    /// Reads the whole configuration
    /// </summary>
    private void ReadConfig()
    {
      try
      {
        string strExtAudio = null;
        string strExtVideo = null;
        ExtensionSettings mplayerSetting = new ExtensionSettings(".mplayer", PlayMode.Unrecognized, "", true);
        _extensionSettings = new Dictionary<string, ExtensionSettings>();
        _extensionSettings.Add(mplayerSetting.Name, mplayerSetting);
        _extensionSettingsExtPlayer = new Dictionary<string, ExtensionSettings>();
        _extensionSettingsExtPlayer.Add(mplayerSetting.Name, mplayerSetting);
        String arguments;
        ExtensionSettings settings;
        String videoArguments;
        String audioArguments;
        using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
        {
          _osdMode = (OSDMode)xmlreader.GetValueAsInt("mplayer", "osd", (int)OSDMode.InternalMPlayer);
          strExtAudio = xmlreader.GetValueAsString("mplayer", "enabledextensionsAudio", "");
          strExtVideo = xmlreader.GetValueAsString("mplayer", "enabledextensionsVideo", "");
          _rebuildIndex = xmlreader.GetValueAsBool("mplayer", "rebuildIndex", false);
          _priorityBoost = xmlreader.GetValueAsBool("mplayer", "priorityBoost", true);
          _framedrop = xmlreader.GetValueAsBool("mplayer", "framedrop", false);
          _doubleBuffering = xmlreader.GetValueAsBool("mplayer", "doubleBuffering", true);
          _directRendering = xmlreader.GetValueAsBool("mplayer", "directRendering", true);
          _audioNormalize = xmlreader.GetValueAsBool("mplayer", "audioNormalize", false);
          _passthroughAC3_DTS = xmlreader.GetValueAsBool("mplayer", "passthroughAC3DTS", false);
          _soundOutputDriver = (SoundOutputDriver)xmlreader.GetValueAsInt("mplayer", "soundOutputDriver", (int)SoundOutputDriver.DirectSound);
          _soundOutputDevice = xmlreader.GetValueAsInt("mplayer", "soundOutputDevice", 0);
          _deinterlace = (Deinterlace)xmlreader.GetValueAsInt("mplayer", "deinterlace", (int)Deinterlace.Adaptive);
          _aspectRatio = (AspectRatio)xmlreader.GetValueAsInt("mplayer", "aspectRatio", (int)AspectRatio.Automatic);
          _postProcessing = (PostProcessing)xmlreader.GetValueAsInt("mplayer", "postProcessing", (int)PostProcessing.Maximum);
          _audioChannels = (AudioChannels)xmlreader.GetValueAsInt("mplayer", "audioChannels", (int)AudioChannels.Default);
          _noiseDenoise = (NoiseDenoise)xmlreader.GetValueAsInt("mplayer", "noise", (int)NoiseDenoise.Nothing);
          _cacheSize = xmlreader.GetValueAsInt("mplayer", "cacheSize", 0);
          _audioDelayStep = xmlreader.GetValueAsInt("mplayer", "audioDelayStep", 100);
          _subtitleDelayStep = xmlreader.GetValueAsInt("mplayer", "subtitleDelayStep", 100);
          _subtitlePosition = xmlreader.GetValueAsInt("mplayer", "subtitlePosition", 100);
          _subtitleSize = xmlreader.GetValueAsInt("mplayer", "subtitleSize", 5);
          string subtitleFontName = xmlreader.GetValueAsString("mplayer", "subtitleFontName", "Arial");
          _subtitleFontSet = CheckSubtitleFont(subtitleFontName, out _subtitleFontFileName);
          _mplayerPath = xmlreader.GetValueAsString("mplayer", "mplayerPath", "C:\\Program Files\\MPlayer\\");
          xmlreader.GetValueAsString("mplayer", "mplayerPath", "C:\\Program Files\\MPlayer\\Mplayer.exe");
          arguments = xmlreader.GetValueAsString("mplayer", "generalArguments", "");
          settings = new ExtensionSettings("general", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "dvdArguments", String.Empty);
          settings = new ExtensionSettings("dvd://", PlayMode.Video, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "vcdArguments", String.Empty);
          settings = new ExtensionSettings("vcd://", PlayMode.Video, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "svcdArguments", String.Empty);
          settings = new ExtensionSettings("svcd://", PlayMode.Video, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "cueArguments", String.Empty);
          settings = new ExtensionSettings("cue://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "ftpArguments", String.Empty);
          settings = new ExtensionSettings("ftp://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "httpArguments", String.Empty);
          settings = new ExtensionSettings("http://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          settings = new ExtensionSettings("http_proxy://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "mmsArguments", String.Empty);
          settings = new ExtensionSettings("mms://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          settings = new ExtensionSettings("mmst://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "mpstArguments", String.Empty);
          settings = new ExtensionSettings("mpst://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "rtspArguments", String.Empty);
          settings = new ExtensionSettings("rtsp://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          settings = new ExtensionSettings("rtp://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "sdpArguments", String.Empty);
          settings = new ExtensionSettings("sdp://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "udpArguments", String.Empty);
          settings = new ExtensionSettings("udp://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          arguments = xmlreader.GetValueAsString("mplayer", "unsvArguments", String.Empty);
          settings = new ExtensionSettings("unsv://", PlayMode.Unrecognized, arguments, false);
          _extensionSettings.Add(settings.Name, settings);
          videoArguments = xmlreader.GetValueAsString("mplayer", "videoArguments", String.Empty);
          audioArguments = xmlreader.GetValueAsString("mplayer", "audioArguments", String.Empty);
          _enableSubtitles = xmlreader.GetValueAsBool("mplayer", "enableSubtitles", false);
          _videoOutputDriver = (VideoOutputDriver)xmlreader.GetValueAsInt("mplayer", "videoOutputDriver", (int)VideoOutputDriver.DirectX);
          string timeout = (xmlreader.GetValueAsString("movieplayer", "skipsteptimeout", "1500"));

          if (timeout == string.Empty)
            _seekStepTimeout = 1500;
          else
            _seekStepTimeout = Convert.ToInt16(timeout);

          String m_strLanguage = xmlreader.GetValueAsString("skin", "language", "English");
          LocalizeStrings.Load(m_strLanguage);
        }
        LoadXMLData();
      } catch (Exception e)
      {
        Log.Error(e);
      }
      _supportedExtensions = new String[_extensionSettingsExtPlayer.Count];
      _extensionSettingsExtPlayer.Keys.CopyTo(_supportedExtensions, 0);
    }

    /// <summary>
    /// Loads the Plugin specific XML configuration file
    /// </summary>
    private void LoadXMLData()
    {
      ExtensionSettings settings = null;
      PlayMode mode = PlayMode.Unrecognized;
      XmlDocument doc = new XmlDocument();
      string path = Config.GetFile(Config.Dir.Config, "MPlayer_ExtPlayer.xml");
      doc.Load(path);
      XmlNodeList listExtensionFamilies = doc.DocumentElement.SelectNodes("/mplayer/extensions");
      foreach (XmlNode nodeFamily in listExtensionFamilies)
      {
        if (nodeFamily.Attributes["family"].Value.Equals("Video"))
        {
          mode = PlayMode.Video;
        }
        else if (nodeFamily.Attributes["family"].Value.Equals("Audio"))
        {
          mode = PlayMode.Audio;
        }
        XmlNodeList listExtensions = nodeFamily.SelectNodes("Extension");
        foreach (XmlNode nodeExtension in listExtensions)
        {
          settings = new ExtensionSettings();
          settings.Name = nodeExtension.Attributes["name"].Value;
          settings.Arguments = nodeExtension.Attributes["arguments"].Value;
          settings.ExtPlayerUse = Boolean.Parse(nodeExtension.Attributes["extPlayerUse"].Value);
          settings.PlayMode = mode;
          if (!_extensionSettings.ContainsKey(settings.Name))
          {
            _extensionSettings.Add(settings.Name, settings);
            if (settings.ExtPlayerUse)
            {
              _extensionSettingsExtPlayer.Add(settings.Name, settings);
            }
          }
        }
      }

    }

    /// <summary>
    /// Generates the general _arguments
    /// </summary>
    /// <param _name="handle">Handle to inner panel</param>
    /// <returns>General _arguments</returns>
    private StringBuilder GetGeneralArguments(IntPtr handle)
    {
      StringBuilder arguments = new StringBuilder();
      if (_mplayerPath.EndsWith("\\gmplayer.exe"))
      {
        arguments.Append("-nogui -noconsolecontrols ");
      }
      arguments.Append("-slave -quiet -identify -wid ");
      arguments.Append(handle);
      arguments.Append(" -colorkey 0x101010 -nokeepaspect -autosync 100 -noborder ");
      if (_subtitlePosition != 100)
      {
        arguments.Append("-subpos " + _subtitlePosition + " ");
      }
      if (_subtitleSize != 5)
      {
        arguments.Append("−subfont-text-scale " + _subtitleSize + " ");
        arguments.Append("−subfont-osd-scale " + _subtitleSize + " ");
      }
      if (_subtitleFontSet)
      {
        arguments.Append("-font ");
        arguments.Append(_subtitleFontFileName);
        arguments.Append(" ");
      }
      if (_priorityBoost)
      {
        arguments.Append("-priority abovenormal ");
      }
      if (_rebuildIndex)
      {
        arguments.Append("-idx ");
      }
      if (_framedrop)
      {
        arguments.Append("-framedrop ");
      }
      if (_doubleBuffering)
      {
        arguments.Append("-double ");
      }
      else
      {
        arguments.Append("-nodouble ");
      }
      if (_directRendering)
      {
        arguments.Append("-dr ");
      }
      if (_audioNormalize)
      {
        arguments.Append("-af volnorm ");
      }
      if (_passthroughAC3_DTS)
      {
        arguments.Append("-afm hwac3 ");
      }
      if (_cacheSize > 0)
      {
        arguments.Append("-cache ");
        arguments.Append(_cacheSize);
        arguments.Append(" ");
      }
      switch (_soundOutputDriver)
      {
        case SoundOutputDriver.NoDecoding:
          arguments.Append("-nosound ");
          break;
        case SoundOutputDriver.NoOutput:
          arguments.Append("-ao null ");
          break;
        case SoundOutputDriver.Win32:
          arguments.Append("-ao win32 ");
          break;
        case SoundOutputDriver.DirectSound:
          arguments.Append("-ao dsound:device=");
          arguments.Append(_soundOutputDevice);
          arguments.Append(" ");
          break;
      }
      switch (_deinterlace)
      {
        case Deinterlace.Simple:
          arguments.Append("-vf-add lavcdeint ");
          break;
        case Deinterlace.Adaptive:
          arguments.Append("-vf-add kerndeint ");
          break;
      }
      switch (_postProcessing)
      {
        case PostProcessing.Automatic:
          arguments.Append("-autoq 10 -vf-add pp ");
          break;
        case PostProcessing.Maximum:
          arguments.Append("-vf-add pp=hb/vb/dr ");
          break;
      }
      switch (_aspectRatio)
      {
        case AspectRatio.m_4x3:
          arguments.Append(" -aspect 4:3 ");
          break;
        case AspectRatio.m_16x9:
          arguments.Append(" -aspect 16:9 ");
          break;
        case AspectRatio.m_2_35:
          arguments.Append(" -aspect 2.35 ");
          break;
      }
      switch (_audioChannels)
      {
        case AudioChannels.Stereo:
          arguments.Append(" -channels 2 ");
          break;
        case AudioChannels.Surround:
          arguments.Append(" -channels 4 ");
          break;
        case AudioChannels.Full_5_1:
          arguments.Append(" -channels 6 ");
          break;
      }
      switch (_noiseDenoise)
      {
        case NoiseDenoise.Noise:
          arguments.Append(" -vf-add noise=05h:05h ");
          break;
        case NoiseDenoise.HighQualityDenoise:
          arguments.Append(" -vf-add hqdn3d ");
          break;
        case NoiseDenoise.Denoise:
          arguments.Append(" -vf-add denoise3d ");
          break;
      }
      switch (_videoOutputDriver)
      {
        case VideoOutputDriver.DirectX:
          arguments.Append(" -vo directx ");
          break;
        case VideoOutputDriver.DirectXNoAccel:
          arguments.Append(" -vo directx:noaccel ");
          break;
        case VideoOutputDriver.OpenGL:
          arguments.Append(" -vo gl");
          break;
        case VideoOutputDriver.OpenGL2:
          arguments.Append(" -vo gl2 ");
          break;
      }
      arguments.Append(_extensionSettings["general"].Arguments);
      arguments.Append(" ");
      return arguments;
    }

    /// <summary>
    /// Check if the current screen is the primary screen or we have to add and extra argument
    /// </summary>
    /// <returns>Additional argument for a different screen</returns>
    private String GetScreenArguments()
    {
      Screen currentScreen = GUIGraphicsContext.currentScreen;
      if (currentScreen.Primary)
      {
        return String.Empty;
      }
      Screen[] allScreens = Screen.AllScreens;
      int i;
      for (i = 0; i < allScreens.Length; i++)
      {
        if (allScreens[i] == currentScreen)
        {
          break;
        }
      }
      Log.Debug("MPlayer: Detected different screen. Number: " + i);
      i++;
      return " -adapter " + i;
    }
    #endregion

    #region public methods
    /// <summary>
    /// Checks if the fileName has a video or not
    /// </summary>
    /// <param _name="fileName">Filename to check</param>
    /// <returns>true, when file or stream has a video</returns>
    public bool HasFileOrStreamVideo(String fileName)
    {
      bool isVideo = false;
      if (fileName.StartsWith("dvd://"))
      {
        isVideo = true;
      }
      else if (fileName.StartsWith("vcd://"))
      {
        isVideo = true;
      }
      else if (fileName.StartsWith("svcd://"))
      {
        isVideo = true;
      }
      else if (fileName.StartsWith("cue://") ||
               fileName.StartsWith("ftp://") ||
               fileName.StartsWith("http://") ||
               fileName.StartsWith("http_proxy://") ||
               fileName.StartsWith("mms://") ||
               fileName.StartsWith("mmst://") ||
               fileName.StartsWith("mpst://") ||
               fileName.StartsWith("rtp://") ||
               fileName.StartsWith("rtsp://") ||
               fileName.StartsWith("sdp://") ||
               fileName.StartsWith("udp://") ||
               fileName.StartsWith("unsv://"))
      {
        if (_extensionSettings.ContainsKey(System.IO.Path.GetExtension(fileName)))
        {
          isVideo = _extensionSettings[System.IO.Path.GetExtension(fileName)].PlayMode == PlayMode.Video;
        }
        else
        {
          isVideo = true;
        }
      }
      else if (fileName.EndsWith(".cda"))
      {
        isVideo = false;
      }
      else
      {
        ExtensionSettings extSettings = _extensionSettings[System.IO.Path.GetExtension(fileName).Trim().ToLower()];
        isVideo = (extSettings.PlayMode == PlayMode.Video);
      }
      return isVideo;
    }

    /// <summary>
    /// Creates a process object and sets the starting _arguments
    /// </summary>
    /// <param _name="fileName">FileName that should be started</param>
    /// <param _name="handle">Handle of inner panel</param>
    /// <returns>UpdateGUI object</returns>
    public Process CreateProcessForFileName(String fileName, IntPtr handle)
    {
      Process mplayerProcess = new Process();
      mplayerProcess.StartInfo.UseShellExecute = false;
      mplayerProcess.StartInfo.RedirectStandardInput = true;
      mplayerProcess.StartInfo.RedirectStandardOutput = true;
      mplayerProcess.StartInfo.WorkingDirectory = System.IO.Path.GetDirectoryName(_mplayerPath);
      mplayerProcess.StartInfo.FileName = _mplayerPath;
      StringBuilder arguments = GetGeneralArguments(handle);
      if (fileName.StartsWith("dvd://"))
      {
        String file = fileName.Substring(6);
        arguments.Append(_extensionSettings["dvd://"].Arguments);
        arguments.Append(" -dvd-device \"");
        arguments.Append(file);
        arguments.Append("\"");
        arguments.Append(" -alang ");
        arguments.Append(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
        arguments.Append(" -slang ");
        arguments.Append(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
        arguments.Append(" dvd://");
      }
      else if (fileName.StartsWith("vcd://"))
      {
        String file = fileName.Substring(6);
        arguments.Append(_extensionSettings["vcd://"].Arguments);
        arguments.Append(" \"vcd://");
        arguments.Append(file);
        arguments.Append("\" ");
      }
      else if (fileName.StartsWith("svcd://"))
      {
        String file = fileName.Substring(7);
        arguments.Append(_extensionSettings["svcd://"].Arguments);
        arguments.Append(" \"vcd://");
        arguments.Append(file);
        arguments.Append("\" ");
      }
      else if (fileName.StartsWith("cue://") ||
               fileName.StartsWith("ftp://") ||
               fileName.StartsWith("http://") ||
               fileName.StartsWith("http_proxy://") ||
               fileName.StartsWith("mms://") ||
               fileName.StartsWith("mmst://") ||
               fileName.StartsWith("mpst://") ||
               fileName.StartsWith("rtp://") ||
               fileName.StartsWith("rtsp://") ||
               fileName.StartsWith("sdp://") ||
               fileName.StartsWith("udp://") ||
               fileName.StartsWith("unsv://"))
      {
        int index = fileName.IndexOf("://");
        String file = fileName.Substring(index + 3);
        String protocol = fileName.Substring(0, index + +3).ToLower();
        Log.Info("MPlayer: StreamProtocol: " + protocol);
        Log.Info("MPlayer: StremFilename: " + file);
        arguments.Append(_extensionSettings[protocol].Arguments);
        if (_extensionSettings.ContainsKey(System.IO.Path.GetExtension(fileName)))
        {
          arguments.Append(_extensionSettings[System.IO.Path.GetExtension(fileName)].Arguments);
        }
        arguments.Append(" \"");
        arguments.Append(protocol);
        arguments.Append(file);
        arguments.Append("\" ");
      }
      else if (fileName.EndsWith(".cda"))
      {
        String drive = System.IO.Path.GetDirectoryName(fileName);
        if (drive.EndsWith("\\"))
        {
          drive = drive.Substring(0, drive.Length - 1);
        }
        String trackNumber = System.IO.Path.GetFileNameWithoutExtension(fileName).Substring(5);
        arguments.Append(_extensionSettings[".cda"].Arguments);
        arguments.Append(" cdda://");
        arguments.Append(trackNumber);
        arguments.Append(" -cdrom-device ");
        arguments.Append(drive);
        arguments.Append(" ");
      }
      else
      {
        ExtensionSettings extSettings = _extensionSettings[System.IO.Path.GetExtension(fileName).Trim().ToLower()];
        arguments.Append(extSettings.Arguments);
        arguments.Append(" \"");
        arguments.Append(fileName);
        arguments.Append("\" ");
      }
      arguments.Append(GetScreenArguments());
      Log.Info("MPlayer: All Arguments: " + arguments);
      mplayerProcess.StartInfo.Arguments = arguments.ToString();
      mplayerProcess.StartInfo.CreateNoWindow = true;
      mplayerProcess.EnableRaisingEvents = true;
      return mplayerProcess;
    }

    /// <summary>
    /// Method which specifiy, if the player supports the file
    /// </summary>
    /// <param _name="filename">Filename</param>
    /// <returns>true, if the player can play this file</returns>
    public bool SupportsFile(String filename)
    {
      string ext = null;
      int dot = filename.LastIndexOf(".");    // couldn't find the dot to get the extension
      if (dot == -1)
        return false;

      ext = filename.Substring(dot).Trim();
      if (ext.Length == 0)
        return false;   // no extension so return false;

      ext = ext.ToLower();
      if (ext.Equals(".mplayer"))
      {
        if (filename.StartsWith("dvd://") || filename.StartsWith("vcd://") || filename.StartsWith("svcd://") ||
            filename.StartsWith("ftp://") || filename.StartsWith("http://") || filename.StartsWith("http_proxy://") ||
            filename.StartsWith("mms://") || filename.StartsWith("mmst://") || filename.StartsWith("mpst://") ||
            filename.StartsWith("rtp://") || filename.StartsWith("rtsp://") || filename.StartsWith("sdp://") ||
            filename.StartsWith("udp://") || filename.StartsWith("unsv://") || filename.StartsWith("ZZZZ://"))
        {
          return true;
        }
        filename = filename.Remove(filename.LastIndexOf(".mplayer"));
        dot = filename.LastIndexOf(".");    // couldn't find the dot to get the real extension
        if (dot == -1)
          return false;

        ext = filename.Substring(dot).Trim().ToLower();
        if (ext.Length == 0)
          return false;   // no real extension so return false;
        if (_extensionSettings.ContainsKey(ext))
        {
          return true;
        }
      }
      if (_extensionSettingsExtPlayer.ContainsKey(ext))
      {
        return true;
      }
      // could not match the extension, so return false;
      return false;
    }
    #endregion

    #region properties
    /// <summary>
    /// Installed Fonts that can be used
    /// </summary>
    public String[] PossibleFonts
    {
      get { return _fontsCollection.ToArray(); }
    }

    /// <summary>
    /// Supported Extensions of MPlayer plugin
    /// </summary>
    public String[] SupportedExtensions
    {
      get { return _supportedExtensions; }
    }

    /// <summary>
    /// Timeout before a seek step is performed
    /// </summary>
    public int SeekStepTimeout
    {
      get { return _seekStepTimeout; }
    }
    #endregion

  }
}
