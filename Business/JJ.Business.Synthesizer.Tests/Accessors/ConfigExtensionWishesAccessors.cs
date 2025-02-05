using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

// TODO: Rename file to plural -s.

namespace JJ.Business.Synthesizer.Tests.Accessors
{

    // Helper

    /// <summary>
    /// Specialized accessor for easier use with ConfigWishes extensions,
    /// which require interplay between multiple accessors and
    /// otherwise repetitive explicit parameter type specifications.
    /// </summary>
    internal class ConfigExtensionWishesAccessor : AccessorEx
    {
        public ConfigExtensionWishesAccessor(Type type) : base(type) { }

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

    // Primaries

    internal static class BitExtensionWishesAccessor
    {
        private static readonly Type _underlyingType = typeof(BitExtensionWishes);
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(_underlyingType);
        
        // With ConfigResolver

        public static bool Is8Bit(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static bool Is16Bit(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static bool Is32Bit(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static int Bits(this ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static int GetBits(this ConfigResolverAccessor obj) => _accessor.GetInt(obj);

        public static ConfigResolverAccessor With8Bit(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor With16Bit(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor With32Bit(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor As8Bit(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor As16Bit(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor As32Bit(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Set8Bit(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Set16Bit(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Set32Bit(this ConfigResolverAccessor obj) => _accessor.Call(obj);

        public static ConfigResolverAccessor Bits(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor WithBits(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor AsBits(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor SetBits(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);

        // With ConfigSection
        
        public static bool Is8Bit(this ConfigSectionAccessor  obj) => _accessor.GetBool(obj);
        public static bool Is16Bit(this ConfigSectionAccessor obj) => _accessor.GetBool(obj);
        public static bool Is32Bit(this ConfigSectionAccessor obj) => _accessor.GetBool(obj);
        public static int? Bits(this    ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        public static int? GetBits(this ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    }

    internal static class ChannelsExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(ChannelsExtensionWishes));

        // With ConfigResolver

        public static bool IsMono(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static bool IsStereo(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static int Channels(this ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static int GetChannels(this ConfigResolverAccessor obj) => _accessor.GetInt(obj);

        public static ConfigResolverAccessor Mono(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Stereo(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Channels(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor WithMono(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithStereo(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithChannels(this ConfigResolverAccessor obj, int? channels) => _accessor.Set(obj, channels);
        public static ConfigResolverAccessor AsMono(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor AsStereo(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetMono(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetStereo(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetChannels(this ConfigResolverAccessor obj, int? channels) => _accessor.Set(obj, channels);

        // With ConfigSection

        public static bool IsMono(this ConfigSectionAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
        public static bool IsStereo(this ConfigSectionAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
        public static int? Channels(this ConfigSectionAccessor obj) => _accessor.InvokeMethod<int?>(obj.Obj);
        public static int? GetChannels(this ConfigSectionAccessor obj) => _accessor.InvokeMethod<int?>(obj.Obj);
    }

    internal static class ChannelExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(ChannelExtensionWishes));
        
        // With ConfigResolver

        public static bool IsCenter(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static bool IsLeft(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static bool IsRight(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static int? Channel(this ConfigResolverAccessor obj) => _accessor.GetNullyInt(obj);

        public static ConfigResolverAccessor Center(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithCenter(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor AsCenter(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Left(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithLeft(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor AsLeft(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Right(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithRight(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor AsRight(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor NoChannel(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Channel(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor WithChannel(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor AsChannel(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor SetCenter(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetLeft(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetRight(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetNoChannel(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetChannel(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
    }   
    
    internal static class SamplingRateExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(SamplingRateExtensionWishes));
        
        // With ConfigResolver
        
        public static int SamplingRate(this ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static ConfigResolverAccessor SamplingRate(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);

        // With ConfigSection

        public static int? SamplingRate(this ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    }

    internal static class AudioFormatExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(AudioFormatExtensionWishes));

        // With ConfigResolver
        
        public static AudioFileFormatEnum AudioFormat(this ConfigResolverAccessor obj) => _accessor.Get<AudioFileFormatEnum>(obj);
        public static ConfigResolverAccessor AudioFormat(this ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);

        public static bool IsRaw(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static bool IsWav(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        
        // With ConfigSection
        
        public static AudioFileFormatEnum? AudioFormat(this ConfigSectionAccessor obj) => _accessor.Get<AudioFileFormatEnum?>(obj);
        
        public static bool IsRaw(this ConfigSectionAccessor obj) => _accessor.GetBool(obj);
        public static bool IsWav(this ConfigSectionAccessor obj) => _accessor.GetBool(obj);

        public static ConfigResolverAccessor AsRaw(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor AsWav(this ConfigResolverAccessor obj) => _accessor.Call(obj);
    }
    
    internal static class InterpolationExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(InterpolationExtensionWishes));

        // With ConfigResolver
        
        public static InterpolationTypeEnum Interpolation(this ConfigResolverAccessor obj) => _accessor.Get<InterpolationTypeEnum>(obj);
        public static ConfigResolverAccessor Interpolation(this ConfigResolverAccessor obj, InterpolationTypeEnum? value) => _accessor.Set(obj, value);

        public static bool IsLinear(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static bool IsBlocky(this ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        
        public static ConfigResolverAccessor Linear(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Blocky(this ConfigResolverAccessor obj) => _accessor.Call(obj);
        
        // With ConfigSection
        
        public static InterpolationTypeEnum? Interpolation(this ConfigSectionAccessor obj) => _accessor.Get<InterpolationTypeEnum?>(obj);
        
        public static bool IsLinear(this ConfigSectionAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
        public static bool IsBlocky(this ConfigSectionAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
    }

    internal static class CourtesyFrameExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(CourtesyFrameExtensionWishes));
        
        // With ConfigResolver
        
        public static int CourtesyFrames(this ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static ConfigResolverAccessor CourtesyFrames(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        
        // With ConfigSection
        
        public static int? CourtesyFrames(this ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    }

    // Derived Properties
    
    internal static class SizeOfBitDepthExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(SizeOfBitDepthExtensionWishes));
        
        // With ConfigResolver
        
        internal static int SizeOfBitDepth(this ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        internal static ConfigResolverAccessor SizeOfBitDepth(this ConfigResolverAccessor obj, int? byteSize) => _accessor.Set(obj, byteSize);

        // With ConfigSection

        public static int? SizeOfBitDepth(this ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    }

    internal static class FrameSizeExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(FrameSizeExtensionWishes));

        internal static int FrameSize(this ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        internal static int? FrameSize(this ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    }

    internal static class CourtesyByteExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(CourtesyByteExtensionWishes));

        // With ConfigResolver

        public static int CourtesyBytes(this ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        
        public static ConfigResolverAccessor CourtesyBytes(this ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);

        // With ConfigSection

        public static int? CourtesyBytes(this ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    }

    internal static class FileExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(FileExtensionWishes));
        
        // With ConfigResolver
        
        internal static string FileExtension(this ConfigResolverAccessor obj) => _accessor.GetString(obj);
        internal static ConfigResolverAccessor FileExtension(this ConfigResolverAccessor obj, string value) => _accessor.Set(obj, value);

        // With ConfigSection

        public static string FileExtension(this ConfigSectionAccessor obj) => _accessor.GetString(obj);
    }

    internal static class HeaderLengthExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(HeaderLengthExtensionWishes));
        
        // With ConfigResolver
        
        public static int? HeaderLength(this ConfigResolverAccessor obj) => _accessor.GetNullyInt(obj);
        public static ConfigResolverAccessor HeaderLength(this ConfigResolverAccessor obj, int? value)  => _accessor.Set(obj, value);
        
        // With ConfigSection
        
        public static int? HeaderLength(this ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    }

    internal static class MaxAmplitudeExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(MaxAmplitudeExtensionWishes));
        
        public static double  MaxAmplitude(this ConfigResolverAccessor obj) => _accessor.GetDouble(obj);
        public static double? MaxAmplitude(this ConfigSectionAccessor obj) => _accessor.GetNullyDouble(obj);
    }
    
    // Durations
    
    internal static class AudioLengthExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(AudioLengthExtensionWishes));
        
        // With ConfigResolver
        
        public static FlowNode AudioLength(this ConfigResolverAccessor obj, SynthWishes synthWishes)
            => _accessor.Get<FlowNode>(obj, synthWishes);

        public static ConfigResolverAccessor AudioLength(this ConfigResolverAccessor obj, double? value, SynthWishes synthWishes)
            => _accessor.Set(obj, value, synthWishes);

        // With ConfigSection

        public static double? AudioLength(this ConfigSectionAccessor obj) => _accessor.GetNullyDouble(obj);
    }
        
    internal static class ByteCountExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(ByteCountExtensionWishes));
        
        // with ConfigResolver
        
        public static int ByteCount(this ConfigResolverAccessor obj, SynthWishes synthWishes) 
            => _accessor.GetInt(obj, synthWishes);
        
        public static ConfigResolverAccessor ByteCount(this ConfigResolverAccessor obj, int? value, SynthWishes synthWishes)  
            => _accessor.Set(obj, value, synthWishes);
        
        // With ConfigSection
        
        public static int? ByteCount(this ConfigSectionAccessor obj) => _accessor.InvokeMethod<int?>(obj.Obj);
    }
        
    internal static class FrameCountExtensionWishesAccessor
    {
        private static readonly ConfigExtensionWishesAccessor _accessor = new ConfigExtensionWishesAccessor(typeof(FrameCountExtensionWishes));

        // With ConfigResolver
        
        public static int FrameCount(this ConfigResolverAccessor obj, SynthWishes synthWishes) => _accessor.GetInt(obj, synthWishes);

        public static ConfigResolverAccessor FrameCount(this ConfigResolverAccessor obj, int? value, SynthWishes synthWishes)
            => _accessor.Set(obj, value, synthWishes);

        public static int? FrameCount(this ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    }
}
