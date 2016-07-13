using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using System.Linq;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class InletPointConverter
    {
        private readonly Dictionary<int, Point> _destInletPointDictionary = new Dictionary<int, Point>();

        public IList<Point> ConvertToInletPoints(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            IList<InletViewModel> sourceInletViewModelsToConvert = sourceOperatorViewModel.Inlets
                                                                                          .Where(inlet => inlet.Visible)
                                                                                          .ToArray();
            if (sourceInletViewModelsToConvert.Count == 0)
            {
                return new Point[0];
            }

            IList<Point> destInletPoints = new List<Point>(sourceInletViewModelsToConvert.Count);

            float inletWidth = destOperatorRectangle.Position.Width / sourceInletViewModelsToConvert.Count;
            float x = 0;
            foreach (InletViewModel sourceInletViewModel in sourceInletViewModelsToConvert)
            {
                if (!sourceInletViewModel.Visible)
                {
                    continue;
                }

                Point destInletPoint = ConvertToInletPoint(sourceInletViewModel, destOperatorRectangle);

                destInletPoint.Position.X = x + inletWidth / 2f;
                destInletPoint.Position.Y = 0;

                destInletPoints.Add(destInletPoint);

                x += inletWidth;
            }

            return destInletPoints;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Point ConvertToInletPoint(InletViewModel sourceInletViewModel, Rectangle destOperatorRectangle)
        {
            int inletID = sourceInletViewModel.ID;

            Point destInletPoint = TryGetInletPoint(destOperatorRectangle, inletID);

            if (destInletPoint == null)
            {
                destInletPoint = new Point();
                destInletPoint.Diagram = destOperatorRectangle.Diagram;
                destInletPoint.Parent = destOperatorRectangle;
                destInletPoint.Tag = VectorGraphicsTagHelper.GetInletTag(inletID);

                _destInletPointDictionary.Add(inletID, destInletPoint);
            }

            destInletPoint.PointStyle = StyleHelper.PointStyle;

            return destInletPoint;
        }

        private Point TryGetInletPoint(Element destParent, int id)
        {
            Point destPoint;
            if (!_destInletPointDictionary.TryGetValue(id, out destPoint))
            {
                destPoint = destParent.Children
                                      .OfType<Point>()
                                      .Where(x => VectorGraphicsTagHelper.TryGetInletID(x.Tag) == id)
                                      .FirstOrDefault(); // First instead of Single will make sure that excessive ones are cleaned up.

                if (destPoint != null)
                {
                    _destInletPointDictionary.Add(id, destPoint);
                }
            }

            return destPoint;
        }

        public void TryRemove(int inletID)
        {
            Point destElement;
            if (_destInletPointDictionary.TryGetValue(inletID, out destElement))
            {
                _destInletPointDictionary.Remove(inletID);

                destElement.Children.Clear();
                destElement.Parent = null;
                destElement.Diagram = null;
            }
        }
    }
}