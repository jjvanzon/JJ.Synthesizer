using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class OutletPointConverter
    {
        private Dictionary<int, Point> _destOutletPointDictionary = new Dictionary<int, Point>();

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
            int id = sourceOutletViewModel.ID;
            Point destOutletPoint = TryGetOutletPoint(destOperatorRectangle, id);
            if (destOutletPoint == null)
            {
                destOutletPoint = new Point();
                destOutletPoint.Diagram = destOperatorRectangle.Diagram;
                destOutletPoint.Parent = destOperatorRectangle;
                destOutletPoint.Tag = VectorGraphicsTagHelper.GetOutletTag(id);

                _destOutletPointDictionary.Add(id, destOutletPoint);
            }

            destOutletPoint.PointStyle = StyleHelper.PointStyle;

            return destOutletPoint;
        }

        private Point TryGetOutletPoint(Element destParent, int id)
        {
            Point destPoint;
            if (!_destOutletPointDictionary.TryGetValue(id, out destPoint))
            {
                destPoint = destParent.Children
                                      .OfType<Point>()
                                      .Where(x => VectorGraphicsTagHelper.IsOutletTag(x.Tag) &&
                                                  VectorGraphicsTagHelper.GetOutletID(x.Tag) == id)
                                      .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destPoint != null)
                {
                    _destOutletPointDictionary.Add(id, destPoint);
                }
            }

            return destPoint;
        }
    }
}
