using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class ScaleValidator_InDocument : FluentValidator<Scale>
    {
        public ScaleValidator_InDocument(Scale obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Document, PropertyDisplayNames.Document).NotNull();

            Execute(new NameValidator(Object.Name, required: true));
        }
    }
}
