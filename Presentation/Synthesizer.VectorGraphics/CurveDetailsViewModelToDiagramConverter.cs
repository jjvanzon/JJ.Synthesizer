using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.Comparative;
using JJ.Framework.Exceptions.InvalidValues;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Presentation.Synthesizer.VectorGraphics.Configuration;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using Curve = JJ.Data.Synthesizer.Entities.Curve;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class CurveDetailsViewModelToDiagramConverter
    {
        private readonly CurveFacade _curveFacade;

        private class CurveInfo
        {
            public Curve MockCurve { get; set; }
            public IList<NodeInfo> NodeInfos { get; set; }
        }

        private class NodeInfo
        {
            public NodeViewModel NodeViewModel { get; set; }
            public Node MockNode { get; set; }
        }

        private const float MINIMUM_X_RANGE = 1E-9f;
        private const float MINIMUM_Y_RANGE = -1E-9f;
        private const int MINIMUM_NODE_COUNT = 2;
        private const int DEFAULT_LINE_SEGMENT_COUNT = 10;
        private const float DEFAULT_CLICKABLE_REGION_SIZE_IN_PIXELS = 20;

        /// <summary> Elements with this tag are deleted and recreated upon each conversion. </summary>
        private const string HELPER_ELEMENT_TAG = "Helper Element";

        private static readonly int _lineSegmentCount = GetLineSegmentCount();
        private static readonly int _lineSegmentPointCount = GetLineSegmentCount() + 1;
        private static readonly float _nodeClickableRegionSizeInPixels = GetNodeClickableRegionSizeInPixels();

        private readonly Line _xAxis;
        private readonly Line _yAxis;
        private readonly Label _topBoundCoodinateLabel;
        private readonly Label _bottomBoundCoodinateLabel;
        private readonly Label _rightBoundCoodinateLabel;
        private readonly Label _leftBoundCoodinateLabel;
        private readonly Label _waterMarkTitleLabel;
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
        private ICalculatorWithPosition _currentCurveCalculator;

        /// <summary> Not nullable. Never replaced with a new instance. Neither are its properties. </summary>
        public CurveDetailsViewModelToDiagramConverterResult Result { get; }

        public CurveDetailsViewModelToDiagramConverter(
            int doubleClickSpeedInMilliseconds,
            int doubleClickDeltaInPixels,
            ITextMeasurer textMeasurer,
            CurveFacade curveFacade)
        {
            _curveFacade = curveFacade ?? throw new ArgumentNullException(nameof(curveFacade));

            Result = new CurveDetailsViewModelToDiagramConverterResult(textMeasurer, doubleClickSpeedInMilliseconds, doubleClickDeltaInPixels);

            _xAxis = CreateXAxis(Result.Diagram);
            _yAxis = CreateYAxis(Result.Diagram);
            _topBoundCoodinateLabel = CreateTopBoundCoordinateLabel(Result.Diagram);
            _bottomBoundCoodinateLabel = CreateBottomBoundCoordinateLabel(Result.Diagram);
            _rightBoundCoodinateLabel = CreateRightBoundCoordinateLabel(Result.Diagram);
            _leftBoundCoodinateLabel = CreateLeftBoundCoordinateLabel(Result.Diagram);
            _waterMarkTitleLabel = CreateWaterMarkTitleLabel(Result.Diagram);
        }

        public void Execute(CurveDetailsViewModel curveDetailsViewModel)
        {
            if (curveDetailsViewModel == null) throw new NullException(() => curveDetailsViewModel);
            if (curveDetailsViewModel.Nodes.Count < MINIMUM_NODE_COUNT) throw new LessThanException(() => curveDetailsViewModel.Nodes.Count, MINIMUM_NODE_COUNT);

            _currentCurveInfo = CreateCurveInfo(curveDetailsViewModel.Nodes.Values.ToArray());
            _currentCurveCalculator = _curveFacade.CreateInterpretedCalculator(_currentCurveInfo.MockCurve);

            // Delete All Lines
            IList<Element> elementsToDelete = Result.Diagram.Elements
                                                    .Where(x => string.Equals(Convert.ToString(x.Tag), HELPER_ELEMENT_TAG))
                                                    .ToArray();

            foreach (Element elementToDelete in elementsToDelete)
            {
                elementToDelete.Dispose();
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
            //  The difference will probably be 'marginal', but it can get noticable when you make the diagram very small.)
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
            UpdateWaterMarkTitleLabel(curveDetailsViewModel.Curve.Name);
            UpdateXAxis();
            UpdateYAxis();
            UpdateLeftBoundCoordinateLabel(minX);
            UpdateRightBoundCoordinateLabel(maxX);
            UpdateTopBoundCoordinateLabel(maxY);
            UpdateBottomBoundCoordinateLabel(minY);

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
                if (!_rectangleDictionary.TryGetValue(nodeViewModel.ID, out Rectangle rectangle))
                {
                    rectangle = new Rectangle(Result.Diagram.Background)
                    {
                        Tag = nodeViewModel.ID,
                        MustBubble = false,
                        Style = StyleHelper.RectangleStyleInvisible
                    };

                    rectangle.Gestures.Add(
                        Result.MoveNodeGesture,
                        Result.SelectNodeGesture,
                        Result.ExpandNodeMouseGesture,
                        Result.NodeToolTipGesture);

                    _rectangleDictionary.Add(nodeViewModel.ID, rectangle);
                }
                rectangle.Position.X = x - scaledNodeRectangleWidthOver2;
                rectangle.Position.Y = y - scaledNodeRectangleHeightOver2;
                rectangle.Position.Width = scaledNodeRectangleWidth;
                rectangle.Position.Height = scaledNodeRectangleHeight;

                // Convert Point
                if (!_pointDictionary.TryGetValue(nodeViewModel.ID, out Point point))
                {
                    point = new Point(rectangle)
                    {
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
                    var previousInterpolationTypeEnum = (InterpolationTypeEnum)previousNodeViewModel.Interpolation.ID;
                    var interpolationTypeEnum = (InterpolationTypeEnum)nodeViewModel.Interpolation.ID;

                    CreateLines_WithRelatedElements(previousPoint, point, previousInterpolationTypeEnum, interpolationTypeEnum);
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
                // NOTE: The tolerant TryGetValue calls are done for defensive programming.
                // This class is long-lived and if an exception occurs and then you assign another view model, 
                // you do not want to be stuck with even more exceptions.
                // By being tolerant about what is in the dictionaries,
                // you have a chance that state may actually be cleaned up.

                // Delete point
                if (_pointDictionary.TryGetValue(idToDelete, out Point pointToDelete))
                {
                    pointToDelete.Dispose();
                    _pointDictionary.Remove(idToDelete);
                }

                // Delete rectangle
                // ReSharper disable once InvertIf
                if (_rectangleDictionary.TryGetValue(idToDelete, out Rectangle rectangleToDelete))
                {
                    rectangleToDelete.Dispose();
                    _rectangleDictionary.Remove(idToDelete);
                }
            }
        }

        private Line CreateXAxis(Diagram diagram)
        {
            var line = new Line(diagram.Background)
            {
                LineStyle = StyleHelper.LineStyleTransparent,
                PointA = new Point(diagram.Background)
                {
                    PointStyle = StyleHelper.PointStyleInvisible
                },
                PointB = new Point(diagram.Background)
                {
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
            float y = _xAxis.Diagram.Background.Position.AbsoluteToRelativeY(0);

            _xAxis.PointA.Position.Y = y;
            _xAxis.PointB.Position.X = _xAxis.Diagram.Background.Position.Width;
            _xAxis.PointB.Position.Y = y;

            float margin = _xAxis.Diagram.Position.PixelsToHeight(StyleHelper.PointStyleThick.Width / 2 + 1);

            // NOTE: Positions are relative and axis is flipped.
            _xAxis.Visible = _xAxis.Position.Y < 0 + margin &&
                             _xAxis.Position.Y > _xAxis.Parent.Position.Height - margin;
        }

        private Line CreateYAxis(Diagram diagram)
        {
            var line = new Line(diagram.Background)
            {
                LineStyle = StyleHelper.LineStyleTransparent,
                PointA = new Point(diagram.Background)
                {
                    PointStyle = StyleHelper.PointStyleInvisible
                },
                PointB = new Point(diagram.Background)
                {
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
            _yAxis.PointA.Position.X = _yAxis.Diagram.Background.Position.AbsoluteToRelativeX(0);
            _yAxis.PointB.Position.X = _yAxis.Diagram.Background.Position.AbsoluteToRelativeX(0);
            _yAxis.PointB.Position.Y = _yAxis.Diagram.Background.Position.Height;

            float margin = _xAxis.Diagram.Position.PixelsToWidth(StyleHelper.PointStyleThick.Width / 2 + 1);

            // NOTE: Positions are relative and axis is NOT flipped.
            _yAxis.Visible = _yAxis.Position.X > 0 + margin &&
                             _yAxis.Position.X < _xAxis.Parent.Position.Width - margin;
        }

        private Label CreateLeftBoundCoordinateLabel(Diagram diagram)
        {
            var label = new Label(diagram.Background);
            label.Position.X = 0;
            label.TextStyle = StyleHelper.TextStyleSmallerTransparent.Clone();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Center;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left;
#if DEBUG
            label.Tag = "Left-Bound Label";
#endif
            return label;
        }

        private void UpdateLeftBoundCoordinateLabel(float minX)
        {
            _leftBoundCoodinateLabel.Position.Y = _leftBoundCoodinateLabel.Diagram.Background.Position.Height / 2;
            _leftBoundCoodinateLabel.Text = minX.ToString("0.###");
        }

        private Label CreateRightBoundCoordinateLabel(Diagram diagram)
        {
            var label = new Label(diagram.Background)
            {
                TextStyle = StyleHelper.TextStyleSmallerTransparent.Clone()
            };
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Center;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Right;
#if DEBUG
            label.Tag = "Right-Bound Label";
#endif
            return label;
        }

        private void UpdateRightBoundCoordinateLabel(float maxX)
        {
            _rightBoundCoodinateLabel.Position.X = _rightBoundCoodinateLabel.Diagram.Background.Position.Width;
            _rightBoundCoodinateLabel.Position.Y = _rightBoundCoodinateLabel.Diagram.Background.Position.Height / 2;
            _rightBoundCoodinateLabel.Text = maxX.ToString("0.###");
        }

        private Label CreateTopBoundCoordinateLabel(Diagram diagram)
        {
            var label = new Label(diagram.Background);
            label.Position.Y = 0;
            label.TextStyle = StyleHelper.TextStyleSmallerTransparent.Clone();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Top;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center;
#if DEBUG
            label.Tag = "Top-Bound Label";
#endif
            return label;
        }

        private void UpdateTopBoundCoordinateLabel(float maxY)
        {
            _topBoundCoodinateLabel.Position.X = _topBoundCoodinateLabel.Diagram.Background.Position.Width / 2;
            _topBoundCoodinateLabel.Text = maxY.ToString("0.###");
        }

        private Label CreateBottomBoundCoordinateLabel(Diagram diagram)
        {
            var label = new Label(diagram.Background)
            {
                TextStyle = StyleHelper.TextStyleSmallerTransparent.Clone()
            };

            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Bottom;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center;
#if DEBUG
            label.Tag = "Bottom-Bound Label";
#endif
            return label;
        }

        private void UpdateBottomBoundCoordinateLabel(float minY)
        {
            _bottomBoundCoodinateLabel.Position.X = _bottomBoundCoodinateLabel.Diagram.Background.Position.Width / 2;
            _bottomBoundCoodinateLabel.Position.Y = _bottomBoundCoodinateLabel.Diagram.Background.Position.Height;
            _bottomBoundCoodinateLabel.Text = minY.ToString("0.###");
        }

        private Label CreateWaterMarkTitleLabel(Diagram diagram)
        {
            var label = new Label(diagram.Background)
            {
                ZIndex = -1,
                TextStyle = StyleHelper.CenterWaterMarkTextStyle
            };
            label.Position.Y = 0;
#if DEBUG
            label.Tag = "Title Label";
#endif
            return label;
        }

        private void UpdateWaterMarkTitleLabel(string text)
        {
            _waterMarkTitleLabel.Text = text;
            // 44 pixels magin from the left is to accomodate a reasonable amount of digits
            // in the left-bound coordinate label without overlapping the title label.
            _waterMarkTitleLabel.Position.X = _waterMarkTitleLabel.Diagram.Background.Position.PixelsToRelativeX(44);
            _waterMarkTitleLabel.Position.Width = _waterMarkTitleLabel.Diagram.Position.ScaledWidth - _waterMarkTitleLabel.Position.X;
            _waterMarkTitleLabel.Position.Height = _waterMarkTitleLabel.Diagram.Position.ScaledHeight;
        }

        private void CreateLines_WithRelatedElements(
            Point previousPoint,
            Point nextPoint,
            InterpolationTypeEnum interpolationTypeEnum,
            InterpolationTypeEnum nextInterpolationTypeEnum)
        {
            Diagram diagram = previousPoint.Diagram;
            Point destPoint;

            Node mockNode0 = _currentCurveInfo.NodeInfos
                                              .Where(nt => nt.NodeViewModel.ID == (int)previousPoint.Tag)
                                              .Select(nt => nt.MockNode)
                                              .Single();

            Node mockNode1 = _currentCurveInfo.NodeInfos
                                              .Where(nt => nt.NodeViewModel.ID == (int)nextPoint.Tag)
                                              .Select(nt => nt.MockNode)
                                              .Single();

            // ReSharper disable once UseObjectOrCollectionInitializer
            var destPoints = new List<Point>(_lineSegmentPointCount + 1);

            destPoints.Add(previousPoint);

            // Vertical line at the start: straight down to 0.
            bool mustCreateVerticalLineAtTheStart = interpolationTypeEnum == InterpolationTypeEnum.Undefined;
            if (mustCreateVerticalLineAtTheStart)
            {
                destPoint = new Point(parent: previousPoint)
                {
                    PointStyle = StyleHelper.PointStyleInvisible,
                    Tag = HELPER_ELEMENT_TAG
                };
                destPoint.Position.X = 0;
                destPoint.Position.Y = destPoint.Parent.Position.AbsoluteToRelativeY(0);
                destPoints.Add(destPoint);
            }

            double step = (mockNode1.X - mockNode0.X) / _lineSegmentCount;
            double x = mockNode0.X + step;

            for (var i = 0; i < _lineSegmentPointCount - 2; i++)
            {
                double y = _currentCurveCalculator.Calculate(x);

                // Vertical line in the middle, for stripe interpolation.
                bool mustCreateVerticalLineInTheMiddle = interpolationTypeEnum == InterpolationTypeEnum.Stripe ||
                                                         nextInterpolationTypeEnum == InterpolationTypeEnum.Stripe;
                if (mustCreateVerticalLineInTheMiddle)
                {
                    bool isHalfWay = i >= (_lineSegmentPointCount - 1) / 2;
                    if (isHalfWay)
                    {
                        destPoint = new Point(diagram.Background)
                        {
                            PointStyle = StyleHelper.PointStyleInvisible,
                            Tag = HELPER_ELEMENT_TAG
                        };
                        destPoint.Position.X = destPoint.Parent.Position.AbsoluteToRelativeX((float)(x - step));
                        destPoint.Position.Y = destPoint.Parent.Position.AbsoluteToRelativeY((float)y);
                        destPoints.Add(destPoint);
                    }
                }

                destPoint = new Point(diagram.Background)
                {
                    PointStyle = StyleHelper.PointStyleInvisible,
                    Tag = HELPER_ELEMENT_TAG
                };
                destPoint.Position.X = destPoint.Parent.Position.AbsoluteToRelativeX((float)x);
                destPoint.Position.Y = destPoint.Parent.Position.AbsoluteToRelativeY((float)y);
                destPoints.Add(destPoint);

                x += step;
            }

            // Vertical line at the end: point right under or above the next node.
            bool mustCreateVerticalLineAtTheEnd = (interpolationTypeEnum == InterpolationTypeEnum.Block ||
                                                   interpolationTypeEnum == InterpolationTypeEnum.Stripe ||
                                                   interpolationTypeEnum == InterpolationTypeEnum.Undefined) &&
                                                  nextInterpolationTypeEnum != InterpolationTypeEnum.Stripe;
            if (mustCreateVerticalLineAtTheEnd)
            {
                var extraPoint = new Point(parent: nextPoint)
                {
                    PointStyle = StyleHelper.PointStyleInvisible,
                    Tag = HELPER_ELEMENT_TAG
                };
                extraPoint.Position.X = 0;

                if (interpolationTypeEnum != InterpolationTypeEnum.Undefined)
                {
                    extraPoint.Position.Y = extraPoint.Parent.Position.AbsoluteToRelativeY(previousPoint.Position.AbsoluteY);
                }
                else
                {
                    extraPoint.Position.Y = extraPoint.Parent.Position.AbsoluteToRelativeY(0);
                }

                destPoints.Add(extraPoint);
            }

            destPoints.Add(nextPoint);

            for (int i = 0; i < destPoints.Count - 1; i++)
            {
                Point destPointA = destPoints[i];
                Point destPointB = destPoints[i + 1];

                // ReSharper disable once UnusedVariable
                var destLine = new Line(previousPoint)
                {
                    PointA = destPointA,
                    PointB = destPointB,
                    LineStyle = StyleHelper.LineStyleThick,
                    Tag = HELPER_ELEMENT_TAG
                };
            }
        }

        private CurveInfo CreateCurveInfo(IList<NodeViewModel> nodeViewModels)
        {
            (double X, double Y, InterpolationTypeEnum)[] nodeTuples =
                nodeViewModels.Select(x => (x.X, x.Y, (InterpolationTypeEnum)x.Interpolation.ID)).ToArray();

            Curve mockCurve = _curveFacade.Create(nodeTuples);

            IList<NodeInfo> nodeInfos = mockCurve.Nodes.Zip(
                 nodeViewModels,
                 (e, v) => new NodeInfo
                 {
                     MockNode = e,
                     NodeViewModel = v
                 }).ToArray();

            return new CurveInfo
            {
                MockCurve = mockCurve,
                NodeInfos = nodeInfos
            };
        }

        // Helpers

        private static int GetLineSegmentCount()
        {
            var config = CustomConfigurationManager.TryGetSection<ConfigurationSection>();
            if (config == null) return DEFAULT_LINE_SEGMENT_COUNT;
            return config.CurveLineSegmentCount;
        }

        private static float GetNodeClickableRegionSizeInPixels()
        {
            var config = CustomConfigurationManager.TryGetSection<ConfigurationSection>();
            if (config == null) return DEFAULT_CLICKABLE_REGION_SIZE_IN_PIXELS;
            return config.NodeClickableRegionSizeInPixels;
        }
    }
}