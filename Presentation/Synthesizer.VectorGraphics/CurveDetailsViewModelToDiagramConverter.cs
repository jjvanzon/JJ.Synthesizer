﻿using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Api;
using JJ.Business.Synthesizer.Calculation.Curves;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Common.Exceptions;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Configuration;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

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

        private const float MINIMUM_X_RANGE = 1E-9f;
        private const float MINIMUM_Y_RANGE = -1E-9f;
        private const int MINIMUM_NODE_COUNT = 2;
        private const int DEFAULT_LINE_SEGMENT_COUNT = 10;
        private const float DEFAULT_CLICKABLE_REGION_SIZE_IN_PIXELS = 20;
        private const bool DEFAULT_MUST_SHOW_INVISIBLE_ELEMENTS = false;

        /// <summary> Elements with this tag are deleted and recreated upon each conversion. <summary>
        private const string HELPER_ELEMENT_TAG = "Helper Element";

        private static int _lineSegmentCount = GetLineSegmentCount();
        private static int _lineSegmentPointCount = GetLineSegmentCount() + 1;
        private static float _nodeClickableRegionSizeInPixels = GetNodeClickableRegionSizeInPixels();
        private static bool _mustShowInvisibleElements = GetMustShowInvisibleElements();

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

        /// <summary> Not nullable. Never replaced with a new instance. Neither are its properties. </summary>
        public CurveDetailsViewModelToDiagramConverterResult Result { get; private set; }

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

            _currentCurveInfo = CreateCurveInfo(curveDetailsViewModel.Nodes.Values.ToArray());
            _currentCurveCalculator = CurveApi.CreateInterpretedCalculator(_currentCurveInfo.MockCurve);

            // Delete All Lines
            IList<Element> elementsToDelete = Result.Diagram.Elements
                                                            .Where(x => String.Equals(Convert.ToString(x.Tag), HELPER_ELEMENT_TAG))
                                                            .ToArray();

            foreach (Element elementToDelete in elementsToDelete)
            {
                elementToDelete.Children.Clear();
                elementToDelete.Parent = null;
                elementToDelete.Diagram = null;
            }

            IList<NodeViewModel> sortedNodeViewModels = curveDetailsViewModel.Nodes.Values.OrderBy(x => x.X).ToArray();
            float minX = (float)sortedNodeViewModels.First().X;
            float maxX = (float)sortedNodeViewModels.Last().X;
            float minY = (float)sortedNodeViewModels.Select(x => x.Y).Min();
            float maxY = (float)sortedNodeViewModels.Select(x => x.Y).Max();

            float xRange = maxX - minX;
            if (xRange < MINIMUM_X_RANGE)
            {
                xRange = MINIMUM_X_RANGE;
            }

            // NOTE: The direction of the y-axis is inverted, so range is negative.
            float yRange = minY - maxY;
            if (yRange > MINIMUM_Y_RANGE)
            {
                yRange = MINIMUM_Y_RANGE;
            }

            // Set Scaling
            Result.Diagram.Position.ScaleModeEnum = ScaleModeEnum.ViewPort;
            Result.Diagram.Position.ScaledX = minX;
            Result.Diagram.Position.ScaledWidth = xRange;
            Result.Diagram.Position.ScaledY = maxY;
            Result.Diagram.Position.ScaledHeight = yRange;

            // Set Margin
            // (This is not full-proof, since margin is calculated based on the point's pixel width and scaling without margin,
            //  But then the scaling is changed based on the margin, making the point's scaled width a little off.
            //  The difference will probably be marginal, but it can get noticable when you make the diagram very small.)
            float marginInPixels = StyleHelper.PointStyleThick.Width / 2;
            float marginX = Result.Diagram.Position.PixelsToWidth(marginInPixels);
            float marginY = Result.Diagram.Position.PixelsToHeight(marginInPixels);
            Result.Diagram.Position.ScaledX -= marginX;
            Result.Diagram.Position.ScaledWidth += marginX * 2;
            Result.Diagram.Position.ScaledY -= marginY;
            Result.Diagram.Position.ScaledHeight += marginY * 2;

            // NOTE: We need to calculate so that the background element's
            // coordinates are set, on which coordinates 
            // of all sorts of other elements are based.
            Result.Diagram.Recalculate();

            // Update Misc Elements
            UpdateXAxis();
            UpdateYAxis();
            UpdateLeftBoundLabel(minX);
            UpdateRightBoundLabel(maxX);
            UpdateTopBoundLabel(maxY);
            UpdateBottomBoundLabel(minY);

            // Points, Lines and Clickable Regions
            float scaledNodeRectangleWidth = Result.Diagram.Position.PixelsToWidth(_nodeClickableRegionSizeInPixels);
            float scaledNodeRectangleHeight = Result.Diagram.Position.PixelsToHeight(_nodeClickableRegionSizeInPixels);
            float scaledNodeRectangleWidthOver2 = scaledNodeRectangleWidth / 2;
            float scaledNodeRectangleHeightOver2 = scaledNodeRectangleHeight / 2;

            Point previousPoint = null;
            NodeViewModel previousNodeViewModel = null;

            foreach (NodeViewModel nodeViewModel in sortedNodeViewModels)
            {
                // Coordinates are always relative. (Lowest x translates to x = 0, relative to the background.)
                float x = Result.Diagram.Background.Position.AbsoluteToRelativeX((float)nodeViewModel.X);
                float y = Result.Diagram.Background.Position.AbsoluteToRelativeY((float)nodeViewModel.Y);

                // Convert Rectangle
                Rectangle rectangle;
                if (!_rectangleDictionary.TryGetValue(nodeViewModel.ID, out rectangle))
                {
                    rectangle = new Rectangle
                    {
                        Diagram = Result.Diagram,
                        Parent = Result.Diagram.Background,
                        Tag = nodeViewModel.ID
                    };
                    rectangle.Style.LineStyle = StyleHelper.BorderStyleInvisible;
                    rectangle.Style.BackStyle = StyleHelper.BackStyleInvisible;

                    rectangle.Gestures.Add(
                        Result.MoveNodeGesture,
                        Result.SelectNodeGesture,
                        Result.ShowNodePropertiesMouseGesture,
                        Result.NodeToolTipGesture);

                    _rectangleDictionary.Add(nodeViewModel.ID, rectangle);
                }
                rectangle.Position.X = x - scaledNodeRectangleWidthOver2;
                rectangle.Position.Y = y - scaledNodeRectangleHeightOver2;
                rectangle.Position.Width = scaledNodeRectangleWidth;
                rectangle.Position.Height = scaledNodeRectangleHeight;

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
                point.Position.X = scaledNodeRectangleWidthOver2;
                point.Position.Y = scaledNodeRectangleHeightOver2;

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
            IEnumerable<int> idsToKeep = curveDetailsViewModel.Nodes.Keys;
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
                    pointToDelete.Children.Clear();
                    pointToDelete.Parent = null;
                    pointToDelete.Diagram = null;
                    _pointDictionary.Remove(idToDelete);
                }

                // Delete rectangle
                Rectangle rectangleToDelete;
                if (_rectangleDictionary.TryGetValue(idToDelete, out rectangleToDelete))
                {
                    rectangleToDelete.Children.Clear();
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
                    PointStyle = StyleHelper.PointStyleInvisible
                },
                PointB = new Point
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    PointStyle = StyleHelper.PointStyleInvisible
                }
            };

            line.PointA.Position.X = 0;

