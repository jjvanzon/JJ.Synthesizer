using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Validation
{
    internal class CustomOperatorInletValidator : FluentValidator<Inlet>
    {
        public CustomOperatorInletValidator(Inlet obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Name, CommonTitles.Name).NotNullOrWhiteSpace();
        }
    }
}
