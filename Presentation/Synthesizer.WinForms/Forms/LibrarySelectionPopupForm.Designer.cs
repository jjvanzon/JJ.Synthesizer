namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    partial class LibrarySelectionPopupForm
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
            this.librarySelectionPopupUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.LibrarySelectionPopupUserControl();
            this.SuspendLayout();
            // 
            // librarySelectionPopupUserControl
            // 
            this.librarySelectionPopupUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.librarySelectionPopupUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.librarySelectionPopupUserControl.Location = new System.Drawing.Point(0, 0);
            this.librarySelectionPopupUserControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.librarySelectionPopupUserControl.Name = "librarySelectionPopupUserControl";
            this.librarySelectionPopupUserControl.Size = new System.Drawing.Size(917, 549);
            this.librarySelectionPopupUserControl.TabIndex = 0;
            this.librarySelectionPopupUserControl.ViewModel = null;
            this.librarySelectionPopupUserControl.CancelRequested += new System.EventHandler(this.librarySelectionPopupUserControl_CancelRequested);
            // 
            // LibrarySelectionPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 549);
            this.Controls.Add(this.librarySelectionPopupUserControl);
            this.Name = "LibrarySelectionPopupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LibrarySelectionPopupForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LibrarySelectionPopupForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.LibrarySelectionPopupUserControl librarySelectionPopupUserControl;
    }
}