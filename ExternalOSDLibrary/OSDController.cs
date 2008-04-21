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
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;
using MediaPortal.GUI.Library;
using MediaPortal.Player;
using MediaPortal.Configuration;

namespace ExternalOSDLibrary {

  /// <summary>
  /// Controller for the ExternalOSDLibrary. This is the main entry point for usage in an external player
  /// </summary>
  public class OSDController : IDisposable{
    #region variables
    /// <summary>
    /// Singleton instance
    /// </summary>
    private static OSDController singleton;

    /// <summary>
    /// Fullscreen window
    /// </summary>
    private FullscreenWindow _fullscreenWindow;

    /// <summary>
    /// Video OSD window
    /// </summary>
    private VideoOSDWindow _videoOSDWindow;

    /// <summary>
    /// Dialog (Context) window
    /// </summary>
    private DialogWindow _dialogWindow;

    /// <summary>
    /// Form of the osd
    /// </summary>
    private OSDForm _osdForm;

    /// <summary>
    /// Second form of the osd
    /// </summary>
    private OSDForm _osdForm2;

    /// <summary>
    /// Indicates, if additional osd information is displayed
    /// </summary>
    private bool _showAdditionalOSD;

    /// <summary>
    /// Label of the additional osd
    /// </summary>
    private String _label;

    /// <summary>
    /// Strikeout the label of the addional osd
    /// </summary>
    private bool _strikeOut;

    /// <summary>
    /// Time of the last update
    /// </summary>
    private DateTime _lastUpdate;

    /// <summary>
    /// Satus of the cache
    /// </summary>
    private float _cacheFill;

    /// <summary>
    /// Indicates, if the cache status should be displayed
    /// </summary>
    private bool _showCacheStatus;

    /// <summary>
    /// Indicates, if the init label should be displayed
    /// </summary>
    private bool _showInit;

    /// <summary>
    /// Indicates, if an update is needed
    /// </summary>
    private bool _needUpdate;

    /// <summary>
    /// Event handler for the size changed event
    /// </summary>
    private EventHandler _sizeChanged;

    /// <summary>
    /// MP parent form
    /// </summary>
    private Form _parentForm;

    /// <summary>
    /// Indicates if MP is minimized
    /// </summary>
    private bool _minimized;

    /// <summary>
    /// Indicates if the screen should be blanked in fullscreen
    /// </summary>
    private bool _blankScreen;
    #endregion

    #region ctor
    /// <summary>
    /// Returns the singleton instance
    /// </summary>
    /// <returns>Singleton instance</returns>
    public static OSDController getInstance() {
      if (singleton == null) {
        singleton = new OSDController();
      }
      return singleton;
    }

    /// <summary>
    /// Constructor which initializes the osd controller
    /// </summary>
    private OSDController() {
      _fullscreenWindow = new FullscreenWindow();
      _videoOSDWindow = new VideoOSDWindow();
      _dialogWindow = new DialogWindow();
      _osdForm = new OSDForm();
      _osdForm2 = new OSDForm();
      _parentForm = GUIGraphicsContext.form;
      _sizeChanged = new EventHandler(parent_SizeChanged);
      _parentForm.SizeChanged += _sizeChanged;
      _minimized = false;
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml"))) {
        _blankScreen = xmlreader.GetValueAsBool("externalOSDLibrary", "blankScreen", true);
      }
    }
    #endregion

    #region public methods
    /// <summary>
    /// Activates the osd. This methods must be called first, otherwise nothing will displayed
    /// </summary>
    public void Activate() {
      _osdForm.ShowForm();
      _osdForm2.ShowForm();
      UpdateGUI();
    }

