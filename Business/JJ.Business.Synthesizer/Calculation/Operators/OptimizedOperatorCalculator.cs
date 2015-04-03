using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Visitors;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Calculation.Operators
{
    public class OptimizedOperatorCalculator : IOperatorCalculator
    {
        private OperatorCalculatorBase[] _rootOperatorCalculators;

        public OptimizedOperatorCalculator(params Outlet[] channelOutlets)
            : this((IList<Outlet>)channelOutlets)
        { }

        public OptimizedOperatorCalculator(IList<Outlet> channelOutlets)
        {
            if (channelOutlets == null) throw new NullException(() => channelOutlets);

            var visitor = new OptimizedOperatorCalculatorVisitor();
            _rootOperatorCalculators = visitor.Execute(channelOutlets).ToArray();
        }

        public double Calculate(double time, int channelIndex)
        {
            return _rootOperatorCalculators[channelIndex].Calculate(time, channelIndex);
        }
    }
}
