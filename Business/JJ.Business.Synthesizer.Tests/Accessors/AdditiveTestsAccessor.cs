using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Functional;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class AdditiveTestsAccessor
    {
        private readonly Accessor            _accessor;
        private readonly SynthWishesAccessor _baseAccessor;

        public AdditiveTestsAccessor(AdditiveTests obj)
        {
            _accessor     = new Accessor(obj);
            _baseAccessor = new SynthWishesAccessor(obj);
        }

        /// <inheritdoc cref="docs._captureindexer" />
        public SynthWishes.CaptureIndexer _ => _baseAccessor._;
        
        /// <inheritdoc cref="docs._metallophone"/>
        public FluentOutlet MetallophoneJingle
            => (FluentOutlet)_accessor.GetPropertyValue(MemberName());

        /// <inheritdoc cref="docs._metallophone"/>
        public FluentOutlet Metallophone(FluentOutlet freq, FluentOutlet duration = null)
            => (FluentOutlet)_accessor.InvokeMethod(MemberName(), freq, duration ?? _[1]);


    }
}
