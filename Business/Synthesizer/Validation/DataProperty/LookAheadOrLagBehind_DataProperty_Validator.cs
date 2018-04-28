using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DataProperty
{
	internal class LookAheadOrLagBehind_DataProperty_Validator : VersatileValidator
	{
		public LookAheadOrLagBehind_DataProperty_Validator(string data) 
		{ 
			// ReSharper disable once InvertIf
			if (DataPropertyParser.DataIsWellFormed(data))
			{
				string stringValue = DataPropertyParser.TryGetString(data, nameof(OperatorWrapper_WithInterpolation_AndLookAheadOrLagBehind.LookAheadOrLagBehind));

				For(stringValue, ResourceFormatter.LookAheadOrLagBehind)
					//.NotNullOrEmpty()
					.IsEnum<LookAheadOrLagBehindEnum>();
					//.IsNot(LookAheadOrLagBehindEnum.Undefined);
			}
		}
	}
}
