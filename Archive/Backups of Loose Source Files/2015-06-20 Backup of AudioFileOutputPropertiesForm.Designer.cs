namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    partial class AudioFileOutputPropertiesForm
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
            this.audioFileOutputPropertiesUserControl1 = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioFileOutputPropertiesUserControl();
            this.SuspendLayout();
            // 
            // audioFileOutputPropertiesUserControl1
            // 
            this.audioFileOutputPropertiesUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioFileOutputPropertiesUserControl1.Location = new System.Drawing.Point(0, 0);
            this.audioFileOutputPropertiesUserControl1.Name = "audioFileOutputPropertiesUserControl1";
            this.audioFileOutputPropertiesUserControl1.Size = new System.Drawing.Size(231, 394);
            this.audioFileOutputPropertiesUserControl1.TabIndex = 0;
            // 
            // AudioFileOutputPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 394);
            this.Controls.Add(this.audioFileOutputPropertiesUserControl1);
            this.Name = "AudioFileOutputPropertiesForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AudioFileOutputPropertiesForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.AudioFileOutputPropertiesUserControl audioFileOutputPropertiesUserControl1;

    }
}