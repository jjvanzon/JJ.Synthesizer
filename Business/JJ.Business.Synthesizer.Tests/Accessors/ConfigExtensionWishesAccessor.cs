using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class BitExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(BitExtensionWishes));
        public static int? Bits(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is8Bit(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is16Bit(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool Is32Bit(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class ChannelsExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(ChannelsExtensionWishes));
        public static int? Channels(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsMono(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsStereo(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class SamplingRateExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(SamplingRateExtensionWishes));
        public static int? SamplingRate(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class InterpolationExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(InterpolationExtensionWishes));
        public static InterpolationTypeEnum? Interpolation(this ConfigSectionAccessor obj) => (InterpolationTypeEnum?)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsLinear(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(),obj.Obj);
        public static bool IsBlocky(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class AudioFormatExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(AudioFormatExtensionWishes));
        public static AudioFileFormatEnum? AudioFormat(this ConfigSectionAccessor obj) => (AudioFileFormatEnum?)_accessor.InvokeMethod(MemberName(), obj.Obj);
        public static bool IsRaw(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(),obj.Obj);
        public static bool IsWav(this ConfigSectionAccessor obj) => (bool)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class CourtesyFrameExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(CourtesyFrameExtensionWishes));
        public static int? CourtesyFrames(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    // Derived Properties
    
    internal static class SizeOfBitDepthExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(SizeOfBitDepthExtensionWishes));
        public static int? SizeOfBitDepth(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class FrameSizeExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(FrameSizeExtensionWishes));
        public static int? FrameSize(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class CourtesyByteExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(CourtesyByteExtensionWishes));
        public static int? CourtesyBytes(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class FileExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(FileExtensionWishes));
        public static string FileExtension(this ConfigSectionAccessor obj) => (string)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class HeaderLengthExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(HeaderLengthExtensionWishes));
        public static int? HeaderLength(this ConfigSectionAccessor obj) => (int?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }

    internal static class MaxAmplitudeExtensionWishesAccessor
    {
        private static readonly Accessor _accessor = new Accessor(typeof(MaxAmplitudeExtensionWishes));
        public static double? MaxAmplitude(this ConfigSectionAccessor obj) => (double?)_accessor.InvokeMethod(MemberName(), obj.Obj);
    }
}
