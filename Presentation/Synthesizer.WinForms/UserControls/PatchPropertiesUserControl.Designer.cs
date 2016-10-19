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
            this.buttonAddToCurentPatches = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelGroup
            // 
            this.labelGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGroup.Location = new System.Drawing.Point(0, 24);
            this.labelGroup.Margin = new System.Windows.Forms.Padding(0);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(120, 24);
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
            this.labelName.Size = new System.Drawing.Size(120, 24);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(120, 0);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(16, 20);
            this.textBoxName.TabIndex = 11;
            // 
            // textBoxGroup
            // 
            this.textBoxGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxGroup.Location = new System.Drawing.Point(120, 24);
            this.textBoxGroup.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxGroup.Name = "textBoxGroup";
            this.textBoxGroup.Size = new System.Drawing.Size(16, 20);
            this.textBoxGroup.TabIndex = 13;
            // 
            // buttonAddToCurentPatches
            // 
            this.buttonAddToCurentPatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAddToCurentPatches.FlatAppearance.BorderSize = 0;
            this.buttonAddToCurentPatches.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddToCurentPatches.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.buttonAddToCurentPatches.Location = new System.Drawing.Point(123, 51);
            this.buttonAddToCurentPatches.Name = "buttonAddToCurentPatches";
            this.buttonAddToCurentPatches.Size = new System.Drawing.Size(10, 24);
            this.buttonAddToCurentPatches.TabIndex = 9;
            this.buttonAddToCurentPatches.Text = "buttonAddToCurentPatches";
            this.buttonAddToCurentPatches.UseVisualStyleBackColor = true;
            this.buttonAddToCurentPatches.Click += new System.EventHandler(this.buttonAddToCurentPatches_Click);
            // 
            // PatchPropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.labelGroup);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxGroup);
            this.Controls.Add(this.buttonAddToCurentPatches);
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Name = "PatchPropertiesUserControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelGroup;
        private System.Windows.Forms.TextBox textBoxGroup;
        private System.Windows.Forms.Button buttonAddToCurentPatches;
    }
}
