using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private const float NODE_RECTANGLE_SIZE_ABSOLUTE = 20;

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

            diagram.Elements.ForEach(x => x.Parent = null);
            diagram.Elements.Clear();

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

            float minValue = (float)sortedNodeViewModels.Select(x => x.Value).Union(0f).Min();
            float maxValue = (float)sortedNodeViewModels.Select(x => x.Value).Union(0f).Max();

            float valueDiff;
            // Temporarily DO NOT invert the direction of the Y axis.
            valueDiff = maxValue - minValue;
            diagram.ScaledY = minValue;
            diagram.ScaledHeight = valueDiff;
            // Do invert the direction of the Y axis.
            //valueDiff = minValue - maxValue;
            //diagram.ScaleY = maxValue;
            //diagram.ScaleHeight = valueDiff;

            // TODO: Use the helper methods in Diagram to scale the coordinates.
            float scaledNodeRectangleWidth;
            float scaledNodeRectangleHeight;
            ToScaledCoordinates(
                diagram, 
                NODE_RECTANGLE_SIZE_ABSOLUTE, NODE_RECTANGLE_SIZE_ABSOLUTE, 
                out scaledNodeRectangleWidth, out scaledNodeRectangleHeight);

            float scaledNodeRectangleWidthOver2 = scaledNodeRectangleWidth / 2;
            float scaledNodeRectangleHeightOver2 = scaledNodeRectangleHeight / 2;

            Point previousPoint = null;

            foreach (NodeViewModel nodeViewModel in sortedNodeViewModels)
            {
                // TODO: Coordinates are always relative to the BackGround!
                // That means we can STILL not map the values one to one to the Vector Graphics coordinates.
                float x = (float)nodeViewModel.Time - minTime;
                float y = (float)nodeViewModel.Value - minValue;

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

        private void ToScaledCoordinates(Diagram diagram, float absoluteX, float absoluteY, out float scaledX, out float scaledY)
        {
            switch (diagram.ScaleModeEnum)
            {
                case ScaleModeEnum.None:
                    scaledX = absoluteX;
                    scaledY = absoluteY;
                    break;

                case ScaleModeEnum.ViewPort:
                    scaledX = absoluteX / diagram.WidthInPixels * diagram.ScaledWidth;
                    scaledY = absoluteY / diagram.HeightInPixels * diagram.ScaledHeight;
                    break;

                default:
                    throw new ValueNotSupportedException(diagram.ScaleModeEnum);
            }
        }
    }
}
