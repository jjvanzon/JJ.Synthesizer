using JJ.Business.Synthesizer.OperatorWrappers;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.LinkTo;
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

        public ValueOperator CreateValueOperator(double value = 0)
        {
            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = PropertyNames.ValueOperator;
            op.Name = PropertyDisplayNames.Value;

            Outlet outlet = _outletRepository.Create();
            outlet.Name = PropertyNames.Result;
            outlet.LinkTo(op);

            var wrapper = new ValueOperator(op);
            wrapper.Value = 0;
            return wrapper; 
        }

        public Add CreateAdd(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = PropertyNames.Add;
            op.Name = PropertyDisplayNames.Add;

            Inlet operandAInlet = _inletRepository.Create();
            operandAInlet.Name = PropertyNames.OperandA;
            operandAInlet.LinkTo(op);

            Inlet operandBInlet = _inletRepository.Create();
            operandBInlet.Name = PropertyNames.OperandB;
            operandBInlet.LinkTo(op);

            Outlet outlet = _outletRepository.Create();
            outlet.Name = PropertyNames.Result;
            outlet.LinkTo(op);

            var wrapper = new Add(op);
            wrapper.OperandA = operandA;
            wrapper.OperandB = operandB;
            return wrapper;
        }

        public Substract CreateSubstract(Outlet operandA = null, Outlet operandB = null)
        {
            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = PropertyNames.Substract;
            op.Name = PropertyDisplayNames.Substract;

            Inlet operandAInlet = _inletRepository.Create();
            operandAInlet.Name = PropertyNames.OperandA;
            operandAInlet.LinkTo(op);

            Inlet operandBInlet = _inletRepository.Create();
            operandBInlet.Name = PropertyNames.OperandB;
            operandBInlet.LinkTo(op);

            Outlet outlet = _outletRepository.Create();
            outlet.Name = PropertyNames.Result;
            outlet.LinkTo(op);

            var wrapper = new Substract(op);
            wrapper.OperandA = operandA;
            wrapper.OperandB = operandB;
            return wrapper;
        }

        public Multiply CreateMultiply(Outlet operandA = null, Outlet operandB = null, Outlet origin = null)
        {
            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = PropertyNames.Multiply;
            op.Name = PropertyDisplayNames.Multiply;

            Inlet operandAInlet = _inletRepository.Create();
            operandAInlet.Name = PropertyNames.OperandA;
            operandAInlet.LinkTo(op);

            Inlet operandBInlet = _inletRepository.Create();
            operandBInlet.Name = PropertyNames.OperandB;
            operandBInlet.LinkTo(op);

            Inlet originInlet = _inletRepository.Create();
            operandBInlet.Name = PropertyNames.Origin;
            operandBInlet.LinkTo(op);

            Outlet outlet = _outletRepository.Create();
            outlet.Name = PropertyNames.Result;
            outlet.LinkTo(op);

            var wrapper = new Multiply(op);
            wrapper.OperandA = operandA;
            wrapper.OperandB = operandB;
            wrapper.Origin = origin;
            return wrapper;
        }
    }
}
