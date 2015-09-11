using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class InletRectangleConverter
    {
        private IGesture _dropLineGesture;
        private IGesture _inletToolTipGesture;

        /// <param name="inletToolTipGesture">nullable</param>
        public InletRectangleConverter(DropLineGesture dropLineGesture, ToolTipGesture inletToolTipGesture)
        {
            if (dropLineGesture == null) throw new NullException(() => dropLineGesture);

            _dropLineGesture = dropLineGesture;
            _inletToolTipGesture = inletToolTipGesture;
        }

        public IList<Rectangle> ConvertToInletRectangles(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            if (sourceOperatorViewModel.Inlets.Count == 0)
            {
                return new Rectangle[0];
            }

            IList<Rectangle> destInletRectangles = new List<Rectangle>(sourceOperatorViewModel.Inlets.Count);

            float rowHeight = destOperatorRectangle.Height / 4;
            float heightOverflow = StyleHelper.PointStyle.Width / 2;
            float inletWidth = destOperatorRectangle.Width / sourceOperatorViewModel.Inlets.Count;
            float x = 0;

            foreach (InletViewModel sourceInletViewModel in sourceOperatorViewModel.Inlets)
            {
                Rectangle destInletRectangle = ConvertToInletRectangle(sourceInletViewModel, destOperatorRectangle);

                destInletRectangle.X = x;
                destInletRectangle.Y = -heightOverflow;
                destInletRectangle.Width = inletWidth;
                destInletRectangle.Height = rowHeight + heightOverflow;

                destInletRectangles.Add(destInletRectangle);

                x += inletWidth;
            }

            return destInletRectangles;
        }

        /// <summary> Converts everything but its coordinates. </summary>
        private Rectangle ConvertToInletRectangle(InletViewModel sourceInletViewModel, Rectangle destOperatorRectangle)
        {
            int id = sourceInletViewModel.ID;

            Rectangle destInletRectangle = TryGetInletRectangle(destOperatorRectangle, id);
            if (destInletRectangle == null)
            {
                destInletRectangle = new Rectangle();
                destInletRectangle.Diagram = destOperatorRectangle.Diagram;
                destInletRectangle.Parent = destOperatorRectangle;
                destInletRectangle.Tag = VectorGraphicsTagHelper.GetInletTag(id);

                _destInletRectangleDictionary.Add(id, destInletRectangle);
            }

            destInletRectangle.BackStyle = StyleHelper.BackStyleInvisible;
            destInletRectangle.LineStyle = StyleHelper.BorderStyleInvisible;

            destInletRectangle.Gestures.Clear();
            destInletRectangle.Gestures.Add(_dropLineGesture);

            if (_inletToolTipGesture != null)
            {
                destInletRectangle.Gestures.Add(_inletToolTipGesture);
                //destInletRectangle.MustBubble = false; // The is only done to make the tooltip work, so if the tooltip uses another region, it is not necessary anymore.
            }

            return destInletRectangle;
        }

        private Dictionary<int, Rectangle> _destInletRectangleDictionary = new Dictionary<int, Rectangle>();

        private Rectangle TryGetInletRectangle(Element destParent, int inletID)
        {
            Rectangle destRectangle;
            if (!_destInletRectangleDictionary.TryGetValue(inletID, out destRectangle))
            {
                destRectangle = destParent.Children
                                          .OfType<Rectangle>()
                                          .Where(x => VectorGraphicsTagHelper.TryGetInletID(x.Tag) == inletID)
                                          .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destRectangle != null)
                {
                    _destInletRectangleDictionary.Add(inletID, destRectangle);
                }
            }

            return destRectangle;
        }
    }
}
