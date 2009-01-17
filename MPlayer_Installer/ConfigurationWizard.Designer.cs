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

namespace MPlayer
{
  partial class ConfigurationWizard
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

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationWizard));
      this.generalSection1 = new MPlayer.ConfigurationPanel.GeneralSection();
      this.infoGroup = new System.Windows.Forms.GroupBox();
      this.infoBox = new System.Windows.Forms.Label();
      this.optionsGroup = new System.Windows.Forms.GroupBox();
      this.guiConfiguration1 = new MPlayer.ConfigurationPanel.GUIConfiguration();
      this.audioSection1 = new MPlayer.ConfigurationPanel.AudioSection();
      this.videoSection1 = new MPlayer.ConfigurationPanel.VideoSection();
      this.subtitleSection1 = new MPlayer.ConfigurationPanel.SubtitleSection();
      this.streamSection1 = new MPlayer.ConfigurationPanel.StreamSection();
      this.extensionSection1 = new MPlayer.ConfigurationPanel.ExtensionSection();
      this.finishButton = new System.Windows.Forms.Button();
      this.nextButton = new System.Windows.Forms.Button();
      this.backButton = new System.Windows.Forms.Button();
      this.mainGroup = new System.Windows.Forms.GroupBox();
      this.mainLabel = new System.Windows.Forms.Label();
      this.infoGroup.SuspendLayout();
      this.optionsGroup.SuspendLayout();
      this.mainGroup.SuspendLayout();
      this.SuspendLayout();
      // 
      // generalSection1
      // 
      this.generalSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.generalSection1.AutoSize = true;
      this.generalSection1.BackColor = System.Drawing.Color.Transparent;
      this.generalSection1.Location = new System.Drawing.Point(6, 16);
      this.generalSection1.Name = "generalSection1";
      this.generalSection1.Size = new System.Drawing.Size(403, 405);
      this.generalSection1.TabIndex = 0;
      // 
      // infoGroup
      // 
      this.infoGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoGroup.Controls.Add(this.infoBox);
      this.infoGroup.Location = new System.Drawing.Point(433, 83);
      this.infoGroup.Name = "infoGroup";
      this.infoGroup.Size = new System.Drawing.Size(246, 437);
      this.infoGroup.TabIndex = 3;
      this.infoGroup.TabStop = false;
      this.infoGroup.Text = "Informations";
      // 
      // infoBox
      // 
      this.infoBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoBox.BackColor = System.Drawing.SystemColors.Control;
      this.infoBox.Location = new System.Drawing.Point(6, 19);
      this.infoBox.Name = "infoBox";
      this.infoBox.Size = new System.Drawing.Size(234, 415);
      this.infoBox.TabIndex = 0;
      this.infoBox.Text = "Informations Richtext box element\nAdditional informations\n";
      // 
      // optionsGroup
      // 
      this.optionsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.optionsGroup.AutoSize = true;
      this.optionsGroup.Controls.Add(this.guiConfiguration1);
      this.optionsGroup.Controls.Add(this.audioSection1);
      this.optionsGroup.Controls.Add(this.videoSection1);
      this.optionsGroup.Controls.Add(this.generalSection1);
      this.optionsGroup.Controls.Add(this.subtitleSection1);
      this.optionsGroup.Controls.Add(this.streamSection1);
      this.optionsGroup.Controls.Add(this.extensionSection1);
      this.optionsGroup.Location = new System.Drawing.Point(12, 83);
      this.optionsGroup.Name = "optionsGroup";
      this.optionsGroup.Size = new System.Drawing.Size(415, 437);
      this.optionsGroup.TabIndex = 4;
      this.optionsGroup.TabStop = false;
      this.optionsGroup.Text = "Options";
      // 
      // guiConfiguration1
      // 
      this.guiConfiguration1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.guiConfiguration1.AutoSize = true;
      this.guiConfiguration1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.guiConfiguration1.BackColor = System.Drawing.Color.Transparent;
      this.guiConfiguration1.Location = new System.Drawing.Point(6, 16);
      this.guiConfiguration1.Name = "guiConfiguration1";
      this.guiConfiguration1.Size = new System.Drawing.Size(402, 378);
      this.guiConfiguration1.TabIndex = 6;
      // 
      // audioSection1
      // 
      this.audioSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.audioSection1.AutoSize = true;
      this.audioSection1.BackColor = System.Drawing.Color.Transparent;
      this.audioSection1.Location = new System.Drawing.Point(6, 16);
      this.audioSection1.Name = "audioSection1";
      this.audioSection1.Size = new System.Drawing.Size(403, 405);
      this.audioSection1.TabIndex = 2;
      // 
      // videoSection1
      // 
      this.videoSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.videoSection1.AutoSize = true;
      this.videoSection1.BackColor = System.Drawing.Color.Transparent;
      this.videoSection1.Location = new System.Drawing.Point(6, 16);
      this.videoSection1.Name = "videoSection1";
      this.videoSection1.Size = new System.Drawing.Size(403, 405);
      this.videoSection1.TabIndex = 1;
      // 
      // subtitleSection1
      // 
      this.subtitleSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.subtitleSection1.AutoSize = true;
      this.subtitleSection1.BackColor = System.Drawing.Color.Transparent;
      this.subtitleSection1.Location = new System.Drawing.Point(6, 16);
      this.subtitleSection1.Name = "subtitleSection1";
      this.subtitleSection1.Size = new System.Drawing.Size(403, 405);
      this.subtitleSection1.TabIndex = 5;
      // 
      // streamSection1
      // 
      this.streamSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.streamSection1.AutoSize = true;
      this.streamSection1.BackColor = System.Drawing.Color.Transparent;
      this.streamSection1.Location = new System.Drawing.Point(6, 16);
      this.streamSection1.Name = "streamSection1";
      this.streamSection1.Size = new System.Drawing.Size(403, 405);
      this.streamSection1.TabIndex = 4;
      // 
      // extensionSection1
      // 
      this.extensionSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.extensionSection1.AutoSize = true;
      this.extensionSection1.BackColor = System.Drawing.Color.Transparent;
      this.extensionSection1.Location = new System.Drawing.Point(6, 16);
      this.extensionSection1.Name = "extensionSection1";
      this.extensionSection1.Size = new System.Drawing.Size(403, 405);
      this.extensionSection1.TabIndex = 3;
      // 
      // finishButton
      // 
      this.finishButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.finishButton.AutoSize = true;
      this.finishButton.Location = new System.Drawing.Point(604, 526);
      this.finishButton.Name = "finishButton";
      this.finishButton.Size = new System.Drawing.Size(75, 23);
      this.finishButton.TabIndex = 5;
      this.finishButton.Text = "&Finish";
      this.finishButton.UseVisualStyleBackColor = true;
      this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
      // 
      // nextButton
      // 
      this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.nextButton.AutoSize = true;
      this.nextButton.Location = new System.Drawing.Point(523, 526);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(75, 23);
      this.nextButton.TabIndex = 6;
      this.nextButton.Text = "&Next";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // backButton
      // 
      this.backButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.backButton.AutoSize = true;
      this.backButton.Location = new System.Drawing.Point(442, 526);
      this.backButton.Name = "backButton";
      this.backButton.Size = new System.Drawing.Size(75, 23);
      this.backButton.TabIndex = 7;
      this.backButton.Text = "&Back";
      this.backButton.UseVisualStyleBackColor = true;
      this.backButton.Click += new System.EventHandler(this.backButton_Click);
      // 
      // mainGroup
      // 
      this.mainGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mainGroup.AutoSize = true;
      this.mainGroup.Controls.Add(this.mainLabel);
      this.mainGroup.Location = new System.Drawing.Point(12, 12);
      this.mainGroup.Name = "mainGroup";
      this.mainGroup.Size = new System.Drawing.Size(667, 65);
      this.mainGroup.TabIndex = 8;
      this.mainGroup.TabStop = false;
      // 
      // mainLabel
      // 
      this.mainLabel.AutoSize = true;
      this.mainLabel.Location = new System.Drawing.Point(6, 16);
      this.mainLabel.Name = "mainLabel";
      this.mainLabel.Size = new System.Drawing.Size(381, 13);
      this.mainLabel.TabIndex = 0;
      this.mainLabel.Text = "This configuration wizard will guide you through the configuration of My MPlayer";
      // 
      // ConfigurationWizard
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(691, 561);
      this.Controls.Add(this.optionsGroup);
      this.Controls.Add(this.mainGroup);
      this.Controls.Add(this.backButton);
      this.Controls.Add(this.nextButton);
      this.Controls.Add(this.finishButton);
      this.Controls.Add(this.infoGroup);
      this.Name = "ConfigurationWizard";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "My MPlayer Configuration Wizard (1/7)";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigurationWizard_FormClosing);
      this.infoGroup.ResumeLayout(false);
      this.optionsGroup.ResumeLayout(false);
      this.optionsGroup.PerformLayout();
      this.mainGroup.ResumeLayout(false);
      this.mainGroup.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private MPlayer.ConfigurationPanel.GeneralSection generalSection1;
    private System.Windows.Forms.GroupBox infoGroup;
    private System.Windows.Forms.GroupBox optionsGroup;
    private System.Windows.Forms.Button finishButton;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.Button backButton;
    private System.Windows.Forms.GroupBox mainGroup;
    private System.Windows.Forms.Label mainLabel;
    private MPlayer.ConfigurationPanel.VideoSection videoSection1;
    private MPlayer.ConfigurationPanel.SubtitleSection subtitleSection1;
    private MPlayer.ConfigurationPanel.StreamSection streamSection1;
    private MPlayer.ConfigurationPanel.ExtensionSection extensionSection1;
    private MPlayer.ConfigurationPanel.AudioSection audioSection1;
    private MPlayer.ConfigurationPanel.GUIConfiguration guiConfiguration1;
    private System.Windows.Forms.Label infoBox;
  }
}