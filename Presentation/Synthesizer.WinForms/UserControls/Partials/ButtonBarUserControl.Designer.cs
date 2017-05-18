namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    partial class ButtonBarUserControl
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.CloseIcon;
            this.buttonClose.Location = new System.Drawing.Point(314, 8);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(21, 23);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.FlatAppearance.BorderSize = 0;
            this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRemove.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.RemoveIcon;
            this.buttonRemove.Location = new System.Drawing.Point(289, 8);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(21, 23);
            this.buttonRemove.TabIndex = 14;
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.AddIcon;
            this.buttonAdd.Location = new System.Drawing.Point(264, 8);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(21, 23);
            this.buttonAdd.TabIndex = 15;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.FlatAppearance.BorderSize = 0;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.SaveIcon;
            this.buttonSave.Location = new System.Drawing.Point(163, 8);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(21, 23);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.FlatAppearance.BorderSize = 0;
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlay.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.PlayIcon;
            this.buttonPlay.Location = new System.Drawing.Point(129, 8);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(21, 23);
            this.buttonPlay.TabIndex = 17;
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.FlatAppearance.BorderSize = 0;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.RefreshIcon;
            this.buttonRefresh.Location = new System.Drawing.Point(196, 8);
            this.buttonRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(21, 23);
            this.buttonRefresh.TabIndex = 18;
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.FlatAppearance.BorderSize = 0;
            this.buttonOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOpen.Image = global::JJ.Presentation.Synthesizer.WinForms.Properties.Resources.OpenWindowIcon;
            this.buttonOpen.Location = new System.Drawing.Point(229, 8);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(0);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(21, 23);
            this.buttonOpen.TabIndex = 19;
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // TitleBarUserControl
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonClose);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TitleBarUserControl";
            this.Size = new System.Drawing.Size(337, 85);
            this.Load += new System.EventHandler(this.TitleBarUserControl_Load);
            this.Resize += new System.EventHandler(this.TitleBarUserControl_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonOpen;
    }
}
