using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class DocumentValidator_RootDocument : FluentValidator<Document>
    {
        public DocumentValidator_RootDocument(Document obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            // Root Document should have no patches.
            For(() => Object.MainPatch, PropertyDisplayNames.MainPatch).IsNull();
            For(() => Object.Patches.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Patches)).Is(0);
        }
    }
}
