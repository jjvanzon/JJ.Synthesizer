namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    partial class TitleBarUserControl
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
            this.diagramControl = new JJ.Framework.WinForms.Controls.DiagramControl();
            this.SuspendLayout();
            // 
            // diagramControl
            // 
            this.diagramControl.Diagram = null;
            this.diagramControl.Location = new System.Drawing.Point(142, 32);
            this.diagramControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.diagramControl.Name = "diagramControl";
            this.diagramControl.Size = new System.Drawing.Size(53, 20);
            this.diagramControl.TabIndex = 1;
            // 
            // TitleBarUserControl
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.diagramControl);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TitleBarUserControl";
            this.Size = new System.Drawing.Size(337, 32);
            this.Load += new System.EventHandler(this.TitleBarUserControl_Load);
            this.SizeChanged += new System.EventHandler(this.TitleBarUserControl_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.WinForms.Controls.DiagramControl diagramControl;
    }
}
