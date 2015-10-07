using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JJ.Framework.Common;
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
        private float _margin = StyleHelper.PointStyleThick.Width / 2f;
        private const float _regionSize = 20;
        private const float _regionSizeOver2 = _regionSize / 2;

        /// <param name="mustShowInvisibleElements">for debugging</param>
        public CurveDetailsViewModelToDiagramConverter(bool mustShowInvisibleElements)
        {
            if (mustShowInvisibleElements)
            {
                StyleHelper.MakeHiddenStylesVisible();
            }
        }

        /// <param name="result">Pass an existing result to update an existing diagram.</param>
        public NodeViewModelsToDiagramConverterResult Execute(
            CurveDetailsViewModel detailsViewModel,
            NodeViewModelsToDiagramConverterResult result)
        {
            if (detailsViewModel == null) throw new NullException(() => detailsViewModel);
            if (detailsViewModel.Entity == null) throw new NullException(() => detailsViewModel.Entity);

            if (result == null)
            {
                result = new NodeViewModelsToDiagramConverterResult(new Diagram(), new KeyDownGesture(), new SelectNodeGesture());
                result.Diagram.Gestures.Add(result.KeyDownGesture);
            }

            Diagram diagram = result.Diagram;
            diagram.Elements.ForEach(x => x.Parent = null);
            diagram.Elements.Clear();

            if (detailsViewModel.Entity.Nodes.Count < 2)
            {
                return result;
            }

            // Calculate Scaling and Margin
            float widthWithinMargin = diagram.Background.Width - _margin - _margin;
            if (widthWithinMargin < 0)
            {
                widthWithinMargin = 0;
            }
            float heightWithinMargin = diagram.Background.Height - _margin - _margin;
            if (heightWithinMargin < 0)
            {
                heightWithinMargin = 0;
            }

            IList<NodeViewModel> sortedNodeViewModels = detailsViewModel.Entity.Nodes.OrderBy(x => x.Time).ToArray();

            float minTime = (float)sortedNodeViewModels.First().Time;
            float maxTime = (float)sortedNodeViewModels.Last().Time;
            float timeDiff = maxTime - minTime;
            float xFactor = 1;
            if (timeDiff != 0)
            {
                xFactor = widthWithinMargin / timeDiff;
            }

            float minValue = (float)sortedNodeViewModels.Select(x => x.Value).Union(0f).Min();
            float maxValue = (float)sortedNodeViewModels.Select(x => x.Value).Union(0f).Max();
            float valueDiff = maxValue - minValue;
            float yFactor = 1;
            if (valueDiff != 0)
            {
                yFactor = heightWithinMargin / valueDiff;
            }

            var moveGesture = new MoveGesture();

            Point previousPoint = null;

            foreach (NodeViewModel nodeViewModel in sortedNodeViewModels)
            {
                float x = _margin + ((float)nodeViewModel.Time - minTime) * xFactor;
                float y = _margin + heightWithinMargin - ((float)nodeViewModel.Value - minValue) * yFactor;

                var rectangle = new Rectangle()
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    X = x - _regionSizeOver2,
                    Y = y - _regionSizeOver2,
                    Width = _regionSize,
                    Height = _regionSize,
                    LineStyle = StyleHelper.BorderStyleInvisible,
                    BackStyle = StyleHelper.BackStyleInvisible,
                    Tag = nodeViewModel.ID
                };
                rectangle.Gestures.Add(moveGesture, result.SelectNodeGesture);

                var point = new Point
                {
                    Diagram = diagram,
                    Parent = rectangle,
                    X = _regionSizeOver2,
                    Y = _regionSizeOver2,
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
    }
}
