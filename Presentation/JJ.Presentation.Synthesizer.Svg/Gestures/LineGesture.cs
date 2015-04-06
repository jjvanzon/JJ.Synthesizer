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
    public class LineGesture : IGesture, IDisposable
    {
        private Diagram _diagram;
        private Line _line;

        private float _mouseDownX;
        private float _mouseDownY;

        // TODO: If the DragGesture and DropGesture do not really add any functionality
        // that I almost programmed myself here, then remove them?
        private DragGesture _dragGesture;
        private DropGesture _dropGesture;
        private IList<IGesture> _baseGestures;
        private MouseMoveGesture _canvasMouseMoveGesture;
        private MouseUpGesture _canvasMouseUpGesture;

        public LineGesture(Diagram diagram, LineStyle lineStyle = null, int lineZIndex = 0)
        {
            if (diagram == null) throw new NullException(() => diagram);

            _diagram = diagram;

            _line = new Line
            { 
                Diagram = _diagram,
                ZIndex = lineZIndex,
                Visible = false,
                Tag = "THIS IS THE LINE GESTURE LINE!"
            };

            if (lineStyle != null)
            {
                _line.LineStyle = lineStyle;
            };

            _dragGesture = new DragGesture();
            _dropGesture = new DropGesture();

            _baseGestures = new IGesture[] { _dragGesture, _dropGesture };

            _dragGesture.OnDragging += _dragGesture_OnDragging;

            _canvasMouseMoveGesture = new MouseMoveGesture();
            _canvasMouseMoveGesture.MouseMove += _canvasMouseMoveGesture_MouseMove;
            _diagram.RootRectangle.Gestures.Add(_canvasMouseMoveGesture);

            _canvasMouseUpGesture = new MouseUpGesture();
            _canvasMouseUpGesture.OnMouseUp += _canvasMouseUpGesture_OnMouseUp;
            _diagram.RootRectangle.Gestures.Add(_canvasMouseUpGesture);
        }

        ~LineGesture()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_dragGesture != null)
            {
                _dragGesture.OnDragging -= _dragGesture_OnDragging;
            }

            if (_canvasMouseMoveGesture != null)
            {
                if (_diagram != null)
                {
                    _diagram.RootRectangle.Gestures.Remove(_canvasMouseMoveGesture);
                }
                _canvasMouseMoveGesture.MouseMove -= _canvasMouseMoveGesture_MouseMove;
            }

            GC.SuppressFinalize(this);
        }

        // IGesture

        bool IGesture.MouseCaptureRequired
        {
            get { return false; }
        }

        void IGesture.MouseDown(object sender, MouseEventArgs e)
        {
            foreach (IGesture baseGesture in _baseGestures)
            {
                baseGesture.MouseDown(sender, e);
            }

            if (e.Element != null)
            {
                _mouseDownX = e.X;
                _mouseDownY = e.Y;
            }
        }

        void IGesture.MouseMove(object sender, MouseEventArgs e)
        {
            foreach (IGesture baseGesture in _baseGestures)
            {
                baseGesture.MouseMove(sender, e);
            }
        }

        void IGesture.MouseUp(object sender, MouseEventArgs e)
        {
            foreach (IGesture baseGesture in _baseGestures)
            {
                baseGesture.MouseUp(sender, e);
            }

            _line.Visible = false;
        }

        // Events

        private void _canvasMouseMoveGesture_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragGesture.DraggedElement != null)
            {
                _line.Visible = false;

                _line.PointA.X = _mouseDownX;
                _line.PointA.Y = _mouseDownY;

                _line.PointB.X = e.X;
                _line.PointB.Y = e.Y;

                _line.Visible = true;
            }
        }

        private void _dragGesture_OnDragging(object sender, DraggingEventArgs e)
        {
            _line.PointA.X = _mouseDownX;
            _line.PointA.Y = _mouseDownY;

            _line.PointB.X = e.X;
            _line.PointB.Y = e.Y;

            _line.Visible = true;
        }

        void _canvasMouseUpGesture_OnMouseUp(object sender, MouseEventArgs e)
        {
            _line.Visible = false;
        }
    }
}
