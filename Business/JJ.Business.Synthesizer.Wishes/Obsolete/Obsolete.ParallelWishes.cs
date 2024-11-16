using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Wishes.Helpers;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    [Obsolete]
    public static class ObsoleteSynthWishesParallelExtensions
    {
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
            var cacheResults = new Result<StreamAudioData>[termCount];
            var reloadedSamples = new FlowNode[termCount];
            
            var stopWatch = Stopwatch.StartNew();
            
            // Save to files
            Parallel.For(0, termCount, i =>
            {
                Console.WriteLine($"{FrameworkStringWishes.PrettyTime()} Start Task: {displayNames[i]}", "SynthWishes");
                
                // Get outlets first
                var channelOutlets = new FlowNode[channelCount];
                
                var originalChannel = synthWishes.Channel;
                try
                {
                    for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
                    {
                        synthWishes.ChannelIndex = channelIndex;
                        channelOutlets[channelIndex] = termFuncs[i](); // This runs parallels, because the funcs can contain another parallel add.
                    }
                }
                finally
                {
                    synthWishes.Channel = originalChannel;
                }
                
                // Generate audio
                cacheResults[i] = synthWishes.Cache(channelOutlets, names[i]);
                
                Console.WriteLine($"{FrameworkStringWishes.PrettyTime()} End Task: {displayNames[i]}", "SynthWishes");
            });
            
            // Moved this out of the parallel loop,
            // but feels strange to process less in parallel.
            
            // Reload Samples
            for (int i = 0; i < termCount; i++)
            {
                var cacheResult = cacheResults[i];
                
                // Play if needed
                if (synthWishes.GetPlayAllTapes) synthWishes.Play(cacheResult.Data);
                
                // Read from bytes or file
                reloadedSamples[i] = synthWishes.Sample(cacheResult, name: displayNames[i]);
                
                // Diagnostic actions
                //if (GetDiskCaching)
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
            double audioDuration = cacheResults.Max(y => y.Data.AudioFileOutput.Duration);
            double calculationDuration = stopWatch.Elapsed.TotalSeconds;
            int complexity = cacheResults.Sum(y => y.Complexity());
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
                fileNames[i] = $"{name}{sep}Term {i + 1} {nameof(SynthWishes.ParallelAdd)} {guidString}.wav";
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