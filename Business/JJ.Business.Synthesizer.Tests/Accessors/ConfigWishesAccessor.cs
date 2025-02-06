using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal static class ConfigWishesAccessor
    {
        private static ConfigWishesHelperAccessor _accessor = new ConfigWishesHelperAccessor(typeof(ConfigWishes));

        // AudioFormat
        
        internal static bool IsWav(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        internal static bool IsRaw(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        internal static AudioFileFormatEnum AudioFormat(ConfigResolverAccessor obj) => _accessor.Get<AudioFileFormatEnum>(obj);
        internal static AudioFileFormatEnum GetAudioFormat(ConfigResolverAccessor obj) => _accessor.Get<AudioFileFormatEnum>(obj);

        internal static ConfigResolverAccessor WithWav(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor AsWav(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor FromWav(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor ToWav(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor SetWav(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor WithRaw(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor AsRaw(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor FromRaw(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor ToRaw(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor SetRaw(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor AudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor WithAudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor AsAudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor FromAudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor ToAudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor SetAudioFormat(ConfigResolverAccessor obj, AudioFileFormatEnum? value) => _accessor.Set(obj, value);

        internal static bool IsWav(ConfigSectionAccessor obj) => _accessor.GetBool(obj);
        internal static bool IsRaw(ConfigSectionAccessor obj) => _accessor.GetBool(obj);
        internal static AudioFileFormatEnum? AudioFormat(ConfigSectionAccessor obj) => _accessor.Get<AudioFileFormatEnum?>(obj);
        internal static AudioFileFormatEnum? GetAudioFormat(ConfigSectionAccessor obj) => _accessor.Get<AudioFileFormatEnum?>(obj);
        
        // AudioLength
        
        /// <inheritdoc cref="docs._audiolength" />
        internal static FlowNode AudioLength(ConfigResolverAccessor obj, SynthWishes synthWishes) => _accessor.Get<FlowNode>(obj, synthWishes);
        /// <inheritdoc cref="docs._audiolength" />
        internal static FlowNode GetAudioLength(ConfigResolverAccessor obj, SynthWishes synthWishes) => _accessor.Get<FlowNode>(obj, synthWishes);

        /// <inheritdoc cref="docs._audiolength" />
        internal static ConfigResolverAccessor AudioLength(ConfigResolverAccessor obj, double? newLength, SynthWishes synthWishes) => _accessor.Set(obj, newLength, synthWishes);
        /// <inheritdoc cref="docs._audiolength" />
        internal static ConfigResolverAccessor AudioLength(ConfigResolverAccessor obj, FlowNode newLength) => _accessor.Set(obj, newLength);
        /// <inheritdoc cref="docs._audiolength" />
        internal static ConfigResolverAccessor WithAudioLength(ConfigResolverAccessor obj, double? newLength, SynthWishes synthWishes) => _accessor.Set(obj, newLength, synthWishes);
        /// <inheritdoc cref="docs._audiolength" />
        internal static ConfigResolverAccessor WithAudioLength(ConfigResolverAccessor obj, FlowNode newLength) => _accessor.Set(obj, newLength);
        /// <inheritdoc cref="docs._audiolength" />
        internal static ConfigResolverAccessor SetAudioLength(ConfigResolverAccessor obj, double? newLength, SynthWishes synthWishes) => _accessor.Set(obj, newLength, synthWishes);
        /// <inheritdoc cref="docs._audiolength" />
        internal static ConfigResolverAccessor SetAudioLength(ConfigResolverAccessor obj, FlowNode newLength) => _accessor.Set(obj, newLength);

        internal static double? AudioLength(ConfigSectionAccessor obj) => _accessor.GetNullyDouble(obj);
        internal static double? GetAudioLength(ConfigSectionAccessor obj) => _accessor.GetNullyDouble(obj);

        // Bits
        
        internal static bool Is8Bit(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        internal static bool Is16Bit(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        internal static bool Is32Bit(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        internal static int Bits(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        internal static int GetBits(ConfigResolverAccessor obj) => _accessor.GetInt(obj);

        internal static ConfigResolverAccessor With8Bit(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor With16Bit(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor With32Bit(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor As8Bit(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor As16Bit(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor As32Bit(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor Set8Bit(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor Set16Bit(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor Set32Bit(ConfigResolverAccessor obj) => _accessor.Call(obj);
        internal static ConfigResolverAccessor Bits(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor WithBits(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor AsBits(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor SetBits(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);

        internal static bool Is8Bit(ConfigSectionAccessor obj) => _accessor.GetBool(obj);
        internal static bool Is16Bit(ConfigSectionAccessor obj) => _accessor.GetBool(obj);
        internal static bool Is32Bit(ConfigSectionAccessor obj) => _accessor.GetBool(obj);
        internal static int? Bits(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        internal static int? GetBits(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);

        // ByteCount

        public static int ByteCount(ConfigResolverAccessor obj, SynthWishes synthWishes) => _accessor.GetInt(obj, synthWishes);
        public static int GetByteCount(ConfigResolverAccessor obj, SynthWishes synthWishes) => _accessor.GetInt(obj, synthWishes);
        public static ConfigResolverAccessor ByteCount(ConfigResolverAccessor obj, int? value, SynthWishes synthWishes) => _accessor.Set(obj, value, synthWishes);
        public static ConfigResolverAccessor WithByteCount(ConfigResolverAccessor obj, int? value, SynthWishes synthWishes) => _accessor.Set(obj, value, synthWishes);
        public static ConfigResolverAccessor SetByteCount(ConfigResolverAccessor obj, int? value, SynthWishes synthWishes) => _accessor.Set(obj, value, synthWishes);

        public static int? ByteCount(ConfigSectionAccessor obj) => _accessor.InvokeMethod<int?>(obj.Obj);
        public static int? GetByteCount(ConfigSectionAccessor obj) => _accessor.InvokeMethod<int?>(obj.Obj);
        
        // Channel
        
        public static bool IsCenter(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static bool IsLeft(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static bool IsRight(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static int? Channel(ConfigResolverAccessor obj) => _accessor.GetNullyInt(obj);
        public static ConfigResolverAccessor Center(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithCenter(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor AsCenter(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Left(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithLeft(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor AsLeft(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Right(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithRight(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor AsRight(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor NoChannel(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Channel(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor WithChannel(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor AsChannel(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor SetCenter(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetLeft(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetRight(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetNoChannel(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetChannel(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);

        // Channels

        public static bool IsMono(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static bool IsStereo(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static int Channels(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static int GetChannels(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static ConfigResolverAccessor Mono(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Stereo(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Channels(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor WithMono(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithStereo(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithChannels(ConfigResolverAccessor obj, int? channels) => _accessor.Set(obj, channels);
        public static ConfigResolverAccessor AsMono(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor AsStereo(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetMono(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetStereo(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetChannels(ConfigResolverAccessor obj, int? channels) => _accessor.Set(obj, channels);

        public static bool IsMono(ConfigSectionAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
        public static bool IsStereo(ConfigSectionAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
        public static int? Channels(ConfigSectionAccessor obj) => _accessor.InvokeMethod<int?>(obj.Obj);
        public static int? GetChannels(ConfigSectionAccessor obj) => _accessor.InvokeMethod<int?>(obj.Obj);

        // CourtesyBytes

        public static int CourtesyBytes(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static int GetCourtesyBytes(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static ConfigResolverAccessor CourtesyBytes(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor WithCourtesyBytes(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor SetCourtesyBytes(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);

        public static int? CourtesyBytes(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        public static int? GetCourtesyBytes(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    
        // CourtesyFrames
        
        public static int CourtesyFrames(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static int GetCourtesyFrames(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static ConfigResolverAccessor CourtesyFrames(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor WithCourtesyFrames(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor SetCourtesyFrames(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        
        public static int? CourtesyFrames(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        public static int? GetCourtesyFrames(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    
        // FileExtension
        
        internal static string FileExtension(ConfigResolverAccessor obj) => _accessor.GetString(obj);
        internal static string GetFileExtension(ConfigResolverAccessor obj) => _accessor.GetString(obj);
        internal static ConfigResolverAccessor FileExtension(ConfigResolverAccessor obj, string value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor WithFileExtension(ConfigResolverAccessor obj, string value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor AsFileExtension(ConfigResolverAccessor obj, string value) => _accessor.Set(obj, value);
        internal static ConfigResolverAccessor SetFileExtension(ConfigResolverAccessor obj, string value) => _accessor.Set(obj, value);

        public static string FileExtension(ConfigSectionAccessor obj) => _accessor.GetString(obj);
        public static string GetFileExtension(ConfigSectionAccessor obj) => _accessor.GetString(obj);

        // FrameCount

        public static int FrameCount(ConfigResolverAccessor obj, SynthWishes synthWishes) => _accessor.GetInt(obj, synthWishes);
        public static int GetFrameCount(ConfigResolverAccessor obj, SynthWishes synthWishes) => _accessor.GetInt(obj, synthWishes);
        public static ConfigResolverAccessor FrameCount(ConfigResolverAccessor obj, int? value, SynthWishes synthWishes) => _accessor.Set(obj, value, synthWishes);
        public static ConfigResolverAccessor WithFrameCount(ConfigResolverAccessor obj, int? value, SynthWishes synthWishes) => _accessor.Set(obj, value, synthWishes);
        public static ConfigResolverAccessor SetFrameCount(ConfigResolverAccessor obj, int? value, SynthWishes synthWishes) => _accessor.Set(obj, value, synthWishes);

        public static int? FrameCount(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        public static int? GetFrameCount(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    
        // FrameSize
        
        internal static int FrameSize(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        internal static int GetFrameSize(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        
        internal static int? FrameSize(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        internal static int? GetFrameSize(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        
        // HeaderLength
        
        public static int? HeaderLength(ConfigResolverAccessor obj) => _accessor.GetNullyInt(obj);
        public static int? GetHeaderLength(ConfigResolverAccessor obj) => _accessor.GetNullyInt(obj);
        public static ConfigResolverAccessor HeaderLength(ConfigResolverAccessor obj, int? value)  => _accessor.Set(obj, value);
        public static ConfigResolverAccessor WithHeaderLength(ConfigResolverAccessor obj, int? value)  => _accessor.Set(obj, value);
        public static ConfigResolverAccessor SetHeaderLength(ConfigResolverAccessor obj, int? value)  => _accessor.Set(obj, value);
        
        public static int? HeaderLength(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        public static int? GetHeaderLength(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        
        // Interpolation
        
        public static bool IsLinear(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static bool IsBlocky(ConfigResolverAccessor obj) => _accessor.GetBool(obj);
        public static InterpolationTypeEnum Interpolation(ConfigResolverAccessor obj) => _accessor.Get<InterpolationTypeEnum>(obj);
        public static InterpolationTypeEnum GetInterpolation(ConfigResolverAccessor obj) => _accessor.Get<InterpolationTypeEnum>(obj);
        public static ConfigResolverAccessor Linear(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Blocky(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithLinear(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor WithBlocky(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor AsLinear(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor AsBlocky(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetLinear(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor SetBlocky(ConfigResolverAccessor obj) => _accessor.Call(obj);
        public static ConfigResolverAccessor Interpolation(ConfigResolverAccessor obj, InterpolationTypeEnum? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor WithInterpolation(ConfigResolverAccessor obj, InterpolationTypeEnum? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor AsInterpolation(ConfigResolverAccessor obj, InterpolationTypeEnum? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor SetInterpolation(ConfigResolverAccessor obj, InterpolationTypeEnum? value) => _accessor.Set(obj, value);
        
        public static bool IsLinear(ConfigSectionAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
        public static bool IsBlocky(ConfigSectionAccessor obj) => _accessor.InvokeMethod<bool>(obj.Obj);
        public static InterpolationTypeEnum? Interpolation(ConfigSectionAccessor obj) => _accessor.Get<InterpolationTypeEnum?>(obj);
        public static InterpolationTypeEnum? GetInterpolation(ConfigSectionAccessor obj) => _accessor.Get<InterpolationTypeEnum?>(obj);

        // MaxAmplitude
        
        public static double  MaxAmplitude(ConfigResolverAccessor obj) => _accessor.GetDouble(obj);
        public static double  GetMaxAmplitude(ConfigResolverAccessor obj) => _accessor.GetDouble(obj);
        
        public static double? MaxAmplitude(ConfigSectionAccessor obj) => _accessor.GetNullyDouble(obj);
        public static double? GetMaxAmplitude(ConfigSectionAccessor obj) => _accessor.GetNullyDouble(obj);
        
        // SamplingRate
        
        public static int SamplingRate(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static int GetSamplingRate(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        public static ConfigResolverAccessor SamplingRate(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor WithSamplingRate(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);
        public static ConfigResolverAccessor SetSamplingRate(ConfigResolverAccessor obj, int? value) => _accessor.Set(obj, value);

        public static int? SamplingRate(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        public static int? GetSamplingRate(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        
        // SizeOfBitDepth
        
        internal static int SizeOfBitDepth(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        internal static int GetSizeOfBitDepth(ConfigResolverAccessor obj) => _accessor.GetInt(obj);
        internal static ConfigResolverAccessor SizeOfBitDepth(ConfigResolverAccessor obj, int? byteSize) => _accessor.Set(obj, byteSize);
        internal static ConfigResolverAccessor WithSizeOfBitDepth(ConfigResolverAccessor obj, int? byteSize) => _accessor.Set(obj, byteSize);
        internal static ConfigResolverAccessor SetSizeOfBitDepth(ConfigResolverAccessor obj, int? byteSize) => _accessor.Set(obj, byteSize);

        public static int? SizeOfBitDepth(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
        public static int? GetSizeOfBitDepth(ConfigSectionAccessor obj) => _accessor.GetNullyInt(obj);
    }
}
