﻿using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
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
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelScaleType = new System.Windows.Forms.Label();
            this.comboBoxScaleType = new System.Windows.Forms.ComboBox();
            this.labelBaseFrequency = new System.Windows.Forms.Label();
            this.numericUpDownBaseFrequency = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBaseFrequency)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Location = new System.Drawing.Point(0, 0);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(160, 30);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxName.Location = new System.Drawing.Point(160, 0);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(13, 22);
            this.textBoxName.TabIndex = 11;
            // 
            // labelScaleType
            // 
            this.labelScaleType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScaleType.Location = new System.Drawing.Point(0, 30);
            this.labelScaleType.Margin = new System.Windows.Forms.Padding(0);
            this.labelScaleType.Name = "labelScaleType";
            this.labelScaleType.Size = new System.Drawing.Size(160, 30);
            this.labelScaleType.TabIndex = 17;
            this.labelScaleType.Text = "labelScaleType";
            this.labelScaleType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxScaleType
            // 
            this.comboBoxScaleType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxScaleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScaleType.FormattingEnabled = true;
            this.comboBoxScaleType.Location = new System.Drawing.Point(160, 30);
            this.comboBoxScaleType.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxScaleType.Name = "comboBoxScaleType";
            this.comboBoxScaleType.Size = new System.Drawing.Size(13, 24);
            this.comboBoxScaleType.TabIndex = 16;
            // 
            // labelBaseFrequency
            // 
            this.labelBaseFrequency.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelBaseFrequency.Location = new System.Drawing.Point(0, 60);
            this.labelBaseFrequency.Margin = new System.Windows.Forms.Padding(0);
            this.labelBaseFrequency.Name = "labelBaseFrequency";
            this.labelBaseFrequency.Size = new System.Drawing.Size(160, 30);
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
            this.numericUpDownBaseFrequency.Location = new System.Drawing.Point(160, 60);
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
            this.numericUpDownBaseFrequency.Size = new System.Drawing.Size(13, 22);
            this.numericUpDownBaseFrequency.TabIndex = 20;
            this.numericUpDownBaseFrequency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ScalePropertiesUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelScaleType);
            this.Controls.Add(this.comboBoxScaleType);
            this.Controls.Add(this.labelBaseFrequency);
            this.Controls.Add(this.numericUpDownBaseFrequency);
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ScalePropertiesUserControl";
            this.Size = new System.Drawing.Size(13, 12);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBaseFrequency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ComboBox comboBoxScaleType;
        private System.Windows.Forms.Label labelScaleType;
        private System.Windows.Forms.Label labelBaseFrequency;
        private System.Windows.Forms.NumericUpDown numericUpDownBaseFrequency;
    }
}