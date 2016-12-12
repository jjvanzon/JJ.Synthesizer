using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithStructs;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors.WithStructs;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers.WithStructs
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

            int count = 8;
            for (int i = 0; i < count; i++)
            {
                addCalculator.SetVarCalculator(i, CreateOperatorCalculatorStructure_SinglePartial(dimensionStack));
            }

            return addCalculator;
        }

        public static IOperatorCalculator CreateOperatorCalculatorFromDto(OperatorDtoBase dto, DimensionStack dimensionStack)
        {
            var visitor = new OperatorDtoToOperatorCalculatorVisitor(dimensionStack);
            IOperatorCalculator calculator = visitor.Execute(dto);

            return calculator;
        }
    }
}