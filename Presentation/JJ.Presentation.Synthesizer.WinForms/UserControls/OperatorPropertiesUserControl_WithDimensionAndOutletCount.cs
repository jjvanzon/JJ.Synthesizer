using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_WithDimensionAndOutletCount 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_WithDimensionAndOutletCount()
        {
            InitializeComponent();
        }

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelOutletCount, numericUpDownOutletCount);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelName, textBoxName);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelOutletCount.Text = CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets);
            labelDimension.Text = PropertyDisplayNames.Dimension;
        }

        // Binding

        private new OperatorPropertiesViewModel_WithDimensionAndOutletCount ViewModel => 
                   (OperatorPropertiesViewModel_WithDimensionAndOutletCount)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;
            numericUpDownOutletCount.Value = ViewModel.OutletCount;
            labelOperatorTypeValue.Text = ViewModel.OperatorType.Name;

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
            ViewModel.OutletCount = (int)numericUpDownOutletCount.Value;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
        }
    }
}
