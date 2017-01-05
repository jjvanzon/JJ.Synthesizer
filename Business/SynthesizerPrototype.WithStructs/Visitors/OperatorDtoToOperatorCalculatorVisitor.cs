using System;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Visitors;
using JJ.Business.SynthesizerPrototype.WithStructs.Calculation;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithStructs.Helpers;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.WithStructs.Visitors
{
    public class OperatorDtoToOperatorCalculatorVisitor
    {
        private readonly DimensionStack _dimensionStack;

        public OperatorDtoToOperatorCalculatorVisitor(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            _dimensionStack = dimensionStack;
        }

        public IOperatorCalculator Execute(OperatorDtoBase sourceOperatorDto)
        {
            var preProcessingVisitor = new OperatorDtoPreProcessingExecutor();
            sourceOperatorDto = preProcessingVisitor.Execute(sourceOperatorDto);

            Type destOperatorCalculatorType_ClosedGeneric = OperatorDtoToCalculatorTypeConverter.ConvertToClosedGenericType(sourceOperatorDto);
            IOperatorCalculator destOperatorCalculator = (IOperatorCalculator)Activator.CreateInstance(destOperatorCalculatorType_ClosedGeneric);

            var variableAssignmentVisitor = new VariableAssignment_OperatorDtoVisitor(_dimensionStack);
            destOperatorCalculator = variableAssignmentVisitor.Execute(sourceOperatorDto, destOperatorCalculator);

            return destOperatorCalculator;
        }
    }
}
