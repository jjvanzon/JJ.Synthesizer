using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class InstrumentListUserControl
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
            this.specializedDataGridView = new JJ.Presentation.Synthesizer.WinForms.UserControls.SpecializedDataGridView();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.ListIndexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.specializedDataGridView)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // specializedDataGridView
            // 
            this.specializedDataGridView.AllowUserToAddRows = false;
            this.specializedDataGridView.AllowUserToDeleteRows = false;
            this.specializedDataGridView.AllowUserToResizeRows = false;
            this.specializedDataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.specializedDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.specializedDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.specializedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.specializedDataGridView.ColumnHeadersVisible = false;
            this.specializedDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ListIndexColumn,
            this.NameColumn});
            this.specializedDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.specializedDataGridView.Location = new System.Drawing.Point(0, 26);
            this.specializedDataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.specializedDataGridView.Name = "specializedDataGridView";
            this.specializedDataGridView.RowHeadersVisible = false;
            this.specializedDataGridView.Size = new System.Drawing.Size(310, 315);
            this.specializedDataGridView.TabIndex = 0;
            this.specializedDataGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.specializedDataGridView_KeyDown);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.specializedDataGridView, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.titleBarUserControl, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(310, 341);
            this.tableLayoutPanel.TabIndex = 3;
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
            this.titleBarUserControl.Size = new System.Drawing.Size(310, 26);
            this.titleBarUserControl.TabIndex = 3;
            this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
            this.titleBarUserControl.RemoveClicked += new System.EventHandler(this.titleBarUserControl_RemoveClicked);
            this.titleBarUserControl.AddClicked += new System.EventHandler(this.titleBarUserControl_AddClicked);
            // 
            // ListIndexColumn
            // 
            this.ListIndexColumn.DataPropertyName = "ListIndex";
            this.ListIndexColumn.HeaderText = "ListIndex";
            this.ListIndexColumn.Name = "ListIndexColumn";
            this.ListIndexColumn.ReadOnly = true;
            this.ListIndexColumn.Visible = false;
            this.ListIndexColumn.Width = 80;
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameColumn.DataPropertyName = "Name";
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            // 
            // InstrumentListUserControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "InstrumentListUserControl";
            this.Size = new System.Drawing.Size(310, 341);
            ((System.ComponentModel.ISupportInitialize)(this.specializedDataGridView)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SpecializedDataGridView specializedDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private TitleBarUserControl titleBarUserControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn ListIndexColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
    }
}
