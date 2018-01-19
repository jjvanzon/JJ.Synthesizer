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
			this.maskedTextBoxFromControllerValue = new System.Windows.Forms.MaskedTextBox();
			this.labelFromControllerValue = new System.Windows.Forms.Label();
			this.maskedTextBoxTillControllerValue = new System.Windows.Forms.MaskedTextBox();
			this.labelTillControllerValue = new System.Windows.Forms.Label();
			this.maskedTextBoxFromNoteNumber = new System.Windows.Forms.MaskedTextBox();
			this.labelFromNoteNumber = new System.Windows.Forms.Label();
			this.maskedTextBoxTillNoteNumber = new System.Windows.Forms.MaskedTextBox();
			this.labelTillNoteNumber = new System.Windows.Forms.Label();
			this.labelStandardDimension = new System.Windows.Forms.Label();
			this.comboBoxStandardDimension = new System.Windows.Forms.ComboBox();
			this.labelCustomDimensionName = new System.Windows.Forms.Label();
			this.textBoxCustomDimensionName = new System.Windows.Forms.TextBox();
			this.labelFromDimensionValue = new System.Windows.Forms.Label();
			this.textBoxFromDimensionValue = new System.Windows.Forms.TextBox();
			this.labelTillDimensionValue = new System.Windows.Forms.Label();
			this.textBoxTillDimensionValue = new System.Windows.Forms.TextBox();
			this.labelFromPosition = new System.Windows.Forms.Label();
			this.textBoxFromPosition = new System.Windows.Forms.TextBox();
			this.labelTillPosition = new System.Windows.Forms.Label();
			this.textBoxTillPosition = new System.Windows.Forms.TextBox();
			this.labelScale = new System.Windows.Forms.Label();
			this.comboBoxScale = new System.Windows.Forms.ComboBox();
			this.maskedTextBoxFromToneNumber = new System.Windows.Forms.MaskedTextBox();
			this.labelFromToneNumber = new System.Windows.Forms.Label();
			this.maskedTextBoxTillToneNumber = new System.Windows.Forms.MaskedTextBox();
			this.labelTillToneNumber = new System.Windows.Forms.Label();
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
			// labelControllerCode
			// 
			this.labelControllerCode.Location = new System.Drawing.Point(25, 104);
			this.labelControllerCode.Margin = new System.Windows.Forms.Padding(0);
			this.labelControllerCode.Name = "labelControllerCode";
			this.labelControllerCode.Size = new System.Drawing.Size(183, 22);
			this.labelControllerCode.TabIndex = 20;
			this.labelControllerCode.Text = "labelControllerCode";
			this.labelControllerCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// maskedTextBoxControllerCode
			// 
			this.maskedTextBoxControllerCode.Location = new System.Drawing.Point(211, 105);
			this.maskedTextBoxControllerCode.Mask = "###";
			this.maskedTextBoxControllerCode.Name = "maskedTextBoxControllerCode";
			this.maskedTextBoxControllerCode.PromptChar = ' ';
			this.maskedTextBoxControllerCode.Size = new System.Drawing.Size(100, 22);
			this.maskedTextBoxControllerCode.TabIndex = 21;
			this.maskedTextBoxControllerCode.ValidatingType = typeof(int);
			// 
			// maskedTextBoxFromControllerValue
			// 
			this.maskedTextBoxFromControllerValue.Location = new System.Drawing.Point(210, 131);
			this.maskedTextBoxFromControllerValue.Mask = "###";
			this.maskedTextBoxFromControllerValue.Name = "maskedTextBoxFromControllerValue";
			this.maskedTextBoxFromControllerValue.PromptChar = ' ';
			this.maskedTextBoxFromControllerValue.Size = new System.Drawing.Size(100, 22);
			this.maskedTextBoxFromControllerValue.TabIndex = 23;
			this.maskedTextBoxFromControllerValue.ValidatingType = typeof(int);
			// 
			// labelFromControllerValue
			// 
			this.labelFromControllerValue.Location = new System.Drawing.Point(24, 130);
			this.labelFromControllerValue.Margin = new System.Windows.Forms.Padding(0);
			this.labelFromControllerValue.Name = "labelFromControllerValue";
			this.labelFromControllerValue.Size = new System.Drawing.Size(183, 22);
			this.labelFromControllerValue.TabIndex = 22;
			this.labelFromControllerValue.Text = "labelFromControllerValue";
			this.labelFromControllerValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// maskedTextBoxTillControllerValue
			// 
			this.maskedTextBoxTillControllerValue.Location = new System.Drawing.Point(212, 157);
			this.maskedTextBoxTillControllerValue.Mask = "###";
			this.maskedTextBoxTillControllerValue.Name = "maskedTextBoxTillControllerValue";
			this.maskedTextBoxTillControllerValue.PromptChar = ' ';
			this.maskedTextBoxTillControllerValue.Size = new System.Drawing.Size(100, 22);
			this.maskedTextBoxTillControllerValue.TabIndex = 25;
			this.maskedTextBoxTillControllerValue.ValidatingType = typeof(int);
			// 
			// labelTillControllerValue
			// 
			this.labelTillControllerValue.Location = new System.Drawing.Point(26, 156);
			this.labelTillControllerValue.Margin = new System.Windows.Forms.Padding(0);
			this.labelTillControllerValue.Name = "labelTillControllerValue";
			this.labelTillControllerValue.Size = new System.Drawing.Size(183, 22);
			this.labelTillControllerValue.TabIndex = 24;
			this.labelTillControllerValue.Text = "labelTillControllerValue";
			this.labelTillControllerValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// maskedTextBoxFromNoteNumber
			// 
			this.maskedTextBoxFromNoteNumber.Location = new System.Drawing.Point(209, 177);
			this.maskedTextBoxFromNoteNumber.Mask = "###";
			this.maskedTextBoxFromNoteNumber.Name = "maskedTextBoxFromNoteNumber";
			this.maskedTextBoxFromNoteNumber.PromptChar = ' ';
			this.maskedTextBoxFromNoteNumber.Size = new System.Drawing.Size(100, 22);
			this.maskedTextBoxFromNoteNumber.TabIndex = 27;
			this.maskedTextBoxFromNoteNumber.ValidatingType = typeof(int);
			// 
			// labelFromNoteNumber
			// 
			this.labelFromNoteNumber.Location = new System.Drawing.Point(23, 176);
			this.labelFromNoteNumber.Margin = new System.Windows.Forms.Padding(0);
			this.labelFromNoteNumber.Name = "labelFromNoteNumber";
			this.labelFromNoteNumber.Size = new System.Drawing.Size(183, 22);
			this.labelFromNoteNumber.TabIndex = 26;
			this.labelFromNoteNumber.Text = "labelFromNoteNumber";
			this.labelFromNoteNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// maskedTextBoxTillNoteNumber
			// 
			this.maskedTextBoxTillNoteNumber.Location = new System.Drawing.Point(208, 200);
			this.maskedTextBoxTillNoteNumber.Mask = "###";
			this.maskedTextBoxTillNoteNumber.Name = "maskedTextBoxTillNoteNumber";
			this.maskedTextBoxTillNoteNumber.PromptChar = ' ';
			this.maskedTextBoxTillNoteNumber.Size = new System.Drawing.Size(100, 22);
			this.maskedTextBoxTillNoteNumber.TabIndex = 29;
			this.maskedTextBoxTillNoteNumber.ValidatingType = typeof(int);
			// 
			// labelTillNoteNumber
			// 
			this.labelTillNoteNumber.Location = new System.Drawing.Point(22, 199);
			this.labelTillNoteNumber.Margin = new System.Windows.Forms.Padding(0);
			this.labelTillNoteNumber.Name = "labelTillNoteNumber";
			this.labelTillNoteNumber.Size = new System.Drawing.Size(183, 22);
			this.labelTillNoteNumber.TabIndex = 28;
			this.labelTillNoteNumber.Text = "labelTillNoteNumber";
			this.labelTillNoteNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelStandardDimension
			// 
			this.labelStandardDimension.Location = new System.Drawing.Point(25, 219);
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
			this.comboBoxStandardDimension.Location = new System.Drawing.Point(209, 222);
			this.comboBoxStandardDimension.Margin = new System.Windows.Forms.Padding(0);
			this.comboBoxStandardDimension.Name = "comboBoxStandardDimension";
			this.comboBoxStandardDimension.Size = new System.Drawing.Size(130, 24);
			this.comboBoxStandardDimension.TabIndex = 32;
			// 
			// labelCustomDimensionName
			// 
			this.labelCustomDimensionName.Location = new System.Drawing.Point(14, 248);
			this.labelCustomDimensionName.Margin = new System.Windows.Forms.Padding(0);
			this.labelCustomDimensionName.Name = "labelCustomDimensionName";
			this.labelCustomDimensionName.Size = new System.Drawing.Size(203, 22);
			this.labelCustomDimensionName.TabIndex = 30;
			this.labelCustomDimensionName.Text = "labelCustomDimensionName";
			this.labelCustomDimensionName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxCustomDimensionName
			// 
			this.textBoxCustomDimensionName.Location = new System.Drawing.Point(222, 249);
			this.textBoxCustomDimensionName.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxCustomDimensionName.Name = "textBoxCustomDimensionName";
			this.textBoxCustomDimensionName.Size = new System.Drawing.Size(105, 22);
			this.textBoxCustomDimensionName.TabIndex = 31;
			// 
			// labelFromDimensionValue
			// 
			this.labelFromDimensionValue.Location = new System.Drawing.Point(30, 276);
			this.labelFromDimensionValue.Margin = new System.Windows.Forms.Padding(0);
			this.labelFromDimensionValue.Name = "labelFromDimensionValue";
			this.labelFromDimensionValue.Size = new System.Drawing.Size(180, 22);
			this.labelFromDimensionValue.TabIndex = 34;
			this.labelFromDimensionValue.Text = "labelFromDimensionValue";
			this.labelFromDimensionValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxFromDimensionValue
			// 
			this.textBoxFromDimensionValue.Location = new System.Drawing.Point(223, 274);
			this.textBoxFromDimensionValue.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxFromDimensionValue.Name = "textBoxFromDimensionValue";
			this.textBoxFromDimensionValue.Size = new System.Drawing.Size(105, 22);
			this.textBoxFromDimensionValue.TabIndex = 35;
			// 
			// labelTillDimensionValue
			// 
			this.labelTillDimensionValue.Location = new System.Drawing.Point(34, 300);
			this.labelTillDimensionValue.Margin = new System.Windows.Forms.Padding(0);
			this.labelTillDimensionValue.Name = "labelTillDimensionValue";
			this.labelTillDimensionValue.Size = new System.Drawing.Size(180, 22);
			this.labelTillDimensionValue.TabIndex = 36;
			this.labelTillDimensionValue.Text = "labelTillDimensionValue";
			this.labelTillDimensionValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxTillDimensionValue
			// 
			this.textBoxTillDimensionValue.Location = new System.Drawing.Point(227, 298);
			this.textBoxTillDimensionValue.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxTillDimensionValue.Name = "textBoxTillDimensionValue";
			this.textBoxTillDimensionValue.Size = new System.Drawing.Size(105, 22);
			this.textBoxTillDimensionValue.TabIndex = 37;
			// 
			// labelFromPosition
			// 
			this.labelFromPosition.Location = new System.Drawing.Point(34, 327);
			this.labelFromPosition.Margin = new System.Windows.Forms.Padding(0);
			this.labelFromPosition.Name = "labelFromPosition";
			this.labelFromPosition.Size = new System.Drawing.Size(180, 22);
			this.labelFromPosition.TabIndex = 38;
			this.labelFromPosition.Text = "labelFromPosition";
			this.labelFromPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxFromPosition
			// 
			this.textBoxFromPosition.Location = new System.Drawing.Point(227, 325);
			this.textBoxFromPosition.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxFromPosition.Name = "textBoxFromPosition";
			this.textBoxFromPosition.Size = new System.Drawing.Size(105, 22);
			this.textBoxFromPosition.TabIndex = 39;
			// 
			// labelTillPosition
			// 
			this.labelTillPosition.Location = new System.Drawing.Point(32, 353);
			this.labelTillPosition.Margin = new System.Windows.Forms.Padding(0);
			this.labelTillPosition.Name = "labelTillPosition";
			this.labelTillPosition.Size = new System.Drawing.Size(180, 22);
			this.labelTillPosition.TabIndex = 40;
			this.labelTillPosition.Text = "labelTillPosition";
			this.labelTillPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxTillPosition
			// 
			this.textBoxTillPosition.Location = new System.Drawing.Point(225, 351);
			this.textBoxTillPosition.Margin = new System.Windows.Forms.Padding(0);
			this.textBoxTillPosition.Name = "textBoxTillPosition";
			this.textBoxTillPosition.Size = new System.Drawing.Size(105, 22);
			this.textBoxTillPosition.TabIndex = 41;
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
			// maskedTextBoxFromToneNumber
			// 
			this.maskedTextBoxFromToneNumber.Location = new System.Drawing.Point(230, 398);
			this.maskedTextBoxFromToneNumber.Mask = "####";
			this.maskedTextBoxFromToneNumber.Name = "maskedTextBoxFromToneNumber";
			this.maskedTextBoxFromToneNumber.PromptChar = ' ';
			this.maskedTextBoxFromToneNumber.Size = new System.Drawing.Size(100, 22);
			this.maskedTextBoxFromToneNumber.TabIndex = 45;
			this.maskedTextBoxFromToneNumber.ValidatingType = typeof(int);
			// 
			// labelFromToneNumber
			// 
			this.labelFromToneNumber.Location = new System.Drawing.Point(44, 397);
			this.labelFromToneNumber.Margin = new System.Windows.Forms.Padding(0);
			this.labelFromToneNumber.Name = "labelFromToneNumber";
			this.labelFromToneNumber.Size = new System.Drawing.Size(183, 22);
			this.labelFromToneNumber.TabIndex = 44;
			this.labelFromToneNumber.Text = "labelFromToneNumber";
			this.labelFromToneNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// maskedTextBoxTillToneNumber
			// 
			this.maskedTextBoxTillToneNumber.Location = new System.Drawing.Point(229, 424);
			this.maskedTextBoxTillToneNumber.Mask = "####";
			this.maskedTextBoxTillToneNumber.Name = "maskedTextBoxTillToneNumber";
			this.maskedTextBoxTillToneNumber.PromptChar = ' ';
			this.maskedTextBoxTillToneNumber.Size = new System.Drawing.Size(100, 22);
			this.maskedTextBoxTillToneNumber.TabIndex = 47;
			this.maskedTextBoxTillToneNumber.ValidatingType = typeof(int);
			// 
			// labelTillToneNumber
			// 
			this.labelTillToneNumber.Location = new System.Drawing.Point(43, 423);
			this.labelTillToneNumber.Margin = new System.Windows.Forms.Padding(0);
			this.labelTillToneNumber.Name = "labelTillToneNumber";
			this.labelTillToneNumber.Size = new System.Drawing.Size(183, 22);
			this.labelTillToneNumber.TabIndex = 46;
			this.labelTillToneNumber.Text = "labelTillToneNumber";
			this.labelTillToneNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// MidiMappingElementPropertiesUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Controls.Add(this.maskedTextBoxTillToneNumber);
			this.Controls.Add(this.labelTillToneNumber);
			this.Controls.Add(this.maskedTextBoxFromToneNumber);
			this.Controls.Add(this.labelFromToneNumber);
			this.Controls.Add(this.labelScale);
			this.Controls.Add(this.comboBoxScale);
			this.Controls.Add(this.labelTillPosition);
			this.Controls.Add(this.textBoxTillPosition);
			this.Controls.Add(this.labelFromPosition);
			this.Controls.Add(this.textBoxFromPosition);
			this.Controls.Add(this.labelTillDimensionValue);
			this.Controls.Add(this.textBoxTillDimensionValue);
			this.Controls.Add(this.labelFromDimensionValue);
			this.Controls.Add(this.textBoxFromDimensionValue);
			this.Controls.Add(this.labelStandardDimension);
			this.Controls.Add(this.comboBoxStandardDimension);
			this.Controls.Add(this.labelCustomDimensionName);
			this.Controls.Add(this.textBoxCustomDimensionName);
			this.Controls.Add(this.maskedTextBoxTillNoteNumber);
			this.Controls.Add(this.labelTillNoteNumber);
			this.Controls.Add(this.maskedTextBoxFromNoteNumber);
			this.Controls.Add(this.labelFromNoteNumber);
			this.Controls.Add(this.maskedTextBoxTillControllerValue);
			this.Controls.Add(this.labelTillControllerValue);
			this.Controls.Add(this.maskedTextBoxFromControllerValue);
			this.Controls.Add(this.labelFromControllerValue);
			this.Controls.Add(this.maskedTextBoxControllerCode);
			this.Controls.Add(this.labelControllerCode);
			this.Controls.Add(this.checkBoxIsRelative);
			this.Controls.Add(this.labelIsRelative);
			this.Controls.Add(this.checkBoxIsActive);
			this.Controls.Add(this.labelIsActive);
			this.DeleteButtonVisible = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MidiMappingElementPropertiesUserControl";
			this.Size = new System.Drawing.Size(467, 518);
			this.TitleBarText = "";
			this.Controls.SetChildIndex(this.labelIsActive, 0);
			this.Controls.SetChildIndex(this.checkBoxIsActive, 0);
			this.Controls.SetChildIndex(this.labelIsRelative, 0);
			this.Controls.SetChildIndex(this.checkBoxIsRelative, 0);
			this.Controls.SetChildIndex(this.labelControllerCode, 0);
			this.Controls.SetChildIndex(this.maskedTextBoxControllerCode, 0);
			this.Controls.SetChildIndex(this.labelFromControllerValue, 0);
			this.Controls.SetChildIndex(this.maskedTextBoxFromControllerValue, 0);
			this.Controls.SetChildIndex(this.labelTillControllerValue, 0);
			this.Controls.SetChildIndex(this.maskedTextBoxTillControllerValue, 0);
			this.Controls.SetChildIndex(this.labelFromNoteNumber, 0);
			this.Controls.SetChildIndex(this.maskedTextBoxFromNoteNumber, 0);
			this.Controls.SetChildIndex(this.labelTillNoteNumber, 0);
			this.Controls.SetChildIndex(this.maskedTextBoxTillNoteNumber, 0);
			this.Controls.SetChildIndex(this.textBoxCustomDimensionName, 0);
			this.Controls.SetChildIndex(this.labelCustomDimensionName, 0);
			this.Controls.SetChildIndex(this.comboBoxStandardDimension, 0);
			this.Controls.SetChildIndex(this.labelStandardDimension, 0);
			this.Controls.SetChildIndex(this.textBoxFromDimensionValue, 0);
			this.Controls.SetChildIndex(this.labelFromDimensionValue, 0);
			this.Controls.SetChildIndex(this.textBoxTillDimensionValue, 0);
			this.Controls.SetChildIndex(this.labelTillDimensionValue, 0);
			this.Controls.SetChildIndex(this.textBoxFromPosition, 0);
			this.Controls.SetChildIndex(this.labelFromPosition, 0);
			this.Controls.SetChildIndex(this.textBoxTillPosition, 0);
			this.Controls.SetChildIndex(this.labelTillPosition, 0);
			this.Controls.SetChildIndex(this.comboBoxScale, 0);
			this.Controls.SetChildIndex(this.labelScale, 0);
			this.Controls.SetChildIndex(this.labelFromToneNumber, 0);
			this.Controls.SetChildIndex(this.maskedTextBoxFromToneNumber, 0);
			this.Controls.SetChildIndex(this.labelTillToneNumber, 0);
			this.Controls.SetChildIndex(this.maskedTextBoxTillToneNumber, 0);
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
		private System.Windows.Forms.MaskedTextBox maskedTextBoxFromControllerValue;
		private System.Windows.Forms.Label labelFromControllerValue;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxTillControllerValue;
		private System.Windows.Forms.Label labelTillControllerValue;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxFromNoteNumber;
		private System.Windows.Forms.Label labelFromNoteNumber;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxTillNoteNumber;
		private System.Windows.Forms.Label labelTillNoteNumber;
		private System.Windows.Forms.Label labelStandardDimension;
		private System.Windows.Forms.ComboBox comboBoxStandardDimension;
		private System.Windows.Forms.Label labelCustomDimensionName;
		private System.Windows.Forms.TextBox textBoxCustomDimensionName;
		private System.Windows.Forms.Label labelFromDimensionValue;
		private System.Windows.Forms.TextBox textBoxFromDimensionValue;
		private System.Windows.Forms.Label labelTillDimensionValue;
		private System.Windows.Forms.TextBox textBoxTillDimensionValue;
		private System.Windows.Forms.Label labelFromPosition;
		private System.Windows.Forms.TextBox textBoxFromPosition;
		private System.Windows.Forms.Label labelTillPosition;
		private System.Windows.Forms.TextBox textBoxTillPosition;
		private System.Windows.Forms.Label labelScale;
		private System.Windows.Forms.ComboBox comboBoxScale;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxFromToneNumber;
		private System.Windows.Forms.Label labelFromToneNumber;
		private System.Windows.Forms.MaskedTextBox maskedTextBoxTillToneNumber;
		private System.Windows.Forms.Label labelTillToneNumber;
	}
}
