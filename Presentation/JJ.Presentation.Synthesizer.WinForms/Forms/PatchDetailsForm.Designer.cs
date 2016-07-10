using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    partial class PatchDetailsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.patchDetailsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.PatchDetailsUserControl();
            this.SuspendLayout();
            // 
            // patchDetailsUserControl
            // 
            this.patchDetailsUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.patchDetailsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchDetailsUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.patchDetailsUserControl.Location = new System.Drawing.Point(0, 0);
            this.patchDetailsUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.patchDetailsUserControl.Name = "patchDetailsUserControl";
            this.patchDetailsUserControl.Size = new System.Drawing.Size(1052, 610);
            this.patchDetailsUserControl.TabIndex = 1;
            // 
            // PatchDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 610);
            this.Controls.Add(this.patchDetailsUserControl);
            this.Name = "PatchDetailsForm";
            this.Text = "PatchDetailsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatchDetailsForm_FormClosing);
            this.Load += new System.EventHandler(this.PatchDetailsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.PatchDetailsUserControl patchDetailsUserControl;
    }
}