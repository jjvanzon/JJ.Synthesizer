using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using static System.Environment;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Wishes
{
    internal static class LogWishes
    {
        // Pretty Calculation Graphs
        
        public static IList<string> GetSynthLog(Buff buff, double calculationDuration)
        {
            // Get Info
            var stringifiedChannels = new List<string>();

            foreach (var audioFileOutputChannel in buff.UnderlyingAudioFileOutput.AudioFileOutputChannels)
            {
                string stringify = audioFileOutputChannel.Outlet?.Stringify() ?? "";
                stringifiedChannels.Add(stringify);
            }

            // Gather Lines
            var lines = new List<string>();

            lines.Add("");
            lines.Add(GetPrettyTitle(FetchName(buff)));
            lines.Add("");

            string realTimeComplexityMessage = FormatMetrics(buff.UnderlyingAudioFileOutput.Duration, calculationDuration, buff.Complexity());
            lines.Add(realTimeComplexityMessage);
            lines.Add("");

            lines.Add($"Calculation time: {PrettyDuration(calculationDuration)}");
            lines.Add($"Audio length: {PrettyDuration(buff.UnderlyingAudioFileOutput.Duration)}");
            lines.Add($"Sampling rate: {buff.UnderlyingAudioFileOutput.SamplingRate} Hz " +
                      $"| {buff.UnderlyingAudioFileOutput.GetSampleDataTypeEnum()} " +
                      $"| {buff.UnderlyingAudioFileOutput.GetSpeakerSetupEnum()}");

            lines.Add("");

            IList<string> warnings = buff.Messages.ToArray();
            if (warnings.Any())
            {
                lines.Add("Warnings:");
                lines.AddRange(warnings.Select(warning => $"- {warning}"));
                lines.Add("");
            }

            for (var i = 0; i < buff.UnderlyingAudioFileOutput.AudioFileOutputChannels.Count; i++)
            {
                var channelString = stringifiedChannels[i];

                lines.Add($"Calculation Channel {i + 1}:");
                lines.Add("");
                lines.Add(channelString);
                lines.Add("");
            }

            if (buff.Bytes != null)
            {
                lines.Add($"{PrettyByteCount(buff.Bytes.Length)} written to memory.");
            }
            if (File.Exists(buff.FilePath)) // TODO: Remove the if. It may be redundant now.
            {
                lines.Add($"Output file: {Path.GetFullPath(buff.FilePath)}");
            }

            lines.Add("");

            return lines;
        }

        public static string FormatMetrics(double audioDuration, double calculationDuration, int complexity)
        {
            string realTimeMessage = FormatRealTimeMessage(audioDuration, calculationDuration);
            string sep = realTimeMessage != default ? " | " : "";
            string complexityMessage = $"Complexity Ｏ ( {complexity} )";
            string metricsMessage = $"{realTimeMessage}{sep}{complexityMessage}";
            return metricsMessage;
        }
        
        public static string FormatRealTimeMessage(double audioDuration, double calculationDuration)
        {
            //var isRunningInTooling = ToolingHelper.IsRunningInTooling;
            //if (isRunningInTooling)
            //{
            //    // If running in tooling, omitting the performance message from the result,
            //    // because it has little meaning with sampling rates  below 150
            //    // that are employed for tooling by default, to keep them running fast.
            //    return default;
            //}

            double realTimePercent = audioDuration / calculationDuration* 100;

            string realTimeStatusGlyph;
            if (realTimePercent < 100)
            {
                realTimeStatusGlyph = "❌";
            }
            else
            {
                realTimeStatusGlyph = "✔";
            }

            var realTimeMessage = $"{realTimeStatusGlyph} {realTimePercent:F0} % Real Time";

            return realTimeMessage;
        }

        // Tape Hierarchy
        
        public static string PlotTapeHierarchy(IList<Tape> tapes)
        {
            var sb = new StringBuilderWithIndentationWish("   ", NewLine);
            PlotTapeHierarchy(tapes, sb);
            return sb.ToString();
        }
        
        private static void PlotTapeHierarchy(IList<Tape> tapes, StringBuilderWithIndentationWish sb)
        {
            sb.AppendLine("Tape Hierarchy");
            sb.AppendLine("--------------");
            
            if (tapes == null)
            {
                sb.AppendLine("<Tapes=null>");
                sb.AppendLine();
                return;
            }
            
            if (tapes.Count == 0)
            {
                sb.AppendLine("<Tapes.Count=0>");
                sb.AppendLine();
                return;
            }
            
            IList<Tape> roots = tapes.Where(tape => tape?.ParentTape == null).ToArray();
            if (roots.Count == 0)
            {
                sb.AppendLine($"<{tapes.Count} tapes but no roots>");
                foreach (Tape tape in tapes)
                {
                    sb.AppendLine(tape == null ? "<Tape=null>" : tape.GetName);
                }
            }
            
            foreach(var root in roots)
            {
                PlotTapeHierarchyRecursive(root, sb);
            }
            
            sb.AppendLine();
        }
        
        private static void PlotTapeHierarchyRecursive(Tape tape, StringBuilderWithIndentationWish sb)
        {
            if (tape == null) 
            {
                sb.AppendLine("<Tape=null>");
                return;
            }
                
            if (tape.ChildTapes == null) 
            {
                sb.AppendLine("<Tape.ChildTapes=null)>");
                return;
            }
            
            string name = tape.GetName;
            if (string.IsNullOrWhiteSpace(name))
            {
                sb.AppendLine("<Tape.GetName=whitespace>");
            }
            else
            {
                sb.AppendLine(name);
            }
            
            foreach (Tape childTape in tape.ChildTapes)
            {
                sb.Indent();
                PlotTapeHierarchyRecursive(childTape, sb);
                sb.Outdent();
            }
        }

        // Optimization Loggers

        public static void LogComputeConstant(
            FlowNode a, string mathSymbol, FlowNode b, FlowNode result,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad("Compute const") + $" : {Stringify(opName, a, mathSymbol, b)} => {Stringify(result)}");
        
        public static void LogIdentityOperation(
            FlowNode a, string mathSymbol, FlowNode identityValue,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad("Identity op") + $" : {Stringify(opName, a, mathSymbol, identityValue)} => {Stringify(a)}");
        
        public static void LogIdentityOperation(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad($"Identity op ({dimension})") + $" : {Stringify(opName, signal, dimension, mathSymbol, transform)} => {Stringify(signal)}");
        
        public static void LogAlwaysOneOptimization(
            FlowNode a, string mathSymbol, FlowNode b,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad("Always 1") + $" : {Stringify(opName, a, mathSymbol, b)} => 1");
        
        public static void LogAlwaysOneOptimization(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad($"Always 1 ({dimension})") + " : " +
                                 $"{Stringify(opName, signal, dimension, mathSymbol, transform)} => " +
                                 $"{Stringify(opName, signal, dimension, "=", 1)}");
        
        public static void LogInvariance(
            FlowNode signal, string dimension, string mathSymbol, FlowNode transform,
            [CallerMemberName] string opName = null)
            => Console.WriteLine(Pad($"Invariance ({dimension})") + $" : {Stringify(opName, signal, dimension, mathSymbol, transform)} => {Stringify(signal)}");
        
        public static void LogDivisionByMultiplication(FlowNode a, FlowNode b, FlowNode result)
            => Console.WriteLine(Pad("Div => mul") + $" : {Stringify(a)} / {Stringify(b)} => {Stringify(result)}");
        
        public static void LogDistributeMultiplyOverAddition(FlowNode formulaBefore, FlowNode formulaAfter)
            => Console.WriteLine(Pad("Distribute * over +") + $" : {Stringify(formulaBefore)} => {Stringify(formulaAfter)}");
        
        public static void LogAdditionOptimizations(
            IList<FlowNode> terms, IList<FlowNode> flattenedTerms, IList<FlowNode> optimizedTerms,
            IList<FlowNode> consts, double constant, [CallerMemberName] string opName = null)
        {
            string symbol = "+";
            
            bool wasFlattened = terms.Count != flattenedTerms.Count;
            if (wasFlattened)
            {
                Console.WriteLine(Pad($"Flatten {symbol}") + $" : {Stringify(opName, symbol, terms)} => {Stringify(opName, symbol, flattenedTerms)}");
            }
            
            bool hasConst0 = consts.Count >= 1 && constant == 0;
            if (hasConst0)
            {
                Console.WriteLine(Pad("Eliminate 0") + $" : {Stringify(opName, symbol, terms)} => {Stringify(opName, symbol, optimizedTerms)}");
            }
            
            bool hasMultipleConsts = consts.Count > 1;
            if (hasMultipleConsts)
            {
                Console.WriteLine(Pad("Compute const") + $" : {Stringify(opName, symbol, flattenedTerms)} => {Stringify(opName, symbol, optimizedTerms)}");
            }
            
            bool noTermsLeft = terms.Count != 0 && optimizedTerms.Count == 0;
            if (noTermsLeft)
            {
                Console.WriteLine(Pad("0 terms remain") + $" : {Stringify(opName, symbol, terms)} => 0");
            }
            
            bool oneTermLeft = optimizedTerms.Count == 1;
            if (oneTermLeft)
            {
                Console.WriteLine(Pad($"Eliminate {symbol}") + $" : {Stringify(opName, symbol, flattenedTerms)} => {Stringify(symbol, optimizedTerms)}");
            }
        }
        
        public static void LogMultiplicationOptimizations(
            IList<FlowNode> factors, IList<FlowNode> optimizedFactors,
            IList<FlowNode> consts, double constant, [CallerMemberName] string opName = null)
        {
            string symbol = "*";
            
            bool hasConst1 = consts.Count >= 1 && constant == 1;
            if (hasConst1)
            {
                Console.WriteLine(Pad("Eliminate 1") + $" : {Stringify(opName, symbol, factors)} => {Stringify(opName, symbol, optimizedFactors)}");
            }
            
            bool hasMultipleConsts = consts.Count > 1;
            if (hasMultipleConsts)
            {
                Console.WriteLine(Pad("Compute const") + $" : {Stringify(opName, symbol, factors)} => {Stringify(opName, symbol, optimizedFactors)}");
            }
            
            bool noFactorsLeft = factors.Count != 0 && optimizedFactors.Count == 0;
            if (noFactorsLeft)
            {
                Console.WriteLine(Pad("0 factors remain") + $" : {Stringify(opName, symbol, factors)} => 1");
            }
            
            bool oneFactorLeft = optimizedFactors.Count == 1;
            if (oneFactorLeft)
            {
                Console.WriteLine(Pad($"Eliminate {symbol}") + $" : {Stringify(opName, symbol, optimizedFactors)} => {Stringify(symbol, optimizedFactors)}");
            }
        }
        
        // Specialized Stringifications
        
        internal static string Stringify(string opName, FlowNode a, string mathSymbol, FlowNode b)
            => Stringify(opName, mathSymbol, a, b);
        
        internal static string Stringify(string opName, string mathSymbol, params FlowNode[] operands)
            => Stringify(opName, mathSymbol, (IList<FlowNode>)operands);
        
        internal static string Stringify(string opName, string mathSymbol, IList<FlowNode> operands)
            => $"{opName}({Stringify(mathSymbol, operands)})";
        
        internal static string Stringify(string mathSymbol, IList<FlowNode> operands)
            => string.Join(" " + mathSymbol + " ", operands.Select(Stringify));
        
        internal static string Stringify(FlowNode operand)
            => operand.Stringify(true);
        
        internal static string Stringify(
            string opName, FlowNode signal, string dimension, string mathSymbol, FlowNode transform)
            => $"{opName}({Stringify(signal)}, {dimension} {mathSymbol} {Stringify(transform)})";
        
        internal static string Stringify(
            string opName, FlowNode signal, string dimension, string mathSymbol, double value)
            => $"{opName}({Stringify(signal)}, {dimension} {mathSymbol} {value})";
 
        private static string Pad(string text) => (text ?? "").PadRight(19);
    }
}
