using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_WithOutletCount 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_WithOutletCount()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelOutletCount.Text = CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets);
        }

        protected override void AddProperties()
        {
            AddProperty(_labelOperatorTypeTitle, _labelOperatorTypeValue);
            AddProperty(labelOutletCount, numericUpDownOutletCount);
            AddProperty(_labelName, _textBoxName);
            AddProperty(_labelStandardDimension, _comboBoxStandardDimension);
            AddProperty(_labelCustomDimensionName, _textBoxCustomDimensionName);
        }

        // Binding

        public new OperatorPropertiesViewModel_WithOutletCount ViewModel
        {
            get { return (OperatorPropertiesViewModel_WithOutletCount)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            numericUpDownOutletCount.Value = ViewModel.OutletCount;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.OutletCount = (int)numericUpDownOutletCount.Value;
        }
    }
}
