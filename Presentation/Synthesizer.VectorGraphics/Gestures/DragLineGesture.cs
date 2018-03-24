using System;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.TypeChecking;
using JJ.Framework.Mathematics;
using JJ.Framework.VectorGraphics.EventArg;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Framework.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Gestures
{
	public class DragLineGesture : DragGesture, IDisposable
	{
		private readonly Line _line;
		private float _sourceX;
		private float _sourceY;

		public DragLineGesture(Diagram diagram, LineStyle lineStyle = null, int lineZIndex = 0)
		{
			if (diagram == null) throw new NullException(() => diagram);

			_line = LineGestureHelper.CreateLine(diagram, lineStyle, lineZIndex);

			Dragging += this_Dragging;
			DragCanceled += this_DragCancelled;
		}

		~DragLineGesture()
		{
			Dispose();
		}

		public void Dispose()
		{
			Dragging -= this_Dragging;
			DragCanceled -= this_DragCancelled;

			GC.SuppressFinalize(this);
		}

		protected override void HandleMouseDown(object sender, MouseEventArgs e)
		{
			GetSourceCoordinates(e.Element, out _sourceX, out _sourceY);

			base.HandleMouseDown(sender, e);
		}

		private void this_Dragging(object sender, DraggingEventArgs e)
		{
			_line.PointA.Position.X = _sourceX;
			_line.PointA.Position.Y = _sourceY;

			_line.PointB.Position.X = e.X;
			_line.PointB.Position.Y = e.Y;

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
			if (!(element is Rectangle rectangle))
			{
				throw new IsNotTypeException<Rectangle>(() => element);
			}

			Geometry.GetCenter_ByWidthAndHeight(
				rectangle.CalculatedValues.XInPixels, rectangle.CalculatedValues.YInPixels,
				// TODO: Should these not be the calculated Width and Height?
				rectangle.Position.Width, rectangle.Position.Height,
				out x, out y);
		}
	}
}
