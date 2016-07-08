using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_WithInletCount
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInletCount)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(147, 30);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(10, 22);
            this.textBoxName.TabIndex = 11;
            // 
            // labelOperatorTypeTitle
            // 
            this.labelOperatorTypeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.labelOperatorTypeValue.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.labelInletCount.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.numericUpDownInletCount.Dock = System.Windows.Forms.DockStyle.Fill;
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
            // OperatorPropertiesUserControl_WithInletCount
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
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_WithInletCount";
            this.Size = new System.Drawing.Size(10, 10);
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
    }
}
