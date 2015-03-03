using JJ.Business.Synthesizer.ExtendedEntities;
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

        public Value CreateValueOperator()
        {
            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = ObjectNames.Value;
            op.Name = ObjectNames.Value;

            Outlet outlet = _outletRepository.Create();
            outlet.Name = ObjectNames.Result;
            outlet.LinkTo(op);

            Value valueOperator = new Value(op);
            return valueOperator; 
        }

        public Add CreateAddOperator()
        {
            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = ObjectNames.Add;
            op.Name = ObjectNames.Add;

            Inlet operandA = _inletRepository.Create();
            operandA.Name = ObjectNames.OperandA;
            operandA.LinkTo(op);

            Inlet operandB = _inletRepository.Create();
            operandB.Name = ObjectNames.OperandB;
            operandB.LinkTo(op);

            Outlet outlet = _outletRepository.Create();
            outlet.Name = ObjectNames.Result;
            outlet.LinkTo(op);

            Add addOperator = new Add(op);
            return addOperator; 
        }
    }
}
