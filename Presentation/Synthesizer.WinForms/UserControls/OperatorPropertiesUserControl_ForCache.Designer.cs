using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	partial class OperatorPropertiesUserControl_ForCache
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
			this.labelSpeakerSetup = new System.Windows.Forms.Label();
			this.comboBoxSpeakerSetup = new System.Windows.Forms.ComboBox();
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
			this.labelInterpolation.Location = new System.Drawing.Point(0, 0);
			this.labelInterpolation.Margin = new System.Windows.Forms.Padding(0);
			this.labelInterpolation.Name = "labelInterpolation";
			this.labelInterpolation.Size = new System.Drawing.Size(10, 10);
			this.labelInterpolation.TabIndex = 24;
			this.labelInterpolation.Text = "labelInterpolation";
			this.labelInterpolation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxInterpolation
			// 
			this.comboBoxInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxInterpolation.FormattingEnabled = true;
			this.comboBoxInterpolation.Location = new System.Drawing.Point(0, 0);
			this.comboBoxInterpolation.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxInterpolation.Name = "comboBoxInterpolation";
			this.comboBoxInterpolation.Size = new System.Drawing.Size(10, 24);
			this.comboBoxInterpolation.TabIndex = 25;
			// 
			// labelSpeakerSetup
			// 
			this.labelSpeakerSetup.Cursor = System.Windows.Forms.Cursors.Default;
			this.labelSpeakerSetup.Location = new System.Drawing.Point(0, 0);
			this.labelSpeakerSetup.Margin = new System.Windows.Forms.Padding(0);
			this.labelSpeakerSetup.Name = "labelSpeakerSetup";
			this.labelSpeakerSetup.Size = new System.Drawing.Size(10, 10);
			this.labelSpeakerSetup.TabIndex = 26;
			this.labelSpeakerSetup.Text = "labelSpeakerSetup";
			this.labelSpeakerSetup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxSpeakerSetup
			// 
			this.comboBoxSpeakerSetup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSpeakerSetup.FormattingEnabled = true;
			this.comboBoxSpeakerSetup.Location = new System.Drawing.Point(0, 0);
			this.comboBoxSpeakerSetup.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxSpeakerSetup.Name = "comboBoxSpeakerSetup";
			this.comboBoxSpeakerSetup.Size = new System.Drawing.Size(10, 24);
			this.comboBoxSpeakerSetup.TabIndex = 27;
			// 
			// OperatorPropertiesUserControl_ForCache
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Controls.Add(this.labelInterpolation);
			this.Controls.Add(this.comboBoxInterpolation);
			this.Controls.Add(this.labelSpeakerSetup);
			this.Controls.Add(this.comboBoxSpeakerSetup);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "OperatorPropertiesUserControl_ForCache";
			this.DeleteButtonVisible = true;
			this.Size = new System.Drawing.Size(638, 443);
			this.TitleBarText = "Operator Properties";
			this.Controls.SetChildIndex(this._textBoxName, 0);
			this.Controls.SetChildIndex(this._labelName, 0);
			this.Controls.SetChildIndex(this._textBoxCustomDimensionName, 0);
			this.Controls.SetChildIndex(this._labelCustomDimensionName, 0);
			this.Controls.SetChildIndex(this._comboBoxStandardDimension, 0);
			this.Controls.SetChildIndex(this._labelStandardDimension, 0);
			this.Controls.SetChildIndex(this.comboBoxSpeakerSetup, 0);
			this.Controls.SetChildIndex(this.labelSpeakerSetup, 0);
			this.Controls.SetChildIndex(this.comboBoxInterpolation, 0);
			this.Controls.SetChildIndex(this.labelInterpolation, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label labelInterpolation;
		private System.Windows.Forms.ComboBox comboBoxInterpolation;
		private System.Windows.Forms.Label labelSpeakerSetup;
		private System.Windows.Forms.ComboBox comboBoxSpeakerSetup;
	}
}
