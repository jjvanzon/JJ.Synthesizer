using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
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
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, FlowNode duration,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, duration, null, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => Intercept(signal, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode signal, FlowNode duration, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrCreate(signal, duration, callback, null, filePath, callerMemberName);
            tape.IsIntercept = true;
            return signal;
        }

        // Instance InterceptChannel (Start-of-Chain)

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, null, null, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, FlowNode duration, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, string filePath, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => InterceptChannel(channel, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode channel, FlowNode duration, string filePath,
            Action<Tape> channelCallback, [CallerMemberName] string callerMemberName = null)
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
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode duration,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode Intercept(
            FlowNode duration, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Intercept(this, duration, filePath, callback, callerMemberName);
        
        // FlowNode InterceptChannel (Mid-Chain)
       
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode duration, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            string filePath, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode InterceptChannel(
            FlowNode duration, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.InterceptChannel(this, duration, filePath, callback, callerMemberName);
    }
}
