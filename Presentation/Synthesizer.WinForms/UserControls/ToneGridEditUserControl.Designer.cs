namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	partial class ToneGridEditUserControl
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToneGridEditUserControl));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
			this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
			this.specializedDataGridView = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.SpecializedDataGridView();
			this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PlayColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.OctaveColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FrequencyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ToneNumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tableLayoutPanelMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.specializedDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanelMain
			// 
			this.tableLayoutPanelMain.ColumnCount = 1;
			this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanelMain.Controls.Add(this.titleBarUserControl, 0, 0);
			this.tableLayoutPanelMain.Controls.Add(this.specializedDataGridView, 0, 1);
			this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(4);
			this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
			this.tableLayoutPanelMain.RowCount = 2;
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
			this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 298F));
			this.tableLayoutPanelMain.Size = new System.Drawing.Size(623, 609);
			this.tableLayoutPanelMain.TabIndex = 8;
			// 
			// titleBarUserControl
			// 
			this.titleBarUserControl.AddButtonVisible = true;
			this.titleBarUserControl.AddToInstrumentButtonVisible = false;
			this.titleBarUserControl.BackColor = System.Drawing.SystemColors.Control;
			this.titleBarUserControl.CloseButtonVisible = true;
			this.titleBarUserControl.DeleteButtonVisible = true;
			this.titleBarUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.titleBarUserControl.ExpandButtonVisible = false;
			this.titleBarUserControl.Location = new System.Drawing.Point(0, 0);
			this.titleBarUserControl.Margin = new System.Windows.Forms.Padding(0);
			this.titleBarUserControl.Name = "titleBarUserControl";
			this.titleBarUserControl.PlayButtonVisible = false;
			this.titleBarUserControl.RedoButtonVisible = false;
			this.titleBarUserControl.RefreshButtonVisible = false;
			this.titleBarUserControl.SaveButtonVisible = false;
			this.titleBarUserControl.Size = new System.Drawing.Size(623, 32);
			this.titleBarUserControl.TabIndex = 8;
			this.titleBarUserControl.TitleLabelVisible = true;
			this.titleBarUserControl.UndoButtonVisible = false;
			this.titleBarUserControl.AddClicked += new System.EventHandler(this.titleBarUserControl_AddClicked);
			this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
			this.titleBarUserControl.DeleteClicked += new System.EventHandler(this.titleBarUserControl_DeleteClicked);
			// 
			// specializedDataGridView
			// 
			this.specializedDataGridView.AllowUserToAddRows = false;
			this.specializedDataGridView.AllowUserToDeleteRows = false;
			this.specializedDataGridView.AllowUserToResizeRows = false;
			this.specializedDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.specializedDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
			this.specializedDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.specializedDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.specializedDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.specializedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.specializedDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDColumn,
            this.PlayColumn,
            this.OctaveColumn,
            this.ValueColumn,
            this.FrequencyColumn,
            this.ToneNumberColumn});
			this.specializedDataGridView.EnableHeadersVisualStyles = false;
			this.specializedDataGridView.Location = new System.Drawing.Point(0, 26);
			this.specializedDataGridView.Margin = new System.Windows.Forms.Padding(0);
			this.specializedDataGridView.Name = "specializedDataGridView";
			this.specializedDataGridView.RowHeadersVisible = false;
			this.specializedDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.specializedDataGridView.Size = new System.Drawing.Size(623, 583);
			this.specializedDataGridView.TabIndex = 9;
			this.specializedDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.specializedDataGridView_CellClick);
			this.specializedDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.specializedDataGridView_CellEndEdit);
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
			// PlayColumn
			// 
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle1.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle1.NullValue")));
			this.PlayColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.PlayColumn.HeaderText = "";
			this.PlayColumn.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.PlayIconThinner;
			this.PlayColumn.Name = "PlayColumn";
			this.PlayColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.PlayColumn.Width = 20;
			// 
			// OctaveColumn
			// 
			this.OctaveColumn.DataPropertyName = "Octave";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle2.Format = "0";
			this.OctaveColumn.DefaultCellStyle = dataGridViewCellStyle2;
			this.OctaveColumn.FillWeight = 40F;
			this.OctaveColumn.HeaderText = "Octave";
			this.OctaveColumn.MaxInputLength = 12;
			this.OctaveColumn.Name = "OctaveColumn";
			this.OctaveColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.OctaveColumn.Width = 85;
			// 
			// ValueColumn
			// 
			this.ValueColumn.DataPropertyName = "Value";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle3.Format = "0.#################";
			this.ValueColumn.DefaultCellStyle = dataGridViewCellStyle3;
			this.ValueColumn.HeaderText = "Value";
			this.ValueColumn.MaxInputLength = 18;
			this.ValueColumn.Name = "ValueColumn";
			this.ValueColumn.Width = 85;
			// 
			// FrequencyColumn
			// 
			this.FrequencyColumn.DataPropertyName = "Frequency";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle4.Format = "0.##";
			this.FrequencyColumn.DefaultCellStyle = dataGridViewCellStyle4;
			this.FrequencyColumn.HeaderText = "Frequency";
			this.FrequencyColumn.Name = "FrequencyColumn";
			this.FrequencyColumn.ReadOnly = true;
			this.FrequencyColumn.Width = 85;
			// 
			// ToneNumberColumn
			// 
			this.ToneNumberColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.ToneNumberColumn.DataPropertyName = "ToneNumber";
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.ToneNumberColumn.DefaultCellStyle = dataGridViewCellStyle5;
			this.ToneNumberColumn.HeaderText = "ToneNumber";
			this.ToneNumberColumn.Name = "ToneNumberColumn";
			this.ToneNumberColumn.ReadOnly = true;
			// 
			// ToneGridEditUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Controls.Add(this.tableLayoutPanelMain);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ToneGridEditUserControl";
			this.Size = new System.Drawing.Size(623, 609);
			this.Leave += new System.EventHandler(this.ToneGridEditUserControl_Leave);
			this.tableLayoutPanelMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.specializedDataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
		private Partials.TitleBarUserControl titleBarUserControl;
		private Partials.SpecializedDataGridView specializedDataGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn IDColumn;
		private System.Windows.Forms.DataGridViewImageColumn PlayColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn OctaveColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ValueColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn FrequencyColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ToneNumberColumn;
	}
}
