using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.EventArg
{
	public class IDEventArgs : EventArgs
	{
		public int ID { get; }

		public IDEventArgs(int id) => ID = id;
	}
}
