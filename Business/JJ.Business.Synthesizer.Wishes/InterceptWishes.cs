using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Persistence.Synthesizer;
// ReSharper disable ParameterHidesMember

namespace JJ.Business.Synthesizer.Wishes
{
    // Intercept in SynthWishes

    public partial class SynthWishes
    {
        // Instance Intercept (Start-of-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, 
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, null, null, x => { callback(x); return x; }, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, 
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, FlowNode duration,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, duration, null, x => { callback(x); return x; }, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, duration, null, callback, callerMemberName);
                
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, string filePath,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, null, filePath, x => { callback(x); return x; }, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, FlowNode duration, string filePath,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, duration, filePath, x => { callback(x); return x; }, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, FlowNode duration, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(signal, duration, callback, null, filePath, callerMemberName);
            tape.IsIntercept = true;
            return signal;
        }

        // Instance InterceptChannel (Start-of-Chain)
                
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel,
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, null, null, (b, i) => { callback(b, i); return b; }, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, null, null, callback, callerMemberName);
                
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, FlowNode duration, 
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, duration, null, (b, i) => { callback(b, i); return b; }, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, string filePath, 
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, null, filePath, (b, i) => { callback(b, i); return b; }, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, FlowNode duration, string filePath,
            Action<Buff, int> channelCallback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, duration, filePath, (b, i) => { channelCallback(b, i); return b; }, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, FlowNode duration, string filePath,
            Func<Buff, int, Buff> channelCallback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(channel, duration, null, channelCallback, filePath, callerMemberName);
            tape.IsInterceptChannel = true;
            return channel;
        }
    }

    public partial class FlowNode
    {
        // FlowNode Intercept (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode duration,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode duration,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            string filePath,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, null, filePath, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode duration, string filePath,
            Action<Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, duration, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode duration, string filePath,
            Func<Buff, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, duration, filePath, callback, callerMemberName);
        
        // FlowNode InterceptChannel (Mid-Chain)
       
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode duration, 
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode duration, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            string filePath, 
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, null, filePath, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            string filePath, 
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode duration, string filePath,
            Action<Buff, int> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, duration, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode duration, string filePath,
            Func<Buff, int, Buff> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, duration, filePath, callback, callerMemberName);
    }
}
