using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
	internal class ChangeInputOutletEventArgs : EventArgs
	{
		public int PatchID { get; }
		public int InletID { get; }
		public int InputOutletID { get; }

		public ChangeInputOutletEventArgs(int patchID, int inletID, int inputOutletID)
		{
			PatchID = patchID;
			InletID = inletID;
			InputOutletID = inputOutletID;
		}
	}
}