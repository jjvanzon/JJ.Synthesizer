using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithStructs.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Calculation
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public struct Multiply_OperatorCalculator_VarA_ConstB<TACalculator>
        : IOperatorCalculator_VarA_ConstB
        where TACalculator : IOperatorCalculator
    {
        private TACalculator _aCalculator;
        public IOperatorCalculator ACalculator
        {
            get => _aCalculator;
            set => _aCalculator = (TACalculator)value;
        }

        public double B { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Calculate()
        {
            double a = _aCalculator.Calculate();

            return OperatorCalculatorHelper.Multiply(a, B);
        }

        private string DebuggerDisplay => DebugHelper.GetDebuggerDisplay(this);
    }
}