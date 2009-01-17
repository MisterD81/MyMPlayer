#region Copyright (C) 2005-2007 Team MediaPortal

/* 
 *	Copyright (C) 2005-2007 Team MediaPortal
 *	http://www.team-mediaportal.com
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
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using MediaPortal.GUI.Library;
using MediaPortal.Configuration;
using MediaPortal.Localisation;

namespace MPlayer
{
  /// <summary>
  /// Enumerations of all OSD Messages
  /// </summary>
  public enum LocalizedMessages
  {
    /// <summary>
    /// Speed
    /// </summary>
    Speed = 0,
    /// <summary>
    /// Audio
    /// </summary>
    Audio = 1,
    /// <summary>
    /// Subtitles
    /// </summary>
    Subtitles = 2,
    /// <summary>
    /// Enabled
    /// </summary>
    Enabled = 3,
    /// <summary>
    /// Disabled
    /// </summary>
    Disabled = 4,
    /// <summary>
    /// Seek
    /// </summary>
    Seek = 5,
    /// <summary>
    /// Jump To
    /// </summary>
    JumpTo = 6,
    /// <summary>
    /// Subtitle Position
    /// </summary>
    SubtitlePosition = 7,
    /// <summary>
    /// Subtitle Size
    /// </summary>
    SubtitleSize = 8,
    /// <summary>
    /// Subtitle Delay
    /// </summary>
    SubtitleDelay = 9,
    /// <summary>
    /// Audio Delay
    /// </summary>
    AudioDelay = 10,
    /// <summary>
    /// Display mode
    /// </summary>
    DisplayMode = 11,
    /// <summary>
    /// Mute
    /// </summary>
    Mute = 12,
    /// <summary>
    /// Volume
    /// </summary>
    Volume = 13,
    /// <summary>
    /// Play Stream button
    /// </summary>
    PlayStream = 14,
    /// <summary>
    /// Play Disc button
    /// </summary>
    PlayDisc = 15,
    /// <summary>
    /// Delete button
    /// </summary>
    Delete = 16
  }

  /// <summary>
  /// This class will hold all text used in the application
  /// The text is loaded for the current language from
  /// the file language/[language]/strings.xml
  /// </summary>
  public class LocalizeStrings
  {
    #region Variables
    static LocalisationProvider _stringProvider;
    static Dictionary<string, string> _cultures;
    static string[] _languages;
    #endregion

    #region Constructors/Destructors
    /// <summary>
    /// singleton. Dont allow any instance of this class
    /// </summary>
    private LocalizeStrings()
    {
    }

    /// <summary>
    /// Dispose
    /// </summary>
    static public void Dispose()
    {
      if (_stringProvider != null)
        _stringProvider.Dispose();
    }
    #endregion

    #region Public Methods
    /// <summary>
    /// Public method to load the text from a strings/xml file into memory
    /// </summary>
    /// <param _name="language">Language</param>
    /// <returns>
    /// true when text is loaded
    /// false when it was unable to load the text
    /// </returns>
    //[Obsolete("This method has changed", true)]
    static public bool Load(string language)
    {
      bool isPrefixEnabled;

      using (MediaPortal.Profile.Settings reader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
        isPrefixEnabled = reader.GetValueAsBool("general", "myprefix", true);

      string directory = Config.GetSubFolder(Config.Dir.Language, "MPlayer");
      string cultureName = null;
      if (language != null)
        cultureName = GetCultureName(language);

      Log.Info("MPlayer: Loading localised Strings - Path: {0} Culture: {1}  Language: {2} Prefix: {3}", directory, cultureName, language, isPrefixEnabled);

      _stringProvider = new LocalisationProvider(directory, cultureName, isPrefixEnabled);

      return true;
    }

    /// <summary>
    /// Retrieves the current language
    /// </summary>
    /// <returns></returns>
    static public string CurrentLanguage()
    {
      if (_stringProvider == null)
        Load(null);

      return _stringProvider.CurrentLanguage.EnglishName;
    }

    /// <summary>
    /// Changes the current lagnuage
    /// </summary>
    /// <param _name="language">New Language</param>
    static public void ChangeLanguage(string language)
    {
      if (_stringProvider == null)
        Load(language);
      else
        _stringProvider.ChangeLanguage(GetCultureName(language));
    }

    /// <summary>
    /// Get the translation for a given id and format the sting with
    /// the given parameters
    /// </summary>
    /// <param _name="dwCode">id of text</param>
    /// <param _name="parameters">parameters used in the formating</param>
    /// <returns>
    /// string containing the translated text
    /// </returns>
    static public string Get(int dwCode, object[] parameters)
    {
      if (_stringProvider == null)
        Load(null);

      string translation = _stringProvider.GetString("unmapped", dwCode);
      // if parameters or the translation is null, return the translation.
      if ((translation == null) || (parameters == null))
      {
        return translation;
      }
      // return the formatted string. If formatting fails, log the error
      // and return the unformatted string.
      try
      {
        return String.Format(translation, parameters);
      } catch (System.FormatException e)
      {
        Log.Error("Error formatting translation with id {0}", dwCode);
        Log.Error("Unformatted translation: {0}", translation);
        Log.Error(e);
        return translation;
      }
    }

    /// <summary>
    /// Get the translation for a given id
    /// </summary>
    /// <param _name="dwCode">id of text</param>
    /// <returns>
    /// string containing the translated text
    /// </returns>
    static public string Get(int dwCode)
    {
      if (_stringProvider == null)
        Load(null);

      string translation = _stringProvider.GetString("unmapped", dwCode);

      if (translation == null)
      {
        Log.Error("No translation found for id {0}", dwCode);
        return String.Empty;
      }

      return translation;
    }

    /// <summary>
    /// Localize a label
    /// </summary>
    /// <param _name="strLabel">Label</param>
    static public void LocalizeLabel(ref string strLabel)
    {
      if (_stringProvider == null)
        Load(null);

      if (strLabel == null)
        strLabel = String.Empty;
      if (strLabel == "-")
        strLabel = "";
      if (strLabel == "")
        return;
      // This can't be a valid string code if the first character isn't a number.
      // This check will save us from catching unnecessary exceptions.
      if (!char.IsNumber(strLabel, 0))
        return;

      int dwLabelID;

      try
      {
        dwLabelID = Int32.Parse(strLabel);
      } catch (FormatException e)
      {
        Log.Error(e);
        strLabel = String.Empty;
        return;
      }

      strLabel = _stringProvider.GetString("unmapped", dwLabelID);
      if (strLabel == null)
      {
        Log.Error("No translation found for id {0}", dwLabelID);
        strLabel = String.Empty;
      }
    }

    /// <summary>
    /// Retrieves if the local is supported
    /// </summary>
    /// <returns>Name of the supported local</returns>
    public static string LocalSupported()
    {
      if (_stringProvider == null)
        Load(null);

      CultureInfo culture = _stringProvider.GetBestLanguage();

      return culture.EnglishName;
    }

    /// <summary>
    /// Retrieves an array with the supported languages
    /// </summary>
    /// <returns>Array with the supported languages</returns>
    public static string[] SupportedLanguages()
    {
      if (_languages == null)
      {
        if (_stringProvider == null)
          Load(null);

        CultureInfo[] cultures = _stringProvider.AvailableLanguages();

        SortedList sortedLanguages = new SortedList();
        foreach (CultureInfo culture in cultures)
          sortedLanguages.Add(culture.EnglishName, culture.EnglishName);

        _languages = new string[sortedLanguages.Count];

        for (int i = 0; i < sortedLanguages.Count; i++)
        {
          _languages[i] = (string)sortedLanguages.GetByIndex(i);
        }
      }

      return _languages;
    }

    /// <summary>
    /// Retrieves the _name of the culture
    /// </summary>
    /// <param _name="language">Language</param>
    /// <returns>Culture</returns>
    static public string GetCultureName(string language)
    {
      if (_cultures == null)
      {
        _cultures = new Dictionary<string, string>();

        CultureInfo[] cultureList = CultureInfo.GetCultures(CultureTypes.AllCultures);

        for (int i = 0; i < cultureList.Length; i++)
        {
          _cultures.Add(cultureList[i].EnglishName, cultureList[i].Name);
        }
      }

      if (_cultures.ContainsKey(language))
        return _cultures[language];

      return null;
    }
    #endregion
  }
}
