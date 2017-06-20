//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using JetBrains.Annotations;
//using JJ.Business.Synthesizer.Calculation.Operators;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Extensions
//{
//    internal static class VariableInput_OperatorCalculator_Extensions
//    {
//        public static IEnumerable<VariableInput_OperatorCalculator> Sort([NotNull] this IEnumerable<VariableInput_OperatorCalculator> inlets)
//        {
//            if (inlets == null) throw new NullException(() => inlets);

//            // NOTE: When you change this, also change the extension method for ExtendedVariableInfo
//            return inlets.OrderBy(x => x.Position)
//                         .ThenBy(x => x.DimensionEnum == DimensionEnum.Undefined)
//                         .ThenBy(x => x.DimensionEnum)
//                         .ThenBy(x => string.IsNullOrWhiteSpace(x.CanonicalName))
//                         .ThenBy(x => x.CanonicalName);
//        }
//    }
//}
