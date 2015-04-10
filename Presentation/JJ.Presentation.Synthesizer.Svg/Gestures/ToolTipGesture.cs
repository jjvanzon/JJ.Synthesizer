using JJ.Framework.Mathematics;
using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Framework.Presentation.Svg.Gestures;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Presentation.Svg.Models.Styling;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Svg.EventArg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Svg.Gestures
{
    public class ToolTipGesture : GestureBase
    {
        private const int RECTANGLE_WIDTH = 125;
        private const int RECTANGLE_HEIGHT = 50;

        private Diagram _diagram;
        private Rectangle _rectangle;
        private Label _label;

        private TextStyle _textStyle;
        private int _zIndex;

        public event EventHandler<ToolTipTextEventArgs> ToolTipTextRequested;

        // TODO: I must implement mouse leave for this.
        //public event EventHandler<ElementEventArgs> HideToolTip;

        public ToolTipGesture(Diagram diagram, BackStyle backStyle, LineStyle lineStyle, TextStyle textStyle, int zIndex = Int32.MaxValue / 2)
        {
            if (diagram == null) throw new NullException(() => diagram);

            _textStyle = textStyle;
            _zIndex = zIndex;

            _diagram = diagram;

            _rectangle = new Rectangle
            {
                Diagram = diagram,
                BackStyle = backStyle,
                LineStyle = lineStyle,
                Width = RECTANGLE_WIDTH,
                Height = RECTANGLE_HEIGHT,
                Visible = false,
                ZIndex = zIndex,
                Tag = "ToolTip Rectangle"
            };

            _label = new Label
            {
                Diagram = diagram,
                Parent = _rectangle,
                TextStyle = textStyle,
                Width = RECTANGLE_WIDTH,
                Height = RECTANGLE_HEIGHT,
                ZIndex = zIndex + 1,
                Tag = "ToolTip Label"
            };
        }

        public override void HandleMouseMove(object sender, MouseEventArgs e)
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

            var e2 = new ToolTipTextEventArgs(e.Element);

            ToolTipTextRequested(sender, e2);

            if (!String.IsNullOrEmpty(e2.ToolTipText))
            {
                // TODO: Adapt width to text width.
                _rectangle.Visible = false;

                _rectangle.Parent = e.Element;
                _rectangle.X = e.X - e.Element.CalculatedX;
                _rectangle.Y = e.Y - e.Element.CalculatedY;

                //_rectangle.X = e.X;
                //_rectangle.Y = e.Y;

                _label.Text = e2.ToolTipText;
                // HACK: Something is off with the inverse property management, it seems,
                // which messes up the label parent. Perhaps this hack will work.
                _label.Parent = _rectangle;

                _rectangle.Visible = true;

                IList<Element> occurrences = _diagram.Canvas.Children
                                                            .UnionRecursive(x => x.Children)
                                                            .Where(x => String.Equals(x.Tag, _rectangle.Tag))
                                                            .ToArray();

                _diagram.Recalculate();
            }
        }
    }
}
