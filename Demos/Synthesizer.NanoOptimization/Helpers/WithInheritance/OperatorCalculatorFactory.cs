using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithInheritance;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Visitors.WithInheritance;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Helpers.WithInheritance
{
    internal static partial class OperatorCalculatorFactory
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

            var calculator =
                new Multiply_OperatorCalculator_VarA_ConstB
                (
                    new Shift_OperatorCalculator_VarSignal_ConstDifference
                    (
                        new Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        (
                            new VariableInput_OperatorCalculator(frequency),
                            dimensionStack
                        ),
                        phaseShift,
                        dimensionStack
                    ),
                    volume
                );

            return calculator;
        }

        public static OperatorCalculatorBase CreateOperatorCalculatorStructure_SinglePartial_FromDto(DimensionStack dimensionStack)
        {
            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_SinglePartial();

            return CreateOperatorCalculatorFromDto(dto, dimensionStack);
        }

        public static OperatorCalculatorBase CreateOperatorCalculatorFromDto(OperatorDtoBase dto, DimensionStack dimensionStack)
        {
            var visitor = new OperatorDtoToOperatorCalculatorVisitor(dimensionStack);
            OperatorCalculatorBase calculator = visitor.Execute(dto);

            return calculator;
        }
    }
}
