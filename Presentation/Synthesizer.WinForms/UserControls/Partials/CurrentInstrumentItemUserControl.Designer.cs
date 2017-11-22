namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
	partial class CurrentInstrumentItemUserControl
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
			this.components = new System.ComponentModel.Container();
			this.labelName = new System.Windows.Forms.Label();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.buttonMoveBackward = new System.Windows.Forms.Button();
			this.buttonMoveForward = new System.Windows.Forms.Button();
			this.buttonPlay = new System.Windows.Forms.Button();
			this.buttonExpand = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// labelName
			// 
			this.labelName.AutoSize = true;
			this.labelName.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
			this.labelName.Location = new System.Drawing.Point(48, 9);
			this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(130, 25);
			this.labelName.TabIndex = 0;
			this.labelName.Text = "labelName";
			// 
			// buttonRemove
			// 
			this.buttonRemove.FlatAppearance.BorderSize = 0;
			this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonRemove.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.RemoveIcon;
			this.buttonRemove.Location = new System.Drawing.Point(254, 12);
			this.buttonRemove.Margin = new System.Windows.Forms.Padding(0);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.Size = new System.Drawing.Size(24, 25);
			this.buttonRemove.TabIndex = 1;
			this.buttonRemove.UseVisualStyleBackColor = true;
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// toolTip
			// 
			this.toolTip.AutomaticDelay = 1;
			this.toolTip.AutoPopDelay = 100000;
			this.toolTip.InitialDelay = 1;
			this.toolTip.ReshowDelay = 200;
			this.toolTip.UseAnimation = false;
			this.toolTip.UseFading = false;
			// 
			// buttonMoveBackward
			// 
			this.buttonMoveBackward.FlatAppearance.BorderSize = 0;
			this.buttonMoveBackward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonMoveBackward.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.LessThanIcon;
			this.buttonMoveBackward.Location = new System.Drawing.Point(20, 12);
			this.buttonMoveBackward.Margin = new System.Windows.Forms.Padding(0);
			this.buttonMoveBackward.Name = "buttonMoveBackward";
			this.buttonMoveBackward.Size = new System.Drawing.Size(24, 25);
			this.buttonMoveBackward.TabIndex = 2;
			this.buttonMoveBackward.UseVisualStyleBackColor = true;
			this.buttonMoveBackward.Click += new System.EventHandler(this.buttonMoveBackward_Click);
			// 
			// buttonMoveForward
			// 
			this.buttonMoveForward.FlatAppearance.BorderSize = 0;
			this.buttonMoveForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonMoveForward.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.GreaterThanIcon;
			this.buttonMoveForward.Location = new System.Drawing.Point(182, 12);
			this.buttonMoveForward.Margin = new System.Windows.Forms.Padding(0);
			this.buttonMoveForward.Name = "buttonMoveForward";
			this.buttonMoveForward.Size = new System.Drawing.Size(24, 25);
			this.buttonMoveForward.TabIndex = 3;
			this.buttonMoveForward.UseVisualStyleBackColor = true;
			this.buttonMoveForward.Click += new System.EventHandler(this.buttonMoveForward_Click);
			// 
			// buttonPlay
			// 
			this.buttonPlay.FlatAppearance.BorderSize = 0;
			this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonPlay.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.PlayIcon;
			this.buttonPlay.Location = new System.Drawing.Point(206, 12);
			this.buttonPlay.Margin = new System.Windows.Forms.Padding(0);
			this.buttonPlay.Name = "buttonPlay";
			this.buttonPlay.Size = new System.Drawing.Size(24, 25);
			this.buttonPlay.TabIndex = 4;
			this.buttonPlay.UseVisualStyleBackColor = true;
			this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
			// 
			// buttonExpand
			// 
			this.buttonExpand.FlatAppearance.BorderSize = 0;
			this.buttonExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.buttonExpand.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.OpenWindowIcon;
			this.buttonExpand.Location = new System.Drawing.Point(225, 14);
			this.buttonExpand.Margin = new System.Windows.Forms.Padding(0);
			this.buttonExpand.Name = "buttonExpand";
			this.buttonExpand.Size = new System.Drawing.Size(24, 25);
			this.buttonExpand.TabIndex = 5;
			this.buttonExpand.UseVisualStyleBackColor = true;
			this.buttonExpand.Click += new System.EventHandler(this.buttonExpand_Click);
			// 
			// CurrentInstrumentItemUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.buttonExpand);
			this.Controls.Add(this.buttonPlay);
			this.Controls.Add(this.buttonMoveForward);
			this.Controls.Add(this.buttonMoveBackward);
			this.Controls.Add(this.buttonRemove);
			this.Controls.Add(this.labelName);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "CurrentInstrumentItemUserControl";
			this.Size = new System.Drawing.Size(285, 60);
			this.Load += new System.EventHandler(this.CurrentInstrumentItemUserControl_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelName;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Button buttonMoveBackward;
		private System.Windows.Forms.Button buttonMoveForward;
		private System.Windows.Forms.Button buttonPlay;
		private System.Windows.Forms.Button buttonExpand;
	}
}
