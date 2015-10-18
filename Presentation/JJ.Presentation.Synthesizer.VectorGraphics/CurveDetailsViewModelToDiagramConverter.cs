using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Gestures;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class CurveDetailsViewModelToDiagramConverter
    {
        private const int MINIMUM_NODE_COUNT = 2;
        private const float NODE_RECTANGLE_SIZE_IN_PIXELS = 20;

        private readonly int _doubleClickSpeedInMilliseconds;
        private readonly int _doubleClickDeltaInPixels;

        /// <param name="mustShowInvisibleElements">for debugging</param>
        public CurveDetailsViewModelToDiagramConverter(
            int doubleClickSpeedInMilliseconds,
            int doubleClickDeltaInPixels,
            bool mustShowInvisibleElements)
        {
            _doubleClickSpeedInMilliseconds = doubleClickSpeedInMilliseconds;
            _doubleClickDeltaInPixels = doubleClickDeltaInPixels;

            if (mustShowInvisibleElements)
            {
                StyleHelper.MakeHiddenStylesVisible();
            }
        }

        /// <param name="result">Pass an existing result to update an existing diagram.</param>
        public CurveDetailsViewModelToDiagramConverterResult Execute(
            CurveDetailsViewModel detailsViewModel,
            CurveDetailsViewModelToDiagramConverterResult result)
        {
            if (detailsViewModel == null) throw new NullException(() => detailsViewModel);
            if (detailsViewModel.Entity == null) throw new NullException(() => detailsViewModel.Entity);

            if (result == null)
            {
                // TODO: It looks like you may as well instantiate the things you pass to the constructor right inside the class.
                var showCurvePropertiesGesture = new ShowCurvePropertiesGesture(_doubleClickSpeedInMilliseconds, _doubleClickDeltaInPixels);
                result = new CurveDetailsViewModelToDiagramConverterResult(
                    new Diagram(), new KeyDownGesture(), new SelectNodeGesture(), new MoveGesture(), showCurvePropertiesGesture);

                result.Diagram.Gestures.Add(result.KeyDownGesture);
            }

            Diagram diagram = result.Diagram;
            // Clear Elements
            diagram.Elements.ForEach(x => x.Parent = null);
            diagram.Elements.Clear();
            // Clear Gestures
            diagram.Background.Gestures.Clear();
            diagram.Background.Gestures.Add(result.ShowCurvePropertiesGesture);

            if (detailsViewModel.Entity.Nodes.Count < MINIMUM_NODE_COUNT)
            {
                return result;
            }

            IList<NodeViewModel> sortedNodeViewModels = detailsViewModel.Entity.Nodes.OrderBy(x => x.Time).ToArray();
            float minTime = (float)sortedNodeViewModels.First().Time;
            float maxTime = (float)sortedNodeViewModels.Last().Time;
            float minValue = (float)sortedNodeViewModels.Select(x => x.Value).Min();
            float maxValue = (float)sortedNodeViewModels.Select(x => x.Value).Max();

            // Set Scaling
            diagram.ScaleModeEnum = ScaleModeEnum.ViewPort;
            diagram.ScaledX = minTime;
            diagram.ScaledWidth = maxTime - minTime;
            // NOTE: The direction of the y-axis is inverted.
            diagram.ScaledY = maxValue;
            diagram.ScaledHeight = minValue - maxValue;

            // Set Margin
            // (This is not full-proof, since margin is calculated based on the point's pixel width and scaling without margin,
            //  But then the scaling is changed based on the margin, making the point's scaled width a little off.
            //  The difference will probably be marginal, but it can get noticable when you make the diagram very small.)
            float marginInPixels = StyleHelper.PointStyleThick.Width / 2;
            float marginX = diagram.PixelsToWidth(marginInPixels);
            float marginY = diagram.PixelsToHeight(marginInPixels);
            diagram.ScaledX -= marginX;
            diagram.ScaledWidth += marginX * 2;
            diagram.ScaledY -= marginY;
            diagram.ScaledHeight += marginY * 2;

            // Misc Elements
            CreateXAxis(diagram);
            CreateYAxis(diagram);
            CreateLeftBoundLabel(diagram, minTime);
            CreateRightBoundLabel(diagram, maxTime);
            CreateTopBoundLabel(diagram, maxValue);
            CreateBottomBoundLabel(diagram, minValue);

            // Points, Lines and Clickable Regions
            float scaledNodeRectangleWidth = diagram.PixelsToWidth(NODE_RECTANGLE_SIZE_IN_PIXELS);
            float scaledNodeRectangleHeight = diagram.PixelsToHeight(NODE_RECTANGLE_SIZE_IN_PIXELS);
            float scaledNodeRectangleWidthOver2 = scaledNodeRectangleWidth / 2;
            float scaledNodeRectangleHeightOver2 = scaledNodeRectangleHeight / 2;

            Point previousPoint = null;
            NodeViewModel previousNodeViewModel = null;

            foreach (NodeViewModel nodeViewModel in sortedNodeViewModels)
            {
                // Coordinates are always relative. (Lowest time translates to x = 0, relative to the background.)
                float x = diagram.Background.AbsoluteToRelativeX((float)nodeViewModel.Time);
                float y = diagram.Background.AbsoluteToRelativeY((float)nodeViewModel.Value);

                var rectangle = new Rectangle()
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    X = x - scaledNodeRectangleWidthOver2,
                    Y = y - scaledNodeRectangleHeightOver2,
                    Width = scaledNodeRectangleWidth,
                    Height = scaledNodeRectangleHeight,
                    LineStyle = StyleHelper.BorderStyleInvisible,
                    BackStyle = StyleHelper.BackStyleInvisible,
                    Tag = nodeViewModel.ID
                };
                rectangle.Gestures.Add(result.MoveNodeGesture, result.SelectNodeGesture);

                var point = new Point
                {
                    Diagram = diagram,
                    Parent = rectangle,
                    X = scaledNodeRectangleWidthOver2,
                    Y = scaledNodeRectangleHeightOver2,
                    PointStyle = StyleHelper.PointStyleThick,
                    ZIndex = 1
                };

                // TODO: Low priority: If NodeViewModel had a property IsSelected,
                // you would not have had to pass the CurveDetailsViewModel to this converter,
                // but only an IList<NodeViewModel>.
                if (nodeViewModel.ID == detailsViewModel.SelectedNodeID)
                {
                    point.PointStyle = StyleHelper.PointStyleThickSelected;
                }

                if (previousPoint != null)
                {
                    NodeTypeEnum nodeTypeEnum = (NodeTypeEnum)previousNodeViewModel.NodeType.ID;
                    TryCreateLine(diagram, previousPoint, point, nodeTypeEnum);
                }

                previousPoint = point;
                previousNodeViewModel = nodeViewModel;
            }

            return result;
        }

        /// <summary> Does not create a line in case of NodeTypeEnum.Off. </summary>
        private static Line TryCreateLine(Diagram diagram, Point previousPoint, Point nextPoint, NodeTypeEnum nodeTypeEnum)
        {
            switch (nodeTypeEnum)
            {
                case NodeTypeEnum.Line:
                    {
                        var line = new Line
                        {
                            Diagram = diagram,
                            Parent = diagram.Background,
                            PointA = previousPoint,
                            PointB = nextPoint,
                            LineStyle = StyleHelper.LineStyleThick
                        };
                        return line;
                    }

                case NodeTypeEnum.Block:
                    {
                        var line = new Line
                        {
                            Diagram = diagram,
                            Parent = diagram.Background,
                            PointA = previousPoint,
                            // Create a point that creates a horizontal line up to the next node.
                            PointB = new Point
                            {
                                Diagram = diagram,
                                Parent = diagram.Background,
                                X = nextPoint.X,
                                Y = previousPoint.Y,
                                PointStyle = StyleHelper.PointStyleInvisible
                            },
                            LineStyle = StyleHelper.LineStyleThick
                        };
                        return line;
                    }

                case NodeTypeEnum.Off:
                    return null;

                default:
                    throw new InvalidValueException(nodeTypeEnum);
            }
        }

        private static Line CreateXAxis(Diagram diagram)
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
                    Y = diagram.Background.AbsoluteToRelativeY(0),
                    PointStyle = StyleHelper.PointStyleInvisible
                },
                PointB = new Point
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    X = diagram.Background.Width,
                    Y = diagram.Background.AbsoluteToRelativeY(0),
                    PointStyle = StyleHelper.PointStyleInvisible
                }
            };

            return line;
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
                    X = diagram.Background.AbsoluteToRelativeX(0),
                    Y = 0,
                    PointStyle = StyleHelper.PointStyleInvisible
                },
                PointB = new Point
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    X = diagram.Background.AbsoluteToRelativeX(0),
                    Y = diagram.Background.Height,
                    PointStyle = StyleHelper.PointStyleInvisible
                }
            };
            return line;
        }

        private static Label CreateLeftBoundLabel(Diagram diagram, float minTime)
        {
            var label = new Label
            {
                Diagram = diagram,
                Parent = diagram.Background,
                X = 0,
                Y = diagram.Background.Height / 2,
                Text = minTime.ToString("0.###"),
            };

            label.TextStyle = StyleHelper.CreateTextStyleSmallerTransparent();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Center;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left;

            return label;
        }

        private static Label CreateRightBoundLabel(Diagram diagram, float maxTime)
        {
            var label = new Label
            {
                Diagram = diagram,
                Parent = diagram.Background,
                X = diagram.Background.Width,
                Y = diagram.Background.Height / 2,
                Text = maxTime.ToString("0.###"),
            };

            label.TextStyle = StyleHelper.CreateTextStyleSmallerTransparent();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Center;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Right;

            return label;
        }

        private static Label CreateTopBoundLabel(Diagram diagram, float maxValue)
        {
            var label = new Label
            {
                Diagram = diagram,
                Parent = diagram.Background,
                X = diagram.Background.Width / 2,
                Y = 0,
                Text = maxValue.ToString("0.###"),
            };

            label.TextStyle = StyleHelper.CreateTextStyleSmallerTransparent();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Top;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center;

            return label;
        }

        private static Label CreateBottomBoundLabel(Diagram diagram, float minValue)
        {
            var label = new Label
            {
                Diagram = diagram,
                Parent = diagram.Background,
                X = diagram.Background.Width / 2,
                Y = diagram.Background.Height,
                Text = minValue.ToString("0.###"),
            };

            label.TextStyle = StyleHelper.CreateTextStyleSmallerTransparent();
            label.TextStyle.VerticalAlignmentEnum = VerticalAlignmentEnum.Bottom;
            label.TextStyle.HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center;

            return label;
        }

    }
}
