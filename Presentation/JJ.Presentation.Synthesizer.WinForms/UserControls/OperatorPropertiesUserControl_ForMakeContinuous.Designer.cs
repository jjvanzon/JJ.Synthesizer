using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_ForMakeContinuous
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
            this.labelInletCount = new System.Windows.Forms.Label();
            this.numericUpDownInletCount = new System.Windows.Forms.NumericUpDown();
            this.comboBoxDimension = new System.Windows.Forms.ComboBox();
            this.labelDimension = new System.Windows.Forms.Label();
            this.labelInterpolation = new System.Windows.Forms.Label();
            this.comboBoxInterpolation = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInletCount)).BeginInit();
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
            this.textBoxName.Size = new System.Drawing.Size(10, 22);
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
            this.labelOperatorTypeValue.Size = new System.Drawing.Size(10, 30);
            this.labelOperatorTypeValue.TabIndex = 13;
            this.labelOperatorTypeValue.Text = "labelOperatorTypeValue";
            this.labelOperatorTypeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelInletCount
            // 
            this.labelInletCount.Location = new System.Drawing.Point(0, 60);
            this.labelInletCount.Margin = new System.Windows.Forms.Padding(0);
            this.labelInletCount.Name = "labelInletCount";
            this.labelInletCount.Size = new System.Drawing.Size(147, 30);
            this.labelInletCount.TabIndex = 14;
            this.labelInletCount.Text = "labelInletCount";
            this.labelInletCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownInletCount
            // 
            this.numericUpDownInletCount.Location = new System.Drawing.Point(147, 60);
            this.numericUpDownInletCount.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownInletCount.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericUpDownInletCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownInletCount.Name = "numericUpDownInletCount";
            this.numericUpDownInletCount.Size = new System.Drawing.Size(10, 22);
            this.numericUpDownInletCount.TabIndex = 15;
            this.numericUpDownInletCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // comboBoxDimension
            // 
            this.comboBoxDimension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDimension.FormattingEnabled = true;
            this.comboBoxDimension.Location = new System.Drawing.Point(147, 120);
            this.comboBoxDimension.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxDimension.Name = "comboBoxDimension";
            this.comboBoxDimension.Size = new System.Drawing.Size(10, 24);
            this.comboBoxDimension.TabIndex = 23;
            // 
            // labelDimension
            // 
            this.labelDimension.Location = new System.Drawing.Point(0, 120);
            this.labelDimension.Margin = new System.Windows.Forms.Padding(0);
            this.labelDimension.Name = "labelDimension";
            this.labelDimension.Size = new System.Drawing.Size(147, 30);
            this.labelDimension.TabIndex = 22;
            this.labelDimension.Text = "labelDimension";
            this.labelDimension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // OperatorPropertiesUserControl_ForMakeContinuous
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelOperatorTypeTitle);
            this.Controls.Add(this.labelOperatorTypeValue);
            this.Controls.Add(this.labelInletCount);
            this.Controls.Add(this.numericUpDownInletCount);
            this.Controls.Add(this.comboBoxDimension);
            this.Controls.Add(this.labelDimension);
            this.Controls.Add(this.labelInterpolation);
            this.Controls.Add(this.comboBoxInterpolation);
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OperatorPropertiesUserControl_ForMakeContinuous";
            this.Size = new System.Drawing.Size(234, 185);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInletCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelOperatorTypeTitle;
        private System.Windows.Forms.Label labelOperatorTypeValue;
        private System.Windows.Forms.Label labelInletCount;
        private System.Windows.Forms.NumericUpDown numericUpDownInletCount;
        private System.Windows.Forms.Label labelDimension;
        private System.Windows.Forms.ComboBox comboBoxDimension;
        private System.Windows.Forms.Label labelInterpolation;
        private System.Windows.Forms.ComboBox comboBoxInterpolation;
    }
}
