using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Shared;
using JJ.Demos.Synthesizer.Inlining.WithInheritance;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Helpers
{
    internal static class OperatorCalculatorFactory_WithInheritance
    {
        public static OperatorCalculatorBase CreateOperatorCalculatorStructure_8Partials(DimensionStack dimensionStack)
        {
            var calculator = new Add_OperatorCalculator_8Vars(
                CreateOperatorCalculatorStructure_SinglePartial(dimensionStack),
                CreateOperatorCalculatorStructure_SinglePartial(dimensionStack),
                CreateOperatorCalculatorStructure_SinglePartial(dimensionStack),
                CreateOperatorCalculatorStructure_SinglePartial(dimensionStack),
                CreateOperatorCalculatorStructure_SinglePartial(dimensionStack),
                CreateOperatorCalculatorStructure_SinglePartial(dimensionStack),
                CreateOperatorCalculatorStructure_SinglePartial(dimensionStack),
                CreateOperatorCalculatorStructure_SinglePartial(dimensionStack));

            return calculator;
        }

        public static OperatorCalculatorBase CreateOperatorCalculatorStructure_SinglePartial(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            double frequency = 440.0;
            double volume = 10.0;
            double phaseShift = 0.25;

            var frequency_VariableInput_Calculator = new VariableInput_OperatorCalculator();
            frequency_VariableInput_Calculator._value = frequency;

            var calculator =
                new Multiply_OperatorCalculator_VarA_ConstB
                (
                    new Shift_OperatorCalculator_VarSignal_ConstDifference
                    (
                        new Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        (
                            frequency_VariableInput_Calculator,
                            dimensionStack
                        ),
                        phaseShift,
                        dimensionStack
                    ),
                    volume
                );

            return calculator;
        }
    }
}
