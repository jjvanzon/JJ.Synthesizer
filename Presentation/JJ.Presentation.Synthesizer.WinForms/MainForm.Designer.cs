namespace JJ.Presentation.Synthesizer.WinForms
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
            this.splitContainerTree = new System.Windows.Forms.SplitContainer();
            this.documentTreeUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentTreeUserControl();
            this.splitContainerProperties = new System.Windows.Forms.SplitContainer();
            this.audioFileOutputListUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioFileOutputListUserControl();
            this.instrumentListUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.InstrumentListUserControl();
            this.effectListUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentListUserControl();
            this.documentDetailsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentDetailsUserControl();
            this.documentListUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentListUserControl();
            this.documentPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentPropertiesUserControl();
            this.menuUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.MenuUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTree)).BeginInit();
            this.splitContainerTree.Panel1.SuspendLayout();
            this.splitContainerTree.Panel2.SuspendLayout();
            this.splitContainerTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).BeginInit();
            this.splitContainerProperties.Panel1.SuspendLayout();
            this.splitContainerProperties.Panel2.SuspendLayout();
            this.splitContainerProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerTree
            // 
            this.splitContainerTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTree.Location = new System.Drawing.Point(0, 24);
            this.splitContainerTree.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainerTree.Name = "splitContainerTree";
            // 
            // splitContainerTree.Panel1
            // 
            this.splitContainerTree.Panel1.Controls.Add(this.documentTreeUserControl);
            // 
            // splitContainerTree.Panel2
            // 
            this.splitContainerTree.Panel2.Controls.Add(this.splitContainerProperties);
            this.splitContainerTree.Size = new System.Drawing.Size(1094, 657);
            this.splitContainerTree.SplitterDistance = 242;
            this.splitContainerTree.TabIndex = 5;
            // 
            // documentTreeUserControl
            // 
            this.documentTreeUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentTreeUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentTreeUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.documentTreeUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentTreeUserControl.Name = "documentTreeUserControl";
            this.documentTreeUserControl.Size = new System.Drawing.Size(242, 657);
            this.documentTreeUserControl.TabIndex = 0;
            this.documentTreeUserControl.Visible = false;
            // 
            // splitContainerProperties
            // 
            this.splitContainerProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerProperties.Location = new System.Drawing.Point(0, 0);
            this.splitContainerProperties.Name = "splitContainerProperties";
            // 
            // splitContainerProperties.Panel1
            // 
            this.splitContainerProperties.Panel1.Controls.Add(this.audioFileOutputListUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.instrumentListUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.effectListUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.documentDetailsUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.documentListUserControl);
            // 
            // splitContainerProperties.Panel2
            // 
            this.splitContainerProperties.Panel2.Controls.Add(this.documentPropertiesUserControl);
            this.splitContainerProperties.Size = new System.Drawing.Size(848, 657);
            this.splitContainerProperties.SplitterDistance = 604;
            this.splitContainerProperties.TabIndex = 2;
            // 
            // audioFileOutputListUserControl
            // 
            this.audioFileOutputListUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioFileOutputListUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.audioFileOutputListUserControl.Location = new System.Drawing.Point(0, 0);
            this.audioFileOutputListUserControl.Name = "audioFileOutputListUserControl";
            this.audioFileOutputListUserControl.Size = new System.Drawing.Size(604, 657);
            this.audioFileOutputListUserControl.TabIndex = 5;
            this.audioFileOutputListUserControl.Visible = false;
            // 
            // instrumentListUserControl
            // 
            this.instrumentListUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.instrumentListUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.instrumentListUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.instrumentListUserControl.Location = new System.Drawing.Point(0, 0);
            this.instrumentListUserControl.Name = "instrumentListUserControl";
            this.instrumentListUserControl.Size = new System.Drawing.Size(604, 657);
            this.instrumentListUserControl.TabIndex = 4;
            this.instrumentListUserControl.Visible = false;
            // 
            // effectListUserControl
            // 
            this.effectListUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.effectListUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.effectListUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.effectListUserControl.Location = new System.Drawing.Point(0, 0);
            this.effectListUserControl.Name = "effectListUserControl";
            this.effectListUserControl.Size = new System.Drawing.Size(604, 657);
            this.effectListUserControl.TabIndex = 3;
            this.effectListUserControl.Visible = false;
            // 
            // documentDetailsUserControl
            // 
            this.documentDetailsUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.documentDetailsUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentDetailsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentDetailsUserControl.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentDetailsUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentDetailsUserControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.documentDetailsUserControl.Name = "documentDetailsUserControl";
            this.documentDetailsUserControl.Size = new System.Drawing.Size(604, 657);
            this.documentDetailsUserControl.TabIndex = 1;
            this.documentDetailsUserControl.Visible = false;
            // 
            // documentListUserControl
            // 
            this.documentListUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentListUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentListUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.documentListUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentListUserControl.Margin = new System.Windows.Forms.Padding(2);
            this.documentListUserControl.Name = "documentListUserControl";
            this.documentListUserControl.Size = new System.Drawing.Size(604, 657);
            this.documentListUserControl.TabIndex = 0;
            this.documentListUserControl.Visible = false;
            // 
            // documentPropertiesUserControl
            // 
            this.documentPropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.documentPropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentPropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentPropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.documentPropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentPropertiesUserControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.documentPropertiesUserControl.Name = "documentPropertiesUserControl";
            this.documentPropertiesUserControl.Size = new System.Drawing.Size(240, 657);
            this.documentPropertiesUserControl.TabIndex = 1;
            // 
            // menuUserControl
            // 
            this.menuUserControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuUserControl.Location = new System.Drawing.Point(0, 0);
            this.menuUserControl.Margin = new System.Windows.Forms.Padding(2);
            this.menuUserControl.Name = "menuUserControl";
            this.menuUserControl.Size = new System.Drawing.Size(1094, 24);
            this.menuUserControl.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1094, 681);
            this.Controls.Add(this.splitContainerTree);
            this.Controls.Add(this.menuUserControl);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainerTree.Panel1.ResumeLayout(false);
            this.splitContainerTree.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTree)).EndInit();
            this.splitContainerTree.ResumeLayout(false);
            this.splitContainerProperties.Panel1.ResumeLayout(false);
            this.splitContainerProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerProperties)).EndInit();
            this.splitContainerProperties.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.MenuUserControl menuUserControl;
        private System.Windows.Forms.SplitContainer splitContainerTree;
        private UserControls.DocumentListUserControl documentListUserControl;
        private UserControls.DocumentTreeUserControl documentTreeUserControl;
        private System.Windows.Forms.SplitContainer splitContainerProperties;
        private UserControls.DocumentDetailsUserControl documentDetailsUserControl;
        private UserControls.DocumentPropertiesUserControl documentPropertiesUserControl;
        private UserControls.DocumentListUserControl effectListUserControl;
        private UserControls.InstrumentListUserControl instrumentListUserControl;
        private UserControls.AudioFileOutputListUserControl audioFileOutputListUserControl;

    }
}