using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class OperatorPropertiesUserControl_ForInletsToDimension 
		: OperatorPropertiesUserControlBase
	{
		public OperatorPropertiesUserControl_ForInletsToDimension() => InitializeComponent();

		// Gui

		protected override void SetTitles()
		{
			base.SetTitles();

			labelInterpolation.Text = ResourceFormatter.Interpolation;
		}

		protected override void AddProperties()
		{
			AddProperty(_labelUnderlyingPatch, _comboBoxUnderlyingPatch);
			AddProperty(_labelInletCount, _numericUpDownInletCount);
			AddProperty(labelInterpolation, comboBoxInterpolation);
			AddProperty(_labelStandardDimension, _comboBoxStandardDimension);
			AddProperty(_labelCustomDimensionName, _textBoxCustomDimensionName);
			AddProperty(_labelName, _textBoxName);
		}

		// Binding

		public new OperatorPropertiesViewModel_ForInletsToDimension ViewModel
		{
			get => (OperatorPropertiesViewModel_ForInletsToDimension)base.ViewModel;
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
		}

		protected override void ApplyControlsToViewModel()
		{
			base.ApplyControlsToViewModel();

			ViewModel.Interpolation = (IDAndName)comboBoxInterpolation.SelectedItem;
		}
	}
}
