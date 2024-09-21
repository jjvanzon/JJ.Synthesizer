using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
	/// <summary>
	/// Additional tests written upon retro-actively isolating older synthesizer versions.
	/// </summary>
	[TestClass]
	public class SynthesizerTests2
	{
		[TestMethod]
		public void Test_Synthesizer_Sine_With_Volume_Curve()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				var tester = new SineWithVolumeCurveTester(context);
				tester.Test_Synthesizer_Sine_With_Volume_Curve();
			}
		}

		[TestMethod]
		public void Test_Synthesizer_Additive_Sines_And_Samples()
		{
			using (IContext context = PersistenceHelper.CreateContext())
			{
				var tester = new AdditiveTester(context);
				tester.Test_Synthesizer_Additive_Sines_And_Samples();
			}
		}
	}
}
