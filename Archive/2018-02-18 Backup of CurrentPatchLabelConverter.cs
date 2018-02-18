//using System;
//using System.Collections.Generic;
//using JJ.Framework.VectorGraphics.Helpers;
//using JJ.Framework.VectorGraphics.Models.Elements;
//using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
//using JJ.Presentation.Synthesizer.ViewModels.Items;

//namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
//{
//	internal class CurrentPatchLabelConverter
//	{
//		private readonly ITextMeasurer _textMeasurer;
//		private readonly Dictionary<int, Label> _dictionary = new Dictionary<int, Label>();

//		public CurrentPatchLabelConverter(ITextMeasurer textMeasurer)
//		{
//			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));
//		}

//		public Label Convert(CurrentInstrumentPatchViewModel viewModel, Element parentElement)
//		{
//			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

//			if (!_dictionary.TryGetValue(viewModel.PatchID, out Label label)) {

//				label = new Label(parentElement.Diagram.Background)
//				{
//					TextStyle = StyleHelper.TitleTextStyle
//				};
//				label.Position.Height = StyleHelper.TITLE_BAR_HEIGHT;

//				_dictionary[viewModel.PatchID] = label;
//			}

//			label.Text = viewModel.Name;
//			(label.Position.Width, _) = _textMeasurer.GetTextSize(label.Text, label.TextStyle.Font);

//			return label;
//		}

//		public void TryRemove(int patchID)
//		{
//			if (_dictionary.TryGetValue(patchID, out Label label))
//			{
//				label.Dispose();

//				_dictionary.Remove(patchID);
//			}
//		}
//	}
//}
