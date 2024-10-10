using System;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Helpers;
using System.Collections.Generic;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Infos;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthesizerSugarBase
    {
        // This file adds overloads to the OperatorFactory methods,
        // that create a curve, wraps it into an operator, and caches it.
        
        private readonly CurveFactory _curveFactory;

        // Overloads with CurveFactory

        /// <inheritdoc cref="CurveFactory.CreateCurve(NodeInfo[])" />
        public CurveInWrapper CurveIn(string name, params NodeInfo[] nodeInfos)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(nodeInfos)));

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, NodeInfo[])" />
        public CurveInWrapper CurveIn(string name, double timeSpan, params NodeInfo[] nodeInfos)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(timeSpan, nodeInfos)));

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, double?[])" />
        public CurveInWrapper CurveIn(string name, double timeSpan, params double?[] values)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(timeSpan, values)));

        // Overloads with Curve Wishes

        public CurveInWrapper CurveIn(string name, IList<NodeInfo> nodeInfos)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.Create(nodeInfos)));

        // Overloads with Curves Wishes from Tuples

        /// <inheritdoc cref="docs.createcurvewithtuples" />
        public CurveInWrapper CurveIn(string name, params (double time, double value)[] nodeTuples)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.Create(nodeTuples)));

        /// <inheritdoc cref="docs.createcurvewithtuples" />
        public CurveInWrapper CurveIn(string name, IList<(double time, double value)> nodeTuples)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.Create(nodeTuples)));

        // Overloads with Curves Wishes from Strings

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(string name, IList<string> lines) 
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.Create(lines)));

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(string name, params string[] lines) 
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.Create(lines)));

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(
            string name, double start = 0, double end = 1, double min = 0, double max = 1, 
            params string[] lines)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.Create(start, end, min, max, lines)));

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(
            string name, double start = 0, double end = 1, double min = 0, double max = 1, 
            IList<string> lines = null)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.Create(start, end, min, max, lines)));

        // Curve Caching

        private readonly object _curveInDictionaryLock = new object();
        private readonly Dictionary<string, CurveInWrapper> _curveInDictionary =
                     new Dictionary<string, CurveInWrapper>();

        private CurveInWrapper GetOrCreateCurveIn(string name, Func<CurveInWrapper> func)
        {
            lock (_curveInDictionaryLock)
            {
                if (_curveInDictionary.TryGetValue(name, out CurveInWrapper curveIn))
                {
                    return curveIn;
                }

                curveIn = func();
                _curveInDictionary[name] = curveIn;

                return curveIn;
            }
        }
    }
}
