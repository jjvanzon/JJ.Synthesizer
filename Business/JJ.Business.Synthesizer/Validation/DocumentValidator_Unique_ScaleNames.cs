using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DocumentValidator_Unique_ScaleNames : FluentValidator<Document>
    {
        public DocumentValidator_Unique_ScaleNames(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            ValidateScaleNamesUnique();
        }

        private void ValidateScaleNamesUnique()
        {
            IList<string> names = Object.EnumerateSelfAndParentAndChildren()
                                        .SelectMany(x => x.Scales)
                                        .Select(x => x.Name)
                                        .ToArray();

            int uniqueNameCount = names.Distinct().Count();

            if (names.Count != uniqueNameCount)
            {
                IList<string> duplicateNames = Object.EnumerateSelfAndParentAndChildren()
                                                     .SelectMany(x => x.Scales)
                                                     .GroupBy(x => x.Name)
                                                     .Where(x => x.Count() > 1)
                                                     .Select(x => x.Key)
                                                     .ToArray();

                string message = MessageFormatter.NamesNotUnique_WithEntityTypeNameAndNames(PropertyDisplayNames.Scale, duplicateNames);
                ValidationMessages.Add(PropertyNames.Scales, message);
            }
        }
    }
}
