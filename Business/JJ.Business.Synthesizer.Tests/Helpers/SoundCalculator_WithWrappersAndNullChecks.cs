using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests
{
    public class SoundCalculator_WithWrappersAndNullChecks : ISoundCalculator
    {
        private IDictionary<string, Func<Operator, double, double>> _funcDictionary;

        public SoundCalculator_WithWrappersAndNullChecks()
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
            var wrapper = new ValueOperatorWrapper(op);
            return wrapper.Value;
        }

        private double CalculateAdd(Operator op, double time)
        {
            var wrapper = new Add(op);

            if (wrapper.OperandA == null || wrapper.OperandB == null) return 0;

            double a = CalculateValue(wrapper.OperandA, time);
            double b = CalculateValue(wrapper.OperandB, time);
            return a + b;
        }

        private double CalculateSubstract(Operator op, double time)
        {
            var wrapper = new Substract(op);

            if (wrapper.OperandA == null || wrapper.OperandB == null) return 0;

            double a = CalculateValue(wrapper.OperandA, time);
            double b = CalculateValue(wrapper.OperandB, time);
            return a - b;
        }

        private double CalculateMultiply(Operator op, double time)
        {
            var wrapper = new Multiply(op);

            if (wrapper.Origin == null)
            {
                if (wrapper.OperandA == null || wrapper.OperandB == null)
                {
                    return 0;
                }

                double a = CalculateValue(wrapper.OperandA, time);
                double b = CalculateValue(wrapper.OperandB, time);
                return a * b;
            }
            else
            {
                double origin = CalculateValue(wrapper.Origin, time);

                if (wrapper.OperandA == null || wrapper.OperandB == null)
                {
                    return origin;
                }

                double a = CalculateValue(wrapper.OperandA, time);
                double b = CalculateValue(wrapper.OperandB, time);
                return (a - origin) * b + origin;
            }
        }
    }
}