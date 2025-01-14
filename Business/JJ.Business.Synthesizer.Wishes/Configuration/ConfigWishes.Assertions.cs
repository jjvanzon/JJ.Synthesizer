using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Wishes.Common;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Framework.Wishes.Common.FilledInWishes;

namespace JJ.Business.Synthesizer.Wishes.Configuration
{
    public partial class ConfigWishes
    {
        // Primary Audio Properties
        
        public static int      AssertBits          (int     bits                        ) => bits.In(32, 16, 8)            ? bits           : throw new Exception($"{nameof(Bits)} = {bits} not valid. Supported values: 8, 16, 32.");
        public static int    ? AssertBits          (int   ? bits                        ) => !Has(bits)                    ? bits           : AssertBits(bits.Value);
        public static int      AssertChannels      (int     channels                    ) => channels.In(1, 2)             ? channels       : throw new Exception($"Channels = {channels} not valid. Supported values: 1, 2."); 
        public static int    ? AssertChannels      (int   ? channels                    ) => !Has(channels)                ? channels       : AssertChannels(channels.Value);
        public static int      AssertChannel       (int     channel                     ) => channel.In(0, 1)              ? channel        :  throw new Exception($"Channel = {channel} not valid. Supported values: 0, 1."); 
        public static int    ? AssertChannel       (int   ? channel                     ) => channel                       ?.                 AssertChannel();
        public static int      AssertSamplingRate  (int     samplingRate                ) => samplingRate              > 0 ? samplingRate   : throw new Exception($"{nameof(samplingRate)} {samplingRate} below 0.");
        public static int    ? AssertSamplingRate  (int   ? samplingRate                ) => !Has(samplingRate)            ? samplingRate   : AssertSamplingRate(samplingRate.Value);
        public static AudioFileFormatEnum    Assert(AudioFileFormatEnum    audioFormat  ) => audioFormat.In(Raw, Wav)      ? audioFormat    : throw new Exception($"AudioFormat = {audioFormat} not valid. Supported values: {Raw}, {Wav}.");
        public static AudioFileFormatEnum  ? Assert(AudioFileFormatEnum  ? audioFormat  ) => !Has(audioFormat)             ? audioFormat    : Assert(audioFormat.Value);
        public static InterpolationTypeEnum  Assert(InterpolationTypeEnum  interpolation) => interpolation.In(Line, Block) ? interpolation  : throw new Exception($"Interpolation = {interpolation} not valid. Supported values: {Line}, {Block}"); 
        public static InterpolationTypeEnum? Assert(InterpolationTypeEnum? interpolation) => !Has(interpolation)           ? interpolation  : Assert(interpolation.Value);
        public static int      AssertCourtesyFrames(int     courtesyFrames              ) => courtesyFrames           >= 0 ? courtesyFrames : throw new Exception($"{nameof(CourtesyFrames)} {courtesyFrames} below 0.");
        public static int    ? AssertCourtesyFrames(int   ? courtesyFrames              ) => courtesyFrames                ?.                 AssertCourtesyFrames();
        
        // Derived
        public static int      AssertSizeOfBitDepth(int     sizeOfBitDepth              ) => sizeOfBitDepth.In(1, 2, 4)    ? sizeOfBitDepth : throw new Exception($"SizeOfBitDepth = {sizeOfBitDepth} not supported. Supported values: 1, 2, 4.");
        public static int    ? AssertSizeOfBitDepth(int   ? sizeOfBitDepth              ) => !Has(sizeOfBitDepth)          ? sizeOfBitDepth : AssertSizeOfBitDepth(sizeOfBitDepth.Value);
        public static double   AssertMaxAmplitude  (double  maxAmplitude                ) => maxAmplitude.In(MaxAmplitude(8), MaxAmplitude(16), MaxAmplitude(32)) ? maxAmplitude : throw new Exception($"{nameof(MaxAmplitude)} = {maxAmplitude} not supported. Supported values: {MaxAmplitude(8)}, {MaxAmplitude(16)}, {MaxAmplitude(32)}");
        public static double ? AssertMaxAmplitude  (double? maxAmplitude                ) => !Has(maxAmplitude)            ? maxAmplitude   : AssertMaxAmplitude(maxAmplitude.Value);
        public static int      AssertFrameSize     (int     frameSize                   ) => frameSize                >= 1 ? frameSize      : throw new Exception($"{nameof(FrameSize)} {frameSize} below minimum value of 1.");
        public static int    ? AssertFrameSize     (int   ? frameSize                   ) => !Has(frameSize)               ? frameSize      : AssertHeaderLength(frameSize.Value);
        public static int      AssertHeaderLength  (int     headerLength                ) => headerLength.In(0, 44)        ? headerLength   : throw new Exception($"{nameof(HeaderLength)} {headerLength} not supported.");
        public static int    ? AssertHeaderLength  (int   ? headerLength                ) => headerLength                  ?.                 AssertHeaderLength();
        public static int      AssertCourtesyBytes (int     courtesyBytes               ) => courtesyBytes            >= 0 ? courtesyBytes  : throw new Exception($"{nameof(CourtesyBytes)} {courtesyBytes} below 0.");
        public static int    ? AssertCourtesyBytes (int   ? courtesyBytes               ) => courtesyBytes                 ?.                 AssertCourtesyBytes();
        public static string   AssertFileExtension (string  fileExtension               ) => fileExtension.In(".raw", ".wav") || !Has(fileExtension) ? fileExtension : throw new Exception($"{nameof(FileExtension)} = {fileExtension} not valid. Supported values: .raw, .wav");

