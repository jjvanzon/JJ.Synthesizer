using JJ.Framework.Persistence;
using System;

namespace JJ.Business.Synthesizer.Tests
{
	internal class SynthesizerTester_FMDetuneAndPitchModulation
	{
		private readonly IContext _context;

		public SynthesizerTester_FMDetuneAndPitchModulation(IContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public void Test_FM_With_Detune_And_Pitch_Modulation()
		{
			throw new NotImplementedException();
		}
	}
}
