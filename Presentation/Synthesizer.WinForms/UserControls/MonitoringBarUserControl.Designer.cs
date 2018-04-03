namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	partial class MonitoringBarUserControl
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
			this.diagramControl.Location = new System.Drawing.Point(49, 65);
			this.diagramControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.diagramControl.Name = "diagramControl";
			this.diagramControl.Size = new System.Drawing.Size(53, 20);
			this.diagramControl.TabIndex = 1;
			// 
			// MonitoringBarUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.diagramControl);
			this.Name = "MonitoringBarUserControl";
			this.SizeChanged += new System.EventHandler(this.MonitoringBarUserControl_SizeChanged);
			this.ResumeLayout(false);

		}

		#endregion

		private Framework.WinForms.Controls.DiagramControl diagramControl;
	}
}
