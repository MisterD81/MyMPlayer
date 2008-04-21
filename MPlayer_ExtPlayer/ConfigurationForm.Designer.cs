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

namespace MPlayer {
  partial class ConfigurationForm {

    #region Dispose
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
    #endregion

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent() {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.general_Tab = new System.Windows.Forms.TabPage();
      this.video_Tab = new System.Windows.Forms.TabPage();
      this.subtitles_Tab = new System.Windows.Forms.TabPage();
      this.audio_Tab = new System.Windows.Forms.TabPage();
      this.extension_Tab = new System.Windows.Forms.TabPage();
      this.dvd_vcd_svcd_streams_Tab = new System.Windows.Forms.TabPage();
      this.fontDialog1 = new System.Windows.Forms.FontDialog();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.generalSection1 = new MPlayer.ConfigurationPanel.GeneralSection();
      this.videoSection1 = new MPlayer.ConfigurationPanel.VideoSection();
      this.subtitleSection1 = new MPlayer.ConfigurationPanel.SubtitleSection();
      this.audioSection1 = new MPlayer.ConfigurationPanel.AudioSection();
      this.extensionSection1 = new MPlayer.ConfigurationPanel.ExtensionSection();
      this.streamSection1 = new MPlayer.ConfigurationPanel.StreamSection();
      this.tabControl1.SuspendLayout();
      this.general_Tab.SuspendLayout();
      this.video_Tab.SuspendLayout();
      this.subtitles_Tab.SuspendLayout();
      this.audio_Tab.SuspendLayout();
      this.extension_Tab.SuspendLayout();
      this.dvd_vcd_svcd_streams_Tab.SuspendLayout();
      this.SuspendLayout();
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.okButton.AutoSize = true;
      this.okButton.Location = new System.Drawing.Point(25, 453);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 3;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.AutoSize = true;
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(332, 453);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 20;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // folderBrowserDialog1
      // 
      this.folderBrowserDialog1.Description = "Select the folger of MPlayer:";
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.general_Tab);
      this.tabControl1.Controls.Add(this.video_Tab);
      this.tabControl1.Controls.Add(this.subtitles_Tab);
      this.tabControl1.Controls.Add(this.audio_Tab);
      this.tabControl1.Controls.Add(this.extension_Tab);
      this.tabControl1.Controls.Add(this.dvd_vcd_svcd_streams_Tab);
      this.tabControl1.Location = new System.Drawing.Point(12, 12);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(412, 424);
      this.tabControl1.TabIndex = 24;
      // 
      // general_Tab
      // 
      this.general_Tab.Controls.Add(this.generalSection1);
      this.general_Tab.Location = new System.Drawing.Point(4, 22);
      this.general_Tab.Name = "general_Tab";
      this.general_Tab.Padding = new System.Windows.Forms.Padding(3);
      this.general_Tab.Size = new System.Drawing.Size(404, 398);
      this.general_Tab.TabIndex = 0;
      this.general_Tab.Text = "General";
      this.general_Tab.UseVisualStyleBackColor = true;
      // 
      // video_Tab
      // 
      this.video_Tab.Controls.Add(this.videoSection1);
      this.video_Tab.Location = new System.Drawing.Point(4, 22);
      this.video_Tab.Name = "video_Tab";
      this.video_Tab.Padding = new System.Windows.Forms.Padding(3);
      this.video_Tab.Size = new System.Drawing.Size(404, 398);
      this.video_Tab.TabIndex = 4;
      this.video_Tab.Text = "Video";
      this.video_Tab.UseVisualStyleBackColor = true;
      // 
      // subtitles_Tab
      // 
      this.subtitles_Tab.Controls.Add(this.subtitleSection1);
      this.subtitles_Tab.Location = new System.Drawing.Point(4, 22);
      this.subtitles_Tab.Name = "subtitles_Tab";
      this.subtitles_Tab.Size = new System.Drawing.Size(404, 398);
      this.subtitles_Tab.TabIndex = 6;
      this.subtitles_Tab.Text = "Subtitles";
      this.subtitles_Tab.UseVisualStyleBackColor = true;
      // 
      // audio_Tab
      // 
      this.audio_Tab.Controls.Add(this.audioSection1);
      this.audio_Tab.Location = new System.Drawing.Point(4, 22);
      this.audio_Tab.Name = "audio_Tab";
      this.audio_Tab.Size = new System.Drawing.Size(404, 398);
      this.audio_Tab.TabIndex = 5;
      this.audio_Tab.Text = "Audio";
      this.audio_Tab.UseVisualStyleBackColor = true;
      // 
      // extension_Tab
      // 
      this.extension_Tab.Controls.Add(this.extensionSection1);
      this.extension_Tab.Location = new System.Drawing.Point(4, 22);
      this.extension_Tab.Name = "extension_Tab";
      this.extension_Tab.Padding = new System.Windows.Forms.Padding(3);
      this.extension_Tab.Size = new System.Drawing.Size(404, 398);
      this.extension_Tab.TabIndex = 1;
      this.extension_Tab.Text = "Extensions";
      this.extension_Tab.UseVisualStyleBackColor = true;
      // 
      // dvd_vcd_svcd_streams_Tab
      // 
      this.dvd_vcd_svcd_streams_Tab.Controls.Add(this.streamSection1);
      this.dvd_vcd_svcd_streams_Tab.Location = new System.Drawing.Point(4, 22);
      this.dvd_vcd_svcd_streams_Tab.Name = "dvd_vcd_svcd_streams_Tab";
      this.dvd_vcd_svcd_streams_Tab.Size = new System.Drawing.Size(404, 398);
      this.dvd_vcd_svcd_streams_Tab.TabIndex = 3;
      this.dvd_vcd_svcd_streams_Tab.Text = "DVD/VCD/SVCD/Streams";
      this.dvd_vcd_svcd_streams_Tab.UseVisualStyleBackColor = true;
      // 
      // generalSection1
      // 
      this.generalSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.generalSection1.BackColor = System.Drawing.Color.Transparent;
      this.generalSection1.Location = new System.Drawing.Point(0, 0);
      this.generalSection1.Name = "generalSection1";
      this.generalSection1.Size = new System.Drawing.Size(404, 398);
      this.generalSection1.TabIndex = 0;
      // 
      // videoSection1
      // 
      this.videoSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.videoSection1.BackColor = System.Drawing.Color.Transparent;
      this.videoSection1.Location = new System.Drawing.Point(0, 0);
      this.videoSection1.Name = "videoSection1";
      this.videoSection1.Size = new System.Drawing.Size(395, 397);
      this.videoSection1.TabIndex = 0;
      // 
      // subtitleSection1
      // 
      this.subtitleSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.subtitleSection1.BackColor = System.Drawing.Color.Transparent;
      this.subtitleSection1.Location = new System.Drawing.Point(0, 0);
      this.subtitleSection1.Name = "subtitleSection1";
      this.subtitleSection1.Size = new System.Drawing.Size(395, 397);
      this.subtitleSection1.TabIndex = 0;
      // 
      // audioSection1
      // 
      this.audioSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.audioSection1.BackColor = System.Drawing.Color.Transparent;
      this.audioSection1.Location = new System.Drawing.Point(0, 0);
      this.audioSection1.Name = "audioSection1";
      this.audioSection1.Size = new System.Drawing.Size(395, 397);
      this.audioSection1.TabIndex = 0;
      // 
      // extensionSection1
      // 
      this.extensionSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.extensionSection1.BackColor = System.Drawing.Color.Transparent;
      this.extensionSection1.Location = new System.Drawing.Point(0, 0);
      this.extensionSection1.Name = "extensionSection1";
      this.extensionSection1.Size = new System.Drawing.Size(395, 397);
      this.extensionSection1.TabIndex = 0;
      // 
      // streamSection1
      // 
      this.streamSection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.streamSection1.BackColor = System.Drawing.Color.Transparent;
      this.streamSection1.Location = new System.Drawing.Point(0, 0);
      this.streamSection1.Name = "streamSection1";
      this.streamSection1.Size = new System.Drawing.Size(395, 397);
      this.streamSection1.TabIndex = 0;
      // 
      // ConfigurationForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(440, 485);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimizeBox = false;
      this.Name = "ConfigurationForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "MPlayer Configuration";
      this.Load += new System.EventHandler(this.ConfigurationForm_Load);
      this.tabControl1.ResumeLayout(false);
      this.general_Tab.ResumeLayout(false);
      this.video_Tab.ResumeLayout(false);
      this.subtitles_Tab.ResumeLayout(false);
      this.audio_Tab.ResumeLayout(false);
      this.extension_Tab.ResumeLayout(false);
      this.dvd_vcd_svcd_streams_Tab.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    #region variables
    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    #endregion

    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage general_Tab;
    private System.Windows.Forms.TabPage extension_Tab;
    private System.Windows.Forms.TabPage dvd_vcd_svcd_streams_Tab;
    private System.Windows.Forms.FontDialog fontDialog1;
    private System.Windows.Forms.TabPage video_Tab;
    private System.Windows.Forms.TabPage audio_Tab;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.TabPage subtitles_Tab;
    private MPlayer.ConfigurationPanel.GeneralSection generalSection1;
    private MPlayer.ConfigurationPanel.VideoSection videoSection1;
    private MPlayer.ConfigurationPanel.AudioSection audioSection1;
    private MPlayer.ConfigurationPanel.SubtitleSection subtitleSection1;
    private MPlayer.ConfigurationPanel.StreamSection streamSection1;
    private MPlayer.ConfigurationPanel.ExtensionSection extensionSection1;

  }
}