using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public static class OperatorWrapperFactory
    {
        private static Dictionary<OperatorTypeEnum, Func<Operator, OperatorWrapperBase>> _createOperatorWrapperDelegateDictionary =
                   new Dictionary<OperatorTypeEnum, Func<Operator, OperatorWrapperBase>>
        {
            { OperatorTypeEnum.Absolute, Create_Absolute_OperatorWrapper },
            { OperatorTypeEnum.Add , Create_Add_OperatorWrapper },
            { OperatorTypeEnum.Adder , Create_Adder_OperatorWrapper },
            { OperatorTypeEnum.And, Create_And_OperatorWrapper },
            { OperatorTypeEnum.Average, Create_Average_OperatorWrapper },
            { OperatorTypeEnum.AverageDiscrete, Create_AverageDiscrete_OperatorWrapper },
            { OperatorTypeEnum.Bundle, Create_Bundle_OperatorWrapper },
            { OperatorTypeEnum.ChangeTrigger, Create_ChangeTrigger_OperatorWrapper },
            { OperatorTypeEnum.Cache, Create_Cache_OperatorWrapper },
            { OperatorTypeEnum.Delay , Create_Delay_OperatorWrapper },
            { OperatorTypeEnum.Divide , Create_Divide_OperatorWrapper },
            { OperatorTypeEnum.Earlier , Create_Earlier_OperatorWrapper },
            { OperatorTypeEnum.Equal, Create_Equal_OperatorWrapper },
            { OperatorTypeEnum.Exponent, Create_Exponent_OperatorWrapper },
            { OperatorTypeEnum.Filter, Create_Filter_OperatorWrapper },
            { OperatorTypeEnum.GetDimension, Create_GetDimension_OperatorWrapper },
            { OperatorTypeEnum.GreaterThan, Create_GreaterThan_OperatorWrapper },
            { OperatorTypeEnum.GreaterThanOrEqual, Create_GreaterThanOrEqual_OperatorWrapper },
            { OperatorTypeEnum.HighPassFilter, Create_HighPassFilter_OperatorWrapper },
            { OperatorTypeEnum.Hold, Create_Hold_OperatorWrapper },
            { OperatorTypeEnum.If, Create_If_OperatorWrapper },
            { OperatorTypeEnum.LessThan, Create_LessThan_OperatorWrapper },
            { OperatorTypeEnum.LessThanOrEqual, Create_LessThanOrEqual_OperatorWrapper },
            { OperatorTypeEnum.Loop, Create_Loop_OperatorWrapper },
            { OperatorTypeEnum.LowPassFilter, Create_LowPassFilter_OperatorWrapper },
            { OperatorTypeEnum.MakeContinuous, Create_MakeContinuous_OperatorWrapper },
            { OperatorTypeEnum.MakeDiscrete, Create_MakeDiscrete_OperatorWrapper },
            { OperatorTypeEnum.MaxDiscrete, Create_MaxDiscrete_OperatorWrapper },
            { OperatorTypeEnum.Maximum, Create_Maximum_OperatorWrapper },
            { OperatorTypeEnum.MinDiscrete, Create_MinDiscrete_OperatorWrapper },
            { OperatorTypeEnum.Minimum, Create_Minimum_OperatorWrapper },
            { OperatorTypeEnum.Multiply , Create_Multiply_OperatorWrapper },
            { OperatorTypeEnum.Narrower, Create_Narrower_OperatorWrapper },
            { OperatorTypeEnum.Negative, Create_Negative_OperatorWrapper },
            { OperatorTypeEnum.Noise, Create_Noise_OperatorWrapper },
            { OperatorTypeEnum.Not, Create_Not_OperatorWrapper },
            { OperatorTypeEnum.NotEqual, Create_NotEqual_OperatorWrapper },
            { OperatorTypeEnum.Number , Create_Number_OperatorWrapper },
            { OperatorTypeEnum.OneOverX, Create_OneOverX_OperatorWrapper },
            { OperatorTypeEnum.Or, Create_Or_OperatorWrapper },
            { OperatorTypeEnum.PatchInlet , Create_PatchInlet_OperatorWrapper },
            { OperatorTypeEnum.PatchOutlet, Create_PatchOutlet_OperatorWrapper },
            { OperatorTypeEnum.Power, Create_Power_OperatorWrapper },
            { OperatorTypeEnum.PulseTrigger, Create_PulseTrigger_OperatorWrapper },
            { OperatorTypeEnum.Pulse, Create_Pulse_OperatorWrapper },
            { OperatorTypeEnum.Random, Create_Random_OperatorWrapper },
            { OperatorTypeEnum.Range, Create_Range_OperatorWrapper },
            { OperatorTypeEnum.Resample, Create_Resample_OperatorWrapper },
            { OperatorTypeEnum.Reset, Create_Reset_OperatorWrapper },
            { OperatorTypeEnum.Reverse, Create_Reverse_OperatorWrapper },
            { OperatorTypeEnum.Round, Create_Round_OperatorWrapper },
            { OperatorTypeEnum.SawDown, Create_SawDown_OperatorWrapper },
            { OperatorTypeEnum.SawUp, Create_SawUp_OperatorWrapper },
            { OperatorTypeEnum.Scaler, Create_Scaler_OperatorWrapper },
            { OperatorTypeEnum.Select, Create_Select_OperatorWrapper },
            { OperatorTypeEnum.SetDimension, Create_SetDimension_OperatorWrapper },
            { OperatorTypeEnum.Shift, Create_Shift_OperatorWrapper },
            { OperatorTypeEnum.Sine , Create_Sine_OperatorWrapper },
            { OperatorTypeEnum.SlowDown , Create_SlowDown_OperatorWrapper },
            { OperatorTypeEnum.Spectrum, Create_Spectrum_OperatorWrapper },
            { OperatorTypeEnum.SpeedUp , Create_SpeedUp_OperatorWrapper },
            { OperatorTypeEnum.Square, Create_Square_OperatorWrapper },
            { OperatorTypeEnum.Stretch, Create_Stretch_OperatorWrapper },
            { OperatorTypeEnum.Subtract, Create_Subtract_OperatorWrapper },
            { OperatorTypeEnum.TimePower , Create_TimePower_OperatorWrapper },
            { OperatorTypeEnum.ToggleTrigger , Create_ToggleTrigger_OperatorWrapper },
            { OperatorTypeEnum.Triangle, Create_Triangle_OperatorWrapper },
            { OperatorTypeEnum.Unbundle, Create_Unbundle_OperatorWrapper },
        };

        public static Dictionary<OperatorTypeEnum, Func<Operator, OperatorWrapperBase>> CreateOperatorWrapperDelegateDictionary
        {
            get
            {
                return _createOperatorWrapperDelegateDictionary;
            }

            set
            {
                _createOperatorWrapperDelegateDictionary = value;
            }
        }

        public static OperatorWrapperBase CreateOperatorWrapper(
            Operator op,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository,
            IPatchRepository patchRepository)
        {
            if (op == null) throw new NullException(() => op);

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Curve:
                    return new Curve_OperatorWrapper(op, curveRepository);

                case OperatorTypeEnum.Sample:
                    return new Sample_OperatorWrapper(op, sampleRepository);

                case OperatorTypeEnum.CustomOperator:
                    return new CustomOperator_OperatorWrapper(op, patchRepository);

                default:
                    Func<Operator, OperatorWrapperBase> func;
                    if (!_createOperatorWrapperDelegateDictionary.TryGetValue(operatorTypeEnum, out func))
                    {
                        throw new Exception(String.Format("_createOperatorWrapperDelegateDictionary does not contain entry for OperatorTypeEnum '{0}'.", operatorTypeEnum));
                    }

                    OperatorWrapperBase wrapper = func(op);

                    return wrapper;
            }
        }

        private static Absolute_OperatorWrapper Create_Absolute_OperatorWrapper(Operator op) { return new Absolute_OperatorWrapper(op); }
        private static Add_OperatorWrapper Create_Add_OperatorWrapper(Operator op) { return new Add_OperatorWrapper(op); }
        private static Adder_OperatorWrapper Create_Adder_OperatorWrapper(Operator op) { return new Adder_OperatorWrapper(op); }
        private static And_OperatorWrapper Create_And_OperatorWrapper(Operator op) { return new And_OperatorWrapper(op); }
        private static Average_OperatorWrapper Create_Average_OperatorWrapper(Operator op) { return new Average_OperatorWrapper(op); }
        private static AverageDiscrete_OperatorWrapper Create_AverageDiscrete_OperatorWrapper(Operator op) { return new AverageDiscrete_OperatorWrapper(op); }
        private static Bundle_OperatorWrapper Create_Bundle_OperatorWrapper(Operator op) { return new Bundle_OperatorWrapper(op); }
        private static Cache_OperatorWrapper Create_Cache_OperatorWrapper(Operator op) { return new Cache_OperatorWrapper(op); }
        private static ChangeTrigger_OperatorWrapper Create_ChangeTrigger_OperatorWrapper(Operator op) { return new ChangeTrigger_OperatorWrapper(op); }
        private static Delay_OperatorWrapper Create_Delay_OperatorWrapper(Operator op) { return new Delay_OperatorWrapper(op); }
        private static Divide_OperatorWrapper Create_Divide_OperatorWrapper(Operator op) { return new Divide_OperatorWrapper(op); }
        private static Earlier_OperatorWrapper Create_Earlier_OperatorWrapper(Operator op) { return new Earlier_OperatorWrapper(op); }
        private static Equal_OperatorWrapper Create_Equal_OperatorWrapper(Operator op) { return new Equal_OperatorWrapper(op); }
        private static Exponent_OperatorWrapper Create_Exponent_OperatorWrapper(Operator op) { return new Exponent_OperatorWrapper(op); }
        private static Filter_OperatorWrapper Create_Filter_OperatorWrapper(Operator op) { return new Filter_OperatorWrapper(op); }
        private static GetDimension_OperatorWrapper Create_GetDimension_OperatorWrapper(Operator op) { return new GetDimension_OperatorWrapper(op); }
        private static GreaterThan_OperatorWrapper Create_GreaterThan_OperatorWrapper(Operator op) { return new GreaterThan_OperatorWrapper(op); }
        private static GreaterThanOrEqual_OperatorWrapper Create_GreaterThanOrEqual_OperatorWrapper(Operator op) { return new GreaterThanOrEqual_OperatorWrapper(op); }
        private static HighPassFilter_OperatorWrapper Create_HighPassFilter_OperatorWrapper(Operator op) { return new HighPassFilter_OperatorWrapper(op); }
        private static Hold_OperatorWrapper Create_Hold_OperatorWrapper(Operator op) { return new Hold_OperatorWrapper(op); }
        private static If_OperatorWrapper Create_If_OperatorWrapper(Operator op) { return new If_OperatorWrapper(op); }
        private static LessThan_OperatorWrapper Create_LessThan_OperatorWrapper(Operator op) { return new LessThan_OperatorWrapper(op); }
        private static LessThanOrEqual_OperatorWrapper Create_LessThanOrEqual_OperatorWrapper(Operator op) { return new LessThanOrEqual_OperatorWrapper(op); }
        private static Loop_OperatorWrapper Create_Loop_OperatorWrapper(Operator op) { return new Loop_OperatorWrapper(op); }
        private static LowPassFilter_OperatorWrapper Create_LowPassFilter_OperatorWrapper(Operator op) { return new LowPassFilter_OperatorWrapper(op); }
        private static MakeContinuous_OperatorWrapper Create_MakeContinuous_OperatorWrapper(Operator op) { return new MakeContinuous_OperatorWrapper(op); }
        private static MakeDiscrete_OperatorWrapper Create_MakeDiscrete_OperatorWrapper(Operator op) { return new MakeDiscrete_OperatorWrapper(op); }
        private static MaxDiscrete_OperatorWrapper Create_MaxDiscrete_OperatorWrapper(Operator op) { return new MaxDiscrete_OperatorWrapper(op); }
        private static Maximum_OperatorWrapper Create_Maximum_OperatorWrapper(Operator op) { return new Maximum_OperatorWrapper(op); }
        private static MinDiscrete_OperatorWrapper Create_MinDiscrete_OperatorWrapper(Operator op) { return new MinDiscrete_OperatorWrapper(op); }
        private static Minimum_OperatorWrapper Create_Minimum_OperatorWrapper(Operator op) { return new Minimum_OperatorWrapper(op); }
        private static Multiply_OperatorWrapper Create_Multiply_OperatorWrapper(Operator op) { return new Multiply_OperatorWrapper(op); }
        private static Narrower_OperatorWrapper Create_Narrower_OperatorWrapper(Operator op) { return new Narrower_OperatorWrapper(op); }
        private static Negative_OperatorWrapper Create_Negative_OperatorWrapper(Operator op) { return new Negative_OperatorWrapper(op); }
        private static Noise_OperatorWrapper Create_Noise_OperatorWrapper(Operator op) { return new Noise_OperatorWrapper(op); }
        private static Not_OperatorWrapper Create_Not_OperatorWrapper(Operator op) { return new Not_OperatorWrapper(op); }
        private static NotEqual_OperatorWrapper Create_NotEqual_OperatorWrapper(Operator op) { return new NotEqual_OperatorWrapper(op); }
        private static Number_OperatorWrapper Create_Number_OperatorWrapper(Operator op) { return new Number_OperatorWrapper(op); }
        private static OneOverX_OperatorWrapper Create_OneOverX_OperatorWrapper(Operator op) { return new OneOverX_OperatorWrapper(op); }
        private static Or_OperatorWrapper Create_Or_OperatorWrapper(Operator op) { return new Or_OperatorWrapper(op); }
        private static PatchInlet_OperatorWrapper Create_PatchInlet_OperatorWrapper(Operator op) { return new PatchInlet_OperatorWrapper(op); }
        private static PatchOutlet_OperatorWrapper Create_PatchOutlet_OperatorWrapper(Operator op) { return new PatchOutlet_OperatorWrapper(op); }
        private static Power_OperatorWrapper Create_Power_OperatorWrapper(Operator op) { return new Power_OperatorWrapper(op); }
        private static Pulse_OperatorWrapper Create_Pulse_OperatorWrapper(Operator op) { return new Pulse_OperatorWrapper(op); }
        private static PulseTrigger_OperatorWrapper Create_PulseTrigger_OperatorWrapper(Operator op) { return new PulseTrigger_OperatorWrapper(op); }
        private static Random_OperatorWrapper Create_Random_OperatorWrapper(Operator op) { return new Random_OperatorWrapper(op); }
        private static Range_OperatorWrapper Create_Range_OperatorWrapper(Operator op) { return new Range_OperatorWrapper(op); }
        private static Resample_OperatorWrapper Create_Resample_OperatorWrapper(Operator op) { return new Resample_OperatorWrapper(op); }
        private static Reset_OperatorWrapper Create_Reset_OperatorWrapper(Operator op) { return new Reset_OperatorWrapper(op); }
        private static Reverse_OperatorWrapper Create_Reverse_OperatorWrapper(Operator op) { return new Reverse_OperatorWrapper(op); }
        private static Round_OperatorWrapper Create_Round_OperatorWrapper(Operator op) { return new Round_OperatorWrapper(op); }
        private static SawDown_OperatorWrapper Create_SawDown_OperatorWrapper(Operator op) { return new SawDown_OperatorWrapper(op); }
        private static SawUp_OperatorWrapper Create_SawUp_OperatorWrapper(Operator op) { return new SawUp_OperatorWrapper(op); }
        private static Scaler_OperatorWrapper Create_Scaler_OperatorWrapper(Operator op) { return new Scaler_OperatorWrapper(op); }
        private static Select_OperatorWrapper Create_Select_OperatorWrapper(Operator op) { return new Select_OperatorWrapper(op); }
        private static SetDimension_OperatorWrapper Create_SetDimension_OperatorWrapper(Operator op) { return new SetDimension_OperatorWrapper(op); }
        private static Shift_OperatorWrapper Create_Shift_OperatorWrapper(Operator op) { return new Shift_OperatorWrapper(op); }
        private static Sine_OperatorWrapper Create_Sine_OperatorWrapper(Operator op) { return new Sine_OperatorWrapper(op); }
        private static SlowDown_OperatorWrapper Create_SlowDown_OperatorWrapper(Operator op) { return new SlowDown_OperatorWrapper(op); }
        private static Spectrum_OperatorWrapper Create_Spectrum_OperatorWrapper(Operator op) { return new Spectrum_OperatorWrapper(op); }
        private static SpeedUp_OperatorWrapper Create_SpeedUp_OperatorWrapper(Operator op) { return new SpeedUp_OperatorWrapper(op); }
        private static Square_OperatorWrapper Create_Square_OperatorWrapper(Operator op) { return new Square_OperatorWrapper(op); }
        private static Stretch_OperatorWrapper Create_Stretch_OperatorWrapper(Operator op) { return new Stretch_OperatorWrapper(op); }
        private static Subtract_OperatorWrapper Create_Subtract_OperatorWrapper(Operator op) { return new Subtract_OperatorWrapper(op); }
        private static TimePower_OperatorWrapper Create_TimePower_OperatorWrapper(Operator op) { return new TimePower_OperatorWrapper(op); }
        private static ToggleTrigger_OperatorWrapper Create_ToggleTrigger_OperatorWrapper(Operator op) { return new ToggleTrigger_OperatorWrapper(op); }
        private static Triangle_OperatorWrapper Create_Triangle_OperatorWrapper(Operator op) { return new Triangle_OperatorWrapper(op); }
        private static Unbundle_OperatorWrapper Create_Unbundle_OperatorWrapper(Operator op) { return new Unbundle_OperatorWrapper(op); }
    }
}