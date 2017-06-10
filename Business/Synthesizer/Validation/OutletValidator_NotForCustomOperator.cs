using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OutletValidator_NotForCustomOperator : VersatileValidator<Outlet>
    {
        public OutletValidator_NotForCustomOperator(Outlet obj, DimensionEnum expectedDimensionEnum)
            : base(obj)
        {
            For(() => obj.GetDimensionEnum(), ResourceFormatter.Dimension).Is(expectedDimensionEnum);
        }
    }
}