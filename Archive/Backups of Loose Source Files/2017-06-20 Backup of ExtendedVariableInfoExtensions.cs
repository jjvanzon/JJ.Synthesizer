//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using JetBrains.Annotations;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Roslyn.Helpers
//{
//    internal static class ExtendedVariableInfoExtensions
//    {
//        public static IEnumerable<ExtendedVariableInfo> Sort([NotNull] this IEnumerable<ExtendedVariableInfo> list)
//        {
//            if (list == null) throw new NullException(() => list);

//            return list.OrderBy(x => x.Position)
//                       .ThenBy(x => x.DimensionEnum == DimensionEnum.Undefined)
//                       .ThenBy(x => x.DimensionEnum)
//                       .ThenBy(x => string.IsNullOrWhiteSpace(x.VariableNameCamelCase))
//                       .ThenBy(x => x.VariableNameCamelCase);

//        }
//    }
//}
