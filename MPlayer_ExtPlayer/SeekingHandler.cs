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
using System.Globalization;
using System.Threading;
using MediaPortal.GUI.Library;
using Action = MediaPortal.GUI.Library.Action;

namespace MPlayer
{
  /// <summary>
  /// This class handles all seeking relevant tasks for the MPlayer external player plugin
  /// </summary>
  internal class SeekingHandler : IMessageHandler
  {
    #region variables
    /// <summary>
    /// Playing _speed
    /// </summary>
    private int _speed;

    /// <summary>
    /// Length of the file
    /// </summary>
    private double _duration;

    /// <summary>
    /// Base Time for calculating the current time
    /// </summary>
    private double _baseTime;

    /// <summary>
    /// Additional time to the base time for the current playing time
    /// </summary>
    private DateTime _additionalTime;

    /// <summary>
    /// Current PlayTime
    /// </summary>
    private double _currentPosition;

    /// <summary>
    /// Relativ Seek Percentage
    /// </summary>
    private int _relativSeekPercentage;

    /// <summary>
    /// Reference to the main player component
    /// </summary>
    private readonly MPlayerExtPlayer _player;

    /// <summary>
    /// Instance of the current OSD Handler
    /// </summary>
    private readonly IOSDHandler _osdHandler;

    /// <summary>
    /// Indicates if an absolute seek should be performed
    /// </summary>
    private bool _performSeekRelative;

    /// <summary>
    /// Destination time of an absolute seek
    /// </summary>
    private double _seekAbsoluteDestinationTime;

    /// <summary>
    /// Indicates, if the current position should be checked
    /// </summary>
    private bool _checkTime;

    /// <summary>
    /// Indicates if a possible dvd menu was detected
    /// </summary>
    private bool _isDvdMenu;

    /// <summary>
    /// Stores the last stream pos
    /// </summary>
    private int _lastStreamPos;
    #endregion

