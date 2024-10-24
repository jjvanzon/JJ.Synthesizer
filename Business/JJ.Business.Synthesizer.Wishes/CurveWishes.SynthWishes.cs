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

        public FluentOutlet Curve(string name, IList<NodeInfo> nodeInfos)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeInfos));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public FluentOutlet Curve(IList<NodeInfo> nodeInfos)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeInfos));
            
            string hintName = new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, hintName);
            
            return _[wrapper];
        }

        public FluentOutlet Curve(string name, params NodeInfo[] nodeInfos)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeInfos));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public FluentOutlet Curve(params NodeInfo[] nodeInfos)
        {
            CurveInWrapper wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeInfos));

            string hintName = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            AssignNames(wrapper, hintName);
            
            return _[wrapper];
        }

        // Overloads with doubles

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, double?[])" />
        /// <inheritdoc cref="docs._createcurve" />
        public FluentOutlet Curve(string name, params double?[] values)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(timeSpan: 1, values));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, double?[])" />
        /// <inheritdoc cref="docs._createcurve" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public FluentOutlet Curve(params double?[] values)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(timeSpan: 1, values));
            
            string hintName = new StackFrame(1).GetMethod().Name.CutLeft("get_");
            AssignNames(wrapper, hintName);
            
            return _[wrapper];
        }

        // Overload with Tuples

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public FluentOutlet Curve(string name, IList<(double time, double value)> nodeTuples)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeTuples));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public FluentOutlet Curve(IList<(double time, double value)> nodeTuples)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeTuples));

            string hintName = new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, hintName);
            
            return _[wrapper];
        }

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public FluentOutlet Curve(string name, params (double time, double value)[] nodeTuples)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeTuples));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public FluentOutlet Curve(params (double time, double value)[] nodeTuples)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeTuples));
            
            string hintName = new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, hintName);
            
            return _[wrapper];
        }

        // Overload with Strings

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public FluentOutlet Curve(string text)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(text));
            
            string hintName = new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, hintName);
            
            return _[wrapper];
        }

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        public FluentOutlet Curve(string name, IList<string> lines)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(lines));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public FluentOutlet Curve(IList<string> lines)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(lines));
            
            string hintName = new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, hintName);
            
            return _[wrapper];
        }

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public FluentOutlet Curve(string requiredFirstLine, params string[] remainingLines)
        {
            string[] combinedLines = new[] { requiredFirstLine }.Concat(remainingLines).ToArray();
            
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(combinedLines));

            string hint = new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, hint);
            
            return _[wrapper];
        }

        // Overload with String and Ranges

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        public FluentOutlet Curve(
            string name,
            (double start, double end) x,
            (double min, double max) y,
            string requiredFirstLine,
            params string[] remainingLines)
        {
            string[] combinedLines = new[] { requiredFirstLine }.Concat(remainingLines).ToArray();
            
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, combinedLines));
            
            AssignNames(wrapper, name);
            
            return _[wrapper];
        }

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public FluentOutlet Curve(
            (double start, double end) x,
            (double min, double max) y,
            string line1, // Enforces at least 1 line
            params string[] remainingLines)
        {
            string[] combinedLines = new[] { line1 }.Concat(remainingLines).ToArray();
            
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, combinedLines));

            string hintName = new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, hintName);
            
            return _[wrapper];
        }

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        public FluentOutlet Curve(
            string name,
            (double start, double end) x,
            (double min, double max) y,
            IList<string> lines)
            => _[_operatorFactory.CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, lines))];

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        [MethodImpl(MethodImplOptions.NoInlining)]
        public FluentOutlet Curve(
            (double start, double end) x,
            (double min, double max) y,
            IList<string> lines)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, lines));
            
            string hintName = new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, hintName);
            
            return _[wrapper];
        }

        // Helpers

        private void AssignNames(CurveInWrapper wrapper, string hintName)
        {
            hintName = hintName ?? "";
            hintName = hintName.CutLeft("get_").CutLeft("set_");
            
            wrapper.Curve.Name = hintName;
            wrapper.Result.Operator.Name = hintName;
        }
    }
}