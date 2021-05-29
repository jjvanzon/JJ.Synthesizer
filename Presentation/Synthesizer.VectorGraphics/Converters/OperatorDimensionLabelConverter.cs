using System.Collections.Generic;
using JJ.Framework.VectorGraphics.Models.Elements;
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
            if (!sourceOperatorViewModel.Dimension.Visible)
            {
                return null;
            }

            int operatorID = sourceOperatorViewModel.ID;

            if (!_destOperatorDimensionLabelDictionary.TryGetValue(operatorID, out Label destOperatorDimensionLabel))
            {
                destOperatorDimensionLabel = new Label(destOperatorRectangle)
                {
                    Tag = operatorID,
                    TextStyle = StyleHelper.DimensionTextStyle
                };

                _destOperatorDimensionLabelDictionary.Add(operatorID, destOperatorDimensionLabel);
                _destOperatorDimensionLabelHashSet.Add(destOperatorDimensionLabel);
            }

            destOperatorDimensionLabel.Text = "→ " + sourceOperatorViewModel.Dimension.Name;
            destOperatorDimensionLabel.Position.X = destOperatorRectangle.Position.Width / 2f + StyleHelper.SPACING_SMALL;
            destOperatorDimensionLabel.Position.Y = destOperatorRectangle.Position.Height + StyleHelper.SPACING_SMALL;

            return destOperatorDimensionLabel;
        }

        public void TryRemove(Label destElement)
        {
            if (_destOperatorDimensionLabelHashSet.Contains(destElement))
            {
                int operatorID = (int)destElement.Tag;

                _destOperatorDimensionLabelDictionary.Remove(operatorID);
                _destOperatorDimensionLabelHashSet.Remove(destElement);

                destElement.Dispose();
            }
        }
    }
}
