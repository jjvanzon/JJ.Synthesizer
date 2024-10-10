using System;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Helpers;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public CurveInWrapper CurveIn([CallerMemberName] string key = null, params NodeInfo[] nodeInfos)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(nodeInfos)));

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, NodeInfo[])" />
        public CurveInWrapper CurveIn(string key, double timeSpan, params NodeInfo[] nodeInfos)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(timeSpan, nodeInfos)));

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, double?[])" />
        public CurveInWrapper CurveIn(string key, double timeSpan, params double?[] values)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(timeSpan, values)));

        // Overloads with Curve Wishes

        public CurveInWrapper CurveIn(string key, IList<NodeInfo> nodeInfos)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.Create(nodeInfos)));


        public CurveInWrapper CurveIn(IList<NodeInfo> nodeInfos, [CallerMemberName] string key = null)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.Create(nodeInfos)));

        // Overloads with Curves Wishes from Tuples

        /// <inheritdoc cref="docs.createcurvewithtuples" />
        public CurveInWrapper CurveIn([CallerMemberName] string key = null, params (double time, double value)[] nodeTuples)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.Create(nodeTuples)));

        /// <inheritdoc cref="docs.createcurvewithtuples" />
        public CurveInWrapper CurveIn(string key, IList<(double time, double value)> nodeTuples)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.Create(nodeTuples)));

        /// <inheritdoc cref="docs.createcurvewithtuples" />
        public CurveInWrapper CurveIn(IList<(double time, double value)> nodeTuples, [CallerMemberName] string key = null)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.Create(nodeTuples)));

        // Overloads with Curves Wishes from Strings

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(string key, IList<string> lines)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.Create(lines)));

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(IList<string> lines, [CallerMemberName] string key = null)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.Create(lines)));

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(string key, params string[] lines) 
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.Create(lines)));

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(
            string key, double start = 0, double end = 1, double min = 0, double max = 1, 
            params string[] lines)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.Create(start, end, min, max, lines)));

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(
            double start = 0, double end = 1, double min = 0, double max = 1,
            IList<string> lines = null, [CallerMemberName] string key = null)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.Create(start, end, min, max, lines)));

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(string text, [CallerMemberName] string key = null)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.Create(text)));

        // Curve Caching

        private readonly object _curveInDictionaryLock = new object();
        private readonly Dictionary<string, CurveInWrapper> _curveInDictionary =
                     new Dictionary<string, CurveInWrapper>();

        /// <inheritdoc cref="docs.createcurve" />
        private CurveInWrapper GetOrCreateCurveIn(string key, Func<CurveInWrapper> func)
        {
            if (string.IsNullOrEmpty(key))
            {
                return func();
            }
            
            lock (_curveInDictionaryLock)
            {
                if (_curveInDictionary.TryGetValue(key, out CurveInWrapper curveIn))
                {
                    return curveIn;
                }

                curveIn = func();
                _curveInDictionary[key] = curveIn;

                return curveIn;
            }
        }
    }
}
