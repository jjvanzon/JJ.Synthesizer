using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
	internal class ScaleAndToneEventArgs : EventArgs
	{
		public int ScaleID { get; }
		public int ToneID { get; }

		public ScaleAndToneEventArgs(int scaleID, int toneID)
		{
			ScaleID = scaleID;
			ToneID = toneID;
		}

	}
}
