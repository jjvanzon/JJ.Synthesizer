﻿using System.Collections.Generic;
using System.Linq;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Visitors;
using JJ.Business.SynthesizerPrototype.WithInheritance.Calculation;
using JJ.Business.SynthesizerPrototype.WithInheritance.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithInheritance.Helpers;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.SynthesizerPrototype.WithInheritance.Visitors
{
    public class OperatorDtoToOperatorCalculatorVisitor : OperatorDtoVisitorBase_AfterMathSimplification
    {
        private readonly DimensionStack _dimensionStack;
        private readonly Stack<OperatorCalculatorBase> _stack = new Stack<OperatorCalculatorBase>();

        public OperatorDtoToOperatorCalculatorVisitor(DimensionStack dimensionStack)
            => _dimensionStack = dimensionStack ?? throw new NullException(() => dimensionStack);

        public OperatorCalculatorBase Execute(IOperatorDto dto)
        {
            var preProcessingVisitor = new OperatorDtoPreProcessingExecutor();
            dto = preProcessingVisitor.Execute(dto);

            Visit_OperatorDto_Polymorphic(dto);

            return _stack.Pop();
        }

        // Add

        protected override IOperatorDto Visit_Add_OperatorDto_Vars_NoConsts(Add_OperatorDto_Vars_NoConsts dto)
        {
            base.Visit_Add_OperatorDto_Vars_NoConsts(dto);

            IList<OperatorCalculatorBase> operandCalculators = dto.InputOperatorDtos.Select(x => _stack.Pop()).ToArray();
            OperatorCalculatorBase calculator = OperatorCalculatorFactory.CreateAddCalculator_Vars(operandCalculators);

            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Add_OperatorDto_Vars_1Const(Add_OperatorDto_Vars_1Const dto)
        {
            base.Visit_Add_OperatorDto_Vars_1Const(dto);

            IList<OperatorCalculatorBase> varCalculators = dto.Vars.Select(x => _stack.Pop()).ToArray();
            OperatorCalculatorBase calculator = OperatorCalculatorFactory.CreateAddCalculator_Vars_1Const(varCalculators, dto.ConstValue);

            _stack.Push(calculator);

            return dto;
        }

        // Multiply

        protected override IOperatorDto Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            OperatorCalculatorBase aCalculator = _stack.Pop();
            var calculator = new Multiply_OperatorCalculator_VarA_ConstB(aCalculator, dto.B);
            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Multiply_OperatorDto_VarA_VarB(Multiply_OperatorDto_VarA_VarB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_VarB(dto);

            var calculator = new Multiply_OperatorCalculator_VarA_VarB(_stack.Pop(), _stack.Pop());
            _stack.Push(calculator);

            return dto;
        }

        // Number

        protected override IOperatorDto Visit_Number_OperatorDto(Number_OperatorDto dto)
        {
            base.Visit_Number_OperatorDto(dto);

            var calculator = new Number_OperatorCalculator(dto.Number);
            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Number_OperatorDto_NaN(Number_OperatorDto_NaN dto)
        {
            base.Visit_Number_OperatorDto_NaN(dto);

            var calculator = new Number_OperatorCalculator_NaN();
            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Number_OperatorDto_One(Number_OperatorDto_One dto)
        {
            base.Visit_Number_OperatorDto_One(dto);

            var calculator = new Number_OperatorCalculator_One();
            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Number_OperatorDto_Zero(Number_OperatorDto_Zero dto)
        {
            base.Visit_Number_OperatorDto_Zero(dto);

            var calculator = new Number_OperatorCalculator_Zero();
            _stack.Push(calculator);

            return dto;
        }

        // Shift

        protected override IOperatorDto Visit_Shift_OperatorDto_VarSignal_ConstDistance(Shift_OperatorDto_VarSignal_ConstDistance dto)
        {
            base.Visit_Shift_OperatorDto_VarSignal_ConstDistance(dto);

            var calculator = new Shift_OperatorCalculator_VarSignal_ConstDifference(_stack.Pop(), dto.Distance, _dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Shift_OperatorDto_VarSignal_VarDistance(Shift_OperatorDto_VarSignal_VarDistance dto)
        {
            base.Visit_Shift_OperatorDto_VarSignal_VarDistance(dto);

            var calculator = new Shift_OperatorCalculator_VarSignal_VarDifference(_stack.Pop(), _stack.Pop(), _dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        // Sine

        protected override IOperatorDto Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(
            Sine_OperatorDto_ConstFrequency_NoOriginShifting dto)
        {
            base.Visit_Sine_OperatorDto_ConstFrequency_NoOriginShifting(dto);

            var calculator = new Sine_OperatorCalculator_ConstFrequency_NoOriginShifting(dto.Frequency, _dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(Sine_OperatorDto_VarFrequency_NoPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_NoPhaseTracking(dto);

            var calculator = new Sine_OperatorCalculator_VarFrequency_NoPhaseTracking(_stack.Pop(), _dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(
            Sine_OperatorDto_VarFrequency_WithPhaseTracking dto)
        {
            base.Visit_Sine_OperatorDto_VarFrequency_WithPhaseTracking(dto);

            var calculator = new Sine_OperatorCalculator_VarFrequency_WithPhaseTracking(_stack.Pop(), _dimensionStack);
            _stack.Push(calculator);

            return dto;
        }

        // VariableInput

        protected override IOperatorDto Visit_VariableInput_OperatorDto(VariableInput_OperatorDto dto)
        {
            base.Visit_VariableInput_OperatorDto(dto);

            var calculator = new VariableInput_OperatorCalculator(dto.DefaultValue);
            _stack.Push(calculator);

            return dto;
        }

        protected override IOperatorDto Visit_OperatorDto_Polymorphic(IOperatorDto dto)
        {
            VisitorHelper.WithStackCheck(_stack, () => base.Visit_OperatorDto_Polymorphic(dto));

            return dto;
        }
    }
}