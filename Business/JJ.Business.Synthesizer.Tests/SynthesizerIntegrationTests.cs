using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
	/// <summary>
	/// Additional tests written upon retro-actively isolating older synthesizer versions.
	/// </summary>
	[TestClass]
	public class SynthesizerTests_Additive
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
	}
}
