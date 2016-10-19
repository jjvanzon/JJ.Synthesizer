using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class NodeEventArgs : EventArgs
    {
        public int CurveID { get; }
        public int NodeID { get; }

        public NodeEventArgs(int curveID, int nodeID)
        {
            CurveID = curveID;
            NodeID = nodeID;
        }
    }
}
