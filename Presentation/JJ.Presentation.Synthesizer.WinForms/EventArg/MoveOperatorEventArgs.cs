using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class MoveOperatorEventArgs : EventArgs
    {
        public int PatchID { get; private set; }
        public int OperatorID { get; private set; }
        public float CenterX { get; private set; }
        public float CenterY { get; private set; }

        public MoveOperatorEventArgs(int patchID, int operatorID, float centerX, float centerY)
        {
            PatchID = patchID;
            OperatorID = operatorID;
            CenterX = centerX;
            CenterY = centerY;
        }
    }
}