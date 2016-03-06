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
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(205, 165);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // buttonPreviewAutoPatch
            // 
            this.buttonPreviewAutoPatch.FlatAppearance.BorderSize = 0;
            this.buttonPreviewAutoPatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPreviewAutoPatch.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.OpenWindowIconThinner;
            this.buttonPreviewAutoPatch.Location = new System.Drawing.Point(227, 7);
            this.buttonPreviewAutoPatch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.buttonClose.Location = new System.Drawing.Point(267, 7);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(32, 30);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // CurrentPatchesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonPreviewAutoPatch);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.flowLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CurrentPatchesUserControl";
            this.Size = new System.Drawing.Size(523, 220);
            this.SizeChanged += new System.EventHandler(this.CurrentPatchesUserControl_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonPreviewAutoPatch;
    }
}
