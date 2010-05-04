namespace BluRip
{
    partial class AdvancedVideoOptionsEdit
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
            this.groupBoxFps = new System.Windows.Forms.GroupBox();
            this.labelFramerate = new System.Windows.Forms.Label();
            this.comboBoxFramerate = new System.Windows.Forms.ComboBox();
            this.textBoxFps = new System.Windows.Forms.TextBox();
            this.groupBoxAutocrop = new System.Windows.Forms.GroupBox();
            this.checkBoxManualResize = new System.Windows.Forms.CheckBox();
            this.checkBoxManualBorders = new System.Windows.Forms.CheckBox();
            this.checkBoxManualCrop = new System.Windows.Forms.CheckBox();
            this.groupBoxResize = new System.Windows.Forms.GroupBox();
            this.textBoxSizeY = new System.Windows.Forms.TextBox();
            this.labelSizeY = new System.Windows.Forms.Label();
            this.textBoxSizeX = new System.Windows.Forms.TextBox();
            this.labelSizeX = new System.Windows.Forms.Label();
            this.groupBoxAddBorders = new System.Windows.Forms.GroupBox();
            this.textBoxBorderBottom = new System.Windows.Forms.TextBox();
            this.labelBorderBottom = new System.Windows.Forms.Label();
            this.textBoxBorderTop = new System.Windows.Forms.TextBox();
            this.labelBorderTop = new System.Windows.Forms.Label();
            this.textBoxBorderRight = new System.Windows.Forms.TextBox();
            this.labelBorderRight = new System.Windows.Forms.Label();
            this.textBoxBorderLeft = new System.Windows.Forms.TextBox();
            this.labelBorderLeft = new System.Windows.Forms.Label();
            this.groupBoxCrop = new System.Windows.Forms.GroupBox();
            this.textBoxCropBottom = new System.Windows.Forms.TextBox();
            this.labelCropBottom = new System.Windows.Forms.Label();
            this.textBoxCropTop = new System.Windows.Forms.TextBox();
            this.labelCropTop = new System.Windows.Forms.Label();
            this.textBoxCropRight = new System.Windows.Forms.TextBox();
            this.labelCropRight = new System.Windows.Forms.Label();
            this.textBoxCropLeft = new System.Windows.Forms.TextBox();
            this.labelCropLeft = new System.Windows.Forms.Label();
            this.checkBoxManualFps = new System.Windows.Forms.CheckBox();
            this.checkBoxManualAutoCrop = new System.Windows.Forms.CheckBox();
            this.groupBoxInputResolution = new System.Windows.Forms.GroupBox();
            this.textBoxInputResY = new System.Windows.Forms.TextBox();
            this.labelInputResY = new System.Windows.Forms.Label();
            this.textBoxInputResX = new System.Windows.Forms.TextBox();
            this.labelInputResX = new System.Windows.Forms.Label();
            this.checkBoxManualInputRes = new System.Windows.Forms.CheckBox();
            this.checkBoxnoMkvDemux = new System.Windows.Forms.CheckBox();
            this.groupBoxNoMkvDemux = new System.Windows.Forms.GroupBox();
            this.labelChooseVideoExtension = new System.Windows.Forms.Label();
            this.comboBoxVideoExtension = new System.Windows.Forms.ComboBox();
            this.labelVideoExtension = new System.Windows.Forms.Label();
            this.textBoxVideoExtension = new System.Windows.Forms.TextBox();
            this.groupBoxFps.SuspendLayout();
            this.groupBoxAutocrop.SuspendLayout();
            this.groupBoxResize.SuspendLayout();
            this.groupBoxAddBorders.SuspendLayout();
            this.groupBoxCrop.SuspendLayout();
            this.groupBoxInputResolution.SuspendLayout();
            this.groupBoxNoMkvDemux.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(12, 408);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(93, 408);
            // 
            // groupBoxFps
            // 
            this.groupBoxFps.Controls.Add(this.labelFramerate);
            this.groupBoxFps.Controls.Add(this.comboBoxFramerate);
            this.groupBoxFps.Controls.Add(this.textBoxFps);
            this.groupBoxFps.Location = new System.Drawing.Point(12, 35);
            this.groupBoxFps.Name = "groupBoxFps";
            this.groupBoxFps.Size = new System.Drawing.Size(156, 61);
            this.groupBoxFps.TabIndex = 2;
            this.groupBoxFps.TabStop = false;
            this.groupBoxFps.Text = "Framerate";
            // 
            // labelFramerate
            // 
            this.labelFramerate.AutoSize = true;
            this.labelFramerate.Location = new System.Drawing.Point(78, 15);
            this.labelFramerate.Name = "labelFramerate";
            this.labelFramerate.Size = new System.Drawing.Size(66, 13);
            this.labelFramerate.TabIndex = 2;
            this.labelFramerate.Text = "Choose from";
            // 
            // comboBoxFramerate
            // 
            this.comboBoxFramerate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFramerate.FormattingEnabled = true;
            this.comboBoxFramerate.Items.AddRange(new object[] {
            "23.976",
            "25.0",
            "29.967"});
            this.comboBoxFramerate.Location = new System.Drawing.Point(81, 31);
            this.comboBoxFramerate.Name = "comboBoxFramerate";
            this.comboBoxFramerate.Size = new System.Drawing.Size(69, 21);
            this.comboBoxFramerate.TabIndex = 1;
            this.comboBoxFramerate.SelectedIndexChanged += new System.EventHandler(this.comboBoxFramerate_SelectedIndexChanged);
            // 
            // textBoxFps
            // 
            this.textBoxFps.Location = new System.Drawing.Point(6, 32);
            this.textBoxFps.Name = "textBoxFps";
            this.textBoxFps.Size = new System.Drawing.Size(69, 20);
            this.textBoxFps.TabIndex = 0;
            this.textBoxFps.TextChanged += new System.EventHandler(this.textBoxFps_TextChanged);
            // 
            // groupBoxAutocrop
            // 
            this.groupBoxAutocrop.Controls.Add(this.checkBoxManualResize);
            this.groupBoxAutocrop.Controls.Add(this.checkBoxManualBorders);
            this.groupBoxAutocrop.Controls.Add(this.checkBoxManualCrop);
            this.groupBoxAutocrop.Controls.Add(this.groupBoxResize);
            this.groupBoxAutocrop.Controls.Add(this.groupBoxAddBorders);
            this.groupBoxAutocrop.Controls.Add(this.groupBoxCrop);
            this.groupBoxAutocrop.Location = new System.Drawing.Point(12, 125);
            this.groupBoxAutocrop.Name = "groupBoxAutocrop";
            this.groupBoxAutocrop.Size = new System.Drawing.Size(316, 278);
            this.groupBoxAutocrop.TabIndex = 3;
            this.groupBoxAutocrop.TabStop = false;
            this.groupBoxAutocrop.Text = "Autocrop";
            // 
            // checkBoxManualResize
            // 
            this.checkBoxManualResize.AutoSize = true;
            this.checkBoxManualResize.Location = new System.Drawing.Point(6, 189);
            this.checkBoxManualResize.Name = "checkBoxManualResize";
            this.checkBoxManualResize.Size = new System.Drawing.Size(150, 17);
            this.checkBoxManualResize.TabIndex = 4;
            this.checkBoxManualResize.Text = "Set resize values manually";
            this.checkBoxManualResize.UseVisualStyleBackColor = true;
            this.checkBoxManualResize.CheckedChanged += new System.EventHandler(this.checkBoxManualResize_CheckedChanged);
            // 
            // checkBoxManualBorders
            // 
            this.checkBoxManualBorders.AutoSize = true;
            this.checkBoxManualBorders.Location = new System.Drawing.Point(6, 108);
            this.checkBoxManualBorders.Name = "checkBoxManualBorders";
            this.checkBoxManualBorders.Size = new System.Drawing.Size(179, 17);
            this.checkBoxManualBorders.TabIndex = 3;
            this.checkBoxManualBorders.Text = "Set add borders values manually";
            this.checkBoxManualBorders.UseVisualStyleBackColor = true;
            this.checkBoxManualBorders.CheckedChanged += new System.EventHandler(this.checkBoxManualBorders_CheckedChanged);
            // 
            // checkBoxManualCrop
            // 
            this.checkBoxManualCrop.AutoSize = true;
            this.checkBoxManualCrop.Location = new System.Drawing.Point(6, 19);
            this.checkBoxManualCrop.Name = "checkBoxManualCrop";
            this.checkBoxManualCrop.Size = new System.Drawing.Size(144, 17);
            this.checkBoxManualCrop.TabIndex = 2;
            this.checkBoxManualCrop.Text = "Set crop values manually";
            this.checkBoxManualCrop.UseVisualStyleBackColor = true;
            this.checkBoxManualCrop.CheckedChanged += new System.EventHandler(this.checkBoxManualCrop_CheckedChanged);
            // 
            // groupBoxResize
            // 
            this.groupBoxResize.Controls.Add(this.textBoxSizeY);
            this.groupBoxResize.Controls.Add(this.labelSizeY);
            this.groupBoxResize.Controls.Add(this.textBoxSizeX);
            this.groupBoxResize.Controls.Add(this.labelSizeX);
            this.groupBoxResize.Location = new System.Drawing.Point(6, 212);
            this.groupBoxResize.Name = "groupBoxResize";
            this.groupBoxResize.Size = new System.Drawing.Size(304, 59);
            this.groupBoxResize.TabIndex = 1;
            this.groupBoxResize.TabStop = false;
            this.groupBoxResize.Text = "Resize";
            // 
            // textBoxSizeY
            // 
            this.textBoxSizeY.Location = new System.Drawing.Point(58, 32);
            this.textBoxSizeY.Name = "textBoxSizeY";
            this.textBoxSizeY.Size = new System.Drawing.Size(46, 20);
            this.textBoxSizeY.TabIndex = 15;
            this.textBoxSizeY.TextChanged += new System.EventHandler(this.textBoxSizeY_TextChanged);
            // 
            // labelSizeY
            // 
            this.labelSizeY.AutoSize = true;
            this.labelSizeY.Location = new System.Drawing.Point(58, 16);
            this.labelSizeY.Name = "labelSizeY";
            this.labelSizeY.Size = new System.Drawing.Size(38, 13);
            this.labelSizeY.TabIndex = 14;
            this.labelSizeY.Text = "Heigth";
            // 
            // textBoxSizeX
            // 
            this.textBoxSizeX.Location = new System.Drawing.Point(6, 32);
            this.textBoxSizeX.Name = "textBoxSizeX";
            this.textBoxSizeX.Size = new System.Drawing.Size(46, 20);
            this.textBoxSizeX.TabIndex = 13;
            this.textBoxSizeX.TextChanged += new System.EventHandler(this.textBoxSizeX_TextChanged);
            // 
            // labelSizeX
            // 
            this.labelSizeX.AutoSize = true;
            this.labelSizeX.Location = new System.Drawing.Point(6, 16);
            this.labelSizeX.Name = "labelSizeX";
            this.labelSizeX.Size = new System.Drawing.Size(35, 13);
            this.labelSizeX.TabIndex = 12;
            this.labelSizeX.Text = "Width";
            // 
            // groupBoxAddBorders
            // 
            this.groupBoxAddBorders.Controls.Add(this.textBoxBorderBottom);
            this.groupBoxAddBorders.Controls.Add(this.labelBorderBottom);
            this.groupBoxAddBorders.Controls.Add(this.textBoxBorderTop);
            this.groupBoxAddBorders.Controls.Add(this.labelBorderTop);
            this.groupBoxAddBorders.Controls.Add(this.textBoxBorderRight);
            this.groupBoxAddBorders.Controls.Add(this.labelBorderRight);
            this.groupBoxAddBorders.Controls.Add(this.textBoxBorderLeft);
            this.groupBoxAddBorders.Controls.Add(this.labelBorderLeft);
            this.groupBoxAddBorders.Location = new System.Drawing.Point(6, 131);
            this.groupBoxAddBorders.Name = "groupBoxAddBorders";
            this.groupBoxAddBorders.Size = new System.Drawing.Size(304, 61);
            this.groupBoxAddBorders.TabIndex = 1;
            this.groupBoxAddBorders.TabStop = false;
            this.groupBoxAddBorders.Text = "Add borders (only positive values)";
            // 
            // textBoxBorderBottom
            // 
            this.textBoxBorderBottom.Location = new System.Drawing.Point(162, 32);
            this.textBoxBorderBottom.Name = "textBoxBorderBottom";
            this.textBoxBorderBottom.Size = new System.Drawing.Size(46, 20);
            this.textBoxBorderBottom.TabIndex = 15;
            this.textBoxBorderBottom.TextChanged += new System.EventHandler(this.textBoxBorderBottom_TextChanged);
            // 
            // labelBorderBottom
            // 
            this.labelBorderBottom.AutoSize = true;
            this.labelBorderBottom.Location = new System.Drawing.Point(162, 16);
            this.labelBorderBottom.Name = "labelBorderBottom";
            this.labelBorderBottom.Size = new System.Drawing.Size(40, 13);
            this.labelBorderBottom.TabIndex = 14;
            this.labelBorderBottom.Text = "Bottom";
            // 
            // textBoxBorderTop
            // 
            this.textBoxBorderTop.Location = new System.Drawing.Point(110, 32);
            this.textBoxBorderTop.Name = "textBoxBorderTop";
            this.textBoxBorderTop.Size = new System.Drawing.Size(46, 20);
            this.textBoxBorderTop.TabIndex = 13;
            this.textBoxBorderTop.TextChanged += new System.EventHandler(this.textBoxBorderTop_TextChanged);
            // 
            // labelBorderTop
            // 
            this.labelBorderTop.AutoSize = true;
            this.labelBorderTop.Location = new System.Drawing.Point(110, 16);
            this.labelBorderTop.Name = "labelBorderTop";
            this.labelBorderTop.Size = new System.Drawing.Size(26, 13);
            this.labelBorderTop.TabIndex = 12;
            this.labelBorderTop.Text = "Top";
            // 
            // textBoxBorderRight
            // 
            this.textBoxBorderRight.Location = new System.Drawing.Point(58, 32);
            this.textBoxBorderRight.Name = "textBoxBorderRight";
            this.textBoxBorderRight.Size = new System.Drawing.Size(46, 20);
            this.textBoxBorderRight.TabIndex = 11;
            this.textBoxBorderRight.TextChanged += new System.EventHandler(this.textBoxBorderRight_TextChanged);
            // 
            // labelBorderRight
            // 
            this.labelBorderRight.AutoSize = true;
            this.labelBorderRight.Location = new System.Drawing.Point(58, 16);
            this.labelBorderRight.Name = "labelBorderRight";
            this.labelBorderRight.Size = new System.Drawing.Size(32, 13);
            this.labelBorderRight.TabIndex = 10;
            this.labelBorderRight.Text = "Right";
            // 
            // textBoxBorderLeft
            // 
            this.textBoxBorderLeft.Location = new System.Drawing.Point(6, 32);
            this.textBoxBorderLeft.Name = "textBoxBorderLeft";
            this.textBoxBorderLeft.Size = new System.Drawing.Size(46, 20);
            this.textBoxBorderLeft.TabIndex = 9;
            this.textBoxBorderLeft.TextChanged += new System.EventHandler(this.textBoxBorderLeft_TextChanged);
            // 
            // labelBorderLeft
            // 
            this.labelBorderLeft.AutoSize = true;
            this.labelBorderLeft.Location = new System.Drawing.Point(6, 16);
            this.labelBorderLeft.Name = "labelBorderLeft";
            this.labelBorderLeft.Size = new System.Drawing.Size(25, 13);
            this.labelBorderLeft.TabIndex = 8;
            this.labelBorderLeft.Text = "Left";
            // 
            // groupBoxCrop
            // 
            this.groupBoxCrop.Controls.Add(this.textBoxCropBottom);
            this.groupBoxCrop.Controls.Add(this.labelCropBottom);
            this.groupBoxCrop.Controls.Add(this.textBoxCropTop);
            this.groupBoxCrop.Controls.Add(this.labelCropTop);
            this.groupBoxCrop.Controls.Add(this.textBoxCropRight);
            this.groupBoxCrop.Controls.Add(this.labelCropRight);
            this.groupBoxCrop.Controls.Add(this.textBoxCropLeft);
            this.groupBoxCrop.Controls.Add(this.labelCropLeft);
            this.groupBoxCrop.Location = new System.Drawing.Point(6, 42);
            this.groupBoxCrop.Name = "groupBoxCrop";
            this.groupBoxCrop.Size = new System.Drawing.Size(304, 60);
            this.groupBoxCrop.TabIndex = 0;
            this.groupBoxCrop.TabStop = false;
            this.groupBoxCrop.Text = "Crop (only positive values)";
            // 
            // textBoxCropBottom
            // 
            this.textBoxCropBottom.Location = new System.Drawing.Point(162, 32);
            this.textBoxCropBottom.Name = "textBoxCropBottom";
            this.textBoxCropBottom.Size = new System.Drawing.Size(46, 20);
            this.textBoxCropBottom.TabIndex = 7;
            this.textBoxCropBottom.TextChanged += new System.EventHandler(this.textBoxCropBottom_TextChanged);
            // 
            // labelCropBottom
            // 
            this.labelCropBottom.AutoSize = true;
            this.labelCropBottom.Location = new System.Drawing.Point(162, 16);
            this.labelCropBottom.Name = "labelCropBottom";
            this.labelCropBottom.Size = new System.Drawing.Size(40, 13);
            this.labelCropBottom.TabIndex = 6;
            this.labelCropBottom.Text = "Bottom";
            // 
            // textBoxCropTop
            // 
            this.textBoxCropTop.Location = new System.Drawing.Point(110, 32);
            this.textBoxCropTop.Name = "textBoxCropTop";
            this.textBoxCropTop.Size = new System.Drawing.Size(46, 20);
            this.textBoxCropTop.TabIndex = 5;
            this.textBoxCropTop.TextChanged += new System.EventHandler(this.textBoxCropTop_TextChanged);
            // 
            // labelCropTop
            // 
            this.labelCropTop.AutoSize = true;
            this.labelCropTop.Location = new System.Drawing.Point(110, 16);
            this.labelCropTop.Name = "labelCropTop";
            this.labelCropTop.Size = new System.Drawing.Size(26, 13);
            this.labelCropTop.TabIndex = 4;
            this.labelCropTop.Text = "Top";
            // 
            // textBoxCropRight
            // 
            this.textBoxCropRight.Location = new System.Drawing.Point(58, 32);
            this.textBoxCropRight.Name = "textBoxCropRight";
            this.textBoxCropRight.Size = new System.Drawing.Size(46, 20);
            this.textBoxCropRight.TabIndex = 3;
            this.textBoxCropRight.TextChanged += new System.EventHandler(this.textBoxCropRight_TextChanged);
            // 
            // labelCropRight
            // 
            this.labelCropRight.AutoSize = true;
            this.labelCropRight.Location = new System.Drawing.Point(58, 16);
            this.labelCropRight.Name = "labelCropRight";
            this.labelCropRight.Size = new System.Drawing.Size(32, 13);
            this.labelCropRight.TabIndex = 2;
            this.labelCropRight.Text = "Right";
            // 
            // textBoxCropLeft
            // 
            this.textBoxCropLeft.Location = new System.Drawing.Point(6, 32);
            this.textBoxCropLeft.Name = "textBoxCropLeft";
            this.textBoxCropLeft.Size = new System.Drawing.Size(46, 20);
            this.textBoxCropLeft.TabIndex = 1;
            this.textBoxCropLeft.TextChanged += new System.EventHandler(this.textBoxCropLeft_TextChanged);
            // 
            // labelCropLeft
            // 
            this.labelCropLeft.AutoSize = true;
            this.labelCropLeft.Location = new System.Drawing.Point(6, 16);
            this.labelCropLeft.Name = "labelCropLeft";
            this.labelCropLeft.Size = new System.Drawing.Size(25, 13);
            this.labelCropLeft.TabIndex = 0;
            this.labelCropLeft.Text = "Left";
            // 
            // checkBoxManualFps
            // 
            this.checkBoxManualFps.AutoSize = true;
            this.checkBoxManualFps.Location = new System.Drawing.Point(8, 12);
            this.checkBoxManualFps.Name = "checkBoxManualFps";
            this.checkBoxManualFps.Size = new System.Drawing.Size(133, 17);
            this.checkBoxManualFps.TabIndex = 4;
            this.checkBoxManualFps.Text = "Set framerate manually";
            this.checkBoxManualFps.UseVisualStyleBackColor = true;
            this.checkBoxManualFps.CheckedChanged += new System.EventHandler(this.checkBoxManualFps_CheckedChanged);
            // 
            // checkBoxManualAutoCrop
            // 
            this.checkBoxManualAutoCrop.AutoSize = true;
            this.checkBoxManualAutoCrop.Location = new System.Drawing.Point(8, 102);
            this.checkBoxManualAutoCrop.Name = "checkBoxManualAutoCrop";
            this.checkBoxManualAutoCrop.Size = new System.Drawing.Size(106, 17);
            this.checkBoxManualAutoCrop.TabIndex = 5;
            this.checkBoxManualAutoCrop.Text = "Disable autocrop";
            this.checkBoxManualAutoCrop.UseVisualStyleBackColor = true;
            this.checkBoxManualAutoCrop.CheckedChanged += new System.EventHandler(this.checkBoxManualAutoCrop_CheckedChanged);
            // 
            // groupBoxInputResolution
            // 
            this.groupBoxInputResolution.Controls.Add(this.textBoxInputResY);
            this.groupBoxInputResolution.Controls.Add(this.labelInputResY);
            this.groupBoxInputResolution.Controls.Add(this.textBoxInputResX);
            this.groupBoxInputResolution.Controls.Add(this.labelInputResX);
            this.groupBoxInputResolution.Location = new System.Drawing.Point(174, 35);
            this.groupBoxInputResolution.Name = "groupBoxInputResolution";
            this.groupBoxInputResolution.Size = new System.Drawing.Size(154, 61);
            this.groupBoxInputResolution.TabIndex = 6;
            this.groupBoxInputResolution.TabStop = false;
            this.groupBoxInputResolution.Text = "Input resolution";
            // 
            // textBoxInputResY
            // 
            this.textBoxInputResY.Location = new System.Drawing.Point(71, 31);
            this.textBoxInputResY.Name = "textBoxInputResY";
            this.textBoxInputResY.Size = new System.Drawing.Size(59, 20);
            this.textBoxInputResY.TabIndex = 3;
            this.textBoxInputResY.TextChanged += new System.EventHandler(this.textBoxInputResY_TextChanged);
            // 
            // labelInputResY
            // 
            this.labelInputResY.AutoSize = true;
            this.labelInputResY.Location = new System.Drawing.Point(71, 16);
            this.labelInputResY.Name = "labelInputResY";
            this.labelInputResY.Size = new System.Drawing.Size(14, 13);
            this.labelInputResY.TabIndex = 2;
            this.labelInputResY.Text = "Y";
            // 
            // textBoxInputResX
            // 
            this.textBoxInputResX.Location = new System.Drawing.Point(6, 31);
            this.textBoxInputResX.Name = "textBoxInputResX";
            this.textBoxInputResX.Size = new System.Drawing.Size(59, 20);
            this.textBoxInputResX.TabIndex = 1;
            this.textBoxInputResX.TextChanged += new System.EventHandler(this.textBoxInputResX_TextChanged);
            // 
            // labelInputResX
            // 
            this.labelInputResX.AutoSize = true;
            this.labelInputResX.Location = new System.Drawing.Point(6, 16);
            this.labelInputResX.Name = "labelInputResX";
            this.labelInputResX.Size = new System.Drawing.Size(14, 13);
            this.labelInputResX.TabIndex = 0;
            this.labelInputResX.Text = "X";
            // 
            // checkBoxManualInputRes
            // 
            this.checkBoxManualInputRes.AutoSize = true;
            this.checkBoxManualInputRes.Location = new System.Drawing.Point(180, 12);
            this.checkBoxManualInputRes.Name = "checkBoxManualInputRes";
            this.checkBoxManualInputRes.Size = new System.Drawing.Size(160, 17);
            this.checkBoxManualInputRes.TabIndex = 7;
            this.checkBoxManualInputRes.Text = "Set input resolution manually";
            this.checkBoxManualInputRes.UseVisualStyleBackColor = true;
            this.checkBoxManualInputRes.CheckedChanged += new System.EventHandler(this.checkBoxManualInputRes_CheckedChanged);
            // 
            // checkBoxnoMkvDemux
            // 
            this.checkBoxnoMkvDemux.AutoSize = true;
            this.checkBoxnoMkvDemux.Location = new System.Drawing.Point(12, 409);
            this.checkBoxnoMkvDemux.Name = "checkBoxnoMkvDemux";
            this.checkBoxnoMkvDemux.Size = new System.Drawing.Size(130, 17);
            this.checkBoxnoMkvDemux.TabIndex = 8;
            this.checkBoxnoMkvDemux.Text = "Do not demux to .mkv";
            this.checkBoxnoMkvDemux.UseVisualStyleBackColor = true;
            this.checkBoxnoMkvDemux.Visible = false;
            this.checkBoxnoMkvDemux.CheckedChanged += new System.EventHandler(this.checkBoxnoMkvDemux_CheckedChanged);
            // 
            // groupBoxNoMkvDemux
            // 
            this.groupBoxNoMkvDemux.Controls.Add(this.labelChooseVideoExtension);
            this.groupBoxNoMkvDemux.Controls.Add(this.comboBoxVideoExtension);
            this.groupBoxNoMkvDemux.Controls.Add(this.labelVideoExtension);
            this.groupBoxNoMkvDemux.Controls.Add(this.textBoxVideoExtension);
            this.groupBoxNoMkvDemux.Location = new System.Drawing.Point(12, 432);
            this.groupBoxNoMkvDemux.Name = "groupBoxNoMkvDemux";
            this.groupBoxNoMkvDemux.Size = new System.Drawing.Size(316, 59);
            this.groupBoxNoMkvDemux.TabIndex = 9;
            this.groupBoxNoMkvDemux.TabStop = false;
            this.groupBoxNoMkvDemux.Text = "Demuxing options";
            this.groupBoxNoMkvDemux.Visible = false;
            // 
            // labelChooseVideoExtension
            // 
            this.labelChooseVideoExtension.AutoSize = true;
            this.labelChooseVideoExtension.Location = new System.Drawing.Point(132, 16);
            this.labelChooseVideoExtension.Name = "labelChooseVideoExtension";
            this.labelChooseVideoExtension.Size = new System.Drawing.Size(66, 13);
            this.labelChooseVideoExtension.TabIndex = 3;
            this.labelChooseVideoExtension.Text = "Choose from";
            // 
            // comboBoxVideoExtension
            // 
            this.comboBoxVideoExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxVideoExtension.FormattingEnabled = true;
            this.comboBoxVideoExtension.Items.AddRange(new object[] {
            ".vc1",
            ".h264",
            ".d2v"});
            this.comboBoxVideoExtension.Location = new System.Drawing.Point(135, 31);
            this.comboBoxVideoExtension.Name = "comboBoxVideoExtension";
            this.comboBoxVideoExtension.Size = new System.Drawing.Size(121, 21);
            this.comboBoxVideoExtension.TabIndex = 2;
            this.comboBoxVideoExtension.SelectedIndexChanged += new System.EventHandler(this.comboBoxVideoExtension_SelectedIndexChanged);
            // 
            // labelVideoExtension
            // 
            this.labelVideoExtension.AutoSize = true;
            this.labelVideoExtension.Location = new System.Drawing.Point(6, 16);
            this.labelVideoExtension.Name = "labelVideoExtension";
            this.labelVideoExtension.Size = new System.Drawing.Size(82, 13);
            this.labelVideoExtension.TabIndex = 1;
            this.labelVideoExtension.Text = "Video extension";
            // 
            // textBoxVideoExtension
            // 
            this.textBoxVideoExtension.Location = new System.Drawing.Point(6, 32);
            this.textBoxVideoExtension.Name = "textBoxVideoExtension";
            this.textBoxVideoExtension.Size = new System.Drawing.Size(123, 20);
            this.textBoxVideoExtension.TabIndex = 0;
            this.textBoxVideoExtension.TextChanged += new System.EventHandler(this.textBoxVideoExtension_TextChanged);
            // 
            // AdvancedVideoOptionsEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(340, 443);
            this.Controls.Add(this.checkBoxManualInputRes);
            this.Controls.Add(this.groupBoxInputResolution);
            this.Controls.Add(this.checkBoxManualFps);
            this.Controls.Add(this.checkBoxManualAutoCrop);
            this.Controls.Add(this.groupBoxAutocrop);
            this.Controls.Add(this.groupBoxFps);
            this.Controls.Add(this.groupBoxNoMkvDemux);
            this.Controls.Add(this.checkBoxnoMkvDemux);
            this.Name = "AdvancedVideoOptionsEdit";
            this.Text = "Edit advanced video options";
            this.Shown += new System.EventHandler(this.AdvancedVideoOptionsEdit_Shown);
            this.Controls.SetChildIndex(this.checkBoxnoMkvDemux, 0);
            this.Controls.SetChildIndex(this.groupBoxNoMkvDemux, 0);
            this.Controls.SetChildIndex(this.buttonOk, 0);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.Controls.SetChildIndex(this.groupBoxFps, 0);
            this.Controls.SetChildIndex(this.groupBoxAutocrop, 0);
            this.Controls.SetChildIndex(this.checkBoxManualAutoCrop, 0);
            this.Controls.SetChildIndex(this.checkBoxManualFps, 0);
            this.Controls.SetChildIndex(this.groupBoxInputResolution, 0);
            this.Controls.SetChildIndex(this.checkBoxManualInputRes, 0);
            this.groupBoxFps.ResumeLayout(false);
            this.groupBoxFps.PerformLayout();
            this.groupBoxAutocrop.ResumeLayout(false);
            this.groupBoxAutocrop.PerformLayout();
            this.groupBoxResize.ResumeLayout(false);
            this.groupBoxResize.PerformLayout();
            this.groupBoxAddBorders.ResumeLayout(false);
            this.groupBoxAddBorders.PerformLayout();
            this.groupBoxCrop.ResumeLayout(false);
            this.groupBoxCrop.PerformLayout();
            this.groupBoxInputResolution.ResumeLayout(false);
            this.groupBoxInputResolution.PerformLayout();
            this.groupBoxNoMkvDemux.ResumeLayout(false);
            this.groupBoxNoMkvDemux.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFps;
        private System.Windows.Forms.TextBox textBoxFps;
        private System.Windows.Forms.GroupBox groupBoxAutocrop;
        private System.Windows.Forms.CheckBox checkBoxManualFps;
        private System.Windows.Forms.CheckBox checkBoxManualAutoCrop;
        private System.Windows.Forms.CheckBox checkBoxManualResize;
        private System.Windows.Forms.CheckBox checkBoxManualBorders;
        private System.Windows.Forms.CheckBox checkBoxManualCrop;
        private System.Windows.Forms.GroupBox groupBoxResize;
        private System.Windows.Forms.GroupBox groupBoxAddBorders;
        private System.Windows.Forms.GroupBox groupBoxCrop;
        private System.Windows.Forms.TextBox textBoxCropLeft;
        private System.Windows.Forms.Label labelCropLeft;
        private System.Windows.Forms.TextBox textBoxCropBottom;
        private System.Windows.Forms.Label labelCropBottom;
        private System.Windows.Forms.TextBox textBoxCropTop;
        private System.Windows.Forms.Label labelCropTop;
        private System.Windows.Forms.TextBox textBoxCropRight;
        private System.Windows.Forms.Label labelCropRight;
        private System.Windows.Forms.TextBox textBoxSizeY;
        private System.Windows.Forms.Label labelSizeY;
        private System.Windows.Forms.TextBox textBoxSizeX;
        private System.Windows.Forms.Label labelSizeX;
        private System.Windows.Forms.TextBox textBoxBorderBottom;
        private System.Windows.Forms.Label labelBorderBottom;
        private System.Windows.Forms.TextBox textBoxBorderTop;
        private System.Windows.Forms.Label labelBorderTop;
        private System.Windows.Forms.TextBox textBoxBorderRight;
        private System.Windows.Forms.Label labelBorderRight;
        private System.Windows.Forms.TextBox textBoxBorderLeft;
        private System.Windows.Forms.Label labelBorderLeft;
        private System.Windows.Forms.Label labelFramerate;
        private System.Windows.Forms.ComboBox comboBoxFramerate;
        private System.Windows.Forms.GroupBox groupBoxInputResolution;
        private System.Windows.Forms.TextBox textBoxInputResY;
        private System.Windows.Forms.Label labelInputResY;
        private System.Windows.Forms.TextBox textBoxInputResX;
        private System.Windows.Forms.Label labelInputResX;
        private System.Windows.Forms.CheckBox checkBoxManualInputRes;
        private System.Windows.Forms.CheckBox checkBoxnoMkvDemux;
        private System.Windows.Forms.GroupBox groupBoxNoMkvDemux;
        private System.Windows.Forms.Label labelChooseVideoExtension;
        private System.Windows.Forms.ComboBox comboBoxVideoExtension;
        private System.Windows.Forms.Label labelVideoExtension;
        private System.Windows.Forms.TextBox textBoxVideoExtension;
    }
}
