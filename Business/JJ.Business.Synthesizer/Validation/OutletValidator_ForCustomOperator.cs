using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OutletValidator_ForCustomOperator : FluentValidator<Outlet>
    {
        public OutletValidator_ForCustomOperator(Outlet obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            // TODO: Add certain validations?
        }
    }
}
