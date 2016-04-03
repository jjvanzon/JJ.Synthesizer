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
            this.buttonPreviewAutoPatch = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonPreviewAutoPatchPolyphonic = new System.Windows.Forms.Button();
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
            // buttonPreviewAutoPatch
            // 
            this.buttonPreviewAutoPatch.FlatAppearance.BorderSize = 0;
            this.buttonPreviewAutoPatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPreviewAutoPatch.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.OpenWindowIconThinner;
            this.buttonPreviewAutoPatch.Location = new System.Drawing.Point(254, 7);
            this.buttonPreviewAutoPatch.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPreviewAutoPatch.Name = "buttonPreviewAutoPatch";
            this.buttonPreviewAutoPatch.Size = new System.Drawing.Size(32, 30);
            this.buttonPreviewAutoPatch.TabIndex = 3;
            this.buttonPreviewAutoPatch.UseVisualStyleBackColor = true;
            this.buttonPreviewAutoPatch.Click += new System.EventHandler(this.buttonPreviewAutoPatch_Click);
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
            // buttonPreviewAutoPatchPolyphonic
            // 
            this.buttonPreviewAutoPatchPolyphonic.FlatAppearance.BorderSize = 0;
            this.buttonPreviewAutoPatchPolyphonic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPreviewAutoPatchPolyphonic.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.OpenWindowIconThinner;
            this.buttonPreviewAutoPatchPolyphonic.Location = new System.Drawing.Point(213, 7);
            this.buttonPreviewAutoPatchPolyphonic.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPreviewAutoPatchPolyphonic.Name = "buttonPreviewAutoPatchPolyphonic";
            this.buttonPreviewAutoPatchPolyphonic.Size = new System.Drawing.Size(32, 30);
            this.buttonPreviewAutoPatchPolyphonic.TabIndex = 4;
            this.buttonPreviewAutoPatchPolyphonic.UseVisualStyleBackColor = true;
            this.buttonPreviewAutoPatchPolyphonic.Click += new System.EventHandler(this.buttonPreviewAutoPatchPolyphonic_Click);
            // 
            // CurrentPatchesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonPreviewAutoPatchPolyphonic);
            this.Controls.Add(this.buttonPreviewAutoPatch);
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
        private System.Windows.Forms.Button buttonPreviewAutoPatch;
        private System.Windows.Forms.Button buttonPreviewAutoPatchPolyphonic;
    }
}
