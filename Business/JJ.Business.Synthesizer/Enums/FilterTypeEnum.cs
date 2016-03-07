using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Enums
{
    /// <summary> Enumerates the filter types supported by NAudio BiQuadFilter. </summary>
    public enum FilterTypeEnum
    {
        Undefined,
        LowPassFilter,
        HighPassFilter,
        BandPassFilterConstantSkirtGain,
        BandPassFilterConstantPeakGain,
        NotchFilter,
        AllPassFilter,
        PeakingEQ,
        LowShelf,
        HighShelf
    }
}
