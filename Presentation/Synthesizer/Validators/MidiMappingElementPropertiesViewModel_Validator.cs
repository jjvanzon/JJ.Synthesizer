using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Validators
{
	internal class MidiMappingElementPropertiesViewModel_Validator : VersatileValidator
	{
		public MidiMappingElementPropertiesViewModel_Validator(MidiMappingElementPropertiesViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			For(viewModel.FromDimensionValue, ResourceFormatter.FromDimensionValue).IsDouble();
			For(viewModel.TillDimensionValue, ResourceFormatter.TillDimensionValue).IsDouble();
			For(viewModel.MinDimensionValue, ResourceFormatter.MinDimensionValue).IsDouble();
			For(viewModel.MaxDimensionValue, ResourceFormatter.MaxDimensionValue).IsDouble();
			For(viewModel.FromPosition, ResourceFormatter.FromPosition).IsInteger();
			For(viewModel.TillPosition, ResourceFormatter.TillPosition).IsInteger();
		}
	}
}