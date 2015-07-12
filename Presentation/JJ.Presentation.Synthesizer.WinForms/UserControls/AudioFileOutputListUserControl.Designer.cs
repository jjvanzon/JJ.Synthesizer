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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.specializedDataGridView = new JJ.Presentation.Synthesizer.WinForms.UserControls.SpecializedDataGridView();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AudioFileFormatColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SampleDataTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpeakerSetupColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SamplingRateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.specializedDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.titleBarUserControl, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.specializedDataGridView, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(796, 400);
            this.tableLayoutPanel.TabIndex = 4;
            // 
            // titleBarUserControl
            // 
            this.titleBarUserControl.AddButtonVisible = true;
            this.titleBarUserControl.BackColor = System.Drawing.SystemColors.Control;
            this.titleBarUserControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleBarUserControl.CloseButtonVisible = true;
            this.titleBarUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleBarUserControl.Location = new System.Drawing.Point(0, 0);
            this.titleBarUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.titleBarUserControl.Name = "titleBarUserControl";
            this.titleBarUserControl.RemoveButtonVisible = true;
            this.titleBarUserControl.Size = new System.Drawing.Size(796, 26);
            this.titleBarUserControl.TabIndex = 3;
            this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
            this.titleBarUserControl.RemoveClicked += new System.EventHandler(this.titleBarUserControl_RemoveClicked);
            this.titleBarUserControl.AddClicked += new System.EventHandler(this.titleBarUserControl_AddClicked);
            // 
            // specializedDataGridView
            // 
            this.specializedDataGridView.AllowUserToAddRows = false;
            this.specializedDataGridView.AllowUserToDeleteRows = false;
            this.specializedDataGridView.AllowUserToResizeRows = false;
            this.specializedDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.specializedDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.specializedDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.specializedDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.specializedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.specializedDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDColumn,
            this.NameColumn,
            this.AudioFileFormatColumn,
            this.SampleDataTypeColumn,
            this.SpeakerSetupColumn,
            this.SamplingRateColumn});
            this.specializedDataGridView.Location = new System.Drawing.Point(0, 26);
            this.specializedDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.specializedDataGridView.Name = "specializedDataGridView";
            this.specializedDataGridView.RowHeadersVisible = false;
            this.specializedDataGridView.Size = new System.Drawing.Size(796, 374);
            this.specializedDataGridView.TabIndex = 0;
            this.specializedDataGridView.DoubleClick += new System.EventHandler(this.specializedDataGridView_DoubleClick);
            this.specializedDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.specializedDataGridView_KeyDown);
            // 
            // IDColumn
            // 
            this.IDColumn.DataPropertyName = "ID";
            this.IDColumn.HeaderText = "ID";
            this.IDColumn.Name = "IDColumn";
            this.IDColumn.ReadOnly = true;
            this.IDColumn.Visible = false;
            this.IDColumn.Width = 80;
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameColumn.DataPropertyName = "Name";
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
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
            // AudioFileOutputListUserControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "AudioFileOutputListUserControl";
            this.Size = new System.Drawing.Size(796, 400);
            this.tableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.specializedDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SpecializedDataGridView specializedDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private Partials.TitleBarUserControl titleBarUserControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AudioFileFormatColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SampleDataTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpeakerSetupColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SamplingRateColumn;
    }
}
