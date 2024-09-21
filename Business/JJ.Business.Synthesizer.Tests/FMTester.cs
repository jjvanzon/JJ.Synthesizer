using JJ.Framework.Persistence;
using System;

namespace JJ.Business.Synthesizer.Tests
{
	internal class FMTester
	{
		private readonly IContext _context;

		public FMTester(IContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public void Test_FM_With_Detune_And_Pitch_Modulation()
		{
			throw new NotImplementedException();
		}
	}
}
