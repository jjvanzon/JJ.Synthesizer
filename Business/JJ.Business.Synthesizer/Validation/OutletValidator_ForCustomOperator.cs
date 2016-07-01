using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
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
            For(() => Object.GetDimensionEnum(), PropertyDisplayNames.Dimension).IsEnum<DimensionEnum>();
        }
    }
}
