﻿using JJ.Business.Synthesizer.OperatorWrappers;
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

        public Value CreateValue()
        {
            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = PropertyNames.Value;
            op.Name = PropertyDisplayNames.Value;

            Outlet outlet = _outletRepository.Create();
            outlet.Name = PropertyNames.Result;
            outlet.LinkTo(op);

            var value = new Value(op);
            return value; 
        }

        public Add CreateAdd()
        {
            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = PropertyNames.Add;
            op.Name = PropertyDisplayNames.Add;

            Inlet operandA = _inletRepository.Create();
            operandA.Name = PropertyNames.OperandA;
            operandA.LinkTo(op);

            Inlet operandB = _inletRepository.Create();
            operandB.Name = PropertyNames.OperandB;
            operandB.LinkTo(op);

            Outlet outlet = _outletRepository.Create();
            outlet.Name = PropertyNames.Result;
            outlet.LinkTo(op);

            var add = new Add(op);
            return add;
        }

        public Substract CreateSubstract()
        {
            Operator op = _operatorRepository.Create();
            op.OperatorTypeName = PropertyNames.Substract;
            op.Name = PropertyDisplayNames.Substract;

            Inlet operandA = _inletRepository.Create();
            operandA.Name = PropertyNames.OperandA;
            operandA.LinkTo(op);

            Inlet operandB = _inletRepository.Create();
            operandB.Name = PropertyNames.OperandB;
            operandB.LinkTo(op);

            Outlet outlet = _outletRepository.Create();
            outlet.Name = PropertyNames.Result;
            outlet.LinkTo(op);

            var substract = new Substract(op);
            return substract;
        }
    }
}