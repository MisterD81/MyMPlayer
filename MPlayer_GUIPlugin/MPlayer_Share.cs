#region Copyright (C) 2006-2009 MisterD

/* 
 *	Copyright (C) 2006-2009 MisterD
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
  /// <summary>
  /// Specific version of a share for the configuration dialog
  /// </summary>
  public class MPlayer_Share
  {
    #region variables
    /// <summary>
    /// Name of the share
    /// </summary>
    private String _name;

    /// <summary>
    /// Path of the share
    /// </summary>
    private String _path;
    #endregion

    #region Properties
    /// <summary>
    /// Gets/Sets the _name of the share
    /// </summary>
    public String Name
    {
      get { return _name; }
      set { _name = value; }
    }

    /// <summary>
    /// Gets/Sets the _path of the share
    /// </summary>
    public String Path
    {
      get { return _path; }
      set { _path = value; }
    }
    #endregion

    #region To-String method
    /// <summary>
    /// Returns the _name of the share
    /// </summary>
    /// <returns>Name of the share</returns>
    public override string ToString()
    {
      return _name;
    }
    #endregion
  }
}
