using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
	internal class PatchAndOperatorEventArgs : EventArgs
	{
		public int PatchID { get; }
		public int OperatorID { get; }

		public PatchAndOperatorEventArgs(int patchID, int operatorID)
		{
			PatchID = patchID;
			OperatorID = operatorID;
		}
	}
}