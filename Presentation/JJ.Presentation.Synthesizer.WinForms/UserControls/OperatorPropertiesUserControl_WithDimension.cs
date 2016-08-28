using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_WithDimension
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_WithDimension()
        {
            InitializeComponent();
        }

        // Gui
        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelDimension.Text = PropertyDisplayNames.Dimension;
            labelCustomDimensionName.Text = Titles.CustomDimension;
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelCustomDimensionName, textBoxCustomDimensionName);
            AddProperty(labelName, textBoxName);
        }

        // Binding

        private new OperatorPropertiesViewModel_WithDimension ViewModel => (OperatorPropertiesViewModel_WithDimension)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;
            labelOperatorTypeValue.Text = ViewModel.OperatorType.Name;
            textBoxCustomDimensionName.Text = ViewModel.CustomDimensionName;

            if (comboBoxDimension.DataSource == null)
            {
                comboBoxDimension.ValueMember = PropertyNames.ID;
                comboBoxDimension.DisplayMember = PropertyNames.Name;
                comboBoxDimension.DataSource = ViewModel.DimensionLookup;
            }

            if (ViewModel.Dimension != null)
            {
                comboBoxDimension.SelectedValue = ViewModel.Dimension.ID;
            }
            else
            {
                comboBoxDimension.SelectedValue = 0;
            }
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
            ViewModel.CustomDimensionName = textBoxCustomDimensionName.Text;
        }
    }
}
