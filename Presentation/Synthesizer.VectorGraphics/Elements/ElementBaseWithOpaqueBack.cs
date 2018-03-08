using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

// ReSharper disable MemberCanBeProtected.Global

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	public class ElementBaseWithOpaqueBack : ElementBase
	{
		protected readonly Rectangle _backRectangle;

		public ElementBaseWithOpaqueBack(Element parent, RectangleStyle rectangleStyle = null) : base(parent)
			=> _backRectangle = CreateRectangle(rectangleStyle);

		public virtual void PositionElements()
		{
			_backRectangle.Position.Width = Position.Width;
			_backRectangle.Position.Height = Position.Height;
		}

		private Rectangle CreateRectangle(RectangleStyle rectangleStyle)
		{
			var rectangle = new Rectangle(this)
			{
				Style = rectangleStyle ?? StyleHelper.RectangleStyleNeutralGrayBack
			};
			rectangle.Position.Height = StyleHelper.ROW_HEIGHT;

			return rectangle;
		}
	}
}