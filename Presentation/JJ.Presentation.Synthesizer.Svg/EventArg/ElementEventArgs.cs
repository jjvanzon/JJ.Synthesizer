using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Presentation.Synthesizer.Svg.EventArg
{
    public class ElementEventArgs : EventArgs
    {
        public Element Element { get; private set; }

        public ElementEventArgs(Element element)
        {
            if (element == null) throw new NullException(() => element);

            Element = element;
        }
    }
}
