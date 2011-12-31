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
using System.Drawing;
using MediaPortal.GUI.Library;

namespace ExternalOSDLibrary
{
  /// <summary>
  /// This class represents a GUIProgressControl
  /// </summary>
  public class ProgressControlElement : BaseElement
  {
    #region variables
    /// <summary>
    /// GUIProgressControl
    /// </summary>
    private readonly GUIProgressControl _progressControl;

    /// <summary>
    /// Left image
    /// </summary>
    private readonly Bitmap _leftBitmap;

    /// <summary>
    /// Middle image
    /// </summary>
    private readonly Bitmap _midBitmap;

    /// <summary>
    /// Right image
    /// </summary>
    private readonly Bitmap _rightBitmap;

    /// <summary>
    /// Background image
    /// </summary>
    private readonly Bitmap _backgroundBitmap;

    /// <summary>
    /// Percentage of the progress control
    /// </summary>
    private int _percentage;
    #endregion

    #region ctor
    /// <summary>
    /// Creates the element and retrieves all information from the control
    /// </summary>
    /// <param name="control">GUIControl</param>
    public ProgressControlElement(GUIControl control)
      : base(control)
    {
      _progressControl = control as GUIProgressControl;
      if (_progressControl != null)
      {
        _leftBitmap = LoadBitmap(_progressControl.BackTextureLeftName);
        _midBitmap = LoadBitmap(_progressControl.BackTextureMidName);
        _rightBitmap = LoadBitmap(_progressControl.BackTextureRightName);
        _backgroundBitmap = LoadBitmap(_progressControl.BackGroundTextureName);
        _percentage = GetPercentage();
      }
    }
    #endregion

    #region implmenented abstract method
    /// <summary>
    /// Draws the element on the given graphics
    /// </summary>
    /// <param name="graph">Graphics</param>
    public override void DrawElement(Graphics graph)
    {
      if (_wasVisible)
      {
        float fWidth = _percentage;
        DrawProgressBar(graph, fWidth, _percentage);
      }
    }

    /// <summary>
    /// Disposes the object
    /// </summary>
    public override void Dispose()
    {
    }

    /// <summary>
    /// Checks, if an update for the element is needed
    /// </summary>
    /// <returns>true, if an update is needed</returns>
    protected override bool CheckElementSpecificForUpdate()
    {
      bool result = false;
      int newPercentage = GetPercentage();
      if (newPercentage != _percentage)
      {
        _percentage = newPercentage;
        result = true;
      }
      return result;
    }
    #endregion

    #region public overrides methods
    /// <summary>
    /// Draws the element for the cache status.
    /// </summary>
    /// <param name="graph">Graphics</param>
    /// <param name="cacheFill">Status of the cache</param>
    public override void DrawCacheStatus(Graphics graph, float cacheFill)
    {
      _progressControl.Percentage = cacheFill;
      DrawProgressBar(graph, cacheFill, (int)cacheFill);
    }
    #endregion

    #region private methods
    /// <summary>
    /// Draws the progress bar with the given width and percentage
    /// </summary>
    /// <param name="graph">Graphics</param>
    /// <param name="fWidth">Width, depending on the percentage</param>
    /// <param name="percent">Percentage</param>
    private void DrawProgressBar(Graphics graph, float fWidth, int percent)
    {
      fWidth /= 100.0f;
      if (_backgroundBitmap != null)
      {
        graph.DrawImage(_backgroundBitmap, _progressControl.XPosition, _progressControl.YPosition, _progressControl.Width, _progressControl.Height);
      }
      int iWidthLeft = _leftBitmap != null ? _leftBitmap.Width:0;
      int iHeightLeft = _leftBitmap != null ? _leftBitmap.Height:0;
      int iHeightMid = _midBitmap != null ? _midBitmap.Height : 0;
      int iWidthRight = _rightBitmap != null ? _rightBitmap.Width:0;
      int iHeightRight = _rightBitmap != null ? _rightBitmap.Height : 0;
      GUIGraphicsContext.ScaleHorizontal(ref iWidthLeft);
      GUIGraphicsContext.ScaleHorizontal(ref iWidthRight);
      GUIGraphicsContext.ScaleVertical(ref iHeightLeft);
      GUIGraphicsContext.ScaleVertical(ref iHeightRight);
      //iHeight=20;
      int off = 12;
      GUIGraphicsContext.ScaleHorizontal(ref off);
      fWidth *= _progressControl.Width - 2 * off - iWidthLeft - iWidthRight;
      int iXPos = off + _progressControl.XPosition;

      int iYPos = _progressControl.YPosition + (_progressControl.Height - iHeightLeft) / 2;
      if (_leftBitmap != null)
      {
        graph.DrawImage(_leftBitmap, iXPos, iYPos, iWidthLeft, iHeightLeft);
      }
      iXPos += iWidthLeft;
      if (percent > 0 && (int)fWidth > 1)
      {
        if (_midBitmap != null)
        {
          iYPos = _progressControl.YPosition + (_progressControl.Height - iHeightMid) / 2;
          graph.DrawImage(_midBitmap, iXPos, iYPos, (int)Math.Abs(fWidth), iHeightMid);
        }
        iXPos += (int)fWidth;
      }
      if (_rightBitmap != null)
      {
        iYPos = _progressControl.YPosition + (_progressControl.Height - iHeightRight) / 2;
        graph.DrawImage(_rightBitmap, iXPos, iYPos, iWidthRight, iHeightRight);
      }
    }

    /// <summary>
    /// Calculates the percentage of the control
    /// </summary>
    /// <returns></returns>
    private int GetPercentage()
    {
      float percent;
      float.TryParse(GUIPropertyManager.Parse(_progressControl.Property), out percent);
      if (percent > 100)
        percent = 100;
      return (int)percent;
    }
    #endregion
  }
}
