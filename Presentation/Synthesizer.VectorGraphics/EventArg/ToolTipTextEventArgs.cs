using System;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.VectorGraphics.Models.Elements;

namespace JJ.Presentation.Synthesizer.VectorGraphics.EventArg
{
	public class ToolTipTextEventArgs : EventArgs
	{
		public Element Element { get; }
		public string ToolTipText { get; set; }

		public ToolTipTextEventArgs(Element element)
		{
			Element = element ?? throw new NullException(() => element);
		}
	}
}
