using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForCache 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForCache() => InitializeComponent();

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelInterpolation.Text = ResourceFormatter.Interpolation;
            labelSpeakerSetup.Text = ResourceFormatter.SpeakerSetup;
        }

        protected override void AddProperties()
        {
            AddProperty(_labelUnderlyingPatch, _comboBoxUnderlyingPatch);
            AddProperty(labelInterpolation, comboBoxInterpolation);
            AddProperty(labelSpeakerSetup, comboBoxSpeakerSetup);
            AddProperty(_labelStandardDimension, _comboBoxStandardDimension);
            AddProperty(_labelCustomDimensionName, _textBoxCustomDimensionName);
            AddProperty(_labelName, _textBoxName);
        }

        // Binding

        public new OperatorPropertiesViewModel_ForCache ViewModel
        {
            get => (OperatorPropertiesViewModel_ForCache)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            // Interpolation
            if (comboBoxInterpolation.DataSource == null)
            {
                comboBoxInterpolation.ValueMember = nameof(IDAndName.ID);
                comboBoxInterpolation.DisplayMember = nameof(IDAndName.Name);
                comboBoxInterpolation.DataSource = ViewModel.InterpolationLookup;
            }
            comboBoxInterpolation.SelectedValue = ViewModel.Interpolation?.ID ?? 0;

            // SpeakerSetup
            if (comboBoxSpeakerSetup.DataSource == null)
            {
                comboBoxSpeakerSetup.ValueMember = nameof(IDAndName.ID);
                comboBoxSpeakerSetup.DisplayMember = nameof(IDAndName.Name);
                comboBoxSpeakerSetup.DataSource = ViewModel.SpeakerSetupLookup;
            }
            comboBoxSpeakerSetup.SelectedValue = ViewModel.SpeakerSetup?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.Interpolation = (IDAndName)comboBoxInterpolation.SelectedItem;
            ViewModel.SpeakerSetup = (IDAndName)comboBoxSpeakerSetup.SelectedItem;
        }
    }
}
