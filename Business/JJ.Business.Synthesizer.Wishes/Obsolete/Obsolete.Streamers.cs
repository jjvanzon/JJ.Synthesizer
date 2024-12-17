using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.StreamerObsoleteMessages;

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    internal static class StreamerObsoleteMessages
    {
        public const string ObsoleteMessage =
            "Streaming method obsolete. " +
            "Use mid-chain like .Save() and .Play() instead e.g., " +
            "Run(MySound); void MySound => Sine(A4).Save().Play());";
    }

    // Run

    [Obsolete(ObsoleteMessage, true)]
    internal static class RunObsoleteExtensions
    {
        [Obsolete(ObsoleteMessage, true)]
        public static void Run(this SynthWishes synthWishes, Action action, bool newInstance)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (newInstance)
            {
                synthWishes.RunOnNewInstance(action);
            }
            else
            {
                synthWishes.RunOnThisInstance(action);
            }
        }

        [Obsolete(ObsoleteMessage, true)]
        public static void RunWithRecord(this SynthWishes synthWishes, Action action)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (action == null) throw new ArgumentNullException(nameof(action));

            var dummy = synthWishes[0.5];
            synthWishes.Record(() => { action(); return dummy; });
        }

        [Obsolete(ObsoleteMessage, true)]
        public static void RunWithSave(this SynthWishes synthWishes, Action action)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var dummy = synthWishes[0.5];
            synthWishes.Save(() => { action(); return dummy; });
        }
    }

    // MakeBuff

    [Obsolete(ObsoleteMessage)]
    internal static class MakeBuffObsoleteExtensions
    {
        [Obsolete("Up for deprecation after dependency has been deprecated too.")]
        public static Buff MakeBuff(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            bool inMemory, bool mustPad, IList<string> additionalMessages,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            var channelSignals = synthWishes.GetChannelSignals(func);

            Console.WriteLine("");
            Console.WriteLine(GetConfigLog(synthWishes));
            Console.WriteLine("");

            return synthWishes.MakeBuff(channelSignals, duration, inMemory, mustPad, additionalMessages, name, filePath, callerMemberName);
        }

        [Obsolete(ObsoleteMessage, true)]
        public static Buff MakeBuff(
            this SynthWishes synthWishes,
            FlowNode channel, FlowNode duration,
            bool inMemory, bool mustPad, IList<string> additionalMessages,
            string name, string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                new[] { channel }, duration,
                inMemory, mustPad, additionalMessages, name, filePath, callerMemberName);
        }

    }
    
    // Record
    
    // TODO: Make Internal once last overload is fully deprecated.
    [Obsolete(ObsoleteMessage)]
    public static class RecordObsoleteExtensions
    {
        // Record With Func (Start-of-Chain)
        
        [Obsolete("Up for deprecation. Use this instead: Buff buff = null; Run(() => MySound.Intercept(b => buff = b));")]
        public static Buff Record(
            this SynthWishes synthWishes,
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            
            return synthWishes.MakeBuff(
                func, null,
                inMemory: true, default, null, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                func, duration,
                inMemory: true, default, null, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            Func<FlowNode> func, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                func, null,
                inMemory: true, mustPad, null, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                func, duration,
                inMemory: true, mustPad, null, name, null, callerMemberName);
        }
        
        // Record With FlowNode (Start-of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            FlowNode signal,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                new[] { signal }, null,
                inMemory: true, default, null, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            FlowNode signal, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                new[] { signal }, duration,
                inMemory: true, mustPad, null, name, null, callerMemberName);
        }
        
        // Record With List of FlowNodes (Start-of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                channelSignals, null,
                inMemory: true, default, null, name, null, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration, bool mustPad,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                channelSignals, duration,
                inMemory: true, mustPad, null, name, null, callerMemberName);
        }
    
        // Record on FlowNode (End-of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static FlowNode Record(
            this FlowNode flowNode,
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            
            SynthWishes.MakeBuff(
                buff,
                inMemory: true, flowNode.GetExtraBufferFrames, null, name, null, callerMemberName);

            return flowNode;
        }

        [Obsolete(ObsoleteMessage, true)]
        public static FlowNode Record(
            this FlowNode flowNode,
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (flowNode == null) throw new ArgumentNullException(nameof(flowNode));
            SynthWishes.MakeBuff(
                entity,
                inMemory: true, flowNode.GetExtraBufferFrames, null, name, null, callerMemberName);

            return flowNode;
        }
    
        // Record Buff to Buff Extensions (End-of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.MakeBuff(
                buff,
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            this AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.MakeBuff(
                entity,
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);

    }

    [Obsolete(ObsoleteMessage, true)]
    internal static class RecordObsoleteStatics
    {
        // Record Statics (Buff to Buff) (End-Of-Chain)
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.MakeBuff(
                buff,
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Record(
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
            => SynthWishes.MakeBuff(
                entity,
                inMemory: true, ConfigWishes.Default.GetExtraBufferFrames, null, name, null, callerMemberName);
    }

    [Obsolete(ObsoleteMessage, true)]
    internal static class RecordObsoleteStaticsTurnedInstanceExtensions
    {
        // Record Statics Turned Instance (End-of-Chain)

        // On Buffs (End-of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static SynthWishes Record(
            this SynthWishes synthWishes,
            Buff buff,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null)
                throw new ArgumentNullException(nameof(synthWishes));
            if (buff == null)
                throw new ArgumentNullException(nameof(buff));

            SynthWishes.MakeBuff(
                buff,
                inMemory: true, synthWishes.GetExtraBufferFrames, null, name, null, callerMemberName);

            return synthWishes;
        }

        [Obsolete(ObsoleteMessage, true)]
        public static SynthWishes Record(
            this SynthWishes synthWishes,
            AudioFileOutput entity,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null)
                throw new ArgumentNullException(nameof(synthWishes));
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            SynthWishes.MakeBuff(
                entity,
                inMemory: true, synthWishes.GetExtraBufferFrames, null, name, null, callerMemberName);

            return synthWishes;
        }
    }

    // Save

    [Obsolete(ObsoleteMessage, true)]
    internal static class SaveObsoleteExtensions
    {
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Save(
            this SynthWishes synthWishes,
            Func<FlowNode> func,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                func, null,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Save(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                func, duration,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff MaterializeSave(
            this SynthWishes synthWishes,
            FlowNode channel,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                channel, null,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff MaterializeSave(
            this SynthWishes synthWishes,
            FlowNode channel, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                channel, duration,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Save(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                channelSignals, null,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Save(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration,
            string filePath = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MakeBuff(
                channelSignals, duration,
                inMemory: false, mustPad: true, null, null, filePath, callerMemberName);
        }
    }

    // Play

    [Obsolete(ObsoleteMessage, true)]
    internal static class PlayObsoleteExtensions
    {
        // SynthWishes Instance (Start-Of-Chain)

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Play(
            this SynthWishes synthWishes,
            Func<FlowNode> func,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.Play(func, null, name, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Play(
            this SynthWishes synthWishes,
            Func<FlowNode> func, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            Buff buff =
                synthWishes.MakeBuff(
                    func, duration,
                    inMemory: true, mustPad: true, null, name, null, callerMemberName);

            Buff buff2 = SynthWishes.InternalPlay(synthWishes, buff);

            return buff2;
        }

        [Obsolete(ObsoleteMessage, true)]
        public static Buff MaterializePlay(
            this SynthWishes synthWishes,
            FlowNode channel,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.MaterializePlay(channel, null, name, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff MaterializePlay(
            this SynthWishes synthWishes,
            FlowNode channel, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            Buff buff =
                synthWishes.MakeBuff(
                    channel, duration,
                    inMemory: true, mustPad: true, null, name, null, callerMemberName);

            Buff buff2 = SynthWishes.InternalPlay(synthWishes, buff);

            return buff2;
        }

        [Obsolete(ObsoleteMessage, true)]
        public static Buff Play(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            return synthWishes.Play(channelSignals, null, name, callerMemberName);
        }
        
        [Obsolete(ObsoleteMessage, true)]
        public static Buff Play(
            this SynthWishes synthWishes,
            IList<FlowNode> channelSignals, FlowNode duration,
            string name = null, [CallerMemberName] string callerMemberName = null)
        {
            if (synthWishes == null) throw new ArgumentNullException(nameof(synthWishes));
            Buff buff =
                synthWishes.MakeBuff(
                    channelSignals, duration,
                    inMemory: true, mustPad: true, null, name, null, callerMemberName);

            Buff buff2 = SynthWishes.InternalPlay(synthWishes, buff);

            return buff2;
        }
    }
}
