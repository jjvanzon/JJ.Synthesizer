namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	partial class MidiMappingElementPropertiesUserControl
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
			this.checkBoxIsActive = new System.Windows.Forms.CheckBox();
			this.labelIsActive = new System.Windows.Forms.Label();
			this.checkBoxIsRelative = new System.Windows.Forms.CheckBox();
			this.labelIsRelative = new System.Windows.Forms.Label();
			this.labelControllerCode = new System.Windows.Forms.Label();
			this.maskedTextBoxControllerCode = new System.Windows.Forms.MaskedTextBox();
			this.SuspendLayout();
			// 
			// checkBoxIsActive
			// 
			this.checkBoxIsActive.AutoSize = true;
			this.checkBoxIsActive.Location = new System.Drawing.Point(124, 47);
			this.checkBoxIsActive.Name = "checkBoxIsActive";
			this.checkBoxIsActive.Size = new System.Drawing.Size(136, 20);
			this.checkBoxIsActive.TabIndex = 17;
			this.checkBoxIsActive.Text = "checkBoxIsActive";
			this.checkBoxIsActive.UseVisualStyleBackColor = true;
			// 
			// labelIsActive
			// 
			this.labelIsActive.Location = new System.Drawing.Point(24, 45);
			this.labelIsActive.Margin = new System.Windows.Forms.Padding(0);
			this.labelIsActive.Name = "labelIsActive";
			this.labelIsActive.Size = new System.Drawing.Size(98, 22);
			this.labelIsActive.TabIndex = 16;
			this.labelIsActive.Text = "labelIsActive";
			this.labelIsActive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkBoxIsRelative
			// 
			this.checkBoxIsRelative.AutoSize = true;
			this.checkBoxIsRelative.Location = new System.Drawing.Point(127, 76);
			this.checkBoxIsRelative.Name = "checkBoxIsRelative";
			this.checkBoxIsRelative.Size = new System.Drawing.Size(149, 20);
			this.checkBoxIsRelative.TabIndex = 19;
			this.checkBoxIsRelative.Text = "checkBoxIsRelative";
			this.checkBoxIsRelative.UseVisualStyleBackColor = true;
			// 
			// labelIsRelative
			// 
			this.labelIsRelative.Location = new System.Drawing.Point(27, 74);
			this.labelIsRelative.Margin = new System.Windows.Forms.Padding(0);
			this.labelIsRelative.Name = "labelIsRelative";
			this.labelIsRelative.Size = new System.Drawing.Size(98, 22);
			this.labelIsRelative.TabIndex = 18;
			this.labelIsRelative.Text = "labelIsRelative";
			this.labelIsRelative.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelControllerCode
			// 
			this.labelControllerCode.Location = new System.Drawing.Point(25, 104);
			this.labelControllerCode.Margin = new System.Windows.Forms.Padding(0);
			this.labelControllerCode.Name = "labelControllerCode";
			this.labelControllerCode.Size = new System.Drawing.Size(140, 22);
			this.labelControllerCode.TabIndex = 20;
			this.labelControllerCode.Text = "labelControllerCode";
			this.labelControllerCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// maskedTextBoxControllerCode
			// 
			this.maskedTextBoxControllerCode.Location = new System.Drawing.Point(168, 105);
			this.maskedTextBoxControllerCode.Mask = "###";
			this.maskedTextBoxControllerCode.Name = "maskedTextBoxControllerCode";
			this.maskedTextBoxControllerCode.PromptChar = ' ';
			this.maskedTextBoxControllerCode.Size = new System.Drawing.Size(100, 22);
			this.maskedTextBoxControllerCode.TabIndex = 21;
			this.maskedTextBoxControllerCode.ValidatingType = typeof(int);
			// 
			// MidiMappingElementPropertiesUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Controls.Add(this.maskedTextBoxControllerCode);
			this.Controls.Add(this.labelControllerCode);
			this.Controls.Add(this.checkBoxIsRelative);
			this.Controls.Add(this.labelIsRelative);
			this.Controls.Add(this.checkBoxIsActive);
			this.Controls.Add(this.labelIsActive);
			this.DeleteButtonVisible = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MidiMappingElementPropertiesUserControl";
			this.Size = new System.Drawing.Size(467, 305);
			this.TitleBarText = "";
			this.Controls.SetChildIndex(this.labelIsActive, 0);
			this.Controls.SetChildIndex(this.checkBoxIsActive, 0);
			this.Controls.SetChildIndex(this.labelIsRelative, 0);
			this.Controls.SetChildIndex(this.checkBoxIsRelative, 0);
			this.Controls.SetChildIndex(this.labelControllerCode, 0);
			this.Controls.SetChildIndex(this.maskedTextBoxControllerCode, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxIsActive;
		private System.Windows.Forms.Label labelIsActive;
		private System.Windows.Forms.CheckBox checkBoxIsRelative;
		private System.Windows.Forms.Label labelIsRelative;
		private System.Windows.Forms.Label labelControllerCode;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxControllerCode;
	}
}
