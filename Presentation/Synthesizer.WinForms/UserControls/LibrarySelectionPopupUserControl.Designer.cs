namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class LibrarySelectionPopupUserControl
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.librarySelectionGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.LibrarySelectionGridUserControl();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(0, 208);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(169, 36);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(259, 208);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(161, 36);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // librarySelectionGridUserControl
            // 
            this.librarySelectionGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.librarySelectionGridUserControl.Name = "librarySelectionGridUserControl";
            this.librarySelectionGridUserControl.Size = new System.Drawing.Size(420, 205);
            this.librarySelectionGridUserControl.TabIndex = 0;
            this.librarySelectionGridUserControl.ViewModel = null;
            // 
            // LibrarySelectionPopupUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.librarySelectionGridUserControl);
            this.Name = "LibrarySelectionPopupUserControl";
            this.Size = new System.Drawing.Size(420, 244);
            this.Load += new System.EventHandler(this.Base_Load);
            this.VisibleChanged += new System.EventHandler(this.LibrarySelectionPopupUserControl_VisibleChanged);
            this.Resize += new System.EventHandler(this.Base_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Partials.LibrarySelectionGridUserControl librarySelectionGridUserControl;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}
