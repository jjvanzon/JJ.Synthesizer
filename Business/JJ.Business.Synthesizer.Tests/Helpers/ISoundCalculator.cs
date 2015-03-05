using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer
{
    internal interface ISoundCalculator
    {
        double CalculateValue(Outlet outlet, double time);
    }
}
