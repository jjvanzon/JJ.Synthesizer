namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    partial class SampleListForm
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
            this.sampleListUserControl1 = new JJ.Presentation.Synthesizer.WinForms.UserControls.SampleListUserControl();
            this.SuspendLayout();
            // 
            // sampleListUserControl1
            // 
            this.sampleListUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sampleListUserControl1.Location = new System.Drawing.Point(0, 0);
            this.sampleListUserControl1.Margin = new System.Windows.Forms.Padding(4);
            this.sampleListUserControl1.Name = "sampleListUserControl1";
            this.sampleListUserControl1.Size = new System.Drawing.Size(973, 515);
            this.sampleListUserControl1.TabIndex = 0;
            // 
            // SampleListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 515);
            this.Controls.Add(this.sampleListUserControl1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SampleListForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SampleListForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.SampleListUserControl sampleListUserControl1;
    }
}