using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    public interface ICurveCalculator
    {
        double CalculateY(double x);
    }
}
