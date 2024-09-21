using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
	[TestClass]
	public class AdditiveTests
	{
		[TestMethod]
		public void Test_Synthesizer_Additive_Sines_And_Samples()
		{
			new AdditiveTester().Test_Synthesizer_Additive_Sines_And_Samples();
		}
	}
}
