using System;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Framework.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable once SuggestBaseTypeForParameter

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	internal class PictureButtonElement : ElementBase
	{
		private readonly MouseDownGesture _mouseDownGesture;
		private readonly Picture _picture;

		public event EventHandler<MouseEventArgs> MouseDown
		{
			add => _mouseDownGesture.MouseDown += value;
			remove => _mouseDownGesture.MouseDown -= value;
		}

		public PictureStyle PictureStyle
		{
			// ReSharper disable once UnusedMember.Global
			get => _picture.Style;
			set => _picture.Style = value;
		}

		public PictureButtonElement(Element parent, object underlyingPicture, string toolTipText, ToolTipElement toolTipElement)
			: base(parent)
		{
			_mouseDownGesture = new MouseDownGesture();
			_picture = CreatePicture(underlyingPicture, _mouseDownGesture);

			var toolTipGesture = new ToolTipGesture(toolTipElement, toolTipText, preferredSideToShowToolTip: ToolTipPositioningEnum.CenterRight);
			Gestures.Add(toolTipGesture);

			Position.Width = StyleHelper.PICTURE_BUTTON_PICTURE_SIZE;
			Position.Height = StyleHelper.PICTURE_BUTTON_MARGIN + StyleHelper.PICTURE_BUTTON_PICTURE_SIZE + StyleHelper.PICTURE_BUTTON_MARGIN;
		}

		private Picture CreatePicture(object underlyingPicture, MouseDownGesture mouseDownGesture)
        {
            var picture = new Picture(this)
            {
                UnderlyingPicture = underlyingPicture,
                Style = StyleHelper.IconPictureStyle
            };

            picture.Position.Width = StyleHelper.PICTURE_BUTTON_PICTURE_SIZE;
            picture.Position.Height = StyleHelper.PICTURE_BUTTON_PICTURE_SIZE;

            picture.Gestures.Add(mouseDownGesture);

            return picture;
        }

        public void PositionElements()
        {
            _picture.Position.CenterX = Position.Width / 2;
            _picture.Position.CenterY = Position.Height / 2;
        }
    }
}