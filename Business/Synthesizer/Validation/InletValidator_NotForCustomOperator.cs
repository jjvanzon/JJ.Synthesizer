using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
    internal class InletValidator_NotForCustomOperator : VersatileValidator<Inlet>
    {
        private readonly DimensionEnum _expectedDimensionEnum;

        public InletValidator_NotForCustomOperator(
            Inlet obj, 
            DimensionEnum expectedDimensionEnum)
            : base(obj, postponeExecute: true)
        {
            _expectedDimensionEnum = expectedDimensionEnum;

            Execute();
        }

        protected sealed override void Execute()
        {
            For(() => Obj.Name, CommonResourceFormatter.Name).IsNullOrEmpty();
            For(() => Obj.GetDimensionEnum(), ResourceFormatter.Dimension).Is(_expectedDimensionEnum);
        }
    }
}