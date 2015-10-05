using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.VectorGraphics.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.VectorGraphics
{
    public class NodeViewModelsToDiagramConverter
    {
        private float _margin;

        public NodeViewModelsToDiagramConverter()
        {
            _margin = StyleHelper.PointStyle.Width / 2f;
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

            double widthWithinMargin = diagram.Background.Width - _margin - _margin;
            if (widthWithinMargin < 0)
            {
                widthWithinMargin = 0;
            }
            double heightWithinMargin = diagram.Background.Height - _margin - _margin;
            if (heightWithinMargin < 0)
            {
                heightWithinMargin = 0;
            }

            viewModels = viewModels.OrderBy(x => x.Time).ToArray();

            double minTime = viewModels.First().Time;
            double maxTime = viewModels.Last().Time;
            double timeDiff = maxTime - minTime;
            double xFactor = 1;
            if (timeDiff != 0)
            {
                xFactor = widthWithinMargin / timeDiff;
            }

            double minValue = viewModels.Select(x => x.Value).Union(0.0).Min();
            double maxValue = viewModels.Select(x => x.Value).Union(0.0).Max();
            double valueDiff = maxValue - minValue;
            double yFactor = 1;
            if (valueDiff != 0)
            {
                yFactor = heightWithinMargin / valueDiff;
            }

            Point previousPoint = null;

            foreach (NodeViewModel viewModel in viewModels)
            {
                double x = _margin + (viewModel.Time - minTime) * xFactor;
                double y = _margin + heightWithinMargin - (viewModel.Value - minValue) * yFactor;

                var point = new Point
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    X = (float)x,
                    Y = (float)y,
                    PointStyle = StyleHelper.PointStyle
                };

                if (previousPoint != null)
                {
                    var line = new Line
                    {
                        Diagram = diagram,
                        Parent = diagram.Background,
                        PointA = previousPoint,
                        PointB = point,
                        LineStyle = StyleHelper.LineStyle
                    };
                }

                previousPoint = point;
            }
        }
    }
}
