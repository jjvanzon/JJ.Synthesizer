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
	internal class OutletRectangleConverter
	{
		private readonly Dictionary<int, Rectangle> _destOutletRectangleDictionary = new Dictionary<int, Rectangle>();
		private readonly HashSet<Rectangle> _destOutletRectangleHashSet = new HashSet<Rectangle>();
		private readonly GestureBase _dragLineGesture;
		private readonly ToolTipGesture _outletToolTipGesture;

		public OutletRectangleConverter(DragLineGesture dragLineGesture, ToolTipGesture outletToolTipGesture)
		{
			_dragLineGesture = dragLineGesture ?? throw new NullException(() => dragLineGesture);
			_outletToolTipGesture = outletToolTipGesture ?? throw new NullException(() => outletToolTipGesture);
		}

		public IList<Rectangle> ConvertToOutletRectangles(OperatorViewModel sourceOperatorViewModel, Rectangle destOperatorRectangle)
		{
			if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);
			if (destOperatorRectangle == null) throw new NullException(() => destOperatorRectangle);

			IList<OutletViewModel> sourceOutletViewModelsToConvert =
				sourceOperatorViewModel.Outlets
									   .Where(outlet => outlet.Visible)
									   .ToArray();

			if (sourceOutletViewModelsToConvert.Count == 0)
			{
				return new Rectangle[0];
			}

			IList<Rectangle> destOutletRectangles = new List<Rectangle>(sourceOutletViewModelsToConvert.Count);

			float outletWidth = destOperatorRectangle.Position.Width / sourceOutletViewModelsToConvert.Count;
			float rowHeight = destOperatorRectangle.Position.Height / 4;
			const float heightOverflow = StyleHelper.INLET_OUTLET_RECTANGLE_HEIGHT_OVERFLOW_IN_PIXELS;
			float x = 0;
			float y = rowHeight * 3;

			foreach (OutletViewModel sourceOutletViewModel in sourceOutletViewModelsToConvert)
			{
				Rectangle destOutletRectangle = ConvertToOutletRectangle(sourceOutletViewModel, destOperatorRectangle);

				destOutletRectangle.Position.X = x;
				destOutletRectangle.Position.Y = y;
				destOutletRectangle.Position.Width = outletWidth;
				destOutletRectangle.Position.Height = rowHeight + heightOverflow;

				destOutletRectangles.Add(destOutletRectangle);

				x += outletWidth;
			}

			return destOutletRectangles;
		}

		/// <summary> Converts everything but its coordinates. </summary>
		private Rectangle ConvertToOutletRectangle(OutletViewModel sourceOutletViewModel, Rectangle destOperatorRectangle)
		{
			int outletID = sourceOutletViewModel.ID;

			if (!_destOutletRectangleDictionary.TryGetValue(outletID, out Rectangle destOutletRectangle))
			{
				destOutletRectangle = new Rectangle(destOperatorRectangle)
				{
					Tag = outletID,
					Style = 
					{
						BackStyle = StyleHelper.BackStyleInvisible,
						LineStyle = StyleHelper.BorderStyleInvisible
					}
				};


				_destOutletRectangleDictionary.Add(outletID, destOutletRectangle);
				_destOutletRectangleHashSet.Add(destOutletRectangle);
			}

			destOutletRectangle.Gestures.Clear();
			destOutletRectangle.Gestures.Add(_dragLineGesture);
			destOutletRectangle.Gestures.Add(_outletToolTipGesture);
			destOutletRectangle.MustBubble = false; // So drag does not result in a move.

			return destOutletRectangle;
		}

		public void TryRemove(Rectangle destElement)
		{
			if (!_destOutletRectangleHashSet.Contains(destElement))
			{
				return;
			}

			var outletID = (int)destElement.Tag;

			_destOutletRectangleDictionary.Remove(outletID);
			_destOutletRectangleHashSet.Remove(destElement);

			destElement.Dispose();
		}
	}
}
