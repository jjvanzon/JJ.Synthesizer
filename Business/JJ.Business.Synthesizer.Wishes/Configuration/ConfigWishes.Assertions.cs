using System;
using System.Collections;
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

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    public partial class ConfigWishes
    {
        private const string Channels = "Channels";
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

        public static double  AssertAudioLength(double  audioLength, bool strict = true)  => AssertNullOrBottom(nameof(AudioLength), audioLength, 0.0, strict) ?? default;
        public static double? AssertAudioLength(double? audioLength, bool strict = false) => AssertNullOrBottom(nameof(AudioLength), audioLength, 0.0, strict);
        public static int     AssertFrameCount(int      frameCount,  bool strict = true)  => AssertNullOrBottom(nameof(FrameCount), frameCount, 0, strict) ?? default;
        public static int?    AssertFrameCount(int?     frameCount,  bool strict = false) => AssertNullOrBottom(nameof(FrameCount), frameCount, 0, strict);
        public static int     AssertByteCount(int       byteCount,   bool strict = true)  => AssertNullOrBottom(nameof(ByteCount), byteCount, 0, strict) ?? default;
        public static int?    AssertByteCount(int?      byteCount,   bool strict = false) => AssertNullOrBottom(nameof(ByteCount), byteCount, 0, strict);

        // Misc

        // TODO: Consider what to do about the strict flags here.

        public static int? AssertFrameCountMinusCourtesyFrames(int? frameCount, int courtesyFrames)
        {
            AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames);
            return frameCount - courtesyFrames;
        }
        
        public static void AssertFrameCountWithCourtesyFrames(int? frameCount, int courtesyFrames)
        {
            if (frameCount == null) return;
            AssertFrameCountWithCourtesyFrames(frameCount.Value, courtesyFrames);
        }        

        public static int AssertFrameCountMinusCourtesyFrames(int frameCount, int courtesyFrames)
        {
            AssertFrameCountWithCourtesyFrames(frameCount, courtesyFrames);
            return frameCount - courtesyFrames;
        }
        
        public static void AssertFrameCountWithCourtesyFrames(int frameCount, int courtesyFrames)
        {
            AssertFrameCount(frameCount);
            AssertCourtesyFrames(courtesyFrames);
            
            if (frameCount < courtesyFrames)
            {
                throw new Exception(
                    $"{nameof(FrameCountExtensionWishes.FrameCount)} = {frameCount} " +
                    $"but should be a minimum of {courtesyFrames} {nameof(CourtesyFrames)}.");
            }
        }
        
        public static int AssertCourtesyBytes(int courtesyBytes, int frameSize)
        {
            AssertCourtesyBytes(courtesyBytes);
            AssertFrameSize(frameSize);

            if (courtesyBytes % frameSize != 0)
            {
                throw new Exception($"{nameof(CourtesyBytes)} not a multiple of {nameof(FrameSize)}: " +
                                    $"{new{courtesyBytes, frameSize}}");
            }
            
            return courtesyBytes;
        }

        /// <summary> Max file size is 2GB. Returns 0 if file not exists. </summary>
        public static int AssertFileSize(string filePath)
        {
            if (Exists(filePath))
            {
                long fileSize = new FileInfo(filePath).Length;
                int maxSize = int.MaxValue;
                if (fileSize > maxSize) throw new Exception($"File too large. Max size = {PrettyByteCount(maxSize)}");
                return (int)fileSize;
            }
            
            return 0;
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
        // Primary Audio Properties

        public static int AssertBits(this int bits)
            => ConfigWishes.AssertBits(bits);

        public static int? AssertBits(this int? bits)
            => ConfigWishes.AssertBits(bits);

        public static int AssertChannels(this int channels)
            => ConfigWishes.AssertChannels(channels);

        public static int? AssertChannels(this int? channels)
            => ConfigWishes.AssertChannels(channels);

        public static int AssertChannel(this int channel)
            => ConfigWishes.AssertChannel(channel);

        public static int? AssertChannel(this int? channel)
            => ConfigWishes.AssertChannel(channel);

        public static int AssertSamplingRate(this int samplingRate)
            => ConfigWishes.AssertSamplingRate(samplingRate);

        public static int? AssertSamplingRate(this int? samplingRate)
            => ConfigWishes.AssertSamplingRate(samplingRate);

        public static AudioFileFormatEnum AssertAudioFormat(this AudioFileFormatEnum audioFormat)
            => ConfigWishes.AssertAudioFormat(audioFormat);

        public static AudioFileFormatEnum? AssertAudioFormat(this AudioFileFormatEnum? audioFormat)
            => ConfigWishes.AssertAudioFormat(audioFormat);

        public static InterpolationTypeEnum AssertInterpolation(this InterpolationTypeEnum interpolation)
            => ConfigWishes.AssertInterpolation(interpolation);

        public static InterpolationTypeEnum? AssertInterpolation(this InterpolationTypeEnum? interpolation)
            => ConfigWishes.AssertInterpolation(interpolation);

        public static int AssertCourtesyFrames(this int courtesyFrames)
            => ConfigWishes.AssertCourtesyFrames(courtesyFrames);

        public static int? AssertCourtesyFrames(this int? courtesyFrames)
            => ConfigWishes.AssertCourtesyFrames(courtesyFrames);


        // Derived

        public static int AssertSizeOfBitDepth(this int sizeOfBitDepth)
            => ConfigWishes.AssertSizeOfBitDepth(sizeOfBitDepth);

        public static int? AssertSizeOfBitDepth(this int? sizeOfBitDepth)
            => ConfigWishes.AssertSizeOfBitDepth(sizeOfBitDepth);

        public static double AssertMaxAmplitude(this double maxAmplitude)
            => ConfigWishes.AssertMaxAmplitude(maxAmplitude);

        public static double? AssertMaxAmplitude(this double? maxAmplitude)
            => ConfigWishes.AssertMaxAmplitude(maxAmplitude);

        public static int AssertFrameSize(this int frameSize)
            => ConfigWishes.AssertFrameSize(frameSize);

        public static int? AssertFrameSize(this int? frameSize)
            => ConfigWishes.AssertFrameSize(frameSize);

        public static int AssertHeaderLength(this int headerLength)
            => ConfigWishes.AssertHeaderLength(headerLength);

        public static int? AssertHeaderLength(this int? headerLength)
            => ConfigWishes.AssertHeaderLength(headerLength);

        public static string AssertFileExtension(this string fileExtension)
            => ConfigWishes.AssertFileExtension(fileExtension);

        public static int AssertCourtesyBytes(this int courtesyBytes)
            => ConfigWishes.AssertCourtesyBytes(courtesyBytes);

        public static int? AssertCourtesyBytes(this int? courtesyBytes)
            => ConfigWishes.AssertCourtesyBytes(courtesyBytes);

        // Durations
        
        public static double AssertAudioLength(this double audioLength)
            => ConfigWishes.AssertAudioLength(audioLength);
        
        public static double? AssertAudioLength(this double? audioLength)
            => ConfigWishes.AssertAudioLength(audioLength);
        
        public static int AssertFrameCount(this int frameCount)
            => ConfigWishes.AssertFrameCount(frameCount);
        
        public static int? AssertFrameCount(this int? frameCount)
            => ConfigWishes.AssertFrameCount(frameCount);
        
        public static int AssertByteCount(this int byteCount)
            => ConfigWishes.AssertByteCount(byteCount);
        
        public static int? AssertByteCount(this int? byteCount)
            => ConfigWishes.AssertByteCount(byteCount);

        // Misc
        
        public static int AssertCourtesyBytes(this int courtesyBytes, int frameSize) 
            => ConfigWishes.AssertCourtesyBytes(courtesyBytes, frameSize);
        
        public static int AssertFileSize(this string filePath) 
            => ConfigWishes.AssertFileSize(filePath);
    }
}
