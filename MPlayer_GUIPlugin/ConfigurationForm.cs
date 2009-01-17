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
using MediaPortal.UserInterface.Controls;

namespace MPlayer
{
  /// <summary>
  /// Configuration of the window plugin for MPlayer
  /// </summary>
  public partial class ConfigurationForm : MPConfigForm
  {

    #region ctor
    /// <summary>
    /// Standard constructor
    /// </summary>
    public ConfigurationForm()
    {
      InitializeComponent();
    }
    #endregion

    #region Event handling
    /// <summary>
    /// Handles the form load event
    /// </summary>
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event Arguments</param>
    private void ConfigurationForm_Load(object sender, EventArgs e)
    {
      guiConfiguration1.LoadConfiguration();
    }

    /// <summary>
    /// Handles the OK-Button click event
    /// </summary>
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event Arguments</param>
    private void okButton_Click(object sender, EventArgs e)
    {
      guiConfiguration1.SaveConfiguration();
      Close();
    }

    /// <summary>
    /// Handles the Cancel-Button click event
    /// </summary>
    /// <param _name="sender">Sender object</param>
    /// <param _name="e">Event Arguments</param>
    private void cancelButton_Click(object sender, EventArgs e)
    {
      Close();
    }
    #endregion

  }
}