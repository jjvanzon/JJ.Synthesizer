using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class StringEventArgs : EventArgs
    {
        public string Value { get; private set; }

        public StringEventArgs(string value)
        {
            Value = value;
        }
    }
}
