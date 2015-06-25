using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Visitors;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
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

        /// <summary>
        /// This overload has ChannelOutlets as params.
        /// </summary>
        public OptimizedOperatorCalculator(ICurveRepository curveRepository, ISampleRepository sampleRepository, params Outlet[] channelOutlets)
            : this((IList<Outlet>)channelOutlets, curveRepository, sampleRepository)
        { }

        /// <summary>
        /// This overload has ChannelOutlets as an IList<T>.
        /// </summary>
        public OptimizedOperatorCalculator(IList<Outlet> channelOutlets, ICurveRepository curveRepository, ISampleRepository sampleRepository)
        {
            if (channelOutlets == null) throw new NullException(() => channelOutlets);

            var visitor = new OptimizedOperatorCalculatorVisitor();
            _rootOperatorCalculators = visitor.Execute(channelOutlets, curveRepository, sampleRepository).ToArray();
        }

        public double Calculate(double time, int channelIndex)
        {
            return _rootOperatorCalculators[channelIndex].Calculate(time, channelIndex);
        }
    }
}
