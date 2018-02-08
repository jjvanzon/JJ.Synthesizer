using System;
using JJ.Framework.Exceptions;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Framework.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	public class ToolTipGesture : GestureBase
	{
		private const float TOOL_TIP_RECTANGLE_HEIGHT_IN_PIXELS = 20f;
		private const float TEXT_MARGIN_IN_PIXELS = 3f;

		// The benefit of making these static, 
		// is that it decreases the chance of showing several tool tips at the same time.
		private static readonly Rectangle _toolTipRectangle = CreateToolTipRectangle();
		private static readonly Label _toolTipLabel = CreateToolTipLabel();

		public event EventHandler<ToolTipTextEventArgs> ToolTipTextRequested;

		private readonly Diagram _diagram;
		private readonly BackStyle _backStyle;
		private readonly LineStyle _lineStyle;
		private readonly TextStyle _textStyle;
		private readonly ITextMeasurer _textMeasurer;
		private readonly int _zIndex;
		private readonly bool _preferShowOnBottom;
		private readonly MouseLeaveGesture _mouseLeaveGesture;

		private Element _previousElement;

		public ToolTipGesture(
			Diagram diagram,
			BackStyle backStyle,
			LineStyle lineStyle,
			TextStyle textStyle,
			ITextMeasurer textMeasurer,
			bool preferShowOnBottom = false,
			int zIndex = int.MaxValue / 2)
		{
			_diagram = diagram ?? throw new NullException(() => diagram);
			_backStyle = backStyle;
			_lineStyle = lineStyle;
			_textStyle = textStyle;
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));
			_preferShowOnBottom = preferShowOnBottom;
			_zIndex = zIndex;

			_mouseLeaveGesture = new MouseLeaveGesture();
			_mouseLeaveGesture.MouseLeave += _mouseLeaveGesture_MouseLeave;
		}

		~ToolTipGesture()
		{
			if (_mouseLeaveGesture != null)
			{
				_mouseLeaveGesture.MouseLeave -= _mouseLeaveGesture_MouseLeave;
			}
		}

		private static Rectangle CreateToolTipRectangle()
		{
			var rectangle = new Rectangle
			{
				Enabled = false,
				Tag = "ToolTip Rectangle"
			};

			return rectangle;
		}

		private static Label CreateToolTipLabel()
		{
			var label = new Label
			{
				Tag = "ToolTip Label"
			};

			return label;
		}

		protected override void HandleMouseMove(object sender, MouseEventArgs e)
		{
			if (ToolTipTextRequested == null)
			{
				return;
			}

			// TODO: Is e.Element not nullable?
			if (e.Element == null)
			{
				return;
			}

			if (e.Element == _previousElement)
			{
				return;
			}

			var e2 = new ToolTipTextEventArgs(e.Element);
			ToolTipTextRequested(sender, e2);

			if (!string.IsNullOrEmpty(e2.ToolTipText))
			{
				SetToolTipText(e2.Element, e2.ToolTipText);
			}

			_previousElement = e.Element;
		}

		private void _mouseLeaveGesture_MouseLeave(object sender, MouseEventArgs e)
		{
			HideToolTip(e.Element);
		}

		public void SetToolTipText(string text)
		{
			if (_previousElement == null)
			{
				return;
			}

			SetToolTipText(_previousElement, text);
		}

		private void SetToolTipText(Element element, string text)
		{
			if (element == null) throw new NullException(() => element);

			HideToolTip(element); // Also removes it from the previous diagram, if needed.

			if (string.IsNullOrEmpty(text))
			{
				// Keep it hidden, in case of no text.
				return;
			}

			_toolTipRectangle.Diagram = _diagram;
			_toolTipRectangle.Parent = element;
			_toolTipRectangle.Style.BackStyle = _backStyle;
			_toolTipRectangle.Style.LineStyle = _lineStyle;
			_toolTipRectangle.ZIndex = _zIndex;

			_toolTipLabel.Diagram = _diagram;
			_toolTipLabel.Parent = _toolTipRectangle;
			_toolTipLabel.TextStyle = _textStyle;
			_toolTipLabel.ZIndex = _zIndex;
			_toolTipLabel.Text = text;

			// Set text width
			(float width, _)  = _textMeasurer.GetTextSize(text, _toolTipLabel.TextStyle.Font);
			float textWidthInPixels = width;
			float widthInPixels = TEXT_MARGIN_IN_PIXELS * 2f + textWidthInPixels;
			float scaledWidth = _diagram.Position.PixelsToWidth(widthInPixels);
			_toolTipRectangle.Position.Width = scaledWidth;
			_toolTipLabel.Position.Width = scaledWidth;

			// Set height (can change with coordinate scaling)
			float scaledHeight = _diagram.Position.PixelsToHeight(TOOL_TIP_RECTANGLE_HEIGHT_IN_PIXELS);
			_toolTipRectangle.Position.Height = scaledHeight;
			_toolTipLabel.Position.Height = scaledHeight;

			// Set X and Y
			PositionOnTheRight(_toolTipRectangle);
			if (_preferShowOnBottom)
			{
				PositionOnBottom(_toolTipRectangle);
			}
			else
			{
				PositionOnTop(_toolTipRectangle);
			}

			// Correct position if out of diagram bounds.
			bool rightBoundIsExceeded = RightBoundIsExceeded(_toolTipRectangle);
			if (rightBoundIsExceeded)
			{
				PositionOnTheLeft(_toolTipRectangle);
			}

			bool leftBoundIsExceeded = LeftBoundIsExceeded(_toolTipRectangle);
			if (leftBoundIsExceeded)
			{
				PositionOnTheRight(element);
			}

			bool topBoundIsExceeded = TopBoundIsExceeded(_toolTipRectangle);
			if (topBoundIsExceeded)
			{
				PositionOnBottom(_toolTipRectangle);
			}

			bool bottomBoundIsExceeded = BottomBoundIsExceeded(_toolTipRectangle);
			if (bottomBoundIsExceeded)
			{
				PositionOnTop(_toolTipRectangle);
			}

			// Add _mouseLeaveGesture.
			if (!element.Gestures.Contains(_mouseLeaveGesture))
			{
				element.Gestures.Add(_mouseLeaveGesture);
			}
		}

		private bool BottomBoundIsExceeded(Element toolTipElement)
		{
			bool bottomBoundIsExceeded;
			if (!_diagram.Position.YAxisIsFlipped)
			{
				bottomBoundIsExceeded = toolTipElement.Position.AbsoluteBottom > toolTipElement.Diagram.Position.ScaledBottom;
			}
			else
			{
				bottomBoundIsExceeded = toolTipElement.Position.AbsoluteBottom < toolTipElement.Diagram.Position.ScaledBottom;
			}

			return bottomBoundIsExceeded;
		}

		private bool TopBoundIsExceeded(Element toolTipElement)
		{
			bool topBoundIsExceeded;
			if (!_diagram.Position.YAxisIsFlipped)
			{
				topBoundIsExceeded = toolTipElement.Position.AbsoluteY < toolTipElement.Diagram.Position.ScaledY;
			}
			else
			{
				topBoundIsExceeded = toolTipElement.Position.AbsoluteY > toolTipElement.Diagram.Position.ScaledY;
			}

			return topBoundIsExceeded;
		}

		private bool RightBoundIsExceeded(Element toolTipElement)
		{
			bool rightBoundIsExceeded;
			if (!_diagram.Position.XAxisIsFlipped)
			{
				rightBoundIsExceeded = toolTipElement.Position.AbsoluteRight > toolTipElement.Diagram.Position.ScaledRight;
			}
			else
			{
				rightBoundIsExceeded = toolTipElement.Position.AbsoluteRight < toolTipElement.Diagram.Position.ScaledRight;
			}

			return rightBoundIsExceeded;
		}

		private bool LeftBoundIsExceeded(Element element)
		{
			bool leftBoundIsExceeded;
			if (!element.Diagram.Position.XAxisIsFlipped)
			{
				leftBoundIsExceeded = element.Position.AbsoluteX < element.Diagram.Position.ScaledX;
			}
			else
			{
				leftBoundIsExceeded = element.Position.AbsoluteX > element.Diagram.Position.ScaledX;
			}

			return leftBoundIsExceeded;
		}

		// Note it is an assumption that the tool tip height will be similar to the mouse arrow height.

		private static void PositionOnTheLeft(Element toolTipElement)
		{
			toolTipElement.Position.X = toolTipElement.Parent.Position.Width / 2f - toolTipElement.Position.Width;
		}

		private static void PositionOnTheRight(Element toolTipElement)
		{
			toolTipElement.Position.X = toolTipElement.Parent.Position.Width / 2f;
		}

		private static void PositionOnTop(Element toolTipElement)
		{
			toolTipElement.Position.Y = -toolTipElement.Position.Height;
		}

		private static void PositionOnBottom(Element toolTipElement)
		{
			toolTipElement.Position.Y = toolTipElement.Position.Height + toolTipElement.Position.Height;
		}

		private void HideToolTip(Element parentElement)
		{
			if (parentElement == null) throw new NullException(() => parentElement);

			_toolTipLabel.Parent = null;
			_toolTipRectangle.Parent = null;

			_toolTipLabel.Children.Clear();
			_toolTipRectangle.Children.Clear();

			_toolTipLabel.Diagram = null;
			_toolTipRectangle.Diagram = null;

			parentElement.Gestures.Remove(_mouseLeaveGesture);

			_previousElement?.Gestures.Remove(_mouseLeaveGesture);
			_previousElement = null;
		}
	}
}