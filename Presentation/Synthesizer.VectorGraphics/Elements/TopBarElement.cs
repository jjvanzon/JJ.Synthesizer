using System.Linq;
using JJ.Framework.Resources;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    public class TopBarElement : ElementBase
    {
        public TopButtonBarElement TopButtonBarElement { get; }
        public InstrumentBarElement InstrumentBarElement { get; }
        public PictureButtonElement DocumentClosePictureButton { get; }

        public TopBarElement(Element parent, UnderlyingPictureWrapper underlyingPictureWrapper, ITextMeasurer textMeasurer)
            : base(parent)
        {
            var toolTipElement = new ToolTipElement(
                parent?.Diagram?.Background,
                StyleHelper.ToolTipBackStyle,
                StyleHelper.ToolTipLineStyle,
                StyleHelper.ToolTipTextStyle,
                textMeasurer);

            TopButtonBarElement = new TopButtonBarElement(this, toolTipElement, underlyingPictureWrapper);
            InstrumentBarElement = new InstrumentBarElement(this, underlyingPictureWrapper, textMeasurer);
            DocumentClosePictureButton = new PictureButtonElement(this, underlyingPictureWrapper.PictureClose, CommonResourceFormatter.Close, toolTipElement);
        }

        public void PositionElements()
        {
            TopButtonBarElement.PositionElements();

            DocumentClosePictureButton.Position.Right = Position.Width - StyleHelper.SPACING_SMALL;

            InstrumentBarElement.Position.Width = DocumentClosePictureButton.Position.X -
                                                   TopButtonBarElement.MaxWidth -
                                                   StyleHelper.SPACING_SMALL_TIMES_2;

            InstrumentBarElement.Position.X = TopButtonBarElement.MaxWidth + StyleHelper.SPACING_SMALL;
            InstrumentBarElement.PositionElements();

            Position.Height = new[] { InstrumentBarElement.Position.Height, DocumentClosePictureButton.Position.Height, TopButtonBarElement.Position.Height }.Max();
        }
    }
}