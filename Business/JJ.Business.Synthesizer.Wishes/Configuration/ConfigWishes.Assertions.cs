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

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    public partial class ConfigWishes
    {
        public static int?                 [] ValidBits            { get; } = { 8, 16, 32                                          };
        public static int                  [] ValidChannels        { get; } = { 1, 2                                               };
        public static int                  [] ValidChannel         { get; } = { 0, 1                                               };
        public static AudioFileFormatEnum  [] ValidAudioFormats    { get; } = { Raw, Wav                                           };
        public static InterpolationTypeEnum[] ValidInterpolations  { get; } = { Line, Block                                        };
        public static int?                 [] ValidSizesOfBitDepth { get; } =   ValidBits        .Select(SizeOfBitDepth).ToArray()  ;
        public static double?              [] ValidMaxAmplitudes   { get; } =   ValidBits        .Select(MaxAmplitude  ).ToArray()  ;
        public static int                  [] ValidHeaderLengths   { get; } =   ValidAudioFormats.Select(HeaderLength  ).ToArray()  ;
        public static string               [] ValidFileExtensions  { get; } =   ValidAudioFormats.Select(FileExtension ).ToArray()  ;

        // Piloting Strict Flags
        
        private static int? AssertValue(int? value, ICollection<int> validValues, string name, bool strict)
        {
            if (value.In(validValues)) return value;
            
            if (!Has(value) && !strict)
            {
                return value;
            }
            
            throw new Exception($"{name} = {value} not supported. Supported values: " + Join(", ", validValues));
        }

        
        public static int AssertSizeOfBitDepth(int sizeOfBitDepth, bool strict = true)
            => AssertSizeOfBitDepth((int?)sizeOfBitDepth, strict) ?? default;
            
        public static int? AssertSizeOfBitDepth(int? sizeOfBitDepth, bool strict = false)
        {
            if (sizeOfBitDepth.In(ValidSizesOfBitDepth)) return sizeOfBitDepth;
            
            if (!Has(sizeOfBitDepth) && !strict)
            {
                return sizeOfBitDepth;
            }
            
            throw new Exception($"{nameof(SizeOfBitDepth)} = {sizeOfBitDepth} not supported. Supported values: " + Join(", ", ValidSizesOfBitDepth));
        }
        
        public static int AssertBits(int bits, bool strict = true)
            => AssertBits((int?)bits, strict) ?? default;
        
        public static int? AssertBits(int? bits, bool strict = false)
        {
            if (bits.In(ValidBits)) return bits;
            
            if (!Has(bits) && !strict)
            {
                return bits;
            }
            
            throw new Exception($"{nameof(Bits)} = {bits} not valid. Supported values: " + Join(", ", ValidBits));
        }

        public static int AssertChannels(int channels, bool strict = true)
        {
            return channels.In(ValidChannels) ? channels : throw new Exception($"{nameof(ChannelsExtensionWishes.Channels)} = {channels} not valid. Supported values: " + Join(", ", ValidChannels));
        }
        
        public static int? AssertChannels(int? channels, bool strict = false)
        {
            return !Has(channels) ? channels : AssertChannels(channels.Value);
        }
        
                
        // Primary Audio Properties

        public static int      AssertChannel       (int     channel                     , bool strict = true ) => channel       .In(ValidChannel        ) ? channel        : throw new Exception($"{nameof(ChannelExtensionWishes.Channel)} = {channel} not valid. Supported values: " + Join(", ", ValidChannel));
        public static int    ? AssertChannel       (int   ? channel                     , bool strict = false) => channel                                 ?.                 AssertChannel();
        public static int      AssertSamplingRate  (int     samplingRate                , bool strict = true ) => samplingRate                        > 0 ? samplingRate   : throw new Exception($"{nameof(SamplingRateExtensionWishes.SamplingRate)} {samplingRate} below 0.");
        public static int    ? AssertSamplingRate  (int   ? samplingRate                , bool strict = false) => !Has(samplingRate)                      ? samplingRate   : AssertSamplingRate(samplingRate.Value);
        public static int      AssertCourtesyFrames(int     courtesyFrames              , bool strict = true ) => courtesyFrames                     >= 0 ? courtesyFrames : throw new Exception($"{nameof(CourtesyFrames)} {courtesyFrames} below 0.");
        public static int    ? AssertCourtesyFrames(int   ? courtesyFrames              , bool strict = false) => courtesyFrames                          ?.                 AssertCourtesyFrames();
        public static AudioFileFormatEnum    AssertAudioFormat  (AudioFileFormatEnum    audioFormat  , bool strict = true ) => audioFormat   .In(ValidAudioFormats   ) ? audioFormat    : throw new Exception($"{nameof(AudioFormatExtensionWishes.AudioFormat)} = {audioFormat} not valid. Supported values: " + Join(", ", ValidAudioFormats));
        public static AudioFileFormatEnum  ? AssertAudioFormat  (AudioFileFormatEnum  ? audioFormat  , bool strict = false) => !Has(audioFormat)                       ? audioFormat    : AssertAudioFormat(audioFormat.Value);
        public static InterpolationTypeEnum  AssertInterpolation(InterpolationTypeEnum  interpolation, bool strict = true ) => interpolation .In(ValidInterpolations ) ? interpolation  : throw new Exception($"{nameof(InterpolationExtensionWishes.Interpolation)} = {interpolation} not valid. Supported values: " + Join(", ", ValidInterpolations));
        public static InterpolationTypeEnum? AssertInterpolation(InterpolationTypeEnum? interpolation, bool strict = false) => !Has(interpolation)                     ? interpolation  : AssertInterpolation(interpolation.Value);

        // Derived Audio Properties
        
        public static double   AssertMaxAmplitude  (double  maxAmplitude                , bool strict = true ) => maxAmplitude  .In(ValidMaxAmplitudes  ) ? maxAmplitude   : throw new Exception($"{nameof(MaxAmplitude)} = {maxAmplitude} not supported. Supported values: " + Join(", ", ValidMaxAmplitudes));
        public static double ? AssertMaxAmplitude  (double? maxAmplitude                , bool strict = false) => !Has(maxAmplitude)                      ? maxAmplitude   : AssertMaxAmplitude(maxAmplitude.Value);
        public static int      AssertFrameSize     (int     frameSize                   , bool strict = true ) => frameSize                          >= 1 ? frameSize      : throw new Exception($"{nameof(FrameSize)} {frameSize} below minimum value of 1.");
        public static int    ? AssertFrameSize     (int   ? frameSize                   , bool strict = false) => !Has(frameSize)                         ? frameSize      : AssertHeaderLength(frameSize.Value);
        public static int      AssertHeaderLength  (int     headerLength                , bool strict = true ) => headerLength  .In(ValidHeaderLengths  ) ? headerLength   : throw new Exception($"{nameof(HeaderLength)} {headerLength} not supported. Supported values: " + Join(", ", ValidHeaderLengths));
        public static int    ? AssertHeaderLength  (int   ? headerLength                , bool strict = false) => headerLength                            ?.                 AssertHeaderLength();
        public static string   AssertFileExtension (string  fileExtension               , bool strict = true ) => fileExtension .In(ValidFileExtensions ) || !Has(fileExtension) ? fileExtension : throw new Exception($"{nameof(FileExtension)} = {fileExtension} not valid. Supported values:" + Join(", ", ValidFileExtensions));
        public static int      AssertCourtesyBytes (int     courtesyBytes               , bool strict = false) => courtesyBytes                      >= 0 ? courtesyBytes  : throw new Exception($"{nameof(CourtesyBytes)} {courtesyBytes} below 0.");
        public static int    ? AssertCourtesyBytes (int   ? courtesyBytes               , bool strict = true ) => courtesyBytes                           ?.                 AssertCourtesyBytes();
        
        // Durations
        
        public static double  AssertAudioLength    (double  audioLength                 , bool strict = true ) => audioLength                         >= 0 ? audioLength    : throw new Exception($"{nameof(AudioLength)} {audioLength} below 0.");
        public static double? AssertAudioLength    (double? audioLength                 , bool strict = false) => !Has(audioLength)                        ? audioLength    : AssertAudioLength(audioLength.Value);
        public static int     AssertFrameCount     (int     frameCount                  , bool strict = true ) => frameCount                          >= 0 ? frameCount     : throw new Exception($"{nameof(FrameCount)} {frameCount} below 0.");
        public static int?    AssertFrameCount     (int   ? frameCount                  , bool strict = false) => !Has(frameCount)                         ? frameCount     : AssertFrameCount(frameCount.Value);
        public static int     AssertByteCount      (int     byteCount                   , bool strict = true ) => byteCount                           >= 0 ? byteCount      : throw new Exception($"{nameof(ByteCount)} {byteCount} below 0.");
        public static int?    AssertByteCount      (int   ? byteCount                   , bool strict = false) => byteCount                                ?.                 AssertByteCount();
    
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
    }
    
    public static class ConfigAssertionExtensionWishes
    {
        // Primary Audio Properties
        
        public static int                    AssertBits          (this int                    bits          ) => ConfigWishes.AssertBits          (bits          );
        public static int                  ? AssertBits          (this int                  ? bits          ) => ConfigWishes.AssertBits          (bits          );
        public static int                    AssertChannels      (this int                    channels      ) => ConfigWishes.AssertChannels      (channels      );
        public static int                  ? AssertChannels      (this int                  ? channels      ) => ConfigWishes.AssertChannels      (channels      );
        public static int                    AssertChannel       (this int                    channel       ) => ConfigWishes.AssertChannel       (channel       );
        public static int                  ? AssertChannel       (this int                  ? channel       ) => ConfigWishes.AssertChannel       (channel       );
        public static int                    AssertSamplingRate  (this int                    samplingRate  ) => ConfigWishes.AssertSamplingRate  (samplingRate  );
        public static int                  ? AssertSamplingRate  (this int                  ? samplingRate  ) => ConfigWishes.AssertSamplingRate  (samplingRate  );
        public static AudioFileFormatEnum    AssertAudioFormat   (this AudioFileFormatEnum    audioFormat   ) => ConfigWishes.AssertAudioFormat   (audioFormat   );
        public static AudioFileFormatEnum  ? AssertAudioFormat   (this AudioFileFormatEnum  ? audioFormat   ) => ConfigWishes.AssertAudioFormat   (audioFormat   );
        public static InterpolationTypeEnum  AssertInterpolation (this InterpolationTypeEnum  interpolation ) => ConfigWishes.AssertInterpolation (interpolation );
        public static InterpolationTypeEnum? AssertInterpolation (this InterpolationTypeEnum? interpolation ) => ConfigWishes.AssertInterpolation (interpolation );
        public static int                    AssertCourtesyFrames(this int                    courtesyFrames) => ConfigWishes.AssertCourtesyFrames(courtesyFrames);
        public static int                  ? AssertCourtesyFrames(this int                  ? courtesyFrames) => ConfigWishes.AssertCourtesyFrames(courtesyFrames);
        
        // Derived
        
        public static int                    AssertSizeOfBitDepth(this int                    sizeOfBitDepth) => ConfigWishes.AssertSizeOfBitDepth(sizeOfBitDepth);
        public static int                  ? AssertSizeOfBitDepth(this int                  ? sizeOfBitDepth) => ConfigWishes.AssertSizeOfBitDepth(sizeOfBitDepth);
        public static double                 AssertMaxAmplitude  (this double                 maxAmplitude  ) => ConfigWishes.AssertMaxAmplitude  (maxAmplitude  );
        public static double               ? AssertMaxAmplitude  (this double               ? maxAmplitude  ) => ConfigWishes.AssertMaxAmplitude  (maxAmplitude  );
        public static int                    AssertFrameSize     (this int                    frameSize     ) => ConfigWishes.AssertFrameSize     (frameSize     );
        public static int                  ? AssertFrameSize     (this int                  ? frameSize     ) => ConfigWishes.AssertFrameSize     (frameSize     );
        public static int                    AssertHeaderLength  (this int                    headerLength  ) => ConfigWishes.AssertHeaderLength  (headerLength  );
        public static int                  ? AssertHeaderLength  (this int                  ? headerLength  ) => ConfigWishes.AssertHeaderLength  (headerLength  );
        public static string                 AssertFileExtension (this string                 fileExtension ) => ConfigWishes.AssertFileExtension (fileExtension );
        public static int                    AssertCourtesyBytes (this int                    courtesyBytes ) => ConfigWishes.AssertCourtesyBytes (courtesyBytes );
        public static int                  ? AssertCourtesyBytes (this int                  ? courtesyBytes ) => ConfigWishes.AssertCourtesyBytes (courtesyBytes );

        // Durations
        
        public static double                 AssertAudioLength   (this double                 audioLength   ) => ConfigWishes.AssertAudioLength   (audioLength   );
        public static double               ? AssertAudioLength   (this double               ? audioLength   ) => ConfigWishes.AssertAudioLength   (audioLength   );
        public static int                    AssertFrameCount    (this int                    frameCount    ) => ConfigWishes.AssertFrameCount    (frameCount    );
        public static int                  ? AssertFrameCount    (this int                  ? frameCount    ) => ConfigWishes.AssertFrameCount    (frameCount    );
        public static int                    AssertByteCount     (this int                    byteCount     ) => ConfigWishes.AssertByteCount     (byteCount     );
        public static int                  ? AssertByteCount     (this int                  ? byteCount     ) => ConfigWishes.AssertByteCount     (byteCount     );
 
        // Misc
        
        public static int AssertCourtesyBytes(this int courtesyBytes, int frameSize) => ConfigWishes.AssertCourtesyBytes(courtesyBytes, frameSize);
        public static int AssertFileSize(this string filePath) => ConfigWishes.AssertFileSize(filePath);
    }
}
