using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Wishes.Common;
using static System.IO.File;
using static System.String;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Text.StringWishes;

namespace JJ.Business.Synthesizer.Wishes.Config
{
    public partial class ConfigWishes
    {
        private const string Interpolation = "Interpolation";
        private const string SamplingRate = "SamplingRate";
        
        public static int                  [] ValidBits            { get; } = { 8, 16, 32   };
        public static int                  [] ValidChannels        { get; } = { 1, 2        };
        public static int                  [] ValidChannel         { get; } = { 0, 1        };
        public static AudioFileFormatEnum  [] ValidAudioFormats    { get; } = { Raw, Wav    };
        public static InterpolationTypeEnum[] ValidInterpolations  { get; } = { Line, Block };
        
        public static int                  [] ValidSizesOfBitDepth { get; } = ValidBits        .Select(SizeOfBitDepth).ToArray();
        public static double               [] ValidMaxAmplitudes   { get; } = ValidBits        .Select(MaxAmplitude  ).ToArray();
        public static int                  [] ValidHeaderLengths   { get; } = ValidAudioFormats.Select(HeaderLength  ).ToArray();
        public static string               [] ValidFileExtensions  { get; } = ValidAudioFormats.Select(FileExtension ).ToArray();

        // Primary Audio Properties

        public static int AssertBits(int bits, bool strict = true) => AssertOptions(nameof(Bits), bits, ValidBits, strict);
        public static int? AssertBits(int? bits, bool strict = false) => AssertOptions(nameof(Bits), bits, ValidBits, strict);
        public static int AssertChannels(int channels, bool strict = true) => AssertOptions(nameof(Channels), channels, ValidChannels, strict);
        public static int? AssertChannels(int? channels, bool strict = false) => AssertOptions(nameof(Channels), channels, ValidChannels, strict);
        public static int AssertChannel(int Channel, bool strict = true) => AssertOptions(nameof(Channel), Channel, ValidChannel, strict);
        public static int? AssertChannel(int? Channel, bool strict = false) => AssertOptions(nameof(Channel), Channel, ValidChannel, strict);
        public static int AssertSamplingRate(int samplingRate, bool strict = true) => AssertNullyBottom(nameof(SamplingRate), samplingRate, 0, strict) ?? default;
        public static int? AssertSamplingRate(int? samplingRate, bool strict = false) => AssertNullyBottom(nameof(SamplingRate), samplingRate, 0, strict);
        
        public static int AssertCourtesyFrames(int courtesyFrames, bool strict = true) 
            => AssertNullOrBottom(nameof(CourtesyFrames), courtesyFrames, 0, strict) ?? default;
        
        public static int? AssertCourtesyFrames(int? courtesyFrames, bool strict = false) 
            => AssertNullOrBottom(nameof(CourtesyFrames), courtesyFrames, 0, strict);

        public static AudioFileFormatEnum AssertAudioFormat(AudioFileFormatEnum audioFormat, bool strict = true)
            => AssertOptions(nameof(AudioFormat), audioFormat, ValidAudioFormats, strict);

        public static AudioFileFormatEnum? AssertAudioFormat(AudioFileFormatEnum? audioFormat, bool strict = false)
            => AssertOptions(nameof(AudioFormat), audioFormat, ValidAudioFormats, strict);

        public static InterpolationTypeEnum AssertInterpolation(InterpolationTypeEnum interpolation, bool strict = true)
            => AssertOptions(nameof(Interpolation), interpolation, ValidInterpolations, strict);

        public static InterpolationTypeEnum? AssertInterpolation(InterpolationTypeEnum? interpolation, bool strict = false)
            => AssertOptions(nameof(Interpolation), interpolation, ValidInterpolations, strict);

        // Derived Audio Properties

        public static int AssertSizeOfBitDepth(int sizeOfBitDepth, bool strict = true)
            => AssertOptions(nameof(SizeOfBitDepth), sizeOfBitDepth, ValidSizesOfBitDepth, strict);

        public static int? AssertSizeOfBitDepth(int? sizeOfBitDepth, bool strict = false)
            => AssertOptions(nameof(SizeOfBitDepth), sizeOfBitDepth, ValidSizesOfBitDepth, strict);

        public static double AssertMaxAmplitude(double maxAmplitude, bool strict = true)
            => AssertOptions(nameof(MaxAmplitude), maxAmplitude, ValidMaxAmplitudes, strict);

        public static double? AssertMaxAmplitude(double? maxAmplitude, bool strict = false)
            => AssertOptions(nameof(MaxAmplitude), maxAmplitude, ValidMaxAmplitudes, strict);

        public static int AssertHeaderLength(int headerLength, bool strict = true)
            => AssertOptions(nameof(HeaderLength), headerLength, ValidHeaderLengths, strict);

