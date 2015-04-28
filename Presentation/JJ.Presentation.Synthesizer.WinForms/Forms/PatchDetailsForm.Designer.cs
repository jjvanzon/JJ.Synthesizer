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
            this.patchDetailsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchDetailsUserControl.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patchDetailsUserControl.Location = new System.Drawing.Point(0, 0);
            this.patchDetailsUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.patchDetailsUserControl.Name = "patchDetailsUserControl";
            this.patchDetailsUserControl.Size = new System.Drawing.Size(986, 634);
            this.patchDetailsUserControl.TabIndex = 0;
            // 
            // PatchDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 634);
            this.Controls.Add(this.patchDetailsUserControl);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PatchDetailsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private JJ.Presentation.Synthesizer.WinForms.UserControls.PatchDetailsUserControl patchDetailsUserControl;

    }
}