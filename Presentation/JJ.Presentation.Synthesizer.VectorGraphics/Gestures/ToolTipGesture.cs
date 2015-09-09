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
        private const int RECTANGLE_HEIGHT = 20;

        public event EventHandler<ToolTipTextEventArgs> ToolTipTextRequested;

        private Diagram _diagram;
        private Rectangle _rectangle;
        private Label _label;
        private MouseLeaveGesture _mouseLeaveGesture;

        public ToolTipGesture(Diagram diagram, BackStyle backStyle, LineStyle lineStyle, TextStyle textStyle, int zIndex = Int32.MaxValue / 2)
        {
            if (diagram == null) throw new NullException(() => diagram);

            _diagram = diagram;

            _rectangle = new Rectangle
            {
                Diagram = diagram,
                Parent = diagram.Canvas,
                BackStyle = backStyle,
                LineStyle = lineStyle,
                Visible = false,
                Enabled = false,
                ZIndex = zIndex,
                Height = RECTANGLE_HEIGHT,
                Tag = "ToolTip Rectangle"
            };

            _label = new Label
            {
                Diagram = diagram,
                Parent = _rectangle,
                TextStyle = textStyle,
                ZIndex = zIndex + 1,
                Height = RECTANGLE_HEIGHT,
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

        public override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (e == null) throw new NullException(() => e);

            if (ToolTipTextRequested == null)
            {
                return;
            }

            // TODO: Is e.Element not nullable?
            if (e.Element == null)
            {
                return;
            }

            var e2 = new ToolTipTextEventArgs(e.Element);

            ToolTipTextRequested(sender, e2);

            if (!String.IsNullOrEmpty(e2.ToolTipText))
            {
                // Set text width
                float margin = 3f;
                float textWidth = TextHelper.ApproximateTextWidth(e2.ToolTipText, _label.TextStyle.Font);
                float width = margin * 2f + textWidth;
                _rectangle.Width = width;
                _label.Width = width; 

                _rectangle.Diagram = _diagram;
                _rectangle.Parent = e.Element;
                _rectangle.X = e.X - e.Element.CalculatedX - margin; // The relative coordinate, move some distance to the left.
                _rectangle.Y = e.Y - e.Element.CalculatedY - _rectangle.Height - margin; // The relative coordinate, transposed to above the mouse arrow, plus some distance upward.
                _label.Text = e2.ToolTipText;
                _rectangle.Visible = true;

                if (!e.Element.Gestures.Contains(_mouseLeaveGesture))
                {
                    e.Element.Gestures.Add(_mouseLeaveGesture);
                }
            }
        }

        private void _mouseLeaveGesture_MouseLeave(object sender, MouseEventArgs e)
        {
            _rectangle.Visible = false;
            
            e.Element.Gestures.Remove(_mouseLeaveGesture);
        }
    }
}
