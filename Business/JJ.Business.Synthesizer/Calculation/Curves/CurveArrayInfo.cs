using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Curves
{
    internal class CurveArrayInfo
    {
        public double[] Array { get; set; }
        public double MinTime { get; set; }
        public double Rate { get; set; }
        public double ValueAfter { get; set; }
        public double ValueBefore { get; set; }
    }
}
