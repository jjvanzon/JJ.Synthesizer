using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Framework.Testing;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Warnings;
using System.Diagnostics;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Business.Synthesizer.Warnings.Entities;

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
                ValueOperator value1 = factory.NewValue(2);
                ValueOperator value2 = factory.NewValue(3);
                Add add = factory.NewAdd(value1, value2);
                ValueOperator value3 = factory.NewValue(1);
                Substract substract = factory.NewSubstract(add, value3);

                IValidator validator = new RecursiveOperatorValidator(substract.Operator);
                validator.Verify();

                ISoundCalculator calculator = CreateSoundCalculator();
                double value = calculator.CalculateValue(add, 0);
                Assert.AreEqual(5, value, 0.00000000000001);
                value = calculator.CalculateValue(substract, 0);
                Assert.AreEqual(4, value, 0.00000000000001);

                // Test performance a bit.
                int repeats = 88200;
                Outlet outlet = substract.Operator.Outlets[0];
                Stopwatch sw = Stopwatch.StartNew();
                for (int i = 0; i < repeats; i++)
                {
                    value = calculator.CalculateValue(outlet, 0);
                }
                sw.Stop();
                long ms = sw.ElapsedMilliseconds;

                // Test recursive validator
                value1.Value = 0;
                substract.Operator.Inlets[0].Name = "134";
                IValidator validator2 = new RecursiveOperatorValidator(substract.Operator);
                IValidator warningValidator = new RecursiveOperatorWarningValidator(substract.Operator);

                //Assert.Inconclusive(String.Format("{0}ms for 1s of sound.", ms));
            }
        }

        [TestMethod]
        public void Test_Synthesizer_AddOperatorValidator()
        {
            IValidator validator1 = new PatchInletValidator(new Operator());
            IValidator validator2 = new PatchInletValidator(new Operator 
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

                IValidator validator1 = new AddWarningValidator(factory.NewAdd().Operator);
                IValidator validator2 = new ValueOperatorWarningValidator(factory.NewValue().Operator);

                bool isValid = validator1.IsValid &&
                               validator2.IsValid;
            }
        }

        private ISoundCalculator CreateSoundCalculator()
        {
            return new SoundCalculator3();
        }
    }
}