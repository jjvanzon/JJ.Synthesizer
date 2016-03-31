using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Documents
{
    internal class RootDocument_DocumentValidator : FluentValidator<Document>
    {
        public RootDocument_DocumentValidator(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            // Root Document should have no patches.
            For(() => Object.Patches.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Patches)).Is(0);

            For(() => Object.GroupName, PropertyDisplayNames.GroupName).IsNullOrEmpty();
        }
    }
}
