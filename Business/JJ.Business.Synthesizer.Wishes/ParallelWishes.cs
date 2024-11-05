using JJ.Business.Synthesizer.Enums;
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
            => ParallelAdd(1, (IList<Func<FluentOutlet>>)funcs);

        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(IList<Func<FluentOutlet>> funcs, [CallerMemberName] string callerMemberName = null)
            => ParallelAdd(1, funcs, callerMemberName);

        /// <inheritdoc cref="docs._paralleladd" />
        public FluentOutlet ParallelAdd(double volume, params Func<FluentOutlet>[] funcs)
            => ParallelAdd(volume, (IList<Func<FluentOutlet>>)funcs);

        public FluentOutlet ParallelAdd(double volume, IList<Func<FluentOutlet>> funcs, [CallerMemberName] string callerMemberName = null)
            => _parallelWishes.ParallelAdd(volume, funcs, callerMemberName);
        
        /// <inheritdoc cref="docs._paralleladd" />
        private class ParallelWishes
        {
            private readonly SynthWishes x;

            /// <inheritdoc cref="docs._paralleladd" />
            public ParallelWishes(SynthWishes synthWishes) => x = synthWishes;

            /// <inheritdoc cref="docs._paralleladd" />
            public FluentOutlet ParallelAdd(double volume, IList<Func<FluentOutlet>> funcs, [CallerMemberName] string callerMemberName = null)
            {
                string name = x.FetchName(callerMemberName);

                if (funcs == null) throw new ArgumentNullException(nameof(funcs));

                // If parallels disabled
                if (!x.GetParallelEnabled)
                { 
                    // Return a normal Add of the Outlets returned by the funcs.
                    return volume * x.Add(funcs.Select(x => x()).ToArray());
                }
                
                // Prep variables
                int termCount = funcs.Count;
                int channelCount = x.GetSpeakerSetup.GetChannelCount();
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

                    var originalChannel = x.Channel;
                    try
                    {
                        for (int j = 0; j < channelCount; j++)
                        {
                            x.ChannelIndex = j;
                            channelOutlets[j] = x.Multiply(funcs[i](), volume); // This runs parallels, because the funcs can contain another parallel add.
                        }
                    }
                    finally
                    {
                        x.Channel = originalChannel;
                    }

                    // Generate audio
                    cacheResults[i] = x.Cache(channelOutlets, names[i]);

                    Console.WriteLine($"{PrettyTime()} End Task: {displayNames[i]}", "SynthWishes");
                });
                                
                // Moved this out of the parallel loop,
                // but feels to process less in parallel.
            
                // Reload Samples
                for (int i = 0; i < termCount; i++)
                {
                    var cacheResult = cacheResults[i];

                    // Play if needed
                    if (x.MustPlayParallels) x._playWishes.Play(cacheResult.Data);

                    if (!x.MustCacheToDisk)
                    {
                        // Read from bytes
                        reloadedSamples[i] = x.Sample(cacheResult.Data.Bytes).SetName(displayNames[i]);
                    }
                    else
                    {
                        // Read from file
                        reloadedSamples[i] = x.Sample(names[i]).SetName(displayNames[i]);

                        // Diagnostic actions
                        {
                            // Save reloaded samples to disk.
                            var reloadedSampleRepeated = Repeat(reloadedSamples[i], channelCount).ToArray();
                            var saveResult2 = x.Save(reloadedSampleRepeated, names[i] + "_Reloaded.wav");

                            // Play to test the sample loading.
                            if (x.MustPlayParallels) x._playWishes.Play(saveResult2.Data);
                        }
                    }
                }
                
                
                stopWatch.Stop();

                // Report total real-time and complexity metrics.
                double audioDuration = cacheResults.Max(x => x.Data.AudioFileOutput.Duration);
                double calculationDuration = stopWatch.Elapsed.TotalSeconds;
                int complexity = cacheResults.Sum(x => x.Data.Complexity);
                string formattedMetrics = x.FormatMetrics(audioDuration, calculationDuration, complexity);
                string message = $"{PrettyTime()} Totals {name} Terms: {formattedMetrics}";
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