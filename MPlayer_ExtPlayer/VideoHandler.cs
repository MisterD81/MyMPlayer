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
using System.Windows.Forms;
using MediaPortal.GUI.Library;

namespace MPlayer {
  /// <summary>
  /// This class handles all video relevant tasks for the MPlayer external player plugin
  /// </summary>
  internal class VideoHandler : IDisposable, IMessageHandler {
    #region variables
    /// <summary>
    /// Outer panel for the video display
    /// </summary>
    private Panel _mplayerOuterPanel;

    /// <summary>
    /// Inner panel for the video display
    /// </summary>
    private Panel _mplayerInnerPanel;

    /// <summary>
    /// Background panel for the video display
    /// </summary>
    private Panel _mplayerBackgroundPanel;
    /// <summary>
    /// Position X of the video
    /// </summary>
    private int _positionX;

    /// <summary>
    /// Position Y of the video
    /// </summary>
    private int _positionY;

    /// <summary>
    /// Height of the video
    /// </summary>
    private int _renderHeight;

    /// <summary>
    /// Width of the video
    /// </summary>
    private int _renderWidth;

    /// <summary>
    /// Update needed on the video
    /// </summary>
    private bool _needUpdate;

    /// <summary>
    /// Is video fullscreen?
    /// </summary>
    private bool _isFullScreen;

    /// <summary>
    /// Is playing video
    /// </summary>
    private bool _isVideo;

    /// <summary>
    /// Contrast
    /// </summary>
    private int _contrast;

    /// <summary>
    /// Brightness
    /// </summary>
    private int _brightness;

    /// <summary>
    /// Gamma
    /// </summary>
    private int _gamma;

    /// <summary>
    /// Width of the video
    /// </summary>
    private int _videoWidth;

    /// <summary>
    /// Heigth of the video
    /// </summary>
    private int _videoHeight;

    /// <summary>
    /// Geometry for A/R
    /// </summary>
    private Geometry.Type _ar;

    /// <summary>
    /// Indicates, if the OpenGL second generation video output driver is used
    /// </summary>
    private bool _openGL;

    /// <summary>
    /// String representation, of the aspect ratio field. Needed for OpenGL and OpenGL2
    /// </summary>
    private String _aspect_ratio;

    /// <summary>
    /// Source Rectangle
    /// </summary>
    private Rectangle _sourceRectangle;

    /// <summary>
    /// Video Rectangle
    /// </summary>
    private Rectangle _videoRectangle;

    /// <summary>
    /// Reference to the main player component
    /// </summary>
    private MPlayer_ExtPlayer _player;

    /// <summary>
    /// Instance of the current OSD Handler
    /// </summary>
    private IOSDHandler _osdHandler;
    #endregion

    #region ctor
    /// <summary>
    /// Constructor which initialises the video handler
    /// </summary>
    /// <param name="player">Instance of external player</param>
    /// <param name="osdHandler">Instance of the osdHandler</param>
    public VideoHandler(MPlayer_ExtPlayer player, IOSDHandler osdHandler) {
      _player = player;
      _osdHandler = osdHandler;
      _aspect_ratio = "1.0";
      _videoHeight = -1;
      _videoWidth = 0;
      _ar = GUIGraphicsContext.ARType;
      _mplayerBackgroundPanel = new Panel();
      _mplayerBackgroundPanel.BackColor = Color.Black;
      _mplayerBackgroundPanel.Size = new System.Drawing.Size(0, 0);
      _mplayerBackgroundPanel.Location = new Point(0, 0);
      _mplayerOuterPanel = new Panel();
      _mplayerOuterPanel.BackColor = Color.Black;
      _mplayerOuterPanel.Size = new System.Drawing.Size(0, 0);
      _mplayerOuterPanel.Location = new Point(0, 0);
      _mplayerInnerPanel = new Panel();
      _mplayerInnerPanel.Size = new System.Drawing.Size(0, 0);
      _mplayerInnerPanel.BackColor = Color.FromArgb(16, 16, 16);
      _mplayerOuterPanel.Controls.Add(_mplayerInnerPanel);
      _mplayerInnerPanel.Location = new Point(0, 0);
    }

