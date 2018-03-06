using System.Collections.Generic;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
	public static class EntityFactory
	{
		public static Outlet CreateMockOperatorStructure(OperatorFactory x)
		{
			if (x == null) throw new NullException(() => x);

			OperatorWrapper subtract = x.Subtract(x.Add(x.Number(2), x.Number(3)), x.Number(1));
			return subtract;
		}

		public static Outlet CreateTimePowerEffectWithEcho(OperatorFactory x, Outlet signal)
		{
			Outlet timePower = CreateTimePowerEffect(x, signal);
			Outlet echo = CreateEcho(x, timePower);
			return echo;
		}

		public static Outlet CreateMultiplyWithEcho(OperatorFactory x, Outlet signal)
		{
			if (x == null) throw new NullException(() => x);

			Outlet multiply = x.MultiplyWithOrigin(signal, x.Number(1.5));
			Outlet echo = CreateEcho(x, multiply);
			//Outlet myOutlet = x.Add(x.Sample(sample1), x.Multiply(x.Sample(sample2), x.Value(0.5)));
			return echo;
		}

		public static Outlet CreateTimePowerEffect(OperatorFactory x, Outlet signal)
		{
			if (x == null) throw new NullException(() => x);

			Outlet outlet = x.TimePower(signal, x.Number(1.5));
			return outlet;
		}

		public static Outlet CreateEcho(OperatorFactory x, Outlet signal, int count = 15, double denominator = 1.5, double delayValue = 0.25)
		{
			if (x == null) throw new NullException(() => x);

			double cumulativeDenominator = 1;
			double cumulativeDelay = 0;

			IList<Outlet> repeats = new List<Outlet>(count);

			for (int i = 0; i < count; i++)
			{
				Outlet divide = x.Divide(signal, x.Number(cumulativeDenominator));
				Outlet shift = x.Shift(divide, x.Number(cumulativeDelay));
				repeats.Add(shift);

				cumulativeDenominator *= denominator;
				cumulativeDelay += delayValue;
			}

			OperatorWrapper add = x.Add(repeats);
			return add;
		}
	}
}
