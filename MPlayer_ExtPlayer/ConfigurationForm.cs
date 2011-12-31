#region Copyright (C) 2006-2012 MisterD

/* 
 *	Copyright (C) 2006-2012 MisterD
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
  /// Configuration Form for the External player plugin
  /// </summary>
  public partial class ConfigurationForm : MPConfigForm
  {

    #region ctor
    /// <summary>
    /// Standard Windows Form constructor
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
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void ConfigurationForm_Load(object sender, EventArgs e)
    {
      loadConfiguration();
    }

    /// <summary>
    /// Handles the OK-Button click event
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void okButton_Click(object sender, EventArgs e)
    {
      saveConfiguration();
      Close();
    }

    /// <summary>
    /// Handles the Cancel-Button click event
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event Arguments</param>
    private void cancelButton_Click(object sender, EventArgs e)
    {
      Close();
    }
    #endregion

    #region Configuration Methods
    /// <summary>
    /// Loads the whole configuration
    /// </summary>
    private void loadConfiguration()
    {
      generalSection1.LoadConfiguration();
      videoSection1.LoadConfiguration();
      subtitleSection1.LoadConfiguration();
      audioSection1.LoadConfiguration();
      extensionSection1.LoadConfiguration();
      streamSection1.LoadConfiguration();
    }

    /// <summary>
    /// Stores the whole configuration
    /// </summary>
    private void saveConfiguration()
    {
      generalSection1.SaveConfiguration();
      videoSection1.SaveConfiguration();
      subtitleSection1.SaveConfiguration();
      audioSection1.SaveConfiguration();
      extensionSection1.SaveConfiguration();
      streamSection1.SaveConfiguration();
    }
    #endregion


  }
}