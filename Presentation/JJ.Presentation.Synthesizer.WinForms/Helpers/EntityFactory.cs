using JJ.Business.Synthesizer.Factories;
using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal static class EntityFactory
    {
        public static Outlet CreateTestPatch1(PersistenceWrapper persistenceWrapper)
        {
            if (persistenceWrapper == null) throw new NullException(() => persistenceWrapper);

            OperatorFactory x = CreateOperatorFactory(persistenceWrapper);

            return x.Add();
        }

        public static Outlet CreateTestPatch2(PersistenceWrapper persistenceWrapper)
        {
            if (persistenceWrapper == null) throw new NullException(() => persistenceWrapper);

            OperatorFactory x = CreateOperatorFactory(persistenceWrapper);

            return x.Substract(x.Add(x.Value(1), x.Value(2)), x.Value(3));
        }

        private static OperatorFactory CreateOperatorFactory(PersistenceWrapper persistenceWrapper)
        {
            var operatorFactory = new OperatorFactory(
                persistenceWrapper.OperatorRepository,
                persistenceWrapper.InletRepository,
                persistenceWrapper.OutletRepository,
                persistenceWrapper.CurveInRepository,
                persistenceWrapper.ValueOperatorRepository,
                persistenceWrapper.SampleOperatorRepository);

            return operatorFactory;
        }
    }
}
