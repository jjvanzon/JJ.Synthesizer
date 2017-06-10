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
        public InletValidator_NotForCustomOperator(Inlet inlet, DimensionEnum expectedDimensionEnum)
            : base(inlet)
        {
            For(() => inlet.GetDimensionEnum(), ResourceFormatter.Dimension).Is(expectedDimensionEnum);
        }
    }
}