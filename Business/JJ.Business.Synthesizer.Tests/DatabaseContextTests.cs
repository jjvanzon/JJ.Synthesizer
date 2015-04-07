using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class DatabaseContextTests
    {
        [TestMethod]
        public void Test_Synthesizer_DatabaseContext()
        {
            using (IContext context = PersistenceHelper.CreateDatabaseContext())
            {
                var operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
                Operator op = operatorRepository.Create();
                op.Name = "Test Operator";
                op.OperatorTypeName = "TestOperatorType";

                var valueOperatorRepository = PersistenceHelper.CreateRepository<IValueOperatorRepository>(context);
                ValueOperator valueOperator = valueOperatorRepository.Create();
                valueOperator.LinkTo(op);
            }
        }
    }
}
