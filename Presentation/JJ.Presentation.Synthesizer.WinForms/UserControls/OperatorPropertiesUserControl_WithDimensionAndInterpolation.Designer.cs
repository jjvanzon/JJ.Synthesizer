using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_WithDimensionAndInterpolation
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
            this.labelInterpolation = new System.Windows.Forms.Label();
            this.comboBoxInterpolation = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelInterpolation
            // 
            this.labelInterpolation.Location = new System.Drawing.Point(0, 60);
            this.labelInterpolation.Margin = new System.Windows.Forms.Padding(0);
            this.labelInterpolation.Name = "labelInterpolation";
            this.labelInterpolation.Size = new System.Drawing.Size(147, 30);
            this.labelInterpolation.TabIndex = 20;
            this.labelInterpolation.Text = "labelInterpolation";
            this.labelInterpolation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxInterpolation
            // 
            this.comboBoxInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInterpolation.FormattingEnabled = true;
            this.comboBoxInterpolation.Location = new System.Drawing.Point(147, 60);
            this.comboBoxInterpolation.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxInterpolation.Name = "comboBoxInterpolation";
            this.comboBoxInterpolation.Size = new System.Drawing.Size(10, 24);
            this.comboBoxInterpolation.TabIndex = 21;
            // 
            // OperatorPropertiesUserControl_WithDimensionAndInterpolation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelInterpolation);
            this.Controls.Add(this.comboBoxInterpolation);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_WithDimensionAndInterpolation";
            this.Size = new System.Drawing.Size(10, 10);
            this.Controls.SetChildIndex(this.comboBoxInterpolation, 0);
            this.Controls.SetChildIndex(this.labelInterpolation, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelInterpolation;
        private System.Windows.Forms.ComboBox comboBoxInterpolation;
    }
}
