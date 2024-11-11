using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Infos;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
// ReSharper disable ParameterHidesMember

namespace JJ.Business.Synthesizer.Wishes
{
    // Curve Creation Extensions

    public static class CurveFactoryExtensionWishes
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
    }

    // Curve Creation SynthWishes
    
    public partial class SynthWishes
    {
        // Overloads with NodeInfo

        /// <inheritdoc cref="docs._createcurve" />
        public FluentOutlet Curve(IEnumerable<NodeInfo> nodeInfos, [CallerMemberName] string callerMemberName = null)
        {
            return Curve(nodeInfos.ToArray(), callerMemberName);
        }

        /// <inheritdoc cref="docs._createcurve" />
        public FluentOutlet Curve(IList<NodeInfo> nodeInfos, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName);
            var curve = _[_operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeInfos))];
            AssignNames(curve, name);
            return curve;
        }

        /// <inheritdoc cref="docs._createcurve" />
        public FluentOutlet Curve(params NodeInfo[] nodeInfos)
        {
            string name = FetchName();
            var curve = _[_operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeInfos))];
            AssignNames(curve, name);
            return curve;
        }

        // Overloads with doubles

        /// <inheritdoc cref="docs._createcurve" />
        public FluentOutlet Curve(params double?[] values)
        {
            string name = FetchName();
            var curve = _[_operatorFactory.CurveIn(_curveFactory.CreateCurve(timeSpan: 1, values))];
            AssignNames(curve, name);
            return curve;
        }

        // Overload with Tuples

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public FluentOutlet Curve(
            IList<(double time, double value)> nodeTuples, 
            [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName);
            var curve = _[_operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeTuples))];
            AssignNames(curve, name);
            return curve;
        }

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public FluentOutlet Curve(params (double time, double value)[] nodeTuples)
        {
            string name = FetchName();
            var curve = _[_operatorFactory.CurveIn(_curveFactory.CreateCurve(nodeTuples))];
            AssignNames(curve, name);
            return curve;
        }

        // Helpers
        
        private void AssignNames(FluentOutlet fluentOutlet, string name)
        {
            fluentOutlet.UnderlyingCurve().Name = name;
            fluentOutlet.Operator.Name = name;
        }
    }
    
    // ASCII Curves Extensions
    
    /// <inheritdoc cref="docs._createcurvefromstring" />
    public static class AsciiCurveExtensionWishes
    { 
        /// <inheritdoc cref="docs._createcurvefromstring" />
        public static Curve CreateCurve(this CurveFactory curveFactory, string text)
            => curveFactory.CreateCurve(0, 1, 0, 1, text);

        /// <inheritdoc cref="docs._createcurvefromstring" />
        public static Curve CreateCurve(
            this CurveFactory curveFactory,
            double start = 0, double end = 1, double min = 0, double max = 1, string text = null)
        {
            if (curveFactory == null) throw new ArgumentNullException(nameof(curveFactory));
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException(nameof(text));

            var lines = TrimAsciiCurve(text);

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

        /// <inheritdoc cref="docs._trimasciicurves" />
        private static IList<string> TrimAsciiCurve(string text) => TrimAsciiCurve(new [] { text });

        /// <inheritdoc cref="docs._trimasciicurves" />
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
    
    // ASCII Curves SynthWishes

    public partial class SynthWishes
    { 
        /// <inheritdoc cref="docs._createcurvefromstring" />
        public FluentOutlet Curve(string text, [CallerMemberName] string callerMemberName = null)
        {
            //string name = FetchName(callerMemberName, GetCallerNameFromStack());
            string name = FetchName(callerMemberName);
            var curve = _[_operatorFactory.CurveIn(_curveFactory.CreateCurve(text))];
            AssignNames(curve, name);
            return curve;
        }

        // Overload with String and Ranges

        /// <inheritdoc cref="docs._createcurvefromstring" />
        public FluentOutlet Curve(
            (double start, double end) x,
            (double min, double max) y,
            string text, [CallerMemberName] string callerMemberName = null)
        {
            //string name = FetchName(callerMemberName, GetCallerNameFromStack());
            string name = FetchName(callerMemberName);
            var curve = _[_operatorFactory.CurveIn(_curveFactory.CreateCurve(x.start, x.end, y.min, y.max, text))];
            AssignNames(curve, name);
            return curve;
        }
    }

    // Curves in FluentOutlet

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="docs._curvewithoperator"/>
        public FluentOutlet Curve(FluentOutlet curve)
            => _wrappedOutlet * curve;

        public FluentOutlet Curve(IList<NodeInfo> nodeInfos, [CallerMemberName] string callerMemberName = null)
            => _wrappedOutlet * _synthWishes.Curve(nodeInfos, callerMemberName);

        public FluentOutlet Curve(params NodeInfo[] nodeInfos)
            => _wrappedOutlet * _synthWishes.Curve(nodeInfos);

        /// <inheritdoc cref="docs._createcurve" />
        public FluentOutlet Curve(string name, params double?[] values)
            => _wrappedOutlet * _synthWishes.Curve(values);

        /// <inheritdoc cref="docs._createcurve" />
        public FluentOutlet Curve(params double?[] values)
            => _wrappedOutlet * _synthWishes.Curve(values);

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public FluentOutlet Curve(
            IList<(double time, double value)> nodeTuples, [CallerMemberName] string callerMemberName = null)
            => _wrappedOutlet * _synthWishes.Curve(nodeTuples, callerMemberName);

        /// <inheritdoc cref="docs._createcurvewithtuples" />
        public FluentOutlet Curve(params (double time, double value)[] nodeTuples)
            => _wrappedOutlet * _synthWishes.Curve(nodeTuples);

        /// <inheritdoc cref="docs._createcurvefromstring" />
        public FluentOutlet Curve(string text, [CallerMemberName] string callerMemberName = null)
            => _wrappedOutlet * _synthWishes.Curve(text, callerMemberName);

        /// <inheritdoc cref="docs._createcurvefromstring" />
        public FluentOutlet Curve(
            (double start, double end) x,
            (double min, double max) y,
            string text, [CallerMemberName] string callerMemberName = null)
            => _wrappedOutlet * this._synthWishes.Curve(x, y, text, callerMemberName);
    }
}