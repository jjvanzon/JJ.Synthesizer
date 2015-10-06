using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Presentation.VectorGraphics.Gestures;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class NodeViewModelsToDiagramConverter
    {
        private float _margin = StyleHelper.PointStyleThicker.Width / 2f;
        private const float _regionSize = 20;
        private const float _regionSizeOver2 = _regionSize / 2;

        /// <param name="mustShowInvisibleElements">for debugging</param>
        public NodeViewModelsToDiagramConverter(bool mustShowInvisibleElements)
        {
            if (mustShowInvisibleElements)
            {
                StyleHelper.MakeHiddenStylesVisible();
            }
        }

        public void Execute(IList<NodeViewModel> viewModels, Diagram diagram)
        {
            if (viewModels == null) throw new NullException(() => viewModels);
            if (diagram == null) throw new NullException(() => diagram);

            diagram.Elements.ForEach(x => x.Parent = null);
            diagram.Elements.Clear();

            if (viewModels.Count < 2)
            {
                return;
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

            viewModels = viewModels.OrderBy(x => x.Time).ToArray();

            float minTime = (float)viewModels.First().Time;
            float maxTime = (float)viewModels.Last().Time;
            float timeDiff = maxTime - minTime;
            float xFactor = 1;
            if (timeDiff != 0)
            {
                xFactor = widthWithinMargin / timeDiff;
            }

            float minValue = (float)viewModels.Select(x => x.Value).Union(0f).Min();
            float maxValue = (float)viewModels.Select(x => x.Value).Union(0f).Max();
            float valueDiff = maxValue - minValue;
            float yFactor = 1;
            if (valueDiff != 0)
            {
                yFactor = heightWithinMargin / valueDiff;
            }

            var moveGesture = new MoveGesture();

            Point previousPoint = null;

            foreach (NodeViewModel viewModel in viewModels)
            {
                float x = _margin + ((float)viewModel.Time - minTime) * xFactor;
                float y = _margin + heightWithinMargin - ((float)viewModel.Value - minValue) * yFactor;

                var rectangle = new Rectangle()
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    X = x - _regionSizeOver2,
                    Y = y - _regionSizeOver2,
                    Width = _regionSize,
                    Height = _regionSize,
                    LineStyle = StyleHelper.BorderStyleInvisible,
                    BackStyle = StyleHelper.BackStyleInvisible
                };
                rectangle.Gestures.Add(moveGesture);

                var point = new Point
                {
                    Diagram = diagram,
                    Parent = rectangle,
                    X = _regionSizeOver2,
                    Y = _regionSizeOver2,
                    PointStyle = StyleHelper.PointStyleThicker
                };

                if (previousPoint != null)
                {
                    var line = new Line
                    {
                        Diagram = diagram,
                        Parent = diagram.Background,
                        PointA = previousPoint,
                        PointB = point,
                        LineStyle = StyleHelper.LineStyleThicker
                    };
                }

                previousPoint = point;
            }
        }
    }
}
