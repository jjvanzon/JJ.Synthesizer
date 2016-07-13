using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class OperatorDimensionLabelConverter
    {
        private readonly Dictionary<int, Label> _destOperatorDimensionLabelDictionary = new Dictionary<int, Label>();

        public Label TryConvertToDimensionLabel(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (String.IsNullOrEmpty(sourceOperatorViewModel.Dimension.Name))
            {
                return null;
            }

            int operatorID = sourceOperatorViewModel.ID;

            Label operatorDimensionLabel;
            if (!_destOperatorDimensionLabelDictionary.TryGetValue(operatorID, out operatorDimensionLabel))
            {
                operatorDimensionLabel = new Label
                {
                    Diagram = destOperatorRectangle.Diagram,
                    Parent = destOperatorRectangle,
                    Tag = VectorGraphicsTagHelper.GetOperatorTag(operatorID)
                };

                _destOperatorDimensionLabelDictionary.Add(operatorID, operatorDimensionLabel);
            }

            operatorDimensionLabel.Text = "→ " + sourceOperatorViewModel.Dimension.Name;
            operatorDimensionLabel.TextStyle = StyleHelper.DimensionTextStyle;
            operatorDimensionLabel.Position.X = destOperatorRectangle.Position.Width / 2f + StyleHelper.DEFAULT_SPACING;
            operatorDimensionLabel.Position.Y = destOperatorRectangle.Position.Height + StyleHelper.DEFAULT_SPACING;

            return operatorDimensionLabel;
        }

        public void TryRemove(int operatorID)
        {
            Label destElement;
            if (_destOperatorDimensionLabelDictionary.TryGetValue(operatorID, out destElement))
            {
                _destOperatorDimensionLabelDictionary.Remove(operatorID);

                destElement.Children.Clear();
                destElement.Parent = null;
                destElement.Diagram = null;
            }
        }
    }
}
