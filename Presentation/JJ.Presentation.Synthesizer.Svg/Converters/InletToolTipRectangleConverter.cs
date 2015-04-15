using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Svg.Gestures;
using JJ.Presentation.Synthesizer.Svg.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg.Converters
{
    [Obsolete("", true)]
    internal class InletToolTipRectangleConverter
    {
        private ToolTipGesture _inletToolTipGesture;

        public InletToolTipRectangleConverter(ToolTipGesture inletToolTipGesture)
        {
            if (inletToolTipGesture == null) throw new NullException(() => inletToolTipGesture);

            _inletToolTipGesture = inletToolTipGesture;
        }

        public IList<Rectangle> ConvertToInletToolTipRectangles(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            if (sourceOperatorViewModel.Inlets.Count == 0)
            {
                return new Rectangle[0];
            }

            IList<Rectangle> destRectangles = new List<Rectangle>(sourceOperatorViewModel.Inlets.Count);

            float rowHeight = destOperatorRectangle.Height / 4;
            float heightOverflow = StyleHelper.PointStyle.Width / 2;
            float inletWidth = destOperatorRectangle.Width / sourceOperatorViewModel.Inlets.Count;
            float x = 0;

            foreach (InletViewModel sourceInletViewModel in sourceOperatorViewModel.Inlets)
            {
                Rectangle destRectangle = ConvertToInletToolTipRectangle(sourceInletViewModel, destOperatorRectangle);

                destRectangle.X = x;
                destRectangle.Y = -heightOverflow;
                destRectangle.Width = inletWidth;
                destRectangle.Height = rowHeight + heightOverflow;

                destRectangles.Add(destRectangle);

                x += inletWidth;
            }

            return destRectangles;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Rectangle ConvertToInletToolTipRectangle(InletViewModel sourceInletViewModel, Rectangle destOperatorRectangle)
        {
            // Convert to Inlet Rectangle
            Rectangle destRectangle = TryGetInletRectangle(destOperatorRectangle, sourceInletViewModel.ID);
            if (destRectangle == null)
            {
                destRectangle = new Rectangle();
                destRectangle.Diagram = destOperatorRectangle.Diagram;
                destRectangle.Parent = destOperatorRectangle;
                destRectangle.Tag = TagHelper.GetInletTag(sourceInletViewModel.ID);

                _destRectangleDictionary.Add(sourceInletViewModel.ID, destRectangle);
            }

            destRectangle.BackStyle = StyleHelper.BackStyleInvisible;
            destRectangle.LineStyle = StyleHelper.LineStyleInvisible;

            destRectangle.Gestures.Clear();
            destRectangle.Gestures.Add(_inletToolTipGesture);
            destRectangle.MustBubble = false; // The is only done to make the tooltip work, so if the tooltip uses another region, it is not necessary anymore.

            return destRectangle;
        }

        private Dictionary<int, Rectangle> _destRectangleDictionary = new Dictionary<int, Rectangle>();

        private Rectangle TryGetInletRectangle(Element destParent, int inletID)
        {
            // TODO: Remove this code line if the elements can be uniquely identified.
            return null;

            Rectangle destRectangle;
            if (!_destRectangleDictionary.TryGetValue(inletID, out destRectangle))
            {
                // TODO: Come up with a unique tag name
                destRectangle = destParent.Children
                                          .OfType<Rectangle>()
                                          .Where(x => TagHelper.TryGetInletID(x.Tag) == inletID)
                                          .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destRectangle != null)
                {
                    _destRectangleDictionary.Add(inletID, destRectangle);
                }
            }

            return destRectangle;
        }
    }
}
