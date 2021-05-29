using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl() => InitializeComponent();

        public new OperatorPropertiesViewModel ViewModel
        {
            // ReSharper disable once UnusedMember.Global
            get => (OperatorPropertiesViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void AddProperties()
        {
            AddProperty(_labelUnderlyingPatch, _comboBoxUnderlyingPatch);
            AddProperty(_labelName, _textBoxName);

            // Put these last, so we can make them invisible dynamically.
            // Trying to close the gap where the invisible controls are, proved a disaster.
            // TableLayoutPanel cannot help us with it.
            // Trying to recreate the rows dynamically made a total mess of my base class.
            AddProperty(_labelStandardDimension, _comboBoxStandardDimension);
            AddProperty(_labelCustomDimensionName, _textBoxCustomDimensionName);

            AddProperty(_labelInletCount, _numericUpDownInletCount);
            AddProperty(_labelOutletCount, _numericUpDownOutletCount);
        }
    }
}
