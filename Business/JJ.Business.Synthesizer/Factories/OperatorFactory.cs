using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Factories
{
    public class OperatorFactory
    {
        private IOperatorRepository _operatorRepository;
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        // TODO: Remove outcommented code, also from other parts of this code file.
        //private ICurveInRepository _curveInRepository;
        //private IValueOperatorRepository _valueOperatorRepository;
        //private ISampleOperatorRepository _sampleOperatorRepository;

        public OperatorFactory(
            IOperatorRepository operatorRepository, 
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository)
            //ICurveInRepository curveInRepository,
            //IValueOperatorRepository valueOperatorRepository,
            //ISampleOperatorRepository sampleOperatorRepository
        {
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            //if (curveInRepository == null) throw new NullException(() => curveInRepository);
            //if (valueOperatorRepository == null) throw new NullException(() => valueOperatorRepository);
            //if (sampleOperatorRepository == null) throw new NullException(() => sampleOperatorRepository);

            _operatorRepository = operatorRepository;
            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;
            //_curveInRepository = curveInRepository;
            //_valueOperatorRepository = valueOperatorRepository;
            //_sampleOperatorRepository = sampleOperatorRepository;
        }

        public AddWrapper Add(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Add, PropertyDisplayNames.Add, 2,
                PropertyNames.OperandA, PropertyNames.OperandB,
                PropertyNames.Result);

            var wrapper = new AddWrapper(op)
            {
                OperandA = operandA,
                OperandB = operandB
            };

            return wrapper;
        }

        public AdderWrapper Adder(params Outlet[] operands)
        {
            return Adder((IList<Outlet>)operands);
        }

        public AdderWrapper Adder(IList<Outlet> operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = PropertyNames.Adder;
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

            var wrapper = new AdderWrapper(op);
            return wrapper;
        }

        public DivideWrapper Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Divide, PropertyDisplayNames.Divide, 3,
                PropertyNames.Numerator, PropertyNames.Denominator, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new DivideWrapper(op)
            {
                Numerator = numerator,
                Denominator = denominator,
                Origin = origin
            };

            return wrapper;
        }

        public MultiplyWrapper Multiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Multiply, PropertyDisplayNames.Multiply, 3,
                PropertyNames.OperandA, PropertyNames.OperandB, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new MultiplyWrapper(op)
            {
                OperandA = operandA,
                OperandB = operandB,
                Origin = origin
            };

            return wrapper;
        }

        public PatchInletWrapper PatchInlet(Outlet input = null)
        {
            Operator op = CreateOperator(
                PropertyNames.PatchInlet, PropertyDisplayNames.PatchInlet, 1,
                PropertyNames.Input,
                PropertyNames.Result);

            var wrapper = new PatchInletWrapper(op)
            {
                Input = input
            };

            return wrapper;
        }

        public PatchOutletWrapper PatchOutlet(Outlet input = null)
        {
            Operator op = CreateOperator(
                PropertyNames.PatchOutlet, PropertyDisplayNames.PatchOutlet, 1,
                PropertyNames.Input,
                PropertyNames.Result);

            var wrapper = new PatchOutletWrapper(op)
            {
                Input = input
            };

            return wrapper;
        }

        public PowerWrapper Power(Outlet @base = null, Outlet exponent = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Power, PropertyDisplayNames.Power, 2, 
                PropertyNames.Base, PropertyNames.Exponent, 
                PropertyNames.Result);

            var wrapper = new PowerWrapper(op)
            {
                Base = @base,
                Exponent = exponent
            };

            return wrapper;
        }

        public SineWrapper Sine(Outlet volume = null, Outlet pitch = null, Outlet level = null, Outlet phaseStart = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Sine, PropertyDisplayNames.Sine, 4,
                PropertyNames.Volume, PropertyNames.Pitch, PropertyNames.Level, PropertyNames.PhaseStart,
                PropertyNames.Result);

            var wrapper = new SineWrapper(op)
            {
                Volume = volume,
                Pitch = pitch,
                Level = level,
                PhaseStart = phaseStart
            };

            return wrapper;
        }

        public SubstractWrapper Substract(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Substract, PropertyDisplayNames.Substract, 2,
                PropertyNames.OperandA, PropertyNames.OperandB,
                PropertyNames.Result);

            var wrapper = new SubstractWrapper(op)
            {
                OperandA = operandA,
                OperandB = operandB
            };

            return wrapper;
        }

        public TimeAddWrapper TimeAdd(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperator(
                PropertyNames.TimeAdd, PropertyDisplayNames.TimeAdd, 2,
                PropertyNames.Signal, PropertyNames.TimeDifference,
                PropertyNames.Result);

            var wrapper = new TimeAddWrapper(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            return wrapper;
        }

        public TimeDivideWrapper TimeDivide(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                PropertyNames.TimeDivide, PropertyDisplayNames.TimeDivide, 3,
                PropertyNames.Signal, PropertyNames.TimeDivider, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new TimeDivideWrapper(op)
            {
                Signal = signal,
                TimeDivider = timeDivider,
                Origin = origin
            };

            return wrapper;
        }

        public TimeMultiplyWrapper TimeMultiply(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                PropertyNames.TimeMultiply, PropertyDisplayNames.TimeMultiply, 3,
                PropertyNames.Signal, PropertyNames.TimeMultiplier, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new TimeMultiplyWrapper(op)
            {
                Signal = signal,
                TimeMultiplier = timeMultiplier,
                Origin = origin
            };

            return wrapper;
        }

        public TimePowerWrapper TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                PropertyNames.TimePower, PropertyDisplayNames.TimePower, 3,
                PropertyNames.Signal, PropertyNames.Exponent, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new TimePowerWrapper(op)
            {
                Signal = signal,
                Exponent = exponent,
                Origin = origin
            };

            return wrapper;
        }

        public TimeSubstractWrapper TimeSubstract(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperator(
                PropertyNames.TimeSubstract, PropertyDisplayNames.TimeSubstract, 2, 
                PropertyNames.Signal, PropertyNames.TimeDifference, 
                PropertyNames.Result);

            var wrapper = new TimeSubstractWrapper(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            return wrapper;
        }

        public ValueOperatorWrapper Value(double value = 0)
        {
            Operator op = CreateOperator(
                PropertyNames.ValueOperator, PropertyDisplayNames.ValueOperator, 0,
                PropertyNames.Result);

            //ValueOperator valueOperator = _valueOperatorRepository.Create();
            //valueOperator.LinkTo(op);

            var wrapper = new ValueOperatorWrapper(op)
            {
                Value = value
            };

            return wrapper;
        }

        public CurveInWrapper CurveIn(Curve curve = null)
        {
            Operator op = CreateOperator(
                PropertyNames.CurveIn,PropertyDisplayNames.CurveIn, 0,
                PropertyNames.Result);

            //CurveIn curveIn = _curveInRepository.Create();
            //curveIn.LinkTo(op);
            //curveIn.LinkTo(curve);

            var wrapper = new CurveInWrapper(op, _curveRepository);

            if (curve != null)
            {
                wrapper.CurveID = curve.ID;
            }

            return wrapper;
        }

        public SampleOperatorWrapper Sample(Sample sample = null)
        {
            Operator op = CreateOperator(
                PropertyNames.SampleOperator, PropertyDisplayNames.SampleOperator, 0,
                PropertyNames.Result);

            //SampleOperator sampleOperator = _sampleOperatorRepository.Create();
            //sampleOperator.LinkTo(op);
            //sampleOperator.LinkTo(sample);

            var wrapper = new SampleOperatorWrapper(op, _sampleRepository);
            if (sample != null)
            {
                wrapper.SampleID = sample.ID;
            }

            return wrapper;
        }

        private Operator CreateOperator(string operatorTypeName, string displayName, int inletCount, params string[] inletAndOutletNames)
        {
            if (inletCount > inletAndOutletNames.Length) throw new Exception("inletCount cannot be greater than inletAndOutletNames.Length.");

            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = operatorTypeName;
            op.Name = displayName;

            foreach (string inletName in inletAndOutletNames.Take(inletCount))
            {
                Inlet inlet = _inletRepository.Create();
                inlet.Name = inletName;
                inlet.LinkTo(op);
            }

            foreach (string outletName in inletAndOutletNames.Skip(inletCount))
            {
                Outlet outlet = _outletRepository.Create();
                outlet.Name = outletName;
                outlet.LinkTo(op);
            }

            return op;
        }
    }
}
