using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Shared;
using JJ.Demos.Synthesizer.Inlining.WithInheritance;

namespace JJ.Demos.Synthesizer.Inlining.Helpers
{
    internal static class OperatorCalculatorFactory_WithInheritance
    {
        public static OperatorCalculatorBase CreateOperatorCalculatorStructure()
        {
            var dimensionStack = new DimensionStack();

            double volume = 10.0;
            double phaseShift = 0.25;

            var rootCalculator =
                new Multiply_OperatorCalculator_VarA_ConstB
                (
                    new Shift_OperatorCalculator_VarSignal_ConstDifference
                    (
                        new Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        (
                            new VariableInput_OperatorCalculator(),
                            dimensionStack
                        ),
                        phaseShift,
                        dimensionStack
                    ),
                    volume
                );

            return rootCalculator;
        }
    }
}
