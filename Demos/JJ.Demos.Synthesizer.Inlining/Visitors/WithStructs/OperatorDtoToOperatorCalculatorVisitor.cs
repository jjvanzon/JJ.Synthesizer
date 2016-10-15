using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Calculation;
using JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Demos.Synthesizer.Inlining.Helpers.WithStucts;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Visitors.WithStructs
{
    internal class OperatorDtoToOperatorCalculatorVisitor
    {
        private readonly DimensionStack _dimensionStack;

        public OperatorDtoToOperatorCalculatorVisitor(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            _dimensionStack = dimensionStack;
        }

        public object Execute(OperatorDto sourceOperatorDto)
        {
            var preProcessingVisitor = new PreProcessing_OperatorDtoVisitor();
            sourceOperatorDto = preProcessingVisitor.Execute(sourceOperatorDto);

            Type destOperatorCalculatorType_ClosedGeneric = OperatorDtoToOperatorCalculatorTypeConverter.ConvertToClosedGenericType(sourceOperatorDto);
            IOperatorCalculator destOperatorCalculator = (IOperatorCalculator)Activator.CreateInstance(destOperatorCalculatorType_ClosedGeneric);

            var variableAssignmentVisitor = new VariableAssignment_OperatorDtoVisitor(_dimensionStack);
            variableAssignmentVisitor.Execute(sourceOperatorDto, destOperatorCalculator);


            throw new NotImplementedException();
        }
    }
}
