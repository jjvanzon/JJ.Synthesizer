using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Wishes.Helpers;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    [Obsolete]
    public static class ObsoleteTapeWishesExtensions
    {
        [Obsolete]
        private static void RunTapeLeavesPerBatch(this SynthWishes synthWishes, Tape[] tapes)
        {
            while (tapes.Length > 0)
            {
                tapes = synthWishes.RunTapeLeafBatch(tapes);
            }
        }
        
        [Obsolete]
        private static Tape[] RunTapeLeafBatch(this SynthWishes synthWishes,Tape[] tapes)
        {
            // Get leaves
            Tape[] leaves = tapes.Where(x => x.ChildTapes.Count == 0).ToArray();
            
            // Execute tasks for the leaves
            Task[] tasks = new Task[leaves.Length];
            for (var i = 0; i < leaves.Length; i++)
            {
                Tape tape = leaves[i];
                tasks[i] = Task.Run(() => synthWishes.RunTape(tape));
            }
            
            // Ensure the leaf batch completes before moving on
            Task.WaitAll(tasks);
            
            // Remove parent-child relationship
            foreach (Tape leaf in leaves)
            {
                leaf.ParentTape?.ChildTapes.Remove(leaf);
                leaf.ParentTape = null;
            }
            
            // Return remaining tapes
            Tape[] remainingTapes = tapes.Except(leaves).ToArray();
            return remainingTapes;
        }
        
        [Obsolete]
        private static void RunTapesPerNestingLevel(this SynthWishes synthWishes, Tape[] tapes)
        {
            // Group tasks by nesting level
            var tapeGroups = tapes.OrderByDescending(x => x.NestingLevel)
                                  .GroupBy(x => x.NestingLevel)
                                  .Select(x => x.ToArray())
                                  .ToArray();
            
            // Execute each nesting level's task simultaneously.
            foreach (Tape[] tapeGroup in tapeGroups)
            {
                Task[] tasks = new Task[tapeGroup.Length];
                for (var i = 0; i < tapeGroup.Length; i++)
                {
                    Tape tape = tapeGroup[i];
                    tasks[i] = Task.Run(() => synthWishes.RunTape(tape));
                }
                Task.WaitAll(tasks); // Ensure each level completes before moving up
            }
        }
        [Obsolete]
        private static FlowNode ParallelAdd_MixedGraphBuildUpAndParallelExecution(
            this SynthWishes synthWishes,
            IList<Func<FlowNode>> termFuncs,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            name = synthWishes.FetchName(name, callerMemberName);
            
            // Prep variables
            int termCount = termFuncs.Count;
            int channelCount = synthWishes.GetSpeakers.GetChannelCount();
            string[] names = GetParallelNames(termCount, name);
            string[] displayNames = names.Select(GetDisplayName).ToArray();
            var cacheBuffs = new Buff[termCount];
            var reloadedSamples = new FlowNode[termCount];
            
            var stopWatch = Stopwatch.StartNew();
            
            // Save to files
            Parallel.For(0, termCount, i =>
            {
                Console.WriteLine($"{FrameworkStringWishes.PrettyTime()} Start Task: {displayNames[i]}", "SynthWishes");
                
                // Get outlets first
                var channels = new FlowNode[channelCount];
                
                var originalChannel = synthWishes.GetChannel;
                try
                {
                    for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
                    {
                        synthWishes.WithChannelIndex(channelIndex);
                        channels[channelIndex] = termFuncs[i](); // This runs parallels, because the funcs can contain another parallel add.
                    }
                }
                finally
                {
                    synthWishes.WithChannel(originalChannel);
                }
                
                // Generate audio
                cacheBuffs[i] = synthWishes.Cache(channels, synthWishes.GetAudioLength, names[i]);
                
                Console.WriteLine($"{FrameworkStringWishes.PrettyTime()} End Task: {displayNames[i]}", "SynthWishes");
            });
            
            // Moved this out of the parallel loop,
            // but feels strange to process less in parallel.
            
            // Reload Samples
            for (int i = 0; i < termCount; i++)
            {
                var cacheBuff = cacheBuffs[i];
                
                // Play if needed
                if (synthWishes.GetPlayAllTapes) synthWishes.Play(cacheBuff);
                
                // Read from bytes or file
                reloadedSamples[i] = synthWishes.Sample(cacheBuff, name: displayNames[i]);
                
                // Diagnostic actions
                //if (GetDiskCacheOn)
                //{
                //    // Save reloaded samples to disk.
                //    var reloadedSampleRepeated = Repeat(reloadedSamples[i], channelCount).ToArray();
                //    var saveResult2 = Save(reloadedSampleRepeated, names[i] + "_Reloaded.wav");
                
                //    // Play to test the sample loading.
                //    if (GetPlayAllTapes) Play(saveResult2.Data);
                //}
            }
            
            
            stopWatch.Stop();
            
            // Report total real-time and complexity metrics.
            double audioDuration = cacheBuffs.Max(x => x.UnderlyingAudioFileOutput.Duration);
            double calculationDuration = stopWatch.Elapsed.TotalSeconds;
            int complexity = cacheBuffs.Sum(x => x.Complexity());
            string formattedMetrics = SynthWishes.FormatMetrics(audioDuration, calculationDuration, complexity);
            string message = $"{FrameworkStringWishes.PrettyTime()} Totals {name} Terms: {formattedMetrics}";
            Console.WriteLine(message);
            
            return synthWishes.Add(reloadedSamples);
        }
        
        // Helpers
        
        [Obsolete]
        private static string[] GetParallelNames(int count, string name)
        {
            string guidString = $"{Guid.NewGuid():N}";
            
            string sep = " ";
            if (string.IsNullOrEmpty(name)) sep = "";
            
            var fileNames = new string[count];
            for (int i = 0; i < count; i++)
            {
                fileNames[i] = $"{name}{sep}Term {i + 1} Parallel {guidString}.wav";
            }
            
            return fileNames;
        }
        
        [Obsolete]
        private static string GetDisplayName(string fileName)
        {
            if (fileName == null) return null;
            return Path.GetFileNameWithoutExtension(fileName.WithShortGuids(4));
        }
    }
}