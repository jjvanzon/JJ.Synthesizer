using System;
using System.Collections.Generic;
using System.Linq;
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
            diagram.ScaleModeEnum = ScaleModeEnum.ViewPort;
            // Clear Elements
            diagram.Elements.ForEach(x => x.Parent = null);
            diagram.Elements.Clear();
            // Clear Gestures
            diagram.Background.Gestures.Clear();
            diagram.Background.Gestures.Add(result.ShowCurvePropertiesGesture);

            if (detailsViewModel.Entity.Nodes.Count < 2)
            {
                return result;
            }

            IList<NodeViewModel> sortedNodeViewModels = detailsViewModel.Entity.Nodes.OrderBy(x => x.Time).ToArray();

            float minTime = (float)sortedNodeViewModels.First().Time;
            float maxTime = (float)sortedNodeViewModels.Last().Time;
            float timeDiff = maxTime - minTime;
            diagram.ScaledX = minTime;
            diagram.ScaledWidth = timeDiff;

            float minValue = (float)sortedNodeViewModels.Select(x => x.Value).Min();
            float maxValue = (float)sortedNodeViewModels.Select(x => x.Value).Max();
            float valueDiff;
            valueDiff = minValue - maxValue; // NOTE: The direction of the y-axis is inverted.
            diagram.ScaledY = maxValue;
            diagram.ScaledHeight = valueDiff;

            // Axes
            CreateXAxis(diagram);
            CreateYAxis(diagram);

            // Points, Lines and Clickable Regions
            float scaledNodeRectangleWidth = diagram.PixelsToWidth(NODE_RECTANGLE_SIZE_IN_PIXELS);
            float scaledNodeRectangleHeight = diagram.PixelsToHeight(NODE_RECTANGLE_SIZE_IN_PIXELS);
            float scaledNodeRectangleWidthOver2 = scaledNodeRectangleWidth / 2;
            float scaledNodeRectangleHeightOver2 = scaledNodeRectangleHeight / 2;

            Point previousPoint = null;

            foreach (NodeViewModel nodeViewModel in sortedNodeViewModels)
            {
                // Coordinates are always relative to the Background (lowest time translates to x = 0, relative to the background)
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
                    var line = new Line
                    {
                        Diagram = diagram,
                        Parent = diagram.Background,
                        PointA = previousPoint,
                        PointB = point,
                        LineStyle = StyleHelper.LineStyleThick
                    };
                }

                previousPoint = point;
            }

            return result;
        }

        private static void CreateXAxis(Diagram diagram)
        {
            var xAxis = new Line
            {
                Diagram = diagram,
                Parent = diagram.Background,
                LineStyle = StyleHelper.LineStyleDashed,
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
        }

        private static void CreateYAxis(Diagram diagram)
        {
            var xAxis = new Line
            {
                Diagram = diagram,
                Parent = diagram.Background,
                LineStyle = StyleHelper.LineStyleDashed,
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
        }
    }
}
