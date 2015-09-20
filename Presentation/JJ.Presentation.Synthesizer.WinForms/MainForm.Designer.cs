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
            this.curveGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.CurveGridUserControl();
            this.audioFileOutputGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioFileOutputGridUserControl();
            this.instrumentGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.ChildDocumentGridUserControl();
            this.effectGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.ChildDocumentGridUserControl();
            this.documentDetailsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentDetailsUserControl();
            this.documentGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentGridUserControl();
            this.patchDetailsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.PatchDetailsUserControl();
            this.sampleGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.SampleGridUserControl();
            this.patchGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.PatchGridUserControl();
            this.operatorPropertiesUserControl_ForSample = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForSample();
            this.operatorPropertiesUserControl_ForCustomOperator = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForCustomOperator();
            this.operatorPropertiesUserControl_ForNumber = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForNumber();
            this.operatorPropertiesUserControl_ForPatchOutlet = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForPatchOutlet();
            this.operatorPropertiesUserControl_ForPatchInlet = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForPatchInlet();
            this.documentPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentPropertiesUserControl();
            this.operatorPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl();
            this.childDocumentPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.ChildDocumentPropertiesUserControl();
            this.samplePropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.SamplePropertiesUserControl();
            this.audioFileOutputPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioFileOutputPropertiesUserControl();
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
            this.splitContainerTree.SplitterDistance = 199;
            this.splitContainerTree.TabIndex = 5;
            // 
            // documentTreeUserControl
            // 
            this.documentTreeUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentTreeUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentTreeUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.documentTreeUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentTreeUserControl.Name = "documentTreeUserControl";
            this.documentTreeUserControl.Size = new System.Drawing.Size(199, 657);
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
            this.splitContainerProperties.Panel1.Controls.Add(this.curveGridUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.audioFileOutputGridUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.instrumentGridUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.effectGridUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.documentDetailsUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.documentGridUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.patchDetailsUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.sampleGridUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.patchGridUserControl);
            // 
            // splitContainerProperties.Panel2
            // 
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForPatchInlet);
            this.splitContainerProperties.Panel2.Controls.Add(this.documentPropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.childDocumentPropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.samplePropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.audioFileOutputPropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForSample);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForCustomOperator);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForNumber);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForPatchOutlet);
            this.splitContainerProperties.Size = new System.Drawing.Size(891, 657);
            this.splitContainerProperties.SplitterDistance = 621;
            this.splitContainerProperties.TabIndex = 2;
            // 
            // curveGridUserControl
            // 
            this.curveGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curveGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.curveGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.curveGridUserControl.Name = "curveGridUserControl";
            this.curveGridUserControl.Size = new System.Drawing.Size(621, 657);
            this.curveGridUserControl.TabIndex = 6;
            this.curveGridUserControl.Visible = false;
            // 
            // audioFileOutputGridUserControl
            // 
            this.audioFileOutputGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioFileOutputGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.audioFileOutputGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.audioFileOutputGridUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.audioFileOutputGridUserControl.Name = "audioFileOutputGridUserControl";
            this.audioFileOutputGridUserControl.Size = new System.Drawing.Size(621, 657);
            this.audioFileOutputGridUserControl.TabIndex = 5;
            this.audioFileOutputGridUserControl.Visible = false;
            // 
            // instrumentGridUserControl
            // 
            this.instrumentGridUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.instrumentGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.instrumentGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.instrumentGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.instrumentGridUserControl.Name = "instrumentGridUserControl";
            this.instrumentGridUserControl.Size = new System.Drawing.Size(621, 657);
            this.instrumentGridUserControl.TabIndex = 4;
            this.instrumentGridUserControl.Title = "Title";
            this.instrumentGridUserControl.Visible = false;
            // 
            // effectGridUserControl
            // 
            this.effectGridUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.effectGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.effectGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.effectGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.effectGridUserControl.Name = "effectGridUserControl";
            this.effectGridUserControl.Size = new System.Drawing.Size(621, 657);
            this.effectGridUserControl.TabIndex = 3;
            this.effectGridUserControl.Title = "Title";
            this.effectGridUserControl.Visible = false;
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
            this.documentDetailsUserControl.Size = new System.Drawing.Size(621, 657);
            this.documentDetailsUserControl.TabIndex = 1;
            this.documentDetailsUserControl.Visible = false;
            // 
            // documentGridUserControl
            // 
            this.documentGridUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.documentGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentGridUserControl.Margin = new System.Windows.Forms.Padding(2);
            this.documentGridUserControl.Name = "documentGridUserControl";
            this.documentGridUserControl.Size = new System.Drawing.Size(621, 657);
            this.documentGridUserControl.TabIndex = 0;
            this.documentGridUserControl.Visible = false;
            // 
            // patchDetailsUserControl
            // 
            this.patchDetailsUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.patchDetailsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchDetailsUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.patchDetailsUserControl.Location = new System.Drawing.Point(0, 0);
            this.patchDetailsUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.patchDetailsUserControl.Name = "patchDetailsUserControl";
            this.patchDetailsUserControl.Size = new System.Drawing.Size(621, 657);
            this.patchDetailsUserControl.TabIndex = 10;
            this.patchDetailsUserControl.Visible = false;
            // 
            // sampleGridUserControl
            // 
            this.sampleGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sampleGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.sampleGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.sampleGridUserControl.Name = "sampleGridUserControl";
            this.sampleGridUserControl.Size = new System.Drawing.Size(621, 657);
            this.sampleGridUserControl.TabIndex = 9;
            this.sampleGridUserControl.Visible = false;
            // 
            // patchGridUserControl
            // 
            this.patchGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.patchGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.patchGridUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.patchGridUserControl.Name = "patchGridUserControl";
            this.patchGridUserControl.Size = new System.Drawing.Size(621, 657);
            this.patchGridUserControl.TabIndex = 8;
            this.patchGridUserControl.Visible = false;
            // 
            // operatorPropertiesUserControl_ForSample
            // 
            this.operatorPropertiesUserControl_ForSample.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForSample.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForSample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForSample.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForSample.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForSample.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.operatorPropertiesUserControl_ForSample.Name = "operatorPropertiesUserControl_ForSample";
            this.operatorPropertiesUserControl_ForSample.Size = new System.Drawing.Size(266, 657);
            this.operatorPropertiesUserControl_ForSample.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForCustomOperator
            // 
            this.operatorPropertiesUserControl_ForCustomOperator.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForCustomOperator.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForCustomOperator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForCustomOperator.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForCustomOperator.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForCustomOperator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.operatorPropertiesUserControl_ForCustomOperator.Name = "operatorPropertiesUserControl_ForCustomOperator";
            this.operatorPropertiesUserControl_ForCustomOperator.Size = new System.Drawing.Size(266, 657);
            this.operatorPropertiesUserControl_ForCustomOperator.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForNumber
            // 
            this.operatorPropertiesUserControl_ForNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForNumber.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForNumber.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForNumber.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForNumber.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.operatorPropertiesUserControl_ForNumber.Name = "operatorPropertiesUserControl_ForNumber";
            this.operatorPropertiesUserControl_ForNumber.Size = new System.Drawing.Size(266, 657);
            this.operatorPropertiesUserControl_ForNumber.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForPatchOutlet
            // 
            this.operatorPropertiesUserControl_ForPatchOutlet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForPatchOutlet.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForPatchOutlet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForPatchOutlet.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForPatchOutlet.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForPatchOutlet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.operatorPropertiesUserControl_ForPatchOutlet.Name = "operatorPropertiesUserControl_ForPatchOutlet";
            this.operatorPropertiesUserControl_ForPatchOutlet.Size = new System.Drawing.Size(266, 657);
            this.operatorPropertiesUserControl_ForPatchOutlet.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForPatchInlet
            // 
            this.operatorPropertiesUserControl_ForPatchInlet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForPatchInlet.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForPatchInlet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForPatchInlet.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForPatchInlet.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForPatchInlet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.operatorPropertiesUserControl_ForPatchInlet.Name = "operatorPropertiesUserControl_ForPatchInlet";
            this.operatorPropertiesUserControl_ForPatchInlet.Size = new System.Drawing.Size(266, 657);
            this.operatorPropertiesUserControl_ForPatchInlet.TabIndex = 1;
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
            this.documentPropertiesUserControl.Size = new System.Drawing.Size(266, 657);
            this.documentPropertiesUserControl.TabIndex = 1;
            // 
            // operatorPropertiesUserControl
            // 
            this.operatorPropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.operatorPropertiesUserControl.Name = "operatorPropertiesUserControl";
            this.operatorPropertiesUserControl.Size = new System.Drawing.Size(266, 657);
            this.operatorPropertiesUserControl.TabIndex = 1;
            // 
            // childDocumentPropertiesUserControl
            // 
            this.childDocumentPropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.childDocumentPropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.childDocumentPropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.childDocumentPropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.childDocumentPropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.childDocumentPropertiesUserControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.childDocumentPropertiesUserControl.Name = "childDocumentPropertiesUserControl";
            this.childDocumentPropertiesUserControl.Size = new System.Drawing.Size(266, 657);
            this.childDocumentPropertiesUserControl.TabIndex = 1;
            // 
            // samplePropertiesUserControl
            // 
            this.samplePropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.samplePropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.samplePropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.samplePropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.samplePropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.samplePropertiesUserControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.samplePropertiesUserControl.Name = "samplePropertiesUserControl";
            this.samplePropertiesUserControl.Size = new System.Drawing.Size(266, 657);
            this.samplePropertiesUserControl.TabIndex = 1;
            // 
            // audioFileOutputPropertiesUserControl
            // 
            this.audioFileOutputPropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.audioFileOutputPropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.audioFileOutputPropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioFileOutputPropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.audioFileOutputPropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.audioFileOutputPropertiesUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.audioFileOutputPropertiesUserControl.Name = "audioFileOutputPropertiesUserControl";
            this.audioFileOutputPropertiesUserControl.Size = new System.Drawing.Size(266, 657);
            this.audioFileOutputPropertiesUserControl.TabIndex = 1;
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
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
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
        private UserControls.DocumentGridUserControl documentGridUserControl;
        private UserControls.DocumentTreeUserControl documentTreeUserControl;
        private System.Windows.Forms.SplitContainer splitContainerProperties;
        private UserControls.DocumentDetailsUserControl documentDetailsUserControl;
        private UserControls.DocumentPropertiesUserControl documentPropertiesUserControl;
        private UserControls.ChildDocumentGridUserControl effectGridUserControl;
        private UserControls.ChildDocumentGridUserControl instrumentGridUserControl;
        private UserControls.AudioFileOutputGridUserControl audioFileOutputGridUserControl;
        private UserControls.PatchGridUserControl patchGridUserControl;
        private UserControls.CurveGridUserControl curveGridUserControl;
        private UserControls.SampleGridUserControl sampleGridUserControl;
        private UserControls.AudioFileOutputPropertiesUserControl audioFileOutputPropertiesUserControl;
        private UserControls.SamplePropertiesUserControl samplePropertiesUserControl;
        private UserControls.PatchDetailsUserControl patchDetailsUserControl;
        private UserControls.ChildDocumentPropertiesUserControl childDocumentPropertiesUserControl;
        private UserControls.OperatorPropertiesUserControl operatorPropertiesUserControl;
        private UserControls.OperatorPropertiesUserControl_ForPatchInlet operatorPropertiesUserControl_ForPatchInlet;
        private UserControls.OperatorPropertiesUserControl_ForPatchOutlet operatorPropertiesUserControl_ForPatchOutlet;
        private UserControls.OperatorPropertiesUserControl_ForNumber operatorPropertiesUserControl_ForNumber;
        private UserControls.OperatorPropertiesUserControl_ForCustomOperator operatorPropertiesUserControl_ForCustomOperator;
        private UserControls.OperatorPropertiesUserControl_ForSample operatorPropertiesUserControl_ForSample;
    }
}