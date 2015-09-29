using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class ScalePropertiesUserControl
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
            this.labelScaleType = new System.Windows.Forms.Label();
            this.comboBoxScaleType = new System.Windows.Forms.ComboBox();
            this.labelBaseFrequency = new System.Windows.Forms.Label();
            this.numericUpDownBaseFrequency = new System.Windows.Forms.NumericUpDown();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBaseFrequency)).BeginInit();
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
            this.tableLayoutPanelProperties.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanelProperties.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanelProperties.Controls.Add(this.labelScaleType, 0, 1);
            this.tableLayoutPanelProperties.Controls.Add(this.comboBoxScaleType, 1, 1);
            this.tableLayoutPanelProperties.Controls.Add(this.labelBaseFrequency, 0, 2);
            this.tableLayoutPanelProperties.Controls.Add(this.numericUpDownBaseFrequency, 1, 2);
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
            // labelName
            // 
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Location = new System.Drawing.Point(0, 0);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(120, 24);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(120, 0);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(10, 20);
            this.textBoxName.TabIndex = 11;
            // 
            // labelScaleType
            // 
            this.labelScaleType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScaleType.Location = new System.Drawing.Point(0, 24);
            this.labelScaleType.Margin = new System.Windows.Forms.Padding(0);
            this.labelScaleType.Name = "labelScaleType";
            this.labelScaleType.Size = new System.Drawing.Size(120, 24);
            this.labelScaleType.TabIndex = 17;
            this.labelScaleType.Text = "labelScaleType";
            this.labelScaleType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxScaleType
            // 
            this.comboBoxScaleType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxScaleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScaleType.FormattingEnabled = true;
            this.comboBoxScaleType.Location = new System.Drawing.Point(120, 24);
            this.comboBoxScaleType.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxScaleType.Name = "comboBoxScaleType";
            this.comboBoxScaleType.Size = new System.Drawing.Size(10, 21);
            this.comboBoxScaleType.TabIndex = 16;
            // 
            // labelBaseFrequency
            // 
            this.labelBaseFrequency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBaseFrequency.Location = new System.Drawing.Point(0, 48);
            this.labelBaseFrequency.Margin = new System.Windows.Forms.Padding(0);
            this.labelBaseFrequency.Name = "labelBaseFrequency";
            this.labelBaseFrequency.Size = new System.Drawing.Size(120, 24);
            this.labelBaseFrequency.TabIndex = 18;
            this.labelBaseFrequency.Text = "labelBaseFrequency";
            this.labelBaseFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericUpDownBaseFrequency
            // 
            this.numericUpDownBaseFrequency.DecimalPlaces = 6;
            this.numericUpDownBaseFrequency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDownBaseFrequency.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownBaseFrequency.Location = new System.Drawing.Point(120, 48);
            this.numericUpDownBaseFrequency.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownBaseFrequency.Maximum = new decimal(new int[] {
            25000,
            0,
            0,
            0});
            this.numericUpDownBaseFrequency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBaseFrequency.Name = "numericUpDownBaseFrequency";
            this.numericUpDownBaseFrequency.Size = new System.Drawing.Size(10, 20);
            this.numericUpDownBaseFrequency.TabIndex = 20;
            this.numericUpDownBaseFrequency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            // ScalePropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Name = "ScalePropertiesUserControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.Load += new System.EventHandler(this.ScalePropertiesUserControl_Load);
            this.Leave += new System.EventHandler(this.ScalePropertiesUserControl_Leave);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelProperties.ResumeLayout(false);
            this.tableLayoutPanelProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBaseFrequency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private TitleBarUserControl titleBarUserControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelProperties;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ComboBox comboBoxScaleType;
        private System.Windows.Forms.Label labelScaleType;
        private System.Windows.Forms.Label labelBaseFrequency;
        private System.Windows.Forms.NumericUpDown numericUpDownBaseFrequency;
    }
}
