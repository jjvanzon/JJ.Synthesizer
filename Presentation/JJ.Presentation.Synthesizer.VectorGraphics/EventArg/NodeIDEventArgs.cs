using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.VectorGraphics.EventArg
{
    public class NodeIDEventArgs : EventArgs
    {
        public int NodeID { get; private set; }

        public NodeIDEventArgs(int nodeID)
        {
            NodeID = nodeID;
        }
    }
}
