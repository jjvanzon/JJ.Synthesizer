using JJ.Framework.Presentation.Svg.Gestures;
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
    internal class OperatorToolTipRectangleConverter
    {
        private ToolTipGesture _operatorToolTipGesture;

        public OperatorToolTipRectangleConverter(ToolTipGesture operatorToolTipGesture)
        {
            if (operatorToolTipGesture == null) throw new NullException(() => operatorToolTipGesture);

            _operatorToolTipGesture = operatorToolTipGesture;
        }

        public Rectangle ConvertToOperatorRectangle(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            Rectangle destOperatorToolTipRectangle = TryGetRectangle(destOperatorRectangle, sourceOperatorViewModel.ID);
            if (destOperatorToolTipRectangle == null)
            {
                destOperatorToolTipRectangle = new Rectangle
                {
                    Diagram = destOperatorRectangle.Diagram,
                    Parent = destOperatorRectangle,
                    Tag = TagHelper.GetOperatorTag(sourceOperatorViewModel.ID)
                };

                _destOperatorToolTipRectangleDictionary.Add(sourceOperatorViewModel.ID, destOperatorToolTipRectangle);
            }

            destOperatorToolTipRectangle.Width = StyleHelper.DEFAULT_WIDTH;
            destOperatorToolTipRectangle.Height = StyleHelper.DEFAULT_HEIGHT;
            destOperatorToolTipRectangle.X = sourceOperatorViewModel.CenterX - StyleHelper.DEFAULT_WIDTH / 2f;
            destOperatorToolTipRectangle.Y = sourceOperatorViewModel.CenterY - StyleHelper.DEFAULT_HEIGHT / 2f;

            destOperatorToolTipRectangle.BackStyle = StyleHelper.BackStyleInvisible;
            destOperatorToolTipRectangle.LineStyle = StyleHelper.LineStyleInvisible;

            if (_operatorToolTipGesture != null)
            {
                destOperatorToolTipRectangle.Gestures.Add(_operatorToolTipGesture);
            }

            return destOperatorToolTipRectangle;
        }

        private Dictionary<int, Rectangle> _destOperatorToolTipRectangleDictionary = new Dictionary<int, Rectangle>();

        private Rectangle TryGetRectangle(Element destParent, int operatorID)
        {
            Rectangle destOperatorToolTipRectangle;
            if (!_destOperatorToolTipRectangleDictionary.TryGetValue(operatorID, out destOperatorToolTipRectangle))
            {
                destOperatorToolTipRectangle = destParent.Children
                                                         .OfType<Rectangle>()
                                                         .Where(x => TagHelper.TryGetOperatorID(x.Tag) == operatorID)
                                                         .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destOperatorToolTipRectangle != null)
                {
                    _destOperatorToolTipRectangleDictionary.Add(operatorID, destOperatorToolTipRectangle);
                }
            }

            return destOperatorToolTipRectangle;
        }

    }
}
