using JJ.Business.Synthesizer.Enums;
using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Wishes.Helpers;

namespace JJ.Business.Synthesizer.Wishes
{
    // SynthWishes Parallelization

    public partial class SynthWishes
    {
        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(params Func<Outlet>[] funcs)
            => ParallelAdd(1, (IList<Func<Outlet>>)funcs);

        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(
            IList<Func<Outlet>> funcs, [CallerMemberName] string callerMemberName = null)
            => ParallelAdd(1, funcs, callerMemberName);

        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(double volume, params Func<Outlet>[] funcs)
            => ParallelAdd(volume, (IList<Func<Outlet>>)funcs);
        
        /// <inheritdoc cref="_paralleladd" />
        public FluentOutlet ParallelAdd(
            double volume, IList<Func<Outlet>> funcs, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName);

            if (funcs == null) throw new ArgumentNullException(nameof(funcs));
            
            if (PreviewParallels)
            {
                return ParallelAdd_WithPreviewParallels(volume, funcs, name);
            }

            // Prep variables
            int termCount = funcs.Count;
            int channelCount = SpeakerSetup.GetChannelCount();
            string[] fileNames = GetParallelAdd_FileNames(termCount, name);
            var reloadedSamples = new Outlet[termCount];
            var outlets = new Outlet[termCount][];
            for (int i = 0; i < termCount; i++)
            { 
                outlets[i] = new Outlet[channelCount];
            }

            try
            {
                // Save to files
                Parallel.For(0, termCount, i =>
                {
                    Debug.WriteLine($"Start parallel task: {fileNames[i]}", "SynthWishes");
                                
                    // Get outlets first (before going parallel ?)
                    ChannelEnum originalChannel = Channel;
                    try
                    {
                        for (int j = 0; j < channelCount; j++)
                        {
                            ChannelIndex = j;
                            outlets[i][j] = Multiply(funcs[i](), volume); // This runs parallels, because the funcs can contain another parallel add.
                        }
                    }
                    finally
                    {
                        Channel = originalChannel;
                    }

                    _saveAudioWishes.SaveAudioBase(outlets[i], fileNames[i]);
                    
                    Debug.WriteLine($"End parallel task: {fileNames[i]}", "SynthWishes");
                });

                // Reload Samples
                for (int i = 0; i < termCount; i++)
                {
                    reloadedSamples[i] = Sample(fileNames[i]);
                }
            }
            finally
            {
                // Clean-up
                for (var j = 0; j < fileNames.Length; j++)
                {
                    string filePath = fileNames[j];
                    if (File.Exists(filePath))
                    {
                        try { File.Delete(filePath); }
                        catch { /* Ignore file delete exception, so you can keep open file in apps.*/ }
                    }
                }
            }

            return Add(reloadedSamples);
        }
        
        /// <inheritdoc cref="_withpreviewparallels"/>
        public bool PreviewParallels { get; private set; }

        /// <inheritdoc cref="_withpreviewparallels"/>
        public SynthWishes WithPreviewParallels()
        {
            PreviewParallels = true;
            return this;
        }
        
        /// <inheritdoc cref="_withpreviewparallels"/>
        private FluentOutlet ParallelAdd_WithPreviewParallels(
            double volume, IList<Func<Outlet>> funcs, string name)
        {
            // Arguments already checked in public method
            
            // Prep variables
            int termCount = funcs.Count;
            int channelCount = SpeakerSetup.GetChannelCount();
            string[] fileNames = GetParallelAdd_FileNames(termCount, name);
            var reloadedSamples = new Outlet[termCount];
            var outlets = new Outlet[termCount][];
            for (int i = 0; i < termCount; i++)
            { 
                outlets[i] = new Outlet[channelCount];
            }

            // Save and play files
            Parallel.For(0, termCount, i =>
            {
                Debug.WriteLine($"Start parallel task: {fileNames[i]}", "SynthWishes");
                
                // Get outlets first (before going parallel?)
                ChannelEnum originalChannel = Channel;
                try
                {
                    for (int j = 0; j < channelCount; j++)
                    {
                        ChannelIndex = j;
                        outlets[i][j] = Multiply(funcs[i](), volume); // This runs parallels, because the funcs can contain another parallel add.
                    }
                }
                finally
                {
                    Channel = originalChannel;
                }

                var saveResult = _saveAudioWishes.SaveAudioBase(outlets[i], fileNames[i]);
                PlayIfAllowed(saveResult.Data);
            
                Debug.WriteLine($"End parallel task: {fileNames[i]}", "SynthWishes");
            });

            // Reload sample
            for (int i = 0; i < termCount; i++)
            {
                reloadedSamples[i] = Sample(fileNames[i]);

                // Save and play to test the sample loading
                // TODO: This doesn't actually save the reloaded samples. replace outlets[i] by a repeat of reloaded samples.
                var saveResult = _saveAudioWishes.SaveAudioBase(outlets[i], fileNames[i] + "_Reloaded.wav");
                PlayIfAllowed(saveResult.Data);
            }

            return Add(reloadedSamples);
        }

        private string[] GetParallelAdd_FileNames(int count, string name)
        {
            string guidString = $"{Guid.NewGuid()}";

            if (!name.Contains(nameof(ParallelAdd), ignoreCase: true))
            {
                name += " " + nameof(ParallelAdd);
            }

            var fileNames = new string[count];
            for (int i = 0; i < count; i++)
            {
                fileNames[i] = $"{name} (Term {i + 1}) {guidString}.wav";
            }

            return fileNames;
        } 

    }
}
