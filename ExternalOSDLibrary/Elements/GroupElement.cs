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
using System.Drawing.Imaging;
using System.Text;
using System.Windows;
using MediaPortal.GUI.Library;

namespace ExternalOSDLibrary {
  /// <summary>
  /// This class represents a GUIGroup
  /// </summary>
  public class GroupElement : BaseElement {
    #region variables
    /// <summary>
    /// GUICheckMarkControl
    /// </summary>
    private GUIGroup _group;

    /// <summary>
    /// List of all children elements
    /// </summary>
    private List<BaseElement> _childrens;

    #endregion

    #region ctor
    /// <summary>
    /// Creates the element and retrieves all information from the control
    /// </summary>
    /// <param name="control">GUIControl</param>
    public GroupElement(GUIControl control)
      : base(control) {
      _group = control as GUIGroup;
      UIElementCollection childrens = _group.Children;
      _childrens = new List<BaseElement>();
      GUIControl temp;
      BaseElement element;
      foreach (UIElement child in childrens) {
        temp = child as GUIControl;
        if (temp != null) {
          element = BaseWindow.GenerateElement(temp);
          if (element != null) {
            _childrens.Add(element);
          }
        }
      }
      Log.Debug("VideoPlayerOSD: Found group element: " + _group.GetID);
    }
    #endregion

    #region implmenented abstract method
    /// <summary>
    /// Draws the element on the given graphics
    /// </summary>
    /// <param name="graph">Graphics</param>
    public override void DrawElement(Graphics graph) {
      if (_group.Visible) {
        foreach (BaseElement child in _childrens) {
          child.DrawElement(graph);
        }
      }
    }

    /// <summary>
    /// Disposes the object
    /// </summary>
    public override void Dispose() {
      foreach (BaseElement child in _childrens) {
        child.Dispose();
      }
    }

    /// <summary>
    /// Checks, if an update for the element is needed
    /// </summary>
    /// <returns>true, if an update is needed</returns>
    protected override bool CheckElementSpecificForUpdate() {
      bool result = false;
      foreach (BaseElement child in _childrens) {
        result = result | child.CheckForUpdate();
      }
      return result;
    }
    #endregion
  }
}
