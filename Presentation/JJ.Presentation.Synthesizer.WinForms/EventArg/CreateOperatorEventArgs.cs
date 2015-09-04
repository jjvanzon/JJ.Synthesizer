using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class CreateOperatorEventArgs : EventArgs
    {
        public int OperatorTypeID { get; private set; }

        public CreateOperatorEventArgs(int operatorTypeID)
        {
            OperatorTypeID = operatorTypeID;
        }
    }
}