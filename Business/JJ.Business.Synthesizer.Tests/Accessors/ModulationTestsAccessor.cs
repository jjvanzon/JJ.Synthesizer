using JJ.Business.Synthesizer.Tests.Functional;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class ModulationTestsAccessor
    {
        private readonly Accessor _accessor;
        private readonly SynthWishesAccessor _baseAccessor;
            
        public ModulationTestsAccessor(ModulationTests obj)
        {
            _accessor     = new Accessor(obj);
            _baseAccessor = new SynthWishesAccessor(obj);
        }

        /// <inheritdoc cref="docs._captureindexer" />
        public SynthWishes.CaptureIndexer _ => _baseAccessor._; 

        /// <inheritdoc cref="docs._detunica" />
        public FluentOutlet Detunica1(FluentOutlet freq, FluentOutlet duration = null, FluentOutlet detuneDepth = null, FluentOutlet chorusRate = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1], detuneDepth ?? _[0.8], chorusRate ?? _[0.03]);

        /// <inheritdoc cref="docs._detunica" />
        public FluentOutlet Detunica2(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._detunica" />
        public FluentOutlet Detunica3(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._detunica" />
        public FluentOutlet Detunica4(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._detunica" />
        public FluentOutlet Detunica5(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);
        
        /// <inheritdoc cref="docs._detunica" />
        public FluentOutlet DetunicaBass(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1]);

        /// <inheritdoc cref="docs._detunica" />
        public FluentOutlet DetunicaJingle
            => (FluentOutlet)_accessor.GetPropertyValue(MemberName());

        public FluentOutlet Vibraphase(FluentOutlet freq = null, FluentOutlet duration = null, FluentOutlet depthAdjust1 = null, FluentOutlet depthAdjust2 = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq ?? _[440], duration ?? _[1], depthAdjust1 ?? _[0.005], depthAdjust2 ?? _[0.250]);
        
        public FluentOutlet VibraphaseChord
            => (FluentOutlet)_accessor.GetPropertyValue(MemberName());
    }
}
