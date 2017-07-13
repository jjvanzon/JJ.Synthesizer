using System;
using System.Collections.Generic;
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

namespace JJ.Business.Synthesizer
{
    public class OperatorFactory
    {
        private const double DEFAULT_DIFFERENCE = 1.0;
        private const double DEFAULT_END_TIME = 1.0;
        private const double DEFAULT_EXPONENT = 2.0;
        private const double DEFAULT_FACTOR = 2.0;
        private const double DEFAULT_RANDOM_RATE = 16.0;
        private const double DEFAULT_REVERSE_FACTOR = 1.0;
        private const double DEFAULT_SCALE_SOURCE_VALUE_A = -1.0;
        private const double DEFAULT_SCALE_SOURCE_VALUE_B = 1.0;
        private const double DEFAULT_SCALE_TARGET_VALUE_A = 1.0;
        private const double DEFAULT_SCALE_TARGET_VALUE_B = 4.0;
        private const double DEFAULT_START_TIME = 0.0;

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch And(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch AverageFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            return NewForAggregateFollower(
                signal,
                sliceLength,
                sampleCount,
                standardDimension,
                customDimension,
                MethodBase.GetCurrentMethod());
        }

        public OperatorWrapper_WithCollectionRecalculation AverageOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
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
                collectionRecalculation,
                MethodBase.GetCurrentMethod());
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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Cache_OperatorWrapper(op);
            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.Start] = start;
            wrapper.Inputs[DimensionEnum.End] = end;
            wrapper.Inputs[DimensionEnum.SamplingRate] = samplingRate;
            wrapper.InterpolationType = interpolationTypeEnum;
            wrapper.SpeakerSetup = speakerSetupEnum;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch ChangeTrigger(Outlet passThrough = null, Outlet reset = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
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
            DimensionEnum standardDimension = DimensionEnum.Undefined,
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
                collectionRecalculation,
                MethodBase.GetCurrentMethod());
        }

        public OperatorWrapper_WithCollectionRecalculation ClosestOverDimensionExp(
            Outlet input = null,
            Outlet collection = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
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
                collectionRecalculation,
                MethodBase.GetCurrentMethod());
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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Curve_OperatorWrapper(op, _repositories.CurveRepository)
            {
                CurveID = curve?.ID
            };

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch DivideWithOrigin(Outlet a = null, Outlet b = null, Outlet origin = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;
            wrapper.Inputs[DimensionEnum.Origin] = origin;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Equal(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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

        public OperatorWrapper_WithUnderlyingPatch GetDimension(DimensionEnum standardDimension = DimensionEnum.Undefined, string customDimension = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch GreaterThan(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch GreaterThanOrEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = transitionFrequency;
            wrapper.Inputs[DimensionEnum.Slope] = transitionSlope;
            wrapper.Inputs[DimensionEnum.Decibel] = dbGain;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Hold(Outlet signal = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Signal] = signal;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch If(Outlet condition = null, Outlet then = null, Outlet @else = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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

            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Interpolate_OperatorWrapper(op);
            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.SamplingRate] = samplingRate;
            wrapper.InterpolationType = interpolationType;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch LessThan(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch LessThanOrEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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

        public OperatorWrapper_WithUnderlyingPatch MaxFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            return NewForAggregateFollower(signal, sliceLength, sampleCount, standardDimension, customDimension, MethodBase.GetCurrentMethod());
        }

        public OperatorWrapper_WithCollectionRecalculation MaxOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
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
                collectionRecalculation,
                MethodBase.GetCurrentMethod());
        }

        public OperatorWrapper_WithUnderlyingPatch MinOverInlets(params Outlet[] items)
        {
            return MinOverInlets((IList<Outlet>)items);
        }

        public OperatorWrapper_WithUnderlyingPatch MinOverInlets(IList<Outlet> items)
        {
            return NewWithItemInlets(MethodBase.GetCurrentMethod(), items);
        }

        public OperatorWrapper_WithUnderlyingPatch MinFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            return NewForAggregateFollower(signal, sliceLength, sampleCount, standardDimension, customDimension, MethodBase.GetCurrentMethod());
        }

        public OperatorWrapper_WithCollectionRecalculation MinOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
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
                collectionRecalculation,
                MethodBase.GetCurrentMethod());
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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;
            wrapper.Inputs[DimensionEnum.Origin] = origin;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Negative(Outlet number = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Noise(
            DimensionEnum? standardDimension = null,
            string customDimension = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch NotchFilter(
            Outlet sound = null, 
            Outlet centerFrequency = null, 
            Outlet width = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch NotEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public Number_OperatorWrapper Number(double number = 0)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new Number_OperatorWrapper(op) { Number = number };

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch OneOverX(Outlet number = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Number] = number;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Or(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Frequency] = centerFrequency;
            wrapper.Inputs[DimensionEnum.Width] = width;
            wrapper.Inputs[DimensionEnum.Decibel] = dbGain;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch Power(Outlet @base = null, Outlet exponent = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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

        public OperatorWrapper_WithUnderlyingPatch PulseTrigger(Outlet passThrough = null, Outlet reset = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.PassThrough]= passThrough;
            wrapper.Inputs[DimensionEnum.Reset] = reset;

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

        public OperatorWrapper_WithUnderlyingPatch RangeOverDimension(
            Outlet from = null, 
            Outlet till = null, 
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.From] = @from;
            wrapper.Inputs[DimensionEnum.Till] = till;
            wrapper.Inputs[DimensionEnum.Step] = step;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch RangeOverOutlets(
            Outlet from = null,
            Outlet step = null,
            int? outletCount = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

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

        public OperatorWrapper_WithUnderlyingPatch Round(Outlet signal = null, Outlet step = null, Outlet offset = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.Step] = step;
            wrapper.Inputs[DimensionEnum.Offset] = offset;

            return wrapper;
        }

        public Sample_OperatorWrapper Sample(
            Sample sample = null, 
            Outlet frequency = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Sample_OperatorWrapper(op, _repositories.SampleRepository)
            {
                SampleID = sample?.ID
            };
            wrapper.Inputs[DimensionEnum.Frequency] = frequency;

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

        public OperatorWrapper_WithUnderlyingPatch SetDimension(
            Outlet passThrough = null, 
            Outlet number = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.PassThrough] = passThrough;
            wrapper.Inputs[DimensionEnum.Number] = number;

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

        public OperatorWrapper_WithCollectionRecalculation SortOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
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
                collectionRecalculation,
                MethodBase.GetCurrentMethod());
        }

        public OperatorWrapper_WithUnderlyingPatch SortOverInlets(params Outlet[] items)
        {
            return SortOverInlets((IList<Outlet>)items);
        }

        public OperatorWrapper_WithUnderlyingPatch SortOverInlets(IList<Outlet> items)
        {
            return NewWithItemInlets(MethodBase.GetCurrentMethod(), items);
        }

        public OperatorWrapper_WithUnderlyingPatch Spectrum(
            Outlet sound = null, 
            Outlet start = null, 
            Outlet end = null, 
            Outlet frequencyCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Sound] = sound;
            wrapper.Inputs[DimensionEnum.Start] = start;
            wrapper.Inputs[DimensionEnum.End] = end;
            wrapper.Inputs[DimensionEnum.FrequencyCount] = frequencyCount;

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
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.A] = a;
            wrapper.Inputs[DimensionEnum.B] = b;

            return wrapper;
        }

        public OperatorWrapper_WithUnderlyingPatch SumFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            return NewForAggregateFollower(signal, sliceLength, sampleCount, standardDimension, customDimension, MethodBase.GetCurrentMethod());
        }

        public OperatorWrapper_WithCollectionRecalculation SumOverDimension(
            Outlet signal = null,
            Outlet from = null,
            Outlet till = null,
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
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
                collectionRecalculation,
                MethodBase.GetCurrentMethod());
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

        public OperatorWrapper_WithUnderlyingPatch ToggleTrigger(Outlet passThrough = null, Outlet reset = null)
        {
            Operator op = CreateBase(MethodBase.GetCurrentMethod());

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.PassThrough] = passThrough;
            wrapper.Inputs[DimensionEnum.Reset] = reset;

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
            Operator op = CreateBase(methodBase);

            op.SetStandardDimensionEnum(standardDimension ?? DimensionEnum.Undefined, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Frequency] = frequency;

            return wrapper;
        }

        private OperatorWrapper_WithUnderlyingPatch NewWithItemInlets(MethodBase methodBase, IList<Outlet> items)
        {
            if (items == null) throw new NullException(() => items);

            Operator op = CreateBase(methodBase);

            VoidResult setInletCountResult = _patchManager.SetOperatorInletCount(op, items.Count);
            setInletCountResult.Assert();

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs.SetMany(DimensionEnum.Item, items);

            return wrapper;
        }

        private OperatorWrapper_WithUnderlyingPatch NewWithInputAndItemsInlets(MethodBase methodBase, Outlet input, IList<Outlet> items)
        {
            if (items == null) throw new NullException(() => items);

            Operator op = CreateBase(methodBase);

            VoidResult setInletCountResult = _patchManager.SetOperatorInletCount(op, items.Count + 1);
            setInletCountResult.Assert();

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Input] = input;
            wrapper.Inputs.SetMany(DimensionEnum.Item, items);

            return wrapper;
        }

        private OperatorWrapper_WithUnderlyingPatch NewForAggregateFollower(
            Outlet signal,
            Outlet sliceLength,
            Outlet sampleCount,
            DimensionEnum standardDimension,
            string customDimension,
            MethodBase methodBaseWithSameNameAsSystemPatch)
        {
            Operator op = CreateBase(methodBaseWithSameNameAsSystemPatch);

            op.CustomDimensionName = customDimension;
            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);

            var wrapper = new OperatorWrapper_WithUnderlyingPatch(op);
            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.SliceLength] = sliceLength;
            wrapper.Inputs[DimensionEnum.SampleCount] = sampleCount;

            return wrapper;
        }

        private OperatorWrapper_WithCollectionRecalculation NewForAggregateOverDimension(
            Outlet signal,
            Outlet from,
            Outlet till,
            Outlet step,
            DimensionEnum standardDimension,
            string customDimension,
            CollectionRecalculationEnum collectionRecalculation,
            MethodBase methodBaseWithSameNameAsSystemPatch)
        {
            Operator op = CreateBase(methodBaseWithSameNameAsSystemPatch);

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new OperatorWrapper_WithCollectionRecalculation(op);
            wrapper.Inputs[DimensionEnum.Signal] = signal;
            wrapper.Inputs[DimensionEnum.From] = @from;
            wrapper.Inputs[DimensionEnum.Till] = till;
            wrapper.Inputs[DimensionEnum.Step] = step;
            wrapper.CollectionRecalculation = collectionRecalculation;

            return wrapper;
        }

        private OperatorWrapper_WithCollectionRecalculation NewForClosestOverDimension_OrClosestOverDimensionExp(
            Outlet input,
            Outlet collection,
            Outlet from,
            Outlet till,
            Outlet step,
            DimensionEnum standardDimension,
            string customDimension,
            CollectionRecalculationEnum collectionRecalculation,
            MethodBase methodBaseWithSameNameAsSystemPatch)
        {
            Operator op = CreateBase(methodBaseWithSameNameAsSystemPatch);

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new OperatorWrapper_WithCollectionRecalculation(op);
            wrapper.Inputs[DimensionEnum.Input] = input;
            wrapper.Inputs[DimensionEnum.Collection] = collection;
            wrapper.Inputs[DimensionEnum.From] = @from;
            wrapper.Inputs[DimensionEnum.Till] = till;
            wrapper.Inputs[DimensionEnum.Step] = step;
            wrapper.CollectionRecalculation = collectionRecalculation;

            return wrapper;
        }

        // Generic methods for operator creation

        public Operator New(string systemPatchName, int variableInletOrOutletCount = 16)
        {
            Patch patch = _documentManager.GetSystemPatch(systemPatchName);
            return New(patch, variableInletOrOutletCount);
        }

        /// <summary> Called from the DocumentTree view's New action. </summary>
        public Operator New(Patch patch, int variableInletOrOutletCount = 16)
        {
            if (NameHelper.AreEqual(patch.Name, nameof(Absolute))) return Absolute();
            if (NameHelper.AreEqual(patch.Name, nameof(Add))) return Add(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(patch.Name, nameof(AllPassFilter))) return AllPassFilter();
            if (NameHelper.AreEqual(patch.Name, nameof(And))) return And();
            if (NameHelper.AreEqual(patch.Name, nameof(AverageFollower))) return AverageFollower();
            if (NameHelper.AreEqual(patch.Name, nameof(AverageOverDimension))) return AverageOverDimension();
            if (NameHelper.AreEqual(patch.Name, nameof(AverageOverInlets))) return AverageOverInlets(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(patch.Name, nameof(BandPassFilterConstantPeakGain))) return BandPassFilterConstantPeakGain();
            if (NameHelper.AreEqual(patch.Name, nameof(BandPassFilterConstantTransitionGain))) return BandPassFilterConstantTransitionGain();
            if (NameHelper.AreEqual(patch.Name, nameof(Cache))) return Cache();
            if (NameHelper.AreEqual(patch.Name, nameof(ChangeTrigger))) return ChangeTrigger();
            if (NameHelper.AreEqual(patch.Name, nameof(ClosestOverDimension))) return ClosestOverDimension();
            if (NameHelper.AreEqual(patch.Name, nameof(ClosestOverDimensionExp))) return ClosestOverDimensionExp();
            if (NameHelper.AreEqual(patch.Name, nameof(ClosestOverInlets))) return ClosestOverInlets(null, new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(patch.Name, nameof(ClosestOverInletsExp))) return ClosestOverInletsExp(null, new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(patch.Name, nameof(Curve))) return Curve();
            if (NameHelper.AreEqual(patch.Name, nameof(CustomOperator))) return CustomOperator();
            if (NameHelper.AreEqual(patch.Name, nameof(DimensionToOutlets))) return DimensionToOutlets(null, variableInletOrOutletCount);
            if (NameHelper.AreEqual(patch.Name, nameof(Divide))) return Divide();
            if (NameHelper.AreEqual(patch.Name, nameof(Equal))) return Equal();
            if (NameHelper.AreEqual(patch.Name, nameof(Exponent))) return Exponent();
            if (NameHelper.AreEqual(patch.Name, nameof(GetDimension))) return GetDimension();
            if (NameHelper.AreEqual(patch.Name, nameof(GreaterThan))) return GreaterThan();
            if (NameHelper.AreEqual(patch.Name, nameof(GreaterThanOrEqual))) return GreaterThanOrEqual();
            if (NameHelper.AreEqual(patch.Name, nameof(HighPassFilter))) return HighPassFilter();
            if (NameHelper.AreEqual(patch.Name, nameof(HighShelfFilter))) return HighShelfFilter();
            if (NameHelper.AreEqual(patch.Name, nameof(Hold))) return Hold();
            if (NameHelper.AreEqual(patch.Name, nameof(If))) return If();
            if (NameHelper.AreEqual(patch.Name, nameof(InletsToDimension))) return InletsToDimension(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(patch.Name, nameof(Interpolate))) return Interpolate();
            if (NameHelper.AreEqual(patch.Name, nameof(LessThan))) return LessThan();
            if (NameHelper.AreEqual(patch.Name, nameof(LessThanOrEqual))) return LessThanOrEqual();
            if (NameHelper.AreEqual(patch.Name, nameof(Loop))) return Loop();
            if (NameHelper.AreEqual(patch.Name, nameof(LowPassFilter))) return LowPassFilter();
            if (NameHelper.AreEqual(patch.Name, nameof(LowShelfFilter))) return LowShelfFilter();
            if (NameHelper.AreEqual(patch.Name, nameof(MaxFollower))) return MaxFollower();
            if (NameHelper.AreEqual(patch.Name, nameof(MaxOverDimension))) return MaxOverDimension();
            if (NameHelper.AreEqual(patch.Name, nameof(MaxOverInlets))) return MaxOverInlets(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(patch.Name, nameof(MinFollower))) return MinFollower();
            if (NameHelper.AreEqual(patch.Name, nameof(MinOverDimension))) return MinOverDimension();
            if (NameHelper.AreEqual(patch.Name, nameof(MinOverInlets))) return MinOverInlets(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(patch.Name, nameof(Multiply))) return Multiply(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(patch.Name, nameof(Negative))) return Negative();
            if (NameHelper.AreEqual(patch.Name, nameof(Noise))) return Noise();
            if (NameHelper.AreEqual(patch.Name, nameof(Not))) return Not();
            if (NameHelper.AreEqual(patch.Name, nameof(NotchFilter))) return NotchFilter();
            if (NameHelper.AreEqual(patch.Name, nameof(NotEqual))) return NotEqual();
            if (NameHelper.AreEqual(patch.Name, nameof(Number))) return Number();
            if (NameHelper.AreEqual(patch.Name, nameof(Or))) return Or();
            if (NameHelper.AreEqual(patch.Name, nameof(PatchInlet))) return PatchInlet();
            if (NameHelper.AreEqual(patch.Name, nameof(PatchOutlet))) return PatchOutlet();
            if (NameHelper.AreEqual(patch.Name, nameof(PeakingEQFilter))) return PeakingEQFilter();
            if (NameHelper.AreEqual(patch.Name, nameof(Power))) return Power();
            if (NameHelper.AreEqual(patch.Name, nameof(Pulse))) return Pulse();
            if (NameHelper.AreEqual(patch.Name, nameof(PulseTrigger))) return PulseTrigger();
            if (NameHelper.AreEqual(patch.Name, nameof(Random))) return Random();
            if (NameHelper.AreEqual(patch.Name, nameof(RangeOverDimension))) return RangeOverDimension();
            if (NameHelper.AreEqual(patch.Name, nameof(RangeOverOutlets))) return RangeOverOutlets(outletCount: variableInletOrOutletCount);
            if (NameHelper.AreEqual(patch.Name, nameof(Reset))) return Reset();
            if (NameHelper.AreEqual(patch.Name, nameof(Reverse))) return Reverse();
            if (NameHelper.AreEqual(patch.Name, nameof(Round))) return Round();
            if (NameHelper.AreEqual(patch.Name, nameof(Sample))) return Sample();
            if (NameHelper.AreEqual(patch.Name, nameof(SawDown))) return SawDown();
            if (NameHelper.AreEqual(patch.Name, nameof(SawUp))) return SawUp();
            if (NameHelper.AreEqual(patch.Name, nameof(Scaler))) return Scaler();
            if (NameHelper.AreEqual(patch.Name, nameof(SetDimension))) return SetDimension();
            if (NameHelper.AreEqual(patch.Name, nameof(Shift))) return Shift();
            if (NameHelper.AreEqual(patch.Name, nameof(Sine))) return Sine();
            if (NameHelper.AreEqual(patch.Name, nameof(SortOverDimension))) return SortOverDimension();
            if (NameHelper.AreEqual(patch.Name, nameof(SortOverInlets))) return SortOverInlets(new Outlet[variableInletOrOutletCount]);
            if (NameHelper.AreEqual(patch.Name, nameof(Spectrum))) return Spectrum();
            if (NameHelper.AreEqual(patch.Name, nameof(Square))) return Square();
            if (NameHelper.AreEqual(patch.Name, nameof(Squash))) return Squash();
            if (NameHelper.AreEqual(patch.Name, nameof(Stretch))) return Stretch();
            if (NameHelper.AreEqual(patch.Name, nameof(Subtract))) return Subtract();
            if (NameHelper.AreEqual(patch.Name, nameof(SumFollower))) return SumFollower();
            if (NameHelper.AreEqual(patch.Name, nameof(SumOverDimension))) return SumOverDimension();
            if (NameHelper.AreEqual(patch.Name, nameof(TimePower))) return TimePower();
            if (NameHelper.AreEqual(patch.Name, nameof(ToggleTrigger))) return ToggleTrigger();
            if (NameHelper.AreEqual(patch.Name, nameof(Triangle))) return Triangle();

            Operator op = CustomOperator(patch);
            return op;
        }

        /// <param name="variableInletOrOutletCount">
        /// Applies to operators with a variable amount of inlets or outlets,
        /// such as the Adder operator and the InletsToDimension operator.
        /// </param>
        [Obsolete("Called from the PatchDetails view's Toolbox, which will be removed once all standard operators have been boostrapped.")]
        public Operator New(OperatorTypeEnum operatorTypeEnum, int variableInletOrOutletCount = 16)
        {
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Cache: return Cache();
                case OperatorTypeEnum.ChangeTrigger: return ChangeTrigger();
                case OperatorTypeEnum.Curve: return Curve();
                case OperatorTypeEnum.Exponent: return Exponent();
                case OperatorTypeEnum.GetDimension: return GetDimension();
                case OperatorTypeEnum.Hold: return Hold();
                case OperatorTypeEnum.Interpolate: return Interpolate();
                case OperatorTypeEnum.Loop: return Loop();
                case OperatorTypeEnum.Number: return Number();
                case OperatorTypeEnum.PatchInlet: return PatchInlet();
                case OperatorTypeEnum.PatchOutlet: return PatchOutlet();
                case OperatorTypeEnum.PulseTrigger: return PulseTrigger();
                case OperatorTypeEnum.Random: return Random();
                case OperatorTypeEnum.Reset: return Reset();
                case OperatorTypeEnum.Reverse: return Reverse();
                case OperatorTypeEnum.Round: return Round();
                case OperatorTypeEnum.Sample: return Sample();
                case OperatorTypeEnum.Scaler: return Scaler();
                case OperatorTypeEnum.SetDimension: return SetDimension();
                case OperatorTypeEnum.Shift: return Shift();
                case OperatorTypeEnum.Spectrum: return Spectrum();
                case OperatorTypeEnum.Squash: return Squash();
                case OperatorTypeEnum.Stretch: return Stretch();
                case OperatorTypeEnum.TimePower: return TimePower();
                case OperatorTypeEnum.ToggleTrigger: return ToggleTrigger();
                
                default:
                    throw new Exception($"{nameof(OperatorTypeEnum)} '{operatorTypeEnum}' not supported by the {GetType().Name}.{MethodBase.GetCurrentMethod().Name} method.");
            }
        }

        /// <param name="methodBaseWithSameNameAsSystemPatch">methodBase.Name must match an OperatorTypeEnum member's name.</param>
        private Operator CreateBase(MethodBase methodBaseWithSameNameAsSystemPatch)
        {
            string systemPatchName = methodBaseWithSameNameAsSystemPatch.Name;
            Patch patch = _documentManager.GetSystemPatch(systemPatchName);

            Operator op = CustomOperator(patch);

            op.UnlinkOperatorType();

            return op;
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
    }
}
