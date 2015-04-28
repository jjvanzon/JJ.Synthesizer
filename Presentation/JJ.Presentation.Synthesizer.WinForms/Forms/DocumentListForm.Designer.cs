namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    partial class DocumentListForm
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
            this.documentListUserControl1 = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentListUserControl();
            this.SuspendLayout();
            // 
            // documentListUserControl1
            // 
            this.documentListUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentListUserControl1.Location = new System.Drawing.Point(0, 0);
            this.documentListUserControl1.Margin = new System.Windows.Forms.Padding(4);
            this.documentListUserControl1.Name = "documentListUserControl1";
            this.documentListUserControl1.Size = new System.Drawing.Size(439, 504);
            this.documentListUserControl1.TabIndex = 0;
            // 
            // DocumentListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 504);
            this.Controls.Add(this.documentListUserControl1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DocumentListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.DocumentListUserControl documentListUserControl1;
    }
}