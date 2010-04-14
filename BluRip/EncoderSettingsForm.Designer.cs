namespace BluRip
{
    partial class EncoderSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelDesc = new System.Windows.Forms.Label();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.labelSettings = new System.Windows.Forms.Label();
            this.textBoxSettings = new System.Windows.Forms.TextBox();
            this.checkBox2pass = new System.Windows.Forms.CheckBox();
            this.textBoxSettings2 = new System.Windows.Forms.TextBox();
            this.labelSettings2 = new System.Windows.Forms.Label();
            this.labelSizeValue = new System.Windows.Forms.Label();
            this.textBoxSizeValue = new System.Windows.Forms.TextBox();
            this.comboBoxSizeType = new System.Windows.Forms.ComboBox();
            this.labelSizeType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(12, 194);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(93, 194);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelDesc
            // 
            this.labelDesc.AutoSize = true;
            this.labelDesc.Location = new System.Drawing.Point(12, 9);
            this.labelDesc.Name = "labelDesc";
            this.labelDesc.Size = new System.Drawing.Size(60, 13);
            this.labelDesc.TabIndex = 2;
            this.labelDesc.Text = "Description";
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Location = new System.Drawing.Point(12, 25);
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.Size = new System.Drawing.Size(406, 20);
            this.textBoxDesc.TabIndex = 1;
            // 
            // labelSettings
            // 
            this.labelSettings.AutoSize = true;
            this.labelSettings.Location = new System.Drawing.Point(12, 48);
            this.labelSettings.Name = "labelSettings";
            this.labelSettings.Size = new System.Drawing.Size(55, 13);
            this.labelSettings.TabIndex = 4;
            this.labelSettings.Text = "Parameter";
            // 
            // textBoxSettings
            // 
            this.textBoxSettings.Location = new System.Drawing.Point(12, 64);
            this.textBoxSettings.Name = "textBoxSettings";
            this.textBoxSettings.Size = new System.Drawing.Size(406, 20);
            this.textBoxSettings.TabIndex = 2;
            // 
            // checkBox2pass
            // 
            this.checkBox2pass.AutoSize = true;
            this.checkBox2pass.Location = new System.Drawing.Point(12, 90);
            this.checkBox2pass.Name = "checkBox2pass";
            this.checkBox2pass.Size = new System.Drawing.Size(88, 17);
            this.checkBox2pass.TabIndex = 4;
            this.checkBox2pass.Text = "2 pass profile";
            this.checkBox2pass.UseVisualStyleBackColor = true;
            this.checkBox2pass.CheckedChanged += new System.EventHandler(this.checkBox2pass_CheckedChanged);
            // 
            // textBoxSettings2
            // 
            this.textBoxSettings2.Location = new System.Drawing.Point(12, 126);
            this.textBoxSettings2.Name = "textBoxSettings2";
            this.textBoxSettings2.Size = new System.Drawing.Size(406, 20);
            this.textBoxSettings2.TabIndex = 3;
            // 
            // labelSettings2
            // 
            this.labelSettings2.AutoSize = true;
            this.labelSettings2.Location = new System.Drawing.Point(12, 110);
            this.labelSettings2.Name = "labelSettings2";
            this.labelSettings2.Size = new System.Drawing.Size(124, 13);
            this.labelSettings2.TabIndex = 7;
            this.labelSettings2.Text = "Parameter (second pass)";
            // 
            // labelSizeValue
            // 
            this.labelSizeValue.AutoSize = true;
            this.labelSizeValue.Location = new System.Drawing.Point(12, 149);
            this.labelSizeValue.Name = "labelSizeValue";
            this.labelSizeValue.Size = new System.Drawing.Size(56, 13);
            this.labelSizeValue.TabIndex = 9;
            this.labelSizeValue.Text = "Size value";
            // 
            // textBoxSizeValue
            // 
            this.textBoxSizeValue.Location = new System.Drawing.Point(15, 165);
            this.textBoxSizeValue.Name = "textBoxSizeValue";
            this.textBoxSizeValue.Size = new System.Drawing.Size(228, 20);
            this.textBoxSizeValue.TabIndex = 10;
            // 
            // comboBoxSizeType
            // 
            this.comboBoxSizeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSizeType.FormattingEnabled = true;
            this.comboBoxSizeType.Items.AddRange(new object[] {
            "Not used",
            "Specify Bitrate",
            "Specify target size [MB]"});
            this.comboBoxSizeType.Location = new System.Drawing.Point(249, 165);
            this.comboBoxSizeType.Name = "comboBoxSizeType";
            this.comboBoxSizeType.Size = new System.Drawing.Size(169, 21);
            this.comboBoxSizeType.TabIndex = 11;
            // 
            // labelSizeType
            // 
            this.labelSizeType.AutoSize = true;
            this.labelSizeType.Location = new System.Drawing.Point(246, 149);
            this.labelSizeType.Name = "labelSizeType";
            this.labelSizeType.Size = new System.Drawing.Size(79, 13);
            this.labelSizeType.TabIndex = 12;
            this.labelSizeType.Text = "Size value type";
            // 
            // EncoderSettingsForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(431, 227);
            this.Controls.Add(this.labelSizeType);
            this.Controls.Add(this.comboBoxSizeType);
            this.Controls.Add(this.textBoxSizeValue);
            this.Controls.Add(this.labelSizeValue);
            this.Controls.Add(this.textBoxSettings2);
            this.Controls.Add(this.labelSettings2);
            this.Controls.Add(this.checkBox2pass);
            this.Controls.Add(this.textBoxSettings);
            this.Controls.Add(this.labelSettings);
            this.Controls.Add(this.textBoxDesc);
            this.Controls.Add(this.labelDesc);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EncoderSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit x264 profile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelDesc;
        private System.Windows.Forms.TextBox textBoxDesc;
        private System.Windows.Forms.Label labelSettings;
        private System.Windows.Forms.TextBox textBoxSettings;
        private System.Windows.Forms.CheckBox checkBox2pass;
        private System.Windows.Forms.TextBox textBoxSettings2;
        private System.Windows.Forms.Label labelSettings2;
        private System.Windows.Forms.Label labelSizeValue;
        private System.Windows.Forms.TextBox textBoxSizeValue;
        private System.Windows.Forms.ComboBox comboBoxSizeType;
        private System.Windows.Forms.Label labelSizeType;
    }
}