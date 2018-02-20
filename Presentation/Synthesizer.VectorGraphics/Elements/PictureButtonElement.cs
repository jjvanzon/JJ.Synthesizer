using System;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable once SuggestBaseTypeForParameter

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	internal class PictureButtonElement : ElementBase
	{
		private readonly MouseDownGesture _mouseDownGesture;

		public event EventHandler<MouseEventArgs> MouseDown
		{
			add => _mouseDownGesture.MouseDown += value;
			remove => _mouseDownGesture.MouseDown -= value;
		}

		public PictureButtonElement(Element parent, object underlyingPicture, string toolTipText, ToolTipElement toolTipElement)
			: base(parent)
		{
			_mouseDownGesture = new MouseDownGesture();
			Picture picture = CreatePicture(underlyingPicture, toolTipText, toolTipElement, _mouseDownGesture);

			Position.Width = picture.Position.Width;
			Position.Height = picture.Position.Height;
		}

		private Picture CreatePicture(
			object underlyingPicture,
			string toolTipText,
			ToolTipElement toolTipElement,
			MouseDownGesture mouseDownGesture)
		{
			var picture = new Picture(this)
			{
				UnderlyingPicture = underlyingPicture
			};
			picture.Position.Width = StyleHelper.ICON_BUTTON_PICTURE_SIZE;
			picture.Position.Height = StyleHelper.ICON_BUTTON_PICTURE_SIZE;
			picture.Position.Y = StyleHelper.ICON_BUTTON_MARGIN;
			picture.Style = StyleHelper.IconPictureStyle;

			picture.Gestures.Add(mouseDownGesture);

			var toolTipGesture = new ToolTipGesture(toolTipElement, toolTipText, preferredSideToShowToolTip: ToolTipPositioningEnum.CenterRight);
			picture.Gestures.Add(toolTipGesture);

			return picture;
		}
	}
}