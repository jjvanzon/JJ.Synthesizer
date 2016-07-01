using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Api.Helpers;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Calculation;

namespace JJ.Business.Synthesizer.Api
{
    public class PatchApi
    {
        private readonly PatchManager _patchManager;

        public Patch Patch
        {
            get { return _patchManager.Patch; }
        }

        public PatchApi()
        {
            _patchManager = new PatchManager(RepositoryHelper.PatchRepositories);
            _patchManager.CreatePatch();
        }

        public Absolute_OperatorWrapper Absolute(Outlet x = null)
        {
            return _patchManager.Absolute(x);
        }

        public Add_OperatorWrapper Add(params Outlet[] operands)
        {
            return _patchManager.Add(operands);
        }

        public Add_OperatorWrapper Add(IList<Outlet> operands)
        {
            return _patchManager.Add(operands);
        }

        public And_OperatorWrapper And(Outlet a = null, Outlet b = null)
        {
            return _patchManager.And(a, b);
        }

        public Average_OperatorWrapper Average(params Outlet[] operands)
        {
            return _patchManager.Average(operands);
        }

        public Average_OperatorWrapper Average(IList<Outlet> operands)
        {
            return _patchManager.Average(operands);
        }

        public AverageOverDimension_OperatorWrapper AverageOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum dimension = DimensionEnum.Undefined,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return _patchManager.AverageOverDimension(signal, from, till, step, dimension, collectionRecalculation);
        }

