using JJ.Persistence.Synthesizer;
using System;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes.FilledInWishes;
using static System.IO.File;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_IO_Wishes.FileWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    // Save in SynthWishes

    public partial class SynthWishes
    {
        // SynthWishes Statics (Buff-to-Buff) (End-of-Chain)
        
        public static TapeAction Save(
            TapeAction action, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (action == null) throw new NullException(() => action);
            LogAction(action);
            InternalSave(action.Tape.Buff, action.GetFilePath(filePath, callerMemberName), callerMemberName);
            return action;
        }

        public static Tape Save(
            Tape tape, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (tape == null) throw new NullException(() => tape);
            LogAction(tape, MemberName());
            string filePathResolved = tape.GetFilePath(filePath, callerMemberName);
            InternalSave(tape.Buff, filePathResolved, callerMemberName);
            return tape;
        }
        
        public static Buff Save(
            Buff buff,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (buff == null) throw new ArgumentNullException(nameof(buff));
            LogAction(buff, MemberName());
            string filePathResolved = ResolveFilePath(buff.AudioFormat(), filePath, callerMemberName); // Resolve to use AudioFormat
            buff.FilePath = InternalSave(buff, filePathResolved, callerMemberName);
            return buff;
        }

        public static Buff Save(
            AudioFileOutput audioFileOutput,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (audioFileOutput == null) throw new ArgumentNullException(nameof(audioFileOutput));
            
            LogAction(audioFileOutput, MemberName());
            
            if (Exists(audioFileOutput.FilePath))
            {
                return new Buff
                {
                    FilePath = audioFileOutput.FilePath = InternalSave(audioFileOutput.FilePath, filePath, callerMemberName),
                    UnderlyingAudioFileOutput = audioFileOutput
                };
            }
            else
            {
                return MakeBuffLegacy(
                    audioFileOutput,
                    inMemory: false, ConfigWishes.Static.GetCourtesyFrames, 
                    audioFileOutput.Name, filePath, callerMemberName);
            }
        }

        public static string Save(
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            LogAction(sample, MemberName());
            string resolvedFilePath = ResolveFilePath(sample.AudioFormat(), filePath, ResolveName(sample, callerMemberName));
            return sample.Location = InternalSave(sample.Bytes, resolvedFilePath, callerMemberName);
        }
        
        public static string Save(
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            LogAction("Memory", MemberName());
            return InternalSave(bytes, filePath, callerMemberName);
        }
        
        public static string Save(
            string sourceFilePath, 
            string destFilePath = null, [CallerMemberName] string callerMemberName = null)
        {
            LogAction("File", MemberName());
            return InternalSave(sourceFilePath, destFilePath, callerMemberName);
        }
        
        private static string InternalSave(Buff buff, string destFilePath, string callerMemberName)
        {
            if (FilledIn(buff.Bytes))
            {
                return InternalSave(buff.Bytes, destFilePath, callerMemberName);
            }
            
            if (Exists(buff.FilePath))
            {
                return InternalSave(buff.FilePath, destFilePath, callerMemberName);
            }
             
            throw new Exception("No audio recorded. You could call Record(tape), " +
                                "but this really shouldn't happen if you use the Run command.");
        }
        
        private static string InternalSave(byte[] bytes, string destFilePath, string callerMemberName)
        {
            string resolvedDestFilePath = ResolveFilePath("", destFilePath, callerMemberName);
            
            (string numberedDestFilePath, FileStream fileStream) 
                = CreateSafeFileStream(resolvedDestFilePath, maxExtensionLength: ConfigWishes.Static.GetFileExtensionMaxLength);
            
            using (fileStream)
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }

            LogOutputFile(numberedDestFilePath);
            
            return numberedDestFilePath;
        }

        private static string InternalSave(string sourceFilePath, string destFilePath, string callerMemberName)
        {
            string resolvedDestFilePath = ResolveFilePath("", destFilePath, sourceFilePath, callerMemberName);
            (string numberedDestFilePath, FileStream destStream) 
                = CreateSafeFileStream(resolvedDestFilePath, maxExtensionLength: ConfigWishes.Static.GetFileExtensionMaxLength);
            
            using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (destStream)
            {
                sourceStream.CopyTo(destStream);
            }
            
            LogOutputFile(numberedDestFilePath, sourceFilePath);
            
            return numberedDestFilePath;
        }
        
        // SynthWishes Instance Save (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode signal, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => Save(signal, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode signal, FlowNode duration, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            _tapes.Upsert(ActionEnum.Save, signal, duration, null, filePath, null, callerMemberName);
            return signal;
        }
        
        // SynthWishes Instance SaveChannels (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannels(
            FlowNode signal, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => SaveChannels(signal, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannels(
            FlowNode signal, FlowNode duration, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            _tapes.Upsert(ActionEnum.SaveChannels, signal, duration, null, filePath, null, callerMemberName);
            return signal;
        }
    }
    
    // SynthWishes Statics Turned Instance (Buff-to-Buff) (End-of-Chain)

    /// <inheritdoc cref="docs._makebuff" />
    public static class SynthWishesSaveStaticsTurnedInstanceExtensions
    {
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            Tape tape, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(tape, filePath, callerMemberName);
            return synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }

        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            Buff buff, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(buff, filePath, callerMemberName);
            return synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            AudioFileOutput audioFileOutput, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(audioFileOutput, filePath, callerMemberName);
            return synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }

        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(sample, filePath, callerMemberName);
            return synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        public static SynthWishes Save(
            this SynthWishes synthWishes,
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(bytes, filePath, callerMemberName);
            return synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
    }

    public partial class FlowNode
    {
        // FlowNode Save (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Save(this, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Save(this, duration, filePath, callerMemberName);

        // FlowNode SaveChannels (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannels(
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.SaveChannels(this, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannels(
            FlowNode duration, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.SaveChannels(this, duration, filePath, callerMemberName);

        // FlowNode Save (Buff-to-Buff) (End-of-Chain)

        public FlowNode Save(
            Tape tape, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        { SynthWishes.Save(tape, filePath, callerMemberName); return this; }

        public FlowNode Save(
            Buff buff, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        { SynthWishes.Save(buff, filePath, callerMemberName); return this; }
        
        public FlowNode Save(
            AudioFileOutput audioFileOutput, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        { SynthWishes.Save(audioFileOutput, filePath, callerMemberName); return this; }

        public FlowNode Save(
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        { SynthWishes.Save(sample, filePath, callerMemberName); return this; }
        
        public FlowNode Save(
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        { SynthWishes.Save(bytes, filePath, callerMemberName); return this; }
    }

    // Save Extensions (Buff-to-Buff) (End-of-Chain)

    public static class SaveExtensionWishes 
    {
        public static TapeAction Save(
            this TapeAction tapeAction,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(tapeAction, filePath, callerMemberName);

        public static Tape Save(
            this Tape tape,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(tape, filePath, callerMemberName);
        
        public static Buff Save(
            this Buff buff,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(buff, filePath, callerMemberName);
        
        public static Buff Save(
            this AudioFileOutput audioFileOutput,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(audioFileOutput, filePath, callerMemberName);
        
        public static void Save(
            this Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(sample, filePath, callerMemberName);

        public static void Save(
            this byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(bytes, filePath, callerMemberName);
    }
}
