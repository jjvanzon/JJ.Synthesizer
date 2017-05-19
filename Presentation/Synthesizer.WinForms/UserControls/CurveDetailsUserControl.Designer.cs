namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class CurveDetailsUserControl
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
            this.diagramControl = new JJ.Framework.Presentation.WinForms.Controls.DiagramControl();
            this.SuspendLayout();
            // 
            // _titleBarUserControl
            // 
            this._titleBarUserControl.AddButtonVisible = true;
            this._titleBarUserControl.RemoveButtonVisible = true;
            this._titleBarUserControl.Size = new System.Drawing.Size(672, 32);
            this._titleBarUserControl.TabIndex = 2;
            // 
            // diagramControl
            // 
            this.diagramControl.Diagram = null;
            this.diagramControl.Location = new System.Drawing.Point(0, 26);
            this.diagramControl.Margin = new System.Windows.Forms.Padding(0);
            this.diagramControl.Name = "diagramControl";
            this.diagramControl.Size = new System.Drawing.Size(372, 255);
            this.diagramControl.TabIndex = 1;
            // 
            // CurveDetailsUserControl
            // 
            this.AddButtonVisible = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.diagramControl);
            this.Name = "CurveDetailsUserControl";
            this.RemoveButtonVisible = true;
            this.Size = new System.Drawing.Size(672, 393);
            this.AddRequested += new System.EventHandler(this.CurveDetailsUserControl_AddClicked);
            this.RemoveRequested += new System.EventHandler(this.CurveDetailsUserControl_RemoveClicked);
            this.Load += new System.EventHandler(this.CurveDetailsUserControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CurveDetailsUserControl_Paint);
            this.Resize += new System.EventHandler(this.CurveDetailsUserControl_Resize);
            this.Controls.SetChildIndex(this.diagramControl, 0);
            this.Controls.SetChildIndex(this._titleBarUserControl, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Presentation.WinForms.Controls.DiagramControl diagramControl;
    }
}
