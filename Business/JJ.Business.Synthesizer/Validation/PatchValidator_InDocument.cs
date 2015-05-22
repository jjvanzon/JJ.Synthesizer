using JJ.Business.Synthesizer.Constants;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary>
    /// A patch in a document has additional rules,
    /// for instance that the name be filled in.
    /// </summary>
    public class PatchValidator_InDocument : FluentValidator<Patch>
    {
        public PatchValidator_InDocument(Patch obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Patch patch = Object;

            if (patch == null) throw new NullException(() => patch);

            For(() => patch.Name, CommonTitles.Name)
                .NotNullOrWhiteSpace()
                .MaxLength(DefaultConstants.NAME_MAX_LENGTH)
                .NotInteger();

            For(() => patch.Document, PropertyDisplayNames.Document)
                .NotNull();
        }
    }
}
