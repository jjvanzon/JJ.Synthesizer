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
        private const float RECTANGLE_HEIGHT_IN_PIXELS = 20f;
        private const float MARGIN_IN_PIXELS = 3f;

        public event EventHandler<ToolTipTextEventArgs> ToolTipTextRequested;

        private Diagram _diagram;
        private Rectangle _toolTipRectangle;
        private Label _toolTipLabel;
        private MouseLeaveGesture _mouseLeaveGesture;

        private Element _previousElement;

        public ToolTipGesture(Diagram diagram, BackStyle backStyle, LineStyle lineStyle, TextStyle textStyle, int zIndex = Int32.MaxValue / 2)
        {
            if (diagram == null) throw new NullException(() => diagram);

            _diagram = diagram;

            _toolTipRectangle = new Rectangle
            {
                Diagram = diagram,
                Parent = diagram.Background,
                BackStyle = backStyle,
                LineStyle = lineStyle,
                Visible = false,
                Enabled = false,
                ZIndex = zIndex,
                Tag = "ToolTip Rectangle"
            };

            _toolTipLabel = new Label
            {
                Diagram = diagram,
                Parent = _toolTipRectangle,
                TextStyle = textStyle,
                // TODO: In theory you do not need this zIndex.
                ZIndex = zIndex + 1,
                Tag = "ToolTip Label"
            };

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

            if (!String.IsNullOrEmpty(e2.ToolTipText))
            {
                SetToolTipText(e2.Element, e2.ToolTipText);
            }

            _previousElement = e.Element;
        }

        private void _mouseLeaveGesture_MouseLeave(object sender, MouseEventArgs e)
        {
            Hide(e.Element);
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

            if (String.IsNullOrEmpty(text))
            {
                Hide(element);
            }
            _toolTipRectangle.Diagram = _diagram;
            _toolTipRectangle.Parent = element;

            // Set text width
            float textWidthInPixels = TextHelper.ApproximateTextWidth(text, _toolTipLabel.TextStyle.Font);
            float widthInPixels = MARGIN_IN_PIXELS * 2f + textWidthInPixels;
            float scaledWidth = ScaleHelper.PixelsToWidth(_diagram, widthInPixels);
            _toolTipRectangle.Width = scaledWidth;
            _toolTipLabel.Width = scaledWidth;

            // Set height (can change with scaling)
            float scaledHeight = ScaleHelper.PixelsToHeight(_diagram, RECTANGLE_HEIGHT_IN_PIXELS);
            _toolTipRectangle.Height = scaledHeight;
            _toolTipLabel.Height = scaledHeight;

            _toolTipRectangle.X = element.Width / 2f;
            _toolTipRectangle.Y = -_toolTipRectangle.Height;

            _toolTipLabel.Text = text;
            _toolTipRectangle.Visible = true;

            if (!element.Gestures.Contains(_mouseLeaveGesture))
            {
                element.Gestures.Add(_mouseLeaveGesture);
            }
        }

        private void Hide(Element element)
        {
            if (element == null) throw new NullException(() => element);

            _toolTipRectangle.Visible = false;

            // TODO: Use PreviousElement instead?
            element.Gestures.Remove(_mouseLeaveGesture);

            _previousElement = null;
        }
    }
}
