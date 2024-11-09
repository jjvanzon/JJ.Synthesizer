using JJ.Business.Synthesizer.Tests.Functional;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class FMTestsAccessor
    {
        private readonly Accessor _accessor;
        private readonly Accessor _baseAccessor;
            
        public FMTestsAccessor(FMTests obj)
        {
            _accessor     = new Accessor(obj);
            _baseAccessor = new Accessor(obj, typeof(SynthWishes));
        }

        public SynthWishes.CaptureIndexer _ 
            => (SynthWishes.CaptureIndexer)_baseAccessor.GetFieldValue(nameof(_));
        
        public FluentOutlet FluteMelody1
            => (FluentOutlet)_accessor.GetPropertyValue(nameof(FluteMelody1));

        public FluentOutlet Flute1(FluentOutlet freq = null, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(nameof(Flute1), freq ?? _[440], duration ?? _[1]);

        public FluentOutlet Jingle()
            => (FluentOutlet)_accessor.InvokeMethod(nameof(Jingle));
    }
}
