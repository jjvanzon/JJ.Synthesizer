namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    partial class CurrentPatchesUserControl
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
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonShowAutoPatch = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonShowAutoPatchPolyphonic = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(205, 165);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // buttonShowAutoPatch
            // 
            this.buttonShowAutoPatch.FlatAppearance.BorderSize = 0;
            this.buttonShowAutoPatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowAutoPatch.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.OpenWindowIconThinner;
            this.buttonShowAutoPatch.Location = new System.Drawing.Point(254, 7);
            this.buttonShowAutoPatch.Margin = new System.Windows.Forms.Padding(4);
            this.buttonShowAutoPatch.Name = "buttonShowAutoPatch";
            this.buttonShowAutoPatch.Size = new System.Drawing.Size(32, 30);
            this.buttonShowAutoPatch.TabIndex = 3;
            this.buttonShowAutoPatch.UseVisualStyleBackColor = true;
            this.buttonShowAutoPatch.Click += new System.EventHandler(this.buttonShowAutoPatch_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.CloseIconThinner;
            this.buttonClose.Location = new System.Drawing.Point(294, 4);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(32, 30);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonShowAutoPatchPolyphonic
            // 
            this.buttonShowAutoPatchPolyphonic.FlatAppearance.BorderSize = 0;
            this.buttonShowAutoPatchPolyphonic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowAutoPatchPolyphonic.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.OpenWindowIconThinner;
            this.buttonShowAutoPatchPolyphonic.Location = new System.Drawing.Point(213, 7);
            this.buttonShowAutoPatchPolyphonic.Margin = new System.Windows.Forms.Padding(4);
            this.buttonShowAutoPatchPolyphonic.Name = "buttonShowAutoPatchPolyphonic";
            this.buttonShowAutoPatchPolyphonic.Size = new System.Drawing.Size(32, 30);
            this.buttonShowAutoPatchPolyphonic.TabIndex = 4;
            this.buttonShowAutoPatchPolyphonic.UseVisualStyleBackColor = true;
            this.buttonShowAutoPatchPolyphonic.Click += new System.EventHandler(this.buttonShowAutoPatchPolyphonic_Click);
            // 
            // CurrentPatchesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonShowAutoPatchPolyphonic);
            this.Controls.Add(this.buttonShowAutoPatch);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.flowLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CurrentPatchesUserControl";
            this.Size = new System.Drawing.Size(523, 220);
            this.SizeChanged += new System.EventHandler(this.CurrentPatchesUserControl_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonShowAutoPatch;
        private System.Windows.Forms.Button buttonShowAutoPatchPolyphonic;
    }
}
