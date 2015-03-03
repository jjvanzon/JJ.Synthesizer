using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.OperatorWrappers;
using JJ.Framework.Testing;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Warnings;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerTests
    {
        [TestMethod]
        public void Test_Synthesizer()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
                IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
                IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);

                var manager = new OperatorFactory(operatorRepository, inletRepository, outletRepository);

                Value value1 = manager.CreateValue();
                value1.Result.Value = 2;

                Value value2 = manager.CreateValue();
                value2.Result.Value = 3;

                Add add = manager.CreateAdd();
                add.OperandA = value1.Result;
                add.OperandB = value2.Result;

                Value value3 = manager.CreateValue();
                value3.Result.Value = 1;

                Substract substract = manager.CreateSubstract();
                substract.OperandA = add.Result;
                substract.OperandB = value3.Result;

                // TODO: Validate whole patch.
                IValidator validator1 = new BasicOperatorValidator(add.Operator);
                IValidator validator2 = new AddOperatorValidator(add.Operator);
                IValidator validator3 = new SubstractOperatorValidator(substract.Operator);
                validator1.Verify();
                validator2.Verify();
                validator3.Verify();

                var calculator = new SoundCalculator();
                double value = calculator.GetValue(add.Result, 0);
                Assert.AreEqual(5, value, 0.00000000000001);

                value = calculator.GetValue(substract.Result, 0);
                Assert.AreEqual(4, value, 0.00000000000001);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_AddOperatorValidator()
        {
            IValidator validator1 = new AddOperatorValidator(new Operator());
            IValidator validator2 = new AddOperatorValidator(new Operator 
            {
                Inlets = new Inlet[]
                { 
                    new Inlet { Name = "qwer"},
                    new Inlet { Name = "asdf" },
                },
                Outlets = new Outlet[]
                {
                    new Outlet { Name = "zxcv" }
                }
            });

            bool isValid = validator1.IsValid && validator2.IsValid;
        }

        [TestMethod]
        public void Test_Synthesizer_WarningValidators()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
                IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
                IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);

                var manager = new OperatorFactory(operatorRepository, inletRepository, outletRepository);

                IValidator validator1 = new AddOperatorWarningValidator(manager.CreateAdd().Operator);
                IValidator validator2 = new ValueOperatorWarningValidator(manager.CreateValue().Operator);

                bool isValid = validator1.IsValid &&
                               validator2.IsValid;
            }
        }
    }
}