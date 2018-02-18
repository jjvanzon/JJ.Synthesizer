//using System;
//using System.Collections.Generic;
//using JJ.Framework.VectorGraphics.Models.Elements;
//using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
//using JJ.Presentation.Synthesizer.ViewModels.Items;

//namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
//{
//	internal class CurrentPatchRectangleConverter
//	{
//		private readonly Diagram _destDiagram;
//		private readonly Dictionary<int, Rectangle> _dictionary = new Dictionary<int, Rectangle>();

//		public CurrentPatchRectangleConverter(Diagram destDiagram)
//		{
//			_destDiagram = destDiagram ?? throw new ArgumentNullException(nameof(destDiagram));
//		}

//		public Rectangle Convert(CurrentInstrumentPatchViewModel viewModel)
//		{
//			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

//			if (!_dictionary.TryGetValue(viewModel.PatchID, out Rectangle rectangle))
//			{
//				rectangle = new Rectangle(_destDiagram.Background)
//				{
//					Style = StyleHelper.RectangleStyleInvisible,
//					Tag = viewModel.PatchID
//				};
//				rectangle.Position.Height = StyleHelper.TITLE_BAR_HEIGHT;

//				_dictionary[viewModel.PatchID] = rectangle;
//			}

//			return rectangle;
//		}

//		public void TryRemove(int patchID)
//		{
//			if (_dictionary.TryGetValue(patchID, out Rectangle destRectangle))
//			{
//				destRectangle.Dispose();

//				_dictionary.Remove(patchID);
//			}
//		}
//	}
//}