        public static int AssertFrameSize(int frameSize, bool strict = true)
            => AssertNullyBottom(nameof(FrameSize), frameSize, 1, strict) ?? default;

        public static int? AssertFrameSize(int? frameSize, bool strict = false)
            => AssertNullyBottom(nameof(FrameSize), frameSize, 1, strict);

        public static int AssertCourtesyBytes(int courtesyBytes, bool strict = true)
            => AssertNullOrBottom(nameof(CourtesyBytes), courtesyBytes, 0, strict) ?? default;

        public static int? AssertCourtesyBytes(int? courtesyBytes, bool strict = false)
            => AssertNullOrBottom(nameof(CourtesyBytes), courtesyBytes, 0, strict);

        public static int? AssertHeaderLength(int? headerLength, bool strict = false)
            => AssertOptions(nameof(HeaderLength), headerLength, ValidHeaderLengths, strict);

        public static string AssertFileExtension(string fileExtension, bool strict = true)
            => AssertOptions(nameof(FileExtension), fileExtension, ValidFileExtensions, strict);

        // Durations

        public static double AssertAudioLength(double audioLength, bool strict = true)
            => AssertNullOrBottom(nameof(AudioLength), audioLength, 0.0, strict) ?? default;

        public static double? AssertAudioLength(double? audioLength, bool strict = false)
            => AssertNullOrBottom(nameof(AudioLength), audioLength, 0.0, strict);

        public static int AssertFrameCount(int frameCount, bool strict = true)
            => AssertNullOrBottom(nameof(FrameCount), frameCount, 0, strict) ?? default;

        public static int? AssertFrameCount(int? frameCount, bool strict = false)
            => AssertNullOrBottom(nameof(FrameCount), frameCount, 0, strict);

        public static int AssertByteCount(int byteCount, bool strict = true)
            => AssertNullOrBottom(nameof(ByteCount), byteCount, 0, strict) ?? default;

        public static int? AssertByteCount(int? byteCount, bool strict = false)
            => AssertNullOrBottom(nameof(ByteCount), byteCount, 0, strict);

        // FrameCount and CourtesyFrames

        public static int AssertFrameCountMinusCourtesyFrames(int frameCount, int courtesyFrames, bool strict = true) => AssertFrameCountMinusCourtesyFrames((int?)frameCount, (int?)courtesyFrames, strict) ?? default;
        public static int? AssertFrameCountMinusCourtesyFrames(int? frameCount, int? courtesyFrames, bool strict = false)
        {
            AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames, strict);
            return frameCount - courtesyFrames;
        }
        
        public static void AssertFrameCountWithCourtesyFrames(int  frameCount, int  courtesyFrames, bool strict = true)  => AssertFrameCountWithCourtesyFrames((int?)frameCount, (int?)courtesyFrames, strict);
        public static void AssertFrameCountWithCourtesyFrames(int  frameCount, int? courtesyFrames, bool strict = false) => AssertFrameCountWithCourtesyFrames((int?)frameCount, courtesyFrames,       strict);
        public static void AssertFrameCountWithCourtesyFrames(int? frameCount, int  courtesyFrames, bool strict = false) => AssertFrameCountWithCourtesyFrames(frameCount,       (int?)courtesyFrames, strict);
        public static void AssertFrameCountWithCourtesyFrames(int? frameCount, int? courtesyFrames, bool strict = false)
        {
            AssertFrameCount(frameCount, strict);
            AssertCourtesyFrames(courtesyFrames, strict);

            if (frameCount < courtesyFrames)
            {
                throw new Exception(
                    $"{nameof(FrameCountExtensionWishes.FrameCount)} = {frameCount} " +
                    $"but should be a minimum of {courtesyFrames} {nameof(CourtesyFrames)}.");
            }
        }
        
        // CourtesyBytes with FrameSize
        
        public static int AssertCourtesyBytes(int courtesyBytes, int frameSize, bool strict = true)
        {
            AssertCourtesyBytes(courtesyBytes, strict);
            AssertFrameSize(frameSize, strict);

            if (courtesyBytes % frameSize != 0)
            {
                throw new Exception($"{nameof(CourtesyBytes)} not a multiple of {nameof(FrameSize)}: " +
                                    $"{new{courtesyBytes, frameSize}}");
            }
            
            return courtesyBytes;
        }
        
        // File

        /// <summary> Max file size is 2GB. Returns 0 if file not exists. </summary>
        public static int AssertFileSize(string filePath, bool strict = false)
        {
            if (!strict)
            {
                if (!Has(filePath)) return default;
                if (!Exists(filePath)) return default;
            }
            
            long fileSize = new FileInfo(filePath).Length;
            int maxSize = int.MaxValue;
            
            if (fileSize > maxSize) throw new Exception($"File too large. Max size = {PrettyByteCount(maxSize)}");
            
            return (int)fileSize;
        }
        
