using JJ.Business.Synthesizer.Tests.Functional;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

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
        public SynthWishes.CaptureIndexer _ => _baseAccessor._;

        /// <inheritdoc cref="docs._flute1" />
        public FluentOutlet Flute1(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._flute2" />
        public FluentOutlet Flute2(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._flute3" />
        public FluentOutlet Flute3(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._flute4" />
        public FluentOutlet Flute4(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._default" />
        public FluentOutlet Pad(FluentOutlet delay = null, FluentOutlet freq = null, FluentOutlet volume = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), delay ?? _[0], freq ?? _[440], volume ?? _[1], duration ?? _[1]);
        
        public FluentOutlet Organ(FluentOutlet delay = null, FluentOutlet freq = null, FluentOutlet volume = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), delay ?? _[0], freq ?? _[440], volume ?? _[1], duration ?? _[1]);

        /// <inheritdoc cref="docs._horn" />
        public FluentOutlet Horn(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
            
        /// <inheritdoc cref="docs._trombone" />
        public FluentOutlet Trombone(FluentOutlet freq = null, FluentOutlet durationFactor = null)        
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], durationFactor ?? _[1]);
        
        /// <inheritdoc cref="docs._default" />
        public FluentOutlet ElectricNote(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FluentOutlet RippleBass(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FluentOutlet RippleNote_SharpMetallic(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FluentOutlet RippleSound_Clean(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FluentOutlet RippleSound_FantasyEffect(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FluentOutlet RippleSound_CoolDouble(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._ripplebass" />
        public FluentOutlet Create_FM_Noise_Beating(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
                        
        public FluentOutlet FluteMelody1
            => (FluentOutlet)_accessor.GetPropertyValue(MemberName());
        
        public FluentOutlet FluteMelody2
            => (FluentOutlet)_accessor.GetPropertyValue(MemberName());

        public FluentOutlet OrganChords 
            => (FluentOutlet)_accessor.GetPropertyValue(MemberName());

        public FluentOutlet PadChords(FluentOutlet volume = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), volume ?? _[1]);

        public FluentOutlet HornMelody1       
            => (FluentOutlet)_accessor.GetPropertyValue(MemberName());

        public FluentOutlet HornMelody2       
            => (FluentOutlet)_accessor.GetPropertyValue(MemberName());

        public FluentOutlet TromboneMelody1   
            => (FluentOutlet)_accessor.GetPropertyValue(MemberName());

        public FluentOutlet TromboneMelody2   
            => (FluentOutlet)_accessor.GetPropertyValue(MemberName());

        public FluentOutlet RippleBassMelody2 
            => (FluentOutlet)_accessor.GetPropertyValue(MemberName());

        public FluentOutlet Jingle()
            => (FluentOutlet)_accessor.InvokeMethod(MemberName());
    }
}
