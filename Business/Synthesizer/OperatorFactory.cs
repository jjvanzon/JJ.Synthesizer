using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Exceptions;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.Operators;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer
{
    public class OperatorFactory
    {
        private const double DEFAULT_FILTER_FREQUENCY = 1760.0;
        private const double DEFAULT_FREQUENCY = 440.0;
        private const double DEFAULT_FROM = 0.0;
        private const double DEFAULT_TILL = 15.0;
        private const double DEFAULT_FILTER_WIDTH = 1.0;
        private const double DEFAULT_BLOB_VOLUME = 1.0;
        private const double DEFAULT_DB_GAIN = 3.0;
        private const double DEFAULT_DIFFERENCE = 1.0;
        private const double DEFAULT_END_TIME = 1.0;
        private const double DEFAULT_EXPONENT = 2.0;
        private const double DEFAULT_FACTOR = 2.0;
        private const double DEFAULT_PULSE_WIDTH = 0.5;
        private const double DEFAULT_RANDOM_RATE = 16.0;
        private const double DEFAULT_RANGE_FROM = 1.0;
        private const double DEFAULT_RANGE_TILL = 16.0;
        private const double DEFAULT_REVERSE_FACTOR = 1.0;
        private const double DEFAULT_SAMPLE_COUNT = 100.0;
        private const double DEFAULT_SAMPLING_RATE = 44100.0;
        private const double DEFAULT_SCALE_SOURCE_VALUE_A = -1.0;
        private const double DEFAULT_SCALE_SOURCE_VALUE_B = 1.0;
        private const double DEFAULT_SCALE_TARGET_VALUE_A = 1.0;
        private const double DEFAULT_SCALE_TARGET_VALUE_B = 4.0;
        private const double DEFAULT_TRANSITION_SLOPE = 1.0;
        private const double DEFAULT_SLICE_LENGTH = 0.02;
        private const double DEFAULT_SPECTRUM_FREQUENCY_COUNT = 256.0;
        private const double DEFAULT_START_TIME = 0.0;
        private const double DEFAULT_STEP = 1.0;

        private readonly Patch _patch;
        private readonly RepositoryWrapper _repositories;
        private readonly PatchManager _patchManager;
        private readonly DocumentManager _documentManager;

        public OperatorFactory([NotNull] Patch patch, RepositoryWrapper repositories)
        {
            _patch = patch ?? throw new NullException(() => patch);
            _repositories = repositories ?? throw new NullException(() => repositories);

            _documentManager = new DocumentManager(_repositories);
            _patchManager = new PatchManager(_repositories);
        }

        public OperatorWrapper_WithUnderlyingPatch Absolute(Outlet number = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Add(params Outlet[] items)
        {
            return Add((IList<Outlet>)items);
        }

        public OperatorWrapper_WithUnderlyingPatch Add(IList<Outlet> items)
        {
            if (items == null) throw new NullException(() => items);

            Operator op = New(MethodBase.GetCurrentMethod());

            VoidResult setInletCountResult = _patchManager.SetOperatorInletCount(op, items.Count);
            setInletCountResult.Assert();

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs.SetMany(DimensionEnum.Item, items);

            return wrapper;
        }

        public AllPassFilter_OperatorWrapper AllPassFilter(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.AllPassFilter,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.Width },
                new[] { DimensionEnum.Sound });

            var wrapper = new AllPassFilter_OperatorWrapper(op)
            {
                SoundInput = sound,
                CenterFrequency = centerFrequency,
                Width = width
            };

            wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.WidthInlet.DefaultValue = DEFAULT_FILTER_WIDTH;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch And(Outlet a = null, Outlet b = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public AverageFollower_OperatorWrapper AverageFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.AverageFollower,
                new[] { DimensionEnum.Signal, DimensionEnum.SliceLength, DimensionEnum.SampleCount },
                new[] { DimensionEnum.Signal });

            op.CustomDimensionName = customDimension;
            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);

            var wrapper = new AverageFollower_OperatorWrapper(op)
            {
                SignalInput = signal,
                SliceLength = sliceLength,
                SampleCount = sampleCount,
            };

            wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
            wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public AverageOverDimension_OperatorWrapper AverageOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.AverageOverDimension,
                new[] { DimensionEnum.Signal, DimensionEnum.From, DimensionEnum.Till, DimensionEnum.Step },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new AverageOverDimension_OperatorWrapper(op)
            {
                SignalInput = signal,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public AverageOverInlets_OperatorWrapper AverageOverInlets(params Outlet[] operands)
        {
            return AverageOverInlets((IList<Outlet>)operands);
        }

        public AverageOverInlets_OperatorWrapper AverageOverInlets(IList<Outlet> items)
        {
            if (items == null) throw new NullException(() => items);

            Operator op = CreateBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.AverageOverInlets, items);

            new Versatile_OperatorValidator(op).Assert();

            var wrapper = new AverageOverInlets_OperatorWrapper(op);
            return wrapper;
        }

        public BandPassFilterConstantPeakGain_OperatorWrapper BandPassFilterConstantPeakGain(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.BandPassFilterConstantPeakGain,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.Width },
                new[] { DimensionEnum.Sound });

            var wrapper = new BandPassFilterConstantPeakGain_OperatorWrapper(op)
            {
                SoundInput = sound,
                CenterFrequency = centerFrequency,
                Width = width
            };

            wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.WidthInlet.DefaultValue = DEFAULT_FILTER_WIDTH;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public BandPassFilterConstantTransitionGain_OperatorWrapper BandPassFilterConstantTransitionGain(
            Outlet signal = null,
            Outlet centerFrequency = null,
            Outlet width = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.BandPassFilterConstantTransitionGain,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.Width },
                new[] { DimensionEnum.Sound });

            var wrapper = new BandPassFilterConstantTransitionGain_OperatorWrapper(op)
            {
                SoundInput = signal,
                CenterFrequency = centerFrequency,
                Width = width
            };

            wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.WidthInlet.DefaultValue = DEFAULT_FILTER_WIDTH;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Cache_OperatorWrapper Cache(
            Outlet signal = null,
            Outlet start = null,
            Outlet end = null,
            Outlet samplingRate = null,
            InterpolationTypeEnum interpolationTypeEnum = InterpolationTypeEnum.Line,
            SpeakerSetupEnum speakerSetupEnum = SpeakerSetupEnum.Mono,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Cache,
                new[] { DimensionEnum.Signal, DimensionEnum.Start, DimensionEnum.End, DimensionEnum.SamplingRate },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Cache_OperatorWrapper(op)
            {
                Signal = signal,
                Start = start,
                End = end,
                SamplingRate = samplingRate,
                InterpolationType = interpolationTypeEnum,
                SpeakerSetup = speakerSetupEnum
            };

            wrapper.StartInlet.DefaultValue = DEFAULT_START_TIME;
            wrapper.EndInlet.DefaultValue = DEFAULT_END_TIME;
            wrapper.SamplingRateInlet.DefaultValue = DEFAULT_SAMPLING_RATE;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public ChangeTrigger_OperatorWrapper ChangeTrigger(Outlet calculation = null, Outlet reset = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.ChangeTrigger,
                new[] { DimensionEnum.PassThrough, DimensionEnum.Reset },
                new[] { DimensionEnum.PassThrough });

            var wrapper = new ChangeTrigger_OperatorWrapper(op)
            {
                PassThroughInput = calculation,
                Reset = reset
            };

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public ClosestOverDimension_OperatorWrapper ClosestOverDimension(
            Outlet input = null,
            Outlet collection = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.ClosestOverDimension,
                new[] { DimensionEnum.Input, DimensionEnum.Collection, DimensionEnum.From, DimensionEnum.Till, DimensionEnum.Step },
                new[] { DimensionEnum.Number });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new ClosestOverDimension_OperatorWrapper(op)
            {
                Input = input,
                Collection = collection,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public ClosestOverDimensionExp_OperatorWrapper ClosestOverDimensionExp(
            Outlet input = null,
            Outlet collection = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.ClosestOverDimensionExp,
                new[] { DimensionEnum.Input, DimensionEnum.Collection, DimensionEnum.From, DimensionEnum.Till, DimensionEnum.Step },
                new[] { DimensionEnum.Number });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new ClosestOverDimensionExp_OperatorWrapper(op)
            {
                Input = input,
                Collection = collection,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public ClosestOverInlets_OperatorWrapper ClosestOverInlets(Outlet input, params Outlet[] items)
        {
            return ClosestOverInlets(input, (IList<Outlet>)items);
        }

        public ClosestOverInlets_OperatorWrapper ClosestOverInlets(Outlet input, IList<Outlet> items)
        {
            Operator op = CreateBase_WithVariableInletCountAndOneOutlet(
                OperatorTypeEnum.ClosestOverInlets,
                input.Concat(items).ToArray());

            op.Inlets[0].SetDimensionEnum(DimensionEnum.Input, _repositories.DimensionRepository);

            var wrapper = new ClosestOverInlets_OperatorWrapper(op);

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public ClosestOverInletsExp_OperatorWrapper ClosestOverInletsExp(Outlet input, params Outlet[] items)
        {
            return ClosestOverInletsExp(input, (IList<Outlet>)items);
        }

        public ClosestOverInletsExp_OperatorWrapper ClosestOverInletsExp(Outlet input, IList<Outlet> items)
        {
            Operator op = CreateBase_WithVariableInletCountAndOneOutlet(
                OperatorTypeEnum.ClosestOverInletsExp,
                input.Concat(items).ToArray());

            op.Inlets[0].SetDimensionEnum(DimensionEnum.Input, _repositories.DimensionRepository);

            var wrapper = new ClosestOverInletsExp_OperatorWrapper(op);

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Curve_OperatorWrapper Curve(
            Curve curve = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Curve,
                new DimensionEnum[0],
                new[] { DimensionEnum.Number });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Curve_OperatorWrapper(op, _repositories.CurveRepository)
            {
                CurveID = curve?.ID
            };

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch CustomOperator()
        {
            var op = new Operator { ID = _repositories.IDRepository.GetID() };
            op.SetOperatorTypeEnum(OperatorTypeEnum.CustomOperator, _repositories);

            _repositories.OperatorRepository.Insert(op);

            op.LinkTo(_patch);

            new Versatile_OperatorValidator(op).Assert();

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch CustomOperator(Patch underlyingPatch)
        {
            OperatorWrapper_WithUnderlyingPatch wrapper = CustomOperator();
            Operator op = wrapper;

            op.UnderlyingPatch = underlyingPatch;

            new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories).Execute();

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public OperatorWrapper_WithUnderlyingPatch CustomOperator(Patch underlyingPatch, IList<Outlet> operands)
        {
            if (underlyingPatch == null) throw new NullException(() => underlyingPatch);
            if (operands == null) throw new NullException(() => operands);

            OperatorWrapper_WithUnderlyingPatch wrapper = CustomOperator(underlyingPatch);

            SetOperands(wrapper.WrappedOperator, operands);

            wrapper.WrappedOperator.LinkTo(_patch);

            new Versatile_OperatorValidator(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public OperatorWrapper_WithUnderlyingPatch CustomOperator(Patch underlyingPatch, params Outlet[] operands)
        {
            return CustomOperator(underlyingPatch, (IList<Outlet>)operands);
        }

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension, string customDimension, int outletCount)
            => DimensionToOutletsPrivate(operand, standardDimension, customDimension, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension, string customDimension)
            => DimensionToOutletsPrivate(operand, standardDimension, customDimension, null);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension, int outletCount)
            => DimensionToOutletsPrivate(operand, standardDimension, null, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, DimensionEnum standardDimension)
            => DimensionToOutletsPrivate(operand, standardDimension, null, null);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, string customDimension, int outletCount)
            => DimensionToOutletsPrivate(operand, null, customDimension, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, string customDimension)
            => DimensionToOutletsPrivate(operand, null, customDimension, null);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand, int outletCount)
            => DimensionToOutletsPrivate(operand, null, null, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(Outlet operand)
            => DimensionToOutletsPrivate(operand, null, null, null);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, string customDimension, int outletCount)
            => DimensionToOutletsPrivate(null, standardDimension, customDimension, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, string customDimension)
            => DimensionToOutletsPrivate(null, standardDimension, customDimension, null);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension, int outletCount)
            => DimensionToOutletsPrivate(null, standardDimension, null, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(DimensionEnum standardDimension)
            => DimensionToOutletsPrivate(null, standardDimension, null, null);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(int outletCount, string customDimension)
            => DimensionToOutletsPrivate(null, null, customDimension, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(string customDimension)
            => DimensionToOutletsPrivate(null, null, customDimension, null);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets(int outletCount)
            => DimensionToOutletsPrivate(null, null, null, outletCount);

        public DimensionToOutlets_OperatorWrapper DimensionToOutlets()
            => DimensionToOutletsPrivate(null, null, null, null);

        private DimensionToOutlets_OperatorWrapper DimensionToOutletsPrivate(
            Outlet operand,
            DimensionEnum? dimension,
            string customDimension,
            int? outletCount)
        {
            dimension = dimension ?? DimensionEnum.Undefined;
            outletCount = outletCount ?? 1;

            if (outletCount < 1) throw new LessThanException(() => outletCount, 1);

            Operator op = CreateBase(
                OperatorTypeEnum.DimensionToOutlets,
                new[] { DimensionEnum.Signal },
                Enumerable.Repeat(DimensionEnum.Item, outletCount.Value).ToArray());

            op.SetStandardDimensionEnum(dimension.Value, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new DimensionToOutlets_OperatorWrapper(op)
            {
                Input = operand
            };

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Divide(Outlet a = null, Outlet b = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch DivideWithOrigin(Outlet a = null, Outlet b = null, Outlet origin = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;
            wrapper.Inputs[DimensionEnum.Origin] = origin;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Equal(Outlet a = null, Outlet b = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public Exponent_OperatorWrapper Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Exponent,
                new[] { DimensionEnum.Low, DimensionEnum.High, DimensionEnum.Ratio },
                new[] { DimensionEnum.Number });

            var wrapper = new Exponent_OperatorWrapper(op)
            {
                Low = low,
                High = high,
                Ratio = ratio
            };

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public GetDimension_OperatorWrapper GetDimension(
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.GetDimension,
                new DimensionEnum[0],
                new[] { DimensionEnum.Number });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new GetDimension_OperatorWrapper(op);

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch GreaterThan(Outlet a = null, Outlet b = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch GreaterThanOrEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public HighPassFilter_OperatorWrapper HighPassFilter(
            Outlet sound = null, 
            Outlet minFrequency = null,
            Outlet blobVolume = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.HighPassFilter,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.BlobVolume },
                new[] { DimensionEnum.Sound });

            var wrapper = new HighPassFilter_OperatorWrapper(op)
            {
                SoundInput = sound,
                MinFrequency = minFrequency,
                BlobVolume = blobVolume
            };

            wrapper.MinFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.BlobVolumeInlet.DefaultValue = DEFAULT_BLOB_VOLUME;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public HighShelfFilter_OperatorWrapper HighShelfFilter(
            Outlet sound = null,
            Outlet transitionFrequency = null,
            Outlet transitionSlope = null,
            Outlet dbGain = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.HighShelfFilter,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.Slope, DimensionEnum.Decibel },
                new[] { DimensionEnum.Sound });

            var wrapper = new HighShelfFilter_OperatorWrapper(op)
            {
                SoundInput = sound,
                TransitionFrequency = transitionFrequency,
                TransitionSlope = transitionSlope,
                DBGain = dbGain
            };

            wrapper.TransitionFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.TransitionSlopeInlet.DefaultValue = DEFAULT_TRANSITION_SLOPE;
            wrapper.DBGainInlet.DefaultValue = DEFAULT_DB_GAIN;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Hold_OperatorWrapper Hold(Outlet signal = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Hold,
                new[] { DimensionEnum.Signal },
                new[] { DimensionEnum.Number });

            var wrapper = new Hold_OperatorWrapper(op)
            {
                Signal = signal,
            };

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public If_OperatorWrapper If(Outlet condition = null, Outlet then = null, Outlet @else = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.If,
                new[] { DimensionEnum.Condition, DimensionEnum.Then, DimensionEnum.Else },
                new[] { DimensionEnum.Number });

            var wrapper = new If_OperatorWrapper(op)
            {
                Condition = condition,
                Then = then,
                Else = @else
            };

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(ResampleInterpolationTypeEnum interpolation, DimensionEnum standardDimension, params Outlet[] operands)
        {
            return InletsToDimensionPrivate(operands, interpolation, standardDimension);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(ResampleInterpolationTypeEnum interpolation, params Outlet[] operands)
        {
            return InletsToDimensionPrivate(operands, interpolation, null);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(DimensionEnum standardDimension, params Outlet[] operands)
        {
            return InletsToDimensionPrivate(operands, null, standardDimension);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(params Outlet[] operands)
        {
            return InletsToDimensionPrivate(operands, null, null);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> operands, ResampleInterpolationTypeEnum interpolation, DimensionEnum standardDimension)
        {
            return InletsToDimensionPrivate(operands, interpolation, standardDimension);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> operands, ResampleInterpolationTypeEnum interpolation)
        {
            return InletsToDimensionPrivate(operands, interpolation, null);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> operands, DimensionEnum standardDimension)
        {
            return InletsToDimensionPrivate(operands, null, standardDimension);
        }

        public InletsToDimension_OperatorWrapper InletsToDimension(IList<Outlet> operands)
        {
            return InletsToDimensionPrivate(operands, null, null);
        }

        private InletsToDimension_OperatorWrapper InletsToDimensionPrivate(
            IList<Outlet> operands, 
            ResampleInterpolationTypeEnum? interpolation, 
            DimensionEnum? dimension,
            string customDimension = null)
        {
            if (operands == null) throw new NullException(() => operands);
            interpolation = interpolation ?? ResampleInterpolationTypeEnum.Stripe;
            dimension = dimension ?? DimensionEnum.Undefined;

            Operator op = CreateBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.InletsToDimension, operands);
            op.SetStandardDimensionEnum(dimension.Value, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new InletsToDimension_OperatorWrapper(op)
            {
                InterpolationType = interpolation.Value
            };
            wrapper.SignalOutlet.SetDimensionEnum(DimensionEnum.Signal, _repositories.DimensionRepository);

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Interpolate_OperatorWrapper Interpolate(
            Outlet signal = null,
            Outlet samplingRate = null,
            ResampleInterpolationTypeEnum interpolationType = ResampleInterpolationTypeEnum.CubicSmoothSlope,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Interpolate,
                new[] { DimensionEnum.Signal, DimensionEnum.SamplingRate },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Interpolate_OperatorWrapper(op)
            {
                Signal = signal,
                SamplingRate = samplingRate,
                InterpolationType = interpolationType
            };

            wrapper.SamplingRateInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch LessThan(Outlet a = null, Outlet b = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch LessThanOrEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public Loop_OperatorWrapper Loop(
            Outlet signal = null,
            Outlet skip = null,
            Outlet loopStartMarker = null,
            Outlet loopEndMarker = null,
            Outlet releaseEndMarker = null,
            Outlet noteDuration = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Loop,
                new[]
                {
                    DimensionEnum.Signal,
                    DimensionEnum.Skip,
                    DimensionEnum.LoopStartMarker,
                    DimensionEnum.LoopEndMarker,
                    DimensionEnum.ReleaseEndMarker,
                    DimensionEnum.NoteDuration
                },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Loop_OperatorWrapper(op)
            {
                Signal = signal,
                Skip = skip,
                LoopStartMarker = loopStartMarker,
                LoopEndMarker = loopEndMarker,
                ReleaseEndMarker = releaseEndMarker,
                NoteDuration = noteDuration
            };

            wrapper.LoopStartMarkerInlet.DefaultValue = DEFAULT_START_TIME;
            wrapper.LoopEndMarkerInlet.DefaultValue = DEFAULT_END_TIME;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public LowPassFilter_OperatorWrapper LowPassFilter(
            Outlet sound = null,
            Outlet maxFrequency = null,
            Outlet blobVolume = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.LowPassFilter,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.BlobVolume },
                new[] { DimensionEnum.Sound });

            var wrapper = new LowPassFilter_OperatorWrapper(op)
            {
                SoundInput = sound,
                MaxFrequency = maxFrequency,
                BlobVolume = blobVolume
            };

            wrapper.MaxFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.BlobVolumeInlet.DefaultValue = DEFAULT_FILTER_WIDTH;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public LowShelfFilter_OperatorWrapper LowShelfFilter(
            Outlet sound = null,
            Outlet transitionFrequency = null,
            Outlet transitionSlope = null,
            Outlet dbGain = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.LowShelfFilter,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.Slope, DimensionEnum.Decibel },
                new[] { DimensionEnum.Sound });

            var wrapper = new LowShelfFilter_OperatorWrapper(op)
            {
                SoundInput = sound,
                TransitionFrequency = transitionFrequency,
                TransitionSlope = transitionSlope,
                DBGain = dbGain
            };

            wrapper.TransitionFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.TransitionSlopeInlet.DefaultValue = DEFAULT_TRANSITION_SLOPE;
            wrapper.DBGainInlet.DefaultValue = DEFAULT_DB_GAIN;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public MaxOverInlets_OperatorWrapper MaxOverInlets(params Outlet[] operands)
        {
            return MaxOverInlets((IList<Outlet>)operands);
        }

        public MaxOverInlets_OperatorWrapper MaxOverInlets(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = CreateBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.MaxOverInlets, operands);

            new Versatile_OperatorValidator(op).Assert();

            var wrapper = new MaxOverInlets_OperatorWrapper(op);
            return wrapper;
        }

        public MaxFollower_OperatorWrapper MaxFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.MaxFollower,
                new[] { DimensionEnum.Signal, DimensionEnum.SliceLength, DimensionEnum.SampleCount },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new MaxFollower_OperatorWrapper(op)
            {
                SignalInput = signal,
                SliceLength = sliceLength,
                SampleCount = sampleCount
            };

            wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
            wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public MaxOverDimension_OperatorWrapper MaxOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.MaxOverDimension,
                new[] { DimensionEnum.Signal, DimensionEnum.From, DimensionEnum.Till, DimensionEnum.Step },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new MaxOverDimension_OperatorWrapper(op)
            {
                SignalInput = signal,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public MinOverInlets_OperatorWrapper MinOverInlets(params Outlet[] operands)
        {
            return MinOverInlets((IList<Outlet>)operands);
        }

        public MinOverInlets_OperatorWrapper MinOverInlets(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = CreateBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.MinOverInlets, operands);

            new Versatile_OperatorValidator(op).Assert();

            var wrapper = new MinOverInlets_OperatorWrapper(op);
            return wrapper;
        }

        public MinFollower_OperatorWrapper MinFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.MinFollower,
                new[] { DimensionEnum.Signal, DimensionEnum.SliceLength, DimensionEnum.SampleCount },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new MinFollower_OperatorWrapper(op)
            {
                SignalInput = signal,
                SliceLength = sliceLength,
                SampleCount = sampleCount
            };

            wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
            wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public MinOverDimension_OperatorWrapper MinOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.MinOverDimension,
                new[] { DimensionEnum.Signal, DimensionEnum.From, DimensionEnum.Till, DimensionEnum.Step },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new MinOverDimension_OperatorWrapper(op)
            {
                SignalInput = signal,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Multiply(params Outlet[] items)
        {
            return Multiply((IList<Outlet>)items);
        }

        public OperatorWrapper_WithUnderlyingPatch Multiply(IList<Outlet> items)
        {
            if (items == null) throw new NullException(() => items);

            Operator op = New(MethodBase.GetCurrentMethod());

            VoidResult setInletCountResult = _patchManager.SetOperatorInletCount(op, items.Count);
            setInletCountResult.Assert();

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs.SetMany(DimensionEnum.Item, items);

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch MultiplyWithOrigin(Outlet a = null, Outlet b = null, Outlet origin = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;
            wrapper.Inputs[DimensionEnum.Origin] = origin;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Negative(Outlet number = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public Noise_OperatorWrapper Noise(
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Noise,
                new DimensionEnum[0],
                new[] { DimensionEnum.Sound });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Noise_OperatorWrapper(op);

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Not(Outlet number = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public NotchFilter_OperatorWrapper NotchFilter(
            Outlet sound = null, 
            Outlet centerFrequency = null, 
            Outlet width = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.NotchFilter,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.Width },
                new[] { DimensionEnum.Sound });

            var wrapper = new NotchFilter_OperatorWrapper(op)
            {
                SoundInput = sound,
                CenterFrequency = centerFrequency,
                Width = width
            };

            wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.WidthInlet.DefaultValue = DEFAULT_FILTER_WIDTH;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }


        public OperatorWrapper_WithUnderlyingPatch NotEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public Number_OperatorWrapper Number(double number = 0)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Number,
                new DimensionEnum[0],
                new[] { DimensionEnum.Number });

            var wrapper = new Number_OperatorWrapper(op)
            {
                Number = number
            };

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch OneOverX(Outlet number = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Or(Outlet a = null, Outlet b = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet()
        {
            Operator op = CreateBase(
                OperatorTypeEnum.PatchInlet,
                new[] { DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined });

            var wrapper = new PatchInlet_OperatorWrapper(op);
            wrapper.Inlet.SetDimensionEnum(DimensionEnum.Number, _repositories.DimensionRepository);

            new Operator_SideEffect_GeneratePatchInletPosition(op).Execute();

            // Call save to execute side-effects and robust validation.
            VoidResult result = _patchManager.SaveOperator(op);
            result.Assert();

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(DimensionEnum dimension)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();

            wrapper.Inlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);

            new Versatile_OperatorValidator(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Inlet.Name = name;

            new Versatile_OperatorValidator(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name, double defaultValue)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Inlet.Name = name;
            wrapper.Inlet.DefaultValue = defaultValue;

            new Versatile_OperatorValidator(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(DimensionEnum dimension, double defaultValue)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Inlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);
            wrapper.Inlet.DefaultValue = defaultValue;

            new Versatile_OperatorValidator(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.PatchOutlet,
                new[] { DimensionEnum.Undefined },
                new[] { DimensionEnum.Undefined });

            var wrapper = new PatchOutlet_OperatorWrapper(op)
            {
                Input = input
            };
            wrapper.Outlet.SetDimensionEnum(DimensionEnum.Number, _repositories.DimensionRepository);

            new Operator_SideEffect_GeneratePatchOutletPosition(op).Execute();

            // Call save to execute side-effects and robust validation.
            VoidResult result = _patchManager.SaveOperator(op);
            result.Assert();

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(DimensionEnum dimension, Outlet input = null)
        {
            PatchOutlet_OperatorWrapper wrapper = PatchOutlet(input);
            wrapper.Outlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);

            new Versatile_OperatorValidator(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(string name, Outlet input = null)
        {
            PatchOutlet_OperatorWrapper wrapper = PatchOutlet(input);
            wrapper.Outlet.Name = name;

            new Versatile_OperatorValidator(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        public PeakingEQFilter_OperatorWrapper PeakingEQFilter(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null,
            Outlet dbGain = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.PeakingEQFilter,
                new[] { DimensionEnum.Sound, DimensionEnum.Frequency, DimensionEnum.Width, DimensionEnum.Decibel },
                new[] { DimensionEnum.Sound });

            var wrapper = new PeakingEQFilter_OperatorWrapper(op)
            {
                Sound = sound,
                CenterFrequency = centerFrequency,
                Width = width,
                DBGain = dbGain
            };

            wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.DBGainInlet.DefaultValue = DEFAULT_DB_GAIN;
            wrapper.WidthInlet.DefaultValue = DEFAULT_FILTER_WIDTH;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Power(Outlet @base = null, Outlet exponent = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Base] = @base;
            wrapper.Inputs[DimensionEnum.Exponent] = exponent;

            return wrapper;
        }

        public Pulse_OperatorWrapper Pulse(
            Outlet frequency = null, 
            Outlet width = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Pulse,
                new[] { DimensionEnum.Frequency, DimensionEnum.Width },
                new[] { DimensionEnum.Sound });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Pulse_OperatorWrapper(op)
            {
                Frequency = frequency,
                Width = width,
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;
            wrapper.WidthInlet.DefaultValue = DEFAULT_PULSE_WIDTH;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public PulseTrigger_OperatorWrapper PulseTrigger(Outlet passThrough = null, Outlet reset = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.PulseTrigger,
                new[] { DimensionEnum.PassThrough, DimensionEnum.Reset },
                new[] { DimensionEnum.PassThrough });

            var wrapper = new PulseTrigger_OperatorWrapper(op)
            {
                PassThroughInput = passThrough,
                Reset = reset
            };

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Random_OperatorWrapper Random(
            Outlet rate = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Random,
                new[] { DimensionEnum.Rate },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Random_OperatorWrapper(op)
            {
                Rate = rate,
                InterpolationType = ResampleInterpolationTypeEnum.Stripe
            };

            wrapper.RateInlet.DefaultValue = DEFAULT_RANDOM_RATE;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public RangeOverDimension_OperatorWrapper RangeOverDimension(
            Outlet from = null, 
            Outlet till = null, 
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.RangeOverDimension,
                new[] { DimensionEnum.From, DimensionEnum.Till, DimensionEnum.Step },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new RangeOverDimension_OperatorWrapper(op)
            {
                From = from,
                Till = till,
                Step = step
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_RANGE_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_RANGE_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public RangeOverOutlets_OperatorWrapper RangeOverOutlets(
            Outlet from = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            int? outletCount = null)
        {
            outletCount = outletCount ?? 1;

            if (outletCount < 1) throw new LessThanException(() => outletCount, 1);

            Operator op = CreateBase(
                OperatorTypeEnum.RangeOverOutlets,
                new[] { DimensionEnum.From, DimensionEnum.Step },
                Enumerable.Repeat(DimensionEnum.Item, outletCount.Value).ToArray());

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new RangeOverOutlets_OperatorWrapper(op)
            {
                From = from,
                Step = step
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_RANGE_FROM;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Reset_OperatorWrapper Reset(Outlet passThrough = null, int? position = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Reset,
                new[] { DimensionEnum.PassThrough },
                new[] { DimensionEnum.PassThrough });

            var wrapper = new Reset_OperatorWrapper(op)
            {
                PassThroughInput = passThrough,
                Position = position
            };

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Reverse_OperatorWrapper Reverse(
            Outlet signal = null, 
            Outlet factor = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Reverse,
                new[] { DimensionEnum.Signal, DimensionEnum.Factor },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Reverse_OperatorWrapper(op)
            {
                Signal = signal,
                Factor = factor
            };

            wrapper.FactorInlet.DefaultValue = DEFAULT_REVERSE_FACTOR;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Round_OperatorWrapper Round(Outlet signal = null, Outlet step = null, Outlet offset = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Round,
                new[] { DimensionEnum.Signal, DimensionEnum.Step, DimensionEnum.Offset },
                new[] { DimensionEnum.Signal });

            var wrapper = new Round_OperatorWrapper(op)
            {
                Signal = signal,
                Step = step,
                Offset = offset
            };

            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Sample_OperatorWrapper Sample(
            Sample sample = null, 
            Outlet frequency = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Sample,
                new[] { DimensionEnum.Frequency },
                new[] { DimensionEnum.Sound });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Sample_OperatorWrapper(op, _repositories.SampleRepository)
            {
                Frequency = frequency,
                SampleID = sample?.ID
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public SawDown_OperatorWrapper SawDown(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.SawDown,
                new[] { DimensionEnum.Frequency },
                new[] { DimensionEnum.Sound });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SawDown_OperatorWrapper(op)
            {
                Frequency = frequency
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public SawUp_OperatorWrapper SawUp(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.SawUp,
                new[] { DimensionEnum.Frequency },
                new[] { DimensionEnum.Sound });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SawUp_OperatorWrapper(op)
            {
                Frequency = frequency,
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Scaler_OperatorWrapper Scaler(
            Outlet signal = null,
            Outlet sourceValueA = null,
            Outlet sourceValueB = null,
            Outlet targetValueA = null,
            Outlet targetValueB = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Scaler,
                new[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new[] { DimensionEnum.Signal });

            var wrapper = new Scaler_OperatorWrapper(op)
            {
                SignalInput = signal,
                SourceValueA = sourceValueA,
                SourceValueB = sourceValueB,
                TargetValueA = targetValueA,
                TargetValueB = targetValueB,
            };

            wrapper.SourceValueAInlet.DefaultValue = DEFAULT_SCALE_SOURCE_VALUE_A;
            wrapper.SourceValueBInlet.DefaultValue = DEFAULT_SCALE_SOURCE_VALUE_B;
            wrapper.TargetValueAInlet.DefaultValue = DEFAULT_SCALE_TARGET_VALUE_A;
            wrapper.TargetValueBInlet.DefaultValue = DEFAULT_SCALE_TARGET_VALUE_B;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public SetDimension_OperatorWrapper SetDimension(
            Outlet passThrough = null, 
            Outlet number = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.SetDimension,
                new[] { DimensionEnum.PassThrough, DimensionEnum.Number },
                new[] { DimensionEnum.PassThrough });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SetDimension_OperatorWrapper(op)
            {
                PassThroughInput = passThrough,
                Number = number
            };

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Shift_OperatorWrapper Shift(Outlet signal = null, Outlet difference = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Shift,
                new[] { DimensionEnum.Signal, DimensionEnum.Difference },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Shift_OperatorWrapper(op)
            {
                Signal = signal,
                Difference = difference
            };

            wrapper.DifferenceInlet.DefaultValue = DEFAULT_DIFFERENCE;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Sine(Outlet frequency = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Frequency] = frequency;

            return wrapper;
        }

        public SortOverDimension_OperatorWrapper SortOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.SortOverDimension,
                new[] { DimensionEnum.Signal, DimensionEnum.From, DimensionEnum.Till, DimensionEnum.Step },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SortOverDimension_OperatorWrapper(op)
            {
                SignalInput = signal,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public SortOverInlets_OperatorWrapper SortOverInlets(params Outlet[] operands)
        {
            return SortOverInlets((IList<Outlet>)operands);
        }

        public SortOverInlets_OperatorWrapper SortOverInlets(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = CreateBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.SortOverInlets, operands);
            op.Outlets[0].SetDimensionEnum(DimensionEnum.Signal, _repositories.DimensionRepository);

            new Versatile_OperatorValidator(op).Assert();

            var wrapper = new SortOverInlets_OperatorWrapper(op);

            return wrapper;
        }

        public Spectrum_OperatorWrapper Spectrum(
            Outlet sound = null, 
            Outlet start = null, 
            Outlet end = null, 
            Outlet frequencyCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Spectrum,
                new[] { DimensionEnum.Sound, DimensionEnum.Start, DimensionEnum.End, DimensionEnum.FrequencyCount },
                new[] { DimensionEnum.Volume });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Spectrum_OperatorWrapper(op)
            {
                Sound = sound,
                Start = start,
                End = end,
                FrequencyCount = frequencyCount
            };

            wrapper.StartInlet.DefaultValue = DEFAULT_START_TIME;
            wrapper.EndInlet.DefaultValue = DEFAULT_END_TIME;
            wrapper.FrequencyCountInlet.DefaultValue = DEFAULT_SPECTRUM_FREQUENCY_COUNT;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Square_OperatorWrapper Square(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Square,
                new[] { DimensionEnum.Frequency },
                new[] { DimensionEnum.Sound });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Square_OperatorWrapper(op)
            {
                Frequency = frequency,
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Squash_OperatorWrapper Squash(
            Outlet signal = null,
            Outlet factor = null,
            Outlet origin = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Squash,
                new[] { DimensionEnum.Signal, DimensionEnum.Factor, DimensionEnum.Origin },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Squash_OperatorWrapper(op)
            {
                Signal = signal,
                Factor = factor,
                Origin = origin
            };

            wrapper.FactorInlet.DefaultValue = DEFAULT_FACTOR;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Stretch_OperatorWrapper Stretch(
            Outlet signal = null, 
            Outlet factor = null, 
            Outlet origin = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Stretch,
                new[] { DimensionEnum.Signal, DimensionEnum.Factor, DimensionEnum.Origin },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Stretch_OperatorWrapper(op)
            {
                Signal = signal,
                Factor = factor,
                Origin = origin
            };

            wrapper.FactorInlet.DefaultValue = DEFAULT_FACTOR;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Subtract(Outlet a = null, Outlet b = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public SumFollower_OperatorWrapper SumFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.SumFollower,
                new[] { DimensionEnum.Signal, DimensionEnum.SliceLength, DimensionEnum.SampleCount },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SumFollower_OperatorWrapper(op)
            {
                SignalInput = signal,
                SliceLength = sliceLength,
                SampleCount = sampleCount
            };

            wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
            wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public SumOverDimension_OperatorWrapper SumOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null,
            CollectionRecalculationEnum collectionRecalculation = CollectionRecalculationEnum.Continuous)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.SumOverDimension,
                new[] { DimensionEnum.Signal, DimensionEnum.From, DimensionEnum.Till, DimensionEnum.Step },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SumOverDimension_OperatorWrapper(op)
            {
                SignalInput = signal,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public TimePower_OperatorWrapper TimePower(
            Outlet signal = null, 
            Outlet exponent = null, 
            Outlet origin = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.TimePower,
                new[] { DimensionEnum.Signal, DimensionEnum.Exponent, DimensionEnum.Origin },
                new[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new TimePower_OperatorWrapper(op)
            {
                Signal = signal,
                Exponent = exponent,
                Origin = origin
            };

            wrapper.ExponentInlet.DefaultValue = DEFAULT_EXPONENT;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public ToggleTrigger_OperatorWrapper ToggleTrigger(Outlet passThrough = null, Outlet reset = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.ToggleTrigger,
                new[] { DimensionEnum.PassThrough, DimensionEnum.Reset },
                new[] { DimensionEnum.PassThrough });

            var wrapper = new ToggleTrigger_OperatorWrapper(op)
            {
                PassThroughInput = passThrough,
                Reset = reset
            };

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        public Triangle_OperatorWrapper Triangle(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
        {
            Operator op = CreateBase(
                OperatorTypeEnum.Triangle,
                new[] { DimensionEnum.Frequency },
                new[] { DimensionEnum.Sound });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Triangle_OperatorWrapper(op)
            {
                Frequency = frequency
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

            new Versatile_OperatorValidator(op).Assert();

            return wrapper;
        }

        // Helpers

        private void SetOperands(Operator op, IList<Outlet> operands)
        {
            if (op.Inlets.Count != operands.Count) throw new NotEqualException(() => op.Inlets.Count, () => operands.Count);

            for (int i = 0; i < operands.Count; i++)
            {
                Inlet inlet = op.Inlets[i];
                Outlet operand = operands[i];

                inlet.LinkTo(operand);
            }
        }

        // Generic methods for operator creation

        /// <param name="methodBase">methodBase.Name must match an OperatorTypeEnum member's name.</param>
        private Operator New(MethodBase methodBase)
        {
            string patchName = methodBase.Name;

            return New(patchName);
        }

        public Operator New(string systemPatchName)
        {
            Patch patch = _documentManager.GetSystemPatch(systemPatchName);

            Operator op = CustomOperator(patch);

            op.SetOperatorTypeEnum(EnumHelper.Parse<OperatorTypeEnum>(systemPatchName), _repositories);

            return op;
        }

        public Operator New(Patch patch)
        {
            Operator op = CustomOperator(patch);

            if (patch.IsSystemPatch())
            {
                // TODO: If it proves that you do not need OperatorTypeEnum anymore for DivideWithOrigin,
                // You can juse clear the operator type here.
                Enum.TryParse(patch.Name, out OperatorTypeEnum operatorTypeEnum);
                op.SetOperatorTypeEnum(operatorTypeEnum, _repositories);
            }

            return op;
        }

        /// <param name="variableInletOrOutletCount">
        /// Applies to operators with a variable amount of inlets or outlets,
        /// such as the Adder operator and the InletsToDimension operator.
        /// </param>
        public Operator New(OperatorTypeEnum operatorTypeEnum, int variableInletOrOutletCount = 16)
        {
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Absolute: return Absolute();
                case OperatorTypeEnum.Add: return Add(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.AllPassFilter: return AllPassFilter();
                case OperatorTypeEnum.And: return And();
                case OperatorTypeEnum.AverageFollower: return AverageFollower();
                case OperatorTypeEnum.AverageOverDimension: return AverageOverDimension();
                case OperatorTypeEnum.AverageOverInlets: return AverageOverInlets(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.BandPassFilterConstantPeakGain: return BandPassFilterConstantPeakGain();
                case OperatorTypeEnum.BandPassFilterConstantTransitionGain: return BandPassFilterConstantTransitionGain();
                case OperatorTypeEnum.Cache: return Cache();
                case OperatorTypeEnum.ChangeTrigger: return ChangeTrigger();
                case OperatorTypeEnum.ClosestOverDimension: return ClosestOverDimension();
                case OperatorTypeEnum.ClosestOverDimensionExp: return ClosestOverDimensionExp();
                case OperatorTypeEnum.ClosestOverInlets: return ClosestOverInlets(null, new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.ClosestOverInletsExp: return ClosestOverInletsExp(null, new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.Curve: return Curve();
                case OperatorTypeEnum.CustomOperator: return CustomOperator();
                case OperatorTypeEnum.DimensionToOutlets: return DimensionToOutlets(null, variableInletOrOutletCount);
                case OperatorTypeEnum.Divide: return Divide();
                case OperatorTypeEnum.Equal: return Equal();
                case OperatorTypeEnum.Exponent: return Exponent();
                case OperatorTypeEnum.GetDimension: return GetDimension();
                case OperatorTypeEnum.GreaterThan: return GreaterThan();
                case OperatorTypeEnum.GreaterThanOrEqual: return GreaterThanOrEqual();
                case OperatorTypeEnum.HighPassFilter: return HighPassFilter();
                case OperatorTypeEnum.HighShelfFilter: return HighShelfFilter();
                case OperatorTypeEnum.Hold: return Hold();
                case OperatorTypeEnum.If: return If();
                case OperatorTypeEnum.InletsToDimension: return InletsToDimension(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.Interpolate: return Interpolate();
                case OperatorTypeEnum.LessThan: return LessThan();
                case OperatorTypeEnum.LessThanOrEqual: return LessThanOrEqual();
                case OperatorTypeEnum.Loop: return Loop();
                case OperatorTypeEnum.LowPassFilter: return LowPassFilter();
                case OperatorTypeEnum.LowShelfFilter: return LowShelfFilter();
                case OperatorTypeEnum.MaxFollower: return MaxFollower();
                case OperatorTypeEnum.MaxOverDimension: return MaxOverDimension();
                case OperatorTypeEnum.MaxOverInlets: return MaxOverInlets(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.MinFollower: return MinFollower();
                case OperatorTypeEnum.MinOverDimension: return MinOverDimension();
                case OperatorTypeEnum.MinOverInlets: return MinOverInlets(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.Multiply: return Multiply(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.Negative: return Negative();
                case OperatorTypeEnum.Noise: return Noise();
                case OperatorTypeEnum.Not: return Not();
                case OperatorTypeEnum.NotchFilter: return NotchFilter();
                case OperatorTypeEnum.NotEqual: return NotEqual();
                case OperatorTypeEnum.Number: return Number();
                case OperatorTypeEnum.Or: return Or();
                case OperatorTypeEnum.PatchInlet: return PatchInlet();
                case OperatorTypeEnum.PatchOutlet: return PatchOutlet();
                case OperatorTypeEnum.PeakingEQFilter: return PeakingEQFilter();
                case OperatorTypeEnum.Power: return Power();
                case OperatorTypeEnum.Pulse: return Pulse();
                case OperatorTypeEnum.PulseTrigger: return PulseTrigger();
                case OperatorTypeEnum.Random: return Random();
                case OperatorTypeEnum.RangeOverDimension: return RangeOverDimension();
                case OperatorTypeEnum.RangeOverOutlets: return RangeOverOutlets(outletCount: variableInletOrOutletCount);
                case OperatorTypeEnum.Reset: return Reset();
                case OperatorTypeEnum.Reverse: return Reverse();
                case OperatorTypeEnum.Round: return Round();
                case OperatorTypeEnum.Sample: return Sample();
                case OperatorTypeEnum.SawDown: return SawDown();
                case OperatorTypeEnum.SawUp: return SawUp();
                case OperatorTypeEnum.Scaler: return Scaler();
                case OperatorTypeEnum.SetDimension: return SetDimension();
                case OperatorTypeEnum.Shift: return Shift();
                case OperatorTypeEnum.Sine: return Sine();
                case OperatorTypeEnum.SortOverDimension: return SortOverDimension();
                case OperatorTypeEnum.SortOverInlets: return SortOverInlets(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.Spectrum: return Spectrum();
                case OperatorTypeEnum.Square: return Square();
                case OperatorTypeEnum.Squash: return Squash();
                case OperatorTypeEnum.Stretch: return Stretch();
                case OperatorTypeEnum.Subtract: return Subtract();
                case OperatorTypeEnum.SumFollower: return SumFollower();
                case OperatorTypeEnum.SumOverDimension: return SumOverDimension();
                case OperatorTypeEnum.TimePower: return TimePower();
                case OperatorTypeEnum.ToggleTrigger: return ToggleTrigger();
                case OperatorTypeEnum.Triangle: return Triangle();

                default:
                    throw new Exception($"OperatorTypeEnum '{operatorTypeEnum}' not supported by the PatchManager.CreateOperator method.");
            }
        }

        private Operator CreateBase(
            OperatorTypeEnum operatorTypeEnum,
            IList<DimensionEnum> inletDimensionEnums,
            IList<DimensionEnum> outletDimensionEnums)
        {
            int inletCount = inletDimensionEnums.Count;
            int outletCount = outletDimensionEnums.Count;

            var op = new Operator { ID = _repositories.IDRepository.GetID() };
            op.SetOperatorTypeEnum(operatorTypeEnum, _repositories);

            op.LinkTo(_patch);
            _repositories.OperatorRepository.Insert(op);

            for (int i = 0; i < inletCount; i++)
            {
                Inlet inlet = _patchManager.CreateInlet(op);
                inlet.Position = i;

                DimensionEnum dimensionEnum = inletDimensionEnums[i];
                inlet.SetDimensionEnum(dimensionEnum, _repositories.DimensionRepository);
            }

            for (int i = 0; i < outletCount; i++)
            {
                Outlet outlet = _patchManager.CreateOutlet(op);
                outlet.Position = i;

                DimensionEnum dimensionEnum = outletDimensionEnums[i];
                outlet.SetDimensionEnum(dimensionEnum, _repositories.DimensionRepository);
            }

            return op;
        }

        private Operator CreateBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum operatorTypeEnum, IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            var op = new Operator { ID = _repositories.IDRepository.GetID() };
            op.SetOperatorTypeEnum(operatorTypeEnum, _repositories);
            _repositories.OperatorRepository.Insert(op);

            for (int i = 0; i < operands.Count; i++)
            {
                var inlet = new Inlet
                {
                    ID = _repositories.IDRepository.GetID(),
                    Position = i
                };
                inlet.SetDimensionEnum(DimensionEnum.Item, _repositories.DimensionRepository);
                inlet.LinkTo(op);
                _repositories.InletRepository.Insert(inlet);

                Outlet operand = operands[i];
                inlet.LinkTo(operand);
            }

            var outlet = new Outlet { ID = _repositories.IDRepository.GetID() };
            outlet.SetDimensionEnum(DimensionEnum.Number, _repositories.DimensionRepository);
            outlet.LinkTo(op);
            _repositories.OutletRepository.Insert(outlet);

            op.LinkTo(_patch);

            return op;
        }
    }
}
