using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    // Primaries
    
    internal static class BitExtensionWishesAccessor
    {
        private static readonly Type _underlyingType = typeof(BitExtensionWishes);
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(_underlyingType);
        
        // With ConfigResolver
        
        public static int Bits(this ConfigResolverAccessor obj) => (int)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static ConfigResolverAccessor Bits(this ConfigResolverAccessor obj, int? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new[]{ obj.Obj, value }, new[]{ null, typeof(int?) }));
        
        public static bool Is8Bit(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is16Bit(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is32Bit(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);

        public static ConfigResolverAccessor With8Bit(this ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));
        public static ConfigResolverAccessor With16Bit(this ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));
        public static ConfigResolverAccessor With32Bit(this ConfigResolverAccessor obj) => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));

        // With ConfigSection
        
        public static int? Bits(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is8Bit(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is16Bit(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is32Bit(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class ChannelsExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(ChannelsExtensionWishes));
        
        // With ConfigResolver
        
        public static int Channels(this ConfigResolverAccessor obj) => (int)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static ConfigResolverAccessor Channels(this ConfigResolverAccessor obj, int? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new[]{ obj.Obj, value }, new[]{ null, typeof(int?) }));
        public static ConfigResolverAccessor SetChannels(this ConfigResolverAccessor obj, int? channels) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new[]{ obj.Obj, channels }, new[]{ null, typeof(int?) }));

        public static bool IsMono(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsStereo(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static ConfigResolverAccessor Mono(this ConfigResolverAccessor obj) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));
        
        public static ConfigResolverAccessor Stereo(this ConfigResolverAccessor obj) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));

        public static ConfigResolverAccessor SetMono(this ConfigResolverAccessor obj) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));
        
        public static ConfigResolverAccessor SetStereo(this ConfigResolverAccessor obj) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));

        public static ConfigResolverAccessor WithMono(this ConfigResolverAccessor obj) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));
        
        public static ConfigResolverAccessor WithStereo(this ConfigResolverAccessor obj) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));

        // With ConfigSection
        
        public static int? Channels(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsMono(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsStereo(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }
    
    internal static class ChannelExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(ChannelExtensionWishes));
        
        // With ConfigResolver
        
        public static int? Channel(this ConfigResolverAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static ConfigResolverAccessor Channel(this ConfigResolverAccessor obj, int? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new[]{ obj.Obj, value }, new[]{ null, typeof(int?) }));
        
        public static bool IsCenter(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsLeft(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsRight(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static ConfigResolverAccessor Center(this ConfigResolverAccessor obj) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));
        
        public static ConfigResolverAccessor Left(this ConfigResolverAccessor obj) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));
        
        public static ConfigResolverAccessor Right(this ConfigResolverAccessor obj) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));
        
        public static ConfigResolverAccessor NoChannel(this ConfigResolverAccessor obj)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));
    }   
    
    internal static class SamplingRateExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(SamplingRateExtensionWishes));
        
        // With ConfigResolver
        
        public static int SamplingRate(this ConfigResolverAccessor obj) => (int)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static ConfigResolverAccessor SamplingRate(this ConfigResolverAccessor obj, int? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new[]{ obj.Obj, value }, new[]{ null, typeof(int?) }));
        
        // With ConfigSection
        
        public static int? SamplingRate(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class AudioFormatExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(AudioFormatExtensionWishes));

        // With ConfigResolver
        
        public static AudioFileFormatEnum AudioFormat(this ConfigResolverAccessor obj) 
            => (AudioFileFormatEnum)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static ConfigResolverAccessor AudioFormat(this ConfigResolverAccessor obj, AudioFileFormatEnum? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new[]{ obj?.Obj, value }, new[]{ null, typeof(AudioFileFormatEnum?) }));
        
        public static bool IsRaw(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsWav(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        // With ConfigSection
        
        public static AudioFileFormatEnum? AudioFormat(this ConfigSectionAccessor obj) 
            => (AudioFileFormatEnum?)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static bool IsRaw(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsWav(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }
    
    internal static class InterpolationExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(InterpolationExtensionWishes));

        // With ConfigResolver
        
        public static InterpolationTypeEnum Interpolation(this ConfigResolverAccessor obj) 
            => (InterpolationTypeEnum)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static ConfigResolverAccessor Interpolation(this ConfigResolverAccessor obj, InterpolationTypeEnum? value) 
            => new ConfigResolverAccessor(
                _accessor.InvokeMethod(MemberName(), 
                new[]{ obj.Obj, value },
                new[] { null, typeof(InterpolationTypeEnum?) }));
        
        public static bool IsLinear(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(),obj.Obj);
        public static bool IsBlocky(this ConfigResolverAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static ConfigResolverAccessor Linear(this ConfigResolverAccessor obj) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));
        
        public static ConfigResolverAccessor Blocky(this ConfigResolverAccessor obj) 
            =>  new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj));
        
        // With ConfigSection
        
        public static InterpolationTypeEnum? Interpolation(this ConfigSectionAccessor obj) 
            => (InterpolationTypeEnum?)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static bool IsLinear(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(),obj.Obj);
        public static bool IsBlocky(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class CourtesyFrameExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(CourtesyFrameExtensionWishes));
        
        // With ConfigResolver
        
        public static int CourtesyFrames(this ConfigResolverAccessor obj) => (int)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static ConfigResolverAccessor CourtesyFrames(this ConfigResolverAccessor obj, int? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new[]{ obj.Obj, value },
                new[]{ null, typeof(int?) }));
        
        // With ConfigSection
        
        public static int? CourtesyFrames(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    // Derived Properties
    
    internal static class SizeOfBitDepthExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(SizeOfBitDepthExtensionWishes));
        
        // With ConfigResolver
        
        internal static int SizeOfBitDepth(this ConfigResolverAccessor obj) => (int)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        internal static ConfigResolverAccessor SizeOfBitDepth(this ConfigResolverAccessor obj, int? byteSize) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(),
                new[]{ obj.Obj, byteSize },
                new[]{ null, typeof(int?) }));

        // With ConfigSection
        
        public static int? SizeOfBitDepth(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class FrameSizeExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(FrameSizeExtensionWishes));
        
        internal static int  FrameSize(this ConfigResolverAccessor  obj)  => (int)_accessor.InvokeMethod(MemberName(), obj.Obj);
        internal static int? FrameSize(this ConfigSectionAccessor   obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class CourtesyByteExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(CourtesyByteExtensionWishes));

        // With ConfigResolver

        public static int CourtesyBytes(this ConfigResolverAccessor obj) => (int)_accessor.InvokeMethod(MemberName(), obj.Obj);
        
        public static ConfigResolverAccessor CourtesyBytes(this ConfigResolverAccessor obj, int? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj, value));

        // With ConfigSection

        public static int? CourtesyBytes(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class FileExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(FileExtensionWishes));
        
        // With ConfigResolver
        
        internal static string FileExtension(this ConfigResolverAccessor obj) => (string)_accessor.InvokeMethod(MemberName(), obj.Obj);
        internal static ConfigResolverAccessor FileExtension(this ConfigResolverAccessor obj, string value)
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new[] { obj.Obj, value }, new [] { null, typeof(string) }));
        
        // With ConfigSection
        
        public static string FileExtension(this ConfigSectionAccessor obj) => (string)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class HeaderLengthExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(HeaderLengthExtensionWishes));
        
        // With ConfigResolver
        
        public static int? HeaderLength(this ConfigResolverAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static ConfigResolverAccessor HeaderLength(this ConfigResolverAccessor obj, int? value) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj, value));
        
        // With ConfigSection
        
        public static int? HeaderLength(this ConfigSectionAccessor  obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class MaxAmplitudeExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(MaxAmplitudeExtensionWishes));
        
        public static double  MaxAmplitude(this ConfigResolverAccessor obj) => (double )_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static double? MaxAmplitude(this ConfigSectionAccessor  obj) => (double?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }
    
    // Durations
    
    internal static class AudioLengthExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(AudioLengthExtensionWishes));
        
        // With ConfigResolver
        
        public static double AudioLength(this ConfigResolverAccessor obj, SynthWishes synthWishes) 
            => (double)_accessor.InvokeMethod(MemberName(), obj.Obj, synthWishes);
        
        public static ConfigResolverAccessor AudioLength(this ConfigResolverAccessor obj, double? value, SynthWishes synthWishes) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(
                MemberName(), 
                new[]{ obj.Obj, value, synthWishes }, 
                new[]{ null, typeof(double?), null }));
        
        // With ConfigSection
        
        public static double? AudioLength(this ConfigSectionAccessor obj) => (double?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }
        
    internal static class ByteCountExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(ByteCountExtensionWishes));
        
        // with ConfigResolver
        
        public static int ByteCount(this ConfigResolverAccessor obj, SynthWishes synthWishes) 
            => (int)_accessor.InvokeMethod(MemberName(), obj.Obj, synthWishes);
        
        public static ConfigResolverAccessor ByteCount(this ConfigResolverAccessor obj, int? value, SynthWishes synthWishes)  
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), obj.Obj, value, synthWishes));
        
        // With ConfigSection
        
        public static int? ByteCount(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }
        
    internal static class FrameCountExtensionWishesAccessor
    {
        private static readonly Accessor_Adapted _accessor = new Accessor_Adapted(typeof(FrameCountExtensionWishes));

        // With ConfigResolver
        
        public static int FrameCount(this ConfigResolverAccessor obj, SynthWishes synthWishes) 
            => (int)_accessor.InvokeMethod(MemberName(), obj.Obj, synthWishes);
        
        public static ConfigResolverAccessor FrameCount(this ConfigResolverAccessor obj, int? value, SynthWishes synthWishes)  
            => new ConfigResolverAccessor(
                _accessor.InvokeMethod(MemberName(), 
                new[]{ obj.Obj, value, synthWishes },
                new[] { null, typeof(int?), null }));
        
        public static int? FrameCount(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }
}
