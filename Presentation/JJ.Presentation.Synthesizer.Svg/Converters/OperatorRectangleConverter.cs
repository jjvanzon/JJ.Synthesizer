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
    internal class OperatorRectangleConverter
    {
        private Diagram _diagram;
        private MoveGesture _moveGesture;
        private SelectOperatorGesture _selectOperatorGesture;
        private ToolTipGesture _operatorToolTipGesture;

        /// <param name="operatorToolTipGesture">nullable</param>
        public OperatorRectangleConverter(
            Diagram diagram,
            MoveGesture moveGesture,
            SelectOperatorGesture selectOperatorGesture,
            ToolTipGesture operatorToolTipGesture)
        {
            if (diagram == null) throw new NullException(() => diagram);
            if (moveGesture == null) throw new NullException(() => moveGesture);
            if (selectOperatorGesture == null) throw new NullException(() => selectOperatorGesture);

            _diagram = diagram;
            _moveGesture = moveGesture;
            _selectOperatorGesture = selectOperatorGesture;
            _operatorToolTipGesture = operatorToolTipGesture;
        }

        public Rectangle ConvertToOperatorRectangle(OperatorViewModel sourceOperatorViewModel, Diagram destDiagram)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destDiagram == null) throw new NullException(() => destDiagram);

            Rectangle destOperatorRectangle = TryGetOperatorRectangle(sourceOperatorViewModel.ID);
            if (destOperatorRectangle == null)
            {
                destOperatorRectangle = new Rectangle
                {
                    Diagram = destDiagram,
                    Parent = destDiagram.Canvas,
                    Tag = TagHelper.GetOperatorTag(sourceOperatorViewModel.ID)
                };

                _destOperatorRectangleDictionary.Add(sourceOperatorViewModel.ID, destOperatorRectangle);
            }

            destOperatorRectangle.Width = StyleHelper.DEFAULT_WIDTH;
            destOperatorRectangle.Height = StyleHelper.DEFAULT_HEIGHT;
            destOperatorRectangle.X = sourceOperatorViewModel.CenterX - StyleHelper.DEFAULT_WIDTH / 2f;
            destOperatorRectangle.Y = sourceOperatorViewModel.CenterY - StyleHelper.DEFAULT_HEIGHT / 2f;

            if (sourceOperatorViewModel.IsSelected)
            {
                destOperatorRectangle.BackStyle = StyleHelper.BackStyleSelected;
                destOperatorRectangle.LineStyle = StyleHelper.LineStyleSelected;
            }
            else
            {
                destOperatorRectangle.BackStyle = StyleHelper.BackStyle;
                destOperatorRectangle.LineStyle = StyleHelper.LineStyle;
            }

            destOperatorRectangle.Gestures.Clear();
            destOperatorRectangle.Gestures.Add(_moveGesture);
            destOperatorRectangle.Gestures.Add(_selectOperatorGesture);

            if (_operatorToolTipGesture != null)
            {
                destOperatorRectangle.Gestures.Add(_operatorToolTipGesture);
            }

            return destOperatorRectangle;
        }

        private Dictionary<int, Rectangle> _destOperatorRectangleDictionary = new Dictionary<int, Rectangle>();

        private Rectangle TryGetOperatorRectangle(int operatorID)
        {
            Rectangle destRectangle;
            if (!_destOperatorRectangleDictionary.TryGetValue(operatorID, out destRectangle))
            {
                destRectangle = _diagram.Elements
                                        .OfType<Rectangle>()
                                        .Where(x => TagHelper.TryGetOperatorID(x.Tag) == operatorID)
                                        .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destRectangle != null)
                {
                    _destOperatorRectangleDictionary.Add(operatorID, destRectangle);
                }
            }

            return destRectangle;
        }
    }
}