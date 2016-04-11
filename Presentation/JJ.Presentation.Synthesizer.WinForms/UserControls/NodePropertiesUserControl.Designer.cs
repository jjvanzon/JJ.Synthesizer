using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class NodePropertiesUserControl
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
            this.labelNodeType = new System.Windows.Forms.Label();
            this.comboBoxNodeType = new System.Windows.Forms.ComboBox();
            this.labelY = new System.Windows.Forms.Label();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.labelX = new System.Windows.Forms.Label();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
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
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(10, 10);
            this.tableLayoutPanelMain.TabIndex = 8;
            // 
            // tableLayoutPanelProperties
            // 
            this.tableLayoutPanelProperties.ColumnCount = 2;
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanelProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelProperties.Controls.Add(this.comboBoxNodeType, 1, 2);
            this.tableLayoutPanelProperties.Controls.Add(this.labelY, 0, 1);
            this.tableLayoutPanelProperties.Controls.Add(this.numericUpDownX, 1, 0);
            this.tableLayoutPanelProperties.Controls.Add(this.numericUpDownY, 1, 1);
            this.tableLayoutPanelProperties.Controls.Add(this.labelNodeType, 0, 2);
            this.tableLayoutPanelProperties.Controls.Add(this.labelX, 0, 0);
            this.tableLayoutPanelProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelProperties.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanelProperties.Name = "tableLayoutPanelProperties";
            this.tableLayoutPanelProperties.RowCount = 4;
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanelProperties.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelProperties.Size = new System.Drawing.Size(10, 10);
            this.tableLayoutPanelProperties.TabIndex = 8;
            // 
            // labelNodeType
            // 
            this.labelNodeType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNodeType.Location = new System.Drawing.Point(0, 48);
            this.labelNodeType.Margin = new System.Windows.Forms.Padding(0);
            this.labelNodeType.Name = "labelNodeType";
            this.labelNodeType.Size = new System.Drawing.Size(120, 24);
            this.labelNodeType.TabIndex = 15;
            this.labelNodeType.Text = "labelNodeType";
            this.labelNodeType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxNodeType
            // 
            this.comboBoxNodeType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxNodeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNodeType.FormattingEnabled = true;
            this.comboBoxNodeType.Location = new System.Drawing.Point(120, 48);
            this.comboBoxNodeType.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxNodeType.Name = "comboBoxNodeType";
            this.comboBoxNodeType.Size = new System.Drawing.Size(10, 21);
            this.comboBoxNodeType.TabIndex = 16;
            // 
            // labelY
            // 
            this.labelY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelY.Location = new System.Drawing.Point(0, 24);
            this.labelY.Margin = new System.Windows.Forms.Padding(0);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(120, 24);
            this.labelY.TabIndex = 2;
            this.labelY.Text = "labelName";
            this.labelY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.DecimalPlaces = 6;
            this.numericUpDownX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownX.Location = new System.Drawing.Point(120, 0);
            this.numericUpDownX.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownX.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownX.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(10, 20);
            this.numericUpDownX.TabIndex = 21;
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.DecimalPlaces = 6;
            this.numericUpDownY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownY.Location = new System.Drawing.Point(120, 24);
            this.numericUpDownY.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownY.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownY.Minimum = new decimal(new int[] {
            1000000000,
            0,
            0,
            -2147483648});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(10, 20);
            this.numericUpDownY.TabIndex = 22;
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
            this.titleBarUserControl.Size = new System.Drawing.Size(16, 21);
            this.titleBarUserControl.TabIndex = 7;
            this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
            // 
            // labelX
            // 
            this.labelX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelX.Location = new System.Drawing.Point(0, 0);
            this.labelX.Margin = new System.Windows.Forms.Padding(0);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(120, 24);
            this.labelX.TabIndex = 23;
            this.labelX.Text = "labelX";
            this.labelX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // NodePropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "NodePropertiesUserControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.Load += new System.EventHandler(this.NodePropertiesUserControl_Load);
            this.Leave += new System.EventHandler(this.NodePropertiesUserControl_Leave);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private TitleBarUserControl titleBarUserControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProperties;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelNodeType;
        private System.Windows.Forms.ComboBox comboBoxNodeType;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.Label labelX;
    }
}
