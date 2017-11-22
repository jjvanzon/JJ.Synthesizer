using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
	internal class MoveOperatorEventArgs : EventArgs
	{
		public int PatchID { get; }
		public int OperatorID { get; }
		public float X { get; }
		public float Y { get; }

		public MoveOperatorEventArgs(int patchID, int operatorID, float x, float y)
		{
			PatchID = patchID;
			OperatorID = operatorID;
			X = x;
			Y = y;
		}
	}
}