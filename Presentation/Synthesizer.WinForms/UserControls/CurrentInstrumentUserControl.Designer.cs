namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class CurrentInstrumentUserControl
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
            this.buttonShowAutoPatch = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // buttonShowAutoPatch
            // 
            this.buttonShowAutoPatch.FlatAppearance.BorderSize = 0;
            this.buttonShowAutoPatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowAutoPatch.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.OpenWindowIcon;
            this.buttonShowAutoPatch.Location = new System.Drawing.Point(286, 9);
            this.buttonShowAutoPatch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonShowAutoPatch.Name = "buttonShowAutoPatch";
            this.buttonShowAutoPatch.Size = new System.Drawing.Size(36, 38);
            this.buttonShowAutoPatch.TabIndex = 3;
            this.buttonShowAutoPatch.UseVisualStyleBackColor = true;
            this.buttonShowAutoPatch.Click += new System.EventHandler(this.buttonShowAutoPatch_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.FlatAppearance.BorderSize = 0;
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlay.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.PlayIcon;
            this.buttonPlay.Location = new System.Drawing.Point(241, 9);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(36, 38);
            this.buttonPlay.TabIndex = 4;
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
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
            // CurrentInstrumentUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonShowAutoPatch);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CurrentInstrumentUserControl";
            this.Size = new System.Drawing.Size(588, 275);
            this.Load += new System.EventHandler(this.CurrentInstrumentUserControl_Load);
            this.SizeChanged += new System.EventHandler(this.Base_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonShowAutoPatch;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
