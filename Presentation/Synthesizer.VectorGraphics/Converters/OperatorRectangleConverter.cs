using System;
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using JJ.Framework.VectorGraphics.Gestures;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
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
		private readonly SelectGesture _selectOperatorGesture;
		private readonly ExpandMouseGesture _showOperatorPropertiesMouseGesture;
		private readonly ITextMeasurer _textMeasurer;

		public OperatorRectangleConverter(
			Diagram diagram,
			MoveGesture moveGesture,
			SelectGesture selectOperatorGesture,
			ExpandMouseGesture showOperatorPropertiesMouseGesture,
			ITextMeasurer textMeasurer)
		{
			_diagram = diagram ?? throw new NullException(() => diagram);
			_moveGesture = moveGesture ?? throw new NullException(() => moveGesture);
			_selectOperatorGesture = selectOperatorGesture ?? throw new NullException(() => selectOperatorGesture);
			_showOperatorPropertiesMouseGesture = showOperatorPropertiesMouseGesture ?? throw new NullException(() => showOperatorPropertiesMouseGesture);
			_textMeasurer = textMeasurer ?? throw new ArgumentNullException(nameof(textMeasurer));
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
					Tag = operatorID
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
				destOperatorRectangle.Style.BackStyle = StyleHelper.OperatorBackStyleSelected;
				destOperatorRectangle.Style.LineStyle = StyleHelper.OperatorBorderStyleSelected;
			}
			else
			{
				destOperatorRectangle.Style.LineStyle = StyleHelper.OperatorBorderStyle;
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

		private float GetOperatorWidth(OperatorViewModel sourceOperatorViewModel)
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

		private float GetOtherOperatorWidth(OperatorViewModel sourceOperatorViewModel)
		{
			WidthAndHeight widthAndHeight = _textMeasurer.GetTextSize(sourceOperatorViewModel.Caption, StyleHelper.DefaultFont);
			float textWidth = widthAndHeight.Width;
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

		private float GetNumberOperatorWidth(OperatorViewModel sourceOperatorViewModel)
		{
			float spacing = StyleHelper.SPACING;
			spacing *= 0.8f; // Use a smaller spacing for numbers.


			WidthAndHeight widthAndHeight = _textMeasurer.GetTextSize(sourceOperatorViewModel.Caption, StyleHelper.NumberOperatorFont);
			float textWidth = widthAndHeight.Width;
			float width = textWidth + spacing + spacing;


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

			int operatorID = (int)destElement.Tag;

			_destOperatorRectangleDictionary.Remove(operatorID);
			_destOperatorRectangleHashSet.Remove(destElement);

			destElement.Children.Clear();
			destElement.Parent = null;
			destElement.Diagram = null;
		}
	}
}