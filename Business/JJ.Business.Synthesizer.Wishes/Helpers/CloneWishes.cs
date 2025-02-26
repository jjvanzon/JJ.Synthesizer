using System;
using System.Linq;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Collections;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class CloneWishes
    {
        internal static Tape DeepClone(Tape source) 
        {
            var dest = new Tape(source.SynthWishes);
            DeepClone(source, dest);
            return dest;
        }
        
        internal static void DeepClone(Tape source, Tape dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));

            dest.SynthWishes = source.SynthWishes;

            // Buff
            dest.Bytes = source.Bytes;
            dest.FilePathResolved = source.FilePathResolved;
            dest.UnderlyingAudioFileOutput = source.UnderlyingAudioFileOutput;
            dest.Sample = source.Sample;
            
            // Name
            dest.FallbackName = source.FallbackName;
            dest.FilePathSuggested = source.FilePathSuggested;
        
            // Signals
            dest.Outlets = source.Outlets;

            // Durations
            dest.Duration = source.Duration;
            dest.LeadingSilence = source.LeadingSilence;
            dest.TrailingSilence = source.TrailingSilence;

            // Config
            CloneConfig(source.Config, dest.Config);

            // Actions
            CloneActions(source.Actions, dest.Actions);
            dest.IsPadded = source.IsPadded;
            dest.IsTape = source.IsTape;

            // Hierarchy
            dest.ClearHierarchy();
            dest.ParentTapes.AddRange(source.ParentTapes);
            dest.ChildTapes.AddRange(source.ChildTapes);
            dest.NestingLevel = source.NestingLevel;
        }
        
        internal static Buff CloneBuff(Buff source)
        {
            var dest = new Buff();
            CloneBuff(source, dest);
            return dest;
        }
        
        internal static void CloneBuff(Buff source, Buff dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            dest.Bytes = source.Bytes;
            dest.FilePath = source.FilePath;
            dest.UnderlyingAudioFileOutput = source.UnderlyingAudioFileOutput;
            dest.Tape = source.Tape;
            dest.SynthWishes = source.SynthWishes;
        }

        internal static void CloneConfig(TapeConfig source, TapeConfig dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            dest.SamplingRate   = source.SamplingRate  ;
            dest.Bits           = source.Bits          ;
            dest.Channels       = source.Channels      ;
            dest.Channel        = source.Channel       ;
            dest.AudioFormat    = source.AudioFormat   ;
            dest.Interpolation  = source.Interpolation ;
            dest.CourtesyFrames = source.CourtesyFrames;
        }

        internal static Tape CloneTape(SynthWishes source)
        {
            var dest = new Tape(source);
            CloneTape(source, dest);
            return dest;
        }

        internal static void CloneTape(SynthWishes source, Tape dest)
        {
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            if (source == null) throw new ArgumentNullException(nameof(source));
            
            CloneConfig(source.Config, dest.Config);

            dest.SynthWishes = source;
            dest.LeadingSilence = source.GetLeadingSilence.Value;
            dest.TrailingSilence = source.GetTrailingSilence.Value;
            dest.Actions.DiskCache.On = source.GetDiskCache;
            dest.Actions.PlayAllTapes.On = source.GetPlayAllTapes;
        }

        internal static void CloneConfig(ConfigResolver source, TapeConfig dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            dest.SamplingRate   = source.GetSamplingRate  ;
            dest.Bits           = source.GetBits          ;
            dest.Channels       = source.GetChannels      ;
            dest.Channel        = source.GetChannel       ;
            dest.AudioFormat    = source.GetAudioFormat   ;
            dest.Interpolation  = source.GetInterpolation ;
            dest.CourtesyFrames = source.GetCourtesyFrames;
        }
        
        internal static Tape CloneTape(AudioFileOutput source)
        {
            var dest = new Tape();
            CloneTape(source, dest);
            return dest;
        }

        private static void CloneTape(AudioFileOutput source, Tape dest)
        {
            dest.UnderlyingAudioFileOutput = source;
            
            dest.FallbackName = source.Name;
            dest.FilePathSuggested = source.FilePath;
            dest.Duration = source.Duration;
            dest.Config.SamplingRate = source.SamplingRate;
            dest.Config.Bits = source.Bits();
            dest.Config.Channels = source.Channels();
            dest.Config.AudioFormat = source.AudioFormat();

            source.AudioFileOutputChannels.ForEach(x => dest.Outlets.Add(x.Outlet));
        }

        internal static void CloneActions(TapeActions source, TapeActions dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            
            CloneAction(source.Play,                dest.Play);
            CloneAction(source.Save,                dest.Save);
            CloneAction(source.BeforeRecord,        dest.BeforeRecord);
            CloneAction(source.AfterRecord,         dest.AfterRecord);
            CloneAction(source.PlayChannels,        dest.PlayChannels);
            CloneAction(source.SaveChannels,        dest.SaveChannels);
            CloneAction(source.BeforeRecordChannel, dest.BeforeRecordChannel);
            CloneAction(source.AfterRecordChannel,  dest.AfterRecordChannel);
            CloneAction(source.PlayAllTapes,        dest.PlayAllTapes);
            CloneAction(source.DiskCache,           dest.DiskCache);
        }
        
        internal static void CloneAction(TapeAction source, TapeAction dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            dest.On = source.On;
            dest.Done = source.Done;
            dest.Callback = source.Callback;
            dest.FilePathSuggested = source.FilePathSuggested;
        }
    }
    
    internal static class CloneExtensionWishes
    {
        internal static Tape CloneTape(this SynthWishes synthWishes)
        {
            return CloneWishes.CloneTape(synthWishes);
        }
    }
}
