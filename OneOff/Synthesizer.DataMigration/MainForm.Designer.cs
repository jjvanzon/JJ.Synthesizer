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
            this.checkBoxMustAssertWarningIncrease = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxMustAssertWarningIncrease
            // 
            this.checkBoxMustAssertWarningIncrease.AutoSize = true;
            this.checkBoxMustAssertWarningIncrease.Location = new System.Drawing.Point(163, 105);
            this.checkBoxMustAssertWarningIncrease.Name = "checkBoxMustAssertWarningIncrease";
            this.checkBoxMustAssertWarningIncrease.Size = new System.Drawing.Size(219, 21);
            this.checkBoxMustAssertWarningIncrease.TabIndex = 1;
            this.checkBoxMustAssertWarningIncrease.Text = "Must Assert Warning Increase";
            this.checkBoxMustAssertWarningIncrease.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 538);
            this.Controls.Add(this.checkBoxMustAssertWarningIncrease);
            this.Description = "C# processes for migrating data when it is not easily done with SQL.";
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MustShowExceptions = true;
            this.Name = "MainForm";
            this.OnRunProcess += new System.EventHandler(this.MainForm_OnRunProcess);
            this.Controls.SetChildIndex(this.checkBoxMustAssertWarningIncrease, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxMustAssertWarningIncrease;
    }
}

