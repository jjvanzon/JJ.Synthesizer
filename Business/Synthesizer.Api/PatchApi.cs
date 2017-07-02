using System.Collections.Generic;
using JJ.Business.Synthesizer.Api.Helpers;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Calculation;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Api
{
    public class PatchApi
    {
        private readonly PatchManager _patchManager;
        private readonly OperatorFactory _operatorFactory;

        public PatchApi()
        {
            _patchManager = new PatchManager(RepositoryHelper.Repositories);
            Patch patch = _patchManager.CreatePatch();
            _operatorFactory = new OperatorFactory(patch, RepositoryHelper.Repositories);
        }

        public OperatorWrapper_WithUnderlyingPatch Absolute(Outlet number = null)
            => _operatorFactory.Absolute(number);

        public OperatorWrapper_WithUnderlyingPatch Add(params Outlet[] items) 
            => _operatorFactory.Add(items);

        public OperatorWrapper_WithUnderlyingPatch Add(IList<Outlet> items)
            => _operatorFactory.Add(items);

        public OperatorWrapper_WithUnderlyingPatch And(Outlet a = null, Outlet b = null)
            => _operatorFactory.And(a, b);

        public AllPassFilter_OperatorWrapper AllPassFilter(
            Outlet sound = null, 
            Outlet centerFrequency = null, 
            Outlet width = null)
            => _operatorFactory.AllPassFilter(sound, centerFrequency, width);

        public AverageFollower_OperatorWrapper AverageFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.AverageFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);

        public AverageOverDimension_OperatorWrapper AverageOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.AverageOverDimension(signal, from, till, step, standardDimension, customDimension, collectionRecalculation);

        public AverageOverInlets_OperatorWrapper AverageOverInlets(params Outlet[] items)
            => _operatorFactory.AverageOverInlets(items);

        public AverageOverInlets_OperatorWrapper AverageOverInlets(IList<Outlet> items)
            => _operatorFactory.AverageOverInlets(items);

        public BandPassFilterConstantPeakGain_OperatorWrapper BandPassFilterConstantPeakGain(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null)
            => _operatorFactory.BandPassFilterConstantPeakGain(sound, centerFrequency, width);

        public BandPassFilterConstantTransitionGain_OperatorWrapper BandPassFilterConstantTransitionGain(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null)
            => _operatorFactory.BandPassFilterConstantTransitionGain(sound, centerFrequency, width);

        public Cache_OperatorWrapper Cache(
            Outlet signal = null,
            Outlet start = null,
            Outlet end = null,
            Outlet samplingRate = null,
            InterpolationTypeEnum interpolationType = InterpolationTypeEnum.Line,
            SpeakerSetupEnum speakerSetup = SpeakerSetupEnum.Mono,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Cache(
                signal, 
                start, 
                end, 
                samplingRate, 
                interpolationType, 
                speakerSetup, 
                standardDimension,
                customDimension);

        public ChangeTrigger_OperatorWrapper ChangeTrigger(Outlet passThrough, Outlet reset)
            => _operatorFactory.ChangeTrigger(passThrough, reset);

        public ClosestOverInlets_OperatorWrapper ClosestOverInlets(Outlet input, params Outlet[] items)
            => _operatorFactory.ClosestOverInlets(input, items);

        public ClosestOverInlets_OperatorWrapper ClosestOverInlets(Outlet input, IList<Outlet> items)
            => _operatorFactory.ClosestOverInlets(input, items);

        public ClosestOverInletsExp_OperatorWrapper ClosestExpOverInlets(Outlet input, params Outlet[] items)
            => _operatorFactory.ClosestOverInletsExp(input, items);

        public ClosestOverInletsExp_OperatorWrapper ClosestExpOverInlets(Outlet input, IList<Outlet> items)
            => _operatorFactory.ClosestOverInletsExp(input, items);

        public ClosestOverDimension_OperatorWrapper ClosestOverDimension(
            Outlet input = null,
            Outlet collection = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.ClosestOverDimension(input, from, till, step, collection, standardDimension, customDimension, collectionRecalculation);

        public ClosestOverDimensionExp_OperatorWrapper ClosestOverDimensionExp(
            Outlet input = null,
            Outlet collection = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.ClosestOverDimensionExp(input, from, till, step, collection, standardDimension, customDimension, collectionRecalculation);

        public Curve_OperatorWrapper Curve(Curve curve = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
            => _operatorFactory.Curve(curve, standardDimension, customDimension);

        public OperatorWrapper_WithUnderlyingPatch CustomOperator()
            => _operatorFactory.CustomOperator();

        public OperatorWrapper_WithUnderlyingPatch CustomOperator(Patch underlyingPatch)
            => _operatorFactory.CustomOperator(underlyingPatch);

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public OperatorWrapper_WithUnderlyingPatch CustomOperator(Patch underlyingPatch, params Outlet[] operands)
            => _operatorFactory.CustomOperator(underlyingPatch, operands);

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public OperatorWrapper_WithUnderlyingPatch CustomOperator(Patch underlyingPatch, IList<Outlet> operands)
            => _operatorFactory.CustomOperator(underlyingPatch, operands);

        public OperatorWrapper_WithUnderlyingPatch Divide(Outlet a = null, Outlet b = null)
            => _operatorFactory.Divide(a, b);

        public OperatorWrapper_WithUnderlyingPatch Equal(Outlet a = null, Outlet b = null)
            => _operatorFactory.Equal(a, b);

        public Exponent_OperatorWrapper Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
            => _operatorFactory.Exponent(low, high, ratio);

        public GetDimension_OperatorWrapper GetDimension(DimensionEnum standardDimension = DimensionEnum.Undefined, string customDimension = null)
            => _operatorFactory.GetDimension(standardDimension, customDimension);

        public OperatorWrapper_WithUnderlyingPatch GreaterThan(Outlet a = null, Outlet b = null)
            => _operatorFactory.GreaterThan(a, b);

        public OperatorWrapper_WithUnderlyingPatch GreaterThanOrEqual(Outlet a = null, Outlet b = null)
            => _operatorFactory.GreaterThanOrEqual(a, b);

        public HighPassFilter_OperatorWrapper HighPassFilter(
            Outlet sound = null, 
            Outlet minFrequency = null,
            Outlet blobVolume = null)
            => _operatorFactory.HighPassFilter(sound, minFrequency, blobVolume);

        public HighShelfFilter_OperatorWrapper HighShelfFilter(
            Outlet sound = null,
            Outlet transitionFrequency = null,
            Outlet transitionSlope = null,
            Outlet dbGain = null)
            => _operatorFactory.HighShelfFilter(sound, transitionFrequency, transitionSlope, dbGain);

        public If_OperatorWrapper If(Outlet condition = null, Outlet then = null, Outlet @else = null)
            => _operatorFactory.If(condition, then, @else);

        public OperatorWrapper_WithUnderlyingPatch LessThan(Outlet a = null, Outlet b = null)
            => _operatorFactory.LessThan(a, b);

        public OperatorWrapper_WithUnderlyingPatch LessThanOrEqual(Outlet a = null, Outlet b = null)
            => _operatorFactory.LessThanOrEqual(a, b);

        public Loop_OperatorWrapper Loop(
            Outlet signal = null,
            Outlet skip = null,
            Outlet loopStartMarker = null,
            Outlet loopEndMarker = null,
            Outlet releaseEndMarker = null,
            Outlet noteDuration = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Loop(signal, skip, loopStartMarker, loopEndMarker, releaseEndMarker, noteDuration, standardDimension, customDimension);

        public LowPassFilter_OperatorWrapper LowPassFilter(
            Outlet sound = null, 
            Outlet maxFrequency = null,
            Outlet width = null)
            => _operatorFactory.LowPassFilter(sound, maxFrequency, width);

        public LowShelfFilter_OperatorWrapper LowShelfFilter(
            Outlet sound = null,
            Outlet shelfFrequency = null,
            Outlet shelfSlope = null,
            Outlet dbGain = null)
            => _operatorFactory.LowShelfFilter(sound, shelfFrequency, shelfSlope, dbGain);

        public InletsToDimension_OperatorWrapper InletsToDimension(ResampleInterpolationTypeEnum interpolation, DimensionEnum standardDimension, params Outlet[] operands)
            => _operatorFactory.InletsToDimension(interpolation, standardDimension, operands);

        public InletsToDimension_OperatorWrapper InletsToDimension(ResampleInterpolationTypeEnum interpolation, params Outlet[] operands)
            => _operatorFactory.InletsToDimension(interpolation, operands);

        public InletsToDimension_OperatorWrapper InletsToDimension(DimensionEnum standardDimension, params Outlet[] operands)
            => _operatorFactory.InletsToDimension(standardDimension, operands);

        public InletsToDimension_OperatorWrapper InletsToDimension(params Outlet[] operands)
            => _operatorFactory.InletsToDimension(operands);

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> operands, ResampleInterpolationTypeEnum interpolation, DimensionEnum standardDimension)
            => _operatorFactory.InletsToDimension(operands, interpolation, standardDimension);

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> operands, ResampleInterpolationTypeEnum interpolation)
            => _operatorFactory.InletsToDimension(operands, interpolation);

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> operands, DimensionEnum standardDimension)
            => _operatorFactory.InletsToDimension(operands, standardDimension);

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> operands)
            => _operatorFactory.InletsToDimension(operands);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension, string customDimension, int outletCount)
            => _operatorFactory.DimensionToOutlets(operand, standardDimension, customDimension, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension, string customDimension)
            => _operatorFactory.DimensionToOutlets(operand, standardDimension, customDimension);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension, int outletCount)
            => _operatorFactory.DimensionToOutlets(operand, standardDimension, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension)
            => _operatorFactory.DimensionToOutlets(operand, standardDimension);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, string customDimension, int outletCount)
            => _operatorFactory.DimensionToOutlets(operand, customDimension, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, string customDimension)
            => _operatorFactory.DimensionToOutlets(operand, customDimension);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, int outletCount)
            => _operatorFactory.DimensionToOutlets(operand, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand)
            => _operatorFactory.DimensionToOutlets(operand);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, string customDimension, int outletCount)
            => _operatorFactory.DimensionToOutlets(standardDimension, customDimension, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, string customDimension)
            => _operatorFactory.DimensionToOutlets(standardDimension, customDimension);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, int outletCount)
            => _operatorFactory.DimensionToOutlets(standardDimension, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension)
            => _operatorFactory.DimensionToOutlets(standardDimension);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(int outletCount, string customDimension)
            => _operatorFactory.DimensionToOutlets(outletCount, customDimension);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(string customDimension)
            => _operatorFactory.DimensionToOutlets(customDimension);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(int outletCount)
            => _operatorFactory.DimensionToOutlets(outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets()
            => _operatorFactory.DimensionToOutlets();

        public MaxOverInlets_OperatorWrapper MaxOverInlets(params Outlet[] operands)
            => _operatorFactory.MaxOverInlets(operands);

        public MaxOverInlets_OperatorWrapper MaxOverInlets(IList<Outlet> operands)
            => _operatorFactory.MaxOverInlets(operands);

        public MaxFollower_OperatorWrapper MaxFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.MaxFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);

        public MaxOverDimension_OperatorWrapper MaxOverDimension(
            Outlet signal = null,
            Outlet from = null, 
            Outlet till = null, 
            Outlet step = null, 
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.MaxOverDimension(signal, from, till, step, standardDimension, customDimension, collectionRecalculation);

        public MinOverInlets_OperatorWrapper MinOverInlets(params Outlet[] operands)
            => _operatorFactory.MinOverInlets(operands);

        public MinOverInlets_OperatorWrapper MinOverInlets(IList<Outlet> operands)
            => _operatorFactory.MinOverInlets(operands);

        public MinFollower_OperatorWrapper MinFollower(
            Outlet signal = null, 
            Outlet sliceLength = null, 
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.MinFollower(signal, sliceLength, sampleCount, standardDimension);

        public MinOverDimension_OperatorWrapper MinOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.MinOverDimension(signal, from, till, step, standardDimension, customDimension, collectionRecalculation);

        public OperatorWrapper_WithUnderlyingPatch Multiply(params Outlet[] operands)
            => _operatorFactory.Multiply(operands);

        public OperatorWrapper_WithUnderlyingPatch Multiply(IList<Outlet> operands)
            => _operatorFactory.Multiply(operands);

        public OperatorWrapper_WithUnderlyingPatch MultiplyWithOrigin(Outlet a = null, Outlet b = null, Outlet origin = null)
            => _operatorFactory.MultiplyWithOrigin(a, b, origin);

        public Squash_OperatorWrapper Squash(
            Outlet signal = null, 
            Outlet factor = null, 
            Outlet origin = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Squash(signal, factor, origin, standardDimension, customDimension);

        public OperatorWrapper_WithUnderlyingPatch Negative(Outlet number = null)
            => _operatorFactory.Negative(number);

        public Noise_OperatorWrapper Noise(DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
            => _operatorFactory.Noise(standardDimension, customDimension);

        public OperatorWrapper_WithUnderlyingPatch Not(Outlet number = null)
            => _operatorFactory.Not(number);

        public NotchFilter_OperatorWrapper NotchFilter(
            Outlet sound = null, 
            Outlet centerFrequency = null, 
            Outlet width = null)
            => _operatorFactory.NotchFilter(sound, centerFrequency, width);

        public OperatorWrapper_WithUnderlyingPatch NotEqual(Outlet a = null, Outlet b = null)
            => _operatorFactory.NotEqual(a, b);

        public Number_OperatorWrapper Number(double number = 0)
            => _operatorFactory.Number(number);

        public OperatorWrapper_WithUnderlyingPatch OneOverX(Outlet number = null)
            => _operatorFactory.OneOverX(number);

        public OperatorWrapper_WithUnderlyingPatch Or(Outlet a = null, Outlet b = null)
            => _operatorFactory.Or(a, b);

        public PatchInlet_OperatorWrapper PatchInlet()
            => _operatorFactory.PatchInlet();

        public PatchInlet_OperatorWrapper PatchInlet(DimensionEnum dimension)
            => _operatorFactory.PatchInlet(dimension);

        public PatchInlet_OperatorWrapper PatchInlet(string name)
            => _operatorFactory.PatchInlet(name);

        public PatchInlet_OperatorWrapper PatchInlet(string name, double defaultValue)
            => _operatorFactory.PatchInlet(name, defaultValue);

        public PatchInlet_OperatorWrapper PatchInlet(DimensionEnum dimension, double defaultValue)
            => _operatorFactory.PatchInlet(dimension, defaultValue);

        public PatchOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
            => _operatorFactory.PatchOutlet(input);

        public PatchOutlet_OperatorWrapper PatchOutlet(DimensionEnum dimension, Outlet input = null)
            => _operatorFactory.PatchOutlet(dimension, input);

        public PatchOutlet_OperatorWrapper PatchOutlet(string name, Outlet input = null)
            => _operatorFactory.PatchOutlet(name, input);

        public PeakingEQFilter_OperatorWrapper PeakingEQFilter(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null,
            Outlet dbGain = null)
            => _operatorFactory.PeakingEQFilter(sound, centerFrequency, width, dbGain);

        public OperatorWrapper_WithUnderlyingPatch Power(Outlet @base = null, Outlet exponent = null)
            => _operatorFactory.Power(@base, exponent);

        public Pulse_OperatorWrapper Pulse(
            Outlet frequency = null, 
            Outlet width = null, 
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Pulse(frequency, width, standardDimension, customDimension);

        public PulseTrigger_OperatorWrapper PulseTrigger(Outlet calculation, Outlet reset)
            => _operatorFactory.PulseTrigger(calculation, reset);

        public Random_OperatorWrapper Random(Outlet rate = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
            => _operatorFactory.Random(rate, standardDimension, customDimension);

        public RangeOverDimension_OperatorWrapper RangeOverDimension(
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null)
            => _operatorFactory.RangeOverDimension(from, till, step, standardDimension, customDimension);

        public RangeOverOutlets_OperatorWrapper RangeOverOutlets(
            Outlet from = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            int? outletCount = null)
            => _operatorFactory.RangeOverOutlets(from, step, standardDimension, customDimension, outletCount);

        public Interpolate_OperatorWrapper Interpolate(
            Outlet signal = null,
            Outlet samplingRate = null,
            ResampleInterpolationTypeEnum interpolationType = ResampleInterpolationTypeEnum.CubicSmoothSlope,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Interpolate(signal, samplingRate, interpolationType, standardDimension, customDimension);

        public Reset_OperatorWrapper Reset(Outlet passThrough = null, int? position = null)
            => _operatorFactory.Reset(passThrough, position);

        public Reverse_OperatorWrapper Reverse(
            Outlet signal = null, 
            Outlet factor = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Reverse(signal, factor, standardDimension, customDimension);

        public Round_OperatorWrapper Round(Outlet signal = null, Outlet step = null, Outlet offset = null)
            => _operatorFactory.Round(signal, step, offset);

        public Sample_OperatorWrapper Sample(Sample sample = null, Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
            => _operatorFactory.Sample(sample, frequency, standardDimension, customDimension);

        public SawDown_OperatorWrapper SawDown(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
            => _operatorFactory.SawDown(frequency, standardDimension, customDimension);

        public SawUp_OperatorWrapper SawUp(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
            => _operatorFactory.SawUp(frequency, standardDimension, customDimension);

        public Scaler_OperatorWrapper Scaler(
            Outlet signal = null,
            Outlet sourceValueA = null,
            Outlet sourceValueB = null,
            Outlet targetValueA = null,
            Outlet targetValueB = null)
            => _operatorFactory.Scaler(signal, sourceValueA, sourceValueB, targetValueA, targetValueB);

        public SetDimension_OperatorWrapper SetDimension(
            Outlet calculation = null, 
            Outlet number = null, 
            DimensionEnum standardDimension = DimensionEnum.Undefined, 
            string customDimension = null)
            => _operatorFactory.SetDimension(calculation, number, standardDimension, customDimension);

        public Shift_OperatorWrapper Shift(
            Outlet signal = null, 
            Outlet difference = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Shift(signal, difference, standardDimension, customDimension);

        public OperatorWrapper_WithUnderlyingPatch Sine(Outlet frequency = null) => _operatorFactory.Sine(frequency);

        public SortOverInlets_OperatorWrapper SortOverInlets(params Outlet[] operands)
            => _operatorFactory.SortOverInlets(operands);

        public SortOverInlets_OperatorWrapper SortOverInlets(IList<Outlet> operands)
            => _operatorFactory.SortOverInlets(operands);

        public SortOverDimension_OperatorWrapper SortOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.SortOverDimension(signal, from, till, step, standardDimension, customDimension, collectionRecalculation);

        public Spectrum_OperatorWrapper Spectrum(
            Outlet sound = null,
            Outlet start = null,
            Outlet end = null,
            Outlet samplingRate = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Spectrum(sound, start, end, samplingRate, standardDimension, customDimension);

        public Square_OperatorWrapper Square(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
            => _operatorFactory.Square(frequency, standardDimension, customDimension);

        public Stretch_OperatorWrapper Stretch(
            Outlet signal = null, 
            Outlet factor = null, 
            Outlet origin = null, 
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Stretch(signal, factor, origin, standardDimension, customDimension);

        public OperatorWrapper_WithUnderlyingPatch Subtract(Outlet a = null, Outlet b = null)
            => _operatorFactory.Subtract(a, b);

        public SumOverDimension_OperatorWrapper SumOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.SumOverDimension(signal, from, till, step, standardDimension, customDimension, collectionRecalculation);

        public SumFollower_OperatorWrapper SumFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.SumFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);

        public TimePower_OperatorWrapper TimePower(
            Outlet signal = null, 
            Outlet exponent = null, 
            Outlet origin = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.TimePower(signal, exponent, origin, standardDimension, customDimension);

        public ToggleTrigger_OperatorWrapper ToggleTrigger(Outlet passThrough, Outlet reset)
            => _operatorFactory.ToggleTrigger(passThrough, reset);

        public Triangle_OperatorWrapper Triangle(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
            => _operatorFactory.Triangle(frequency, standardDimension, customDimension);

        public IPatchCalculator CreateCalculator(
            Outlet outlet,
            int samplingRate,
            int channelCount,
            int channelIndex,
            CalculatorCache calculatorCache)
            => _patchManager.CreateCalculator(outlet, samplingRate, channelCount, channelIndex, calculatorCache);
    }
}
