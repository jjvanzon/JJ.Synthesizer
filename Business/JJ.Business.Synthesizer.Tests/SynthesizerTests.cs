using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.ExtendedEntities;
using JJ.Framework.Testing;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerTests
    {
        [TestMethod]
        public void Test_Synthesizer()
        {
            // TODO: Create repositories.
            IOperatorRepository operatorRepository = null;
            IInletRepository inletRepository = null;
            IOutletRepository outletRepository = null;

            var manager = new OperatorFactory(operatorRepository, inletRepository, outletRepository);

            Value value1 = manager.CreateValueOperator();
            value1.Result.Value = 2;

            Value value2 = manager.CreateValueOperator();
            value2.Result.Value = 3;

            Add add = manager.CreateAddOperator();
            add.OperandA = value1.Result;
            add.OperandB = value2.Result;
            
            // TODO: Validate whole patch.
            IValidator validator1 = new OperatorValidator(add.Operator);
            IValidator validator2 = new AddValidator(add.Operator);
            validator1.Verify();
            validator2.Verify();

            var calculator = new Calculator();
            double value = calculator.GetValue(add.Result, 0);
            Assert.AreEqual(5, value, 0.00000000000001);
        }
    }
}
