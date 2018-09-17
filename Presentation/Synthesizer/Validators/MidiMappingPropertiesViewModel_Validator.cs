using System;
using JJ.Business.Synthesizer.StringResources;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Validators
{
	internal class MidiMappingPropertiesViewModel_Validator : VersatileValidator
	{
		public MidiMappingPropertiesViewModel_Validator(MidiMappingPropertiesViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			For(viewModel.FromDimensionValue, ResourceFormatter.FromDimensionValue).IsDouble();
			For(viewModel.TillDimensionValue, ResourceFormatter.TillDimensionValue).IsDouble();
			For(viewModel.MinDimensionValue, ResourceFormatter.MinDimensionValue).IsDouble();
			For(viewModel.MaxDimensionValue, ResourceFormatter.MaxDimensionValue).IsDouble();
			For(viewModel.Position, ResourceFormatter.Position).IsInteger();
		}
	}
}