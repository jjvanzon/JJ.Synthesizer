namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class PatchDetailsUserControl
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

            UnbindSvgEvents();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.diagramControl1 = new JJ.Framework.Presentation.WinForms.DiagramControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.tableLayoutPanelTitleAndContent = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelToolboxAndPatch = new System.Windows.Forms.TableLayoutPanel();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.tableLayoutPanelPlayButtonAndValueTextBox = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelTitleAndContent.SuspendLayout();
            this.tableLayoutPanelToolboxAndPatch.SuspendLayout();
            this.tableLayoutPanelPlayButtonAndValueTextBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // diagramControl1
            // 
            this.diagramControl1.Diagram = null;
            this.diagramControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagramControl1.Location = new System.Drawing.Point(220, 0);
            this.diagramControl1.Margin = new System.Windows.Forms.Padding(0);
            this.diagramControl1.Name = "diagramControl1";
            this.diagramControl1.Size = new System.Drawing.Size(493, 267);
            this.diagramControl1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Font = new System.Drawing.Font("Verdana", 10F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(220, 267);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // textBoxValue
            // 
            this.textBoxValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxValue.Location = new System.Drawing.Point(104, 0);
            this.textBoxValue.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(613, 23);
            this.textBoxValue.TabIndex = 4;
            this.textBoxValue.TextChanged += new System.EventHandler(this.textBoxValue_TextChanged);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPlay.Location = new System.Drawing.Point(0, 0);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(100, 36);
            this.buttonPlay.TabIndex = 3;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // tableLayoutPanelTitleAndContent
            // 
            this.tableLayoutPanelTitleAndContent.ColumnCount = 1;
            this.tableLayoutPanelTitleAndContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelTitleAndContent.Controls.Add(this.tableLayoutPanelToolboxAndPatch, 0, 2);
            this.tableLayoutPanelTitleAndContent.Controls.Add(this.titleBarUserControl, 0, 0);
            this.tableLayoutPanelTitleAndContent.Controls.Add(this.tableLayoutPanelPlayButtonAndValueTextBox, 0, 1);
            this.tableLayoutPanelTitleAndContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelTitleAndContent.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelTitleAndContent.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelTitleAndContent.Name = "tableLayoutPanelTitleAndContent";
            this.tableLayoutPanelTitleAndContent.RowCount = 3;
            this.tableLayoutPanelTitleAndContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanelTitleAndContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanelTitleAndContent.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelTitleAndContent.Size = new System.Drawing.Size(713, 325);
            this.tableLayoutPanelTitleAndContent.TabIndex = 5;
            // 
            // tableLayoutPanelToolboxAndPatch
            // 
            this.tableLayoutPanelToolboxAndPatch.ColumnCount = 2;
            this.tableLayoutPanelToolboxAndPatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanelToolboxAndPatch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelToolboxAndPatch.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanelToolboxAndPatch.Controls.Add(this.diagramControl1, 1, 0);
            this.tableLayoutPanelToolboxAndPatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelToolboxAndPatch.Location = new System.Drawing.Point(0, 62);
            this.tableLayoutPanelToolboxAndPatch.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelToolboxAndPatch.Name = "tableLayoutPanelToolboxAndPatch";
            this.tableLayoutPanelToolboxAndPatch.RowCount = 1;
            this.tableLayoutPanelToolboxAndPatch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelToolboxAndPatch.Size = new System.Drawing.Size(713, 267);
            this.tableLayoutPanelToolboxAndPatch.TabIndex = 2;
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
            this.titleBarUserControl.Size = new System.Drawing.Size(713, 26);
            this.titleBarUserControl.TabIndex = 0;
            this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
            // 
            // tableLayoutPanelPlayButtonAndValueTextBox
            // 
            this.tableLayoutPanelPlayButtonAndValueTextBox.ColumnCount = 2;
            this.tableLayoutPanelPlayButtonAndValueTextBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelPlayButtonAndValueTextBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelPlayButtonAndValueTextBox.Controls.Add(this.buttonPlay, 0, 0);
            this.tableLayoutPanelPlayButtonAndValueTextBox.Controls.Add(this.textBoxValue, 1, 0);
            this.tableLayoutPanelPlayButtonAndValueTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelPlayButtonAndValueTextBox.Location = new System.Drawing.Point(0, 26);
            this.tableLayoutPanelPlayButtonAndValueTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelPlayButtonAndValueTextBox.Name = "tableLayoutPanelPlayButtonAndValueTextBox";
            this.tableLayoutPanelPlayButtonAndValueTextBox.RowCount = 1;
            this.tableLayoutPanelPlayButtonAndValueTextBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPlayButtonAndValueTextBox.Size = new System.Drawing.Size(713, 36);
            this.tableLayoutPanelPlayButtonAndValueTextBox.TabIndex = 1;
            // 
            // PatchDetailsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanelTitleAndContent);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PatchDetailsUserControl";
            this.Size = new System.Drawing.Size(713, 325);
            this.tableLayoutPanelTitleAndContent.ResumeLayout(false);
            this.tableLayoutPanelToolboxAndPatch.ResumeLayout(false);
            this.tableLayoutPanelPlayButtonAndValueTextBox.ResumeLayout(false);
            this.tableLayoutPanelPlayButtonAndValueTextBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Presentation.WinForms.DiagramControl diagramControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelTitleAndContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelToolboxAndPatch;
        private Partials.TitleBarUserControl titleBarUserControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPlayButtonAndValueTextBox;
    }
}