using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Collections;

namespace JJ.Framework.Mathematics
{
    public static class TextPlotter
    {
        private const char BLACK_SQUARE_CHAR = (char)9632;
        private const char WHITE_SQUARE_CHAR = (char)9633;
        private const char EM_SPACE_CHARACTER = (char)8195;
        private const char EM_DASH_CHARACTER = '—';

        public static IList<string> Plot(
            IList<(double x, double y)> points,
            int columnCount,
            int lineCount,
            char plotChar = BLACK_SQUARE_CHAR,
            char backgroundChar = EM_SPACE_CHARACTER)
        {
            double minX = 0;
            double maxX = 0;
            double minY = 0;
            double maxY = 0;

            if (points.Count != 0)
            {
                minX = points.MinOrDefault(point => point.x);
                maxX = points.MaxOrDefault(point => point.x);
                minY = points.MinOrDefault(point => point.y);
                maxY = points.MaxOrDefault(point => point.y);
            }

            //double[] doublesOverColumns = MathHelper.SpreadDoubles(minX, maxX, columnCount);
            //double[] doublesOverLines = MathHelper.SpreadDoubles(minY, maxY, lineCount);

            var charArray = new char[lineCount][];

            for (var i = 0; i < lineCount; i++)
            {
                charArray[i] = Enumerable.Repeat(backgroundChar, columnCount).ToArray();
            }

            foreach ((double x, double y) in points)
            {
                double columnDouble = MathHelper.ScaleLinearly(x, minX, maxX, 0, columnCount - 1);
                double lineDouble = MathHelper.ScaleLinearly(y, minY, maxY, 0, lineCount - 1);

                var columnInteger = (int)Math.Round(columnDouble, MidpointRounding.AwayFromZero);
                var lineInteger = (int)Math.Round(lineDouble, MidpointRounding.AwayFromZero);

                charArray[lineInteger][columnInteger] = plotChar;
            }

            IList<string> lines = new List<string>();

            for (var i = 0; i < charArray.Length; i++)
            {
                var line = new string(charArray[i]);
                lines.Add(line);
            }

            return lines;
        }
    }
}