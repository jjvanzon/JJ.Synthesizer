﻿//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Validation;
//using JJ.Framework.Validation.Resources;

//namespace JJ.Business.Synthesizer.Validation.Patches
//{
//    internal class PatchValidator_UniqueInletListIndexes : VersatileValidator<Patch>
//    {
//        public PatchValidator_UniqueInletListIndexes(Patch obj)
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            bool listIndexesAreUnique = ValidationHelper.PatchInletListIndexesAreUniqueWithinPatch(Obj);
//            if (!listIndexesAreUnique)
//            {
//                string message = ResourceFormatter.Inlets + ": " + ValidationResourceFormatter.NotUniquePlural(ResourceFormatter.ListIndexes);
//                ValidationMessages.Add(nameof(OperatorTypeEnum.PatchInlet), message);
//            }
//        }
//    }
//}
