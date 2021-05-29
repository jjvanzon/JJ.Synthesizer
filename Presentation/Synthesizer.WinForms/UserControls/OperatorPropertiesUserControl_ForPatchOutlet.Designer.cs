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
            this.labelPosition = new System.Windows.Forms.Label();
            this.numericUpDownPosition = new System.Windows.Forms.NumericUpDown();
            this.labelDimension = new System.Windows.Forms.Label();
            this.comboBoxDimension = new System.Windows.Forms.ComboBox();
            this.checkBoxNameOrDimensionHidden = new System.Windows.Forms.CheckBox();
            this.labelNameOrDimensionHidden = new System.Windows.Forms.Label();
            this.checkBoxIsRepeating = new System.Windows.Forms.CheckBox();
            this.labelIsRepeating = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownInletCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownOutletCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosition)).BeginInit();
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
            // _labelCustomDimensionName
            // 
            this._labelCustomDimensionName.TabIndex = 10;
            this._labelCustomDimensionName.Text = "Custom Dimension";
            // 
            // _textBoxCustomDimensionName
            // 
            this._textBoxCustomDimensionName.TabIndex = 11;
            // 
            // _labelInletCount
            // 
            this._labelInletCount.Text = "Number of Inlets";
            // 
            // _labelOutletCount
            // 
            this._labelOutletCount.Text = "Number of Outlets";
            // 
            // labelPosition
            // 
            this.labelPosition.Location = new System.Drawing.Point(0, 59);
            this.labelPosition.Margin = new System.Windows.Forms.Padding(0);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(147, 30);
            this.labelPosition.TabIndex = 14;
            this.labelPosition.Text = "labelPosition";
            this.labelPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownPosition
            // 
            this.numericUpDownPosition.Location = new System.Drawing.Point(147, 59);
            this.numericUpDownPosition.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownPosition.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownPosition.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.numericUpDownPosition.Name = "numericUpDownPosition";
            this.numericUpDownPosition.Size = new System.Drawing.Size(13, 22);
            this.numericUpDownPosition.TabIndex = 18;
            this.numericUpDownPosition.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelDimension
            // 
            this.labelDimension.Location = new System.Drawing.Point(76, 190);
            this.labelDimension.Margin = new System.Windows.Forms.Padding(0);
            this.labelDimension.Name = "labelDimension";
            this.labelDimension.Size = new System.Drawing.Size(147, 30);
            this.labelDimension.TabIndex = 40;
            this.labelDimension.Text = "labelDimension";
            this.labelDimension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxDimension
            // 
            this.comboBoxDimension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDimension.FormattingEnabled = true;
            this.comboBoxDimension.Location = new System.Drawing.Point(239, 200);
            this.comboBoxDimension.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxDimension.Name = "comboBoxDimension";
            this.comboBoxDimension.Size = new System.Drawing.Size(353, 24);
            this.comboBoxDimension.TabIndex = 39;
            // 
            // checkBoxNameOrDimensionHidden
            // 
            this.checkBoxNameOrDimensionHidden.AutoSize = true;
            this.checkBoxNameOrDimensionHidden.Location = new System.Drawing.Point(404, 233);
            this.checkBoxNameOrDimensionHidden.Name = "checkBoxNameOrDimensionHidden";
            this.checkBoxNameOrDimensionHidden.Size = new System.Drawing.Size(18, 17);
            this.checkBoxNameOrDimensionHidden.TabIndex = 44;
            this.checkBoxNameOrDimensionHidden.UseVisualStyleBackColor = true;
            // 
            // labelNameOrDimensionHidden
            // 
            this.labelNameOrDimensionHidden.Location = new System.Drawing.Point(246, 232);
            this.labelNameOrDimensionHidden.Margin = new System.Windows.Forms.Padding(0);
            this.labelNameOrDimensionHidden.Name = "labelNameOrDimensionHidden";
            this.labelNameOrDimensionHidden.Size = new System.Drawing.Size(147, 30);
            this.labelNameOrDimensionHidden.TabIndex = 43;
            this.labelNameOrDimensionHidden.Text = "labelNameOrDimensionHidden";
            this.labelNameOrDimensionHidden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBoxIsRepeating
            // 
            this.checkBoxIsRepeating.AutoSize = true;
            this.checkBoxIsRepeating.Location = new System.Drawing.Point(404, 267);
            this.checkBoxIsRepeating.Name = "checkBoxIsRepeating";
            this.checkBoxIsRepeating.Size = new System.Drawing.Size(18, 17);
            this.checkBoxIsRepeating.TabIndex = 46;
            this.checkBoxIsRepeating.UseVisualStyleBackColor = true;
            // 
            // labelIsRepeating
            // 
            this.labelIsRepeating.Location = new System.Drawing.Point(246, 259);
            this.labelIsRepeating.Margin = new System.Windows.Forms.Padding(0);
            this.labelIsRepeating.Name = "labelIsRepeating";
            this.labelIsRepeating.Size = new System.Drawing.Size(147, 30);
            this.labelIsRepeating.TabIndex = 45;
            this.labelIsRepeating.Text = "labelIsRepeating";
            this.labelIsRepeating.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OperatorPropertiesUserControl_ForPatchOutlet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.checkBoxIsRepeating);
            this.Controls.Add(this.labelIsRepeating);
            this.Controls.Add(this.checkBoxNameOrDimensionHidden);
            this.Controls.Add(this.labelNameOrDimensionHidden);
            this.Controls.Add(this.labelDimension);
            this.Controls.Add(this.comboBoxDimension);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.numericUpDownPosition);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_ForPatchOutlet";
            this.Size = new System.Drawing.Size(669, 414);
            this.TitleBarText = "Operator Properties";
            this.Controls.SetChildIndex(this._comboBoxUnderlyingPatch, 0);
            this.Controls.SetChildIndex(this._labelUnderlyingPatch, 0);
            this.Controls.SetChildIndex(this._textBoxName, 0);
            this.Controls.SetChildIndex(this._labelName, 0);
            this.Controls.SetChildIndex(this._textBoxCustomDimensionName, 0);
            this.Controls.SetChildIndex(this._labelStandardDimension, 0);
            this.Controls.SetChildIndex(this._comboBoxStandardDimension, 0);
            this.Controls.SetChildIndex(this._labelCustomDimensionName, 0);
            this.Controls.SetChildIndex(this.numericUpDownPosition, 0);
            this.Controls.SetChildIndex(this.labelPosition, 0);
            this.Controls.SetChildIndex(this.comboBoxDimension, 0);
            this.Controls.SetChildIndex(this.labelDimension, 0);
            this.Controls.SetChildIndex(this.labelNameOrDimensionHidden, 0);
            this.Controls.SetChildIndex(this.checkBoxNameOrDimensionHidden, 0);
            this.Controls.SetChildIndex(this.labelIsRepeating, 0);
            this.Controls.SetChildIndex(this.checkBoxIsRepeating, 0);
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownInletCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numericUpDownOutletCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.NumericUpDown numericUpDownPosition;
        protected System.Windows.Forms.Label labelDimension;
        protected System.Windows.Forms.ComboBox comboBoxDimension;
        private System.Windows.Forms.CheckBox checkBoxNameOrDimensionHidden;
        protected System.Windows.Forms.Label labelNameOrDimensionHidden;
        private System.Windows.Forms.CheckBox checkBoxIsRepeating;
        protected System.Windows.Forms.Label labelIsRepeating;
    }
}
