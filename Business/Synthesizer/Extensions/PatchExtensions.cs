using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class PatchExtensions
    {
        public static IEnumerable<Operator> EnumerateOperatorsOfType(this Patch patch, OperatorTypeEnum operatorTypeEnum)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.Operators.Where(x => x.GetOperatorTypeEnum() == operatorTypeEnum);
        }

        public static IList<Operator> GetOperatorsOfType(this Patch patch, OperatorTypeEnum operatorTypeEnum)
        {
            return EnumerateOperatorsOfType(patch, operatorTypeEnum).ToArray();
        }
    }
}