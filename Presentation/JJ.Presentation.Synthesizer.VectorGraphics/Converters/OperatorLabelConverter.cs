using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Linq;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
    internal class OperatorLabelConverter
    {
        public Label ConvertToOperatorLabel(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
        {
            if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
            if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

            Label destLabel = destOperatorRectangle.Children.OfType<Label>().FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.
            if (destLabel == null)
            {
                destLabel = new Label();
                destLabel.Diagram = destOperatorRectangle.Diagram;
                destLabel.Parent = destOperatorRectangle;
            }

            destLabel.Text = sourceOperatorViewModel.Caption;
            destLabel.Width = destOperatorRectangle.Width;
            destLabel.Height = destOperatorRectangle.Height;
            destLabel.TextStyle = StyleHelper.TextStyle;

            return destLabel;
        }
    }
}