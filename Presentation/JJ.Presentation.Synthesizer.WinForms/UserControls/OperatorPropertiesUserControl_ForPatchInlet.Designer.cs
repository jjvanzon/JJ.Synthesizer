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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.TabIndex = 6;
            this.labelName.Text = "Name";
            // 
            // labelOperatorTypeTitle
            // 
            this.labelOperatorTypeTitle.TabIndex = 4;
            this.labelOperatorTypeTitle.Text = "Type:";
            // 
            // labelOperatorTypeValue
            // 
            this.labelOperatorTypeValue.TabIndex = 5;
            // 
            // textBoxCustomDimensionName
            // 
            this.textBoxCustomDimensionName.TabIndex = 11;
            // 
            // labelStandardDimension
            // 
            this.labelStandardDimension.TabIndex = 8;
            this.labelStandardDimension.Text = "Standard Dimension";
            // 
            // comboBoxStandardDimension
            // 
            this.comboBoxStandardDimension.TabIndex = 9;
            // 
            // labelCustomDimensionName
            // 
            this.labelCustomDimensionName.TabIndex = 10;
            this.labelCustomDimensionName.Text = "Custom Dimension";
            // 
            // textBoxName
            // 
            this.textBoxName.TabIndex = 7;
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
            // OperatorPropertiesUserControl_ForPatchInlet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelDimension);
            this.Controls.Add(this.comboBoxDimension);
            this.Controls.Add(this.numericUpDownNumber);
            this.Controls.Add(this.labelDefaultValue);
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.textBoxDefaultValue);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_ForPatchInlet";
            this.Size = new System.Drawing.Size(688, 401);
            this.TitleBarText = "Operator Properties";
            this.Controls.SetChildIndex(this.textBoxName, 0);
            this.Controls.SetChildIndex(this.labelOperatorTypeValue, 0);
            this.Controls.SetChildIndex(this.labelOperatorTypeTitle, 0);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.textBoxCustomDimensionName, 0);
            this.Controls.SetChildIndex(this.labelStandardDimension, 0);
            this.Controls.SetChildIndex(this.comboBoxStandardDimension, 0);
            this.Controls.SetChildIndex(this.labelCustomDimensionName, 0);
            this.Controls.SetChildIndex(this.textBoxDefaultValue, 0);
            this.Controls.SetChildIndex(this.labelNumber, 0);
            this.Controls.SetChildIndex(this.labelDefaultValue, 0);
            this.Controls.SetChildIndex(this.numericUpDownNumber, 0);
            this.Controls.SetChildIndex(this.comboBoxDimension, 0);
            this.Controls.SetChildIndex(this.labelDimension, 0);
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
    }
}