    /// <summary>
    /// Performs an update on the osd, should be called from the process method of the player
    /// </summary>
    public void UpdateGUI() {
      bool update = _needUpdate | _videoOSDWindow.CheckForUpdate() | _dialogWindow.CheckForUpdate() | _fullscreenWindow.CheckForUpdate();
      if(_needUpdate){
        _needUpdate=false;
      }else{
        if (_showAdditionalOSD) {
          TimeSpan ts = DateTime.Now - _lastUpdate;
          if (ts.Seconds >= 3){
            _showAdditionalOSD = false;
            update = true;
          }
        }
      }
      if (update) {
        Bitmap image = new Bitmap(_osdForm.Width, _osdForm.Height);
        Graphics graph = Graphics.FromImage(image);
        if (_blankScreen && GUIGraphicsContext.Fullscreen) {
          graph.FillRectangle(new SolidBrush(Color.FromArgb(0, 0, 0)), new Rectangle(0,0,_osdForm.Size.Width,_osdForm.Size.Height));
        }
        graph.TextRenderingHint = TextRenderingHint.AntiAlias;
        graph.SmoothingMode = SmoothingMode.AntiAlias;
        if (_showAdditionalOSD) {
            _fullscreenWindow.DrawAlternativeOSD(graph, _label, _strikeOut);
        }
        if (_showInit) {
          _fullscreenWindow.DrawAlternativeOSD(graph, _label, false);
        }
        if (_showCacheStatus) {
          _fullscreenWindow.DrawCacheStatus(graph, _cacheFill);
        }
        _fullscreenWindow.DrawWindow(graph);
        _videoOSDWindow.DrawWindow(graph);
        _dialogWindow.DrawWindow(graph);
        _osdForm.Image = image;
        _osdForm.Refresh();
        _osdForm2.Image = image;
        _osdForm2.Refresh();
      }
    }

    /// <summary>
    /// Deactivates the osd. Nothing will be displayed until it will be reactivated.
    /// </summary>
    public void Deactivate() {
      _osdForm.Hide();
      _osdForm2.Hide();
    }

    /// <summary>
    /// Indicates if the video osd is visible
    /// </summary>
    /// <returns></returns>
    public bool IsOSDVisible() {
      return _videoOSDWindow.CheckVisibility();
    }

    /// <summary>
    /// Shows additional osd information
    /// </summary>
    /// <param name="label">Label content</param>
    /// <param name="strikeOut">srikeout the label, if true</param>
    public void ShowAlternativeOSD(String label, bool strikeOut) {
      _label = label;
      _strikeOut = strikeOut;
      _lastUpdate = DateTime.Now;
      _showAdditionalOSD = true;
      _showCacheStatus = false;
      _needUpdate = true;
    }

    /// <summary>
    /// Shows the cache status
    /// </summary>
    /// <param name="cacheFill"></param>
    public void ShowCacheStatus(float cacheFill) {
      _cacheFill = cacheFill;
      _showAdditionalOSD = false;
      _showCacheStatus = true;
      _needUpdate = true;
    }

    /// <summary>
    /// Hides the cache status
    /// </summary>
    public void HideCacheStatus() {
      _showCacheStatus = false;
      _needUpdate = true;
    }

    /// <summary>
    /// Shows the init message
    /// </summary>
    /// <param name="label">Label of the init</param>
    public void ShowInit(String label) {
      _label = label;
      _showInit = true;
      _showCacheStatus = false;
      _showAdditionalOSD = false;
      _needUpdate = true;
    }

    /// <summary>
    /// Hides the init message
    /// </summary>
    public void HideInit() {
      _showInit = false;
      _needUpdate = true;
    }
    #endregion

    #region private methods
    /// <summary>
    /// Event handler to adjust this form to the new location/size of the parent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void parent_SizeChanged(Object sender, EventArgs args) {
      if (_parentForm.WindowState == FormWindowState.Minimized) {
        Log.Debug("MINIMIZING");
        _minimized = true;
        return;
      }
      if (!_minimized ) {
        Log.Debug("NOT MINIMIZED. DIPOSING");
        singleton.Dispose();
      }
      Log.Debug("RESET MINIMIZED");
      _minimized = false;
    }
    #endregion

    #region IDisposable Member
    /// <summary>
    /// Disposes the osd controller
    /// </summary>
    public void Dispose() {
      _fullscreenWindow.Dispose();
      _dialogWindow.Dispose();
      _videoOSDWindow.Dispose();
      _osdForm.Dispose();
      _osdForm2.Dispose();
      _parentForm.SizeChanged -= _sizeChanged;
      singleton = null;
    }
    #endregion
  }
}
