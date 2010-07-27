namespace BluRip
{
    partial class AutoCrop
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
            this.buttonFlash = new System.Windows.Forms.Button();
            this.numericUpDownCropTop = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCropBottom = new System.Windows.Forms.NumericUpDown();
            this.labelCropTop = new System.Windows.Forms.Label();
            this.labelCropBottom = new System.Windows.Forms.Label();
            this.numericUpDownFrame = new System.Windows.Forms.NumericUpDown();
            this.labelFrame = new System.Windows.Forms.Label();
            this.buttonCalc = new System.Windows.Forms.Button();
            this.panelManualCrop = new System.Windows.Forms.Panel();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCropTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCropBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).BeginInit();
            this.panelManualCrop.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonFlash
            // 
            this.buttonFlash.Location = new System.Drawing.Point(129, 3);
            this.buttonFlash.Name = "buttonFlash";
            this.buttonFlash.Size = new System.Drawing.Size(120, 23);
            this.buttonFlash.TabIndex = 2;
            this.buttonFlash.Text = "Flash";
            this.buttonFlash.UseVisualStyleBackColor = true;
            this.buttonFlash.Click += new System.EventHandler(this.buttonFlash_Click);
            // 
            // numericUpDownCropTop
            // 
            this.numericUpDownCropTop.Location = new System.Drawing.Point(3, 16);
            this.numericUpDownCropTop.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.numericUpDownCropTop.Name = "numericUpDownCropTop";
            this.numericUpDownCropTop.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownCropTop.TabIndex = 3;
            this.numericUpDownCropTop.ValueChanged += new System.EventHandler(this.numericUpDownCropTop_ValueChanged);
            // 
            // numericUpDownCropBottom
            // 
            this.numericUpDownCropBottom.Location = new System.Drawing.Point(3, 55);
            this.numericUpDownCropBottom.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.numericUpDownCropBottom.Name = "numericUpDownCropBottom";
            this.numericUpDownCropBottom.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownCropBottom.TabIndex = 4;
            this.numericUpDownCropBottom.ValueChanged += new System.EventHandler(this.numericUpDownCropBottom_ValueChanged);
            // 
            // labelCropTop
            // 
            this.labelCropTop.AutoSize = true;
            this.labelCropTop.Location = new System.Drawing.Point(3, 0);
            this.labelCropTop.Name = "labelCropTop";
            this.labelCropTop.Size = new System.Drawing.Size(50, 13);
            this.labelCropTop.TabIndex = 5;
            this.labelCropTop.Text = "Crop top:";
            // 
            // labelCropBottom
            // 
            this.labelCropBottom.AutoSize = true;
            this.labelCropBottom.Location = new System.Drawing.Point(3, 39);
            this.labelCropBottom.Name = "labelCropBottom";
            this.labelCropBottom.Size = new System.Drawing.Size(67, 13);
            this.labelCropBottom.TabIndex = 6;
            this.labelCropBottom.Text = "Crop bottom:";
            // 
            // numericUpDownFrame
            // 
            this.numericUpDownFrame.Location = new System.Drawing.Point(3, 94);
            this.numericUpDownFrame.Name = "numericUpDownFrame";
            this.numericUpDownFrame.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownFrame.TabIndex = 7;
            this.numericUpDownFrame.ValueChanged += new System.EventHandler(this.numericUpDownFrame_ValueChanged);
            // 
            // labelFrame
            // 
            this.labelFrame.AutoSize = true;
            this.labelFrame.Location = new System.Drawing.Point(3, 78);
            this.labelFrame.Name = "labelFrame";
            this.labelFrame.Size = new System.Drawing.Size(77, 13);
            this.labelFrame.TabIndex = 8;
            this.labelFrame.Text = "Frame number:";
            // 
            // buttonCalc
            // 
            this.buttonCalc.Location = new System.Drawing.Point(129, 29);
            this.buttonCalc.Name = "buttonCalc";
            this.buttonCalc.Size = new System.Drawing.Size(120, 23);
            this.buttonCalc.TabIndex = 9;
            this.buttonCalc.Text = "Calc values";
            this.buttonCalc.UseVisualStyleBackColor = true;
            this.buttonCalc.Click += new System.EventHandler(this.buttonCalc_Click);
            // 
            // panelManualCrop
            // 
            this.panelManualCrop.Controls.Add(this.buttonCancel);
            this.panelManualCrop.Controls.Add(this.buttonOk);
            this.panelManualCrop.Controls.Add(this.labelCropTop);
            this.panelManualCrop.Controls.Add(this.buttonCalc);
            this.panelManualCrop.Controls.Add(this.numericUpDownCropTop);
            this.panelManualCrop.Controls.Add(this.buttonFlash);
            this.panelManualCrop.Controls.Add(this.labelFrame);
            this.panelManualCrop.Controls.Add(this.numericUpDownCropBottom);
            this.panelManualCrop.Controls.Add(this.numericUpDownFrame);
            this.panelManualCrop.Controls.Add(this.labelCropBottom);
            this.panelManualCrop.Location = new System.Drawing.Point(12, 12);
            this.panelManualCrop.Name = "panelManualCrop";
            this.panelManualCrop.Size = new System.Drawing.Size(368, 121);
            this.panelManualCrop.TabIndex = 10;
            this.panelManualCrop.Visible = false;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(255, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(255, 29);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // AutoCrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 297);
            this.Controls.Add(this.panelManualCrop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AutoCrop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoCrop";
            this.Shown += new System.EventHandler(this.AutoCrop_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AutoCrop_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCropTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCropBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).EndInit();
            this.panelManualCrop.ResumeLayout(false);
            this.panelManualCrop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonFlash;
        private System.Windows.Forms.NumericUpDown numericUpDownCropTop;
        private System.Windows.Forms.NumericUpDown numericUpDownCropBottom;
        private System.Windows.Forms.Label labelCropTop;
        private System.Windows.Forms.Label labelCropBottom;
        private System.Windows.Forms.NumericUpDown numericUpDownFrame;
        private System.Windows.Forms.Label labelFrame;
        private System.Windows.Forms.Button buttonCalc;
        private System.Windows.Forms.Panel panelManualCrop;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;

    }
}