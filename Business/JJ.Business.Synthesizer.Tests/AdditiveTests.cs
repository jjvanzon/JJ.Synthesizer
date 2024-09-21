using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
	/// <summary>
	/// More tests written upon retro-actively isolating older synthesizer versions.
	/// </summary>
	[TestClass]
	public class AdditiveTests
	{
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
