using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Calculation;
using JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Demos.Synthesizer.Inlining.Visitors.WithStructs;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Helpers.WithStructs
{
    internal static class OperatorCalculatorFactory
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
            > CreateOperatorCalculatorStructure_SinglePartial(DimensionStack dimensionStack)
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

        public static
            Add_OperatorCalculator_8Vars
            <
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDifference
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDifference
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDifference
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDifference
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDifference
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDifference
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDifference
                    <
                        Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                        <
                            VariableInput_OperatorCalculator
                        >
                    >
                >,
                Multiply_OperatorCalculator_VarA_ConstB
                <
                    Shift_OperatorCalculator_VarSignal_ConstDifference
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
                        Shift_OperatorCalculator_VarSignal_ConstDifference
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDifference
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDifference
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDifference
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDifference
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDifference
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDifference
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >,
                    Multiply_OperatorCalculator_VarA_ConstB
                    <
                        Shift_OperatorCalculator_VarSignal_ConstDifference
                        <
                            Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                            <
                                VariableInput_OperatorCalculator
                            >
                        >
                    >
                >();

            addCalculator._calculator1._b = volume;
            addCalculator._calculator1._aCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator1._aCalculator._distance = phaseShift;
            addCalculator._calculator1._aCalculator._signalCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator1._aCalculator._signalCalculator._frequencyCalculator._value = frequency;

            addCalculator._calculator2._b = volume;
            addCalculator._calculator2._aCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator2._aCalculator._distance = phaseShift;
            addCalculator._calculator2._aCalculator._signalCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator2._aCalculator._signalCalculator._frequencyCalculator._value = frequency;

            addCalculator._calculator3._b = volume;
            addCalculator._calculator3._aCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator3._aCalculator._distance = phaseShift;
            addCalculator._calculator3._aCalculator._signalCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator3._aCalculator._signalCalculator._frequencyCalculator._value = frequency;

            addCalculator._calculator4._b = volume;
            addCalculator._calculator4._aCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator4._aCalculator._distance = phaseShift;
            addCalculator._calculator4._aCalculator._signalCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator4._aCalculator._signalCalculator._frequencyCalculator._value = frequency;

            addCalculator._calculator5._b = volume;
            addCalculator._calculator5._aCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator5._aCalculator._distance = phaseShift;
            addCalculator._calculator5._aCalculator._signalCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator5._aCalculator._signalCalculator._frequencyCalculator._value = frequency;

            addCalculator._calculator6._b = volume;
            addCalculator._calculator6._aCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator6._aCalculator._distance = phaseShift;
            addCalculator._calculator6._aCalculator._signalCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator6._aCalculator._signalCalculator._frequencyCalculator._value = frequency;

            addCalculator._calculator7._b = volume;
            addCalculator._calculator7._aCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator7._aCalculator._distance = phaseShift;
            addCalculator._calculator7._aCalculator._signalCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator7._aCalculator._signalCalculator._frequencyCalculator._value = frequency;

            addCalculator._calculator8._b = volume;
            addCalculator._calculator8._aCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator8._aCalculator._distance = phaseShift;
            addCalculator._calculator8._aCalculator._signalCalculator._dimensionStack = dimensionStack;
            addCalculator._calculator8._aCalculator._signalCalculator._frequencyCalculator._value = frequency;

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