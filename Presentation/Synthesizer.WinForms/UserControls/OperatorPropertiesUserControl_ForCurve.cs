using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class OperatorPropertiesUserControl_ForCurve
		: OperatorPropertiesUserControlBase
	{
		public OperatorPropertiesUserControl_ForCurve() => InitializeComponent();

		protected override void AddProperties()
		{
			AddProperty(_labelUnderlyingPatch, _comboBoxUnderlyingPatch);
			AddProperty(_labelStandardDimension, _comboBoxStandardDimension);
			AddProperty(_labelCustomDimensionName, _textBoxCustomDimensionName);
			AddProperty(_labelName, _textBoxName);
		}

		public new OperatorPropertiesViewModel_ForCurve ViewModel
		{
			// ReSharper disable once UnusedMember.Global
			get => (OperatorPropertiesViewModel_ForCurve)base.ViewModel;
			set => base.ViewModel = value;
		}
	}
}
