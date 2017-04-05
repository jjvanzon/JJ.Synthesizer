namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class LibraryPropertiesUserControl
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
            this.labelNameTitle = new System.Windows.Forms.Label();
            this.labelNameValue = new System.Windows.Forms.Label();
            this.labelAlias = new System.Windows.Forms.Label();
            this.textBoxAlias = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelNameTitle
            // 
            this.labelNameTitle.Location = new System.Drawing.Point(17, 30);
            this.labelNameTitle.Name = "labelNameTitle";
            this.labelNameTitle.Size = new System.Drawing.Size(10, 10);
            this.labelNameTitle.TabIndex = 4;
            this.labelNameTitle.Text = "labelNameTitle";
            // 
            // labelNameValue
            // 
            this.labelNameValue.Location = new System.Drawing.Point(128, 30);
            this.labelNameValue.Name = "labelNameValue";
            this.labelNameValue.Size = new System.Drawing.Size(10, 10);
            this.labelNameValue.TabIndex = 5;
            this.labelNameValue.Text = "labelNameValue";
            // 
            // labelAlias
            // 
            this.labelAlias.Location = new System.Drawing.Point(30, 71);
            this.labelAlias.Name = "labelAlias";
            this.labelAlias.Size = new System.Drawing.Size(10, 10);
            this.labelAlias.TabIndex = 6;
            this.labelAlias.Text = "labelAlias";
            // 
            // textBoxAlias
            // 
            this.textBoxAlias.Location = new System.Drawing.Point(116, 73);
            this.textBoxAlias.Name = "textBoxAlias";
            this.textBoxAlias.Size = new System.Drawing.Size(10, 22);
            this.textBoxAlias.TabIndex = 7;
            // 
            // LibraryPropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxAlias);
            this.Controls.Add(this.labelAlias);
            this.Controls.Add(this.labelNameValue);
            this.Controls.Add(this.labelNameTitle);
            this.Name = "LibraryPropertiesUserControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.Controls.SetChildIndex(this.labelNameTitle, 0);
            this.Controls.SetChildIndex(this.labelNameValue, 0);
            this.Controls.SetChildIndex(this.labelAlias, 0);
            this.Controls.SetChildIndex(this.textBoxAlias, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNameTitle;
        private System.Windows.Forms.Label labelNameValue;
        private System.Windows.Forms.Label labelAlias;
        private System.Windows.Forms.TextBox textBoxAlias;
    }
}
