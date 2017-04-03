using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer
{
    internal interface ITestOperatorCalculator
    {
        double Calculate(Outlet outlet, double time);
    }
}
