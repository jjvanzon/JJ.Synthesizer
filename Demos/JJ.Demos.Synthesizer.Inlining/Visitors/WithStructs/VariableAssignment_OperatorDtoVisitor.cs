using System;
using System.Collections.Generic;
using System.Reflection;
using JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Visitors.WithStructs
{
    internal class VariableAssignment_OperatorDtoVisitor : OperatorDtoVisitorBase_AfterSimplification
    {
        private readonly Stack<IOperatorCalculator> _calculatorStack;

        public void Execute(OperatorDto sourceOperatorDto, IOperatorCalculator destOperatorCalculator)
        {
            if (sourceOperatorDto == null) throw new NullException(() => sourceOperatorDto);
            if (destOperatorCalculator == null) throw new NullException(() => destOperatorCalculator);

            Visit_OperatorDto_Polymorphic(sourceOperatorDto);
        }

        protected override OperatorDto Visit_Multiply_OperatorDto_VarA_ConstB(Multiply_OperatorDto_VarA_ConstB dto)
        {
            base.Visit_Multiply_OperatorDto_VarA_ConstB(dto);

            //multiplyCalculator_Accessor._b = dto.B;

            // You would like to follow a path, but you can't, because you cannot use intermediate variables.
            // You cannot do GetValue on a property and still expect to be working with the same struct instance.
            // I cannot maintain anything in a variable, so I would have to assign
            // somehow following the whole path in one blow.

            // Unsafe code working with pointers?
            // Something that creates a lot of temporary copies, but in the end, gives you the same result?
            // But then depth-first: gather up a value deepest, and then one layer above it.

            //var multiplyAccessor_Accessor = new MultiplyAccessor_Accessor();

            FieldInfo fieldInfo = null;
            //fieldInfo

            return dto;
        }
    }
}