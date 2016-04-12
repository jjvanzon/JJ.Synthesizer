using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal class CurveArrayInfo
    {
        public double[] Array { get; set; }
        public double MinX { get; set; }
        public double Rate { get; set; }
        public double YAfter { get; set; }
        public double YBefore { get; set; }
    }
}
