using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Dto
{
    internal abstract class OperatorDtoBase_Filter_ManyConsts_WithWidthOrBlobVolume : OperatorDtoBase_Filter_ManyConsts
    {
        public abstract double WidthOrBlobVolume { get; }
    }
}
