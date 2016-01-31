using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer.Calculation.Random
{
    internal static class RandomCalculatorFactory
    {
        /// <summary>
        /// Returns a base class, not an interface, because dispatch through a base class is faster than using an interface.
        /// </summary>
        public static RandomCalculatorBase CreateRandomCalculator(InterpolationTypeEnum interpolationTypeEnum)
        {
            switch (interpolationTypeEnum)
            {
                case InterpolationTypeEnum.Block:
                    return new RandomCalculator_BlockInterpolation();

                case InterpolationTypeEnum.Line:
                    return new RandomCalculator_LineInterpolation();

                default:
                    throw new ValueNotSupportedException(interpolationTypeEnum);
            }
        }
    }
}
