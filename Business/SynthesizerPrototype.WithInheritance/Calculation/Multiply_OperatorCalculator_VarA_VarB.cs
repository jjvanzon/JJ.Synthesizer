using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithInheritance.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Calculation
{
    internal class Multiply_OperatorCalculator_VarA_VarB : OperatorCalculatorBase
    {
        private readonly OperatorCalculatorBase _aCalculator;
        private readonly OperatorCalculatorBase _bCalculator;

        public Multiply_OperatorCalculator_VarA_VarB(
            OperatorCalculatorBase aCalculator,
            OperatorCalculatorBase bCalculator)
        {
            _aCalculator = aCalculator ?? throw new NullException(() => aCalculator);
            _bCalculator = bCalculator ?? throw new NullException(() => bCalculator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Calculate()
        {
            double a = _aCalculator.Calculate();
            double b = _bCalculator.Calculate();
            double result = OperatorCalculatorHelper.Multiply(a, b);
            return result;
        }
    }
}
