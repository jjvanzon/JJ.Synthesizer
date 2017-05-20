using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_ForCurve
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
            this.labelCurve = new System.Windows.Forms.Label();
            this.comboBoxCurve = new System.Windows.Forms.ComboBox();
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
            // labelCurve
            // 
            this.labelCurve.Location = new System.Drawing.Point(0, 0);
            this.labelCurve.Margin = new System.Windows.Forms.Padding(0);
            this.labelCurve.Name = "labelCurve";
            this.labelCurve.Size = new System.Drawing.Size(10, 10);
            this.labelCurve.TabIndex = 15;
            this.labelCurve.Text = "labelCurve";
            this.labelCurve.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCurve
            // 
            this.comboBoxCurve.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCurve.FormattingEnabled = true;
            this.comboBoxCurve.Location = new System.Drawing.Point(0, 0);
            this.comboBoxCurve.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxCurve.Name = "comboBoxCurve";
            this.comboBoxCurve.Size = new System.Drawing.Size(421, 24);
            this.comboBoxCurve.TabIndex = 16;
            // 
            // OperatorPropertiesUserControl_ForCurve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelCurve);
            this.Controls.Add(this.comboBoxCurve);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_ForCurve";
            this.RemoveButtonVisible = true;
            this.TitleBarText = "Operator Properties";
            this.Controls.SetChildIndex(this._textBoxName, 0);
            this.Controls.SetChildIndex(this._labelName, 0);
            this.Controls.SetChildIndex(this._textBoxCustomDimensionName, 0);
            this.Controls.SetChildIndex(this._labelCustomDimensionName, 0);
            this.Controls.SetChildIndex(this._comboBoxStandardDimension, 0);
            this.Controls.SetChildIndex(this._labelStandardDimension, 0);
            this.Controls.SetChildIndex(this._labelOperatorTypeValue, 0);
            this.Controls.SetChildIndex(this._labelOperatorTypeTitle, 0);
            this.Controls.SetChildIndex(this.comboBoxCurve, 0);
            this.Controls.SetChildIndex(this.labelCurve, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelCurve;
        private System.Windows.Forms.ComboBox comboBoxCurve;
    }
}
