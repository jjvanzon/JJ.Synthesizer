using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Presentation.VectorGraphics.Models.Styling;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.EventArg;
using System;
using JJ.Framework.Presentation.VectorGraphics.Helpers;

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
        private readonly int _zIndex;
        private readonly MouseLeaveGesture _mouseLeaveGesture;

        private Element _previousElement;

        public ToolTipGesture(Diagram diagram, BackStyle backStyle, LineStyle lineStyle, TextStyle textStyle, int zIndex = Int32.MaxValue / 2)
        {
            if (diagram == null) throw new NullException(() => diagram);

            _diagram = diagram;
            _backStyle = backStyle;
            _lineStyle = lineStyle;
            _textStyle = textStyle;
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

            if (!String.IsNullOrEmpty(e2.ToolTipText))
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

            if (String.IsNullOrEmpty(text))
            {
                // Keep it hidden, in case of no text.
                return;
            }

            _toolTipRectangle.Diagram = _diagram;
            _toolTipRectangle.Parent = element;
            _toolTipRectangle.BackStyle = _backStyle;
            _toolTipRectangle.LineStyle = _lineStyle;
            _toolTipRectangle.ZIndex = _zIndex;
            _toolTipLabel.Diagram = _diagram;
            _toolTipLabel.Parent = _toolTipRectangle;
            _toolTipLabel.TextStyle = _textStyle;
            _toolTipLabel.ZIndex = _zIndex;

            _toolTipLabel.Text = text;

            // Set text width
            float textWidthInPixels = TextHelper.ApproximateTextWidth(text, _toolTipLabel.TextStyle.Font);
            float widthInPixels = TEXT_MARGIN_IN_PIXELS * 2f + textWidthInPixels;
            float scaledWidth = _diagram.Position.PixelsToWidth(widthInPixels);
            _toolTipRectangle.Position.Width = scaledWidth;
            _toolTipLabel.Position.Width = scaledWidth;

            // Set height (can change with coordinate scaling)
            float scaledHeight = _diagram.Position.PixelsToHeight(TOOL_TIP_RECTANGLE_HEIGHT_IN_PIXELS);
            _toolTipRectangle.Position.Height = scaledHeight;
            _toolTipLabel.Position.Height = scaledHeight;

            _toolTipRectangle.Position.X = element.Position.Width / 2f;
            _toolTipRectangle.Position.Y = -_toolTipRectangle.Position.Height;

            // Correct position if out of diagram bounds.
            bool rightBoundIsExceeded;
            if (!_diagram.Position.XAxisIsFlipped)
            {
                rightBoundIsExceeded = _toolTipRectangle.Position.AbsoluteRight > _diagram.Position.ScaledRight;
            }
            else
            {
                rightBoundIsExceeded = _toolTipRectangle.Position.AbsoluteRight < _diagram.Position.ScaledRight;
            }

            if (rightBoundIsExceeded)
            {
                _toolTipRectangle.Position.X -= _toolTipRectangle.Position.Width;
            }

            bool topBoundIsExceeded;
            if (!_diagram.Position.YAxisIsFlipped)
            {
                topBoundIsExceeded = _toolTipRectangle.Position.AbsoluteY < _diagram.Position.ScaledY;
            }
            else
            {
                topBoundIsExceeded = _toolTipRectangle.Position.AbsoluteY > _diagram.Position.ScaledY;
            }

            if (topBoundIsExceeded)
            {
                _toolTipRectangle.Position.Y = element.Position.Height + _toolTipRectangle.Position.Height; // Note it is an assumption that the tool tip height will be similar to the mouse arrow height.
            }

            // Add _mouseLeaveGesture.
            if (!element.Gestures.Contains(_mouseLeaveGesture))
            {
                element.Gestures.Add(_mouseLeaveGesture);
            }
        }

        private void HideToolTip(Element element)
        {
            if (element == null) throw new NullException(() => element);

            _toolTipLabel.Parent = null;
            _toolTipRectangle.Parent = null;

            _toolTipLabel.Children.Clear();;
            _toolTipRectangle.Children.Clear();

            _toolTipLabel.Diagram = null;
            _toolTipRectangle.Diagram = null;

            element.Gestures.Remove(_mouseLeaveGesture);

            if (_previousElement != null)
            {
                _previousElement.Gestures.Remove(_mouseLeaveGesture);
            }
            _previousElement = null;
        }
    }
}