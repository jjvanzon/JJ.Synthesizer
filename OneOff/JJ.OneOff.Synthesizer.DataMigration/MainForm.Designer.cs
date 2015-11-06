namespace JJ.OneOff.Synthesizer.DataMigration
{
    partial class MainForm
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
            this.radioButtonMigrateSineVolumes = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButtonMigrateSineVolumes
            // 
            this.radioButtonMigrateSineVolumes.AutoSize = true;
            this.radioButtonMigrateSineVolumes.Checked = true;
            this.radioButtonMigrateSineVolumes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radioButtonMigrateSineVolumes.Location = new System.Drawing.Point(197, 131);
            this.radioButtonMigrateSineVolumes.Name = "radioButtonMigrateSineVolumes";
            this.radioButtonMigrateSineVolumes.Size = new System.Drawing.Size(163, 21);
            this.radioButtonMigrateSineVolumes.TabIndex = 1;
            this.radioButtonMigrateSineVolumes.TabStop = true;
            this.radioButtonMigrateSineVolumes.Text = "Migrate Sine Volumes";
            this.radioButtonMigrateSineVolumes.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 345);
            this.Controls.Add(this.radioButtonMigrateSineVolumes);
            this.Description = "C# processes for migrating data when it is not easily done with SQL.";
            this.MustShowExceptions = true;
            this.Name = "MainForm";
            this.OnRunProcess += new System.EventHandler(this.MainForm_OnRunProcess);
            this.Controls.SetChildIndex(this.radioButtonMigrateSineVolumes, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonMigrateSineVolumes;
    }
}

