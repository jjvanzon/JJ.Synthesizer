using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Persistence;

// ReSharper disable MemberCanBeProtected.Global

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase
    {
        // This file adds overloads to the OperatorFactory methods,
        // that create a curve, wraps it into an operator, and caches it.

        private CurveFactory _curveFactory;

        private void InitializeCurveWishes(IContext context)
        {
            _curveFactory = TestHelper.CreateCurveFactory(context);
        }

        // Overloads with NodeInfo
        
        public CurveInWrapper CurveIn(string key, IList<NodeInfo> nodeInfos)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(nodeInfos)));

        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(IList<NodeInfo> nodeInfos)
        {
            string key = new StackFrame(1).GetMethod().Name;
            return GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(nodeInfos)));
        }

        public CurveInWrapper CurveIn(string key, params NodeInfo[] nodeInfos)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(nodeInfos)));

        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(params NodeInfo[] nodeInfos)
        {
            string key = new StackFrame(1).GetMethod().Name;
            return GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(nodeInfos)));
        }

        // Overloads with doubles

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, double?[])" />
        /// <inheritdoc cref="docs.createcurve" />
        public CurveInWrapper CurveIn(string key, params double?[] values)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(timeSpan: 1, values)));

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, double?[])" />
        /// <inheritdoc cref="docs.createcurve" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(params double?[] values)
        {
            string key = new StackFrame(1).GetMethod().Name;
            return GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(timeSpan: 1, values)));
        }

        // Overload with Tuples

        /// <inheritdoc cref="docs.createcurvewithtuples" />
        public CurveInWrapper CurveIn(string key, IList<(double time, double value)> nodeTuples)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(nodeTuples)));

        /// <inheritdoc cref="docs.createcurvewithtuples" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(IList<(double time, double value)> nodeTuples)
        {
            string key = new StackFrame(1).GetMethod().Name;
            return GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(nodeTuples)));
        }

        /// <inheritdoc cref="docs.createcurvewithtuples" />
        public CurveInWrapper CurveIn(string key, params (double time, double value)[] nodeTuples)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(nodeTuples)));

        /// <inheritdoc cref="docs.createcurvewithtuples" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper  CurveIn(params (double time, double value)[] nodeTuples)
        {
            string key = new StackFrame(1).GetMethod().Name;
            return GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(nodeTuples)));
        }

        // Overload with Strings

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(string text)
        {
            string key = new StackFrame(1).GetMethod().Name;
            return GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(text)));
        }

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(string key, IList<string> lines)
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(lines)));

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(IList<string> lines)
        {
            string key = new StackFrame(1).GetMethod().Name;
            return GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(lines)));
        }

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(string requiredFirstLine, params string[] remainingLines)
        {
            string key = new StackFrame(1).GetMethod().Name;
            string[] combinedLines = new[] { requiredFirstLine }.Concat(remainingLines).ToArray();
            return GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(combinedLines)));
        }

        // Overload with String and Ranges
        
        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(
            string key, 
            (double start, double end) x,
            (double min, double max) y,
            string requiredFirstLine,
            params string[] remainingLines)
        {
            string[] combinedLines = new[] { requiredFirstLine }.Concat(remainingLines).ToArray();
            return GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, combinedLines)));
        }

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(
            (double start, double end) x, 
            (double min, double max) y,
            string line1, // Enforces at least 1 line
            params string[] remainingLines)
        {
            string key = new StackFrame(1).GetMethod().Name;
            string[] combinedLines = new[] { line1 }.Concat(remainingLines).ToArray();
            return GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, combinedLines)));
        }

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        public CurveInWrapper CurveIn(
            string key,
            (double start, double end) x,
            (double min, double max) y,
            IList<string> lines) 
            => GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, lines)));

        /// <inheritdoc cref="docs.createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(
            (double start, double end) x, 
            (double min, double max) y,
            IList<string> lines)
        {
            string key = new StackFrame(1).GetMethod().Name;
            return GetOrCreateCurveIn(key, () => CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, lines)));
        }

        // Curve Caching

        private readonly object _curveLock = new object();

        private readonly Dictionary<string, CurveInWrapper> _curveInDictionary =
            new Dictionary<string, CurveInWrapper>();

        /// <inheritdoc cref="docs.createcurve" />
        private CurveInWrapper GetOrCreateCurveIn(string key, Func<CurveInWrapper> func)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception(
                    $"Cache {nameof(key)} could not be resolved from context. " +
                    $"Consider explicitly specifying the {nameof(key)} parameter.");
            }

            lock (_curveLock)
            {
                if (_curveInDictionary.TryGetValue(key, out CurveInWrapper curveIn)) return curveIn;

                // ReSharper disable once LocalizableElement
                //Console.WriteLine($"Adding curve '{key}' to dictionary.");
                
                curveIn = func();
                _curveInDictionary[key] = curveIn;

                return curveIn;
            }
        }
    }
}