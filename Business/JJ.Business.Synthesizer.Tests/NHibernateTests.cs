using JJ.Business.Synthesizer.Tests.Helpers;
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
    public class NHibernateTests
    {
        [TestMethod]
        public void Test_Synthesizer_DatabaseContext()
        {
            using (IContext context = PersistenceHelper.CreateDatabaseContext())
            {
                var repository = PersistenceHelper.CreateRepository<ICurveRepository>(context);
                Curve curve = repository.Create();
                curve.Name = "Bla";

                context.Commit();
            }
        }
    }
}
