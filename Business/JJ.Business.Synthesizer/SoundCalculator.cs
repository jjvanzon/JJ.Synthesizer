using JJ.Business.Synthesizer.OperatorWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer
{
    public class SoundCalculator
    {
        public double GetValue(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            if (op.OperatorTypeName == PropertyNames.Value) // Reference comparisons for performance.
            {
                return outlet.Value;
            }
            else if (op.OperatorTypeName == PropertyNames.Add)
            {
                return CalculateAdd(op, time);
            }
            else if (op.OperatorTypeName == PropertyNames.Substract)
            {
                return CalculateSubstract(op, time);
            }

            throw new Exception(String.Format("OperatorTypeName value '{0}' is not supported.", op.OperatorTypeName));
        }

        private double CalculateAdd(Operator op, double time)
        {
            var wrapper = new Add(op);
            double a = GetValue(wrapper.OperandA, time);
            double b = GetValue(wrapper.OperandB, time);
            return a + b;
        }

        private double CalculateSubstract(Operator op, double time)
        {
            var wrapper = new Substract(op);
            double a = GetValue(wrapper.OperandA, time);
            double b = GetValue(wrapper.OperandB, time);
            return a - b;
        }
    }
}
