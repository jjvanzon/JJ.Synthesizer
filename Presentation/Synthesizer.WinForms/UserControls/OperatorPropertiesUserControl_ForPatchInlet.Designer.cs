using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_ForPatchInlet
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
            this.numericUpDownNumber = new System.Windows.Forms.NumericUpDown();
            this.labelDefaultValue = new System.Windows.Forms.Label();
            this.labelNumber = new System.Windows.Forms.Label();
            this.textBoxDefaultValue = new System.Windows.Forms.TextBox();
            this.comboBoxDimension = new System.Windows.Forms.ComboBox();
            this.labelDimension = new System.Windows.Forms.Label();
            this.labelWarnIfEmpty = new System.Windows.Forms.Label();
            this.checkBoxWarnIfEmpty = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // _labelName
            // 
            this._labelName.TabIndex = 6;
            this._labelName.Text = "Name";
            // 
            // _textBoxName
            // 
            this._textBoxName.TabIndex = 7;
            // 
            // _labelOperatorTypeTitle
            // 
            this._labelOperatorTypeTitle.TabIndex = 4;
            this._labelOperatorTypeTitle.Text = "Type:";
            // 
            // _labelOperatorTypeValue
            // 
            this._labelOperatorTypeValue.TabIndex = 5;
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
            this._labelStandardDimension.TabIndex = 8;
            this._labelStandardDimension.Text = "Standard Dimension";
            // 
            // _comboBoxStandardDimension
            // 
            this._comboBoxStandardDimension.Size = new System.Drawing.Size(121, 24);
            this._comboBoxStandardDimension.TabIndex = 9;
            // 
            // _textBoxCustomDimensionName
            // 
            this._textBoxCustomDimensionName.TabIndex = 11;
            // 
            // _labelCustomDimensionName
            // 
            this._labelCustomDimensionName.Location = new System.Drawing.Point(0, 83);
            this._labelCustomDimensionName.Size = new System.Drawing.Size(159, 23);
            this._labelCustomDimensionName.TabIndex = 10;
            this._labelCustomDimensionName.Text = "Custom Dimension";
            // 
            // numericUpDownNumber
            // 
            this.numericUpDownNumber.Location = new System.Drawing.Point(252, 180);
            this.numericUpDownNumber.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownNumber.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownNumber.Name = "numericUpDownNumber";
            this.numericUpDownNumber.Size = new System.Drawing.Size(143, 22);
            this.numericUpDownNumber.TabIndex = 18;
            this.numericUpDownNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelDefaultValue
            // 
            this.labelDefaultValue.Location = new System.Drawing.Point(50, 197);
            this.labelDefaultValue.Margin = new System.Windows.Forms.Padding(0);
            this.labelDefaultValue.Name = "labelDefaultValue";
            this.labelDefaultValue.Size = new System.Drawing.Size(147, 30);
            this.labelDefaultValue.TabIndex = 19;
            this.labelDefaultValue.Text = "labelDefaultValue";
            this.labelDefaultValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelNumber
            // 
            this.labelNumber.Location = new System.Drawing.Point(56, 172);
            this.labelNumber.Margin = new System.Windows.Forms.Padding(0);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(147, 30);
            this.labelNumber.TabIndex = 14;
            this.labelNumber.Text = "labelNumber";
            this.labelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxDefaultValue
            // 
            this.textBoxDefaultValue.Location = new System.Drawing.Point(254, 208);
            this.textBoxDefaultValue.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxDefaultValue.Name = "textBoxDefaultValue";
            this.textBoxDefaultValue.Size = new System.Drawing.Size(143, 22);
            this.textBoxDefaultValue.TabIndex = 22;
            // 
            // comboBoxDimension
            // 
            this.comboBoxDimension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDimension.FormattingEnabled = true;
            this.comboBoxDimension.Location = new System.Drawing.Point(239, 245);
            this.comboBoxDimension.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxDimension.Name = "comboBoxDimension";
            this.comboBoxDimension.Size = new System.Drawing.Size(353, 24);
            this.comboBoxDimension.TabIndex = 37;
            // 
            // labelDimension
            // 
            this.labelDimension.Location = new System.Drawing.Point(76, 235);
            this.labelDimension.Margin = new System.Windows.Forms.Padding(0);
            this.labelDimension.Name = "labelDimension";
            this.labelDimension.Size = new System.Drawing.Size(147, 30);
            this.labelDimension.TabIndex = 38;
            this.labelDimension.Text = "labelDimension";
            this.labelDimension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelWarnIfEmpty
            // 
            this.labelWarnIfEmpty.Location = new System.Drawing.Point(91, 275);
            this.labelWarnIfEmpty.Margin = new System.Windows.Forms.Padding(0);
            this.labelWarnIfEmpty.Name = "labelWarnIfEmpty";
            this.labelWarnIfEmpty.Size = new System.Drawing.Size(147, 30);
            this.labelWarnIfEmpty.TabIndex = 39;
            this.labelWarnIfEmpty.Text = "labelWarnIfEmpty";
            this.labelWarnIfEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxWarnIfEmpty
            // 
            this.checkBoxWarnIfEmpty.AutoSize = true;
            this.checkBoxWarnIfEmpty.Location = new System.Drawing.Point(249, 283);
            this.checkBoxWarnIfEmpty.Name = "checkBoxWarnIfEmpty";
            this.checkBoxWarnIfEmpty.Size = new System.Drawing.Size(18, 17);
            this.checkBoxWarnIfEmpty.TabIndex = 40;
            this.checkBoxWarnIfEmpty.UseVisualStyleBackColor = true;
            // 
            // OperatorPropertiesUserControl_ForPatchInlet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.checkBoxWarnIfEmpty);
            this.Controls.Add(this.labelWarnIfEmpty);
            this.Controls.Add(this.labelDimension);
            this.Controls.Add(this.comboBoxDimension);
            this.Controls.Add(this.numericUpDownNumber);
            this.Controls.Add(this.labelDefaultValue);
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.textBoxDefaultValue);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_ForPatchInlet";
            this.RemoveButtonVisible = true;
            this.Size = new System.Drawing.Size(688, 401);
            this.TitleBarText = "Operator Properties";
            this.Controls.SetChildIndex(this._comboBoxUnderlyingPatch, 0);
            this.Controls.SetChildIndex(this._labelUnderlyingPatch, 0);
            this.Controls.SetChildIndex(this._textBoxName, 0);
            this.Controls.SetChildIndex(this._labelOperatorTypeValue, 0);
            this.Controls.SetChildIndex(this._labelOperatorTypeTitle, 0);
            this.Controls.SetChildIndex(this._labelName, 0);
            this.Controls.SetChildIndex(this._textBoxCustomDimensionName, 0);
            this.Controls.SetChildIndex(this._labelStandardDimension, 0);
            this.Controls.SetChildIndex(this._comboBoxStandardDimension, 0);
            this.Controls.SetChildIndex(this._labelCustomDimensionName, 0);
            this.Controls.SetChildIndex(this.textBoxDefaultValue, 0);
            this.Controls.SetChildIndex(this.labelNumber, 0);
            this.Controls.SetChildIndex(this.labelDefaultValue, 0);
            this.Controls.SetChildIndex(this.numericUpDownNumber, 0);
            this.Controls.SetChildIndex(this.comboBoxDimension, 0);
            this.Controls.SetChildIndex(this.labelDimension, 0);
            this.Controls.SetChildIndex(this.labelWarnIfEmpty, 0);
            this.Controls.SetChildIndex(this.checkBoxWarnIfEmpty, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.NumericUpDown numericUpDownNumber;
        private System.Windows.Forms.Label labelDefaultValue;
        private System.Windows.Forms.TextBox textBoxDefaultValue;
        protected System.Windows.Forms.ComboBox comboBoxDimension;
        protected System.Windows.Forms.Label labelDimension;
        protected System.Windows.Forms.Label labelWarnIfEmpty;
        private System.Windows.Forms.CheckBox checkBoxWarnIfEmpty;
    }
}
