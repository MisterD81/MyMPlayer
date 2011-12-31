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

namespace MPlayer.ConfigurationPanel
{
  partial class VideoSection
  {
    /// <summary> 
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
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
      this.label6 = new System.Windows.Forms.Label();
      this.videoOutputDriver = new System.Windows.Forms.ComboBox();
      this.framedrop = new System.Windows.Forms.CheckBox();
      this.directRendering = new System.Windows.Forms.CheckBox();
      this.doubleBuffering = new System.Windows.Forms.CheckBox();
      this.label21 = new System.Windows.Forms.Label();
      this.noiseDenoise = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.deinterlace = new System.Windows.Forms.ComboBox();
      this.aspectRatio = new System.Windows.Forms.ComboBox();
      this.postProcessing = new System.Windows.Forms.ComboBox();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 18);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(99, 13);
      this.label6.TabIndex = 68;
      this.label6.Text = "Video output driver:";
      this.toolTip.SetToolTip(this.label6, "Defines the used video output driver. The following are supported:\r\n- DirectX, wh" +
              "ich is the recommend one\r\n- DirectX without HW-Acceleration\r\n- OpenGL\r\n- OpenGL " +
              "2\r\n");
      // 
      // videoOutputDriver
      // 
      this.videoOutputDriver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.videoOutputDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.videoOutputDriver.FormattingEnabled = true;
      this.videoOutputDriver.Items.AddRange(new object[] {
            "DirectX",
            "DirectX - No Acceleration",
            "OpenGL",
            "OpenGL2",
            "Direct3D - Beta"});
      this.videoOutputDriver.Location = new System.Drawing.Point(129, 15);
      this.videoOutputDriver.Name = "videoOutputDriver";
      this.videoOutputDriver.Size = new System.Drawing.Size(260, 21);
      this.videoOutputDriver.TabIndex = 67;
      this.toolTip.SetToolTip(this.videoOutputDriver, "Defines the used video output driver. The following are supported:\r\n- DirectX\r\n- " +
              "DirectX without HW-Acceleration\r\n- OpenGL\r\n- OpenGL 2\r\n- Direct3D - currently be" +
              "ta");
      // 
      // framedrop
      // 
      this.framedrop.AutoSize = true;
      this.framedrop.Location = new System.Drawing.Point(9, 150);
      this.framedrop.Name = "framedrop";
      this.framedrop.Size = new System.Drawing.Size(76, 17);
      this.framedrop.TabIndex = 66;
      this.framedrop.Text = "Framedrop";
      this.toolTip.SetToolTip(this.framedrop, "Indicates, if MPlayer should drop some frame to get a better A/V sync");
      this.framedrop.UseVisualStyleBackColor = true;
      // 
      // directRendering
      // 
      this.directRendering.AutoSize = true;
      this.directRendering.Location = new System.Drawing.Point(9, 179);
      this.directRendering.Name = "directRendering";
      this.directRendering.Size = new System.Drawing.Size(106, 17);
      this.directRendering.TabIndex = 65;
      this.directRendering.Text = "Direct Rendering";
      this.toolTip.SetToolTip(this.directRendering, "Indicates, if direct rendering should be used.\r\nWARNING: This option could cause " +
              "screen coruptions,\r\nbut if it\'s work you will get better results");
      this.directRendering.UseVisualStyleBackColor = true;
      // 
      // doubleBuffering
      // 
      this.doubleBuffering.AutoSize = true;
      this.doubleBuffering.Location = new System.Drawing.Point(9, 208);
      this.doubleBuffering.Name = "doubleBuffering";
      this.doubleBuffering.Size = new System.Drawing.Size(105, 17);
      this.doubleBuffering.TabIndex = 64;
      this.doubleBuffering.Text = "Double Buffering";
      this.toolTip.SetToolTip(this.doubleBuffering, "Indicates, if double buffering should be used.\r\nDouble buffering can reduce the f" +
              "lickering of the video");
      this.doubleBuffering.UseVisualStyleBackColor = true;
      // 
      // label21
      // 
      this.label21.AutoSize = true;
      this.label21.Location = new System.Drawing.Point(6, 126);
      this.label21.Name = "label21";
      this.label21.Size = new System.Drawing.Size(81, 13);
      this.label21.TabIndex = 63;
      this.label21.Text = "Noise/Denoise:";
      this.toolTip.SetToolTip(this.label21, "Defines the used noise/denoise mode\r\n");
      // 
      // noiseDenoise
      // 
      this.noiseDenoise.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.noiseDenoise.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.noiseDenoise.FormattingEnabled = true;
      this.noiseDenoise.Items.AddRange(new object[] {
            "Nothing",
            "Noise",
            "High Quality Denoise",
            "Denoise"});
      this.noiseDenoise.Location = new System.Drawing.Point(129, 123);
      this.noiseDenoise.Name = "noiseDenoise";
      this.noiseDenoise.Size = new System.Drawing.Size(260, 21);
      this.noiseDenoise.TabIndex = 62;
      this.toolTip.SetToolTip(this.noiseDenoise, "Defines the used noise/denoise mode\r\n");
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 45);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(82, 13);
      this.label2.TabIndex = 56;
      this.label2.Text = "Postprocessing:";
      this.toolTip.SetToolTip(this.label2, "Defines the used postprocessing mode\r\n");
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 72);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(71, 13);
      this.label3.TabIndex = 57;
      this.label3.Text = "Aspect Ratio:";
      this.toolTip.SetToolTip(this.label3, "Defines the aspect ratio of all files. If you select autodetect,\r\nthan MPlayer de" +
              "tects the aspect ratio automatically.\r\n");
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 99);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(64, 13);
      this.label4.TabIndex = 58;
      this.label4.Text = "Deinterlace:";
      this.toolTip.SetToolTip(this.label4, "Defines the used deinterlace mode");
      // 
      // deinterlace
      // 
      this.deinterlace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.deinterlace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.deinterlace.FormattingEnabled = true;
      this.deinterlace.Items.AddRange(new object[] {
            "Off",
            "Simple",
            "Adaptive"});
      this.deinterlace.Location = new System.Drawing.Point(129, 96);
      this.deinterlace.Name = "deinterlace";
      this.deinterlace.Size = new System.Drawing.Size(260, 21);
      this.deinterlace.TabIndex = 61;
      this.toolTip.SetToolTip(this.deinterlace, "Defines the used deinterlace mode");
      // 
      // aspectRatio
      // 
      this.aspectRatio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.aspectRatio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.aspectRatio.FormattingEnabled = true;
      this.aspectRatio.Items.AddRange(new object[] {
            "Autodetect",
            "4:3",
            "16:9",
            "2,35"});
      this.aspectRatio.Location = new System.Drawing.Point(129, 69);
      this.aspectRatio.Name = "aspectRatio";
      this.aspectRatio.Size = new System.Drawing.Size(260, 21);
      this.aspectRatio.TabIndex = 60;
      this.toolTip.SetToolTip(this.aspectRatio, "Defines the aspect ratio of all files. If you select autodetect,\r\nthan MPlayer de" +
              "tects the aspect ratio automatically.");
      // 
      // postProcessing
      // 
      this.postProcessing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.postProcessing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.postProcessing.FormattingEnabled = true;
      this.postProcessing.Items.AddRange(new object[] {
            "Off",
            "Automatic",
            "Maximum quality"});
      this.postProcessing.Location = new System.Drawing.Point(129, 42);
      this.postProcessing.Name = "postProcessing";
      this.postProcessing.Size = new System.Drawing.Size(260, 21);
      this.postProcessing.TabIndex = 59;
      this.toolTip.SetToolTip(this.postProcessing, "Defines the used postprocessing mode");
      // 
      // VideoSection
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.label6);
      this.Controls.Add(this.videoOutputDriver);
      this.Controls.Add(this.framedrop);
      this.Controls.Add(this.directRendering);
      this.Controls.Add(this.doubleBuffering);
      this.Controls.Add(this.label21);
      this.Controls.Add(this.noiseDenoise);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.deinterlace);
      this.Controls.Add(this.aspectRatio);
      this.Controls.Add(this.postProcessing);
      this.Name = "VideoSection";
      this.Size = new System.Drawing.Size(403, 405);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.ComboBox videoOutputDriver;
    private System.Windows.Forms.CheckBox framedrop;
    private System.Windows.Forms.CheckBox directRendering;
    private System.Windows.Forms.CheckBox doubleBuffering;
    private System.Windows.Forms.Label label21;
    private System.Windows.Forms.ComboBox noiseDenoise;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox deinterlace;
    private System.Windows.Forms.ComboBox aspectRatio;
    private System.Windows.Forms.ComboBox postProcessing;
    private System.Windows.Forms.ToolTip toolTip;
  }
}
