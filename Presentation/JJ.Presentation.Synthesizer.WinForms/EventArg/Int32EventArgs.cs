using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class Int32EventArgs : EventArgs
    {
        public int Value { get; private set; }

        public Int32EventArgs(int value)
        {
            Value = value;
        }
    }
}
