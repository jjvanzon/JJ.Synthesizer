using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithStructs;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers.WithStucts;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Visitors.WithStructs
{
    internal class OperatorDtoToOperatorCalculatorVisitor
    {
        private readonly DimensionStack _dimensionStack;

        public OperatorDtoToOperatorCalculatorVisitor(DimensionStack dimensionStack)
        {
            if (dimensionStack == null) throw new NullException(() => dimensionStack);
            _dimensionStack = dimensionStack;
        }

        public IOperatorCalculator Execute(OperatorDto sourceOperatorDto)
        {
            var preProcessingVisitor = new PreProcessing_OperatorDtoVisitor();
            sourceOperatorDto = preProcessingVisitor.Execute(sourceOperatorDto);

            Type destOperatorCalculatorType_ClosedGeneric = OperatorDtoToCalculatorTypeConverter.ConvertToClosedGenericType(sourceOperatorDto);
            IOperatorCalculator destOperatorCalculator = (IOperatorCalculator)Activator.CreateInstance(destOperatorCalculatorType_ClosedGeneric);

            var variableAssignmentVisitor = new VariableAssignment_OperatorDtoVisitor(_dimensionStack);
            destOperatorCalculator = variableAssignmentVisitor.Execute(sourceOperatorDto, destOperatorCalculator);

            return destOperatorCalculator;
        }
    }
}
