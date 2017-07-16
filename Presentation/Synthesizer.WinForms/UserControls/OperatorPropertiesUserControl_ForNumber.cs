using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForNumber 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForNumber() => InitializeComponent();

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelNumber.Text = ResourceFormatter.Number;
        }

        protected override void AddProperties()
        {
            AddProperty(_labelUnderlyingPatch, _comboBoxUnderlyingPatch);
            AddProperty(labelNumber, textBoxNumber);
            AddProperty(_labelName, _textBoxName);
        }

        // Binding

        public new OperatorPropertiesViewModel_ForNumber ViewModel
        {
            get => (OperatorPropertiesViewModel_ForNumber)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            textBoxNumber.Text = ViewModel.Number;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.Number = textBoxNumber.Text;
        }
    }
}
