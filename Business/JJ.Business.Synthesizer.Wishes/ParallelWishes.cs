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
// ReSharper disable ParameterHidesMember

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

                // If preview parallels
                if (x.PreviewParallels)
                {
                    // Call the variation that saves files and plays audio
                    return ParallelAdd_WithPreviewParallels(volume, funcs, name);
                }

                // Prep variables
                int termCount = funcs.Count;
                int channelCount = x.SpeakerSetup.GetChannelCount();
                bool mustWriteToMemory = x.PreviewParallels == false;
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

                        // Generate audio bytes or files
                        var saveAudioResult = x.SaveAudio(outlets[i], names[i], mustWriteToMemory);
                        saveAudioResults[i] = saveAudioResult;
                        
                        // Play audio in case if preview parallels enabled.
                        if (x.PreviewParallels)
                        { 
                            x._playWishes.PlayIfAllowed(saveAudioResult.Data);
                        }

                        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} End Task: {displayNames[i]}", "SynthWishes");
                    });
                }
                finally
                {
                    // Clean-up
                    if (!x.PreviewParallels) // Keep files in case of preview parallels.
                    {
                        for (var j = 0; j < names.Length; j++)
                        {
                            string filePath = names[j];
                            if (File.Exists(filePath))
                            {
                                try { File.Delete(filePath); }
                                catch { /* Ignore file delete exception, so you can keep open file in apps.*/ }
                            }
                        }
                    }
                }
                
                // TODO: Moved this out of the parallel loop,
                // for consistency with the preview parallels variation of the method.
                // But I feel odd about processing less in parallel...
                
                // Reload Samples
                for (int i = 0; i < termCount; i++)
                {
                    // TODO: This can be done more generically.
                    if (!x.PreviewParallels)
                    {
                        reloadedSamples[i] = x.Sample(saveAudioResults[i].Data.Bytes);
                    }
                    else
                    {
                        reloadedSamples[i] = x.Sample(names[i]);
                        
                        // Save and play to test the sample loading
                        // TODO: This doesn't actually save the reloaded samples. replace outlets[i] by a repeat of reloaded samples.
                        var saveResult = x.SaveAudio(outlets[i], names[i] + "_Reloaded.wav");
                        x._playWishes.PlayIfAllowed(saveResult.Data);
                    }
                }
                
                stopWatch.Stop();

                // Report total real-time and complexity metrics.
                {
                    double audioDuration = saveAudioResults.Max(x => x.Data.AudioFileOutput.Duration);
                    double calculationDuration = stopWatch.Elapsed.TotalSeconds;
                    int complexity = saveAudioResults.Sum(x => x.Data.Complexity);
                    string formattedMetrics = x.FormatMetrics(audioDuration, calculationDuration, complexity);
                    string message = $"Totals {name} Terms: {formattedMetrics}";
                    Console.WriteLine(message);
                }
                
                return x.Add(reloadedSamples);
            }

            /// <inheritdoc cref="_withpreviewparallels"/>
            private FluentOutlet ParallelAdd_WithPreviewParallels(double volume, IList<Func<Outlet>> funcs, string name)
            {
                // Arguments already checked in public method

                // Prep variables
                int termCount = funcs.Count;
                int channelCount = x.SpeakerSetup.GetChannelCount();
                bool mustWriteToMemory = x.PreviewParallels == false;
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

                        // Generate audio bytes or files
                        var saveAudioResult = x.SaveAudio(outlets[i], names[i], mustWriteToMemory);
                        saveAudioResults[i] = saveAudioResult;
                        
                        // Play audio in case if preview parallels enabled.
                        if (x.PreviewParallels)
                        { 
                            x._playWishes.PlayIfAllowed(saveAudioResult.Data);
                        }

                        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} End Task: {displayNames[i]}", "SynthWishes");
                    });
                }
                finally
                {
                    // Clean-up
                    if (!x.PreviewParallels) // Keep files in case of preview parallels.
                    {
                        for (var j = 0; j < names.Length; j++)
                        {
                            string filePath = names[j];
                            if (File.Exists(filePath))
                            {
                                try { File.Delete(filePath); }
                                catch { /* Ignore file delete exception, so you can keep open file in apps.*/ }
                            }
                        }
                    }
                }
                
                // TODO: Moved this out of the parallel loop,
                // for consistency with the preview parallels variation of the method.
                // But I feel odd about processing less in parallel...
                
                // Reload Samples
                for (int i = 0; i < termCount; i++)
                {
                    // TODO: This can be done more generically.
                    if (!x.PreviewParallels)
                    {
                        reloadedSamples[i] = x.Sample(saveAudioResults[i].Data.Bytes);
                    }
                    else
                    {
                        reloadedSamples[i] = x.Sample(names[i]);
                        
                        // Save and play to test the sample loading
                        // TODO: This doesn't actually save the reloaded samples. replace outlets[i] by a repeat of reloaded samples.
                        var saveResult = x.SaveAudio(outlets[i], names[i] + "_Reloaded.wav");
                        x._playWishes.PlayIfAllowed(saveResult.Data);
                    }
                }
                
                stopWatch.Stop();

                // Report total real-time and complexity metrics.
                {
                    double audioDuration = saveAudioResults.Max(x => x.Data.AudioFileOutput.Duration);
                    double calculationDuration = stopWatch.Elapsed.TotalSeconds;
                    int complexity = saveAudioResults.Sum(x => x.Data.Complexity);
                    string formattedMetrics = x.FormatMetrics(audioDuration, calculationDuration, complexity);
                    string message = $"Totals {name} Terms: {formattedMetrics}";
                    Console.WriteLine(message);
                }
                
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