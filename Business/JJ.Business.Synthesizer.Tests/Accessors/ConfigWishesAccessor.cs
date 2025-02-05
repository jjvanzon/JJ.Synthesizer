using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Reflection;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class ConfigWishesAccessor
    {
        private static ConfigWishesHelperAccessor _accessor = new ConfigWishesHelperAccessor(typeof(ConfigWishes));

        // AudioFormat
        
        internal static bool IsWav(ConfigResolverAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
        internal static bool IsRaw(ConfigResolverAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
        internal static AudioFileFormatEnum AudioFormat(ConfigResolverAccessor obj) => _accessor.InvokeMethod<AudioFileFormatEnum>(obj.Obj);
        internal static AudioFileFormatEnum GetAudioFormat(ConfigResolverAccessor obj) => _accessor.InvokeMethod<AudioFileFormatEnum>(obj.Obj);

        internal static ConfigResolverAccessor WithWav(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor AsWav(ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj));
        internal static ConfigResolverAccessor FromWav(ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj));
        internal static ConfigResolverAccessor ToWav(ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj));
        internal static ConfigResolverAccessor SetWav(ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj));
        internal static ConfigResolverAccessor WithRaw(ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj));
        internal static ConfigResolverAccessor AsRaw(ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj));
        internal static ConfigResolverAccessor FromRaw(ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj));
        internal static ConfigResolverAccessor ToRaw(ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj));
        internal static ConfigResolverAccessor SetRaw(ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(obj.Obj));
        internal static ConfigResolverAccessor AudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor WithAudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor AsAudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor FromAudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor ToAudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor SetAudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);

        internal static bool IsWav(ConfigSectionAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
        internal static bool IsRaw(ConfigSectionAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
        internal static AudioFileFormatEnum? AudioFormat(ConfigSectionAccessor obj) => _accessor.InvokeMethod<AudioFileFormatEnum?>(obj.Obj);
        internal static AudioFileFormatEnum? GetAudioFormat(ConfigSectionAccessor obj) => _accessor.InvokeMethod<AudioFileFormatEnum?>(obj.Obj);
    }
}
