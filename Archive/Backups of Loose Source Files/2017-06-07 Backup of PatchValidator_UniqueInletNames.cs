﻿//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Presentation.Resources;
//using JJ.Framework.Validation;
//using JJ.Framework.Validation.Resources;

//namespace JJ.Business.Synthesizer.Validation.Patches
//{
//    internal class PatchValidator_UniqueInletNames : VersatileValidator<Patch>
//    {
//        public PatchValidator_UniqueInletNames(Patch obj)
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            bool namesAreUnique = ValidationHelper.PatchInletNamesAreUniqueWithinPatch(Obj);
//            if (!namesAreUnique)
//            {
//                string message = ResourceFormatter.Inlets + ": " + ValidationResourceFormatter.NotUniquePlural(CommonResourceFormatter.Names);
//                ValidationMessages.Add(nameof(OperatorTypeEnum.PatchInlet), message);
//            }
//        }
//    }
//}
