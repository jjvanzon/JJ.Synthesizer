using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
	internal class OperatorLabelConverter
	{
		private readonly Dictionary<int, Label> _destOperatorLabelDictionary = new Dictionary<int, Label>();
		private readonly HashSet<Label> _destOperatorLabelHashSet = new HashSet<Label>();

		public Label ConvertToOperatorLabel(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
		{
			if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
			if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

			int operatorID = sourceOperatorViewModel.ID;

			if (!_destOperatorLabelDictionary.TryGetValue(operatorID, out Label destLabel))
			{
				destLabel = new Label
				{
					Diagram = destOperatorRectangle.Diagram,
					Parent = destOperatorRectangle,
					Tag = operatorID
				};

				_destOperatorLabelDictionary.Add(operatorID, destLabel);
				_destOperatorLabelHashSet.Add(destLabel);
			}

			destLabel.Text = sourceOperatorViewModel.Caption;
			destLabel.Position.Width = destOperatorRectangle.Position.Width;
			destLabel.Position.Height = destOperatorRectangle.Position.Height;

			if (sourceOperatorViewModel.IsSmaller)
			{
				destLabel.TextStyle = StyleHelper.NumberOperatorTextStyle;
			}
			else
			{
				destLabel.TextStyle = StyleHelper.DefaultTextStyle;
			}

			return destLabel;
		}

		public void TryRemove(Label destElement)
		{
			if (_destOperatorLabelHashSet.Contains(destElement))
			{
				int operatorID = (int)destElement.Tag;

				_destOperatorLabelDictionary.Remove(operatorID);
				_destOperatorLabelHashSet.Remove(destElement);

				destElement.Children.Clear();
				destElement.Parent = null;
				destElement.Diagram = null;
			}
		}
	}
}