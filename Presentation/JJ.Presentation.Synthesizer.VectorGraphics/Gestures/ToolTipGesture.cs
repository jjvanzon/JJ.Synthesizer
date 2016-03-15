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
        private Rectangle _rectangle;
        private Label _label;
        private MouseLeaveGesture _mouseLeaveGesture;

        private Element _previousElement;

        public ToolTipGesture(Diagram diagram, BackStyle backStyle, LineStyle lineStyle, TextStyle textStyle, int zIndex = Int32.MaxValue / 2)
        {
            if (diagram == null) throw new NullException(() => diagram);

            _diagram = diagram;

            _rectangle = new Rectangle
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

            _label = new Label
            {
                Diagram = diagram,
                Parent = _rectangle,
                TextStyle = textStyle,
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
                _rectangle.Diagram = _diagram;
                _rectangle.Parent = e.Element;

                // Set text width
                float textWidthInPixels = TextHelper.ApproximateTextWidth(e2.ToolTipText, _label.TextStyle.Font);
                float widthInPixels = MARGIN_IN_PIXELS * 2f + textWidthInPixels;
                float scaledWidth = ScaleHelper.PixelsToWidth(_diagram, widthInPixels);
                _rectangle.Width = scaledWidth;
                _label.Width = scaledWidth;

                // Set height (can change with scaling)
                float scaledHeight = ScaleHelper.PixelsToHeight(_diagram, RECTANGLE_HEIGHT_IN_PIXELS);
                _rectangle.Height = scaledHeight;
                _label.Height = scaledHeight;

                _rectangle.X = e.Element.Width / 2f;
                _rectangle.Y = -_rectangle.Height;

                _label.Text = e2.ToolTipText;
                _rectangle.Visible = true;

                if (!e.Element.Gestures.Contains(_mouseLeaveGesture))
                {
                    e.Element.Gestures.Add(_mouseLeaveGesture);
                }
            }

            _previousElement = e.Element;
        }

        private void _mouseLeaveGesture_MouseLeave(object sender, MouseEventArgs e)
        {
            _rectangle.Visible = false;
            
            e.Element.Gestures.Remove(_mouseLeaveGesture);

            _previousElement = null;
        }
    }
}
