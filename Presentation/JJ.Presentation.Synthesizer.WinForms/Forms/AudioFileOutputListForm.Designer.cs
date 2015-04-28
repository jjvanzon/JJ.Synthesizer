namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    partial class AudioFileOutputListForm
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
            this.audioFileOutputListUserControl1 = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioFileOutputListUserControl();
            this.SuspendLayout();
            // 
            // audioFileOutputListUserControl1
            // 
            this.audioFileOutputListUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioFileOutputListUserControl1.Location = new System.Drawing.Point(0, 0);
            this.audioFileOutputListUserControl1.Margin = new System.Windows.Forms.Padding(4);
            this.audioFileOutputListUserControl1.Name = "audioFileOutputListUserControl1";
            this.audioFileOutputListUserControl1.Size = new System.Drawing.Size(938, 560);
            this.audioFileOutputListUserControl1.TabIndex = 0;
            // 
            // AudioFileOutputListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 560);
            this.Controls.Add(this.audioFileOutputListUserControl1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AudioFileOutputListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.AudioFileOutputListUserControl audioFileOutputListUserControl1;
    }
}