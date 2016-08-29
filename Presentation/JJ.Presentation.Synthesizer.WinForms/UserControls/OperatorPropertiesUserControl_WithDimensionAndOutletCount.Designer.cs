using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_WithDimensionAndOutletCount
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
            // OperatorPropertiesUserControl_WithDimensionAndOutletCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelOutletCount);
            this.Controls.Add(this.numericUpDownOutletCount);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_WithDimensionAndOutletCount";
            this.Size = new System.Drawing.Size(10, 10);
            this.Controls.SetChildIndex(this.numericUpDownOutletCount, 0);
            this.Controls.SetChildIndex(this.labelOutletCount, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOutletCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelOutletCount;
        private System.Windows.Forms.NumericUpDown numericUpDownOutletCount;
    }
}
