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

                var factory = new OperatorFactory(operatorRepository, inletRepository, outletRepository);
                ValueOperator value1 = factory.CreateValueOperator(2);
                ValueOperator value2 = factory.CreateValueOperator(3);
                Add add = factory.CreateAdd(value1, value2);
                ValueOperator value3 = factory.CreateValueOperator(1);
                Substract substract = factory.CreateSubstract(add, value3);

                IValidator validator = new RecursiveOperatorValidator(substract.Operator);
                validator.Verify();

                var calculator = new SoundCalculator();
                double value = calculator.GetValue(add, 0);
                Assert.AreEqual(5, value, 0.00000000000001);

                value = calculator.GetValue(substract, 0);
                Assert.AreEqual(4, value, 0.00000000000001);
                
                // Test recursive validator
                value1.Value = 0;
                substract.Operator.Inlets[0].Name = "134";
                IValidator validator2 = new RecursiveOperatorValidator(substract.Operator);
                IValidator warningValidator = new RecursiveOperatorWarningValidator(substract.Operator);
            }
        }

        [TestMethod]
        public void Test_Synthesizer_AddOperatorValidator()
        {
            IValidator validator1 = new AddValidator(new Operator());
            IValidator validator2 = new AddValidator(new Operator 
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

            bool isValid = validator1.IsValid && 
                           validator2.IsValid;
        }

        [TestMethod]
        public void Test_Synthesizer_WarningValidators()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
                IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
                IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);

                var factory = new OperatorFactory(operatorRepository, inletRepository, outletRepository);

                IValidator validator1 = new AddWarningValidator(factory.CreateAdd().Operator);
                IValidator validator2 = new ValueOperatorWarningValidator(factory.CreateValueOperator().Operator);

                bool isValid = validator1.IsValid &&
                               validator2.IsValid;
            }
        }
    }
}