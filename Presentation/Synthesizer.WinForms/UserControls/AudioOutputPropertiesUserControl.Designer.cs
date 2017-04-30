namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class AudioOutputPropertiesUserControl
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
            this.labelSamplingRate = new System.Windows.Forms.Label();
            this.labelSpeakerSetup = new System.Windows.Forms.Label();
            this.comboBoxSpeakerSetup = new System.Windows.Forms.ComboBox();
            this.numericUpDownSamplingRate = new System.Windows.Forms.NumericUpDown();
            this.labelMaxConcurrentNotes = new System.Windows.Forms.Label();
            this.numericUpDownMaxConcurrentNotes = new System.Windows.Forms.NumericUpDown();
            this.labelDesiredBufferDuration = new System.Windows.Forms.Label();
            this.numericUpDownDesiredBufferDuration = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamplingRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxConcurrentNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesiredBufferDuration)).BeginInit();
            this.SuspendLayout();
            // 
            // labelSamplingRate
            // 
            this.labelSamplingRate.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSamplingRate.Location = new System.Drawing.Point(0, 0);
            this.labelSamplingRate.Margin = new System.Windows.Forms.Padding(0);
            this.labelSamplingRate.Name = "labelSamplingRate";
            this.labelSamplingRate.Size = new System.Drawing.Size(133, 30);
            this.labelSamplingRate.TabIndex = 3;
            this.labelSamplingRate.Text = "labelSamplingRate";
            this.labelSamplingRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSpeakerSetup
            // 
            this.labelSpeakerSetup.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSpeakerSetup.Location = new System.Drawing.Point(0, 30);
            this.labelSpeakerSetup.Margin = new System.Windows.Forms.Padding(0);
            this.labelSpeakerSetup.Name = "labelSpeakerSetup";
            this.labelSpeakerSetup.Size = new System.Drawing.Size(133, 30);
            this.labelSpeakerSetup.TabIndex = 6;
            this.labelSpeakerSetup.Text = "labelSpeakerSetup";
            this.labelSpeakerSetup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSpeakerSetup
            // 
            this.comboBoxSpeakerSetup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpeakerSetup.FormattingEnabled = true;
            this.comboBoxSpeakerSetup.Location = new System.Drawing.Point(133, 30);
            this.comboBoxSpeakerSetup.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxSpeakerSetup.Name = "comboBoxSpeakerSetup";
            this.comboBoxSpeakerSetup.Size = new System.Drawing.Size(10, 24);
            this.comboBoxSpeakerSetup.TabIndex = 15;
            // 
            // numericUpDownSamplingRate
            // 
            this.numericUpDownSamplingRate.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownSamplingRate.Location = new System.Drawing.Point(133, 0);
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
            // labelMaxConcurrentNotes
            // 
            this.labelMaxConcurrentNotes.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelMaxConcurrentNotes.Location = new System.Drawing.Point(0, 60);
            this.labelMaxConcurrentNotes.Margin = new System.Windows.Forms.Padding(0);
            this.labelMaxConcurrentNotes.Name = "labelMaxConcurrentNotes";
            this.labelMaxConcurrentNotes.Size = new System.Drawing.Size(133, 30);
            this.labelMaxConcurrentNotes.TabIndex = 21;
            this.labelMaxConcurrentNotes.Text = "labelMaxConcurrentNotes";
            this.labelMaxConcurrentNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownMaxConcurrentNotes
            // 
            this.numericUpDownMaxConcurrentNotes.Location = new System.Drawing.Point(133, 60);
            this.numericUpDownMaxConcurrentNotes.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownMaxConcurrentNotes.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownMaxConcurrentNotes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxConcurrentNotes.Name = "numericUpDownMaxConcurrentNotes";
            this.numericUpDownMaxConcurrentNotes.Size = new System.Drawing.Size(10, 22);
            this.numericUpDownMaxConcurrentNotes.TabIndex = 22;
            this.numericUpDownMaxConcurrentNotes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelDesiredBufferDuration
            // 
            this.labelDesiredBufferDuration.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelDesiredBufferDuration.Location = new System.Drawing.Point(0, 90);
            this.labelDesiredBufferDuration.Margin = new System.Windows.Forms.Padding(0);
            this.labelDesiredBufferDuration.Name = "labelDesiredBufferDuration";
            this.labelDesiredBufferDuration.Size = new System.Drawing.Size(133, 30);
            this.labelDesiredBufferDuration.TabIndex = 23;
            this.labelDesiredBufferDuration.Text = "labelDesiredBufferDuration";
            this.labelDesiredBufferDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownDesiredBufferDuration
            // 
            this.numericUpDownDesiredBufferDuration.DecimalPlaces = 4;
            this.numericUpDownDesiredBufferDuration.Increment = new decimal(new int[] {
            2,
            0,
            0,
            196608});
            this.numericUpDownDesiredBufferDuration.Location = new System.Drawing.Point(133, 90);
            this.numericUpDownDesiredBufferDuration.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownDesiredBufferDuration.Name = "numericUpDownDesiredBufferDuration";
            this.numericUpDownDesiredBufferDuration.Size = new System.Drawing.Size(10, 22);
            this.numericUpDownDesiredBufferDuration.TabIndex = 24;
            // 
            // AudioOutputPropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelSamplingRate);
            this.Controls.Add(this.labelSpeakerSetup);
            this.Controls.Add(this.comboBoxSpeakerSetup);
            this.Controls.Add(this.numericUpDownSamplingRate);
            this.Controls.Add(this.labelMaxConcurrentNotes);
            this.Controls.Add(this.numericUpDownMaxConcurrentNotes);
            this.Controls.Add(this.labelDesiredBufferDuration);
            this.Controls.Add(this.numericUpDownDesiredBufferDuration);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AudioOutputPropertiesUserControl";
            this.PlayButtonVisible = true;
            this.Size = new System.Drawing.Size(10, 10);
            this.Controls.SetChildIndex(this.numericUpDownDesiredBufferDuration, 0);
            this.Controls.SetChildIndex(this.labelDesiredBufferDuration, 0);
            this.Controls.SetChildIndex(this.numericUpDownMaxConcurrentNotes, 0);
            this.Controls.SetChildIndex(this.labelMaxConcurrentNotes, 0);
            this.Controls.SetChildIndex(this.numericUpDownSamplingRate, 0);
            this.Controls.SetChildIndex(this.comboBoxSpeakerSetup, 0);
            this.Controls.SetChildIndex(this.labelSpeakerSetup, 0);
            this.Controls.SetChildIndex(this.labelSamplingRate, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamplingRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxConcurrentNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesiredBufferDuration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelSamplingRate;
        private System.Windows.Forms.Label labelSpeakerSetup;
        private System.Windows.Forms.ComboBox comboBoxSpeakerSetup;
        private System.Windows.Forms.NumericUpDown numericUpDownSamplingRate;
        private System.Windows.Forms.Label labelMaxConcurrentNotes;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxConcurrentNotes;
        private System.Windows.Forms.Label labelDesiredBufferDuration;
        private System.Windows.Forms.NumericUpDown numericUpDownDesiredBufferDuration;
    }
}
