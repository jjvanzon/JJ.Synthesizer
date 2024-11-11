using JJ.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Wishes.Helpers;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class PersistenceHelper
    {
        public static IContext CreateContext()
        {
            return ServiceFactory.CreateContext();
        }

        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
        {
            return ServiceFactory.CreateRepository<TRepositoryInterface>(context);
        }
    }
}
