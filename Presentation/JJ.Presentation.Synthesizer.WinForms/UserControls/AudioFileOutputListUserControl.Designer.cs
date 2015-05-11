namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class AudioFileOutputListUserControl
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AudioFileFormatColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SampleDataTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpeakerSetupColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SamplingRateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelTitle = new System.Windows.Forms.Label();
            this.pagerControl = new JJ.Framework.Presentation.WinForms.PagerControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDColumn,
            this.NameColumn,
            this.AudioFileFormatColumn,
            this.SampleDataTypeColumn,
            this.SpeakerSetupColumn,
            this.SamplingRateColumn});
            this.dataGridView.Location = new System.Drawing.Point(0, 24);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(796, 346);
            this.dataGridView.TabIndex = 0;
            // 
            // IDColumn
            // 
            this.IDColumn.DataPropertyName = "ID";
            this.IDColumn.HeaderText = "ID";
            this.IDColumn.Name = "IDColumn";
            this.IDColumn.ReadOnly = true;
            this.IDColumn.Width = 80;
            // 
            // NameColumn
            // 
            this.NameColumn.DataPropertyName = "Name";
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Width = 250;
            // 
            // AudioFileFormatColumn
            // 
            this.AudioFileFormatColumn.DataPropertyName = "AudioFileFormat";
            this.AudioFileFormatColumn.HeaderText = "AudioFileFormat";
            this.AudioFileFormatColumn.Name = "AudioFileFormatColumn";
            this.AudioFileFormatColumn.ReadOnly = true;
            this.AudioFileFormatColumn.Width = 120;
            // 
            // SampleDataTypeColumn
            // 
            this.SampleDataTypeColumn.DataPropertyName = "SampleDataType";
            this.SampleDataTypeColumn.HeaderText = "SampleDataType";
            this.SampleDataTypeColumn.Name = "SampleDataTypeColumn";
            this.SampleDataTypeColumn.ReadOnly = true;
            this.SampleDataTypeColumn.Width = 120;
            // 
            // SpeakerSetupColumn
            // 
            this.SpeakerSetupColumn.DataPropertyName = "SpeakerSetup";
            this.SpeakerSetupColumn.HeaderText = "SpeakerSetup";
            this.SpeakerSetupColumn.Name = "SpeakerSetupColumn";
            this.SpeakerSetupColumn.ReadOnly = true;
            this.SpeakerSetupColumn.Width = 120;
            // 
            // SamplingRateColumn
            // 
            this.SamplingRateColumn.DataPropertyName = "SamplingRate";
            this.SamplingRateColumn.HeaderText = "SamplingRate";
            this.SamplingRateColumn.Name = "SamplingRateColumn";
            this.SamplingRateColumn.ReadOnly = true;
            this.SamplingRateColumn.Width = 120;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(-3, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Padding = new System.Windows.Forms.Padding(3);
            this.labelTitle.Size = new System.Drawing.Size(799, 24);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Audio File Outputs";
            // 
            // pagerControl
            // 
            this.pagerControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl.Location = new System.Drawing.Point(0, 387);
            this.pagerControl.Name = "pagerControl";
            this.pagerControl.Size = new System.Drawing.Size(796, 13);
            this.pagerControl.TabIndex = 2;
            this.pagerControl.GoToFirstPageClicked += new System.EventHandler(this.pagerControl_GoToFirstPageClicked);
            this.pagerControl.GoToPreviousPageClicked += new System.EventHandler(this.pagerControl_GoToPreviousPageClicked);
            this.pagerControl.PageNumberClicked += new System.EventHandler<JJ.Framework.Presentation.WinForms.PageNumberEventArgs>(this.pagerControl_PageNumberClicked);
            this.pagerControl.GoToNextPageClicked += new System.EventHandler(this.pagerControl_GoToNextPageClicked);
            this.pagerControl.GoToLastPageClicked += new System.EventHandler(this.pagerControl_GoToLastPageClicked);
            // 
            // AudioFileOutputListUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pagerControl);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.dataGridView);
            this.Name = "AudioFileOutputListUserControl";
            this.Size = new System.Drawing.Size(796, 400);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label labelTitle;
        private Framework.Presentation.WinForms.PagerControl pagerControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AudioFileFormatColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SampleDataTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpeakerSetupColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SamplingRateColumn;
    }
}
