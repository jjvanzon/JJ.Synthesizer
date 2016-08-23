using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class StringEventArgs : EventArgs
    {
        public string Value { get; }

        public StringEventArgs(string value)
        {
            Value = value;
        }
    }
}
