//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Framework.VectorGraphics.Gestures;
//using JJ.Framework.VectorGraphics.Helpers;
//using JJ.Framework.VectorGraphics.Models.Elements;
//using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
//using JJ.Presentation.Synthesizer.ViewModels;
//using JJ.Presentation.Synthesizer.ViewModels.Items;

//namespace JJ.Presentation.Synthesizer.VectorGraphics
//{
//	public class CurrentInstrumentViewModelToDiagramConverter
//	{
//		private const float SPACING = 1f;
//		private const float BUTTON_SIZE = 24f;
//		private const float ROW_HEIGHT = 24f;

//		private readonly ITextMeasurer _textMeasurer;
//		private readonly Dictionary<int, CurrentInstrumentViewModelToDiagramConverterPatchResultItem> _patchElementDictionary;
//		//private readonly Dictionary<int, Rectangle> _rectangleDictionary;
//		//private readonly Dictionary<int, Label> _labelDictionary;
//		//private readonly Dictionary<int, Picture> _pictureDictionary;

//		public CurrentInstrumentViewModelToDiagramConverterResult Result { get; private set; }

//		public CurrentInstrumentViewModelToDiagramConverter(ITextMeasurer textMeasurer)
//		{
//			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));

//			//_rectangleDictionary = new Dictionary<int, Rectangle>();
//			//_labelDictionary = new Dictionary<int, Label>();
//			//_pictureDictionary = new Dictionary<int, Picture>();
//			_patchElementDictionary = new Dictionary<int, CurrentInstrumentViewModelToDiagramConverterPatchResultItem>();

//			Result = new CurrentInstrumentViewModelToDiagramConverterResult();
//		}

//		public Diagram Convert(CurrentInstrumentViewModel viewModel)
//		{
//			if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

//			foreach (CurrentInstrumentItemViewModel itemViewModel in viewModel.List)
//			{
//				ConvertPatch(itemViewModel);
//			}

//			IEnumerable<int> patchIDsToKeep = viewModel.List.Select(x => x.PatchID);
//			IEnumerable<int> patchIDsToDelete = _patchElementDictionary.Select(x => x.Key).Except(patchIDsToKeep);
//			foreach (int patchIDToDelete in patchIDsToDelete.ToArray())
//			{
//				var patchResultItemToDelete = _patchElementDictionary[patchIDToDelete];

//				// TODO: Delete all elements in the item result.
//			}

//			throw new NotImplementedException();
//		}

//		//private Retangle ConvertPatchTo

//		private CurrentInstrumentViewModelToDiagramConverterPatchResultItem ConvertPatch(CurrentInstrumentItemViewModel viewModel)
//		{
//			// TODO: Create-Read-Update conversion.

//			if (!_patchElementDictionary.TryGetValue(viewModel.PatchID, out var patchResultItem))
//			{
//				patchResultItem = new CurrentInstrumentViewModelToDiagramConverterPatchResultItem
//				{
//					MouseDownGesture = new MouseDownGesture()
//				};

//				_patchElementDictionary[viewModel.PatchID] = patchResultItem;
//			}


//			var invisibleRectangle = new Rectangle
//			{
//				Diagram = Result.Diagram,
//				Parent = Result.Diagram.Background,
//				Style = StyleHelper.RectangleStyleInvisible
//			};

//			float x = SPACING;
//			float y = SPACING;

//			if (viewModel.CanGoBackward)
//			{
//				// TODO: Make reusable method.
//				var resultItem = new CurrentInstrumentViewModelToDiagramConverterPatchResultItem();

//				var moveBackPicture = new Picture
//				{
//					Diagram = Result.Diagram,
//					Parent = invisibleRectangle
//					// TODO: Reuse Image Resources.
//					//UnderlyingPicture = 
//				};
//				moveBackPicture.Position.X = x;
//				moveBackPicture.Position.Y = y;
//				moveBackPicture.Position.Width = BUTTON_SIZE;
//				moveBackPicture.Position.Height = BUTTON_SIZE;
//				moveBackPicture.Gestures.Add(resultItem.MouseDownGesture);

//				x += BUTTON_SIZE + SPACING;
//			}

//			var label = new Label
//			{
//				Diagram = Result.Diagram,
//				Parent = invisibleRectangle,
//				Text = viewModel.Name,
//				// TODO: Probably needs a different style.
//				TextStyle = StyleHelper.DefaultTextStyle
//			};
//			label.Position.X = x;
//			label.Position.Y = y;
//			label.Position.Width = _textMeasurer.GetTextSize(label.Text, label.TextStyle.Font).width;
//			label.Position.Height = ROW_HEIGHT;

//			// TODO: Other buttons.

//			throw new NotImplementedException();
//		}
//	}
//}