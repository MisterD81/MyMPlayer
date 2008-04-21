namespace MPlayer.ConfigurationPanel {
  partial class ExtensionSection {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtensionSection));
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.videoExtList = new System.Windows.Forms.ListBox();
      this.videoDelete = new System.Windows.Forms.Button();
      this.videoExtension = new System.Windows.Forms.TextBox();
      this.videoPlayerUse = new System.Windows.Forms.CheckBox();
      this.videoAdd = new System.Windows.Forms.Button();
      this.videoArgument = new System.Windows.Forms.TextBox();
      this.audioPlayerUse = new System.Windows.Forms.CheckBox();
      this.audioArgument = new System.Windows.Forms.TextBox();
      this.audioExtension = new System.Windows.Forms.TextBox();
      this.audioDelete = new System.Windows.Forms.Button();
      this.audioAdd = new System.Windows.Forms.Button();
      this.audioExtList = new System.Windows.Forms.ListBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.label24 = new System.Windows.Forms.Label();
      this.label23 = new System.Windows.Forms.Label();
      this.label25 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label26 = new System.Windows.Forms.Label();
      this.label27 = new System.Windows.Forms.Label();
      this.label28 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // videoExtList
      // 
      this.videoExtList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.videoExtList.FormattingEnabled = true;
      this.videoExtList.Location = new System.Drawing.Point(6, 19);
      this.videoExtList.Name = "videoExtList";
      this.videoExtList.Size = new System.Drawing.Size(78, 160);
      this.videoExtList.TabIndex = 18;
      this.toolTip.SetToolTip(this.videoExtList, "List of supported video extensions");
      this.videoExtList.SelectedIndexChanged += new System.EventHandler(this.videoExtList_SelectedIndexChanged);
      // 
      // videoDelete
      // 
      this.videoDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.videoDelete.AutoSize = true;
      this.videoDelete.Location = new System.Drawing.Point(174, 156);
      this.videoDelete.Name = "videoDelete";
      this.videoDelete.Size = new System.Drawing.Size(75, 23);
      this.videoDelete.TabIndex = 20;
      this.videoDelete.Text = "&Delete";
      this.toolTip.SetToolTip(this.videoDelete, "Deletes the selected video extension");
      this.videoDelete.UseVisualStyleBackColor = true;
      this.videoDelete.Click += new System.EventHandler(this.videoDelete_Click);
      // 
      // videoExtension
      // 
      this.videoExtension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.videoExtension.Enabled = false;
      this.videoExtension.Location = new System.Drawing.Point(196, 19);
      this.videoExtension.Name = "videoExtension";
      this.videoExtension.Size = new System.Drawing.Size(192, 20);
      this.videoExtension.TabIndex = 21;
      this.toolTip.SetToolTip(this.videoExtension, "Name of the selected video extension");
      this.videoExtension.Leave += new System.EventHandler(this.videoExtension_Leave);
      // 
      // videoPlayerUse
      // 
      this.videoPlayerUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.videoPlayerUse.AutoSize = true;
      this.videoPlayerUse.Enabled = false;
      this.videoPlayerUse.Location = new System.Drawing.Point(196, 71);
      this.videoPlayerUse.Name = "videoPlayerUse";
      this.videoPlayerUse.Size = new System.Drawing.Size(15, 14);
      this.videoPlayerUse.TabIndex = 25;
      this.toolTip.SetToolTip(this.videoPlayerUse, resources.GetString("videoPlayerUse.ToolTip"));
      this.videoPlayerUse.UseVisualStyleBackColor = true;
      // 
      // videoAdd
      // 
      this.videoAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.videoAdd.AutoSize = true;
      this.videoAdd.Location = new System.Drawing.Point(93, 156);
      this.videoAdd.Name = "videoAdd";
      this.videoAdd.Size = new System.Drawing.Size(75, 23);
      this.videoAdd.TabIndex = 19;
      this.videoAdd.Text = "&Add";
      this.toolTip.SetToolTip(this.videoAdd, "Adds a new video extension to the\r\nlist of supported extensions.");
      this.videoAdd.UseVisualStyleBackColor = true;
      this.videoAdd.Click += new System.EventHandler(this.videoAdd_Click);
      // 
      // videoArgument
      // 
      this.videoArgument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.videoArgument.Enabled = false;
      this.videoArgument.Location = new System.Drawing.Point(196, 45);
      this.videoArgument.Name = "videoArgument";
      this.videoArgument.Size = new System.Drawing.Size(192, 20);
      this.videoArgument.TabIndex = 24;
      this.toolTip.SetToolTip(this.videoArgument, "Arguments which will be used, when the selected extension is played");
      // 
      // audioPlayerUse
      // 
      this.audioPlayerUse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.audioPlayerUse.AutoSize = true;
      this.audioPlayerUse.Enabled = false;
      this.audioPlayerUse.Location = new System.Drawing.Point(196, 71);
      this.audioPlayerUse.Name = "audioPlayerUse";
      this.audioPlayerUse.Size = new System.Drawing.Size(15, 14);
      this.audioPlayerUse.TabIndex = 43;
      this.toolTip.SetToolTip(this.audioPlayerUse, resources.GetString("audioPlayerUse.ToolTip"));
      this.audioPlayerUse.UseVisualStyleBackColor = true;
      // 
      // audioArgument
      // 
      this.audioArgument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.audioArgument.Enabled = false;
      this.audioArgument.Location = new System.Drawing.Point(196, 45);
      this.audioArgument.Name = "audioArgument";
      this.audioArgument.Size = new System.Drawing.Size(192, 20);
      this.audioArgument.TabIndex = 42;
      this.toolTip.SetToolTip(this.audioArgument, "Arguments which will be used, when the selected extension is played\r\n");
      // 
      // audioExtension
      // 
      this.audioExtension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.audioExtension.Enabled = false;
      this.audioExtension.Location = new System.Drawing.Point(196, 19);
      this.audioExtension.Name = "audioExtension";
      this.audioExtension.Size = new System.Drawing.Size(192, 20);
      this.audioExtension.TabIndex = 39;
      this.toolTip.SetToolTip(this.audioExtension, "Name of the selected audio  extension");
      this.audioExtension.Leave += new System.EventHandler(this.audioExtension_Leave);
      // 
      // audioDelete
      // 
      this.audioDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.audioDelete.AutoSize = true;
      this.audioDelete.Location = new System.Drawing.Point(174, 156);
      this.audioDelete.Name = "audioDelete";
      this.audioDelete.Size = new System.Drawing.Size(75, 23);
      this.audioDelete.TabIndex = 38;
      this.audioDelete.Text = "&Delete";
      this.toolTip.SetToolTip(this.audioDelete, "Deletes the selected video extension");
      this.audioDelete.UseVisualStyleBackColor = true;
      this.audioDelete.Click += new System.EventHandler(this.audioDelete_Click);
      // 
      // audioAdd
      // 
      this.audioAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.audioAdd.AutoSize = true;
      this.audioAdd.Location = new System.Drawing.Point(93, 156);
      this.audioAdd.Name = "audioAdd";
      this.audioAdd.Size = new System.Drawing.Size(75, 23);
      this.audioAdd.TabIndex = 37;
      this.audioAdd.Text = "&Add";
      this.toolTip.SetToolTip(this.audioAdd, "Adds a new audio extension to the\r\nlist of supported extensions.\r\n");
      this.audioAdd.UseVisualStyleBackColor = true;
      this.audioAdd.Click += new System.EventHandler(this.audioAdd_Click);
      // 
      // audioExtList
      // 
      this.audioExtList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.audioExtList.FormattingEnabled = true;
      this.audioExtList.Location = new System.Drawing.Point(6, 19);
      this.audioExtList.Name = "audioExtList";
      this.audioExtList.Size = new System.Drawing.Size(78, 160);
      this.audioExtList.TabIndex = 36;
      this.toolTip.SetToolTip(this.audioExtList, "List of supported audio extensions");
      this.audioExtList.SelectedIndexChanged += new System.EventHandler(this.audioExtList_SelectedIndexChanged);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.videoExtList);
      this.groupBox1.Controls.Add(this.videoDelete);
      this.groupBox1.Controls.Add(this.label24);
      this.groupBox1.Controls.Add(this.label23);
      this.groupBox1.Controls.Add(this.label25);
      this.groupBox1.Controls.Add(this.videoExtension);
      this.groupBox1.Controls.Add(this.videoPlayerUse);
      this.groupBox1.Controls.Add(this.videoAdd);
      this.groupBox1.Controls.Add(this.videoArgument);
      this.groupBox1.Location = new System.Drawing.Point(3, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(394, 189);
      this.groupBox1.TabIndex = 30;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Video";
      // 
      // label24
      // 
      this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label24.AutoSize = true;
      this.label24.Location = new System.Drawing.Point(90, 48);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(60, 13);
      this.label24.TabIndex = 23;
      this.label24.Text = "Arguments:";
      // 
      // label23
      // 
      this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label23.AutoSize = true;
      this.label23.Location = new System.Drawing.Point(90, 22);
      this.label23.Name = "label23";
      this.label23.Size = new System.Drawing.Size(56, 13);
      this.label23.TabIndex = 22;
      this.label23.Text = "Extension:";
      // 
      // label25
      // 
      this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label25.AutoSize = true;
      this.label25.Location = new System.Drawing.Point(90, 72);
      this.label25.Name = "label25";
      this.label25.Size = new System.Drawing.Size(100, 13);
      this.label25.TabIndex = 26;
      this.label25.Text = "External Player use:";
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.label26);
      this.groupBox2.Controls.Add(this.audioPlayerUse);
      this.groupBox2.Controls.Add(this.audioArgument);
      this.groupBox2.Controls.Add(this.label27);
      this.groupBox2.Controls.Add(this.label28);
      this.groupBox2.Controls.Add(this.audioExtension);
      this.groupBox2.Controls.Add(this.audioDelete);
      this.groupBox2.Controls.Add(this.audioAdd);
      this.groupBox2.Controls.Add(this.audioExtList);
      this.groupBox2.Location = new System.Drawing.Point(3, 201);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(394, 189);
      this.groupBox2.TabIndex = 31;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Audio";
      // 
      // label26
      // 
      this.label26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label26.AutoSize = true;
      this.label26.Location = new System.Drawing.Point(90, 72);
      this.label26.Name = "label26";
      this.label26.Size = new System.Drawing.Size(100, 13);
      this.label26.TabIndex = 44;
      this.label26.Text = "External Player use:";
      // 
      // label27
      // 
      this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label27.AutoSize = true;
      this.label27.Location = new System.Drawing.Point(90, 48);
      this.label27.Name = "label27";
      this.label27.Size = new System.Drawing.Size(60, 13);
      this.label27.TabIndex = 41;
      this.label27.Text = "Arguments:";
      // 
      // label28
      // 
      this.label28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label28.AutoSize = true;
      this.label28.Location = new System.Drawing.Point(90, 22);
      this.label28.Name = "label28";
      this.label28.Size = new System.Drawing.Size(56, 13);
      this.label28.TabIndex = 40;
      this.label28.Text = "Extension:";
      // 
      // ExtensionSection
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.Transparent;
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Name = "ExtensionSection";
      this.Size = new System.Drawing.Size(403, 405);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ToolTip toolTip;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.ListBox videoExtList;
    private System.Windows.Forms.Button videoDelete;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.Label label25;
    private System.Windows.Forms.TextBox videoExtension;
    private System.Windows.Forms.CheckBox videoPlayerUse;
    private System.Windows.Forms.Button videoAdd;
    private System.Windows.Forms.TextBox videoArgument;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label26;
    private System.Windows.Forms.CheckBox audioPlayerUse;
    private System.Windows.Forms.TextBox audioArgument;
    private System.Windows.Forms.Label label27;
    private System.Windows.Forms.Label label28;
    private System.Windows.Forms.TextBox audioExtension;
    private System.Windows.Forms.Button audioDelete;
    private System.Windows.Forms.Button audioAdd;
    private System.Windows.Forms.ListBox audioExtList;
  }
}
