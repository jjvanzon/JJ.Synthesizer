using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes
{
    public static class CurveFactoryExtensions
    {
        public static Curve CreateCurve(this CurveFactory curveFactory, IList<NodeInfo> nodeInfos)
            => curveFactory.CreateCurve(nodeInfos.ToArray());

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public static Curve CreateCurve(this CurveFactory curveFactory, params (double time, double value)[] nodeTuples)
            => curveFactory.CreateCurve((IList<(double x, double y)>)nodeTuples);

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public static Curve CreateCurve(this CurveFactory curveFactory, IList<(double time, double value)> nodeTuples)
        {
            if (curveFactory == null) throw new ArgumentNullException(nameof(curveFactory));
            if (nodeTuples == null) throw new ArgumentNullException(nameof(nodeTuples));
            var nodeInfos = nodeTuples.Select(x => new NodeInfo(x.Item1, x.Item2)).ToArray();
            Curve curve = curveFactory.CreateCurve(nodeInfos);
            return curve;
        }

        // ASCII Curves

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        public static Curve CreateCurve(this CurveFactory curveFactory, IList<string> lines)
            => curveFactory.CreateCurve(0, 1, 0, 1, lines?.ToList());

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        public static Curve CreateCurve(this CurveFactory curveFactory, params string[] lines)
            => curveFactory.CreateCurve(0, 1, 0, 1, lines?.ToList());

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        public static Curve CreateCurve(
            this CurveFactory curveFactory,
            double start = 0, double end = 1, double min = 0, double max = 1,
            params string[] lines)
            => curveFactory.CreateCurve(start, end, min, max, lines?.ToList());

        /// <inheritdoc cref="docs._createcurvefromstrings" />
        public static Curve CreateCurve(
            this CurveFactory curveFactory,
            double start = 0, double end = 1, double min = 0, double max = 1,
            IList<string> lines = null)
        {
            if (curveFactory == null) throw new ArgumentNullException(nameof(curveFactory));
            if (lines == null) throw new ArgumentNullException(nameof(lines));
            if (lines.Count == 0) throw new Exception($"{lines} collection empty.");

            lines = TrimAsciiCurve(lines);

            // Helper variables
            double timeSpan = end - start;
            double valueSpan = max - min;
            double characterSpan = lines.Max(x => x.Length) - 1; // -1 to get the space between centers of characters.
            double lineSpan = lines.Count - 1; // -1 because it's the space between centers of characters.

            // Background character is the most used character
            char backgroundChar = lines.SelectMany(x => x)
                                       .GroupBy(x => x)
                                       .OrderByDescending(x => x.Count())
                                       .Select(x => x.Key)
                                       .FirstOrDefault();

            IList<NodeInfo> nodes = new List<NodeInfo>();
            for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
            {
                string line = lines[lineIndex];

                for (int charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    char chr = line[charIndex];
                    if (chr == backgroundChar) continue;

                    // Note that values rise from bottom to top, 
                    // which is the opposite of how text lines are usually numbered.
                    double reversedLineIndex = lineSpan - lineIndex;

                    // Scale character positions to desired value ranges
                    double time = start + charIndex / characterSpan * timeSpan;
                    double value = min + reversedLineIndex / lineSpan * valueSpan;

                    nodes.Add(new NodeInfo(time, value));
                }
            }

            // Sort by time
            nodes = nodes.OrderBy(x => x.Time).ToArray();
            
            return curveFactory.CreateCurve(nodes);
        }

        /// <summary>
        /// Prep Data: Split into unique lines and determine the window where there are characters.
        /// White space is trimmed off of the top, bottom, left and right,
        /// leaving only the block of characters that contains data.
        /// </summary>
        private static IList<string> TrimAsciiCurve(IList<string> lines)
        {
            var lines2 = lines;
            
            // Replace nulls
            lines2 = lines2.Select(x => x ?? "").ToList();
            
            // Split strings with enters in it.
            lines2 = lines2.SelectMany(x => x.Split(Environment.NewLine, StringSplitOptions.None)).ToList();
            
            // Trim off leading empty lines
            int lineFirstIndex = lines2.TakeWhile(string.IsNullOrWhiteSpace).Count(); // Index of first non-empty line
            lines2 = lines2.Skip(lineFirstIndex).ToList();
            
            // Trim off trailing empty lines
            int trailingEmptyLineCount = lines2.Reverse().TakeWhile(string.IsNullOrWhiteSpace).Count(); // Index of last non-empty line
            int lineLastIndex = lines2.Count - 1 - trailingEmptyLineCount;
            lines2 = lines2.Take(lineLastIndex + 1).ToList();
            
            // Make all the lines equally long
            int maxLineLength = lines2.Max(x => x.Length);
            lines2 = lines2.Select(x => x.PadRight(maxLineLength, ' ')).ToList();
            
            // Determine start of block of characters:
            // Find first non-empty character in any line
            int firstCharIndex = lines2.Min(x => x.TakeWhile(char.IsWhiteSpace).Count());
            
            // Trim off block of empty space at the beginning
            lines2 = lines2.Select(x => x.Substring(firstCharIndex)).ToList();
            
            // Trim off empty space at the end
            lines2 = lines2.Select(x => x.TrimEnd()).ToList();
            
            return lines2;
        }
    }
}