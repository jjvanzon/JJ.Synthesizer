using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;

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

        public FluentOutlet Curve(IEnumerable<NodeInfo> nodeInfos) => Curve(nodeInfos.ToArray());

        public FluentOutlet Curve(IList<NodeInfo> nodeInfos)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeInfos));
            string name = FetchName() ?? new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        public FluentOutlet Curve(params NodeInfo[] nodeInfos)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeInfos));
            string name = FetchName() ?? new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        // Overloads with doubles

        /// <inheritdoc cref="CurveFactory.CreateCurve(double, double?[])" />
        /// <inheritdoc cref="docs._createcurve" />
        public FluentOutlet Curve(params double?[] values)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(timeSpan: 1, values));
            string name = FetchName() ?? new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, name);
            return _[wrapper];
        }
        
        // Overload with Tuples

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public FluentOutlet Curve(IList<(double time, double value)> nodeTuples)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeTuples));
            string name = FetchName() ?? new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public FluentOutlet Curve(params (double time, double value)[] nodeTuples)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeTuples));
            string name = FetchName() ?? new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        // Overload with Strings

        /// <inheritdoc cref="docs._createcurvefromstring" />
        public FluentOutlet Curve(string text)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(text));
            string name = FetchName() ?? new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        // Overload with String and Ranges

        /// <inheritdoc cref="docs._createcurvefromstring" />
        public FluentOutlet Curve(
            (double start, double end) x,
            (double min, double max) y,
            string text)
        {
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, text));
            string name = FetchName() ?? new StackFrame(1).GetMethod().Name;
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        // Helpers

        private void AssignNames(CurveInWrapper wrapper, string uglyName)
        {
            uglyName = GetPrettyName(uglyName);
            
            wrapper.Curve.Name = uglyName;
            wrapper.Result.Operator.Name = uglyName;
        }
    }
}