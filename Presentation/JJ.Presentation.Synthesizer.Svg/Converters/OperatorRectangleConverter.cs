using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Presentation.Svg.Gestures;
using JJ.Framework.Presentation.Svg.Helpers;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Svg.Gestures;
using JJ.Presentation.Synthesizer.Svg.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Svg.Converters
{
    internal class OperatorRectangleConverter
    {
        private Diagram _diagram;
        private MoveGesture _moveGesture;
        private SelectOperatorGesture _selectOperatorGesture;

        public OperatorRectangleConverter(
            Diagram diagram,
            MoveGesture moveGesture,
            SelectOperatorGesture selectOperatorGesture)
        {
            if (diagram == null) throw new NullException(() => diagram);
            if (moveGesture == null) throw new NullException(() => moveGesture);
            if (selectOperatorGesture == null) throw new NullException(() => selectOperatorGesture);

            _diagram = diagram;
            _moveGesture = moveGesture;
            _selectOperatorGesture = selectOperatorGesture;
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
                    Tag = SvgTagHelper.GetOperatorTag(sourceOperatorViewModel.ID)
                };

                _destOperatorRectangleDictionary.Add(sourceOperatorViewModel.ID, destOperatorRectangle);
            }

            float width = GetOperatorWidth(sourceOperatorViewModel);
            destOperatorRectangle.Width = width;
            destOperatorRectangle.Height = StyleHelper.DEFAULT_HEIGHT;
            destOperatorRectangle.X = sourceOperatorViewModel.CenterX - width / 2f;
            destOperatorRectangle.Y = sourceOperatorViewModel.CenterY - StyleHelper.DEFAULT_HEIGHT / 2f;

            if (sourceOperatorViewModel.IsSelected)
            {
                destOperatorRectangle.BackStyle = StyleHelper.BackStyleSelected;
                destOperatorRectangle.LineStyle = StyleHelper.BorderStyleSelected;
            }
            else
            {
                destOperatorRectangle.BackStyle = StyleHelper.BackStyle;
                destOperatorRectangle.LineStyle = StyleHelper.BorderStyle;
            }

            destOperatorRectangle.Gestures.Clear();
            destOperatorRectangle.Gestures.Add(_moveGesture);
            destOperatorRectangle.Gestures.Add(_selectOperatorGesture);

            return destOperatorRectangle;
        }

        private static float GetOperatorWidth(OperatorViewModel sourceOperatorViewModel)
        {
            float width = TextHelper.ApproximateTextWidth(sourceOperatorViewModel.Caption, StyleHelper.DefaultFont) + StyleHelper.SpacingTimes2;

            // Compensate for the fact that numbers are averagely wider than letters.
            bool isValueOperator = sourceOperatorViewModel.OperatorType.ID == (int)OperatorTypeEnum.Value;
            if (isValueOperator)
            {
                width += StyleHelper.SpacingTimes2;
            }
            return width;
        }

        private Dictionary<int, Rectangle> _destOperatorRectangleDictionary = new Dictionary<int, Rectangle>();

        private Rectangle TryGetOperatorRectangle(int id)
        {
            Rectangle destRectangle;
            if (!_destOperatorRectangleDictionary.TryGetValue(id, out destRectangle))
            {
                destRectangle = _diagram.Elements
                                        .OfType<Rectangle>()
                                        .Where(x => SvgTagHelper.IsOperatorTag(x.Tag) &&
                                                    SvgTagHelper.GetOperatorID(x.Tag) == id)
                                        .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (destRectangle != null)
                {
                    _destOperatorRectangleDictionary.Add(id, destRectangle);
                }
            }

            return destRectangle;
        }
    }
}