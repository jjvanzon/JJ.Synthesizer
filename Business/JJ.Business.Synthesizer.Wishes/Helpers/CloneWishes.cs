using System;
using System.Linq;
using JJ.Business.Synthesizer.Wishes.JJ_Framework_Collections_Copied;
using JJ.Business.Synthesizer.Wishes.TapeWishes;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    public static class CloneWishes
    {
        public static Tape CloneTape(Tape source) 
        {
            var dest = new Tape();
            CloneTape(source, dest);
            return dest;
        }
        
        public static void CloneTape(Tape source, Tape dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            
            // Buff
            CloneBuff(source.Buff, dest.Buff);
        
            // Name
            dest.FallBackName = source.FallBackName;
            dest.FilePathSuggested = source.FilePathSuggested;
        
            // Signals
            dest.Signal = source.Signal;
            dest.Signals = source.Signals?.ToArray() ?? Array.Empty<FlowNode>();

            // Durations
            dest.Duration = source.Duration;
            dest.LeadingSilence = source.LeadingSilence;
            dest.TrailingSilence = source.TrailingSilence;

            // Config
            CloneTapeConfig(source.Config, dest.Config);
            dest.Channel = source.Channel;

            // Actions
            CloneActions(source.Actions, dest.Actions);
            dest.IsPadded = source.IsPadded;
            dest.IsTape = source.IsTape;

            // Hierarchy
            dest.ClearRelationships();
            dest.ParentTapes.AddRange(source.ParentTapes);
            dest.ChildTapes.AddRange(source.ChildTapes);
            dest.NestingLevel = source.NestingLevel;
        }
        
        public static Buff CloneBuff(Buff source)
        {
            var dest = new Buff();
            CloneBuff(source, dest);
            return dest;
        }    
        
        public static void CloneBuff(Buff source, Buff dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            dest.Bytes = source.Bytes;
            dest.FilePath = source.FilePath;
            dest.UnderlyingAudioFileOutput = source.UnderlyingAudioFileOutput;
        }

        internal static TapeConfig CloneTapeConfig(TapeConfig source)
        {
            var dest = new TapeConfig();
            CloneTapeConfig(source, dest);
            return dest;
        }
        
        public static void CloneTapeConfig(TapeConfig source, TapeConfig dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            dest.SamplingRate   = source.SamplingRate  ;
            dest.Bits           = source.Bits          ;
            dest.Channels       = source.Channels      ;
            dest.AudioFormat    = source.AudioFormat   ;
            dest.Interpolation  = source.Interpolation ;
            dest.CourtesyFrames = source.CourtesyFrames;
        }

        public static void CloneActions(TapeActions source, TapeActions dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            
            CloneAction(source.Play, dest.Play);
            CloneAction(source.Save, dest.Save);
            CloneAction(source.BeforeRecord, dest.BeforeRecord);
            CloneAction(source.AfterRecord, dest.AfterRecord);
            CloneAction(source.PlayChannels, dest.PlayChannels);
            CloneAction(source.SaveChannels, dest.SaveChannels);
            CloneAction(source.BeforeRecordChannel, dest.BeforeRecordChannel);
            CloneAction(source.AfterRecordChannel, dest.AfterRecordChannel);
            CloneAction(source.PlayAllTapes, dest.PlayAllTapes);
            CloneAction(source.DiskCache, dest.DiskCache);
        }

        public static void CloneAction(TapeAction source, TapeAction dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));
            dest.On = source.On;
            dest.Done = source.Done;
            dest.Callback = source.Callback;
        }
    }
}
