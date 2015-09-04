using JJ.Framework.Presentation.Svg.Gestures;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Svg.Gestures;
using JJ.Presentation.Synthesizer.Svg.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Svg.Converters
{
    internal class OutletRectangleConverter
    {
        private DragGesture _dragGesture;
        private LineGesture _lineGesture;
        private ToolTipGesture _outletToolTipGesture;

        /// <param name="outletToolTipGesture">nullable</param>
        public OutletRectangleConverter(DragGesture dragGesture, LineGesture lineGesture, ToolTipGesture outletToolTipGesture)
        {
            if (dragGesture == null) throw new NullException(() => dragGesture);
            if (lineGesture == null) throw new NullException(() => lineGesture);

            _dragGesture = dragGesture;
            _lineGesture = lineGesture;
            _outletToolTipGesture = outletToolTipGesture;
        }

        public IList<Rectangle> ConvertToOutletRectangles(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            if (sourceOperatorViewModel.Outlets.Count == 0)
            {
                return new Rectangle[0];
            }

            IList<Rectangle> destOutletRectangles = new List<Rectangle>(sourceOperatorViewModel.Inlets.Count);

            float outletWidth = destOperatorRectangle.Width / sourceOperatorViewModel.Outlets.Count;
            float rowHeight = destOperatorRectangle.Height / 4;
            float heightOverflow = StyleHelper.PointStyle.Width / 2;
            float x = 0;
            float y = rowHeight * 3;

            foreach (OutletViewModel sourceOutletViewModel in sourceOperatorViewModel.Outlets)
            {
                Rectangle destOutletRectangle = ConvertToOutletRectangle(sourceOutletViewModel, destOperatorRectangle);

                destOutletRectangle.X = x;
                destOutletRectangle.Y = y;
                destOutletRectangle.Width = outletWidth;
                destOutletRectangle.Height = rowHeight + heightOverflow;

                destOutletRectangles.Add(destOutletRectangle);

                x += outletWidth;
            }

            return destOutletRectangles;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Rectangle ConvertToOutletRectangle(OutletViewModel sourceOutletViewModel, Rectangle destOperatorRectangle)
        {
            int id = sourceOutletViewModel.ID;

            Rectangle destOutletRectangle = TryGetOutletRectangle(destOperatorRectangle, id);
            if (destOutletRectangle == null)
            {
                destOutletRectangle = new Rectangle();
                destOutletRectangle.Diagram = destOperatorRectangle.Diagram;
                destOutletRectangle.Parent = destOperatorRectangle;
                destOutletRectangle.Tag = SvgTagHelper.GetOutletTag(id);

                _destOutletRectangleDictionary.Add(id, destOutletRectangle);
            }

            destOutletRectangle.BackStyle = StyleHelper.BackStyleInvisible;
            destOutletRectangle.LineStyle = StyleHelper.BorderStyleInvisible;

            destOutletRectangle.Gestures.Clear();
            destOutletRectangle.Gestures.Add(_dragGesture);
            destOutletRectangle.Gestures.Add(_lineGesture);
            destOutletRectangle.MustBubble = false; // So drag does not result in a move.

            if (_outletToolTipGesture != null)
            {
                destOutletRectangle.Gestures.Add(_outletToolTipGesture);
            }

            return destOutletRectangle;
        }

        private Dictionary<int, Rectangle> _destOutletRectangleDictionary = new Dictionary<int, Rectangle>();

        private Rectangle TryGetOutletRectangle(Element destParent, int id)
        {
            Rectangle destRectangle;
            if (!_destOutletRectangleDictionary.TryGetValue(id, out destRectangle))
            {
                destRectangle = destParent.Children
                                          .OfType<Rectangle>()
                                          .Where(x => SvgTagHelper.TryGetOutletID(x.Tag) == id)
                                          .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destRectangle != null)
                {
                    _destOutletRectangleDictionary.Add(id, destRectangle);
                }
            }

            return destRectangle;
        }
    }
}
