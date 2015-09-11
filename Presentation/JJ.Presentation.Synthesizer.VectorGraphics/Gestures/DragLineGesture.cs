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
    public class DragLineGesture : DragGesture, IDisposable
    {
        private Diagram _diagram;
        private Line _line;
        private float _mouseDownX;
        private float _mouseDownY;

        public DragLineGesture(Diagram diagram, LineStyle lineStyle = null, int lineZIndex = 0)
        {
            if (diagram == null) throw new NullException(() => diagram);

            _diagram = diagram;

            _line = LineGestureHelper.CreateLine(diagram, lineStyle, lineZIndex);

            this.Dragging += this_Dragging;
            this.DragCanceled += this_DragCancelled;
        }

        ~DragLineGesture()
        {
            Dispose();
        }

        public void Dispose()
        {
            this.Dragging -= this_Dragging;
            this.DragCanceled -= this_DragCancelled;

            GC.SuppressFinalize(this);
        }

        public override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            _mouseDownX = e.X;
            _mouseDownY = e.Y;

            base.HandleMouseDown(sender, e);
        }

        private void this_Dragging(object sender, DraggingEventArgs e)
        {
            _line.PointA.X = _mouseDownX;
            _line.PointA.Y = _mouseDownY;

            _line.PointB.X = e.X;
            _line.PointB.Y = e.Y;

            _line.Visible = true;
        }

        private void this_DragCancelled(object sender, EventArgs e)
        {
            _line.Visible = false;
        }
    }
}
