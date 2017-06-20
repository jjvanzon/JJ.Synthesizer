//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;

//namespace JJ.Business.Synthesizer.Extensions
//{
//    public static class OutletExtensions
//    {
//        public static IEnumerable<Outlet> Sort(this IEnumerable<Outlet> outlets)
//        {
//            if (outlets == null) throw new NullException(() => outlets);

//            return outlets.OrderBy(x => x.Position)
//                          .ThenBy(x => x.GetDimensionEnum() == DimensionEnum.Undefined)
//                          .ThenBy(x => x.GetDimensionEnum())
//                          .ThenBy(x => string.IsNullOrWhiteSpace(x.Name))
//                          .ThenBy(x => x.Name)
//                          .ThenBy(x => x.IsObsolete);
//        }
//    }
//}
