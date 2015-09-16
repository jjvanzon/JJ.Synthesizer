using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Converters;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class ViewModelToDiagramConverter
    {
        private class OperatorElements
        {
            public Rectangle OperatorRectangle { get; set; }
            public IList<Point> InletPoints { get; set; }
            public IList<Point> InletControlPoints { get; set; }
            public IList<Point> OutletPoints { get; set; }
            public IList<Point> OutletControlPoints { get; set; }
        }

        private readonly int _doubleClickSpeedInMilliseconds;
        private readonly int _doubleClickDeltaInPixels;
        private bool _tooltipFeatureEnabled;
        private ViewModelToDiagramConverterResult _result;
        private Dictionary<OperatorViewModel, OperatorElements> _convertedOperatorDictionary;
        private IList<Curve> _convertedCurves;

        private OperatorRectangleConverter _operatorToRectangleConverter;
        private OperatorLabelConverter _operatorToLabelConverter;
        private InletRectangleConverter _inletRectangleConverter;
        private InletPointConverter _inletPointConverter;
        private InletControlPointConverter _inletControlPointConverter;
        private OutletRectangleConverter _outletRectangleConverter;
        private OutletPointConverter _outletPointConverter;
        private OutletControlPointConverter _outletControlPointConverter;
        private OperatorToolTipRectangleConverter _operatorToolTipRectangleConverter;

        private int _currentPatchID;

        /// <param name="mustShowInvisibleElements">for debugging</param>
        public ViewModelToDiagramConverter(
            int doubleClickSpeedInMilliseconds,
            int doubleClickDeltaInPixels,
            bool mustShowInvisibleElements = false,
            bool tooltipFeatureEnabled = true)
        {
            _tooltipFeatureEnabled = tooltipFeatureEnabled;
            _doubleClickSpeedInMilliseconds = doubleClickSpeedInMilliseconds;
            _doubleClickDeltaInPixels = doubleClickDeltaInPixels;

            if (mustShowInvisibleElements)
            {
                StyleHelper.MakeHiddenStylesVisible();
            }
        }
        
        // General

        /// <param name="result">Pass an existing result to update an existing diagram.</param>
        public ViewModelToDiagramConverterResult Execute(PatchViewModel sourcePatchViewModel, ViewModelToDiagramConverterResult result = null)
        {
            if (sourcePatchViewModel == null) throw new NullException(() => sourcePatchViewModel);

            if (result == null || sourcePatchViewModel.ID != _currentPatchID)
            {
                _currentPatchID = sourcePatchViewModel.ID;

                var destDiagram = new Diagram();
                var moveGesture = new MoveGesture();
                var dragLineGesture = new DragLineGesture(destDiagram, StyleHelper.LineStyleDashed, StyleHelper.DRAG_DROP_LINE_ZINDEX);
                var dropLineGesture = new DropLineGesture(
                    destDiagram, new DragLineGesture[] { dragLineGesture }, StyleHelper.LineStyleDashed, StyleHelper.DRAG_DROP_LINE_ZINDEX );
                var selectOperatorGesture = new SelectOperatorGesture();
                var deleteOperatorGesture = new DeleteOperatorGesture();
                var doubleClickOperatorGesture = new DoubleClickGesture(_doubleClickSpeedInMilliseconds, _doubleClickDeltaInPixels);

                ToolTipGesture operatorToolTipGesture = null;
                ToolTipGesture inletToolTipGesture = null;
                ToolTipGesture outletToolTipGesture = null;
                if (_tooltipFeatureEnabled)
                {
                    operatorToolTipGesture = new ToolTipGesture(destDiagram, StyleHelper.ToolTipBackStyle, StyleHelper.ToolTipLineStyle, StyleHelper.ToolTipTextStyle, zIndex: 2);
                    inletToolTipGesture = new ToolTipGesture(destDiagram, StyleHelper.ToolTipBackStyle, StyleHelper.ToolTipLineStyle, StyleHelper.ToolTipTextStyle, zIndex: 2);
                    outletToolTipGesture = new ToolTipGesture(destDiagram, StyleHelper.ToolTipBackStyle, StyleHelper.ToolTipLineStyle, StyleHelper.ToolTipTextStyle, zIndex: 2);
                }

                _operatorToRectangleConverter = new OperatorRectangleConverter(destDiagram, moveGesture, selectOperatorGesture, doubleClickOperatorGesture);
                _operatorToLabelConverter = new OperatorLabelConverter();
                _inletRectangleConverter = new InletRectangleConverter(dropLineGesture, inletToolTipGesture);
                _inletPointConverter = new InletPointConverter();
                _inletControlPointConverter = new InletControlPointConverter();
                _outletRectangleConverter = new OutletRectangleConverter(dragLineGesture, outletToolTipGesture);
                _outletPointConverter = new OutletPointConverter();
                _outletControlPointConverter = new OutletControlPointConverter();
                if (_tooltipFeatureEnabled)
                {
                    _operatorToolTipRectangleConverter = new OperatorToolTipRectangleConverter(operatorToolTipGesture);
                }

                result = new ViewModelToDiagramConverterResult(
                    destDiagram, 
                    moveGesture, 
                    dragLineGesture, 
                    dropLineGesture, 
                    selectOperatorGesture, 
                    deleteOperatorGesture,
                    doubleClickOperatorGesture,
                    operatorToolTipGesture, 
                    inletToolTipGesture, 
                    outletToolTipGesture);
            }

            _convertedOperatorDictionary = new Dictionary<OperatorViewModel, OperatorElements>();
            _convertedCurves = new List<Curve>();

            IList<Rectangle> destExistingOperatorRectangles = result.Diagram.Background
                                                                            .Children
                                                                            .OfType<Rectangle>()
                                                                            .Where(x => VectorGraphicsTagHelper.IsOperatorTag(x.Tag))
                                                                            .ToArray();
            IList<Curve> destExistingCurves = result.Diagram.Background
                                                            .Children
                                                            .OfType<Curve>()
                                                            .Where(x => VectorGraphicsTagHelper.IsInletTag(x.Tag))
                                                            .ToArray();
            _result = result;

            // Do not do Gestures.Clear, because that would mess up DragCancelled in the DragLine gesture.
            if (!_result.Diagram.Background.Gestures.Contains(_result.DeleteOperatorGesture))
            {
                _result.Diagram.Background.Gestures.Add(_result.DeleteOperatorGesture);
            }

            foreach (OperatorViewModel sourceOperatorViewModel in sourcePatchViewModel.Operators)
            {
                ConvertToRectangles_WithRelatedObject_Recursive(sourceOperatorViewModel, result.Diagram);
            }

            // Delete operator rectangles + descendants
            IList<Rectangle> destConvertedOperatorRectangles = _convertedOperatorDictionary.Select(x => x.Value.OperatorRectangle).ToArray();
            IList<Rectangle> destOperatorRectanglesToDelete = destExistingOperatorRectangles.Except(destConvertedOperatorRectangles).ToArray();

            foreach (Rectangle destOperatorRectangleToDelete in destOperatorRectanglesToDelete)
            {
                IList<Element> destDescendantElements = destOperatorRectanglesToDelete.SelectMany(x => x.Children)
                                                                                      .UnionRecursive(x => x.Children)
                                                                                      .ToArray();
                foreach (Element destDescendantElement in destDescendantElements)
                {
                    destDescendantElement.Parent = null;
                    destDescendantElement.Diagram = null;
                }

                destOperatorRectangleToDelete.Parent = null;
                destOperatorRectangleToDelete.Diagram = null;
            }

            // Delete lines
            IList<Curve> destCurvesToDelete = destExistingCurves.Except(_convertedCurves).ToArray();
            foreach (Curve destLineToDelete in destCurvesToDelete)
            {
                destLineToDelete.Parent = null;
                destLineToDelete.Diagram = null;
            }

            _convertedOperatorDictionary = null;
            _convertedCurves = null;

            return _result;
        }

        private OperatorElements ConvertToRectangles_WithRelatedObject_Recursive(OperatorViewModel sourceOperatorViewModel, Diagram destDiagram)
        {
            OperatorElements operatorVectorGraphicsElements1;
            if (_convertedOperatorDictionary.TryGetValue(sourceOperatorViewModel, out operatorVectorGraphicsElements1))
            {
                return operatorVectorGraphicsElements1;
            }

            operatorVectorGraphicsElements1 = ConvertToRectangle_WithRelatedObjects(sourceOperatorViewModel, destDiagram);

            _convertedOperatorDictionary.Add(sourceOperatorViewModel, operatorVectorGraphicsElements1);

            // Go recursive and tie operators together with curves.
            for (int i = 0; i < sourceOperatorViewModel.Inlets.Count; i++)
            {
                InletViewModel inletViewModel = sourceOperatorViewModel.Inlets[i];

                if (inletViewModel.InputOutlet != null)
                {
                    // Recursive call
                    OperatorElements operatorVectorGraphicsElements2 = ConvertToRectangles_WithRelatedObject_Recursive(inletViewModel.InputOutlet.Operator, destDiagram);

                    int id = inletViewModel.ID;
                    

                    Curve destCurve = TryGetInletCurve(id);
                    if (destCurve == null)
                    {
                        destCurve = new Curve
                        {
                            LineStyle = StyleHelper.LineStyleThin,
                            ZIndex = -1,
                            Tag = VectorGraphicsTagHelper.GetInletTag(id),
                            Diagram = destDiagram,
                            Parent = destDiagram.Background
                        };
                        _inletCurveDictionary.Add(id, destCurve);
                    }

                    destCurve.PointA = operatorVectorGraphicsElements1.InletPoints[i];
                    destCurve.ControlPointA = operatorVectorGraphicsElements1.InletControlPoints[i];

                    _convertedCurves.Add(destCurve);

                    int? outletIndex = operatorVectorGraphicsElements2.OutletPoints.TryGetIndexOf(x => VectorGraphicsTagHelper.GetOutletID(x.Tag) == inletViewModel.InputOutlet.ID);
                    if (outletIndex.HasValue) 
                    {
                        destCurve.PointB = operatorVectorGraphicsElements2.OutletPoints[outletIndex.Value];
                        destCurve.ControlPointB = operatorVectorGraphicsElements2.OutletControlPoints[outletIndex.Value];
                    }
                }
            }

            return operatorVectorGraphicsElements1;
        }

        private Dictionary<int, Curve> _inletCurveDictionary = new Dictionary<int, Curve>();

        private Curve TryGetInletCurve(int id)
        {
            Curve curve;
            if (!_inletCurveDictionary.TryGetValue(id, out curve))
            {
                curve = _result.Diagram.Background.Children
                                              .OfType<Curve>()
                                              .Where(x => VectorGraphicsTagHelper.TryGetInletID(x.Tag) == id)
                                              .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (curve != null)
                {
                    _inletCurveDictionary.Add(id, curve);
                }
            }

            return curve;
        }

        private OperatorElements ConvertToRectangle_WithRelatedObjects(OperatorViewModel sourceOperatorViewModel, Diagram destDiagram)
        {
            Rectangle destOperatorRectangle = _operatorToRectangleConverter.ConvertToOperatorRectangle(sourceOperatorViewModel, destDiagram);
            Label destLabel = _operatorToLabelConverter.ConvertToOperatorLabel(sourceOperatorViewModel, destOperatorRectangle);
            IList<Rectangle> destInletRectangles = _inletRectangleConverter.ConvertToInletRectangles(sourceOperatorViewModel, destOperatorRectangle);
            IList<Point> destInletPoints = _inletPointConverter.ConvertToInletPoints(sourceOperatorViewModel, destOperatorRectangle);
            IList<Point> destInletControlPoints = _inletControlPointConverter.ConvertToInletControlPoints(destInletPoints);
            IList<Rectangle> destOutletRectangles = _outletRectangleConverter.ConvertToOutletRectangles(sourceOperatorViewModel, destOperatorRectangle);
            IList<Point> destOutletPoints = _outletPointConverter.ConvertToOutletPoints(sourceOperatorViewModel, destOperatorRectangle);
            IList<Point> destOutletControlPoints = _outletControlPointConverter.ConvertToOutletControlPoints(destOutletPoints);

            Rectangle destOperatorToolTipRectangle = null;
            if (_tooltipFeatureEnabled)
            {
                destOperatorToolTipRectangle = _operatorToolTipRectangleConverter.ConvertToOperatorToolTipRectangle(sourceOperatorViewModel, destOperatorRectangle);
            }

            IList<Element> childrenToDelete = destOperatorRectangle.Children
                                                                   .Except(destLabel)
                                                                   .Except(destInletRectangles)
                                                                   .Except(destInletPoints)
                                                                   .Except(destInletControlPoints)
                                                                   .Except(destOutletRectangles)
                                                                   .Except(destOutletPoints)
                                                                   .Except(destOutletControlPoints)
                                                                   .Except(destOperatorToolTipRectangle)
                                                                   .ToArray();
            foreach (Element childToDelete in childrenToDelete)
            {
                // TODO: This is pretty dirty.
                bool isToolTipElement = childToDelete.Tag != null  && !childToDelete.Tag.ToString().Contains("Tooltip");
                if (isToolTipElement)
                {
                    continue;
                }

                childToDelete.Children.Clear();
                childToDelete.Parent = null;
                childToDelete.Diagram = null;
            }

            // Return result
            return new OperatorElements
            {
                OperatorRectangle = destOperatorRectangle,
                InletPoints = destInletPoints,
                InletControlPoints = destInletControlPoints,
                OutletPoints = destOutletPoints,
                OutletControlPoints = destOutletControlPoints
            };
        }
    }
}
