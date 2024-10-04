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

        /// <inheritdoc cref="CreateCurveDocs" />
        public static Curve CreateCurve(this CurveFactory curveFactory, params (double time, double value)[] nodeTuples)
            => curveFactory.CreateCurve((IList<(double x, double y)>)nodeTuples);

        /// <inheritdoc cref="CreateCurveDocs" />
        public static Curve CreateCurve([NotNull] this CurveFactory curveFactory, [NotNull] IList<(double time, double value)> nodeTuples)
        {
            if (curveFactory == null) throw new NullException(() => curveFactory);
            if (nodeTuples == null) throw new NullException(() => nodeTuples);
            var nodeInfos = nodeTuples.Select(x => new NodeInfo(x.Item1, x.Item2)).ToArray();
            Curve curve = curveFactory.CreateCurve(nodeInfos);
            return curve;
        }

        public static Curve CreateCurve([NotNull] this CurveFactory curveFactory, [NotNull] IList<string> lines)
            => curveFactory.CreateCurve(lines.ToArray());

        /*
        public static Curve CreateCurve([NotNull] this CurveFactory curveFactory, [NotNull] params string[] lines)
            => curveFactory.CreateCurve(0, 1, 0, 1, lines);*/

        /// <summary>
        /// Create a Curve from a list of strings, that 'ASCII-encode' the curve, creating a block with time on the horizontal axis and values on the vertical axis. A space character is just empty space, but that character is configurable. Any other character is seen as a data point.
        /// </summary>
        public static Curve CreateCurve([NotNull] this CurveFactory curveFactory, [NotNull] params string[] lines)
        {
            if (curveFactory == null) throw new NullException(() => curveFactory);
            if (lines == null) throw new NullException(() => lines);
            if (lines.Length == 0) throw new Exception($"{lines} collection empty.");
            if (lines.Any(x => x.Length == 0)) throw new Exception($"There are empty {lines}.");
            
            // Helper variables
            double startTime = 0.0;
            double totalTime = 1.0;
            double minValue = 0.0;
            double maxValue = 1.0;
            double timeSpan = totalTime - startTime;
            double valueSpan = maxValue - startTime;
            double characterSpan = lines.Max(x => x.Length) - 1;
            double lineSpan = lines.Length - 1;
            
            // Background character is the most used character
            char backgroundChar = lines.SelectMany(c => c)
                                       .GroupBy(c => c)
                                       .OrderByDescending(g => g.Count())
                                       .Select(g => g.Key)
                                       .FirstOrDefault();

            // Note that the values rise from top to bottom,
            // So reverse the lines
            
            var nodes = new List<NodeInfo>();
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                var line = lines[lineIndex];
                
                for (int charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    if (line[charIndex] == backgroundChar)
                    {
                        continue;
                    }

                    // Note that values rise from bottom to top, 
                    // which is the opposite of how text lines are usually numbered.
                    double reversedLineIndex = lineSpan - lineIndex; 

                    double time = startTime + charIndex / characterSpan * timeSpan;
                    double value = minValue + reversedLineIndex / lineSpan * valueSpan;
                    
                    nodes.Add(new NodeInfo(time, value));
                }
            }
            
            return curveFactory.CreateCurve(nodes.OrderBy(x => x.Time).ToArray());
        }
        
        /*
        // Create a Curve from a list of strings, that 'ASCII-encode' the curve, creating a block with time on the horizontal axis and values on the vertical axis. A space character is just empty space, but that character is configurable. Any other character is seen as a data point.    
        public static Curve CreateCurveFromUnicode(this CurveFactory curveFactory, IList<string> lines, char spaceChar)
        {
            // Note that the values rise from top to bottom, which is the opposite of how text lines are usually numbered.
            var linesReversed = lines.Reverse().ToList();

            // Note that the data point character is not monospace. Does a trick to find the center of the character, by counting the in-between space characters and using halves to represent intermediate data point characters, then scaling it to a given time span in seconds. Values enjoy a more equally spaced mapping due to lines being of equal height.
            throw new NotImplementedException();
        }
        */

        #region Docs

        /// <summary>
        /// Shorthand that takes tuples like (0, 0), (0.1, 0.2), (0.2, 1.0)
        /// and creates a curve from them.
        /// </summary>
        /// <param name="curveFactory">The factory used to create the <see cref="Curve"/>.</param>
        /// <param name="nodeTuples">
        /// A list of tuples representing the nodes,
        /// where each tuple contains the x and y coordinates of a node.
        /// </param>
        /// <returns>A <see cref="Curve"/> object populated with the specified nodes.</returns>
        [UsedImplicitly]
        private static Curve CreateCurveDocs(this CurveFactory curveFactory, object nodeTuples) 
            => throw new NotSupportedException();
        
        #endregion
    }
}