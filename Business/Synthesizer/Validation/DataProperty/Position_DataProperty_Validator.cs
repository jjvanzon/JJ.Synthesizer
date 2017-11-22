using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.DataProperty
{
	internal class Position_DataProperty_Validator : VersatileValidator
	{
		public Position_DataProperty_Validator(string data) 
		{ 
			if (DataPropertyParser.DataIsWellFormed(data))
			{
				const string dataKey = nameof(Reset_OperatorWrapper.Position);

				string stringValue = DataPropertyParser.TryGetString(data, dataKey);

				For(stringValue, dataKey, DataPropertyParser.FormattingCulture).IsInteger();
			}
		}
	}
}
