namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    partial class TitleBarUserControl
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
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.pictureBoxRemove = new System.Windows.Forms.PictureBox();
            this.pictureBoxAdd = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxClose.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.CloseIcon;
            this.pictureBoxClose.Location = new System.Drawing.Point(72, 5);
            this.pictureBoxClose.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxClose.TabIndex = 8;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxClose_MouseDown);
            // 
            // pictureBoxRemove
            // 
            this.pictureBoxRemove.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxRemove.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.RemoveIcon;
            this.pictureBoxRemove.Location = new System.Drawing.Point(52, 5);
            this.pictureBoxRemove.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxRemove.Name = "pictureBoxRemove";
            this.pictureBoxRemove.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxRemove.TabIndex = 9;
            this.pictureBoxRemove.TabStop = false;
            this.pictureBoxRemove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxRemove_MouseDown);
            // 
            // pictureBoxAdd
            // 
            this.pictureBoxAdd.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxAdd.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.AddIcon;
            this.pictureBoxAdd.Location = new System.Drawing.Point(32, 5);
            this.pictureBoxAdd.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxAdd.Name = "pictureBoxAdd";
            this.pictureBoxAdd.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxAdd.TabIndex = 10;
            this.pictureBoxAdd.TabStop = false;
            this.pictureBoxAdd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxAdd_MouseDown);
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Padding = new System.Windows.Forms.Padding(3);
            this.labelTitle.Size = new System.Drawing.Size(30, 24);
            this.labelTitle.TabIndex = 7;
            this.labelTitle.Text = "Title";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TitleBarUserControl
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pictureBoxClose);
            this.Controls.Add(this.pictureBoxRemove);
            this.Controls.Add(this.pictureBoxAdd);
            this.Controls.Add(this.labelTitle);
            this.Name = "TitleBarUserControl";
            this.Size = new System.Drawing.Size(93, 26);
            this.Load += new System.EventHandler(this.TitleBarUserControl_Load);
            this.Resize += new System.EventHandler(this.TitleBarUserControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAdd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.PictureBox pictureBoxRemove;
        private System.Windows.Forms.PictureBox pictureBoxAdd;
        private System.Windows.Forms.Label labelTitle;



    }
}
