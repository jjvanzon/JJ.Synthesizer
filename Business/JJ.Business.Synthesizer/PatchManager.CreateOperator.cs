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
using JJ.Framework.Common.Exceptions;

namespace JJ.Business.Synthesizer
{
    public partial class PatchManager
    {
        public Absolute_OperatorWrapper Absolute(Outlet x = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Absolute, inletCount: 1, outletCount: 1);

            var wrapper = new Absolute_OperatorWrapper(op)
            {
                X = x,
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Add_OperatorWrapper Add(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Add, inletCount: 2, outletCount: 1);

            var wrapper = new Add_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Adder_OperatorWrapper Adder(params Outlet[] operands)
        {
            return Adder((IList<Outlet>)operands);
        }

        public Adder_OperatorWrapper Adder(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(OperatorTypeEnum.Adder, _repositories.OperatorTypeRepository);
            _repositories.OperatorRepository.Insert(op);

            for (int i = 0; i < operands.Count; i++)
            {
                var inlet = new Inlet();
                inlet.ID = _repositories.IDRepository.GetID();
                inlet.ListIndex = i;
                inlet.LinkTo(op);
                _repositories.InletRepository.Insert(inlet);

                Outlet operand = operands[i];
                inlet.InputOutlet = operand;
            }

            var outlet = new Outlet();
            outlet.ID = _repositories.IDRepository.GetID();
            outlet.LinkTo(op);
            _repositories.OutletRepository.Insert(outlet);

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            var wrapper = new Adder_OperatorWrapper(op);
            return wrapper;
        }

        public And_OperatorWrapper And(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.And, inletCount: 2, outletCount: 1);

            var wrapper = new And_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Average_OperatorWrapper Average(
            Outlet signal = null, 
            Outlet timeSliceDuration = null, 
            Outlet sampleCount = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Average, inletCount: 3, outletCount: 1);

            var wrapper = new Average_OperatorWrapper(op)
            {
                Signal = signal,
                TimeSliceDuration = timeSliceDuration,
                SampleCount = sampleCount
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Bundle_OperatorWrapper Bundle(params Outlet[] operands)
        {
            return Bundle((IList<Outlet>)operands);
        }

        public Bundle_OperatorWrapper Bundle(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(OperatorTypeEnum.Bundle, _repositories.OperatorTypeRepository);
            _repositories.OperatorRepository.Insert(op);

            for (int i = 0; i < operands.Count; i++)
            {
                var inlet = new Inlet();
                inlet.ID = _repositories.IDRepository.GetID();
                inlet.ListIndex = i;
                inlet.LinkTo(op);
                _repositories.InletRepository.Insert(inlet);

                Outlet operand = operands[i];
                inlet.InputOutlet = operand;
            }

            var outlet = new Outlet();
            outlet.ID = _repositories.IDRepository.GetID();
            outlet.LinkTo(op);
            _repositories.OutletRepository.Insert(outlet);

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            var wrapper = new Bundle_OperatorWrapper(op);
            return wrapper;
        }

        public ChangeTrigger_OperatorWrapper ChangeTrigger(Outlet calculation = null, Outlet reset = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.ChangeTrigger, inletCount: 2, outletCount: 1);

            var wrapper = new ChangeTrigger_OperatorWrapper(op)
            {
                Calculation = calculation,
                Reset = reset
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Cache_OperatorWrapper Cache(
            Outlet signal = null,
            Outlet startTime = null,
            Outlet endTime = null,
            Outlet samplingRate = null, 
            InterpolationTypeEnum interpolationTypeEnum = InterpolationTypeEnum.Line,
            SpeakerSetupEnum speakerSetupEnum = SpeakerSetupEnum.Mono)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Cache, inletCount: 4, outletCount: 1);

            var wrapper = new Cache_OperatorWrapper(op)
            {
                Signal = signal,
                StartTime = startTime,
                EndTime = endTime,
                SamplingRate = samplingRate,
                InterpolationTypeEnum = interpolationTypeEnum,
                SpeakerSetupEnum = speakerSetupEnum
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Curve_OperatorWrapper Curve(Curve curve = null, DimensionEnum dimension = DimensionEnum.Time)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Curve, inletCount: 0, outletCount: 1);

            var wrapper = new Curve_OperatorWrapper(op, _repositories.CurveRepository);
            wrapper.Dimension = dimension;

            if (curve != null)
            {
                wrapper.CurveID = curve.ID;
            }

            op.LinkTo(Patch);

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

            var wrapper = new CustomOperator_OperatorWrapper(op, _repositories.PatchRepository);

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

        public Delay_OperatorWrapper Delay(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Delay, inletCount: 2, outletCount: 1);

            var wrapper = new Delay_OperatorWrapper(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Divide_OperatorWrapper Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Divide, inletCount: 3, outletCount: 1);

            var wrapper = new Divide_OperatorWrapper(op)
            {
                Numerator = numerator,
                Denominator = denominator,
                Origin = origin
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Earlier_OperatorWrapper Earlier(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Earlier, inletCount: 2, outletCount: 1);

            var wrapper = new Earlier_OperatorWrapper(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Equal_OperatorWrapper Equal(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Equal, inletCount: 2, outletCount: 1);

            var wrapper = new Equal_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Exponent_OperatorWrapper Exponent(Outlet low = null, Outlet high = null, Outlet ratio = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Exponent, inletCount: 3, outletCount: 1);

            var wrapper = new Exponent_OperatorWrapper(op)
            {
                Low = low,
                High = high,
                Ratio = ratio
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Filter_OperatorWrapper Filter(
            FilterTypeEnum filterTypeEnum = FilterTypeEnum.LowPassFilter,
            Outlet signal = null,
            Outlet frequency = null,
            Outlet bandWidth = null,
            Outlet dbGain = null,
            Outlet shelfSlope = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Filter, inletCount: 5, outletCount: 1);

            var wrapper = new Filter_OperatorWrapper(op)
            {
                FilterTypeEnum = filterTypeEnum,
                Signal = signal, 
                Frequency = frequency,
                BandWidth = bandWidth,
                DBGain = dbGain,
                ShelfSlope = shelfSlope
            };

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public GetDimension_OperatorWrapper GetDimension(DimensionEnum dimension = DimensionEnum.Undefined)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.GetDimension, inletCount: 0, outletCount: 1);

            var wrapper = new GetDimension_OperatorWrapper(op)
            {
                Dimension = dimension
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public GreaterThan_OperatorWrapper GreaterThan(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.GreaterThan, inletCount: 2, outletCount: 1);

            var wrapper = new GreaterThan_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public GreaterThanOrEqual_OperatorWrapper GreaterThanOrEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.GreaterThanOrEqual, inletCount: 2, outletCount: 1);

            var wrapper = new GreaterThanOrEqual_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public HighPassFilter_OperatorWrapper HighPassFilter(Outlet signal = null, Outlet minFrequency = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.HighPassFilter, inletCount: 2, outletCount: 1);

            var wrapper = new HighPassFilter_OperatorWrapper(op)
            {
                Signal = signal,
                MinFrequency = minFrequency
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public If_OperatorWrapper If(Outlet condition = null, Outlet then = null, Outlet @else = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.If, inletCount: 3, outletCount: 1);

            var wrapper = new If_OperatorWrapper(op)
            {
                Condition = condition,
                Then = then,
                Else = @else
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public LessThan_OperatorWrapper LessThan(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.LessThan, inletCount: 2, outletCount: 1);

            var wrapper = new LessThan_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public LessThanOrEqual_OperatorWrapper LessThanOrEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.LessThanOrEqual, inletCount: 2, outletCount: 1);

            var wrapper = new LessThanOrEqual_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            op.LinkTo(Patch);

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
            Outlet noteDuration = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Loop, inletCount: 6, outletCount: 1);

            var wrapper = new Loop_OperatorWrapper(op)
            {
                Signal = signal,
                Skip = skip,
                LoopStartMarker = loopStartMarker,
                LoopEndMarker = loopEndMarker,
                ReleaseEndMarker = releaseEndMarker,
                NoteDuration = noteDuration
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public LowPassFilter_OperatorWrapper LowPassFilter(Outlet signal = null, Outlet maxFrequency = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.LowPassFilter, inletCount: 2, outletCount: 1);

            var wrapper = new LowPassFilter_OperatorWrapper(op)
            {
                Signal = signal,
                MaxFrequency = maxFrequency
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Maximum_OperatorWrapper Maximum(
            Outlet signal = null,
            Outlet timeSliceDuration = null,
            Outlet sampleCount = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Maximum, inletCount: 3, outletCount: 1);

            var wrapper = new Maximum_OperatorWrapper(op)
            {
                Signal = signal,
                TimeSliceDuration = timeSliceDuration,
                SampleCount = sampleCount
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Minimum_OperatorWrapper Minimum(
            Outlet signal = null,
            Outlet timeSliceDuration = null,
            Outlet sampleCount = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Minimum, inletCount: 3, outletCount: 1);

            var wrapper = new Minimum_OperatorWrapper(op)
            {
                Signal = signal,
                TimeSliceDuration = timeSliceDuration,
                SampleCount = sampleCount
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Multiply_OperatorWrapper Multiply(Outlet a = null, Outlet b = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Multiply, inletCount: 3, outletCount: 1);

            var wrapper = new Multiply_OperatorWrapper(op)
            {
                A = a,
                B = b,
                Origin = origin
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Narrower_OperatorWrapper Narrower(Outlet signal = null, Outlet factor = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Narrower, inletCount: 3, outletCount: 1);

            var wrapper = new Narrower_OperatorWrapper(op)
            {
                Signal = signal,
                Factor = factor,
                Origin = origin
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Negative_OperatorWrapper Negative(Outlet x = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Negative, inletCount: 1, outletCount: 1);

            var wrapper = new Negative_OperatorWrapper(op)
            {
                X = x,
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Noise_OperatorWrapper Noise()
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Noise, inletCount: 0, outletCount: 1);

            var wrapper = new Noise_OperatorWrapper(op);

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Not_OperatorWrapper Not(Outlet x = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Not, inletCount: 1, outletCount: 1);

            var wrapper = new Not_OperatorWrapper(op)
            {
                X = x,
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public NotEqual_OperatorWrapper NotEqual(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.NotEqual, inletCount: 2, outletCount: 1);

            var wrapper = new NotEqual_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Number_OperatorWrapper Number(double number = 0)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Number, inletCount: 0, outletCount: 1);

            var wrapper = new Number_OperatorWrapper(op)
            {
                Number = number
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public OneOverX_OperatorWrapper OneOverX(Outlet x = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.OneOverX, inletCount: 1, outletCount: 1);

            var wrapper = new OneOverX_OperatorWrapper(op)
            {
                X = x,
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Or_OperatorWrapper Or(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Or, inletCount: 2, outletCount: 1);

            var wrapper = new Or_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet()
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.PatchInlet, inletCount: 1, outletCount: 1);

            var wrapper = new PatchInlet_OperatorWrapper(op)
            {
                // You have to set this property or the wrapper's ListIndex getter would crash.
                ListIndex = 0,
            };

            op.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.WrappedOperator);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(DimensionEnum dimensionEnum)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            wrapper.Inlet.SetDimensionEnum(dimensionEnum, _repositories.DimensionRepository);

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

        public PatchInlet_OperatorWrapper PatchInlet(DimensionEnum dimensionEnum, double defaultValue)
        {
            PatchInlet_OperatorWrapper wrapper = PatchInlet();
            Inlet patchInletInlet = wrapper.Inlet;
            patchInletInlet.SetDimensionEnum(dimensionEnum, _repositories.DimensionRepository);
            patchInletInlet.DefaultValue = defaultValue;

            VoidResult result = ValidateOperatorNonRecursive(wrapper.WrappedOperator);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.PatchOutlet, inletCount: 1, outletCount: 1);

            var wrapper = new PatchOutlet_OperatorWrapper(op)
            {
                Input = input,
                // You have to set this property two or the wrapper's ListIndex property getter would crash.
                ListIndex = 0,
            };

            op.LinkTo(Patch);

            ExecuteSideEffectsForCreatingPatchInletOrPatchOutlet(wrapper.WrappedOperator);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(DimensionEnum dimensionEnum, Outlet input = null)
        {
            PatchOutlet_OperatorWrapper wrapper = PatchOutlet(input);
            wrapper.Result.SetDimensionEnum(dimensionEnum, _repositories.DimensionRepository);

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

        public Power_OperatorWrapper Power(Outlet @base = null, Outlet exponent = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Power, inletCount: 2, outletCount: 1);

            var wrapper = new Power_OperatorWrapper(op)
            {
                Base = @base,
                Exponent = exponent
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Pulse_OperatorWrapper Pulse(Outlet frequency = null, Outlet width = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Pulse, inletCount: 3, outletCount: 1);

            var wrapper = new Pulse_OperatorWrapper(op)
            {
                Frequency = frequency,
                Width = width,
                PhaseShift = phaseShift
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Random_OperatorWrapper Random(Outlet rate = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Random, inletCount: 2, outletCount: 1);

            var wrapper = new Random_OperatorWrapper(op)
            {
                Rate = rate,
                PhaseShift = phaseShift,
                ResampleInterpolationTypeEnum = ResampleInterpolationTypeEnum.Block
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Resample_OperatorWrapper Resample(
            Outlet signal = null, 
            Outlet samplingRate = null, 
            ResampleInterpolationTypeEnum interpolationType = ResampleInterpolationTypeEnum.CubicSmoothInclination)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Resample, inletCount: 2, outletCount: 1);

            var wrapper = new Resample_OperatorWrapper(op)
            {
                Signal = signal,
                SamplingRate = samplingRate,
                InterpolationType = interpolationType
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Reverse_OperatorWrapper Reverse(Outlet signal = null, Outlet speed = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Reverse, inletCount: 2, outletCount: 1);

            var wrapper = new Reverse_OperatorWrapper(op)
            {
                Signal = signal,
                Speed = speed,
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Reset_OperatorWrapper Reset(Outlet operand = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Reset, inletCount: 1, outletCount: 1);

            var wrapper = new Reset_OperatorWrapper(op)
            {
                Operand = operand
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Round_OperatorWrapper Round(Outlet signal = null, Outlet step = null, Outlet offset = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Round, inletCount: 3, outletCount: 1);

            var wrapper = new Round_OperatorWrapper(op)
            {
                Signal = signal,
                Step = step,
                Offset = offset
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Sample_OperatorWrapper Sample(Sample sample = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Sample, inletCount: 1, outletCount: 1);

            var wrapper = new Sample_OperatorWrapper(op, _repositories.SampleRepository);
            if (sample != null)
            {
                wrapper.SampleID = sample.ID;
            }

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public SawDown_OperatorWrapper SawDown(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SawDown, inletCount: 2, outletCount: 1);

            var wrapper = new SawDown_OperatorWrapper(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public SawUp_OperatorWrapper SawUp(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SawUp, inletCount: 2, outletCount: 1);

            var wrapper = new SawUp_OperatorWrapper(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            op.LinkTo(Patch);

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
            Operator op = CreateOperatorBase(OperatorTypeEnum.Scaler, inletCount: 5, outletCount: 1);

            var wrapper = new Scaler_OperatorWrapper(op)
            {
                Signal = signal,
                SourceValueA = sourceValueA,
                SourceValueB = sourceValueB,
                TargetValueA = targetValueA,
                TargetValueB = targetValueB,
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Select_OperatorWrapper Select(Outlet signal = null, Outlet position = null, DimensionEnum dimension = DimensionEnum.Time)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Select, inletCount: 2, outletCount: 1);

            var wrapper = new Select_OperatorWrapper(op)
            {
                Signal = signal,
                Position = position,
                Dimension = dimension
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public SetDimension_OperatorWrapper SetDimension(Outlet calculation = null, Outlet value = null, DimensionEnum dimension = DimensionEnum.Undefined)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SetDimension, inletCount: 2, outletCount: 1);

            var wrapper = new SetDimension_OperatorWrapper(op)
            {
                Calculation = calculation,
                Value = value,
                Dimension = dimension
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Shift_OperatorWrapper Shift(Outlet signal = null, Outlet difference = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Shift, inletCount: 2, outletCount: 1);

            var wrapper = new Shift_OperatorWrapper(op)
            {
                Signal = signal,
                Difference = difference
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Sine_OperatorWrapper Sine(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Sine, inletCount: 2, outletCount: 1);

            var wrapper = new Sine_OperatorWrapper(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public SlowDown_OperatorWrapper SlowDown(Outlet signal = null, Outlet factor = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SlowDown, inletCount: 2, outletCount: 1);

            var wrapper = new SlowDown_OperatorWrapper(op)
            {
                Signal = signal,
                Factor = factor,
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Spectrum_OperatorWrapper Spectrum(Outlet signal = null, Outlet startTime = null, Outlet endTime = null, Outlet frequencyCount = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Spectrum, inletCount: 4, outletCount: 1);

            var wrapper = new Spectrum_OperatorWrapper(op)
            {
                Signal = signal,
                StartTime = startTime,
                EndTime = endTime,
                FrequencyCount = frequencyCount
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public SpeedUp_OperatorWrapper SpeedUp(Outlet signal = null, Outlet factor = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.SpeedUp, inletCount: 2, outletCount: 1);

            var wrapper = new SpeedUp_OperatorWrapper(op)
            {
                Signal = signal,
                Factor = factor,
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Square_OperatorWrapper Square(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Square, inletCount: 2, outletCount: 1);

            var wrapper = new Square_OperatorWrapper(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Stretch_OperatorWrapper Stretch(Outlet signal = null, Outlet factor = null, Outlet origin = null, DimensionEnum dimension = DimensionEnum.Time)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Stretch, inletCount: 3, outletCount: 1);

            var wrapper = new Stretch_OperatorWrapper(op)
            {
                Signal = signal,
                Factor = factor,
                Origin = origin,
                Dimension = dimension
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Subtract_OperatorWrapper Subtract(Outlet a = null, Outlet b = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Subtract, inletCount: 2, outletCount: 1);

            var wrapper = new Subtract_OperatorWrapper(op)
            {
                A = a,
                B = b
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public TimePower_OperatorWrapper TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.TimePower, inletCount: 3, outletCount: 1);

            var wrapper = new TimePower_OperatorWrapper(op)
            {
                Signal = signal,
                Exponent = exponent,
                Origin = origin
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public ToggleTrigger_OperatorWrapper ToggleTrigger(Outlet calculation = null, Outlet reset = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.ToggleTrigger, inletCount: 2, outletCount: 1);

            var wrapper = new ToggleTrigger_OperatorWrapper(op)
            {
                Calculation = calculation,
                Reset = reset
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Triangle_OperatorWrapper Triangle(Outlet frequency = null, Outlet phaseShift = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.Triangle, inletCount: 2, outletCount: 1);

            var wrapper = new Triangle_OperatorWrapper(op)
            {
                Frequency = frequency,
                PhaseShift = phaseShift
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public PulseTrigger_OperatorWrapper PulseTrigger(Outlet calculation = null, Outlet reset = null)
        {
            Operator op = CreateOperatorBase(OperatorTypeEnum.PulseTrigger, inletCount: 2, outletCount: 1);

            var wrapper = new PulseTrigger_OperatorWrapper(op)
            {
                Calculation = calculation,
                Reset = reset
            };

            op.LinkTo(Patch);

            VoidResult result = ValidateOperatorNonRecursive(op);
            ResultHelper.Assert(result);

            return wrapper;
        }

        public Unbundle_OperatorWrapper Unbundle(Outlet operand = null, int outletCount = 1)
        {
            if (outletCount < 1) throw new LessThanException(() => outletCount, 1);

            Operator op = CreateOperatorBase(OperatorTypeEnum.Unbundle, inletCount: 1, outletCount: outletCount);

            var wrapper = new Unbundle_OperatorWrapper(op)
            {
                Operand = operand
            };

            op.LinkTo(Patch);

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
                // TODO: Use LinkTo.
                op.Inlets[i].InputOutlet = operands[i];
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

        /// <param name="inletCount">
        /// Applies to operators with a variable amount of inlets, such as the Adder operator and the Bundle operator.
        /// </param>
        public Operator CreateOperator(OperatorTypeEnum operatorTypeEnum, int inletCount = 16)
        {
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.Absolute: return Absolute();
                case OperatorTypeEnum.Add: return Add();
                case OperatorTypeEnum.Adder: return Adder(new Outlet[inletCount]);
                case OperatorTypeEnum.And: return And();
                case OperatorTypeEnum.Average: return Average();
                case OperatorTypeEnum.Bundle: return Bundle(new Outlet[inletCount]);
                case OperatorTypeEnum.ChangeTrigger: return ChangeTrigger();
                case OperatorTypeEnum.Cache: return Cache();
                case OperatorTypeEnum.Curve: return Curve();
                case OperatorTypeEnum.CustomOperator: return CustomOperator();
                case OperatorTypeEnum.GetDimension: return GetDimension();
                case OperatorTypeEnum.Delay: return Delay();
                case OperatorTypeEnum.Divide: return Divide();
                case OperatorTypeEnum.Earlier: return Earlier();
                case OperatorTypeEnum.Equal: return Equal();
                case OperatorTypeEnum.Exponent: return Exponent();
                case OperatorTypeEnum.Filter: return Filter();
                case OperatorTypeEnum.GreaterThan: return GreaterThan();
                case OperatorTypeEnum.GreaterThanOrEqual: return GreaterThanOrEqual();
                case OperatorTypeEnum.HighPassFilter: return HighPassFilter();
                case OperatorTypeEnum.If: return If();
                case OperatorTypeEnum.LessThan: return LessThan();
                case OperatorTypeEnum.LessThanOrEqual: return LessThanOrEqual();
                case OperatorTypeEnum.Loop: return Loop();
                case OperatorTypeEnum.LowPassFilter: return LowPassFilter();
                case OperatorTypeEnum.Maximum: return Maximum();
                case OperatorTypeEnum.Minimum: return Minimum();
                case OperatorTypeEnum.Multiply: return Multiply();
                case OperatorTypeEnum.Narrower: return Narrower();
                case OperatorTypeEnum.Noise: return Noise();
                case OperatorTypeEnum.Not: return Not();
                case OperatorTypeEnum.NotEqual: return NotEqual();
                case OperatorTypeEnum.Negative: return Negative();
                case OperatorTypeEnum.Number: return Number();
                case OperatorTypeEnum.OneOverX: return OneOverX();
                case OperatorTypeEnum.Or: return Or();
                case OperatorTypeEnum.PatchInlet: return PatchInlet();
                case OperatorTypeEnum.PatchOutlet: return PatchOutlet();
                case OperatorTypeEnum.Power: return Power();
                case OperatorTypeEnum.Pulse: return Pulse();
                case OperatorTypeEnum.PulseTrigger: return PulseTrigger();
                case OperatorTypeEnum.Random: return Random();
                case OperatorTypeEnum.Resample: return Resample();
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
                case OperatorTypeEnum.SlowDown: return SlowDown();
                case OperatorTypeEnum.Spectrum: return Spectrum();
                case OperatorTypeEnum.SpeedUp: return SpeedUp();
                case OperatorTypeEnum.Square: return Square();
                case OperatorTypeEnum.Stretch: return Stretch();
                case OperatorTypeEnum.Subtract: return Subtract();
                case OperatorTypeEnum.TimePower: return TimePower();
                case OperatorTypeEnum.ToggleTrigger: return ToggleTrigger();
                case OperatorTypeEnum.Triangle: return Triangle();
                case OperatorTypeEnum.Unbundle: return Unbundle();

                default:
                    throw new ValueNotSupportedException(operatorTypeEnum);
            }
        }

        private Operator CreateOperatorBase(OperatorTypeEnum operatorTypeEnum, int inletCount, int outletCount)
        {
            var op = new Operator();
            op.ID = _repositories.IDRepository.GetID();
            op.SetOperatorTypeEnum(operatorTypeEnum, _repositories.OperatorTypeRepository);

            // TODO: This code line was just added on the fly, but really the whole code file must be checked,
            // so that the operators are always linked to the (nullable) Patch.
            op.LinkTo(Patch);
            _repositories.OperatorRepository.Insert(op);

            for (int i = 0; i < inletCount; i++)
            {
                Inlet inlet = CreateInlet(op);
                inlet.ListIndex = i;
            }

            for (int i = 0; i < outletCount; i++)
            {
                Outlet outlet = CreateOutlet(op);
                outlet.ListIndex = i;
            }

            return op;
        }
    }
}
