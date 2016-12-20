using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Exceptions;
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
            { typeof(Divide_OperatorWrapper), OperatorTypeEnum.Divide },
            { typeof(MultiplyWithOrigin_OperatorWrapper), OperatorTypeEnum.MultiplyWithOrigin },
            { typeof(PatchInlet_OperatorWrapper), OperatorTypeEnum.PatchInlet },
            { typeof(PatchOutlet_OperatorWrapper), OperatorTypeEnum.PatchOutlet },
            { typeof(Power_OperatorWrapper), OperatorTypeEnum.Power },
            { typeof(Sine_OperatorWrapper), OperatorTypeEnum.Sine },
            { typeof(Subtract_OperatorWrapper), OperatorTypeEnum.Subtract },
            { typeof(TimePower_OperatorWrapper), OperatorTypeEnum.TimePower },
            { typeof(Number_OperatorWrapper), OperatorTypeEnum.Number },
            { typeof(Curve_OperatorWrapper), OperatorTypeEnum.Curve },
            { typeof(Sample_OperatorWrapper), OperatorTypeEnum.Sample },
            { typeof(Noise_OperatorWrapper), OperatorTypeEnum.Noise },
            { typeof(Interpolate_OperatorWrapper), OperatorTypeEnum.Interpolate },
            { typeof(CustomOperator_OperatorWrapper), OperatorTypeEnum.CustomOperator },
            { typeof(SawUp_OperatorWrapper), OperatorTypeEnum.SawUp },
            { typeof(Square_OperatorWrapper), OperatorTypeEnum.Square },
            { typeof(Triangle_OperatorWrapper), OperatorTypeEnum.Triangle },
            { typeof(Exponent_OperatorWrapper), OperatorTypeEnum.Exponent },
            { typeof(Loop_OperatorWrapper), OperatorTypeEnum.Loop },
            { typeof(Select_OperatorWrapper), OperatorTypeEnum.Select },
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