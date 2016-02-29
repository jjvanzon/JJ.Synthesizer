using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Common;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.Helpers;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Business.Synthesizer.Api;
using JJ.Business.Synthesizer.Enums;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.VectorGraphics.Configuration;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Framework.Common.Exceptions;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class CurveDetailsViewModelToDiagramConverter
    {
        private class CurveInfo
        {
            public JJ.Data.Synthesizer.Curve MockCurve { get; set; }
            public IList<NodeTuple> NodeTuples { get; set; }
        }

        private class NodeTuple
        {
            public NodeViewModel NodeViewModel { get; set; }
            public Node MockNode { get; set; }
        }

        private static int _lineSegmentCount = GetLineSegmentCount();
        private static int _lineSegmentPointCount = GetLineSegmentCount() + 1;
        private static float _nodeClickableRegionSizeInPixels = GetNodeClickableRegionSizeInPixels();
        private static bool _mustShowInvisibleElements = GetMustShowInvisibleElements();

        private const int MINIMUM_NODE_COUNT = 2;

        /// <summary> Elements with this tag are deleted and recreated upon each conversion. <summary>
        private const string HELPER_ELEMENT_TAG = "Helper Element";

        /// <summary> Not nullable. Never replaced with a new instance. Neither are its properties. </summary>
        public CurveDetailsViewModelToDiagramConverterResult Result { get; private set; }

        private readonly Line _xAxis;
        private readonly Line _yAxis;
        private readonly Label _topBoundLabel;
        private readonly Label _bottomBoundLabel;
        private readonly Label _rightBoundLabel;
        private readonly Label _leftBoundLabel;
        /// <summary> Key is Node.ID. </summary>
        private readonly Dictionary<int, Point> _pointDictionary = new Dictionary<int, Point>();
        /// <summary> Key is Node.ID. </summary>
        private readonly Dictionary<int, Rectangle> _rectangleDictionary = new Dictionary<int, Rectangle>();

        /// <summary>
        /// A mock curve is created out of the view model,
        /// to calculate intermediate points for curvy lines 
        /// using the same calculation as in the sound.
        /// </summary>
        private CurveInfo _currentCurveInfo;
        private ICurveCalculator _currentCurveCalculator;

        /// <param name="mustShowInvisibleElements">for debugging</param>
        public CurveDetailsViewModelToDiagramConverter(int doubleClickSpeedInMilliseconds, int doubleClickDeltaInPixels)
        {
            Result = new CurveDetailsViewModelToDiagramConverterResult(doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);

            _xAxis = CreateXAxis(Result.Diagram);
            _yAxis = CreateYAxis(Result.Diagram);
            _topBoundLabel = CreateTopBoundLabel(Result.Diagram);
            _bottomBoundLabel = CreateBottomBoundLabel(Result.Diagram);
            _rightBoundLabel = CreateRightBoundLabel(Result.Diagram);
            _leftBoundLabel = CreateLeftBoundLabel(Result.Diagram);

            if (_mustShowInvisibleElements)
            {
                StyleHelper.MakeHiddenStylesVisible();
            }
        }

        public void Execute(CurveDetailsViewModel curveDetailsViewModel)
        {
            if (curveDetailsViewModel == null) throw new NullException(() => curveDetailsViewModel);
            if (curveDetailsViewModel.Nodes.Count < MINIMUM_NODE_COUNT) throw new LessThanException(() => curveDetailsViewModel.Nodes.Count, MINIMUM_NODE_COUNT);

            _currentCurveInfo = CreateCurveInfo(curveDetailsViewModel.Nodes);
            _currentCurveCalculator = CurveApi.CreateInterpretedCalculator(_currentCurveInfo.MockCurve);

            // Delete All Lines
            IList<Element> elementsToDelete = Result.Diagram.Elements
                                                            .Where(x => String.Equals(Convert.ToString(x.Tag), HELPER_ELEMENT_TAG))
                                                            .ToArray();
                                                            
            foreach (Element elementToDelete in elementsToDelete)
            {
                elementToDelete.Parent = null;
                elementToDelete.Diagram = null;
            }

            IList<NodeViewModel> sortedNodeViewModels = curveDetailsViewModel.Nodes.OrderBy(x => x.Time).ToArray();
            float minTime = (float)sortedNodeViewModels.First().Time;
            float maxTime = (float)sortedNodeViewModels.Last().Time;
            float minValue = (float)sortedNodeViewModels.Select(x => x.Value).Min();
            float maxValue = (float)sortedNodeViewModels.Select(x => x.Value).Max();

            // Set Scaling
            Result.Diagram.ScaleModeEnum = ScaleModeEnum.ViewPort;
            Result.Diagram.ScaledX = minTime;
            Result.Diagram.ScaledWidth = maxTime - minTime;
            // NOTE: The direction of the y-axis is inverted.
            Result.Diagram.ScaledY = maxValue;
            Result.Diagram.ScaledHeight = minValue - maxValue;

            // Set Margin
            // (This is not full-proof, since margin is calculated based on the point's pixel width and scaling without margin,
            //  But then the scaling is changed based on the margin, making the point's scaled width a little off.
            //  The difference will probably be marginal, but it can get noticable when you make the diagram very small.)
            float marginInPixels = StyleHelper.PointStyleThick.Width / 2;
            float marginX = ScaleHelper.PixelsToWidth(Result.Diagram, marginInPixels);
            float marginY = ScaleHelper.PixelsToHeight(Result.Diagram, marginInPixels);
            Result.Diagram.ScaledX -= marginX;
            Result.Diagram.ScaledWidth += marginX * 2;
            Result.Diagram.ScaledY -= marginY;
            Result.Diagram.ScaledHeight += marginY * 2;

            // NOTE: We need to calculate so that the background element's
            // coordinates are set, on which coordinates 
            // of all sorts of other elements are based.
            Result.Diagram.Recalculate();

            // Update Misc Elements
            UpdateXAxis();
            UpdateYAxis();
            UpdateLeftBoundLabel(minTime);
            UpdateRightBoundLabel(maxTime);
            UpdateTopBoundLabel(maxValue);
            UpdateBottomBoundLabel(minValue);

            // Points, Lines and Clickable Regions
            float scaledNodeRectangleWidth = ScaleHelper.PixelsToWidth(Result.Diagram, _nodeClickableRegionSizeInPixels);
            float scaledNodeRectangleHeight = ScaleHelper.PixelsToHeight(Result.Diagram, _nodeClickableRegionSizeInPixels);
            float scaledNodeRectangleWidthOver2 = scaledNodeRectangleWidth / 2;
            float scaledNodeRectangleHeightOver2 = scaledNodeRectangleHeight / 2;

            Point previousPoint = null;
            NodeViewModel previousNodeViewModel = null;

            foreach (NodeViewModel nodeViewModel in sortedNodeViewModels)
            {
                // Coordinates are always relative. (Lowest time translates to x = 0, relative to the background.)
                float x = ScaleHelper.AbsoluteToRelativeX(Result.Diagram.Background, (float)nodeViewModel.Time);
                float y = ScaleHelper.AbsoluteToRelativeY(Result.Diagram.Background, (float)nodeViewModel.Value);

                // Convert Rectangle
                Rectangle rectangle;
                if (!_rectangleDictionary.TryGetValue(nodeViewModel.ID, out rectangle))
                {
                    rectangle = new Rectangle
                    {
                        Diagram = Result.Diagram,
                        Parent = Result.Diagram.Background,
                        LineStyle = StyleHelper.BorderStyleInvisible,
                        BackStyle = StyleHelper.BackStyleInvisible,
                        Tag = nodeViewModel.ID
                    };
                    rectangle.Gestures.Add(Result.MoveNodeGesture, Result.SelectNodeGesture, Result.ShowNodePropertiesGesture);
                    rectangle.MustBubble = false;

                    _rectangleDictionary.Add(nodeViewModel.ID, rectangle);
                }
                rectangle.X = x - scaledNodeRectangleWidthOver2;
                rectangle.Y = y - scaledNodeRectangleHeightOver2;
                rectangle.Width = scaledNodeRectangleWidth;
                rectangle.Height = scaledNodeRectangleHeight;

                // Convert Point
                Point point;
                if (!_pointDictionary.TryGetValue(nodeViewModel.ID, out point))
                {
                    point = new Point
                    {
                        Diagram = Result.Diagram,
                        Parent = rectangle,
                        PointStyle = StyleHelper.PointStyleThick,
                        ZIndex = 1,
                        Tag = nodeViewModel.ID
                    };

                    _pointDictionary.Add(nodeViewModel.ID, point);
                }
                point.X = scaledNodeRectangleWidthOver2;
                point.Y = scaledNodeRectangleHeightOver2;

                // TODO: Low priority: If NodeViewModel had a property IsSelected,
                // you would not have had to pass the CurveDetailsViewModel to this converter,
                // but only an IList<NodeViewModel>.
                if (nodeViewModel.ID == curveDetailsViewModel.SelectedNodeID)
                {
                    point.PointStyle = StyleHelper.PointStyleThickSelected;
                }
                else
                {
                    point.PointStyle = StyleHelper.PointStyleThick;
                }

                if (previousPoint != null)
                {
                    NodeTypeEnum nodeTypeEnum = (NodeTypeEnum)previousNodeViewModel.NodeType.ID;
                    CreateLines_WithRelatedElements(Result.Diagram, previousPoint, point, nodeTypeEnum);
                }

                previousPoint = point;
                previousNodeViewModel = nodeViewModel;
            }

            // Delete accessory points and rectangles.
            IEnumerable<int> existingIDs = Result.Diagram.Elements.Select(x => x.Tag).OfType<int>();
            IEnumerable<int> idsToKeep = curveDetailsViewModel.Nodes.Select(x => x.ID);
            IList<int> idsToDelete = existingIDs.Except(idsToKeep).ToArray();

            foreach (int idToDelete in idsToDelete)
            {
                // NOTE: The tollerant TryGetValue calls are done for defensive programming.
                // This class is long-lived and if an exception occurs and then you assign another view model, 
                // you do not want to be stuck with even more exceptions.
                // By being tollerant about what is in the dictionaries,
                // you have a chance that state may actually be cleaned up.

                // Delete point
                Point pointToDelete;
                if (_pointDictionary.TryGetValue(idToDelete, out pointToDelete))
                {
                    pointToDelete.Parent = null;
                    pointToDelete.Diagram = null;
                    _pointDictionary.Remove(idToDelete);
                }

                // Delete rectangle
                Rectangle rectangleToDelete;
                if (_rectangleDictionary.TryGetValue(idToDelete, out rectangleToDelete))
                {
                    rectangleToDelete.Parent = null;
                    rectangleToDelete.Diagram = null;
                    _rectangleDictionary.Remove(idToDelete);
                }
            }
        }

        private Line CreateXAxis(Diagram diagram)
        {
            var line = new Line
            {
                Diagram = diagram,
                Parent = diagram.Background,
                LineStyle = StyleHelper.LineStyleTransparent,
                PointA = new Point
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    X = 0,
                    PointStyle = StyleHelper.PointStyleInvisible
                },
                PointB = new Point
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    PointStyle = StyleHelper.PointStyleInvisible
                }
            };
