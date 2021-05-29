using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_WithInterpolation 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_WithInterpolation() => InitializeComponent();

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelInterpolation.Text = ResourceFormatter.Interpolation;
            labelFollowingMode.Text = ResourceFormatter.FollowingMode;
        }

        protected override void AddProperties()
        {
            AddProperty(_labelUnderlyingPatch, _comboBoxUnderlyingPatch);
            AddProperty(labelInterpolation, comboBoxInterpolation);
            AddProperty(_labelStandardDimension, _comboBoxStandardDimension);
            AddProperty(_labelCustomDimensionName, _textBoxCustomDimensionName);
            AddProperty(_labelName, _textBoxName);
            AddProperty(labelFollowingMode, comboBoxFollowingMode);
        }

        // Binding

        public new OperatorPropertiesViewModel_WithInterpolation ViewModel
        {
            // ReSharper disable once MemberCanBePrivate.Global
            get => (OperatorPropertiesViewModel_WithInterpolation)base.ViewModel;
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

            // FollowingMode
            labelFollowingMode.Visible = ViewModel.CanEditFollowingMode;
            comboBoxFollowingMode.Visible = ViewModel.CanEditFollowingMode;
            if (comboBoxFollowingMode.DataSource == null)
            {
                comboBoxFollowingMode.ValueMember = nameof(IDAndName.ID);
                comboBoxFollowingMode.DisplayMember = nameof(IDAndName.Name);
                comboBoxFollowingMode.DataSource = ViewModel.FollowingModeLookup;
            }
            comboBoxFollowingMode.SelectedValue = ViewModel.FollowingMode?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.Interpolation = (IDAndName)comboBoxInterpolation.SelectedItem;
            ViewModel.FollowingMode = (IDAndName)comboBoxFollowingMode.SelectedItem;
        }
    }
}
