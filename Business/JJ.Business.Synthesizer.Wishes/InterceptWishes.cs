using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

// ReSharper disable ParameterHidesMember

namespace JJ.Business.Synthesizer.Wishes
{
    // Intercept in SynthWishes

    public partial class SynthWishes
    {
        // Instance BeforeRecord (Start-of-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecord(
            FlowNode signal, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => BeforeRecord(signal, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecord(
            FlowNode signal, FlowNode duration,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => BeforeRecord(signal, duration, null, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecord(
            FlowNode signal, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => BeforeRecord(signal, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecord(
            FlowNode signal, FlowNode duration, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrNew(signal, duration, callback, null, null, null, filePath, callerMemberName);
            tape.Actions.BeforeRecord.On = true;
            LogAction(tape, "Update");
            return signal;
        }

        // Instance AfterRecord (Start-of-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecord(
            FlowNode signal, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => AfterRecord(signal, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecord(
            FlowNode signal, FlowNode duration,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => AfterRecord(signal, duration, null, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecord(
            FlowNode signal, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => AfterRecord(signal, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecord(
            FlowNode signal, FlowNode duration, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrNew(signal, duration, null, callback, null, null, filePath, callerMemberName);
            tape.Actions.AfterRecord.On = true;
            LogAction(tape, "Update");
            return signal;
        }

        // Instance BeforeRecordChannel (Start-of-Chain)

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecordChannel(
            FlowNode signal,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => BeforeRecordChannel(signal, null, null, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecordChannel(
            FlowNode signal, FlowNode duration, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => BeforeRecordChannel(signal, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecordChannel(
            FlowNode signal, string filePath, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => BeforeRecordChannel(signal, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecordChannel(
            FlowNode signal, FlowNode duration, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrNew(signal, duration, null, null, callback, null, filePath, callerMemberName);
            tape.Actions.BeforeRecordChannel.On = true;
            LogAction(tape, "Update");
            return signal;
        }
        
        // Instance AfterRecordChannel (Start-of-Chain)

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecordChannel(
            FlowNode signal,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => AfterRecordChannel(signal, null, null, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecordChannel(
            FlowNode signal, FlowNode duration, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => AfterRecordChannel(signal, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecordChannel(
            FlowNode signal, string filePath, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => AfterRecordChannel(signal, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecordChannel(
            FlowNode signal, FlowNode duration, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.GetOrNew(signal, duration, null, null, null, callback, filePath, callerMemberName);
            tape.Actions.AfterRecordChannel.On = true;
            LogAction(tape, "Update");
            return signal;
        }
    }

    public partial class FlowNode
    {
        // FlowNode BeforeRecord (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecord(
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecord(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecord(
            FlowNode duration,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecord(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecord(
            string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecord(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecord(
            FlowNode duration, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecord(this, duration, filePath, callback, callerMemberName);
        
        // FlowNode AfterRecord (Mid-Chain)
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecord(
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecord(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecord(
            FlowNode duration,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecord(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecord(
            string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecord(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecord(
            FlowNode duration, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecord(this, duration, filePath, callback, callerMemberName);

        // FlowNode BeforeRecordChannel (Mid-Chain)
       
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecordChannel(
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecordChannel(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecordChannel(
            FlowNode duration, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecordChannel(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecordChannel(
            string filePath, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecordChannel(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecordChannel(
            FlowNode duration, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecordChannel(this, duration, filePath, callback, callerMemberName);
    
        // FlowNode AfterRecordChannel (Mid-Chain)
       
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecordChannel(
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecordChannel(this, null, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecordChannel(
            FlowNode duration, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecordChannel(this, duration, null, callback, callerMemberName);
        
        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecordChannel(
            string filePath, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecordChannel(this, null, filePath, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecordChannel(
            FlowNode duration, string filePath,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecordChannel(this, duration, filePath, callback, callerMemberName);
    }
}
