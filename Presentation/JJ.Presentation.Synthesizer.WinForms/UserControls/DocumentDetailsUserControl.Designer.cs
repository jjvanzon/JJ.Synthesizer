using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class DocumentDetailsUserControl
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelName = new System.Windows.Forms.Label();
            this.labelIDTitle = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelIDValue = new System.Windows.Forms.Label();
            this.titleBarUserControl1 = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.titleBarUserControl1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonSave, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(430, 336);
            this.tableLayoutPanel2.TabIndex = 8;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.labelName, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelIDTitle, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.textBoxName, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.labelIDValue, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 27);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(424, 276);
            this.tableLayoutPanel.TabIndex = 8;
            // 
            // labelName
            // 
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Location = new System.Drawing.Point(0, 24);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(80, 24);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelIDTitle
            // 
            this.labelIDTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelIDTitle.Location = new System.Drawing.Point(0, 0);
            this.labelIDTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelIDTitle.Name = "labelIDTitle";
            this.labelIDTitle.Size = new System.Drawing.Size(80, 24);
            this.labelIDTitle.TabIndex = 0;
            this.labelIDTitle.Text = "labelIDTitle";
            this.labelIDTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(80, 24);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(344, 20);
            this.textBoxName.TabIndex = 11;
            // 
            // labelIDValue
            // 
            this.labelIDValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelIDValue.Location = new System.Drawing.Point(80, 0);
            this.labelIDValue.Margin = new System.Windows.Forms.Padding(0);
            this.labelIDValue.Name = "labelIDValue";
            this.labelIDValue.Size = new System.Drawing.Size(344, 24);
            this.labelIDValue.TabIndex = 1;
            this.labelIDValue.Text = "Bla";
            this.labelIDValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // titleBarUserControl1
            // 
            this.titleBarUserControl1.AddButtonVisible = false;
            this.titleBarUserControl1.BackColor = System.Drawing.SystemColors.Control;
            this.titleBarUserControl1.CloseButtonVisible = true;
            this.titleBarUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleBarUserControl1.Location = new System.Drawing.Point(0, 0);
            this.titleBarUserControl1.Margin = new System.Windows.Forms.Padding(0);
            this.titleBarUserControl1.Name = "titleBarUserControl1";
            this.titleBarUserControl1.RemoveButtonVisible = false;
            this.titleBarUserControl1.Size = new System.Drawing.Size(430, 24);
            this.titleBarUserControl1.TabIndex = 7;
            this.titleBarUserControl1.CloseClicked += new System.EventHandler(this.titleBarUserControl1_CloseClicked);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(3, 309);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "buttonSave";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // DocumentDetailsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "DocumentDetailsUserControl";
            this.Size = new System.Drawing.Size(430, 336);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private TitleBarUserControl titleBarUserControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelIDValue;
        private System.Windows.Forms.Label labelIDTitle;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonSave;
    }
}
