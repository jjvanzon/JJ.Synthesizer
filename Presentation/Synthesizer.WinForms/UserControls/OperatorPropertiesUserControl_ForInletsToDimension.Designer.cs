using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	partial class OperatorPropertiesUserControl_ForInletsToDimension
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
			// labelInterpolation
			// 
			this.labelInterpolation.Location = new System.Drawing.Point(0, 90);
			this.labelInterpolation.Margin = new System.Windows.Forms.Padding(0);
			this.labelInterpolation.Name = "labelInterpolation";
			this.labelInterpolation.Size = new System.Drawing.Size(147, 30);
			this.labelInterpolation.TabIndex = 24;
			this.labelInterpolation.Text = "labelInterpolation";
			this.labelInterpolation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxInterpolation
			// 
			this.comboBoxInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxInterpolation.FormattingEnabled = true;
			this.comboBoxInterpolation.Location = new System.Drawing.Point(147, 90);
			this.comboBoxInterpolation.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxInterpolation.Name = "comboBoxInterpolation";
			this.comboBoxInterpolation.Size = new System.Drawing.Size(10, 24);
			this.comboBoxInterpolation.TabIndex = 25;
			// 
			// OperatorPropertiesUserControl_ForInletsToDimension
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Controls.Add(this.labelInterpolation);
			this.Controls.Add(this.comboBoxInterpolation);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "OperatorPropertiesUserControl_ForInletsToDimension";
			this.RemoveButtonVisible = true;
			this.Size = new System.Drawing.Size(234, 185);
			this.TitleBarText = "Operator Properties";
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
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label labelInterpolation;
		private System.Windows.Forms.ComboBox comboBoxInterpolation;
	}
}
