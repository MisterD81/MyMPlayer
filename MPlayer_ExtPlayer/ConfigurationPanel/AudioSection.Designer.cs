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

namespace MPlayer.ConfigurationPanel
{
  partial class AudioSection
  {
    /// <summary> 
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param _name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Komponenten-Designer generierter Code

    /// <summary> 
    /// Erforderliche Methode für die Designerunterstützung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioSection));
      this.audioDelayStep = new System.Windows.Forms.NumericUpDown();
      this.label32 = new System.Windows.Forms.Label();
      this.label29 = new System.Windows.Forms.Label();
      this.soundOutputDevice = new System.Windows.Forms.ComboBox();
      this.passthroughAC3_DTS = new System.Windows.Forms.CheckBox();
      this.audioNormalize = new System.Windows.Forms.CheckBox();
      this.label7 = new System.Windows.Forms.Label();
      this.audioChannels = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.soundOutputDriver = new System.Windows.Forms.ComboBox();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.actionInfoLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.audioDelayStep)).BeginInit();
      this.SuspendLayout();
      // 
      // audioDelayStep
      // 
      this.audioDelayStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.audioDelayStep.Location = new System.Drawing.Point(138, 96);
      this.audioDelayStep.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.audioDelayStep.Name = "audioDelayStep";
      this.audioDelayStep.Size = new System.Drawing.Size(251, 20);
      this.audioDelayStep.TabIndex = 69;
      this.toolTip.SetToolTip(this.audioDelayStep, "With this option you could define in which steps\r\nthe audio delay will be changed" +
              " during playback");
      this.audioDelayStep.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
      // 
      // label32
      // 
      this.label32.AutoSize = true;
      this.label32.Location = new System.Drawing.Point(6, 98);
      this.label32.Name = "label32";
      this.label32.Size = new System.Drawing.Size(126, 13);
      this.label32.TabIndex = 68;
      this.label32.Text = "Audio Delay Step (msec):";
      this.toolTip.SetToolTip(this.label32, "With this option you could define in which steps\r\nthe audio delay will be changed" +
              " during playback");
      // 
      // label29
      // 
      this.label29.AutoSize = true;
      this.label29.Location = new System.Drawing.Point(6, 45);
      this.label29.Name = "label29";
      this.label29.Size = new System.Drawing.Size(109, 13);
      this.label29.TabIndex = 67;
      this.label29.Text = "Sound output device:";
      this.toolTip.SetToolTip(this.label29, "Defines the selected output device.\r\nThis can only be choosen, if the direct soun" +
              "d output\r\ndriver is selected.\r\n");
      // 
      // soundOutputDevice
      // 
      this.soundOutputDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.soundOutputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.soundOutputDevice.FormattingEnabled = true;
      this.soundOutputDevice.Items.AddRange(new object[] {
            "(don\'t decode sound)",
            "(don\'t play sound)",
            "Win32",
            "DirectSound"});
      this.soundOutputDevice.Location = new System.Drawing.Point(138, 42);
      this.soundOutputDevice.Name = "soundOutputDevice";
      this.soundOutputDevice.Size = new System.Drawing.Size(251, 21);
      this.soundOutputDevice.TabIndex = 66;
      this.toolTip.SetToolTip(this.soundOutputDevice, "Defines the selected output device.\r\nThis can only be choosen, if the direct soun" +
              "d output\r\ndriver is selected.");
      // 
      // passthroughAC3_DTS
      // 
      this.passthroughAC3_DTS.AutoSize = true;
      this.passthroughAC3_DTS.Location = new System.Drawing.Point(9, 132);
      this.passthroughAC3_DTS.Name = "passthroughAC3_DTS";
      this.passthroughAC3_DTS.Size = new System.Drawing.Size(135, 17);
      this.passthroughAC3_DTS.TabIndex = 65;
      this.passthroughAC3_DTS.Text = "Passthrough AC3/DTS";
      this.toolTip.SetToolTip(this.passthroughAC3_DTS, "Indicates, if AC3/DTS should be passed through.\r\nIf you select this option and");
      this.passthroughAC3_DTS.UseVisualStyleBackColor = true;
      // 
      // audioNormalize
      // 
      this.audioNormalize.AutoSize = true;
      this.audioNormalize.Location = new System.Drawing.Point(9, 161);
      this.audioNormalize.Name = "audioNormalize";
      this.audioNormalize.Size = new System.Drawing.Size(108, 17);
      this.audioNormalize.TabIndex = 64;
      this.audioNormalize.Text = "Volume normalize";
      this.toolTip.SetToolTip(this.audioNormalize, "Indicates, if the volume should be normalized during playback.");
      this.audioNormalize.UseVisualStyleBackColor = true;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(6, 72);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(84, 13);
      this.label7.TabIndex = 63;
      this.label7.Text = "Audio Channels:";
      this.toolTip.SetToolTip(this.label7, "Defines how many audio channels will be decoded");
      // 
      // audioChannels
      // 
      this.audioChannels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.audioChannels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.audioChannels.FormattingEnabled = true;
      this.audioChannels.Items.AddRange(new object[] {
            "Default",
            "Stereo",
            "Surround",
            "Full 5.1"});
      this.audioChannels.Location = new System.Drawing.Point(138, 69);
      this.audioChannels.Name = "audioChannels";
      this.audioChannels.Size = new System.Drawing.Size(251, 21);
      this.audioChannels.TabIndex = 62;
      this.toolTip.SetToolTip(this.audioChannels, "Defines how many audio channels will be decoded");
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 18);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(103, 13);
      this.label1.TabIndex = 60;
      this.label1.Text = "Sound output driver:";
      this.toolTip.SetToolTip(this.label1, "Defines the sound output driver.\r\nThe direct sound output driver is recommend\r\n");
      // 
      // soundOutputDriver
      // 
      this.soundOutputDriver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.soundOutputDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.soundOutputDriver.FormattingEnabled = true;
      this.soundOutputDriver.Items.AddRange(new object[] {
            "(don\'t decode sound)",
            "(don\'t play sound)",
            "Win32",
            "DirectSound"});
      this.soundOutputDriver.Location = new System.Drawing.Point(138, 15);
      this.soundOutputDriver.Name = "soundOutputDriver";
      this.soundOutputDriver.Size = new System.Drawing.Size(251, 21);
      this.soundOutputDriver.TabIndex = 61;
      this.toolTip.SetToolTip(this.soundOutputDriver, "Defines the sound output driver.\r\nThe direct sound output driver is recommend");
      this.soundOutputDriver.SelectedIndexChanged += new System.EventHandler(this.soundOutputDriver_SelectedIndexChanged);
      // 
      // actionInfoLabel
      // 
      this.actionInfoLabel.AutoSize = true;
      this.actionInfoLabel.Location = new System.Drawing.Point(6, 205);
      this.actionInfoLabel.Name = "actionInfoLabel";
      this.actionInfoLabel.Size = new System.Drawing.Size(385, 52);
      this.actionInfoLabel.TabIndex = 83;
      this.actionInfoLabel.Text = resources.GetString("actionInfoLabel.Text");
      // 
      // AudioSection
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.actionInfoLabel);
      this.Controls.Add(this.audioDelayStep);
      this.Controls.Add(this.label32);
      this.Controls.Add(this.label29);
      this.Controls.Add(this.soundOutputDevice);
      this.Controls.Add(this.passthroughAC3_DTS);
      this.Controls.Add(this.audioNormalize);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.audioChannels);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.soundOutputDriver);
      this.Name = "AudioSection";
      this.Size = new System.Drawing.Size(403, 405);
      this.toolTip.SetToolTip(this, "Define the sound output driver.");
      ((System.ComponentModel.ISupportInitialize)(this.audioDelayStep)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.NumericUpDown audioDelayStep;
    private System.Windows.Forms.Label label32;
    private System.Windows.Forms.Label label29;
    private System.Windows.Forms.ComboBox soundOutputDevice;
    private System.Windows.Forms.CheckBox passthroughAC3_DTS;
    private System.Windows.Forms.CheckBox audioNormalize;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox audioChannels;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox soundOutputDriver;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.Label actionInfoLabel;
  }
}
