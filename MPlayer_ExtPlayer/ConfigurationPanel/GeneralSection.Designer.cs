#region Copyright (C) 2006-2015 MisterD

/* 
 *	Copyright (C) 2006-2015 MisterD
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
  partial class GeneralSection
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralSection));
      this.label35 = new System.Windows.Forms.Label();
      this.osdSelect = new System.Windows.Forms.ComboBox();
      this.label30 = new System.Windows.Forms.Label();
      this.cacheSize = new System.Windows.Forms.ComboBox();
      this.folderSearch = new System.Windows.Forms.Button();
      this.label8 = new System.Windows.Forms.Label();
      this.mplayerPath = new System.Windows.Forms.TextBox();
      this.rebuildIndex = new System.Windows.Forms.CheckBox();
      this.priorityBoost = new System.Windows.Forms.CheckBox();
      this.label5 = new System.Windows.Forms.Label();
      this.optionalArguments = new System.Windows.Forms.TextBox();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.externalLibGroup = new System.Windows.Forms.GroupBox();
      this.externalOSDLibraryBlank = new System.Windows.Forms.CheckBox();
      this.externalLibGroup.SuspendLayout();
      this.SuspendLayout();
      // 
      // label35
      // 
      this.label35.AutoSize = true;
      this.label35.Location = new System.Drawing.Point(6, 18);
      this.label35.Name = "label35";
      this.label35.Size = new System.Drawing.Size(33, 13);
      this.label35.TabIndex = 76;
      this.label35.Text = "OSD:";
      this.toolTip.SetToolTip(this.label35, resources.GetString("label35.ToolTip"));
      // 
      // osdSelect
      // 
      this.osdSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.osdSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.osdSelect.FormattingEnabled = true;
      this.osdSelect.Items.AddRange(new object[] {
            "Internal MPlayer OSD",
            "External OSD Library(MP like)"});
      this.osdSelect.Location = new System.Drawing.Point(129, 15);
      this.osdSelect.Name = "osdSelect";
      this.osdSelect.Size = new System.Drawing.Size(260, 21);
      this.osdSelect.TabIndex = 75;
      this.toolTip.SetToolTip(this.osdSelect, resources.GetString("osdSelect.ToolTip"));
      // 
      // label30
      // 
      this.label30.AutoSize = true;
      this.label30.Location = new System.Drawing.Point(3, 45);
      this.label30.Name = "label30";
      this.label30.Size = new System.Drawing.Size(59, 13);
      this.label30.TabIndex = 74;
      this.label30.Text = "Cachesize:";
      this.toolTip.SetToolTip(this.label30, "The size of cache that MPlayer will be used in KB (Kilobytes).\r\nFor streaming a v" +
              "alue of at least 4096 is recommend.\r\n");
      // 
      // cacheSize
      // 
      this.cacheSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cacheSize.FormattingEnabled = true;
      this.cacheSize.Items.AddRange(new object[] {
            "2048",
            "4096",
            "8192"});
      this.cacheSize.Location = new System.Drawing.Point(129, 42);
      this.cacheSize.Name = "cacheSize";
      this.cacheSize.Size = new System.Drawing.Size(260, 21);
      this.cacheSize.TabIndex = 73;
      this.toolTip.SetToolTip(this.cacheSize, "The size of cache that MPlayer will be used in KB (Kilobytes).\r\nFor streaming a v" +
              "alue of at least 4096 is recommend.\r\n");
      this.cacheSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CacheSizeKeyPress);
      // 
      // folderSearch
      // 
      this.folderSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.folderSearch.AutoSize = true;
      this.folderSearch.Location = new System.Drawing.Point(313, 154);
      this.folderSearch.Name = "folderSearch";
      this.folderSearch.Size = new System.Drawing.Size(76, 23);
      this.folderSearch.TabIndex = 72;
      this.folderSearch.Text = "&Browse ...";
      this.toolTip.SetToolTip(this.folderSearch, "Search for the executable of MPlayer\r\n");
      this.folderSearch.UseVisualStyleBackColor = true;
      this.folderSearch.Click += new System.EventHandler(this.FolderSearchClick);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(3, 159);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(85, 13);
      this.label8.TabIndex = 71;
      this.label8.Text = "Path to MPlayer:";
      this.toolTip.SetToolTip(this.label8, "Path to the executable of MPlayer\r\n");
      // 
      // mplayerPath
      // 
      this.mplayerPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mplayerPath.Location = new System.Drawing.Point(129, 156);
      this.mplayerPath.Name = "mplayerPath";
      this.mplayerPath.Size = new System.Drawing.Size(174, 20);
      this.mplayerPath.TabIndex = 70;
      this.mplayerPath.Text = "C:\\Program Files\\MPlayer";
      this.toolTip.SetToolTip(this.mplayerPath, "Path to the executable of MPlayer\r\n");
      // 
      // rebuildIndex
      // 
      this.rebuildIndex.AutoSize = true;
      this.rebuildIndex.Location = new System.Drawing.Point(6, 98);
      this.rebuildIndex.Name = "rebuildIndex";
      this.rebuildIndex.Size = new System.Drawing.Size(165, 17);
      this.rebuildIndex.TabIndex = 66;
      this.rebuildIndex.Text = "Rebuild file index if necessary";
      this.toolTip.SetToolTip(this.rebuildIndex, "Indicates, if the index of the file should be rebuild if necessary\r\n");
      this.rebuildIndex.UseVisualStyleBackColor = true;
      // 
      // priorityBoost
      // 
      this.priorityBoost.AutoSize = true;
      this.priorityBoost.Location = new System.Drawing.Point(6, 127);
      this.priorityBoost.Name = "priorityBoost";
      this.priorityBoost.Size = new System.Drawing.Size(87, 17);
      this.priorityBoost.TabIndex = 67;
      this.priorityBoost.Text = "Priority Boost";
      this.toolTip.SetToolTip(this.priorityBoost, "Increases the process priority of MPlayer to above normal.\r\n");
      this.priorityBoost.UseVisualStyleBackColor = true;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(3, 72);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(102, 13);
      this.label5.TabIndex = 68;
      this.label5.Text = "Optional Arguments:";
      this.toolTip.SetToolTip(this.label5, "Specify the optional arguments that\r\nshould be used for every playback.\r\n");
      // 
      // optionalArguments
      // 
      this.optionalArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.optionalArguments.Location = new System.Drawing.Point(129, 69);
      this.optionalArguments.Name = "optionalArguments";
      this.optionalArguments.Size = new System.Drawing.Size(260, 20);
      this.optionalArguments.TabIndex = 69;
      this.toolTip.SetToolTip(this.optionalArguments, "Specify the optional arguments that\r\nshould be used for every playback.\r\n");
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      this.openFileDialog1.Title = "Select MPlayer:";
      // 
      // externalLibGroup
      // 
      this.externalLibGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.externalLibGroup.AutoSize = true;
      this.externalLibGroup.Controls.Add(this.externalOSDLibraryBlank);
      this.externalLibGroup.Location = new System.Drawing.Point(9, 202);
      this.externalLibGroup.Name = "externalLibGroup";
      this.externalLibGroup.Size = new System.Drawing.Size(380, 105);
      this.externalLibGroup.TabIndex = 77;
      this.externalLibGroup.TabStop = false;
      this.externalLibGroup.Text = "External OSD Library";
      // 
      // externalOSDLibraryBlank
      // 
      this.externalOSDLibraryBlank.AutoSize = true;
      this.externalOSDLibraryBlank.Checked = true;
      this.externalOSDLibraryBlank.CheckState = System.Windows.Forms.CheckState.Checked;
      this.externalOSDLibraryBlank.Location = new System.Drawing.Point(6, 31);
      this.externalOSDLibraryBlank.Name = "externalOSDLibraryBlank";
      this.externalOSDLibraryBlank.Size = new System.Drawing.Size(354, 30);
      this.externalOSDLibraryBlank.TabIndex = 0;
      this.externalOSDLibraryBlank.Text = "Blank screen in fullscreen\r\n(If you see a black screen in fullscreen, you should " +
          "turn this option off)";
      this.externalOSDLibraryBlank.UseVisualStyleBackColor = true;
      // 
      // GeneralSection
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.externalLibGroup);
      this.Controls.Add(this.label35);
      this.Controls.Add(this.osdSelect);
      this.Controls.Add(this.label30);
      this.Controls.Add(this.cacheSize);
      this.Controls.Add(this.folderSearch);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.mplayerPath);
      this.Controls.Add(this.rebuildIndex);
      this.Controls.Add(this.priorityBoost);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.optionalArguments);
      this.Name = "GeneralSection";
      this.Size = new System.Drawing.Size(403, 405);
      this.externalLibGroup.ResumeLayout(false);
      this.externalLibGroup.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label35;
    private System.Windows.Forms.ComboBox osdSelect;
    private System.Windows.Forms.Label label30;
    private System.Windows.Forms.ComboBox cacheSize;
    private System.Windows.Forms.Button folderSearch;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox mplayerPath;
    private System.Windows.Forms.CheckBox rebuildIndex;
    private System.Windows.Forms.CheckBox priorityBoost;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox optionalArguments;
    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.GroupBox externalLibGroup;
    private System.Windows.Forms.CheckBox externalOSDLibraryBlank;
  }
}
