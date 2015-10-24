using System;
using System.Collections.Generic;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.VectorGraphics.EventArg;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Presentation.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Framework.Mathematics;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
    public class DragLineGesture : DragGesture, IDisposable
    {
        private Diagram _diagram;
        private Line _line;
        private float _sourceX;
        private float _sourceY;

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

        protected override void HandleMouseDown(object sender, MouseEventArgs e)
        {
            GetSourceCoordinates(e.Element, out _sourceX, out _sourceY);

            base.HandleMouseDown(sender, e);
        }

        private void this_Dragging(object sender, DraggingEventArgs e)
        {
            _line.PointA.X = _sourceX;
            _line.PointA.Y = _sourceY;

            _line.PointB.X = e.X;
            _line.PointB.Y = e.Y;

            _line.Visible = true;
        }

        private void this_DragCancelled(object sender, EventArgs e)
        {
            _line.Visible = false;
        }

        private void GetSourceCoordinates(Element element, out float x, out float y)
        {
            // Right now this only works if the OutletRectangle's center is the same as the OutletPoint,
            // but it would perform worse to query for the OutletPoint.
            var rectangle = element as Rectangle;
            if (rectangle == null)
            {
                throw new IsNotTypeException<Rectangle>(() => element);
            }

            ICalculatedValues calculatedValues = rectangle;

            Geometry.GetCenter_ByWidthAndHeight(
                calculatedValues.CalculatedXInPixels, calculatedValues.CalculatedYInPixels,
                // TODO: Should these not be the calculated Width and Height?
                rectangle.Width, rectangle.Height,
                out x, out y);
        }
    }
}
