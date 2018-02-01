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
			this.labelMidiControllerCode = new System.Windows.Forms.Label();
			this.maskedTextBoxMidiControllerCode = new System.Windows.Forms.MaskedTextBox();
			this.labelStandardDimension = new System.Windows.Forms.Label();
			this.comboBoxStandardDimension = new System.Windows.Forms.ComboBox();
			this.labelCustomDimensionName = new System.Windows.Forms.Label();
			this.textBoxCustomDimensionName = new System.Windows.Forms.TextBox();
			this.labelScale = new System.Windows.Forms.Label();
			this.comboBoxScale = new System.Windows.Forms.ComboBox();
			this.fromTillUserControlDimensionValues = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.FromTillUserControl();
			this.fromTillUserControlMidiControllerValues = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.FromTillUserControl();
			this.labelDimensionValues = new System.Windows.Forms.Label();
			this.labelMidiControllerValues = new System.Windows.Forms.Label();
			this.labelMidiVelocities = new System.Windows.Forms.Label();
			this.fromTillUserControlMidiVelocities = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.FromTillUserControl();
			this.labelPositions = new System.Windows.Forms.Label();
			this.fromTillUserControlPositions = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.FromTillUserControl();
			this.labelMidiNoteNumbers = new System.Windows.Forms.Label();
			this.fromTillUserControlMidiNoteNumbers = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.FromTillUserControl();
			this.labelToneNumbers = new System.Windows.Forms.Label();
			this.fromTillUserControlToneNumbers = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.FromTillUserControl();
			this.labelMinMaxDimensionValues = new System.Windows.Forms.Label();
			this.fromTillUserControlMinMaxDimensionValues = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.FromTillUserControl();
			this.SuspendLayout();
			// 
			// checkBoxIsActive
			// 
			this.checkBoxIsActive.AutoSize = true;
			this.checkBoxIsActive.Location = new System.Drawing.Point(220, 470);
			this.checkBoxIsActive.Name = "checkBoxIsActive";
			this.checkBoxIsActive.Size = new System.Drawing.Size(136, 20);
			this.checkBoxIsActive.TabIndex = 17;
			this.checkBoxIsActive.Text = "checkBoxIsActive";
			this.checkBoxIsActive.UseVisualStyleBackColor = true;
			// 
			// labelIsActive
			// 
			this.labelIsActive.Location = new System.Drawing.Point(120, 468);
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
			this.checkBoxIsRelative.Location = new System.Drawing.Point(223, 450);
			this.checkBoxIsRelative.Name = "checkBoxIsRelative";
			this.checkBoxIsRelative.Size = new System.Drawing.Size(149, 20);
			this.checkBoxIsRelative.TabIndex = 19;
			this.checkBoxIsRelative.Text = "checkBoxIsRelative";
			this.checkBoxIsRelative.UseVisualStyleBackColor = true;
			// 
			// labelIsRelative
			// 
			this.labelIsRelative.Location = new System.Drawing.Point(123, 448);
			this.labelIsRelative.Margin = new System.Windows.Forms.Padding(0);
			this.labelIsRelative.Name = "labelIsRelative";
			this.labelIsRelative.Size = new System.Drawing.Size(98, 22);
			this.labelIsRelative.TabIndex = 18;
			this.labelIsRelative.Text = "labelIsRelative";
			this.labelIsRelative.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMidiControllerCode
			// 
			this.labelMidiControllerCode.Location = new System.Drawing.Point(25, 42);
			this.labelMidiControllerCode.Margin = new System.Windows.Forms.Padding(0);
			this.labelMidiControllerCode.Name = "labelMidiControllerCode";
			this.labelMidiControllerCode.Size = new System.Drawing.Size(183, 22);
			this.labelMidiControllerCode.TabIndex = 20;
			this.labelMidiControllerCode.Text = "labelMidiControllerCode";
			this.labelMidiControllerCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// maskedTextBoxMidiControllerCode
			// 
			this.maskedTextBoxMidiControllerCode.Location = new System.Drawing.Point(211, 43);
			this.maskedTextBoxMidiControllerCode.Mask = "###";
			this.maskedTextBoxMidiControllerCode.Name = "maskedTextBoxMidiControllerCode";
			this.maskedTextBoxMidiControllerCode.PromptChar = ' ';
			this.maskedTextBoxMidiControllerCode.Size = new System.Drawing.Size(100, 22);
			this.maskedTextBoxMidiControllerCode.TabIndex = 21;
			this.maskedTextBoxMidiControllerCode.ValidatingType = typeof(int);
			// 
			// labelStandardDimension
			// 
			this.labelStandardDimension.Location = new System.Drawing.Point(25, 157);
			this.labelStandardDimension.Margin = new System.Windows.Forms.Padding(0);
			this.labelStandardDimension.Name = "labelStandardDimension";
			this.labelStandardDimension.Size = new System.Drawing.Size(168, 22);
			this.labelStandardDimension.TabIndex = 33;
			this.labelStandardDimension.Text = "labelStandardDimension";
			this.labelStandardDimension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxStandardDimension
			// 
			this.comboBoxStandardDimension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxStandardDimension.FormattingEnabled = true;
			this.comboBoxStandardDimension.Location = new System.Drawing.Point(209, 160);
			this.comboBoxStandardDimension.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxStandardDimension.Name = "comboBoxStandardDimension";
			this.comboBoxStandardDimension.Size = new System.Drawing.Size(130, 24);
			this.comboBoxStandardDimension.TabIndex = 32;
			// 
			// labelCustomDimensionName
			// 
			this.labelCustomDimensionName.Location = new System.Drawing.Point(14, 186);
			this.labelCustomDimensionName.Margin = new System.Windows.Forms.Padding(0);
			this.labelCustomDimensionName.Name = "labelCustomDimensionName";
			this.labelCustomDimensionName.Size = new System.Drawing.Size(203, 22);
			this.labelCustomDimensionName.TabIndex = 30;
			this.labelCustomDimensionName.Text = "labelCustomDimensionName";
			this.labelCustomDimensionName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxCustomDimensionName
			// 
			this.textBoxCustomDimensionName.Location = new System.Drawing.Point(222, 187);
			this.textBoxCustomDimensionName.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxCustomDimensionName.Name = "textBoxCustomDimensionName";
			this.textBoxCustomDimensionName.Size = new System.Drawing.Size(105, 22);
			this.textBoxCustomDimensionName.TabIndex = 31;
			// 
			// labelScale
			// 
			this.labelScale.Location = new System.Drawing.Point(42, 372);
			this.labelScale.Margin = new System.Windows.Forms.Padding(0);
			this.labelScale.Name = "labelScale";
			this.labelScale.Size = new System.Drawing.Size(168, 22);
			this.labelScale.TabIndex = 43;
			this.labelScale.Text = "labelScale";
			this.labelScale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxScale
			// 
			this.comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxScale.FormattingEnabled = true;
			this.comboBoxScale.Location = new System.Drawing.Point(226, 375);
			this.comboBoxScale.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxScale.Name = "comboBoxScale";
			this.comboBoxScale.Size = new System.Drawing.Size(130, 24);
			this.comboBoxScale.TabIndex = 42;
			// 
			// fromTillUserControlDimensionValues
			// 
			this.fromTillUserControlDimensionValues.From = "";
			this.fromTillUserControlDimensionValues.Location = new System.Drawing.Point(221, 548);
			this.fromTillUserControlDimensionValues.Margin = new System.Windows.Forms.Padding(0);
			this.fromTillUserControlDimensionValues.Mask = "";
			this.fromTillUserControlDimensionValues.Name = "fromTillUserControlDimensionValues";
			this.fromTillUserControlDimensionValues.Size = new System.Drawing.Size(133, 22);
			this.fromTillUserControlDimensionValues.TabIndex = 56;
			this.fromTillUserControlDimensionValues.Till = "";
			// 
			// fromTillUserControlMidiControllerValues
			// 
			this.fromTillUserControlMidiControllerValues.From = "";
			this.fromTillUserControlMidiControllerValues.Location = new System.Drawing.Point(215, 573);
			this.fromTillUserControlMidiControllerValues.Margin = new System.Windows.Forms.Padding(0);
			this.fromTillUserControlMidiControllerValues.Mask = "###";
			this.fromTillUserControlMidiControllerValues.Name = "fromTillUserControlMidiControllerValues";
			this.fromTillUserControlMidiControllerValues.Size = new System.Drawing.Size(133, 22);
			this.fromTillUserControlMidiControllerValues.TabIndex = 57;
			this.fromTillUserControlMidiControllerValues.Till = "";
			// 
			// labelDimensionValues
			// 
			this.labelDimensionValues.Location = new System.Drawing.Point(32, 549);
			this.labelDimensionValues.Margin = new System.Windows.Forms.Padding(0);
			this.labelDimensionValues.Name = "labelDimensionValues";
			this.labelDimensionValues.Size = new System.Drawing.Size(180, 22);
			this.labelDimensionValues.TabIndex = 58;
			this.labelDimensionValues.Text = "labelDimensionValues";
			this.labelDimensionValues.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMidiControllerValues
			// 
			this.labelMidiControllerValues.Location = new System.Drawing.Point(33, 573);
			this.labelMidiControllerValues.Margin = new System.Windows.Forms.Padding(0);
			this.labelMidiControllerValues.Name = "labelMidiControllerValues";
			this.labelMidiControllerValues.Size = new System.Drawing.Size(180, 22);
			this.labelMidiControllerValues.TabIndex = 59;
			this.labelMidiControllerValues.Text = "labelMidiControllerValues";
			this.labelMidiControllerValues.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMidiVelocities
			// 
			this.labelMidiVelocities.Location = new System.Drawing.Point(34, 598);
			this.labelMidiVelocities.Margin = new System.Windows.Forms.Padding(0);
			this.labelMidiVelocities.Name = "labelMidiVelocities";
			this.labelMidiVelocities.Size = new System.Drawing.Size(180, 22);
			this.labelMidiVelocities.TabIndex = 61;
			this.labelMidiVelocities.Text = "labelMidiVelocities";
			this.labelMidiVelocities.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fromTillUserControlMidiVelocities
			// 
			this.fromTillUserControlMidiVelocities.From = "";
			this.fromTillUserControlMidiVelocities.Location = new System.Drawing.Point(216, 598);
			this.fromTillUserControlMidiVelocities.Margin = new System.Windows.Forms.Padding(0);
			this.fromTillUserControlMidiVelocities.Mask = "###";
			this.fromTillUserControlMidiVelocities.Name = "fromTillUserControlMidiVelocities";
			this.fromTillUserControlMidiVelocities.Size = new System.Drawing.Size(133, 22);
			this.fromTillUserControlMidiVelocities.TabIndex = 60;
			this.fromTillUserControlMidiVelocities.Till = "";
			// 
			// labelPositions
			// 
			this.labelPositions.Location = new System.Drawing.Point(34, 625);
			this.labelPositions.Margin = new System.Windows.Forms.Padding(0);
			this.labelPositions.Name = "labelPositions";
			this.labelPositions.Size = new System.Drawing.Size(180, 22);
			this.labelPositions.TabIndex = 63;
			this.labelPositions.Text = "labelPositions";
			this.labelPositions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fromTillUserControlPositions
			// 
			this.fromTillUserControlPositions.From = "";
			this.fromTillUserControlPositions.Location = new System.Drawing.Point(216, 625);
			this.fromTillUserControlPositions.Margin = new System.Windows.Forms.Padding(0);
			this.fromTillUserControlPositions.Mask = "";
			this.fromTillUserControlPositions.Name = "fromTillUserControlPositions";
			this.fromTillUserControlPositions.Size = new System.Drawing.Size(133, 22);
			this.fromTillUserControlPositions.TabIndex = 62;
			this.fromTillUserControlPositions.Till = "";
			// 
			// labelMidiNoteNumbers
			// 
			this.labelMidiNoteNumbers.Location = new System.Drawing.Point(30, 651);
			this.labelMidiNoteNumbers.Margin = new System.Windows.Forms.Padding(0);
			this.labelMidiNoteNumbers.Name = "labelMidiNoteNumbers";
			this.labelMidiNoteNumbers.Size = new System.Drawing.Size(180, 22);
			this.labelMidiNoteNumbers.TabIndex = 65;
			this.labelMidiNoteNumbers.Text = "labelMidiNoteNumbers";
			this.labelMidiNoteNumbers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fromTillUserControlMidiNoteNumbers
			// 
			this.fromTillUserControlMidiNoteNumbers.From = "";
			this.fromTillUserControlMidiNoteNumbers.Location = new System.Drawing.Point(212, 651);
			this.fromTillUserControlMidiNoteNumbers.Margin = new System.Windows.Forms.Padding(0);
			this.fromTillUserControlMidiNoteNumbers.Mask = "###";
			this.fromTillUserControlMidiNoteNumbers.Name = "fromTillUserControlMidiNoteNumbers";
			this.fromTillUserControlMidiNoteNumbers.Size = new System.Drawing.Size(133, 22);
			this.fromTillUserControlMidiNoteNumbers.TabIndex = 64;
			this.fromTillUserControlMidiNoteNumbers.Till = "";
			// 
			// labelToneNumbers
			// 
			this.labelToneNumbers.Location = new System.Drawing.Point(31, 677);
			this.labelToneNumbers.Margin = new System.Windows.Forms.Padding(0);
			this.labelToneNumbers.Name = "labelToneNumbers";
			this.labelToneNumbers.Size = new System.Drawing.Size(180, 22);
			this.labelToneNumbers.TabIndex = 67;
			this.labelToneNumbers.Text = "labelToneNumbers";
			this.labelToneNumbers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fromTillUserControlToneNumbers
			// 
			this.fromTillUserControlToneNumbers.From = "";
			this.fromTillUserControlToneNumbers.Location = new System.Drawing.Point(213, 677);
			this.fromTillUserControlToneNumbers.Margin = new System.Windows.Forms.Padding(0);
			this.fromTillUserControlToneNumbers.Mask = "###";
			this.fromTillUserControlToneNumbers.Name = "fromTillUserControlToneNumbers";
			this.fromTillUserControlToneNumbers.Size = new System.Drawing.Size(133, 22);
			this.fromTillUserControlToneNumbers.TabIndex = 66;
			this.fromTillUserControlToneNumbers.Till = "";
			// 
			// labelMinMaxDimensionValues
			// 
			this.labelMinMaxDimensionValues.Location = new System.Drawing.Point(24, 702);
			this.labelMinMaxDimensionValues.Margin = new System.Windows.Forms.Padding(0);
			this.labelMinMaxDimensionValues.Name = "labelMinMaxDimensionValues";
			this.labelMinMaxDimensionValues.Size = new System.Drawing.Size(180, 22);
			this.labelMinMaxDimensionValues.TabIndex = 69;
			this.labelMinMaxDimensionValues.Text = "labelMinMaxDimensionValues";
			this.labelMinMaxDimensionValues.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fromTillUserControlMinMaxDimensionValues
			// 
			this.fromTillUserControlMinMaxDimensionValues.From = "";
			this.fromTillUserControlMinMaxDimensionValues.Location = new System.Drawing.Point(213, 701);
			this.fromTillUserControlMinMaxDimensionValues.Margin = new System.Windows.Forms.Padding(0);
			this.fromTillUserControlMinMaxDimensionValues.Mask = "";
			this.fromTillUserControlMinMaxDimensionValues.Name = "fromTillUserControlMinMaxDimensionValues";
			this.fromTillUserControlMinMaxDimensionValues.Size = new System.Drawing.Size(133, 22);
			this.fromTillUserControlMinMaxDimensionValues.TabIndex = 68;
			this.fromTillUserControlMinMaxDimensionValues.Till = "";
			// 
			// MidiMappingElementPropertiesUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Controls.Add(this.labelMinMaxDimensionValues);
			this.Controls.Add(this.fromTillUserControlMinMaxDimensionValues);
			this.Controls.Add(this.labelToneNumbers);
			this.Controls.Add(this.fromTillUserControlToneNumbers);
			this.Controls.Add(this.labelMidiNoteNumbers);
			this.Controls.Add(this.fromTillUserControlMidiNoteNumbers);
			this.Controls.Add(this.labelPositions);
			this.Controls.Add(this.fromTillUserControlPositions);
			this.Controls.Add(this.labelMidiVelocities);
			this.Controls.Add(this.fromTillUserControlMidiVelocities);
			this.Controls.Add(this.labelMidiControllerValues);
			this.Controls.Add(this.labelDimensionValues);
			this.Controls.Add(this.fromTillUserControlMidiControllerValues);
			this.Controls.Add(this.fromTillUserControlDimensionValues);
			this.Controls.Add(this.labelScale);
			this.Controls.Add(this.comboBoxScale);
			this.Controls.Add(this.labelStandardDimension);
			this.Controls.Add(this.comboBoxStandardDimension);
			this.Controls.Add(this.labelCustomDimensionName);
			this.Controls.Add(this.textBoxCustomDimensionName);
			this.Controls.Add(this.maskedTextBoxMidiControllerCode);
			this.Controls.Add(this.labelMidiControllerCode);
			this.Controls.Add(this.checkBoxIsRelative);
			this.Controls.Add(this.labelIsRelative);
			this.Controls.Add(this.checkBoxIsActive);
			this.Controls.Add(this.labelIsActive);
			this.DeleteButtonVisible = true;
			this.ExpandButtonVisible = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MidiMappingElementPropertiesUserControl";
			this.Size = new System.Drawing.Size(467, 746);
			this.TitleBarText = "";
			this.Controls.SetChildIndex(this.labelIsActive, 0);
			this.Controls.SetChildIndex(this.checkBoxIsActive, 0);
			this.Controls.SetChildIndex(this.labelIsRelative, 0);
			this.Controls.SetChildIndex(this.checkBoxIsRelative, 0);
			this.Controls.SetChildIndex(this.labelMidiControllerCode, 0);
			this.Controls.SetChildIndex(this.maskedTextBoxMidiControllerCode, 0);
			this.Controls.SetChildIndex(this.textBoxCustomDimensionName, 0);
			this.Controls.SetChildIndex(this.labelCustomDimensionName, 0);
			this.Controls.SetChildIndex(this.comboBoxStandardDimension, 0);
			this.Controls.SetChildIndex(this.labelStandardDimension, 0);
			this.Controls.SetChildIndex(this.comboBoxScale, 0);
			this.Controls.SetChildIndex(this.labelScale, 0);
			this.Controls.SetChildIndex(this.fromTillUserControlDimensionValues, 0);
			this.Controls.SetChildIndex(this.fromTillUserControlMidiControllerValues, 0);
			this.Controls.SetChildIndex(this.labelDimensionValues, 0);
			this.Controls.SetChildIndex(this.labelMidiControllerValues, 0);
			this.Controls.SetChildIndex(this.fromTillUserControlMidiVelocities, 0);
			this.Controls.SetChildIndex(this.labelMidiVelocities, 0);
			this.Controls.SetChildIndex(this.fromTillUserControlPositions, 0);
			this.Controls.SetChildIndex(this.labelPositions, 0);
			this.Controls.SetChildIndex(this.fromTillUserControlMidiNoteNumbers, 0);
			this.Controls.SetChildIndex(this.labelMidiNoteNumbers, 0);
			this.Controls.SetChildIndex(this.fromTillUserControlToneNumbers, 0);
			this.Controls.SetChildIndex(this.labelToneNumbers, 0);
			this.Controls.SetChildIndex(this.fromTillUserControlMinMaxDimensionValues, 0);
			this.Controls.SetChildIndex(this.labelMinMaxDimensionValues, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxIsActive;
		private System.Windows.Forms.Label labelIsActive;
		private System.Windows.Forms.CheckBox checkBoxIsRelative;
		private System.Windows.Forms.Label labelIsRelative;
		private System.Windows.Forms.Label labelMidiControllerCode;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxMidiControllerCode;
		private System.Windows.Forms.Label labelStandardDimension;
		private System.Windows.Forms.ComboBox comboBoxStandardDimension;
		private System.Windows.Forms.Label labelCustomDimensionName;
		private System.Windows.Forms.TextBox textBoxCustomDimensionName;
		private System.Windows.Forms.Label labelScale;
		private System.Windows.Forms.ComboBox comboBoxScale;
		private Partials.FromTillUserControl fromTillUserControlDimensionValues;
		private Partials.FromTillUserControl fromTillUserControlMidiControllerValues;
		private System.Windows.Forms.Label labelDimensionValues;
		private System.Windows.Forms.Label labelMidiControllerValues;
		private System.Windows.Forms.Label labelMidiVelocities;
		private Partials.FromTillUserControl fromTillUserControlMidiVelocities;
		private System.Windows.Forms.Label labelPositions;
		private Partials.FromTillUserControl fromTillUserControlPositions;
		private System.Windows.Forms.Label labelMidiNoteNumbers;
		private Partials.FromTillUserControl fromTillUserControlMidiNoteNumbers;
		private System.Windows.Forms.Label labelToneNumbers;
		private Partials.FromTillUserControl fromTillUserControlToneNumbers;
		private System.Windows.Forms.Label labelMinMaxDimensionValues;
		private Partials.FromTillUserControl fromTillUserControlMinMaxDimensionValues;
	}
}
