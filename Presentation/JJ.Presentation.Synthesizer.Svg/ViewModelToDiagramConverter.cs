using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.Svg.Enums;
using JJ.Framework.Presentation.Svg.Helpers;
using JJ.Framework.Presentation.Svg.Models.Elements;
using JJ.Framework.Presentation.Svg.Models.Styling;
using JJ.Framework.Presentation.Svg.Gestures;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.Svg.Gestures;
using JJ.Presentation.Synthesizer.Svg.Converters;
using JJ.Presentation.Synthesizer.Svg.Helpers;

namespace JJ.Presentation.Synthesizer.Svg
{
    public class ViewModelToDiagramConverter
    {
        private class OperatorElements
        {
            public Rectangle OperatorRectangle { get; set; }
            public IList<Point> InletPoints { get; set; }
            public IList<Point> OutletPoints { get; set; }
            public IList<Rectangle> InletRectangles { get; set; }
            public IList<Rectangle> OutletRectangles { get; set; }
        }

        private bool _tooltipFeatureEnabled;
        private ViewModelToDiagramConverterResult _result;
        private Dictionary<OperatorViewModel, OperatorElements> _convertedOperatorDictionary;
        private IList<Line> _convertedLines;

        private OperatorRectangleConverter _operatorToRectangleConverter;
        private OperatorLabelConverter _operatorToLabelConverter;
        private InletRectangleConverter _inletRectangleConverter;
        private InletPointConverter _inletPointConverter;
        private OutletRectangleConverter _outletRectangleConverter;
        private OutletPointConverter _outletPointConverter;

        /// <param name="mustShowInvisibleElements">for debugging</param>
        public ViewModelToDiagramConverter(bool mustShowInvisibleElements = false, bool tooltipFeatureEnabled = true)
        {
            _tooltipFeatureEnabled = tooltipFeatureEnabled;

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

            if (result == null)
            {
                var destDiagram = new Diagram();
                var moveGesture = new MoveGesture();
                var dragGesture = new DragGesture();
                var dropGesture = new DropGesture(dragGesture);
                var lineGesture = new LineGesture(destDiagram, StyleHelper.LineStyleDashed, lineZIndex: -1);
                var selectOperatorGesture = new SelectOperatorGesture();
                var deleteOperatorGesture = new DeleteOperatorGesture();

                ToolTipGesture operatorToolTipGesture = null;
                ToolTipGesture inletToolTipGesture = null;
                ToolTipGesture outletToolTipGesture = null;
                if (_tooltipFeatureEnabled)
                {
                    operatorToolTipGesture = new ToolTipGesture(destDiagram, StyleHelper.ToolTipBackStyle, StyleHelper.ToolTipLineStyle, StyleHelper.ToolTipTextStyle, zIndex: 2);
                    inletToolTipGesture = new ToolTipGesture(destDiagram, StyleHelper.ToolTipBackStyle, StyleHelper.ToolTipLineStyle, StyleHelper.ToolTipTextStyle, zIndex: 2);
                    outletToolTipGesture = new ToolTipGesture(destDiagram, StyleHelper.ToolTipBackStyle, StyleHelper.ToolTipLineStyle, StyleHelper.ToolTipTextStyle, zIndex: 2);
                }

                _operatorToRectangleConverter = new OperatorRectangleConverter(destDiagram, moveGesture, selectOperatorGesture, operatorToolTipGesture);
                _operatorToLabelConverter = new OperatorLabelConverter();
                _inletRectangleConverter = new InletRectangleConverter(dropGesture, inletToolTipGesture);
                _inletPointConverter = new InletPointConverter();
                _outletRectangleConverter = new OutletRectangleConverter(dragGesture, lineGesture, outletToolTipGesture);
                _outletPointConverter = new OutletPointConverter();

                result = new ViewModelToDiagramConverterResult(destDiagram, moveGesture, dragGesture, dropGesture, lineGesture, selectOperatorGesture, deleteOperatorGesture, operatorToolTipGesture, inletToolTipGesture, outletToolTipGesture);
            }

            _convertedOperatorDictionary = new Dictionary<OperatorViewModel, OperatorElements>();
            _convertedLines = new List<Line>();

            IList<Rectangle> destExistingOperatorRectangles = result.Diagram.Canvas.Children
                                                                                   .OfType<Rectangle>()
                                                                                   .Where(x => TagHelper.IsOperatorTag(x.Tag))
                                                                                   .ToArray();
            IList<Line> destExistingLines = result.Diagram.Canvas.Children
                                                                 .OfType<Line>()
                                                                 .Where(x => TagHelper.IsInletTag(x.Tag))
                                                                 .ToArray();
            _result = result;
            _result.Diagram.Canvas.Gestures.Clear();
            _result.Diagram.Canvas.Gestures.Add(_result.DeleteOperatorGesture);
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
            IList<Line> destLinesToDelete = destExistingLines.Except(_convertedLines).ToArray();
            foreach (Line destLineToDelete in destLinesToDelete)
            {
                destLineToDelete.Parent = null;
                destLineToDelete.Diagram = null;
            }

            _convertedOperatorDictionary = null;
            _convertedLines = null;

            return _result;
        }

