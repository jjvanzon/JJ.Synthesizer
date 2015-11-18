using System;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    /// <summary> For operators other than CustomOperator. </summary>
    internal class OutletValidator_ForOtherOperator : FluentValidator<Outlet>
    {
        /// <summary> For operators other than CustomOperator. </summary>
        public OutletValidator_ForOtherOperator(Outlet obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            For(() => Object.Name, CommonTitles.Name).IsNullOrEmpty();
        }
    }
}
