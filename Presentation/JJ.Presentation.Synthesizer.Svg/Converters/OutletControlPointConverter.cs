using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Svg.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg.Converters
{
    internal class OutletControlPointConverter
    {
        private const float CONTROL_POINT_DISTANCE = 50;

        public IList<Point> ConvertToOutletControlPoints(IList<Point> sourceOutletPoints, Rectangle destOperatorRectangle)
        {
            if (sourceOutletPoints == null) throw new NullException(() => sourceOutletPoints);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            IList<Point> destOutletControlPoints = sourceOutletPoints.Select(x => ConvertPoint(x, destOperatorRectangle)).ToList();

            return destOutletControlPoints;
        }

        private Point ConvertPoint(Point sourceOutletPoint, Rectangle destOperatorRectangle)
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