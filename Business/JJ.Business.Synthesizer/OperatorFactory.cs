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

namespace JJ.Business.Synthesizer
{
    public class OperatorFactory
    {
        private IOperatorRepository _operatorRepository;
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;

        public OperatorFactory(
            IOperatorRepository operatorRepository, 
            IInletRepository inletRepository,
            IOutletRepository outletRepository)
        {
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);

            _operatorRepository = operatorRepository;
            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
        }

        public Add NewAdd(Outlet operandA = null, Outlet operandB = null)
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

        public Adder NewAdder(params Outlet[] operands)
        {
            if (operands == null) throw new NullException(() => operands);

            // TODO: Is it any use delegating to the helper method,
            // when you do something this specialized?
            Operator op = CreateOperator(
                PropertyNames.Add, PropertyDisplayNames.Add, operands.Length,
                operands.Select(x => "")  // TODO: Fill in proper names.
                        .Union(PropertyNames.Result)
                        .ToArray());

            var wrapper = new Adder(op);
            for (int i = 0; i < operands.Length; i++)
			{
			    wrapper.Operands[i] = operands[i];
			}
            return wrapper;
        }

        public Divide NewDivide(Outlet numerator = null, Outlet denominator = null, Outlet origin = null)
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

        public Multiply NewMultiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
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

        public PatchInlet NewPatchInlet(Outlet input = null)
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

        public PatchOutlet NewPatchOutlet(Outlet input = null)
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

        public Power NewPower(Outlet @base = null, Outlet exponent = null)
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

        public Sine NewSine(Outlet volume = null, Outlet pitch = null, Outlet level = null, Outlet phaseStart = null)
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

        public Substract NewSubstract(Outlet operandA = null, Outlet operandB = null)
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

        public TimeAdd NewTimeAdd(Outlet signal = null, Outlet timeDifference = null)
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

        public TimeDivide NewTimeDivide(Outlet signal = null, Outlet timeDivider = null, Outlet origin = null)
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

        public TimeMultiply NewTimeMultiply(Outlet signal = null, Outlet timeMultiplier = null, Outlet origin = null)
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

        public TimePower NewTimePower(Outlet signal = null, Outlet exponent = null, Outlet origin = null)
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

        public TimeSubstract NewTimeSubstract(Outlet signal = null, Outlet timeDifference = null)
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

        public ValueOperator NewValue(double value = 0)
        {
            Operator op = CreateOperator(
                PropertyNames.ValueOperator,
                PropertyDisplayNames.ValueOperator, 0,
                PropertyNames.Result);

            var wrapper = new ValueOperator(op)
            {
                Value = value
            };

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
