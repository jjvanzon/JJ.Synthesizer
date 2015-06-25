using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Factories
{
    public class OperatorFactory
    {
        private IOperatorRepository _operatorRepository;
        private IOperatorTypeRepository _operatorTypeRepository;
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;

        public OperatorFactory(
            IOperatorRepository operatorRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository)
        {
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _operatorRepository = operatorRepository;
            _operatorTypeRepository = operatorTypeRepository;
            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
        }

        public Add_OperatorWrapper Add(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.Add, PropertyDisplayNames.Add, 2,
                PropertyNames.OperandA, PropertyNames.OperandB,
                PropertyNames.Result);

            var wrapper = new Add_OperatorWrapper(op)
            {
                OperandA = operandA,
                OperandB = operandB
            };

            return wrapper;
        }

        public Adder_OperatorWrapper Adder(params Outlet[] operands)
        {
            return Adder((IList<Outlet>)operands);
        }

        public Adder_OperatorWrapper Adder(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = _operatorRepository.Create();
            op.SetOperatorTypeEnum(OperatorTypeEnum.Adder, _operatorTypeRepository);
            op.Name = PropertyDisplayNames.Adder;

            for (int i = 0; i < operands.Count; i++)
            {
                Inlet inlet = _inletRepository.Create();
                inlet.Name = String.Format("{0}{1}", PropertyNames.Operand, i + 1);
                inlet.LinkTo(op);

                Outlet operand = operands[i];
                inlet.InputOutlet = operand;
            }

            Outlet outlet = _outletRepository.Create();
            outlet.Name = PropertyNames.Result;
            outlet.LinkTo(op);

            var wrapper = new Adder_OperatorWrapper(op);
            return wrapper;
        }

        public Divide_OperatorWrapper Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.Divide, PropertyDisplayNames.Divide, 3,
                PropertyNames.Numerator, PropertyNames.Denominator, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new Divide_OperatorWrapper(op)
            {
                Numerator = numerator,
                Denominator = denominator,
                Origin = origin
            };

            return wrapper;
        }

        public Multiply_OperatorWrapper Multiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.Multiply, PropertyDisplayNames.Multiply, 3,
                PropertyNames.OperandA, PropertyNames.OperandB, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new Multiply_OperatorWrapper(op)
            {
                OperandA = operandA,
                OperandB = operandB,
                Origin = origin
            };

            return wrapper;
        }

        public PatchInlet_OperatorWrapper PatchInlet(Outlet input = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.PatchInlet, PropertyDisplayNames.PatchInlet, 1,
                PropertyNames.Input,
                PropertyNames.Result);

            var wrapper = new PatchInlet_OperatorWrapper(op)
            {
                Input = input
            };

            return wrapper;
        }

        public PatchOutlet_OperatorWrapper PatchOutlet(Outlet input = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.PatchOutlet, PropertyDisplayNames.PatchOutlet, 1,
                PropertyNames.Input,
                PropertyNames.Result);

            var wrapper = new PatchOutlet_OperatorWrapper(op)
            {
                Input = input
            };

            return wrapper;
        }

        public Power_OperatorWrapper Power(Outlet @base = null, Outlet exponent = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.Power, PropertyDisplayNames.Power, 2,
                PropertyNames.Base, PropertyNames.Exponent,
                PropertyNames.Result);

            var wrapper = new Power_OperatorWrapper(op)
            {
                Base = @base,
                Exponent = exponent
            };

            return wrapper;
        }

        public Sine_OperatorWrapper Sine(Outlet volume = null, Outlet pitch = null, Outlet level = null, Outlet phaseStart = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.Sine, PropertyDisplayNames.Sine, 4,
                PropertyNames.Volume, PropertyNames.Pitch, PropertyNames.Level, PropertyNames.PhaseStart,
                PropertyNames.Result);

            var wrapper = new Sine_OperatorWrapper(op)
            {
                Volume = volume,
                Pitch = pitch,
                Level = level,
                PhaseStart = phaseStart
            };

            return wrapper;
        }

        public Substract_OperatorWrapper Substract(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.Substract, PropertyDisplayNames.Substract, 2,
                PropertyNames.OperandA, PropertyNames.OperandB,
                PropertyNames.Result);

            var wrapper = new Substract_OperatorWrapper(op)
            {
                OperandA = operandA,
                OperandB = operandB
            };

            return wrapper;
        }

        public TimeAdd_OperatorWrapper TimeAdd(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.TimeAdd, PropertyDisplayNames.TimeAdd, 2,
                PropertyNames.Signal, PropertyNames.TimeDifference,
                PropertyNames.Result);

            var wrapper = new TimeAdd_OperatorWrapper(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            return wrapper;
        }

        public TimeDivide_OperatorWrapper TimeDivide(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.TimeDivide, PropertyDisplayNames.TimeDivide, 3,
                PropertyNames.Signal, PropertyNames.TimeDivider, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new TimeDivide_OperatorWrapper(op)
            {
                Signal = signal,
                TimeDivider = timeDivider,
                Origin = origin
            };

            return wrapper;
        }

        public TimeMultiply_OperatorWrapper TimeMultiply(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.TimeMultiply, PropertyDisplayNames.TimeMultiply, 3,
                PropertyNames.Signal, PropertyNames.TimeMultiplier, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new TimeMultiply_OperatorWrapper(op)
            {
                Signal = signal,
                TimeMultiplier = timeMultiplier,
                Origin = origin
            };

            return wrapper;
        }

        public TimePower_OperatorWrapper TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.TimePower, PropertyDisplayNames.TimePower, 3,
                PropertyNames.Signal, PropertyNames.Exponent, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new TimePower_OperatorWrapper(op)
            {
                Signal = signal,
                Exponent = exponent,
                Origin = origin
            };

            return wrapper;
        }

        public TimeSubstract_OperatorWrapper TimeSubstract(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.TimeSubstract, PropertyDisplayNames.TimeSubstract, 2,
                PropertyNames.Signal, PropertyNames.TimeDifference,
                PropertyNames.Result);

            var wrapper = new TimeSubstract_OperatorWrapper(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            return wrapper;
        }

        public Value_OperatorWrapper Value(double value = 0)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.Value, PropertyDisplayNames.Value, 0,
                PropertyNames.Result);

            var wrapper = new Value_OperatorWrapper(op)
            {
                Value = value
            };

            return wrapper;
        }

        public CurveIn_OperatorWrapper CurveIn(Curve curve = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.CurveIn, PropertyDisplayNames.CurveIn, 0,
                PropertyNames.Result);

            var wrapper = new CurveIn_OperatorWrapper(op, _curveRepository);

            if (curve != null)
            {
                wrapper.CurveID = curve.ID;
            }

            return wrapper;
        }

        public Sample_OperatorWrapper Sample(Sample sample = null)
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.Sample, PropertyDisplayNames.Sample, 0,
                PropertyNames.Result);

            var wrapper = new Sample_OperatorWrapper(op, _sampleRepository);
            if (sample != null)
            {
                wrapper.SampleID = sample.ID;
            }

            return wrapper;
        }

        public WhiteNoise_OperatorWrapper WhiteNoise()
        {
            Operator op = CreateOperator(
                OperatorTypeEnum.WhiteNoise, PropertyDisplayNames.WhiteNoise, 0,
                PropertyNames.Result);

            var wrapper = new WhiteNoise_OperatorWrapper(op);

            return wrapper;
        }

        private Operator CreateOperator(OperatorTypeEnum operatorTypeEnum, string name, int inletCount, params string[] inletAndOutletNames)
        {
            return OperatorHelper.CreateOperator(
                _operatorRepository, _operatorTypeRepository, _inletRepository, _outletRepository,
                operatorTypeEnum, name, inletCount, inletAndOutletNames);
        }
    }
}