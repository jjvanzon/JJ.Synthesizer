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
        public FluentOutlet ParallelAdd(params Func<Outlet>[] funcs)
            => ParallelAdd(1, (IList<Func<Outlet>>)funcs);

        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(IList<Func<Outlet>> funcs, [CallerMemberName] string callerMemberName = null)
            => ParallelAdd(1, funcs, callerMemberName);

        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(double volume, params Func<Outlet>[] funcs)
            => ParallelAdd(volume, (IList<Func<Outlet>>)funcs);

        public FluentOutlet ParallelAdd(double volume, IList<Func<Outlet>> funcs, [CallerMemberName] string callerMemberName = null)
            => _parallelWishes.ParallelAdd(volume, funcs, callerMemberName);
        
        /// <inheritdoc cref="_paralleladd" />
        private class ParallelWishes
        {
            private readonly SynthWishes x;

            /// <inheritdoc cref="_paralleladd" />
            public ParallelWishes(SynthWishes synthWishes) => x = synthWishes;

            /// <inheritdoc cref="_paralleladd" />
            public FluentOutlet ParallelAdd(double volume, IList<Func<Outlet>> funcs, [CallerMemberName] string callerMemberName = null)
            {
                string name = x.FetchName(callerMemberName);

                if (funcs == null) throw new ArgumentNullException(nameof(funcs));

                // If parallels disabled
                if (!x.ParallelEnabled)
                { 
                    // Return a normal Add of the Outlets returned by the funcs.
                    return volume * x.Add(funcs.Select(x => x()).ToArray());
                }
                
                // Set flags based on 'preview parallels'.
                bool inMemory = !x.MustSaveParallels;

                // Prep variables
                int termCount = funcs.Count;
                int channelCount = x.SpeakerSetup.GetChannelCount();
                string[] names = GetParallelAddNames(termCount, name);
                string[] displayNames = names.Select(x => x.WithShortGuids(4)).ToArray();
                var saveAudioResults = new Result<SaveAudioResultData>[termCount];
                var reloadedSamples = new Outlet[termCount];
                var outlets = new Outlet[termCount][];
                for (int i = 0; i < termCount; i++)
                {
                    outlets[i] = new Outlet[channelCount];
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
                        var saveAudioResult = x.SaveAudio(outlets[i], names[i], inMemory);
                        saveAudioResults[i] = saveAudioResult;
                        
                        // Play if needed
                        if (x.MustPlayParallels)
                        { 
                            x._playWishes.PlayIfAllowed(saveAudioResult.Data);
                        }

                        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} End Task: {displayNames[i]}", "SynthWishes");
                    });
                }
                finally
                {
                    // Clean up files
                    if (!inMemory && !x.MustSaveParallels) 
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
                
                // TODO: Moved this out of the parallel loop,
                // for consistency with the preview parallels variation of the method.
                // But I feel odd about processing less in parallel...
                
                // Reload Samples
                for (int i = 0; i < termCount; i++)
                {
                    var saveAudioResult = saveAudioResults[i];
                    
                    if (inMemory)
                    { 
                        // Read from bytes
                        reloadedSamples[i] = x.Sample(saveAudioResult.Data.Bytes);
                    }
                    else
                    {
                        // Read from file
                        reloadedSamples[i] = x.Sample(names[i]);
                        
                        // Save reloaded samples again.
                        if (x.MustSaveParallels)
                        {
                            var reloadedSampleRepeated = Repeat(reloadedSamples[i], channelCount).ToArray();
                            var saveAudioResult2 = x.SaveAudio(reloadedSampleRepeated, names[i] + "_Reloaded.wav", mustWriteToMemory: false);
                            
                            // Play to test the sample loading.
                            if (x.MustPlayParallels) x._playWishes.PlayIfAllowed(saveAudioResult2.Data);
                        }
                    }
                }
                
                stopWatch.Stop();

                // Report total real-time and complexity metrics.
                double audioDuration = saveAudioResults.Max(x => x.Data.AudioFileOutput.Duration);
                double calculationDuration = stopWatch.Elapsed.TotalSeconds;
                int complexity = saveAudioResults.Sum(x => x.Data.Complexity);
                string formattedMetrics = x.FormatMetrics(audioDuration, calculationDuration, complexity);
                string message = $"Totals {name} Terms: {formattedMetrics}";
                Console.WriteLine(message);
                
                return x.Add(reloadedSamples);
            }

            private string[] GetParallelAddNames(int count, string name)
            {
                string guidString = $"{NewGuid()}";

                string sep = " ";
                if (string.IsNullOrEmpty(name))  sep = "";
                

                var fileNames = new string[count];
                for (int i = 0; i < count; i++)
                {
                    fileNames[i] = $"{name}{sep}Term {i + 1} {nameof(ParallelAdd)} {guidString}";
                }

                return fileNames;
            }

        }
    }
}