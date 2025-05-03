using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using JJ.Framework.Reflection.Core;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    /// <summary>
    /// Specialized accessor for easier use with ConfigWishes extensions,
    /// which require interplay between multiple accessors and
    /// otherwise repetitive explicit parameter type specifications.
    /// </summary>
    internal class ConfigWishesHelperAccessor : AccessorCore
    {
        public ConfigWishesHelperAccessor(Type type) : base(type) { }
        
        // For ConfigResolver
        
        public ConfigResolverAccessor Set<TValue>(ConfigResolverAccessor obj, TValue value, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            
            return new ConfigResolverAccessor(Call(
                name,
                new[] { obj.Obj, value },
                new[] { null, typeof(TValue) }));
        }
        
        public ConfigResolverAccessor Set<TValue>(ConfigResolverAccessor obj, TValue value, SynthWishes synthWishes, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            
            return new ConfigResolverAccessor(Call(
                name,
                new[] { obj.Obj, value, synthWishes },
                new[] { null, typeof(TValue), null }));
        }
        
        public ConfigResolverAccessor Call(ConfigResolverAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return new ConfigResolverAccessor(Call(name, obj.Obj));
        }
        
        public object Get(ConfigResolverAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return Call(obj.Obj, name);
        }
        
        public T Get<T>(ConfigResolverAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (T)Call(obj.Obj, name);
        }
        
        public T Get<T>(ConfigResolverAccessor obj, SynthWishes synthWishes, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (T)Call(obj.Obj, synthWishes, name);
        }
        
        public bool GetBool(ConfigResolverAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (bool)Call(obj.Obj, name);
        }
        
        public int GetInt(ConfigResolverAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (int)Call(obj.Obj, name);
        }
        
        public int GetInt(ConfigResolverAccessor obj, SynthWishes synthWishes, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (int)Call(obj.Obj, synthWishes, name);
        }
        
        public int? GetNullyInt(ConfigResolverAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (int?)Call(obj.Obj, name);
        }
        
        public string GetString(ConfigResolverAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (string)Call(obj.Obj, name);
        }
        
        public double GetDouble(ConfigResolverAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (double)Call(obj.Obj, name);
        }
        
        public double? GetNullyDouble(ConfigResolverAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (double?)Call(obj.Obj, name);
        }
        
        // For ConfigSection
        
        public T Get<T>(ConfigSectionAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (T)Call(obj.Obj, name);
        }
        
        public bool GetBool(ConfigSectionAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (bool)Call(obj.Obj, name);
        }
        
        public int? GetNullyInt(ConfigSectionAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (int?)Call(obj.Obj, name);
        }
        
        public string GetString(ConfigSectionAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (string)Call(obj.Obj, name);
        }
        
        public double? GetNullyDouble(ConfigSectionAccessor obj, [CallerMemberName] string name = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return (double?)Call(obj.Obj, name);
        }
    }
}