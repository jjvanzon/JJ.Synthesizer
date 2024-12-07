using JJ.Persistence.Synthesizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

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
        public Buff MaterializeSave(
            FlowNode channel,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                channel, null,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff MaterializeSave(
            FlowNode channel, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
            => MakeBuff(
                channel, duration,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Save(
            IList<FlowNode> channelSignals,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channelSignals, null,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public Buff Save(
            IList<FlowNode> channelSignals, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                channelSignals, duration,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);

        // Instance Save (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode channel, [CallerMemberName] string filePath = null)
            => SaveBase(channel, null, filePath, null);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode channel, FlowNode duration, [CallerMemberName] string filePath = null)
            => SaveBase(channel, duration, filePath, null);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode channel, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => SaveBase(channel, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode channel, FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => SaveBase(channel, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode channel, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => SaveBase(channel, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode channel, FlowNode duration, string filePath, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => SaveBase(channel, duration, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        private FlowNode SaveBase(
            FlowNode channel, FlowNode duration, string filePath, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(channel, filePath, callerMemberName);
            tape.IsSave = true;
            tape.Duration = duration;
            tape.Callback = callback;
            return channel;
        }
        
        // Instance SaveChannel (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode channel, [CallerMemberName] string filePath = null)
            => SaveChannelBase(channel, null, filePath, null);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode channel, FlowNode duration, [CallerMemberName] string filePath = null)
            => SaveChannelBase(channel, duration, filePath, null);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode channel,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => SaveChannelBase(channel, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode channel, FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => SaveChannelBase(channel, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode channel, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => SaveChannelBase(channel, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode channel, FlowNode duration, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => SaveChannelBase(channel, duration, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        private FlowNode SaveChannelBase(
            FlowNode channel, FlowNode duration, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(channel, filePath, callerMemberName);
            tape.IsSaveChannel = true;
            tape.Duration = duration;
            tape.ChannelCallback = callback;
            return channel;
        }

        // Save in Statics (Buff to Buff) (End-of-Chain)
        
        public static Buff Save(
            Buff buff,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                buff,
                inMemory: false, Default.GetExtraBufferFrames, null, null, filePath, callerMemberName);

        public static Buff Save(
            AudioFileOutput audioFileOutput,
            string filePath = null, [CallerMemberName] string callerMemberName = null) 
            => MakeBuff(
                audioFileOutput,
                inMemory: false, Default.GetExtraBufferFrames, null, null, filePath, callerMemberName);

        public static void Save(
            Sample sample, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            // TODO: Use (some variation of) FetchFilePath.
            string resolvedFilePath = FetchName(sample, callerMemberName, explicitName: filePath);
            Save(sample.Bytes, resolvedFilePath, callerMemberName);
        }
        
        public static void Save(
            byte[] bytes, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            string resolvedFilePath = FetchFilePath(filePath, callerMemberName);
            File.WriteAllBytes(resolvedFilePath, bytes);
            Console.WriteLine($"Output file:{NewLine}'{resolvedFilePath}'");
        }
            
        public static void Save(
            string sourceFilePath, 
            string destFilePath = null, [CallerMemberName] string callerMemberName = null) 
        {
            string resolvedDestFilePath = FetchFilePath(destFilePath, sourceFilePath, callerMemberName: callerMemberName);
            File.Copy(sourceFilePath, resolvedDestFilePath);
            Console.WriteLine($"Output file:{NewLine}'{resolvedDestFilePath}'{NewLine}(Copied from '{sourceFilePath}')");
        }
    }
    
    // Statics Turned Instance (from Buff) (End-of-Chain)

    /// <inheritdoc cref="docs._makebuff" />
    public static class SynthWishesSaveStaticsTurnedInstanceExtensions
    {
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
            [CallerMemberName] string filePath = null)
            => _synthWishes.Save(this, filePath);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode duration, [CallerMemberName] string filePath = null)
            => _synthWishes.Save(this, duration, filePath);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Save(this, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Save(this, duration, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Save(this, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Save(
            FlowNode duration, string filePath, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Save(this, duration, filePath, callback, callerMemberName);

        // FlowNode SaveChannel (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            [CallerMemberName] string filePath = null)
            => _synthWishes.SaveChannel(this, filePath);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode duration, [CallerMemberName] string filePath = null)
            => _synthWishes.SaveChannel(this, duration, filePath);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.SaveChannel(this, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.SaveChannel(this, duration, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.SaveChannel(this, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode SaveChannel(
            FlowNode duration, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.SaveChannel(this, duration, filePath, callback, callerMemberName);

        // Save on FlowNode (End-of-Chain)

        public FlowNode Save(
            Buff buff, 
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            SynthWishes.Save(buff, filePath, callerMemberName);
            return this;
        }
        
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

    // Buff Extensions (End-of-Chain)

    public static class SaveExtensionWishes 
    {
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
