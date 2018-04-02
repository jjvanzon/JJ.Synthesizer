namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	partial class MidiMappingPropertiesUserControl
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
			this.labelDimension = new System.Windows.Forms.Label();
			this.comboBoxDimension = new System.Windows.Forms.ComboBox();
			this.labelName = new System.Windows.Forms.Label();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.labelMidiMappingType = new System.Windows.Forms.Label();
			this.comboBoxMidiMappingType = new System.Windows.Forms.ComboBox();
			this.fromTillDimensionValues = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.FromTillUserControl();
			this.fromTillMidiValues = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.FromTillUserControl();
			this.labelDimensionValues = new System.Windows.Forms.Label();
			this.labelMidiValues = new System.Windows.Forms.Label();
			this.labelMinMaxDimensionValues = new System.Windows.Forms.Label();
			this.fromTillMinMaxDimensionValues = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.FromTillUserControl();
			this.labelPosition = new System.Windows.Forms.Label();
			this.textBoxPosition = new System.Windows.Forms.TextBox();
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
			this.labelMidiControllerCode.Location = new System.Drawing.Point(25, 98);
			this.labelMidiControllerCode.Margin = new System.Windows.Forms.Padding(0);
			this.labelMidiControllerCode.Name = "labelMidiControllerCode";
			this.labelMidiControllerCode.Size = new System.Drawing.Size(183, 22);
			this.labelMidiControllerCode.TabIndex = 20;
			this.labelMidiControllerCode.Text = "labelMidiControllerCode";
			this.labelMidiControllerCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// maskedTextBoxMidiControllerCode
			// 
			this.maskedTextBoxMidiControllerCode.Location = new System.Drawing.Point(211, 99);
			this.maskedTextBoxMidiControllerCode.Mask = "###";
			this.maskedTextBoxMidiControllerCode.Name = "maskedTextBoxMidiControllerCode";
			this.maskedTextBoxMidiControllerCode.PromptChar = ' ';
			this.maskedTextBoxMidiControllerCode.Size = new System.Drawing.Size(100, 22);
			this.maskedTextBoxMidiControllerCode.TabIndex = 21;
			this.maskedTextBoxMidiControllerCode.ValidatingType = typeof(int);
			// 
			// labelDimension
			// 
			this.labelDimension.Location = new System.Drawing.Point(25, 230);
			this.labelDimension.Margin = new System.Windows.Forms.Padding(0);
			this.labelDimension.Name = "labelDimension";
			this.labelDimension.Size = new System.Drawing.Size(168, 22);
			this.labelDimension.TabIndex = 33;
			this.labelDimension.Text = "labelDimension";
			this.labelDimension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxDimension
			// 
			this.comboBoxDimension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxDimension.FormattingEnabled = true;
			this.comboBoxDimension.Location = new System.Drawing.Point(209, 233);
			this.comboBoxDimension.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxDimension.Name = "comboBoxDimension";
			this.comboBoxDimension.Size = new System.Drawing.Size(130, 24);
			this.comboBoxDimension.TabIndex = 32;
			// 
			// labelName
			// 
			this.labelName.Location = new System.Drawing.Point(14, 259);
			this.labelName.Margin = new System.Windows.Forms.Padding(0);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(203, 22);
			this.labelName.TabIndex = 30;
			this.labelName.Text = "labelName";
			this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxName
			// 
			this.textBoxName.Location = new System.Drawing.Point(222, 260);
			this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(105, 22);
			this.textBoxName.TabIndex = 31;
			// 
			// labelMidiMappingType
			// 
			this.labelMidiMappingType.Location = new System.Drawing.Point(17, 40);
			this.labelMidiMappingType.Margin = new System.Windows.Forms.Padding(0);
			this.labelMidiMappingType.Name = "labelMidiMappingType";
			this.labelMidiMappingType.Size = new System.Drawing.Size(168, 22);
			this.labelMidiMappingType.TabIndex = 43;
			this.labelMidiMappingType.Text = "labelMidiMappingType";
			this.labelMidiMappingType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxMidiMappingType
			// 
			this.comboBoxMidiMappingType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMidiMappingType.FormattingEnabled = true;
			this.comboBoxMidiMappingType.Location = new System.Drawing.Point(201, 43);
			this.comboBoxMidiMappingType.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxMidiMappingType.Name = "comboBoxMidiMappingType";
			this.comboBoxMidiMappingType.Size = new System.Drawing.Size(130, 24);
			this.comboBoxMidiMappingType.TabIndex = 42;
			this.comboBoxMidiMappingType.SelectedIndexChanged += new System.EventHandler(this.comboBoxMidiMappingType_SelectedIndexChanged);
			// 
			// fromTillDimensionValues
			// 
			this.fromTillDimensionValues.From = "";
			this.fromTillDimensionValues.Location = new System.Drawing.Point(213, 330);
			this.fromTillDimensionValues.Margin = new System.Windows.Forms.Padding(0);
			this.fromTillDimensionValues.Mask = "";
			this.fromTillDimensionValues.Name = "fromTillDimensionValues";
			this.fromTillDimensionValues.Size = new System.Drawing.Size(133, 22);
			this.fromTillDimensionValues.TabIndex = 56;
			this.fromTillDimensionValues.Till = "";
			// 
			// fromTillMidiValues
			// 
			this.fromTillMidiValues.From = "";
			this.fromTillMidiValues.Location = new System.Drawing.Point(209, 72);
			this.fromTillMidiValues.Margin = new System.Windows.Forms.Padding(0);
			this.fromTillMidiValues.Mask = "###";
			this.fromTillMidiValues.Name = "fromTillMidiValues";
			this.fromTillMidiValues.Size = new System.Drawing.Size(133, 22);
			this.fromTillMidiValues.TabIndex = 57;
			this.fromTillMidiValues.Till = "";
			// 
			// labelDimensionValues
			// 
			this.labelDimensionValues.Location = new System.Drawing.Point(24, 331);
			this.labelDimensionValues.Margin = new System.Windows.Forms.Padding(0);
			this.labelDimensionValues.Name = "labelDimensionValues";
			this.labelDimensionValues.Size = new System.Drawing.Size(180, 22);
			this.labelDimensionValues.TabIndex = 58;
			this.labelDimensionValues.Text = "labelDimensionValues";
			this.labelDimensionValues.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMidiValues
			// 
			this.labelMidiValues.Location = new System.Drawing.Point(27, 72);
			this.labelMidiValues.Margin = new System.Windows.Forms.Padding(0);
			this.labelMidiValues.Name = "labelMidiValues";
			this.labelMidiValues.Size = new System.Drawing.Size(180, 22);
			this.labelMidiValues.TabIndex = 59;
			this.labelMidiValues.Text = "labelMidiValues";
			this.labelMidiValues.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelMinMaxDimensionValues
			// 
			this.labelMinMaxDimensionValues.Location = new System.Drawing.Point(24, 356);
			this.labelMinMaxDimensionValues.Margin = new System.Windows.Forms.Padding(0);
			this.labelMinMaxDimensionValues.Name = "labelMinMaxDimensionValues";
			this.labelMinMaxDimensionValues.Size = new System.Drawing.Size(180, 22);
			this.labelMinMaxDimensionValues.TabIndex = 69;
			this.labelMinMaxDimensionValues.Text = "labelMinMaxDimensionValues";
			this.labelMinMaxDimensionValues.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fromTillMinMaxDimensionValues
			// 
			this.fromTillMinMaxDimensionValues.From = "";
			this.fromTillMinMaxDimensionValues.Location = new System.Drawing.Point(213, 355);
			this.fromTillMinMaxDimensionValues.Margin = new System.Windows.Forms.Padding(0);
			this.fromTillMinMaxDimensionValues.Mask = "";
			this.fromTillMinMaxDimensionValues.Name = "fromTillMinMaxDimensionValues";
			this.fromTillMinMaxDimensionValues.Size = new System.Drawing.Size(133, 22);
			this.fromTillMinMaxDimensionValues.TabIndex = 68;
			this.fromTillMinMaxDimensionValues.Till = "";
			// 
			// labelPosition
			// 
			this.labelPosition.Location = new System.Drawing.Point(17, 287);
			this.labelPosition.Margin = new System.Windows.Forms.Padding(0);
			this.labelPosition.Name = "labelPosition";
			this.labelPosition.Size = new System.Drawing.Size(203, 22);
			this.labelPosition.TabIndex = 70;
			this.labelPosition.Text = "labelPosition";
			this.labelPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxPosition
			// 
			this.textBoxPosition.Location = new System.Drawing.Point(225, 288);
			this.textBoxPosition.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxPosition.Name = "textBoxPosition";
			this.textBoxPosition.Size = new System.Drawing.Size(105, 22);
			this.textBoxPosition.TabIndex = 71;
			// 
			// MidiMappingPropertiesUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Controls.Add(this.labelPosition);
			this.Controls.Add(this.textBoxPosition);
			this.Controls.Add(this.labelMinMaxDimensionValues);
			this.Controls.Add(this.fromTillMinMaxDimensionValues);
			this.Controls.Add(this.labelMidiValues);
			this.Controls.Add(this.labelDimensionValues);
			this.Controls.Add(this.fromTillMidiValues);
			this.Controls.Add(this.fromTillDimensionValues);
			this.Controls.Add(this.labelMidiMappingType);
			this.Controls.Add(this.comboBoxMidiMappingType);
			this.Controls.Add(this.labelDimension);
			this.Controls.Add(this.comboBoxDimension);
			this.Controls.Add(this.labelName);
			this.Controls.Add(this.textBoxName);
			this.Controls.Add(this.maskedTextBoxMidiControllerCode);
			this.Controls.Add(this.labelMidiControllerCode);
			this.Controls.Add(this.checkBoxIsRelative);
			this.Controls.Add(this.labelIsRelative);
			this.Controls.Add(this.checkBoxIsActive);
			this.Controls.Add(this.labelIsActive);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MidiMappingPropertiesUserControl";
			this.Size = new System.Drawing.Size(467, 746);
			this.TitleBarText = "";
			this.Controls.SetChildIndex(this.labelIsActive, 0);
			this.Controls.SetChildIndex(this.checkBoxIsActive, 0);
			this.Controls.SetChildIndex(this.labelIsRelative, 0);
			this.Controls.SetChildIndex(this.checkBoxIsRelative, 0);
			this.Controls.SetChildIndex(this.labelMidiControllerCode, 0);
			this.Controls.SetChildIndex(this.maskedTextBoxMidiControllerCode, 0);
			this.Controls.SetChildIndex(this.textBoxName, 0);
			this.Controls.SetChildIndex(this.labelName, 0);
			this.Controls.SetChildIndex(this.comboBoxDimension, 0);
			this.Controls.SetChildIndex(this.labelDimension, 0);
			this.Controls.SetChildIndex(this.comboBoxMidiMappingType, 0);
			this.Controls.SetChildIndex(this.labelMidiMappingType, 0);
			this.Controls.SetChildIndex(this.fromTillDimensionValues, 0);
			this.Controls.SetChildIndex(this.fromTillMidiValues, 0);
			this.Controls.SetChildIndex(this.labelDimensionValues, 0);
			this.Controls.SetChildIndex(this.labelMidiValues, 0);
			this.Controls.SetChildIndex(this.fromTillMinMaxDimensionValues, 0);
			this.Controls.SetChildIndex(this.labelMinMaxDimensionValues, 0);
			this.Controls.SetChildIndex(this.textBoxPosition, 0);
			this.Controls.SetChildIndex(this.labelPosition, 0);
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
		private System.Windows.Forms.Label labelDimension;
		private System.Windows.Forms.ComboBox comboBoxDimension;
		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label labelMidiMappingType;
		private System.Windows.Forms.ComboBox comboBoxMidiMappingType;
		private Partials.FromTillUserControl fromTillDimensionValues;
		private Partials.FromTillUserControl fromTillMidiValues;
		private System.Windows.Forms.Label labelDimensionValues;
		private System.Windows.Forms.Label labelMidiValues;
		private System.Windows.Forms.Label labelMinMaxDimensionValues;
		private Partials.FromTillUserControl fromTillMinMaxDimensionValues;
		private System.Windows.Forms.Label labelPosition;
		private System.Windows.Forms.TextBox textBoxPosition;
	}
}
