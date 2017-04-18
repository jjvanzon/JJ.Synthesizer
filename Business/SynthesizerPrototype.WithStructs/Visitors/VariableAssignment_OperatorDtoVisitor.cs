using System;
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Visitors;
using JJ.Business.SynthesizerPrototype.WithStructs.Calculation;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithStructs.Helpers;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Visitors
{
    internal class VariableAssignment_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterMathSimplification
    {
        private readonly DimensionStack _dimensionStack;
        private readonly Stack<IOperatorCalculator> _stack = new Stack<IOperatorCalculator>();

        public VariableAssignment_OperatorDtoVisitor(DimensionStack dimensionStack)
        {
            _dimensionStack = dimensionStack ?? throw new NullException(() => dimensionStack);
        }

        // Execute

        public IOperatorCalculator Execute(IOperatorDto sourceDto, IOperatorCalculator destCalculator)
        {
            if (sourceDto == null) throw new NullException(() => sourceDto);
            if (destCalculator == null) throw new NullException(() => destCalculator);

            Visit_OperatorDto_Polymorphic(sourceDto);

            return _stack.Pop();
        }

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

            return dto;
        }

        // Visit

        protected override IOperatorDto Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            base.Visit_Add_OperatorDto_Vars_NoConsts(dto);

            IOperatorCalculator_Vars calculator = (IOperatorCalculator_Vars)CreateCalculator(dto);

            int varCount = dto.Vars.Count;
            for (int i = 0; i < varCount; i++)
            {
                IOperatorCalculator varOperatorCalculator = _stack.Pop();
                calculator.SetVarCalculator(i, varOperatorCalculator);
            }

            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            base.Visit_Add_OperatorDto_Vars_1Const(dto);

            IOperatorCalculator_Vars_1Const calculator = (IOperatorCalculator_Vars_1Const)CreateCalculator(dto);
            calculator.ConstValue = dto.ConstValue;

            int varCount = dto.Vars.Count;
            for (int i = 0; i < varCount; i++)
            {
                IOperatorCalculator varOperatorCalculator = _stack.Pop();
                calculator.SetVarCalculator(i, varOperatorCalculator);
            }

            _stack.Push(calculator);

            return dto;

        }

        protected override IOperatorDto Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            Process_OperatorDto_VarA_ConstB(dto);

            return dto;
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_VarB(dto);

            Process_OperatorDto_VarA_VarB(dto);

            return dto;
        }

        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto(dto);

            var calculator = (Number_OperatorCalculator)CreateCalculator(dto);
            calculator.Number = dto.Number;

            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            base.Visit_Number_OperatorDto_NaN(dto);

            Process_OperatorDto_WithoutVariables(dto);

            return dto;
        }

        protected override IOperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            base.Visit_Number_OperatorDto_One(dto);

            Process_OperatorDto_WithoutVariables(dto);

            return dto;
        }

        protected override IOperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            base.Visit_Number_OperatorDto_Zero(dto);

            Process_OperatorDto_WithoutVariables(dto);

            return dto;
        }

        protected override IOperatorDto Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            base.Visit_Shift_OperatorDto_VarSignal_ConstDistance(dto);

            IOperatorCalculator signalCalculator = _stack.Pop();

            var calculator = (IShift_OperatorCalculator_VarSignal_ConstDistance)CreateCalculator(dto);
            calculator.Distance = dto.Distance;
            calculator.SignalCalculator = signalCalculator;
            calculator.DimensionStack = _dimensionStack;

            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            base.Visit_Shift_OperatorDto_VarSignal_VarDistance(dto);

            IOperatorCalculator signalCalculator = _stack.Pop();
            IOperatorCalculator distanceCalculator = _stack.Pop();

            var calculator = (IShift_OperatorCalculator_VarSignal_VarDistance)CreateCalculator(dto);
            calculator.SignalCalculator = signalCalculator;
            calculator.DistanceCalculator = distanceCalculator;
            calculator.DimensionStack = _dimensionStack;

            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            base.Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(dto);

            var calculator = (Sine_OperatorCalculator_ConstFrequency_NoOriginShifting)CreateCalculator(dto);
            calculator.DimensionStack = _dimensionStack;
            calculator.Frequency = dto.Frequency;

            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(dto);

            Process_OperatorDto_VarFrequency(dto);

            return dto;
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(dto);

            Process_OperatorDto_VarFrequency(dto);

            return dto;
        }

        protected override IOperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            base.Visit_VariableInput_OperatorDto(dto);

            var calculator = (VariableInput_OperatorCalculator)CreateCalculator(dto);
            calculator._value = dto.DefaultValue;

            _stack.Push(calculator);

            return dto;
        }

        // Reused Methods

        private void Process_OperatorDto_VarA_VarB(IOperatorDto dto)
        {
            IOperatorCalculator aCalculator = _stack.Pop();
            IOperatorCalculator bCalculator = _stack.Pop();

            var calculator = (IOperatorCalculator_VarA_VarB)CreateCalculator(dto);
            calculator.ACalculator = aCalculator;
            calculator.BCalculator = bCalculator;

            _stack.Push(calculator);
        }

        private void Process_OperatorDto_VarA_ConstB(OperatorDtoBase_VarA_ConstB dto)
        {
            IOperatorCalculator aCalculator = _stack.Pop();

            var calculator = (IOperatorCalculator_VarA_ConstB)CreateCalculator(dto);
            calculator.ACalculator = aCalculator;
            calculator.B = dto.B;

            _stack.Push(calculator);
        }

        private void Process_OperatorDto_VarFrequency(OperatorDtoBase_VarFrequency dto)
        {
            IOperatorCalculator frequencyCalculator = _stack.Pop();

            var calculator = (ISine_OperatorCalculator_VarFrequency)CreateCalculator(dto);
            calculator.DimensionStack = _dimensionStack;
            calculator.FrequencyCalculator = frequencyCalculator;

            _stack.Push(calculator);
        }

        private void Process_OperatorDto_WithoutVariables(IOperatorDto dto)
        {
            var calculator = (IOperatorCalculator)CreateCalculator(dto);

            _stack.Push(calculator);
        }

        private object CreateCalculator(IOperatorDto dto)
        {
            Type calculatorType = OperatorDtoToCalculatorTypeConverter.ConvertToClosedGenericType(dto);
            var calculator = Activator.CreateInstance(calculatorType);
            return calculator;
        }
    }
}