using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Svg.Helpers;
using JJ.Presentation.Synthesizer.Svg.Structs;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg.Converters
{
    internal class OutletPointConverter
    {
        public IList<Point> ConvertToOutletPoints(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            if (sourceOperatorViewModel.Outlets.Count == 0)
            {
                return new Point[0];
            }

            IList<Point> destOutletPoints = new List<Point>(sourceOperatorViewModel.Outlets.Count);

            float outletWidth = destOperatorRectangle.Width / sourceOperatorViewModel.Outlets.Count;
            float x = 0;

            foreach (OutletViewModel sourceOutletViewModel in sourceOperatorViewModel.Outlets)
            {
                Point destOutletPoint = ConvertToOutletPoint(sourceOutletViewModel, destOperatorRectangle);

                destOutletPoint.X = x + outletWidth / 2f;
                destOutletPoint.Y = destOperatorRectangle.Height;

                destOutletPoints.Add(destOutletPoint);

                x += outletWidth;
            }

            return destOutletPoints;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Point ConvertToOutletPoint(OutletViewModel sourceOutletViewModel, Rectangle destOperatorRectangle)
        {
            var key = new InletOrOutletKey(sourceOutletViewModel.Keys.OperatorIndexNumber, sourceOutletViewModel.Keys.OutletListIndex);

            Point destOutletPoint = TryGetOutletPoint(destOperatorRectangle, key);
            if (destOutletPoint == null)
            {
                destOutletPoint = new Point();
                destOutletPoint.Diagram = destOperatorRectangle.Diagram;
                destOutletPoint.Parent = destOperatorRectangle;
                destOutletPoint.Tag = EntityKeyHelper.GetOutletTag(key);

                _destOutletPointDictionary.Add(key, destOutletPoint);
            }

            destOutletPoint.PointStyle = StyleHelper.PointStyle;

            return destOutletPoint;
        }

        private Dictionary<InletOrOutletKey, Point> _destOutletPointDictionary = new Dictionary<InletOrOutletKey, Point>();

        private Point TryGetOutletPoint(Element destParent, InletOrOutletKey key)
        {
            Point destPoint;
            if (!_destOutletPointDictionary.TryGetValue(key, out destPoint))
            {
                destPoint = destParent.Children
                                      .OfType<Point>()
                                      .Where(x => EntityKeyHelper.IsOutletTag(x.Tag) &&
                                                  EntityKeyHelper.GetOutletKey(x.Tag) == key)
                                      .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destPoint != null)
                {
                    _destOutletPointDictionary.Add(key, destPoint);
                }
            }

            return destPoint;
        }
    }
}
