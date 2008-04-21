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
using System.Text;

namespace MPlayer {

  #region enumeration
  /// <summary>
  /// Possible PlayMode of the player
  /// </summary>
  public enum PlayMode {
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
  public class ExtensionSettings {

    #region variables
    /// <summary>
    /// PlayMode of the extension
    /// </summary>
    private PlayMode playMode;

    /// <summary>
    /// Arguments for the extension
    /// </summary>
    private String arguments;

    /// <summary>
    /// Extension
    /// </summary>
    private String name;

    /// <summary>
    /// Use this extension in external player
    /// </summary>
    private bool extPlayerUse;
    #endregion

    #region ctor
    /// <summary>
    /// Constructor which only sets the playmode to unrecognized
    /// </summary>
    public ExtensionSettings() {
      playMode = PlayMode.Unrecognized;
    }

    /// <summary>
    /// Constructor which gets all data
    /// </summary>
    /// <param name="name">Extension</param>
    /// <param name="playMode">PlayMode</param>
    /// <param name="arguments">Arguments</param>
    /// <param name="extPlayerUse">Use in external player</param>
    public ExtensionSettings(String name, PlayMode playMode, String arguments, bool extPlayerUse) {
      this.name = name;
      this.playMode = playMode;
      this.arguments = arguments;
      this.extPlayerUse = extPlayerUse;
    }
    #endregion

    #region properties
    /// <summary>
    /// PlayMode of the Extension
    /// </summary>
    public PlayMode PlayMode {
      get { return playMode; }
      set { this.playMode = value; }
    }

    /// <summary>
    /// Arguments of the Externsion
    /// </summary>
    public String Arguments {
      get { return arguments; }
      set { this.arguments = value; }
    }

    /// <summary>
    /// Extension
    /// </summary>
    public String Name {
      get { return name; }
      set { this.name = value; }
    }

    /// <summary>
    /// Use this extension in external player
    /// </summary>
    public bool ExtPlayerUse {
      get { return extPlayerUse; }
      set { this.extPlayerUse = value; }
    }
    #endregion

    #region Overrides
    /// <summary>
    /// Prints the extension name
    /// </summary>
    /// <returns>Extension name</returns>
    public override string ToString() {
      return name;
    }

    /// <summary>
    /// Compares a given string with the name
    /// </summary>
    /// <param name="obj">Object to compare</param>
    /// <returns>true, if objects or by a given string the names are equal</returns>
    public override bool Equals(object obj) {
      String temp = obj as String;
      if (temp == null) {
        return base.Equals(obj);
      }
      return temp.Equals(name);
    }

    /// <summary>
    /// Gets the standard hashcode. Overridden to avoid compiler warning
    /// </summary>
    /// <returns>Hashcode of the object</returns>
    public override int GetHashCode() {
      return base.GetHashCode();
    }
    #endregion
  }
}
