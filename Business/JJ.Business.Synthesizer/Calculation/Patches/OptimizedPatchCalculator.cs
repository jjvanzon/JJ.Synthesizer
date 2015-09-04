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

        /// <summary>
        /// This overload has ChannelOutlets as params.
        /// </summary>
        public OptimizedPatchCalculator(
            WhiteNoiseCalculator whiteNoiseCalculator,
            ICurveRepository curveRepository, 
            ISampleRepository sampleRepository,
            IDocumentRepository documentRepository,
            params Outlet[] channelOutlets)
            : this((IList<Outlet>)channelOutlets, whiteNoiseCalculator, curveRepository, sampleRepository, documentRepository)
        { }

        /// <summary>
        /// This overload has ChannelOutlets as an IList<T>.
        /// </summary>
        public OptimizedPatchCalculator(
            IList<Outlet> channelOutlets,
            WhiteNoiseCalculator whiteNoiseCalculator,
            ICurveRepository curveRepository, 
            ISampleRepository sampleRepository,
            IDocumentRepository documentRepository)
        {
            if (channelOutlets == null) throw new NullException(() => channelOutlets);

            var visitor = new OptimizedPatchCalculatorVisitor();
            _rootOperatorCalculators = visitor.Execute(channelOutlets, whiteNoiseCalculator, curveRepository, sampleRepository, documentRepository).ToArray();
        }

        public double Calculate(double time, int channelIndex)
        {
            return _rootOperatorCalculators[channelIndex].Calculate(time, channelIndex);
        }
    }
}
