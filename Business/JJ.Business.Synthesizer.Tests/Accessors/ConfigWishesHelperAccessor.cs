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
    internal class ConfigWishesHelperAccessor : AccessorEx
    {
        public ConfigWishesHelperAccessor(Type type) : base(type) { }
        
        // For ConfigResolver
        
        public ConfigResolverAccessor Set<TValue>(ConfigResolverAccessor obj, TValue value, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            
            return new ConfigResolverAccessor(InvokeMethod(
                callerMemberName,
                new[] { obj.Obj, value },
                new[] { null, typeof(TValue) }));
        }
        
        public ConfigResolverAccessor Set<TValue>(ConfigResolverAccessor obj, TValue value, SynthWishes synthWishes, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            
            return new ConfigResolverAccessor(InvokeMethod(
                callerMemberName,
                new[] { obj.Obj, value, synthWishes },
                new[] { null, typeof(TValue), null }));
        }
        
        public ConfigResolverAccessor Call(ConfigResolverAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return new ConfigResolverAccessor(InvokeMethod(callerMemberName, obj.Obj));
        }
        
        public object Get(ConfigResolverAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod(obj.Obj, callerMemberName);
        }
        
        public T Get<T>(ConfigResolverAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<T>(obj.Obj, callerMemberName);
        }
        
        public T Get<T>(ConfigResolverAccessor obj, SynthWishes synthWishes, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<T>(obj.Obj, synthWishes, callerMemberName);
        }
        
        public bool GetBool(ConfigResolverAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<bool>(obj.Obj, callerMemberName);
        }
        
        public int GetInt(ConfigResolverAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<int>(obj.Obj, callerMemberName);
        }
        
        public int GetInt(ConfigResolverAccessor obj, SynthWishes synthWishes, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<int>(obj.Obj, synthWishes, callerMemberName);
        }
        
        public int? GetNullyInt(ConfigResolverAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<int?>(obj.Obj, callerMemberName);
        }
        
        public string GetString(ConfigResolverAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<string>(obj.Obj, callerMemberName);
        }
        
        public double GetDouble(ConfigResolverAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<double>(obj.Obj, callerMemberName);
        }
        
        public double? GetNullyDouble(ConfigResolverAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<double?>(obj.Obj, callerMemberName);
        }
        
        // For ConfigSection
        
        public T Get<T>(ConfigSectionAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<T>(obj.Obj, callerMemberName);
        }
        
        public bool GetBool(ConfigSectionAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<bool>(obj.Obj, callerMemberName);
        }
        
        public int? GetNullyInt(ConfigSectionAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<int?>(obj.Obj, callerMemberName);
        }
        
        public string GetString(ConfigSectionAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<string>(obj.Obj, callerMemberName);
        }
        
        public double? GetNullyDouble(ConfigSectionAccessor obj, [CallerMemberName] string callerMemberName = null)
        {
            if (obj == null) throw new NullException(() => obj);
            return InvokeMethod<double?>(obj.Obj, callerMemberName);
        }
    }
}