        public AverageFollower_OperatorWrapper AverageFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.AverageFollower(signal, sliceLength, sampleCount, dimension);
        }

        public Bundle_OperatorWrapper Bundle(params Outlet[] operands)
        {
            return _patchManager.Bundle(operands);
        }

        public Bundle_OperatorWrapper Bundle(DimensionEnum dimension, params Outlet[] operands)
        {
            return _patchManager.Bundle(operands, dimension);
        }

        public Bundle_OperatorWrapper Bundle(IList<Outlet> operands, DimensionEnum dimension = DimensionEnum.Undefined)
        {
            return _patchManager.Bundle(operands, dimension);
        }

        public Cache_OperatorWrapper Cache(
            Outlet signal = null,
            Outlet startTime = null,
            Outlet endTime = null,
            Outlet samplingRate = null,
            InterpolationTypeEnum interpolationType = InterpolationTypeEnum.Line,
            SpeakerSetupEnum speakerSetup = SpeakerSetupEnum.Mono,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Cache(
                signal, 
                startTime, 
                endTime, 
                samplingRate, 
                interpolationType, 
                speakerSetup, 
                dimension);
        }

        public ChangeTrigger_OperatorWrapper ChangeTrigger(Outlet calculation, Outlet reset)
        {
            return _patchManager.ChangeTrigger(calculation, reset);
        }

        public Closest_OperatorWrapper Closest(Outlet input, params Outlet[] items)
        {
            return _patchManager.Closest(input, items);
        }

        public Closest_OperatorWrapper Closest(Outlet input, IList<Outlet> items)
        {
            return _patchManager.Closest(input, items);
        }

        public ClosestExp_OperatorWrapper ClosestExp(Outlet input, params Outlet[] items)
        {
            return _patchManager.ClosestExp(input, items);
        }

        public ClosestExp_OperatorWrapper ClosestExp(Outlet input, IList<Outlet> items)
        {
            return _patchManager.ClosestExp(input, items);
        }

        public ClosestOverDimension_OperatorWrapper ClosestOverDimension(
            Outlet input = null,
            Outlet collection = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum dimension = DimensionEnum.Undefined,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return _patchManager.ClosestOverDimension(input, from, till, step, collection, dimension, collectionRecalculation);
        }

        public ClosestOverDimensionExp_OperatorWrapper ClosestOverDimensionExp(
            Outlet input = null,
            Outlet collection = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum dimension = DimensionEnum.Undefined,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return _patchManager.ClosestOverDimensionExp(input, from, till, step, collection, dimension, collectionRecalculation);
        }

        public Curve_OperatorWrapper Curve(Curve curve = null, DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Curve(curve, dimension);
        }

        public CustomOperator_OperatorWrapper CustomOperator()
        {
            return _patchManager.CustomOperator();
        }

        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch)
        {
            return _patchManager.CustomOperator(underlyingPatch);
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch, params Outlet[] operands)
        {
            return _patchManager.CustomOperator(underlyingPatch, operands);
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch, IList<Outlet> operands)
        {
            return _patchManager.CustomOperator(underlyingPatch, operands);
        }

        public Delay_OperatorWrapper Delay(
            Outlet signal = null,
            Outlet timeDifference = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Delay(signal, timeDifference, dimension);
        }

        public Divide_OperatorWrapper Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            return _patchManager.Divide(numerator, denominator, origin);
        }

        public Earlier_OperatorWrapper Earlier(
            Outlet signal = null, 
            Outlet timeDifference = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Earlier(signal, timeDifference, dimension);
        }

        public Equal_OperatorWrapper Equal(Outlet a = null, Outlet b = null)
        {
            return _patchManager.Equal(a, b);
        }

        public Exponent_OperatorWrapper Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
        {
            return _patchManager.Exponent(low, high, ratio);
        }

        public Filter_OperatorWrapper Filter(
            FilterTypeEnum filterTypeEnum = FilterTypeEnum.LowPassFilter,
            Outlet signal = null,
            Outlet frequency = null,
            Outlet bandWidth = null,
            Outlet dbGain = null,
            Outlet shelfSlope = null)
        {
            return _patchManager.Filter(filterTypeEnum, signal, frequency, bandWidth, dbGain, shelfSlope);
        }

        public GetDimension_OperatorWrapper GetDimension(DimensionEnum dimension = DimensionEnum.Undefined)
        {
            return _patchManager.GetDimension(dimension);
        }

        public GreaterThan_OperatorWrapper GreaterThan(Outlet a = null, Outlet b = null)
        {
            return _patchManager.GreaterThan(a, b);
        }

        public GreaterThanOrEqual_OperatorWrapper GreaterThanOrEqual(Outlet a = null, Outlet b = null)
        {
            return _patchManager.GreaterThanOrEqual(a, b);
        }

        public HighPassFilter_OperatorWrapper HighPassFilter(Outlet signal = null, Outlet minFrequency = null)
        {
            return _patchManager.HighPassFilter(signal, minFrequency);
        }

        public If_OperatorWrapper If(Outlet condition = null, Outlet then = null, Outlet @else = null)
        {
            return _patchManager.If(condition, then, @else);
        }

        public LessThan_OperatorWrapper LessThan(Outlet a = null, Outlet b = null)
        {
            return _patchManager.LessThan(a, b);
        }

        public LessThanOrEqual_OperatorWrapper LessThanOrEqual(Outlet a = null, Outlet b = null)
        {
            return _patchManager.LessThanOrEqual(a, b);
        }

        public Loop_OperatorWrapper Loop(
            Outlet signal = null,
            Outlet skip = null,
            Outlet loopStartMarker = null,
            Outlet loopEndMarker = null,
            Outlet releaseEndMarker = null,
            Outlet noteDuration = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Loop(signal, skip, loopStartMarker, loopEndMarker, releaseEndMarker, noteDuration, dimension);
        }

        public LowPassFilter_OperatorWrapper LowPassFilter(Outlet signal = null, Outlet maxFrequency = null)
        {
            return _patchManager.LowPassFilter(signal, maxFrequency);
        }

        public MakeContinuous_OperatorWrapper MakeContinuous(ResampleInterpolationTypeEnum interpolation, DimensionEnum dimension, params Outlet[] operands)
        {
            return _patchManager.MakeContinuous(interpolation, dimension, operands);
        }

        public MakeContinuous_OperatorWrapper MakeContinuous(ResampleInterpolationTypeEnum interpolation, params Outlet[] operands)
        {
            return _patchManager.MakeContinuous(interpolation, operands);
        }

        public MakeContinuous_OperatorWrapper MakeContinuous(DimensionEnum dimension, params Outlet[] operands)
        {
            return _patchManager.MakeContinuous(dimension, operands);
        }

        public MakeContinuous_OperatorWrapper MakeContinuous(params Outlet[] operands)
        {
            return _patchManager.MakeContinuous(operands);
        }

        public MakeContinuous_OperatorWrapper MakeContinuous(IList<Outlet> operands, ResampleInterpolationTypeEnum interpolation, DimensionEnum dimension)
        {
            return _patchManager.MakeContinuous(operands, interpolation, dimension);
        }

        public MakeContinuous_OperatorWrapper MakeContinuous(IList<Outlet> operands, ResampleInterpolationTypeEnum interpolation)
        {
            return _patchManager.MakeContinuous(operands, interpolation);
        }

        public MakeContinuous_OperatorWrapper MakeContinuous(IList<Outlet> operands, DimensionEnum dimension)
        {
            return _patchManager.MakeContinuous(operands, dimension);
        }

        public MakeContinuous_OperatorWrapper MakeContinuous(IList<Outlet> operands)
        {
            return _patchManager.MakeContinuous(operands);
        }

        public MakeDiscrete_OperatorWrapper MakeDiscrete(Outlet operand, DimensionEnum dimension, int outletCount)
        {
            return _patchManager.MakeDiscrete(operand, dimension, outletCount);
        }

        public MakeDiscrete_OperatorWrapper MakeDiscrete(Outlet operand, DimensionEnum dimension)
        {
            return _patchManager.MakeDiscrete(operand, dimension);
        }

        public MakeDiscrete_OperatorWrapper MakeDiscrete(Outlet operand, int outletCount)
        {
            return _patchManager.MakeDiscrete(operand, outletCount);
        }

        public MakeDiscrete_OperatorWrapper MakeDiscrete(Outlet operand)
        {
            return _patchManager.MakeDiscrete(operand);
        }

        public MakeDiscrete_OperatorWrapper MakeDiscrete(DimensionEnum dimension, int outletCount)
        {
            return _patchManager.MakeDiscrete(dimension, outletCount);
        }

        public MakeDiscrete_OperatorWrapper MakeDiscrete(DimensionEnum dimension)
        {
            return _patchManager.MakeDiscrete(dimension);
        }

        public MakeDiscrete_OperatorWrapper MakeDiscrete(int outletCount)
        {
            return _patchManager.MakeDiscrete(outletCount);
        }

        public MakeDiscrete_OperatorWrapper MakeDiscrete()
        {
            return _patchManager.MakeDiscrete();
        }

        public Max_OperatorWrapper Max(params Outlet[] operands)
        {
            return _patchManager.Max(operands);
        }

        public Max_OperatorWrapper Max(IList<Outlet> operands)
        {
            return _patchManager.Max(operands);
        }

        public MaxFollower_OperatorWrapper MaxFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.MaxFollower(signal, sliceLength, sampleCount, dimension);
        }

        public MaxOverDimension_OperatorWrapper MaxOverDimension(
            Outlet signal = null,
            Outlet from = null, 
            Outlet till = null, 
            Outlet step = null, 
            DimensionEnum dimension = DimensionEnum.Undefined,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return _patchManager.MaxOverDimension(signal, from, till, step, dimension, collectionRecalculation);
        }

        public Min_OperatorWrapper Min(params Outlet[] operands)
        {
            return _patchManager.Min(operands);
        }

        public Min_OperatorWrapper Min(IList<Outlet> operands)
        {
            return _patchManager.Min(operands);
        }

        public MinFollower_OperatorWrapper MinFollower(
            Outlet signal = null, 
            Outlet sliceLength = null, 
            Outlet sampleCount = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.MinFollower(signal, sliceLength, sampleCount, dimension);
        }

        public MinOverDimension_OperatorWrapper MinOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum dimension = DimensionEnum.Undefined,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return _patchManager.MinOverDimension(signal, from, till, step, dimension, collectionRecalculation);
        }

        public Multiply_OperatorWrapper Multiply(params Outlet[] operands)
        {
            return _patchManager.Multiply(operands);
        }

        public Multiply_OperatorWrapper Multiply(IList<Outlet> operands)
        {
            return _patchManager.Multiply(operands);
        }

        public MultiplyWithOrigin_OperatorWrapper MultiplyWithOrigin(Outlet a = null, Outlet b = null, Outlet origin = null)
        {
            return _patchManager.MultiplyWithOrigin(a, b, origin);
        }

        public Narrower_OperatorWrapper Narrower(
            Outlet signal = null, 
            Outlet factor = null, 
            Outlet origin = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Narrower(signal, factor, origin, dimension);
        }

        public Negative_OperatorWrapper Negative(Outlet x = null)
        {
            return _patchManager.Negative(x);
        }

        public Noise_OperatorWrapper Noise(DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Noise(dimension);
        }

        public Not_OperatorWrapper Not(Outlet x = null)
        {
            return _patchManager.Not(x);
        }

        public NotEqual_OperatorWrapper NotEqual(Outlet a = null, Outlet b = null)
        {
            return _patchManager.NotEqual(a, b);
        }

        public Number_OperatorWrapper Number(double number = 0)
        {
            return _patchManager.Number(number);
        }

        public OneOverX_OperatorWrapper OneOverX(Outlet x = null)
        {
            return _patchManager.OneOverX(x);
        }

        public Or_OperatorWrapper Or(Outlet a = null, Outlet b = null)
        {
            return _patchManager.Or(a, b);
        }

        public PatchInlet_OperatorWrapper PatchInlet()
        {
            return _patchManager.PatchInlet();
        }

        public PatchInlet_OperatorWrapper PatchInlet(DimensionEnum dimension)
        {
            return _patchManager.PatchInlet(dimension);
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name)
        {
            return _patchManager.PatchInlet(name);
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name, double defaultValue)
        {
            return _patchManager.PatchInlet(name, defaultValue);
        }

        public PatchInlet_OperatorWrapper PatchInlet(DimensionEnum dimension, double defaultValue)
        {
            return _patchManager.PatchInlet(dimension, defaultValue);
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
        {
            return _patchManager.PatchOutlet(input);
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(DimensionEnum dimensionEnum, Outlet input = null)
        {
            return _patchManager.PatchOutlet(dimensionEnum, input);
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(string name, Outlet input = null)
        {
            return _patchManager.PatchOutlet(name, input);
        }

        public Power_OperatorWrapper Power(Outlet @base = null, Outlet exponent = null)
        {
            return _patchManager.Power(@base, exponent);
        }

        public Pulse_OperatorWrapper Pulse(
            Outlet frequency = null, 
            Outlet width = null, 
            Outlet phaseShift = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Pulse(frequency, width, phaseShift, dimension);
        }

        public PulseTrigger_OperatorWrapper PulseTrigger(Outlet calculation, Outlet reset)
        {
            return _patchManager.PulseTrigger(calculation, reset);
        }

        public Random_OperatorWrapper Random(
            Outlet rate = null, 
            Outlet phaseShift = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Random(rate, phaseShift, dimension);
        }

        public Resample_OperatorWrapper Resample(
            Outlet signal = null,
            Outlet samplingRate = null,
            ResampleInterpolationTypeEnum interpolationType = ResampleInterpolationTypeEnum.CubicSmoothSlope,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Resample(signal, samplingRate, interpolationType, dimension);
        }

        public Reset_OperatorWrapper Reset(Outlet operand = null, int? listIndex = null)
        {
            return _patchManager.Reset(operand, listIndex);
        }

        public Reverse_OperatorWrapper Reverse(
            Outlet signal = null, 
            Outlet speed = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Reverse(signal, speed, dimension);
        }

        public Round_OperatorWrapper Round(Outlet signal = null, Outlet step = null, Outlet offset = null)
        {
            return _patchManager.Round(signal, step, offset);
        }

        public Sample_OperatorWrapper Sample(Sample sample = null, DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Sample(sample, dimension);
        }

        public SawDown_OperatorWrapper SawDown(
            Outlet frequency = null, 
            Outlet phaseShift = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.SawDown(frequency, phaseShift, dimension);
        }

        public SawUp_OperatorWrapper SawUp(
            Outlet frequency = null, 
            Outlet phaseShift = null, 
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.SawUp(frequency, phaseShift, dimension);
        }

        public Scaler_OperatorWrapper Scaler(
            Outlet signal = null,
            Outlet sourceValueA = null,
            Outlet sourceValueB = null,
            Outlet targetValueA = null,
            Outlet targetValueB = null)
        {
            return _patchManager.Scaler(signal, sourceValueA, sourceValueB, targetValueA, targetValueB);
        }

        public Select_OperatorWrapper Select(
            Outlet signal = null, 
            Outlet position = null, 
            DimensionEnum dimension = DimensionEnum.Undefined)
        {
            return _patchManager.Select(signal, position, dimension);
        }

        public SetDimension_OperatorWrapper SetDimension(Outlet calculation = null, Outlet value = null, DimensionEnum dimension = DimensionEnum.Undefined)
        {
            return _patchManager.SetDimension(calculation, value, dimension);
        }

        public Shift_OperatorWrapper Shift(
            Outlet signal = null, 
            Outlet difference = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Shift(signal, difference, dimension);
        }

        public Sine_OperatorWrapper Sine(
            Outlet frequency = null, 
            Outlet phaseShift = null, 
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Sine(frequency, phaseShift, dimension);
        }

        public SlowDown_OperatorWrapper SlowDown(
            Outlet signal = null, 
            Outlet factor = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.SlowDown(signal, factor, dimension);
        }

        public Sort_OperatorWrapper Sort(params Outlet[] operands)
        {
            return _patchManager.Sort(operands);
        }

        public Sort_OperatorWrapper Sort(IList<Outlet> operands)
        {
            return _patchManager.Sort(operands);
        }

        public SortOverDimension_OperatorWrapper SortOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum dimension = DimensionEnum.Undefined,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return _patchManager.SortOverDimension(signal, from, till, step, dimension, collectionRecalculation);
        }

        public Spectrum_OperatorWrapper Spectrum(
            Outlet signal = null,
            Outlet startTime = null,
            Outlet endTime = null,
            Outlet samplingRate = null)
        {
            return _patchManager.Spectrum(signal, startTime, endTime, samplingRate);
        }

        public SpeedUp_OperatorWrapper SpeedUp(
            Outlet signal = null, 
            Outlet factor = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.SpeedUp(signal, factor, dimension);
        }

        public Square_OperatorWrapper Square(
            Outlet frequency = null, 
            Outlet phaseShift = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Square(frequency, phaseShift, dimension);
        }

        public Stretch_OperatorWrapper Stretch(
            Outlet signal = null, 
            Outlet factor = null, 
            Outlet origin = null, 
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Stretch(signal, factor, origin, dimension);
        }

        public Subtract_OperatorWrapper Subtract(Outlet a = null, Outlet b = null)
        {
            return _patchManager.Subtract(a, b);
        }

        public SumOverDimension_OperatorWrapper SumOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum dimension = DimensionEnum.Undefined,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return _patchManager.SumOverDimension(signal, from, till, step, dimension, collectionRecalculation);
        }

        public SumFollower_OperatorWrapper SumFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.SumFollower(signal, sliceLength, sampleCount, dimension);
        }

        public TimePower_OperatorWrapper TimePower(
            Outlet signal = null, 
            Outlet exponent = null, 
            Outlet origin = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.TimePower(signal, exponent, origin, dimension);
        }

        public ToggleTrigger_OperatorWrapper ToggleTrigger(Outlet calculation, Outlet reset)
        {
            return _patchManager.ToggleTrigger(calculation, reset);
        }

        public Triangle_OperatorWrapper Triangle(
            Outlet frequency = null, 
            Outlet phaseShift = null,
            DimensionEnum dimension = DimensionEnum.Time)
        {
            return _patchManager.Triangle(frequency, phaseShift, dimension);
        }

        public Unbundle_OperatorWrapper Unbundle(Outlet operand, DimensionEnum dimension, int outletCount)
        {
            return _patchManager.Unbundle(operand, dimension, outletCount);
        }

        public Unbundle_OperatorWrapper Unbundle(Outlet operand, DimensionEnum dimension)
        {
            return _patchManager.Unbundle(operand, dimension);
        }

        public Unbundle_OperatorWrapper Unbundle(Outlet operand, int outletCount)
        {
            return _patchManager.Unbundle(operand, outletCount);
        }

        public Unbundle_OperatorWrapper Unbundle(Outlet operand)
        {
            return _patchManager.Unbundle(operand);
        }

        public Unbundle_OperatorWrapper Unbundle(DimensionEnum dimension, int outletCount)
        {
            return _patchManager.Unbundle(dimension, outletCount);
        }

        public Unbundle_OperatorWrapper Unbundle(DimensionEnum dimension)
        {
            return _patchManager.Unbundle(dimension);
        }

        public Unbundle_OperatorWrapper Unbundle(int outletCount)
        {
            return _patchManager.Unbundle(outletCount);
        }

        public Unbundle_OperatorWrapper Unbundle()
        {
            return _patchManager.Unbundle();
        }

        public IPatchCalculator CreateCalculator(
            Outlet outlet,
            int channelCount,
            int channelIndex,
            CalculatorCache calculatorCache,
            bool mustSubstituteSineForUnfilledInSignalPatchInlets = true)
        {
            return _patchManager.CreateCalculator(outlet, channelCount, channelIndex, calculatorCache, mustSubstituteSineForUnfilledInSignalPatchInlets);
        }
    }
}
