using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithStructs;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.NanoOptimization.Helpers.WithStucts
{
    internal static class OperatorDtoToCalculatorTypeConverter
    {
        private static readonly Dictionary<Type, Type> _dtoType_Concrete_To_CalculatorType_OpenGeneric_Dictionary =
                            new Dictionary<Type, Type>
        {
            { typeof(Add_OperatorDto_VarA_ConstB), typeof(Add_OperatorCalculator_VarA_ConstB<>) },
            { typeof(Add_OperatorDto_VarA_VarB), typeof(Add_OperatorCalculator_VarA_VarB<,>) },
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

        public static Type ConvertToClosedGenericType(OperatorDto operatorDto)
        {
            if (operatorDto == null) throw new NullException(() => operatorDto);

            Type dtoType_Concrete = operatorDto.GetType();
            Type calculatorType_OpenGeneric = Get_CalculatorType_OpenGeneric_By_DtoType_Concrete(dtoType_Concrete);

            IList<Type> calculatorType_OpenGenericTypeArguments = calculatorType_OpenGeneric.GetGenericArguments();
            IList<InletDto> inletDtos = operatorDto.InletDtos;

            if (calculatorType_OpenGenericTypeArguments.Count != inletDtos.Count)
            {
                throw new NotEqualException(
                    () => calculatorType_OpenGenericTypeArguments.Count,
                    () => inletDtos.Count);
            }

            int count = inletDtos.Count;

            // .NET does not believe in generic types with 0 type arguments, so handle that.
            if (count == 0)
            {
                return calculatorType_OpenGeneric;
            }

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

        [DebuggerHidden]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Type Get_CalculatorType_OpenGeneric_By_DtoType_Concrete(Type dtoType_Concrete)
        {
            Type type;
            if (!_dtoType_Concrete_To_CalculatorType_OpenGeneric_Dictionary.TryGetValue(dtoType_Concrete, out type))
            {
                throw new Exception(String.Format("No calculator type available for DTO type '{0}'.", dtoType_Concrete.Name));
            }
            return type;
        }
    }
}
