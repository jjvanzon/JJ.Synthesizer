using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class InletValidator_NotForCustomOperator : VersatileValidator<Inlet>
    {
        private readonly int _expectedListIndex;
        private readonly DimensionEnum _expectedDimensionEnum;

        public InletValidator_NotForCustomOperator(
            Inlet obj, 
            int expectedListIndex,
            DimensionEnum expectedDimensionEnum)
            : base(obj, postponeExecute: true)
        {
            _expectedListIndex = expectedListIndex;
            _expectedDimensionEnum = expectedDimensionEnum;

            Execute();
        }

        protected sealed override void Execute()
        {
            For(() => Obj.Name, CommonResourceFormatter.Name).IsNullOrEmpty();

            // ListIndex check DOES NOT apply to CustomOperators, 
            // because those need to be more flexible or it would become unmanageable for the user.
            For(() => Obj.ListIndex, ResourceFormatter.ListIndex).Is(_expectedListIndex);

            For(() => Obj.GetDimensionEnum(), ResourceFormatter.Dimension).Is(_expectedDimensionEnum);
        }
    }
}