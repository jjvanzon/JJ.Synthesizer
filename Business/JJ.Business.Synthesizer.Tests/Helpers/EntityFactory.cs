using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Managers;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    public static class EntityFactory
    {
        public static Outlet CreateMockOperatorStructure(PatchManager x)
        {
            if (x == null) throw new NullException(() => x);

            OperatorWrapper_Substract substract = x.Substract(x.Add(x.Number(2), x.Number(3)), x.Number(1));
            return substract;
        }

        public static Outlet CreateTimePowerEffectWithEcho(PatchManager x, Outlet signal)
        {
            Outlet timePower = CreateTimePowerEffect(x, signal);
            Outlet echo = CreateEcho(x, timePower);
            return echo;
        }

        public static Outlet CreateMultiplyWithEcho(PatchManager x, Outlet signal)
        {
            if (x == null) throw new NullException(() => x);

            Outlet multiply = x.Multiply(signal, x.Number(1.5));
            Outlet echo = CreateEcho(x, multiply);
            //Outlet myOutlet = x.Add(x.Sample(sample1), x.Multiply(x.Sample(sample2), x.Value(0.5)));
            return echo;
        }

        public static Outlet CreateTimePowerEffect(PatchManager x, Outlet signal)
        {
            if (x == null) throw new NullException(() => x);

            Outlet outlet = x.TimePower(signal, x.Number(1.5));
            return outlet;
        }

        public static Outlet CreateEcho(PatchManager x, Outlet signal, int count = 15, double denominator = 1.5, double delay = 0.25)
        {
            if (x == null) throw new NullException(() => x);

            double cumulativeDenominator = 1;
            double cumulativeDelay = 0;

            IList<Outlet> repeats = new List<Outlet>(count);

            for (int i = 0; i < count; i++)
            {
                Outlet divide = x.Divide(signal, x.Number(cumulativeDenominator));
                Outlet timeAdd = x.Delay(divide, x.Number(cumulativeDelay));
                repeats.Add(timeAdd);

                cumulativeDenominator *= denominator;
                cumulativeDelay += delay;
            }

            OperatorWrapper_Adder adder = x.Adder(repeats);
            return adder;
        }
    }
}