#if DEBUG
            line.Tag = "X-Axis";
            line.PointA.Tag = "X-Axis";
            line.PointB.Tag = "X-Axis";
#endif
            return line;
        }

        private void UpdateXAxis()
        {
            _xAxis.PointA.Position.Y = Result.Diagram.Background.Position.AbsoluteToRelativeY(0);
            _xAxis.PointB.Position.X = Result.Diagram.Background.Position.Width;
            _xAxis.PointB.Position.Y = Result.Diagram.Background.Position.AbsoluteToRelativeY(0);
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
                    PointStyle = StyleHelper.PointStyleInvisible
                },
                PointB = new Point
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    PointStyle = StyleHelper.PointStyleInvisible
                }
            };

            line.PointA.Position.Y = 0;

#if DEBUG
            line.Tag = "Y-Axis";
            line.PointA.Tag = "Y-Axis";
            line.PointB.Tag = "Y-Axis";
#endif
            return line;
        }

        private void UpdateYAxis()
        {
            _yAxis.PointA.Position.X = Result.Diagram.Background.Position.AbsoluteToRelativeX(0);
            _yAxis.PointB.Position.X = Result.Diagram.Background.Position.AbsoluteToRelativeX(0);
            _yAxis.PointB.Position.Y = Result.Diagram.Background.Position.Height;
        }

        private Label CreateLeftBoundLabel(Diagram diagram)
        {
            var label = new Label
            {
                Diagram = diagram,
                Parent = diagram.Background,
            };
            label.Position.X = 0;
            label.TextStyle = StyleHelper.CreateTextStyleSmallerTransparent();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Center;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left;
#if DEBUG
            label.Tag = "Left-Bound Label";
#endif
            return label;
        }

        private void UpdateLeftBoundLabel(float minX)
        {
            _leftBoundLabel.Position.Y = Result.Diagram.Background.Position.Height / 2;
            _leftBoundLabel.Text = minX.ToString("0.###");
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

        private void UpdateRightBoundLabel(float maxX)
        {
            _rightBoundLabel.Position.X = Result.Diagram.Background.Position.Width;
            _rightBoundLabel.Position.Y = Result.Diagram.Background.Position.Height / 2;
            _rightBoundLabel.Text = maxX.ToString("0.###");
        }

        private Label CreateTopBoundLabel(Diagram diagram)
        {
            var label = new Label
            {
                Diagram = diagram,
                Parent = diagram.Background,
            };
            label.Position.Y = 0;
            label.TextStyle = StyleHelper.CreateTextStyleSmallerTransparent();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Top;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center;
#if DEBUG
            label.Tag = "Top-Bound Label";
#endif
            return label;
        }

        private void UpdateTopBoundLabel(float maxY)
        {
            _topBoundLabel.Position.X = Result.Diagram.Background.Position.Width / 2;
            _topBoundLabel.Text = maxY.ToString("0.###");
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

        private void UpdateBottomBoundLabel(float minY)
        {
            _bottomBoundLabel.Position.X = Result.Diagram.Background.Position.Width / 2;
            _bottomBoundLabel.Position.Y = Result.Diagram.Background.Position.Height;
            _bottomBoundLabel.Text = minY.ToString("0.###");
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
                PointStyle = StyleHelper.PointStyleInvisible,
                Tag = HELPER_ELEMENT_TAG
            };
            verticalLineTo0.PointB.Position.X = 0;
            verticalLineTo0.PointB.Position.Y = previousPoint.Position.AbsoluteToRelativeY(0);

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
                PointStyle = StyleHelper.PointStyleInvisible,
                Tag = HELPER_ELEMENT_TAG
            };
            horizontalLine.PointB.Position.X = 0;
            horizontalLine.PointB.Position.Y = nextPoint.Position.AbsoluteToRelativeY(0);


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
                PointStyle = StyleHelper.PointStyleInvisible,
                Tag = HELPER_ELEMENT_TAG
            };
            line.PointB.Position.X = 0;
            line.PointB.Position.Y = nextPoint.Position.AbsoluteToRelativeY(previousPoint.Position.AbsoluteY);

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
                                              .Where(nt => nt.NodeViewModel.ID == (int)previousPoint.Tag)
                                              .Select(nt => nt.MockNode)
                                              .Single();

            Node mockNode1 = _currentCurveInfo.NodeTuples
                                              .Where(nt => nt.NodeViewModel.ID == (int)nextPoint.Tag)
                                              .Select(nt => nt.MockNode)
                                              .Single();

            var destPoints = new List<Point>(_lineSegmentPointCount);

            destPoints.Add(previousPoint);

            double step = (mockNode1.X - mockNode0.X) / _lineSegmentCount;
            double x = mockNode0.X + step;
            for (int i = 0; i < _lineSegmentPointCount - 2; i++)
            {
                double y = _currentCurveCalculator.CalculateY(x);

                var destPoint = new Point
                {
                    Diagram = previousPoint.Diagram,
                    Parent = previousPoint.Diagram.Background,
                    PointStyle = StyleHelper.PointStyleInvisible,
                    Tag = HELPER_ELEMENT_TAG
                };

                destPoint.Position.X = destPoint.Parent.Position.AbsoluteToRelativeX((float)x);
                destPoint.Position.Y = destPoint.Parent.Position.AbsoluteToRelativeY((float)y);

                destPoints.Add(destPoint);

                x += step;
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
            IList<NodeInfo> nodeInfoList = nodeViewModels.Select(x => CreateNodeInfo(x)).ToArray();

            JJ.Data.Synthesizer.Curve mockCurve = CurveApi.Create(nodeInfoList);

            IList<NodeTuple> nodeTuples = new List<NodeTuple>();

            for (int i = 0; i < nodeInfoList.Count; i++)
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

        private static int GetLineSegmentCount()
        {
            var config = ConfigurationHelper.TryGetSection<ConfigurationSection>();
            if (config == null) return DEFAULT_LINE_SEGMENT_COUNT;
            return config.CurveLineSegmentCount;
        }

        private static float GetNodeClickableRegionSizeInPixels()
        {
            var config = ConfigurationHelper.TryGetSection<ConfigurationSection>();
            if (config == null) return DEFAULT_CLICKABLE_REGION_SIZE_IN_PIXELS;
            return config.NodeClickableRegionSizeInPixels;
        }

        private static bool GetMustShowInvisibleElements()
        {
            var config = ConfigurationHelper.TryGetSection<ConfigurationSection>();
            if (config == null) return DEFAULT_MUST_SHOW_INVISIBLE_ELEMENTS;
            return config.MustShowInvisibleElements;
        }

        private static NodeInfo CreateNodeInfo(NodeViewModel x)
        {
            return new NodeInfo(x.X, x.Y, (NodeTypeEnum)x.NodeType.ID);
        }
    }
}