using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
    public sealed class TitleBarElement : ElementBase
    {
        public Label TitleLabel { get; }
        public ButtonBarElement ButtonBarElement { get; }

        public TitleBarElement(
            Element parent,
            ITextMeasurer textMeasurer,
            UnderlyingPictureWrapper underlyingPictureWrapper)
            : base(parent)
        {
            var toolTipElement = new ToolTipElement(
                parent?.Diagram?.Background,
                StyleHelper.ToolTipBackStyle,
                StyleHelper.ToolTipLineStyle,
                StyleHelper.ToolTipTextStyle,
                textMeasurer);

            TitleLabel = new Label(this) { TextStyle = StyleHelper.TitleTextStyle };
            ButtonBarElement = new ButtonBarElement(this, toolTipElement, underlyingPictureWrapper);

            TitleLabel.Position.Height = StyleHelper.ROW_HEIGHT;
            Position.Height = StyleHelper.ROW_HEIGHT;

            // Magic Defaults
            ButtonBarElement.PictureButtonClose.Visible = true;
        }

        // Positioning

        public float ButtonBarWidth => ButtonBarElement.Position.Width;

        public void PositionElements()
        {
            ButtonBarElement.PositionElements();

            TitleLabel.Position.Width = Position.Width - ButtonBarElement.Position.Width;
            ButtonBarElement.Position.X = Position.Width - ButtonBarElement.Position.Width;
        }
    }
}