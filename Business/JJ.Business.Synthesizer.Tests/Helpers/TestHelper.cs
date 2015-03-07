using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class TestHelper
    {
        public static OperatorFactory CreateOperatorFactory(IContext context)
        {
            IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
            IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
            IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);
            ICurveInRepository curveInRepository = PersistenceHelper.CreateRepository<ICurveInRepository>(context);
            IValueOperatorRepository valueOperatorRepository = PersistenceHelper.CreateRepository<IValueOperatorRepository>(context);
            var factory = new OperatorFactory(operatorRepository, inletRepository, outletRepository, curveInRepository, valueOperatorRepository);
            return factory;
        }

        public static CurveFactory CreateCurveFactory(IContext context)
        {
            ICurveRepository curveRepository = PersistenceHelper.CreateRepository<ICurveRepository>(context);
            INodeRepository nodeRepository = PersistenceHelper.CreateRepository<INodeRepository>(context);
            INodeTypeRepository nodeTypeRepository = PersistenceHelper.CreateRepository<INodeTypeRepository>(context);
            var factory = new CurveFactory(curveRepository, nodeRepository, nodeTypeRepository);
            return factory;
        }
    }
}
