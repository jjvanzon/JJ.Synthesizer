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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TitleBarUserControl));
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.pictureBoxRemove = new System.Windows.Forms.PictureBox();
            this.pictureBoxAdd = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.pictureBoxPlay = new System.Windows.Forms.PictureBox();
            this.pictureBoxSave = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSave)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxClose.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxClose.Image")));
            this.pictureBoxClose.Location = new System.Drawing.Point(196, 8);
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
            this.pictureBoxRemove.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxRemove.Image")));
            this.pictureBoxRemove.Location = new System.Drawing.Point(176, 8);
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
            this.pictureBoxAdd.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxAdd.Image")));
            this.pictureBoxAdd.Location = new System.Drawing.Point(156, 8);
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
            this.labelTitle.Size = new System.Drawing.Size(65, 24);
            this.labelTitle.TabIndex = 7;
            this.labelTitle.Text = "Title";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBoxPlay
            // 
            this.pictureBoxPlay.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPlay.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.PlayIcon;
            this.pictureBoxPlay.Location = new System.Drawing.Point(104, 8);
            this.pictureBoxPlay.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxPlay.Name = "pictureBoxPlay";
            this.pictureBoxPlay.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxPlay.TabIndex = 11;
            this.pictureBoxPlay.TabStop = false;
            this.pictureBoxPlay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPlay_MouseDown);
            // 
            // pictureBoxSave
            // 
            this.pictureBoxSave.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSave.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.SaveIcon;
            this.pictureBoxSave.Location = new System.Drawing.Point(131, 8);
            this.pictureBoxSave.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxSave.Name = "pictureBoxSave";
            this.pictureBoxSave.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSave.TabIndex = 12;
            this.pictureBoxSave.TabStop = false;
            this.pictureBoxSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxSave_MouseDown);
            // 
            // TitleBarUserControl
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pictureBoxSave);
            this.Controls.Add(this.pictureBoxPlay);
            this.Controls.Add(this.pictureBoxClose);
            this.Controls.Add(this.pictureBoxRemove);
            this.Controls.Add(this.pictureBoxAdd);
            this.Controls.Add(this.labelTitle);
            this.Name = "TitleBarUserControl";
            this.Size = new System.Drawing.Size(337, 85);
            this.Load += new System.EventHandler(this.TitleBarUserControl_Load);
            this.Resize += new System.EventHandler(this.TitleBarUserControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.PictureBox pictureBoxRemove;
        private System.Windows.Forms.PictureBox pictureBoxAdd;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox pictureBoxPlay;
        private System.Windows.Forms.PictureBox pictureBoxSave;
    }
}
