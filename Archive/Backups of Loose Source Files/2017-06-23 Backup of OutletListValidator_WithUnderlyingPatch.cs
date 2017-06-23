//using System.Collections.Generic;
//using JetBrains.Annotations;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation
//{
//    internal class OutletListValidator_WithUnderlyingPatch : VersatileValidator
//    {
//        public OutletListValidator_WithUnderlyingPatch([NotNull] IList<Outlet> outlets)
//        {
//            if (outlets == null) throw new NullException(() => outlets);

//            foreach (Outlet outlet in outlets)
//            {
//                string messagePrefix = ValidationHelper.GetMessagePrefix(outlet);
//                ExecuteValidator(new OutletValidator_WithUnderlyingPatch(outlet), messagePrefix);
//            }
//        }
//    }
//}
