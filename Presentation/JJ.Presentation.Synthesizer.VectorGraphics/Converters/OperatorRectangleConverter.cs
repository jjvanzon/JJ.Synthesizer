using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Helpers;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;
using System.Linq;
using System;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class OperatorRectangleConverter
    {
        private Dictionary<int, Rectangle> _destOperatorRectangleDictionary = new Dictionary<int, Rectangle>();

        private Diagram _diagram;
        private MoveGesture _moveGesture;
        private SelectOperatorGesture _selectOperatorGesture;
        private DoubleClickGesture _doubleClickOperatorGesture;

        public OperatorRectangleConverter(
            Diagram diagram,
            MoveGesture moveGesture,
            SelectOperatorGesture selectOperatorGesture,
            DoubleClickGesture doubleClickOperatorGesture)
        {
            if (diagram == null) throw new NullException(() => diagram);
            if (moveGesture == null) throw new NullException(() => moveGesture);
            if (selectOperatorGesture == null) throw new NullException(() => selectOperatorGesture);
            if (doubleClickOperatorGesture == null) throw new NullException(() => doubleClickOperatorGesture);

            _diagram = diagram;
            _moveGesture = moveGesture;
            _selectOperatorGesture = selectOperatorGesture;
            _doubleClickOperatorGesture = doubleClickOperatorGesture;
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
                    Parent = destDiagram.Background,
                    Tag = VectorGraphicsTagHelper.GetOperatorTag(sourceOperatorViewModel.ID)
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
            destOperatorRectangle.Gestures.Add(_doubleClickOperatorGesture);

            return destOperatorRectangle;
        }

        private static float GetOperatorWidth(OperatorViewModel sourceOperatorViewModel)
        {
            float width = TextHelper.ApproximateTextWidth(sourceOperatorViewModel.Caption, StyleHelper.DefaultFont) + StyleHelper.SpacingTimes2;

            // Compensate for the fact that numbers are averagely wider than letters.
            bool isValueOperator = sourceOperatorViewModel.OperatorType.ID == (int)OperatorTypeEnum.Number;
            if (isValueOperator)
            {
                width += StyleHelper.SpacingTimes2;
            }
            return width;
        }

        private Rectangle TryGetOperatorRectangle(int id)
        {
            Rectangle destRectangle;
            if (!_destOperatorRectangleDictionary.TryGetValue(id, out destRectangle))
            {
                destRectangle = _diagram.Elements
                                        .OfType<Rectangle>()
                                        .Where(x => VectorGraphicsTagHelper.IsOperatorTag(x.Tag) &&
                                                    VectorGraphicsTagHelper.GetOperatorID(x.Tag) == id)
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