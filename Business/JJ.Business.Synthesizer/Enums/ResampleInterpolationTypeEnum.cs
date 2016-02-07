using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Enums
{
    public enum ResampleInterpolationTypeEnum
    {
        Undefined,
        Block,
        Stripe,
        LineRememberT0,
        LineRememberT1,
        CubicEquidistant,
        CubicAbruptInclination,
        CubicSmoothInclination,
        Hermite
    }
}
