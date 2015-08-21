using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class ChildDocumentPropertiesUserControl
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxChildDocumentType = new System.Windows.Forms.ComboBox();
            this.labelChildDocumentType = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.labelMainPatch = new System.Windows.Forms.Label();
            this.comboBoxMainPatch = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.titleBarUserControl, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(10, 10);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.comboBoxChildDocumentType, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelChildDocumentType, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelName, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.textBoxName, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.labelMainPatch, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.comboBoxMainPatch, 1, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(10, 10);
            this.tableLayoutPanel.TabIndex = 8;
            // 
            // comboBoxChildDocumentType
            // 
            this.comboBoxChildDocumentType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxChildDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChildDocumentType.FormattingEnabled = true;
            this.comboBoxChildDocumentType.Location = new System.Drawing.Point(120, 24);
            this.comboBoxChildDocumentType.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxChildDocumentType.Name = "comboBoxChildDocumentType";
            this.comboBoxChildDocumentType.Size = new System.Drawing.Size(10, 21);
            this.comboBoxChildDocumentType.TabIndex = 14;
            // 
            // labelChildDocumentType
            // 
            this.labelChildDocumentType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelChildDocumentType.Location = new System.Drawing.Point(0, 24);
            this.labelChildDocumentType.Margin = new System.Windows.Forms.Padding(0);
            this.labelChildDocumentType.Name = "labelChildDocumentType";
            this.labelChildDocumentType.Size = new System.Drawing.Size(120, 24);
            this.labelChildDocumentType.TabIndex = 12;
            this.labelChildDocumentType.Text = "labelChildDocumentType";
            this.labelChildDocumentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            // titleBarUserControl
            // 
            this.titleBarUserControl.AddButtonVisible = false;
            this.titleBarUserControl.BackColor = System.Drawing.SystemColors.Control;
            this.titleBarUserControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            // labelMainPatch
            // 
            this.labelMainPatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMainPatch.Location = new System.Drawing.Point(0, 48);
            this.labelMainPatch.Margin = new System.Windows.Forms.Padding(0);
            this.labelMainPatch.Name = "labelMainPatch";
            this.labelMainPatch.Size = new System.Drawing.Size(120, 24);
            this.labelMainPatch.TabIndex = 15;
            this.labelMainPatch.Text = "labelMainPatch";
            this.labelMainPatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxMainPatch
            // 
            this.comboBoxMainPatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxMainPatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMainPatch.FormattingEnabled = true;
            this.comboBoxMainPatch.Location = new System.Drawing.Point(120, 48);
            this.comboBoxMainPatch.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxMainPatch.Name = "comboBoxMainPatch";
            this.comboBoxMainPatch.Size = new System.Drawing.Size(10, 21);
            this.comboBoxMainPatch.TabIndex = 16;
            // 
            // ChildDocumentPropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "ChildDocumentPropertiesUserControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.VisibleChanged += new System.EventHandler(this.ChildDocumentPropertiesUserControl_VisibleChanged);
            this.Enter += new System.EventHandler(this.ChildDocumentPropertiesUserControl_Enter);
            this.Leave += new System.EventHandler(this.ChildDocumentPropertiesUserControl_Leave);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private TitleBarUserControl titleBarUserControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelChildDocumentType;
        private System.Windows.Forms.ComboBox comboBoxChildDocumentType;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelMainPatch;
        private System.Windows.Forms.ComboBox comboBoxMainPatch;
    }
}
