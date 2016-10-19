using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_ForCache
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
            this.labelSpeakerSetup = new System.Windows.Forms.Label();
            this.comboBoxSpeakerSetup = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelInterpolation
            // 
            this.labelInterpolation.Location = new System.Drawing.Point(0, 0);
            this.labelInterpolation.Margin = new System.Windows.Forms.Padding(0);
            this.labelInterpolation.Name = "labelInterpolation";
            this.labelInterpolation.Size = new System.Drawing.Size(10, 10);
            this.labelInterpolation.TabIndex = 24;
            this.labelInterpolation.Text = "labelInterpolation";
            this.labelInterpolation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxInterpolation
            // 
            this.comboBoxInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInterpolation.FormattingEnabled = true;
            this.comboBoxInterpolation.Location = new System.Drawing.Point(0, 0);
            this.comboBoxInterpolation.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxInterpolation.Name = "comboBoxInterpolation";
            this.comboBoxInterpolation.Size = new System.Drawing.Size(10, 24);
            this.comboBoxInterpolation.TabIndex = 25;
            // 
            // labelSpeakerSetup
            // 
            this.labelSpeakerSetup.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelSpeakerSetup.Location = new System.Drawing.Point(0, 0);
            this.labelSpeakerSetup.Margin = new System.Windows.Forms.Padding(0);
            this.labelSpeakerSetup.Name = "labelSpeakerSetup";
            this.labelSpeakerSetup.Size = new System.Drawing.Size(10, 10);
            this.labelSpeakerSetup.TabIndex = 26;
            this.labelSpeakerSetup.Text = "labelSpeakerSetup";
            this.labelSpeakerSetup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSpeakerSetup
            // 
            this.comboBoxSpeakerSetup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpeakerSetup.FormattingEnabled = true;
            this.comboBoxSpeakerSetup.Location = new System.Drawing.Point(0, 0);
            this.comboBoxSpeakerSetup.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxSpeakerSetup.Name = "comboBoxSpeakerSetup";
            this.comboBoxSpeakerSetup.Size = new System.Drawing.Size(10, 24);
            this.comboBoxSpeakerSetup.TabIndex = 27;
            // 
            // OperatorPropertiesUserControl_ForCache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelInterpolation);
            this.Controls.Add(this.comboBoxInterpolation);
            this.Controls.Add(this.labelSpeakerSetup);
            this.Controls.Add(this.comboBoxSpeakerSetup);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_ForCache";
            this.Size = new System.Drawing.Size(638, 443);
            this.Controls.SetChildIndex(this.comboBoxSpeakerSetup, 0);
            this.Controls.SetChildIndex(this.labelSpeakerSetup, 0);
            this.Controls.SetChildIndex(this.comboBoxInterpolation, 0);
            this.Controls.SetChildIndex(this.labelInterpolation, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelInterpolation;
        private System.Windows.Forms.ComboBox comboBoxInterpolation;
        private System.Windows.Forms.Label labelSpeakerSetup;
        private System.Windows.Forms.ComboBox comboBoxSpeakerSetup;
    }
}
