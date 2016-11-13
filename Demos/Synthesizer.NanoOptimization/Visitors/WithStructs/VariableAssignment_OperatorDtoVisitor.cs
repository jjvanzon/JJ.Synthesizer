using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithStructs;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers.WithStucts;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors.WithStructs
{
    internal class VariableAssignment_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterMathSimplification
    {
        private readonly DimensionStack _dimensionStack;
        private readonly Stack<IOperatorCalculator> _stack = new Stack<IOperatorCalculator>();

        public VariableAssignment_OperatorDtoVisitor(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            _dimensionStack = dimensionStack;
        }

        // Execute

        public IOperatorCalculator Execute(OperatorDtoBase sourceDto, IOperatorCalculator destCalculator)
        {
            if (sourceDto == null) throw new NullException(() => sourceDto);
            if (destCalculator == null) throw new NullException(() => destCalculator);

            Visit_OperatorDto_Polymorphic(sourceDto);

            if (_stack.Count != 1)
            {
                throw new NotEqualException(() => _stack.Count, 1);
            }

            return _stack.Pop();
        }

        [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override IList<OperatorDtoBase> VisitInputOperatorDtos(IList<OperatorDtoBase> operatorDtos)
        {
            // Reverse the order, so calculators pop off the stack in the right order.
            for (int i = operatorDtos.Count - 1; i >= 0; i--)
            {
                OperatorDtoBase operatorDto = operatorDtos[i];
                VisitOperatorDto(operatorDto);
            }

            return operatorDtos;
        }

        // Visit

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars(Add_OperatorDto_Vars dto)
        {
            base.Visit_Add_OperatorDto_Vars(dto);

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

        protected override OperatorDtoBase Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
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

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            Process_OperatorDto_VarA_ConstB(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_VarB(dto);

            Process_OperatorDto_VarA_VarB(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_Concrete(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto_Concrete(dto);

            var calculator = (Number_OperatorCalculator)CreateCalculator(dto);
            calculator.Number = dto.Number;

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            base.Visit_Number_OperatorDto_NaN(dto);

            Process_OperatorDto_WithoutVariables(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            base.Visit_Number_OperatorDto_One(dto);

            Process_OperatorDto_WithoutVariables(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            base.Visit_Number_OperatorDto_Zero(dto);

            Process_OperatorDto_WithoutVariables(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
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

        protected override OperatorDtoBase Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
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

        protected override OperatorDtoBase Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            base.Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(dto);

            var calculator = (Sine_OperatorCalculator_ConstFrequency_NoOriginShifting)CreateCalculator(dto);
            calculator.DimensionStack = _dimensionStack;
            calculator.Frequency = dto.Frequency;

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(dto);

            Process_OperatorDto_VarFrequency(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(dto);

            Process_OperatorDto_VarFrequency(dto);

            return dto;
        }

        protected override OperatorDtoBase Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            base.Visit_VariableInput_OperatorDto(dto);

            var calculator = (VariableInput_OperatorCalculator)CreateCalculator(dto);
            calculator._value = dto.DefaultValue;

            _stack.Push(calculator);

            return dto;
        }

        // Reused Methods

        private void Process_OperatorDto_VarA_VarB(OperatorDtoBase dto)
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

        private void Process_OperatorDto_WithoutVariables(OperatorDtoBase dto)
        {
            var calculator = (IOperatorCalculator)CreateCalculator(dto);

            _stack.Push(calculator);
        }

        private object CreateCalculator(OperatorDtoBase dto)
        {
            Type calculatorType = OperatorDtoToCalculatorTypeConverter.ConvertToClosedGenericType(dto);
            var calculator = Activator.CreateInstance(calculatorType);
            return calculator;
        }
    }
}