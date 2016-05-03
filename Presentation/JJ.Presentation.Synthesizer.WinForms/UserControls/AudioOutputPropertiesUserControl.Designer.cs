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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelContent = new System.Windows.Forms.TableLayoutPanel();
            this.labelSamplingRate = new System.Windows.Forms.Label();
            this.labelSpeakerSetup = new System.Windows.Forms.Label();
            this.comboBoxSpeakerSetup = new System.Windows.Forms.ComboBox();
            this.numericUpDownSamplingRate = new System.Windows.Forms.NumericUpDown();
            this.labelMaxConcurrentNotes = new System.Windows.Forms.Label();
            this.numericUpDownMaxConcurrentNotes = new System.Windows.Forms.NumericUpDown();
            this.labelDesiredBufferDuration = new System.Windows.Forms.Label();
            this.numericUpDownDesiredBufferDuration = new System.Windows.Forms.NumericUpDown();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamplingRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxConcurrentNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesiredBufferDuration)).BeginInit();
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
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(10, 10);
            this.tableLayoutPanelMain.TabIndex = 8;
            // 
            // tableLayoutPanelContent
            // 
            this.tableLayoutPanelContent.ColumnCount = 2;
            this.tableLayoutPanelContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanelContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelContent.Controls.Add(this.labelSamplingRate, 0, 0);
            this.tableLayoutPanelContent.Controls.Add(this.labelSpeakerSetup, 0, 1);
            this.tableLayoutPanelContent.Controls.Add(this.comboBoxSpeakerSetup, 1, 1);
            this.tableLayoutPanelContent.Controls.Add(this.numericUpDownSamplingRate, 1, 0);
            this.tableLayoutPanelContent.Controls.Add(this.labelMaxConcurrentNotes, 0, 2);
            this.tableLayoutPanelContent.Controls.Add(this.numericUpDownMaxConcurrentNotes, 1, 2);
            this.tableLayoutPanelContent.Controls.Add(this.labelDesiredBufferDuration, 0, 3);
            this.tableLayoutPanelContent.Controls.Add(this.numericUpDownDesiredBufferDuration, 1, 3);
            this.tableLayoutPanelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelContent.Location = new System.Drawing.Point(4, 30);
            this.tableLayoutPanelContent.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanelContent.Name = "tableLayoutPanelContent";
            this.tableLayoutPanelContent.RowCount = 5;
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelContent.Size = new System.Drawing.Size(10, 10);
            this.tableLayoutPanelContent.TabIndex = 9;
            // 
            // labelSamplingRate
            // 
            this.labelSamplingRate.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSamplingRate.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.labelSpeakerSetup.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.comboBoxSpeakerSetup.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.numericUpDownSamplingRate.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.labelMaxConcurrentNotes.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.numericUpDownMaxConcurrentNotes.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.labelDesiredBufferDuration.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.numericUpDownDesiredBufferDuration.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.titleBarUserControl.Size = new System.Drawing.Size(18, 26);
            this.titleBarUserControl.TabIndex = 8;
            this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
            // 
            // AudioOutputPropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AudioOutputPropertiesUserControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.Load += new System.EventHandler(this.AudioOutputPropertiesUserControl_Load);
            this.Leave += new System.EventHandler(this.AudioOutputPropertiesUserControl_Leave);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamplingRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxConcurrentNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDesiredBufferDuration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private Partials.TitleBarUserControl titleBarUserControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelContent;
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
