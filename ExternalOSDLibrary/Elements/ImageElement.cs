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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using MediaPortal.GUI.Library;

namespace ExternalOSDLibrary {
  /// <summary>
  /// This class represents a GUIImage
  /// </summary>
  public class ImageElement : BaseElement {
    #region variables
    /// <summary>
    /// GUIImage 
    /// </summary>
    private GUIImage _image;

    /// <summary>
    /// Image of this element
    /// </summary>
    private Bitmap _bitmap;
    #endregion

    #region ctor
    /// <summary>
    /// Creates the element and retrieves all information from the control
    /// </summary>
    /// <param name="control">GUIControl</param>
    public ImageElement(GUIControl control)
      : base(control) {
      _image = control as GUIImage;
      _bitmap = loadBitmap(_image.FileName);
      Log.Debug("VideoPlayerOSD: Found image element: " + _image.FileName);
    }
    #endregion

    #region implmenented abstract method
    /// <summary>
    /// Draws the element on the given graphics
    /// </summary>
    /// <param name="graph">Graphics</param>
    public override void DrawElement(Graphics graph) {
      if (_image.Visible && !_image.FileName.Equals("black.bmp")) {
        DrawElementAlternative(graph, GetImageRectangle());
      }
    }

    /// <summary>
    /// Disposes the object
    /// </summary>
    public override void Dispose() {
      if (_bitmap != null) {
        _bitmap.Dispose();
      }
    }

    /// <summary>
    /// Checks, if an update for the element is needed
    /// </summary>
    /// <returns>true, if an update is needed</returns>
    protected override bool CheckElementSpecificForUpdate() {
      return false;
    }

    #endregion

    #region public methods
    /// <summary>
    /// Gets the rectangle of the image
    /// </summary>
    /// <returns>Rectangle of the image</returns>
    public RectangleF GetImageRectangle() {
      return new RectangleF(_image.XPosition, _image.YPosition, _image.Width, _image.Height);
    }

    /// <summary>
    /// Draws the element for additional osd informations
    /// </summary>
    /// <param name="graph">Graphics</param>
    /// <param name="rectangle">Rectangle of the image</param>
    public void DrawElementAlternative(Graphics graph, RectangleF rectangle) {
      if (_bitmap != null) {
        graph.DrawImage(_bitmap, rectangle);
      }
    }
    #endregion

    #region public overrides methods
    /// <summary>
    /// Draws the element for the cache status. 
    /// </summary>
    /// <param name="graph">Graphics</param>
    /// <param name="cacheFill">Status of the cache</param>
    public override void DrawCacheStatus(Graphics graph, float cacheFill) {
      DrawElementAlternative(graph, GetImageRectangle());
    }
    #endregion
  }
}
