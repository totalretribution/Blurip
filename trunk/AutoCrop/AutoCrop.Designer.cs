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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.numericUpDownBorderTop = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBorderBottom = new System.Windows.Forms.NumericUpDown();
            this.labelBorderTop = new System.Windows.Forms.Label();
            this.labelBorderBottom = new System.Windows.Forms.Label();
            this.checkBoxAddBorders = new System.Windows.Forms.CheckBox();
            this.checkBoxResize = new System.Windows.Forms.CheckBox();
            this.labelResizeY = new System.Windows.Forms.Label();
            this.labelResizeX = new System.Windows.Forms.Label();
            this.numericUpDownResizeY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownResizeX = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCropTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCropBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).BeginInit();
            this.panelManualCrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResizeX)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonFlash
            // 
            this.buttonFlash.Location = new System.Drawing.Point(270, 61);
            this.buttonFlash.Name = "buttonFlash";
            this.buttonFlash.Size = new System.Drawing.Size(75, 23);
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
            this.numericUpDownCropTop.Size = new System.Drawing.Size(77, 20);
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
            this.numericUpDownCropBottom.Size = new System.Drawing.Size(77, 20);
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
            this.numericUpDownFrame.Size = new System.Drawing.Size(77, 20);
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
            this.buttonCalc.Location = new System.Drawing.Point(270, 90);
            this.buttonCalc.Name = "buttonCalc";
            this.buttonCalc.Size = new System.Drawing.Size(75, 23);
            this.buttonCalc.TabIndex = 9;
            this.buttonCalc.Text = "Calc values";
            this.buttonCalc.UseVisualStyleBackColor = true;
            this.buttonCalc.Click += new System.EventHandler(this.buttonCalc_Click);
            // 
            // panelManualCrop
            // 
            this.panelManualCrop.Controls.Add(this.checkBoxResize);
            this.panelManualCrop.Controls.Add(this.buttonCalc);
            this.panelManualCrop.Controls.Add(this.buttonFlash);
            this.panelManualCrop.Controls.Add(this.labelResizeY);
            this.panelManualCrop.Controls.Add(this.labelResizeX);
            this.panelManualCrop.Controls.Add(this.numericUpDownResizeY);
            this.panelManualCrop.Controls.Add(this.numericUpDownResizeX);
            this.panelManualCrop.Controls.Add(this.checkBoxAddBorders);
            this.panelManualCrop.Controls.Add(this.labelBorderBottom);
            this.panelManualCrop.Controls.Add(this.labelBorderTop);
            this.panelManualCrop.Controls.Add(this.numericUpDownBorderBottom);
            this.panelManualCrop.Controls.Add(this.numericUpDownBorderTop);
            this.panelManualCrop.Controls.Add(this.buttonCancel);
            this.panelManualCrop.Controls.Add(this.buttonOk);
            this.panelManualCrop.Controls.Add(this.labelCropTop);
            this.panelManualCrop.Controls.Add(this.numericUpDownCropTop);
            this.panelManualCrop.Controls.Add(this.labelFrame);
            this.panelManualCrop.Controls.Add(this.numericUpDownCropBottom);
            this.panelManualCrop.Controls.Add(this.numericUpDownFrame);
            this.panelManualCrop.Controls.Add(this.labelCropBottom);
            this.panelManualCrop.Location = new System.Drawing.Point(12, 12);
            this.panelManualCrop.Name = "panelManualCrop";
            this.panelManualCrop.Size = new System.Drawing.Size(354, 121);
            this.panelManualCrop.TabIndex = 10;
            this.panelManualCrop.Visible = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(270, 32);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(270, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // numericUpDownBorderTop
            // 
            this.numericUpDownBorderTop.Location = new System.Drawing.Point(86, 16);
            this.numericUpDownBorderTop.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.numericUpDownBorderTop.Name = "numericUpDownBorderTop";
            this.numericUpDownBorderTop.Size = new System.Drawing.Size(77, 20);
            this.numericUpDownBorderTop.TabIndex = 11;
            this.numericUpDownBorderTop.ValueChanged += new System.EventHandler(this.numericUpDownBorderTop_ValueChanged);
            // 
            // numericUpDownBorderBottom
            // 
            this.numericUpDownBorderBottom.Location = new System.Drawing.Point(86, 55);
            this.numericUpDownBorderBottom.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.numericUpDownBorderBottom.Name = "numericUpDownBorderBottom";
            this.numericUpDownBorderBottom.Size = new System.Drawing.Size(77, 20);
            this.numericUpDownBorderBottom.TabIndex = 12;
            this.numericUpDownBorderBottom.ValueChanged += new System.EventHandler(this.numericUpDownBorderBottom_ValueChanged);
            // 
            // labelBorderTop
            // 
            this.labelBorderTop.AutoSize = true;
            this.labelBorderTop.Location = new System.Drawing.Point(86, 0);
            this.labelBorderTop.Name = "labelBorderTop";
            this.labelBorderTop.Size = new System.Drawing.Size(78, 13);
            this.labelBorderTop.TabIndex = 11;
            this.labelBorderTop.Text = "AddBorder top:";
            // 
            // labelBorderBottom
            // 
            this.labelBorderBottom.AutoSize = true;
            this.labelBorderBottom.Location = new System.Drawing.Point(86, 39);
            this.labelBorderBottom.Name = "labelBorderBottom";
            this.labelBorderBottom.Size = new System.Drawing.Size(95, 13);
            this.labelBorderBottom.TabIndex = 13;
            this.labelBorderBottom.Text = "AddBorder bottom:";
            // 
            // checkBoxAddBorders
            // 
            this.checkBoxAddBorders.AutoSize = true;
            this.checkBoxAddBorders.Location = new System.Drawing.Point(89, 94);
            this.checkBoxAddBorders.Name = "checkBoxAddBorders";
            this.checkBoxAddBorders.Size = new System.Drawing.Size(81, 17);
            this.checkBoxAddBorders.TabIndex = 11;
            this.checkBoxAddBorders.Text = "AddBorders";
            this.checkBoxAddBorders.UseVisualStyleBackColor = true;
            this.checkBoxAddBorders.CheckedChanged += new System.EventHandler(this.checkBoxAddBorders_CheckedChanged);
            // 
            // checkBoxResize
            // 
            this.checkBoxResize.AutoSize = true;
            this.checkBoxResize.Location = new System.Drawing.Point(190, 94);
            this.checkBoxResize.Name = "checkBoxResize";
            this.checkBoxResize.Size = new System.Drawing.Size(58, 17);
            this.checkBoxResize.TabIndex = 16;
            this.checkBoxResize.Text = "Resize";
            this.checkBoxResize.UseVisualStyleBackColor = true;
            this.checkBoxResize.CheckedChanged += new System.EventHandler(this.checkBoxResize_CheckedChanged);
            // 
            // labelResizeY
            // 
            this.labelResizeY.AutoSize = true;
            this.labelResizeY.Location = new System.Drawing.Point(187, 39);
            this.labelResizeY.Name = "labelResizeY";
            this.labelResizeY.Size = new System.Drawing.Size(50, 13);
            this.labelResizeY.TabIndex = 18;
            this.labelResizeY.Text = "Resize y:";
            // 
            // labelResizeX
            // 
            this.labelResizeX.AutoSize = true;
            this.labelResizeX.Location = new System.Drawing.Point(187, 0);
            this.labelResizeX.Name = "labelResizeX";
            this.labelResizeX.Size = new System.Drawing.Size(50, 13);
            this.labelResizeX.TabIndex = 14;
            this.labelResizeX.Text = "Resize x:";
            // 
            // numericUpDownResizeY
            // 
            this.numericUpDownResizeY.Location = new System.Drawing.Point(187, 55);
            this.numericUpDownResizeY.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.numericUpDownResizeY.Name = "numericUpDownResizeY";
            this.numericUpDownResizeY.Size = new System.Drawing.Size(77, 20);
            this.numericUpDownResizeY.TabIndex = 17;
            this.numericUpDownResizeY.ValueChanged += new System.EventHandler(this.numericUpDownResizeY_ValueChanged);
            // 
            // numericUpDownResizeX
            // 
            this.numericUpDownResizeX.Location = new System.Drawing.Point(187, 16);
            this.numericUpDownResizeX.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.numericUpDownResizeX.Name = "numericUpDownResizeX";
            this.numericUpDownResizeX.Size = new System.Drawing.Size(77, 20);
            this.numericUpDownResizeX.TabIndex = 15;
            this.numericUpDownResizeX.ValueChanged += new System.EventHandler(this.numericUpDownResizeX_ValueChanged);
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBorderBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResizeX)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBoxAddBorders;
        private System.Windows.Forms.Label labelBorderBottom;
        private System.Windows.Forms.Label labelBorderTop;
        private System.Windows.Forms.NumericUpDown numericUpDownBorderBottom;
        private System.Windows.Forms.NumericUpDown numericUpDownBorderTop;
        private System.Windows.Forms.CheckBox checkBoxResize;
        private System.Windows.Forms.Label labelResizeY;
        private System.Windows.Forms.Label labelResizeX;
        private System.Windows.Forms.NumericUpDown numericUpDownResizeY;
        private System.Windows.Forms.NumericUpDown numericUpDownResizeX;

    }
}