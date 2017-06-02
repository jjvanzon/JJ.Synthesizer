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

            if (inlet.InputOutlet?.Operator?.GetOperatorTypeEnum() != OperatorTypeEnum.Number)
            {
                return null;
            }

            if (!DataPropertyParser.DataIsWellFormed(inlet.InputOutlet.Operator.Data))
            {
                return null;
            }

            double? number = DataPropertyParser.TryParseDouble(inlet.InputOutlet.Operator, nameof(Number_OperatorWrapper.Number));
            return number;
        }
    }
}
