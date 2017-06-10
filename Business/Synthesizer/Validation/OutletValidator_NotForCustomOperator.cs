using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OutletValidator_NotForCustomOperator : VersatileValidator<Outlet>
    {
        private readonly DimensionEnum _expectedDimensionEnum;

        public OutletValidator_NotForCustomOperator(
            Outlet obj,
            DimensionEnum expectedDimensionEnum)
            : base(obj, postponeExecute: true)
        {
            _expectedDimensionEnum = expectedDimensionEnum;

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            For(() => Obj.Name, CommonResourceFormatter.Name).IsNullOrEmpty();
            For(() => Obj.GetDimensionEnum(), ResourceFormatter.Dimension).Is(_expectedDimensionEnum);
        }
    }
}