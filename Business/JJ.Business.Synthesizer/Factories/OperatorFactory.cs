using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
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
        private ICurveInRepository _curveInRepository;
        private IValueOperatorRepository _valueOperatorRepository;

        public OperatorFactory(
            IOperatorRepository operatorRepository, 
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            ICurveInRepository curveInRepository,
            IValueOperatorRepository valueOperatorRepository)
        {
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (curveInRepository == null) throw new NullException(() => curveInRepository);
            if (valueOperatorRepository == null) throw new NullException(() => valueOperatorRepository);

            _operatorRepository = operatorRepository;
            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
            _curveInRepository = curveInRepository;
            _valueOperatorRepository = valueOperatorRepository;
        }

        public Add Add(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Add, PropertyDisplayNames.Add, 2,
                PropertyNames.OperandA, PropertyNames.OperandB,
                PropertyNames.Result);

            var wrapper = new Add(op)
            {
                OperandA = operandA,
                OperandB = operandB
            };

            return wrapper;
        }

        public Adder Adder(params Outlet[] operands)
        {
            if (operands == null) throw new NullException(() => operands);

            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = PropertyNames.Adder;
            op.Name = PropertyDisplayNames.Adder;

            for (int i = 0; i < operands.Length; i++)
            {
                Inlet inlet = _inletRepository.Create();
                inlet.Name = String.Format("{0}{1}", PropertyNames.Operand, i + 1);
                inlet.LinkTo(op);

                Outlet operand = operands[i];
                inlet.Input = operand;
            }

            Outlet outlet = _outletRepository.Create();
            outlet.Name = PropertyNames.Result;
            outlet.LinkTo(op);

            var wrapper = new Adder(op);
            return wrapper;
        }

        public Divide Divide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Divide, PropertyDisplayNames.Divide, 3,
                PropertyNames.Numerator, PropertyNames.Denominator, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new Divide(op)
            {
                Numerator = numerator,
                Denominator = denominator,
                Origin = origin
            };

            return wrapper;
        }

        public Multiply Multiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Multiply, PropertyDisplayNames.Multiply, 3,
                PropertyNames.OperandA, PropertyNames.OperandB, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new Multiply(op)
            {
                OperandA = operandA,
                OperandB = operandB,
                Origin = origin
            };

            return wrapper;
        }

        public PatchInlet PatchInlet(Outlet input = null)
        {
            Operator op = CreateOperator(
                PropertyNames.PatchInlet, PropertyDisplayNames.PatchInlet, 1,
                PropertyNames.Input,
                PropertyNames.Result);

            var wrapper = new PatchInlet(op)
            {
                Input = input
            };

            return wrapper;
        }

        public PatchOutlet PatchOutlet(Outlet input = null)
        {
            Operator op = CreateOperator(
                PropertyNames.PatchOutlet, PropertyDisplayNames.PatchOutlet, 1,
                PropertyNames.Input,
                PropertyNames.Result);

            var wrapper = new PatchOutlet(op)
            {
                Input = input
            };

            return wrapper;
        }

        public Power Power(Outlet @base = null, Outlet exponent = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Power, PropertyDisplayNames.Power, 2, 
                PropertyNames.Base, PropertyNames.Exponent, 
                PropertyNames.Result);

            var wrapper = new Power(op)
            {
                Base = @base,
                Exponent = exponent
            };

            return wrapper;
        }

        public Sine Sine(Outlet volume = null, Outlet pitch = null, Outlet level = null, Outlet phaseStart = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Sine, PropertyDisplayNames.Sine, 4,
                PropertyNames.Volume, PropertyNames.Pitch, PropertyNames.Level, PropertyNames.PhaseStart,
                PropertyNames.Result);

            var wrapper = new Sine(op)
            {
                Volume = volume,
                Pitch = pitch,
                Level = level,
                PhaseStart = phaseStart
            };

            return wrapper;
        }

        public Substract Substract(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Substract, PropertyDisplayNames.Substract, 2,
                PropertyNames.OperandA, PropertyNames.OperandB,
                PropertyNames.Result);

            var wrapper = new Substract(op)
            {
                OperandA = operandA,
                OperandB = operandB
            };

            return wrapper;
        }

        public TimeAdd TimeAdd(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperator(
                PropertyNames.TimeAdd, PropertyDisplayNames.TimeAdd, 2,
                PropertyNames.Signal, PropertyNames.TimeDifference,
                PropertyNames.Result);

            var wrapper = new TimeAdd(op)
            {
                Signal = signal,
                TimeDifference = timeDifference
            };

            return wrapper;
        }

        public TimeDivide TimeDivide(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                PropertyNames.TimeDivide, PropertyDisplayNames.TimeDivide, 3,
                PropertyNames.Signal, PropertyNames.TimeDivider, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new TimeDivide(op)
            {
                Signal = signal,
                TimeDivider = timeDivider,
                Origin = origin
            };

            return wrapper;
        }

        public TimeMultiply TimeMultiply(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                PropertyNames.TimeMultiply, PropertyDisplayNames.TimeMultiply, 3,
                PropertyNames.Signal, PropertyNames.TimeMultiplier, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new TimeMultiply(op)
            {
                Signal = signal,
                TimeMultiplier = timeMultiplier,
                Origin = origin
            };

            return wrapper;
        }

        public TimePower TimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
        {
            Operator op = CreateOperator(
                PropertyNames.TimePower, PropertyDisplayNames.TimePower, 3,
                PropertyNames.Signal, PropertyNames.Exponent, PropertyNames.Origin,
                PropertyNames.Result);

            var wrapper = new TimePower(op)
            {
                Signal = signal,
                Exponent = exponent,
                Origin = origin
            };

            return wrapper;
        }

        public TimeSubstract TimeSubstract(Outlet signal = null, Outlet timeDifference = null)
        {
            Operator op = CreateOperator(
                PropertyNames.Divide, PropertyDisplayNames.Divide, 2, 
                PropertyNames.Signal, PropertyNames.TimeDifference, 
                PropertyNames.Result);

            var wrapper = new TimeSubstract(op)
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

            ValueOperator valueOperator = _valueOperatorRepository.Create();
            valueOperator.LinkTo(op);

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

            CurveIn curveIn = _curveInRepository.Create();
            curveIn.LinkTo(op);
            curveIn.LinkTo(curve);

            var wrapper = new CurveInWrapper(curveIn);
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
