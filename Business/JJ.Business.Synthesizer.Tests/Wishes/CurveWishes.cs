using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Infos;
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

        /// <inheritdoc cref="CreateCurveFromAsciiDoc" />
        public static Curve CreateCurve(this CurveFactory curveFactory, IList<string> lines)
            => curveFactory.CreateCurve(lines.ToArray());

        /// <inheritdoc cref="CreateCurveFromAsciiDoc" />
        public static Curve CreateCurve(this CurveFactory curveFactory, params string[] lines)
            => curveFactory.CreateCurve(0, 1, 0, 1, lines);

        /// <inheritdoc cref="CreateCurveFromAsciiDoc" />
        public static Curve CreateCurve(
            this CurveFactory curveFactory,
            double start = 0, double end = 1, double min = 0, double max = 1,
            params string[] lines)
        {
            if (curveFactory == null) throw new NullException(() => curveFactory);
            if (lines == null) throw new NullException(() => lines);
            if (lines.Length == 0) throw new Exception($"{lines} collection empty.");
            if (lines.Any(x => x.Length == 0)) throw new Exception($"There are empty {lines}.");

            // Helper variables
            double timeSpan = end - start;
            double valueSpan = max - min;
            double characterSpan = lines.Max(x => x.Length) - 1;
            double lineSpan = lines.Length - 1;

            // Background character is the most used character
            char backgroundChar = lines.SelectMany(x => x)
                                       .GroupBy(x => x)
                                       .OrderByDescending(x => x.Count())
                                       .Select(x => x.Key)
                                       .FirstOrDefault();

            var nodes = new List<NodeInfo>();
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                var line = lines[lineIndex];

                for (int charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    if (line[charIndex] == backgroundChar) continue;

                    // Note that values rise from bottom to top, 
                    // which is the opposite of how text lines are usually numbered.
                    double reversedLineIndex = lineSpan - lineIndex;

                    // Scale character positions to desired value ranges
                    double time = start + charIndex / characterSpan * timeSpan;
                    double value = min + reversedLineIndex / lineSpan * valueSpan;

                    nodes.Add(new NodeInfo(time, value));
                }
            }

            return curveFactory.CreateCurve(nodes.OrderBy(x => x.Time).ToArray());
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