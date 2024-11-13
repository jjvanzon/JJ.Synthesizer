using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static System.Guid;
using static System.Linq.Enumerable;
using static System.Threading.Tasks.Task;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkStringWishes;

// ReSharper disable ParameterHidesMember
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable ForCanBeConvertedToForeach

namespace JJ.Business.Synthesizer.Wishes
{
    // SynthWishes Parallelization

    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(params FluentOutlet[] termFuncs)
            => ParallelAdd((IList<FluentOutlet>)termFuncs);

        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(IList<FluentOutlet> terms)
        {
            if (terms == null) throw new ArgumentNullException(nameof(terms));
            
            var add = Add(terms);
            
            if (GetParallelEnabled)
            {
                foreach (var term in add.Operands)
                {
                    Tape(term);
                }
            }
            
            return add;
        }

        internal void RunParallelsRecursive(IList<FluentOutlet> channels) 
        {
            if (channels == null) throw new ArgumentNullException(nameof(channels));
            if (channels.Contains(null)) throw new Exception("channels.Contains(null)");
            if (!GetParallelEnabled) return;

            var tasks = new Task[channels.Count];
            for (int i = 0; i < channels.Count; i++)
            {
                int channelIndex = i;
                tasks[channelIndex] = Run(() => RunParallelsRecursive(channels[channelIndex]));
            }
            
            WaitAll(tasks);
        }

        internal void RunParallelsRecursive(FluentOutlet op)
        {
            if (!GetParallelEnabled) return;

            // Gather all tasks with levels
            var tasks = GetParallelTasksRecursive(op, level: 1);

            // Group tasks by nesting level
            var levelGroups = tasks.OrderByDescending(x => x.Level).GroupBy(x => x.Level);
            foreach (var levelGroup in levelGroups)
            {
                // Execute each nesting level's task simultaneously.
                Task[] tasksInLevel = levelGroup.Select(x => x.Task).ToArray();
                tasksInLevel.ForEach(x => x.Start());
                WaitAll(tasksInLevel); // Ensure each level completes before moving up
            }
        }

        private IList<(Task Task, int Level)> GetParallelTasksRecursive(FluentOutlet op, int level)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            var tasks = new List<(Task, int)>();
            var operands = op.Operands.Where(x => x != null).ToArray();
            
            // Recursively gather tasks from child nodes
            foreach (var operand in operands)
            {
                tasks.AddRange(GetParallelTasksRecursive(operand, level + 1));
            }
            
            for (var unsafeI = 0; unsafeI < operands.Length; unsafeI++)
            {
                int i = unsafeI;
                var operand = operands[i];
                
                // Are we being parallel?
                if (IsTape(operand))
                {
                    RemoveTape(operand);
                    
                    var task = new Task(() =>
                    {
                        Console.WriteLine($"{PrettyTime()} Start Task: {operand.Name} (Level {level})");
                        
                        var cacheResult = Cache(operand, operand.Name);
                        var newOperand = Sample(cacheResult, name: operand.Name);
                        
                        op.Operands[i] = newOperand;

                        // Replace all references to tape
                        IList<Operator> connectedOperators = operand.UnderlyingOutlet.ConnectedInlets.Select(x => x.Operator).ToArray();
                        foreach (Operator connectedOperator in connectedOperators)
                        {
                            OperandList operands2 = connectedOperator.Operands();
                            int j = operands2.IndexOf(operand);
                            operands2[j] = newOperand;
                        }

                        Console.WriteLine($"{PrettyTime()} End Task: {operand.Name} (Level {level})");
                    });
                    
                    tasks.Add((task, level));
                }
            }

            return tasks;
        }

        // Old
        
        [Obsolete]
        private FluentOutlet ParallelAdd_MixedGraphBuildUpAndParallelExecution(
            IList<Func<FluentOutlet>> termFuncs, 
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            name = FetchName(name, callerMemberName);

            // Prep variables
            int termCount = termFuncs.Count;
            int channelCount = GetSpeakerSetup.GetChannelCount();
            string[] names = GetParallelNames(termCount, name);
            string[] displayNames = names.Select(GetDisplayName).ToArray();
            var cacheResults = new Result<StreamAudioData>[termCount];
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
                        channelOutlets[channelIndex] = termFuncs[i](); // This runs parallels, because the funcs can contain another parallel add.
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

                // Read from bytes or file
                reloadedSamples[i] = Sample(cacheResult, name: displayNames[i]);

                // Diagnostic actions
                //if (MustCacheToDisk)
                //{
                //    // Save reloaded samples to disk.
                //    var reloadedSampleRepeated = Repeat(reloadedSamples[i], channelCount).ToArray();
                //    var saveResult2 = Save(reloadedSampleRepeated, names[i] + "_Reloaded.wav");

                //    // Play to test the sample loading.
                //    if (MustPlayParallels) Play(saveResult2.Data);
                //}
            }


            stopWatch.Stop();

            // Report total real-time and complexity metrics.
            double audioDuration = cacheResults.Max(x => x.Data.AudioFileOutput.Duration);
            double calculationDuration = stopWatch.Elapsed.TotalSeconds;
            int complexity = cacheResults.Sum(x => x.Complexity());
            string formattedMetrics = FormatMetrics(audioDuration, calculationDuration, complexity);
            string message = $"{PrettyTime()} Totals {name} Terms: {formattedMetrics}";
            Console.WriteLine(message);

            return Add(reloadedSamples);
        }

        // Helpers

        [Obsolete]
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
        
        [Obsolete]
        private static string GetDisplayName(string fileName)
        {
            if (fileName == null) return null;
            return Path.GetFileNameWithoutExtension(fileName.WithShortGuids(4));
        }
        
        private readonly HashSet<Outlet> _tapes = new HashSet<Outlet>();
        private void AddTape(Outlet outlet) => _tapes.Add(outlet);
        private bool IsTape(Outlet outlet) => _tapes.Contains(outlet);
        private void RemoveTape(Outlet outlet) => _tapes.Remove(outlet);
    }
}
