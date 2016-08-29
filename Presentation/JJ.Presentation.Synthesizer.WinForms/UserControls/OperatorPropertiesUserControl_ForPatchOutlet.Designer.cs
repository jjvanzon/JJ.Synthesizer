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
            this.labelNumber = new System.Windows.Forms.Label();
            this.numericUpDownNumber = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumber)).BeginInit();
            this.SuspendLayout();
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
            // OperatorPropertiesUserControl_ForPatchOutlet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.numericUpDownNumber);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_ForPatchOutlet";
            this.Size = new System.Drawing.Size(13, 12);
            this.Controls.SetChildIndex(this.numericUpDownNumber, 0);
            this.Controls.SetChildIndex(this.labelNumber, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.NumericUpDown numericUpDownNumber;
    }
}
