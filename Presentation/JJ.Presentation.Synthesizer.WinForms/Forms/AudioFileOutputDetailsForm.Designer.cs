namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    partial class AudioFileOutputDetailsForm
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
            this.audioFileOutputDetailsUserControl1 = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioFileOutputDetailsUserControl();
            this.SuspendLayout();
            // 
            // audioFileOutputDetailsUserControl1
            // 
            this.audioFileOutputDetailsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioFileOutputDetailsUserControl1.Location = new System.Drawing.Point(0, 0);
            this.audioFileOutputDetailsUserControl1.Name = "audioFileOutputDetailsUserControl1";
            this.audioFileOutputDetailsUserControl1.Size = new System.Drawing.Size(231, 394);
            this.audioFileOutputDetailsUserControl1.TabIndex = 0;
            // 
            // AudioFileOutputDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 394);
            this.Controls.Add(this.audioFileOutputDetailsUserControl1);
            this.Name = "AudioFileOutputDetailsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AudioFileOutputDetailsForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.AudioFileOutputDetailsUserControl audioFileOutputDetailsUserControl1;

    }
}