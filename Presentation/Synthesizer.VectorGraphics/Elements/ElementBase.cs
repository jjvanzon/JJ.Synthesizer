using JJ.Framework.VectorGraphics.Models.Elements;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	internal class ElementBase : Element
	{
		// ReSharper disable once MemberCanBeProtected.Global
		public ElementBase() => Position = new RectanglePosition(this);

		public override ElementPosition Position { get; }
	}
}