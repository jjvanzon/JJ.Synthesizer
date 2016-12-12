using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Calculation.WithStructs;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Helpers.WithStucts;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors.WithStructs
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
