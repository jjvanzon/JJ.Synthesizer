using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Exceptions;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.EventArg
{
    public class ToolTipTextEventArgs : EventArgs
    {
        public Element Element { get; private set; }
        public string ToolTipText { get; set; }

        public ToolTipTextEventArgs(Element element)
        {
            if (element == null) throw new NullException(() => element);

            Element = element;
        }
    }
}
