using System;
using JJ.Business.Synthesizer.Tests.Functional;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class FMTestsAccessor
    {
        private readonly Accessor _accessor;
        private readonly SynthWishesAccessor _baseAccessor;
            
        public FMTestsAccessor(FMTests obj)
        {
            _accessor     = new Accessor(obj);
            _baseAccessor = new SynthWishesAccessor(obj);
        }

        /// <inheritdoc cref="docs._captureindexer" />
        public SynthWishes _ => _baseAccessor._;
               
        public void Run(Action action)
            => _baseAccessor.Run(action);

        /// <inheritdoc cref="docs._flute1" />
        public FlowNode Flute1(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._flute2" />
        public FlowNode Flute2(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._flute3" />
        public FlowNode Flute3(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._flute4" />
        public FlowNode Flute4(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._default" />
        public FlowNode Pad(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        public FlowNode Organ(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._horn" />
        public FlowNode Horn(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
            
        /// <inheritdoc cref="docs._trombone" />
        public FlowNode Trombone(FlowNode freq = null, FlowNode durationFactor = null)        
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], durationFactor ?? _[1]);
        
        /// <inheritdoc cref="docs._default" />
        public FlowNode ElectricNote(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FlowNode RippleBass(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FlowNode RippleNote_SharpMetallic(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FlowNode RippleSound_Clean(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FlowNode RippleSound_FantasyEffect(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FlowNode RippleSound_CoolDouble(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FlowNode Create_FM_Noise_Beating(FlowNode freq = null, FlowNode duration = null)
            => (FlowNode)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
                        
        public FlowNode FluteMelody1
            => (FlowNode)_accessor.GetPropertyValue(MemberName());
        
        public FlowNode FluteMelody2
            => (FlowNode)_accessor.GetPropertyValue(MemberName());

        public FlowNode OrganChords 
            => (FlowNode)_accessor.GetPropertyValue(MemberName());

        public FlowNode PadChords()
            => (FlowNode)_accessor.InvokeMethod(MemberName());

        public FlowNode PadChords2()
            => (FlowNode)_accessor.InvokeMethod(MemberName());

        public FlowNode HornMelody1       
            => (FlowNode)_accessor.GetPropertyValue(MemberName());

        public FlowNode HornMelody2       
            => (FlowNode)_accessor.GetPropertyValue(MemberName());

        public FlowNode TromboneMelody1   
            => (FlowNode)_accessor.GetPropertyValue(MemberName());

        public FlowNode TromboneMelody2   
            => (FlowNode)_accessor.GetPropertyValue(MemberName());

        public FlowNode RippleBassMelody2 
            => (FlowNode)_accessor.GetPropertyValue(MemberName());

        public FlowNode Jingle()
            => (FlowNode)_accessor.InvokeMethod(MemberName());
    }
}