    /// <summary>
    /// Disposes the video handler
    /// </summary>
    public void Dispose() {
      _mplayerInnerPanel.Dispose();
      _mplayerInnerPanel = null;
      _mplayerOuterPanel.Dispose();
      _mplayerOuterPanel = null;
      _mplayerBackgroundPanel.Dispose();
      _mplayerBackgroundPanel = null;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Has the file a video stream
    /// </summary>
    public bool HasVideo {
      get {
        return _isVideo;
      }
      set {
        _isVideo = value;
      }
    }

    /// <summary>
    /// Gets/Sets the x position of the video window
    /// </summary>
    public int PositionX {
      get {
        return _positionX;
      }
      set {
        if (value != _positionX) {
          _positionX = value;
          _needUpdate = true;
        }
      }
    }

    /// <summary>
    /// Gets/Sets the y position of the video window
    /// </summary>
    public int PositionY {
      get {
        return _positionY;
      }
      set {
        if (value != _positionY) {
          _positionY = value;
          _needUpdate = true;
        }
      }
    }

    /// <summary>
    /// Gets/Sets the width of the video window
    /// </summary>
    public int RenderWidth {
      get {
        return _renderWidth;
      }
      set {
        if (value != _renderWidth) {
          _renderWidth = value;
          _needUpdate = true;
        }
      }
    }

    /// <summary>
    /// Gets/Sets the height of the video window
    /// </summary>
    public int RenderHeight {
      get {
        return _renderHeight;
      }
      set {
        if (value != _renderHeight) {
          _renderHeight = value;
          _needUpdate = true;
        }
      }
    }

    /// <summary>
    /// Gets/Sets the width of the video
    /// </summary>
    public int Width {
      get {
        return _videoWidth;
      }
      set {
        this._videoWidth = value;
      }
    }

    /// <summary>
    /// Gets/Sets the height of the video
    /// </summary>
    public int Height {
      get {
        return _videoHeight;
      }
      set {
        this._videoHeight = value;
      }
    }

    /// <summary>
    /// Gets/Sets if the video should be as an fullscreen video
    /// </summary>
    public bool FullScreen {
      get {
        return _isFullScreen;
      }
      set {
        if (value != _isFullScreen) {
          _isFullScreen = value;
          _needUpdate = true;
        }
      }
    }

    /// <summary>
    /// Gets / Sets the aspect ratio
    /// </summary>
    public String AspectRatio {
      get {
        return _aspect_ratio;
      }
      set {
        _aspect_ratio = value;
      }
    }

    /// <summary>
    /// Gets/Sets the _contrast of the video window
    /// </summary>
    public int Contrast {
      get {
        return _contrast;
      }
      set {
        _contrast = value;
        int temp = (value * 2) - 100;
        _player.SendPausingKeepCommand("contrast " + temp + " 1");
      }
    }

    /// <summary>
    /// Gets/Sets the _brightness of the video window
    /// </summary>
    public int Brightness {
      get {
        return _brightness;
      }
      set {
        _brightness = value;
        int temp = (value * 2) - 100;
        _player.SendPausingKeepCommand("brightness " + temp + " 1");
      }
    }

    /// <summary>
    /// Gets/Sets the _gamma of the video window
    /// </summary>
    public int Gamma {
      get {
        return _gamma;
      }
      set {
        _gamma = value;
        int temp = (value * 2) - 100;
        _player.SendPausingKeepCommand("gamma " + temp + " 1");
      }
    }

    /// <summary>
    /// Gets A/R Geometry of the video window
    /// </summary>
    public MediaPortal.GUI.Library.Geometry.Type ARType {
      get {
        return GUIGraphicsContext.ARType;
      }
      set {
        if (_ar != value) {
          _ar = value;
          _osdHandler.ShowDisplayModeChanged(value.ToString());
          _needUpdate = true;
        }

      }
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Places the video window
    /// </summary>
    public void SetVideoWindow() {
      if (_videoHeight < 0) {
        return;
      }
      if (_mplayerBackgroundPanel == null) return;
      if (_mplayerOuterPanel == null) return;
      if (_mplayerInnerPanel == null) return;
      if (GUIGraphicsContext.IsFullScreenVideo != _isFullScreen) {
        _isFullScreen = GUIGraphicsContext.IsFullScreenVideo;
        _needUpdate = true;
      }
      if (!_needUpdate) return;
      _needUpdate = false;

      if (_isFullScreen) {
        _positionX = GUIGraphicsContext.OverScanTop;
        _positionY = GUIGraphicsContext.OverScanLeft;
        _renderWidth = GUIGraphicsContext.OverScanWidth;
        _renderHeight = GUIGraphicsContext.OverScanHeight;

      }
      _mplayerBackgroundPanel.Location = new Point(GUIGraphicsContext.OverScanTop, GUIGraphicsContext.OverScanLeft);
      _mplayerBackgroundPanel.ClientSize = new Size(GUIGraphicsContext.OverScanWidth, GUIGraphicsContext.OverScanHeight);
      _mplayerBackgroundPanel.Size = new Size(GUIGraphicsContext.OverScanWidth, GUIGraphicsContext.OverScanHeight);
      Geometry m_geometry = new MediaPortal.GUI.Library.Geometry();
      m_geometry.ImageWidth = _videoWidth;
      m_geometry.ImageHeight = _videoHeight;
      m_geometry.ScreenWidth = _renderWidth;
      m_geometry.ScreenHeight = _renderHeight;
      m_geometry.ARType = GUIGraphicsContext.ARType;
      m_geometry.PixelRatio = GUIGraphicsContext.PixelRatio;

      m_geometry.GetWindow(_videoWidth, _videoHeight, out _sourceRectangle, out _videoRectangle);

      _positionX += _videoRectangle.X;
      _positionY += _videoRectangle.Y;
      _renderWidth = _videoRectangle.Width;
      _renderHeight = _videoRectangle.Height;
      _mplayerOuterPanel.Location = new Point(_positionX, _positionY);
      _mplayerOuterPanel.ClientSize = new Size(_renderWidth, _renderHeight);
      _mplayerOuterPanel.Size = new Size(_renderWidth, _renderHeight);

      int sourceY = (int)-(((double)_videoRectangle.Height) / _sourceRectangle.Height * _sourceRectangle.Top);
      int sourceHeight = (int)(((double)_videoRectangle.Height) / _sourceRectangle.Height * _videoHeight);
      int sourceX = (int)-(((double)_videoRectangle.Width) / _sourceRectangle.Width * _sourceRectangle.Left);
      int sourceWidth = (int)(((double)_videoRectangle.Width) / _sourceRectangle.Width * _videoWidth);
      _mplayerInnerPanel.Location = new Point(sourceX, sourceY);
      _mplayerInnerPanel.ClientSize = new Size(sourceWidth, sourceHeight);
      _mplayerInnerPanel.Size = new Size(sourceWidth, sourceHeight);
      if (_isFullScreen) {
        _mplayerBackgroundPanel.Visible = true;
        Log.Info("MPlayer: Fullscreen (Destination): (" + _positionX + "," + _positionY + "," + _renderWidth + "," + _renderHeight + ") : " + GUIGraphicsContext.ARType);
        Log.Info("MPlayer: Fullscreen (Source): (" + _sourceRectangle.X + "," + _sourceRectangle.Y + "," + _sourceRectangle.Width + "," + _sourceRectangle.Height + ") : " + GUIGraphicsContext.ARType);
        _osdHandler.ActivateOSD(false);
      } else {
        _mplayerBackgroundPanel.Visible = false;
        Log.Info("MPlayer: Video Window (Destination): (" + _positionX + "," + _positionY + "," + _renderWidth + "," + _renderHeight + ")");
        Log.Info("MPlayer: Video Window (Source): (" + _sourceRectangle.X + "," + _sourceRectangle.Y + "," + _sourceRectangle.Width + "," + _sourceRectangle.Height + ") : " + GUIGraphicsContext.ARType);
        _osdHandler.DeactivateOSD(true);
      }
      _mplayerOuterPanel.BringToFront();
      if (_openGL) {
        _player.SendPausingKeepCommand("switch_ratio " + _aspect_ratio);
      }
    }

    /// <summary>
    /// Returns the handle for the video window
    /// </summary>
    /// <returns>Handle of the video window</returns>
    public IntPtr GetVideoHandle() {
      return _mplayerInnerPanel.Handle;
    }

    /// <summary>
    /// Adss the video window to the mediaportal form
    /// </summary>
    public void AddVideoWindowToForm() {
      GUIGraphicsContext.form.SuspendLayout();
      GUIGraphicsContext.form.Controls.Add(_mplayerBackgroundPanel);
      GUIGraphicsContext.form.Controls.Add(_mplayerOuterPanel);
      GUIGraphicsContext.form.ResumeLayout();
    }

    /// <summary>
    /// Removes the video window from the mediaportal form
    /// </summary>
    public void RemoveVideoWindowToForm() {
      GUIGraphicsContext.form.SuspendLayout();
      GUIGraphicsContext.form.Controls.Remove(_mplayerBackgroundPanel);
      GUIGraphicsContext.form.Controls.Remove(_mplayerOuterPanel);
      GUIGraphicsContext.form.ResumeLayout();
    }
    #endregion

    #region IMessageHandler Member
    /// <summary>
    /// Handles a message that is retrieved from the MPlayer process
    /// </summary>
    /// <param name="message">Message to handle</param>
    public void HandleMessage(string message) {
      if (message.StartsWith("ID_VIDEO_ASPECT")) {
        _aspect_ratio = message.Substring(16);
        Log.Debug("MPlayer: Detected video aspect: " + _aspect_ratio);
      } else if (message.StartsWith("VO: [directx] ") ||
                 message.StartsWith("VO: [gl2] ") ||
                 message.StartsWith("VO: [gl] ")) {
        int pos = message.IndexOf("=> ");
        int newVideoWidth;
        int newVideoHeight;
        String temp = message.Substring(pos + 3);
        pos = temp.IndexOf('x');
        Int32.TryParse(temp.Substring(0, pos), out newVideoWidth);
        temp = temp.Substring(pos + 1);
        pos = temp.IndexOf(' ');
        Int32.TryParse(temp.Substring(0, pos), out newVideoHeight);
        if (newVideoWidth != _videoWidth || newVideoHeight != _videoHeight) {
          _openGL = message.StartsWith("VO: [gl2] ") || message.StartsWith("VO: [gl] ");
          if (_openGL) {
            Log.Debug("MPlayer: Using OpenGL or OpenGL2");
          }
          _player.SendPausingKeepCommand("get_time_pos");
          _osdHandler.DeactivateOSD(true);
          _videoWidth = newVideoWidth;
          _videoHeight = newVideoHeight;
          Log.Info("MPlayer: ASPECT: " + _videoWidth + "x" + _videoHeight);
          _needUpdate = true;
          SetVideoWindow();
        }
      }

    }
    #endregion
  }
}
