using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class InletExtensions
	{
		public static double? TryGetConstantNumber(this Inlet inlet)
		{
			if (inlet == null) throw new NullException(() => inlet);

			// ReSharper disable once InvertIf
			if (inlet.InputOutlet?.Operator?.GetOperatorTypeEnum() == OperatorTypeEnum.Number)
			{
				// ReSharper disable once InvertIf
				if (DataPropertyParser.DataIsWellFormed(inlet.InputOutlet.Operator.Data))
				{
					double? number = DataPropertyParser.TryParseDouble(inlet.InputOutlet.Operator, nameof(Number_OperatorWrapper.Number));
					return number;
				}
			}

			return inlet.DefaultValue;
		}
	}
}
