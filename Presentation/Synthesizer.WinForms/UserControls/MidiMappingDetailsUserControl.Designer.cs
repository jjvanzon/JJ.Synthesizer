namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	partial class MidiMappingDetailsUserControl
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
			this.diagramControl = new JJ.Framework.Presentation.WinForms.Controls.DiagramControl();
			this.SuspendLayout();
			// 
			// diagramControl
			// 
			this.diagramControl.BackColor = System.Drawing.SystemColors.Window;
			this.diagramControl.Diagram = null;
			this.diagramControl.Location = new System.Drawing.Point(0, 32);
			this.diagramControl.Margin = new System.Windows.Forms.Padding(0);
			this.diagramControl.Name = "diagramControl";
			this.diagramControl.Size = new System.Drawing.Size(713, 293);
			this.diagramControl.TabIndex = 3;
			// 
			// MidiMappingDetailsUserControl
			// 
			this.AddToInstrumentButtonVisible = true;
			this.AddButtonVisible = true;
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Controls.Add(this.diagramControl);
			this.Name = "CurveDetailsUserControl";
			this.RemoveButtonVisible = true;
			this.Size = new System.Drawing.Size(672, 393);
			this.Load += new System.EventHandler(this.MidiMappingDetailsUserControl_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.MidiMappingDetailsUserControl_Paint);
			this.Resize += new System.EventHandler(this.MidiMappingDetailsUserControl_Resize);
			this.Controls.SetChildIndex(this.diagramControl, 0);
			this.ResumeLayout(false);
		}

		#endregion

		private Framework.Presentation.WinForms.Controls.DiagramControl diagramControl;
	}
}