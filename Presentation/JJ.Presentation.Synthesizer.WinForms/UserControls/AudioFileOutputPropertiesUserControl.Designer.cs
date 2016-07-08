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
            this.labelFilePath = new System.Windows.Forms.Label();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeMultiplier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmplifier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamplingRate)).BeginInit();
            this.SuspendLayout();
            // 
            // labelFilePath
            // 
            this.labelFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFilePath.Location = new System.Drawing.Point(0, 0);
            this.labelFilePath.Margin = new System.Windows.Forms.Padding(0);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(10, 10);
            this.labelFilePath.TabIndex = 6;
            this.labelFilePath.Text = "FilePath";
            this.labelFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFilePath.Location = new System.Drawing.Point(0, 0);
            this.textBoxFilePath.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.Size = new System.Drawing.Size(10, 22);
            this.textBoxFilePath.TabIndex = 5;
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
            this.numericUpDownTimeMultiplier.Location = new System.Drawing.Point(0, 0);
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
            this.numericUpDownTimeMultiplier.Size = new System.Drawing.Size(10, 22);
            this.numericUpDownTimeMultiplier.TabIndex = 20;
            // 
            // numericUpDownAmplifier
            // 
            this.numericUpDownAmplifier.DecimalPlaces = 3;
            this.numericUpDownAmplifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownAmplifier.Location = new System.Drawing.Point(0, 0);
            this.numericUpDownAmplifier.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownAmplifier.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownAmplifier.Name = "numericUpDownAmplifier";
            this.numericUpDownAmplifier.Size = new System.Drawing.Size(10, 22);
            this.numericUpDownAmplifier.TabIndex = 19;
            // 
            // numericUpDownDuration
            // 
            this.numericUpDownDuration.DecimalPlaces = 3;
            this.numericUpDownDuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownDuration.Location = new System.Drawing.Point(0, 0);
            this.numericUpDownDuration.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownDuration.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownDuration.Name = "numericUpDownDuration";
            this.numericUpDownDuration.Size = new System.Drawing.Size(10, 22);
            this.numericUpDownDuration.TabIndex = 18;
            // 
            // labelName
            // 
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Location = new System.Drawing.Point(0, 0);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(10, 10);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Name";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSamplingRate
            // 
            this.labelSamplingRate.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSamplingRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSamplingRate.Location = new System.Drawing.Point(0, 0);
            this.labelSamplingRate.Margin = new System.Windows.Forms.Padding(0);
            this.labelSamplingRate.Name = "labelSamplingRate";
            this.labelSamplingRate.Size = new System.Drawing.Size(10, 10);
            this.labelSamplingRate.TabIndex = 3;
            this.labelSamplingRate.Text = "SamplingRate";
            this.labelSamplingRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAudioFileFormat
            // 
            this.labelAudioFileFormat.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelAudioFileFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAudioFileFormat.Location = new System.Drawing.Point(0, 0);
            this.labelAudioFileFormat.Margin = new System.Windows.Forms.Padding(0);
            this.labelAudioFileFormat.Name = "labelAudioFileFormat";
            this.labelAudioFileFormat.Size = new System.Drawing.Size(10, 10);
            this.labelAudioFileFormat.TabIndex = 4;
            this.labelAudioFileFormat.Text = "AudioFileFormat";
            this.labelAudioFileFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSampleDataType
            // 
            this.labelSampleDataType.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSampleDataType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSampleDataType.Location = new System.Drawing.Point(0, 0);
            this.labelSampleDataType.Margin = new System.Windows.Forms.Padding(0);
            this.labelSampleDataType.Name = "labelSampleDataType";
            this.labelSampleDataType.Size = new System.Drawing.Size(10, 10);
            this.labelSampleDataType.TabIndex = 5;
            this.labelSampleDataType.Text = "SampleDataType";
            this.labelSampleDataType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSpeakerSetup
            // 
            this.labelSpeakerSetup.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSpeakerSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSpeakerSetup.Location = new System.Drawing.Point(0, 0);
            this.labelSpeakerSetup.Margin = new System.Windows.Forms.Padding(0);
            this.labelSpeakerSetup.Name = "labelSpeakerSetup";
            this.labelSpeakerSetup.Size = new System.Drawing.Size(10, 10);
            this.labelSpeakerSetup.TabIndex = 6;
            this.labelSpeakerSetup.Text = "SpeakerSetup";
            this.labelSpeakerSetup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelStartTime
            // 
            this.labelStartTime.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelStartTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStartTime.Location = new System.Drawing.Point(0, 0);
            this.labelStartTime.Margin = new System.Windows.Forms.Padding(0);
            this.labelStartTime.Name = "labelStartTime";
            this.labelStartTime.Size = new System.Drawing.Size(10, 10);
            this.labelStartTime.TabIndex = 7;
            this.labelStartTime.Text = "StartTime";
            this.labelStartTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDuration
            // 
            this.labelDuration.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelDuration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDuration.Location = new System.Drawing.Point(0, 0);
            this.labelDuration.Margin = new System.Windows.Forms.Padding(0);
            this.labelDuration.Name = "labelDuration";
            this.labelDuration.Size = new System.Drawing.Size(10, 10);
            this.labelDuration.TabIndex = 8;
            this.labelDuration.Text = "Duration";
            this.labelDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelAmplifier
            // 
            this.labelAmplifier.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelAmplifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAmplifier.Location = new System.Drawing.Point(0, 0);
            this.labelAmplifier.Margin = new System.Windows.Forms.Padding(0);
            this.labelAmplifier.Name = "labelAmplifier";
            this.labelAmplifier.Size = new System.Drawing.Size(10, 10);
            this.labelAmplifier.TabIndex = 9;
            this.labelAmplifier.Text = "Amplifier";
            this.labelAmplifier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelTimeMultiplier
            // 
            this.labelTimeMultiplier.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelTimeMultiplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTimeMultiplier.Location = new System.Drawing.Point(0, 0);
            this.labelTimeMultiplier.Margin = new System.Windows.Forms.Padding(0);
            this.labelTimeMultiplier.Name = "labelTimeMultiplier";
            this.labelTimeMultiplier.Size = new System.Drawing.Size(10, 10);
            this.labelTimeMultiplier.TabIndex = 10;
            this.labelTimeMultiplier.Text = "TimeMultiplier";
            this.labelTimeMultiplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(0, 0);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(10, 22);
            this.textBoxName.TabIndex = 11;
            // 
            // comboBoxAudioFileFormat
            // 
            this.comboBoxAudioFileFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxAudioFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAudioFileFormat.FormattingEnabled = true;
            this.comboBoxAudioFileFormat.Location = new System.Drawing.Point(0, 0);
            this.comboBoxAudioFileFormat.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxAudioFileFormat.Name = "comboBoxAudioFileFormat";
            this.comboBoxAudioFileFormat.Size = new System.Drawing.Size(10, 24);
            this.comboBoxAudioFileFormat.TabIndex = 13;
            // 
            // comboBoxSampleDataType
            // 
            this.comboBoxSampleDataType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSampleDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSampleDataType.FormattingEnabled = true;
            this.comboBoxSampleDataType.Location = new System.Drawing.Point(0, 0);
            this.comboBoxSampleDataType.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxSampleDataType.Name = "comboBoxSampleDataType";
            this.comboBoxSampleDataType.Size = new System.Drawing.Size(10, 24);
            this.comboBoxSampleDataType.TabIndex = 14;
            // 
            // comboBoxSpeakerSetup
            // 
            this.comboBoxSpeakerSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxSpeakerSetup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpeakerSetup.FormattingEnabled = true;
            this.comboBoxSpeakerSetup.Location = new System.Drawing.Point(0, 0);
            this.comboBoxSpeakerSetup.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxSpeakerSetup.Name = "comboBoxSpeakerSetup";
            this.comboBoxSpeakerSetup.Size = new System.Drawing.Size(10, 24);
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
            this.numericUpDownStartTime.Location = new System.Drawing.Point(0, 0);
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
            this.numericUpDownStartTime.Size = new System.Drawing.Size(10, 22);
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
            this.numericUpDownSamplingRate.Location = new System.Drawing.Point(0, 0);
            this.numericUpDownSamplingRate.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownSamplingRate.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownSamplingRate.Name = "numericUpDownSamplingRate";
            this.numericUpDownSamplingRate.Size = new System.Drawing.Size(10, 22);
            this.numericUpDownSamplingRate.TabIndex = 17;
            // 
            // AudioFileOutputPropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelFilePath);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.numericUpDownTimeMultiplier);
            this.Controls.Add(this.numericUpDownAmplifier);
            this.Controls.Add(this.numericUpDownDuration);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelSamplingRate);
            this.Controls.Add(this.labelAudioFileFormat);
            this.Controls.Add(this.labelSampleDataType);
            this.Controls.Add(this.labelSpeakerSetup);
            this.Controls.Add(this.labelStartTime);
            this.Controls.Add(this.labelDuration);
            this.Controls.Add(this.labelAmplifier);
            this.Controls.Add(this.labelTimeMultiplier);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.comboBoxAudioFileFormat);
            this.Controls.Add(this.comboBoxSampleDataType);
            this.Controls.Add(this.comboBoxSpeakerSetup);
            this.Controls.Add(this.numericUpDownStartTime);
            this.Controls.Add(this.numericUpDownSamplingRate);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AudioFileOutputPropertiesUserControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.Controls.SetChildIndex(this.numericUpDownSamplingRate, 0);
            this.Controls.SetChildIndex(this.numericUpDownStartTime, 0);
            this.Controls.SetChildIndex(this.comboBoxSpeakerSetup, 0);
            this.Controls.SetChildIndex(this.comboBoxSampleDataType, 0);
            this.Controls.SetChildIndex(this.comboBoxAudioFileFormat, 0);
            this.Controls.SetChildIndex(this.textBoxName, 0);
            this.Controls.SetChildIndex(this.labelTimeMultiplier, 0);
            this.Controls.SetChildIndex(this.labelAmplifier, 0);
            this.Controls.SetChildIndex(this.labelDuration, 0);
            this.Controls.SetChildIndex(this.labelStartTime, 0);
            this.Controls.SetChildIndex(this.labelSpeakerSetup, 0);
            this.Controls.SetChildIndex(this.labelSampleDataType, 0);
            this.Controls.SetChildIndex(this.labelAudioFileFormat, 0);
            this.Controls.SetChildIndex(this.labelSamplingRate, 0);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.numericUpDownDuration, 0);
            this.Controls.SetChildIndex(this.numericUpDownAmplifier, 0);
            this.Controls.SetChildIndex(this.numericUpDownTimeMultiplier, 0);
            this.Controls.SetChildIndex(this.textBoxFilePath, 0);
            this.Controls.SetChildIndex(this.labelFilePath, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeMultiplier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmplifier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamplingRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.TextBox textBoxFilePath;
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
    }
}
