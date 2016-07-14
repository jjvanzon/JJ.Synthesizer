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
        private readonly HashSet<Label> _destOperatorDimensionLabelHashSet = new HashSet<Label>();

        public Label TryConvertToDimensionLabel(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (String.IsNullOrEmpty(sourceOperatorViewModel.Dimension.Name))
            {
                return null;
            }

            int operatorID = sourceOperatorViewModel.ID;

            Label destOperatorDimensionLabel;
            if (!_destOperatorDimensionLabelDictionary.TryGetValue(operatorID, out destOperatorDimensionLabel))
            {
                destOperatorDimensionLabel = new Label
                {
                    Diagram = destOperatorRectangle.Diagram,
                    Parent = destOperatorRectangle,
                    Tag = VectorGraphicsTagHelper.GetOperatorTag(operatorID)
                };

                _destOperatorDimensionLabelDictionary.Add(operatorID, destOperatorDimensionLabel);
                _destOperatorDimensionLabelHashSet.Add(destOperatorDimensionLabel);
            }

            destOperatorDimensionLabel.Text = "→ " + sourceOperatorViewModel.Dimension.Name;
            destOperatorDimensionLabel.TextStyle = StyleHelper.DimensionTextStyle;
            destOperatorDimensionLabel.Position.X = destOperatorRectangle.Position.Width / 2f + StyleHelper.DEFAULT_SPACING;
            destOperatorDimensionLabel.Position.Y = destOperatorRectangle.Position.Height + StyleHelper.DEFAULT_SPACING;

            return destOperatorDimensionLabel;
        }

        public void TryRemove(Label destElement)
        {
            if (_destOperatorDimensionLabelHashSet.Contains(destElement))
            {
                int operatorID = VectorGraphicsTagHelper.GetOperatorID(destElement.Tag);

                _destOperatorDimensionLabelDictionary.Remove(operatorID);
                _destOperatorDimensionLabelHashSet.Remove(destElement);

                destElement.Children.Clear();
                destElement.Parent = null;
                destElement.Diagram = null;
            }
        }
    }
}
