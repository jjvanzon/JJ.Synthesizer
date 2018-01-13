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
			this.labelTitle = new System.Windows.Forms.Label();
			this.buttonBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.ButtonBarUserControl();
			this.SuspendLayout();
			// 
			// labelTitle
			// 
			this.labelTitle.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
			this.labelTitle.Location = new System.Drawing.Point(0, 0);
			this.labelTitle.Margin = new System.Windows.Forms.Padding(0);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Padding = new System.Windows.Forms.Padding(3);
			this.labelTitle.Size = new System.Drawing.Size(337, 32);
			this.labelTitle.TabIndex = 7;
			this.labelTitle.Text = "Title";
			this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonBarUserControl
			// 
			this.buttonBarUserControl.AddButtonVisible = false;
			this.buttonBarUserControl.BackColor = System.Drawing.SystemColors.Control;
			this.buttonBarUserControl.CloseButtonVisible = true;
			this.buttonBarUserControl.Location = new System.Drawing.Point(313, 0);
			this.buttonBarUserControl.Margin = new System.Windows.Forms.Padding(0);
			this.buttonBarUserControl.Name = "buttonBarUserControl";
			this.buttonBarUserControl.ExpandButtonVisible = false;
			this.buttonBarUserControl.PlayButtonVisible = false;
			this.buttonBarUserControl.RefreshButtonVisible = false;
			this.buttonBarUserControl.DeleteButtonVisible = false;
			this.buttonBarUserControl.SaveButtonVisible = false;
			this.buttonBarUserControl.Size = new System.Drawing.Size(24, 32);
			this.buttonBarUserControl.TabIndex = 20;
			// 
			// TitleBarUserControl
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.buttonBarUserControl);
			this.Controls.Add(this.labelTitle);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "TitleBarUserControl";
			this.Size = new System.Drawing.Size(337, 32);
			this.Load += new System.EventHandler(this.TitleBarUserControl_Load);
			this.SizeChanged += new System.EventHandler(this.TitleBarUserControl_SizeChanged);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label labelTitle;
		private ButtonBarUserControl buttonBarUserControl;
	}
}
