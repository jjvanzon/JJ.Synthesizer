using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_WithDimensionAndRecalculation
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
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelProperties = new System.Windows.Forms.TableLayoutPanel();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelOperatorTypeTitle = new System.Windows.Forms.Label();
            this.labelOperatorTypeValue = new System.Windows.Forms.Label();
            this.labelRecalculation = new System.Windows.Forms.Label();
            this.comboBoxRecalculation = new System.Windows.Forms.ComboBox();
            this.labelDimension = new System.Windows.Forms.Label();
            this.comboBoxDimension = new System.Windows.Forms.ComboBox();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelProperties.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelProperties, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.titleBarUserControl, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(10, 10);
            this.tableLayoutPanelMain.TabIndex = 8;
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
            this.tableLayoutPanelProperties.Controls.Add(this.labelRecalculation, 0, 2);
            this.tableLayoutPanelProperties.Controls.Add(this.comboBoxRecalculation, 1, 2);
            this.tableLayoutPanelProperties.Controls.Add(this.labelDimension, 0, 3);
            this.tableLayoutPanelProperties.Controls.Add(this.comboBoxDimension, 1, 3);
            this.tableLayoutPanelProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelProperties.Location = new System.Drawing.Point(4, 30);
            this.tableLayoutPanelProperties.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanelProperties.Name = "tableLayoutPanelProperties";
            this.tableLayoutPanelProperties.RowCount = 5;
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
            // labelRecalculation
            // 
            this.labelRecalculation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRecalculation.Location = new System.Drawing.Point(0, 60);
            this.labelRecalculation.Margin = new System.Windows.Forms.Padding(0);
            this.labelRecalculation.Name = "labelRecalculation";
            this.labelRecalculation.Size = new System.Drawing.Size(147, 30);
            this.labelRecalculation.TabIndex = 20;
            this.labelRecalculation.Text = "labelRecalculation";
            this.labelRecalculation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxRecalculation
            // 
            this.comboBoxRecalculation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxRecalculation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRecalculation.FormattingEnabled = true;
            this.comboBoxRecalculation.Location = new System.Drawing.Point(147, 60);
            this.comboBoxRecalculation.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxRecalculation.Name = "comboBoxRecalculation";
            this.comboBoxRecalculation.Size = new System.Drawing.Size(10, 24);
            this.comboBoxRecalculation.TabIndex = 21;
            // 
            // labelDimension
            // 
            this.labelDimension.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDimension.Location = new System.Drawing.Point(0, 90);
            this.labelDimension.Margin = new System.Windows.Forms.Padding(0);
            this.labelDimension.Name = "labelDimension";
            this.labelDimension.Size = new System.Drawing.Size(147, 30);
            this.labelDimension.TabIndex = 30;
            this.labelDimension.Text = "labelDimension";
            this.labelDimension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxDimension
            // 
            this.comboBoxDimension.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxDimension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDimension.FormattingEnabled = true;
            this.comboBoxDimension.Location = new System.Drawing.Point(147, 90);
            this.comboBoxDimension.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxDimension.Name = "comboBoxDimension";
            this.comboBoxDimension.Size = new System.Drawing.Size(10, 24);
            this.comboBoxDimension.TabIndex = 31;
            // 
            // titleBarUserControl
            // 
            this.titleBarUserControl.AddButtonVisible = false;
            this.titleBarUserControl.BackColor = System.Drawing.SystemColors.Control;
            this.titleBarUserControl.CloseButtonVisible = true;
            this.titleBarUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleBarUserControl.Location = new System.Drawing.Point(0, 0);
            this.titleBarUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.titleBarUserControl.Name = "titleBarUserControl";
            this.titleBarUserControl.RemoveButtonVisible = false;
            this.titleBarUserControl.Size = new System.Drawing.Size(18, 26);
            this.titleBarUserControl.TabIndex = 7;
            this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
            // 
            // OperatorPropertiesUserControl_WithDimensionAndRecalculation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OperatorPropertiesUserControl_WithDimensionAndRecalculation";
            this.Size = new System.Drawing.Size(10, 10);
            this.Load += new System.EventHandler(this.OperatorPropertiesUserControl_WithDimensionAndRecalculation_Load);
            this.Leave += new System.EventHandler(this.OperatorPropertiesUserControl_WithDimensionAndRecalculation_Leave);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelProperties.ResumeLayout(false);
            this.tableLayoutPanelProperties.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private TitleBarUserControl titleBarUserControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProperties;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelOperatorTypeTitle;
        private System.Windows.Forms.Label labelOperatorTypeValue;
        private System.Windows.Forms.Label labelRecalculation;
        private System.Windows.Forms.ComboBox comboBoxRecalculation;
        private System.Windows.Forms.Label labelDimension;
        private System.Windows.Forms.ComboBox comboBoxDimension;
    }
}