#if DEBUG
            line.Tag = "X-Axis";
            line.PointA.Tag = "X-Axis";
            line.PointB.Tag = "X-Axis";
#endif
            return line;
        }

        private void UpdateXAxis()
        {
            _xAxis.PointA.Y = ScaleHelper.AbsoluteToRelativeY(Result.Diagram.Background, 0);
            _xAxis.PointB.X = Result.Diagram.Background.Width;
            _xAxis.PointB.Y = ScaleHelper.AbsoluteToRelativeY(Result.Diagram.Background, 0);
        }

        private Line CreateYAxis(Diagram diagram)
        {
            var line = new Line
            {
                Diagram = diagram,
                Parent = diagram.Background,
                LineStyle = StyleHelper.LineStyleTransparent,
                PointA = new Point
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    Y = 0,
                    PointStyle = StyleHelper.PointStyleInvisible
                },
                PointB = new Point
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    PointStyle = StyleHelper.PointStyleInvisible
                }
            };
#if DEBUG
            line.Tag = "Y-Axis";
            line.PointA.Tag = "Y-Axis";
            line.PointB.Tag = "Y-Axis";
#endif
            return line;
        }

        private void UpdateYAxis()
        {
            _yAxis.PointA.X = ScaleHelper.AbsoluteToRelativeX(Result.Diagram.Background, 0);
            _yAxis.PointB.X = ScaleHelper.AbsoluteToRelativeX(Result.Diagram.Background, 0);
            _yAxis.PointB.Y = Result.Diagram.Background.Height;
        }

        private Label CreateLeftBoundLabel(Diagram diagram)
        {
            var label = new Label
            {
                Diagram = diagram,
                Parent = diagram.Background,
                X = 0
            };

            label.TextStyle = StyleHelper.CreateTextStyleSmallerTransparent();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Center;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left;
#if DEBUG
            label.Tag = "Left-Bound Label";
#endif
            return label;
        }

        private void UpdateLeftBoundLabel(float minTime)
        {
            _leftBoundLabel.Y = Result.Diagram.Background.Height / 2;
            _leftBoundLabel.Text = minTime.ToString("0.###");
        }

        private Label CreateRightBoundLabel(Diagram diagram)
        {
            var label = new Label
            {
                Diagram = diagram,
                Parent = diagram.Background,
            };

            label.TextStyle = StyleHelper.CreateTextStyleSmallerTransparent();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Center;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Right;
#if DEBUG
            label.Tag = "Right-Bound Label";
#endif
            return label;
        }

        private void UpdateRightBoundLabel(float maxTime)
        {
            _rightBoundLabel.X = Result.Diagram.Background.Width;
            _rightBoundLabel.Y = Result.Diagram.Background.Height / 2;
            _rightBoundLabel.Text = maxTime.ToString("0.###");
        }

        private Label CreateTopBoundLabel(Diagram diagram)
        {
            var label = new Label
            {
                Diagram = diagram,
                Parent = diagram.Background,
                Y = 0
            };

            label.TextStyle = StyleHelper.CreateTextStyleSmallerTransparent();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Top;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center;
#if DEBUG
            label.Tag = "Top-Bound Label";
#endif
            return label;
        }

        private void UpdateTopBoundLabel(float maxValue)
        {
            _topBoundLabel.X = Result.Diagram.Background.Width / 2;
            _topBoundLabel.Text = maxValue.ToString("0.###");
        }

        private Label CreateBottomBoundLabel(Diagram diagram)
        {
            var label = new Label
            {
                Diagram = diagram,
                Parent = diagram.Background,
            };

            label.TextStyle = StyleHelper.CreateTextStyleSmallerTransparent();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Bottom;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center;
#if DEBUG
            label.Tag = "Bottom-Bound Label";
#endif
            return label;
        }

        private void UpdateBottomBoundLabel(float minValue)
        {
            _bottomBoundLabel.X = Result.Diagram.Background.Width / 2;
            _bottomBoundLabel.Y = Result.Diagram.Background.Height;
            _bottomBoundLabel.Text = minValue.ToString("0.###");
        }

        private void CreateLines_WithRelatedElements(Diagram diagram, Point previousPoint, Point nextPoint, NodeTypeEnum previousNodeTypeEnum)
        {
            switch (previousNodeTypeEnum)
            {
                case NodeTypeEnum.Line:
                    CreateLines_WithRelatedElements_ForNodeTypeLine(diagram, previousPoint, nextPoint);
                    break;

                case NodeTypeEnum.Block:
                    CreateLines_WithRelatedElements_ForNodeTypeBlock(diagram, previousPoint, nextPoint);
                    break;

                case NodeTypeEnum.Off:
                    CreateLines_WithRelatedElements_ForNodeTypeOff(diagram, previousPoint, nextPoint);
                    break;

                case NodeTypeEnum.Curve:
                    CreateLines_WithRelatedElements_ForNodeTypeCurve(diagram, previousPoint, nextPoint);
                    break;

                default:
                    throw new InvalidValueException(previousNodeTypeEnum);
            }
        }

        private void CreateLines_WithRelatedElements_ForNodeTypeOff(Diagram diagram, Point previousPoint, Point nextPoint)
        {
            var verticalLineTo0 = new Line
            {
                Diagram = diagram,
                Parent = diagram.Background,
                PointA = previousPoint,
                LineStyle = StyleHelper.LineStyleThick,
                Tag = HELPER_ELEMENT_TAG
            };

            verticalLineTo0.PointB = new Point
            {
                Diagram = diagram,
                Parent = previousPoint,
                X = 0,
                Y = ScaleHelper.AbsoluteToRelativeY(previousPoint, 0),
                PointStyle = StyleHelper.PointStyleInvisible,
                Tag = HELPER_ELEMENT_TAG
            };

            var horizontalLine = new Line
            {
                Diagram = diagram,
                Parent = diagram.Background,
                PointA = verticalLineTo0.PointB,
                LineStyle = StyleHelper.LineStyleThick,
                Tag = HELPER_ELEMENT_TAG
            };

            horizontalLine.PointB = new Point
            {
                Diagram = diagram,
                Parent = nextPoint,
                X = 0,
                Y = ScaleHelper.AbsoluteToRelativeY(nextPoint, 0),
                PointStyle = StyleHelper.PointStyleInvisible,
                Tag = HELPER_ELEMENT_TAG
            };

            var verticalLineToNextPoint = new Line
            {
                Diagram = diagram,
                Parent = diagram.Background,
                PointA = horizontalLine.PointB,
                PointB = nextPoint,
                LineStyle = StyleHelper.LineStyleThick,
                Tag = HELPER_ELEMENT_TAG
            };
        }

        private void CreateLines_WithRelatedElements_ForNodeTypeBlock(Diagram diagram, Point previousPoint, Point nextPoint)
        {
            // Create horizontal line to the next node.
            var line = new Line
            {
                Diagram = diagram,
                Parent = diagram.Background,
                PointA = previousPoint,
                LineStyle = StyleHelper.LineStyleThick,
                Tag = HELPER_ELEMENT_TAG
            };

            line.PointB = new Point
            {
                Diagram = diagram,
                Parent = nextPoint,
                X = 0,
                Y = ScaleHelper.AbsoluteToRelativeY(nextPoint, previousPoint.AbsoluteY),
                PointStyle = StyleHelper.PointStyleInvisible,
                Tag = HELPER_ELEMENT_TAG
            };

            // Create vertical line down.
            var line2 = new Line
            {
                Diagram = diagram,
                Parent = diagram.Background,
                PointA = line.PointB,
                PointB = nextPoint,
                LineStyle = StyleHelper.LineStyleThick,
                Tag = HELPER_ELEMENT_TAG
            };
        }

        private void CreateLines_WithRelatedElements_ForNodeTypeLine(Diagram diagram, Point previousPoint, Point nextPoint)
        {
            var line = new Line
            {
                Diagram = diagram,
                Parent = diagram.Background,
                PointA = previousPoint,
                PointB = nextPoint,
                LineStyle = StyleHelper.LineStyleThick,
                Tag = HELPER_ELEMENT_TAG
            };
        }

        private void CreateLines_WithRelatedElements_ForNodeTypeCurve(Diagram diagram, Point previousPoint, Point nextPoint)
        {
            Node mockNode0 = _currentCurveInfo.NodeTuples
                                       .Where(x => x.NodeViewModel.ID == (int)previousPoint.Tag)
                                       .Select(x => x.MockNode)
                                       .Single();

            Node mockNode1 = _currentCurveInfo.NodeTuples
                                       .Where(x => x.NodeViewModel.ID == (int)nextPoint.Tag)
                                       .Select(x => x.MockNode)
                                       .Single();

            var destPoints = new List<Point>(_lineSegmentPointCount);

            destPoints.Add(previousPoint);

            double step = (mockNode1.Time - mockNode0.Time) / _lineSegmentCount;
            double time = mockNode0.Time + step;
            for (int i = 0; i < _lineSegmentPointCount - 2; i++)
            {
                double value = _currentCurveCalculator.CalculateValue(time);

                var destPoint = new Point
                {
                    Diagram = previousPoint.Diagram,
                    Parent = previousPoint.Diagram.Background,
                    PointStyle = StyleHelper.PointStyleInvisible,
                    Tag = HELPER_ELEMENT_TAG
                };

                destPoint.X = ScaleHelper.AbsoluteToRelativeX(destPoint.Parent, (float)time);
                destPoint.Y = ScaleHelper.AbsoluteToRelativeY(destPoint.Parent, (float)value);

                destPoints.Add(destPoint);

                time += step;
            }

            destPoints.Add(nextPoint);

            var destLines = new List<Line>(_lineSegmentCount);

            for (int i = 0; i < destPoints.Count - 1; i++)
            {
                Point destPointA = destPoints[i];
                Point destPointB = destPoints[i + 1];

                var destLine = new Line
                {
                    Diagram = previousPoint.Diagram,
                    Parent = previousPoint,
                    PointA = destPointA,
                    PointB = destPointB,
                    LineStyle = StyleHelper.LineStyleThick,
                    Tag = HELPER_ELEMENT_TAG
                };

                destLines.Add(destLine);
            }
        }

        private CurveInfo CreateCurveInfo(IList<NodeViewModel> nodeViewModels)
        {
            IList<NodeInfo> nodeInfoList = nodeViewModels.Select(x => new NodeInfo(x.Time, x.Value, (NodeTypeEnum)x.NodeType.ID)).ToArray();

            JJ.Data.Synthesizer.Curve mockCurve = CurveApi.Create(nodeInfoList);

            IList<NodeTuple> nodeTuples = new List<NodeTuple>(nodeViewModels.Count);

            for (int i = 0; i < nodeViewModels.Count; i++)
            {
                nodeTuples.Add(new NodeTuple
                {
                    MockNode = mockCurve.Nodes[i],
                    NodeViewModel = nodeViewModels[i]
                });
            }

            return new CurveInfo
            {
                MockCurve = mockCurve,
                NodeTuples = nodeTuples
            };
        }

        // Helpers

        private const int DEFAULT_LINE_SEGMENT_COUNT = 10;

        private static int GetLineSegmentCount()
        {
            var config = ConfigurationHelper.TryGetSection<ConfigurationSection>();
            if (config == null) return DEFAULT_LINE_SEGMENT_COUNT;
            return config.CurveLineSegmentCount;
        }

        private const float DEFAULT_CLICKABLE_REGION_SIZE_IN_PIXELS = 20;

        private static float GetNodeClickableRegionSizeInPixels()
        {
            var config = ConfigurationHelper.TryGetSection<ConfigurationSection>();
            if (config == null) return DEFAULT_CLICKABLE_REGION_SIZE_IN_PIXELS;
            return config.NodeClickableRegionSizeInPixels;
        }

        private const bool DEFAULT_MUST_SHOW_INVISIBLE_ELEMENTS = false;

        private static bool GetMustShowInvisibleElements()
        {
            var config = ConfigurationHelper.TryGetSection<ConfigurationSection>();
            if (config == null) return DEFAULT_MUST_SHOW_INVISIBLE_ELEMENTS;
            return config.MustShowInvisibleElements;
        }
    }
}
