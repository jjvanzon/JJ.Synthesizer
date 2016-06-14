//using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
//namespace JJ.Presentation.Synthesizer.WinForms.UserControls
//{
//    partial class OperatorPropertiesUserControl_ForAggregate
//    {
//        /// <summary> 
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary> 
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Component Designer generated code

//        /// <summary> 
//        /// Required method for Designer support - do not modify 
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
//            this.tableLayoutPanelProperties = new System.Windows.Forms.TableLayoutPanel();
//            this.labelName = new System.Windows.Forms.Label();
//            this.textBoxName = new System.Windows.Forms.TextBox();
//            this.labelOperatorTypeTitle = new System.Windows.Forms.Label();
//            this.labelOperatorTypeValue = new System.Windows.Forms.Label();
//            this.labelTimeSliceDuration = new System.Windows.Forms.Label();
//            this.labelSampleCount = new System.Windows.Forms.Label();
//            this.numericUpDownTimeSliceDuration = new System.Windows.Forms.NumericUpDown();
//            this.numericUpDownSampleCount = new System.Windows.Forms.NumericUpDown();
//            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
//            this.tableLayoutPanel2.SuspendLayout();
//            this.tableLayoutPanelProperties.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeSliceDuration)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSampleCount)).BeginInit();
//            this.SuspendLayout();
//            // 
//            // tableLayoutPanel2
//            // 
//            this.tableLayoutPanel2.ColumnCount = 1;
//            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
//            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanelProperties, 0, 1);
//            this.tableLayoutPanel2.Controls.Add(this.titleBarUserControl, 0, 0);
//            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
//            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
//            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
//            this.tableLayoutPanel2.RowCount = 2;
//            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
//            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
//            this.tableLayoutPanel2.Size = new System.Drawing.Size(10, 10);
//            this.tableLayoutPanel2.TabIndex = 8;
//            // 
//            // tableLayoutPanelProperties
//            // 
//            this.tableLayoutPanelProperties.ColumnCount = 2;
//            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 147F));
//            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
//            this.tableLayoutPanelProperties.Controls.Add(this.labelName, 0, 1);
//            this.tableLayoutPanelProperties.Controls.Add(this.textBoxName, 1, 1);
//            this.tableLayoutPanelProperties.Controls.Add(this.labelOperatorTypeTitle, 0, 0);
//            this.tableLayoutPanelProperties.Controls.Add(this.labelOperatorTypeValue, 1, 0);
//            this.tableLayoutPanelProperties.Controls.Add(this.labelTimeSliceDuration, 0, 2);
//            this.tableLayoutPanelProperties.Controls.Add(this.labelSampleCount, 0, 3);
//            this.tableLayoutPanelProperties.Controls.Add(this.numericUpDownTimeSliceDuration, 1, 2);
//            this.tableLayoutPanelProperties.Controls.Add(this.numericUpDownSampleCount, 1, 3);
//            this.tableLayoutPanelProperties.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.tableLayoutPanelProperties.Location = new System.Drawing.Point(4, 30);
//            this.tableLayoutPanelProperties.Margin = new System.Windows.Forms.Padding(4);
//            this.tableLayoutPanelProperties.Name = "tableLayoutPanelProperties";
//            this.tableLayoutPanelProperties.RowCount = 5;
//            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
//            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
//            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
//            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
//            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle());
//            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
//            this.tableLayoutPanelProperties.Size = new System.Drawing.Size(10, 10);
//            this.tableLayoutPanelProperties.TabIndex = 8;
//            // 
//            // labelName
//            // 
//            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.labelName.Location = new System.Drawing.Point(0, 30);
//            this.labelName.Margin = new System.Windows.Forms.Padding(0);
//            this.labelName.Name = "labelName";
//            this.labelName.Size = new System.Drawing.Size(147, 30);
//            this.labelName.TabIndex = 2;
//            this.labelName.Text = "labelName";
//            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
//            // 
//            // textBoxName
//            // 
//            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.textBoxName.Location = new System.Drawing.Point(147, 30);
//            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
//            this.textBoxName.Name = "textBoxName";
//            this.textBoxName.Size = new System.Drawing.Size(10, 22);
//            this.textBoxName.TabIndex = 11;
//            // 
//            // labelOperatorTypeTitle
//            // 
//            this.labelOperatorTypeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.labelOperatorTypeTitle.Location = new System.Drawing.Point(0, 0);
//            this.labelOperatorTypeTitle.Margin = new System.Windows.Forms.Padding(0);
//            this.labelOperatorTypeTitle.Name = "labelOperatorTypeTitle";
//            this.labelOperatorTypeTitle.Size = new System.Drawing.Size(147, 30);
//            this.labelOperatorTypeTitle.TabIndex = 12;
//            this.labelOperatorTypeTitle.Text = "labelOperatorTypeTitle";
//            this.labelOperatorTypeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
//            // 
//            // labelOperatorTypeValue
//            // 
//            this.labelOperatorTypeValue.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.labelOperatorTypeValue.Location = new System.Drawing.Point(147, 0);
//            this.labelOperatorTypeValue.Margin = new System.Windows.Forms.Padding(0);
//            this.labelOperatorTypeValue.Name = "labelOperatorTypeValue";
//            this.labelOperatorTypeValue.Size = new System.Drawing.Size(10, 30);
//            this.labelOperatorTypeValue.TabIndex = 13;
//            this.labelOperatorTypeValue.Text = "labelOperatorTypeValue";
//            this.labelOperatorTypeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//            // 
//            // labelTimeSliceDuration
//            // 
//            this.labelTimeSliceDuration.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.labelTimeSliceDuration.Location = new System.Drawing.Point(0, 60);
//            this.labelTimeSliceDuration.Margin = new System.Windows.Forms.Padding(0);
//            this.labelTimeSliceDuration.Name = "labelTimeSliceDuration";
//            this.labelTimeSliceDuration.Size = new System.Drawing.Size(147, 30);
//            this.labelTimeSliceDuration.TabIndex = 14;
//            this.labelTimeSliceDuration.Text = "labelTimeSliceDuration";
//            this.labelTimeSliceDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
//            // 
//            // labelSampleCount
//            // 
//            this.labelSampleCount.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.labelSampleCount.Location = new System.Drawing.Point(0, 90);
//            this.labelSampleCount.Margin = new System.Windows.Forms.Padding(0);
//            this.labelSampleCount.Name = "labelSampleCount";
//            this.labelSampleCount.Size = new System.Drawing.Size(147, 30);
//            this.labelSampleCount.TabIndex = 16;
//            this.labelSampleCount.Text = "labelSampleCount";
//            this.labelSampleCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
//            // 
//            // numericUpDownTimeSliceDuration
//            // 
//            this.numericUpDownTimeSliceDuration.DecimalPlaces = 6;
//            this.numericUpDownTimeSliceDuration.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.numericUpDownTimeSliceDuration.Increment = new decimal(new int[] {
//            1,
//            0,
//            0,
//            196608});
//            this.numericUpDownTimeSliceDuration.Location = new System.Drawing.Point(147, 60);
//            this.numericUpDownTimeSliceDuration.Margin = new System.Windows.Forms.Padding(0);
//            this.numericUpDownTimeSliceDuration.Maximum = new decimal(new int[] {
//            600,
//            0,
//            0,
//            0});
//            this.numericUpDownTimeSliceDuration.Minimum = new decimal(new int[] {
//            1,
//            0,
//            0,
//            393216});
//            this.numericUpDownTimeSliceDuration.Name = "numericUpDownTimeSliceDuration";
//            this.numericUpDownTimeSliceDuration.Size = new System.Drawing.Size(10, 22);
//            this.numericUpDownTimeSliceDuration.TabIndex = 21;
//            this.numericUpDownTimeSliceDuration.Value = new decimal(new int[] {
//            1,
//            0,
//            0,
//            0});
//            // 
//            // numericUpDownSampleCount
//            // 
//            this.numericUpDownSampleCount.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.numericUpDownSampleCount.Increment = new decimal(new int[] {
//            10,
//            0,
//            0,
//            0});
//            this.numericUpDownSampleCount.Location = new System.Drawing.Point(147, 90);
//            this.numericUpDownSampleCount.Margin = new System.Windows.Forms.Padding(0);
//            this.numericUpDownSampleCount.Maximum = new decimal(new int[] {
//            2147483647,
//            0,
//            0,
//            0});
//            this.numericUpDownSampleCount.Minimum = new decimal(new int[] {
//            1,
//            0,
//            0,
//            0});
//            this.numericUpDownSampleCount.Name = "numericUpDownSampleCount";
//            this.numericUpDownSampleCount.Size = new System.Drawing.Size(10, 22);
//            this.numericUpDownSampleCount.TabIndex = 23;
//            this.numericUpDownSampleCount.Value = new decimal(new int[] {
//            1,
//            0,
//            0,
//            0});
//            // 
//            // titleBarUserControl
//            // 
//            this.titleBarUserControl.AddButtonVisible = false;
//            this.titleBarUserControl.BackColor = System.Drawing.SystemColors.Control;
//            this.titleBarUserControl.CloseButtonVisible = true;
//            this.titleBarUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.titleBarUserControl.Location = new System.Drawing.Point(0, 0);
//            this.titleBarUserControl.Margin = new System.Windows.Forms.Padding(0);
//            this.titleBarUserControl.Name = "titleBarUserControl";
//            this.titleBarUserControl.RemoveButtonVisible = false;
//            this.titleBarUserControl.Size = new System.Drawing.Size(18, 26);
//            this.titleBarUserControl.TabIndex = 7;
//            this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
//            // 
//            // OperatorPropertiesUserControl_ForAggregate
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
//            this.BackColor = System.Drawing.SystemColors.ButtonFace;
//            this.Controls.Add(this.tableLayoutPanel2);
//            this.Margin = new System.Windows.Forms.Padding(4);
//            this.Name = "OperatorPropertiesUserControl_ForAggregate";
//            this.Size = new System.Drawing.Size(10, 10);
//            this.Load += new System.EventHandler(this.OperatorPropertiesUserControl_ForAggregate_Load);
//            this.Leave += new System.EventHandler(this.OperatorPropertiesUserControl_ForAggregate_Leave);
//            this.tableLayoutPanel2.ResumeLayout(false);
//            this.tableLayoutPanelProperties.ResumeLayout(false);
//            this.tableLayoutPanelProperties.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeSliceDuration)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSampleCount)).EndInit();
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
//        private TitleBarUserControl titleBarUserControl;
//        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProperties;
//        private System.Windows.Forms.Label labelName;
//        private System.Windows.Forms.TextBox textBoxName;
//        private System.Windows.Forms.Label labelOperatorTypeTitle;
//        private System.Windows.Forms.Label labelOperatorTypeValue;
//        private System.Windows.Forms.Label labelTimeSliceDuration;
//        private System.Windows.Forms.Label labelSampleCount;
//        private System.Windows.Forms.NumericUpDown numericUpDownTimeSliceDuration;
//        private System.Windows.Forms.NumericUpDown numericUpDownSampleCount;
//    }
//}
