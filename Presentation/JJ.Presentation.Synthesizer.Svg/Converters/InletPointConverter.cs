using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Svg.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg.Converters
{
    internal class InletPointConverter
    {
        public IList<Point> ConvertToInletPoints(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            if (sourceOperatorViewModel.Inlets.Count == 0)
            {
                return new Point[0];
            }

            IList<Point> destInletPoints = new List<Point>(sourceOperatorViewModel.Inlets.Count);

            float inletWidth = destOperatorRectangle.Width / sourceOperatorViewModel.Inlets.Count;
            float x = 0;
            foreach (InletViewModel sourceInletViewModel in sourceOperatorViewModel.Inlets)
            {
                Point destInletPoint = ConvertToInletPoint(sourceInletViewModel, destOperatorRectangle);

                destInletPoint.X = x + inletWidth / 2f;
                destInletPoint.Y = 0;

                destInletPoints.Add(destInletPoint);

                x += inletWidth;
            }

            return destInletPoints;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Point ConvertToInletPoint(InletViewModel sourceInletViewModel, Rectangle destOperatorRectangle)
        {
            Point destInletPoint = TryGetInletPoint(destOperatorRectangle, sourceInletViewModel.ID);
            if (destInletPoint == null)
            {
                destInletPoint = new Point();
                destInletPoint.Diagram = destOperatorRectangle.Diagram;
                destInletPoint.Parent = destOperatorRectangle;
                destInletPoint.Tag = TagHelper.GetInletTag(sourceInletViewModel.ID);

                _destInletPointDictionary.Add(sourceInletViewModel.ID, destInletPoint);
            }

            destInletPoint.PointStyle = StyleHelper.PointStyle;

            return destInletPoint;
        }

        private Dictionary<int, Point> _destInletPointDictionary = new Dictionary<int, Point>();

        private Point TryGetInletPoint(Element destParent, int inletID)
        {
            Point destPoint;
            if (!_destInletPointDictionary.TryGetValue(inletID, out destPoint))
            {
                destPoint = destParent.Children
                                      .OfType<Point>()
                                      .Where(x => TagHelper.TryGetInletID(x.Tag) == inletID)
                                      .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destPoint != null)
                {
                    _destInletPointDictionary.Add(inletID, destPoint);
                }
            }

            return destPoint;
        }
    }
}
