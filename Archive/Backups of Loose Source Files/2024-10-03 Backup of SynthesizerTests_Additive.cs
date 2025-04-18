﻿using JJ.Business.Synthesizer.Tests.Helpers;
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
		[TestMethod]
		public void Test_Synthesizer_Additive_Sine_And_Curve()
		{
			using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTester_Additive(context).Test_Additive_Sine_And_Curve();
		}

		[TestMethod]
		public void Test_Synthesizer_Additive_Sines_And_Samples()
		{
			using (IContext context = PersistenceHelper.CreateContext()) 
                new SynthesizerTester_Additive(context).Test_Synthesizer_Additive_Sines_And_Samples();
		}
	}
}
