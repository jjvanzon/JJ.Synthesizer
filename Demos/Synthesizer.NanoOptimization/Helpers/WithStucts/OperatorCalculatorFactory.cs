using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Visitors.WithStructs;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Helpers.WithStructs
{
    internal static class OperatorCalculatorFactory
    {
        public static
            Multiply_OperatorCalculator_VarA_ConstB
            <
                Shift_OperatorCalculator_VarSignal_ConstDistance
                <
                    Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                    <
                        VariableInput_OperatorCalculator
                    >
                >
            > CreateOperatorCalculatorStructure_SinglePartial(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            double frequency = 440.0;
            double volume = 10.0;
            double phaseShift = 0.25;

            var variableInputCalculator = new VariableInput_OperatorCalculator();
            variableInputCalculator._value = frequency;

            var sineCalculator =
                new Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                <
                    VariableInput_OperatorCalculator
                >();
            sineCalculator.FrequencyCalculator = variableInputCalculator;
            sineCalculator.DimensionStack = dimensionStack;

            var shiftCalculator =
                new Shift_OperatorCalculator_VarSignal_ConstDistance
                <
                    Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                    <
                        VariableInput_OperatorCalculator
                    >
                >();
            shiftCalculator.SignalCalculator = sineCalculator;
            shiftCalculator.Distance = phaseShift;
            shiftCalculator.DimensionStack = dimensionStack;

            var multiplyCalculator =
                new Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDistance
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >();
            multiplyCalculator.ACalculator = shiftCalculator;
            multiplyCalculator.B = volume;

            return multiplyCalculator;
        }

        public static
            Add_OperatorCalculator_8Vars
            <
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDistance
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDistance
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDistance
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDistance
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDistance
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDistance
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDistance
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDistance
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >
            >
            CreateOperatorCalculatorStructure_8Partials(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);

            double frequency = 440.0;
            double volume = 10.0;
            double phaseShift = 0.25;

            var addCalculator =
                new Add_OperatorCalculator_8Vars
                <
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDistance
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDistance
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDistance
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDistance
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDistance
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDistance
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDistance
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDistance
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >
                >();

            addCalculator.Calculator1 = CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);
            addCalculator.Calculator2 = CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);
            addCalculator.Calculator3 = CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);
            addCalculator.Calculator4 = CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);
            addCalculator.Calculator5 = CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);
            addCalculator.Calculator6 = CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);
            addCalculator.Calculator7 = CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);
            addCalculator.Calculator8 = CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);

            return addCalculator;
        }

        public static IOperatorCalculator CreateOperatorCalculatorStructure_SinglePartial_FromDto(DimensionStack dimensionStack)
        {
            OperatorDto dto = OperatorDtoFactory.CreateOperatorDto_SinglePartial();

            var visitor = new OperatorDtoToOperatorCalculatorVisitor(dimensionStack);
            IOperatorCalculator calculator = visitor.Execute(dto);

            return calculator;
        }
    }
}