using JJ.Business.Synthesizer.Enums;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Wishes.Helpers;
using static System.Guid;
using static System.Linq.Enumerable;
// ReSharper disable ParameterHidesMember
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable ForCanBeConvertedToForeach

namespace JJ.Business.Synthesizer.Wishes
{
    // SynthWishes Parallelization

    public partial class SynthWishes
    {
        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(params Func<FluentOutlet>[] funcs)
            => ParallelAdd(1, (IList<Func<FluentOutlet>>)funcs);

        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(IList<Func<FluentOutlet>> funcs, [CallerMemberName] string callerMemberName = null)
            => ParallelAdd(1, funcs, callerMemberName);

        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(double volume, params Func<FluentOutlet>[] funcs)
            => ParallelAdd(volume, (IList<Func<FluentOutlet>>)funcs);

        public FluentOutlet ParallelAdd(double volume, IList<Func<FluentOutlet>> funcs, [CallerMemberName] string callerMemberName = null)
            => _parallelWishes.ParallelAdd(volume, funcs, callerMemberName);
        
        /// <inheritdoc cref="_paralleladd" />
        private class ParallelWishes
        {
            private readonly SynthWishes x;

            /// <inheritdoc cref="_paralleladd" />
            public ParallelWishes(SynthWishes synthWishes) => x = synthWishes;

            /// <inheritdoc cref="_paralleladd" />
            public FluentOutlet ParallelAdd(double volume, IList<Func<FluentOutlet>> funcs, [CallerMemberName] string callerMemberName = null)
            {
                string name = x.FetchName(callerMemberName);

                if (funcs == null) throw new ArgumentNullException(nameof(funcs));

                // If parallels disabled
                if (!x.ParallelEnabled)
                { 
                    // Return a normal Add of the Outlets returned by the funcs.
                    return volume * x.Add(funcs.Select(x => x()).ToArray());
                }
                
                bool inMemory = x.InMemoryProcessingEnabled && !x.MustSaveParallels;
                bool onDisk = !inMemory;

                // Prep variables
                int termCount = funcs.Count;
                int channelCount = x.SpeakerSetup.GetChannelCount();
                string[] names = GetParallelNames(termCount, name);
                string[] displayNames = names.Select(GetDisplayName).ToArray();
                var saveResults = new Result<SaveResultData>[termCount];
                var reloadedSamples = new FluentOutlet[termCount];
                var outlets = new FluentOutlet[termCount][];
                for (int i = 0; i < termCount; i++)
                {
                    outlets[i] = new FluentOutlet[channelCount];
                }

                var stopWatch = Stopwatch.StartNew();

                try
                {
                    // Save to files
                    Parallel.For(0, termCount, i =>
                    {
                        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} Start Task: {displayNames[i]}", "SynthWishes");

                        // Get outlets first
                        ChannelEnum originalChannel = x.Channel;
                        try
                        {
                            for (int j = 0; j < channelCount; j++)
                            {
                                x.ChannelIndex = j;
                                outlets[i][j] = x.Multiply(funcs[i](), volume); // This runs parallels, because the funcs can contain another parallel add.
                            }
                        }
                        finally
                        {
                            x.Channel = originalChannel;
                        }

                        // Generate audio
                        var saveResult = x.Save(outlets[i], names[i], inMemory);
                        saveResults[i] = saveResult;
                        
                        // Play if needed
                        if (x.MustPlayParallels)
                        { 
                            x._playWishes.PlayIfAllowed(saveResult.Data);
                        }

                        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} End Task: {displayNames[i]}", "SynthWishes");
                    });
                                    
                    // TODO: Moved this out of the parallel loop,
                    // for consistency with the preview parallels variation of the method.
                    // But I feel odd about processing less in parallel...
                
                    // Reload Samples
                    for (int i = 0; i < termCount; i++)
                    {
                        var saveResult = saveResults[i];
                    
                        if (inMemory)
                        { 
                            // Read from bytes
                            reloadedSamples[i] = x.Sample(saveResult.Data.Bytes).SetName(displayNames[i]);
                        }
                        else
                        {
                            // Read from file
                            reloadedSamples[i] = x.Sample(names[i]).SetName(displayNames[i]);
                        
                            // Save reloaded samples again.
                            if (x.MustSaveParallels)
                            {
                                var reloadedSampleRepeated = Repeat(reloadedSamples[i], channelCount).ToArray();
                                var saveResult2 = x.Save(reloadedSampleRepeated, names[i] + "_Reloaded.wav", mustWriteToMemory: false);
                            
                                // Play to test the sample loading.
                                if (x.MustPlayParallels) x._playWishes.PlayIfAllowed(saveResult2.Data);
                            }
                        }
                    }
                }
                finally
                {
                    // Clean up files
                    if (onDisk && !x.MustSaveParallels) 
                    {
                        for (var j = 0; j < names.Length; j++)
                        {
                            string filePath = names[j];
                            if (!File.Exists(filePath)) continue;
                            try { File.Delete(filePath); }
                            catch { /* Ignore file delete exception, so you can keep open file in apps.*/ }
                        }
                    }
                }
                
                stopWatch.Stop();

                // Report total real-time and complexity metrics.
                double audioDuration = saveResults.Max(x => x.Data.AudioFileOutput.Duration);
                double calculationDuration = stopWatch.Elapsed.TotalSeconds;
                int complexity = saveResults.Sum(x => x.Data.Complexity);
                string formattedMetrics = x.FormatMetrics(audioDuration, calculationDuration, complexity);
                string message = $"Totals {name} Terms: {formattedMetrics}";
                Console.WriteLine(message);
                
                return x.Add(reloadedSamples);
            }

            private string[] GetParallelNames(int count, string name)
            {
                string guidString = $"{NewGuid()}";

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

        }
    }
}