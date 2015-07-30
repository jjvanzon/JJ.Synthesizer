using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    public interface IPatchCalculator
    {
        double Calculate(double time, int channelIndex);
    }
}
