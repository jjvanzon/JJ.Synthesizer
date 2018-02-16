using System;
using JJ.Framework.Exceptions;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	public class ToolTipGesture : GestureBase
	{
		public event EventHandler<ToolTipTextEventArgs> ToolTipTextRequested;

		private readonly ToolTipElement _toolTipElement;
		private readonly bool _preferShowOnBottom;
		private readonly MouseLeaveGesture _mouseLeaveGesture;

		private Element _previousElement;

		public ToolTipGesture(ToolTipElement toolTipElement, bool preferShowOnBottom = false)
		{
			_toolTipElement = toolTipElement ?? throw new ArgumentNullException(nameof(toolTipElement));
			_preferShowOnBottom = preferShowOnBottom;

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

			_toolTipElement.Parent = element;

			_toolTipElement.SetText(text);

			// Set X and Y
			PositionOnTheRight(_toolTipElement);
			if (_preferShowOnBottom)
			{
				PositionOnBottom(_toolTipElement);
			}
			else
			{
				PositionOnTop(_toolTipElement);
			}

			// Correct position if out of diagram bounds.
			bool rightBoundIsExceeded = RightBoundIsExceeded(_toolTipElement);
			if (rightBoundIsExceeded)
			{
				PositionOnTheLeft(_toolTipElement);
			}

			bool leftBoundIsExceeded = LeftBoundIsExceeded(_toolTipElement);
			if (leftBoundIsExceeded)
			{
				PositionOnTheRight(_toolTipElement);
			}

			bool topBoundIsExceeded = TopBoundIsExceeded(_toolTipElement);
			if (topBoundIsExceeded)
			{
				PositionOnBottom(_toolTipElement);
			}

			bool bottomBoundIsExceeded = BottomBoundIsExceeded(_toolTipElement);
			if (bottomBoundIsExceeded)
			{
				PositionOnTop(_toolTipElement);
			}

			// Add _mouseLeaveGesture.
			if (!element.Gestures.Contains(_mouseLeaveGesture))
			{
				element.Gestures.Add(_mouseLeaveGesture);
			}

			_toolTipElement.Visible = true;
		}

		private bool BottomBoundIsExceeded(Element toolTipElement)
		{
			bool bottomBoundIsExceeded;
			if (!toolTipElement.Diagram.Position.YAxisIsFlipped)
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
			if (!toolTipElement.Diagram.Position.YAxisIsFlipped)
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
			if (!toolTipElement.Diagram.Position.XAxisIsFlipped)
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

			_toolTipElement.Visible = false;

			parentElement.Gestures.Remove(_mouseLeaveGesture);

			_previousElement?.Gestures.Remove(_mouseLeaveGesture);
			_previousElement = null;
		}
	}
}