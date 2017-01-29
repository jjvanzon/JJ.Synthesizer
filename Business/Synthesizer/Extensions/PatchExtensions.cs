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
        private static readonly Dictionary<Type, OperatorTypeEnum> _OperatorWrapperType_To_OperatorTypeEnum_dictionary = new Dictionary<Type, OperatorTypeEnum>
        {
            { typeof(Add_OperatorWrapper), OperatorTypeEnum.Add },
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
            { typeof(Stretch_OperatorWrapper), OperatorTypeEnum.Stretch },
            { typeof(Squash_OperatorWrapper), OperatorTypeEnum.Squash },
            { typeof(Shift_OperatorWrapper), OperatorTypeEnum.Shift },
            { typeof(Reset_OperatorWrapper), OperatorTypeEnum.Reset },
            { typeof(LowPassFilter_OperatorWrapper), OperatorTypeEnum.LowPassFilter },
            { typeof(HighPassFilter_OperatorWrapper), OperatorTypeEnum.HighPassFilter },
            { typeof(Spectrum_OperatorWrapper), OperatorTypeEnum.Spectrum },
            { typeof(Pulse_OperatorWrapper), OperatorTypeEnum.Pulse },
            { typeof(Random_OperatorWrapper), OperatorTypeEnum.Random },
            { typeof(Equal_OperatorWrapper), OperatorTypeEnum.Equal },
            { typeof(NotEqual_OperatorWrapper), OperatorTypeEnum.NotEqual },
            { typeof(LessThan_OperatorWrapper), OperatorTypeEnum.LessThan },
            { typeof(GreaterThan_OperatorWrapper), OperatorTypeEnum.GreaterThan },
            { typeof(LessThanOrEqual_OperatorWrapper), OperatorTypeEnum.LessThanOrEqual },
            { typeof(GreaterThanOrEqual_OperatorWrapper), OperatorTypeEnum.GreaterThanOrEqual },
            { typeof(And_OperatorWrapper), OperatorTypeEnum.And },
            { typeof(Or_OperatorWrapper), OperatorTypeEnum.Or },
            { typeof(Not_OperatorWrapper), OperatorTypeEnum.Not },
            { typeof(If_OperatorWrapper), OperatorTypeEnum.If },
            { typeof(MinFollower_OperatorWrapper), OperatorTypeEnum.MinFollower },
            { typeof(MaxFollower_OperatorWrapper), OperatorTypeEnum.MaxFollower },
            { typeof(AverageFollower_OperatorWrapper), OperatorTypeEnum.AverageFollower },
            { typeof(Scaler_OperatorWrapper), OperatorTypeEnum.Scaler },
            { typeof(SawDown_OperatorWrapper), OperatorTypeEnum.SawDown },
            { typeof(Absolute_OperatorWrapper), OperatorTypeEnum.Absolute },
            { typeof(Reverse_OperatorWrapper), OperatorTypeEnum.Reverse },
            { typeof(Round_OperatorWrapper), OperatorTypeEnum.Round },
            { typeof(Negative_OperatorWrapper), OperatorTypeEnum.Negative },
            { typeof(OneOverX_OperatorWrapper), OperatorTypeEnum.OneOverX },
            { typeof(Cache_OperatorWrapper), OperatorTypeEnum.Cache },
            { typeof(PulseTrigger_OperatorWrapper), OperatorTypeEnum.PulseTrigger },
            { typeof(ChangeTrigger_OperatorWrapper), OperatorTypeEnum.ChangeTrigger },
            { typeof(ToggleTrigger_OperatorWrapper), OperatorTypeEnum.ToggleTrigger },
            { typeof(GetDimension_OperatorWrapper), OperatorTypeEnum.GetDimension },
            { typeof(SetDimension_OperatorWrapper), OperatorTypeEnum.SetDimension },
            { typeof(Hold_OperatorWrapper), OperatorTypeEnum.Hold },
            { typeof(RangeOverDimension_OperatorWrapper), OperatorTypeEnum.RangeOverDimension },
            { typeof(DimensionToOutlets_OperatorWrapper), OperatorTypeEnum.DimensionToOutlets },
            { typeof(InletsToDimension_OperatorWrapper), OperatorTypeEnum.InletsToDimension },
            { typeof(MaxOverInlets_OperatorWrapper), OperatorTypeEnum.MaxOverInlets },
            { typeof(MinOverInlets_OperatorWrapper), OperatorTypeEnum.MinOverInlets },
            { typeof(AverageOverInlets_OperatorWrapper), OperatorTypeEnum.AverageOverInlets },
            { typeof(MaxOverDimension_OperatorWrapper), OperatorTypeEnum.MaxOverDimension },
            { typeof(MinOverDimension_OperatorWrapper), OperatorTypeEnum.MinOverDimension },
            { typeof(AverageOverDimension_OperatorWrapper), OperatorTypeEnum.AverageOverDimension },
            { typeof(SumOverDimension_OperatorWrapper), OperatorTypeEnum.SumOverDimension },
            { typeof(SumFollower_OperatorWrapper), OperatorTypeEnum.SumFollower },
            { typeof(Multiply_OperatorWrapper), OperatorTypeEnum.Multiply },
            { typeof(ClosestOverInlets_OperatorWrapper), OperatorTypeEnum.ClosestOverInlets },
            { typeof(ClosestOverDimension_OperatorWrapper), OperatorTypeEnum.ClosestOverDimension },
            { typeof(ClosestOverInletsExp_OperatorWrapper), OperatorTypeEnum.ClosestOverInletsExp },
            { typeof(ClosestOverDimensionExp_OperatorWrapper), OperatorTypeEnum.ClosestOverDimensionExp },
            { typeof(SortOverInlets_OperatorWrapper), OperatorTypeEnum.SortOverInlets },
            { typeof(SortOverDimension_OperatorWrapper), OperatorTypeEnum.SortOverDimension },
            { typeof(BandPassFilterConstantTransitionGain_OperatorWrapper), OperatorTypeEnum.BandPassFilterConstantTransitionGain },
            { typeof(BandPassFilterConstantPeakGain_OperatorWrapper), OperatorTypeEnum.BandPassFilterConstantPeakGain },
            { typeof(NotchFilter_OperatorWrapper), OperatorTypeEnum.NotchFilter },
            { typeof(AllPassFilter_OperatorWrapper), OperatorTypeEnum.AllPassFilter },
            { typeof(PeakingEQFilter_OperatorWrapper), OperatorTypeEnum.PeakingEQFilter },
            { typeof(LowShelfFilter_OperatorWrapper), OperatorTypeEnum.LowShelfFilter },
            { typeof(HighShelfFilter_OperatorWrapper), OperatorTypeEnum.HighShelfFilter },
            { typeof(RangeOverOutlets_OperatorWrapper), OperatorTypeEnum.RangeOverOutlets }
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
                throw new NotSupportedException(string.Format("OperatorWrapper Type '{0}' not supported.", typeof(TOperatorWrapper).Name));
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