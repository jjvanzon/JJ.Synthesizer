using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Framework.Data;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEntityFactory = JJ.Business.Synthesizer.Tests.Helpers.EntityFactory;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal static class EntityFactory
    {
        public static Patch CreateTestPatch1(PersistenceWrapper persistenceWrapper)
        {
            if (persistenceWrapper == null) throw new NullException(() => persistenceWrapper);

            OperatorFactory x = CreateOperatorFactory(persistenceWrapper);

            Outlet outlet = x.Add();

            Patch patch = persistenceWrapper.PatchRepository.Create();
            PatchManager.AddToPatchRecursive(outlet.Operator, patch);
            return patch;
        }

        public static Patch CreateTestPatch2(PersistenceWrapper persistenceWrapper)
        {
            if (persistenceWrapper == null) throw new NullException(() => persistenceWrapper);

            OperatorFactory x = CreateOperatorFactory(persistenceWrapper);

            Outlet outlet = x.Substract(x.Add(x.Value(1), x.Value(2)), x.Value(3));

            Patch patch = persistenceWrapper.PatchRepository.Create();
            PatchManager.AddToPatchRecursive(outlet.Operator, patch);
            return patch;
        }

        public static Patch CreateEchoPatch(PersistenceWrapper persistenceWrapper)
        {
            if (persistenceWrapper == null) throw new NullException(() => persistenceWrapper);

            OperatorFactory operatorFactory = CreateOperatorFactory(persistenceWrapper);
            Patch patch = persistenceWrapper.PatchRepository.Create();

            Outlet outlet = operatorFactory.Sample();
            Outlet outlet2 = TestEntityFactory.CreateEcho(operatorFactory, outlet);
            Outlet outlet3 = operatorFactory.PatchOutlet(outlet2);

            PatchManager.AddToPatchRecursive(outlet3.Operator, patch);

            return patch;
        }

        private static OperatorFactory CreateOperatorFactory(PersistenceWrapper persistenceWrapper)
        {
            var operatorFactory = new OperatorFactory(
                persistenceWrapper.OperatorRepository,
                persistenceWrapper.InletRepository,
                persistenceWrapper.OutletRepository,
                persistenceWrapper.CurveRepository,
                persistenceWrapper.SampleRepository);

            return operatorFactory;
        }
    }
}
