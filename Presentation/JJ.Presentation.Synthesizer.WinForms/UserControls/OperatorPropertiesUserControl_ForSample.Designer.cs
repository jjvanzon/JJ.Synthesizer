using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_ForSample
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
            this.labelSample = new System.Windows.Forms.Label();
            this.comboBoxSample = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelSample
            // 
            this.labelSample.Location = new System.Drawing.Point(0, 60);
            this.labelSample.Margin = new System.Windows.Forms.Padding(0);
            this.labelSample.Name = "labelSample";
            this.labelSample.Size = new System.Drawing.Size(160, 30);
            this.labelSample.TabIndex = 15;
            this.labelSample.Text = "labelSample";
            this.labelSample.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSample
            // 
            this.comboBoxSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSample.FormattingEnabled = true;
            this.comboBoxSample.Location = new System.Drawing.Point(160, 60);
            this.comboBoxSample.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxSample.Name = "comboBoxSample";
            this.comboBoxSample.Size = new System.Drawing.Size(10, 24);
            this.comboBoxSample.TabIndex = 16;
            // 
            // OperatorPropertiesUserControl_ForSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelSample);
            this.Controls.Add(this.comboBoxSample);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_ForSample";
            this.Size = new System.Drawing.Size(10, 10);
            this.Controls.SetChildIndex(this.comboBoxSample, 0);
            this.Controls.SetChildIndex(this.labelSample, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelSample;
        private System.Windows.Forms.ComboBox comboBoxSample;
    }
}
