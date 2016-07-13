using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
    internal class PatchValidator_UniqueOutletListIndexes : FluentValidator<Patch>
    {
        public PatchValidator_UniqueOutletListIndexes(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            IList<int> listIndexes = Object.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                           .Where(x => DataPropertyParser.DataIsWellFormed(x))
                                           .Select(x => DataPropertyParser.TryParseInt32(x, PropertyNames.ListIndex))
                                           .Where(x => x.HasValue)
                                           .Select(x => x.Value)
                                           .ToArray();

            bool listIndexesAreUnique = listIndexes.Distinct().Count() == listIndexes.Count;
            if (!listIndexesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.PatchOutlet, Messages.OutletListIndexesAreNotUnique);
            }
        }
    }
}