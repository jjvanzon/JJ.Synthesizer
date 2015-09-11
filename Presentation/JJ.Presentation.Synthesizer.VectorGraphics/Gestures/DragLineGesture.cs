using System;
using System.Collections.Generic;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Presentation.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class DragLineGesture : GestureBase, IDisposable
    {
        private Diagram _diagram;
        private Line _line;
        private float _mouseDownX;
        private float _mouseDownY;

        private DragGesture _dragGesture;
        internal DragGesture DragGesture { get { return _dragGesture; } }

        public event EventHandler<DraggingEventArgs> Dragging
        {
            add { _dragGesture.Dragging += value; }
            remove { _dragGesture.Dragging -= value; }
        }

        public DragLineGesture(Diagram diagram, LineStyle lineStyle = null, int lineZIndex = 0)
        {
            if (diagram == null) throw new NullException(() => diagram);

            _diagram = diagram;

            _line = LineGestureHelper.CreateLine(diagram, lineStyle, lineZIndex);

            _dragGesture = new DragGesture();
            _dragGesture.Dragging += _dragGesture_Dragging;
            _dragGesture.DragCanceled += _dragGesture_DragCancelled;
        }

        ~DragLineGesture()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_dragGesture != null)
            {
                _dragGesture.Dragging -= _dragGesture_Dragging;
                _dragGesture.DragCanceled -= _dragGesture_DragCancelled;
            }

            GC.SuppressFinalize(this);
        }

        // IGesture

        public override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            _dragGesture.HandleMouseDown(sender, e);

            _mouseDownX = e.X;
            _mouseDownY = e.Y;
        }

        public override void HandleMouseMove(object sender, MouseEventArgs e)
        {
            _dragGesture.HandleMouseMove(sender, e);
        }

        public override void HandleMouseUp(object sender, MouseEventArgs e)
        {
            _dragGesture.HandleMouseUp(sender, e);
        }

        // Events

        private void _dragGesture_Dragging(object sender, DraggingEventArgs e)
        {
            _line.PointA.X = _mouseDownX;
            _line.PointA.Y = _mouseDownY;

            _line.PointB.X = e.X;
            _line.PointB.Y = e.Y;

            _line.Visible = true;

            // TODO: _line.Visible does not seem to work.
            _line.Visible = true;
            _line.LineStyle.Visible = true;

        }

        private void _dragGesture_DragCancelled(object sender, EventArgs e)
        {
            // TODO: _line.Visible does not seem to work.
            _line.Visible = false;
            _line.LineStyle.Visible = false;
        }
    }
}
