using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using System.Linq;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class OutletPointConverter
    {
        private readonly Dictionary<int, Point> _destOutletPointDictionary = new Dictionary<int, Point>();

        public IList<Point> ConvertToOutletPoints(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            IList<OutletViewModel> sourceOutletViewModelsToConvert =
                sourceOperatorViewModel.Outlets
                                       .Where(outlet => outlet.Visible)
                                       .ToArray();

            if (sourceOutletViewModelsToConvert.Count == 0)
            {
                return new Point[0];
            }

            IList<Point> destOutletPoints = new List<Point>(sourceOutletViewModelsToConvert.Count);

            float outletWidth = destOperatorRectangle.Position.Width / sourceOutletViewModelsToConvert.Count;
            float x = 0;

            foreach (OutletViewModel sourceOutletViewModel in sourceOutletViewModelsToConvert)
            {
                Point destOutletPoint = ConvertToOutletPoint(sourceOutletViewModel, destOperatorRectangle);

                destOutletPoint.Position.X = x + outletWidth / 2f;
                destOutletPoint.Position.Y = destOperatorRectangle.Position.Height;

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

        public void TryRemove(int id)
        {
            Point destElement;
            if (_destOutletPointDictionary.TryGetValue(id, out destElement))
            {
                _destOutletPointDictionary.Remove(id);

                destElement.Children.Clear();
                destElement.Parent = null;
                destElement.Diagram = null;
            }
        }
    }
}
