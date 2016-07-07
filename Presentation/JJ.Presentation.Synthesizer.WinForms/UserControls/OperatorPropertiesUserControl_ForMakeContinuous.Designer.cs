using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_ForMakeContinuous
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
            this.tableLayoutPanelProperties = new System.Windows.Forms.TableLayoutPanel();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelOperatorTypeTitle = new System.Windows.Forms.Label();
            this.labelOperatorTypeValue = new System.Windows.Forms.Label();
            this.labelInletCount = new System.Windows.Forms.Label();
            this.numericUpDownInletCount = new System.Windows.Forms.NumericUpDown();
            this.comboBoxDimension = new System.Windows.Forms.ComboBox();
            this.labelDimension = new System.Windows.Forms.Label();
            this.labelInterpolation = new System.Windows.Forms.Label();
            this.comboBoxInterpolation = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInletCount)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelProperties
            // 
            this.tableLayoutPanelProperties.ColumnCount = 2;
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 147F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelProperties.Controls.Add(this.labelName, 0, 1);
            this.tableLayoutPanelProperties.Controls.Add(this.textBoxName, 1, 1);
            this.tableLayoutPanelProperties.Controls.Add(this.labelOperatorTypeTitle, 0, 0);
            this.tableLayoutPanelProperties.Controls.Add(this.labelOperatorTypeValue, 1, 0);
            this.tableLayoutPanelProperties.Controls.Add(this.labelInletCount, 0, 2);
            this.tableLayoutPanelProperties.Controls.Add(this.numericUpDownInletCount, 1, 2);
            this.tableLayoutPanelProperties.Controls.Add(this.comboBoxDimension, 1, 4);
            this.tableLayoutPanelProperties.Controls.Add(this.labelDimension, 0, 4);
            this.tableLayoutPanelProperties.Controls.Add(this.labelInterpolation, 0, 3);
            this.tableLayoutPanelProperties.Controls.Add(this.comboBoxInterpolation, 1, 3);
            this.tableLayoutPanelProperties.Location = new System.Drawing.Point(4, 30);
            this.tableLayoutPanelProperties.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanelProperties.Name = "tableLayoutPanelProperties";
            this.tableLayoutPanelProperties.RowCount = 6;
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelProperties.Size = new System.Drawing.Size(10, 10);
            this.tableLayoutPanelProperties.TabIndex = 8;
            // 
            // labelName
            // 
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Location = new System.Drawing.Point(0, 30);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(147, 30);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(147, 30);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(10, 22);
            this.textBoxName.TabIndex = 11;
            // 
            // labelOperatorTypeTitle
            // 
            this.labelOperatorTypeTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOperatorTypeTitle.Location = new System.Drawing.Point(0, 0);
            this.labelOperatorTypeTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelOperatorTypeTitle.Name = "labelOperatorTypeTitle";
            this.labelOperatorTypeTitle.Size = new System.Drawing.Size(147, 30);
            this.labelOperatorTypeTitle.TabIndex = 12;
            this.labelOperatorTypeTitle.Text = "labelOperatorTypeTitle";
            this.labelOperatorTypeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelOperatorTypeValue
            // 
            this.labelOperatorTypeValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOperatorTypeValue.Location = new System.Drawing.Point(147, 0);
            this.labelOperatorTypeValue.Margin = new System.Windows.Forms.Padding(0);
            this.labelOperatorTypeValue.Name = "labelOperatorTypeValue";
            this.labelOperatorTypeValue.Size = new System.Drawing.Size(10, 30);
            this.labelOperatorTypeValue.TabIndex = 13;
            this.labelOperatorTypeValue.Text = "labelOperatorTypeValue";
            this.labelOperatorTypeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelInletCount
            // 
            this.labelInletCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInletCount.Location = new System.Drawing.Point(0, 60);
            this.labelInletCount.Margin = new System.Windows.Forms.Padding(0);
            this.labelInletCount.Name = "labelInletCount";
            this.labelInletCount.Size = new System.Drawing.Size(147, 30);
            this.labelInletCount.TabIndex = 14;
            this.labelInletCount.Text = "labelInletCount";
            this.labelInletCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownInletCount
            // 
            this.numericUpDownInletCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownInletCount.Location = new System.Drawing.Point(147, 60);
            this.numericUpDownInletCount.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownInletCount.Maximum = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.numericUpDownInletCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownInletCount.Name = "numericUpDownInletCount";
            this.numericUpDownInletCount.Size = new System.Drawing.Size(10, 22);
            this.numericUpDownInletCount.TabIndex = 15;
            this.numericUpDownInletCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // comboBoxDimension
            // 
            this.comboBoxDimension.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxDimension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDimension.FormattingEnabled = true;
            this.comboBoxDimension.Location = new System.Drawing.Point(147, 120);
            this.comboBoxDimension.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxDimension.Name = "comboBoxDimension";
            this.comboBoxDimension.Size = new System.Drawing.Size(10, 24);
            this.comboBoxDimension.TabIndex = 23;
            // 
            // labelDimension
            // 
            this.labelDimension.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDimension.Location = new System.Drawing.Point(0, 120);
            this.labelDimension.Margin = new System.Windows.Forms.Padding(0);
            this.labelDimension.Name = "labelDimension";
            this.labelDimension.Size = new System.Drawing.Size(147, 30);
            this.labelDimension.TabIndex = 22;
            this.labelDimension.Text = "labelDimension";
            this.labelDimension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelInterpolation
            // 
            this.labelInterpolation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInterpolation.Location = new System.Drawing.Point(0, 90);
            this.labelInterpolation.Margin = new System.Windows.Forms.Padding(0);
            this.labelInterpolation.Name = "labelInterpolation";
            this.labelInterpolation.Size = new System.Drawing.Size(147, 30);
            this.labelInterpolation.TabIndex = 24;
            this.labelInterpolation.Text = "labelInterpolation";
            this.labelInterpolation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxInterpolation
            // 
            this.comboBoxInterpolation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxInterpolation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInterpolation.FormattingEnabled = true;
            this.comboBoxInterpolation.Location = new System.Drawing.Point(147, 90);
            this.comboBoxInterpolation.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxInterpolation.Name = "comboBoxInterpolation";
            this.comboBoxInterpolation.Size = new System.Drawing.Size(10, 24);
            this.comboBoxInterpolation.TabIndex = 25;
            // 
            // OperatorPropertiesUserControl_ForMakeContinuous
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanelProperties);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "OperatorPropertiesUserControl_ForMakeContinuous";
            this.Size = new System.Drawing.Size(234, 185);
            this.Controls.SetChildIndex(this.tableLayoutPanelProperties, 0);
            this.tableLayoutPanelProperties.ResumeLayout(false);
            this.tableLayoutPanelProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInletCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProperties;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelOperatorTypeTitle;
        private System.Windows.Forms.Label labelOperatorTypeValue;
        private System.Windows.Forms.Label labelInletCount;
        private System.Windows.Forms.NumericUpDown numericUpDownInletCount;
        private System.Windows.Forms.Label labelDimension;
        private System.Windows.Forms.ComboBox comboBoxDimension;
        private System.Windows.Forms.Label labelInterpolation;
        private System.Windows.Forms.ComboBox comboBoxInterpolation;
    }
}
