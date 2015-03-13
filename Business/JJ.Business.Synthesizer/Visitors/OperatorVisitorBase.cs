using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Visitors
{
    internal abstract class OperatorVisitorBase
    {
        private IDictionary<string, Action<Operator>> _delegateDictionary;

        public OperatorVisitorBase()
        {
            _delegateDictionary = new Dictionary<string, Action<Operator>>
            {
                { PropertyNames.Add, VisitAdd },
                { PropertyNames.Adder, VisitAdder },
                { PropertyNames.CurveIn, VisitCurveIn },
                { PropertyNames.Divide, VisitDivide },
                { PropertyNames.Multiply, VisitMultiply },
                { PropertyNames.PatchInlet, VisitPatchInlet },
                { PropertyNames.PatchOutlet, VisitPatchOutlet },
                { PropertyNames.Power, VisitPower },
                { PropertyNames.SampleOperator, VisitSampleOperator },
                { PropertyNames.Sine, VisitSine },
                { PropertyNames.Substract, VisitSubstract },
                { PropertyNames.TimeAdd, VisitTimeAdd },
                { PropertyNames.TimeDivide, VisitTimeDivide },
                { PropertyNames.TimeMultiply, VisitTimeMultiply },
                { PropertyNames.TimePower, VisitTimePower },
                { PropertyNames.TimeSubstract, VisitTimeSubstract },
                { PropertyNames.ValueOperator, VisitValueOperator },
            };
        }

        protected virtual void VisitOperator(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            // Reverse the order of evaluating the inlet,
            // so that the first inlet will be the last one pushed
            // so it will be the first one popped.
            for (int i = op.Inlets.Count - 1; i >= 0; i--)
            {
                Inlet inlet = op.Inlets[i];
                VisitInlet(inlet);
            }

            Action<Operator> action = _delegateDictionary[op.OperatorTypeName];
            action(op);
        }

        protected virtual void VisitInlet(Inlet inlet)
        {
            Outlet outlet = inlet.Input;

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
        protected virtual void VisitValueOperator(Operator op)
        { }
    }
}
