﻿//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Validation;
//using JJ.Framework.Validation.Resources;

//namespace JJ.Business.Synthesizer.Validation.Patches
//{
//    internal class PatchValidator_UniqueOutletListIndexes : VersatileValidator<Patch>
//    {
//        public PatchValidator_UniqueOutletListIndexes(Patch obj)
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            bool listIndexesAreUnique = ValidationHelper.PatchOutletListIndexesAreUniqueWithinPatch(Obj);
//            if (!listIndexesAreUnique)
//            {
//                string message = ResourceFormatter.Outlets + ": " + ValidationResourceFormatter.NotUniquePlural(ResourceFormatter.ListIndexes);
//                ValidationMessages.Add(nameof(OperatorTypeEnum.PatchOutlet), message);
//            }
//        }
//    }
//}