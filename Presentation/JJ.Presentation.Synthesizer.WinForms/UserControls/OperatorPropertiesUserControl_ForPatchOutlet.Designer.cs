using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_ForPatchOutlet
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
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelOperatorTypeTitle = new System.Windows.Forms.Label();
            this.labelOperatorTypeValue = new System.Windows.Forms.Label();
            this.labelNumber = new System.Windows.Forms.Label();
            this.numericUpDownNumber = new System.Windows.Forms.NumericUpDown();
            this.comboBoxDimension = new System.Windows.Forms.ComboBox();
            this.labelDimension = new System.Windows.Forms.Label();
            this.textBoxCustomDimensionName = new System.Windows.Forms.TextBox();
            this.labelCustomDimensionName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(0, 30);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(147, 30);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(147, 30);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(12, 22);
            this.textBoxName.TabIndex = 11;
            // 
            // labelOperatorTypeTitle
            // 
            this.labelOperatorTypeTitle.Location = new System.Drawing.Point(0, 0);
            this.labelOperatorTypeTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelOperatorTypeTitle.Name = "labelOperatorTypeTitle";
            this.labelOperatorTypeTitle.Size = new System.Drawing.Size(147, 30);
            this.labelOperatorTypeTitle.TabIndex = 12;
            this.labelOperatorTypeTitle.Text = "labelOperatorTypeTitle";
            this.labelOperatorTypeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelOperatorTypeValue
            // 
            this.labelOperatorTypeValue.Location = new System.Drawing.Point(147, 0);
            this.labelOperatorTypeValue.Margin = new System.Windows.Forms.Padding(0);
            this.labelOperatorTypeValue.Name = "labelOperatorTypeValue";
            this.labelOperatorTypeValue.Size = new System.Drawing.Size(13, 30);
            this.labelOperatorTypeValue.TabIndex = 13;
            this.labelOperatorTypeValue.Text = "labelOperatorTypeValue";
            this.labelOperatorTypeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelNumber
            // 
            this.labelNumber.Location = new System.Drawing.Point(0, 59);
            this.labelNumber.Margin = new System.Windows.Forms.Padding(0);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(147, 30);
            this.labelNumber.TabIndex = 14;
            this.labelNumber.Text = "labelNumber";
            this.labelNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownNumber
            // 
            this.numericUpDownNumber.Location = new System.Drawing.Point(147, 59);
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
            this.numericUpDownNumber.Size = new System.Drawing.Size(13, 22);
            this.numericUpDownNumber.TabIndex = 18;
            this.numericUpDownNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // comboBoxDimension
            // 
            this.comboBoxDimension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDimension.FormattingEnabled = true;
            this.comboBoxDimension.Location = new System.Drawing.Point(147, 89);
            this.comboBoxDimension.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxDimension.Name = "comboBoxDimension";
            this.comboBoxDimension.Size = new System.Drawing.Size(12, 24);
            this.comboBoxDimension.TabIndex = 22;
            // 
            // labelDimension
            // 
            this.labelDimension.Location = new System.Drawing.Point(0, 89);
            this.labelDimension.Margin = new System.Windows.Forms.Padding(0);
            this.labelDimension.Name = "labelDimension";
            this.labelDimension.Size = new System.Drawing.Size(147, 30);
            this.labelDimension.TabIndex = 23;
            this.labelDimension.Text = "labelDimension";
            this.labelDimension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCustomDimensionName
            // 
            this.textBoxCustomDimensionName.Location = new System.Drawing.Point(52, -8);
            this.textBoxCustomDimensionName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxCustomDimensionName.Name = "textBoxCustomDimensionName";
            this.textBoxCustomDimensionName.Size = new System.Drawing.Size(71, 22);
            this.textBoxCustomDimensionName.TabIndex = 33;
            // 
            // labelCustomDimensionName
            // 
            this.labelCustomDimensionName.Location = new System.Drawing.Point(-111, -7);
            this.labelCustomDimensionName.Margin = new System.Windows.Forms.Padding(0);
            this.labelCustomDimensionName.Name = "labelCustomDimensionName";
            this.labelCustomDimensionName.Size = new System.Drawing.Size(113, 28);
            this.labelCustomDimensionName.TabIndex = 32;
            this.labelCustomDimensionName.Text = "labelCustomDimensionName";
            this.labelCustomDimensionName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OperatorPropertiesUserControl_ForPatchOutlet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.textBoxCustomDimensionName);
            this.Controls.Add(this.labelCustomDimensionName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelOperatorTypeTitle);
            this.Controls.Add(this.labelOperatorTypeValue);
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.numericUpDownNumber);
            this.Controls.Add(this.comboBoxDimension);
            this.Controls.Add(this.labelDimension);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OperatorPropertiesUserControl_ForPatchOutlet";
            this.Size = new System.Drawing.Size(13, 12);
            this.Controls.SetChildIndex(this.labelDimension, 0);
            this.Controls.SetChildIndex(this.comboBoxDimension, 0);
            this.Controls.SetChildIndex(this.numericUpDownNumber, 0);
            this.Controls.SetChildIndex(this.labelNumber, 0);
            this.Controls.SetChildIndex(this.labelOperatorTypeValue, 0);
            this.Controls.SetChildIndex(this.labelOperatorTypeTitle, 0);
            this.Controls.SetChildIndex(this.textBoxName, 0);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.labelCustomDimensionName, 0);
            this.Controls.SetChildIndex(this.textBoxCustomDimensionName, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelOperatorTypeTitle;
        private System.Windows.Forms.Label labelOperatorTypeValue;
        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.NumericUpDown numericUpDownNumber;
        private System.Windows.Forms.ComboBox comboBoxDimension;
        private System.Windows.Forms.Label labelDimension;
        private System.Windows.Forms.TextBox textBoxCustomDimensionName;
        private System.Windows.Forms.Label labelCustomDimensionName;
    }
}
