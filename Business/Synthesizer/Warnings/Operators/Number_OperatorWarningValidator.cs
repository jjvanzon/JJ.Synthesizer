using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
	internal class Number_OperatorWarningValidator : VersatileValidator
	{
		public Number_OperatorWarningValidator(Operator op)
		{
			if (op == null) throw new NullException(() => op);

			if (DataPropertyParser.DataIsWellFormed(op))
			{
				double? number = DataPropertyParser.TryParseDouble(op, nameof(Number_OperatorWrapper.Number));

				For(number, ResourceFormatter.Number).NotZero();
			}
		}
	}
}