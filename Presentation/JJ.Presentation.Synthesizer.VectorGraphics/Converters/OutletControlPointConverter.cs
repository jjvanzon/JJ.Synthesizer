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

        private readonly Dictionary<int, Point> _destOutletControlPointDictionary = new Dictionary<int, Point>();

        public IList<Point> ConvertToOutletControlPoints(IList<Point> sourceOutletPoints)
        {
            if (sourceOutletPoints == null) throw new NullException(() => sourceOutletPoints);

            IList<Point> destOutletControlPoints = sourceOutletPoints.Select(x => ConvertPoint(x)).ToList();

            return destOutletControlPoints;
        }

        private Point ConvertPoint(Point sourceOutletPoint)
        {
            int outletID = VectorGraphicsTagHelper.GetOutletID(sourceOutletPoint.Tag);

            Point destOutletControlPoint;
            if (!_destOutletControlPointDictionary.TryGetValue(outletID, out destOutletControlPoint))
            {
                destOutletControlPoint = new Point
                {
                    Diagram = sourceOutletPoint.Diagram,
                    Parent = sourceOutletPoint.Parent,
                    PointStyle = StyleHelper.PointStyleInvisible,
                    Tag = sourceOutletPoint.Tag
                };

                _destOutletControlPointDictionary.Add(outletID, destOutletControlPoint);
            }

            destOutletControlPoint.Position.X = sourceOutletPoint.Position.X;
            destOutletControlPoint.Position.Y = sourceOutletPoint.Position.Y + CONTROL_POINT_DISTANCE;

            return destOutletControlPoint;
        }

        public void TryRemove(Point destElement)
        {
            if (_destOutletControlPointDictionary.ContainsValue(destElement))
            {
                int outletID = VectorGraphicsTagHelper.GetOutletID(destElement.Tag);

                _destOutletControlPointDictionary.Remove(outletID);

                destElement.Children.Clear();
                destElement.Parent = null;
                destElement.Diagram = null;
            }
        }

        //public void TryRemove(int operatorID)
        //{
        //    Point destElement;
        //    if (_destOutletControlPointDictionary.TryGetValue(operatorID, out destElement))
        //    {
        //        _destOutletControlPointDictionary.Remove(operatorID);

        //        destElement.Children.Clear();
        //        destElement.Parent = null;
        //        destElement.Diagram = null;
        //    }
        //}
    }
}