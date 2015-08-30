using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class PatchExtensions
    {
        public static IList<Operator> GetOperatorsOfType(this Patch patch, OperatorTypeEnum operatorTypeEnum)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.Operators.Where(x => x.GetOperatorTypeEnum() == operatorTypeEnum).ToArray();
        }
    }
}
