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
        public static int                  [] ValidBits            { get; } = { 8, 16, 32                                          };
        public static int                  [] ValidChannels        { get; } = { 1, 2                                               };
        public static int                  [] ValidChannel         { get; } = { 0, 1                                               };
        public static AudioFileFormatEnum  [] ValidAudioFormats    { get; } = { Raw, Wav                                           };
        public static InterpolationTypeEnum[] ValidInterpolations  { get; } = { Line, Block                                        };
        public static int                  [] ValidSizesOfBitDepth { get; } =   ValidBits        .Select(SizeOfBitDepth).ToArray()  ;
        public static double               [] ValidMaxAmplitudes   { get; } =   ValidBits        .Select(MaxAmplitude  ).ToArray()  ;
        public static int                  [] ValidHeaderLengths   { get; } =   ValidAudioFormats.Select(HeaderLength  ).ToArray()  ;
        public static string               [] ValidFileExtensions  { get; } =   ValidAudioFormats.Select(FileExtension ).ToArray()  ;

        // Primary Audio Properties

        public static int      AssertBits          (int     bits                        ) => bits          .In(ValidBits           ) ? bits           : throw new Exception($"{nameof(Bits)} = {bits} not valid. Supported values: " + Join(", ", ValidBits));
        public static int    ? AssertBits          (int   ? bits                        ) => !Has(bits)                              ? bits           : AssertBits(bits.Value);
        public static int      AssertChannels      (int     channels                    ) => channels      .In(ValidChannels       ) ? channels       : throw new Exception($"{nameof(ChannelsExtensionWishes.Channels)} = {channels} not valid. Supported values: "  + Join(", ", ValidChannels));
        public static int    ? AssertChannels      (int   ? channels                    ) => !Has(channels)                          ? channels       : AssertChannels(channels.Value);
        public static int      AssertChannel       (int     channel                     ) => channel       .In(ValidChannel        ) ? channel        : throw new Exception($"{nameof(ChannelExtensionWishes.Channel)} = {channel} not valid. Supported values: " + Join(", ", ValidChannel));
        public static int    ? AssertChannel       (int   ? channel                     ) => channel                                 ?.                 AssertChannel();
        public static int      AssertSamplingRate  (int     samplingRate                ) => samplingRate                        > 0 ? samplingRate   : throw new Exception($"{nameof(SamplingRateExtensionWishes.SamplingRate)} {samplingRate} below 0.");
        public static int    ? AssertSamplingRate  (int   ? samplingRate                ) => !Has(samplingRate)                      ? samplingRate   : AssertSamplingRate(samplingRate.Value);
        public static AudioFileFormatEnum    AssertAudioFormat  (AudioFileFormatEnum    audioFormat  ) => audioFormat   .In(ValidAudioFormats   ) ? audioFormat    : throw new Exception($"{nameof(AudioFormatExtensionWishes.AudioFormat)} = {audioFormat} not valid. Supported values: " + Join(", ", ValidAudioFormats));
        public static AudioFileFormatEnum  ? AssertAudioFormat  (AudioFileFormatEnum  ? audioFormat  ) => !Has(audioFormat)                       ? audioFormat    : AssertAudioFormat(audioFormat.Value);
        public static InterpolationTypeEnum  AssertInterpolation(InterpolationTypeEnum  interpolation) => interpolation .In(ValidInterpolations ) ? interpolation  : throw new Exception($"{nameof(InterpolationExtensionWishes.Interpolation)} = {interpolation} not valid. Supported values: " + Join(", ", ValidInterpolations));
        public static InterpolationTypeEnum? AssertInterpolation(InterpolationTypeEnum? interpolation) => !Has(interpolation)                     ? interpolation  : AssertInterpolation(interpolation.Value);
        public static int      AssertCourtesyFrames(int     courtesyFrames              ) => courtesyFrames                     >= 0 ? courtesyFrames : throw new Exception($"{nameof(CourtesyFrames)} {courtesyFrames} below 0.");
        public static int    ? AssertCourtesyFrames(int   ? courtesyFrames              ) => courtesyFrames                          ?.                 AssertCourtesyFrames();
        
        // Derived Audio Properties
        
        public static int      AssertSizeOfBitDepth(int     sizeOfBitDepth              ) => sizeOfBitDepth.In(ValidSizesOfBitDepth) ? sizeOfBitDepth : throw new Exception($"{nameof(SizeOfBitDepth)} = {sizeOfBitDepth} not supported. Supported values: " + Join(", ", ValidSizesOfBitDepth));
        public static int    ? AssertSizeOfBitDepth(int   ? sizeOfBitDepth              ) => !Has(sizeOfBitDepth)                    ? sizeOfBitDepth : AssertSizeOfBitDepth(sizeOfBitDepth.Value);
        public static double   AssertMaxAmplitude  (double  maxAmplitude                ) => maxAmplitude  .In(ValidMaxAmplitudes  ) ? maxAmplitude   : throw new Exception($"{nameof(MaxAmplitude)} = {maxAmplitude} not supported. Supported values: " + Join(", ", ValidMaxAmplitudes));
        public static double ? AssertMaxAmplitude  (double? maxAmplitude                ) => !Has(maxAmplitude)                      ? maxAmplitude   : AssertMaxAmplitude(maxAmplitude.Value);
        public static int      AssertFrameSize     (int     frameSize                   ) => frameSize                          >= 1 ? frameSize      : throw new Exception($"{nameof(FrameSize)} {frameSize} below minimum value of 1.");
        public static int    ? AssertFrameSize     (int   ? frameSize                   ) => !Has(frameSize)                         ? frameSize      : AssertHeaderLength(frameSize.Value);
        public static int      AssertHeaderLength  (int     headerLength                ) => headerLength  .In(ValidHeaderLengths  ) ? headerLength   : throw new Exception($"{nameof(HeaderLength)} {headerLength} not supported. Supported values: " + Join(", ", ValidHeaderLengths));
        public static int    ? AssertHeaderLength  (int   ? headerLength                ) => headerLength                            ?.                 AssertHeaderLength();
        public static string   AssertFileExtension (string  fileExtension               ) => fileExtension .In(ValidFileExtensions ) || !Has(fileExtension) ? fileExtension : throw new Exception($"{nameof(FileExtension)} = {fileExtension} not valid. Supported values:" + Join(", ", ValidFileExtensions));
        public static int      AssertCourtesyBytes (int     courtesyBytes               ) => courtesyBytes                      >= 0 ? courtesyBytes  : throw new Exception($"{nameof(CourtesyBytes)} {courtesyBytes} below 0.");
        public static int    ? AssertCourtesyBytes (int   ? courtesyBytes               ) => courtesyBytes                           ?.                 AssertCourtesyBytes();
        
        // Durations
        
        public static double  AssertAudioLength    (double  audioLength                 ) => audioLength                         >= 0 ? audioLength    : throw new Exception($"{nameof(AudioLength)} {audioLength} below 0.");
        public static double? AssertAudioLength    (double? audioLength                 ) => !Has(audioLength)                        ? audioLength    : AssertAudioLength(audioLength.Value);
        public static int     AssertFrameCount     (int     frameCount                  ) => frameCount                          >= 0 ? frameCount     : throw new Exception($"{nameof(FrameCount)} {frameCount} below 0.");
        public static int?    AssertFrameCount     (int   ? frameCount                  ) => !Has(frameCount)                         ? frameCount     : AssertFrameCount(frameCount.Value);
        public static int     AssertByteCount      (int     byteCount                   ) => byteCount                           >= 0 ? byteCount      : throw new Exception($"{nameof(ByteCount)} {byteCount} below 0.");
        public static int?    AssertByteCount      (int   ? byteCount                   ) => byteCount                                ?.                 AssertByteCount();
    
        // Misc

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