        // Generic Assertion Methods

        private static T? AssertOptions<T>(string name, T? value, ICollection<T> validValues, bool strict) where T : struct
        {
            if (value.In(validValues)) return value;
            if (!Has(value) && !strict) return value;
            throw NotSupportedException(name, value, validValues);
        }
        
        private static T AssertOptions<T>(string name, T value, ICollection<T> validValues, bool strict)
        {
            if (value.In(validValues)) return value;
            if (!Has(value) && !strict) return value;
            throw NotSupportedException(name, value, validValues);
        }
        
        private static string AssertOptions(string name, string value, ICollection<string> validValues, bool strict)
        {
            if (value.In(validValues)) return value;
            if (!Has(value) && !strict) return value;
            throw NotSupportedException(name, value, validValues);
        }
        
        private static Exception NotSupportedException<T>(string name, object value, IEnumerable<T> validValues) 
            => new Exception($"{name} = {value} not valid. Supported values: " + Join(", ", validValues));

                        
        private static T? AssertNullyBottom<T>(string name, T? value, T min, bool strict = true)
            where T : struct, IComparable<T>
        {
            if (!strict && !Has(value)) return value;
            if (value.HasValue && value.Value.CompareTo(min) < 0) throw new Exception($"{name} {value} below {min}.");
            return value;
        }
        
