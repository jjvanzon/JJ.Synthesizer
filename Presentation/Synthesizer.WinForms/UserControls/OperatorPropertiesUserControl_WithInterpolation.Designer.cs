using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	partial class OperatorPropertiesUserControl_WithInterpolation
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
			this.labelInterpolation = new System.Windows.Forms.Label();
			this.comboBoxInterpolation = new System.Windows.Forms.ComboBox();
			this.labelFollowingMode = new System.Windows.Forms.Label();
			this.comboBoxFollowingMode = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this._numericUpDownInletCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._numericUpDownOutletCount)).BeginInit();
			this.SuspendLayout();
			// 
			// _labelName
			// 
			this._labelName.TabIndex = 9;
			this._labelName.Text = "Name";
			// 
			// _textBoxName
			// 
			this._textBoxName.TabIndex = 10;
			// 
			// _labelUnderlyingPatch
			// 
			this._labelUnderlyingPatch.TabIndex = 3;
			this._labelUnderlyingPatch.Text = "Type";
			// 
			// _comboBoxUnderlyingPatch
			// 
			this._comboBoxUnderlyingPatch.TabIndex = 4;
			// 
			// _labelStandardDimension
			// 
			this._labelStandardDimension.TabIndex = 5;
			this._labelStandardDimension.Text = "Standard Dimension";
			// 
			// _comboBoxStandardDimension
			// 
			this._comboBoxStandardDimension.TabIndex = 6;
			// 
			// _labelCustomDimensionName
			// 
			this._labelCustomDimensionName.TabIndex = 7;
			this._labelCustomDimensionName.Text = "Custom Dimension";
			// 
			// _textBoxCustomDimensionName
			// 
			this._textBoxCustomDimensionName.TabIndex = 8;
			// 
			// _labelInletCount
			// 
			this._labelInletCount.Text = "Number of Inputs";
			// 
			// _labelOutletCount
			// 
			this._labelOutletCount.Text = "Number of Outputs";
			// 
			// labelInterpolation
			// 
			this.labelInterpolation.Location = new System.Drawing.Point(0, 60);
			this.labelInterpolation.Margin = new System.Windows.Forms.Padding(0);
			this.labelInterpolation.Name = "labelInterpolation";
			this.labelInterpolation.Size = new System.Drawing.Size(147, 30);
			this.labelInterpolation.TabIndex = 20;
			this.labelInterpolation.Text = "labelInterpolation";
			this.labelInterpolation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxInterpolation
			// 
			this.comboBoxInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxInterpolation.FormattingEnabled = true;
			this.comboBoxInterpolation.Location = new System.Drawing.Point(147, 60);
			this.comboBoxInterpolation.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxInterpolation.Name = "comboBoxInterpolation";
			this.comboBoxInterpolation.Size = new System.Drawing.Size(100, 24);
			this.comboBoxInterpolation.TabIndex = 21;
			// 
			// labelFollowingMode
			// 
			this.labelFollowingMode.Location = new System.Drawing.Point(98, 131);
			this.labelFollowingMode.Margin = new System.Windows.Forms.Padding(0);
			this.labelFollowingMode.Name = "labelFollowingMode";
			this.labelFollowingMode.Size = new System.Drawing.Size(147, 30);
			this.labelFollowingMode.TabIndex = 22;
			this.labelFollowingMode.Text = "labelFollowingMode";
			this.labelFollowingMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxFollowingMode
			// 
			this.comboBoxFollowingMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFollowingMode.FormattingEnabled = true;
			this.comboBoxFollowingMode.Location = new System.Drawing.Point(245, 131);
			this.comboBoxFollowingMode.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxFollowingMode.Name = "comboBoxFollowingMode";
			this.comboBoxFollowingMode.Size = new System.Drawing.Size(100, 24);
			this.comboBoxFollowingMode.TabIndex = 23;
			// 
			// OperatorPropertiesUserControl_WithInterpolation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Controls.Add(this.labelFollowingMode);
			this.Controls.Add(this.comboBoxFollowingMode);
			this.Controls.Add(this.labelInterpolation);
			this.Controls.Add(this.comboBoxInterpolation);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "OperatorPropertiesUserControl_WithInterpolation";
			this.Size = new System.Drawing.Size(352, 292);
			this.Controls.SetChildIndex(this._comboBoxUnderlyingPatch, 0);
			this.Controls.SetChildIndex(this._labelUnderlyingPatch, 0);
			this.Controls.SetChildIndex(this._textBoxName, 0);
			this.Controls.SetChildIndex(this._labelName, 0);
			this.Controls.SetChildIndex(this._textBoxCustomDimensionName, 0);
			this.Controls.SetChildIndex(this._labelCustomDimensionName, 0);
			this.Controls.SetChildIndex(this._comboBoxStandardDimension, 0);
			this.Controls.SetChildIndex(this._labelStandardDimension, 0);
			this.Controls.SetChildIndex(this.comboBoxInterpolation, 0);
			this.Controls.SetChildIndex(this.labelInterpolation, 0);
			this.Controls.SetChildIndex(this.comboBoxFollowingMode, 0);
			this.Controls.SetChildIndex(this.labelFollowingMode, 0);
			((System.ComponentModel.ISupportInitialize)(this._numericUpDownInletCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._numericUpDownOutletCount)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label labelInterpolation;
		private System.Windows.Forms.ComboBox comboBoxInterpolation;
		private System.Windows.Forms.Label labelFollowingMode;
		private System.Windows.Forms.ComboBox comboBoxFollowingMode;
	}
}
