using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Wishes.Helpers;
using static System.Guid;
using static System.Linq.Enumerable;
using static System.Threading.Tasks.Task;
using static JJ.Business.Synthesizer.Wishes.Helpers.StringWishes;

// ReSharper disable ParameterHidesMember
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable ForCanBeConvertedToForeach

namespace JJ.Business.Synthesizer.Wishes
{
    // SynthWishes Parallelization

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(params Func<FluentOutlet>[] funcs)
            => ParallelAdd((IList<Func<FluentOutlet>>)funcs);

        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(
            IList<Func<FluentOutlet>> funcs, 
            string name = null,
            [CallerMemberName] string callerMemberName = null)
        {
            if (funcs == null) throw new ArgumentNullException(nameof(funcs));
            
            // If parallels disabled
            if (!GetParallelEnabled)
            { 
                // Return a normal Add of the Outlets returned by the funcs.
                return Add(funcs.Select(x => x()).ToArray());
            }
            else
            {
                name = FetchName(name, callerMemberName);

                var add = Add(funcs.Select(termFunc => termFunc()).ToArray());
                add.Name = $"{name}{ParallelAddTag} {NewGuid():N}";

                WithName(name);

                return add;
            }
        }
        
        private FluentOutlet ParallelAdd_MixedGraphBuildUpAndParallelism(
            IList<Func<FluentOutlet>> funcs, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            name = FetchName(name, callerMemberName);

            // Prep variables
            int termCount = funcs.Count;
            int channelCount = GetSpeakerSetup.GetChannelCount();
            string[] names = GetParallelNames(termCount, name);
            string[] displayNames = names.Select(GetDisplayName).ToArray();
            var cacheResults = new Result<SaveResultData>[termCount];
            var reloadedSamples = new FluentOutlet[termCount];

            var stopWatch = Stopwatch.StartNew();

            // Save to files
            Parallel.For(0, termCount, i =>
            {
                Console.WriteLine($"{PrettyTime()} Start Task: {displayNames[i]}", "SynthWishes");

                // Get outlets first
                var channelOutlets = new FluentOutlet[channelCount];

                var originalChannel = Channel;
                try
                {
                    for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
                    {
                        ChannelIndex = channelIndex;
                        channelOutlets[channelIndex] = funcs[i](); // This runs parallels, because the funcs can contain another parallel add.
                    }
                }
                finally
                {
                    Channel = originalChannel;
                }

                // Generate audio
                cacheResults[i] = Cache(channelOutlets, names[i]);

                Console.WriteLine($"{PrettyTime()} End Task: {displayNames[i]}", "SynthWishes");
            });

            // Moved this out of the parallel loop,
            // but feels strange to process less in parallel.

            // Reload Samples
            for (int i = 0; i < termCount; i++)
            {
                var cacheResult = cacheResults[i];

                // Play if needed
                if (MustPlayParallels) Play(cacheResult.Data);

                if (!MustCacheToDisk)
                {
                    // Read from bytes
                    reloadedSamples[i] = Sample(cacheResult.Data.Bytes).SetName(displayNames[i]);
                }
                else
                {
                    // Read from file
                    reloadedSamples[i] = Sample(names[i]).SetName(displayNames[i]);

                    // Diagnostic actions
                    {
                        // Save reloaded samples to disk.
                        var reloadedSampleRepeated = Repeat(reloadedSamples[i], channelCount).ToArray();
                        var saveResult2 = Save(reloadedSampleRepeated, names[i] + "_Reloaded.wav");

                        // Play to test the sample loading.
                        if (MustPlayParallels) Play(saveResult2.Data);
                    }
                }
            }


            stopWatch.Stop();

            // Report total real-time and complexity metrics.
            double audioDuration = cacheResults.Max(x => x.Data.AudioFileOutput.Duration);
            double calculationDuration = stopWatch.Elapsed.TotalSeconds;
            int complexity = cacheResults.Sum(x => x.Data.Complexity);
            string formattedMetrics = FormatMetrics(audioDuration, calculationDuration, complexity);
            string message = $"{PrettyTime()} Totals {name} Terms: {formattedMetrics}";
            Console.WriteLine(message);

            return Add(reloadedSamples);
        }

        public void RunParallelsRecursive(IList<FluentOutlet> channelOutlets)
        {
            var tasks = new Task[channelOutlets.Count];

            for (var i = 0; i < channelOutlets.Count; i++)
            {
                var channelOutlet = channelOutlets[i];
                var task = Run(() => RunParallelsRecursive(channelOutlet));
                tasks[i] = task;
            }

            WaitAll(tasks);
        }

        private void RunParallelsRecursive(FluentOutlet op)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));
            
            if (!GetParallelEnabled) return;

            var operands = op.Operands.ToArray();
            int operandCount = operands.Length;
            
            // Depth-first
            for (var i = 0; i < operandCount; i++)
            {
                var operand = operands[i];
                if (operand != null)
                {
                    RunParallelsRecursive(operand);
                }
            }

            // Some guard statements
            bool isParallel = IsParallel(op);
            if (!isParallel)
            {
                return;
            }

            if (!op.IsAdd && !op.IsAdder && !op.IsSample)
            {
                throw new Exception($"FluentOutlet marked as Parallel is not an {PropertyNames.Add}, {PropertyNames.Adder} or {nameof(Sample)} operator. It is: {op.Stringify(true, true)}");
            }

            RemoveParallelAddTag(op);

            Parallel.For(0, operandCount, i =>
            {
                var operand = operands[i];
                if (operand != null)
                {
                    string taskName = GetParallelTaskName(op.Name, i, operand.Name);
                    string taskDisplayName = GetDisplayName(taskName);

                    Console.WriteLine($"{PrettyTime()} Start Task: {taskDisplayName}", nameof(SynthWishes));

                    byte[] bytes = Cache(operand, taskName).Data.Bytes;
                    op.Operands[i] = Sample(bytes, name: taskDisplayName);

                    Console.WriteLine($"{PrettyTime()} End Task: {taskDisplayName}", nameof(SynthWishes));
                }
            });
        }

        // Helpers

        private string[] GetParallelNames(int count, string name)
        {
            string guidString = $"{NewGuid():N}";

            string sep = " ";
            if (string.IsNullOrEmpty(name)) sep = "";

            var fileNames = new string[count];
            for (int i = 0; i < count; i++)
            {
                fileNames[i] = $"{name}{sep}Term {i + 1} {nameof(ParallelAdd)} {guidString}.wav";
            }

            return fileNames;
        }

        private static string GetDisplayName(string fileName)
        {
            if (fileName == null) return null;
            return Path.GetFileNameWithoutExtension(fileName.WithShortGuids(4));
        }

        private const string ParallelAddTag = " 678976b885a04c79 Parallel Add 8882a57583e82813";

        private static bool IsParallel(FluentOutlet fluentOutlet) => fluentOutlet.Name != null && fluentOutlet.Name.Contains(ParallelAddTag);

        private static void RemoveParallelAddTag(FluentOutlet fluentOutlet)
        {
            fluentOutlet.Name = fluentOutlet.Name?.Replace(ParallelAddTag, " Parallel Add");
        }

        private string GetParallelTaskName(string addOperatorName, int termIndex, string operandName)
        {
            return $"{addOperatorName} (Term {termIndex + 1} - {operandName}).wav";
        }
    }
}
