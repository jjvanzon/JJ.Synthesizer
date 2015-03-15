using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class TestOperatorCalculator_WithWrappersAndNullChecks : ITestOperatorCalculator
    {
        private IDictionary<string, Func<Operator, double, double>> _funcDictionary;

        public TestOperatorCalculator_WithWrappersAndNullChecks()
        {
            _funcDictionary = new Dictionary<string, Func<Operator, double, double>>
            {
                { PropertyNames.ValueOperator, CalculateValueOperator },
                { PropertyNames.Add, CalculateAdd },
                { PropertyNames.Substract, CalculateSubstract },
                { PropertyNames.Multiply, CalculateMultiply },
            };
        }

        public double Calculate(Outlet outlet, double time)
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
            var wrapper = new AddWrapper(op);

            if (wrapper.OperandA == null || wrapper.OperandB == null) return 0;

            double a = Calculate(wrapper.OperandA, time);
            double b = Calculate(wrapper.OperandB, time);
            return a + b;
        }

        private double CalculateSubstract(Operator op, double time)
        {
            var wrapper = new SubstractWrapper(op);

            if (wrapper.OperandA == null || wrapper.OperandB == null) return 0;

            double a = Calculate(wrapper.OperandA, time);
            double b = Calculate(wrapper.OperandB, time);
            return a - b;
        }

        private double CalculateMultiply(Operator op, double time)
        {
            var wrapper = new MultiplyWrapper(op);

            if (wrapper.Origin == null)
            {
                if (wrapper.OperandA == null || wrapper.OperandB == null)
                {
                    return 0;
                }

                double a = Calculate(wrapper.OperandA, time);
                double b = Calculate(wrapper.OperandB, time);
                return a * b;
            }
            else
            {
                double origin = Calculate(wrapper.Origin, time);

                if (wrapper.OperandA == null || wrapper.OperandB == null)
                {
                    return origin;
                }

                double a = Calculate(wrapper.OperandA, time);
                double b = Calculate(wrapper.OperandB, time);
                return (a - origin) * b + origin;
            }
        }
    }
}