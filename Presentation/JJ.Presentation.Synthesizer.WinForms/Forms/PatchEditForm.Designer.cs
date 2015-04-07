namespace JJ.Presentation.Synthesizer.WinForms
{
    partial class PatchEditForm
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

            TryUnbindSvgEvents();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.diagramControl1 = new JJ.Framework.Presentation.WinForms.DiagramControl();
            this.labelSavedMessage = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SuspendLayout();
            // 
            // diagramControl1
            // 
            this.diagramControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.diagramControl1.Diagram = null;
            this.diagramControl1.Location = new System.Drawing.Point(184, 0);
            this.diagramControl1.Margin = new System.Windows.Forms.Padding(4);
            this.diagramControl1.Name = "diagramControl1";
            this.diagramControl1.Size = new System.Drawing.Size(917, 642);
            this.diagramControl1.TabIndex = 0;
            // 
            // labelSavedMessage
            // 
            this.labelSavedMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSavedMessage.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSavedMessage.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelSavedMessage.Location = new System.Drawing.Point(266, 646);
            this.labelSavedMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSavedMessage.Name = "labelSavedMessage";
            this.labelSavedMessage.Size = new System.Drawing.Size(835, 20);
            this.labelSavedMessage.TabIndex = 1;
            this.labelSavedMessage.Text = "Saved";
            this.labelSavedMessage.Visible = false;
            this.labelSavedMessage.Click += new System.EventHandler(this.labelSavedMessage_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSave.Location = new System.Drawing.Point(184, 643);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Font = new System.Drawing.Font("Verdana", 13F);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(180, 666);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // PatchEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 666);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelSavedMessage);
            this.Controls.Add(this.diagramControl1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PatchEditForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.Presentation.WinForms.DiagramControl diagramControl1;
        private System.Windows.Forms.Label labelSavedMessage;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}