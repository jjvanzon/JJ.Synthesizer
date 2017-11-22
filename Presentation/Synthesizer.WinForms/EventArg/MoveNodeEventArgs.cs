using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
	internal class MoveNodeEventArgs : EventArgs
	{
		public int CurveID { get; }
		public int NodeID { get; }
		public float X { get; }
		public float Y { get; }

		public MoveNodeEventArgs(int curveID, int nodeID, float x, float y)
		{
			CurveID = curveID;
			NodeID = nodeID;
			X = x;
			Y = y;
		}
	}
}