using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.SynthesizerPrototype.Tests.Calculation;
using JJ.Business.SynthesizerPrototype.Tests.Calculation.WithStructs;
using JJ.Business.SynthesizerPrototype.Tests.Dto;
using JJ.Business.SynthesizerPrototype.Tests.Helpers.WithStucts;
using JJ.Framework.Exceptions;

namespace JJ.Business.SynthesizerPrototype.Tests.Visitors.WithStructs
{
    internal class OperatorDtoToOperatorCalculatorVisitor
    {
        private readonly DimensionStack _dimensionStack;

        public OperatorDtoToOperatorCalculatorVisitor(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            _dimensionStack = dimensionStack;
        }

        public IOperatorCalculator Execute(OperatorDtoBase sourceOperatorDto)
        {
            var preProcessingVisitor = new OperatorDtoVisitor_PreProcessing();
            sourceOperatorDto = preProcessingVisitor.Execute(sourceOperatorDto);

            Type destOperatorCalculatorType_ClosedGeneric = OperatorDtoToCalculatorTypeConverter.ConvertToClosedGenericType(sourceOperatorDto);
            IOperatorCalculator destOperatorCalculator = (IOperatorCalculator)Activator.CreateInstance(destOperatorCalculatorType_ClosedGeneric);

            var variableAssignmentVisitor = new VariableAssignment_OperatorDtoVisitor(_dimensionStack);
            destOperatorCalculator = variableAssignmentVisitor.Execute(sourceOperatorDto, destOperatorCalculator);

            return destOperatorCalculator;
        }
    }
}
