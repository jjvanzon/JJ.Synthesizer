using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OutletValidator_ForCustomOperator : FluentValidator<Outlet>
    {
        public OutletValidator_ForCustomOperator(Outlet obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Name, CommonTitles.Name).NotNullOrWhiteSpace();
        }
    }
}