        // Durations
        
        public static double  AssertAudioLength    (double  audioLength                 ) => audioLength              >= 0 ? audioLength    : throw new Exception($"{nameof(AudioLength)} {audioLength} below 0.");
        public static double? AssertAudioLength    (double? audioLength                 ) => !Has(audioLength)             ? audioLength    : AssertAudioLength(audioLength.Value);
        public static int     AssertFrameCount     (int     frameCount                  ) => frameCount               >= 0 ? frameCount     : throw new Exception($"{nameof(FrameCount)} {frameCount} below 0.");
        public static int?    AssertFrameCount     (int   ? frameCount                  ) => !Has(frameCount)              ? frameCount     : AssertFrameCount(frameCount.Value);
        public static int     AssertByteCount      (int     byteCount                   ) => byteCount                >= 0 ? byteCount      : throw new Exception($"{nameof(ByteCount)} {byteCount} below 0.");
        public static int?    AssertByteCount      (int   ? byteCount                   ) => byteCount                     ?.                 AssertByteCount();
    }
    
    public static class ConfigAssertionExtensionWishes
    {
        public static int                    AssertBits          (this int                    bits          ) => ConfigWishes.AssertBits          (bits          );
        public static int                  ? AssertBits          (this int                  ? bits          ) => ConfigWishes.AssertBits          (bits          );
        public static int                    AssertChannels      (this int                    channels      ) => ConfigWishes.AssertChannels      (channels      );
        public static int                  ? AssertChannels      (this int                  ? channels      ) => ConfigWishes.AssertChannels      (channels      );
        public static int                    AssertChannel       (this int                    channel       ) => ConfigWishes.AssertChannel       (channel       );
        public static int                  ? AssertChannel       (this int                  ? channel       ) => ConfigWishes.AssertChannel       (channel       );
        public static int                    AssertSamplingRate  (this int                    samplingRate  ) => ConfigWishes.AssertSamplingRate  (samplingRate  );
        public static int                  ? AssertSamplingRate  (this int                  ? samplingRate  ) => ConfigWishes.AssertSamplingRate  (samplingRate  );
        public static AudioFileFormatEnum    Assert              (this AudioFileFormatEnum    audioFormat   ) => ConfigWishes.Assert              (audioFormat   );
        public static AudioFileFormatEnum  ? Assert              (this AudioFileFormatEnum  ? audioFormat   ) => ConfigWishes.Assert              (audioFormat   );
        public static InterpolationTypeEnum  Assert              (this InterpolationTypeEnum  interpolation ) => ConfigWishes.Assert              (interpolation );
        public static InterpolationTypeEnum? Assert              (this InterpolationTypeEnum? interpolation ) => ConfigWishes.Assert              (interpolation );
        public static int                    AssertCourtesyFrames(this int                    courtesyFrames) => ConfigWishes.AssertCourtesyFrames(courtesyFrames);
        public static int                  ? AssertCourtesyFrames(this int                  ? courtesyFrames) => ConfigWishes.AssertCourtesyFrames(courtesyFrames);
        public static double                 AssertAudioLength   (this double                 audioLength   ) => ConfigWishes.AssertAudioLength   (audioLength   );
        public static double               ? AssertAudioLength   (this double               ? audioLength   ) => ConfigWishes.AssertAudioLength   (audioLength   );
        public static int                    AssertFrameCount    (this int                    frameCount    ) => ConfigWishes.AssertFrameCount    (frameCount    );
        public static int                  ? AssertFrameCount    (this int                  ? frameCount    ) => ConfigWishes.AssertFrameCount    (frameCount    );
        public static int                    AssertByteCount     (this int                    byteCount     ) => ConfigWishes.AssertByteCount     (byteCount     );
        public static int                  ? AssertByteCount     (this int                  ? byteCount     ) => ConfigWishes.AssertByteCount     (byteCount     );
        public static int                    AssertSizeOfBitDepth(this int                    sizeOfBitDepth) => ConfigWishes.AssertSizeOfBitDepth(sizeOfBitDepth);
        public static int                  ? AssertSizeOfBitDepth(this int                  ? sizeOfBitDepth) => ConfigWishes.AssertSizeOfBitDepth(sizeOfBitDepth);
        public static double                 AssertMaxAmplitude  (this double                 maxAmplitude  ) => ConfigWishes.AssertMaxAmplitude  (maxAmplitude  );
        public static double               ? AssertMaxAmplitude  (this double               ? maxAmplitude  ) => ConfigWishes.AssertMaxAmplitude  (maxAmplitude  );
        public static int                    AssertFrameSize     (this int                    frameSize     ) => ConfigWishes.AssertFrameSize     (frameSize     );
        public static int                  ? AssertFrameSize     (this int                  ? frameSize     ) => ConfigWishes.AssertFrameSize     (frameSize     );
        public static int                    AssertHeaderLength  (this int                    headerLength  ) => ConfigWishes.AssertHeaderLength  (headerLength  );
        public static int                  ? AssertHeaderLength  (this int                  ? headerLength  ) => ConfigWishes.AssertHeaderLength  (headerLength  );
        public static int                    AssertCourtesyBytes (this int                    courtesyBytes ) => ConfigWishes.AssertCourtesyBytes (courtesyBytes );
        public static int                  ? AssertCourtesyBytes (this int                  ? courtesyBytes ) => ConfigWishes.AssertCourtesyBytes (courtesyBytes );
        public static string                 AssertFileExtension (this string                 fileExtension ) => ConfigWishes.AssertFileExtension (fileExtension );
    }
}
