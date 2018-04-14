using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	partial class NodePropertiesUserControl
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
			this.labelNodeType = new System.Windows.Forms.Label();
			this.comboBoxNodeType = new System.Windows.Forms.ComboBox();
			this.labelY = new System.Windows.Forms.Label();
			this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
			this.labelX = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
			this.SuspendLayout();
			// 
			// labelNodeType
			// 
			this.labelNodeType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelNodeType.Location = new System.Drawing.Point(0, 0);
			this.labelNodeType.Margin = new System.Windows.Forms.Padding(0);
			this.labelNodeType.Name = "labelNodeType";
			this.labelNodeType.Size = new System.Drawing.Size(13, 12);
			this.labelNodeType.TabIndex = 15;
			this.labelNodeType.Text = "labelNodeType";
			this.labelNodeType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxNodeType
			// 
			this.comboBoxNodeType.Dock = System.Windows.Forms.DockStyle.Fill;
			this.comboBoxNodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxNodeType.FormattingEnabled = true;
			this.comboBoxNodeType.Location = new System.Drawing.Point(0, 0);
			this.comboBoxNodeType.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxNodeType.Name = "comboBoxNodeType";
			this.comboBoxNodeType.Size = new System.Drawing.Size(13, 24);
			this.comboBoxNodeType.TabIndex = 16;
			// 
			// labelY
			// 
			this.labelY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelY.Location = new System.Drawing.Point(0, 0);
			this.labelY.Margin = new System.Windows.Forms.Padding(0);
			this.labelY.Name = "labelY";
			this.labelY.Size = new System.Drawing.Size(13, 12);
			this.labelY.TabIndex = 2;
			this.labelY.Text = "labelName";
			this.labelY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// numericUpDownX
			// 
			this.numericUpDownX.DecimalPlaces = 6;
			this.numericUpDownX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numericUpDownX.Increment = new decimal(new int[] {
			1,
			0,
			0,
			65536});
			this.numericUpDownX.Location = new System.Drawing.Point(0, 0);
			this.numericUpDownX.Margin = new System.Windows.Forms.Padding(0);
			this.numericUpDownX.Maximum = new decimal(new int[] {
			1000000000,
			0,
			0,
			0});
			this.numericUpDownX.Minimum = new decimal(new int[] {
			1000000000,
			0,
			0,
			-2147483648});
			this.numericUpDownX.Name = "numericUpDownX";
			this.numericUpDownX.Size = new System.Drawing.Size(13, 22);
			this.numericUpDownX.TabIndex = 21;
			// 
			// numericUpDownY
			// 
			this.numericUpDownY.DecimalPlaces = 6;
			this.numericUpDownY.Dock = System.Windows.Forms.DockStyle.Fill;
			this.numericUpDownY.Increment = new decimal(new int[] {
			1,
			0,
			0,
			65536});
			this.numericUpDownY.Location = new System.Drawing.Point(0, 0);
			this.numericUpDownY.Margin = new System.Windows.Forms.Padding(0);
			this.numericUpDownY.Maximum = new decimal(new int[] {
			1000000000,
			0,
			0,
			0});
			this.numericUpDownY.Minimum = new decimal(new int[] {
			1000000000,
			0,
			0,
			-2147483648});
			this.numericUpDownY.Name = "numericUpDownY";
			this.numericUpDownY.Size = new System.Drawing.Size(13, 22);
			this.numericUpDownY.TabIndex = 22;
			// 
			// labelX
			// 
			this.labelX.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelX.Location = new System.Drawing.Point(0, 0);
			this.labelX.Margin = new System.Windows.Forms.Padding(0);
			this.labelX.Name = "labelX";
			this.labelX.Size = new System.Drawing.Size(13, 12);
			this.labelX.TabIndex = 23;
			this.labelX.Text = "labelX";
			this.labelX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// NodePropertiesUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Controls.Add(this.comboBoxNodeType);
			this.Controls.Add(this.labelY);
			this.Controls.Add(this.numericUpDownX);
			this.Controls.Add(this.numericUpDownY);
			this.Controls.Add(this.labelNodeType);
			this.Controls.Add(this.labelX);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "NodePropertiesUserControl";
			this.Size = new System.Drawing.Size(13, 12);
			this.Controls.SetChildIndex(this.labelX, 0);
			this.Controls.SetChildIndex(this.labelNodeType, 0);
			this.Controls.SetChildIndex(this.numericUpDownY, 0);
			this.Controls.SetChildIndex(this.numericUpDownX, 0);
			this.Controls.SetChildIndex(this.labelY, 0);
			this.Controls.SetChildIndex(this.comboBoxNodeType, 0);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label labelY;
		private System.Windows.Forms.Label labelNodeType;
		private System.Windows.Forms.ComboBox comboBoxNodeType;
		private System.Windows.Forms.NumericUpDown numericUpDownX;
		private System.Windows.Forms.NumericUpDown numericUpDownY;
		private System.Windows.Forms.Label labelX;
	}
}
