using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Infos;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public static class CurveFactoryExtensions
    {
        public static Curve CreateCurve(this CurveFactory curveFactory, IList<NodeInfo> nodeInfos)
            => curveFactory.CreateCurve(nodeInfos.ToArray());

        /// <inheritdoc cref="CreateCurveWithTuplesDoc" />
        public static Curve CreateCurve(this CurveFactory curveFactory, params (double time, double value)[] nodeTuples)
            => curveFactory.CreateCurve((IList<(double x, double y)>)nodeTuples);

        /// <inheritdoc cref="CreateCurveWithTuplesDoc" />
        public static Curve CreateCurve([NotNull] this CurveFactory curveFactory, [NotNull] IList<(double time, double value)> nodeTuples)
        {
            if (curveFactory == null) throw new NullException(() => curveFactory);
            if (nodeTuples == null) throw new NullException(() => nodeTuples);
            var nodeInfos = nodeTuples.Select(x => new NodeInfo(x.Item1, x.Item2)).ToArray();
            Curve curve = curveFactory.CreateCurve(nodeInfos);
            return curve;
        }

        // ASCII Curves
        
        /// <inheritdoc cref="CreateCurveFromAsciiDoc" />
        public static Curve CreateCurve(this CurveFactory curveFactory, IList<string> lines)
            => curveFactory.CreateCurve(0, 1, 0, 1, lines?.ToList());

        /// <inheritdoc cref="CreateCurveFromAsciiDoc" />
        public static Curve CreateCurve(this CurveFactory curveFactory, params string[] lines)
            => curveFactory.CreateCurve(0, 1, 0, 1, lines?.ToList());

        /// <inheritdoc cref="CreateCurveFromAsciiDoc" />
        public static Curve CreateCurve(
            this CurveFactory curveFactory,
            double start = 0, double end = 1, double min = 0, double max = 1,
            params string[] lines)
            => curveFactory.CreateCurve(start, end, min, max, lines?.ToList());
        
        /// <inheritdoc cref="CreateCurveFromAsciiDoc" />
        public static Curve CreateCurve(
            this CurveFactory curveFactory,
            double start = 0, double end = 1, double min = 0, double max = 1,
            IList<string> lines = null)
        {
            if (curveFactory == null) throw new NullException(() => curveFactory);
            if (lines == null) throw new NullException(() => lines);
            if (lines.Count == 0) throw new Exception($"{lines} collection empty.");

            // Prep Data: Split into unique lines and determine the window where there are characters.
            
            var trimmedLines = lines;
            // Replace nulls
            trimmedLines = trimmedLines.Select(x => x ?? "").ToList();
            // Split strings with enters in it.
            trimmedLines = trimmedLines.SelectMany(x => x.Split(Environment.NewLine, StringSplitOptions.None)).ToList();
            // Trim off leading empty lines
            int lineFirstIndex = trimmedLines.TakeWhile(string.IsNullOrWhiteSpace).Count(); // Index of first non-empty line
            trimmedLines = trimmedLines.Skip(lineFirstIndex).ToList();
            // Trim off trailing empty lines
            int trailingEmptyLineCount = trimmedLines.Reverse().TakeWhile(string.IsNullOrWhiteSpace).Count(); // Index of last non-empty line
            int lineLastIndex = trimmedLines.Count - 1 - trailingEmptyLineCount;
            trimmedLines = trimmedLines.Take(lineLastIndex + 1).ToList();
            // Determine first non-empty character in any line
            int firstCharIndex = trimmedLines.Min(x => x.TakeWhile(char.IsWhiteSpace).Count());
            // Trim off block of empty space at the beginning
            trimmedLines = trimmedLines.Select(x => x.Substring(firstCharIndex)).ToList();
            // Determine last non-empty character in any line
            //int lastCharIndex = trimmedLines.Max(x => x.Reverse().TakeWhile(char.IsWhiteSpace).Count());
            // Trim off block of empty space at the end
            //trimmedLines = trimmedLines.Select(x => x.Substring(0, x.Length - (x.Length - lastCharIndex - 1))).ToList();
            //trimmedLines = trimmedLines.Select(x => x.FromTill(firstCharIndex, lastCharIndex)).ToList();
            trimmedLines = trimmedLines.Select(x => x.TrimEnd()).ToList();

            // Helper variables
            double timeSpan = end - start;
            double valueSpan = max - min;
            double characterSpan = trimmedLines.Max(x => x.Length) - 1; // -1 to get the space between centers of characters.
            double lineSpan = trimmedLines.Count - 1; // -1 because it's the space between centers of characters.
            //double charSpan = lastCharIndex - firstCharIndex; // The space between centers of characters.
            //double lineSpan = trimmedLines.Count - 1; // -1 because it's the space between centers of characters.

            // Background character is the most used character
            char backgroundChar = trimmedLines.SelectMany(x => x)
                                              .GroupBy(x => x)
                                              .OrderByDescending(x => x.Count())
                                              .Select(x => x.Key)
                                              .FirstOrDefault();

            IList<NodeInfo> nodes = new List<NodeInfo>();
            for (int lineIndex = 0; lineIndex < trimmedLines.Count; lineIndex++)
            {
                var line = trimmedLines[lineIndex];

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

        #region Docs

        /// <summary>
        /// Shorthand that takes tuples like (0, 0), (0.1, 0.2), (0.2, 1.0)
        /// and creates a curve from them.
        /// </summary>
        /// <param name="curveFactory"> The factory used to create the <see cref="Curve" />. </param>
        /// <param name="nodeTuples">
        /// A list of tuples representing the nodes,
        /// where each tuple contains the x and y coordinates of a node.
        /// </param>
        /// <returns> A <see cref="Curve" /> object populated with the specified nodes. </returns>
        [UsedImplicitly]
        private static Curve CreateCurveWithTuplesDoc(this CurveFactory curveFactory, object nodeTuples)
            => throw new NotSupportedException();


        /// <summary>
        /// Create a Curve from a list of strings, that 'ASCII-encode' the curve. Putting the strings under each other, they create
        /// a block of space with time on the horizontal axis and values on the vertical axis. The background character is usually
        /// just a space character, but any other background character is possible and automatically recognized. Any character
        /// other than the background character is seen as a data point. That way you can creatively choose your own characters.
        /// </summary>
        private static Curve CreateCurveFromAsciiDoc(this CurveFactory curveFactory,
            double start = 0, double end = 1, double min = 0, double max = 1,
            params string[] lines) => throw new NotSupportedException();

        #endregion
    }
}