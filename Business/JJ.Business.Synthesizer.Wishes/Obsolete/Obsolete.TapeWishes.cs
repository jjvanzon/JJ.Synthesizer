﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;
// ReSharper disable UnusedMember.Local

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    [Obsolete]
    public static class ObsoleteTapeWishesExtensions
    {
        [Obsolete]
        private static void RunTapeLeavesPerBatch(this TapeRunner tapeRunner, Tape[] tapes)
        {
            while (tapes.Length > 0)
            {
                tapes = tapeRunner.RunTapeLeafBatch(tapes);
            }
        }
        
        [Obsolete]
        private static Tape[] RunTapeLeafBatch(this TapeRunner tapeRunner,Tape[] tapes)
        {
            // Get leaves
            Tape[] leaves = tapes.Where(x => x.ChildTapes.Count == 0).ToArray();
            
            // Execute tasks for the leaves
            Task[] tasks = new Task[leaves.Length];
            for (var i = 0; i < leaves.Length; i++)
            {
                Tape tape = leaves[i];
                tasks[i] = Task.Run(() => tapeRunner.RunTape(tape));
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
        private static void RunTapesPerNestingLevel(this TapeRunner tapeRunner, Tape[] tapes)
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
                    tasks[i] = Task.Run(() => tapeRunner.RunTape(tape));
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
            name = FetchName(name, callerMemberName);
            
            // Prep variables
            int termCount = termFuncs.Count;
            int speakers = synthWishes.GetSpeakers;
            string[] names = GetParallelNames(termCount, name);
            string[] displayNames = names.Select(GetDisplayName).ToArray();
            var cacheBuffs = new Buff[termCount];
            var reloadedSamples = new FlowNode[termCount];
            
            var stopWatch = Stopwatch.StartNew();
            
            // Save to files
            Parallel.For(0, termCount, i =>
            {
                Console.WriteLine($"{PrettyTime()} Start Task: {displayNames[i]}", "SynthWishes");
                
                // Get outlets first
                var channelSignals = new FlowNode[speakers];
                
                var originalChannel = synthWishes.GetChannel;
                try
                {
                    for (int channel = 0; channel < speakers; channel++)
                    {
                        synthWishes.WithChannel(channel);
                        channelSignals[channel] = termFuncs[i](); // This runs parallels, because the funcs can contain another parallel add.
                    }
                }
                finally
                {
                    synthWishes.WithChannel(originalChannel);
                }
                
                // Generate audio
                cacheBuffs[i] = synthWishes.Cache(channelSignals, synthWishes.GetAudioLength, names[i]);
                
                Console.WriteLine($"{PrettyTime()} End Task: {displayNames[i]}", "SynthWishes");
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
                //if (GetCacheToDisk)
                //{
                //    // Save reloaded samples to disk.
                //    var reloadedSampleRepeated = Repeat(reloadedSamples[i], speakers).ToArray();
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
            string formattedMetrics = FormatMetrics(audioDuration, calculationDuration, complexity);
            string message = $"{PrettyTime()} Totals {name} Terms: {formattedMetrics}";
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