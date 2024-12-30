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
            FlowNode signal, string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => BeforeRecord(signal, null, name, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecord(
            FlowNode signal, FlowNode duration, string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
        {
            _tapes.Upsert(ActionEnum.BeforeRecord, signal, duration, name, null, callback, callerMemberName);
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
            FlowNode signal, string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => AfterRecord(signal, null, name, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecord(
            FlowNode signal, FlowNode duration, string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
        {
            _tapes.Upsert(ActionEnum.AfterRecord, signal, duration, name, null, callback, callerMemberName);
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
            FlowNode signal, string name, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => BeforeRecordChannel(signal, null, name, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecordChannel(
            FlowNode signal, FlowNode duration, string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
        {
            _tapes.Upsert(ActionEnum.BeforeRecordChannel, signal, duration, null, name, callback, callerMemberName);
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
            FlowNode signal, string name, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => AfterRecordChannel(signal, null, name, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecordChannel(
            FlowNode signal, FlowNode duration, string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
        {
            _tapes.Upsert(ActionEnum.AfterRecordChannel, signal, duration, name, null, callback, callerMemberName);
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
            string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecord(this, null, name, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecord(
            FlowNode duration, string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecord(this, duration, name, callback, callerMemberName);
        
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
            string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecord(this, null, name, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecord(
            FlowNode duration, string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecord(this, duration, name, callback, callerMemberName);

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
            string name, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecordChannel(this, null, name, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode BeforeRecordChannel(
            FlowNode duration, string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.BeforeRecordChannel(this, duration, name, callback, callerMemberName);
    
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
            string name, 
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecordChannel(this, null, name, callback, callerMemberName);

        /// <inheritdoc cref="docs._makebuff" />
        public FlowNode AfterRecordChannel(
            FlowNode duration, string name,
            Action<Tape> callback, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.AfterRecordChannel(this, duration, name, callback, callerMemberName);
    }
}
