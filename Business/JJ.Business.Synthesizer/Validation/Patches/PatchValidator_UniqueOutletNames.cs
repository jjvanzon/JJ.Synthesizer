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
    internal class PatchValidator_UniqueOutletNames : FluentValidator<Patch>
    {
        public PatchValidator_UniqueOutletNames(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            IList<string> names = Object.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet)
                                        .Where(x => !String.IsNullOrEmpty(x.Name))
                                        .Select(x => x.Name)
                                        .ToArray();

            bool namesAreUnique = names.Distinct().Count() == names.Count;
            if (!namesAreUnique)
            {
                ValidationMessages.Add(PropertyNames.PatchOutlet, Messages.OutletNamesAreNotUnique);
            }
        }
    }
}
