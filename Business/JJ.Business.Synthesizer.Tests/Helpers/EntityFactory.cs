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
    internal class EntityFactory
    {
        public static Outlet CreateMockOperatorStructure(OperatorFactory x)
        {
            if (x == null) throw new NullException(() => x);

            Substract substract = x.Substract(x.Add(x.Value(2), x.Value(3)), x.Value(1));
            return substract;
        }

        public static Outlet CreateTimePowerEffectWithEcho(OperatorFactory x, Outlet signal)
        {
            Outlet timePower = CreateTimePowerEffect(x, signal);
            Outlet echo = CreateEcho(x, timePower);
            return echo;
        }

        public static Outlet CreateTimePowerEffect(OperatorFactory x, Outlet signal)
        {
            if (x == null) throw new NullException(() => x);

            Outlet outlet = x.TimePower(signal, x.Value(1.5));
            return outlet;
        }

        public static Outlet CreateEcho(OperatorFactory x, Outlet signal, int count = 15, double denominator = 1.5, double delay = 0.25)
        {
            double cumulativeDenominator = 1;
            double cumulativeDelay = 0;

            IList<Outlet> repeats = new List<Outlet>(count);

            for (int i = 0; i < count; i++)
            {
                Outlet divide = x.Divide(signal, x.Value(cumulativeDenominator));
                Outlet timeAdd = x.TimeAdd(divide, x.Value(cumulativeDelay));
                repeats.Add(timeAdd);

                cumulativeDenominator *= denominator;
                cumulativeDelay += delay;
            }

            Adder adder = x.Adder(repeats);
            return adder;
        }
    }
}
