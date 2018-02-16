using JJ.Framework.VectorGraphics.Models.Elements;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	public class ElementBase : Element
	{
		// ReSharper disable once MemberCanBeProtected.Global
		public ElementBase(Element parent) : base(parent) => Position = new RectanglePosition(this);

		public override ElementPosition Position { get; }
	}
}