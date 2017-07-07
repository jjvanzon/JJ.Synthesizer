using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public static class OperatorWrapperFactory
    {
        public static OperatorWrapperBase CreateOperatorWrapper(
            Operator op,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository)
        {
            if (op == null) throw new NullException(() => op);

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            if (operatorTypeEnum == OperatorTypeEnum.Curve)
            {
                return new Curve_OperatorWrapper(op, curveRepository);
            }

            if (operatorTypeEnum == OperatorTypeEnum.Sample)
            {
                return new Sample_OperatorWrapper(op, sampleRepository);
            }

            if (_createOperatorWrapperDelegateDictionary.TryGetValue(operatorTypeEnum, out Func<Operator, OperatorWrapperBase> func))
            {
                OperatorWrapperBase wrapper = func(op);
                return wrapper;
            }

            // Otherwise assume WithUnderlyingPatch.
            return new OperatorWrapper_WithUnderlyingPatch(op);
        }

        private static readonly Dictionary<OperatorTypeEnum, Func<Operator, OperatorWrapperBase>> _createOperatorWrapperDelegateDictionary =
                   new Dictionary<OperatorTypeEnum, Func<Operator, OperatorWrapperBase>>
        {
            { OperatorTypeEnum.AverageFollower, Create_AverageFollower_OperatorWrapper },
            { OperatorTypeEnum.AverageOverDimension, Create_AverageOverDimension_OperatorWrapper },
            { OperatorTypeEnum.Cache, Create_Cache_OperatorWrapper },
            { OperatorTypeEnum.ChangeTrigger, Create_ChangeTrigger_OperatorWrapper },
            { OperatorTypeEnum.ClosestOverDimension, Create_ClosestOverDimension_OperatorWrapper },
            { OperatorTypeEnum.ClosestOverDimensionExp, Create_ClosestOverDimensionExp_OperatorWrapper },
            { OperatorTypeEnum.DimensionToOutlets, Create_DimensionToOutlets_OperatorWrapper },
            { OperatorTypeEnum.Exponent, Create_Exponent_OperatorWrapper },
            { OperatorTypeEnum.GetDimension, Create_GetDimension_OperatorWrapper },
            { OperatorTypeEnum.Hold, Create_Hold_OperatorWrapper },
            { OperatorTypeEnum.InletsToDimension, Create_InletsToDimension_OperatorWrapper },
            { OperatorTypeEnum.Interpolate, Create_Interpolate_OperatorWrapper },
            { OperatorTypeEnum.Loop, Create_Loop_OperatorWrapper },
            { OperatorTypeEnum.MaxFollower, Create_MaxFollower_OperatorWrapper },
            { OperatorTypeEnum.MaxOverDimension, Create_MaxOverDimension_OperatorWrapper },
            { OperatorTypeEnum.MinFollower, Create_MinFollower_OperatorWrapper },
            { OperatorTypeEnum.MinOverDimension, Create_MinOverDimension_OperatorWrapper },
            { OperatorTypeEnum.Number , Create_Number_OperatorWrapper },
            { OperatorTypeEnum.PatchInlet , Create_PatchInlet_OperatorWrapper },
            { OperatorTypeEnum.PatchOutlet, Create_PatchOutlet_OperatorWrapper },
            { OperatorTypeEnum.PulseTrigger, Create_PulseTrigger_OperatorWrapper },
            { OperatorTypeEnum.Random, Create_Random_OperatorWrapper },
            { OperatorTypeEnum.RangeOverDimension, Create_RangeOverDimension_OperatorWrapper },
            { OperatorTypeEnum.Reset, Create_Reset_OperatorWrapper },
            { OperatorTypeEnum.Reverse, Create_Reverse_OperatorWrapper },
            { OperatorTypeEnum.Round, Create_Round_OperatorWrapper },
            { OperatorTypeEnum.Scaler, Create_Scaler_OperatorWrapper },
            { OperatorTypeEnum.SetDimension, Create_SetDimension_OperatorWrapper },
            { OperatorTypeEnum.Shift, Create_Shift_OperatorWrapper },
            { OperatorTypeEnum.SortOverDimension, Create_SortOverDimension_OperatorWrapper },
            { OperatorTypeEnum.Spectrum, Create_Spectrum_OperatorWrapper },
            { OperatorTypeEnum.Squash, Create_Squash_OperatorWrapper },
            { OperatorTypeEnum.Stretch, Create_Stretch_OperatorWrapper },
            { OperatorTypeEnum.SumFollower, Create_SumFollower_OperatorWrapper },
            { OperatorTypeEnum.SumOverDimension, Create_SumOverDimension_OperatorWrapper },
            { OperatorTypeEnum.TimePower , Create_TimePower_OperatorWrapper },
            { OperatorTypeEnum.ToggleTrigger , Create_ToggleTrigger_OperatorWrapper },
        };

        private static AverageFollower_OperatorWrapper Create_AverageFollower_OperatorWrapper(Operator op) { return new AverageFollower_OperatorWrapper(op); }
        private static AverageOverDimension_OperatorWrapper Create_AverageOverDimension_OperatorWrapper(Operator op) { return new AverageOverDimension_OperatorWrapper(op); }
        private static Cache_OperatorWrapper Create_Cache_OperatorWrapper(Operator op) { return new Cache_OperatorWrapper(op); }
        private static ChangeTrigger_OperatorWrapper Create_ChangeTrigger_OperatorWrapper(Operator op) { return new ChangeTrigger_OperatorWrapper(op); }
        private static ClosestOverDimension_OperatorWrapper Create_ClosestOverDimension_OperatorWrapper(Operator op) { return new ClosestOverDimension_OperatorWrapper(op); }
        private static ClosestOverDimensionExp_OperatorWrapper Create_ClosestOverDimensionExp_OperatorWrapper(Operator op) { return new ClosestOverDimensionExp_OperatorWrapper(op); }
        private static DimensionToOutlets_OperatorWrapper Create_DimensionToOutlets_OperatorWrapper(Operator op) { return new DimensionToOutlets_OperatorWrapper(op); }
        private static Exponent_OperatorWrapper Create_Exponent_OperatorWrapper(Operator op) { return new Exponent_OperatorWrapper(op); }
        private static GetDimension_OperatorWrapper Create_GetDimension_OperatorWrapper(Operator op) { return new GetDimension_OperatorWrapper(op); }
        private static Hold_OperatorWrapper Create_Hold_OperatorWrapper(Operator op) { return new Hold_OperatorWrapper(op); }
        private static InletsToDimension_OperatorWrapper Create_InletsToDimension_OperatorWrapper(Operator op) { return new InletsToDimension_OperatorWrapper(op); }
        private static Interpolate_OperatorWrapper Create_Interpolate_OperatorWrapper(Operator op) { return new Interpolate_OperatorWrapper(op); }
        private static Loop_OperatorWrapper Create_Loop_OperatorWrapper(Operator op) { return new Loop_OperatorWrapper(op); }
        private static MaxFollower_OperatorWrapper Create_MaxFollower_OperatorWrapper(Operator op) { return new MaxFollower_OperatorWrapper(op); }
        private static MaxOverDimension_OperatorWrapper Create_MaxOverDimension_OperatorWrapper(Operator op) { return new MaxOverDimension_OperatorWrapper(op); }
        private static MinFollower_OperatorWrapper Create_MinFollower_OperatorWrapper(Operator op) { return new MinFollower_OperatorWrapper(op); }
        private static MinOverDimension_OperatorWrapper Create_MinOverDimension_OperatorWrapper(Operator op) { return new MinOverDimension_OperatorWrapper(op); }
        private static Number_OperatorWrapper Create_Number_OperatorWrapper(Operator op) { return new Number_OperatorWrapper(op); }
        private static PatchInlet_OperatorWrapper Create_PatchInlet_OperatorWrapper(Operator op) { return new PatchInlet_OperatorWrapper(op); }
        private static PatchOutlet_OperatorWrapper Create_PatchOutlet_OperatorWrapper(Operator op) { return new PatchOutlet_OperatorWrapper(op); }
        private static PulseTrigger_OperatorWrapper Create_PulseTrigger_OperatorWrapper(Operator op) { return new PulseTrigger_OperatorWrapper(op); }
        private static Random_OperatorWrapper Create_Random_OperatorWrapper(Operator op) { return new Random_OperatorWrapper(op); }
        private static RangeOverDimension_OperatorWrapper Create_RangeOverDimension_OperatorWrapper(Operator op) { return new RangeOverDimension_OperatorWrapper(op); }
        private static Reset_OperatorWrapper Create_Reset_OperatorWrapper(Operator op) { return new Reset_OperatorWrapper(op); }
        private static Reverse_OperatorWrapper Create_Reverse_OperatorWrapper(Operator op) { return new Reverse_OperatorWrapper(op); }
        private static Round_OperatorWrapper Create_Round_OperatorWrapper(Operator op) { return new Round_OperatorWrapper(op); }
        private static Scaler_OperatorWrapper Create_Scaler_OperatorWrapper(Operator op) { return new Scaler_OperatorWrapper(op); }
        private static SetDimension_OperatorWrapper Create_SetDimension_OperatorWrapper(Operator op) { return new SetDimension_OperatorWrapper(op); }
        private static Shift_OperatorWrapper Create_Shift_OperatorWrapper(Operator op) { return new Shift_OperatorWrapper(op); }
        private static SortOverDimension_OperatorWrapper Create_SortOverDimension_OperatorWrapper(Operator op) { return new SortOverDimension_OperatorWrapper(op); }
        private static Spectrum_OperatorWrapper Create_Spectrum_OperatorWrapper(Operator op) { return new Spectrum_OperatorWrapper(op); }
        private static Squash_OperatorWrapper Create_Squash_OperatorWrapper(Operator op) { return new Squash_OperatorWrapper(op); }
        private static Stretch_OperatorWrapper Create_Stretch_OperatorWrapper(Operator op) { return new Stretch_OperatorWrapper(op); }
        private static SumFollower_OperatorWrapper Create_SumFollower_OperatorWrapper(Operator op) { return new SumFollower_OperatorWrapper(op); }
        private static SumOverDimension_OperatorWrapper Create_SumOverDimension_OperatorWrapper(Operator op) { return new SumOverDimension_OperatorWrapper(op); }
        private static TimePower_OperatorWrapper Create_TimePower_OperatorWrapper(Operator op) { return new TimePower_OperatorWrapper(op); }
        private static ToggleTrigger_OperatorWrapper Create_ToggleTrigger_OperatorWrapper(Operator op) { return new ToggleTrigger_OperatorWrapper(op); }
    }
}