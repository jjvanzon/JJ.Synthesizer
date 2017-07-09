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
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer
{
    public class OperatorFactory
    {
        private const double DEFAULT_FILTER_FREQUENCY = 1760.0;
        private const double DEFAULT_FREQUENCY = 440.0;
        private const double DEFAULT_FROM = 0.0;
        private const double DEFAULT_TILL = 15.0;
        private const double DEFAULT_DIFFERENCE = 1.0;
        private const double DEFAULT_END_TIME = 1.0;
        private const double DEFAULT_EXPONENT = 2.0;
        private const double DEFAULT_FACTOR = 2.0;
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
            return NewWithItemInlets(MethodBase.GetCurrentMethod(), items);
        }

        public OperatorWrapper_WithUnderlyingPatch AllPassFilter(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch AverageOverInlets(params Outlet[] operands)
        {
            return AverageOverInlets((IList<Outlet>)operands);
        }

        public OperatorWrapper_WithUnderlyingPatch AverageOverInlets(IList<Outlet> items)
        {
            return NewWithItemInlets(MethodBase.GetCurrentMethod(), items);
        }

        public OperatorWrapper_WithUnderlyingPatch BandPassFilterConstantPeakGain(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch BandPassFilterConstantTransitionGain(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch ClosestOverInlets(Outlet input, params Outlet[] items)
        {
            return ClosestOverInlets(input, (IList<Outlet>)items);
        }

        public OperatorWrapper_WithUnderlyingPatch ClosestOverInlets(Outlet input, IList<Outlet> items)
        {
            return NewWithInputAndItemsInlets(MethodBase.GetCurrentMethod(), input, items);
        }

        public OperatorWrapper_WithUnderlyingPatch ClosestOverInletsExp(Outlet input, params Outlet[] items)
        {
            return ClosestOverInletsExp(input, (IList<Outlet>)items);
        }

        public OperatorWrapper_WithUnderlyingPatch ClosestOverInletsExp(Outlet input, IList<Outlet> items)
        {
            return NewWithInputAndItemsInlets(MethodBase.GetCurrentMethod(), input, items);
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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch CustomOperator()
        {
            var op = new Operator { ID = _repositories.IDRepository.GetID() };
            op.SetOperatorTypeEnum(OperatorTypeEnum.CustomOperator, _repositories);

            _repositories.OperatorRepository.Insert(op);

            op.LinkTo(_patch);

            new OperatorValidator_Versatile(op).Assert();

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch CustomOperator(Patch underlyingPatch)
        {
            OperatorWrapper_WithUnderlyingPatch wrapper = CustomOperator();
            Operator op = wrapper;

            op.UnderlyingPatch = underlyingPatch;

            new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories).Execute();

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public OperatorWrapper_WithUnderlyingPatch CustomOperator(Patch underlyingPatch, params Outlet[] operands)
        {
            return CustomOperator(underlyingPatch, (IList<Outlet>)operands);
        }

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(Outlet signal, DimensionEnum standardDimension, string customDimension, int outletCount)
            => DimensionToOutletsPrivate(signal, standardDimension, customDimension, outletCount);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(Outlet signal, DimensionEnum standardDimension, string customDimension)
            => DimensionToOutletsPrivate(signal, standardDimension, customDimension, null);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(Outlet signal, DimensionEnum standardDimension, int outletCount)
            => DimensionToOutletsPrivate(signal, standardDimension, null, outletCount);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(Outlet signal, DimensionEnum standardDimension)
            => DimensionToOutletsPrivate(signal, standardDimension, null, null);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(Outlet signal, string customDimension, int outletCount)
            => DimensionToOutletsPrivate(signal, null, customDimension, outletCount);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(Outlet signal, string customDimension)
            => DimensionToOutletsPrivate(signal, null, customDimension, null);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(Outlet signal, int outletCount)
            => DimensionToOutletsPrivate(signal, null, null, outletCount);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(Outlet signal)
            => DimensionToOutletsPrivate(signal, null, null, null);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(DimensionEnum standardDimension, string customDimension, int outletCount)
            => DimensionToOutletsPrivate(null, standardDimension, customDimension, outletCount);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(DimensionEnum standardDimension, string customDimension)
            => DimensionToOutletsPrivate(null, standardDimension, customDimension, null);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(DimensionEnum standardDimension, int outletCount)
            => DimensionToOutletsPrivate(null, standardDimension, null, outletCount);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(DimensionEnum standardDimension)
            => DimensionToOutletsPrivate(null, standardDimension, null, null);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(int outletCount, string customDimension)
            => DimensionToOutletsPrivate(null, null, customDimension, outletCount);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(string customDimension)
            => DimensionToOutletsPrivate(null, null, customDimension, null);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets(int outletCount)
            => DimensionToOutletsPrivate(null, null, null, outletCount);

        public OperatorWrapper_WithUnderlyingPatch DimensionToOutlets()
            => DimensionToOutletsPrivate(null, null, null, null);

        private OperatorWrapper_WithUnderlyingPatch DimensionToOutletsPrivate(
            Outlet signal,
            DimensionEnum? dimension,
            string customDimension,
            int? outletCount)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            // Dimension
            if (dimension.HasValue)
            {
                op.SetStandardDimensionEnum(dimension.Value, _repositories.DimensionRepository);
            }
            op.CustomDimensionName = customDimension;

            // OutletCount
            VoidResult setOutletCountResult = _patchManager.SetOperatorOutletCount(op, outletCount ?? 1);
            setOutletCountResult.Assert();

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Signal] = signal;

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

        public OperatorWrapper_WithUnderlyingPatch HighPassFilter(
            Outlet sound = null, 
            Outlet minFrequency = null,
            Outlet blobVolume = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = minFrequency;
            wrapper.Inputs[DimensionEnum.BlobVolume] = blobVolume;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch HighShelfFilter(
            Outlet sound = null,
            Outlet transitionFrequency = null,
            Outlet transitionSlope = null,
            Outlet dbGain = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = transitionFrequency;
            wrapper.Inputs[DimensionEnum.Slope] = transitionSlope;
            wrapper.Inputs[DimensionEnum.Decibel] = dbGain;

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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch If(Outlet condition = null, Outlet then = null, Outlet @else = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
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
            DimensionEnum? dimension,
            string customDimension = null)
        {
            if (items == null) throw new NullException(() => items);

            Operator op = New(MethodBase.GetCurrentMethod());

            // Dimension
            if (dimension.HasValue)
            {
                op.SetStandardDimensionEnum(dimension.Value, _repositories.DimensionRepository);
            }
            op.CustomDimensionName = customDimension;

            var wrapper = new InletsToDimension_OperatorWrapper(op)
            {
                // Interpolation
                InterpolationType = interpolation ?? ResampleInterpolationTypeEnum.Stripe
            };
            
            // Items
            wrapper.Inputs.SetMany(DimensionEnum.Item, items);

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch LowPassFilter(
            Outlet sound = null,
            Outlet maxFrequency = null,
            Outlet blobVolume = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = maxFrequency;
            wrapper.Inputs[DimensionEnum.BlobVolume] = blobVolume;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch LowShelfFilter(
            Outlet sound = null,
            Outlet transitionFrequency = null,
            Outlet transitionSlope = null,
            Outlet dbGain = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = transitionFrequency;
            wrapper.Inputs[DimensionEnum.Slope] = transitionSlope;
            wrapper.Inputs[DimensionEnum.Decibel] = dbGain;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch MaxOverInlets(params Outlet[] items)
        {
            return MaxOverInlets((IList<Outlet>)items);
        }

        public OperatorWrapper_WithUnderlyingPatch MaxOverInlets(IList<Outlet> items)
        {
            return NewWithItemInlets(MethodBase.GetCurrentMethod(), items);
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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch MinOverInlets(params Outlet[] items)
        {
            return MinOverInlets((IList<Outlet>)items);
        }

        public OperatorWrapper_WithUnderlyingPatch MinOverInlets(IList<Outlet> items)
        {
            return NewWithItemInlets(MethodBase.GetCurrentMethod(), items);
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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Multiply(params Outlet[] items)
        {
            return Multiply((IList<Outlet>)items);
        }

        public OperatorWrapper_WithUnderlyingPatch Multiply(IList<Outlet> items)
        {
            return NewWithItemInlets(MethodBase.GetCurrentMethod(), items);
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

        public OperatorWrapper_WithUnderlyingPatch Noise(
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            if (standardDimension.HasValue)
            {
                op.SetStandardDimensionEnum(standardDimension.Value, _repositories.DimensionRepository);
            }
            op.CustomDimensionName = customDimension;

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Not(Outlet number = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch NotchFilter(
            Outlet sound = null, 
            Outlet centerFrequency = null, 
            Outlet width = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Inlet.Name = name;

            new OperatorValidator_Versatile(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name, double defaultValue)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Inlet.Name = name;
            wrapper.Inlet.DefaultValue = defaultValue;

            new OperatorValidator_Versatile(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(DimensionEnum dimension, double defaultValue)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Inlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);
            wrapper.Inlet.DefaultValue = defaultValue;

            new OperatorValidator_Versatile(wrapper.WrappedOperator).Assert();

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

            new OperatorValidator_Versatile(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(string name, Outlet input = null)
        {
            PatchOutlet_OperatorWrapper wrapper = PatchOutlet(input);
            wrapper.Outlet.Name = name;

            new OperatorValidator_Versatile(wrapper.WrappedOperator).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch PeakingEQFilter(
            Outlet sound = null,
            Outlet centerFrequency = null,
            Outlet width = null,
            Outlet dbGain = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;
            wrapper.Inputs[DimensionEnum.Decibel] = dbGain;

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

        public OperatorWrapper_WithUnderlyingPatch Pulse(
            Outlet frequency = null, 
            Outlet width = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            if (standardDimension.HasValue)
            {
                op.SetStandardDimensionEnum(standardDimension.Value, _repositories.DimensionRepository);
            }
            op.CustomDimensionName = customDimension;

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Frequency] = frequency;
            wrapper.Inputs[DimensionEnum.Width] = width;

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch RangeOverOutlets(
            Outlet from = null,
            Outlet step = null,
            int? outletCount = null)
        {
            Operator op = New(MethodBase.GetCurrentMethod());

            if (outletCount.HasValue)
            {
                VoidResult setOutletCountResult = _patchManager.SetOperatorOutletCount(op, outletCount.Value);
                setOutletCountResult.Assert();
            }

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.From] = from;
            wrapper.Inputs[DimensionEnum.Step] = step;

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch SawDown(
            Outlet frequency = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewWithFrequency(MethodBase.GetCurrentMethod(), frequency, standardDimension, customDimension);
        }

        public OperatorWrapper_WithUnderlyingPatch SawUp(
            Outlet frequency = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewWithFrequency(MethodBase.GetCurrentMethod(), frequency, standardDimension, customDimension);
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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Sine(
            Outlet frequency = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewWithFrequency(MethodBase.GetCurrentMethod(), frequency, standardDimension, customDimension);
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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch SortOverInlets(params Outlet[] items)
        {
            return SortOverInlets((IList<Outlet>)items);
        }

        public OperatorWrapper_WithUnderlyingPatch SortOverInlets(IList<Outlet> items)
        {
            return NewWithItemInlets(MethodBase.GetCurrentMethod(), items);
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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Square(
            Outlet frequency = null,
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            return NewWithFrequency(MethodBase.GetCurrentMethod(), frequency, standardDimension, customDimension);
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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

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

            new OperatorValidator_Versatile(op).Assert();

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Triangle(Outlet frequency = null, DimensionEnum? standardDimension = null, string customDimension = null)
        {
            return NewWithFrequency(MethodBase.GetCurrentMethod(), frequency, standardDimension, customDimension);
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

        private OperatorWrapper_WithUnderlyingPatch NewWithFrequency(
            MethodBase methodBase,
            Outlet frequency,
            DimensionEnum? standardDimension,
            string customDimension)
        {
            Operator op = New(methodBase);

            if (standardDimension.HasValue)
            {
                op.SetStandardDimensionEnum(standardDimension.Value, _repositories.DimensionRepository);
            }
            op.CustomDimensionName = customDimension;

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Frequency] = frequency;

            return wrapper;

        }

        private OperatorWrapper_WithUnderlyingPatch NewWithItemInlets(MethodBase methodBase, IList<Outlet> items)
        {
            if (items == null) throw new NullException(() => items);

            Operator op = New(methodBase);

            VoidResult setInletCountResult = _patchManager.SetOperatorInletCount(op, items.Count);
            setInletCountResult.Assert();

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs.SetMany(DimensionEnum.Item, items);

            return wrapper;
        }

        private OperatorWrapper_WithUnderlyingPatch NewWithInputAndItemsInlets(MethodBase methodBase, Outlet input, IList<Outlet> items)
        {
            if (items == null) throw new NullException(() => items);

            Operator op = New(methodBase);

            VoidResult setInletCountResult = _patchManager.SetOperatorInletCount(op, items.Count);
            setInletCountResult.Assert();

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Input] = input;
            wrapper.Inputs.SetMany(DimensionEnum.Item, items);

            return wrapper;
        }

        // Generic methods for operator creation

        /// <param name="methodBaseWithSameNameAsSystemPatch">methodBase.Name must match an OperatorTypeEnum member's name.</param>
        private Operator New(MethodBase methodBaseWithSameNameAsSystemPatch)
        {
            string systemPatchName = methodBaseWithSameNameAsSystemPatch.Name;

            return New(systemPatchName);
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
