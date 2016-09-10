using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Framework.Business;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Canonical;
using JJ.Business.Canonical;
using JJ.Framework.Common;

namespace JJ.Business.Synthesizer
{
    public partial class PatchManager
    {
        private const double DEFAULT_FILTER_FREQUENCY = 1760.0;
        private const double DEFAULT_FREQUENCY = 440.0;
        private const double DEFAULT_AGGREGATE_FROM = 0.0;
        private const double DEFAULT_AGGREGATE_TILL = 15.0;
        private const double DEFAULT_BAND_WIDTH = 1.0;
        private const double DEFAULT_DB_GAIN = 3.0;
        private const double DEFAULT_DIFFERENCE = 1.0;
        private const double DEFAULT_END_TIME = 1.0;
        private const double DEFAULT_EXPONENT = 2.0;
        private const double DEFAULT_FACTOR = 2.0;
        private const double DEFAULT_PULSE_WIDTH = 0.5;
        private const double DEFAULT_RANDOM_RATE = 16.0;
        private const double DEFAULT_RANGE_FROM = 1.0;
        private const double DEFAULT_RANGE_TILL = 16.0;
        private const double DEFAULT_REVERSE_SPEED = 1.0;
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

        public Absolute_OperatorWrapper Absolute(Outlet x = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Absolute,
                new DimensionEnum[] { DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Absolute_OperatorWrapper(op)
            {
                X = x,
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Add_OperatorWrapper Add(params Outlet[] operands)
        {
            return Add((IList<Outlet>)operands);
        }

        public Add_OperatorWrapper Add(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = CreateOperatorBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.Add, operands);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            var wrapper = new Add_OperatorWrapper(op);
            return wrapper;
        }

        public AllPassFilter_OperatorWrapper AllPassFilter(
            Outlet signal = null,
            Outlet centerFrequency = null,
            Outlet bandWidth = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.AllPassFilter,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            var wrapper = new AllPassFilter_OperatorWrapper(op)
            {
                Signal = signal,
                CenterFrequency = centerFrequency,
                BandWidth = bandWidth
            };

            wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public And_OperatorWrapper And(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.And,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new And_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public AverageOverInlets_OperatorWrapper AverageOverInlets(params Outlet[] operands)
        {
            return AverageOverInlets((IList<Outlet>)operands);
        }

        public AverageOverInlets_OperatorWrapper AverageOverInlets(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = CreateOperatorBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.AverageOverInlets, operands);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            var wrapper = new AverageOverInlets_OperatorWrapper(op);
            return wrapper;
        }

        public AverageFollower_OperatorWrapper AverageFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.AverageFollower,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.CustomDimensionName = customDimension;
            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);

            var wrapper = new AverageFollower_OperatorWrapper(op)
            {
                Signal = signal,
                SliceLength = sliceLength,
                SampleCount = sampleCount,
            };

            wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
            wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.AverageOverDimension,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new AverageOverDimension_OperatorWrapper(op)
            {
                Signal = signal,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public BandPassFilterConstantPeakGain_OperatorWrapper BandPassFilterConstantPeakGain(
            Outlet signal = null,
            Outlet centerFrequency = null,
            Outlet bandWidth = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.BandPassFilterConstantPeakGain,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            var wrapper = new BandPassFilterConstantPeakGain_OperatorWrapper(op)
            {
                Signal = signal,
                CenterFrequency = centerFrequency,
                BandWidth = bandWidth
            };

            wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public BandPassFilterConstantTransitionGain_OperatorWrapper BandPassFilterConstantTransitionGain(
            Outlet signal = null,
            Outlet centerFrequency = null,
            Outlet bandWidth = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.BandPassFilterConstantTransitionGain,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            var wrapper = new BandPassFilterConstantTransitionGain_OperatorWrapper(op)
            {
                Signal = signal,
                CenterFrequency = centerFrequency,
                BandWidth = bandWidth
            };

            wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Bundle_OperatorWrapper Bundle(params Outlet[] operands)
        {
            return Bundle((IList<Outlet>)operands);
        }

        public Bundle_OperatorWrapper Bundle(DimensionEnum standardDimension, params Outlet[] operands)
        {
            return Bundle(operands, standardDimension);
        }

        public Bundle_OperatorWrapper Bundle(
            IList<Outlet> operands, 
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.Bundle, operands);

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Bundle_OperatorWrapper(op);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Cache,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

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

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public ChangeTrigger_OperatorWrapper ChangeTrigger(Outlet calculation = null, Outlet reset = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.ChangeTrigger,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new ChangeTrigger_OperatorWrapper(op)
            {
                PassThroughInput = calculation,
                Reset = reset
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public ClosestOverInlets_OperatorWrapper ClosestOverInlets(Outlet input, params Outlet[] items)
        {
            return ClosestOverInlets(input, (IList<Outlet>)items);
        }

        public ClosestOverInlets_OperatorWrapper ClosestOverInlets(Outlet input, IList<Outlet> items)
        {
            Operator op = CreateOperatorBase_WithVariableInletCountAndOneOutlet(
                OperatorTypeEnum.ClosestOverInlets,
                input.Concat(items).ToArray());

            var wrapper = new ClosestOverInlets_OperatorWrapper(op);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public ClosestOverInletsExp_OperatorWrapper ClosestOverInletsExp(Outlet input, params Outlet[] items)
        {
            return ClosestOverInletsExp(input, (IList<Outlet>)items);
        }

        public ClosestOverInletsExp_OperatorWrapper ClosestOverInletsExp(Outlet input, IList<Outlet> items)
        {
            Operator op = CreateOperatorBase_WithVariableInletCountAndOneOutlet(
                OperatorTypeEnum.ClosestOverInletsExp,
                input.Concat(items).ToArray());

            var wrapper = new ClosestOverInletsExp_OperatorWrapper(op);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.ClosestOverDimension,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

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

            wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.ClosestOverDimensionExp,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

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

            wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Curve_OperatorWrapper Curve(
            Curve curve = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Curve,
                new DimensionEnum[0],
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Curve_OperatorWrapper(op, _repositories.CurveRepository)
            {
                CurveID = curve?.ID
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public CustomOperator_OperatorWrapper CustomOperator()
        {
            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(OperatorTypeEnum.CustomOperator, _repositories.OperatorTypeRepository);

            _repositories.OperatorRepository.Insert(op);

            var wrapper = new CustomOperator_OperatorWrapper(op, _repositories.PatchRepository)
            {
                // Needed to create Operator.Data key "UnderlyingPatchID"
                UnderlyingPatch = null
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch)
        {
            CustomOperator_OperatorWrapper op = CustomOperator();
            op.UnderlyingPatch = underlyingPatch;

            ISideEffect sideEffect = new Operator_SideEffect_ApplyUnderlyingPatch(op, _repositories);
            sideEffect.Execute();

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return op;
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch, IList<Outlet> operands)
        {
            if (underlyingPatch == null) throw new NullException(() => underlyingPatch);
            if (operands == null) throw new NullException(() => operands);

            CustomOperator_OperatorWrapper wrapper = CustomOperator(underlyingPatch);

            SetOperands(wrapper.WrappedOperator, operands);

            wrapper.WrappedOperator.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(wrapper.WrappedOperator);
            ResultHelper.Assert(result);

            return wrapper;
        }

        /// <param name="underlyingPatch">The Patch to base the CustomOperator on.</param>
        public CustomOperator_OperatorWrapper CustomOperator(Patch underlyingPatch, params Outlet[] operands)
        {
            return CustomOperator(underlyingPatch, (IList<Outlet>)operands);
        }

        public Divide_OperatorWrapper Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Divide,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Divide_OperatorWrapper(op)
            {
                Numerator = numerator,
                Denominator = denominator,
                Origin = origin
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Equal_OperatorWrapper Equal(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Equal,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Equal_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Exponent_OperatorWrapper Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Exponent,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Exponent_OperatorWrapper(op)
            {
                Low = low,
                High = high,
                Ratio = ratio
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public GetDimension_OperatorWrapper GetDimension(
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.GetDimension,
                new DimensionEnum[0],
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new GetDimension_OperatorWrapper(op);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public GreaterThan_OperatorWrapper GreaterThan(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.GreaterThan,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new GreaterThan_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public GreaterThanOrEqual_OperatorWrapper GreaterThanOrEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.GreaterThanOrEqual,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new GreaterThanOrEqual_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public HighPassFilter_OperatorWrapper HighPassFilter(
            Outlet signal = null, 
            Outlet minFrequency = null,
            Outlet bandWidth = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.HighPassFilter,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            var wrapper = new HighPassFilter_OperatorWrapper(op)
            {
                Signal = signal,
                MinFrequency = minFrequency,
                BandWidth = bandWidth
            };

            wrapper.MinFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public HighShelfFilter_OperatorWrapper HighShelfFilter(
            Outlet signal = null,
            Outlet transitionFrequency = null,
            Outlet transitionSlope = null,
            Outlet dbGain = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.HighShelfFilter,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            var wrapper = new HighShelfFilter_OperatorWrapper(op)
            {
                Signal = signal,
                TransitionFrequency = transitionFrequency,
                TransitionSlope = transitionSlope,
                DBGain = dbGain
            };

            wrapper.TransitionFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.TransitionSlopeInlet.DefaultValue = DEFAULT_TRANSITION_SLOPE;
            wrapper.DBGainInlet.DefaultValue = DEFAULT_DB_GAIN;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Hold_OperatorWrapper Hold(Outlet signal = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Hold,
                new DimensionEnum[] { DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Hold_OperatorWrapper(op)
            {
                Signal = signal,
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public If_OperatorWrapper If(Outlet condition = null, Outlet then = null, Outlet @else = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.If,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new If_OperatorWrapper(op)
            {
                Condition = condition,
                Then = then,
                Else = @else
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public LessThan_OperatorWrapper LessThan(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.LessThan,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new LessThan_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public LessThanOrEqual_OperatorWrapper LessThanOrEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.LessThanOrEqual,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new LessThanOrEqual_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Loop,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

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

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public LowPassFilter_OperatorWrapper LowPassFilter(
            Outlet signal = null, 
            Outlet maxFrequency = null,
            Outlet bandWidth = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.LowPassFilter,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            var wrapper = new LowPassFilter_OperatorWrapper(op)
            {
                Signal = signal,
                MaxFrequency = maxFrequency,
                BandWidth = bandWidth
            };

            wrapper.MaxFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public LowShelfFilter_OperatorWrapper LowShelfFilter(
            Outlet signal = null,
            Outlet transitionFrequency = null,
            Outlet transitionSlope = null,
            Outlet dbGain = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.LowShelfFilter,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            var wrapper = new LowShelfFilter_OperatorWrapper(op)
            {
                Signal = signal,
                TransitionFrequency = transitionFrequency,
                TransitionSlope = transitionSlope,
                DBGain = dbGain
            };

            wrapper.TransitionFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.TransitionSlopeInlet.DefaultValue = DEFAULT_TRANSITION_SLOPE;
            wrapper.DBGainInlet.DefaultValue = DEFAULT_DB_GAIN;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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
            interpolation = interpolation ?? ResampleInterpolationTypeEnum.Block;
            dimension = dimension ?? DimensionEnum.Undefined;

            Operator op = CreateOperatorBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.InletsToDimension, operands);
            if (dimension.HasValue)
            {
                op.SetStandardDimensionEnum(dimension.Value, _repositories.DimensionRepository);
            }
            op.CustomDimensionName = customDimension;

            var wrapper = new InletsToDimension_OperatorWrapper(op)
            {
                InterpolationType = interpolation.Value
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
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

            Operator op = CreateOperatorBase(
                OperatorTypeEnum.DimensionToOutlets,
                new DimensionEnum[] { DimensionEnum.Undefined },
                Enumerable.Repeat(DimensionEnum.Undefined, outletCount.Value).ToArray());

            if (dimension.HasValue)
            {
                op.SetStandardDimensionEnum(dimension.Value, _repositories.DimensionRepository);
            }
            op.CustomDimensionName = customDimension;

            var wrapper = new DimensionToOutlets_OperatorWrapper(op)
            {
                Operand = operand
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public MaxOverInlets_OperatorWrapper MaxOverInlets(params Outlet[] operands)
        {
            return MaxOverInlets((IList<Outlet>)operands);
        }

        public MaxOverInlets_OperatorWrapper MaxOverInlets(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = CreateOperatorBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.MaxOverInlets, operands);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.MaxFollower,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new MaxFollower_OperatorWrapper(op)
            {
                Signal = signal,
                SliceLength = sliceLength,
                SampleCount = sampleCount
            };

            wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
            wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.MaxOverDimension,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new MaxOverDimension_OperatorWrapper(op)
            {
                Signal = signal,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public MinOverInlets_OperatorWrapper MinOverInlets(params Outlet[] operands)
        {
            return MinOverInlets((IList<Outlet>)operands);
        }

        public MinOverInlets_OperatorWrapper MinOverInlets(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = CreateOperatorBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.MinOverInlets, operands);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.MinFollower,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new MinFollower_OperatorWrapper(op)
            {
                Signal = signal,
                SliceLength = sliceLength,
                SampleCount = sampleCount
            };

            wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
            wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.MinOverDimension,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new MinOverDimension_OperatorWrapper(op)
            {
                Signal = signal,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Multiply_OperatorWrapper Multiply(params Outlet[] operands)
        {
            return Multiply((IList<Outlet>)operands);
        }

        public Multiply_OperatorWrapper Multiply(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = CreateOperatorBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.Multiply, operands);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            var wrapper = new Multiply_OperatorWrapper(op);
            return wrapper;
        }

        public MultiplyWithOrigin_OperatorWrapper MultiplyWithOrigin(Outlet a = null, Outlet b = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.MultiplyWithOrigin,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new MultiplyWithOrigin_OperatorWrapper(op)
            {
                A = a,
                B = b,
                Origin = origin
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Negative_OperatorWrapper Negative(Outlet x = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Negative,
                new DimensionEnum[] { DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Negative_OperatorWrapper(op)
            {
                X = x,
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Noise_OperatorWrapper Noise(
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Noise,
                new DimensionEnum[0],
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Noise_OperatorWrapper(op);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Not_OperatorWrapper Not(Outlet x = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Not,
                new DimensionEnum[] { DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Not_OperatorWrapper(op)
            {
                X = x,
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public NotchFilter_OperatorWrapper NotchFilter(
            Outlet signal = null, 
            Outlet centerFrequency = null, 
            Outlet bandWidth = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.NotchFilter,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            var wrapper = new NotchFilter_OperatorWrapper(op)
            {
                Signal = signal,
                CenterFrequency = centerFrequency,
                BandWidth = bandWidth
            };

            wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public NotEqual_OperatorWrapper NotEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.NotEqual,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new NotEqual_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Number_OperatorWrapper Number(double number = 0)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Number,
                new DimensionEnum[0],
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Number_OperatorWrapper(op)
            {
                Number = number
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public OneOverX_OperatorWrapper OneOverX(Outlet x = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.OneOverX,
                new DimensionEnum[] { DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new OneOverX_OperatorWrapper(op)
            {
                X = x,
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Or_OperatorWrapper Or(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Or, 
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Or_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet()
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.PatchInlet,
                new DimensionEnum[] { DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new PatchInlet_OperatorWrapper(op)
            {
                // You have to set this property or the wrapper's ListIndex getter would crash.
                ListIndex = 0
            };

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.WrappedOperator);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(DimensionEnum dimension)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();

            wrapper.Inlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);

            VoidResult result = ValidateOperatorNonRecursive(wrapper.WrappedOperator);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Name = name;

            VoidResult result = ValidateOperatorNonRecursive(wrapper.WrappedOperator);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(string name, double defaultValue)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Name = name;
            wrapper.Inlet.DefaultValue = defaultValue;

            VoidResult result = ValidateOperatorNonRecursive(wrapper.WrappedOperator);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(DimensionEnum dimension, double defaultValue)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            Inlet patchInletInlet = wrapper.Inlet;
            patchInletInlet.SetDimensionEnum(dimension, _repositories.DimensionRepository);
            patchInletInlet.DefaultValue = defaultValue;

            VoidResult result = ValidateOperatorNonRecursive(wrapper.WrappedOperator);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.PatchOutlet,
                new DimensionEnum[] { DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new PatchOutlet_OperatorWrapper(op)
            {
                Input = input,
                // You have to set this property two or the wrapper's ListIndex property getter would crash.
                ListIndex = 0,
            };

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.WrappedOperator);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(DimensionEnum dimension, Outlet input = null)
        {
            PatchOutlet_OperatorWrapper wrapper = PatchOutlet(input);
            wrapper.Result.SetDimensionEnum(dimension, _repositories.DimensionRepository);

            VoidResult result = ValidateOperatorNonRecursive(wrapper.WrappedOperator);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(string name, Outlet input = null)
        {
            PatchOutlet_OperatorWrapper wrapper = PatchOutlet(input);
            wrapper.Name = name;

            VoidResult result = ValidateOperatorNonRecursive(wrapper.WrappedOperator);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PeakingEQFilter_OperatorWrapper PeakingEQFilter(
            Outlet signal = null,
            Outlet centerFrequency = null,
            Outlet bandWidth = null,
            Outlet dbGain = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.PeakingEQFilter,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            var wrapper = new PeakingEQFilter_OperatorWrapper(op)
            {
                Signal = signal,
                CenterFrequency = centerFrequency,
                BandWidth = bandWidth,
                DBGain = dbGain
            };

            wrapper.CenterFrequencyInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;
            wrapper.DBGainInlet.DefaultValue = DEFAULT_DB_GAIN;
            wrapper.BandWidthInlet.DefaultValue = DEFAULT_BAND_WIDTH;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Power_OperatorWrapper Power(Outlet @base = null, Outlet exponent = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Power,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Power_OperatorWrapper(op)
            {
                Base = @base,
                Exponent = exponent
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Pulse_OperatorWrapper Pulse(
            Outlet frequency = null, 
            Outlet width = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Pulse,
                new DimensionEnum[] { DimensionEnum.Frequency, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Pulse_OperatorWrapper(op)
            {
                Frequency = frequency,
                Width = width,
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;
            wrapper.WidthInlet.DefaultValue = DEFAULT_PULSE_WIDTH;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PulseTrigger_OperatorWrapper PulseTrigger(Outlet calculation = null, Outlet reset = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.PulseTrigger,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new PulseTrigger_OperatorWrapper(op)
            {
                PassThroughInput = calculation,
                Reset = reset
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Random_OperatorWrapper Random(
            Outlet rate = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Random,
                new DimensionEnum[] { DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Random_OperatorWrapper(op)
            {
                Rate = rate,
                InterpolationType = ResampleInterpolationTypeEnum.Block
            };

            wrapper.RateInlet.DefaultValue = DEFAULT_RANDOM_RATE;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Range_OperatorWrapper Range(
            Outlet from = null, 
            Outlet till = null, 
            Outlet step = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Range,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Range_OperatorWrapper(op)
            {
                From = from,
                Till = till,
                Step = step
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_RANGE_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_RANGE_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Interpolate_OperatorWrapper Interpolate(
            Outlet signal = null, 
            Outlet samplingRate = null, 
            ResampleInterpolationTypeEnum interpolationType = ResampleInterpolationTypeEnum.CubicSmoothSlope,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Interpolate,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Interpolate_OperatorWrapper(op)
            {
                Signal = signal,
                SamplingRate = samplingRate,
                InterpolationType = interpolationType
            };

            wrapper.SamplingRateInlet.DefaultValue = DEFAULT_FILTER_FREQUENCY;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Reset_OperatorWrapper Reset(Outlet operand = null, int? listIndex = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Reset,
                new DimensionEnum[] { DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Reset_OperatorWrapper(op)
            {
                PassThroughInput = operand,
                ListIndex = listIndex
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Reverse_OperatorWrapper Reverse(
            Outlet signal = null, 
            Outlet speed = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Reverse,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Reverse_OperatorWrapper(op)
            {
                Signal = signal,
                Speed = speed
            };

            wrapper.SpeedInlet.DefaultValue = DEFAULT_REVERSE_SPEED;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Round_OperatorWrapper Round(Outlet signal = null, Outlet step = null, Outlet offset = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Round,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            var wrapper = new Round_OperatorWrapper(op)
            {
                Signal = signal,
                Step = step,
                Offset = offset
            };

            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Sample_OperatorWrapper Sample(
            Sample sample = null, 
            Outlet frequency = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Sample,
                new DimensionEnum[] { DimensionEnum.Frequency },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Sample_OperatorWrapper(op, _repositories.SampleRepository)
            {
                Frequency = frequency,
                SampleID = sample?.ID
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public SawDown_OperatorWrapper SawDown(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.SawDown,
                new DimensionEnum[] { DimensionEnum.Frequency },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SawDown_OperatorWrapper(op)
            {
                Frequency = frequency
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public SawUp_OperatorWrapper SawUp(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.SawUp,
                new DimensionEnum[] { DimensionEnum.Frequency },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SawUp_OperatorWrapper(op)
            {
                Frequency = frequency,
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Scaler_OperatorWrapper Scaler(
            Outlet signal = null,
            Outlet sourceValueA = null,
            Outlet sourceValueB = null,
            Outlet targetValueA = null,
            Outlet targetValueB = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Scaler,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            var wrapper = new Scaler_OperatorWrapper(op)
            {
                Signal = signal,
                SourceValueA = sourceValueA,
                SourceValueB = sourceValueB,
                TargetValueA = targetValueA,
                TargetValueB = targetValueB,
            };

            wrapper.SourceValueAInlet.DefaultValue = DEFAULT_SCALE_SOURCE_VALUE_A;
            wrapper.SourceValueBInlet.DefaultValue = DEFAULT_SCALE_SOURCE_VALUE_B;
            wrapper.TargetValueAInlet.DefaultValue = DEFAULT_SCALE_TARGET_VALUE_A;
            wrapper.TargetValueBInlet.DefaultValue = DEFAULT_SCALE_TARGET_VALUE_B;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Select_OperatorWrapper Select(
            Outlet signal = null, 
            Outlet position = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Select,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Select_OperatorWrapper(op)
            {
                Signal = signal,
                Position = position
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public SetDimension_OperatorWrapper SetDimension(
            Outlet calculation = null, 
            Outlet value = null,
            DimensionEnum standardDimension = DimensionEnum.Undefined,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.SetDimension,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SetDimension_OperatorWrapper(op)
            {
                PassThroughInput = calculation,
                Value = value
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Shift_OperatorWrapper Shift(Outlet signal = null, Outlet difference = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Shift,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Shift_OperatorWrapper(op)
            {
                Signal = signal,
                Difference = difference
            };

            wrapper.DifferenceInlet.DefaultValue = DEFAULT_DIFFERENCE;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Sine_OperatorWrapper Sine(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Sine,
                new DimensionEnum[] { DimensionEnum.Frequency},
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Sine_OperatorWrapper(op)
            {
                Frequency = frequency
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public SortOverInlets_OperatorWrapper SortOverInlets(params Outlet[] operands)
        {
            return SortOverInlets((IList<Outlet>)operands);
        }

        public SortOverInlets_OperatorWrapper SortOverInlets(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = CreateOperatorBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum.SortOverInlets, operands);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            var wrapper = new SortOverInlets_OperatorWrapper(op);
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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.SortOverDimension,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SortOverDimension_OperatorWrapper(op)
            {
                Signal = signal,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Spectrum_OperatorWrapper Spectrum(
            Outlet signal = null, 
            Outlet start = null, 
            Outlet end = null, 
            Outlet frequencyCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Spectrum,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Volume });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Spectrum_OperatorWrapper(op)
            {
                Signal = signal,
                Start = start,
                End = end,
                FrequencyCount = frequencyCount
            };

            wrapper.StartInlet.DefaultValue = DEFAULT_START_TIME;
            wrapper.EndInlet.DefaultValue = DEFAULT_END_TIME;
            wrapper.FrequencyCountInlet.DefaultValue = DEFAULT_SPECTRUM_FREQUENCY_COUNT;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Square_OperatorWrapper Square(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Square,
                new DimensionEnum[] { DimensionEnum.Frequency },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Square_OperatorWrapper(op)
            {
                Frequency = frequency,
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Squash_OperatorWrapper Squash(
            Outlet signal = null,
            Outlet factor = null,
            Outlet origin = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Squash,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Squash_OperatorWrapper(op)
            {
                Signal = signal,
                Factor = factor,
                Origin = origin
            };

            wrapper.FactorInlet.DefaultValue = DEFAULT_FACTOR;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Stretch_OperatorWrapper Stretch(
            Outlet signal = null, 
            Outlet factor = null, 
            Outlet origin = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Stretch,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Stretch_OperatorWrapper(op)
            {
                Signal = signal,
                Factor = factor,
                Origin = origin
            };

            wrapper.FactorInlet.DefaultValue = DEFAULT_FACTOR;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Subtract_OperatorWrapper Subtract(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Subtract,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new Subtract_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public SumFollower_OperatorWrapper SumFollower(
            Outlet signal = null,
            Outlet sliceLength = null,
            Outlet sampleCount = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.SumFollower,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SumFollower_OperatorWrapper(op)
            {
                Signal = signal,
                SliceLength = sliceLength,
                SampleCount = sampleCount
            };

            wrapper.SliceLengthInlet.DefaultValue = DEFAULT_SLICE_LENGTH;
            wrapper.SampleCountInlet.DefaultValue = DEFAULT_SAMPLE_COUNT;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.SumOverDimension,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new SumOverDimension_OperatorWrapper(op)
            {
                Signal = signal,
                From = from,
                Till = till,
                Step = step,
                CollectionRecalculation = collectionRecalculation
            };

            wrapper.FromInlet.DefaultValue = DEFAULT_AGGREGATE_FROM;
            wrapper.TillInlet.DefaultValue = DEFAULT_AGGREGATE_TILL;
            wrapper.StepInlet.DefaultValue = DEFAULT_STEP;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public TimePower_OperatorWrapper TimePower(
            Outlet signal = null, 
            Outlet exponent = null, 
            Outlet origin = null,
            DimensionEnum standardDimension = DimensionEnum.Time,
            string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.TimePower,
                new DimensionEnum[] { DimensionEnum.Signal, DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new TimePower_OperatorWrapper(op)
            {
                Signal = signal,
                Exponent = exponent,
                Origin = origin
            };

            wrapper.ExponentInlet.DefaultValue = DEFAULT_EXPONENT;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public ToggleTrigger_OperatorWrapper ToggleTrigger(Outlet calculation = null, Outlet reset = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.ToggleTrigger,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                new DimensionEnum[] { DimensionEnum.Undefined });

            var wrapper = new ToggleTrigger_OperatorWrapper(op)
            {
                PassThroughInput = calculation,
                Reset = reset
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Triangle_OperatorWrapper Triangle(Outlet frequency = null, DimensionEnum standardDimension = DimensionEnum.Time, string customDimension = null)
        {
            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Triangle,
                new DimensionEnum[] { DimensionEnum.Frequency },
                new DimensionEnum[] { DimensionEnum.Signal });

            op.SetStandardDimensionEnum(standardDimension, _repositories.DimensionRepository);
            op.CustomDimensionName = customDimension;

            var wrapper = new Triangle_OperatorWrapper(op)
            {
                Frequency = frequency
            };

            wrapper.FrequencyInlet.DefaultValue = DEFAULT_FREQUENCY;

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Unbundle_OperatorWrapper Unbundle(Outlet operand, DimensionEnum standardDimension, string customDimension, int outletCount)
            => UnbundlePrivate(operand, standardDimension, customDimension, outletCount);

        public Unbundle_OperatorWrapper Unbundle(Outlet operand, DimensionEnum standardDimension, string customDimension)
            => UnbundlePrivate(operand, standardDimension, customDimension, null);

        public Unbundle_OperatorWrapper Unbundle(Outlet operand, DimensionEnum standardDimension, int outletCount)
            => UnbundlePrivate(operand, standardDimension, null, outletCount);

        public Unbundle_OperatorWrapper Unbundle(Outlet operand, DimensionEnum standardDimension)
            => UnbundlePrivate(operand, standardDimension, null, null);

        public Unbundle_OperatorWrapper Unbundle(Outlet operand, string customDimension, int outletCount)
            => UnbundlePrivate(operand, null, customDimension, outletCount);

        public Unbundle_OperatorWrapper Unbundle(Outlet operand, string customDimension)
            => UnbundlePrivate(operand, null, customDimension, null);

        public Unbundle_OperatorWrapper Unbundle(Outlet operand, int outletCount)
            => UnbundlePrivate(operand, null, null, outletCount);

        public Unbundle_OperatorWrapper Unbundle(Outlet operand)
            => UnbundlePrivate(operand, null, null, null);

        public Unbundle_OperatorWrapper Unbundle(DimensionEnum standardDimension, string customDimension, int outletCount)
            => UnbundlePrivate(null, standardDimension, customDimension, outletCount);

        public Unbundle_OperatorWrapper Unbundle(DimensionEnum standardDimension, string customDimension)
            => UnbundlePrivate(null, standardDimension, customDimension, null);

        public Unbundle_OperatorWrapper Unbundle(DimensionEnum standardDimension, int outletCount)
            => UnbundlePrivate(null, standardDimension, null, outletCount);

        public Unbundle_OperatorWrapper Unbundle(DimensionEnum standardDimension)
            => UnbundlePrivate(null, standardDimension, null, null);

        public Unbundle_OperatorWrapper Unbundle(int outletCount, string customDimension)
            => UnbundlePrivate(null, null, customDimension, outletCount);

        public Unbundle_OperatorWrapper Unbundle(string customDimension)
            => UnbundlePrivate(null, null, customDimension, null);

        public Unbundle_OperatorWrapper Unbundle(int outletCount)
            => UnbundlePrivate(null, null, null, outletCount);

        public Unbundle_OperatorWrapper Unbundle()
            => UnbundlePrivate(null, null, null, null);

        private Unbundle_OperatorWrapper UnbundlePrivate(Outlet operand, DimensionEnum? dimension, string customDimension, int? outletCount)
        {
            dimension = dimension ?? DimensionEnum.Undefined;
            outletCount = outletCount ?? 1;

            if (outletCount < 1) throw new LessThanException(() => outletCount, 1);

            Operator op = CreateOperatorBase(
                OperatorTypeEnum.Unbundle,
                new DimensionEnum[] { DimensionEnum.Undefined },
                Enumerable.Repeat(DimensionEnum.Undefined, outletCount.Value).ToArray());

            if (dimension.HasValue)
            {
                op.SetStandardDimensionEnum(dimension.Value, _repositories.DimensionRepository);
            }
            op.CustomDimensionName = customDimension;

            var wrapper = new Unbundle_OperatorWrapper(op)
            {
                Operand = operand,
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

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

        private void ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(Operator op)
        {
            ISideEffect sideEffect2 = new Operator_SideEffect_GeneratePatchInletListIndex(op);
            sideEffect2.Execute();

            ISideEffect sideEffect3 = new Operator_SideEffect_GeneratePatchOutletListIndex(op);
            sideEffect3.Execute();

            ISideEffect sideEffect4 = new Patch_SideEffect_UpdateDependentCustomOperators(op.Patch, _repositories);
            sideEffect4.Execute();
        }

        // Generic methods for operator creation

        /// <param name="variableInletOrOutletCount">
        /// Applies to operators with a variable amount of inlets or outlets,
        /// such as the Adder operator and the Bundle operator.
        /// </param>
        public Operator CreateOperator(OperatorTypeEnum operatorTypeEnum, int variableInletOrOutletCount = 16)
        {
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Absolute: return Absolute();
                case OperatorTypeEnum.Add: return Add(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.AllPassFilter: return AllPassFilter();
                case OperatorTypeEnum.And: return And();
                case OperatorTypeEnum.AverageOverInlets: return AverageOverInlets(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.AverageFollower: return AverageFollower();
                case OperatorTypeEnum.AverageOverDimension: return AverageOverDimension();
                case OperatorTypeEnum.BandPassFilterConstantPeakGain: return BandPassFilterConstantPeakGain();
                case OperatorTypeEnum.BandPassFilterConstantTransitionGain: return BandPassFilterConstantTransitionGain();
                case OperatorTypeEnum.Bundle: return Bundle(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.ChangeTrigger: return ChangeTrigger();
                case OperatorTypeEnum.Cache: return Cache();
                case OperatorTypeEnum.ClosestOverInlets: return ClosestOverInlets(null, new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.ClosestOverInletsExp: return ClosestOverInletsExp(null, new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.ClosestOverDimension: return ClosestOverDimension();
                case OperatorTypeEnum.ClosestOverDimensionExp: return ClosestOverDimensionExp();
                case OperatorTypeEnum.Curve: return Curve();
                case OperatorTypeEnum.CustomOperator: return CustomOperator();
                case OperatorTypeEnum.GetDimension: return GetDimension();
                case OperatorTypeEnum.Divide: return Divide();
                case OperatorTypeEnum.Equal: return Equal();
                case OperatorTypeEnum.Exponent: return Exponent();
                case OperatorTypeEnum.GreaterThan: return GreaterThan();
                case OperatorTypeEnum.GreaterThanOrEqual: return GreaterThanOrEqual();
                case OperatorTypeEnum.HighShelfFilter: return HighShelfFilter();
                case OperatorTypeEnum.HighPassFilter: return HighPassFilter();
                case OperatorTypeEnum.Hold: return Hold();
                case OperatorTypeEnum.If: return If();
                case OperatorTypeEnum.LessThan: return LessThan();
                case OperatorTypeEnum.LessThanOrEqual: return LessThanOrEqual();
                case OperatorTypeEnum.Loop: return Loop();
                case OperatorTypeEnum.LowPassFilter: return LowPassFilter();
                case OperatorTypeEnum.LowShelfFilter: return LowShelfFilter();
                case OperatorTypeEnum.InletsToDimension: return InletsToDimension(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.DimensionToOutlets: return DimensionToOutlets(null, variableInletOrOutletCount);
                case OperatorTypeEnum.MaxOverDimension: return MaxOverDimension();
                case OperatorTypeEnum.MaxOverInlets: return MaxOverInlets(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.MaxFollower: return MaxFollower();
                case OperatorTypeEnum.MinOverDimension: return MinOverDimension();
                case OperatorTypeEnum.MinOverInlets: return MinOverInlets(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.MinFollower: return MinFollower();
                case OperatorTypeEnum.Multiply: return Multiply(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.MultiplyWithOrigin: return MultiplyWithOrigin();
                case OperatorTypeEnum.Noise: return Noise();
                case OperatorTypeEnum.Not: return Not();
                case OperatorTypeEnum.NotchFilter: return NotchFilter();
                case OperatorTypeEnum.NotEqual: return NotEqual();
                case OperatorTypeEnum.Negative: return Negative();
                case OperatorTypeEnum.Number: return Number();
                case OperatorTypeEnum.OneOverX: return OneOverX();
                case OperatorTypeEnum.Or: return Or();
                case OperatorTypeEnum.PatchInlet: return PatchInlet();
                case OperatorTypeEnum.PatchOutlet: return PatchOutlet();
                case OperatorTypeEnum.PeakingEQFilter: return PeakingEQFilter();
                case OperatorTypeEnum.Power: return Power();
                case OperatorTypeEnum.Pulse: return Pulse();
                case OperatorTypeEnum.PulseTrigger: return PulseTrigger();
                case OperatorTypeEnum.Random: return Random();
                case OperatorTypeEnum.Range: return Range();
                case OperatorTypeEnum.Interpolate: return Interpolate();
                case OperatorTypeEnum.Reset: return Reset();
                case OperatorTypeEnum.Reverse: return Reverse();
                case OperatorTypeEnum.Round: return Round();
                case OperatorTypeEnum.Sample: return Sample();
                case OperatorTypeEnum.SawDown: return SawDown();
                case OperatorTypeEnum.SawUp: return SawUp();
                case OperatorTypeEnum.Scaler: return Scaler();
                case OperatorTypeEnum.Select: return Select();
                case OperatorTypeEnum.SetDimension: return SetDimension();
                case OperatorTypeEnum.Shift: return Shift();
                case OperatorTypeEnum.Sine: return Sine();
                case OperatorTypeEnum.SortOverInlets: return SortOverInlets(new Outlet[variableInletOrOutletCount]);
                case OperatorTypeEnum.SortOverDimension: return SortOverDimension();
                case OperatorTypeEnum.Spectrum: return Spectrum();
                case OperatorTypeEnum.Square: return Square();
                case OperatorTypeEnum.Squash: return Squash();
                case OperatorTypeEnum.Stretch: return Stretch();
                case OperatorTypeEnum.Subtract: return Subtract();
                case OperatorTypeEnum.SumOverDimension: return SumOverDimension();
                case OperatorTypeEnum.SumFollower: return SumFollower();
                case OperatorTypeEnum.TimePower: return TimePower();
                case OperatorTypeEnum.ToggleTrigger: return ToggleTrigger();
                case OperatorTypeEnum.Triangle: return Triangle();
                case OperatorTypeEnum.Unbundle: return Unbundle(null, variableInletOrOutletCount);

                default:
                    throw new Exception(String.Format("OperatorTypeEnum '{0}' not supported by the PatchManager.CreateOperator method.", operatorTypeEnum));
            }
        }

        private Operator CreateOperatorBase(
            OperatorTypeEnum operatorTypeEnum,
            IList<DimensionEnum> inletDimensionEnums,
            IList<DimensionEnum> outletDimensionEnums)
        {
            int inletCount = inletDimensionEnums.Count;
            int outletCount = outletDimensionEnums.Count;

            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(operatorTypeEnum, _repositories.OperatorTypeRepository);

            op.LinkTo(Patch);
            _repositories.OperatorRepository.Insert(op);

            for (int i = 0; i < inletCount; i++)
            {
                Inlet inlet = CreateInlet(op);
                inlet.ListIndex = i;

                DimensionEnum dimensionEnum = inletDimensionEnums[i];
                inlet.SetDimensionEnum(dimensionEnum, _repositories.DimensionRepository);
            }

            for (int i = 0; i < outletCount; i++)
            {
                Outlet outlet = CreateOutlet(op);
                outlet.ListIndex = i;

                DimensionEnum dimensionEnum = outletDimensionEnums[i];
                outlet.SetDimensionEnum(dimensionEnum, _repositories.DimensionRepository);
            }

            return op;
        }

        private Operator CreateOperatorBase_WithVariableInletCountAndOneOutlet(OperatorTypeEnum operatorTypeEnum, IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(operatorTypeEnum, _repositories.OperatorTypeRepository);
            _repositories.OperatorRepository.Insert(op);

            for (int i = 0; i < operands.Count; i++)
            {
                var inlet = new Inlet();
                inlet.ID = _repositories.IDRepository.GetID();
                inlet.ListIndex = i;
                inlet.LinkTo(op);
                _repositories.InletRepository.Insert(inlet);

                Outlet operand = operands[i];
                inlet.LinkTo(operand);
            }

            var outlet = new Outlet();
            outlet.ID = _repositories.IDRepository.GetID();
            outlet.LinkTo(op);
            _repositories.OutletRepository.Insert(outlet);

            op.LinkTo(Patch);

            return op;
        }
    }
}
