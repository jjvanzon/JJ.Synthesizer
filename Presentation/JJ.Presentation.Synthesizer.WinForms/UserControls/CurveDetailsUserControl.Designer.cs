namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class CurveDetailsUserControl
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.titleBarUserControl = new JJ.Presentation.Synthesizer.WinForms.UserControls.Partials.TitleBarUserControl();
            this.diagramControl = new JJ.Framework.Presentation.WinForms.Controls.DiagramControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.titleBarUserControl, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.diagramControl, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(10, 10);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // titleBarUserControl
            // 
            this.titleBarUserControl.AddButtonVisible = true;
            this.titleBarUserControl.BackColor = System.Drawing.SystemColors.Control;
            this.titleBarUserControl.CloseButtonVisible = true;
            this.titleBarUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleBarUserControl.Location = new System.Drawing.Point(0, 0);
            this.titleBarUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.titleBarUserControl.Name = "titleBarUserControl";
            this.titleBarUserControl.RemoveButtonVisible = true;
            this.titleBarUserControl.Size = new System.Drawing.Size(10, 26);
            this.titleBarUserControl.TabIndex = 0;
            this.titleBarUserControl.CloseClicked += new System.EventHandler(this.titleBarUserControl_CloseClicked);
            this.titleBarUserControl.RemoveClicked += new System.EventHandler(this.titleBarUserControl_RemoveClicked);
            this.titleBarUserControl.AddClicked += new System.EventHandler(this.titleBarUserControl_AddClicked);
            // 
            // diagramControl
            // 
            this.diagramControl.Diagram = null;
            this.diagramControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diagramControl.Location = new System.Drawing.Point(0, 26);
            this.diagramControl.Margin = new System.Windows.Forms.Padding(0);
            this.diagramControl.Name = "diagramControl";
            this.diagramControl.Size = new System.Drawing.Size(10, 10);
            this.diagramControl.TabIndex = 1;
            // 
            // CurveDetailsUserControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "CurveDetailsUserControl";
            this.Size = new System.Drawing.Size(10, 10);
            this.Load += new System.EventHandler(this.CurveDetailsUserControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CurveDetailsUserControl_Paint);
            this.Leave += new System.EventHandler(this.CurveDetailsUserControl_Leave);
            this.Resize += new System.EventHandler(this.CurveDetailsUserControl_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Partials.TitleBarUserControl titleBarUserControl;
        private Framework.Presentation.WinForms.Controls.DiagramControl diagramControl;
    }
}
