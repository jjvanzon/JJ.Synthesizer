using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_ForCustomOperator
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
            this.labelUnderlyingPatch = new System.Windows.Forms.Label();
            this.comboBoxUnderlyingPatch = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelUnderlyingPatch
            // 
            this.labelUnderlyingPatch.Location = new System.Drawing.Point(0, 59);
            this.labelUnderlyingPatch.Margin = new System.Windows.Forms.Padding(0);
            this.labelUnderlyingPatch.Name = "labelUnderlyingPatch";
            this.labelUnderlyingPatch.Size = new System.Drawing.Size(160, 30);
            this.labelUnderlyingPatch.TabIndex = 15;
            this.labelUnderlyingPatch.Text = "labelUnderlyingPatch";
            this.labelUnderlyingPatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxUnderlyingPatch
            // 
            this.comboBoxUnderlyingPatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnderlyingPatch.FormattingEnabled = true;
            this.comboBoxUnderlyingPatch.Location = new System.Drawing.Point(160, 59);
            this.comboBoxUnderlyingPatch.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxUnderlyingPatch.Name = "comboBoxUnderlyingPatch";
            this.comboBoxUnderlyingPatch.Size = new System.Drawing.Size(12, 24);
            this.comboBoxUnderlyingPatch.TabIndex = 16;
            // 
            // OperatorPropertiesUserControl_ForCustomOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelUnderlyingPatch);
            this.Controls.Add(this.comboBoxUnderlyingPatch);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OperatorPropertiesUserControl_ForCustomOperator";
            this.Size = new System.Drawing.Size(13, 12);
            this.Controls.SetChildIndex(this.comboBoxUnderlyingPatch, 0);
            this.Controls.SetChildIndex(this.labelUnderlyingPatch, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelUnderlyingPatch;
        private System.Windows.Forms.ComboBox comboBoxUnderlyingPatch;
    }
}
