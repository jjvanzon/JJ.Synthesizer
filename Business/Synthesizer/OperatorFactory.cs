using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Validation.Operators;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Framework.IO;

namespace JJ.Business.Synthesizer
{
    public class OperatorFactory
    {
        private readonly Patch _patch;
        private readonly RepositoryWrapper _repositories;
        private readonly PatchManager _patchManager;
        private readonly SampleManager _sampleManager;
        private readonly DocumentManager _documentManager;

        public OperatorFactory(Patch patch, RepositoryWrapper repositories)
        {
            _patch = patch ?? throw new NullException(() => patch);
            _repositories = repositories ?? throw new NullException(() => repositories);

            _documentManager = new DocumentManager(_repositories);
            _patchManager = new PatchManager(_repositories);
            _sampleManager = new SampleManager(new SampleRepositories(repositories));
        }

        public OperatorWrapper Absolute(Outlet number = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public OperatorWrapper Add(params Outlet[] items)
        {
            return Add((IList<Outlet>)items);
        }

        public OperatorWrapper Add(IList<Outlet> items)
        {
            return NewWithItemInlets(items);
        }

        public OperatorWrapper AllPassFilter(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;

            return wrapper;
        }

        public OperatorWrapper And(Outlet a = null, Outlet b = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper AverageFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewForAggregateFollower(
                signal,
                sliceLength,
                sampleCount,
                standardDimension,
                customDimension);
        }

        public OperatorWrapper_WithCollectionRecalculation AverageOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return NewForAggregateOverDimension(
                signal,
                from,
                till,
                step,
                standardDimension,
                customDimension,
                collectionRecalculation);
        }

        public OperatorWrapper AverageOverInlets(params Outlet[] operands)
        {
            return AverageOverInlets((IList<Outlet>)operands);
        }

        public OperatorWrapper AverageOverInlets(IList<Outlet> items)
        {
            return NewWithItemInlets(items);
        }

        public OperatorWrapper BandPassFilterConstantPeakGain(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;

            return wrapper;
        }

        public OperatorWrapper BandPassFilterConstantTransitionGain(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;

            return wrapper;
        }

        public Cache_OperatorWrapper Cache(
            Outlet signal = null,
            Outlet start = null,
            Outlet end = null,
            Outlet samplingRate = null,
            InterpolationTypeEnum interpolationTypeEnum = InterpolationTypeEnum.Line,
            SpeakerSetupEnum speakerSetupEnum = SpeakerSetupEnum.Mono,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            Operator op = NewWithDimension(standardDimension, customDimension);

            var wrapper = new Cache_OperatorWrapper(op);
            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.Start] = start;
            wrapper.Inputs[DimensionEnum.End] = end;
            wrapper.Inputs[DimensionEnum.SamplingRate] = samplingRate;
            wrapper.InterpolationType = interpolationTypeEnum;
            wrapper.SpeakerSetup = speakerSetupEnum;

            return wrapper;
        }

        public OperatorWrapper ChangeTrigger(Outlet passThrough = null, Outlet reset = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.PassThrough] = passThrough;
            wrapper.Inputs[DimensionEnum.Reset] = reset;

