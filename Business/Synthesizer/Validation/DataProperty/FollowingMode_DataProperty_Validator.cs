using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DataProperty
{
	internal class FollowingMode_DataProperty_Validator : VersatileValidator
	{
		public FollowingMode_DataProperty_Validator(string data) 
		{ 
			// ReSharper disable once InvertIf
			if (DataPropertyParser.DataIsWellFormed(data))
			{
				string stringValue = DataPropertyParser.TryGetString(data, nameof(OperatorWrapper_WithInterpolation_AndFollowingMode.FollowingMode));

				For(stringValue, ResourceFormatter.FollowingMode)
					//.NotNullOrEmpty()
					.IsEnum<FollowingModeEnum>();
					//.IsNot(FollowingModeEnum.Undefined);
			}
		}
	}
}
