using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Framework.Presentation.Svg.Gestures;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Presentation.Svg.Models.Styling;
using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg.Gestures
{
    // TODO: Make this internal if it proves we are never going to expose this type.
    public class LineGesture : GestureBase, IDisposable
    {
        private Diagram _diagram;
        private Line _line;
        private float _mouseDownX;
        private float _mouseDownY;

        private DragGesture _dragGesture;
        private DropGesture _dropGesture;
        private IList<IGesture> _baseGestures;

        // TODO: This does not work. You would have to have two gestures:
        // one for Line dragging and one for Line dropping,
        // because they are applied to different elements.
        public event EventHandler<DraggingEventArgs> Dragging
        {
            add { _dragGesture.Dragging += value; }
            remove { _dragGesture.Dragging -= value; }
        }

        public event EventHandler<DroppedEventArgs> Dropped
        {
            add { _dropGesture.Dropped += value; }
            remove { _dropGesture.Dropped -= value; }
        }

        public LineGesture(Diagram diagram, LineStyle lineStyle = null, int lineZIndex = 0)
        {
            if (diagram == null) throw new NullException(() => diagram);

            _diagram = diagram;

            _line = new Line
            { 
                Diagram = _diagram,
                ZIndex = lineZIndex,
                Visible = false,
                Tag = "LineGesture Line"
            };

            if (lineStyle != null)
            {
                _line.LineStyle = lineStyle;
            };

            _dragGesture = new DragGesture();
            _dropGesture = new DropGesture();

            _baseGestures = new IGesture[] { _dragGesture, _dropGesture };

            _dragGesture.Dragging += _dragGesture_Dragging;
            _dragGesture.DragCancelled += _dragGesture_DragCancelled;
            _dropGesture.Dropped += _dropGesture_Dropped;
        }

        ~LineGesture()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_dragGesture != null)
            {
                _dragGesture.Dragging -= _dragGesture_Dragging;
                _dropGesture.Dropped -= _dropGesture_Dropped;
            }

            GC.SuppressFinalize(this);
        }

        // IGesture

        public override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            foreach (IGesture baseGesture in _baseGestures)
            {
                baseGesture.HandleMouseDown(sender, e);
            }

            if (e.Element != null)
            {
                _mouseDownX = e.X;
                _mouseDownY = e.Y;
            }
        }

        public override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            foreach (IGesture baseGesture in _baseGestures)
            {
                baseGesture.HandleMouseMove(sender, e);
            }
        }

        public override void HandleMouseUp(object sender, MouseEventArgs e)
        {
            foreach (IGesture baseGesture in _baseGestures)
            {
                baseGesture.HandleMouseUp(sender, e);
            }

            _line.Visible = false;
        }

        // Events

        private void _dragGesture_Dragging(object sender, DraggingEventArgs e)
        {
            _line.PointA.X = _mouseDownX;
            _line.PointA.Y = _mouseDownY;

            _line.PointB.X = e.X;
            _line.PointB.Y = e.Y;

            _line.Visible = true;
        }

        void _dropGesture_Dropped(object sender, DroppedEventArgs e)
        {
            _line.Visible = false;
        }

        private void _dragGesture_DragCancelled(object sender, EventArgs e)
        {
            _line.Visible = false;
        }
    }
}
