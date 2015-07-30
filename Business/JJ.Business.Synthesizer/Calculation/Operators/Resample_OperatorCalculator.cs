using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    internal class Resample_OperatorCalculator : Resample_OperatorCalculator_HermiteInterpolation
    {
        public Resample_OperatorCalculator(OperatorCalculatorBase signalCalculator, OperatorCalculatorBase samplingRateCalculator)
            : base(signalCalculator, samplingRateCalculator)
        { }
    }
}
