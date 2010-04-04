namespace BluRip
{
    partial class AdvancedAudioOptionsEdit
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelExtension = new System.Windows.Forms.Label();
            this.textBoxExtension = new System.Windows.Forms.TextBox();
            this.labelBirate = new System.Windows.Forms.Label();
            this.textBoxBitrate = new System.Windows.Forms.TextBox();
            this.labelParameter = new System.Windows.Forms.Label();
            this.textBoxParameter = new System.Windows.Forms.TextBox();
            this.checkBoxAdditionalAc3Track = new System.Windows.Forms.CheckBox();
            this.groupBoxRequired = new System.Windows.Forms.GroupBox();
            this.labelExtensionList = new System.Windows.Forms.Label();
            this.comboBoxExtension = new System.Windows.Forms.ComboBox();
            this.groupBoxOptional = new System.Windows.Forms.GroupBox();
            this.labelBitrateList = new System.Windows.Forms.Label();
            this.comboBoxBitrate = new System.Windows.Forms.ComboBox();
            this.groupBoxRequired.SuspendLayout();
            this.groupBoxOptional.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(12, 210);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(93, 210);
            // 
            // labelExtension
            // 
            this.labelExtension.AutoSize = true;
            this.labelExtension.Location = new System.Drawing.Point(6, 16);
            this.labelExtension.Name = "labelExtension";
            this.labelExtension.Size = new System.Drawing.Size(108, 13);
            this.labelExtension.TabIndex = 2;
            this.labelExtension.Text = "Extension (e.g. \'.ac3\')";
            // 
            // textBoxExtension
            // 
            this.textBoxExtension.Location = new System.Drawing.Point(6, 32);
            this.textBoxExtension.Name = "textBoxExtension";
            this.textBoxExtension.Size = new System.Drawing.Size(136, 20);
            this.textBoxExtension.TabIndex = 3;
            this.textBoxExtension.TextChanged += new System.EventHandler(this.textBoxExtension_TextChanged);
            // 
            // labelBirate
            // 
            this.labelBirate.AutoSize = true;
            this.labelBirate.Location = new System.Drawing.Point(6, 16);
            this.labelBirate.Name = "labelBirate";
            this.labelBirate.Size = new System.Drawing.Size(89, 13);
            this.labelBirate.TabIndex = 4;
            this.labelBirate.Text = "Bitrate (e.g. \'768\')";
            // 
            // textBoxBitrate
            // 
            this.textBoxBitrate.Location = new System.Drawing.Point(6, 32);
            this.textBoxBitrate.Name = "textBoxBitrate";
            this.textBoxBitrate.Size = new System.Drawing.Size(139, 20);
            this.textBoxBitrate.TabIndex = 5;
            this.textBoxBitrate.TextChanged += new System.EventHandler(this.textBoxBitrate_TextChanged);
            // 
            // labelParameter
            // 
            this.labelParameter.AutoSize = true;
            this.labelParameter.Location = new System.Drawing.Point(6, 55);
            this.labelParameter.Name = "labelParameter";
            this.labelParameter.Size = new System.Drawing.Size(139, 13);
            this.labelParameter.TabIndex = 6;
            this.labelParameter.Text = "Extra parameter (e.g. \'-core\')";
            // 
            // textBoxParameter
            // 
            this.textBoxParameter.Location = new System.Drawing.Point(6, 71);
            this.textBoxParameter.Name = "textBoxParameter";
            this.textBoxParameter.Size = new System.Drawing.Size(139, 20);
            this.textBoxParameter.TabIndex = 7;
            this.textBoxParameter.TextChanged += new System.EventHandler(this.textBoxParameter_TextChanged);
            // 
            // checkBoxAdditionalAc3Track
            // 
            this.checkBoxAdditionalAc3Track.AutoSize = true;
            this.checkBoxAdditionalAc3Track.Location = new System.Drawing.Point(9, 97);
            this.checkBoxAdditionalAc3Track.Name = "checkBoxAdditionalAc3Track";
            this.checkBoxAdditionalAc3Track.Size = new System.Drawing.Size(143, 17);
            this.checkBoxAdditionalAc3Track.TabIndex = 8;
            this.checkBoxAdditionalAc3Track.Text = "Add additional AC3 track";
            this.checkBoxAdditionalAc3Track.UseVisualStyleBackColor = true;
            this.checkBoxAdditionalAc3Track.CheckedChanged += new System.EventHandler(this.checkBoxAdditionalAc3Track_CheckedChanged);
            // 
            // groupBoxRequired
            // 
            this.groupBoxRequired.Controls.Add(this.labelExtensionList);
            this.groupBoxRequired.Controls.Add(this.comboBoxExtension);
            this.groupBoxRequired.Controls.Add(this.labelExtension);
            this.groupBoxRequired.Controls.Add(this.textBoxExtension);
            this.groupBoxRequired.Location = new System.Drawing.Point(12, 12);
            this.groupBoxRequired.Name = "groupBoxRequired";
            this.groupBoxRequired.Size = new System.Drawing.Size(229, 61);
            this.groupBoxRequired.TabIndex = 9;
            this.groupBoxRequired.TabStop = false;
            this.groupBoxRequired.Text = "Required settings";
            // 
            // labelExtensionList
            // 
            this.labelExtensionList.AutoSize = true;
            this.labelExtensionList.Location = new System.Drawing.Point(145, 15);
            this.labelExtensionList.Name = "labelExtensionList";
            this.labelExtensionList.Size = new System.Drawing.Size(66, 13);
            this.labelExtensionList.TabIndex = 5;
            this.labelExtensionList.Text = "Choose from";
            // 
            // comboBoxExtension
            // 
            this.comboBoxExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExtension.FormattingEnabled = true;
            this.comboBoxExtension.Items.AddRange(new object[] {
            ".ac3",
            ".dts",
            ".pcm",
            ".flac",
            ".thd",
            ".dtshd",
            ".eac3"});
            this.comboBoxExtension.Location = new System.Drawing.Point(148, 31);
            this.comboBoxExtension.Name = "comboBoxExtension";
            this.comboBoxExtension.Size = new System.Drawing.Size(75, 21);
            this.comboBoxExtension.TabIndex = 4;
            this.comboBoxExtension.SelectedIndexChanged += new System.EventHandler(this.comboBoxExtension_SelectedIndexChanged);
            // 
            // groupBoxOptional
            // 
            this.groupBoxOptional.Controls.Add(this.labelBitrateList);
            this.groupBoxOptional.Controls.Add(this.comboBoxBitrate);
            this.groupBoxOptional.Controls.Add(this.labelBirate);
            this.groupBoxOptional.Controls.Add(this.textBoxBitrate);
            this.groupBoxOptional.Controls.Add(this.checkBoxAdditionalAc3Track);
            this.groupBoxOptional.Controls.Add(this.textBoxParameter);
            this.groupBoxOptional.Controls.Add(this.labelParameter);
            this.groupBoxOptional.Location = new System.Drawing.Point(12, 79);
            this.groupBoxOptional.Name = "groupBoxOptional";
            this.groupBoxOptional.Size = new System.Drawing.Size(229, 122);
            this.groupBoxOptional.TabIndex = 10;
            this.groupBoxOptional.TabStop = false;
            this.groupBoxOptional.Text = "Optional settings";
            // 
            // labelBitrateList
            // 
            this.labelBitrateList.AutoSize = true;
            this.labelBitrateList.Location = new System.Drawing.Point(145, 15);
            this.labelBitrateList.Name = "labelBitrateList";
            this.labelBitrateList.Size = new System.Drawing.Size(66, 13);
            this.labelBitrateList.TabIndex = 10;
            this.labelBitrateList.Text = "Choose from";
            // 
            // comboBoxBitrate
            // 
            this.comboBoxBitrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBitrate.FormattingEnabled = true;
            this.comboBoxBitrate.Items.AddRange(new object[] {
            "192",
            "448",
            "640",
            "768",
            "1536"});
            this.comboBoxBitrate.Location = new System.Drawing.Point(148, 31);
            this.comboBoxBitrate.Name = "comboBoxBitrate";
            this.comboBoxBitrate.Size = new System.Drawing.Size(75, 21);
            this.comboBoxBitrate.TabIndex = 9;
            this.comboBoxBitrate.SelectedIndexChanged += new System.EventHandler(this.comboBoxBitrate_SelectedIndexChanged);
            // 
            // AdvancedAudioOptionsEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(254, 245);
            this.Controls.Add(this.groupBoxOptional);
            this.Controls.Add(this.groupBoxRequired);
            this.Name = "AdvancedAudioOptionsEdit";
            this.Text = "Edit advanced audio options";
            this.Controls.SetChildIndex(this.buttonOk, 0);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.Controls.SetChildIndex(this.groupBoxRequired, 0);
            this.Controls.SetChildIndex(this.groupBoxOptional, 0);
            this.groupBoxRequired.ResumeLayout(false);
            this.groupBoxRequired.PerformLayout();
            this.groupBoxOptional.ResumeLayout(false);
            this.groupBoxOptional.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelExtension;
        private System.Windows.Forms.TextBox textBoxExtension;
        private System.Windows.Forms.Label labelBirate;
        private System.Windows.Forms.TextBox textBoxBitrate;
        private System.Windows.Forms.Label labelParameter;
        private System.Windows.Forms.TextBox textBoxParameter;
        private System.Windows.Forms.CheckBox checkBoxAdditionalAc3Track;
        private System.Windows.Forms.GroupBox groupBoxRequired;
        private System.Windows.Forms.Label labelExtensionList;
        private System.Windows.Forms.ComboBox comboBoxExtension;
        private System.Windows.Forms.GroupBox groupBoxOptional;
        private System.Windows.Forms.ComboBox comboBoxBitrate;
        private System.Windows.Forms.Label labelBitrateList;
    }
}
