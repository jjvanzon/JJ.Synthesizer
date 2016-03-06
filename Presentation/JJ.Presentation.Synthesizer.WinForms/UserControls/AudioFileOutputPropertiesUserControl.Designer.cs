namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class AudioFileOutputPropertiesUserControl
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
            this.groupBoxFilePath = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelFilePath = new System.Windows.Forms.TableLayoutPanel();
            this.labelFilePath = new System.Windows.Forms.Label();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelGeneral = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDownTimeMultiplier = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAmplifier = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDuration = new System.Windows.Forms.NumericUpDown();
            this.labelName = new System.Windows.Forms.Label();
            this.labelSamplingRate = new System.Windows.Forms.Label();
            this.labelAudioFileFormat = new System.Windows.Forms.Label();
            this.labelSampleDataType = new System.Windows.Forms.Label();
            this.labelSpeakerSetup = new System.Windows.Forms.Label();
            this.labelStartTime = new System.Windows.Forms.Label();
            this.labelDuration = new System.Windows.Forms.Label();
            this.labelAmplifier = new System.Windows.Forms.Label();
            this.labelTimeMultiplier = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.comboBoxAudioFileFormat = new System.Windows.Forms.ComboBox();
            this.comboBoxSampleDataType = new System.Windows.Forms.ComboBox();
            this.comboBoxSpeakerSetup = new System.Windows.Forms.ComboBox();
            this.numericUpDownStartTime = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSamplingRate = new System.Windows.Forms.NumericUpDown();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.audioFileOutputChannelsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.AudioFileOutputChannelsUserControl();
            this.groupBoxFilePath.SuspendLayout();
            this.tableLayoutPanelFilePath.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.groupBoxGeneral.SuspendLayout();
            this.tableLayoutPanelGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeMultiplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmplifier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamplingRate)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxFilePath
            // 
            this.groupBoxFilePath.Controls.Add(this.tableLayoutPanelFilePath);
            this.groupBoxFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFilePath.Location = new System.Drawing.Point(3, 266);
            this.groupBoxFilePath.Name = "groupBoxFilePath";
            this.groupBoxFilePath.Size = new System.Drawing.Size(44, 44);
            this.groupBoxFilePath.TabIndex = 6;
            this.groupBoxFilePath.TabStop = false;
            // 
            // tableLayoutPanelFilePath
            // 
            this.tableLayoutPanelFilePath.ColumnCount = 2;
            this.tableLayoutPanelFilePath.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelFilePath.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelFilePath.Controls.Add(this.labelFilePath, 0, 0);
            this.tableLayoutPanelFilePath.Controls.Add(this.textBoxFilePath, 1, 0);
            this.tableLayoutPanelFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelFilePath.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelFilePath.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelFilePath.Name = "tableLayoutPanelFilePath";
            this.tableLayoutPanelFilePath.RowCount = 2;
            this.tableLayoutPanelFilePath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelFilePath.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelFilePath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFilePath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFilePath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFilePath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFilePath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFilePath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFilePath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFilePath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFilePath.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelFilePath.Size = new System.Drawing.Size(38, 25);
            this.tableLayoutPanelFilePath.TabIndex = 7;
            // 
            // labelFilePath
            // 
            this.labelFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFilePath.Location = new System.Drawing.Point(0, 0);
            this.labelFilePath.Margin = new System.Windows.Forms.Padding(0);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(100, 24);
            this.labelFilePath.TabIndex = 6;
            this.labelFilePath.Text = "FilePath";
            this.labelFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFilePath.Location = new System.Drawing.Point(100, 0);
            this.textBoxFilePath.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(10, 20);
            this.textBoxFilePath.TabIndex = 5;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxGeneral, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxFilePath, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.titleBarUserControl, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.audioFileOutputChannelsUserControl, 0, 3);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 4;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 242F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(50, 34);
            this.tableLayoutPanelMain.TabIndex = 8;
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Controls.Add(this.tableLayoutPanelGeneral);
            this.groupBoxGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxGeneral.Location = new System.Drawing.Point(3, 24);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Size = new System.Drawing.Size(44, 236);
            this.groupBoxGeneral.TabIndex = 6;
            this.groupBoxGeneral.TabStop = false;
            // 
            // tableLayoutPanelGeneral
            // 
            this.tableLayoutPanelGeneral.ColumnCount = 2;
            this.tableLayoutPanelGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelGeneral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelGeneral.Controls.Add(this.numericUpDownTimeMultiplier, 1, 8);
            this.tableLayoutPanelGeneral.Controls.Add(this.numericUpDownAmplifier, 1, 7);
            this.tableLayoutPanelGeneral.Controls.Add(this.numericUpDownDuration, 1, 6);
            this.tableLayoutPanelGeneral.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanelGeneral.Controls.Add(this.labelSamplingRate, 0, 1);
            this.tableLayoutPanelGeneral.Controls.Add(this.labelAudioFileFormat, 0, 2);
            this.tableLayoutPanelGeneral.Controls.Add(this.labelSampleDataType, 0, 3);
            this.tableLayoutPanelGeneral.Controls.Add(this.labelSpeakerSetup, 0, 4);
            this.tableLayoutPanelGeneral.Controls.Add(this.labelStartTime, 0, 5);
            this.tableLayoutPanelGeneral.Controls.Add(this.labelDuration, 0, 6);
            this.tableLayoutPanelGeneral.Controls.Add(this.labelAmplifier, 0, 7);
            this.tableLayoutPanelGeneral.Controls.Add(this.labelTimeMultiplier, 0, 8);
            this.tableLayoutPanelGeneral.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanelGeneral.Controls.Add(this.comboBoxAudioFileFormat, 1, 2);
            this.tableLayoutPanelGeneral.Controls.Add(this.comboBoxSampleDataType, 1, 3);
            this.tableLayoutPanelGeneral.Controls.Add(this.comboBoxSpeakerSetup, 1, 4);
            this.tableLayoutPanelGeneral.Controls.Add(this.numericUpDownStartTime, 1, 5);
            this.tableLayoutPanelGeneral.Controls.Add(this.numericUpDownSamplingRate, 1, 1);
            this.tableLayoutPanelGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelGeneral.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelGeneral.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelGeneral.Name = "tableLayoutPanelGeneral";
            this.tableLayoutPanelGeneral.RowCount = 10;
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelGeneral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelGeneral.Size = new System.Drawing.Size(38, 217);
            this.tableLayoutPanelGeneral.TabIndex = 3;
            // 
            // numericUpDownTimeMultiplier
            // 
            this.numericUpDownTimeMultiplier.DecimalPlaces = 6;
            this.numericUpDownTimeMultiplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownTimeMultiplier.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTimeMultiplier.Location = new System.Drawing.Point(100, 192);
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
            this.numericUpDownTimeMultiplier.Size = new System.Drawing.Size(10, 20);
            this.numericUpDownTimeMultiplier.TabIndex = 20;
            // 
            // numericUpDownAmplifier
            // 
            this.numericUpDownAmplifier.DecimalPlaces = 3;
            this.numericUpDownAmplifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownAmplifier.Location = new System.Drawing.Point(100, 168);
            this.numericUpDownAmplifier.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownAmplifier.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownAmplifier.Name = "numericUpDownAmplifier";
            this.numericUpDownAmplifier.Size = new System.Drawing.Size(10, 20);
            this.numericUpDownAmplifier.TabIndex = 19;
            // 
            // numericUpDownDuration
            // 
            this.numericUpDownDuration.DecimalPlaces = 3;
            this.numericUpDownDuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownDuration.Location = new System.Drawing.Point(100, 144);
            this.numericUpDownDuration.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownDuration.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownDuration.Name = "numericUpDownDuration";
            this.numericUpDownDuration.Size = new System.Drawing.Size(10, 20);
            this.numericUpDownDuration.TabIndex = 18;
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
            // labelStartTime
            // 
            this.labelStartTime.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelStartTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStartTime.Location = new System.Drawing.Point(0, 120);
            this.labelStartTime.Margin = new System.Windows.Forms.Padding(0);
            this.labelStartTime.Name = "labelStartTime";
            this.labelStartTime.Size = new System.Drawing.Size(100, 24);
            this.labelStartTime.TabIndex = 7;
            this.labelStartTime.Text = "StartTime";
            this.labelStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDuration
            // 
            this.labelDuration.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelDuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDuration.Location = new System.Drawing.Point(0, 144);
            this.labelDuration.Margin = new System.Windows.Forms.Padding(0);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(100, 24);
            this.labelDuration.TabIndex = 8;
            this.labelDuration.Text = "Duration";
            this.labelDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAmplifier
            // 
            this.labelAmplifier.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelAmplifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAmplifier.Location = new System.Drawing.Point(0, 168);
            this.labelAmplifier.Margin = new System.Windows.Forms.Padding(0);
            this.labelAmplifier.Name = "labelAmplifier";
            this.labelAmplifier.Size = new System.Drawing.Size(100, 24);
            this.labelAmplifier.TabIndex = 9;
            this.labelAmplifier.Text = "Amplifier";
            this.labelAmplifier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTimeMultiplier
            // 
            this.labelTimeMultiplier.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelTimeMultiplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTimeMultiplier.Location = new System.Drawing.Point(0, 192);
            this.labelTimeMultiplier.Margin = new System.Windows.Forms.Padding(0);
            this.labelTimeMultiplier.Name = "labelTimeMultiplier";
            this.labelTimeMultiplier.Size = new System.Drawing.Size(100, 24);
            this.labelTimeMultiplier.TabIndex = 10;
            this.labelTimeMultiplier.Text = "TimeMultiplier";
            this.labelTimeMultiplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(100, 0);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(10, 20);
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
            this.comboBoxAudioFileFormat.Size = new System.Drawing.Size(10, 21);
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
            this.comboBoxSampleDataType.Size = new System.Drawing.Size(10, 21);
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
            this.comboBoxSpeakerSetup.Size = new System.Drawing.Size(10, 21);
            this.comboBoxSpeakerSetup.TabIndex = 15;
            // 
            // numericUpDownStartTime
            // 
            this.numericUpDownStartTime.DecimalPlaces = 3;
            this.numericUpDownStartTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownStartTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownStartTime.Location = new System.Drawing.Point(100, 120);
            this.numericUpDownStartTime.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownStartTime.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownStartTime.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownStartTime.Name = "numericUpDownStartTime";
            this.numericUpDownStartTime.Size = new System.Drawing.Size(10, 20);
            this.numericUpDownStartTime.TabIndex = 16;
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
            this.numericUpDownSamplingRate.Size = new System.Drawing.Size(10, 20);
            this.numericUpDownSamplingRate.TabIndex = 17;
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
            this.titleBarUserControl.Size = new System.Drawing.Size(50, 21);
            this.titleBarUserControl.TabIndex = 8;
            this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
            // 
            // audioFileOutputChannelsUserControl
            // 
            this.audioFileOutputChannelsUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.audioFileOutputChannelsUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.audioFileOutputChannelsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioFileOutputChannelsUserControl.Location = new System.Drawing.Point(3, 316);
            this.audioFileOutputChannelsUserControl.Name = "audioFileOutputChannelsUserControl";
            this.audioFileOutputChannelsUserControl.Size = new System.Drawing.Size(44, 10);
            this.audioFileOutputChannelsUserControl.TabIndex = 1;
            // 
            // AudioFileOutputPropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "AudioFileOutputPropertiesUserControl";
            this.Size = new System.Drawing.Size(50, 34);
            this.Load += new System.EventHandler(this.AudioFileOutputPropertiesUserControl_Load);
            this.Leave += new System.EventHandler(this.AudioFileOutputPropertiesUserControl_Leave);
            this.groupBoxFilePath.ResumeLayout(false);
            this.tableLayoutPanelFilePath.ResumeLayout(false);
            this.tableLayoutPanelFilePath.PerformLayout();
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.groupBoxGeneral.ResumeLayout(false);
            this.tableLayoutPanelGeneral.ResumeLayout(false);
            this.tableLayoutPanelGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeMultiplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmplifier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamplingRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxFilePath;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelGeneral;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeMultiplier;
        private System.Windows.Forms.NumericUpDown numericUpDownAmplifier;
        private System.Windows.Forms.NumericUpDown numericUpDownDuration;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelSamplingRate;
        private System.Windows.Forms.Label labelAudioFileFormat;
        private System.Windows.Forms.Label labelSampleDataType;
        private System.Windows.Forms.Label labelSpeakerSetup;
        private System.Windows.Forms.Label labelStartTime;
        private System.Windows.Forms.Label labelDuration;
        private System.Windows.Forms.Label labelAmplifier;
        private System.Windows.Forms.Label labelTimeMultiplier;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ComboBox comboBoxAudioFileFormat;
        private System.Windows.Forms.ComboBox comboBoxSampleDataType;
        private System.Windows.Forms.ComboBox comboBoxSpeakerSetup;
        private System.Windows.Forms.NumericUpDown numericUpDownStartTime;
        private System.Windows.Forms.NumericUpDown numericUpDownSamplingRate;
        private Partials.TitleBarUserControl titleBarUserControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelFilePath;
        private Partials.AudioFileOutputChannelsUserControl audioFileOutputChannelsUserControl;
    }
}
