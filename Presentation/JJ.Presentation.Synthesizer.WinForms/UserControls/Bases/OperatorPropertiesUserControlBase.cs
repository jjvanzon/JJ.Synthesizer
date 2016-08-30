using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Bases
{
    internal class OperatorPropertiesUserControlBase : PropertiesUserControlBase
    {
        protected System.Windows.Forms.Label labelName;
        protected System.Windows.Forms.Label labelOperatorTypeTitle;
        protected System.Windows.Forms.Label labelOperatorTypeValue;
        protected System.Windows.Forms.TextBox textBoxCustomDimensionName;
        protected System.Windows.Forms.Label labelStandardDimension;
        protected System.Windows.Forms.ComboBox comboBoxStandardDimension;
        protected System.Windows.Forms.Label labelCustomDimensionName;
        protected System.Windows.Forms.TextBox textBoxName;

        public OperatorPropertiesUserControlBase()
        {
            InitializeComponent();
        }

        private new OperatorPropertiesViewModel ViewModel => (OperatorPropertiesViewModel)base.ViewModel;

        protected override int GetID()
        {
            return ViewModel.ID;
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelStandardDimension.Text = PropertyDisplayNames.StandardDimension;
            labelCustomDimensionName.Text = Titles.CustomDimension;
        }

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;
            labelOperatorTypeValue.Text = ViewModel.OperatorType.Name;

            comboBoxStandardDimension.Visible = ViewModel.StandardDimensionVisible;
            labelStandardDimension.Visible = ViewModel.StandardDimensionVisible;
            labelCustomDimensionName.Visible = ViewModel.CustomDimensionNameVisible;
            textBoxCustomDimensionName.Visible = ViewModel.CustomDimensionNameVisible;

            textBoxCustomDimensionName.Text = ViewModel.CustomDimensionName;

            if (comboBoxStandardDimension.DataSource == null)
            {
                comboBoxStandardDimension.ValueMember = PropertyNames.ID;
                comboBoxStandardDimension.DisplayMember = PropertyNames.Name;
                comboBoxStandardDimension.DataSource = ViewModel.StandardDimensionLookup;
            }

            comboBoxStandardDimension.SelectedValue = ViewModel.StandardDimension?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;

            ViewModel.StandardDimension = (IDAndName)comboBoxStandardDimension.SelectedItem;
            ViewModel.CustomDimensionName = textBoxCustomDimensionName.Text;
        }

        private void InitializeComponent()
        {
            this.labelName = new System.Windows.Forms.Label();
            this.labelOperatorTypeTitle = new System.Windows.Forms.Label();
            this.labelOperatorTypeValue = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxCustomDimensionName = new System.Windows.Forms.TextBox();
            this.labelStandardDimension = new System.Windows.Forms.Label();
            this.comboBoxStandardDimension = new System.Windows.Forms.ComboBox();
            this.labelCustomDimensionName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(34, 74);
            this.labelName.Margin = new System.Windows.Forms.Padding(0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(196, 37);
            this.labelName.TabIndex = 14;
            this.labelName.Text = "labelName";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelOperatorTypeTitle
            // 
            this.labelOperatorTypeTitle.Location = new System.Drawing.Point(34, 42);
            this.labelOperatorTypeTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelOperatorTypeTitle.Name = "labelOperatorTypeTitle";
            this.labelOperatorTypeTitle.Size = new System.Drawing.Size(196, 37);
            this.labelOperatorTypeTitle.TabIndex = 16;
            this.labelOperatorTypeTitle.Text = "labelOperatorTypeTitle";
            this.labelOperatorTypeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelOperatorTypeValue
            // 
            this.labelOperatorTypeValue.Location = new System.Drawing.Point(233, 43);
            this.labelOperatorTypeValue.Margin = new System.Windows.Forms.Padding(0);
            this.labelOperatorTypeValue.Name = "labelOperatorTypeValue";
            this.labelOperatorTypeValue.Size = new System.Drawing.Size(386, 37);
            this.labelOperatorTypeValue.TabIndex = 17;
            this.labelOperatorTypeValue.Text = "labelOperatorTypeValue";
            this.labelOperatorTypeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(233, 81);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(386, 22);
            this.textBoxName.TabIndex = 15;
            // 
            // textBoxCustomDimensionName
            // 
            this.textBoxCustomDimensionName.Location = new System.Drawing.Point(240, 149);
            this.textBoxCustomDimensionName.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxCustomDimensionName.Name = "textBoxCustomDimensionName";
            this.textBoxCustomDimensionName.Size = new System.Drawing.Size(184, 22);
            this.textBoxCustomDimensionName.TabIndex = 34;
            // 
            // labelStandardDimension
            // 
            this.labelStandardDimension.Location = new System.Drawing.Point(82, 114);
            this.labelStandardDimension.Margin = new System.Windows.Forms.Padding(0);
            this.labelStandardDimension.Name = "labelStandardDimension";
            this.labelStandardDimension.Size = new System.Drawing.Size(147, 30);
            this.labelStandardDimension.TabIndex = 35;
            this.labelStandardDimension.Text = "labelStandardDimension";
            this.labelStandardDimension.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxStandardDimension
            // 
            this.comboBoxStandardDimension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStandardDimension.FormattingEnabled = true;
            this.comboBoxStandardDimension.Location = new System.Drawing.Point(236, 120);
            this.comboBoxStandardDimension.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxStandardDimension.Name = "comboBoxStandardDimension";
            this.comboBoxStandardDimension.Size = new System.Drawing.Size(353, 24);
            this.comboBoxStandardDimension.TabIndex = 36;
            // 
            // labelCustomDimensionName
            // 
            this.labelCustomDimensionName.Location = new System.Drawing.Point(37, 144);
            this.labelCustomDimensionName.Margin = new System.Windows.Forms.Padding(0);
            this.labelCustomDimensionName.Name = "labelCustomDimensionName";
            this.labelCustomDimensionName.Size = new System.Drawing.Size(184, 28);
            this.labelCustomDimensionName.TabIndex = 37;
            this.labelCustomDimensionName.Text = "labelCustomDimensionName";
            this.labelCustomDimensionName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // OperatorPropertiesUserControlBase
            // 
            this.Controls.Add(this.labelCustomDimensionName);
            this.Controls.Add(this.comboBoxStandardDimension);
            this.Controls.Add(this.labelStandardDimension);
            this.Controls.Add(this.textBoxCustomDimensionName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelOperatorTypeTitle);
            this.Controls.Add(this.labelOperatorTypeValue);
            this.Controls.Add(this.textBoxName);
            this.Name = "OperatorPropertiesUserControlBase";
            this.Size = new System.Drawing.Size(652, 305);
            this.Controls.SetChildIndex(this.textBoxName, 0);
            this.Controls.SetChildIndex(this.labelOperatorTypeValue, 0);
            this.Controls.SetChildIndex(this.labelOperatorTypeTitle, 0);
            this.Controls.SetChildIndex(this.labelName, 0);
            this.Controls.SetChildIndex(this.textBoxCustomDimensionName, 0);
            this.Controls.SetChildIndex(this.labelStandardDimension, 0);
            this.Controls.SetChildIndex(this.comboBoxStandardDimension, 0);
            this.Controls.SetChildIndex(this.labelCustomDimensionName, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