        private static T? AssertNullOrBottom<T>(string name, T? value, T min, bool strict = true)
            where T : struct, IComparable<T>
        {
            if (!strict && value == null) return value;
            if (value.HasValue && value.Value.CompareTo(min) < 0) throw new Exception($"{name} {value} below {min}.");
            return value;
        }
    }
    
    public static class ConfigAssertionExtensionWishes
    {
        public static int[] ValidBits(this int bits)
            => ConfigWishes.ValidBits;
        
        public static int[] ValidChannels(this int channels)
            => ConfigWishes.ValidChannels;
        
        public static int[] ValidChannel(this int channel)
            => ConfigWishes.ValidChannel;
        
        public static AudioFileFormatEnum[] ValidAudioFormats(this AudioFileFormatEnum audioFormat)
            => ConfigWishes.ValidAudioFormats;
        
        public static InterpolationTypeEnum[] ValidInterpolations(this InterpolationTypeEnum interpolationType)
            => ConfigWishes.ValidInterpolations;
        
        public static int[] ValidSizesOfBitDepth(this int sizeOfBitDepth)
            => ConfigWishes.ValidSizesOfBitDepth;
        
        public static double[] ValidMaxAmplitudes(this double[] maxAmplitude)
            => ConfigWishes.ValidMaxAmplitudes;
        
        public static int[] ValidHeaderLengths(this int headerLength)
            => ConfigWishes.ValidHeaderLengths;
        
        public static string[] ValidFileExtensions(this string fileExtension)
            => ConfigWishes.ValidFileExtensions;

        // Primary Audio Properties
        
        public static int AssertBits(this int bits, bool strict = true)
            => ConfigWishes.AssertBits(bits, strict);
        
        public static int? AssertBits(this int? bits, bool strict = false)
            => ConfigWishes.AssertBits(bits, strict);
        
        public static int AssertChannels(this int channels, bool strict = true)
            => ConfigWishes.AssertChannels(channels, strict);
        
        public static int? AssertChannels(this int? channels, bool strict = false)
            => ConfigWishes.AssertChannels(channels, strict);
        
        public static int AssertChannel(this int Channel, bool strict = true)
            => ConfigWishes.AssertChannel(Channel, strict);
        
        public static int? AssertChannel(this int? Channel, bool strict = false)
            => ConfigWishes.AssertChannel(Channel, strict);
        
        public static int AssertSamplingRate(this int samplingRate, bool strict = true)
            => ConfigWishes.AssertSamplingRate(samplingRate, strict);
        
        public static int? AssertSamplingRate(this int? samplingRate, bool strict = false)
            => ConfigWishes.AssertSamplingRate(samplingRate, strict);
        
        public static int AssertCourtesyFrames(this int courtesyFrames, bool strict = true)
            => ConfigWishes.AssertCourtesyFrames(courtesyFrames, strict);
        
        public static int? AssertCourtesyFrames(this int? courtesyFrames, bool strict = false)
            => ConfigWishes.AssertCourtesyFrames(courtesyFrames, strict);
        
        public static AudioFileFormatEnum AssertAudioFormat(this AudioFileFormatEnum audioFormat, bool strict = true)
            => ConfigWishes.AssertAudioFormat(audioFormat, strict);
        
        public static AudioFileFormatEnum? AssertAudioFormat(this AudioFileFormatEnum? audioFormat, bool strict = false)
            => ConfigWishes.AssertAudioFormat(audioFormat, strict);

        public static InterpolationTypeEnum AssertInterpolation(this InterpolationTypeEnum interpolation, bool strict = true)
            => ConfigWishes.AssertInterpolation(interpolation, strict);
        
        public static InterpolationTypeEnum? AssertInterpolation(this InterpolationTypeEnum? interpolation, bool strict = false)
            => ConfigWishes.AssertInterpolation(interpolation, strict);
        
        // Derived Audio Properties

        public static int AssertSizeOfBitDepth(this int sizeOfBitDepth, bool strict = true)
            => ConfigWishes.AssertSizeOfBitDepth(sizeOfBitDepth, strict);
        
        public static int? AssertSizeOfBitDepth(this int? sizeOfBitDepth, bool strict = false)
            => ConfigWishes.AssertSizeOfBitDepth(sizeOfBitDepth, strict);
        
        public static double AssertMaxAmplitude(this double maxAmplitude, bool strict = true)
            => ConfigWishes.AssertMaxAmplitude(maxAmplitude, strict);
        
        public static double? AssertMaxAmplitude(this double? maxAmplitude, bool strict = false)
            => ConfigWishes.AssertMaxAmplitude(maxAmplitude, strict);
        
        public static int AssertHeaderLength(this int headerLength, bool strict = true)
            => ConfigWishes.AssertHeaderLength(headerLength, strict);
        
        public static int AssertFrameSize(this int frameSize, bool strict = true)
            => ConfigWishes.AssertFrameSize(frameSize, strict);

        public static int? AssertFrameSize(this int? frameSize, bool strict = false)
            => ConfigWishes.AssertFrameSize(frameSize, strict);
        
        public static int AssertCourtesyBytes(this int courtesyBytes, bool strict = true)
            => ConfigWishes.AssertCourtesyBytes(courtesyBytes, strict);
        
        public static int? AssertCourtesyBytes(this int? courtesyBytes, bool strict = false)
            => ConfigWishes.AssertCourtesyBytes(courtesyBytes, strict);
        
        public static int? AssertHeaderLength(this int? headerLength, bool strict = false)
            => ConfigWishes.AssertHeaderLength(headerLength, strict);
        
        public static string AssertFileExtension(this string fileExtension, bool strict = true)
            => ConfigWishes.AssertFileExtension(fileExtension, strict);

        // Durations

        public static double AssertAudioLength(this double audioLength, bool strict = true)
            => ConfigWishes.AssertAudioLength(audioLength, strict);

        public static double? AssertAudioLength(this double? audioLength, bool strict = false)
            => ConfigWishes.AssertAudioLength(audioLength, strict);

        public static int AssertFrameCount(this int frameCount, bool strict = true)
            => ConfigWishes.AssertFrameCount(frameCount, strict);

        public static int? AssertFrameCount(this int? frameCount, bool strict = false)
            => ConfigWishes.AssertFrameCount(frameCount, strict);

        public static int AssertByteCount(this int byteCount, bool strict = true)
            => ConfigWishes.AssertByteCount(byteCount, strict);

        public static int? AssertByteCount(this int? byteCount, bool strict = false)
            => ConfigWishes.AssertByteCount(byteCount, strict);
        
        // FrameCount and CourtesyFrames

        public static int AssertFrameCountMinusCourtesyFrames(this int frameCount, int courtesyFrames, bool strict = true)
            => ConfigWishes.AssertFrameCountMinusCourtesyFrames(frameCount, courtesyFrames, strict);
        
        public static int? AssertFrameCountMinusCourtesyFrames(this int? frameCount, int? courtesyFrames, bool strict = false)
            => ConfigWishes.AssertFrameCountMinusCourtesyFrames(frameCount, courtesyFrames, strict);
        
        public static void AssertFrameCountWithCourtesyFrames(this int  frameCount, int  courtesyFrames, bool strict = true)
            => ConfigWishes.AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames, strict);
        
        public static void AssertFrameCountWithCourtesyFrames(this int  frameCount, int? courtesyFrames, bool strict = false)
            => ConfigWishes.AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames, strict);
        
        public static void AssertFrameCountWithCourtesyFrames(this int? frameCount, int  courtesyFrames, bool strict = false)
            => ConfigWishes.AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames, strict);
        
        public static void AssertFrameCountWithCourtesyFrames(this int? frameCount, int? courtesyFrames, bool strict = false)
            => ConfigWishes.AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames, strict);

        // CourtesyBytes with FrameSize

        public static int AssertCourtesyBytes(this int courtesyBytes, int frameSize, bool strict = true)
            => ConfigWishes.AssertCourtesyBytes(courtesyBytes, frameSize, strict);

        // File

        /// <summary> Max file size is 2GB. Returns 0 if file not exists. </summary>
        public static int AssertFileSize(this string filePath, bool strict = false)
            => ConfigWishes.AssertFileSize(filePath, strict);
    }
}
