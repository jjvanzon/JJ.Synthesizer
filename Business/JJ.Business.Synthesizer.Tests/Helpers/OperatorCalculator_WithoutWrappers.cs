using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Helpers
{
    /// <summary>
    /// Variation without wrapper instantiation.
    /// Trying out a few things that might optimize things.
    /// </summary>
    internal class OperatorCalculator_WithoutWrappers : IOperatorCalculator
    {
        private IDictionary<string, Func<Operator, double, double>> _funcDictionary;

        public OperatorCalculator_WithoutWrappers()
        {
            _funcDictionary = new Dictionary<string, Func<Operator, double, double>>
            {
                { PropertyNames.ValueOperator, CalculateValueOperator },
                { PropertyNames.Add, CalculateAdd },
                { PropertyNames.Substract, CalculateSubstract },
                { PropertyNames.Multiply, CalculateMultiply },
            };
        }

        public double CalculateValue(Outlet outlet, double time)
        {
            Func<Operator, double, double> func = _funcDictionary[outlet.Operator.OperatorTypeName];
            double value = func(outlet.Operator, time);
            return value;
        }

        private double CalculateValueOperator(Operator op, double time)
        {
            return op.AsValueOperator.Value;
        }

        private double CalculateAdd(Operator op, double time)
        {
            if (op.Inlets[0].Input == null || op.Inlets[1].Input == null) return 0;

            double a = CalculateValue(op.Inlets[0].Input, time);
            double b = CalculateValue(op.Inlets[1].Input, time);

            return a + b;
        }

        private double CalculateSubstract(Operator op, double time)
        {
            if (op.Inlets[0].Input == null || op.Inlets[1].Input == null) return 0;

            double a = CalculateValue(op.Inlets[0].Input, time);
            double b = CalculateValue(op.Inlets[1].Input, time);
            return a - b;
        }

        private double CalculateMultiply(Operator op, double time)
        {
            if (op.Inlets[2].Input == null)
            {
                if (op.Inlets[0].Input == null || op.Inlets[1].Input == null) 
                {
                    return 0;
                }

                double a = CalculateValue(op.Inlets[0].Input, time);
                double b = CalculateValue(op.Inlets[1].Input, time);
                return a * b;
            }
            else
            {
                double origin = CalculateValue(op.Inlets[2].Input, time);

                if (op.Inlets[0].Input == null || op.Inlets[1].Input == null) 
                {
                    return origin;
                }

                double a = CalculateValue(op.Inlets[0].Input, time);
                double b = CalculateValue(op.Inlets[1].Input, time);
                return (a - origin) * b + origin;
            }
        }
    }
}
