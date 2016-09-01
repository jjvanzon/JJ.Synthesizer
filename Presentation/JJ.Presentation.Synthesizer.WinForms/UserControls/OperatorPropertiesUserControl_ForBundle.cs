using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForBundle 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForBundle()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelInletCount.Text = CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets);
        }

        protected override void AddProperties()
        {
            AddProperty(_labelOperatorTypeTitle, _labelOperatorTypeValue);
            AddProperty(labelInletCount, numericUpDownInletCount);
            AddProperty(_labelStandardDimension, _comboBoxStandardDimension);
            AddProperty(_labelCustomDimensionName, _textBoxCustomDimensionName);
            AddProperty(_labelName, _textBoxName);
        }

        // Binding

        private new OperatorPropertiesViewModel_ForBundle ViewModel => (OperatorPropertiesViewModel_ForBundle)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            numericUpDownInletCount.Value = ViewModel.InletCount;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.InletCount = (int)numericUpDownInletCount.Value;
        }
    }
}
