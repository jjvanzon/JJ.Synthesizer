﻿using JJ.Framework.WinForms.Controls;

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
            this.diagramControl = new DiagramControl();
            this.SuspendLayout();
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
            this.Size = new System.Drawing.Size(672, 393);
            this.AddRequested += new System.EventHandler(this.CurveDetailsUserControl_AddClicked);
            this.Load += new System.EventHandler(this.CurveDetailsUserControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CurveDetailsUserControl_Paint);
            this.Resize += new System.EventHandler(this.CurveDetailsUserControl_Resize);
            this.Controls.SetChildIndex(this.diagramControl, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private DiagramControl diagramControl;
    }
}
