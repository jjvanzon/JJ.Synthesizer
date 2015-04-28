namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    partial class CurveListForm
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
            this.curveListUserControl1 = new JJ.Presentation.Synthesizer.WinForms.UserControls.CurveListUserControl();
            this.SuspendLayout();
            // 
            // curveListUserControl1
            // 
            this.curveListUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curveListUserControl1.Location = new System.Drawing.Point(0, 0);
            this.curveListUserControl1.Margin = new System.Windows.Forms.Padding(4);
            this.curveListUserControl1.Name = "curveListUserControl1";
            this.curveListUserControl1.Size = new System.Drawing.Size(447, 481);
            this.curveListUserControl1.TabIndex = 0;
            // 
            // CurveListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 481);
            this.Controls.Add(this.curveListUserControl1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CurveListForm";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.CurveListUserControl curveListUserControl1;
    }
}