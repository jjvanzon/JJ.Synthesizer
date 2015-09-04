using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Svg.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Svg.Converters
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
                X = sourceOutletPoint.X,
                Y = sourceOutletPoint.Y + CONTROL_POINT_DISTANCE,
                PointStyle = StyleHelper.PointStyleInvisible
            };

            return destOutletControlPoint;
        }
    }
}