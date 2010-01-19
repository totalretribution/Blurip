namespace BluRip
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageStreamSelect = new System.Windows.Forms.TabPage();
            this.labelStreams = new System.Windows.Forms.Label();
            this.listBoxStreams = new System.Windows.Forms.ListBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.comboBoxTitle = new System.Windows.Forms.ComboBox();
            this.buttonGetStreamInfo = new System.Windows.Forms.Button();
            this.labelPath = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonPath = new System.Windows.Forms.Button();
            this.tabPageProcess = new System.Windows.Forms.TabPage();
            this.buttonStreamDown = new System.Windows.Forms.Button();
            this.buttonStreamUp = new System.Windows.Forms.Button();
            this.buttonDoMux = new System.Windows.Forms.Button();
            this.buttonDoSubtitle = new System.Windows.Forms.Button();
            this.labelEncodeProfile = new System.Windows.Forms.Label();
            this.comboBoxEncodeProfile = new System.Windows.Forms.ComboBox();
            this.buttonDoEncode = new System.Windows.Forms.Button();
            this.buttonDoDemux = new System.Windows.Forms.Button();
            this.buttonDoIndex = new System.Windows.Forms.Button();
            this.buttonClearStreamInfoList = new System.Windows.Forms.Button();
            this.buttonLoadStreamInfo = new System.Windows.Forms.Button();
            this.labelMuxedStreams = new System.Windows.Forms.Label();
            this.listBoxDemuxedStreams = new System.Windows.Forms.ListBox();
            this.groupBoxPath = new System.Windows.Forms.GroupBox();
            this.textBoxMovieTitle = new System.Windows.Forms.TextBox();
            this.labelMovieTitle = new System.Windows.Forms.Label();
            this.buttonTargetfolder = new System.Windows.Forms.Button();
            this.textBoxTargetfilename = new System.Windows.Forms.TextBox();
            this.labelTargetFilename = new System.Windows.Forms.Label();
            this.textBoxTargetFolder = new System.Windows.Forms.TextBox();
            this.labelTargetFolder = new System.Windows.Forms.Label();
            this.textBoxFilePrefix = new System.Windows.Forms.TextBox();
            this.labelFilePrefix = new System.Windows.Forms.Label();
            this.labelWorkingDir = new System.Windows.Forms.Label();
            this.textBoxWorkingDir = new System.Windows.Forms.TextBox();
            this.buttonWorkingDir = new System.Windows.Forms.Button();
            this.buttonStartConvert = new System.Windows.Forms.Button();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.groupBoxDefaultTrack = new System.Windows.Forms.GroupBox();
            this.checkBoxDefaultSubtitleForced = new System.Windows.Forms.CheckBox();
            this.checkBoxDefaultSubtitleTrack = new System.Windows.Forms.CheckBox();
            this.checkBoxDefaultAudioTrack = new System.Windows.Forms.CheckBox();
            this.groupBoxAutoCrop = new System.Windows.Forms.GroupBox();
            this.labelBlackValue = new System.Windows.Forms.Label();
            this.labelNrFrames = new System.Windows.Forms.Label();
            this.numericUpDownBlackValue = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNrFrames = new System.Windows.Forms.NumericUpDown();
            this.groupBoxExternalTools = new System.Windows.Forms.GroupBox();
            this.checkBoxDeleteAfterEncode = new System.Windows.Forms.CheckBox();
            this.buttonMkvmergePath = new System.Windows.Forms.Button();
            this.textBoxMkvmergePath = new System.Windows.Forms.TextBox();
            this.labelMkvmergePath = new System.Windows.Forms.Label();
            this.buttonJavaPath = new System.Windows.Forms.Button();
            this.textBoxJavaPath = new System.Windows.Forms.TextBox();
            this.labelJavaPath = new System.Windows.Forms.Label();
            this.buttonSup2subPath = new System.Windows.Forms.Button();
            this.textBoxSup2subPath = new System.Windows.Forms.TextBox();
            this.labelSup2subPath = new System.Windows.Forms.Label();
            this.buttonX264Path = new System.Windows.Forms.Button();
            this.textBoxX264Path = new System.Windows.Forms.TextBox();
            this.labelX264Path = new System.Windows.Forms.Label();
            this.buttonFfmsindexPath = new System.Windows.Forms.Button();
            this.textBoxFfmsindexPath = new System.Windows.Forms.TextBox();
            this.labelFfmsindexPath = new System.Windows.Forms.Label();
            this.textBoxEac3toPath = new System.Windows.Forms.TextBox();
            this.labelEac3toPath = new System.Windows.Forms.Label();
            this.buttonEac3toPath = new System.Windows.Forms.Button();
            this.groupBoxAutoSelect = new System.Windows.Forms.GroupBox();
            this.buttonLangDown = new System.Windows.Forms.Button();
            this.buttonLangUp = new System.Windows.Forms.Button();
            this.buttonDeleteLanguage = new System.Windows.Forms.Button();
            this.buttonAddLanguage = new System.Windows.Forms.Button();
            this.checkBoxIncludeSubtitle = new System.Windows.Forms.CheckBox();
            this.listBoxPreferedLanguages = new System.Windows.Forms.ListBox();
            this.labelPreferedLanguage = new System.Windows.Forms.Label();
            this.checkBoxPreferDts = new System.Windows.Forms.CheckBox();
            this.checkBoxSelectChapters = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoSelect = new System.Windows.Forms.CheckBox();
            this.tabPageEncodingSettings = new System.Windows.Forms.TabPage();
            this.groupBoxAvisynthCommand = new System.Windows.Forms.GroupBox();
            this.richTextBoxCommandsAfterResize = new System.Windows.Forms.RichTextBox();
            this.labelCommandAfterResize = new System.Windows.Forms.Label();
            this.labelX264Priority = new System.Windows.Forms.Label();
            this.comboBoxX264Priority = new System.Windows.Forms.ComboBox();
            this.groupBoxX264Profiles = new System.Windows.Forms.GroupBox();
            this.labelX264 = new System.Windows.Forms.Label();
            this.buttonDelX264 = new System.Windows.Forms.Button();
            this.listBoxX264Profiles = new System.Windows.Forms.ListBox();
            this.buttonAddX264 = new System.Windows.Forms.Button();
            this.groupBoxGeneralAviSynthSettings = new System.Windows.Forms.GroupBox();
            this.checkBoxEncodeDirectshow = new System.Windows.Forms.CheckBox();
            this.checkBoxCropDirectshow = new System.Windows.Forms.CheckBox();
            this.tabPageSoftware = new System.Windows.Forms.TabPage();
            this.linkLabelAnyDvd = new System.Windows.Forms.LinkLabel();
            this.linkLabelFilterTweaker = new System.Windows.Forms.LinkLabel();
            this.linkLabelMkvtoolnix = new System.Windows.Forms.LinkLabel();
            this.linkLabelJava = new System.Windows.Forms.LinkLabel();
            this.linkLabelFFMpegSrc = new System.Windows.Forms.LinkLabel();
            this.linkLabelBDSup2sub = new System.Windows.Forms.LinkLabel();
            this.linkLabelX264 = new System.Windows.Forms.LinkLabel();
            this.linkLabelEac3to = new System.Windows.Forms.LinkLabel();
            this.linkLabelHaali = new System.Windows.Forms.LinkLabel();
            this.linkLabelAviSynth = new System.Windows.Forms.LinkLabel();
            this.labelSoftwareDesc = new System.Windows.Forms.Label();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.richTextBoxAbout = new System.Windows.Forms.RichTextBox();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.progressBarMain = new System.Windows.Forms.ProgressBar();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.labelLog = new System.Windows.Forms.Label();
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.buttonMinimize = new System.Windows.Forms.Button();
            this.comboBoxCropMode = new System.Windows.Forms.ComboBox();
            this.labelCropMode = new System.Windows.Forms.Label();
            this.tabControlMain.SuspendLayout();
            this.tabPageStreamSelect.SuspendLayout();
            this.tabPageProcess.SuspendLayout();
            this.groupBoxPath.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.groupBoxDefaultTrack.SuspendLayout();
            this.groupBoxAutoCrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlackValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNrFrames)).BeginInit();
            this.groupBoxExternalTools.SuspendLayout();
            this.groupBoxAutoSelect.SuspendLayout();
            this.tabPageEncodingSettings.SuspendLayout();
            this.groupBoxAvisynthCommand.SuspendLayout();
            this.groupBoxX264Profiles.SuspendLayout();
            this.groupBoxGeneralAviSynthSettings.SuspendLayout();
            this.tabPageSoftware.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageStreamSelect);
            this.tabControlMain.Controls.Add(this.tabPageProcess);
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.Controls.Add(this.tabPageEncodingSettings);
            this.tabControlMain.Controls.Add(this.tabPageSoftware);
            this.tabControlMain.Controls.Add(this.tabPageAbout);
            this.tabControlMain.Location = new System.Drawing.Point(12, 12);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1157, 370);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageStreamSelect
            // 
            this.tabPageStreamSelect.Controls.Add(this.labelStreams);
            this.tabPageStreamSelect.Controls.Add(this.listBoxStreams);
            this.tabPageStreamSelect.Controls.Add(this.labelTitle);
            this.tabPageStreamSelect.Controls.Add(this.comboBoxTitle);
            this.tabPageStreamSelect.Controls.Add(this.buttonGetStreamInfo);
            this.tabPageStreamSelect.Controls.Add(this.labelPath);
            this.tabPageStreamSelect.Controls.Add(this.textBoxPath);
            this.tabPageStreamSelect.Controls.Add(this.buttonPath);
            this.tabPageStreamSelect.Location = new System.Drawing.Point(4, 22);
            this.tabPageStreamSelect.Name = "tabPageStreamSelect";
            this.tabPageStreamSelect.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStreamSelect.Size = new System.Drawing.Size(1149, 344);
            this.tabPageStreamSelect.TabIndex = 0;
            this.tabPageStreamSelect.Text = "Stream selection";
            this.tabPageStreamSelect.UseVisualStyleBackColor = true;
            // 
            // labelStreams
            // 
            this.labelStreams.AutoSize = true;
            this.labelStreams.Location = new System.Drawing.Point(6, 114);
            this.labelStreams.Name = "labelStreams";
            this.labelStreams.Size = new System.Drawing.Size(48, 13);
            this.labelStreams.TabIndex = 7;
            this.labelStreams.Text = "Streams:";
            // 
            // listBoxStreams
            // 
            this.listBoxStreams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxStreams.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxStreams.FormattingEnabled = true;
            this.listBoxStreams.ItemHeight = 14;
            this.listBoxStreams.Location = new System.Drawing.Point(6, 130);
            this.listBoxStreams.Name = "listBoxStreams";
            this.listBoxStreams.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxStreams.Size = new System.Drawing.Size(1137, 200);
            this.listBoxStreams.TabIndex = 6;
            this.listBoxStreams.SelectedIndexChanged += new System.EventHandler(this.listBoxStreams_SelectedIndexChanged);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(3, 74);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(42, 13);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "Titlelist:";
            // 
            // comboBoxTitle
            // 
            this.comboBoxTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTitle.FormattingEnabled = true;
            this.comboBoxTitle.Location = new System.Drawing.Point(3, 90);
            this.comboBoxTitle.Name = "comboBoxTitle";
            this.comboBoxTitle.Size = new System.Drawing.Size(839, 21);
            this.comboBoxTitle.TabIndex = 4;
            this.comboBoxTitle.SelectedIndexChanged += new System.EventHandler(this.comboBoxTitle_SelectedIndexChanged);
            // 
            // buttonGetStreamInfo
            // 
            this.buttonGetStreamInfo.Location = new System.Drawing.Point(6, 45);
            this.buttonGetStreamInfo.Name = "buttonGetStreamInfo";
            this.buttonGetStreamInfo.Size = new System.Drawing.Size(116, 23);
            this.buttonGetStreamInfo.TabIndex = 3;
            this.buttonGetStreamInfo.Text = "Get stream infos";
            this.buttonGetStreamInfo.UseVisualStyleBackColor = true;
            this.buttonGetStreamInfo.Click += new System.EventHandler(this.buttonGetStreamInfo_Click);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(6, 3);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(81, 13);
            this.labelPath.TabIndex = 2;
            this.labelPath.Text = "Path to BluRay:";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(6, 19);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(295, 20);
            this.textBoxPath.TabIndex = 1;
            // 
            // buttonPath
            // 
            this.buttonPath.Location = new System.Drawing.Point(307, 16);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(37, 23);
            this.buttonPath.TabIndex = 0;
            this.buttonPath.Text = "...";
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.buttonPath_Click);
            // 
            // tabPageProcess
            // 
            this.tabPageProcess.Controls.Add(this.buttonStreamDown);
            this.tabPageProcess.Controls.Add(this.buttonStreamUp);
            this.tabPageProcess.Controls.Add(this.buttonDoMux);
            this.tabPageProcess.Controls.Add(this.buttonDoSubtitle);
            this.tabPageProcess.Controls.Add(this.labelEncodeProfile);
            this.tabPageProcess.Controls.Add(this.comboBoxEncodeProfile);
            this.tabPageProcess.Controls.Add(this.buttonDoEncode);
            this.tabPageProcess.Controls.Add(this.buttonDoDemux);
            this.tabPageProcess.Controls.Add(this.buttonDoIndex);
            this.tabPageProcess.Controls.Add(this.buttonClearStreamInfoList);
            this.tabPageProcess.Controls.Add(this.buttonLoadStreamInfo);
            this.tabPageProcess.Controls.Add(this.labelMuxedStreams);
            this.tabPageProcess.Controls.Add(this.listBoxDemuxedStreams);
            this.tabPageProcess.Controls.Add(this.groupBoxPath);
            this.tabPageProcess.Controls.Add(this.buttonStartConvert);
            this.tabPageProcess.Location = new System.Drawing.Point(4, 22);
            this.tabPageProcess.Name = "tabPageProcess";
            this.tabPageProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProcess.Size = new System.Drawing.Size(1149, 344);
            this.tabPageProcess.TabIndex = 3;
            this.tabPageProcess.Text = "Process";
            this.tabPageProcess.UseVisualStyleBackColor = true;
            // 
            // buttonStreamDown
            // 
            this.buttonStreamDown.Location = new System.Drawing.Point(991, 51);
            this.buttonStreamDown.Name = "buttonStreamDown";
            this.buttonStreamDown.Size = new System.Drawing.Size(75, 23);
            this.buttonStreamDown.TabIndex = 33;
            this.buttonStreamDown.Text = "Move down";
            this.buttonStreamDown.UseVisualStyleBackColor = true;
            this.buttonStreamDown.Click += new System.EventHandler(this.buttonStreamDown_Click);
            // 
            // buttonStreamUp
            // 
            this.buttonStreamUp.Location = new System.Drawing.Point(991, 22);
            this.buttonStreamUp.Name = "buttonStreamUp";
            this.buttonStreamUp.Size = new System.Drawing.Size(75, 23);
            this.buttonStreamUp.TabIndex = 32;
            this.buttonStreamUp.Text = "Move up";
            this.buttonStreamUp.UseVisualStyleBackColor = true;
            this.buttonStreamUp.Click += new System.EventHandler(this.buttonStreamUp_Click);
            // 
            // buttonDoMux
            // 
            this.buttonDoMux.Location = new System.Drawing.Point(630, 315);
            this.buttonDoMux.Name = "buttonDoMux";
            this.buttonDoMux.Size = new System.Drawing.Size(75, 23);
            this.buttonDoMux.TabIndex = 31;
            this.buttonDoMux.Text = "Only mux";
            this.buttonDoMux.UseVisualStyleBackColor = true;
            this.buttonDoMux.Click += new System.EventHandler(this.buttonDoMux_Click);
            // 
            // buttonDoSubtitle
            // 
            this.buttonDoSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDoSubtitle.Location = new System.Drawing.Point(385, 315);
            this.buttonDoSubtitle.Name = "buttonDoSubtitle";
            this.buttonDoSubtitle.Size = new System.Drawing.Size(138, 23);
            this.buttonDoSubtitle.TabIndex = 30;
            this.buttonDoSubtitle.Text = "Only subtitle processing";
            this.buttonDoSubtitle.UseVisualStyleBackColor = true;
            this.buttonDoSubtitle.Click += new System.EventHandler(this.buttonDoSubtitle_Click);
            // 
            // labelEncodeProfile
            // 
            this.labelEncodeProfile.AutoSize = true;
            this.labelEncodeProfile.Location = new System.Drawing.Point(6, 224);
            this.labelEncodeProfile.Name = "labelEncodeProfile";
            this.labelEncodeProfile.Size = new System.Drawing.Size(83, 13);
            this.labelEncodeProfile.TabIndex = 26;
            this.labelEncodeProfile.Text = "Encoding profile";
            // 
            // comboBoxEncodeProfile
            // 
            this.comboBoxEncodeProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEncodeProfile.FormattingEnabled = true;
            this.comboBoxEncodeProfile.Location = new System.Drawing.Point(9, 240);
            this.comboBoxEncodeProfile.Name = "comboBoxEncodeProfile";
            this.comboBoxEncodeProfile.Size = new System.Drawing.Size(344, 21);
            this.comboBoxEncodeProfile.TabIndex = 25;
            this.comboBoxEncodeProfile.SelectedIndexChanged += new System.EventHandler(this.comboBoxEncodeProfile_SelectedIndexChanged);
            // 
            // buttonDoEncode
            // 
            this.buttonDoEncode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDoEncode.Location = new System.Drawing.Point(529, 315);
            this.buttonDoEncode.Name = "buttonDoEncode";
            this.buttonDoEncode.Size = new System.Drawing.Size(95, 23);
            this.buttonDoEncode.TabIndex = 24;
            this.buttonDoEncode.Text = "Only encode";
            this.buttonDoEncode.UseVisualStyleBackColor = true;
            this.buttonDoEncode.Click += new System.EventHandler(this.buttonDoEncode_Click);
            // 
            // buttonDoDemux
            // 
            this.buttonDoDemux.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDoDemux.Location = new System.Drawing.Point(157, 315);
            this.buttonDoDemux.Name = "buttonDoDemux";
            this.buttonDoDemux.Size = new System.Drawing.Size(75, 23);
            this.buttonDoDemux.TabIndex = 23;
            this.buttonDoDemux.Text = "Only demux";
            this.buttonDoDemux.UseVisualStyleBackColor = true;
            this.buttonDoDemux.Click += new System.EventHandler(this.buttonDoDemux_Click);
            // 
            // buttonDoIndex
            // 
            this.buttonDoIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDoIndex.Location = new System.Drawing.Point(238, 315);
            this.buttonDoIndex.Name = "buttonDoIndex";
            this.buttonDoIndex.Size = new System.Drawing.Size(141, 23);
            this.buttonDoIndex.TabIndex = 22;
            this.buttonDoIndex.Text = "Only indexing + autocrop";
            this.buttonDoIndex.UseVisualStyleBackColor = true;
            this.buttonDoIndex.Click += new System.EventHandler(this.buttonDoIndex_Click);
            // 
            // buttonClearStreamInfoList
            // 
            this.buttonClearStreamInfoList.Location = new System.Drawing.Point(557, 238);
            this.buttonClearStreamInfoList.Name = "buttonClearStreamInfoList";
            this.buttonClearStreamInfoList.Size = new System.Drawing.Size(192, 23);
            this.buttonClearStreamInfoList.TabIndex = 19;
            this.buttonClearStreamInfoList.Text = "Clear demuxed streams list";
            this.buttonClearStreamInfoList.UseVisualStyleBackColor = true;
            this.buttonClearStreamInfoList.Click += new System.EventHandler(this.buttonClearStreamInfoList_Click);
            // 
            // buttonLoadStreamInfo
            // 
            this.buttonLoadStreamInfo.Location = new System.Drawing.Point(359, 238);
            this.buttonLoadStreamInfo.Name = "buttonLoadStreamInfo";
            this.buttonLoadStreamInfo.Size = new System.Drawing.Size(192, 23);
            this.buttonLoadStreamInfo.TabIndex = 18;
            this.buttonLoadStreamInfo.Text = "Load existing streamInfo.xml";
            this.buttonLoadStreamInfo.UseVisualStyleBackColor = true;
            this.buttonLoadStreamInfo.Click += new System.EventHandler(this.buttonLoadStreamInfo_Click);
            // 
            // labelMuxedStreams
            // 
            this.labelMuxedStreams.AutoSize = true;
            this.labelMuxedStreams.Location = new System.Drawing.Point(359, 6);
            this.labelMuxedStreams.Name = "labelMuxedStreams";
            this.labelMuxedStreams.Size = new System.Drawing.Size(94, 13);
            this.labelMuxedStreams.TabIndex = 17;
            this.labelMuxedStreams.Text = "Demuxed streams:";
            // 
            // listBoxDemuxedStreams
            // 
            this.listBoxDemuxedStreams.FormattingEnabled = true;
            this.listBoxDemuxedStreams.Location = new System.Drawing.Point(359, 22);
            this.listBoxDemuxedStreams.Name = "listBoxDemuxedStreams";
            this.listBoxDemuxedStreams.Size = new System.Drawing.Size(626, 212);
            this.listBoxDemuxedStreams.TabIndex = 16;
            // 
            // groupBoxPath
            // 
            this.groupBoxPath.Controls.Add(this.textBoxMovieTitle);
            this.groupBoxPath.Controls.Add(this.labelMovieTitle);
            this.groupBoxPath.Controls.Add(this.buttonTargetfolder);
            this.groupBoxPath.Controls.Add(this.textBoxTargetfilename);
            this.groupBoxPath.Controls.Add(this.labelTargetFilename);
            this.groupBoxPath.Controls.Add(this.textBoxTargetFolder);
            this.groupBoxPath.Controls.Add(this.labelTargetFolder);
            this.groupBoxPath.Controls.Add(this.textBoxFilePrefix);
            this.groupBoxPath.Controls.Add(this.labelFilePrefix);
            this.groupBoxPath.Controls.Add(this.labelWorkingDir);
            this.groupBoxPath.Controls.Add(this.textBoxWorkingDir);
            this.groupBoxPath.Controls.Add(this.buttonWorkingDir);
            this.groupBoxPath.Location = new System.Drawing.Point(6, 6);
            this.groupBoxPath.Name = "groupBoxPath";
            this.groupBoxPath.Size = new System.Drawing.Size(347, 215);
            this.groupBoxPath.TabIndex = 15;
            this.groupBoxPath.TabStop = false;
            this.groupBoxPath.Text = "Path && filenames";
            // 
            // textBoxMovieTitle
            // 
            this.textBoxMovieTitle.Location = new System.Drawing.Point(6, 188);
            this.textBoxMovieTitle.Name = "textBoxMovieTitle";
            this.textBoxMovieTitle.Size = new System.Drawing.Size(286, 20);
            this.textBoxMovieTitle.TabIndex = 22;
            this.textBoxMovieTitle.TextChanged += new System.EventHandler(this.textBoxMovieTitle_TextChanged);
            // 
            // labelMovieTitle
            // 
            this.labelMovieTitle.AutoSize = true;
            this.labelMovieTitle.Location = new System.Drawing.Point(6, 172);
            this.labelMovieTitle.Name = "labelMovieTitle";
            this.labelMovieTitle.Size = new System.Drawing.Size(58, 13);
            this.labelMovieTitle.TabIndex = 21;
            this.labelMovieTitle.Text = "Movie title:";
            // 
            // buttonTargetfolder
            // 
            this.buttonTargetfolder.Location = new System.Drawing.Point(298, 108);
            this.buttonTargetfolder.Name = "buttonTargetfolder";
            this.buttonTargetfolder.Size = new System.Drawing.Size(37, 23);
            this.buttonTargetfolder.TabIndex = 20;
            this.buttonTargetfolder.Text = "...";
            this.buttonTargetfolder.UseVisualStyleBackColor = true;
            this.buttonTargetfolder.Click += new System.EventHandler(this.buttonTargetfolder_Click);
            // 
            // textBoxTargetfilename
            // 
            this.textBoxTargetfilename.Location = new System.Drawing.Point(6, 149);
            this.textBoxTargetfilename.Name = "textBoxTargetfilename";
            this.textBoxTargetfilename.Size = new System.Drawing.Size(286, 20);
            this.textBoxTargetfilename.TabIndex = 19;
            this.textBoxTargetfilename.TextChanged += new System.EventHandler(this.textBoxTargetfilename_TextChanged);
            // 
            // labelTargetFilename
            // 
            this.labelTargetFilename.AutoSize = true;
            this.labelTargetFilename.Location = new System.Drawing.Point(6, 133);
            this.labelTargetFilename.Name = "labelTargetFilename";
            this.labelTargetFilename.Size = new System.Drawing.Size(155, 13);
            this.labelTargetFilename.TabIndex = 18;
            this.labelTargetFilename.Text = "Target filename: (without .mkv!)";
            // 
            // textBoxTargetFolder
            // 
            this.textBoxTargetFolder.Location = new System.Drawing.Point(6, 110);
            this.textBoxTargetFolder.Name = "textBoxTargetFolder";
            this.textBoxTargetFolder.ReadOnly = true;
            this.textBoxTargetFolder.Size = new System.Drawing.Size(286, 20);
            this.textBoxTargetFolder.TabIndex = 17;
            // 
            // labelTargetFolder
            // 
            this.labelTargetFolder.AutoSize = true;
            this.labelTargetFolder.Location = new System.Drawing.Point(6, 94);
            this.labelTargetFolder.Name = "labelTargetFolder";
            this.labelTargetFolder.Size = new System.Drawing.Size(70, 13);
            this.labelTargetFolder.TabIndex = 16;
            this.labelTargetFolder.Text = "Target folder:";
            // 
            // textBoxFilePrefix
            // 
            this.textBoxFilePrefix.Location = new System.Drawing.Point(9, 71);
            this.textBoxFilePrefix.Name = "textBoxFilePrefix";
            this.textBoxFilePrefix.Size = new System.Drawing.Size(283, 20);
            this.textBoxFilePrefix.TabIndex = 15;
            this.textBoxFilePrefix.TextChanged += new System.EventHandler(this.textBoxFilePrefix_TextChanged);
            // 
            // labelFilePrefix
            // 
            this.labelFilePrefix.AutoSize = true;
            this.labelFilePrefix.Location = new System.Drawing.Point(6, 55);
            this.labelFilePrefix.Name = "labelFilePrefix";
            this.labelFilePrefix.Size = new System.Drawing.Size(54, 13);
            this.labelFilePrefix.TabIndex = 14;
            this.labelFilePrefix.Text = "File prefix:";
            // 
            // labelWorkingDir
            // 
            this.labelWorkingDir.AutoSize = true;
            this.labelWorkingDir.Location = new System.Drawing.Point(6, 16);
            this.labelWorkingDir.Name = "labelWorkingDir";
            this.labelWorkingDir.Size = new System.Drawing.Size(93, 13);
            this.labelWorkingDir.TabIndex = 13;
            this.labelWorkingDir.Text = "Working directory:";
            // 
            // textBoxWorkingDir
            // 
            this.textBoxWorkingDir.Location = new System.Drawing.Point(9, 32);
            this.textBoxWorkingDir.Name = "textBoxWorkingDir";
            this.textBoxWorkingDir.ReadOnly = true;
            this.textBoxWorkingDir.Size = new System.Drawing.Size(283, 20);
            this.textBoxWorkingDir.TabIndex = 11;
            // 
            // buttonWorkingDir
            // 
            this.buttonWorkingDir.Location = new System.Drawing.Point(298, 30);
            this.buttonWorkingDir.Name = "buttonWorkingDir";
            this.buttonWorkingDir.Size = new System.Drawing.Size(37, 23);
            this.buttonWorkingDir.TabIndex = 12;
            this.buttonWorkingDir.Text = "...";
            this.buttonWorkingDir.UseVisualStyleBackColor = true;
            this.buttonWorkingDir.Click += new System.EventHandler(this.buttonWorkingDir_Click);
            // 
            // buttonStartConvert
            // 
            this.buttonStartConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStartConvert.Location = new System.Drawing.Point(6, 315);
            this.buttonStartConvert.Name = "buttonStartConvert";
            this.buttonStartConvert.Size = new System.Drawing.Size(75, 23);
            this.buttonStartConvert.TabIndex = 14;
            this.buttonStartConvert.Text = "Start";
            this.buttonStartConvert.UseVisualStyleBackColor = true;
            this.buttonStartConvert.Click += new System.EventHandler(this.buttonStartConvert_Click);
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.groupBoxDefaultTrack);
            this.tabPageSettings.Controls.Add(this.groupBoxAutoCrop);
            this.tabPageSettings.Controls.Add(this.groupBoxExternalTools);
            this.tabPageSettings.Controls.Add(this.groupBoxAutoSelect);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(1149, 344);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // groupBoxDefaultTrack
            // 
            this.groupBoxDefaultTrack.Controls.Add(this.checkBoxDefaultSubtitleForced);
            this.groupBoxDefaultTrack.Controls.Add(this.checkBoxDefaultSubtitleTrack);
            this.groupBoxDefaultTrack.Controls.Add(this.checkBoxDefaultAudioTrack);
            this.groupBoxDefaultTrack.Location = new System.Drawing.Point(412, 194);
            this.groupBoxDefaultTrack.Name = "groupBoxDefaultTrack";
            this.groupBoxDefaultTrack.Size = new System.Drawing.Size(368, 91);
            this.groupBoxDefaultTrack.TabIndex = 6;
            this.groupBoxDefaultTrack.TabStop = false;
            this.groupBoxDefaultTrack.Text = "Default tracks";
            // 
            // checkBoxDefaultSubtitleForced
            // 
            this.checkBoxDefaultSubtitleForced.AutoSize = true;
            this.checkBoxDefaultSubtitleForced.Location = new System.Drawing.Point(6, 65);
            this.checkBoxDefaultSubtitleForced.Name = "checkBoxDefaultSubtitleForced";
            this.checkBoxDefaultSubtitleForced.Size = new System.Drawing.Size(194, 17);
            this.checkBoxDefaultSubtitleForced.TabIndex = 2;
            this.checkBoxDefaultSubtitleForced.Text = "Use forced subtitle track if available";
            this.checkBoxDefaultSubtitleForced.UseVisualStyleBackColor = true;
            this.checkBoxDefaultSubtitleForced.CheckedChanged += new System.EventHandler(this.checkBoxDefaultSubtitleForced_CheckedChanged);
            // 
            // checkBoxDefaultSubtitleTrack
            // 
            this.checkBoxDefaultSubtitleTrack.AutoSize = true;
            this.checkBoxDefaultSubtitleTrack.Location = new System.Drawing.Point(6, 42);
            this.checkBoxDefaultSubtitleTrack.Name = "checkBoxDefaultSubtitleTrack";
            this.checkBoxDefaultSubtitleTrack.Size = new System.Drawing.Size(330, 17);
            this.checkBoxDefaultSubtitleTrack.TabIndex = 1;
            this.checkBoxDefaultSubtitleTrack.Text = "Set first subtitle track with first prefered language as default track";
            this.checkBoxDefaultSubtitleTrack.UseVisualStyleBackColor = true;
            this.checkBoxDefaultSubtitleTrack.CheckedChanged += new System.EventHandler(this.checkBoxDefaultSubtitleTrack_CheckedChanged);
            // 
            // checkBoxDefaultAudioTrack
            // 
            this.checkBoxDefaultAudioTrack.AutoSize = true;
            this.checkBoxDefaultAudioTrack.Location = new System.Drawing.Point(6, 19);
            this.checkBoxDefaultAudioTrack.Name = "checkBoxDefaultAudioTrack";
            this.checkBoxDefaultAudioTrack.Size = new System.Drawing.Size(323, 17);
            this.checkBoxDefaultAudioTrack.TabIndex = 0;
            this.checkBoxDefaultAudioTrack.Text = "Set first audio track with first prefered language as default track";
            this.checkBoxDefaultAudioTrack.UseVisualStyleBackColor = true;
            this.checkBoxDefaultAudioTrack.CheckedChanged += new System.EventHandler(this.checkBoxDefaultAudioTrack_CheckedChanged);
            // 
            // groupBoxAutoCrop
            // 
            this.groupBoxAutoCrop.Controls.Add(this.labelCropMode);
            this.groupBoxAutoCrop.Controls.Add(this.comboBoxCropMode);
            this.groupBoxAutoCrop.Controls.Add(this.labelBlackValue);
            this.groupBoxAutoCrop.Controls.Add(this.labelNrFrames);
            this.groupBoxAutoCrop.Controls.Add(this.numericUpDownBlackValue);
            this.groupBoxAutoCrop.Controls.Add(this.numericUpDownNrFrames);
            this.groupBoxAutoCrop.Location = new System.Drawing.Point(873, 6);
            this.groupBoxAutoCrop.Name = "groupBoxAutoCrop";
            this.groupBoxAutoCrop.Size = new System.Drawing.Size(194, 146);
            this.groupBoxAutoCrop.TabIndex = 5;
            this.groupBoxAutoCrop.TabStop = false;
            this.groupBoxAutoCrop.Text = "AutoCrop settings";
            // 
            // labelBlackValue
            // 
            this.labelBlackValue.AutoSize = true;
            this.labelBlackValue.Location = new System.Drawing.Point(6, 55);
            this.labelBlackValue.Name = "labelBlackValue";
            this.labelBlackValue.Size = new System.Drawing.Size(157, 13);
            this.labelBlackValue.TabIndex = 3;
            this.labelBlackValue.Text = "Max row sum to count as black:";
            // 
            // labelNrFrames
            // 
            this.labelNrFrames.AutoSize = true;
            this.labelNrFrames.Location = new System.Drawing.Point(6, 16);
            this.labelNrFrames.Name = "labelNrFrames";
            this.labelNrFrames.Size = new System.Drawing.Size(114, 13);
            this.labelNrFrames.TabIndex = 2;
            this.labelNrFrames.Text = "# of frames to analyze:";
            // 
            // numericUpDownBlackValue
            // 
            this.numericUpDownBlackValue.Location = new System.Drawing.Point(6, 72);
            this.numericUpDownBlackValue.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownBlackValue.Name = "numericUpDownBlackValue";
            this.numericUpDownBlackValue.Size = new System.Drawing.Size(182, 20);
            this.numericUpDownBlackValue.TabIndex = 1;
            this.numericUpDownBlackValue.ValueChanged += new System.EventHandler(this.numericUpDownBlackValue_ValueChanged);
            // 
            // numericUpDownNrFrames
            // 
            this.numericUpDownNrFrames.Location = new System.Drawing.Point(6, 32);
            this.numericUpDownNrFrames.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownNrFrames.Name = "numericUpDownNrFrames";
            this.numericUpDownNrFrames.Size = new System.Drawing.Size(182, 20);
            this.numericUpDownNrFrames.TabIndex = 0;
            this.numericUpDownNrFrames.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownNrFrames.ValueChanged += new System.EventHandler(this.numericUpDownNrFrames_ValueChanged);
            // 
            // groupBoxExternalTools
            // 
            this.groupBoxExternalTools.Controls.Add(this.checkBoxDeleteAfterEncode);
            this.groupBoxExternalTools.Controls.Add(this.buttonMkvmergePath);
            this.groupBoxExternalTools.Controls.Add(this.textBoxMkvmergePath);
            this.groupBoxExternalTools.Controls.Add(this.labelMkvmergePath);
            this.groupBoxExternalTools.Controls.Add(this.buttonJavaPath);
            this.groupBoxExternalTools.Controls.Add(this.textBoxJavaPath);
            this.groupBoxExternalTools.Controls.Add(this.labelJavaPath);
            this.groupBoxExternalTools.Controls.Add(this.buttonSup2subPath);
            this.groupBoxExternalTools.Controls.Add(this.textBoxSup2subPath);
            this.groupBoxExternalTools.Controls.Add(this.labelSup2subPath);
            this.groupBoxExternalTools.Controls.Add(this.buttonX264Path);
            this.groupBoxExternalTools.Controls.Add(this.textBoxX264Path);
            this.groupBoxExternalTools.Controls.Add(this.labelX264Path);
            this.groupBoxExternalTools.Controls.Add(this.buttonFfmsindexPath);
            this.groupBoxExternalTools.Controls.Add(this.textBoxFfmsindexPath);
            this.groupBoxExternalTools.Controls.Add(this.labelFfmsindexPath);
            this.groupBoxExternalTools.Controls.Add(this.textBoxEac3toPath);
            this.groupBoxExternalTools.Controls.Add(this.labelEac3toPath);
            this.groupBoxExternalTools.Controls.Add(this.buttonEac3toPath);
            this.groupBoxExternalTools.Location = new System.Drawing.Point(6, 6);
            this.groupBoxExternalTools.Name = "groupBoxExternalTools";
            this.groupBoxExternalTools.Size = new System.Drawing.Size(400, 279);
            this.groupBoxExternalTools.TabIndex = 4;
            this.groupBoxExternalTools.TabStop = false;
            this.groupBoxExternalTools.Text = "External tools";
            // 
            // checkBoxDeleteAfterEncode
            // 
            this.checkBoxDeleteAfterEncode.AutoSize = true;
            this.checkBoxDeleteAfterEncode.Location = new System.Drawing.Point(6, 256);
            this.checkBoxDeleteAfterEncode.Name = "checkBoxDeleteAfterEncode";
            this.checkBoxDeleteAfterEncode.Size = new System.Drawing.Size(159, 17);
            this.checkBoxDeleteAfterEncode.TabIndex = 18;
            this.checkBoxDeleteAfterEncode.Text = "Delete source files after mux";
            this.checkBoxDeleteAfterEncode.UseVisualStyleBackColor = true;
            this.checkBoxDeleteAfterEncode.CheckedChanged += new System.EventHandler(this.checkBoxDeleteAfterEncode_CheckedChanged);
            // 
            // buttonMkvmergePath
            // 
            this.buttonMkvmergePath.Location = new System.Drawing.Point(355, 225);
            this.buttonMkvmergePath.Name = "buttonMkvmergePath";
            this.buttonMkvmergePath.Size = new System.Drawing.Size(39, 23);
            this.buttonMkvmergePath.TabIndex = 17;
            this.buttonMkvmergePath.Text = "...";
            this.buttonMkvmergePath.UseVisualStyleBackColor = true;
            this.buttonMkvmergePath.Click += new System.EventHandler(this.buttonMkvmergePath_Click);
            // 
            // textBoxMkvmergePath
            // 
            this.textBoxMkvmergePath.Location = new System.Drawing.Point(6, 227);
            this.textBoxMkvmergePath.Name = "textBoxMkvmergePath";
            this.textBoxMkvmergePath.ReadOnly = true;
            this.textBoxMkvmergePath.Size = new System.Drawing.Size(343, 20);
            this.textBoxMkvmergePath.TabIndex = 16;
            // 
            // labelMkvmergePath
            // 
            this.labelMkvmergePath.AutoSize = true;
            this.labelMkvmergePath.Location = new System.Drawing.Point(6, 211);
            this.labelMkvmergePath.Name = "labelMkvmergePath";
            this.labelMkvmergePath.Size = new System.Drawing.Size(116, 13);
            this.labelMkvmergePath.TabIndex = 15;
            this.labelMkvmergePath.Text = "Path to mkvmerge.exe:";
            // 
            // buttonJavaPath
            // 
            this.buttonJavaPath.Location = new System.Drawing.Point(355, 186);
            this.buttonJavaPath.Name = "buttonJavaPath";
            this.buttonJavaPath.Size = new System.Drawing.Size(39, 23);
            this.buttonJavaPath.TabIndex = 14;
            this.buttonJavaPath.Text = "...";
            this.buttonJavaPath.UseVisualStyleBackColor = true;
            this.buttonJavaPath.Click += new System.EventHandler(this.buttonJavaPath_Click);
            // 
            // textBoxJavaPath
            // 
            this.textBoxJavaPath.Location = new System.Drawing.Point(6, 188);
            this.textBoxJavaPath.Name = "textBoxJavaPath";
            this.textBoxJavaPath.ReadOnly = true;
            this.textBoxJavaPath.Size = new System.Drawing.Size(343, 20);
            this.textBoxJavaPath.TabIndex = 13;
            // 
            // labelJavaPath
            // 
            this.labelJavaPath.AutoSize = true;
            this.labelJavaPath.Location = new System.Drawing.Point(6, 172);
            this.labelJavaPath.Name = "labelJavaPath";
            this.labelJavaPath.Size = new System.Drawing.Size(87, 13);
            this.labelJavaPath.TabIndex = 12;
            this.labelJavaPath.Text = "Path to java.exe:";
            // 
            // buttonSup2subPath
            // 
            this.buttonSup2subPath.Location = new System.Drawing.Point(355, 147);
            this.buttonSup2subPath.Name = "buttonSup2subPath";
            this.buttonSup2subPath.Size = new System.Drawing.Size(39, 23);
            this.buttonSup2subPath.TabIndex = 11;
            this.buttonSup2subPath.Text = "...";
            this.buttonSup2subPath.UseVisualStyleBackColor = true;
            this.buttonSup2subPath.Click += new System.EventHandler(this.buttonSup2subPath_Click);
            // 
            // textBoxSup2subPath
            // 
            this.textBoxSup2subPath.Location = new System.Drawing.Point(6, 149);
            this.textBoxSup2subPath.Name = "textBoxSup2subPath";
            this.textBoxSup2subPath.ReadOnly = true;
            this.textBoxSup2subPath.Size = new System.Drawing.Size(343, 20);
            this.textBoxSup2subPath.TabIndex = 10;
            // 
            // labelSup2subPath
            // 
            this.labelSup2subPath.AutoSize = true;
            this.labelSup2subPath.Location = new System.Drawing.Point(6, 133);
            this.labelSup2subPath.Name = "labelSup2subPath";
            this.labelSup2subPath.Size = new System.Drawing.Size(120, 13);
            this.labelSup2subPath.TabIndex = 9;
            this.labelSup2subPath.Text = "Path to BDSup2Sub.jar:";
            // 
            // buttonX264Path
            // 
            this.buttonX264Path.Location = new System.Drawing.Point(355, 108);
            this.buttonX264Path.Name = "buttonX264Path";
            this.buttonX264Path.Size = new System.Drawing.Size(39, 23);
            this.buttonX264Path.TabIndex = 8;
            this.buttonX264Path.Text = "...";
            this.buttonX264Path.UseVisualStyleBackColor = true;
            this.buttonX264Path.Click += new System.EventHandler(this.buttonX264Path_Click);
            // 
            // textBoxX264Path
            // 
            this.textBoxX264Path.Location = new System.Drawing.Point(6, 110);
            this.textBoxX264Path.Name = "textBoxX264Path";
            this.textBoxX264Path.ReadOnly = true;
            this.textBoxX264Path.Size = new System.Drawing.Size(343, 20);
            this.textBoxX264Path.TabIndex = 7;
            // 
            // labelX264Path
            // 
            this.labelX264Path.AutoSize = true;
            this.labelX264Path.Location = new System.Drawing.Point(6, 94);
            this.labelX264Path.Name = "labelX264Path";
            this.labelX264Path.Size = new System.Drawing.Size(90, 13);
            this.labelX264Path.TabIndex = 6;
            this.labelX264Path.Text = "Path to x264.exe:";
            // 
            // buttonFfmsindexPath
            // 
            this.buttonFfmsindexPath.Location = new System.Drawing.Point(355, 69);
            this.buttonFfmsindexPath.Name = "buttonFfmsindexPath";
            this.buttonFfmsindexPath.Size = new System.Drawing.Size(39, 23);
            this.buttonFfmsindexPath.TabIndex = 5;
            this.buttonFfmsindexPath.Text = "...";
            this.buttonFfmsindexPath.UseVisualStyleBackColor = true;
            this.buttonFfmsindexPath.Click += new System.EventHandler(this.buttonFfmsindexPath_Click);
            // 
            // textBoxFfmsindexPath
            // 
            this.textBoxFfmsindexPath.Location = new System.Drawing.Point(6, 71);
            this.textBoxFfmsindexPath.Name = "textBoxFfmsindexPath";
            this.textBoxFfmsindexPath.ReadOnly = true;
            this.textBoxFfmsindexPath.Size = new System.Drawing.Size(343, 20);
            this.textBoxFfmsindexPath.TabIndex = 4;
            // 
            // labelFfmsindexPath
            // 
            this.labelFfmsindexPath.AutoSize = true;
            this.labelFfmsindexPath.Location = new System.Drawing.Point(6, 55);
            this.labelFfmsindexPath.Name = "labelFfmsindexPath";
            this.labelFfmsindexPath.Size = new System.Drawing.Size(111, 13);
            this.labelFfmsindexPath.TabIndex = 3;
            this.labelFfmsindexPath.Text = "Path to ffmsindex.exe:";
            // 
            // textBoxEac3toPath
            // 
            this.textBoxEac3toPath.Location = new System.Drawing.Point(6, 32);
            this.textBoxEac3toPath.Name = "textBoxEac3toPath";
            this.textBoxEac3toPath.ReadOnly = true;
            this.textBoxEac3toPath.Size = new System.Drawing.Size(343, 20);
            this.textBoxEac3toPath.TabIndex = 1;
            // 
            // labelEac3toPath
            // 
            this.labelEac3toPath.AutoSize = true;
            this.labelEac3toPath.Location = new System.Drawing.Point(6, 16);
            this.labelEac3toPath.Name = "labelEac3toPath";
            this.labelEac3toPath.Size = new System.Drawing.Size(100, 13);
            this.labelEac3toPath.TabIndex = 2;
            this.labelEac3toPath.Text = "Path to eac3to.exe:";
            // 
            // buttonEac3toPath
            // 
            this.buttonEac3toPath.Location = new System.Drawing.Point(355, 29);
            this.buttonEac3toPath.Name = "buttonEac3toPath";
            this.buttonEac3toPath.Size = new System.Drawing.Size(39, 23);
            this.buttonEac3toPath.TabIndex = 0;
            this.buttonEac3toPath.Text = "...";
            this.buttonEac3toPath.UseVisualStyleBackColor = true;
            this.buttonEac3toPath.Click += new System.EventHandler(this.buttonEac3toPath_Click);
            // 
            // groupBoxAutoSelect
            // 
            this.groupBoxAutoSelect.Controls.Add(this.buttonLangDown);
            this.groupBoxAutoSelect.Controls.Add(this.buttonLangUp);
            this.groupBoxAutoSelect.Controls.Add(this.buttonDeleteLanguage);
            this.groupBoxAutoSelect.Controls.Add(this.buttonAddLanguage);
            this.groupBoxAutoSelect.Controls.Add(this.checkBoxIncludeSubtitle);
            this.groupBoxAutoSelect.Controls.Add(this.listBoxPreferedLanguages);
            this.groupBoxAutoSelect.Controls.Add(this.labelPreferedLanguage);
            this.groupBoxAutoSelect.Controls.Add(this.checkBoxPreferDts);
            this.groupBoxAutoSelect.Controls.Add(this.checkBoxSelectChapters);
            this.groupBoxAutoSelect.Controls.Add(this.checkBoxAutoSelect);
            this.groupBoxAutoSelect.Location = new System.Drawing.Point(412, 6);
            this.groupBoxAutoSelect.Name = "groupBoxAutoSelect";
            this.groupBoxAutoSelect.Size = new System.Drawing.Size(455, 182);
            this.groupBoxAutoSelect.TabIndex = 3;
            this.groupBoxAutoSelect.TabStop = false;
            this.groupBoxAutoSelect.Text = "Autoselect streams";
            // 
            // buttonLangDown
            // 
            this.buttonLangDown.Location = new System.Drawing.Point(374, 67);
            this.buttonLangDown.Name = "buttonLangDown";
            this.buttonLangDown.Size = new System.Drawing.Size(75, 23);
            this.buttonLangDown.TabIndex = 9;
            this.buttonLangDown.Text = "Move down";
            this.buttonLangDown.UseVisualStyleBackColor = true;
            this.buttonLangDown.Click += new System.EventHandler(this.buttonLangDown_Click);
            // 
            // buttonLangUp
            // 
            this.buttonLangUp.Location = new System.Drawing.Point(374, 38);
            this.buttonLangUp.Name = "buttonLangUp";
            this.buttonLangUp.Size = new System.Drawing.Size(75, 23);
            this.buttonLangUp.TabIndex = 8;
            this.buttonLangUp.Text = "Move up";
            this.buttonLangUp.UseVisualStyleBackColor = true;
            this.buttonLangUp.Click += new System.EventHandler(this.buttonLangUp_Click);
            // 
            // buttonDeleteLanguage
            // 
            this.buttonDeleteLanguage.Location = new System.Drawing.Point(210, 139);
            this.buttonDeleteLanguage.Name = "buttonDeleteLanguage";
            this.buttonDeleteLanguage.Size = new System.Drawing.Size(75, 23);
            this.buttonDeleteLanguage.TabIndex = 7;
            this.buttonDeleteLanguage.Text = "Delete";
            this.buttonDeleteLanguage.UseVisualStyleBackColor = true;
            this.buttonDeleteLanguage.Click += new System.EventHandler(this.buttonDeleteLanguage_Click);
            // 
            // buttonAddLanguage
            // 
            this.buttonAddLanguage.Location = new System.Drawing.Point(129, 139);
            this.buttonAddLanguage.Name = "buttonAddLanguage";
            this.buttonAddLanguage.Size = new System.Drawing.Size(75, 23);
            this.buttonAddLanguage.TabIndex = 6;
            this.buttonAddLanguage.Text = "Add";
            this.buttonAddLanguage.UseVisualStyleBackColor = true;
            this.buttonAddLanguage.Click += new System.EventHandler(this.buttonAddLanguage_Click);
            // 
            // checkBoxIncludeSubtitle
            // 
            this.checkBoxIncludeSubtitle.AutoSize = true;
            this.checkBoxIncludeSubtitle.Location = new System.Drawing.Point(6, 88);
            this.checkBoxIncludeSubtitle.Name = "checkBoxIncludeSubtitle";
            this.checkBoxIncludeSubtitle.Size = new System.Drawing.Size(102, 17);
            this.checkBoxIncludeSubtitle.TabIndex = 5;
            this.checkBoxIncludeSubtitle.Text = "Include subtitles";
            this.checkBoxIncludeSubtitle.UseVisualStyleBackColor = true;
            this.checkBoxIncludeSubtitle.CheckedChanged += new System.EventHandler(this.checkBoxIncludeSubtitle_CheckedChanged);
            // 
            // listBoxPreferedLanguages
            // 
            this.listBoxPreferedLanguages.FormattingEnabled = true;
            this.listBoxPreferedLanguages.Location = new System.Drawing.Point(129, 38);
            this.listBoxPreferedLanguages.Name = "listBoxPreferedLanguages";
            this.listBoxPreferedLanguages.Size = new System.Drawing.Size(239, 95);
            this.listBoxPreferedLanguages.TabIndex = 4;
            this.listBoxPreferedLanguages.DoubleClick += new System.EventHandler(this.listBoxPreferedLanguages_DoubleClick);
            // 
            // labelPreferedLanguage
            // 
            this.labelPreferedLanguage.AutoSize = true;
            this.labelPreferedLanguage.Location = new System.Drawing.Point(126, 16);
            this.labelPreferedLanguage.Name = "labelPreferedLanguage";
            this.labelPreferedLanguage.Size = new System.Drawing.Size(220, 13);
            this.labelPreferedLanguage.TabIndex = 3;
            this.labelPreferedLanguage.Text = "Prefered language (first will be set as default):";
            // 
            // checkBoxPreferDts
            // 
            this.checkBoxPreferDts.AutoSize = true;
            this.checkBoxPreferDts.Location = new System.Drawing.Point(6, 65);
            this.checkBoxPreferDts.Name = "checkBoxPreferDts";
            this.checkBoxPreferDts.Size = new System.Drawing.Size(108, 17);
            this.checkBoxPreferDts.TabIndex = 2;
            this.checkBoxPreferDts.Text = "Prefer DTS audio";
            this.checkBoxPreferDts.UseVisualStyleBackColor = true;
            this.checkBoxPreferDts.CheckedChanged += new System.EventHandler(this.checkBoxPreferDts_CheckedChanged);
            // 
            // checkBoxSelectChapters
            // 
            this.checkBoxSelectChapters.AutoSize = true;
            this.checkBoxSelectChapters.Location = new System.Drawing.Point(6, 42);
            this.checkBoxSelectChapters.Name = "checkBoxSelectChapters";
            this.checkBoxSelectChapters.Size = new System.Drawing.Size(105, 17);
            this.checkBoxSelectChapters.TabIndex = 1;
            this.checkBoxSelectChapters.Text = "Include chapters";
            this.checkBoxSelectChapters.UseVisualStyleBackColor = true;
            this.checkBoxSelectChapters.CheckedChanged += new System.EventHandler(this.checkBoxSelectChapters_CheckedChanged);
            // 
            // checkBoxAutoSelect
            // 
            this.checkBoxAutoSelect.AutoSize = true;
            this.checkBoxAutoSelect.Location = new System.Drawing.Point(6, 19);
            this.checkBoxAutoSelect.Name = "checkBoxAutoSelect";
            this.checkBoxAutoSelect.Size = new System.Drawing.Size(97, 17);
            this.checkBoxAutoSelect.TabIndex = 0;
            this.checkBoxAutoSelect.Text = "Use autoselect";
            this.checkBoxAutoSelect.UseVisualStyleBackColor = true;
            this.checkBoxAutoSelect.CheckedChanged += new System.EventHandler(this.checkBoxAutoSelect_CheckedChanged);
            // 
            // tabPageEncodingSettings
            // 
            this.tabPageEncodingSettings.Controls.Add(this.groupBoxAvisynthCommand);
            this.tabPageEncodingSettings.Controls.Add(this.labelX264Priority);
            this.tabPageEncodingSettings.Controls.Add(this.comboBoxX264Priority);
            this.tabPageEncodingSettings.Controls.Add(this.groupBoxX264Profiles);
            this.tabPageEncodingSettings.Controls.Add(this.groupBoxGeneralAviSynthSettings);
            this.tabPageEncodingSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageEncodingSettings.Name = "tabPageEncodingSettings";
            this.tabPageEncodingSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEncodingSettings.Size = new System.Drawing.Size(1149, 344);
            this.tabPageEncodingSettings.TabIndex = 4;
            this.tabPageEncodingSettings.Text = "Encoding settings";
            this.tabPageEncodingSettings.UseVisualStyleBackColor = true;
            // 
            // groupBoxAvisynthCommand
            // 
            this.groupBoxAvisynthCommand.Controls.Add(this.richTextBoxCommandsAfterResize);
            this.groupBoxAvisynthCommand.Controls.Add(this.labelCommandAfterResize);
            this.groupBoxAvisynthCommand.Location = new System.Drawing.Point(6, 79);
            this.groupBoxAvisynthCommand.Name = "groupBoxAvisynthCommand";
            this.groupBoxAvisynthCommand.Size = new System.Drawing.Size(334, 132);
            this.groupBoxAvisynthCommand.TabIndex = 14;
            this.groupBoxAvisynthCommand.TabStop = false;
            this.groupBoxAvisynthCommand.Text = "AviSynth commands";
            // 
            // richTextBoxCommandsAfterResize
            // 
            this.richTextBoxCommandsAfterResize.Location = new System.Drawing.Point(6, 32);
            this.richTextBoxCommandsAfterResize.Name = "richTextBoxCommandsAfterResize";
            this.richTextBoxCommandsAfterResize.Size = new System.Drawing.Size(322, 94);
            this.richTextBoxCommandsAfterResize.TabIndex = 1;
            this.richTextBoxCommandsAfterResize.Text = "";
            this.richTextBoxCommandsAfterResize.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // labelCommandAfterResize
            // 
            this.labelCommandAfterResize.AutoSize = true;
            this.labelCommandAfterResize.Location = new System.Drawing.Point(6, 16);
            this.labelCommandAfterResize.Name = "labelCommandAfterResize";
            this.labelCommandAfterResize.Size = new System.Drawing.Size(157, 13);
            this.labelCommandAfterResize.TabIndex = 0;
            this.labelCommandAfterResize.Text = "AviSynth commands after resize";
            // 
            // labelX264Priority
            // 
            this.labelX264Priority.AutoSize = true;
            this.labelX264Priority.Location = new System.Drawing.Point(9, 214);
            this.labelX264Priority.Name = "labelX264Priority";
            this.labelX264Priority.Size = new System.Drawing.Size(103, 13);
            this.labelX264Priority.TabIndex = 13;
            this.labelX264Priority.Text = "x264 process priority";
            // 
            // comboBoxX264Priority
            // 
            this.comboBoxX264Priority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxX264Priority.FormattingEnabled = true;
            this.comboBoxX264Priority.Location = new System.Drawing.Point(9, 230);
            this.comboBoxX264Priority.Name = "comboBoxX264Priority";
            this.comboBoxX264Priority.Size = new System.Drawing.Size(334, 21);
            this.comboBoxX264Priority.TabIndex = 12;
            this.comboBoxX264Priority.SelectedIndexChanged += new System.EventHandler(this.comboBoxX264Priority_SelectedIndexChanged);
            // 
            // groupBoxX264Profiles
            // 
            this.groupBoxX264Profiles.Controls.Add(this.labelX264);
            this.groupBoxX264Profiles.Controls.Add(this.buttonDelX264);
            this.groupBoxX264Profiles.Controls.Add(this.listBoxX264Profiles);
            this.groupBoxX264Profiles.Controls.Add(this.buttonAddX264);
            this.groupBoxX264Profiles.Location = new System.Drawing.Point(346, 6);
            this.groupBoxX264Profiles.Name = "groupBoxX264Profiles";
            this.groupBoxX264Profiles.Size = new System.Drawing.Size(351, 245);
            this.groupBoxX264Profiles.TabIndex = 11;
            this.groupBoxX264Profiles.TabStop = false;
            this.groupBoxX264Profiles.Text = "Encoding profiles";
            // 
            // labelX264
            // 
            this.labelX264.AutoSize = true;
            this.labelX264.Location = new System.Drawing.Point(6, 16);
            this.labelX264.Name = "labelX264";
            this.labelX264.Size = new System.Drawing.Size(66, 13);
            this.labelX264.TabIndex = 7;
            this.labelX264.Text = "x264 profiles";
            // 
            // buttonDelX264
            // 
            this.buttonDelX264.Location = new System.Drawing.Point(90, 216);
            this.buttonDelX264.Name = "buttonDelX264";
            this.buttonDelX264.Size = new System.Drawing.Size(75, 23);
            this.buttonDelX264.TabIndex = 10;
            this.buttonDelX264.Text = "Delete";
            this.buttonDelX264.UseVisualStyleBackColor = true;
            this.buttonDelX264.Click += new System.EventHandler(this.buttonDelX264_Click);
            // 
            // listBoxX264Profiles
            // 
            this.listBoxX264Profiles.FormattingEnabled = true;
            this.listBoxX264Profiles.Location = new System.Drawing.Point(9, 32);
            this.listBoxX264Profiles.Name = "listBoxX264Profiles";
            this.listBoxX264Profiles.Size = new System.Drawing.Size(331, 173);
            this.listBoxX264Profiles.TabIndex = 8;
            this.listBoxX264Profiles.DoubleClick += new System.EventHandler(this.listBoxX264Profiles_DoubleClick);
            // 
            // buttonAddX264
            // 
            this.buttonAddX264.Location = new System.Drawing.Point(9, 216);
            this.buttonAddX264.Name = "buttonAddX264";
            this.buttonAddX264.Size = new System.Drawing.Size(75, 23);
            this.buttonAddX264.TabIndex = 9;
            this.buttonAddX264.Text = "Add";
            this.buttonAddX264.UseVisualStyleBackColor = true;
            this.buttonAddX264.Click += new System.EventHandler(this.buttonAddX264_Click);
            // 
            // groupBoxGeneralAviSynthSettings
            // 
            this.groupBoxGeneralAviSynthSettings.Controls.Add(this.checkBoxEncodeDirectshow);
            this.groupBoxGeneralAviSynthSettings.Controls.Add(this.checkBoxCropDirectshow);
            this.groupBoxGeneralAviSynthSettings.Location = new System.Drawing.Point(6, 6);
            this.groupBoxGeneralAviSynthSettings.Name = "groupBoxGeneralAviSynthSettings";
            this.groupBoxGeneralAviSynthSettings.Size = new System.Drawing.Size(334, 67);
            this.groupBoxGeneralAviSynthSettings.TabIndex = 6;
            this.groupBoxGeneralAviSynthSettings.TabStop = false;
            this.groupBoxGeneralAviSynthSettings.Text = "General AviSynth settings";
            // 
            // checkBoxEncodeDirectshow
            // 
            this.checkBoxEncodeDirectshow.AutoSize = true;
            this.checkBoxEncodeDirectshow.Location = new System.Drawing.Point(6, 42);
            this.checkBoxEncodeDirectshow.Name = "checkBoxEncodeDirectshow";
            this.checkBoxEncodeDirectshow.Size = new System.Drawing.Size(315, 17);
            this.checkBoxEncodeDirectshow.TabIndex = 2;
            this.checkBoxEncodeDirectshow.Text = "Use DirectShowSource for encode avs (else FFVideoSource)";
            this.checkBoxEncodeDirectshow.UseVisualStyleBackColor = true;
            this.checkBoxEncodeDirectshow.CheckedChanged += new System.EventHandler(this.checkBoxEncodeDirectshow_CheckedChanged);
            // 
            // checkBoxCropDirectshow
            // 
            this.checkBoxCropDirectshow.AutoSize = true;
            this.checkBoxCropDirectshow.Location = new System.Drawing.Point(6, 19);
            this.checkBoxCropDirectshow.Name = "checkBoxCropDirectshow";
            this.checkBoxCropDirectshow.Size = new System.Drawing.Size(300, 17);
            this.checkBoxCropDirectshow.TabIndex = 1;
            this.checkBoxCropDirectshow.Text = "Use DirectShowSource for crop avs (else FFVideoSource)";
            this.checkBoxCropDirectshow.UseVisualStyleBackColor = true;
            this.checkBoxCropDirectshow.CheckedChanged += new System.EventHandler(this.checkBoxCropDirectshow_CheckedChanged);
            // 
            // tabPageSoftware
            // 
            this.tabPageSoftware.Controls.Add(this.linkLabelAnyDvd);
            this.tabPageSoftware.Controls.Add(this.linkLabelFilterTweaker);
            this.tabPageSoftware.Controls.Add(this.linkLabelMkvtoolnix);
            this.tabPageSoftware.Controls.Add(this.linkLabelJava);
            this.tabPageSoftware.Controls.Add(this.linkLabelFFMpegSrc);
            this.tabPageSoftware.Controls.Add(this.linkLabelBDSup2sub);
            this.tabPageSoftware.Controls.Add(this.linkLabelX264);
            this.tabPageSoftware.Controls.Add(this.linkLabelEac3to);
            this.tabPageSoftware.Controls.Add(this.linkLabelHaali);
            this.tabPageSoftware.Controls.Add(this.linkLabelAviSynth);
            this.tabPageSoftware.Controls.Add(this.labelSoftwareDesc);
            this.tabPageSoftware.Location = new System.Drawing.Point(4, 22);
            this.tabPageSoftware.Name = "tabPageSoftware";
            this.tabPageSoftware.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSoftware.Size = new System.Drawing.Size(1149, 344);
            this.tabPageSoftware.TabIndex = 5;
            this.tabPageSoftware.Text = "Needed tools";
            this.tabPageSoftware.UseVisualStyleBackColor = true;
            // 
            // linkLabelAnyDvd
            // 
            this.linkLabelAnyDvd.AutoSize = true;
            this.linkLabelAnyDvd.Location = new System.Drawing.Point(6, 133);
            this.linkLabelAnyDvd.Name = "linkLabelAnyDvd";
            this.linkLabelAnyDvd.Size = new System.Drawing.Size(64, 13);
            this.linkLabelAnyDvd.TabIndex = 10;
            this.linkLabelAnyDvd.TabStop = true;
            this.linkLabelAnyDvd.Text = "AnyDvd HD";
            this.linkLabelAnyDvd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAnyDvd_LinkClicked);
            // 
            // linkLabelFilterTweaker
            // 
            this.linkLabelFilterTweaker.AutoSize = true;
            this.linkLabelFilterTweaker.Location = new System.Drawing.Point(6, 120);
            this.linkLabelFilterTweaker.Name = "linkLabelFilterTweaker";
            this.linkLabelFilterTweaker.Size = new System.Drawing.Size(191, 13);
            this.linkLabelFilterTweaker.TabIndex = 9;
            this.linkLabelFilterTweaker.TabStop = true;
            this.linkLabelFilterTweaker.Text = "Preferred Filter Tweaker for Windows 7";
            this.linkLabelFilterTweaker.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFilterTweaker_LinkClicked);
            // 
            // linkLabelMkvtoolnix
            // 
            this.linkLabelMkvtoolnix.AutoSize = true;
            this.linkLabelMkvtoolnix.Location = new System.Drawing.Point(6, 107);
            this.linkLabelMkvtoolnix.Name = "linkLabelMkvtoolnix";
            this.linkLabelMkvtoolnix.Size = new System.Drawing.Size(57, 13);
            this.linkLabelMkvtoolnix.TabIndex = 8;
            this.linkLabelMkvtoolnix.TabStop = true;
            this.linkLabelMkvtoolnix.Text = "mkvtoolnix";
            this.linkLabelMkvtoolnix.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMkvtoolnix_LinkClicked);
            // 
            // linkLabelJava
            // 
            this.linkLabelJava.AutoSize = true;
            this.linkLabelJava.Location = new System.Drawing.Point(6, 94);
            this.linkLabelJava.Name = "linkLabelJava";
            this.linkLabelJava.Size = new System.Drawing.Size(30, 13);
            this.linkLabelJava.TabIndex = 7;
            this.linkLabelJava.TabStop = true;
            this.linkLabelJava.Text = "Java";
            this.linkLabelJava.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelJava_LinkClicked);
            // 
            // linkLabelFFMpegSrc
            // 
            this.linkLabelFFMpegSrc.AutoSize = true;
            this.linkLabelFFMpegSrc.Location = new System.Drawing.Point(6, 81);
            this.linkLabelFFMpegSrc.Name = "linkLabelFFMpegSrc";
            this.linkLabelFFMpegSrc.Size = new System.Drawing.Size(62, 13);
            this.linkLabelFFMpegSrc.TabIndex = 6;
            this.linkLabelFFMpegSrc.TabStop = true;
            this.linkLabelFFMpegSrc.Text = "FFMpegSrc";
            this.linkLabelFFMpegSrc.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFFMpegSrc_LinkClicked);
            // 
            // linkLabelBDSup2sub
            // 
            this.linkLabelBDSup2sub.AutoSize = true;
            this.linkLabelBDSup2sub.Location = new System.Drawing.Point(6, 68);
            this.linkLabelBDSup2sub.Name = "linkLabelBDSup2sub";
            this.linkLabelBDSup2sub.Size = new System.Drawing.Size(64, 13);
            this.linkLabelBDSup2sub.TabIndex = 5;
            this.linkLabelBDSup2sub.TabStop = true;
            this.linkLabelBDSup2sub.Text = "BDSup2sub";
            this.linkLabelBDSup2sub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelBDSup2sub_LinkClicked);
            // 
            // linkLabelX264
            // 
            this.linkLabelX264.AutoSize = true;
            this.linkLabelX264.Location = new System.Drawing.Point(6, 55);
            this.linkLabelX264.Name = "linkLabelX264";
            this.linkLabelX264.Size = new System.Drawing.Size(30, 13);
            this.linkLabelX264.TabIndex = 4;
            this.linkLabelX264.TabStop = true;
            this.linkLabelX264.Text = "x264";
            this.linkLabelX264.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelX264_LinkClicked);
            // 
            // linkLabelEac3to
            // 
            this.linkLabelEac3to.AutoSize = true;
            this.linkLabelEac3to.Location = new System.Drawing.Point(6, 42);
            this.linkLabelEac3to.Name = "linkLabelEac3to";
            this.linkLabelEac3to.Size = new System.Drawing.Size(41, 13);
            this.linkLabelEac3to.TabIndex = 3;
            this.linkLabelEac3to.TabStop = true;
            this.linkLabelEac3to.Text = "Eac3to";
            this.linkLabelEac3to.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelEac3to_LinkClicked);
            // 
            // linkLabelHaali
            // 
            this.linkLabelHaali.AutoSize = true;
            this.linkLabelHaali.Location = new System.Drawing.Point(6, 29);
            this.linkLabelHaali.Name = "linkLabelHaali";
            this.linkLabelHaali.Size = new System.Drawing.Size(98, 13);
            this.linkLabelHaali.TabIndex = 2;
            this.linkLabelHaali.TabStop = true;
            this.linkLabelHaali.Text = "Haali Media Splitter";
            this.linkLabelHaali.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHaali_LinkClicked);
            // 
            // linkLabelAviSynth
            // 
            this.linkLabelAviSynth.AutoSize = true;
            this.linkLabelAviSynth.Location = new System.Drawing.Point(6, 16);
            this.linkLabelAviSynth.Name = "linkLabelAviSynth";
            this.linkLabelAviSynth.Size = new System.Drawing.Size(49, 13);
            this.linkLabelAviSynth.TabIndex = 1;
            this.linkLabelAviSynth.TabStop = true;
            this.linkLabelAviSynth.Text = "AviSynth";
            this.linkLabelAviSynth.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAviSynth_LinkClicked);
            // 
            // labelSoftwareDesc
            // 
            this.labelSoftwareDesc.AutoSize = true;
            this.labelSoftwareDesc.Location = new System.Drawing.Point(6, 3);
            this.labelSoftwareDesc.Name = "labelSoftwareDesc";
            this.labelSoftwareDesc.Size = new System.Drawing.Size(221, 13);
            this.labelSoftwareDesc.TabIndex = 0;
            this.labelSoftwareDesc.Text = "The following tools are needed to use BluRip:";
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.richTextBoxAbout);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(1149, 344);
            this.tabPageAbout.TabIndex = 6;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // richTextBoxAbout
            // 
            this.richTextBoxAbout.Location = new System.Drawing.Point(6, 6);
            this.richTextBoxAbout.Name = "richTextBoxAbout";
            this.richTextBoxAbout.ReadOnly = true;
            this.richTextBoxAbout.Size = new System.Drawing.Size(386, 222);
            this.richTextBoxAbout.TabIndex = 0;
            this.richTextBoxAbout.Text = resources.GetString("richTextBoxAbout.Text");
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 430);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(1157, 346);
            this.richTextBoxLog.TabIndex = 1;
            this.richTextBoxLog.Text = "";
            // 
            // progressBarMain
            // 
            this.progressBarMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarMain.Location = new System.Drawing.Point(12, 388);
            this.progressBarMain.Name = "progressBarMain";
            this.progressBarMain.Size = new System.Drawing.Size(1006, 23);
            this.progressBarMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarMain.TabIndex = 3;
            this.progressBarMain.Visible = false;
            // 
            // buttonAbort
            // 
            this.buttonAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAbort.Location = new System.Drawing.Point(1024, 388);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(75, 23);
            this.buttonAbort.TabIndex = 4;
            this.buttonAbort.Text = "Abort";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Visible = false;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // labelLog
            // 
            this.labelLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(12, 414);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(28, 13);
            this.labelLog.TabIndex = 5;
            this.labelLog.Text = "Log:";
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
            this.notifyIconMain.Text = "notifyIcon1";
            this.notifyIconMain.DoubleClick += new System.EventHandler(this.notifyIconMain_DoubleClick);
            // 
            // buttonMinimize
            // 
            this.buttonMinimize.Location = new System.Drawing.Point(1105, 388);
            this.buttonMinimize.Name = "buttonMinimize";
            this.buttonMinimize.Size = new System.Drawing.Size(60, 23);
            this.buttonMinimize.TabIndex = 6;
            this.buttonMinimize.Text = "Minimize";
            this.buttonMinimize.UseVisualStyleBackColor = true;
            this.buttonMinimize.Click += new System.EventHandler(this.buttonMinimize_Click);
            // 
            // comboBoxCropMode
            // 
            this.comboBoxCropMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCropMode.FormattingEnabled = true;
            this.comboBoxCropMode.Items.AddRange(new object[] {
            "Overcrop - Height mod 8",
            "Overcrop - Height mod 16",
            "Undercrop - Height mod 8",
            "Undercrop - Height mod 16",
            "Resize - Height mod 8",
            "Resize - Height mod 16",
            "AddBorder - Height mod 8",
            "AddBorder - Height mod 16"});
            this.comboBoxCropMode.Location = new System.Drawing.Point(6, 112);
            this.comboBoxCropMode.Name = "comboBoxCropMode";
            this.comboBoxCropMode.Size = new System.Drawing.Size(182, 21);
            this.comboBoxCropMode.TabIndex = 7;
            this.comboBoxCropMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxCropMode_SelectedIndexChanged);
            // 
            // labelCropMode
            // 
            this.labelCropMode.AutoSize = true;
            this.labelCropMode.Location = new System.Drawing.Point(6, 95);
            this.labelCropMode.Name = "labelCropMode";
            this.labelCropMode.Size = new System.Drawing.Size(98, 13);
            this.labelCropMode.TabIndex = 8;
            this.labelCropMode.Text = "Crop/Resize mode:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1181, 788);
            this.Controls.Add(this.buttonMinimize);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.buttonAbort);
            this.Controls.Add(this.progressBarMain);
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.tabControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BluRip 1080p";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageStreamSelect.ResumeLayout(false);
            this.tabPageStreamSelect.PerformLayout();
            this.tabPageProcess.ResumeLayout(false);
            this.tabPageProcess.PerformLayout();
            this.groupBoxPath.ResumeLayout(false);
            this.groupBoxPath.PerformLayout();
            this.tabPageSettings.ResumeLayout(false);
            this.groupBoxDefaultTrack.ResumeLayout(false);
            this.groupBoxDefaultTrack.PerformLayout();
            this.groupBoxAutoCrop.ResumeLayout(false);
            this.groupBoxAutoCrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlackValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNrFrames)).EndInit();
            this.groupBoxExternalTools.ResumeLayout(false);
            this.groupBoxExternalTools.PerformLayout();
            this.groupBoxAutoSelect.ResumeLayout(false);
            this.groupBoxAutoSelect.PerformLayout();
            this.tabPageEncodingSettings.ResumeLayout(false);
            this.tabPageEncodingSettings.PerformLayout();
            this.groupBoxAvisynthCommand.ResumeLayout(false);
            this.groupBoxAvisynthCommand.PerformLayout();
            this.groupBoxX264Profiles.ResumeLayout(false);
            this.groupBoxX264Profiles.PerformLayout();
            this.groupBoxGeneralAviSynthSettings.ResumeLayout(false);
            this.groupBoxGeneralAviSynthSettings.PerformLayout();
            this.tabPageSoftware.ResumeLayout(false);
            this.tabPageSoftware.PerformLayout();
            this.tabPageAbout.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageStreamSelect;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.Button buttonGetStreamInfo;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.Label labelEac3toPath;
        private System.Windows.Forms.TextBox textBoxEac3toPath;
        private System.Windows.Forms.Button buttonEac3toPath;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Label labelStreams;
        private System.Windows.Forms.ListBox listBoxStreams;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.ComboBox comboBoxTitle;
        private System.Windows.Forms.GroupBox groupBoxAutoSelect;
        private System.Windows.Forms.CheckBox checkBoxPreferDts;
        private System.Windows.Forms.CheckBox checkBoxSelectChapters;
        private System.Windows.Forms.CheckBox checkBoxAutoSelect;
        private System.Windows.Forms.GroupBox groupBoxExternalTools;
        private System.Windows.Forms.Button buttonDeleteLanguage;
        private System.Windows.Forms.Button buttonAddLanguage;
        private System.Windows.Forms.CheckBox checkBoxIncludeSubtitle;
        private System.Windows.Forms.ListBox listBoxPreferedLanguages;
        private System.Windows.Forms.Label labelPreferedLanguage;
        private System.Windows.Forms.ProgressBar progressBarMain;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.Label labelWorkingDir;
        private System.Windows.Forms.Button buttonWorkingDir;
        private System.Windows.Forms.TextBox textBoxWorkingDir;
        private System.Windows.Forms.Button buttonStartConvert;
        private System.Windows.Forms.Button buttonFfmsindexPath;
        private System.Windows.Forms.TextBox textBoxFfmsindexPath;
        private System.Windows.Forms.Label labelFfmsindexPath;
        private System.Windows.Forms.Button buttonSup2subPath;
        private System.Windows.Forms.TextBox textBoxSup2subPath;
        private System.Windows.Forms.Label labelSup2subPath;
        private System.Windows.Forms.Button buttonX264Path;
        private System.Windows.Forms.TextBox textBoxX264Path;
        private System.Windows.Forms.Label labelX264Path;
        private System.Windows.Forms.TabPage tabPageProcess;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.GroupBox groupBoxPath;
        private System.Windows.Forms.TextBox textBoxFilePrefix;
        private System.Windows.Forms.Label labelFilePrefix;
        private System.Windows.Forms.Button buttonJavaPath;
        private System.Windows.Forms.TextBox textBoxJavaPath;
        private System.Windows.Forms.Label labelJavaPath;
        private System.Windows.Forms.GroupBox groupBoxAutoCrop;
        private System.Windows.Forms.Label labelBlackValue;
        private System.Windows.Forms.Label labelNrFrames;
        private System.Windows.Forms.NumericUpDown numericUpDownBlackValue;
        private System.Windows.Forms.NumericUpDown numericUpDownNrFrames;
        private System.Windows.Forms.Label labelMuxedStreams;
        private System.Windows.Forms.ListBox listBoxDemuxedStreams;
        private System.Windows.Forms.Button buttonClearStreamInfoList;
        private System.Windows.Forms.Button buttonLoadStreamInfo;
        private System.Windows.Forms.GroupBox groupBoxGeneralAviSynthSettings;
        private System.Windows.Forms.TabPage tabPageEncodingSettings;
        private System.Windows.Forms.CheckBox checkBoxEncodeDirectshow;
        private System.Windows.Forms.CheckBox checkBoxCropDirectshow;
        private System.Windows.Forms.Label labelX264;
        private System.Windows.Forms.Button buttonDelX264;
        private System.Windows.Forms.Button buttonAddX264;
        private System.Windows.Forms.ListBox listBoxX264Profiles;
        private System.Windows.Forms.GroupBox groupBoxX264Profiles;
        private System.Windows.Forms.Button buttonDoDemux;
        private System.Windows.Forms.Button buttonDoIndex;
        private System.Windows.Forms.Button buttonDoEncode;
        private System.Windows.Forms.Label labelEncodeProfile;
        private System.Windows.Forms.ComboBox comboBoxEncodeProfile;
        private System.Windows.Forms.Button buttonDoSubtitle;
        private System.Windows.Forms.TabPage tabPageSoftware;
        private System.Windows.Forms.LinkLabel linkLabelHaali;
        private System.Windows.Forms.LinkLabel linkLabelAviSynth;
        private System.Windows.Forms.Label labelSoftwareDesc;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.LinkLabel linkLabelFFMpegSrc;
        private System.Windows.Forms.LinkLabel linkLabelBDSup2sub;
        private System.Windows.Forms.LinkLabel linkLabelX264;
        private System.Windows.Forms.LinkLabel linkLabelEac3to;
        private System.Windows.Forms.LinkLabel linkLabelJava;
        private System.Windows.Forms.RichTextBox richTextBoxAbout;
        private System.Windows.Forms.Button buttonMkvmergePath;
        private System.Windows.Forms.TextBox textBoxMkvmergePath;
        private System.Windows.Forms.Label labelMkvmergePath;
        private System.Windows.Forms.LinkLabel linkLabelMkvtoolnix;
        private System.Windows.Forms.Label labelX264Priority;
        private System.Windows.Forms.ComboBox comboBoxX264Priority;
        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.Button buttonMinimize;
        private System.Windows.Forms.LinkLabel linkLabelFilterTweaker;
        private System.Windows.Forms.Button buttonDoMux;
        private System.Windows.Forms.TextBox textBoxTargetfilename;
        private System.Windows.Forms.Label labelTargetFilename;
        private System.Windows.Forms.TextBox textBoxTargetFolder;
        private System.Windows.Forms.Label labelTargetFolder;
        private System.Windows.Forms.Button buttonTargetfolder;
        private System.Windows.Forms.TextBox textBoxMovieTitle;
        private System.Windows.Forms.Label labelMovieTitle;
        private System.Windows.Forms.Button buttonStreamDown;
        private System.Windows.Forms.Button buttonStreamUp;
        private System.Windows.Forms.Button buttonLangDown;
        private System.Windows.Forms.Button buttonLangUp;
        private System.Windows.Forms.GroupBox groupBoxDefaultTrack;
        private System.Windows.Forms.CheckBox checkBoxDefaultSubtitleForced;
        private System.Windows.Forms.CheckBox checkBoxDefaultSubtitleTrack;
        private System.Windows.Forms.CheckBox checkBoxDefaultAudioTrack;
        private System.Windows.Forms.GroupBox groupBoxAvisynthCommand;
        private System.Windows.Forms.RichTextBox richTextBoxCommandsAfterResize;
        private System.Windows.Forms.Label labelCommandAfterResize;
        private System.Windows.Forms.CheckBox checkBoxDeleteAfterEncode;
        private System.Windows.Forms.LinkLabel linkLabelAnyDvd;
        private System.Windows.Forms.ComboBox comboBoxCropMode;
        private System.Windows.Forms.Label labelCropMode;
    }
}

