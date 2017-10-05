namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class PatchDetailsUserControl
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

            UnbindVectorGraphicsEvents();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.diagramControl1 = new JJ.Framework.Presentation.WinForms.Controls.DiagramControl();
            this.SuspendLayout();
            // 
            // diagramControl1
            // 
            this.diagramControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diagramControl1.BackColor = System.Drawing.SystemColors.Window;
            this.diagramControl1.Diagram = null;
            this.diagramControl1.Location = new System.Drawing.Point(0, 32);
            this.diagramControl1.Margin = new System.Windows.Forms.Padding(0);
            this.diagramControl1.Name = "diagramControl1";
            this.diagramControl1.Size = new System.Drawing.Size(713, 293);
            this.diagramControl1.TabIndex = 3;
            // 
            // PatchDetailsUserControl
            // 
            this.AddToInstrumentButtonVisible = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.diagramControl1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PatchDetailsUserControl";
            this.PlayButtonVisible = true;
            this.RemoveButtonVisible = true;
            this.Size = new System.Drawing.Size(713, 325);
            this.Controls.SetChildIndex(this.diagramControl1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Presentation.WinForms.Controls.DiagramControl diagramControl1;
    }
}