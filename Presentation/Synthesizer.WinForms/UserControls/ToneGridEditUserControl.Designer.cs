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
			this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
			this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
			this.specializedDataGridView = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.SpecializedDataGridView();
			this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PlayColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.OctaveColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FrequencyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
			this.titleBarUserControl.BackColor = System.Drawing.SystemColors.Control;
			this.titleBarUserControl.CloseButtonVisible = true;
			this.titleBarUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.titleBarUserControl.Location = new System.Drawing.Point(0, 0);
			this.titleBarUserControl.Margin = new System.Windows.Forms.Padding(0);
			this.titleBarUserControl.Name = "titleBarUserControl";
			this.titleBarUserControl.ExpandButtonVisible = false;
			this.titleBarUserControl.PlayButtonVisible = false;
			this.titleBarUserControl.RefreshButtonVisible = false;
			this.titleBarUserControl.RemoveButtonVisible = true;
			this.titleBarUserControl.SaveButtonVisible = false;
			this.titleBarUserControl.Size = new System.Drawing.Size(623, 26);
			this.titleBarUserControl.TabIndex = 8;
			this.titleBarUserControl.AddClicked += new System.EventHandler(this.titleBarUserControl_AddClicked);
			this.titleBarUserControl.RemoveClicked += new System.EventHandler(this.titleBarUserControl_RemoveClicked);
			this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
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
			this.NumberColumn,
			this.FrequencyColumn});
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
			dataGridViewCellStyle2.Format = "0";
			this.OctaveColumn.DefaultCellStyle = dataGridViewCellStyle2;
			this.OctaveColumn.FillWeight = 40F;
			this.OctaveColumn.HeaderText = "Octave";
			this.OctaveColumn.MaxInputLength = 12;
			this.OctaveColumn.Name = "OctaveColumn";
			this.OctaveColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// NumberColumn
			// 
			this.NumberColumn.DataPropertyName = "Number";
			dataGridViewCellStyle3.Format = "0.#################";
			this.NumberColumn.DefaultCellStyle = dataGridViewCellStyle3;
			this.NumberColumn.HeaderText = "Number";
			this.NumberColumn.MaxInputLength = 18;
			this.NumberColumn.Name = "NumberColumn";
			this.NumberColumn.Width = 200;
			// 
			// FrequencyColumn
			// 
			this.FrequencyColumn.DataPropertyName = "Frequency";
			dataGridViewCellStyle4.Format = "0.##";
			this.FrequencyColumn.DefaultCellStyle = dataGridViewCellStyle4;
			this.FrequencyColumn.HeaderText = "Frequency";
			this.FrequencyColumn.Name = "FrequencyColumn";
			this.FrequencyColumn.ReadOnly = true;
			this.FrequencyColumn.Width = 200;
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
		private System.Windows.Forms.DataGridViewTextBoxColumn NumberColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn FrequencyColumn;
	}
}
