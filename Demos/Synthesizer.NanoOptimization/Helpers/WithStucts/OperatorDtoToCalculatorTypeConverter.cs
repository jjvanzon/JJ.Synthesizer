using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithStructs;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Helpers.WithStucts
{
    internal static class OperatorDtoToCalculatorTypeConverter
    {
        private static readonly Dictionary<Type, Type> _dtoType_Concrete_To_CalculatorType_OpenGeneric_Dictionary =
                            new Dictionary<Type, Type>
        {
            { typeof(Multiply_OperatorDto_VarA_ConstB), typeof(Multiply_OperatorCalculator_VarA_ConstB<>) },
            { typeof(Multiply_OperatorDto_VarA_VarB), typeof(Multiply_OperatorCalculator_VarA_VarB<,>) },
            { typeof(Number_OperatorDto), typeof(Number_OperatorCalculator) },
            { typeof(Number_OperatorDto_NaN), typeof(Number_OperatorCalculator_NaN) },
            { typeof(Number_OperatorDto_One), typeof(Number_OperatorCalculator_One) },
            { typeof(Number_OperatorDto_Zero), typeof(Number_OperatorCalculator_Zero) },
            { typeof(Shift_OperatorDto_VarSignal_ConstDistance), typeof(Shift_OperatorCalculator_VarSignal_ConstDistance<>) },
            { typeof(Shift_OperatorDto_VarSignal_VarDistance), typeof(Shift_OperatorCalculator_VarSignal_VarDistance<,>) },
            { typeof(Sine_OperatorDto_ConstFrequency_NoOriginShifting), typeof(Sine_OperatorCalculator_ConstFrequency_NoOriginShifting) },
            { typeof(Sine_OperatorDto_VarFrequency_NoPhaseTracking), typeof(Sine_OperatorCalculator_VarFrequency_NoPhaseTracking<>) },
            { typeof(Sine_OperatorDto_VarFrequency_WithPhaseTracking), typeof(Sine_OperatorCalculator_VarFrequency_WithPhaseTracking<>) },
            { typeof(VariableInput_OperatorDto), typeof(VariableInput_OperatorCalculator) },
        };

        public static Type ConvertToClosedGenericType(OperatorDtoBase operatorDto)
        {
            if (operatorDto == null) throw new NullException(() => operatorDto);

            Type calculatorType_OpenGeneric = Get_CalculatorType_OpenGeneric_By_DtoType_Concrete(operatorDto);

            IList<Type> calculatorType_OpenGenericTypeArguments = calculatorType_OpenGeneric.GetGenericArguments();
            IList<OperatorDtoBase> inputOperatorDtos = operatorDto.InputOperatorDtos;

            if (calculatorType_OpenGenericTypeArguments.Count != inputOperatorDtos.Count)
            {
                throw new NotEqualException(
                    () => calculatorType_OpenGenericTypeArguments.Count,
                    () => inputOperatorDtos.Count);
            }

            int count = inputOperatorDtos.Count;

            // .NET does not believe in generic types with 0 type arguments, so handle that.
            if (count == 0)
            {
                return calculatorType_OpenGeneric;
            }

            Type[] calculatorType_ClosedGenericTypeArguments = new Type[count];
            for (int i = 0; i < count; i++)
            {
                Type calculatorType_OpenGenericTypeArgument = calculatorType_OpenGenericTypeArguments[i];
                OperatorDtoBase inputOperatorDto = inputOperatorDtos[i];

                Type calculatorType_ClosedGenericTypeArgument = ConvertToClosedGenericType(inputOperatorDto);

                calculatorType_ClosedGenericTypeArguments[i] = calculatorType_ClosedGenericTypeArgument;
            }

            Type calculatorType_ClosedGeneric = calculatorType_OpenGeneric.MakeGenericType(calculatorType_ClosedGenericTypeArguments);

            return calculatorType_ClosedGeneric;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Type Get_CalculatorType_OpenGeneric_By_DtoType_Concrete(OperatorDtoBase operatorDto)
        {
            Type dtoType_Concrete = operatorDto.GetType();

            if (dtoType_Concrete == typeof(Add_OperatorDto_Vars))
            {
                switch (operatorDto.InputOperatorDtos.Count)
                {
                    case 2: return typeof(Add_OperatorCalculator_2Vars<,>);
                    case 3: return typeof(Add_OperatorCalculator_3Vars<,,>);
                    case 4: return typeof(Add_OperatorCalculator_4Vars<,,,>);
                    case 5: return typeof(Add_OperatorCalculator_5Vars<,,,,>);
                    case 6: return typeof(Add_OperatorCalculator_6Vars<,,,,,>);
                    case 7: return typeof(Add_OperatorCalculator_7Vars<,,,,,,>);
                    case 8: return typeof(Add_OperatorCalculator_8Vars<,,,,,,,>);
                    default: return typeof(Add_OperatorCalculator_VarArray);
                }
            }

            if (dtoType_Concrete == typeof(Add_OperatorDto_Vars_1Const))
            {
                switch (operatorDto.InputOperatorDtos.Count)
                {
                    case 1: return typeof(Add_OperatorCalculator_1Vars_1Const<>);
                    case 2: return typeof(Add_OperatorCalculator_2Vars_1Const<,>);
                    case 3: return typeof(Add_OperatorCalculator_3Vars_1Const<,,>);
                    case 4: return typeof(Add_OperatorCalculator_4Vars_1Const<,,,>);
                    case 5: return typeof(Add_OperatorCalculator_5Vars_1Const<,,,,>);
                    case 6: return typeof(Add_OperatorCalculator_6Vars_1Const<,,,,,>);
                    case 7: return typeof(Add_OperatorCalculator_7Vars_1Const<,,,,,,>);
                    default: return typeof(Add_OperatorCalculator_VarArray_1Const);
                }
            }

            Type type;
            if (!_dtoType_Concrete_To_CalculatorType_OpenGeneric_Dictionary.TryGetValue(dtoType_Concrete, out type))
            {
                throw new Exception(String.Format("No calculator type available for DTO type '{0}'.", dtoType_Concrete.Name));
            }
            return type;
        }
    }
}
