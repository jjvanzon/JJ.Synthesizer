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
            this.labelVolumeFactor = new System.Windows.Forms.Label();
            this.numericUpDownVolumeFactor = new System.Windows.Forms.NumericUpDown();
            this.labelSpeedFactor = new System.Windows.Forms.Label();
            this.numericUpDownSpeedFactor = new System.Windows.Forms.NumericUpDown();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSamplingRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolumeFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedFactor)).BeginInit();
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
            this.tableLayoutPanelContent.Controls.Add(this.labelVolumeFactor, 0, 2);
            this.tableLayoutPanelContent.Controls.Add(this.numericUpDownVolumeFactor, 1, 2);
            this.tableLayoutPanelContent.Controls.Add(this.labelSpeedFactor, 0, 3);
            this.tableLayoutPanelContent.Controls.Add(this.numericUpDownSpeedFactor, 1, 3);
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
            // labelVolumeFactor
            // 
            this.labelVolumeFactor.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelVolumeFactor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVolumeFactor.Location = new System.Drawing.Point(0, 60);
            this.labelVolumeFactor.Margin = new System.Windows.Forms.Padding(0);
            this.labelVolumeFactor.Name = "labelVolumeFactor";
            this.labelVolumeFactor.Size = new System.Drawing.Size(133, 30);
            this.labelVolumeFactor.TabIndex = 9;
            this.labelVolumeFactor.Text = "labelVolumeFactor";
            this.labelVolumeFactor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownVolumeFactor
            // 
            this.numericUpDownVolumeFactor.DecimalPlaces = 3;
            this.numericUpDownVolumeFactor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownVolumeFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownVolumeFactor.Location = new System.Drawing.Point(133, 60);
            this.numericUpDownVolumeFactor.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownVolumeFactor.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownVolumeFactor.Name = "numericUpDownVolumeFactor";
            this.numericUpDownVolumeFactor.Size = new System.Drawing.Size(10, 22);
            this.numericUpDownVolumeFactor.TabIndex = 19;
            // 
            // labelSpeedFactor
            // 
            this.labelSpeedFactor.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSpeedFactor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelSpeedFactor.Location = new System.Drawing.Point(0, 90);
            this.labelSpeedFactor.Margin = new System.Windows.Forms.Padding(0);
            this.labelSpeedFactor.Name = "labelSpeedFactor";
            this.labelSpeedFactor.Size = new System.Drawing.Size(133, 30);
            this.labelSpeedFactor.TabIndex = 10;
            this.labelSpeedFactor.Text = "labelSpeedFactor";
            this.labelSpeedFactor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownSpeedFactor
            // 
            this.numericUpDownSpeedFactor.DecimalPlaces = 6;
            this.numericUpDownSpeedFactor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownSpeedFactor.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownSpeedFactor.Location = new System.Drawing.Point(133, 90);
            this.numericUpDownSpeedFactor.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownSpeedFactor.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownSpeedFactor.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownSpeedFactor.Name = "numericUpDownSpeedFactor";
            this.numericUpDownSpeedFactor.Size = new System.Drawing.Size(10, 22);
            this.numericUpDownSpeedFactor.TabIndex = 20;
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVolumeFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeedFactor)).EndInit();
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
        private System.Windows.Forms.Label labelVolumeFactor;
        private System.Windows.Forms.NumericUpDown numericUpDownVolumeFactor;
        private System.Windows.Forms.Label labelSpeedFactor;
        private System.Windows.Forms.NumericUpDown numericUpDownSpeedFactor;
    }
}
