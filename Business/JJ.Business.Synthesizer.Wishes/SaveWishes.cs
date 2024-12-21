using JJ.Persistence.Synthesizer;
using System;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes.FilledInWishes;
using static System.Environment;
using static System.IO.File;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_IO_Wishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    // Save in SynthWishes

    public partial class SynthWishes
    {
        // SynthWishes Instance Save (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode channel, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => Save(channel, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode channel, FlowNode duration, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(channel, duration, null, null, filePath, callerMemberName);
            tape.IsSave = true;
            return channel;
        }
        
        // SynthWishes Instance SaveChannel (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode channel, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => SaveChannel(channel, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode channel, FlowNode duration, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(channel, duration, null, null, filePath, callerMemberName);
            tape.IsSaveChannel = true;
            return channel;
        }

        // SynthWishes Statics (Buff to Buff) (End-of-Chain)
        
        public static Tape Save(
            Tape tape, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (tape == null) throw new NullException(() => tape);
            
            string filePathResolved2 = tape.GetFilePath(filePath);
            
            if (FilledIn(tape.Bytes))
            {
                Save(tape.Bytes, filePathResolved2, callerMemberName);
                return tape;
            }
            
            if (Exists(tape.FilePathResolved))
            {
                Save(tape.FilePathResolved, filePathResolved2, callerMemberName);
                return tape;
            }
            
            throw new Exception("No audio in either memory or file.");
        }

        public static Buff Save(
            Buff buff,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (buff == null) throw new ArgumentNullException(nameof(buff));

            // Reuse Buff
            string destFilePath = ResolveFilePath(buff.GetAudioFormat, filePath, callerMemberName); // Resolve to use AudioFormat

            if (FilledIn(buff.Bytes))
            {
                Save(buff.Bytes, destFilePath, callerMemberName);
                return buff;
            }
            
            if (Exists(buff.FilePath))
            {
                Save(buff.FilePath, destFilePath, callerMemberName);
                return buff;
            }
            
            throw new Exception("No audio in either memory or file.");
        }
        
        [Obsolete("", true)]
        public static Buff Save(
            AudioFileOutput audioFileOutput,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuffObsoleteExtensions.MakeBuff(
                audioFileOutput,
                inMemory: false, Default.GetExtraBufferFrames, null, null, filePath, callerMemberName);

        public static string Save(
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string resolvedFilePath = ResolveFilePath("", filePath, ResolveName(sample, callerMemberName));
            return Save(sample.Bytes, resolvedFilePath, callerMemberName);
        }
        
        public static string Save(
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string resolvedFilePath = ResolveFilePath("", filePath, callerMemberName);
            
            (string numberedFilePath, FileStream fileStream) = CreateSafeFileStream(resolvedFilePath);
            
            using (fileStream)
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
            
            LogOutputFile(numberedFilePath);
            
            return numberedFilePath;
        }
            
        public static string Save(
            string sourceFilePath, 
            string destFilePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string resolvedDestFilePath = ResolveFilePath("", destFilePath, sourceFilePath, callerMemberName: callerMemberName);
            (string numberedDestFilePath, FileStream destStream) = CreateSafeFileStream(resolvedDestFilePath);
            
            using (var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (destStream)
            {
                sourceStream.CopyTo(destStream);
            } 

            LogOutputFile(numberedDestFilePath, sourceFilePath);
            
            return numberedDestFilePath;
        }
    }
    
    // SynthWishes Statics Turned Instance (from Buff) (End-of-Chain)

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
        
        [Obsolete("", true)]
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

        // FlowNode SaveChannel (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.SaveChannel(this, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode duration, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.SaveChannel(this, duration, filePath, callerMemberName);

        // FlowNode Save (End-of-Chain)

        public FlowNode Save(
            Tape tape, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            SynthWishes.Save(tape, filePath, callerMemberName);
            return this;
        }

        public FlowNode Save(
            Buff buff, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            SynthWishes.Save(buff, filePath, callerMemberName);
            return this;
        }
        
        [Obsolete("", true)]
        public FlowNode Save(
            AudioFileOutput audioFileOutput, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(audioFileOutput, filePath, callerMemberName);
            return this; 
        }

        public FlowNode Save(
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(sample, filePath, callerMemberName); 
            return this; 
        }
        
        public FlowNode Save(
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(bytes, filePath, callerMemberName); 
            return this; 
        }
    }

    // Save Buff Extensions (End-of-Chain)

    public static class SaveExtensionWishes 
    {
        public static Tape Save(
            this Tape tape,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(tape, filePath, callerMemberName);
        
        public static Buff Save(
            this Buff buff,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(buff, filePath, callerMemberName);
        
        [Obsolete("", true)]
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
