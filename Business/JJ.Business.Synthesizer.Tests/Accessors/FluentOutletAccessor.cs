using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class FluentOutletAccessor
    {
        private readonly Accessor _accessor;
        
        public FluentOutletAccessor(FluentOutlet obj)
        {
            _accessor = new Accessor(obj);
        }

        public string DebuggerDisplay => (string)_accessor.GetPropertyValue(MemberName());
    }
}