            return wrapper;
        }

        public OperatorWrapper_WithCollectionRecalculation ClosestOverDimension(
            Outlet input = null,
            Outlet collection = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return NewForClosestOverDimension_OrClosestOverDimensionExp(
                input,
                collection,
                from,
                till,
                step,
                standardDimension,
                customDimension,
                collectionRecalculation);
        }

        public OperatorWrapper_WithCollectionRecalculation ClosestOverDimensionExp(
            Outlet input = null,
            Outlet collection = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return NewForClosestOverDimension_OrClosestOverDimensionExp(
                input,
                collection,
                from,
                till,
                step,
                standardDimension,
                customDimension,
                collectionRecalculation);
        }

        public OperatorWrapper ClosestOverInlets(Outlet input, params Outlet[] items)
        {
            return ClosestOverInlets(input, (IList<Outlet>)items);
        }

        public OperatorWrapper ClosestOverInlets(Outlet input, IList<Outlet> items)
        {
            return NewWithInputAndItemsInlets(input, items);
        }

        public OperatorWrapper ClosestOverInletsExp(Outlet input, params Outlet[] items)
        {
            return ClosestOverInletsExp(input, (IList<Outlet>)items);
        }

        public OperatorWrapper ClosestOverInletsExp(Outlet input, IList<Outlet> items)
        {
            return NewWithInputAndItemsInlets(input, items);
        }

        public Curve_OperatorWrapper Curve(
            Curve curve = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            Operator op = NewWithDimension(standardDimension, customDimension);

            var wrapper = new Curve_OperatorWrapper(op, _repositories.CurveRepository)
            {
                CurveID = curve?.ID
            };

            return wrapper;
        }

        public OperatorWrapper DimensionToOutlets(Outlet signal, DimensionEnum standardDimension, string customDimension, int outletCount)
            => DimensionToOutletsPrivate(signal, standardDimension, customDimension, outletCount);

        public OperatorWrapper DimensionToOutlets(Outlet signal, DimensionEnum standardDimension, string customDimension)
            => DimensionToOutletsPrivate(signal, standardDimension, customDimension, null);

        public OperatorWrapper DimensionToOutlets(Outlet signal, DimensionEnum standardDimension, int outletCount)
            => DimensionToOutletsPrivate(signal, standardDimension, null, outletCount);

        public OperatorWrapper DimensionToOutlets(Outlet signal, DimensionEnum standardDimension)
            => DimensionToOutletsPrivate(signal, standardDimension, null, null);

        public OperatorWrapper DimensionToOutlets(Outlet signal, string customDimension, int outletCount)
            => DimensionToOutletsPrivate(signal, null, customDimension, outletCount);

        public OperatorWrapper DimensionToOutlets(Outlet signal, string customDimension)
            => DimensionToOutletsPrivate(signal, null, customDimension, null);

        public OperatorWrapper DimensionToOutlets(Outlet signal, int outletCount)
            => DimensionToOutletsPrivate(signal, null, null, outletCount);

        public OperatorWrapper DimensionToOutlets(Outlet signal)
            => DimensionToOutletsPrivate(signal, null, null, null);

        public OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, string customDimension, int outletCount)
            => DimensionToOutletsPrivate(null, standardDimension, customDimension, outletCount);

        public OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, string customDimension)
            => DimensionToOutletsPrivate(null, standardDimension, customDimension, null);

        public OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, int outletCount)
            => DimensionToOutletsPrivate(null, standardDimension, null, outletCount);

        public OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension)
            => DimensionToOutletsPrivate(null, standardDimension, null, null);

        public OperatorWrapper DimensionToOutlets(int outletCount, string customDimension)
            => DimensionToOutletsPrivate(null, null, customDimension, outletCount);

        public OperatorWrapper DimensionToOutlets(string customDimension)
            => DimensionToOutletsPrivate(null, null, customDimension, null);

        public OperatorWrapper DimensionToOutlets(int outletCount)
            => DimensionToOutletsPrivate(null, null, null, outletCount);

        public OperatorWrapper DimensionToOutlets()
            => DimensionToOutletsPrivate(null, null, null, null);

        private OperatorWrapper DimensionToOutletsPrivate(
            Outlet signal,
            DimensionEnum? standardDimension,
            string customDimension,
            int? outletCount)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);
            Operator op = wrapper.WrappedOperator;

            // OutletCount
            VoidResult setOutletCountResult = _patchManager.SetOperatorOutletCount(op, outletCount ?? 1);
            setOutletCountResult.Assert();

            wrapper.Inputs[DimensionEnum.Signal] = signal;

            return wrapper;
        }

        public OperatorWrapper Divide(Outlet a = null, Outlet b = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper DivideWithOrigin(Outlet a = null, Outlet b = null, Outlet origin = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;
            wrapper.Inputs[DimensionEnum.Origin] = origin;

            return wrapper;
        }

        public OperatorWrapper Equal(Outlet a = null, Outlet b = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Low] = low;
            wrapper.Inputs[DimensionEnum.High] = high;
            wrapper.Inputs[DimensionEnum.Ratio] = ratio;

            return wrapper;
        }

        public OperatorWrapper GetPosition(DimensionEnum? standardDimension = null, string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);
            return wrapper;
        }

        public OperatorWrapper GreaterThan(Outlet a = null, Outlet b = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper GreaterThanOrEqual(Outlet a = null, Outlet b = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper HighPassFilter(
            Outlet sound = null, 
            Outlet minFrequency = null,
            Outlet blobVolume = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = minFrequency;
            wrapper.Inputs[DimensionEnum.BlobVolume] = blobVolume;

            return wrapper;
        }

        public OperatorWrapper HighShelfFilter(
            Outlet sound = null,
            Outlet transitionFrequency = null,
            Outlet transitionSlope = null,
            Outlet dbGain = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = transitionFrequency;
            wrapper.Inputs[DimensionEnum.Slope] = transitionSlope;
            wrapper.Inputs[DimensionEnum.Decibel] = dbGain;

            return wrapper;
        }

        public OperatorWrapper Hold(Outlet signal = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Signal] = signal;

            return wrapper;
        }

        public OperatorWrapper If(Outlet condition = null, Outlet then = null, Outlet @else = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Condition] = condition;
            wrapper.Inputs[DimensionEnum.Then] = then;
            wrapper.Inputs[DimensionEnum.Else] = @else;

            return wrapper;
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(ResampleInterpolationTypeEnum interpolation, DimensionEnum standardDimension, params Outlet[] items)
        {
            return InletsToDimensionPrivate(items, interpolation, standardDimension);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(ResampleInterpolationTypeEnum interpolation, params Outlet[] items)
        {
            return InletsToDimensionPrivate(items, interpolation, null);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(DimensionEnum standardDimension, params Outlet[] items)
        {
            return InletsToDimensionPrivate(items, null, standardDimension);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(params Outlet[] items)
        {
            return InletsToDimensionPrivate(items, null, null);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> items, ResampleInterpolationTypeEnum interpolation, DimensionEnum standardDimension)
        {
            return InletsToDimensionPrivate(items, interpolation, standardDimension);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> items, ResampleInterpolationTypeEnum interpolation)
        {
            return InletsToDimensionPrivate(items, interpolation, null);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> items, DimensionEnum standardDimension)
        {
            return InletsToDimensionPrivate(items, null, standardDimension);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> items)
        {
            return InletsToDimensionPrivate(items, null, null);
        }

        private InletsToDimension_OperatorWrapper InletsToDimensionPrivate(
            IList<Outlet> items, 
            ResampleInterpolationTypeEnum? interpolation, 
            DimensionEnum? standardDimension,
            string customDimension = null)
        {
            if (items == null) throw new NullException(() => items);

            Operator op = NewWithDimension(standardDimension, customDimension);

            var wrapper = new InletsToDimension_OperatorWrapper(op)
            {
                // Interpolation
                InterpolationType = interpolation ?? ResampleInterpolationTypeEnum.Stripe
            };
            
            // Items
            wrapper.Inputs.SetMany(DimensionEnum.Item, items);

            return wrapper;
        }

        public Interpolate_OperatorWrapper Interpolate(
            Outlet signal = null,
            Outlet samplingRate = null,
            ResampleInterpolationTypeEnum interpolationType = ResampleInterpolationTypeEnum.CubicSmoothSlope,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            Operator op = NewWithDimension(standardDimension, customDimension);

            var wrapper = new Interpolate_OperatorWrapper(op);
            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.SamplingRate] = samplingRate;
            wrapper.InterpolationType = interpolationType;

            return wrapper;
        }

        public OperatorWrapper LessThan(Outlet a = null, Outlet b = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper LessThanOrEqual(Outlet a = null, Outlet b = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper Loop(
            Outlet signal = null,
            Outlet skip = null,
            Outlet loopStartMarker = null,
            Outlet loopEndMarker = null,
            Outlet releaseEndMarker = null,
            Outlet noteDuration = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);

            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.Skip] = skip;
            wrapper.Inputs[DimensionEnum.LoopStartMarker] = loopStartMarker;
            wrapper.Inputs[DimensionEnum.LoopEndMarker] = loopEndMarker;
            wrapper.Inputs[DimensionEnum.ReleaseEndMarker] = releaseEndMarker;
            wrapper.Inputs[DimensionEnum.NoteDuration] = noteDuration;

            return wrapper;
        }

        public OperatorWrapper LowPassFilter(
            Outlet sound = null,
            Outlet maxFrequency = null,
            Outlet blobVolume = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = maxFrequency;
            wrapper.Inputs[DimensionEnum.BlobVolume] = blobVolume;

            return wrapper;
        }

        public OperatorWrapper LowShelfFilter(
            Outlet sound = null,
            Outlet transitionFrequency = null,
            Outlet transitionSlope = null,
            Outlet dbGain = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = transitionFrequency;
            wrapper.Inputs[DimensionEnum.Slope] = transitionSlope;
            wrapper.Inputs[DimensionEnum.Decibel] = dbGain;

            return wrapper;
        }

        public OperatorWrapper MaxOverInlets(params Outlet[] items)
        {
            return MaxOverInlets((IList<Outlet>)items);
        }

        public OperatorWrapper MaxOverInlets(IList<Outlet> items)
        {
            return NewWithItemInlets(items);
        }

        public OperatorWrapper MaxFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewForAggregateFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);
        }

        public OperatorWrapper_WithCollectionRecalculation MaxOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return NewForAggregateOverDimension(
                signal,
                from,
                till,
                step,
                standardDimension,
                customDimension,
                collectionRecalculation);
        }

        public OperatorWrapper MinOverInlets(params Outlet[] items)
        {
            return MinOverInlets((IList<Outlet>)items);
        }

        public OperatorWrapper MinOverInlets(IList<Outlet> items)
        {
            return NewWithItemInlets(items);
        }

        public OperatorWrapper MinFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewForAggregateFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);
        }

        public OperatorWrapper_WithCollectionRecalculation MinOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return NewForAggregateOverDimension(
                signal,
                from,
                till,
                step,
                standardDimension,
                customDimension,
                collectionRecalculation);
        }

        public OperatorWrapper Multiply(params Outlet[] items)
        {
            return Multiply((IList<Outlet>)items);
        }

        public OperatorWrapper Multiply(IList<Outlet> items)
        {
            return NewWithItemInlets(items);
        }

        public OperatorWrapper MultiplyWithOrigin(Outlet a = null, Outlet b = null, Outlet origin = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;
            wrapper.Inputs[DimensionEnum.Origin] = origin;

            return wrapper;
        }

        public OperatorWrapper Negative(Outlet number = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public OperatorWrapper Noise(
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);
            return wrapper;
        }

        public OperatorWrapper Not(Outlet number = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public OperatorWrapper NotchFilter(
            Outlet sound = null, 
            Outlet centerFrequency = null, 
            Outlet width = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;

            return wrapper;
        }

        public OperatorWrapper NotEqual(Outlet a = null, Outlet b = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public Number_OperatorWrapper Number(double number = 0)
        {
            Operator op = NewBase();

            var wrapper = new Number_OperatorWrapper(op) { Number = number };

            return wrapper;
        }

        public OperatorWrapper OneOverX(Outlet number = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public OperatorWrapper Or(Outlet a = null, Outlet b = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public PatchInletOrOutlet_OperatorWrapper PatchInlet()
        {
            Operator op = NewBase();

            new Operator_SideEffect_GeneratePatchInletPosition(op).Execute();

            // Call save to execute side-effects and robust validation.
            _patchManager.SaveOperator(op).Assert();

            return new PatchInletOrOutlet_OperatorWrapper(op);
        }

        public PatchInletOrOutlet_OperatorWrapper PatchInlet(DimensionEnum dimension)
        {
            PatchInletOrOutlet_OperatorWrapper wrapper = PatchInlet();

            wrapper.Inlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);

            return wrapper;
        }

        public PatchInletOrOutlet_OperatorWrapper PatchInlet(string name)
        {
            PatchInletOrOutlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Inlet.Name = name;

            return wrapper;
        }

        public PatchInletOrOutlet_OperatorWrapper PatchInlet(string name, double defaultValue)
        {
            PatchInletOrOutlet_OperatorWrapper wrapper = PatchInlet();

            wrapper.Inlet.Name = name;
            wrapper.Inlet.DefaultValue = defaultValue;

            return wrapper;
        }

        public PatchInletOrOutlet_OperatorWrapper PatchInlet(DimensionEnum dimension, double defaultValue)
        {
            PatchInletOrOutlet_OperatorWrapper wrapper = PatchInlet();

            wrapper.Inlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);
            wrapper.Inlet.DefaultValue = defaultValue;

            return wrapper;
        }

        public PatchInletOrOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
        {
            Operator op = NewBase();

            var wrapper = new PatchInletOrOutlet_OperatorWrapper(op) { Input = input };

            new Operator_SideEffect_GeneratePatchOutletPosition(op).Execute();

            // Call save to execute side-effects and robust validation.
            _patchManager.SaveOperator(op).Assert();

            return wrapper;
        }

        public PatchInletOrOutlet_OperatorWrapper PatchOutlet(DimensionEnum dimension, Outlet input = null)
        {
            PatchInletOrOutlet_OperatorWrapper wrapper = PatchOutlet(input);

            wrapper.Outlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);

            return wrapper;
        }

        public PatchInletOrOutlet_OperatorWrapper PatchOutlet(string name, Outlet input = null)
        {
            PatchInletOrOutlet_OperatorWrapper wrapper = PatchOutlet(input);

            wrapper.Outlet.Name = name;

            return wrapper;
        }

        public OperatorWrapper PeakingEQFilter(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null,
            Outlet dbGain = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;
            wrapper.Inputs[DimensionEnum.Decibel] = dbGain;

            return wrapper;
        }

        public OperatorWrapper Power(Outlet @base = null, Outlet exponent = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Base] = @base;
            wrapper.Inputs[DimensionEnum.Exponent] = exponent;

            return wrapper;
        }

        public OperatorWrapper Pulse(
            Outlet frequency = null, 
            Outlet width = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);

            wrapper.Inputs[DimensionEnum.Frequency] = frequency;
            wrapper.Inputs[DimensionEnum.Width] = width;

            return wrapper;
        }

        public OperatorWrapper PulseTrigger(Outlet passThrough = null, Outlet reset = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.PassThrough]= passThrough;
            wrapper.Inputs[DimensionEnum.Reset] = reset;

            return wrapper;
        }

        public Random_OperatorWrapper Random(
            Outlet rate = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            Operator op = NewWithDimension(standardDimension, customDimension);

            var wrapper = new Random_OperatorWrapper(op);
            wrapper.Inputs[DimensionEnum.Rate] = rate;
            wrapper.InterpolationType = ResampleInterpolationTypeEnum.Stripe;

            return wrapper;
        }

        public OperatorWrapper RangeOverDimension(
            Outlet from = null, 
            Outlet till = null, 
            Outlet step = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);

            wrapper.Inputs[DimensionEnum.From] = from;
            wrapper.Inputs[DimensionEnum.Till] = till;
            wrapper.Inputs[DimensionEnum.Step] = step;

            return wrapper;
        }

        public OperatorWrapper RangeOverOutlets(
            Outlet from = null,
            Outlet step = null,
            int? outletCount = null)
        {
            OperatorWrapper wrapper = NewBase();
            Operator op = wrapper.WrappedOperator;

            if (outletCount.HasValue)
            {
                VoidResult setOutletCountResult = _patchManager.SetOperatorOutletCount(op, outletCount.Value);
                setOutletCountResult.Assert();
            }

            wrapper.Inputs[DimensionEnum.From] = from;
            wrapper.Inputs[DimensionEnum.Step] = step;

            return wrapper;
        }

        public Reset_OperatorWrapper Reset(Outlet passThrough = null, int? position = null)
        {
            Operator op = NewBase();

            var wrapper = new Reset_OperatorWrapper(op);
            wrapper.Inputs[DimensionEnum.PassThrough] = passThrough;
            wrapper.Position = position;

            return wrapper;
        }

        public OperatorWrapper Reverse(
            Outlet signal = null, 
            Outlet factor = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);

            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.Factor] = factor;

            return wrapper;
        }

        public OperatorWrapper Round(Outlet signal = null, Outlet step = null, Outlet offset = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.Step] = step;
            wrapper.Inputs[DimensionEnum.Offset] = offset;

            return wrapper;
        }

        public OperatorWrapper Sample(
            Stream stream, 
            Outlet frequency = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null,
            AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Undefined)
        {
            byte[] bytes = StreamHelper.StreamToBytes(stream);

            OperatorWrapper wrapper = Sample(bytes, frequency, standardDimension, customDimension, audioFileFormatEnum);

            return wrapper;
        }

        public OperatorWrapper Sample(
            byte[] bytes,
            Outlet frequency = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null,
            AudioFileFormatEnum audioFileFormatEnum = AudioFileFormatEnum.Undefined)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);

            SampleInfo sampleInfo = _sampleManager.CreateSample(bytes, audioFileFormatEnum);
            Sample sample = sampleInfo.Sample;
            wrapper.WrappedOperator.LinkTo(sample);

            wrapper.Inputs[DimensionEnum.Frequency] = frequency;

            return wrapper;
        }

        public OperatorWrapper SawDown(
            Outlet frequency = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewWithFrequency(frequency, standardDimension, customDimension);
        }

        public OperatorWrapper SawUp(
            Outlet frequency = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewWithFrequency(frequency, standardDimension, customDimension);
        }

        public Scaler_OperatorWrapper Scaler(
            Outlet signal = null,
            Outlet sourceValueA = null,
            Outlet sourceValueB = null,
            Outlet targetValueA = null,
            Outlet targetValueB = null)
        {
            Operator op = NewBase();

            var wrapper = new Scaler_OperatorWrapper(op)
            {
                SignalInput = signal,
                SourceValueA = sourceValueA,
                SourceValueB = sourceValueB,
                TargetValueA = targetValueA,
                TargetValueB = targetValueB,
            };

            return wrapper;
        }

        public OperatorWrapper SetDimension(
            Outlet passThrough = null, 
            Outlet number = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);

            wrapper.Inputs[DimensionEnum.PassThrough] = passThrough;
            wrapper.Inputs[DimensionEnum.Inherit] = number;

            return wrapper;
        }

        public OperatorWrapper Shift(
            Outlet signal = null,
            Outlet difference = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);

            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.Difference] = difference;

            return wrapper;
        }

        public OperatorWrapper Sine(
            Outlet frequency = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewWithFrequency(frequency, standardDimension, customDimension);
        }

        public OperatorWrapper_WithCollectionRecalculation SortOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return NewForAggregateOverDimension(
                signal,
                from,
                till,
                step,
                standardDimension,
                customDimension,
                collectionRecalculation);
        }

        public OperatorWrapper SortOverInlets(params Outlet[] items)
        {
            return SortOverInlets((IList<Outlet>)items);
        }

        public OperatorWrapper SortOverInlets(IList<Outlet> items)
        {
            return NewWithItemInlets(items);
        }

        public OperatorWrapper Spectrum(
            Outlet sound = null, 
            Outlet start = null, 
            Outlet end = null, 
            Outlet frequencyCount = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);

            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Start] = start;
            wrapper.Inputs[DimensionEnum.End] = end;
            wrapper.Inputs[DimensionEnum.FrequencyCount] = frequencyCount;

            return wrapper;
        }

        public OperatorWrapper Square(
            Outlet frequency = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewWithFrequency(frequency, standardDimension, customDimension);
        }

        public OperatorWrapper Squash(
            Outlet signal = null,
            Outlet factor = null,
            Outlet origin = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);

            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.Factor] = factor;
            wrapper.Inputs[DimensionEnum.Origin] = origin;

            return wrapper;
        }

        public OperatorWrapper Stretch(
            Outlet signal = null, 
            Outlet factor = null, 
            Outlet origin = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);

            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.Factor] = factor;
            wrapper.Inputs[DimensionEnum.Origin] = origin;

            return wrapper;
        }

        public OperatorWrapper Subtract(Outlet a = null, Outlet b = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper SumFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewForAggregateFollower(signal, sliceLength, sampleCount, standardDimension, customDimension);
        }

        public OperatorWrapper_WithCollectionRecalculation SumOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            return NewForAggregateOverDimension(
                signal,
                from,
                till,
                step,
                standardDimension,
                customDimension,
                collectionRecalculation);
        }

        public OperatorWrapper TimePower(
            Outlet signal = null, 
            Outlet exponent = null, 
            Outlet origin = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension);

            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.Exponent] = exponent;
            wrapper.Inputs[DimensionEnum.Origin] = origin;

            return wrapper;
        }

        public OperatorWrapper ToggleTrigger(Outlet passThrough = null, Outlet reset = null)
        {
            OperatorWrapper wrapper = NewBase();

            wrapper.Inputs[DimensionEnum.PassThrough] = passThrough;
            wrapper.Inputs[DimensionEnum.Reset] = reset;

            return wrapper;
        }

        public OperatorWrapper Triangle(Outlet frequency = null, DimensionEnum? standardDimension = null, string customDimension = null)
        {
            return NewWithFrequency(frequency, standardDimension, customDimension);
        }

        // Helpers

        /// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
        private OperatorWrapper NewWithFrequency(
            Outlet frequency,
            DimensionEnum? standardDimension,
            string customDimension,
            [CallerMemberName] string systemPatchName = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension, systemPatchName);

            wrapper.Inputs[DimensionEnum.Frequency] = frequency;

            return wrapper;
        }

        /// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
        private OperatorWrapper NewWithDimension(
            DimensionEnum? standardDimension,
            string customDimension,
            [CallerMemberName] string systemPatchName = null)
        {
            OperatorWrapper wrapper = NewBase(systemPatchName);
            Operator op = wrapper.WrappedOperator;

            if (standardDimension.HasValue)
            {
                op.SetStandardDimensionEnum(standardDimension.Value, _repositories.DimensionRepository);
            }

            op.CustomDimensionName = customDimension;

            return wrapper;
        }

        /// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
        private OperatorWrapper NewWithItemInlets(IList<Outlet> items, [CallerMemberName] string systemPatchName = null)
        {
            if (items == null) throw new NullException(() => items);

            Operator op = NewBase(systemPatchName);

            VoidResult setInletCountResult = _patchManager.SetOperatorInletCount(op, items.Count);
            setInletCountResult.Assert();

            var wrapper = new OperatorWrapper(op);
            wrapper.Inputs.SetMany(DimensionEnum.Item, items);

            return wrapper;
        }

        /// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
        private OperatorWrapper NewWithInputAndItemsInlets(
            Outlet input,
            IList<Outlet> items,
            [CallerMemberName] string systemPatchName = null)
        {
            if (items == null) throw new NullException(() => items);

            Operator op = NewBase(systemPatchName);

            VoidResult setInletCountResult = _patchManager.SetOperatorInletCount(op, items.Count + 1);
            setInletCountResult.Assert();

            var wrapper = new OperatorWrapper(op);
            wrapper.Inputs[DimensionEnum.Input] = input;
            wrapper.Inputs.SetMany(DimensionEnum.Item, items);

            return wrapper;
        }

        /// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
        private OperatorWrapper NewForAggregateFollower(
            Outlet signal,
            Outlet sliceLength,
            Outlet sampleCount,
            DimensionEnum? standardDimension,
            string customDimension,
            [CallerMemberName] string systemPatchName = null)
        {
            OperatorWrapper wrapper = NewWithDimension(standardDimension, customDimension, systemPatchName);

            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.SliceLength] = sliceLength;
            wrapper.Inputs[DimensionEnum.SampleCount] = sampleCount;

            return wrapper;
        }

        /// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
        private OperatorWrapper_WithCollectionRecalculation NewForAggregateOverDimension(
            Outlet signal,
            Outlet from,
            Outlet till,
            Outlet step,
            DimensionEnum? standardDimension,
            string customDimension,
            CollectionRecalculationEnum collectionRecalculation,
            [CallerMemberName] string systemPatchName = null)
        {
            Operator op = NewWithDimension(standardDimension, customDimension, systemPatchName);

            var wrapper = new OperatorWrapper_WithCollectionRecalculation(op);
            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.From] = from;
            wrapper.Inputs[DimensionEnum.Till] = till;
            wrapper.Inputs[DimensionEnum.Step] = step;
            wrapper.CollectionRecalculation = collectionRecalculation;

            return wrapper;
        }

        /// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
        private OperatorWrapper_WithCollectionRecalculation NewForClosestOverDimension_OrClosestOverDimensionExp(
            Outlet input,
            Outlet collection,
            Outlet from,
            Outlet till,
            Outlet step,
            DimensionEnum? standardDimension,
            string customDimension,
            CollectionRecalculationEnum collectionRecalculation,
            [CallerMemberName] string systemPatchName = null)
        {
            Operator op = NewWithDimension( standardDimension, customDimension, systemPatchName);

            var wrapper = new OperatorWrapper_WithCollectionRecalculation(op);
            wrapper.Inputs[DimensionEnum.Input] = input;
            wrapper.Inputs[DimensionEnum.Collection] = collection;
            wrapper.Inputs[DimensionEnum.From] = from;
            wrapper.Inputs[DimensionEnum.Till] = till;
            wrapper.Inputs[DimensionEnum.Step] = step;
            wrapper.CollectionRecalculation = collectionRecalculation;

            return wrapper;
        }

        // Generic methods for operator creation

        public OperatorWrapper New(string systemPatchName, int variableInletOrOutletCount = 16)
        {
            Patch patch = _documentManager.GetSystemPatch(systemPatchName);
            return New(patch, variableInletOrOutletCount);
        }

        public OperatorWrapper New(Patch underlyingPatch, params Outlet[] operands)
        {
            return New(underlyingPatch, (IList<Outlet>)operands);
        }

        public OperatorWrapper New(Patch underlyingPatch, IList<Outlet> operands)
        {
            if (underlyingPatch == null) throw new NullException(() => underlyingPatch);
            if (operands == null) throw new NullException(() => operands);

            OperatorWrapper wrapper = New(underlyingPatch);
            Operator op = wrapper.WrappedOperator;

            if (op.Inlets.Count != operands.Count)
            {
                throw new NotEqualException(() => op.Inlets.Count, () => operands.Count);
            }

            IList<Inlet> sortedInlets = op.Inlets.Sort().ToArray();

            for (int i = 0; i < operands.Count; i++)
            {
                Inlet inlet = sortedInlets[i];
                Outlet operand = operands[i];

                inlet.LinkTo(operand);
            }

            return wrapper;
        }

        /// <summary>
        /// Called from the DocumentTree view's New action.
        /// Does not support creating a Sample operator.
        /// </summary>
        public OperatorWrapper New(Patch underlyingPatch, int variableInletOrOutletCount = 16)
        {
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Absolute))) return Absolute();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Add))) return Add(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(AllPassFilter))) return AllPassFilter();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(And))) return And();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(AverageFollower))) return AverageFollower();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(AverageOverDimension))) return AverageOverDimension();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(AverageOverInlets))) return AverageOverInlets(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(BandPassFilterConstantPeakGain))) return BandPassFilterConstantPeakGain();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(BandPassFilterConstantTransitionGain))) return BandPassFilterConstantTransitionGain();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Cache))) return Cache();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(ChangeTrigger))) return ChangeTrigger();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(ClosestOverDimension))) return ClosestOverDimension();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(ClosestOverDimensionExp))) return ClosestOverDimensionExp();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(ClosestOverInlets))) return ClosestOverInlets(null, new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(ClosestOverInletsExp))) return ClosestOverInletsExp(null, new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Curve))) return Curve();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(DimensionToOutlets))) return DimensionToOutlets(null, variableInletOrOutletCount);
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Divide))) return Divide();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Equal))) return Equal();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Exponent))) return Exponent();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(GetPosition))) return GetPosition();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(GreaterThan))) return GreaterThan();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(GreaterThanOrEqual))) return GreaterThanOrEqual();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(HighPassFilter))) return HighPassFilter();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(HighShelfFilter))) return HighShelfFilter();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Hold))) return Hold();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(If))) return If();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(InletsToDimension))) return InletsToDimension(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Interpolate))) return Interpolate();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(LessThan))) return LessThan();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(LessThanOrEqual))) return LessThanOrEqual();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Loop))) return Loop();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(LowPassFilter))) return LowPassFilter();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(LowShelfFilter))) return LowShelfFilter();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(MaxFollower))) return MaxFollower();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(MaxOverDimension))) return MaxOverDimension();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(MaxOverInlets))) return MaxOverInlets(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(MinFollower))) return MinFollower();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(MinOverDimension))) return MinOverDimension();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(MinOverInlets))) return MinOverInlets(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Multiply))) return Multiply(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Negative))) return Negative();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Noise))) return Noise();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Not))) return Not();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(NotchFilter))) return NotchFilter();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(NotEqual))) return NotEqual();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Number))) return Number();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Or))) return Or();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(PatchInlet))) return PatchInlet();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(PatchOutlet))) return PatchOutlet();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(PeakingEQFilter))) return PeakingEQFilter();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Power))) return Power();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Pulse))) return Pulse();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(PulseTrigger))) return PulseTrigger();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Random))) return Random();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(RangeOverDimension))) return RangeOverDimension();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(RangeOverOutlets))) return RangeOverOutlets(outletCount: variableInletOrOutletCount);
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Reset))) return Reset();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Reverse))) return Reverse();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Round))) return Round();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SawDown))) return SawDown();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SawUp))) return SawUp();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Scaler))) return Scaler();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SetDimension))) return SetDimension();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Shift))) return Shift();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Sine))) return Sine();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SortOverDimension))) return SortOverDimension();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SortOverInlets))) return SortOverInlets(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Spectrum))) return Spectrum();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Square))) return Square();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Squash))) return Squash();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Stretch))) return Stretch();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Subtract))) return Subtract();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SumFollower))) return SumFollower();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(SumOverDimension))) return SumOverDimension();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(TimePower))) return TimePower();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(ToggleTrigger))) return ToggleTrigger();
            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Triangle))) return Triangle();

            if (NameHelper.AreEqual(underlyingPatch.Name, nameof(Sample)))
            {
                throw new NotSupportedException(
                    $"{nameof(Sample)} operator cannot be created with the generic {nameof(New)} method, " +
                    $"because it needs a byte array or Stream. Call the {nameof(Sample)} method instead.");
            }
            OperatorWrapper op = NewBase(underlyingPatch);
            return op;
        }

        /// <param name="systemPatchName">If not provided, falls back to the method name of the caller.</param>
        private OperatorWrapper NewBase([CallerMemberName] string systemPatchName = null)
        {
            Patch patch = _documentManager.GetSystemPatch(systemPatchName);

            return NewBase(patch);
        }

        private OperatorWrapper NewBase(Patch underlyingPatch)
        {
            var op = new Operator { ID = _repositories.IDRepository.GetID() };
            _repositories.OperatorRepository.Insert(op);
            op.LinkTo(_patch);

            op.UnderlyingPatch = underlyingPatch;

            new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories).Execute();

            new OperatorValidator_Basic(op).Assert();

            var wrapper = new OperatorWrapper(op);
            return wrapper;
        }
    }
}
