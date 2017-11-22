using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Operators
{
	public class OperatorValidator_CurveAndSampleAreNull : VersatileValidator
	{
		public OperatorValidator_CurveAndSampleAreNull(Operator op)
		{
			if (op == null) throw new ArgumentNullException(nameof(op));

			For(op.Curve, ResourceFormatter.Curve).IsNull();
			For(op.Sample, ResourceFormatter.Sample).IsNull();
		}
	}
}
