using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.WithStructs.Calculation;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithStructs.Visitors;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.SynthesizerPrototype.Tests.Helpers.WithStructs
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
            const double frequency = 440.0;
            const double volume = 10.0;
            const double phaseShift = 0.25;

            var variableInputCalculator = new VariableInput_OperatorCalculator { _value = frequency };

            var sineCalculator =
                new Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                <
                    VariableInput_OperatorCalculator
                >
                {
                    FrequencyCalculator = variableInputCalculator,
                    DimensionStack = dimensionStack ?? throw new NullException(() => dimensionStack)
                };

            var shiftCalculator =
                new Shift_OperatorCalculator_VarSignal_ConstDistance
                <
                    Sine_OperatorCalculator_VarFrequency_WithPhaseTracking
                    <
                        VariableInput_OperatorCalculator
                    >
                >
                {
                    SignalCalculator = sineCalculator,
                    Distance = phaseShift,
                    DimensionStack = dimensionStack
                };

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
                >
                {
                    ACalculator = shiftCalculator,
                    B = volume
                };

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

            const int count = 8;
            for (int i = 0; i < count; i++)
            {
                addCalculator.SetVarCalculator(i, CreateOperatorCalculatorStructure_SinglePartial(dimensionStack));
            }

            return addCalculator;
        }

        public static IOperatorCalculator CreateOperatorCalculatorFromDto(IOperatorDto dto, DimensionStack dimensionStack)
        {
            var visitor = new OperatorDtoToOperatorCalculatorVisitor(dimensionStack);
            IOperatorCalculator calculator = visitor.Execute(dto);

            return calculator;
        }
    }
}