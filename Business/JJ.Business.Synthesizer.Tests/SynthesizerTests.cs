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
using System.Collections.Generic;

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
                ValueOperator value1 = factory.Value(2);
                ValueOperator value2 = factory.Value(3);
                Add add = factory.Add(value1, value2);
                ValueOperator value3 = factory.Value(1);
                Substract substract = factory.Substract(add, value3);

                IValidator validator = new RecursiveOperatorValidator(substract.Operator);
                validator.Verify();

                ISoundCalculator calculator = new SoundCalculator3();
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
                add.OperandA = null;
                value3.Value = 0;
                substract.Operator.Inlets[0].Name = "134";

                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("nl-NL");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("nl-NL");

                IValidator validator2 = new RecursiveOperatorValidator(substract.Operator);
                IValidator warningValidator = new RecursiveOperatorWarningValidator(substract.Operator);

                //Assert.Inconclusive(String.Format("{0}ms for 1s of sound.", ms));
            }
        }

        [TestMethod]
        public void Test_Synthesizer_AddValidator()
        {
            IValidator validator1 = new AddValidator(new Operator
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

            IValidator validator2 = new AddValidator(new Operator());

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

                IValidator validator1 = new AddWarningValidator(factory.Add().Operator);
                IValidator validator2 = new ValueOperatorWarningValidator(factory.Value().Operator);

                bool isValid = validator1.IsValid &&
                               validator2.IsValid;
            }
        }

        [TestMethod]
        public void Test_Synthesizer_Adder()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
                IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
                IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);

                var factory = new OperatorFactory(operatorRepository, inletRepository, outletRepository);
                ValueOperator val1 = factory.Value(1);
                ValueOperator val2 = factory.Value(2);
                ValueOperator val3 = factory.Value(3);
                Adder adder = factory.Adder(val1, val2, val3);

                IValidator validator = new AdderValidator(adder.Operator);
                validator.Verify();

                var calculator = new SoundCalculator();
                double value = calculator.CalculateValue(adder, 0);

                adder.Operator.Inlets[0].Name = "qwer";
                IValidator validator2 = new AdderValidator(adder.Operator);
                //validator2.Verify();
            }
        }

        [TestMethod]
        public void Test_Synthesizer_PatchBuildingCodeWithShorterCode()
        {
            using (IContext context = PersistenceHelper.CreateContext())
            {
                IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
                IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
                IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);

                var x = new OperatorFactory(operatorRepository, inletRepository, outletRepository);

                Substract substract = x.Substract(x.Add(x.Value(2), x.Value(3)), x.Value(1));

                Substract substract2 = x.Substract
                (
                    x.Add
                    (
                        x.Value(2),
                        x.Value(3)
                    ),
                    x.Value(1)
                );
            }
        }

        //[TestMethod]
        //public void Test_Synthesizer_TemplateTestMethod()
        //{
        //    using (IContext context = PersistenceHelper.CreateContext())
        //    {
        //        IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
        //        IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
        //        IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);

        //        var x = new OperatorFactory(operatorRepository, inletRepository, outletRepository);
        //    }
        //}
    }
}