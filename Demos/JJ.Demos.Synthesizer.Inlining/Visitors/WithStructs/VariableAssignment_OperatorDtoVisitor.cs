using System;
using System.Collections.Generic;
using System.Reflection;
using JJ.Demos.Synthesizer.Inlining.Calculation;
using JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Demos.Synthesizer.Inlining.Helpers.WithStucts;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Visitors.WithStructs
{
    internal class VariableAssignment_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterSimplification
    {
        private readonly DimensionStack _dimensionStack;
        private readonly Stack<IOperatorCalculator> _stack = new Stack<IOperatorCalculator>();

        public VariableAssignment_OperatorDtoVisitor(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            _dimensionStack = dimensionStack;
        }

        public void Execute(OperatorDto sourceDto, IOperatorCalculator destCalculator)
        {
            if (sourceDto == null) throw new NullException(() => sourceDto);
            if (destCalculator == null) throw new NullException(() => destCalculator);

            _stack.Push(destCalculator);

            Visit_OperatorDto_Polymorphic(sourceDto);

            if (_stack.Count != 1)
            {
                throw new NotEqualException(() => _stack.Count, 1);
            }

            _stack.Pop();
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            IOperatorCalculator aCalculator = _stack.Pop();

            var calculator = (IMultiply_OperatorDto_VarA_ConstB)CreateCalculator(dto);
            calculator.ACalculator = aCalculator;
            calculator.B = dto.B;

            _stack.Push(calculator);
            return dto;
        }

        protected override OperatorDto Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            base.Visit_Shift_OperatorDto_VarSignal_ConstDistance(dto);

            IOperatorCalculator signalCalculator = _stack.Pop();

            var calculator = (IShift_OperatorCalculator_VarSignal_ConstDifference)CreateCalculator(dto);
            calculator.DimensionStack = _dimensionStack;
            calculator.Distance = dto.Distance;
            calculator.SignalCalculator = signalCalculator;
            
            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            base.Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(dto);

            var calculator = (Sine_OperatorCalculator_ConstFrequency_NoOriginShifting)CreateCalculator(dto);

            calculator._dimensionStack = _dimensionStack;

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(dto);

            ISine_OperatorCalculator_VarFrequency calculator = CreateCalculator_Sine_VarFrequency(dto);

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(dto);

            ISine_OperatorCalculator_VarFrequency calculator = CreateCalculator_Sine_VarFrequency(dto);

            _stack.Push(calculator);

            return dto;
        }

        private ISine_OperatorCalculator_VarFrequency CreateCalculator_Sine_VarFrequency(Sine_OperatorDto dto)
        {
            IOperatorCalculator frequencyCalculator = _stack.Pop();

            var calculator = (ISine_OperatorCalculator_VarFrequency)CreateCalculator(dto);

            calculator.DimensionStack = _dimensionStack;
            calculator.FrequencyCalculator = frequencyCalculator;

            return calculator;
        }

        protected override OperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            base.Visit_VariableInput_OperatorDto(dto);

            var calculator = (IOperatorCalculator)CreateCalculator(dto);

            _stack.Push(calculator);

            return dto;
        }

        private static object CreateCalculator(OperatorDto dto)
        {
            Type calculatorType = OperatorDtoToOperatorCalculatorTypeConverter.ConvertToClosedGenericType(dto);
            var calculator = Activator.CreateInstance(calculatorType);
            return calculator;
        }
    }
}