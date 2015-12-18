using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using JJ.Business.Synthesizer.EntityWrappers;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class PatchExtensions
    {
        private static Dictionary<Type, OperatorTypeEnum> _OperatorWrapperType_To_OperatorTypeEnum_dictionary = new Dictionary<Type, OperatorTypeEnum>
        {
            { typeof(Add_OperatorWrapper),OperatorTypeEnum.Add },
            { typeof(Adder_OperatorWrapper), OperatorTypeEnum.Adder },
            { typeof(Divide_OperatorWrapper), OperatorTypeEnum.Divide },
            { typeof(Multiply_OperatorWrapper), OperatorTypeEnum.Multiply },
            { typeof(PatchInlet_OperatorWrapper), OperatorTypeEnum.PatchInlet },
            { typeof(PatchOutlet_OperatorWrapper), OperatorTypeEnum.PatchOutlet },
            { typeof(Power_OperatorWrapper), OperatorTypeEnum.Power },
            { typeof(Sine_OperatorWrapper), OperatorTypeEnum.Sine },
            { typeof(Subtract_OperatorWrapper), OperatorTypeEnum.Subtract },
            { typeof(Delay_OperatorWrapper), OperatorTypeEnum.Delay },
            { typeof(SpeedUp_OperatorWrapper), OperatorTypeEnum.SpeedUp },
            { typeof(SlowDown_OperatorWrapper), OperatorTypeEnum.SlowDown },
            { typeof(TimePower_OperatorWrapper), OperatorTypeEnum.TimePower },
            { typeof(Earlier_OperatorWrapper), OperatorTypeEnum.Earlier },
            { typeof(Number_OperatorWrapper), OperatorTypeEnum.Number },
            { typeof(Curve_OperatorWrapper), OperatorTypeEnum.Curve },
            { typeof(Sample_OperatorWrapper), OperatorTypeEnum.Sample },
            { typeof(WhiteNoise_OperatorWrapper), OperatorTypeEnum.WhiteNoise },
            { typeof(Resample_OperatorWrapper), OperatorTypeEnum.Resample },
            { typeof(CustomOperator_OperatorWrapper), OperatorTypeEnum.CustomOperator },
            { typeof(SawTooth_OperatorWrapper), OperatorTypeEnum.SawTooth },
            { typeof(SquareWave_OperatorWrapper), OperatorTypeEnum.SquareWave },
            { typeof(TriangleWave_OperatorWrapper), OperatorTypeEnum.TriangleWave },
            { typeof(Exponent_OperatorWrapper), OperatorTypeEnum.Exponent },
            { typeof(Loop_OperatorWrapper), OperatorTypeEnum.Loop },
            { typeof(Select_OperatorWrapper), OperatorTypeEnum.Select },
            { typeof(Bundle_OperatorWrapper), OperatorTypeEnum.Bundle },
            { typeof(Unbundle_OperatorWrapper), OperatorTypeEnum.Unbundle }
        };

        public static IList<Operator> GetOperatorsOfType(this Patch patch, OperatorTypeEnum operatorTypeEnum)
        {
            if (patch == null) throw new NullException(() => patch);

            return patch.Operators.Where(x => x.GetOperatorTypeEnum() == operatorTypeEnum).ToArray();
        }

        public static IEnumerable<TOperatorWrapper> EnumerateOperatorWrappersOfType<TOperatorWrapper>(this Patch patch)
            where TOperatorWrapper : OperatorWrapperBase
        {
            Type operatorWrapperType = typeof(TOperatorWrapper);
            OperatorTypeEnum operatorTypeEnum;
            if (!_OperatorWrapperType_To_OperatorTypeEnum_dictionary.TryGetValue(operatorWrapperType, out operatorTypeEnum))
            {
                throw new NotSupportedException(String.Format("OperatorWrapper Type '{0}' not supported.", typeof(TOperatorWrapper).Name));
            }

            IList<Operator> operators = GetOperatorsOfType(patch, operatorTypeEnum);

            foreach (Operator op in operators)
            {
                TOperatorWrapper wrapper = (TOperatorWrapper)Activator.CreateInstance(operatorWrapperType, op);
                yield return wrapper;
            }
        }
    }
}