using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class OperatorLabelConverter
    {
        private readonly Dictionary<int, Label> _destOperatorLabelDictionary = new Dictionary<int, Label>();

        public Label ConvertToOperatorLabel(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            int operatorID = sourceOperatorViewModel.ID;

            Label destLabel;
            if (!_destOperatorLabelDictionary.TryGetValue(operatorID, out destLabel))
            {
                destLabel = new Label
                {
                    Diagram = destOperatorRectangle.Diagram,
                    Parent = destOperatorRectangle,
                    Tag = VectorGraphicsTagHelper.GetOperatorTag(operatorID)
                };

                _destOperatorLabelDictionary.Add(operatorID, destLabel);
            }

            destLabel.Text = sourceOperatorViewModel.Caption;
            destLabel.Position.Width = destOperatorRectangle.Position.Width;
            destLabel.Position.Height = destOperatorRectangle.Position.Height;

            if (IsNumberOperator(sourceOperatorViewModel))
            {
                destLabel.TextStyle = StyleHelper.NumberOperatorTextStyle;
            }
            else
            {
                destLabel.TextStyle = StyleHelper.TextStyle;
            }

            return destLabel;
        }

        public void TryRemove(int operatorID)
        {
            Label destElement;
            if (_destOperatorLabelDictionary.TryGetValue(operatorID, out destElement))
            {
                _destOperatorLabelDictionary.Remove(operatorID);

                destElement.Children.Clear();
                destElement.Parent = null;
                destElement.Diagram = null;
            }
        }

        private static bool IsNumberOperator(OperatorViewModel sourceOperatorViewModel)
        {
            return sourceOperatorViewModel.OperatorType.ID == (int)OperatorTypeEnum.Number;
        }
    }
}