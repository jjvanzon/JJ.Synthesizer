using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Calculation.Patches
{
    internal abstract class OperatorVisitorBase
    {
        private IDictionary<OperatorTypeEnum, Action<Operator>> _delegateDictionary;

        public OperatorVisitorBase()
        {
            _delegateDictionary = new Dictionary<OperatorTypeEnum, Action<Operator>>
            {
                { OperatorTypeEnum.Add, VisitAdd },
                { OperatorTypeEnum.Adder, VisitAdder },
                { OperatorTypeEnum.Curve, VisitCurveOperator },
                { OperatorTypeEnum.Divide, VisitDivide },
                { OperatorTypeEnum.Multiply, VisitMultiply },
                { OperatorTypeEnum.Power, VisitPower },
                { OperatorTypeEnum.Resample, VisitResample },
                { OperatorTypeEnum.Sample, VisitSampleOperator },
                { OperatorTypeEnum.Sine, VisitSine },
                { OperatorTypeEnum.Substract, VisitSubstract },
                { OperatorTypeEnum.Delay, VisitDelay },
                { OperatorTypeEnum.SpeedUp, VisitSpeedUp },
                { OperatorTypeEnum.SlowDown, VisitSlowDown },
                { OperatorTypeEnum.TimePower, VisitTimePower },
                { OperatorTypeEnum.TimeSubstract, VisitTimeSubstract },
                { OperatorTypeEnum.Value, VisitValue },
                { OperatorTypeEnum.WhiteNoise, VisitWhiteNoise }
            };
        }

        protected virtual void VisitOperator(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            // TODO: Is the trick below not specific to the OptimizedPatchCalculatorVisitor?

            // Reverse the order of evaluating the inlet,
            // so that the first inlet will be the last one pushed
            // so it will be the first one popped.
            IList<Inlet> inlets = op.Inlets.OrderByDescending(x => x.SortOrder).ToArray();
            foreach (Inlet inlet in inlets)
            {
                VisitInlet(inlet);
            }

            Action<Operator> action;
            if (_delegateDictionary.TryGetValue(op.GetOperatorTypeEnum(), out action))
            {
                action(op);
            }
        }

        protected virtual void VisitInlet(Inlet inlet)
        {
            Outlet outlet = inlet.InputOutlet;

            if (outlet != null)
            {
                VisitOutlet(outlet);
            }
        }

        protected virtual void VisitOutlet(Outlet outlet)
        {
            Operator op = outlet.Operator;
            VisitOperator(op);
        }

        /// <summary> does nothing </summary>
        protected virtual void VisitAdd(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitAdder(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitCurveOperator(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitDivide(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitMultiply(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitPower(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitResample(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitSampleOperator(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitSine(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitSubstract(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitDelay(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitSpeedUp(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitSlowDown(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitTimePower(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitTimeSubstract(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitValue(Operator op) { }

        /// <summary> does nothing </summary>
        protected virtual void VisitWhiteNoise(Operator op) { }
    }
}
