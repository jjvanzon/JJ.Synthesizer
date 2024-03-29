﻿using System.Collections.Generic;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Framework.VectorGraphics.Models.Styling;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Helpers
{
    internal static class LineGestureHelper
    {
        private static readonly Dictionary<Diagram, Line> _lineDictionary = new Dictionary<Diagram, Line>();
        private static readonly object _lock = new object();

        public static Line CreateLine(Diagram diagram, LineStyle lineStyle, int lineZIndex)
        {
            if (diagram == null) throw new NullException(() => diagram);
            lock (_lock)
            {
                if (_lineDictionary.TryGetValue(diagram, out Line line))
                {
                    return line;
                }

                line = new Line(diagram.Background)
                {
                    ZIndex = lineZIndex,
                    Visible = false,
                    Tag = "LineGesture Line",
                    PointA = new Point(diagram.Background)
                    {
                        Visible = false,
                        Tag = "LineGesture Point"
                    },
                    PointB = new Point(diagram.Background)
                    {
                        Visible = false,
                        Tag = "LineGesture Point"
                    }
                };

                if (lineStyle != null)
                {
                    line.LineStyle = lineStyle;
                }

                _lineDictionary.Add(diagram, line);
                return line;
            }
        }
    }
}