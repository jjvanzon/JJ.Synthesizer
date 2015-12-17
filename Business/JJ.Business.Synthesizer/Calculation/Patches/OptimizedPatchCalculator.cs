using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Operators;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal class OptimizedPatchCalculator : IPatchCalculator
    {
        private OperatorCalculatorBase[] _rootOperatorCalculators;

        /// <summary> This overload has ChannelOutlets as params. </summary>
        /// <param name="channelOutlets">Can contain nulls.</param>
        public OptimizedPatchCalculator(
            WhiteNoiseCalculator whiteNoiseCalculator,
            ICurveRepository curveRepository, 
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository,
            params Outlet[] channelOutlets)
            : this((IList<Outlet>)channelOutlets, whiteNoiseCalculator, curveRepository, sampleRepository, patchRepository)
        { }

        /// <summary> This overload has ChannelOutlets as an IList<T>. </summary>
        /// <param name="channelOutlets">Can contain nulls.</param>
        public OptimizedPatchCalculator(
            IList<Outlet> channelOutlets,
            WhiteNoiseCalculator whiteNoiseCalculator,
            ICurveRepository curveRepository, 
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository)
        {
            if (channelOutlets == null) throw new NullException(() => channelOutlets);

            var visitor = new OptimizedPatchCalculatorVisitor(curveRepository, sampleRepository, patchRepository);
            _rootOperatorCalculators = visitor.Execute(channelOutlets, whiteNoiseCalculator).ToArray();
        }

        public double Calculate(double time, int channelIndex)
        {
            return _rootOperatorCalculators[channelIndex].Calculate(time, channelIndex);
        }
    }
}
