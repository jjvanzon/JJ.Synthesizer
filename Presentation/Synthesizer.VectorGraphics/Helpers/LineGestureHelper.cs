using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Presentation.VectorGraphics.Models.Elements;
using JJ.Framework.Presentation.VectorGraphics.Models.Styling;
using JJ.Framework.Exceptions;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Helpers
{
    internal static class LineGestureHelper
    {
        private static Dictionary<Diagram, Line> _lineDictionary = new Dictionary<Diagram, Line>();
        private static object _lock = new object();

        public static Line CreateLine(Diagram diagram, LineStyle lineStyle, int lineZIndex)
        {
            if (diagram == null) throw new NullException(() => diagram);
            lock (_lock)
            {
                Line line;
                if (_lineDictionary.TryGetValue(diagram, out line))
                {
                    return line;
                }

                line = new Line
                {
                    Diagram = diagram,
                    Parent = diagram.Background,
                    ZIndex = lineZIndex,
                    Visible = false,
                    Tag = "LineGesture Line",
                    PointA = new Point
                    {
                        Diagram = diagram,
                        Parent = diagram.Background,
                        Tag = "LineGesture Point"
                    },
                    PointB = new Point
                    {
                        Diagram = diagram,
                        Parent = diagram.Background,
                        Tag = "LineGesture Point"
                    }
                };

                if (lineStyle != null)
                {
                    line.LineStyle = lineStyle;
                };

                _lineDictionary.Add(diagram, line);
                return line;
            }
        }
    }
}