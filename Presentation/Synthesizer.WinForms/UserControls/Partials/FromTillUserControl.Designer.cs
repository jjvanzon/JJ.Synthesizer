namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    partial class FromTillUserControl
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
            this.fromMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.tillMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.labelDash = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fromMaskedTextBox
            // 
            this.fromMaskedTextBox.Location = new System.Drawing.Point(-1, 1);
            this.fromMaskedTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.fromMaskedTextBox.Name = "fromMaskedTextBox";
            this.fromMaskedTextBox.PromptChar = ' ';
            this.fromMaskedTextBox.Size = new System.Drawing.Size(100, 22);
            this.fromMaskedTextBox.TabIndex = 0;
            // 
            // tillMaskedTextBox
            // 
            this.tillMaskedTextBox.Location = new System.Drawing.Point(124, 3);
            this.tillMaskedTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.tillMaskedTextBox.Name = "tillMaskedTextBox";
            this.tillMaskedTextBox.PromptChar = ' ';
            this.tillMaskedTextBox.Size = new System.Drawing.Size(100, 22);
            this.tillMaskedTextBox.TabIndex = 1;
            // 
            // labelDash
            // 
            this.labelDash.AutoSize = true;
            this.labelDash.Location = new System.Drawing.Point(106, 3);
            this.labelDash.Margin = new System.Windows.Forms.Padding(0);
            this.labelDash.Name = "labelDash";
            this.labelDash.Size = new System.Drawing.Size(12, 16);
            this.labelDash.TabIndex = 2;
            this.labelDash.Text = "-";
            // 
            // FromTillUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelDash);
            this.Controls.Add(this.tillMaskedTextBox);
            this.Controls.Add(this.fromMaskedTextBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "FromTillUserControl";
            this.Size = new System.Drawing.Size(227, 28);
            this.Load += new System.EventHandler(this.FromTillUserControl_Load);
            this.SizeChanged += new System.EventHandler(this.FromTillUserControl_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox fromMaskedTextBox;
        private System.Windows.Forms.MaskedTextBox tillMaskedTextBox;
        private System.Windows.Forms.Label labelDash;
    }
}