        private OperatorElements ConvertToRectangles_WithRelatedObject_Recursive(OperatorViewModel sourceOperatorViewModel, Diagram destDiagram)
        {
            OperatorElements operatorSvgElements1;
            if (_convertedOperatorDictionary.TryGetValue(sourceOperatorViewModel, out operatorSvgElements1))
            {
                return operatorSvgElements1;
            }

            operatorSvgElements1 = ConvertToRectangle_WithRelatedObjects(sourceOperatorViewModel, destDiagram);

            _convertedOperatorDictionary.Add(sourceOperatorViewModel, operatorSvgElements1);

            // Go recursive and tie operators together with lines.
            for (int i = 0; i < sourceOperatorViewModel.Inlets.Count; i++)
            {
                InletViewModel inletViewModel = sourceOperatorViewModel.Inlets[i];

                if (inletViewModel.InputOutlet != null)
                {
                    // Recursive call
                    OperatorElements operatorSvgElements2 = ConvertToRectangles_WithRelatedObject_Recursive(inletViewModel.InputOutlet.Operator, destDiagram);

                    Line destLine = TryGetInletLine(inletViewModel.ID);
                    if (destLine == null)
                    {
                        destLine = CreateLine();
                        destLine.Tag = TagHelper.GetInletTag(inletViewModel.ID);
                        destLine.Diagram = destDiagram;
                        destLine.Parent = destDiagram.Canvas;
                        _inletLineDictionary.Add(inletViewModel.ID, destLine);
                    }

                    destLine.PointA = operatorSvgElements1.InletPoints[i];

                    _convertedLines.Add(destLine);

                    if (operatorSvgElements2.OutletPoints.Count > 0) // TODO: This does not work for multiple outlets.
                    {
                        destLine.PointB = operatorSvgElements2.OutletPoints[0];
                    }
                }
            }

            return operatorSvgElements1;
        }

        private Dictionary<int, Line> _inletLineDictionary = new Dictionary<int, Line>();

        private Line TryGetInletLine(int inletID)
        {
            Line line;
            if (!_inletLineDictionary.TryGetValue(inletID, out line))
            {
                line = _result.Diagram.Canvas.Children
                                             .OfType<Line>()
                                             .Where(x => TagHelper.TryGetInletID(x.Tag) == inletID)
                                             .FirstOrDefault(); // First instead of Single will result in excessive ones being cleaned up.

                if (line != null)
                {
                    _inletLineDictionary.Add(inletID, line);
                }
            }

            return line;
        }

        private Line CreateLine()
        {
            var destLine = new Line
            {
                LineStyle = StyleHelper.LineStyleThin,
                ZIndex = -1
            };

            return destLine;
        }

        private OperatorElements ConvertToRectangle_WithRelatedObjects(OperatorViewModel sourceOperatorViewModel, Diagram destDiagram)
        {
            Rectangle destOperatorRectangle = _operatorToRectangleConverter.ConvertToOperatorRectangle(sourceOperatorViewModel, destDiagram);
            Label destLabel = _operatorToLabelConverter.ConvertToOperatorLabel(sourceOperatorViewModel, destOperatorRectangle);
            IList<Rectangle> destInletRectangles = _inletRectangleConverter.ConvertToInletRectangles(sourceOperatorViewModel, destOperatorRectangle);
            IList<Point> destInletPoints = _inletPointConverter.ConvertToInletPoints(sourceOperatorViewModel, destOperatorRectangle);
            IList<Rectangle> destOutletRectangles = _outletRectangleConverter.ConvertToOutletRectangles(sourceOperatorViewModel, destOperatorRectangle);
            IList<Point> destOutletPoints = _outletPointConverter.ConvertToOutletPoints(sourceOperatorViewModel, destOperatorRectangle);

            IList<Element> childrenToDelete = destOperatorRectangle.Children
                                                                   .Except(destLabel)
                                                                   .Except(destInletRectangles)
                                                                   .Except(destInletPoints)
                                                                   .Except(destOutletRectangles)
                                                                   .Except(destOutletPoints)
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
                InletRectangles = destInletRectangles,
                InletPoints = destInletPoints,
                OutletRectangles = destOutletRectangles,
                OutletPoints = destOutletPoints
            };
        }
    }
}
