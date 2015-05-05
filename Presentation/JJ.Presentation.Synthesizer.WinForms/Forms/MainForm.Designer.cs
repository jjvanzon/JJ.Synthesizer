namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.documentListUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentListUserControl();
            this.documentDetailsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentDetailsUserControl();
            this.menuUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.MenuUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.documentListUserControl);
            this.splitContainer1.Panel2.Controls.Add(this.documentDetailsUserControl);
            this.splitContainer1.Size = new System.Drawing.Size(1094, 657);
            this.splitContainer1.SplitterDistance = 242;
            this.splitContainer1.TabIndex = 5;
            // 
            // documentListUserControl
            // 
            this.documentListUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentListUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentListUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.documentListUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentListUserControl.Margin = new System.Windows.Forms.Padding(2);
            this.documentListUserControl.Name = "documentListUserControl";
            this.documentListUserControl.Size = new System.Drawing.Size(848, 657);
            this.documentListUserControl.TabIndex = 0;
            this.documentListUserControl.Visible = false;
            this.documentListUserControl.CloseRequested += new System.EventHandler(this.documentListUserControl_CloseRequested);
            // 
            // documentDetailsUserControl1
            // 
            this.documentDetailsUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.documentDetailsUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentDetailsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentDetailsUserControl.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentDetailsUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentDetailsUserControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.documentDetailsUserControl.Name = "documentDetailsUserControl1";
            this.documentDetailsUserControl.Size = new System.Drawing.Size(848, 657);
            this.documentDetailsUserControl.TabIndex = 1;
            this.documentDetailsUserControl.Visible = false;
            // 
            // menuUserControl
            // 
            this.menuUserControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuUserControl.Location = new System.Drawing.Point(0, 0);
            this.menuUserControl.Margin = new System.Windows.Forms.Padding(2);
            this.menuUserControl.Name = "menuUserControl";
            this.menuUserControl.Size = new System.Drawing.Size(1094, 24);
            this.menuUserControl.TabIndex = 3;
            this.menuUserControl.ShowDocumentListRequested += new System.EventHandler(this.menuUserControl_ShowDocumentListRequested);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1094, 681);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuUserControl);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.MenuUserControl menuUserControl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UserControls.DocumentListUserControl documentListUserControl;
        private UserControls.DocumentDetailsUserControl documentDetailsUserControl;

    }
}