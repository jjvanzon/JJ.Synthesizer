using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Collections;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Configuration;
using JJ.Presentation.Synthesizer.VectorGraphics.Converters;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
	public class PatchViewModelToDiagramConverter
	{
		private class OperatorElements
		{
			public Rectangle OperatorRectangle { get; set; }
			public IList<Point> InletPoints { get; set; }
			public IList<Point> InletControlPoints { get; set; }
			public IList<Point> OutletPoints { get; set; }
			public IList<Point> OutletControlPoints { get; set; }
		}

		private const int DEFAULT_LINE_SEGMENT_COUNT = 15;
		private const bool DEFAULT_MUST_SHOW_INVISIBLE_ELEMENTS = false;

		private static readonly int _lineSegmentCount = GetLineSegmentCount();
		private static readonly bool _mustShowInvisibleElements = GetMustShowInvisibleElements();

		private readonly int _doubleClickSpeedInMilliseconds;
		private readonly int _doubleClickDeltaInPixels;

		private PatchViewModelToDiagramConverterResult _result;

		/// <summary> Maintained to later see which elements are obsolete and should be deleted. </summary>
		private HashSet<Element> _convertedElements;
		/// <summary> Maintained to reuse the same vector graphics elements for an operator already visited. </summary>
		private Dictionary<int, OperatorElements> _operatorID_OperatorElements_Dictionary;
		private Dictionary<int, Curve> _inletID_Curve_Dictionary;
		private OperatorRectangleConverter _operatorRectangleConverter;
		private OperatorLabelConverter _operatorLabelConverter;
		private OperatorDimensionLabelConverter _operatorDimensionLabelConverter;
		private InletRectangleConverter _inletRectangleConverter;
		private InletPointConverter _inletPointConverter;
		private InletControlPointConverter _inletControlPointConverter;
		private OutletRectangleConverter _outletRectangleConverter;
		private OutletPointConverter _outletPointConverter;
		private OutletControlPointConverter _outletControlPointConverter;

		private int _currentPatchID;

		public PatchViewModelToDiagramConverter(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
		{
			_doubleClickSpeedInMilliseconds = doubleClickSpeedInMilliseconds;
			_doubleClickDeltaInPixels = doubleClickDeltaInPixels;

			if (_mustShowInvisibleElements)
			{
				StyleHelper.MakeHiddenStylesVisible();
			}
		}

		/// <param name="result">Pass an existing result to update an existing diagram.</param>
		public PatchViewModelToDiagramConverterResult Execute(
			PatchViewModel sourcePatchViewModel, 
			PatchViewModelToDiagramConverterResult result = null)
		{
			if (sourcePatchViewModel == null) throw new NullException(() => sourcePatchViewModel);

			_result = result;

			bool isDifferentPatch = _result == null || sourcePatchViewModel.ID != _currentPatchID;
			if (isDifferentPatch)
			{
				_currentPatchID = sourcePatchViewModel.ID;

				_result = new PatchViewModelToDiagramConverterResult(_doubleClickSpeedInMilliseconds, _doubleClickDeltaInPixels);

				_operatorRectangleConverter = new OperatorRectangleConverter(
					_result.Diagram,
					_result.MoveGesture,
					_result.SelectOperatorGesture,
					_result.ExpandOperatorMouseGesture);
				_operatorLabelConverter = new OperatorLabelConverter();
				_operatorDimensionLabelConverter = new OperatorDimensionLabelConverter();
				_inletRectangleConverter = new InletRectangleConverter(_result.DropLineGesture, _result.InletToolTipGesture);
				_inletPointConverter = new InletPointConverter();
				_inletControlPointConverter = new InletControlPointConverter();
				_outletRectangleConverter = new OutletRectangleConverter(_result.DragLineGesture, _result.OutletToolTipGesture);
				_outletPointConverter = new OutletPointConverter();
				_outletControlPointConverter = new OutletControlPointConverter();
			}

			_convertedElements = new HashSet<Element>();
			_operatorID_OperatorElements_Dictionary = new Dictionary<int, OperatorElements>();
			_inletID_Curve_Dictionary = new Dictionary<int, Curve>();

			// Do not do Gestures.Clear, because that would mess up DragCancelled in the DragLine gesture.
			if (!_result.Diagram.Background.Gestures.Contains(_result.DeleteOperatorGesture))
			{
				_result.Diagram.Background.Gestures.Add(_result.DeleteOperatorGesture);
			}

			if (!_result.Diagram.Background.Gestures.Contains(_result.ExpandOperatorKeyboardGesture))
			{
				_result.Diagram.Background.Gestures.Add(_result.ExpandOperatorKeyboardGesture);
			}

			if (!_result.Diagram.Background.Gestures.Contains(_result.ExpandPatchGesture))
			{
				_result.Diagram.Background.Gestures.Add(_result.ExpandPatchGesture);
			}

			if (!_result.Diagram.Background.Gestures.Contains(_result.SelectPatchGesture))
			{
				_result.Diagram.Background.Gestures.Add(_result.SelectPatchGesture);
			}

			foreach (OperatorViewModel sourceOperatorViewModel in sourcePatchViewModel.OperatorDictionary.Values)
			{
				ConvertToRectangles_WithRelatedObject_Recursive(sourceOperatorViewModel, _result.Diagram);
			}

			// Delete Elements
			_convertedElements.Add(_result.Diagram.Background);

			IList<Element> elementsToDelete = _result.Diagram.Elements.Except(_convertedElements).ToArray();
			foreach (Element elementToDelete in elementsToDelete)
			{
				// TODO: This is pretty dirty.
				string tagString = Convert.ToString(elementToDelete.Tag) ?? "";

				bool isGestureGeneratedElement = tagString.Contains("ToolTip") ||
												 tagString.Contains("LineGesture");
				if (isGestureGeneratedElement)
				{
					continue;
				}

				switch (elementToDelete)
				{
					case Rectangle rectangleToDelete:
						_inletRectangleConverter.TryRemove(rectangleToDelete);
						_outletRectangleConverter.TryRemove(rectangleToDelete);
						_operatorRectangleConverter.TryRemove(rectangleToDelete);
						break;

					case Point pointToDelete:
						_inletPointConverter.TryRemove(pointToDelete);
						_inletControlPointConverter.TryRemove(pointToDelete);
						_outletPointConverter.TryRemove(pointToDelete);
						_outletControlPointConverter.TryRemove(pointToDelete);
						break;

					case Label labelToDelete:
						_operatorLabelConverter.TryRemove(labelToDelete);
						_operatorDimensionLabelConverter.TryRemove(labelToDelete);
						break;
				}

				elementToDelete.Children.Clear();
				elementToDelete.Parent = null;
				elementToDelete.Diagram = null;
			}

			_operatorID_OperatorElements_Dictionary = null;
			_convertedElements = null;
			_inletID_Curve_Dictionary = null;

			return _result;
		}

		private OperatorElements ConvertToRectangles_WithRelatedObject_Recursive(OperatorViewModel sourceOperatorViewModel1, Diagram destDiagram)
		{
			OperatorElements operatorVectorGraphicsElements1;
			if (_operatorID_OperatorElements_Dictionary.TryGetValue(sourceOperatorViewModel1.ID, out operatorVectorGraphicsElements1))
			{
				return operatorVectorGraphicsElements1;
			}

			operatorVectorGraphicsElements1 = ConvertToRectangle_WithRelatedObjects(sourceOperatorViewModel1);

			_operatorID_OperatorElements_Dictionary.Add(sourceOperatorViewModel1.ID, operatorVectorGraphicsElements1);

			// Go recursive and tie operators together with curves.

			for (int i = 0; i < sourceOperatorViewModel1.Inlets.Count; i++)
			{
				InletViewModel inletViewModel = sourceOperatorViewModel1.Inlets[i];

				if (inletViewModel.InputOutlet == null)
				{
					continue;
				}

				OperatorViewModel sourceOperatorViewModel2 = inletViewModel.InputOutlet.Operator;

				// Recursive call
				OperatorElements operatorVectorGraphicsElements2 =
					ConvertToRectangles_WithRelatedObject_Recursive(sourceOperatorViewModel2, destDiagram);

				int inletID = inletViewModel.ID;

				Curve destCurve = TryGetInletCurve(inletID);
				if (destCurve == null)
				{
					destCurve = new Curve
					{
						SegmentCount = _lineSegmentCount,
						ZIndex = -1,
						Tag = inletID,
						Diagram = destDiagram,
						Parent = destDiagram.Background
					};
					_inletID_Curve_Dictionary.Add(inletID, destCurve);
				}

				_convertedElements.Add(destCurve);

				bool lineMustBeThick = sourceOperatorViewModel1.IsSelected || sourceOperatorViewModel2.IsSelected;
				bool mustHaveWarningAppearance = inletViewModel.HasWarningAppearance ||
												 (inletViewModel.InputOutlet?.HasWarningAppearance ?? false);
				if (mustHaveWarningAppearance)
				{
					if (lineMustBeThick)
					{
						destCurve.LineStyle = StyleHelper.LineStyleWarningThick;
					}
					else
					{
						destCurve.LineStyle = StyleHelper.LineStyleWarning;
					}
				}
				else
				{
					if (lineMustBeThick)
					{
						destCurve.LineStyle = StyleHelper.LineStyleThickDark;
					}
					else
					{
						destCurve.LineStyle = StyleHelper.LineStyle;
					}
				}

				destCurve.PointA = operatorVectorGraphicsElements1.InletPoints[i];
				destCurve.ControlPointA = operatorVectorGraphicsElements1.InletControlPoints[i];

				int? outletIndex = operatorVectorGraphicsElements2.OutletPoints.TryGetIndexOf(x => (int)x.Tag == inletViewModel.InputOutlet.ID);
				// ReSharper disable once InvertIf
				if (outletIndex.HasValue)
				{
					destCurve.PointB = operatorVectorGraphicsElements2.OutletPoints[outletIndex.Value];
					destCurve.ControlPointB = operatorVectorGraphicsElements2.OutletControlPoints[outletIndex.Value];

					// Owned operators move along with the operator they are connected to.
					//bool operator2IsOwned = inletViewModel.InputOutlet.Operator.IsOwned;
					//if (operator2IsOwned)
					//{
					//	operatorVectorGraphicsElements2.OperatorRectangle.Parent = operatorVectorGraphicsElements1.OperatorRectangle;
					//	operatorVectorGraphicsElements2.OperatorRectangle.MustBubble = false;

					//	// Make coordinates relative to the owner rectangle
					//	// and take into account that the centers are stored, 
					//	// while the top-left corner is assigned to the
					//	// vector graphics elements.
					//	operatorVectorGraphicsElements2.OperatorRectangle.X =
					//		sourceOperatorViewModel2.CenterX - sourceOperatorViewModel1.CenterX
					//		- operatorVectorGraphicsElements2.OperatorRectangle.Width / 2f
					//		+ operatorVectorGraphicsElements1.OperatorRectangle.Width / 2f;

					//	operatorVectorGraphicsElements2.OperatorRectangle.Y =
					//		sourceOperatorViewModel2.CenterY - sourceOperatorViewModel1.CenterY
					//		- operatorVectorGraphicsElements2.OperatorRectangle.Height / 2f
					//		+ operatorVectorGraphicsElements1.OperatorRectangle.Height / 2f;
					//}
					//else
					//{
					operatorVectorGraphicsElements2.OperatorRectangle.Parent = destDiagram.Background;
					//}
				}
			}

			return operatorVectorGraphicsElements1;
		}

		private Curve TryGetInletCurve(int id)
		{
			Curve curve;
			if (_inletID_Curve_Dictionary.TryGetValue(id, out curve))
			{
				return curve;
			}

			curve = _result.Diagram.Background.Children
											  .OfType<Curve>()
											  .Where(x => (int)x.Tag == id)
											  .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.
			if (curve != null)
			{
				_inletID_Curve_Dictionary.Add(id, curve);
			}

			return curve;
		}

		private OperatorElements ConvertToRectangle_WithRelatedObjects(OperatorViewModel sourceOperatorViewModel)
		{
			Rectangle destOperatorRectangle = _operatorRectangleConverter.ConvertToOperatorRectangle(sourceOperatorViewModel);
			Label destLabel = _operatorLabelConverter.ConvertToOperatorLabel(sourceOperatorViewModel, destOperatorRectangle);
			Label destDimensionLabel = _operatorDimensionLabelConverter.TryConvertToDimensionLabel(sourceOperatorViewModel, destOperatorRectangle);

			IList<InletViewModel> sourceInletViewModels = sourceOperatorViewModel.Inlets.Where(x => x.Visible).ToArray();

			IList<Rectangle> destInletRectangles = _inletRectangleConverter.ConvertToInletRectangles(sourceOperatorViewModel, destOperatorRectangle);
			IList<Point> destInletPoints = _inletPointConverter.ConvertToInletPoints(sourceOperatorViewModel, destOperatorRectangle);
			IList<Point> destInletControlPoints = _inletControlPointConverter.ConvertToInletControlPoints(destInletPoints, sourceInletViewModels);

			IList<OutletViewModel> sourceOutletViewModels = sourceOperatorViewModel.Outlets.Where(x => x.Visible).ToArray();

			IList<Rectangle> destOutletRectangles = _outletRectangleConverter.ConvertToOutletRectangles(sourceOperatorViewModel, destOperatorRectangle);
			IList<Point> destOutletPoints = _outletPointConverter.ConvertToOutletPoints(sourceOperatorViewModel, destOperatorRectangle);
			IList<Point> destOutletControlPoints = _outletControlPointConverter.ConvertToOutletControlPoints(destOutletPoints, sourceOutletViewModels);

			_convertedElements.Add(destOperatorRectangle);
			_convertedElements.Add(destLabel);
			_convertedElements.AddRange(destInletRectangles);
			_convertedElements.AddRange(destInletPoints);
			_convertedElements.AddRange(destInletControlPoints);
			_convertedElements.AddRange(destOutletRectangles);
			_convertedElements.AddRange(destOutletPoints);
			_convertedElements.AddRange(destOutletControlPoints);
			if (destDimensionLabel != null)
			{
				_convertedElements.Add(destDimensionLabel);
			}

			return new OperatorElements
			{
				OperatorRectangle = destOperatorRectangle,
				InletPoints = destInletPoints,
				InletControlPoints = destInletControlPoints,
				OutletPoints = destOutletPoints,
				OutletControlPoints = destOutletControlPoints
			};
		}

		// Helpers

		private static int GetLineSegmentCount()
		{
			var config = CustomConfigurationManager.TryGetSection<ConfigurationSection>();
			if (config == null) return DEFAULT_LINE_SEGMENT_COUNT;
			return config.PatchLineSegmentCount;
		}

		private static bool GetMustShowInvisibleElements()
		{
			var config = CustomConfigurationManager.TryGetSection<ConfigurationSection>();
			if (config == null) return DEFAULT_MUST_SHOW_INVISIBLE_ELEMENTS;
			return config.MustShowInvisibleElements;
		}
	}
}
