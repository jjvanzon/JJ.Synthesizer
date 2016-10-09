using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Shared;
using JJ.Demos.Synthesizer.Inlining.WithGenericMutableStructsAndHelpers;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Helpers
{
    internal static class OperatorCalculatorFactory_WithGenericMutableStructsAndHelpers
    {
        public static
            Multiply_OperatorCalculator_VarA_ConstB
            <
                Shift_OperatorCalculator_VarSignal_ConstDifference
                <
                    Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                    <
                        VariableInput_OperatorCalculator
                    >
                >
            > CreateOperatorCalculatorStructure(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            double frequency = 440.0;
            double volume = 10.0;
            double phaseShift = 0.25;

            var multiplyCalculator =
                new Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDifference
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >();

            multiplyCalculator._b = volume;
            multiplyCalculator._aCalculator._dimensionStack = dimensionStack;
            multiplyCalculator._aCalculator._distance = phaseShift;

            multiplyCalculator._aCalculator._signalCalculator._dimensionStack = dimensionStack;

            multiplyCalculator._aCalculator._signalCalculator._frequencyCalculator._value = frequency;

            return multiplyCalculator;
        }
    }
}