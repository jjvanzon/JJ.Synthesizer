using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OutletValidator_NotForCustomOperator : VersatileValidator<Outlet>
    {
        private readonly int _expectedListIndex;
        private readonly DimensionEnum _expectedDimensionEnum;

        public OutletValidator_NotForCustomOperator(
            Outlet obj,
            int expectedListIndex,
            DimensionEnum expectedDimensionEnum)
            : base(obj, postponeExecute: true)
        {
            _expectedListIndex = expectedListIndex;
            _expectedDimensionEnum = expectedDimensionEnum;

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            For(() => Obj.Name, CommonResourceFormatter.Name).IsNullOrEmpty();

            // ListIndex check DOES NOT apply to CustomOperators, 
            // because those need to be more flexible or it would become unmanageable for the user.
            For(() => Obj.ListIndex, ResourceFormatter.ListIndex).Is(_expectedListIndex);

            For(() => Obj.GetDimensionEnum(), ResourceFormatter.Dimension).Is(_expectedDimensionEnum);
        }
    }
}