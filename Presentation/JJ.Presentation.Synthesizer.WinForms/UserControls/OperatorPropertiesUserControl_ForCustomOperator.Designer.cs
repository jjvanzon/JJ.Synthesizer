using JJ.Presentation.Synthesizer.WinForms.UserControls.Partials;
namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    partial class OperatorPropertiesUserControl_ForCustomOperator
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
            this.labelUnderlyingPatch = new System.Windows.Forms.Label();
            this.comboBoxUnderlyingPatch = new System.Windows.Forms.ComboBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelOperatorTypeTitle = new System.Windows.Forms.Label();
            this.labelOperatorTypeValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelUnderlyingPatch
            // 
            this.labelUnderlyingPatch.Location = new System.Drawing.Point(0, 48);
            this.labelUnderlyingPatch.Margin = new System.Windows.Forms.Padding(0);
            this.labelUnderlyingPatch.Name = "labelUnderlyingPatch";
            this.labelUnderlyingPatch.Size = new System.Drawing.Size(120, 24);
            this.labelUnderlyingPatch.TabIndex = 15;
            this.labelUnderlyingPatch.Text = "labelUnderlyingPatch";
            this.labelUnderlyingPatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxUnderlyingPatch
            // 
            this.comboBoxUnderlyingPatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUnderlyingPatch.FormattingEnabled = true;
            this.comboBoxUnderlyingPatch.Location = new System.Drawing.Point(120, 48);
            this.comboBoxUnderlyingPatch.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxUnderlyingPatch.Name = "comboBoxUnderlyingPatch";
            this.comboBoxUnderlyingPatch.Size = new System.Drawing.Size(10, 21);
            this.comboBoxUnderlyingPatch.TabIndex = 16;
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(0, 24);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(120, 24);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(120, 24);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(10, 20);
            this.textBoxName.TabIndex = 11;
            // 
            // labelOperatorTypeTitle
            // 
            this.labelOperatorTypeTitle.Location = new System.Drawing.Point(0, 0);
            this.labelOperatorTypeTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelOperatorTypeTitle.Name = "labelOperatorTypeTitle";
            this.labelOperatorTypeTitle.Size = new System.Drawing.Size(120, 24);
            this.labelOperatorTypeTitle.TabIndex = 17;
            this.labelOperatorTypeTitle.Text = "labelOperatorTypeTitle";
            this.labelOperatorTypeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelOperatorTypeValue
            // 
            this.labelOperatorTypeValue.Location = new System.Drawing.Point(120, 0);
            this.labelOperatorTypeValue.Margin = new System.Windows.Forms.Padding(0);
            this.labelOperatorTypeValue.Name = "labelOperatorTypeValue";
            this.labelOperatorTypeValue.Size = new System.Drawing.Size(10, 24);
            this.labelOperatorTypeValue.TabIndex = 18;
            this.labelOperatorTypeValue.Text = "labelOperatorTypeValue";
            this.labelOperatorTypeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OperatorPropertiesUserControl_ForCustomOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.labelUnderlyingPatch);
            this.Controls.Add(this.comboBoxUnderlyingPatch);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelOperatorTypeTitle);
            this.Controls.Add(this.labelOperatorTypeValue);
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Name = "OperatorPropertiesUserControl_ForCustomOperator";
            this.Size = new System.Drawing.Size(10, 10);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelUnderlyingPatch;
        private System.Windows.Forms.ComboBox comboBoxUnderlyingPatch;
        private System.Windows.Forms.Label labelOperatorTypeTitle;
        private System.Windows.Forms.Label labelOperatorTypeValue;
    }
}
