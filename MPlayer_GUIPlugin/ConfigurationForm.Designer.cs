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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
          this.okButton = new System.Windows.Forms.Button();
          this.cancelButton = new System.Windows.Forms.Button();
          this.guiConfiguration1 = new MPlayer.ConfigurationPanel.GUIConfiguration();
          this.SuspendLayout();
          // 
          // okButton
          // 
          this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
          this.okButton.AutoSize = true;
          this.okButton.Location = new System.Drawing.Point(12, 330);
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
          this.cancelButton.Location = new System.Drawing.Point(334, 330);
          this.cancelButton.Name = "cancelButton";
          this.cancelButton.Size = new System.Drawing.Size(75, 23);
          this.cancelButton.TabIndex = 20;
          this.cancelButton.Text = "&Cancel";
          this.cancelButton.UseVisualStyleBackColor = true;
          this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
          // 
          // guiConfiguration1
          // 
          this.guiConfiguration1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.guiConfiguration1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
          this.guiConfiguration1.BackColor = System.Drawing.Color.Transparent;
          this.guiConfiguration1.Location = new System.Drawing.Point(12, 5);
          this.guiConfiguration1.Name = "guiConfiguration1";
          this.guiConfiguration1.Size = new System.Drawing.Size(402, 319);
          this.guiConfiguration1.TabIndex = 21;
          // 
          // ConfigurationForm
          // 
          this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
          this.ClientSize = new System.Drawing.Size(421, 367);
          this.Controls.Add(this.guiConfiguration1);
          this.Controls.Add(this.cancelButton);
          this.Controls.Add(this.okButton);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MinimizeBox = false;
          this.Name = "ConfigurationForm";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
          this.Text = "MPlayer Configuration";
          this.Load += new System.EventHandler(this.ConfigurationForm_Load);
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
      private MPlayer.ConfigurationPanel.GUIConfiguration guiConfiguration1;


      }
}