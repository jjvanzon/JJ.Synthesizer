using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.EventArg
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
