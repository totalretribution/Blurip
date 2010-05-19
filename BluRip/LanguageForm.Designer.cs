namespace BluRip
{
    partial class LanguageForm
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
            this.labelLanguage = new System.Windows.Forms.Label();
            this.textBoxLanguage = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxTranslation = new System.Windows.Forms.TextBox();
            this.labelTranslation = new System.Windows.Forms.Label();
            this.textBoxLanguageShort = new System.Windows.Forms.TextBox();
            this.labelLanguageShort = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelExample = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.Location = new System.Drawing.Point(12, 9);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(174, 13);
            this.labelLanguage.TabIndex = 0;
            this.labelLanguage.Text = "Language (in english like in eac3to)";
            // 
            // textBoxLanguage
            // 
            this.textBoxLanguage.Location = new System.Drawing.Point(12, 25);
            this.textBoxLanguage.Name = "textBoxLanguage";
            this.textBoxLanguage.Size = new System.Drawing.Size(269, 20);
            this.textBoxLanguage.TabIndex = 1;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(12, 148);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // textBoxTranslation
            // 
            this.textBoxTranslation.Location = new System.Drawing.Point(12, 64);
            this.textBoxTranslation.Name = "textBoxTranslation";
            this.textBoxTranslation.Size = new System.Drawing.Size(269, 20);
            this.textBoxTranslation.TabIndex = 4;
            // 
            // labelTranslation
            // 
            this.labelTranslation.AutoSize = true;
            this.labelTranslation.Location = new System.Drawing.Point(12, 48);
            this.labelTranslation.Name = "labelTranslation";
            this.labelTranslation.Size = new System.Drawing.Size(59, 13);
            this.labelTranslation.TabIndex = 3;
            this.labelTranslation.Text = "Translation";
            // 
            // textBoxLanguageShort
            // 
            this.textBoxLanguageShort.Location = new System.Drawing.Point(12, 103);
            this.textBoxLanguageShort.Name = "textBoxLanguageShort";
            this.textBoxLanguageShort.Size = new System.Drawing.Size(269, 20);
            this.textBoxLanguageShort.TabIndex = 6;
            // 
            // labelLanguageShort
            // 
            this.labelLanguageShort.AutoSize = true;
            this.labelLanguageShort.Location = new System.Drawing.Point(12, 87);
            this.labelLanguageShort.Name = "labelLanguageShort";
            this.labelLanguageShort.Size = new System.Drawing.Size(211, 13);
            this.labelLanguageShort.TabIndex = 5;
            this.labelLanguageShort.Text = "Short language tag (ISO639-1 or ISO639-2)";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(93, 148);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // labelExample
            // 
            this.labelExample.AutoSize = true;
            this.labelExample.Location = new System.Drawing.Point(12, 126);
            this.labelExample.Name = "labelExample";
            this.labelExample.Size = new System.Drawing.Size(160, 13);
            this.labelExample.TabIndex = 8;
            this.labelExample.Text = "Example: German - Deutsch - de";
            // 
            // LanguageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 183);
            this.Controls.Add(this.labelExample);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxLanguageShort);
            this.Controls.Add(this.labelLanguageShort);
            this.Controls.Add(this.textBoxTranslation);
            this.Controls.Add(this.labelTranslation);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxLanguage);
            this.Controls.Add(this.labelLanguage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LanguageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit language";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.TextBox textBoxLanguage;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.TextBox textBoxTranslation;
        private System.Windows.Forms.Label labelTranslation;
        private System.Windows.Forms.TextBox textBoxLanguageShort;
        private System.Windows.Forms.Label labelLanguageShort;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelExample;
    }
}