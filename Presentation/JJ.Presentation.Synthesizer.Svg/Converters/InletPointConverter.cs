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
            var key = new InletOrOutletKey(sourceInletViewModel.Keys.OperatorIndexNumber, sourceInletViewModel.Keys.InletListIndex);

            Point destInletPoint = TryGetInletPoint(destOperatorRectangle, key);

            if (destInletPoint == null)
            {
                destInletPoint = new Point();
                destInletPoint.Diagram = destOperatorRectangle.Diagram;
                destInletPoint.Parent = destOperatorRectangle;
                destInletPoint.Tag = EntityKeyHelper.GetInletTag(key);

                _destInletPointDictionary.Add(key, destInletPoint);
            }

            destInletPoint.PointStyle = StyleHelper.PointStyle;

            return destInletPoint;
        }

        private Dictionary<InletOrOutletKey, Point> _destInletPointDictionary = new Dictionary<InletOrOutletKey, Point>();

        private Point TryGetInletPoint(Element destParent, InletOrOutletKey key)
        {
            Point destPoint;
            if (!_destInletPointDictionary.TryGetValue(key, out destPoint))
            {
                destPoint = destParent.Children
                                      .OfType<Point>()
                                      .Where(x => EntityKeyHelper.IsInletTag(x.Tag) &&
                                                  EntityKeyHelper.GetInletKey(x.Tag) == key)
                                      .FirstOrDefault(); // First instead of Single will make sure that excessive ones are cleaned up.

                if (destPoint != null)
                {
                    _destInletPointDictionary.Add(key, destPoint);
                }
            }

            return destPoint;
        }
    }
}
