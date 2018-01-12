using System;
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Helpers;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Converters
{
	internal class OperatorRectangleConverter
	{
		private readonly Dictionary<int, Rectangle> _destOperatorRectangleDictionary = new Dictionary<int, Rectangle>();
		private readonly HashSet<Rectangle> _destOperatorRectangleHashSet = new HashSet<Rectangle>();

		private readonly Diagram _diagram;
		private readonly MoveGesture _moveGesture;
		private readonly SelectOperatorGesture _selectOperatorGesture;
		private readonly ExpandOperatorMouseGesture _showOperatorPropertiesMouseGesture;

		public OperatorRectangleConverter(
			Diagram diagram,
			MoveGesture moveGesture,
			SelectOperatorGesture selectOperatorGesture,
			ExpandOperatorMouseGesture showOperatorPropertiesMouseGesture)
		{
			_diagram = diagram ?? throw new NullException(() => diagram);
			_moveGesture = moveGesture ?? throw new NullException(() => moveGesture);
			_selectOperatorGesture = selectOperatorGesture ?? throw new NullException(() => selectOperatorGesture);
			_showOperatorPropertiesMouseGesture = showOperatorPropertiesMouseGesture ?? throw new NullException(() => showOperatorPropertiesMouseGesture);
		}

		public Rectangle ConvertToOperatorRectangle(OperatorViewModel sourceOperatorViewModel)
		{
			if (sourceOperatorViewModel == null) throw new NullException(() => sourceOperatorViewModel);

			int operatorID = sourceOperatorViewModel.ID;

			if (!_destOperatorRectangleDictionary.TryGetValue(operatorID, out Rectangle destOperatorRectangle))
			{
				destOperatorRectangle = new Rectangle
				{
					Diagram = _diagram,
					Parent = _diagram.Background,
					Tag = VectorGraphicsTagHelper.GetOperatorTag(operatorID)
				};

				_destOperatorRectangleDictionary.Add(operatorID, destOperatorRectangle);
				_destOperatorRectangleHashSet.Add(destOperatorRectangle);
			}

			destOperatorRectangle.Position.Width = GetOperatorWidth(sourceOperatorViewModel);
			destOperatorRectangle.Position.Height = GetOperatorHeight(sourceOperatorViewModel);
			destOperatorRectangle.Position.X = sourceOperatorViewModel.Position.CenterX - destOperatorRectangle.Position.Width / 2f;
			destOperatorRectangle.Position.Y = sourceOperatorViewModel.Position.CenterY - destOperatorRectangle.Position.Height / 2f;

			if (sourceOperatorViewModel.IsSelected)
			{
				destOperatorRectangle.Style.BackStyle = StyleHelper.BackStyleSelected;
				destOperatorRectangle.Style.LineStyle = StyleHelper.BorderStyleSelected;
			}
			else
			{
				destOperatorRectangle.Style.LineStyle = StyleHelper.BorderStyle;
				destOperatorRectangle.Style.BackStyle = StyleHelper.GetGradedBackStyle(sourceOperatorViewModel.StyleGrade);
			}

			destOperatorRectangle.Gestures.Clear();
			destOperatorRectangle.Gestures.Add(_moveGesture);
			destOperatorRectangle.Gestures.Add(_selectOperatorGesture);
			destOperatorRectangle.Gestures.Add(_showOperatorPropertiesMouseGesture);

			destOperatorRectangle.MustBubble = false; // So double click does not bubble to BackGround and cause PatchProperties to show instead of OperatorProperties.

			return destOperatorRectangle;
		}

		private float GetOperatorHeight(OperatorViewModel sourceOperatorViewModel)
		{
			if (sourceOperatorViewModel.IsSmaller)
			{
				return StyleHelper.SMALLER_OBJECT_SIZE;
			}
			else
			{
				return StyleHelper.DEFAULT_OBJECT_SIZE;
			}
		}

		private static float GetOperatorWidth(OperatorViewModel sourceOperatorViewModel)
		{
			if (sourceOperatorViewModel.IsSmaller)
			{
				return GetNumberOperatorWidth(sourceOperatorViewModel);
			}
			else
			{
				return GetOtherOperatorWidth(sourceOperatorViewModel);
			}
		}

		private static float GetOtherOperatorWidth(OperatorViewModel sourceOperatorViewModel)
		{
			float textWidth = TextHelper.ApproximateTextWidth(sourceOperatorViewModel.Caption, StyleHelper.DefaultFont);
			float minimumWidth = GetOtherOperatorMinimumWidth(sourceOperatorViewModel);

			float width = textWidth + StyleHelper.SPACING_TIMES_2;

			if (width < minimumWidth)
			{
				width = minimumWidth;
			}

			return width;
		}

		private static float GetOtherOperatorMinimumWidth(OperatorViewModel sourceOperatorViewModel)
		{
			// Apply a minimum inlet and outlet height.
			int inletOrOutletCount = Math.Max(sourceOperatorViewModel.Outlets.Count, sourceOperatorViewModel.Inlets.Count);
			float minimumWidth = inletOrOutletCount * StyleHelper.MINIMUM_INLET_OR_OUTLET_WIDTH_IN_PIXELS;

			// Apply minimum operator width
			if (minimumWidth < StyleHelper.DEFAULT_OBJECT_SIZE)
			{
				minimumWidth = StyleHelper.DEFAULT_OBJECT_SIZE;
			}

			return minimumWidth;
		}

		private static float GetNumberOperatorWidth(OperatorViewModel sourceOperatorViewModel)
		{
			float spacing = StyleHelper.SPACING;
			spacing *= 0.8f; // Use a smaller spacing for numbers.

			float textWidth = TextHelper.ApproximateTextWidth(sourceOperatorViewModel.Caption, StyleHelper.NumberOperatorFont);
			float width = textWidth + spacing + spacing;

			// Compensate for the fact that numbers are averagely wider than letters.
			width = width + spacing + spacing;

			// Apply minimum operator width
			if (width < StyleHelper.SMALLER_OBJECT_SIZE)
			{
				width = StyleHelper.SMALLER_OBJECT_SIZE;
			}

			return width;
		}

		public void TryRemove(Rectangle destElement)
		{
			if (!_destOperatorRectangleHashSet.Contains(destElement))
			{
				return;
			}

			int operatorID = VectorGraphicsTagHelper.GetOperatorID(destElement.Tag);

			_destOperatorRectangleDictionary.Remove(operatorID);
			_destOperatorRectangleHashSet.Remove(destElement);

			destElement.Children.Clear();
			destElement.Parent = null;
			destElement.Diagram = null;
		}
	}
}