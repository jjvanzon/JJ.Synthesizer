using JJ.Business.Synthesizer.ExtendedEntities;
using JJ.Business.Synthesizer.Names;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer
{
    public class Calculator
    {
        public double GetValue(Outlet outlet, double time)
        {
            Operator op = outlet.Operator;

            if (op.OperatorTypeName == ObjectNames.Value) // Reference comparisons for performance.
            {
                return outlet.Value;
            }
            else if (op.OperatorTypeName == ObjectNames.Add)
            {
                return CalculateAdd(op, time);
            }

            throw new Exception(String.Format("OperatorTypeName value '{0}' is not supported.", op.OperatorTypeName));
        }

        private double CalculateAdd(Operator op, double time)
        {
            Add add = new Add(op);
            double a = GetValue(add.OperandA, time);
            double b = GetValue(add.OperandB, time);
            return a + b;
        }
    }
}
