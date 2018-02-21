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
			CreatePicture(underlyingPicture, _mouseDownGesture);

			var toolTipGesture = new ToolTipGesture(toolTipElement, toolTipText, preferredSideToShowToolTip: ToolTipPositioningEnum.CenterRight);
			Gestures.Add(toolTipGesture);

			Position.Width = StyleHelper.ICON_BUTTON_PICTURE_SIZE;
			Position.Height = StyleHelper.ICON_BUTTON_MARGIN + StyleHelper.ICON_BUTTON_PICTURE_SIZE + StyleHelper.ICON_BUTTON_MARGIN;
		}

		private void CreatePicture(object underlyingPicture, MouseDownGesture mouseDownGesture)
		{
			var picture = new Picture(this)
			{
				UnderlyingPicture = underlyingPicture,
				Style = StyleHelper.IconPictureStyle
			};

			picture.Position.Width = StyleHelper.ICON_BUTTON_PICTURE_SIZE;
			picture.Position.Height = StyleHelper.ICON_BUTTON_PICTURE_SIZE;
			picture.Position.Y = StyleHelper.ICON_BUTTON_MARGIN;

			picture.Gestures.Add(mouseDownGesture);
		}
	}
}