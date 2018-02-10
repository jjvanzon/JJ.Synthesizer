using System;
using System.Collections.Generic;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
	internal class CurrentPatchRectangleConverter
	{
		private const float ROW_HEIGHT = 24f;

		private readonly Diagram _destDiagram;
		private readonly Dictionary<int, Rectangle> _dictionary = new Dictionary<int, Rectangle>();

		public CurrentPatchRectangleConverter(Diagram destDiagram)
		{
			_destDiagram = destDiagram ?? throw new ArgumentNullException(nameof(destDiagram));
		}

		public Rectangle Convert(CurrentInstrumentItemViewModel viewModel)
		{
			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

			if (!_dictionary.TryGetValue(viewModel.PatchID, out Rectangle rectangle))
			{
				rectangle = new Rectangle
				{
					Diagram = _destDiagram,
					Parent = _destDiagram.Background,
					Style = StyleHelper.RectangleStyleInvisible,
					Tag = viewModel.PatchID
				};
				rectangle.Position.Height = ROW_HEIGHT;

				_dictionary[viewModel.PatchID] = rectangle;
			}

			return rectangle;
		}

		public void TryRemove(int patchID)
		{
			if (_dictionary.TryGetValue(patchID, out Rectangle destRectangle))
			{
				destRectangle.Children.Clear();
				destRectangle.Parent = null;
				destRectangle.Diagram = null;

				_dictionary.Remove(patchID);
			}
		}
	}
}
