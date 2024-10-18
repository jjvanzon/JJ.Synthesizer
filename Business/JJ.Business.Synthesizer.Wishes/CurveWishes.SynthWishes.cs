using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;

// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        // This file adds overloads to the OperatorFactory methods,
        // that create a curve, wraps it into an operator, and caches it.

        private CurveFactory _curveFactory;

        private void InitializeCurveWishes(IContext context)
        {
            _curveFactory = ServiceFactory.CreateCurveFactory(context);
        }

        // Overloads with NodeInfo
        
        public CurveInWrapper CurveIn(string name, IList<NodeInfo> nodeInfos)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(nodeInfos)));

        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(IList<NodeInfo> nodeInfos)
        {
            string name = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            return GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(nodeInfos)));
        }

        public CurveInWrapper CurveIn(string name, params NodeInfo[] nodeInfos)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(nodeInfos)));

        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(params NodeInfo[] nodeInfos)
        {
            string name = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            return GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(nodeInfos)));
        }

        // Overloads with doubles

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, double?[])" />
        /// <inheritdoc cref="docs._createcurve" />
        public CurveInWrapper CurveIn(string name, params double?[] values)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(timeSpan: 1, values)));

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, double?[])" />
        /// <inheritdoc cref="docs._createcurve" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(params double?[] values)
        {
            string name = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            return GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(timeSpan: 1, values)));
        }

        // Overload with Tuples

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public CurveInWrapper CurveIn(string name, IList<(double time, double value)> nodeTuples)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(nodeTuples)));

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(IList<(double time, double value)> nodeTuples)
        {
            string name = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            return GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(nodeTuples)));
        }

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public CurveInWrapper CurveIn(string name, params (double time, double value)[] nodeTuples)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(nodeTuples)));

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper  CurveIn(params (double time, double value)[] nodeTuples)
        {
            string name = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            return GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(nodeTuples)));
        }

        // Overload with Strings

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(string text)
        {
            string name = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            return GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(text)));
        }

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        public CurveInWrapper CurveIn(string name, IList<string> lines)
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(lines)));

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(IList<string> lines)
        {
            string name = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            return GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(lines)));
        }

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(string requiredFirstLine, params string[] remainingLines)
        {
            string name = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            string[] combinedLines = new[] { requiredFirstLine }.Concat(remainingLines).ToArray();
            return GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(combinedLines)));
        }

        // Overload with String and Ranges
        
        /// <inheritdoc cref="docs._createcurvefromstrings" />
        public CurveInWrapper CurveIn(
            string name, 
            (double start, double end) x,
            (double min, double max) y,
            string requiredFirstLine,
            params string[] remainingLines)
        {
            string[] combinedLines = new[] { requiredFirstLine }.Concat(remainingLines).ToArray();
            return GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, combinedLines)));
        }

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(
            (double start, double end) x, 
            (double min, double max) y,
            string line1, // Enforces at least 1 line
            params string[] remainingLines)
        {
            string name = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            string[] combinedLines = new[] { line1 }.Concat(remainingLines).ToArray();
            return GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, combinedLines)));
        }

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        public CurveInWrapper CurveIn(
            string name,
            (double start, double end) x,
            (double min, double max) y,
            IList<string> lines) 
            => GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, lines)));

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public CurveInWrapper CurveIn(
            (double start, double end) x, 
            (double min, double max) y,
            IList<string> lines)
        {
            string name = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            return GetOrCreateCurveIn(name, () => CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, lines)));
        }

        // Curve Caching

        public CurveInWrapper GetCurve(string name)
        {
            lock (_curveLock)
            {
                if (_curveInDictionary.TryGetValue(name, out CurveInWrapper curveInWrapper))
                {
                    return curveInWrapper;
                    
                }
                
                throw new Exception($"{nameof(Curve)} with {nameof(name)} '{name}' not found.");
            }
        }

        private readonly object _curveLock = new object();

        private readonly Dictionary<string, CurveInWrapper> _curveInDictionary =
            new Dictionary<string, CurveInWrapper>();

        /// <inheritdoc cref="docs._createcurve" />
        internal CurveInWrapper GetOrCreateCurveIn(string name, Func<CurveInWrapper> func)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception(
                    "Cache key could not be resolved from context. " +
                    "Consider explicitly specifying the name parameter.");
            }

            lock (_curveLock)
            {
                if (_curveInDictionary.TryGetValue(name, out CurveInWrapper curveInWrapper))
                {
                    Console.WriteLine($"Curve {name} reused");
                    
                    return curveInWrapper;
                }
                

                curveInWrapper = func();

                // Assign names
                {
                    Outlet outlet = curveInWrapper;
                    outlet.Operator.Name = name;
                    curveInWrapper.Curve.Name = name;
                }

                _curveInDictionary[name] = curveInWrapper;

                Console.WriteLine($"Curve {name} cached");

                return curveInWrapper;
            }
        }
    }
}