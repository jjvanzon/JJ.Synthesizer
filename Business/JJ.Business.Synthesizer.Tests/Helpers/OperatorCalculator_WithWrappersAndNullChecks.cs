﻿using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;

namespace JJ.Business.Synthesizer.Tests
{
    public class OperatorCalculator_WithWrappersAndNullChecks : IOperatorCalculator
    {
        private IDictionary<string, Func<Operator, double, double>> _funcDictionary;

        public OperatorCalculator_WithWrappersAndNullChecks()
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