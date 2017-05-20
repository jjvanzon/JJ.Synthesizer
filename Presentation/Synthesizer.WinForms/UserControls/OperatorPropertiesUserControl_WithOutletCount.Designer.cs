using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_WithOutletCount
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
            this.labelOutletCount = new System.Windows.Forms.Label();
            this.numericUpDownOutletCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOutletCount)).BeginInit();
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
            // _labelOperatorTypeTitle
            // 
            this._labelOperatorTypeTitle.TabIndex = 3;
            this._labelOperatorTypeTitle.Text = "Type:";
            // 
            // _labelOperatorTypeValue
            // 
            this._labelOperatorTypeValue.TabIndex = 4;
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
            // labelOutletCount
            // 
            this.labelOutletCount.Location = new System.Drawing.Point(0, 60);
            this.labelOutletCount.Margin = new System.Windows.Forms.Padding(0);
            this.labelOutletCount.Name = "labelOutletCount";
            this.labelOutletCount.Size = new System.Drawing.Size(147, 30);
            this.labelOutletCount.TabIndex = 14;
            this.labelOutletCount.Text = "labelOutletCount";
            this.labelOutletCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownOutletCount
            // 
            this.numericUpDownOutletCount.Location = new System.Drawing.Point(147, 60);
            this.numericUpDownOutletCount.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownOutletCount.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericUpDownOutletCount.Name = "numericUpDownOutletCount";
            this.numericUpDownOutletCount.Size = new System.Drawing.Size(10, 22);
            this.numericUpDownOutletCount.TabIndex = 15;
            this.numericUpDownOutletCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // OperatorPropertiesUserControl_WithOutletCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelOutletCount);
            this.Controls.Add(this.numericUpDownOutletCount);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_WithOutletCount";
            this.RemoveButtonVisible = true;
            this.Size = new System.Drawing.Size(10, 10);
            this.TitleBarText = "Operator Properties";
            this.Controls.SetChildIndex(this._textBoxName, 0);
            this.Controls.SetChildIndex(this._labelName, 0);
            this.Controls.SetChildIndex(this._textBoxCustomDimensionName, 0);
            this.Controls.SetChildIndex(this._labelCustomDimensionName, 0);
            this.Controls.SetChildIndex(this._comboBoxStandardDimension, 0);
            this.Controls.SetChildIndex(this._labelStandardDimension, 0);
            this.Controls.SetChildIndex(this._labelOperatorTypeValue, 0);
            this.Controls.SetChildIndex(this._labelOperatorTypeTitle, 0);
            this.Controls.SetChildIndex(this.numericUpDownOutletCount, 0);
            this.Controls.SetChildIndex(this.labelOutletCount, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOutletCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelOutletCount;
        private System.Windows.Forms.NumericUpDown numericUpDownOutletCount;
    }
}
