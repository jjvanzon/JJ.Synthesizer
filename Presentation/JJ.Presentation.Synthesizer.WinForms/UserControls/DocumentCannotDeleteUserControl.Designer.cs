using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class DocumentCannotDeleteUserControl
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
            this.labelCannotDeleteObject = new System.Windows.Forms.Label();
            this.labelMessagesTitle = new System.Windows.Forms.Label();
            this.labelMessageList = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelCannotDeleteObject
            // 
            this.labelCannotDeleteObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCannotDeleteObject.Location = new System.Drawing.Point(3, 0);
            this.labelCannotDeleteObject.Name = "labelCannotDeleteObject";
            this.labelCannotDeleteObject.Size = new System.Drawing.Size(302, 24);
            this.labelCannotDeleteObject.TabIndex = 0;
            this.labelCannotDeleteObject.Text = "labelCannotDeleteObject";
            this.labelCannotDeleteObject.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMessagesTitle
            // 
            this.labelMessagesTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMessagesTitle.Location = new System.Drawing.Point(3, 24);
            this.labelMessagesTitle.Name = "labelMessagesTitle";
            this.labelMessagesTitle.Size = new System.Drawing.Size(302, 24);
            this.labelMessagesTitle.TabIndex = 2;
            this.labelMessagesTitle.Text = "labelMessagesTitle";
            this.labelMessagesTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMessageList
            // 
            this.labelMessageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMessageList.Location = new System.Drawing.Point(3, 48);
            this.labelMessageList.Name = "labelMessageList";
            this.labelMessageList.Size = new System.Drawing.Size(302, 235);
            this.labelMessageList.TabIndex = 3;
            this.labelMessageList.Text = "labelMessageList";
            this.labelMessageList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(3, 286);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(84, 23);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelCannotDeleteObject, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonOK, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelMessageList, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelMessagesTitle, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(308, 313);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // DocumentCannotDeleteUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DocumentCannotDeleteUserControl";
            this.Size = new System.Drawing.Size(308, 313);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelCannotDeleteObject;
        private System.Windows.Forms.Label labelMessagesTitle;
        private System.Windows.Forms.Label labelMessageList;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}
