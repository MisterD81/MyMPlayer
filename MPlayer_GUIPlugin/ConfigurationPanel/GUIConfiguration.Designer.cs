namespace MPlayer.ConfigurationPanel
{
  partial class GUIConfiguration
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
      this.myMusicShare = new System.Windows.Forms.CheckBox();
      this.myVideoShare = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.shareList = new System.Windows.Forms.ListBox();
      this.shareAdd = new System.Windows.Forms.Button();
      this.shareDelete = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.shareName = new System.Windows.Forms.TextBox();
      this.browseButton = new System.Windows.Forms.Button();
      this.label23 = new System.Windows.Forms.Label();
      this.shareLocation = new System.Windows.Forms.TextBox();
      this.label24 = new System.Windows.Forms.Label();
      this.pluginName = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.playlistFolder = new System.Windows.Forms.CheckBox();
      this.dvdNavCheckbox = new System.Windows.Forms.CheckBox();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // myMusicShare
      // 
      this.myMusicShare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.myMusicShare.AutoSize = true;
      this.myMusicShare.Location = new System.Drawing.Point(268, 32);
      this.myMusicShare.Name = "myMusicShare";
      this.myMusicShare.Size = new System.Drawing.Size(124, 17);
      this.myMusicShare.TabIndex = 45;
      this.myMusicShare.Text = "Use MyMusic shares";
      this.myMusicShare.UseVisualStyleBackColor = true;
      // 
      // myVideoShare
      // 
      this.myVideoShare.AutoSize = true;
      this.myVideoShare.Location = new System.Drawing.Point(7, 32);
      this.myVideoShare.Name = "myVideoShare";
      this.myVideoShare.Size = new System.Drawing.Size(123, 17);
      this.myVideoShare.TabIndex = 44;
      this.myVideoShare.Text = "Use MyVideo shares";
      this.myVideoShare.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.shareList);
      this.groupBox1.Controls.Add(this.shareAdd);
      this.groupBox1.Controls.Add(this.shareDelete);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.shareName);
      this.groupBox1.Controls.Add(this.browseButton);
      this.groupBox1.Controls.Add(this.label23);
      this.groupBox1.Controls.Add(this.shareLocation);
      this.groupBox1.Controls.Add(this.label24);
      this.groupBox1.Location = new System.Drawing.Point(7, 60);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(385, 226);
      this.groupBox1.TabIndex = 43;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Additional internal shares:";
      // 
      // shareList
      // 
      this.shareList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.shareList.FormattingEnabled = true;
      this.shareList.Location = new System.Drawing.Point(9, 38);
      this.shareList.Name = "shareList";
      this.shareList.Size = new System.Drawing.Size(366, 95);
      this.shareList.TabIndex = 27;
      this.shareList.SelectedIndexChanged += new System.EventHandler(this.shareList_SelectedIndexChanged);
      // 
      // shareAdd
      // 
      this.shareAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.shareAdd.Location = new System.Drawing.Point(9, 139);
      this.shareAdd.Name = "shareAdd";
      this.shareAdd.Size = new System.Drawing.Size(75, 23);
      this.shareAdd.TabIndex = 28;
      this.shareAdd.Text = "&Add";
      this.shareAdd.UseVisualStyleBackColor = true;
      this.shareAdd.Click += new System.EventHandler(this.shareAdd_Click);
      // 
      // shareDelete
      // 
      this.shareDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.shareDelete.Location = new System.Drawing.Point(87, 139);
      this.shareDelete.Name = "shareDelete";
      this.shareDelete.Size = new System.Drawing.Size(75, 23);
      this.shareDelete.TabIndex = 29;
      this.shareDelete.Text = "&Delete";
      this.shareDelete.UseVisualStyleBackColor = true;
      this.shareDelete.Click += new System.EventHandler(this.shareDelete_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 16);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(74, 13);
      this.label1.TabIndex = 35;
      this.label1.Text = "List of Shares:";
      // 
      // shareName
      // 
      this.shareName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.shareName.Enabled = false;
      this.shareName.Location = new System.Drawing.Point(87, 168);
      this.shareName.Name = "shareName";
      this.shareName.Size = new System.Drawing.Size(207, 20);
      this.shareName.TabIndex = 30;
      this.shareName.Leave += new System.EventHandler(this.shareName_Leave);
      // 
      // browseButton
      // 
      this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.browseButton.Location = new System.Drawing.Point(300, 192);
      this.browseButton.Name = "browseButton";
      this.browseButton.Size = new System.Drawing.Size(75, 23);
      this.browseButton.TabIndex = 34;
      this.browseButton.Text = "Browse...";
      this.browseButton.UseVisualStyleBackColor = true;
      this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
      // 
      // label23
      // 
      this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label23.AutoSize = true;
      this.label23.Location = new System.Drawing.Point(9, 171);
      this.label23.Name = "label23";
      this.label23.Size = new System.Drawing.Size(38, 13);
      this.label23.TabIndex = 31;
      this.label23.Text = "Name:";
      // 
      // shareLocation
      // 
      this.shareLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.shareLocation.Enabled = false;
      this.shareLocation.Location = new System.Drawing.Point(87, 194);
      this.shareLocation.Name = "shareLocation";
      this.shareLocation.Size = new System.Drawing.Size(207, 20);
      this.shareLocation.TabIndex = 33;
      this.shareLocation.Leave += new System.EventHandler(this.shareLocation_Leave);
      // 
      // label24
      // 
      this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label24.AutoSize = true;
      this.label24.Location = new System.Drawing.Point(9, 197);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(48, 13);
      this.label24.TabIndex = 32;
      this.label24.Text = "Location";
      // 
      // pluginName
      // 
      this.pluginName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pluginName.Location = new System.Drawing.Point(180, 6);
      this.pluginName.Name = "pluginName";
      this.pluginName.Size = new System.Drawing.Size(212, 20);
      this.pluginName.TabIndex = 42;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(4, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(170, 13);
      this.label2.TabIndex = 41;
      this.label2.Text = "Name of plugin displayed in Home:";
      // 
      // folderBrowserDialog1
      // 
      this.folderBrowserDialog1.Description = "Select the recording folder:";
      // 
      // playlistFolder
      // 
      this.playlistFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.playlistFolder.AutoSize = true;
      this.playlistFolder.Location = new System.Drawing.Point(7, 292);
      this.playlistFolder.Name = "playlistFolder";
      this.playlistFolder.Size = new System.Drawing.Size(139, 17);
      this.playlistFolder.TabIndex = 46;
      this.playlistFolder.Text = "Treat Playlists as folders";
      this.playlistFolder.UseVisualStyleBackColor = true;
      // 
      // dvdNavCheckbox
      // 
      this.dvdNavCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.dvdNavCheckbox.AutoSize = true;
      this.dvdNavCheckbox.Location = new System.Drawing.Point(152, 292);
      this.dvdNavCheckbox.Name = "dvdNavCheckbox";
      this.dvdNavCheckbox.Size = new System.Drawing.Size(254, 17);
      this.dvdNavCheckbox.TabIndex = 47;
      this.dvdNavCheckbox.Text = "Use dvdnav for DVD plackback (exmperimental)";
      this.dvdNavCheckbox.UseVisualStyleBackColor = true;
      // 
      // GUIConfiguration
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.dvdNavCheckbox);
      this.Controls.Add(this.playlistFolder);
      this.Controls.Add(this.myMusicShare);
      this.Controls.Add(this.myVideoShare);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.pluginName);
      this.Controls.Add(this.label2);
      this.Name = "GUIConfiguration";
      this.Size = new System.Drawing.Size(402, 322);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox myMusicShare;
    private System.Windows.Forms.CheckBox myVideoShare;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ListBox shareList;
    private System.Windows.Forms.Button shareAdd;
    private System.Windows.Forms.Button shareDelete;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox shareName;
    private System.Windows.Forms.Button browseButton;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.TextBox shareLocation;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.TextBox pluginName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.CheckBox playlistFolder;
    private System.Windows.Forms.CheckBox dvdNavCheckbox;
  }
}
