using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	partial class OperatorPropertiesUserControl
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
			this._labelUnderlyingPatch.TabIndex = 5;
			this._labelUnderlyingPatch.Text = "Underlying Patch";
			// 
			// _comboBoxUnderlyingPatch
			// 
			this._comboBoxUnderlyingPatch.Size = new System.Drawing.Size(121, 24);
			this._comboBoxUnderlyingPatch.TabIndex = 6;
			// 
			// _labelStandardDimension
			// 
			this._labelStandardDimension.TabIndex = 5;
			this._labelStandardDimension.Text = "Standard Dimension";
			// 
			// _comboBoxStandardDimension
			// 
			this._comboBoxStandardDimension.Size = new System.Drawing.Size(121, 24);
			this._comboBoxStandardDimension.TabIndex = 6;
			// 
			// _textBoxCustomDimensionName
			// 
			this._textBoxCustomDimensionName.TabIndex = 8;
			// 
			// _labelCustomDimensionName
			// 
			this._labelCustomDimensionName.TabIndex = 7;
			this._labelCustomDimensionName.Text = "Custom Dimension";
			// 
			// OperatorPropertiesUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "OperatorPropertiesUserControl";
			this.Size = new System.Drawing.Size(562, 389);
			this.TitleBarText = "Operator Properties";
			this.Controls.SetChildIndex(this._textBoxName, 0);
			this.Controls.SetChildIndex(this._labelName, 0);
			this.Controls.SetChildIndex(this._textBoxCustomDimensionName, 0);
			this.Controls.SetChildIndex(this._labelCustomDimensionName, 0);
			this.Controls.SetChildIndex(this._comboBoxStandardDimension, 0);
			this.Controls.SetChildIndex(this._labelStandardDimension, 0);
			this.Controls.SetChildIndex(this._comboBoxUnderlyingPatch, 0);
			this.Controls.SetChildIndex(this._labelUnderlyingPatch, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
	}
}
