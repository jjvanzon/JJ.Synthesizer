using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
	/// <summary>
	/// Additional tests written upon retro-actively isolating older synthesizer versions.
	/// </summary>
	[TestClass]
	public class SynthesizerIntegrationTests
	{
		// Sines & Curves

		[TestMethod]
		public void Test_Synthesizer_Sine_With_Volume_Curve()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				var tester = new SynthesizerTester_SineWithVolumeCurve(context);
				tester.Test_Synthesizer_Sine_With_Volume_Curve();
			}
		}

		// Additive

		[TestMethod]
		public void Test_Synthesizer_Additive_Sines_And_Samples()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				var tester = new SynthesizerTester_AdditiveSinesAndSamples(context);
				tester.Test_Synthesizer_Additive_Sines_And_Samples();
			}
		}

		// FM Tuba

		[TestMethod]
		public void Test_Synthesizer_FM_Tuba()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				new SynthesizerTester_FM(context).Test_Synthesizer_FM_Tuba();
			}
		}

		// FM Flutes

		[TestMethod]
		public void Test_Synthesizer_FM_Flute_HardModulated()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				new SynthesizerTester_FM(context).Test_Synthesizer_FM_Flute_HardModulated();
			}
		}

		[TestMethod]
		public void Test_Synthesizer_FM_Flute_HardHigh()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				new SynthesizerTester_FM(context).Test_Synthesizer_FM_Flute_HardHigh();
			}
		}

		[TestMethod]
		public void Test_Synthesizer_FM_Flute_AnotherOne()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				new SynthesizerTester_FM(context).Test_Synthesizer_FM_Flute_AnotherOne();
			}
		}

		[TestMethod]
		public void Test_Synthesizer_FM_Flute_YetAnotherOne()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				new SynthesizerTester_FM(context).Test_Synthesizer_FM_Flute_YetAnotherOne();
			}
		}


		// FM Ripple Effects

		[TestMethod]
		public void Test_Synthesizer_FM_Ripple_FatMetallic()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				new SynthesizerTester_FM(context).Test_Synthesizer_FM_Ripple_FatMetallic();
			}
		}

		[TestMethod]
		public void Test_Synthesizer_FM_Ripple_DeepMetallic()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				new SynthesizerTester_FM(context).Test_Synthesizer_FM_Ripple_DeepMetallic();
			}
		}

		[TestMethod]
		public void Test_Synthesizer_FM_Ripple_FantasyEffect()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				new SynthesizerTester_FM(context).Test_Synthesizer_FM_Ripple_FantasyEffect();
			}
		}

		[TestMethod]
		public void Test_Synthesizer_FM_Ripple_Clean()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				new SynthesizerTester_FM(context).Test_Synthesizer_FM_Ripple_Clean();
			}
		}

		[TestMethod]
		public void Test_Synthesizer_FM_Ripple_CoolDouble()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				new SynthesizerTester_FM(context).Test_Synthesizer_FM_Ripple_CoolDouble();
			}
		}

		// FM Noise

		[TestMethod]
		public void Test_Synthesizer_FM_Noise_Beating()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				new SynthesizerTester_FM(context).Test_Synthesizer_FM_Noise_Beating();
			}
		}
	}
}
