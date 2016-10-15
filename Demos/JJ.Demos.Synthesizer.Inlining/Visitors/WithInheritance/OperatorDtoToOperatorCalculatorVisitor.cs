using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Calculation;
using JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithInheritance;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Visitors.WithInheritance
{
    internal class OperatorDtoToOperatorCalculatorVisitor : OperatorDtoVisitorBase_AfterSimplification
    {
        private readonly DimensionStack _dimensionStack;
        private readonly Stack<OperatorCalculatorBase> _stack = new Stack<OperatorCalculatorBase>();

        public OperatorDtoToOperatorCalculatorVisitor(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            _dimensionStack = dimensionStack;
        }

        public OperatorCalculatorBase Execute(OperatorDto dto)
        {
            Visit_OperatorDto_Polymorphic(dto);

            if (_stack.Count != 1)
            {
                throw new NotEqualException(() => _stack.Count, 1);
            }

            return _stack.Pop();
        }

        protected override OperatorDto Visit_Add_OperatorDto_VarA_ConstB(Add_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Add_OperatorDto_VarA_ConstB(dto);

            OperatorCalculatorBase aCalculator = _stack.Pop();
            OperatorCalculatorBase bCalculator = _stack.Pop();

            var calculator = new Add_OperatorCalculator_VarA_ConstB(aCalculator, dto.B);

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_Add_OperatorDto_VarA_VarB(Add_OperatorDto_VarA_VarB dto)
        {
            base.Visit_Add_OperatorDto_VarA_VarB(dto);

            OperatorCalculatorBase aCalculator = _stack.Pop();
            OperatorCalculatorBase bCalculator = _stack.Pop();

            var calculator = new Add_OperatorCalculator_VarA_VarB(aCalculator, bCalculator);

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            OperatorCalculatorBase aCalculator = _stack.Pop();
            OperatorCalculatorBase bCalculator = _stack.Pop();

            var calculator = new Multiply_OperatorCalculator_VarA_ConstB(aCalculator, dto.B);

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_VarB(dto);

            OperatorCalculatorBase aCalculator = _stack.Pop();
            OperatorCalculatorBase bCalculator = _stack.Pop();

            var calculator = new Multiply_OperatorCalculator_VarA_VarB(aCalculator, bCalculator);

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_Number_OperatorDto_Base(Number_OperatorDto dto)
        {
            // This shouldn't happen. Everything with constants as input should have gotten a specialized Dto.
            throw new NotSupportedException();
        }

        protected override OperatorDto Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            base.Visit_Shift_OperatorDto_VarSignal_ConstDistance(dto);

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase distanceCalculator = _stack.Pop();

            var calculator = new Shift_OperatorCalculator_VarSignal_ConstDifference(signalCalculator, dto.Distance, _dimensionStack);

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            base.Visit_Shift_OperatorDto_VarSignal_VarDistance(dto);

            OperatorCalculatorBase signalCalculator = _stack.Pop();
            OperatorCalculatorBase distanceCalculator = _stack.Pop();

            var calculator = new Shift_OperatorCalculator_VarSignal_VarDifference(signalCalculator, distanceCalculator, _dimensionStack);

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            base.Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(dto);

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();

            var calculator = new Sine_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, _dimensionStack);

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(dto);

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();

            var calculator = new Sine_OperatorCalculator_VarFrequency_NoPhaseTracking(frequencyCalculator, _dimensionStack);

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(dto);

            OperatorCalculatorBase frequencyCalculator = _stack.Pop();

            var calculator = new Sine_OperatorCalculator_VarFrequency_WithPhaseTracking(frequencyCalculator, _dimensionStack);

            _stack.Push(calculator);

            return dto;
        }

        protected override OperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            base.Visit_VariableInput_OperatorDto(dto);

            var calculator = new VariableInput_OperatorCalculator(dto.DefaultValue);

            _stack.Push(calculator);

            return dto;
        }
    }
}
