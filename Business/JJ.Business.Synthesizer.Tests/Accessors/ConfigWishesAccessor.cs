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
    }
}
