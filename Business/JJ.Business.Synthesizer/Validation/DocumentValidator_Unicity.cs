using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DocumentValidator_Unicity : FluentValidator<Scale>
    {
        public DocumentValidator_Unicity(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            ValidateScaleNamesUnique();
        }

        private void ValidateScaleNamesUnique()
        {
            IList<string> names = Object.Document.EnumerateSelfAndParentAndChildren()
                                                 .SelectMany(x => x.Scales)
                                                 .Select(x => x.Name)
                                                 .ToArray();

            int uniqueNameCount = names.Distinct().Count();

            if (names.Count != uniqueNameCount)
            {
                // TODO: Low priority: report what the duplicate name is.
                ValidationMessages.Add(PropertyNames.Scales, Messages.ScaleNamesNotUnique);
            }
        }
    }
}
