using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;

namespace JJ.Business.Synthesizer.Visitors
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
                { OperatorTypeEnum.CurveIn, VisitCurveIn },
                { OperatorTypeEnum.Divide, VisitDivide },
                { OperatorTypeEnum.Multiply, VisitMultiply },
                { OperatorTypeEnum.PatchInlet, VisitPatchInlet },
                { OperatorTypeEnum.PatchOutlet, VisitPatchOutlet },
                { OperatorTypeEnum.Power, VisitPower },
                { OperatorTypeEnum.Sample, VisitSampleOperator },
                { OperatorTypeEnum.Sine, VisitSine },
                { OperatorTypeEnum.Substract, VisitSubstract },
                { OperatorTypeEnum.TimeAdd, VisitTimeAdd },
                { OperatorTypeEnum.TimeDivide, VisitTimeDivide },
                { OperatorTypeEnum.TimeMultiply, VisitTimeMultiply },
                { OperatorTypeEnum.TimePower, VisitTimePower },
                { OperatorTypeEnum.TimeSubstract, VisitTimeSubstract },
                { OperatorTypeEnum.Value, VisitValue },
                { OperatorTypeEnum.WhiteNoise, VisitWhiteNoise },
                { OperatorTypeEnum.Resample, VisitResample },
                { OperatorTypeEnum.CustomOperator, VisitCustomOperator }
            };
        }

        protected virtual void VisitOperator(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            // TODO: Is the trick below not specific to the OptimizedPatchCalculatorVisitor?

            // Reverse the order of evaluating the inlet,
            // so that the first inlet will be the last one pushed
            // so it will be the first one popped.
            for (int i = op.Inlets.Count - 1; i >= 0; i--)
            {
                Inlet inlet = op.Inlets[i];
                VisitInlet(inlet);
            }

            Action<Operator> action = _delegateDictionary[op.GetOperatorTypeEnum()];
            action(op);
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
        protected virtual void VisitAdd(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitAdder(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitCurveIn(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitDivide(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitMultiply(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitPatchInlet(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitPatchOutlet(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitPower(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitSampleOperator(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitSine(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitSubstract(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitTimeAdd(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitTimeDivide(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitTimeMultiply(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitTimePower(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitTimeSubstract(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitValue(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitWhiteNoise(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitResample(Operator op)
        { }

        /// <summary> does nothing </summary>
        protected virtual void VisitCustomOperator(Operator op)
        { }
    }
}
