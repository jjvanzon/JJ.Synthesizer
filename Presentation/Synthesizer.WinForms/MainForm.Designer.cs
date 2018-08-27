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
            this.splitContainerTreeAndRightSide = new System.Windows.Forms.SplitContainer();
            this.documentTreeUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentTreeUserControl();
            this.splitContainerCenterAndProperties = new System.Windows.Forms.SplitContainer();
            this.midiMappingDetailsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.MidiMappingGroupDetailsUserControl();
            this.toneGridEditUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.ToneGridEditUserControl();
            this.audioFileOutputGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioFileOutputGridUserControl();
            this.documentDetailsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentDetailsUserControl();
            this.documentGridUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentGridUserControl();
            this.patchDetailsUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.PatchDetailsUserControl();
            this.midiMappingPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.MidiMappingPropertiesUserControl();
            this.libraryPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.LibraryPropertiesUserControl();
            this.operatorPropertiesUserControl_ForNumber = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForNumber();
            this.operatorPropertiesUserControl_WithCollectionRecalculation = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_WithCollectionRecalculation();
            this.operatorPropertiesUserControl_ForInletsToDimension = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForInletsToDimension();
            this.audioOutputPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioOutputPropertiesUserControl();
            this.operatorPropertiesUserControl_ForCache = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForCache();
            this.operatorPropertiesUserControl_WithInterpolation = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_WithInterpolation();
            this.nodePropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.NodePropertiesUserControl();
            this.operatorPropertiesUserControl_ForCurve = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForCurve();
            this.scalePropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.ScalePropertiesUserControl();
            this.operatorPropertiesUserControl_ForPatchInlet = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForPatchInlet();
            this.documentPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.DocumentPropertiesUserControl();
            this.operatorPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl();
            this.patchPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.PatchPropertiesUserControl();
            this.audioFileOutputPropertiesUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.AudioFileOutputPropertiesUserControl();
            this.operatorPropertiesUserControl_ForSample = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForSample();
            this.operatorPropertiesUserControl_ForPatchOutlet = new JJ.Presentation.Synthesizer.WinForms.UserControls.OperatorPropertiesUserControl_ForPatchOutlet();
            this.splitContainerCurvesAndTopSide = new System.Windows.Forms.SplitContainer();
            this.curveDetailsListUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.CurveDetailsListUserControl();
            this.monitoringBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.MonitoringBarUserControl();
            this.topBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.TopBarUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTreeAndRightSide)).BeginInit();
            this.splitContainerTreeAndRightSide.Panel1.SuspendLayout();
            this.splitContainerTreeAndRightSide.Panel2.SuspendLayout();
            this.splitContainerTreeAndRightSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCenterAndProperties)).BeginInit();
            this.splitContainerCenterAndProperties.Panel1.SuspendLayout();
            this.splitContainerCenterAndProperties.Panel2.SuspendLayout();
            this.splitContainerCenterAndProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCurvesAndTopSide)).BeginInit();
            this.splitContainerCurvesAndTopSide.Panel1.SuspendLayout();
            this.splitContainerCurvesAndTopSide.Panel2.SuspendLayout();
            this.splitContainerCurvesAndTopSide.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerTreeAndRightSide
            // 
            this.splitContainerTreeAndRightSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTreeAndRightSide.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTreeAndRightSide.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainerTreeAndRightSide.Name = "splitContainerTreeAndRightSide";
            // 
            // splitContainerTreeAndRightSide.Panel1
            // 
            this.splitContainerTreeAndRightSide.Panel1.Controls.Add(this.documentTreeUserControl);
            // 
            // splitContainerTreeAndRightSide.Panel2
            // 
            this.splitContainerTreeAndRightSide.Panel2.Controls.Add(this.splitContainerCenterAndProperties);
            this.splitContainerTreeAndRightSide.Size = new System.Drawing.Size(1459, 488);
            this.splitContainerTreeAndRightSide.SplitterDistance = 281;
            this.splitContainerTreeAndRightSide.SplitterWidth = 5;
            this.splitContainerTreeAndRightSide.TabIndex = 5;
            // 
            // documentTreeUserControl
            // 
            this.documentTreeUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.documentTreeUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentTreeUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.documentTreeUserControl.Location = new System.Drawing.Point(0, 0);
            this.documentTreeUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.documentTreeUserControl.Name = "documentTreeUserControl";
            this.documentTreeUserControl.Size = new System.Drawing.Size(281, 488);
            this.documentTreeUserControl.TabIndex = 0;
            this.documentTreeUserControl.ViewModel = null;
            this.documentTreeUserControl.Visible = false;
            // 
            // splitContainerCenterAndProperties
            // 
            this.splitContainerCenterAndProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerCenterAndProperties.Location = new System.Drawing.Point(0, 0);
            this.splitContainerCenterAndProperties.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerCenterAndProperties.Name = "splitContainerCenterAndProperties";
            // 
            // splitContainerCenterAndProperties.Panel1
            // 
            this.splitContainerCenterAndProperties.Panel1.Controls.Add(this.midiMappingDetailsUserControl);
            this.splitContainerCenterAndProperties.Panel1.Controls.Add(this.toneGridEditUserControl);
            this.splitContainerCenterAndProperties.Panel1.Controls.Add(this.audioFileOutputGridUserControl);
            this.splitContainerCenterAndProperties.Panel1.Controls.Add(this.documentDetailsUserControl);
            this.splitContainerCenterAndProperties.Panel1.Controls.Add(this.documentGridUserControl);
            this.splitContainerCenterAndProperties.Panel1.Controls.Add(this.patchDetailsUserControl);
            // 
            // splitContainerCenterAndProperties.Panel2
            // 
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.midiMappingPropertiesUserControl);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.libraryPropertiesUserControl);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForNumber);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_WithCollectionRecalculation);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForInletsToDimension);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.audioOutputPropertiesUserControl);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForCache);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_WithInterpolation);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.nodePropertiesUserControl);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForCurve);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.scalePropertiesUserControl);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForPatchInlet);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.documentPropertiesUserControl);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.patchPropertiesUserControl);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.audioFileOutputPropertiesUserControl);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForSample);
            this.splitContainerCenterAndProperties.Panel2.Controls.Add(this.operatorPropertiesUserControl_ForPatchOutlet);
            this.splitContainerCenterAndProperties.Size = new System.Drawing.Size(1173, 488);
            this.splitContainerCenterAndProperties.SplitterDistance = 615;
            this.splitContainerCenterAndProperties.SplitterWidth = 5;
            this.splitContainerCenterAndProperties.TabIndex = 2;
            // 
            // midiMappingDetailsUserControl
            // 
            this.midiMappingDetailsUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.midiMappingDetailsUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.midiMappingDetailsUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.midiMappingDetailsUserControl.Location = new System.Drawing.Point(0, 0);
            this.midiMappingDetailsUserControl.Margin = new System.Windows.Forms.Padding(4);
            this.midiMappingDetailsUserControl.Name = "midiMappingDetailsUserControl";
            this.midiMappingDetailsUserControl.Size = new System.Drawing.Size(615, 488);
            this.midiMappingDetailsUserControl.TabIndex = 1;
            this.midiMappingDetailsUserControl.TitleBarBackColor = System.Drawing.SystemColors.Window;
            this.midiMappingDetailsUserControl.TitleBarText = "";
            this.midiMappingDetailsUserControl.ViewModel = null;
            this.midiMappingDetailsUserControl.Visible = false;
            // 
            // toneGridEditUserControl
            // 
            this.toneGridEditUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toneGridEditUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toneGridEditUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.toneGridEditUserControl.Location = new System.Drawing.Point(0, 0);
            this.toneGridEditUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.toneGridEditUserControl.Name = "toneGridEditUserControl";
            this.toneGridEditUserControl.Size = new System.Drawing.Size(615, 488);
            this.toneGridEditUserControl.TabIndex = 1;
            this.toneGridEditUserControl.ViewModel = null;
            this.toneGridEditUserControl.Visible = false;
            // 
            // audioFileOutputGridUserControl
            // 
            this.audioFileOutputGridUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioFileOutputGridUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.audioFileOutputGridUserControl.Location = new System.Drawing.Point(0, 0);
            this.audioFileOutputGridUserControl.Margin = new System.Windows.Forms.Padding(5);
            this.audioFileOutputGridUserControl.Name = "audioFileOutputGridUserControl";
            this.audioFileOutputGridUserControl.Size = new System.Drawing.Size(615, 488);
            this.audioFileOutputGridUserControl.TabIndex = 5;
            this.audioFileOutputGridUserControl.ViewModel = null;
            this.audioFileOutputGridUserControl.Visible = false;
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
            this.documentDetailsUserControl.Size = new System.Drawing.Size(615, 488);
            this.documentDetailsUserControl.TabIndex = 1;
            this.documentDetailsUserControl.ViewModel = null;
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
            this.documentGridUserControl.Size = new System.Drawing.Size(615, 488);
            this.documentGridUserControl.TabIndex = 0;
            this.documentGridUserControl.ViewModel = null;
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
            this.patchDetailsUserControl.Size = new System.Drawing.Size(615, 488);
            this.patchDetailsUserControl.TabIndex = 1;
            this.patchDetailsUserControl.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.patchDetailsUserControl.TitleBarText = "Patch Details";
            this.patchDetailsUserControl.ViewModel = null;
            this.patchDetailsUserControl.Visible = false;
            // 
            // midiMappingPropertiesUserControl
            // 
            this.midiMappingPropertiesUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.midiMappingPropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.midiMappingPropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.midiMappingPropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.midiMappingPropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.midiMappingPropertiesUserControl.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.midiMappingPropertiesUserControl.Name = "midiMappingPropertiesUserControl";
            this.midiMappingPropertiesUserControl.Size = new System.Drawing.Size(553, 488);
            this.midiMappingPropertiesUserControl.TabIndex = 1;
            this.midiMappingPropertiesUserControl.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.midiMappingPropertiesUserControl.TitleBarText = "MIDI Mapping Properties";
            this.midiMappingPropertiesUserControl.ViewModel = null;
            this.midiMappingPropertiesUserControl.Visible = false;
            // 
            // libraryPropertiesUserControl
            // 
            this.libraryPropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.libraryPropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.libraryPropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.libraryPropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.libraryPropertiesUserControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.libraryPropertiesUserControl.Name = "libraryPropertiesUserControl";
            this.libraryPropertiesUserControl.Size = new System.Drawing.Size(553, 488);
            this.libraryPropertiesUserControl.TabIndex = 1;
            this.libraryPropertiesUserControl.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.libraryPropertiesUserControl.TitleBarText = "Library Properties";
            this.libraryPropertiesUserControl.ViewModel = null;
            this.libraryPropertiesUserControl.Visible = false;
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
            this.operatorPropertiesUserControl_ForNumber.Size = new System.Drawing.Size(553, 488);
            this.operatorPropertiesUserControl_ForNumber.TabIndex = 1;
            this.operatorPropertiesUserControl_ForNumber.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.operatorPropertiesUserControl_ForNumber.TitleBarText = "Operator Properties";
            this.operatorPropertiesUserControl_ForNumber.ViewModel = null;
            this.operatorPropertiesUserControl_ForNumber.Visible = false;
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
            this.operatorPropertiesUserControl_WithCollectionRecalculation.Size = new System.Drawing.Size(553, 488);
            this.operatorPropertiesUserControl_WithCollectionRecalculation.TabIndex = 1;
            this.operatorPropertiesUserControl_WithCollectionRecalculation.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.operatorPropertiesUserControl_WithCollectionRecalculation.TitleBarText = "Operator Properties";
            this.operatorPropertiesUserControl_WithCollectionRecalculation.ViewModel = null;
            this.operatorPropertiesUserControl_WithCollectionRecalculation.Visible = false;
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
            this.operatorPropertiesUserControl_ForInletsToDimension.Size = new System.Drawing.Size(553, 488);
            this.operatorPropertiesUserControl_ForInletsToDimension.TabIndex = 1;
            this.operatorPropertiesUserControl_ForInletsToDimension.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.operatorPropertiesUserControl_ForInletsToDimension.TitleBarText = "Operator Properties";
            this.operatorPropertiesUserControl_ForInletsToDimension.ViewModel = null;
            this.operatorPropertiesUserControl_ForInletsToDimension.Visible = false;
            // 
            // audioOutputPropertiesUserControl
            // 
            this.audioOutputPropertiesUserControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.audioOutputPropertiesUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioOutputPropertiesUserControl.Font = new System.Drawing.Font("Verdana", 10F);
            this.audioOutputPropertiesUserControl.Location = new System.Drawing.Point(0, 0);
            this.audioOutputPropertiesUserControl.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.audioOutputPropertiesUserControl.Name = "audioOutputPropertiesUserControl";
            this.audioOutputPropertiesUserControl.Size = new System.Drawing.Size(553, 488);
            this.audioOutputPropertiesUserControl.TabIndex = 1;
            this.audioOutputPropertiesUserControl.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.audioOutputPropertiesUserControl.TitleBarText = "Audio Output Properties";
            this.audioOutputPropertiesUserControl.ViewModel = null;
            this.audioOutputPropertiesUserControl.Visible = false;
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
            this.operatorPropertiesUserControl_ForCache.Size = new System.Drawing.Size(553, 488);
            this.operatorPropertiesUserControl_ForCache.TabIndex = 1;
            this.operatorPropertiesUserControl_ForCache.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.operatorPropertiesUserControl_ForCache.TitleBarText = "Operator Properties";
            this.operatorPropertiesUserControl_ForCache.ViewModel = null;
            this.operatorPropertiesUserControl_ForCache.Visible = false;
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
            this.operatorPropertiesUserControl_WithInterpolation.Size = new System.Drawing.Size(553, 488);
            this.operatorPropertiesUserControl_WithInterpolation.TabIndex = 1;
            this.operatorPropertiesUserControl_WithInterpolation.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.operatorPropertiesUserControl_WithInterpolation.TitleBarText = "Operator Properties";
            this.operatorPropertiesUserControl_WithInterpolation.ViewModel = null;
            this.operatorPropertiesUserControl_WithInterpolation.Visible = false;
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
            this.nodePropertiesUserControl.Size = new System.Drawing.Size(553, 488);
            this.nodePropertiesUserControl.TabIndex = 1;
            this.nodePropertiesUserControl.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.nodePropertiesUserControl.TitleBarText = "Node Properties";
            this.nodePropertiesUserControl.ViewModel = null;
            this.nodePropertiesUserControl.Visible = false;
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
            this.operatorPropertiesUserControl_ForCurve.Size = new System.Drawing.Size(553, 488);
            this.operatorPropertiesUserControl_ForCurve.TabIndex = 1;
            this.operatorPropertiesUserControl_ForCurve.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.operatorPropertiesUserControl_ForCurve.TitleBarText = "Operator Properties";
            this.operatorPropertiesUserControl_ForCurve.ViewModel = null;
            this.operatorPropertiesUserControl_ForCurve.Visible = false;
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
            this.scalePropertiesUserControl.Size = new System.Drawing.Size(553, 488);
            this.scalePropertiesUserControl.TabIndex = 1;
            this.scalePropertiesUserControl.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.scalePropertiesUserControl.TitleBarText = "Scale Properties";
            this.scalePropertiesUserControl.ViewModel = null;
            this.scalePropertiesUserControl.Visible = false;
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
            this.operatorPropertiesUserControl_ForPatchInlet.Size = new System.Drawing.Size(553, 488);
            this.operatorPropertiesUserControl_ForPatchInlet.TabIndex = 1;
            this.operatorPropertiesUserControl_ForPatchInlet.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.operatorPropertiesUserControl_ForPatchInlet.TitleBarText = "Operator Properties";
            this.operatorPropertiesUserControl_ForPatchInlet.ViewModel = null;
            this.operatorPropertiesUserControl_ForPatchInlet.Visible = false;
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
            this.documentPropertiesUserControl.Size = new System.Drawing.Size(553, 488);
            this.documentPropertiesUserControl.TabIndex = 1;
            this.documentPropertiesUserControl.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.documentPropertiesUserControl.TitleBarText = "Document Properties";
            this.documentPropertiesUserControl.ViewModel = null;
            this.documentPropertiesUserControl.Visible = false;
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
            this.operatorPropertiesUserControl.Size = new System.Drawing.Size(553, 488);
            this.operatorPropertiesUserControl.TabIndex = 1;
            this.operatorPropertiesUserControl.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.operatorPropertiesUserControl.TitleBarText = "Operator Properties";
            this.operatorPropertiesUserControl.ViewModel = null;
            this.operatorPropertiesUserControl.Visible = false;
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
            this.patchPropertiesUserControl.Size = new System.Drawing.Size(553, 488);
            this.patchPropertiesUserControl.TabIndex = 1;
            this.patchPropertiesUserControl.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.patchPropertiesUserControl.TitleBarText = "Patch Properties";
            this.patchPropertiesUserControl.ViewModel = null;
            this.patchPropertiesUserControl.Visible = false;
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
            this.audioFileOutputPropertiesUserControl.Size = new System.Drawing.Size(553, 488);
            this.audioFileOutputPropertiesUserControl.TabIndex = 1;
            this.audioFileOutputPropertiesUserControl.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.audioFileOutputPropertiesUserControl.TitleBarText = "Title";
            this.audioFileOutputPropertiesUserControl.ViewModel = null;
            this.audioFileOutputPropertiesUserControl.Visible = false;
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
            this.operatorPropertiesUserControl_ForSample.Size = new System.Drawing.Size(553, 488);
            this.operatorPropertiesUserControl_ForSample.TabIndex = 1;
            this.operatorPropertiesUserControl_ForSample.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.operatorPropertiesUserControl_ForSample.TitleBarText = "Operator Properties";
            this.operatorPropertiesUserControl_ForSample.ViewModel = null;
            this.operatorPropertiesUserControl_ForSample.Visible = false;
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
            this.operatorPropertiesUserControl_ForPatchOutlet.Size = new System.Drawing.Size(553, 488);
            this.operatorPropertiesUserControl_ForPatchOutlet.TabIndex = 1;
            this.operatorPropertiesUserControl_ForPatchOutlet.TitleBarBackColor = System.Drawing.SystemColors.Control;
            this.operatorPropertiesUserControl_ForPatchOutlet.TitleBarText = "Operator Properties";
            this.operatorPropertiesUserControl_ForPatchOutlet.ViewModel = null;
            this.operatorPropertiesUserControl_ForPatchOutlet.Visible = false;
            // 
            // splitContainerCurvesAndTopSide
            // 
            this.splitContainerCurvesAndTopSide.Location = new System.Drawing.Point(0, 24);
            this.splitContainerCurvesAndTopSide.Name = "splitContainerCurvesAndTopSide";
            this.splitContainerCurvesAndTopSide.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerCurvesAndTopSide.Panel1
            // 
            this.splitContainerCurvesAndTopSide.Panel1.Controls.Add(this.splitContainerTreeAndRightSide);
            // 
            // splitContainerCurvesAndTopSide.Panel2
            // 
            this.splitContainerCurvesAndTopSide.Panel2.Controls.Add(this.curveDetailsListUserControl);
            this.splitContainerCurvesAndTopSide.Size = new System.Drawing.Size(1459, 749);
            this.splitContainerCurvesAndTopSide.SplitterDistance = 488;
            this.splitContainerCurvesAndTopSide.TabIndex = 16;
            // 
            // curveDetailsListUserControl
            // 
            this.curveDetailsListUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curveDetailsListUserControl.Location = new System.Drawing.Point(0, 0);
            this.curveDetailsListUserControl.Name = "curveDetailsListUserControl";
            this.curveDetailsListUserControl.Size = new System.Drawing.Size(1459, 257);
            this.curveDetailsListUserControl.TabIndex = 0;
            this.curveDetailsListUserControl.ViewModels = null;
            // 
            // monitoringBarUserControl
            // 
            this.monitoringBarUserControl.Location = new System.Drawing.Point(8, 781);
            this.monitoringBarUserControl.Name = "monitoringBarUserControl";
            this.monitoringBarUserControl.Size = new System.Drawing.Size(1439, 12);
            this.monitoringBarUserControl.TabIndex = 18;
            this.monitoringBarUserControl.ViewModel = null;
            // 
            // topBarUserControl
            // 
            this.topBarUserControl.InstrumentBarViewModel = null;
            this.topBarUserControl.Location = new System.Drawing.Point(0, 0);
            this.topBarUserControl.Name = "topBarUserControl";
            this.topBarUserControl.Size = new System.Drawing.Size(150, 0);
            this.topBarUserControl.TabIndex = 19;
            this.topBarUserControl.TopButtonBarViewModel = null;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1459, 838);
            this.Controls.Add(this.topBarUserControl);
            this.Controls.Add(this.monitoringBarUserControl);
            this.Controls.Add(this.splitContainerCurvesAndTopSide);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.splitContainerTreeAndRightSide.Panel1.ResumeLayout(false);
            this.splitContainerTreeAndRightSide.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerTreeAndRightSide)).EndInit();
            this.splitContainerTreeAndRightSide.ResumeLayout(false);
            this.splitContainerCenterAndProperties.Panel1.ResumeLayout(false);
            this.splitContainerCenterAndProperties.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCenterAndProperties)).EndInit();
            this.splitContainerCenterAndProperties.ResumeLayout(false);
            this.splitContainerCurvesAndTopSide.Panel1.ResumeLayout(false);
            this.splitContainerCurvesAndTopSide.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerCurvesAndTopSide)).EndInit();
            this.splitContainerCurvesAndTopSide.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.SplitContainer splitContainerTreeAndRightSide;
		private UserControls.DocumentGridUserControl documentGridUserControl;
		private UserControls.DocumentTreeUserControl documentTreeUserControl;
		private System.Windows.Forms.SplitContainer splitContainerCenterAndProperties;
		private UserControls.DocumentDetailsUserControl documentDetailsUserControl;
		private UserControls.DocumentPropertiesUserControl documentPropertiesUserControl;
		private UserControls.AudioFileOutputGridUserControl audioFileOutputGridUserControl;
		private UserControls.AudioFileOutputPropertiesUserControl audioFileOutputPropertiesUserControl;
		private UserControls.PatchDetailsUserControl patchDetailsUserControl;
		private UserControls.PatchPropertiesUserControl patchPropertiesUserControl;
		private UserControls.OperatorPropertiesUserControl operatorPropertiesUserControl;
		private UserControls.OperatorPropertiesUserControl_ForPatchInlet operatorPropertiesUserControl_ForPatchInlet;
		private UserControls.OperatorPropertiesUserControl_ForPatchOutlet operatorPropertiesUserControl_ForPatchOutlet;
		private UserControls.OperatorPropertiesUserControl_ForNumber operatorPropertiesUserControl_ForNumber;
		private UserControls.OperatorPropertiesUserControl_ForSample operatorPropertiesUserControl_ForSample;
		private UserControls.ToneGridEditUserControl toneGridEditUserControl;
		private UserControls.ScalePropertiesUserControl scalePropertiesUserControl;
		private UserControls.OperatorPropertiesUserControl_ForCurve operatorPropertiesUserControl_ForCurve;
		private UserControls.NodePropertiesUserControl nodePropertiesUserControl;
		private UserControls.OperatorPropertiesUserControl_WithInterpolation operatorPropertiesUserControl_WithInterpolation;
		private UserControls.OperatorPropertiesUserControl_ForCache operatorPropertiesUserControl_ForCache;
		private UserControls.AudioOutputPropertiesUserControl audioOutputPropertiesUserControl;
		private UserControls.OperatorPropertiesUserControl_ForInletsToDimension operatorPropertiesUserControl_ForInletsToDimension;
		private UserControls.OperatorPropertiesUserControl_WithCollectionRecalculation operatorPropertiesUserControl_WithCollectionRecalculation;
		private UserControls.LibraryPropertiesUserControl libraryPropertiesUserControl;
		private System.Windows.Forms.SplitContainer splitContainerCurvesAndTopSide;
		private UserControls.CurveDetailsListUserControl curveDetailsListUserControl;
		private UserControls.MidiMappingGroupDetailsUserControl midiMappingDetailsUserControl;
		private UserControls.MidiMappingPropertiesUserControl midiMappingPropertiesUserControl;
		private UserControls.MonitoringBarUserControl monitoringBarUserControl;
        private UserControls.TopBarUserControl topBarUserControl;
    }
}