using System.Collections.Generic;
using System.IO;
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

        public OperatorWrapper Absolute(Outlet number = null)
            => _operatorFactory.Absolute(number);

        public OperatorWrapper Add(params Outlet[] items) 
            => _operatorFactory.Add(items);

        public OperatorWrapper Add(IList<Outlet> items)
            => _operatorFactory.Add(items);

        public OperatorWrapper And(Outlet a = null, Outlet b = null)
            => _operatorFactory.And(a, b);

        public OperatorWrapper AllPassFilter(
            Outlet sound = null, 
            Outlet centerFrequency = null, 
            Outlet width = null)
            => _operatorFactory.AllPassFilter(sound, centerFrequency, width);

        public OperatorWrapper AverageFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.AverageFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);

        public OperatorWrapper_WithCollectionRecalculation AverageOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.AverageOverDimension(signal, from, till, step, standardDimension, customDimension, collectionRecalculation);

        public OperatorWrapper AverageOverInlets(params Outlet[] items)
            => _operatorFactory.AverageOverInlets(items);

        public OperatorWrapper AverageOverInlets(IList<Outlet> items)
            => _operatorFactory.AverageOverInlets(items);

        public OperatorWrapper BandPassFilterConstantPeakGain(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null)
            => _operatorFactory.BandPassFilterConstantPeakGain(sound, centerFrequency, width);

        public OperatorWrapper BandPassFilterConstantTransitionGain(
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

        public OperatorWrapper ChangeTrigger(Outlet passThrough, Outlet reset)
            => _operatorFactory.ChangeTrigger(passThrough, reset);

        public OperatorWrapper ClosestOverInlets(Outlet input, params Outlet[] items)
            => _operatorFactory.ClosestOverInlets(input, items);

        public OperatorWrapper ClosestOverInlets(Outlet input, IList<Outlet> items)
            => _operatorFactory.ClosestOverInlets(input, items);

        public OperatorWrapper ClosestExpOverInlets(Outlet input, params Outlet[] items)
            => _operatorFactory.ClosestOverInletsExp(input, items);

        public OperatorWrapper ClosestExpOverInlets(Outlet input, IList<Outlet> items)
            => _operatorFactory.ClosestOverInletsExp(input, items);

        public OperatorWrapper_WithCollectionRecalculation ClosestOverDimension(
            Outlet input = null,
            Outlet collection = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.ClosestOverDimension(input, from, till, step, collection, standardDimension, customDimension, collectionRecalculation);

        public OperatorWrapper_WithCollectionRecalculation ClosestOverDimensionExp(
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

        public OperatorWrapper Divide(Outlet a = null, Outlet b = null)
            => _operatorFactory.Divide(a, b);

        public OperatorWrapper DivideWithOrigin(Outlet a = null, Outlet b = null, Outlet origin = null)
            => _operatorFactory.DivideWithOrigin(a, b, origin);

        public OperatorWrapper Equal(Outlet a = null, Outlet b = null)
            => _operatorFactory.Equal(a, b);

        public OperatorWrapper Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
            => _operatorFactory.Exponent(low, high, ratio);

        public OperatorWrapper GetPosition(DimensionEnum standardDimension = DimensionEnum.Undefined, string customDimension = null)
            => _operatorFactory.GetPosition(standardDimension, customDimension);

        public OperatorWrapper GreaterThan(Outlet a = null, Outlet b = null)
            => _operatorFactory.GreaterThan(a, b);

        public OperatorWrapper GreaterThanOrEqual(Outlet a = null, Outlet b = null)
            => _operatorFactory.GreaterThanOrEqual(a, b);

        public OperatorWrapper HighPassFilter(
            Outlet sound = null, 
            Outlet minFrequency = null,
            Outlet blobVolume = null)
            => _operatorFactory.HighPassFilter(sound, minFrequency, blobVolume);

        public OperatorWrapper HighShelfFilter(
            Outlet sound = null,
            Outlet transitionFrequency = null,
            Outlet transitionSlope = null,
            Outlet dbGain = null)
            => _operatorFactory.HighShelfFilter(sound, transitionFrequency, transitionSlope, dbGain);

        public OperatorWrapper If(Outlet condition = null, Outlet then = null, Outlet @else = null)
            => _operatorFactory.If(condition, then, @else);

        public OperatorWrapper LessThan(Outlet a = null, Outlet b = null)
            => _operatorFactory.LessThan(a, b);

        public OperatorWrapper LessThanOrEqual(Outlet a = null, Outlet b = null)
            => _operatorFactory.LessThanOrEqual(a, b);

        public OperatorWrapper Loop(
            Outlet signal = null,
            Outlet skip = null,
            Outlet loopStartMarker = null,
            Outlet loopEndMarker = null,
            Outlet releaseEndMarker = null,
            Outlet noteDuration = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Loop(signal, skip, loopStartMarker, loopEndMarker, releaseEndMarker, noteDuration, standardDimension, customDimension);

        public OperatorWrapper LowPassFilter(
            Outlet sound = null, 
            Outlet maxFrequency = null,
            Outlet width = null)
            => _operatorFactory.LowPassFilter(sound, maxFrequency, width);

        public OperatorWrapper LowShelfFilter(
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

        public OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension, string customDimension, int outletCount)
            => _operatorFactory.DimensionToOutlets(operand, standardDimension, customDimension, outletCount);

        public OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension, string customDimension)
            => _operatorFactory.DimensionToOutlets(operand, standardDimension, customDimension);

        public OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension, int outletCount)
            => _operatorFactory.DimensionToOutlets(operand, standardDimension, outletCount);

        public OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension)
            => _operatorFactory.DimensionToOutlets(operand, standardDimension);

        public OperatorWrapper DimensionToOutlets(Outlet operand, string customDimension, int outletCount)
            => _operatorFactory.DimensionToOutlets(operand, customDimension, outletCount);

        public OperatorWrapper DimensionToOutlets(Outlet operand, string customDimension)
            => _operatorFactory.DimensionToOutlets(operand, customDimension);

        public OperatorWrapper DimensionToOutlets(Outlet operand, int outletCount)
            => _operatorFactory.DimensionToOutlets(operand, outletCount);

        public OperatorWrapper DimensionToOutlets(Outlet operand)
            => _operatorFactory.DimensionToOutlets(operand);

        public OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, string customDimension, int outletCount)
            => _operatorFactory.DimensionToOutlets(standardDimension, customDimension, outletCount);

        public OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, string customDimension)
            => _operatorFactory.DimensionToOutlets(standardDimension, customDimension);

        public OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, int outletCount)
            => _operatorFactory.DimensionToOutlets(standardDimension, outletCount);

        public OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension)
            => _operatorFactory.DimensionToOutlets(standardDimension);

        public OperatorWrapper DimensionToOutlets(int outletCount, string customDimension)
            => _operatorFactory.DimensionToOutlets(outletCount, customDimension);

        public OperatorWrapper DimensionToOutlets(string customDimension)
            => _operatorFactory.DimensionToOutlets(customDimension);

        public OperatorWrapper DimensionToOutlets(int outletCount)
            => _operatorFactory.DimensionToOutlets(outletCount);

        public OperatorWrapper DimensionToOutlets()
            => _operatorFactory.DimensionToOutlets();

        public OperatorWrapper MaxOverInlets(params Outlet[] operands)
            => _operatorFactory.MaxOverInlets(operands);

        public OperatorWrapper MaxOverInlets(IList<Outlet> operands)
            => _operatorFactory.MaxOverInlets(operands);

        public OperatorWrapper MaxFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.MaxFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);

        public OperatorWrapper_WithCollectionRecalculation MaxOverDimension(
            Outlet signal = null,
            Outlet from = null, 
            Outlet till = null, 
            Outlet step = null, 
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.MaxOverDimension(signal, from, till, step, standardDimension, customDimension, collectionRecalculation);

        public OperatorWrapper MinOverInlets(params Outlet[] operands)
            => _operatorFactory.MinOverInlets(operands);

        public OperatorWrapper MinOverInlets(IList<Outlet> operands)
            => _operatorFactory.MinOverInlets(operands);

        public OperatorWrapper MinFollower(
            Outlet signal = null, 
            Outlet sliceLength = null, 
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.MinFollower(signal, sliceLength, sampleCount, standardDimension);

        public OperatorWrapper_WithCollectionRecalculation MinOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.MinOverDimension(signal, from, till, step, standardDimension, customDimension, collectionRecalculation);

        public OperatorWrapper Multiply(params Outlet[] operands)
            => _operatorFactory.Multiply(operands);

        public OperatorWrapper Multiply(IList<Outlet> operands)
            => _operatorFactory.Multiply(operands);

        public OperatorWrapper MultiplyWithOrigin(Outlet a = null, Outlet b = null, Outlet origin = null)
            => _operatorFactory.MultiplyWithOrigin(a, b, origin);

        public OperatorWrapper Squash(
            Outlet signal = null, 
            Outlet factor = null, 
            Outlet origin = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Squash(signal, factor, origin, standardDimension, customDimension);

        public OperatorWrapper Negative(Outlet number = null)
            => _operatorFactory.Negative(number);

        public OperatorWrapper Noise(DimensionEnum? standardDimension = null, string customDimension = null)
            => _operatorFactory.Noise(standardDimension, customDimension);

        public OperatorWrapper Not(Outlet number = null)
            => _operatorFactory.Not(number);

        public OperatorWrapper NotchFilter(
            Outlet sound = null, 
            Outlet centerFrequency = null, 
            Outlet width = null)
            => _operatorFactory.NotchFilter(sound, centerFrequency, width);

        public OperatorWrapper NotEqual(Outlet a = null, Outlet b = null)
            => _operatorFactory.NotEqual(a, b);

        public Number_OperatorWrapper Number(double number = 0)
            => _operatorFactory.Number(number);

        public OperatorWrapper OneOverX(Outlet number = null)
            => _operatorFactory.OneOverX(number);

        public OperatorWrapper Or(Outlet a = null, Outlet b = null)
            => _operatorFactory.Or(a, b);

        public PatchInletOrOutlet_OperatorWrapper PatchInlet()
            => _operatorFactory.PatchInlet();

        public PatchInletOrOutlet_OperatorWrapper PatchInlet(DimensionEnum dimension)
            => _operatorFactory.PatchInlet(dimension);

        public PatchInletOrOutlet_OperatorWrapper PatchInlet(string name)
            => _operatorFactory.PatchInlet(name);

        public PatchInletOrOutlet_OperatorWrapper PatchInlet(string name, double defaultValue)
            => _operatorFactory.PatchInlet(name, defaultValue);

        public PatchInletOrOutlet_OperatorWrapper PatchInlet(DimensionEnum dimension, double defaultValue)
            => _operatorFactory.PatchInlet(dimension, defaultValue);

        public PatchInletOrOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
            => _operatorFactory.PatchOutlet(input);

        public PatchInletOrOutlet_OperatorWrapper PatchOutlet(DimensionEnum dimension, Outlet input = null)
            => _operatorFactory.PatchOutlet(dimension, input);

        public PatchInletOrOutlet_OperatorWrapper PatchOutlet(string name, Outlet input = null)
            => _operatorFactory.PatchOutlet(name, input);

        public OperatorWrapper PeakingEQFilter(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null,
            Outlet dbGain = null)
            => _operatorFactory.PeakingEQFilter(sound, centerFrequency, width, dbGain);

        public OperatorWrapper Power(Outlet @base = null, Outlet exponent = null)
            => _operatorFactory.Power(@base, exponent);

        public OperatorWrapper Pulse(
            Outlet frequency = null, 
            Outlet width = null, 
            DimensionEnum? standardDimension = null,
            string customDimension = null)
            => _operatorFactory.Pulse(frequency, width, standardDimension, customDimension);

        public OperatorWrapper PulseTrigger(Outlet calculation, Outlet reset)
            => _operatorFactory.PulseTrigger(calculation, reset);

        public Random_OperatorWrapper Random(Outlet rate = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
            => _operatorFactory.Random(rate, standardDimension, customDimension);

        public OperatorWrapper RangeOverDimension(
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null)
            => _operatorFactory.RangeOverDimension(from, till, step, standardDimension, customDimension);

        public OperatorWrapper RangeOverOutlets(
            Outlet from = null,
            Outlet step = null,
            int? outletCount = null)
            => _operatorFactory.RangeOverOutlets(from, step, outletCount);

        public Interpolate_OperatorWrapper Interpolate(
            Outlet signal = null,
            Outlet samplingRate = null,
            ResampleInterpolationTypeEnum interpolationType = ResampleInterpolationTypeEnum.CubicSmoothSlope,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Interpolate(signal, samplingRate, interpolationType, standardDimension, customDimension);

        public Reset_OperatorWrapper Reset(Outlet passThrough = null, int? position = null)
            => _operatorFactory.Reset(passThrough, position);

        public OperatorWrapper Reverse(
            Outlet signal = null, 
            Outlet factor = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Reverse(signal, factor, standardDimension, customDimension);

        public OperatorWrapper Round(Outlet signal = null, Outlet step = null, Outlet offset = null)
            => _operatorFactory.Round(signal, step, offset);

        public OperatorWrapper Sample(Stream stream , Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null, AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Undefined)
            => _operatorFactory.Sample(stream, frequency, standardDimension, customDimension, audioFileFormatEnum);

        public OperatorWrapper Sample(byte[] bytes, Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null, AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Undefined)
            => _operatorFactory.Sample(bytes, frequency, standardDimension, customDimension, audioFileFormatEnum);

        public OperatorWrapper SawDown(Outlet frequency = null, DimensionEnum? standardDimension = null, string customDimension = null)
            => _operatorFactory.SawDown(frequency, standardDimension, customDimension);

        public OperatorWrapper SawUp(Outlet frequency = null, DimensionEnum? standardDimension = null, string customDimension = null)
            => _operatorFactory.SawUp(frequency, standardDimension, customDimension);

        public Scaler_OperatorWrapper Scaler(
            Outlet signal = null,
            Outlet sourceValueA = null,
            Outlet sourceValueB = null,
            Outlet targetValueA = null,
            Outlet targetValueB = null)
            => _operatorFactory.Scaler(signal, sourceValueA, sourceValueB, targetValueA, targetValueB);

        public OperatorWrapper SetDimension(
            Outlet calculation = null, 
            Outlet number = null, 
            DimensionEnum standardDimension = DimensionEnum.Undefined, 
            string customDimension = null)
            => _operatorFactory.SetDimension(calculation, number, standardDimension, customDimension);

        public OperatorWrapper Shift(
            Outlet signal = null, 
            Outlet difference = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Shift(signal, difference, standardDimension, customDimension);

        public OperatorWrapper Sine(
            Outlet frequency = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null) => _operatorFactory.Sine(frequency, standardDimension, customDimension);

        public OperatorWrapper SortOverInlets(params Outlet[] operands)
            => _operatorFactory.SortOverInlets(operands);

        public OperatorWrapper SortOverInlets(IList<Outlet> operands)
            => _operatorFactory.SortOverInlets(operands);

        public OperatorWrapper_WithCollectionRecalculation SortOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.SortOverDimension(signal, from, till, step, standardDimension, customDimension, collectionRecalculation);

        public OperatorWrapper Spectrum(
            Outlet sound = null,
            Outlet start = null,
            Outlet end = null,
            Outlet samplingRate = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Spectrum(sound, start, end, samplingRate, standardDimension, customDimension);

        public OperatorWrapper Square(Outlet frequency = null, DimensionEnum? standardDimension = null, string customDimension = null)
            => _operatorFactory.Square(frequency, standardDimension, customDimension);

        public OperatorWrapper Stretch(
            Outlet signal = null, 
            Outlet factor = null, 
            Outlet origin = null, 
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.Stretch(signal, factor, origin, standardDimension, customDimension);

        public OperatorWrapper Subtract(Outlet a = null, Outlet b = null)
            => _operatorFactory.Subtract(a, b);

        public OperatorWrapper_WithCollectionRecalculation SumOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
            => _operatorFactory.SumOverDimension(signal, from, till, step, standardDimension, customDimension, collectionRecalculation);

        public OperatorWrapper SumFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.SumFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);

        public OperatorWrapper TimePower(
            Outlet signal = null, 
            Outlet exponent = null, 
            Outlet origin = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
            => _operatorFactory.TimePower(signal, exponent, origin, standardDimension, customDimension);

        public OperatorWrapper ToggleTrigger(Outlet passThrough, Outlet reset)
            => _operatorFactory.ToggleTrigger(passThrough, reset);

        public OperatorWrapper Triangle(Outlet frequency = null, DimensionEnum? standardDimension = null, string customDimension = null)
            => _operatorFactory.Triangle(frequency, standardDimension, customDimension);

        public OperatorWrapper New(string systemPatchName, int variableInletOrOutletCount = 16)
            => _operatorFactory.New(systemPatchName, variableInletOrOutletCount);

        public OperatorWrapper New(Patch underlyingPatch, params Outlet[] operands)
            => _operatorFactory.New(underlyingPatch, operands);

        public OperatorWrapper New(Patch underlyingPatch, IList<Outlet> operands)
            => _operatorFactory.New(underlyingPatch, operands);

        public OperatorWrapper New(Patch underlyingPatch, int variableInletOrOutletCount = 16)
            => _operatorFactory.New(underlyingPatch, variableInletOrOutletCount);

        public IPatchCalculator CreateCalculator(
            Outlet outlet,
            int samplingRate,
            int channelCount,
            int channelIndex,
            CalculatorCache calculatorCache)
            => _patchManager.CreateCalculator(outlet, samplingRate, channelCount, channelIndex, calculatorCache);
    }
}
