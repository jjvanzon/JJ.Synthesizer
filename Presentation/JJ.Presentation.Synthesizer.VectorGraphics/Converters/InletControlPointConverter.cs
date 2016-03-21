using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class InletControlPointConverter
    {
        private const float CONTROL_POINT_DISTANCE = 50;

        public IList<Point> ConvertToInletControlPoints(IList<Point> sourceInletPoints)
        {
            if (sourceInletPoints == null) throw new NullException(() => sourceInletPoints);

            IList<Point> destOutletControlPoints = sourceInletPoints.Select(x => ConvertPoint(x)).ToList();

            return destOutletControlPoints;
        }

        private Point ConvertPoint(Point sourceInletPoint)
        {
            var destInletControlPoint = new Point
            {
                Diagram = sourceInletPoint.Diagram,
                Parent = sourceInletPoint.Parent,
                PointStyle = StyleHelper.PointStyleInvisible
            };

            destInletControlPoint.Position.X = sourceInletPoint.Position.X;
            destInletControlPoint.Position.Y = sourceInletPoint.Position.Y - CONTROL_POINT_DISTANCE;

            return destInletControlPoint;
        }
    }
}