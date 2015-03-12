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

        protected virtual void Visit(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            Action<Operator> action = _delegateDictionary[op.OperatorTypeName];
            action(op);

            for (int i = 0; i < op.Inlets.Count; i++)
            {
                Outlet outlet = op.Inlets[i].Input;
                // TODO: This code will break if an operator can have multiple outlets.
                Operator op2 = outlet.Operator; 
                Visit(op2);
            }
        }

        protected virtual void VisitAdd(Operator op)
        { }

        protected virtual void VisitAdder(Operator op)
        { }

        protected virtual void VisitCurveIn(Operator op)
        { }

        protected virtual void VisitDivide(Operator op)
        { }

        protected virtual void VisitMultiply(Operator op)
        { }

        protected virtual void VisitPatchInlet(Operator op)
        { }

        protected virtual void VisitPatchOutlet(Operator op)
        { }

        protected virtual void VisitPower(Operator op)
        { }

        protected virtual void VisitSampleOperator(Operator op)
        { }

        protected virtual void VisitSine(Operator op)
        { }

        protected virtual void VisitSubstract(Operator op)
        { }

        protected virtual void VisitTimeAdd(Operator op)
        { }

        protected virtual void VisitTimeDivide(Operator op)
        { }

        protected virtual void VisitTimeMultiply(Operator op)
        { }

        protected virtual void VisitTimePower(Operator op)
        { }

        protected virtual void VisitTimeSubstract(Operator op)
        { }

        protected virtual void VisitValueOperator(Operator op)
        { }
    }
}
