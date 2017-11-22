using System.Threading;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Presentation.Synthesizer.NAudio
{
	internal class EmptyPatchCalculatorContainer : IPatchCalculatorContainer
	{
		public IPatchCalculator Calculator => null;
		public ReaderWriterLockSlim Lock { get; } = new ReaderWriterLockSlim();
		public void RecreateCalculator(Patch patch, int samplingRate, int channelCount, int maxConcurrentNotes) { }
	}
}