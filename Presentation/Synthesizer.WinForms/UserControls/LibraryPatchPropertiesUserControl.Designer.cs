namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class LibraryPatchPropertiesUserControl
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
            this.textBoxGroupValue = new System.Windows.Forms.Label();
            this.labelGroupTitle = new System.Windows.Forms.Label();
            this.labelLibraryNameValue = new System.Windows.Forms.Label();
            this.labelLibraryNameTitle = new System.Windows.Forms.Label();
            this.buttonAddToCurrentPatches = new System.Windows.Forms.Button();
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
            this.labelNameValue.Location = new System.Drawing.Point(169, 36);
            this.labelNameValue.Name = "labelNameValue";
            this.labelNameValue.Size = new System.Drawing.Size(10, 10);
            this.labelNameValue.TabIndex = 5;
            this.labelNameValue.Text = "labelNameValue";
            // 
            // textBoxGroupValue
            // 
            this.textBoxGroupValue.Location = new System.Drawing.Point(172, 72);
            this.textBoxGroupValue.Name = "textBoxGroupValue";
            this.textBoxGroupValue.Size = new System.Drawing.Size(10, 10);
            this.textBoxGroupValue.TabIndex = 7;
            this.textBoxGroupValue.Text = "textBoxGroupValue";
            // 
            // labelGroupTitle
            // 
            this.labelGroupTitle.Location = new System.Drawing.Point(20, 66);
            this.labelGroupTitle.Name = "labelGroupTitle";
            this.labelGroupTitle.Size = new System.Drawing.Size(10, 10);
            this.labelGroupTitle.TabIndex = 6;
            this.labelGroupTitle.Text = "labelGroupTitle";
            // 
            // labelLibraryNameValue
            // 
            this.labelLibraryNameValue.Location = new System.Drawing.Point(176, 103);
            this.labelLibraryNameValue.Name = "labelLibraryNameValue";
            this.labelLibraryNameValue.Size = new System.Drawing.Size(10, 10);
            this.labelLibraryNameValue.TabIndex = 9;
            this.labelLibraryNameValue.Text = "labelLibraryNameValue";
            // 
            // labelLibraryNameTitle
            // 
            this.labelLibraryNameTitle.Location = new System.Drawing.Point(24, 97);
            this.labelLibraryNameTitle.Name = "labelLibraryNameTitle";
            this.labelLibraryNameTitle.Size = new System.Drawing.Size(10, 10);
            this.labelLibraryNameTitle.TabIndex = 8;
            this.labelLibraryNameTitle.Text = "labelLibraryNameTitle";
            // 
            // buttonAddToCurrentPatches
            // 
            this.buttonAddToCurrentPatches.FlatAppearance.BorderSize = 0;
            this.buttonAddToCurrentPatches.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddToCurrentPatches.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonAddToCurrentPatches.Location = new System.Drawing.Point(44, 142);
            this.buttonAddToCurrentPatches.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddToCurrentPatches.Name = "buttonAddToCurrentPatches";
            this.buttonAddToCurrentPatches.Size = new System.Drawing.Size(10, 10);
            this.buttonAddToCurrentPatches.TabIndex = 10;
            this.buttonAddToCurrentPatches.Text = "buttonAddToCurrentPatches";
            this.buttonAddToCurrentPatches.UseVisualStyleBackColor = true;
            this.buttonAddToCurrentPatches.Click += new System.EventHandler(this.buttonAddToCurrentPatches_Click);
            // 
            // LibraryPatchPropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonAddToCurrentPatches);
            this.Controls.Add(this.labelLibraryNameValue);
            this.Controls.Add(this.labelLibraryNameTitle);
            this.Controls.Add(this.textBoxGroupValue);
            this.Controls.Add(this.labelGroupTitle);
            this.Controls.Add(this.labelNameValue);
            this.Controls.Add(this.labelNameTitle);
            this.Name = "LibraryPatchPropertiesUserControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.Controls.SetChildIndex(this.labelNameTitle, 0);
            this.Controls.SetChildIndex(this.labelNameValue, 0);
            this.Controls.SetChildIndex(this.labelGroupTitle, 0);
            this.Controls.SetChildIndex(this.textBoxGroupValue, 0);
            this.Controls.SetChildIndex(this.labelLibraryNameTitle, 0);
            this.Controls.SetChildIndex(this.labelLibraryNameValue, 0);
            this.Controls.SetChildIndex(this.buttonAddToCurrentPatches, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelNameTitle;
        private System.Windows.Forms.Label labelNameValue;
        private System.Windows.Forms.Label textBoxGroupValue;
        private System.Windows.Forms.Label labelGroupTitle;
        private System.Windows.Forms.Label labelLibraryNameValue;
        private System.Windows.Forms.Label labelLibraryNameTitle;
        private System.Windows.Forms.Button buttonAddToCurrentPatches;
    }
}
