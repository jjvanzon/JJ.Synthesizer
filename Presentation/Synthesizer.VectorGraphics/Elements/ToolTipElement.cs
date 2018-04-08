using System;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Framework.VectorGraphics.Models.Styling;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Elements
{
	/// <summary> Instantiate just one if you do not want to be plagued by multiple tool tips showing at the same time. </summary>
	public class ToolTipElement : Rectangle
	{
		private const float TEXT_MARGIN_IN_PIXELS = 3f;
		//private const float TOOL_TIP_RECTANGLE_HEIGHT_IN_PIXELS = 20f;

		private readonly ITextMeasurer _textMeasurer;
		private readonly Label _label;

		public ToolTipElement(
			Element parent,
			BackStyle backStyle,
			LineStyle lineStyle,
			TextStyle textStyle,
			ITextMeasurer textMeasurer,
			int zIndex = int.MaxValue / 2) : base(parent)
		{
			Enabled = false;
			Tag = "ToolTip";
			ZIndex = zIndex;
			Style.BackStyle = backStyle;
			Style.LineStyle = lineStyle;

			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

			_label = new Label(this)
			{
				Tag = "ToolTip Label",
				ZIndex = zIndex,
				TextStyle = textStyle
			};
		}

		public override void Dispose()
		{
			// Do not dispose the ToolTipElement of a parent that just got deleted.
			// Keep the ToolTipElement in the diagram, but hide it.
			Parent = Diagram.Background;
			Visible = false;
		}

		/// <summary> Sets the text and resizes the elements to fit the text. </summary>
		public void SetText(string text)
		{
			_label.Text = text;

			// Set text width
			(float textWidthInPixels, float textHeightInPixels) = _textMeasurer.GetTextSize(text, _label.TextStyle.Font);
			float widthInPixels = textWidthInPixels + TEXT_MARGIN_IN_PIXELS * 2f;
			float heightInPixels = textHeightInPixels + TEXT_MARGIN_IN_PIXELS * 2f;

			float scaledWidth = Diagram.Position.PixelsToWidth(widthInPixels);
			float scaledHeight = Diagram.Position.PixelsToHeight(heightInPixels);
			Position.Width = scaledWidth;
			_label.Position.Width = scaledWidth;
			Position.Height = scaledHeight;
			_label.Position.Height = scaledHeight;

			// Set height (can change with coordinate scaling)
			//float scaledHeight = Diagram.Position.PixelsToHeight(TOOL_TIP_RECTANGLE_HEIGHT_IN_PIXELS);
			//Position.Height = scaledHeight;
			//_label.Position.Height = scaledHeight;
		}
	}
}