using System.Collections.Generic;
using JJ.Framework.Exceptions;
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
				}

				_lineDictionary.Add(diagram, line);
				return line;
			}
		}
	}
}