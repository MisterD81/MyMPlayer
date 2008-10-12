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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using MediaPortal.GUI.Library;
using MediaPortal.GUI.Video;
using System.Windows;

namespace ExternalOSDLibrary
{
  /// <summary>
  /// This class is a windows form on which the osd is displayed
  /// </summary>
  public class OSDForm : Form
  {
    #region variables
    /// <summary>
    /// Event handler for the position changed event
    /// </summary>
    private EventHandler _positionChanged;

    /// <summary>
    /// Parent form (MP)
    /// </summary>
    private Form _parent;

    /// <summary>
    /// Image to be displayed
    /// </summary>
    private Bitmap _image;
    #endregion

    #region ctor
    /// <summary>
    /// Constructor, which sets the initial layout and registers the event handler
    /// </summary>
    public OSDForm()
    {
      this.SuspendLayout();
      _positionChanged = new EventHandler(parent_PositionChanged);
      _parent = GUIGraphicsContext.form;
      this.BackColor = System.Drawing.Color.FromArgb(1, 1, 1);
      this.ForeColor = System.Drawing.Color.FromArgb(1, 1, 1);
      this.TransparencyKey = this.BackColor;
      this.ControlBox = false;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.ResumeLayout();
      this.Opacity = 1;
      this.GotFocus += new EventHandler(OSDForm_GotFocus);
      _parent.LocationChanged += _positionChanged;
    }
    #endregion

    #region properties
    /// <summary>
    /// Gets/Sets the image, which should be displayed
    /// </summary>
    public Bitmap Image
    {
      get { return _image; }
      set { _image = value; }
    }
    #endregion

    #region private methods
    /// <summary>
    /// Action handler to prevent that the osd from got a focus
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Event arguments</param>
    private void OSDForm_GotFocus(object sender, EventArgs e)
    {
      _parent.Focus();
    }

    /// <summary>
    /// Event handler to adjust this form to the new location/size of the parent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void parent_PositionChanged(Object sender, EventArgs args)
    {
      this.Location = _parent.PointToScreen(new Point(0, 0));
      this.Size = _parent.ClientSize;
      this.BringToFront();
    }
    #endregion

    #region protected methods
    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      try
      {
        _parent.LocationChanged -= _positionChanged;
        _parent.SizeChanged -= _positionChanged;
      } catch (Exception ex)
      {
        Log.Error(ex);
      }
      base.Dispose(disposing);
    }

    /// <summary>
    /// Paints the image
    /// </summary>
    /// <param name="e">Event arguments</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      try
      {
        if (_image != null)
        {
          Graphics graph = e.Graphics;
          graph.SmoothingMode = SmoothingMode.AntiAlias;
          graph.DrawImage(_image, 0, 0, Size.Width, Size.Height);
        }
      } catch (Exception ex)
      {
        Log.Error(ex);
      }
    }
    #endregion

    #region public methods
    /// <summary>
    /// Shows the form correctly
    /// </summary>
    public void ShowForm()
    {
      this.Enabled = true;
      this.Show(_parent);
      parent_PositionChanged(null, null);
      this.BringToFront();
      _parent.Focus();
      this.Enabled = false;
    }
    #endregion
  }
}
