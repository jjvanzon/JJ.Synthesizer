using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_ForBundle
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
            this.labelDimension = new System.Windows.Forms.Label();
            this.comboBoxDimension = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInletCount)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(45, 73);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(177, 31);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(232, 73);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(92, 22);
            this.textBoxName.TabIndex = 11;
            // 
            // labelOperatorTypeTitle
            // 
            this.labelOperatorTypeTitle.Location = new System.Drawing.Point(64, 42);
            this.labelOperatorTypeTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelOperatorTypeTitle.Name = "labelOperatorTypeTitle";
            this.labelOperatorTypeTitle.Size = new System.Drawing.Size(165, 31);
            this.labelOperatorTypeTitle.TabIndex = 12;
            this.labelOperatorTypeTitle.Text = "labelOperatorTypeTitle";
            this.labelOperatorTypeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelOperatorTypeValue
            // 
            this.labelOperatorTypeValue.Location = new System.Drawing.Point(229, 42);
            this.labelOperatorTypeValue.Margin = new System.Windows.Forms.Padding(0);
            this.labelOperatorTypeValue.Name = "labelOperatorTypeValue";
            this.labelOperatorTypeValue.Size = new System.Drawing.Size(169, 31);
            this.labelOperatorTypeValue.TabIndex = 13;
            this.labelOperatorTypeValue.Text = "labelOperatorTypeValue";
            this.labelOperatorTypeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelInletCount
            // 
            this.labelInletCount.Location = new System.Drawing.Point(67, 120);
            this.labelInletCount.Margin = new System.Windows.Forms.Padding(0);
            this.labelInletCount.Name = "labelInletCount";
            this.labelInletCount.Size = new System.Drawing.Size(134, 31);
            this.labelInletCount.TabIndex = 14;
            this.labelInletCount.Text = "labelInletCount";
            this.labelInletCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownInletCount
            // 
            this.numericUpDownInletCount.Location = new System.Drawing.Point(232, 120);
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
            this.numericUpDownInletCount.Size = new System.Drawing.Size(92, 22);
            this.numericUpDownInletCount.TabIndex = 15;
            this.numericUpDownInletCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelDimension
            // 
            this.labelDimension.Location = new System.Drawing.Point(109, 151);
            this.labelDimension.Margin = new System.Windows.Forms.Padding(0);
            this.labelDimension.Name = "labelDimension";
            this.labelDimension.Size = new System.Drawing.Size(92, 31);
            this.labelDimension.TabIndex = 22;
            this.labelDimension.Text = "labelDimension";
            this.labelDimension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxDimension
            // 
            this.comboBoxDimension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDimension.FormattingEnabled = true;
            this.comboBoxDimension.Location = new System.Drawing.Point(232, 155);
            this.comboBoxDimension.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxDimension.Name = "comboBoxDimension";
            this.comboBoxDimension.Size = new System.Drawing.Size(92, 24);
            this.comboBoxDimension.TabIndex = 23;
            // 
            // OperatorPropertiesUserControl_ForBundle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelOperatorTypeTitle);
            this.Controls.Add(this.labelOperatorTypeValue);
            this.Controls.Add(this.labelInletCount);
            this.Controls.Add(this.numericUpDownInletCount);
            this.Controls.Add(this.labelDimension);
            this.Controls.Add(this.comboBoxDimension);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OperatorPropertiesUserControl_ForBundle";
            this.Size = new System.Drawing.Size(551, 378);
            this.Controls.SetChildIndex(this.comboBoxDimension, 0);
            this.Controls.SetChildIndex(this.labelDimension, 0);
            this.Controls.SetChildIndex(this.numericUpDownInletCount, 0);
            this.Controls.SetChildIndex(this.labelInletCount, 0);
            this.Controls.SetChildIndex(this.labelOperatorTypeValue, 0);
            this.Controls.SetChildIndex(this.labelOperatorTypeTitle, 0);
            this.Controls.SetChildIndex(this.textBoxName, 0);
            this.Controls.SetChildIndex(this.labelName, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInletCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}
