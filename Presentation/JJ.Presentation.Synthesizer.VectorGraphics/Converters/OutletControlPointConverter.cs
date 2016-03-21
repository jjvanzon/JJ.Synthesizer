using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class OutletControlPointConverter
    {
        private const float CONTROL_POINT_DISTANCE = 50;

        public IList<Point> ConvertToOutletControlPoints(IList<Point> sourceOutletPoints)
        {
            if (sourceOutletPoints == null) throw new NullException(() => sourceOutletPoints);

            IList<Point> destOutletControlPoints = sourceOutletPoints.Select(x => ConvertPoint(x)).ToList();

            return destOutletControlPoints;
        }

        private Point ConvertPoint(Point sourceOutletPoint)
        {
            var destOutletControlPoint = new Point
            {
                Diagram = sourceOutletPoint.Diagram,
                Parent = sourceOutletPoint.Parent,
                PointStyle = StyleHelper.PointStyleInvisible
            };

            destOutletControlPoint.Position.X = sourceOutletPoint.Position.X;
            destOutletControlPoint.Position.Y = sourceOutletPoint.Position.Y + CONTROL_POINT_DISTANCE;

            return destOutletControlPoint;
        }
    }
}