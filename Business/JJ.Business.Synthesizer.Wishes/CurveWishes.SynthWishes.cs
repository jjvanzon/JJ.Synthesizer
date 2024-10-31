using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Business.Synthesizer.Wishes.docs;

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

        /// <inheritdoc cref="_createcurve" />
        public FluentOutlet Curve(IEnumerable<NodeInfo> nodeInfos, [CallerMemberName] string callerMemberName = null)
        {
            return Curve(nodeInfos.ToArray(), callerMemberName);
        }

        /// <inheritdoc cref="_createcurve" />
        public FluentOutlet Curve(IList<NodeInfo> nodeInfos, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName, GetCallerNameFromStack());
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeInfos));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        /// <inheritdoc cref="_createcurve" />
        public FluentOutlet Curve(params NodeInfo[] nodeInfos)
        {
            string name = FetchName(GetCallerNameFromStack());
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeInfos));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        // Overloads with doubles

        /// <inheritdoc cref="_createcurve" />
        public FluentOutlet Curve(params double?[] values)
        {
            string name = FetchName(GetCallerNameFromStack());
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(timeSpan: 1, values));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        // Overload with Tuples

        /// <inheritdoc cref="_createcurvewithtuples" />
        public FluentOutlet Curve(
            IList<(double time, double value)> nodeTuples, 
            [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName, GetCallerNameFromStack());
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeTuples));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        /// <inheritdoc cref="_createcurvewithtuples" />
        public FluentOutlet Curve(params (double time, double value)[] nodeTuples)
        {
            string name = FetchName(GetCallerNameFromStack());
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeTuples));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        // Overload with Strings

        /// <inheritdoc cref="_createcurvefromstring" />
        public FluentOutlet Curve(string text, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName, GetCallerNameFromStack());
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(text));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        // Overload with String and Ranges

        /// <inheritdoc cref="_createcurvefromstring" />
        public FluentOutlet Curve(
            (double start, double end) x,
            (double min, double max) y,
            string text, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName, GetCallerNameFromStack());
            var wrapper = _operatorFactory.CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, text));
            AssignNames(wrapper, name);
            return _[wrapper];
        }

        // Helpers
        
        public string GetCallerNameFromStack() => new StackFrame(2).GetMethod().Name;

        private void AssignNames(CurveInWrapper wrapper, string name)
        {
            wrapper.Curve.Name = name;
            wrapper.Result.Operator.Name = name;
        }
    }
}