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

namespace MPlayer.ConfigurationPanel {
  partial class SubtitleSection {
    /// <summary> 
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Komponenten-Designer generierter Code

    /// <summary> 
    /// Erforderliche Methode für die Designerunterstützung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubtitleSection));
      this.label22 = new System.Windows.Forms.Label();
      this.subtitleFont = new System.Windows.Forms.ComboBox();
      this.subtitleDelayStep = new System.Windows.Forms.NumericUpDown();
      this.label34 = new System.Windows.Forms.Label();
      this.subtitlePosition = new System.Windows.Forms.NumericUpDown();
      this.label33 = new System.Windows.Forms.Label();
      this.subtitleSize = new System.Windows.Forms.NumericUpDown();
      this.label31 = new System.Windows.Forms.Label();
      this.subtitles = new System.Windows.Forms.CheckBox();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.actionInfoLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.subtitleDelayStep)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.subtitlePosition)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.subtitleSize)).BeginInit();
      this.SuspendLayout();
      // 
      // label22
      // 
      this.label22.AutoSize = true;
      this.label22.Location = new System.Drawing.Point(6, 18);
      this.label22.Name = "label22";
      this.label22.Size = new System.Drawing.Size(66, 13);
      this.label22.TabIndex = 81;
      this.label22.Text = "Subtitle font:";
      this.toolTip.SetToolTip(this.label22, "Defines the subtitle font, which will also\r\nbe used for the internal OSD.\r\n");
      // 
      // subtitleFont
      // 
      this.subtitleFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.subtitleFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.subtitleFont.FormattingEnabled = true;
      this.subtitleFont.Location = new System.Drawing.Point(146, 15);
      this.subtitleFont.Name = "subtitleFont";
      this.subtitleFont.Size = new System.Drawing.Size(243, 21);
      this.subtitleFont.TabIndex = 80;
      this.toolTip.SetToolTip(this.subtitleFont, "Defines the subtitle font, which will also\r\nbe used for the internal OSD.");
      // 
      // subtitleDelayStep
      // 
      this.subtitleDelayStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.subtitleDelayStep.Location = new System.Drawing.Point(146, 94);
      this.subtitleDelayStep.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
      this.subtitleDelayStep.Name = "subtitleDelayStep";
      this.subtitleDelayStep.Size = new System.Drawing.Size(243, 20);
      this.subtitleDelayStep.TabIndex = 79;
      this.toolTip.SetToolTip(this.subtitleDelayStep, "With this option you could define in which steps\r\nthe subtitle delay will be chan" +
              "ged during playback");
      this.subtitleDelayStep.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
      // 
      // label34
      // 
      this.label34.AutoSize = true;
      this.label34.Location = new System.Drawing.Point(6, 96);
      this.label34.Name = "label34";
      this.label34.Size = new System.Drawing.Size(134, 13);
      this.label34.TabIndex = 78;
      this.label34.Text = "Subtitle Delay Step (msec):";
      this.toolTip.SetToolTip(this.label34, "With this option you could define in which steps\r\nthe subtitle delay will be chan" +
              "ged during playback");
      // 
      // subtitlePosition
      // 
      this.subtitlePosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.subtitlePosition.Location = new System.Drawing.Point(146, 68);
      this.subtitlePosition.Name = "subtitlePosition";
      this.subtitlePosition.Size = new System.Drawing.Size(243, 20);
      this.subtitlePosition.TabIndex = 77;
      this.toolTip.SetToolTip(this.subtitlePosition, "Defines the position of the subtitles.\r\n0 = top and 100 = bottom");
      this.subtitlePosition.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
      // 
      // label33
      // 
      this.label33.AutoSize = true;
      this.label33.Location = new System.Drawing.Point(6, 70);
      this.label33.Name = "label33";
      this.label33.Size = new System.Drawing.Size(85, 13);
      this.label33.TabIndex = 76;
      this.label33.Text = "Subtitle Position:";
      this.toolTip.SetToolTip(this.label33, "Defines the position of the subtitles.\r\n0 = top and 100 = bottom\r\n");
      // 
      // subtitleSize
      // 
      this.subtitleSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.subtitleSize.Location = new System.Drawing.Point(146, 42);
      this.subtitleSize.Name = "subtitleSize";
      this.subtitleSize.Size = new System.Drawing.Size(243, 20);
      this.subtitleSize.TabIndex = 75;
      this.toolTip.SetToolTip(this.subtitleSize, "Defines the size of the subtitles and the internal OSD.\r\nThe standard value of MP" +
              "layer is 5.");
      this.subtitleSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
      // 
      // label31
      // 
      this.label31.AutoSize = true;
      this.label31.Location = new System.Drawing.Point(6, 44);
      this.label31.Name = "label31";
      this.label31.Size = new System.Drawing.Size(68, 13);
      this.label31.TabIndex = 74;
      this.label31.Text = "Subtitle Size:";
      this.toolTip.SetToolTip(this.label31, "Defines the size of the subtitles and the internal OSD.\r\nThe standard value of MP" +
              "layer is 5.\r\n");
      // 
      // subtitles
      // 
      this.subtitles.AutoSize = true;
      this.subtitles.Location = new System.Drawing.Point(9, 129);
      this.subtitles.Name = "subtitles";
      this.subtitles.Size = new System.Drawing.Size(100, 17);
      this.subtitles.TabIndex = 73;
      this.subtitles.Text = "Enable subtitles";
      this.toolTip.SetToolTip(this.subtitles, "Indicates, if the subtitles should be enabled at startup.\r\nThe forced subtitles w" +
              "ill always be displayed.");
      this.subtitles.UseVisualStyleBackColor = true;
      // 
      // actionInfoLabel
      // 
      this.actionInfoLabel.AutoSize = true;
      this.actionInfoLabel.Location = new System.Drawing.Point(6, 173);
      this.actionInfoLabel.Name = "actionInfoLabel";
      this.actionInfoLabel.Size = new System.Drawing.Size(369, 104);
      this.actionInfoLabel.TabIndex = 82;
      this.actionInfoLabel.Text = resources.GetString("actionInfoLabel.Text");
      // 
      // SubtitleSection
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.actionInfoLabel);
      this.Controls.Add(this.label22);
      this.Controls.Add(this.subtitleFont);
      this.Controls.Add(this.subtitleDelayStep);
      this.Controls.Add(this.label34);
      this.Controls.Add(this.subtitlePosition);
      this.Controls.Add(this.label33);
      this.Controls.Add(this.subtitleSize);
      this.Controls.Add(this.label31);
      this.Controls.Add(this.subtitles);
      this.Name = "SubtitleSection";
      this.Size = new System.Drawing.Size(403, 405);
      ((System.ComponentModel.ISupportInitialize)(this.subtitleDelayStep)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.subtitlePosition)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.subtitleSize)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.ComboBox subtitleFont;
    private System.Windows.Forms.NumericUpDown subtitleDelayStep;
    private System.Windows.Forms.Label label34;
    private System.Windows.Forms.NumericUpDown subtitlePosition;
    private System.Windows.Forms.Label label33;
    private System.Windows.Forms.NumericUpDown subtitleSize;
    private System.Windows.Forms.Label label31;
    private System.Windows.Forms.CheckBox subtitles;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.Label actionInfoLabel;
  }
}
