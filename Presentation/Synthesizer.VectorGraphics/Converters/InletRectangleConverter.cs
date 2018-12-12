using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
	internal class InletRectangleConverter
	{
		private readonly Dictionary<int, Rectangle> _destInletRectangleDictionary = new Dictionary<int, Rectangle>();
		private readonly HashSet<Rectangle> _destInletRectangleHashSet = new HashSet<Rectangle>();

		private readonly GestureBase _dropLineGesture;
		private readonly GestureBase _inletToolTipGesture;

		public InletRectangleConverter(DropLineGesture dropLineGesture, ToolTipGesture inletToolTipGesture)
		{
			_dropLineGesture = dropLineGesture ?? throw new NullException(() => dropLineGesture);
			_inletToolTipGesture = inletToolTipGesture ?? throw new NullException(() => inletToolTipGesture);
		}

		public IList<Rectangle> ConvertToInletRectangles(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
		{
			if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
			if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

			IList<InletViewModel> sourceInletViewModelsToConvert = sourceOperatorViewModel.Inlets
			                                                                              .Where(inlet => inlet.Visible)
			                                                                              .ToArray();
			if (sourceInletViewModelsToConvert.Count == 0)
			{
				return new Rectangle[0];
			}

			IList<Rectangle> destInletRectangles = new List<Rectangle>(sourceInletViewModelsToConvert.Count);

			float rowHeight = destOperatorRectangle.Position.Height / 4;
			const float heightOverflow = StyleHelper.INLET_OUTLET_RECTANGLE_HEIGHT_OVERFLOW_IN_PIXELS;
			float inletWidth = destOperatorRectangle.Position.Width / sourceInletViewModelsToConvert.Count;
			float x = 0;

			foreach (InletViewModel sourceInletViewModel in sourceInletViewModelsToConvert)
			{
				if (!sourceInletViewModel.Visible)
				{
					continue;
				}

				Rectangle destInletRectangle = ConvertToInletRectangle(sourceInletViewModel, destOperatorRectangle);

				destInletRectangle.Position.X = x;
				destInletRectangle.Position.Y = -heightOverflow;
				destInletRectangle.Position.Width = inletWidth;
				destInletRectangle.Position.Height = rowHeight + heightOverflow;

				destInletRectangles.Add(destInletRectangle);

				x += inletWidth;
			}

			return destInletRectangles;
		}

		/// <summary> Converts everything but its coordinates. </summary>
		private Rectangle ConvertToInletRectangle(InletViewModel sourceInletViewModel, Rectangle destOperatorRectangle)
		{
			int inletID = sourceInletViewModel.ID;

			if (!_destInletRectangleDictionary.TryGetValue(inletID, out Rectangle destInletRectangle))
			{
				destInletRectangle = new Rectangle(destOperatorRectangle)
				{
					Tag = inletID,
					Style =
					{
						BackStyle = StyleHelper.BackStyleInvisible,
						LineStyle = StyleHelper.BorderStyleInvisible
					}
				};

				_destInletRectangleDictionary.Add(inletID, destInletRectangle);
				_destInletRectangleHashSet.Add(destInletRectangle);
			}

			destInletRectangle.Gestures.Clear();
			destInletRectangle.Gestures.Add(_dropLineGesture);
			destInletRectangle.Gestures.Add(_inletToolTipGesture);

			return destInletRectangle;
		}

		public void TryRemove(Rectangle destElement)
		{
			if (_destInletRectangleHashSet.Contains(destElement))
			{
				var inletID = (int)destElement.Tag;

				_destInletRectangleDictionary.Remove(inletID);
				_destInletRectangleHashSet.Remove(destElement);

				destElement.Dispose();
			}
		}
	}
}