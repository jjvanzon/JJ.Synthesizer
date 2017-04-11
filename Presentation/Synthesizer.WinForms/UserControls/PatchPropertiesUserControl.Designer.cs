using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class PatchPropertiesUserControl
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
            this.labelGroup = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxGroup = new System.Windows.Forms.TextBox();
            this.buttonAddToInstrument = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelGroup
            // 
            this.labelGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGroup.Location = new System.Drawing.Point(0, 0);
            this.labelGroup.Margin = new System.Windows.Forms.Padding(0);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(13, 12);
            this.labelGroup.TabIndex = 12;
            this.labelGroup.Text = "labelGroup";
            this.labelGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelName
            // 
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Location = new System.Drawing.Point(0, 0);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(13, 12);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(0, 0);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(13, 22);
            this.textBoxName.TabIndex = 11;
            // 
            // textBoxGroup
            // 
            this.textBoxGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxGroup.Location = new System.Drawing.Point(0, 0);
            this.textBoxGroup.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxGroup.Name = "textBoxGroup";
            this.textBoxGroup.Size = new System.Drawing.Size(13, 22);
            this.textBoxGroup.TabIndex = 13;
            // 
            // buttonAddToInstrument
            // 
            this.buttonAddToInstrument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAddToInstrument.FlatAppearance.BorderSize = 0;
            this.buttonAddToInstrument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddToInstrument.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonAddToInstrument.Location = new System.Drawing.Point(0, 0);
            this.buttonAddToInstrument.Padding = new System.Windows.Forms.Padding(0);
            this.buttonAddToInstrument.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAddToInstrument.Name = "buttonAddToInstrument";
            this.buttonAddToInstrument.Size = new System.Drawing.Size(13, 12);
            this.buttonAddToInstrument.TabIndex = 9;
            this.buttonAddToInstrument.Text = "buttonAddToInstrument";
            this.buttonAddToInstrument.UseVisualStyleBackColor = true;
            this.buttonAddToInstrument.Click += new System.EventHandler(this.buttonAddToInstrument_Click);
            // 
            // PatchPropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.labelGroup);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxGroup);
            this.Controls.Add(this.buttonAddToInstrument);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PatchPropertiesUserControl";
            this.Size = new System.Drawing.Size(13, 12);
            this.Controls.SetChildIndex(this.buttonAddToInstrument, 0);
            this.Controls.SetChildIndex(this.textBoxGroup, 0);
            this.Controls.SetChildIndex(this.textBoxName, 0);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.labelGroup, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelGroup;
        private System.Windows.Forms.TextBox textBoxGroup;
        private System.Windows.Forms.Button buttonAddToInstrument;
    }
}
