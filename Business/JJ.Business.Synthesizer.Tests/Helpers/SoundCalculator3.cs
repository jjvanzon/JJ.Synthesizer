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
    /// Variation with no null checks.
    /// Trying out a few things that might optimize things.
    /// </summary>
    internal class SoundCalculator3 : ISoundCalculator
    {
        private IDictionary<string, Func<Operator, double, double>> _funcDictionary;

        public SoundCalculator3()
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
            // TODO: This will break when there are multiple outlets.
            double value = func(outlet.Operator, time);
            return value;
        }

        private double CalculateValueOperator(Operator op, double time)
        {
            return op.Outlets[0].Value;
        }

        private double CalculateAdd(Operator op, double time)
        {
            double a = CalculateValue(op.Inlets[0].Input, time);
            double b = CalculateValue(op.Inlets[1].Input, time);
            return a + b;
        }

        private double CalculateSubstract(Operator op, double time)
        {
            double a = CalculateValue(op.Inlets[0].Input, time);
            double b = CalculateValue(op.Inlets[1].Input, time);
            return a - b;
        }

        private double CalculateMultiply(Operator op, double time)
        {
            if (op.Inlets[2].Input == null)
            {
                double a = CalculateValue(op.Inlets[0].Input, time);
                double b = CalculateValue(op.Inlets[1].Input, time);
                return a * b;
            }
            else
            {
                double origin = CalculateValue(op.Inlets[2].Input, time);
                double a = CalculateValue(op.Inlets[0].Input, time);
                double b = CalculateValue(op.Inlets[1].Input, time);
                return (a - origin) * b + origin;
            }
        }
    }
}
