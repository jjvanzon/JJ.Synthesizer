using System;
using System.Linq;
using JJ.Business.Synthesizer.Wishes.JJ_Framework_Collections_Copied;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class CloneWishes
    {
        internal static Tape DeepClone(Tape source) 
        {
            var dest = new Tape();
            DeepClone(source, dest);
            return dest;
        }
        
        internal static void DeepClone(Tape source, Tape dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            
            // Buff
            dest.Bytes = source.Bytes;
            dest.FilePathResolved = source.FilePathResolved;
            dest.UnderlyingAudioFileOutput = source.UnderlyingAudioFileOutput;

            // Name
            dest.FallBackName = source.FallBackName;
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
        }

        internal static TapeConfig CloneConfig(TapeConfig source)
        {
            var dest = new TapeConfig();
            CloneConfig(source, dest);
            return dest;
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
        }
    }
}
