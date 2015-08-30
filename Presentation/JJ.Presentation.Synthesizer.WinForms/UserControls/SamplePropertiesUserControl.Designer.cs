namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class SamplePropertiesUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelContent = new System.Windows.Forms.TableLayoutPanel();
            this.labelOriginalLocation = new System.Windows.Forms.Label();
            this.comboBoxInterpolationType = new System.Windows.Forms.ComboBox();
            this.labelInterpolationType = new System.Windows.Forms.Label();
            this.numericUpDownBytesToSkip = new System.Windows.Forms.NumericUpDown();
            this.labelBytesToSkip = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelSamplingRate = new System.Windows.Forms.Label();
            this.labelAudioFileFormat = new System.Windows.Forms.Label();
            this.labelSampleDataType = new System.Windows.Forms.Label();
            this.labelSpeakerSetup = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.comboBoxAudioFileFormat = new System.Windows.Forms.ComboBox();
            this.comboBoxSampleDataType = new System.Windows.Forms.ComboBox();
            this.comboBoxSpeakerSetup = new System.Windows.Forms.ComboBox();
            this.numericUpDownSamplingRate = new System.Windows.Forms.NumericUpDown();
            this.labelAmplifier = new System.Windows.Forms.Label();
            this.numericUpDownAmplifier = new System.Windows.Forms.NumericUpDown();
            this.labelTimeMultiplier = new System.Windows.Forms.Label();
            this.numericUpDownTimeMultiplier = new System.Windows.Forms.NumericUpDown();
            this.labelIsActive = new System.Windows.Forms.Label();
            this.checkBoxIsActive = new System.Windows.Forms.CheckBox();
            this.filePathControlOriginalLocation = new JJ.Framework.Presentation.WinForms.Controls.FilePathControl();
            this.labelDurationTitle = new System.Windows.Forms.Label();
            this.labelDurationValue = new System.Windows.Forms.Label();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBytesToSkip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamplingRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmplifier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeMultiplier)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelContent, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.titleBarUserControl, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(10, 10);
            this.tableLayoutPanelMain.TabIndex = 8;
            // 
            // tableLayoutPanelContent
            // 
            this.tableLayoutPanelContent.ColumnCount = 2;
            this.tableLayoutPanelContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelContent.Controls.Add(this.labelOriginalLocation, 0, 10);
            this.tableLayoutPanelContent.Controls.Add(this.comboBoxInterpolationType, 1, 9);
            this.tableLayoutPanelContent.Controls.Add(this.labelInterpolationType, 0, 9);
            this.tableLayoutPanelContent.Controls.Add(this.numericUpDownBytesToSkip, 1, 8);
            this.tableLayoutPanelContent.Controls.Add(this.labelBytesToSkip, 0, 8);
            this.tableLayoutPanelContent.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanelContent.Controls.Add(this.labelSamplingRate, 0, 1);
            this.tableLayoutPanelContent.Controls.Add(this.labelAudioFileFormat, 0, 2);
            this.tableLayoutPanelContent.Controls.Add(this.labelSampleDataType, 0, 3);
            this.tableLayoutPanelContent.Controls.Add(this.labelSpeakerSetup, 0, 4);
            this.tableLayoutPanelContent.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanelContent.Controls.Add(this.comboBoxAudioFileFormat, 1, 2);
            this.tableLayoutPanelContent.Controls.Add(this.comboBoxSampleDataType, 1, 3);
            this.tableLayoutPanelContent.Controls.Add(this.comboBoxSpeakerSetup, 1, 4);
            this.tableLayoutPanelContent.Controls.Add(this.numericUpDownSamplingRate, 1, 1);
            this.tableLayoutPanelContent.Controls.Add(this.labelAmplifier, 0, 5);
            this.tableLayoutPanelContent.Controls.Add(this.numericUpDownAmplifier, 1, 5);
            this.tableLayoutPanelContent.Controls.Add(this.labelTimeMultiplier, 0, 6);
            this.tableLayoutPanelContent.Controls.Add(this.numericUpDownTimeMultiplier, 1, 6);
            this.tableLayoutPanelContent.Controls.Add(this.labelIsActive, 0, 7);
            this.tableLayoutPanelContent.Controls.Add(this.checkBoxIsActive, 1, 7);
            this.tableLayoutPanelContent.Controls.Add(this.filePathControlOriginalLocation, 1, 10);
            this.tableLayoutPanelContent.Controls.Add(this.labelDurationTitle, 0, 11);
            this.tableLayoutPanelContent.Controls.Add(this.labelDurationValue, 1, 11);
            this.tableLayoutPanelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelContent.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanelContent.Name = "tableLayoutPanelContent";
            this.tableLayoutPanelContent.RowCount = 13;
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelContent.Size = new System.Drawing.Size(10, 10);
            this.tableLayoutPanelContent.TabIndex = 9;
            // 
            // labelOriginalLocation
            // 
            this.labelOriginalLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOriginalLocation.Location = new System.Drawing.Point(0, 240);
            this.labelOriginalLocation.Margin = new System.Windows.Forms.Padding(0);
            this.labelOriginalLocation.Name = "labelOriginalLocation";
            this.labelOriginalLocation.Size = new System.Drawing.Size(100, 24);
            this.labelOriginalLocation.TabIndex = 6;
            this.labelOriginalLocation.Text = "labelOriginalLocation";
            this.labelOriginalLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxInterpolationType
            // 
            this.comboBoxInterpolationType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxInterpolationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInterpolationType.FormattingEnabled = true;
            this.comboBoxInterpolationType.Location = new System.Drawing.Point(100, 216);
            this.comboBoxInterpolationType.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxInterpolationType.Name = "comboBoxInterpolationType";
            this.comboBoxInterpolationType.Size = new System.Drawing.Size(21, 21);
            this.comboBoxInterpolationType.TabIndex = 26;
            // 
            // labelInterpolationType
            // 
            this.labelInterpolationType.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelInterpolationType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInterpolationType.Location = new System.Drawing.Point(0, 216);
            this.labelInterpolationType.Margin = new System.Windows.Forms.Padding(0);
            this.labelInterpolationType.Name = "labelInterpolationType";
            this.labelInterpolationType.Size = new System.Drawing.Size(100, 24);
            this.labelInterpolationType.TabIndex = 25;
            this.labelInterpolationType.Text = "labelInterpolationType";
            this.labelInterpolationType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownBytesToSkip
            // 
            this.numericUpDownBytesToSkip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownBytesToSkip.Location = new System.Drawing.Point(100, 192);
            this.numericUpDownBytesToSkip.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownBytesToSkip.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownBytesToSkip.Name = "numericUpDownBytesToSkip";
            this.numericUpDownBytesToSkip.Size = new System.Drawing.Size(21, 20);
            this.numericUpDownBytesToSkip.TabIndex = 24;
            // 
            // labelBytesToSkip
            // 
            this.labelBytesToSkip.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelBytesToSkip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBytesToSkip.Location = new System.Drawing.Point(0, 192);
            this.labelBytesToSkip.Margin = new System.Windows.Forms.Padding(0);
            this.labelBytesToSkip.Name = "labelBytesToSkip";
            this.labelBytesToSkip.Size = new System.Drawing.Size(100, 24);
            this.labelBytesToSkip.TabIndex = 23;
            this.labelBytesToSkip.Text = "labelBytesToSkip";
            this.labelBytesToSkip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelName
            // 
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Location = new System.Drawing.Point(0, 0);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(100, 24);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Name";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSamplingRate
            // 
            this.labelSamplingRate.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSamplingRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSamplingRate.Location = new System.Drawing.Point(0, 24);
            this.labelSamplingRate.Margin = new System.Windows.Forms.Padding(0);
            this.labelSamplingRate.Name = "labelSamplingRate";
            this.labelSamplingRate.Size = new System.Drawing.Size(100, 24);
            this.labelSamplingRate.TabIndex = 3;
            this.labelSamplingRate.Text = "SamplingRate";
            this.labelSamplingRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAudioFileFormat
            // 
            this.labelAudioFileFormat.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelAudioFileFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAudioFileFormat.Location = new System.Drawing.Point(0, 48);
            this.labelAudioFileFormat.Margin = new System.Windows.Forms.Padding(0);
            this.labelAudioFileFormat.Name = "labelAudioFileFormat";
            this.labelAudioFileFormat.Size = new System.Drawing.Size(100, 24);
            this.labelAudioFileFormat.TabIndex = 4;
            this.labelAudioFileFormat.Text = "AudioFileFormat";
            this.labelAudioFileFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSampleDataType
            // 
            this.labelSampleDataType.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSampleDataType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSampleDataType.Location = new System.Drawing.Point(0, 72);
            this.labelSampleDataType.Margin = new System.Windows.Forms.Padding(0);
            this.labelSampleDataType.Name = "labelSampleDataType";
            this.labelSampleDataType.Size = new System.Drawing.Size(100, 24);
            this.labelSampleDataType.TabIndex = 5;
            this.labelSampleDataType.Text = "SampleDataType";
            this.labelSampleDataType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSpeakerSetup
            // 
            this.labelSpeakerSetup.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSpeakerSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSpeakerSetup.Location = new System.Drawing.Point(0, 96);
            this.labelSpeakerSetup.Margin = new System.Windows.Forms.Padding(0);
            this.labelSpeakerSetup.Name = "labelSpeakerSetup";
            this.labelSpeakerSetup.Size = new System.Drawing.Size(100, 24);
            this.labelSpeakerSetup.TabIndex = 6;
            this.labelSpeakerSetup.Text = "SpeakerSetup";
            this.labelSpeakerSetup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(100, 0);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(21, 20);
            this.textBoxName.TabIndex = 11;
            // 
            // comboBoxAudioFileFormat
            // 
            this.comboBoxAudioFileFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxAudioFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAudioFileFormat.FormattingEnabled = true;
            this.comboBoxAudioFileFormat.Location = new System.Drawing.Point(100, 48);
            this.comboBoxAudioFileFormat.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxAudioFileFormat.Name = "comboBoxAudioFileFormat";
            this.comboBoxAudioFileFormat.Size = new System.Drawing.Size(21, 21);
            this.comboBoxAudioFileFormat.TabIndex = 13;
            // 
            // comboBoxSampleDataType
            // 
            this.comboBoxSampleDataType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSampleDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSampleDataType.FormattingEnabled = true;
            this.comboBoxSampleDataType.Location = new System.Drawing.Point(100, 72);
            this.comboBoxSampleDataType.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxSampleDataType.Name = "comboBoxSampleDataType";
            this.comboBoxSampleDataType.Size = new System.Drawing.Size(21, 21);
            this.comboBoxSampleDataType.TabIndex = 14;
            // 
            // comboBoxSpeakerSetup
            // 
            this.comboBoxSpeakerSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSpeakerSetup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpeakerSetup.FormattingEnabled = true;
            this.comboBoxSpeakerSetup.Location = new System.Drawing.Point(100, 96);
            this.comboBoxSpeakerSetup.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxSpeakerSetup.Name = "comboBoxSpeakerSetup";
            this.comboBoxSpeakerSetup.Size = new System.Drawing.Size(21, 21);
            this.comboBoxSpeakerSetup.TabIndex = 15;
            // 
            // numericUpDownSamplingRate
            // 
            this.numericUpDownSamplingRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownSamplingRate.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownSamplingRate.Location = new System.Drawing.Point(100, 24);
            this.numericUpDownSamplingRate.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownSamplingRate.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownSamplingRate.Name = "numericUpDownSamplingRate";
            this.numericUpDownSamplingRate.Size = new System.Drawing.Size(21, 20);
            this.numericUpDownSamplingRate.TabIndex = 17;
            // 
            // labelAmplifier
            // 
            this.labelAmplifier.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelAmplifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAmplifier.Location = new System.Drawing.Point(0, 120);
            this.labelAmplifier.Margin = new System.Windows.Forms.Padding(0);
            this.labelAmplifier.Name = "labelAmplifier";
            this.labelAmplifier.Size = new System.Drawing.Size(100, 24);
            this.labelAmplifier.TabIndex = 9;
            this.labelAmplifier.Text = "Amplifier";
            this.labelAmplifier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownAmplifier
            // 
            this.numericUpDownAmplifier.DecimalPlaces = 3;
            this.numericUpDownAmplifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownAmplifier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAmplifier.Location = new System.Drawing.Point(100, 120);
            this.numericUpDownAmplifier.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownAmplifier.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownAmplifier.Name = "numericUpDownAmplifier";
            this.numericUpDownAmplifier.Size = new System.Drawing.Size(21, 20);
            this.numericUpDownAmplifier.TabIndex = 19;
            // 
            // labelTimeMultiplier
            // 
            this.labelTimeMultiplier.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelTimeMultiplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTimeMultiplier.Location = new System.Drawing.Point(0, 144);
            this.labelTimeMultiplier.Margin = new System.Windows.Forms.Padding(0);
            this.labelTimeMultiplier.Name = "labelTimeMultiplier";
            this.labelTimeMultiplier.Size = new System.Drawing.Size(100, 24);
            this.labelTimeMultiplier.TabIndex = 10;
            this.labelTimeMultiplier.Text = "TimeMultiplier";
            this.labelTimeMultiplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownTimeMultiplier
            // 
            this.numericUpDownTimeMultiplier.DecimalPlaces = 3;
            this.numericUpDownTimeMultiplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownTimeMultiplier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTimeMultiplier.Location = new System.Drawing.Point(100, 144);
            this.numericUpDownTimeMultiplier.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownTimeMultiplier.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownTimeMultiplier.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownTimeMultiplier.Name = "numericUpDownTimeMultiplier";
            this.numericUpDownTimeMultiplier.Size = new System.Drawing.Size(21, 20);
            this.numericUpDownTimeMultiplier.TabIndex = 20;
            // 
            // labelIsActive
            // 
            this.labelIsActive.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelIsActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelIsActive.Location = new System.Drawing.Point(0, 168);
            this.labelIsActive.Margin = new System.Windows.Forms.Padding(0);
            this.labelIsActive.Name = "labelIsActive";
            this.labelIsActive.Size = new System.Drawing.Size(100, 24);
            this.labelIsActive.TabIndex = 21;
            this.labelIsActive.Text = "labelIsActive";
            this.labelIsActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxIsActive
            // 
            this.checkBoxIsActive.AutoSize = true;
            this.checkBoxIsActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxIsActive.Location = new System.Drawing.Point(103, 171);
            this.checkBoxIsActive.Name = "checkBoxIsActive";
            this.checkBoxIsActive.Size = new System.Drawing.Size(15, 18);
            this.checkBoxIsActive.TabIndex = 22;
            this.checkBoxIsActive.UseVisualStyleBackColor = true;
            // 
            // filePathControlOriginalLocation
            // 
            this.filePathControlOriginalLocation.BrowseMode = JJ.Framework.Presentation.WinForms.Helpers.FileBrowseModeEnum.Open;
            this.filePathControlOriginalLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filePathControlOriginalLocation.FilePath = "";
            this.filePathControlOriginalLocation.LabelText = "";
            this.filePathControlOriginalLocation.Location = new System.Drawing.Point(100, 240);
            this.filePathControlOriginalLocation.Margin = new System.Windows.Forms.Padding(0);
            this.filePathControlOriginalLocation.Name = "filePathControlOriginalLocation";
            this.filePathControlOriginalLocation.Size = new System.Drawing.Size(21, 24);
            this.filePathControlOriginalLocation.Spacing = 0;
            this.filePathControlOriginalLocation.TabIndex = 27;
            this.filePathControlOriginalLocation.TextBoxEnabled = false;
            this.filePathControlOriginalLocation.Browsed += new System.EventHandler<JJ.Framework.Presentation.WinForms.EventArg.FilePathEventArgs>(this.filePathControlOriginalLocation_Browsed);
            // 
            // labelDurationTitle
            // 
            this.labelDurationTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDurationTitle.Location = new System.Drawing.Point(0, 264);
            this.labelDurationTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelDurationTitle.Name = "labelDurationTitle";
            this.labelDurationTitle.Size = new System.Drawing.Size(100, 24);
            this.labelDurationTitle.TabIndex = 28;
            this.labelDurationTitle.Text = "labelDurationTitle";
            this.labelDurationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDurationValue
            // 
            this.labelDurationValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDurationValue.Location = new System.Drawing.Point(100, 264);
            this.labelDurationValue.Margin = new System.Windows.Forms.Padding(0);
            this.labelDurationValue.Name = "labelDurationValue";
            this.labelDurationValue.Size = new System.Drawing.Size(21, 24);
            this.labelDurationValue.TabIndex = 29;
            this.labelDurationValue.Text = "labelDurationValue";
            this.labelDurationValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // titleBarUserControl
            // 
            this.titleBarUserControl.AddButtonVisible = false;
            this.titleBarUserControl.BackColor = System.Drawing.SystemColors.Control;
            this.titleBarUserControl.CloseButtonVisible = true;
            this.titleBarUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleBarUserControl.Location = new System.Drawing.Point(0, 0);
            this.titleBarUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.titleBarUserControl.Name = "titleBarUserControl";
            this.titleBarUserControl.RemoveButtonVisible = false;
            this.titleBarUserControl.Size = new System.Drawing.Size(16, 21);
            this.titleBarUserControl.TabIndex = 8;
            this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
            // 
            // SamplePropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "SamplePropertiesUserControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.Load += new System.EventHandler(this.SamplePropertiesUserControl_Load);
            this.VisibleChanged += new System.EventHandler(this.AudioFileOutputPropertiesUserControl_VisibleChanged);
            this.Enter += new System.EventHandler(this.SamplePropertiesUserControl_Enter);
            this.Leave += new System.EventHandler(this.SamplePropertiesUserControl_Leave);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelContent.ResumeLayout(false);
            this.tableLayoutPanelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBytesToSkip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamplingRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmplifier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeMultiplier)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private Partials.TitleBarUserControl titleBarUserControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelContent;
        private System.Windows.Forms.Label labelOriginalLocation;
        private System.Windows.Forms.ComboBox comboBoxInterpolationType;
        private System.Windows.Forms.Label labelInterpolationType;
        private System.Windows.Forms.NumericUpDown numericUpDownBytesToSkip;
        private System.Windows.Forms.Label labelBytesToSkip;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelSamplingRate;
        private System.Windows.Forms.Label labelAudioFileFormat;
        private System.Windows.Forms.Label labelSampleDataType;
        private System.Windows.Forms.Label labelSpeakerSetup;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ComboBox comboBoxAudioFileFormat;
        private System.Windows.Forms.ComboBox comboBoxSampleDataType;
        private System.Windows.Forms.ComboBox comboBoxSpeakerSetup;
        private System.Windows.Forms.NumericUpDown numericUpDownSamplingRate;
        private System.Windows.Forms.Label labelAmplifier;
        private System.Windows.Forms.NumericUpDown numericUpDownAmplifier;
        private System.Windows.Forms.Label labelTimeMultiplier;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeMultiplier;
        private System.Windows.Forms.Label labelIsActive;
        private System.Windows.Forms.CheckBox checkBoxIsActive;
        private JJ.Framework.Presentation.WinForms.Controls.FilePathControl filePathControlOriginalLocation;
        private System.Windows.Forms.Label labelDurationTitle;
        private System.Windows.Forms.Label labelDurationValue;
    }
}
