using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Helpers.WithStucts
{
    internal static class OperatorDtoToOperatorCalculatorTypeConverter
    {
        private static readonly Dictionary<Type, Type> _dtoType_Concrete_To_CalculatorType_OpenGeneric_Dictionary =
                            new Dictionary<Type, Type>
        {
            { typeof(Add_OperatorDto_VarA_VarB), typeof(Add_OperatorCalculator_VarA_VarB<,>) },
            { typeof(Add_OperatorDto_VarA_ConstB), typeof(Add_OperatorCalculator_VarA_ConstB<>) },
            { typeof(Multiply_OperatorDto_VarA_VarB), typeof(Multiply_OperatorCalculator_VarA_VarB<,>) },
            { typeof(Multiply_OperatorDto_VarA_ConstB), typeof(Multiply_OperatorCalculator_VarA_ConstB<>) }
            // TODO: Finish up the rest of the dictionary entries.
        };

        public static Type ConvertToClosedGenericType(OperatorDto operatorDto)
        {
            Type dtoType_Concrete = operatorDto.GetType();
            Type calculatorType_OpenGeneric = _dtoType_Concrete_To_CalculatorType_OpenGeneric_Dictionary[dtoType_Concrete];

            IList<Type> calculatorType_OpenGenericTypeArguments = calculatorType_OpenGeneric.GetGenericArguments();
            IList<InletDto> inletDtos = operatorDto.InletDtos;

            if (calculatorType_OpenGenericTypeArguments.Count != inletDtos.Count)
            {
                throw new NotEqualException(
                    () => calculatorType_OpenGenericTypeArguments.Count,
                    () => inletDtos.Count);
            }

            int count = inletDtos.Count;

            Type[] calculatorType_ClosedGenericTypeArguments = new Type[count];
            for (int i = 0; i < count; i++)
            {
                Type calculatorType_OpenGenericTypeArgument = calculatorType_OpenGenericTypeArguments[i];
                InletDto inletDto = inletDtos[i];

                Type calculatorType_ClosedGenericTypeArgument = ConvertToClosedGenericType(inletDto.InputOperatorDto);

                calculatorType_ClosedGenericTypeArguments[i] = calculatorType_ClosedGenericTypeArgument;
            }

            Type calculatorType_ClosedGeneric = calculatorType_OpenGeneric.MakeGenericType(calculatorType_ClosedGenericTypeArguments);

            return calculatorType_ClosedGeneric;
        }
    }
}
