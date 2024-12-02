using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using static JJ.Business.Synthesizer.Wishes.ConfigResolver;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    // Save in SynthWishes

    public partial class SynthWishes
    {
        // Instance (Start-Of-Chain)

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Save(
            Func<FlowNode> func,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                func, null,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Save(
            Func<FlowNode> func, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                func, duration,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Save(
            FlowNode channel,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channel, null,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Save(
            FlowNode channel, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channel, duration,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Save(
            IList<FlowNode> channels,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channels, null,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Save(
            IList<FlowNode> channels, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channels, duration,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);

        // Instance ChannelSave (Mid-Chain)
        
        public FlowNode ChannelSave(
            FlowNode channel, [CallerMemberName] string filePath = null)
            => ChannelSave(channel, null, filePath, default(Func<Buff, int, Buff>));
        
        public FlowNode ChannelSave(
            FlowNode channel, FlowNode duration, [CallerMemberName] string filePath = null)
            => ChannelSave(channel, duration, filePath, default(Func<Buff, int, Buff>));
        
        public FlowNode ChannelSave(
            FlowNode channel, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => ChannelSave(channel, null, null, callback, callerMemberName);
        
        public FlowNode ChannelSave(
            FlowNode channel, FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => ChannelSave(channel, duration, null, callback, callerMemberName);
        
        public FlowNode ChannelSave(
            FlowNode channel,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => ChannelSave(channel, null, null, callback, callerMemberName);
        
        public FlowNode ChannelSave(
            FlowNode channel, FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => ChannelSave(channel, duration, null, callback, callerMemberName);
        
        public FlowNode ChannelSave(
            FlowNode channel, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => ChannelSave(channel, null, filePath, callback, callerMemberName);
        
        public FlowNode ChannelSave(
            FlowNode channel, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => ChannelSave(channel, null, filePath, callback, callerMemberName);
        
        public FlowNode ChannelSave(
            FlowNode channel, FlowNode duration, string filePath, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => ChannelSave(channel, duration, filePath, (x, y) => callback(x), callerMemberName);

        public FlowNode ChannelSave(
            FlowNode channel, FlowNode duration, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = AddTape(channel, callerMemberName);
            tape.WithSaveChannel = true;
            tape.FilePath = filePath;
            tape.Duration = duration;
            tape.ChannelCallback = callback;
            return channel;
        }

        // Save in Statics (Buff to Buff) (End-of-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public static Buff Save(
            Buff buff,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                buff,
                inMemory: false, Default.GetExtraBufferFrames, null, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public static Buff Save(
            AudioFileOutput audioFileOutput,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                audioFileOutput,
                inMemory: false, Default.GetExtraBufferFrames, null, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public static void Save(
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            // TODO: Use (some variation of) FetchFilePath.
            string resolvedFilePath = FetchName(sample, callerMemberName, explicitName: filePath);
            Save(sample.Bytes, resolvedFilePath, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        public static void Save(
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            // TODO: Use (some variation of) FetchFilePath.
            string resolvedFilePath = FetchName(callerMemberName, explicitName: filePath);
            File.WriteAllBytes(resolvedFilePath, bytes);
        }
    }
    
    // Statics Turned Instance (from Buff) (End-of-Chain)

    /// <inheritdoc cref="docs._makebuff" />
    public static class SynthWishesSaveStaticsTurnedInstanceExtensions
    {
        /// <inheritdoc cref="docs._makebuff" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            Buff buff, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));

            MakeBuff(
                buff, 
                inMemory: false, synthWishes.GetExtraBufferFrames, null, null, filePath, callerMemberName);
            
            return synthWishes;
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            AudioFileOutput audioFileOutput, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            MakeBuff(
                audioFileOutput, 
                inMemory: false, synthWishes.GetExtraBufferFrames, null, null, filePath, callerMemberName);
            
            return synthWishes;
        }

        /// <inheritdoc cref="docs._makebuff" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));

            SynthWishes.Save(sample, filePath, callerMemberName);

            return synthWishes;
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        public static SynthWishes Save(
            this SynthWishes synthWishes,
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            SynthWishes.Save(bytes, filePath, callerMemberName);
            
            return synthWishes;
        }
    }

    public partial class FlowNode
    {
        // ChannelSave (Mid-Chain)
        
        public FlowNode ChannelSave(
            [CallerMemberName] string filePath = null)
            => _synthWishes.ChannelSave(this, filePath);
        
        public FlowNode ChannelSave(
            FlowNode duration, [CallerMemberName] string filePath = null)
            => _synthWishes.ChannelSave(this, duration, filePath);

        public FlowNode ChannelSave(
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.ChannelSave(this, callback, callerMemberName);

        public FlowNode ChannelSave(
            FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.ChannelSave(this, duration, callback, callerMemberName);

        public FlowNode ChannelSave(
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.ChannelSave(this, callback, callerMemberName);

        public FlowNode ChannelSave(
            FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.ChannelSave(this, duration, callback, callerMemberName);

        public FlowNode ChannelSave(
            string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.ChannelSave(this, filePath, callback, callerMemberName);
        
        public FlowNode ChannelSave(
            string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.ChannelSave(this, filePath, callback, callerMemberName);

        public FlowNode ChannelSave(
            FlowNode duration, string filePath, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.ChannelSave(this, duration, filePath, callback, callerMemberName);

        public FlowNode ChannelSave(
            FlowNode duration, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.ChannelSave(this, duration, filePath, callback, callerMemberName);

        // Save on FlowNode (End-of-Chain)

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            Buff buff, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            MakeBuff(
                buff, 
                inMemory: false, GetExtraBufferFrames, null, null, filePath, callerMemberName);

            return this;
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            AudioFileOutput entity, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            MakeBuff(
                entity, 
                inMemory: false, GetExtraBufferFrames, null, null, filePath, callerMemberName);
            
            return this; 
        }

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            Sample entity, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(
                entity, 
                filePath, callerMemberName); 
            
            return this; 
        }
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(
                bytes,
                filePath, callerMemberName); 

            return this; 
        }
    }

    // Buff Extensions (End-of-Chain)

    public static class SaveExtensionWishes 
    {
        /// <inheritdoc cref="docs._makebuff" />
        public static Buff Save(
            this Buff buff,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                buff,
                inMemory: false, Default.GetExtraBufferFrames, null, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public static Buff Save(
            this AudioFileOutput audioFileOutput,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                audioFileOutput,
                inMemory: false, Default.GetExtraBufferFrames, null, null, filePath, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public static void Save(
            this Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(
                sample,
                filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public static void Save(
            this byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(
                bytes, 
                filePath, callerMemberName);
    }
}
