﻿namespace JJ.Presentation.Synthesizer.WinForms
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
            this.curveDetailsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.CurveDetailsUserControl();
            this.toneGridEditUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.ToneGridEditUserControl();
            this.scaleGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.ScaleGridUserControl();
            this.curveGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.CurveGridUserControl();
            this.audioFileOutputGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioFileOutputGridUserControl();
            this.patchGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.PatchGridUserControl();
            this.documentDetailsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentDetailsUserControl();
            this.documentGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentGridUserControl();
            this.patchDetailsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.PatchDetailsUserControl();
            this.sampleGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.SampleGridUserControl();
            this.operatorPropertiesUserControl_ForNumber = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForNumber();
            this.operatorPropertiesUserControl_WithCollectionRecalculation = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_WithCollectionRecalculation();
            this.operatorPropertiesUserControl_WithInletCount = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_WithInletCount();
            this.operatorPropertiesUserControl_ForInletsToDimension = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForInletsToDimension();
            this.audioOutputPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioOutputPropertiesUserControl();
            this.operatorPropertiesUserControl_ForCache = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForCache();
            this.operatorPropertiesUserControl_WithInterpolation = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_WithInterpolation();
            this.operatorPropertiesUserControl_WithOutletCount = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_WithOutletCount();
            this.operatorPropertiesUserControl_ForBundle = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForBundle();
            this.nodePropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.NodePropertiesUserControl();
            this.curvePropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.CurvePropertiesUserControl();
            this.operatorPropertiesUserControl_ForCurve = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForCurve();
            this.scalePropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.ScalePropertiesUserControl();
            this.operatorPropertiesUserControl_ForPatchInlet = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForPatchInlet();
            this.documentPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentPropertiesUserControl();
            this.operatorPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl();
            this.patchPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.PatchPropertiesUserControl();
            this.samplePropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.SamplePropertiesUserControl();
            this.audioFileOutputPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioFileOutputPropertiesUserControl();
            this.operatorPropertiesUserControl_ForSample = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForSample();
            this.operatorPropertiesUserControl_ForCustomOperator = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForCustomOperator();
            this.operatorPropertiesUserControl_ForPatchOutlet = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForPatchOutlet();
            this.menuUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.MenuUserControl();
            this.currentPatchesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.CurrentPatchesUserControl();
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
            this.splitContainerTree.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainerTree.Name = "splitContainerTree";
            // 
            // splitContainerTree.Panel1
            // 
            this.splitContainerTree.Panel1.Controls.Add(this.documentTreeUserControl);
            // 
            // splitContainerTree.Panel2
            // 
            this.splitContainerTree.Panel2.Controls.Add(this.splitContainerProperties);
            this.splitContainerTree.Size = new System.Drawing.Size(1459, 814);
            this.splitContainerTree.SplitterDistance = 265;
            this.splitContainerTree.SplitterWidth = 5;
            this.splitContainerTree.TabIndex = 5;
            // 
            // documentTreeUserControl
            // 
            this.documentTreeUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentTreeUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentTreeUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.documentTreeUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentTreeUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.documentTreeUserControl.Name = "documentTreeUserControl";
            this.documentTreeUserControl.Size = new System.Drawing.Size(265, 814);
            this.documentTreeUserControl.TabIndex = 0;
            this.documentTreeUserControl.Visible = false;
            // 
            // splitContainerProperties
            // 
            this.splitContainerProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerProperties.Location = new System.Drawing.Point(0, 0);
            this.splitContainerProperties.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerProperties.Name = "splitContainerProperties";
            // 
            // splitContainerProperties.Panel1
            // 
            this.splitContainerProperties.Panel1.Controls.Add(this.curveDetailsUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.toneGridEditUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.scaleGridUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.curveGridUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.audioFileOutputGridUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.patchGridUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.documentDetailsUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.documentGridUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.patchDetailsUserControl);
            this.splitContainerProperties.Panel1.Controls.Add(this.sampleGridUserControl);
            // 
            // splitContainerProperties.Panel2
            // 
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForBundle);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForNumber);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_WithCollectionRecalculation);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_WithInletCount);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForInletsToDimension);
            this.splitContainerProperties.Panel2.Controls.Add(this.audioOutputPropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForCache);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_WithInterpolation);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_WithOutletCount);
            this.splitContainerProperties.Panel2.Controls.Add(this.nodePropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.curvePropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForCurve);
            this.splitContainerProperties.Panel2.Controls.Add(this.scalePropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForPatchInlet);
            this.splitContainerProperties.Panel2.Controls.Add(this.documentPropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.patchPropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.samplePropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.audioFileOutputPropertiesUserControl);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForSample);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForCustomOperator);
            this.splitContainerProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForPatchOutlet);
            this.splitContainerProperties.Size = new System.Drawing.Size(1189, 814);
            this.splitContainerProperties.SplitterDistance = 851;
            this.splitContainerProperties.SplitterWidth = 5;
            this.splitContainerProperties.TabIndex = 2;
            // 
            // curveDetailsUserControl
            // 
            this.curveDetailsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curveDetailsUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.curveDetailsUserControl.Location = new System.Drawing.Point(0, 0);
            this.curveDetailsUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.curveDetailsUserControl.Name = "curveDetailsUserControl";
            this.curveDetailsUserControl.Size = new System.Drawing.Size(851, 814);
            this.curveDetailsUserControl.TabIndex = 12;
            // 
            // toneGridEditUserControl
            // 
            this.toneGridEditUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toneGridEditUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toneGridEditUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.toneGridEditUserControl.Location = new System.Drawing.Point(0, 0);
            this.toneGridEditUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.toneGridEditUserControl.Name = "toneGridEditUserControl";
            this.toneGridEditUserControl.Size = new System.Drawing.Size(851, 814);
            this.toneGridEditUserControl.TabIndex = 1;
            // 
            // scaleGridUserControl
            // 
            this.scaleGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.scaleGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.scaleGridUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.scaleGridUserControl.Name = "scaleGridUserControl";
            this.scaleGridUserControl.Size = new System.Drawing.Size(851, 814);
            this.scaleGridUserControl.TabIndex = 11;
            // 
            // curveGridUserControl
            // 
            this.curveGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curveGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.curveGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.curveGridUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.curveGridUserControl.Name = "curveGridUserControl";
            this.curveGridUserControl.Size = new System.Drawing.Size(851, 814);
            this.curveGridUserControl.TabIndex = 6;
            this.curveGridUserControl.Visible = false;
            // 
            // audioFileOutputGridUserControl
            // 
            this.audioFileOutputGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioFileOutputGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.audioFileOutputGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.audioFileOutputGridUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.audioFileOutputGridUserControl.Name = "audioFileOutputGridUserControl";
            this.audioFileOutputGridUserControl.Size = new System.Drawing.Size(851, 814);
            this.audioFileOutputGridUserControl.TabIndex = 5;
            this.audioFileOutputGridUserControl.Visible = false;
            // 
            // patchGridUserControl
            // 
            this.patchGridUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.patchGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.patchGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.patchGridUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.patchGridUserControl.Name = "patchGridUserControl";
            this.patchGridUserControl.Size = new System.Drawing.Size(851, 814);
            this.patchGridUserControl.TabIndex = 13;
            // 
            // documentDetailsUserControl
            // 
            this.documentDetailsUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.documentDetailsUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentDetailsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentDetailsUserControl.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.documentDetailsUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentDetailsUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.documentDetailsUserControl.Name = "documentDetailsUserControl";
            this.documentDetailsUserControl.Size = new System.Drawing.Size(851, 814);
            this.documentDetailsUserControl.TabIndex = 1;
            this.documentDetailsUserControl.Visible = false;
            // 
            // documentGridUserControl
            // 
            this.documentGridUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.documentGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentGridUserControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.documentGridUserControl.Name = "documentGridUserControl";
            this.documentGridUserControl.Size = new System.Drawing.Size(851, 814);
            this.documentGridUserControl.TabIndex = 0;
            this.documentGridUserControl.Visible = false;
            // 
            // patchDetailsUserControl
            // 
            this.patchDetailsUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.patchDetailsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchDetailsUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.patchDetailsUserControl.Location = new System.Drawing.Point(0, 0);
            this.patchDetailsUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.patchDetailsUserControl.Name = "patchDetailsUserControl";
            this.patchDetailsUserControl.Size = new System.Drawing.Size(851, 814);
            this.patchDetailsUserControl.TabIndex = 10;
            this.patchDetailsUserControl.Visible = false;
            // 
            // sampleGridUserControl
            // 
            this.sampleGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sampleGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.sampleGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.sampleGridUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.sampleGridUserControl.Name = "sampleGridUserControl";
            this.sampleGridUserControl.Size = new System.Drawing.Size(851, 814);
            this.sampleGridUserControl.TabIndex = 9;
            this.sampleGridUserControl.Visible = false;
            // 
            // operatorPropertiesUserControl_ForNumber
            // 
            this.operatorPropertiesUserControl_ForNumber.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForNumber.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForNumber.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForNumber.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForNumber.Margin = new System.Windows.Forms.Padding(5);
            this.operatorPropertiesUserControl_ForNumber.Name = "operatorPropertiesUserControl_ForNumber";
            this.operatorPropertiesUserControl_ForNumber.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_ForNumber.TabIndex = 1;
            this.operatorPropertiesUserControl_ForNumber.TitleBarText = "Operator Properties";
            // 
            // operatorPropertiesUserControl_WithCollectionRecalculation
            // 
            this.operatorPropertiesUserControl_WithCollectionRecalculation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_WithCollectionRecalculation.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_WithCollectionRecalculation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_WithCollectionRecalculation.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_WithCollectionRecalculation.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_WithCollectionRecalculation.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.operatorPropertiesUserControl_WithCollectionRecalculation.Name = "operatorPropertiesUserControl_WithCollectionRecalculation";
            this.operatorPropertiesUserControl_WithCollectionRecalculation.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_WithCollectionRecalculation.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_WithInletCount
            // 
            this.operatorPropertiesUserControl_WithInletCount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_WithInletCount.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_WithInletCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_WithInletCount.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_WithInletCount.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_WithInletCount.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.operatorPropertiesUserControl_WithInletCount.Name = "operatorPropertiesUserControl_WithInletCount";
            this.operatorPropertiesUserControl_WithInletCount.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_WithInletCount.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForInletsToDimension
            // 
            this.operatorPropertiesUserControl_ForInletsToDimension.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForInletsToDimension.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForInletsToDimension.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForInletsToDimension.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForInletsToDimension.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForInletsToDimension.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.operatorPropertiesUserControl_ForInletsToDimension.Name = "operatorPropertiesUserControl_ForInletsToDimension";
            this.operatorPropertiesUserControl_ForInletsToDimension.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_ForInletsToDimension.TabIndex = 1;
            // 
            // audioOutputPropertiesUserControl
            // 
            this.audioOutputPropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.audioOutputPropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioOutputPropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.audioOutputPropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.audioOutputPropertiesUserControl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.audioOutputPropertiesUserControl.Name = "audioOutputPropertiesUserControl";
            this.audioOutputPropertiesUserControl.Size = new System.Drawing.Size(333, 814);
            this.audioOutputPropertiesUserControl.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForCache
            // 
            this.operatorPropertiesUserControl_ForCache.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForCache.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForCache.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForCache.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForCache.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForCache.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.operatorPropertiesUserControl_ForCache.Name = "operatorPropertiesUserControl_ForCache";
            this.operatorPropertiesUserControl_ForCache.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_ForCache.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_WithInterpolation
            // 
            this.operatorPropertiesUserControl_WithInterpolation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_WithInterpolation.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_WithInterpolation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_WithInterpolation.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_WithInterpolation.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_WithInterpolation.Margin = new System.Windows.Forms.Padding(4);
            this.operatorPropertiesUserControl_WithInterpolation.Name = "operatorPropertiesUserControl_WithInterpolation";
            this.operatorPropertiesUserControl_WithInterpolation.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_WithInterpolation.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_WithOutletCount
            // 
            this.operatorPropertiesUserControl_WithOutletCount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_WithOutletCount.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_WithOutletCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_WithOutletCount.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_WithOutletCount.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_WithOutletCount.Margin = new System.Windows.Forms.Padding(5);
            this.operatorPropertiesUserControl_WithOutletCount.Name = "operatorPropertiesUserControl_WithOutletCount";
            this.operatorPropertiesUserControl_WithOutletCount.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_WithOutletCount.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForBundle
            // 
            this.operatorPropertiesUserControl_ForBundle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForBundle.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForBundle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForBundle.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForBundle.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForBundle.Margin = new System.Windows.Forms.Padding(5);
            this.operatorPropertiesUserControl_ForBundle.Name = "operatorPropertiesUserControl_ForBundle";
            this.operatorPropertiesUserControl_ForBundle.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_ForBundle.TabIndex = 1;
            this.operatorPropertiesUserControl_ForBundle.TitleBarText = "Operator Properties";
            // 
            // nodePropertiesUserControl
            // 
            this.nodePropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nodePropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.nodePropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodePropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.nodePropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.nodePropertiesUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.nodePropertiesUserControl.Name = "nodePropertiesUserControl";
            this.nodePropertiesUserControl.Size = new System.Drawing.Size(333, 814);
            this.nodePropertiesUserControl.TabIndex = 1;
            // 
            // curvePropertiesUserControl
            // 
            this.curvePropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.curvePropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.curvePropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curvePropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.curvePropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.curvePropertiesUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.curvePropertiesUserControl.Name = "curvePropertiesUserControl";
            this.curvePropertiesUserControl.Size = new System.Drawing.Size(333, 814);
            this.curvePropertiesUserControl.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForCurve
            // 
            this.operatorPropertiesUserControl_ForCurve.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForCurve.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForCurve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForCurve.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForCurve.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForCurve.Margin = new System.Windows.Forms.Padding(5);
            this.operatorPropertiesUserControl_ForCurve.Name = "operatorPropertiesUserControl_ForCurve";
            this.operatorPropertiesUserControl_ForCurve.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_ForCurve.TabIndex = 1;
            // 
            // scalePropertiesUserControl
            // 
            this.scalePropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scalePropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.scalePropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scalePropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.scalePropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.scalePropertiesUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.scalePropertiesUserControl.Name = "scalePropertiesUserControl";
            this.scalePropertiesUserControl.Size = new System.Drawing.Size(333, 814);
            this.scalePropertiesUserControl.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForPatchInlet
            // 
            this.operatorPropertiesUserControl_ForPatchInlet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForPatchInlet.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForPatchInlet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForPatchInlet.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForPatchInlet.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForPatchInlet.Margin = new System.Windows.Forms.Padding(5);
            this.operatorPropertiesUserControl_ForPatchInlet.Name = "operatorPropertiesUserControl_ForPatchInlet";
            this.operatorPropertiesUserControl_ForPatchInlet.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_ForPatchInlet.TabIndex = 1;
            // 
            // documentPropertiesUserControl
            // 
            this.documentPropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.documentPropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentPropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentPropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.documentPropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentPropertiesUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.documentPropertiesUserControl.Name = "documentPropertiesUserControl";
            this.documentPropertiesUserControl.Size = new System.Drawing.Size(333, 814);
            this.documentPropertiesUserControl.TabIndex = 1;
            // 
            // operatorPropertiesUserControl
            // 
            this.operatorPropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.operatorPropertiesUserControl.Name = "operatorPropertiesUserControl";
            this.operatorPropertiesUserControl.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl.TabIndex = 1;
            // 
            // patchPropertiesUserControl
            // 
            this.patchPropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.patchPropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.patchPropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patchPropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.patchPropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.patchPropertiesUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.patchPropertiesUserControl.Name = "patchPropertiesUserControl";
            this.patchPropertiesUserControl.Size = new System.Drawing.Size(333, 814);
            this.patchPropertiesUserControl.TabIndex = 1;
            // 
            // samplePropertiesUserControl
            // 
            this.samplePropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.samplePropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.samplePropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.samplePropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.samplePropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.samplePropertiesUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.samplePropertiesUserControl.Name = "samplePropertiesUserControl";
            this.samplePropertiesUserControl.Size = new System.Drawing.Size(333, 814);
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
            this.audioFileOutputPropertiesUserControl.Size = new System.Drawing.Size(333, 814);
            this.audioFileOutputPropertiesUserControl.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForSample
            // 
            this.operatorPropertiesUserControl_ForSample.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForSample.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForSample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForSample.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForSample.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForSample.Margin = new System.Windows.Forms.Padding(5);
            this.operatorPropertiesUserControl_ForSample.Name = "operatorPropertiesUserControl_ForSample";
            this.operatorPropertiesUserControl_ForSample.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_ForSample.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForCustomOperator
            // 
            this.operatorPropertiesUserControl_ForCustomOperator.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForCustomOperator.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForCustomOperator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForCustomOperator.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForCustomOperator.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForCustomOperator.Margin = new System.Windows.Forms.Padding(5);
            this.operatorPropertiesUserControl_ForCustomOperator.Name = "operatorPropertiesUserControl_ForCustomOperator";
            this.operatorPropertiesUserControl_ForCustomOperator.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_ForCustomOperator.TabIndex = 1;
            // 
            // operatorPropertiesUserControl_ForPatchOutlet
            // 
            this.operatorPropertiesUserControl_ForPatchOutlet.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.operatorPropertiesUserControl_ForPatchOutlet.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.operatorPropertiesUserControl_ForPatchOutlet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operatorPropertiesUserControl_ForPatchOutlet.Font = new System.Drawing.Font("Verdana", 10F);
            this.operatorPropertiesUserControl_ForPatchOutlet.Location = new System.Drawing.Point(0, 0);
            this.operatorPropertiesUserControl_ForPatchOutlet.Margin = new System.Windows.Forms.Padding(5);
            this.operatorPropertiesUserControl_ForPatchOutlet.Name = "operatorPropertiesUserControl_ForPatchOutlet";
            this.operatorPropertiesUserControl_ForPatchOutlet.Size = new System.Drawing.Size(333, 814);
            this.operatorPropertiesUserControl_ForPatchOutlet.TabIndex = 1;
            // 
            // menuUserControl
            // 
            this.menuUserControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuUserControl.Location = new System.Drawing.Point(0, 0);
            this.menuUserControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.menuUserControl.Name = "menuUserControl";
            this.menuUserControl.Size = new System.Drawing.Size(1459, 24);
            this.menuUserControl.TabIndex = 3;
            // 
            // currentPatchesUserControl
            // 
            this.currentPatchesUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentPatchesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.currentPatchesUserControl.Location = new System.Drawing.Point(271, 0);
            this.currentPatchesUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.currentPatchesUserControl.Name = "currentPatchesUserControl";
            this.currentPatchesUserControl.Size = new System.Drawing.Size(1188, 30);
            this.currentPatchesUserControl.TabIndex = 14;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1459, 838);
            this.Controls.Add(this.currentPatchesUserControl);
            this.Controls.Add(this.splitContainerTree);
            this.Controls.Add(this.menuUserControl);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private UserControls.PatchGridUserControl patchGridUserControl;
        private UserControls.AudioFileOutputGridUserControl audioFileOutputGridUserControl;
        private UserControls.CurveGridUserControl curveGridUserControl;
        private UserControls.SampleGridUserControl sampleGridUserControl;
        private UserControls.AudioFileOutputPropertiesUserControl audioFileOutputPropertiesUserControl;
        private UserControls.SamplePropertiesUserControl samplePropertiesUserControl;
        private UserControls.PatchDetailsUserControl patchDetailsUserControl;
        private UserControls.PatchPropertiesUserControl patchPropertiesUserControl;
        private UserControls.OperatorPropertiesUserControl operatorPropertiesUserControl;
        private UserControls.OperatorPropertiesUserControl_ForPatchInlet operatorPropertiesUserControl_ForPatchInlet;
        private UserControls.OperatorPropertiesUserControl_ForPatchOutlet operatorPropertiesUserControl_ForPatchOutlet;
        private UserControls.OperatorPropertiesUserControl_ForNumber operatorPropertiesUserControl_ForNumber;
        private UserControls.OperatorPropertiesUserControl_ForCustomOperator operatorPropertiesUserControl_ForCustomOperator;
        private UserControls.OperatorPropertiesUserControl_ForSample operatorPropertiesUserControl_ForSample;
        private UserControls.ScaleGridUserControl scaleGridUserControl;
        private UserControls.ToneGridEditUserControl toneGridEditUserControl;
        private UserControls.ScalePropertiesUserControl scalePropertiesUserControl;
        private UserControls.CurveDetailsUserControl curveDetailsUserControl;
        private UserControls.OperatorPropertiesUserControl_ForCurve operatorPropertiesUserControl_ForCurve;
        private UserControls.CurvePropertiesUserControl curvePropertiesUserControl;
        private UserControls.NodePropertiesUserControl nodePropertiesUserControl;
        private UserControls.OperatorPropertiesUserControl_ForBundle operatorPropertiesUserControl_ForBundle;
        private UserControls.OperatorPropertiesUserControl_WithOutletCount operatorPropertiesUserControl_WithOutletCount;
        private UserControls.Partials.CurrentPatchesUserControl currentPatchesUserControl;
        private UserControls.OperatorPropertiesUserControl_WithInterpolation operatorPropertiesUserControl_WithInterpolation;
        private UserControls.OperatorPropertiesUserControl_ForCache operatorPropertiesUserControl_ForCache;
        private UserControls.AudioOutputPropertiesUserControl audioOutputPropertiesUserControl;
        private UserControls.OperatorPropertiesUserControl_ForInletsToDimension operatorPropertiesUserControl_ForInletsToDimension;
        private UserControls.OperatorPropertiesUserControl_WithInletCount operatorPropertiesUserControl_WithInletCount;
        private UserControls.OperatorPropertiesUserControl_WithCollectionRecalculation operatorPropertiesUserControl_WithCollectionRecalculation;
    }
}