    #region ctor
    /// <summary>
    /// Constructor which initialises the seeking handler
    /// </summary>
    /// <param name="player">Instance of external player</param>
    /// <param name="osdHandler">Instance of the osdHandler</param>
    public SeekingHandler(MPlayerExtPlayer player, IOSDHandler osdHandler)
    {
      _player = player;
      _osdHandler = osdHandler;
      _additionalTime = DateTime.Now;
      _baseTime = 0;
      _speed = 1;
      _performSeekRelative = false;
      _checkTime = false;
      _isDvdMenu = false;
      _lastStreamPos = -1;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Sets/Gets the current position in the file
    /// </summary>
    public double CurrentPosition
    {
      get
      {
        if (_player.PlayState == PlayState.Stopped)
        {
          return 0d;
        }
        if (_player.PlayState == PlayState.Paused)
        {
          return _currentPosition;
        }
        TimeSpan span = DateTime.Now - _additionalTime;
        _currentPosition = (span.TotalSeconds + _baseTime);
        return _currentPosition;
      }
    }

    /// <summary>
    /// Total length of the file
    /// </summary>
    public double Duration
    {
      get
      {
        return _duration;
      }
    }

    /// <summary>
    /// Gets/Sets the playing _speed of the file
    /// </summary>
    public int Speed
    {
      get
      {
        return _speed;
      }
      set
      {
        if (value > 0 && value != _speed)
        {
          _speed = value;
          _player.SendPausingKeepCommand("speed_set " + _speed);
          if (_speed > 9)
          {
            _osdHandler.ShowSpeedChanged("x  " + _speed + ".00");
          }
          else
          {
            _osdHandler.ShowSpeedChanged("x   " + _speed + ".00");
          }
        }
      }
    }

    /// <summary>
    /// Indicates if a dvd menu was detected
    /// </summary>
    public bool IsDVDMenu
    {
      get { return _isDvdMenu; }
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Returns that the player supports seeking
    /// </summary>
    /// <returns>true</returns>
    public bool CanSeek()
    {
      return _duration > 0;
    }

    /// <summary>
    /// Seek to an absoulte position in seconds
    /// </summary>
    /// <param name="dTime">New absoulte position in seconds</param>
    public void SeekAbsolute(double dTime)
    {
      _seekAbsoluteDestinationTime = dTime;
      _performSeekRelative = true;
      _player.SendPausingKeepCommand("get_time_pos");
      _player.SendPausingKeepCommand("get_time_length");
      _player.SendPausingKeepCommand("get_property stream_pos");
    }

    /// <summary>
    /// Seek to an relative position in seconds
    /// </summary>
    /// <param name="dTime">Relative position in seconds</param>
    public void SeekRelative(double dTime)
    {
      _player.SendPausingKeepCommand("seek " + ((int)dTime) + " 0");
      Thread.Sleep(200);
      _player.SendPausingKeepCommand("get_time_pos");
      _player.SendPausingKeepCommand("get_time_length");
      _player.SendPausingKeepCommand("get_property stream_pos");
    }

    /// <summary>
    /// Seek to an absoulte position in percentage
    /// </summary>
    /// <param name="iPercentage">New absoulte position in percentage</param>
    public void SeekAsolutePercentage(int iPercentage)
    {
      _player.SendPausingKeepCommand("seek " + iPercentage + " 2");
      Thread.Sleep(200);
      _player.SendPausingKeepCommand("get_time_pos");
      _player.SendPausingKeepCommand("get_time_length");
      _player.SendPausingKeepCommand("get_property stream_pos");
    }

    /// <summary>
    /// Seek to an relative position in percentage
    /// </summary>
    /// <param name="iPercentage">Relative position in percentage</param>
    public void SeekRelativePercentage(int iPercentage)
    {
      _player.SendPausingKeepCommand("get_percent_pos");
      _relativSeekPercentage = iPercentage;
    }

    /// <summary>
    /// Handles MP internal action related for the internal osd handler
    /// </summary>
    /// <param name="action">Action to handle</param>
    public void OnAction(Action action)
    {
      switch (action.wID)
      {
        case Action.ActionType.ACTION_NEXT_CHAPTER:
          _player.SendPausingKeepCommand("seek_chapter 1 0");
          break;
        case Action.ActionType.ACTION_PREV_CHAPTER:
          _player.SendPausingKeepCommand("seek_chapter -1 0");
          break;
      }
    }

    /// <summary>
    /// Checks the current position within the file
    /// </summary>
    public void CheckPosition()
    {
      if (_checkTime && !_player.Paused)
      {
        TimeSpan ts = DateTime.Now - _additionalTime;
        if (ts.TotalSeconds > 5)
        {
          _player.SendCommand("get_time_pos");
          _player.SendCommand("get_time_length");
          _player.SendPausingKeepCommand("get_property stream_pos");
          _checkTime = false;
        }
      }
    }
    #endregion

    #region Private methods
    private void PerformSeekRelative()
    {
      if (_performSeekRelative)
      {
        _performSeekRelative = false;
        double destination = _seekAbsoluteDestinationTime - _baseTime;
        _player.SendPausingKeepCommand("seek " + ((int)destination) + " 0");
        Thread.Sleep(200);
        _player.SendPausingKeepCommand("get_time_pos");
        _player.SendPausingKeepCommand("get_time_length");
        _player.SendPausingKeepCommand("get_property stream_pos");
      }
    }

    /// <summary>
    /// Performs the real seek to an relative position in percentage
    /// </summary>
    /// <param name="percentage">Relative position in percentage</param>
    private void PerformSeekRelativePercentage(int percentage)
    {
      _relativSeekPercentage += percentage;
      _player.SendPausingKeepCommand("seek " + _relativSeekPercentage + " 2");
      Thread.Sleep(200);
      _player.SendPausingKeepCommand("get_time_pos");
      _player.SendPausingKeepCommand("get_property stream_pos");
    }

    #endregion

    #region IMessageHandler Member
    /// <summary>
    /// Handles a message that is retrieved from the MPlayer process
    /// </summary>
    /// <param name="message">Message to handle</param>
    public void HandleMessage(string message)
    {
      if (message.StartsWith("ANS_PERCENT_POSITION="))
      {
        int tempValue;
        Int32.TryParse(message.Substring(21), out tempValue);
        PerformSeekRelativePercentage(tempValue);
      }
      else if (message.StartsWith("ANS_TIME_POSITION="))
      {
        _additionalTime = DateTime.Now;
        _checkTime = true;
        Double.TryParse(message.Substring(18).Replace(".",
        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out _baseTime);
        PerformSeekRelative();
      }
      else if (message.StartsWith("ANS_stream_pos=") && !_player.Paused && _player.IsDVD)
      {
        int newStreamPos;
        int.TryParse(message.Substring(15), out newStreamPos);
        if (newStreamPos != _lastStreamPos)
        {
          _lastStreamPos = newStreamPos;
          if (_isDvdMenu)
          {
            Log.Info("MPlayer: DVD Menu lost");
            _isDvdMenu = false;
            _player.SendPausingKeepCommand("get_time_length");
            _player.SendPausingKeepCommand("get_property stream_pos");
          }
        }
        else
        {
          if (!_isDvdMenu)
          {
            Log.Info("MPlayer: DVD Menu detected");
            _player.SendPausingKeepCommand("get_time_length");
            _player.SendPausingKeepCommand("get_property stream_pos");
            _isDvdMenu = true;
          }
        }
      }
      else if (message.StartsWith("ID_LENGTH"))
      {
        Double.TryParse(message.Substring(10).Replace(".",
        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out _duration);
      }
      else if (message.StartsWith("ANS_LENGTH"))
      {
        Double.TryParse(message.Substring(11).Replace(".",
        CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out _duration);
      }
    }
    #endregion
  }
}
