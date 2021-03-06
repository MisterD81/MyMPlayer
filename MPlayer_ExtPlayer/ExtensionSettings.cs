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

namespace MPlayer
{

  #region enumeration
  /// <summary>
  /// Possible PlayMode of the player
  /// </summary>
  public enum PlayMode
  {
    /// <summary>
    /// Video Playing
    /// </summary>
    Video = 0,
    /// <summary>
    /// Audio Playing
    /// </summary>
    Audio = 1,
    /// <summary>
    /// Unrecognized Format
    /// </summary>
    Unrecognized = 2
  }
  #endregion

  /// <summary>
  /// Settings for the extension
  /// </summary>
  public class ExtensionSettings
  {

    #region variables

    #endregion

    #region ctor
    /// <summary>
    /// Constructor which only sets the playmode to unrecognized
    /// </summary>
    public ExtensionSettings()
    {
      PlayMode = PlayMode.Unrecognized;
    }

    /// <summary>
    /// Constructor which gets all data
    /// </summary>
    /// <param name="name">Extension</param>
    /// <param name="playMode">PlayMode</param>
    /// <param name="arguments">Arguments</param>
    /// <param name="extPlayerUse">Use in external player</param>
    public ExtensionSettings(String name, PlayMode playMode, String arguments, bool extPlayerUse)
    {
      Name = name;
      PlayMode = playMode;
      Arguments = arguments;
      ExtPlayerUse = extPlayerUse;
    }
    #endregion

    #region properties

    /// <summary>
    /// PlayMode of the Extension
    /// </summary>
    public PlayMode PlayMode { get; set; }

    /// <summary>
    /// Arguments of the Externsion
    /// </summary>
    public string Arguments { get; set; }

    /// <summary>
    /// Extension
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Use this extension in external player
    /// </summary>
    public bool ExtPlayerUse { get; set; }

    #endregion

    #region Overrides
    /// <summary>
    /// Prints the extension _name
    /// </summary>
    /// <returns>Extension _name</returns>
    public override string ToString()
    {
      return Name;
    }

    /// <summary>
    /// Compares a given string with the _name
    /// </summary>
    /// <param name="obj">Object to compare</param>
    /// <returns>true, if objects or by a given string the names are equal</returns>
    public override bool Equals(object obj)
    {
      String temp = obj as String;
      if (temp == null)
      {
        return base.Equals(obj);
      }
      return temp.Equals(Name);
    }

    /// <summary>
    /// Gets the standard hashcode. Overridden to avoid compiler warning
    /// </summary>
    /// <returns>Hashcode of the object</returns>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
    #endregion
  }
}
