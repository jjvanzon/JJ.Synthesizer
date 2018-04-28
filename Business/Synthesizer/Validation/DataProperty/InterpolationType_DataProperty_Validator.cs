using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DataProperty
{
	internal class InterpolationType_DataProperty_Validator : VersatileValidator
	{
		public InterpolationType_DataProperty_Validator(string data) 
		{ 
			// ReSharper disable once InvertIf
			if (DataPropertyParser.DataIsWellFormed(data))
			{
				string stringValue = DataPropertyParser.TryGetString(data, nameof(OperatorWrapper_WithInterpolation.InterpolationType));

				For(stringValue, ResourceFormatter.InterpolationType)
					.NotNullOrEmpty()
					.IsEnum<InterpolationTypeEnum>()
					.IsNot(InterpolationTypeEnum.Undefined);
			}
		}
	}
}
