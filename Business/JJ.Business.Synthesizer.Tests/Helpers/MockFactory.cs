using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class MockFactory
    {
        public static Outlet CreateMockOperatorStructure(OperatorFactory x)
        {
            if (x == null) throw new NullException(() => x);

            Substract substract = x.Substract(x.Add(x.Value(2), x.Value(3)), x.Value(1));

            return substract.Result;
        }
    }
}
