using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class EventArgs<T> : EventArgs
    {
        public T Value { get; }

        public EventArgs(T value)
        {
            Value = value;
        }
    }
}
