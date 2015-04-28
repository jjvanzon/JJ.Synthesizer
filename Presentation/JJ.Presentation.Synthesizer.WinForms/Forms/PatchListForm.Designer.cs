namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    partial class PatchListForm
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
            this.patchListUserControl1 = new JJ.Presentation.Synthesizer.WinForms.UserControls.PatchListUserControl();
            this.SuspendLayout();
            // 
            // patchListUserControl1
            // 
            this.patchListUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchListUserControl1.Location = new System.Drawing.Point(0, 0);
            this.patchListUserControl1.Margin = new System.Windows.Forms.Padding(4);
            this.patchListUserControl1.Name = "patchListUserControl1";
            this.patchListUserControl1.Size = new System.Drawing.Size(690, 502);
            this.patchListUserControl1.TabIndex = 0;
            // 
            // PatchListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 502);
            this.Controls.Add(this.patchListUserControl1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PatchListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.PatchListUserControl patchListUserControl1;
    }
}