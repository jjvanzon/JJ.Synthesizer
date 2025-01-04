using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class ConfigWishesAccessor
    {
        private readonly Accessor _accessor;
        
        public ConfigWishesAccessor(ConfigWishes obj) 
            => _accessor = new Accessor(obj, typeof(ConfigWishes));
        
        public int _channel
        {
            get => (int)_accessor.GetFieldValue(MemberName());
            set => _accessor.SetFieldValue(MemberName(), value);
        }
        
        public ConfigSectionAccessor _section 
            => new ConfigSectionAccessor(_accessor.GetFieldValue(MemberName()));
    }
}
