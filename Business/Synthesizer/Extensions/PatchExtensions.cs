using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class PatchExtensions
    {
        private static readonly Dictionary<Type, OperatorTypeEnum> _operatorWrapperType_To_OperatorTypeEnum_Dictionary = new Dictionary<Type, OperatorTypeEnum>
        {
            { typeof(Absolute_OperatorWrapper), OperatorTypeEnum.Absolute },
            { typeof(Add_OperatorWrapper), OperatorTypeEnum.Add },
            { typeof(AllPassFilter_OperatorWrapper), OperatorTypeEnum.AllPassFilter },
            { typeof(And_OperatorWrapper), OperatorTypeEnum.And },
            { typeof(AverageFollower_OperatorWrapper), OperatorTypeEnum.AverageFollower },
            { typeof(AverageOverDimension_OperatorWrapper), OperatorTypeEnum.AverageOverDimension },
            { typeof(AverageOverInlets_OperatorWrapper), OperatorTypeEnum.AverageOverInlets },
            { typeof(BandPassFilterConstantPeakGain_OperatorWrapper), OperatorTypeEnum.BandPassFilterConstantPeakGain },
            { typeof(BandPassFilterConstantTransitionGain_OperatorWrapper), OperatorTypeEnum.BandPassFilterConstantTransitionGain },
            { typeof(Cache_OperatorWrapper), OperatorTypeEnum.Cache },
            { typeof(ChangeTrigger_OperatorWrapper), OperatorTypeEnum.ChangeTrigger },
            { typeof(ClosestOverDimension_OperatorWrapper), OperatorTypeEnum.ClosestOverDimension },
            { typeof(ClosestOverDimensionExp_OperatorWrapper), OperatorTypeEnum.ClosestOverDimensionExp },
            { typeof(ClosestOverInlets_OperatorWrapper), OperatorTypeEnum.ClosestOverInlets },
            { typeof(ClosestOverInletsExp_OperatorWrapper), OperatorTypeEnum.ClosestOverInletsExp },
            { typeof(Curve_OperatorWrapper), OperatorTypeEnum.Curve },
            { typeof(CustomOperator_OperatorWrapper), OperatorTypeEnum.CustomOperator },
            { typeof(DimensionToOutlets_OperatorWrapper), OperatorTypeEnum.DimensionToOutlets },
            { typeof(Divide_OperatorWrapper), OperatorTypeEnum.Divide },
            { typeof(Equal_OperatorWrapper), OperatorTypeEnum.Equal },
            { typeof(Exponent_OperatorWrapper), OperatorTypeEnum.Exponent },
            { typeof(GetDimension_OperatorWrapper), OperatorTypeEnum.GetDimension },
            { typeof(GreaterThan_OperatorWrapper), OperatorTypeEnum.GreaterThan },
            { typeof(GreaterThanOrEqual_OperatorWrapper), OperatorTypeEnum.GreaterThanOrEqual },
            { typeof(HighPassFilter_OperatorWrapper), OperatorTypeEnum.HighPassFilter },
            { typeof(HighShelfFilter_OperatorWrapper), OperatorTypeEnum.HighShelfFilter },
            { typeof(Hold_OperatorWrapper), OperatorTypeEnum.Hold },
            { typeof(If_OperatorWrapper), OperatorTypeEnum.If },
            { typeof(InletsToDimension_OperatorWrapper), OperatorTypeEnum.InletsToDimension },
            { typeof(Interpolate_OperatorWrapper), OperatorTypeEnum.Interpolate },
            { typeof(LessThan_OperatorWrapper), OperatorTypeEnum.LessThan },
            { typeof(LessThanOrEqual_OperatorWrapper), OperatorTypeEnum.LessThanOrEqual },
            { typeof(Loop_OperatorWrapper), OperatorTypeEnum.Loop },
            { typeof(LowPassFilter_OperatorWrapper), OperatorTypeEnum.LowPassFilter },
            { typeof(LowShelfFilter_OperatorWrapper), OperatorTypeEnum.LowShelfFilter },
            { typeof(MaxFollower_OperatorWrapper), OperatorTypeEnum.MaxFollower },
            { typeof(MaxOverDimension_OperatorWrapper), OperatorTypeEnum.MaxOverDimension },
            { typeof(MaxOverInlets_OperatorWrapper), OperatorTypeEnum.MaxOverInlets },
            { typeof(MinFollower_OperatorWrapper), OperatorTypeEnum.MinFollower },
            { typeof(MinOverDimension_OperatorWrapper), OperatorTypeEnum.MinOverDimension },
            { typeof(MinOverInlets_OperatorWrapper), OperatorTypeEnum.MinOverInlets },
            { typeof(Multiply_OperatorWrapper), OperatorTypeEnum.Multiply },
            { typeof(MultiplyWithOrigin_OperatorWrapper), OperatorTypeEnum.MultiplyWithOrigin },
            { typeof(Negative_OperatorWrapper), OperatorTypeEnum.Negative },
            { typeof(Noise_OperatorWrapper), OperatorTypeEnum.Noise },
            { typeof(Not_OperatorWrapper), OperatorTypeEnum.Not },
            { typeof(NotchFilter_OperatorWrapper), OperatorTypeEnum.NotchFilter },
            { typeof(NotEqual_OperatorWrapper), OperatorTypeEnum.NotEqual },
            { typeof(Number_OperatorWrapper), OperatorTypeEnum.Number },
            { typeof(OneOverX_OperatorWrapper), OperatorTypeEnum.OneOverX },
            { typeof(Or_OperatorWrapper), OperatorTypeEnum.Or },
            { typeof(PatchInlet_OperatorWrapper), OperatorTypeEnum.PatchInlet },
            { typeof(PatchOutlet_OperatorWrapper), OperatorTypeEnum.PatchOutlet },
            { typeof(PeakingEQFilter_OperatorWrapper), OperatorTypeEnum.PeakingEQFilter },
            { typeof(Power_OperatorWrapper), OperatorTypeEnum.Power },
            { typeof(Pulse_OperatorWrapper), OperatorTypeEnum.Pulse },
            { typeof(PulseTrigger_OperatorWrapper), OperatorTypeEnum.PulseTrigger },
            { typeof(Random_OperatorWrapper), OperatorTypeEnum.Random },
            { typeof(RangeOverDimension_OperatorWrapper), OperatorTypeEnum.RangeOverDimension },
            { typeof(RangeOverOutlets_OperatorWrapper), OperatorTypeEnum.RangeOverOutlets },
            { typeof(Reset_OperatorWrapper), OperatorTypeEnum.Reset },
            { typeof(Reverse_OperatorWrapper), OperatorTypeEnum.Reverse },
            { typeof(Round_OperatorWrapper), OperatorTypeEnum.Round },
            { typeof(Sample_OperatorWrapper), OperatorTypeEnum.Sample },
            { typeof(SawDown_OperatorWrapper), OperatorTypeEnum.SawDown },
            { typeof(SawUp_OperatorWrapper), OperatorTypeEnum.SawUp },
            { typeof(Scaler_OperatorWrapper), OperatorTypeEnum.Scaler },
            { typeof(SetDimension_OperatorWrapper), OperatorTypeEnum.SetDimension },
            { typeof(Shift_OperatorWrapper), OperatorTypeEnum.Shift },
            { typeof(Sine_OperatorWrapper), OperatorTypeEnum.Sine },
            { typeof(SortOverDimension_OperatorWrapper), OperatorTypeEnum.SortOverDimension },
            { typeof(SortOverInlets_OperatorWrapper), OperatorTypeEnum.SortOverInlets },
            { typeof(Spectrum_OperatorWrapper), OperatorTypeEnum.Spectrum },
            { typeof(Square_OperatorWrapper), OperatorTypeEnum.Square },
            { typeof(Squash_OperatorWrapper), OperatorTypeEnum.Squash },
            { typeof(Stretch_OperatorWrapper), OperatorTypeEnum.Stretch },
            { typeof(Subtract_OperatorWrapper), OperatorTypeEnum.Subtract },
            { typeof(SumFollower_OperatorWrapper), OperatorTypeEnum.SumFollower },
            { typeof(SumOverDimension_OperatorWrapper), OperatorTypeEnum.SumOverDimension },
            { typeof(TimePower_OperatorWrapper), OperatorTypeEnum.TimePower },
            { typeof(ToggleTrigger_OperatorWrapper), OperatorTypeEnum.ToggleTrigger },
            { typeof(Triangle_OperatorWrapper), OperatorTypeEnum.Triangle }
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
            if (!_operatorWrapperType_To_OperatorTypeEnum_Dictionary.TryGetValue(operatorWrapperType, out OperatorTypeEnum operatorTypeEnum))
            {
                throw new NotSupportedException($"OperatorWrapper Type '{typeof(TOperatorWrapper)}' not supported.");
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