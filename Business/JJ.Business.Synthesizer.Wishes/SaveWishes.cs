using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

namespace JJ.Business.Synthesizer.Wishes
{
    // Save in SynthWishes

    public partial class SynthWishes
    {
        // Instance (Start-Of-Chain)
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            Func<FlowNode> func, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName, explicitName: filePath);
            
            return StreamAudio(
                func, null,
                inMemory: false, mustPad: true, null, name, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            Func<FlowNode> func, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(callerMemberName, explicitName: filePath);
            
            return StreamAudio(
                func, duration,
                inMemory: false, mustPad: true, null, name, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            FlowNode channel, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(channel, callerMemberName, explicitName: filePath);
            
            return StreamAudio(
                channel, null,
                inMemory: false, mustPad: true, null, name, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            FlowNode channel, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(channel, callerMemberName, explicitName: filePath);
            
            return StreamAudio(
                channel, duration,
                inMemory: false, mustPad: true, null, name, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            IList<FlowNode> channels,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(channels, callerMemberName, explicitName: filePath);
            
            return StreamAudio(
                channels, null,
                inMemory: false, mustPad: true, null, name, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public Buff Save(
            IList<FlowNode> channels, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(channels, callerMemberName, explicitName: filePath);
            
            return StreamAudio(
                channels, duration,
                inMemory: false, mustPad: true, null, filePath, callerMemberName);
        }
        
        // Instance ChannelSave
        
        public FlowNode ChannelSave(FlowNode channel)
            => ChannelSave(channel, null, null, default(Action<Buff, int>));
        
        public FlowNode ChannelSave(FlowNode channel, FlowNode duration)
            => ChannelSave(channel, duration, null, default(Action<Buff, int>));
        
        public FlowNode ChannelSave(FlowNode channel, string filePath)
            => ChannelSave(channel, null, filePath, default(Action<Buff, int>));
        
        public FlowNode ChannelSave(FlowNode channel, FlowNode duration, string filePath)
            => ChannelSave(channel, duration, filePath, default(Action<Buff, int>));
        
        public FlowNode ChannelSave(FlowNode channel, Action<Buff> callback)
            => ChannelSave(channel, null, null, callback);
        
        public FlowNode ChannelSave(FlowNode channel, FlowNode duration, Action<Buff> callback)
            => ChannelSave(channel, duration, null, callback);
        
        public FlowNode ChannelSave(FlowNode channel, Action<Buff, int> callback)
            => ChannelSave(channel, null, null, callback);
        
        public FlowNode ChannelSave(FlowNode channel, FlowNode duration, Action<Buff, int> callback)
            => ChannelSave(channel, duration, null, callback);
        
        public FlowNode ChannelSave(FlowNode channel, string filePath, Action<Buff, int> callback)
            => ChannelSave(channel, null, filePath, callback);
        
        public FlowNode ChannelSave(FlowNode channel, string filePath, Action<Buff> callback)
            => ChannelSave(channel, null, filePath, callback);
        
        public FlowNode ChannelSave(FlowNode channel, FlowNode duration, string filePath, Action<Buff> callback)
            => ChannelSave(channel, duration, filePath, (x, y) => callback(x));

        public FlowNode ChannelSave(FlowNode channel, FlowNode duration, string filePath, Action<Buff, int> callback)
        {
            Tape tape = AddTape(channel);
            tape.MustSave = true;
            tape.FilePath = filePath;
            tape.Duration = duration;
            tape.Callback = callback;
            return channel;
        }

        // Save in Statics (Buff to Buff)

        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Save(
            Buff buff,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(buff, callerMemberName, explicitName: filePath);

            return StreamAudio(
                buff,
                inMemory: false, ConfigResolver.Default.GetExtraBufferFrames, null, name, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Save(
            AudioFileOutput audioFileOutput, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(audioFileOutput, callerMemberName, explicitName: filePath);
            
            return StreamAudio(
                audioFileOutput,
                inMemory: false, ConfigResolver.Default.GetExtraBufferFrames, null, name, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static void Save(
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string resolvedFilePath = FetchName(sample, callerMemberName, explicitName: filePath);
            Save(sample.Bytes, resolvedFilePath, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static void Save(
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string resolvedFilePath = FetchName(callerMemberName, explicitName: filePath);
            File.WriteAllBytes(resolvedFilePath, bytes);
        }
    }
    
    // Statics Turned Instance (from Buff)

    /// <inheritdoc cref="docs._saveorplay" />
    public static class SynthWishesSaveStaticsTurnedInstanceExtensions
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            Buff buff, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            string name = FetchName(buff, callerMemberName, explicitName: filePath);

            StreamAudio(
                buff, 
                inMemory: false, synthWishes.GetExtraBufferFrames, null, name, callerMemberName);
            
            return synthWishes;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            AudioFileOutput audioFileOutput, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            string name = FetchName(audioFileOutput, callerMemberName, explicitName: filePath);
            
            StreamAudio(
                audioFileOutput, 
                inMemory: false, synthWishes.GetExtraBufferFrames, null, name, callerMemberName);
            
            return synthWishes;
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Save(
            this SynthWishes synthWishes, 
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            string resolvedFilePath = FetchName(sample, callerMemberName, explicitName: filePath);

            SynthWishes.Save(sample, resolvedFilePath, callerMemberName);

            return synthWishes;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static SynthWishes Save(
            this SynthWishes synthWishes,
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            string resolvedFilePath = FetchName(callerMemberName, explicitName: filePath);
            
            SynthWishes.Save(bytes, resolvedFilePath, callerMemberName);
            
            return synthWishes;
        }
    }

    // Save on FlowNode

    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Save(
            Buff buff, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(buff, callerMemberName, explicitName: filePath);
            
            StreamAudio(
                buff, 
                inMemory: false, GetExtraBufferFrames, null, name, callerMemberName);

            return this;
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Save(
            AudioFileOutput entity, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            string name = FetchName(entity, callerMemberName, explicitName: filePath);
            
            StreamAudio(
                entity, 
                inMemory: false, GetExtraBufferFrames, null, name, callerMemberName);
            
            return this; 
        }

        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Save(
            Sample entity, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(
                entity, 
                filePath, callerMemberName); 
            
            return this; 
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public FlowNode Save(
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            SynthWishes.Save(
                bytes,
                filePath, callerMemberName); 

            return this; 
        }
        
        // ChannelSave
        
        public FlowNode ChannelSave()
            => _synthWishes.ChannelSave(this);
        
        public FlowNode ChannelSave(string filePath)
            => _synthWishes.ChannelSave(this, filePath);
        
        public FlowNode ChannelSave(Action<Buff> callback)
            => _synthWishes.ChannelSave(this, callback);
        
        public FlowNode ChannelSave(Action<Buff, int> callback)
            => _synthWishes.ChannelSave(this, callback);
        
        public FlowNode ChannelSave(string filePath, Action<Buff> callback)
            => _synthWishes.ChannelSave(this, filePath, callback);
        
        public FlowNode ChannelSave(string filePath, Action<Buff, int> callback)
            => _synthWishes.ChannelSave(this, filePath, callback);
    }

    // Buff Extensions

    public static class SaveExtensionWishes 
    {
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Save(
            this Buff buff,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(buff, callerMemberName, explicitName: filePath);
            
            return StreamAudio(
                buff,
                inMemory: false, ConfigResolver.Default.GetExtraBufferFrames, null, name, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static Buff Save(
            this AudioFileOutput audioFileOutput,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string name = FetchName(audioFileOutput, callerMemberName, explicitName: filePath);
            
            return StreamAudio(
                audioFileOutput,
                inMemory: false, ConfigResolver.Default.GetExtraBufferFrames, null, name, callerMemberName);
        }
        
        /// <inheritdoc cref="docs._saveorplay" />
        public static void Save(
            this Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(
                sample,
                filePath, callerMemberName);

        /// <inheritdoc cref="docs._saveorplay" />
        public static void Save(
            this byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => SynthWishes.Save(
                bytes, 
                filePath, callerMemberName);
    }
}
